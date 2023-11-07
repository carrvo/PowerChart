using System;
using System.Management.Automation;
using System.Threading;
using System.Windows.Forms;

namespace PowerChart
{
    /// <summary>
    /// <para type="synopsis">
    /// Displays the chart.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.Show, "Chart")]
    public sealed class ShowChartCommand : PSCmdlet
    {
        /// <summary>
        /// <para type="description">
        /// <see cref="PowerForm"/> to display.
        /// </para>
        /// </summary>
        [Parameter(Mandatory=true, Position=0)]
        [ValidateNotNull]
        public PowerForm? Chart { get; set; }

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            new Thread(new ThreadStart(() => Chart?.ShowDialog())).Start();
        }
    }
}
