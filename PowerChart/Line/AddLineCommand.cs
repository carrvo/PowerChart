using PowerChart.Scatter;
using System;
using System.Drawing;
using System.Management.Automation;

namespace PowerChart.Line
{
    /// <summary>
    /// <para type="synopsis">
    /// Adds lines to the chart.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.Add, "Line")]
    public sealed class AddLineCommand : PSCmdlet
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
        /// X coordinate for the next point on the chart.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Single Point")]
        [ValidateNotNull]
        public Int32 XCoordinate { get; set; }

        /// <summary>
        /// Y coordinate for the next point on the chart.
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
        /// <see cref="InputObject"/> property to use as an x coordinate for the next point on the chart.
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Properties")]
        [ValidateNotNull]
        public String XProperty { get; set; } = String.Empty;

        /// <summary>
        /// <see cref="InputObject"/> property to use as an y coordinate for the next point on the chart.
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "Properties")]
        [ValidateNotNull]
        public String YProperty { get; set; } = String.Empty;

        /// <summary>
        /// <para type="description"><see cref="Color"/> to draw with.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 3)]
        [ValidateNotNull]
        public Pen Color { get; set; } = new(NextColor);

        /// <summary>
        /// <para type="description">Set the size of the <see cref="Pen.Width"/>.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 3)]
        [ValidateNotNull]
        public Single Size { get; set; } = default;

        private static Int32 KnownColorMax = Enum.GetValues(typeof(KnownColor)).Length;
        private static Random ColorPicker = new((Int32)DateTime.Now.Ticks);
        private static Color NextColor => System.Drawing.Color.FromKnownColor((KnownColor)ColorPicker.NextInt64(0, KnownColorMax));

        private LineSeries? Series { get; set; } = null;

        /// <inheritdoc/>
        protected override void BeginProcessing()
        {
            if (Size != default)
            {
                Color.Width = Size;
            }
            Series = new(Color);
            Chart?.Series?.Add(Series);
        }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            try
            {
                switch (ParameterSetName)
                {
                    case "Properties":
                        XCoordinate = CastProperty(InputObject.Properties[XProperty]);
                        YCoordinate = CastProperty(InputObject.Properties[YProperty]);
                        break;
                    default:
                        break;
                }
                Series?.AddPoint(XCoordinate, YCoordinate);
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
            if (property.Value is null)
            {
                throw new ArgumentException($"Property value of {property.Name} is null.");
            }

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
                throw new ArgumentException($"Property value of {property.Name} is unsupported type {property.TypeNameOfValue} ({property.Value?.GetType()?.FullName}): {property.Value}");
            }
        }
    }
}
