using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PowerChart
{
    /// <summary>
    /// Form to hold the Chart.
    /// </summary>
    public partial class PowerForm : Form
    {
        internal IList<DataSeries> Series { get; }

        /// <summary>
        /// Reference to the <see cref="Thread"/> that the dialog box is running on.
        /// </summary>
        public Thread Dialog { get; internal set; }

        /// <summary>
        /// Title of the Chart.
        /// </summary>
        public String Title
        {
            get => lblTitle.Text;
            set => lblTitle.Text = value;
        }

        /// <summary>
        /// Label for x-axis.
        /// </summary>
        public String XAxisLabel
        {
            get => lblXAxis.Text;
            set => lblXAxis.Text = value;
        }

        /// <summary>
        /// Label for y-axis.
        /// </summary>
        public String YAxisLabel
        {
            get => lblYAxis.Text;
            set => lblYAxis.Text = value;
        }

        /// <summary>
        /// The <see cref="Color"/> of the axis lines.
        /// </summary>
        public Color AxisColor { get; set; } = Color.Black;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PowerForm()
        {
            InitializeComponent();
            Series = new List<DataSeries>();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(AxisColor);
            pen.Width = 2;

            Int32 axisOrigin = lblYAxis.Width + 10;
            Int32 axisYEnd = pnlMain.Height - axisOrigin;
            Int32 axisXEnd = pnlMain.Width - axisOrigin;
            Int32 axisHeight = axisYEnd - axisOrigin;
            Int32 axisWidth = axisXEnd - axisOrigin;

            // y axis
            g.DrawLine(pen, new Point(axisOrigin, axisOrigin), new Point(axisOrigin, axisYEnd));

            // x axis
            g.DrawLine(pen, new Point(axisOrigin, axisYEnd), new Point(axisXEnd, axisYEnd));

            Int32 xOverallMin = Series.Any() ? Series.Min(x => x.xMin) : 0;
            Int32 yOverallMin = Series.Any() ? Series.Min(x => x.yMin) : 0;
            Int32 xOverallRange = Series.Any() ? Series.Max(x => x.xMax) - xOverallMin : 1;
            Int32 yOverallRange = Series.Any() ? Series.Max(x => x.yMax) - yOverallMin : 1;

            foreach (var series in Series)
            {
                lock (series.DataPoints)
                {
                    foreach (var point in series.DataPoints)
                    {
                        // Get the data scaled to the data itself (across all series)
                        Double xScale = (Double)(point.X - xOverallMin) / xOverallRange;
                        Double yScale = (Double)(point.Y - yOverallMin) / yOverallRange;
                        // Convert and scale to the drawing
                        Int32 x = (Int32)(xScale * axisWidth);
                        Int32 y = (Int32)(yScale * axisHeight);

                        Point chartPoint = new(x + axisOrigin, axisYEnd - y);
                        series.Draw(g, new Rectangle(chartPoint, new Size(2, 2)));
                    }
                }
            }
        }
    }
}
