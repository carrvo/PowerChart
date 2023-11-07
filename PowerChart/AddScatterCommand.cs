using System;
using System.Linq;
using System.Management.Automation;

namespace PowerChart
{
    /// <summary>
    /// <para type="synopsis">
    /// Adds scatter points to the chart.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "Scatter")]
    public sealed class AddScatterCommand : PSCmdlet
    {
        /// <summary>
        /// <para type="description">
        /// Adding to <see cref="PowerForm"/>.
        /// </para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        [ValidateNotNull]
        public PowerForm? Chart { get; set; }

        /// <summary>
        /// X coordinate for a point on the chart.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Single Point")]
        [ValidateNotNull]
        public Int32 XCoordinate { get; set; }

        /// <summary>
        /// Y coordinate for a point on the chart.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "Single Point")]
        [ValidateNotNull]
        public Int32 YCoordinate { get; set; }

        /// <summary>
        /// <see cref="PSObject"/> whose properties are used for charting.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = "Properties")]
        [ValidateNotNull]
        public PSObject InputObject { get; set; } = new();

        /// <summary>
        /// <see cref="InputObject"/> property to use as an x coordinate for a point on the chart.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Properties")]
        [ValidateNotNull]
        public String XProperty { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="InputObject"/> property to use as an y coordinate for a point on the chart.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "Properties")]
        [ValidateNotNull]
        public String YProperty { get; set; } = String.Empty;

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            try
            {
                switch (ParameterSetName)
                {
                    case "Properties":
                        XCoordinate = CastProperty(InputObject.Properties.Single(x => x.Name == XProperty));
                        YCoordinate = CastProperty(InputObject.Properties.Single(x => x.Name == YProperty));
                        break;
                    default:
                        break;
                }
                Chart?.AddScatterPoint(XCoordinate, YCoordinate);
            }
            catch (ArgumentException ex)
            {
                WriteError(new ErrorRecord(
                    ex,
                    "Casting InputObject Property",
                    ErrorCategory.ParserError,
                    InputObject
                ));
            }
        }

        private static Int32 CastProperty(PSPropertyInfo property)
        {
            if (property.TypeNameOfValue == typeof(Int32).FullName)
            {
                return (Int32)property.Value;
            }
            else if (property.Value is PSObject psInt && psInt.BaseObject.GetType().FullName == typeof(Int32).FullName)
            {
                return (Int32)psInt.BaseObject;
            }
            else if (property.Value is PSObject psDouble && psDouble.BaseObject.GetType().FullName == typeof(Double).FullName)
            {
                Double d = (Double)psDouble.BaseObject;
                return (Int32)d;
            }
            else
            {
                throw new ArgumentException($"Property value of {property.Name} is unsupported type {property.TypeNameOfValue} ({property.Value?.GetType()?.FullName}).");
            }
        }
    }
}
