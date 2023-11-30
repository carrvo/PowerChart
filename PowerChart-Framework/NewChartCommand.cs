using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PowerChart
{
    /// <summary>
    /// <para type="synopsis">
    /// Creates a chart.
    /// </para>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "Chart")]
    public sealed class NewChartCommand : PSCmdlet
    {
        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            var form = new PowerForm();
            WriteObject(form);
        }
    }
}
