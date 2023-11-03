using System;
using System.Management.Automation;
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
        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            var form = new PowerForm();
            new Thread(new ThreadStart(() => form.ShowDialog())).Start();
        }
    }
}
