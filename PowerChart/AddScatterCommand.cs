using System;
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

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "Single Point")]
        [ValidateNotNull]
        public Int32 XCoordinate { get; set; }

        [Parameter(Mandatory = true, Position = 2, ParameterSetName = "Single Point")]
        [ValidateNotNull]
        public Int32 YCoordinate { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            Chart?.AddScatterPoint(XCoordinate, YCoordinate);
        }
    }
}
