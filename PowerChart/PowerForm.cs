using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerChart
{
    /// <summary>
    /// Form to hold the Chart.
    /// </summary>
    public partial class PowerForm : Form
    {
        IList<Point> ScatterPoints { get; }

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
        /// Constructor.
        /// </summary>
        public PowerForm()
        {
            InitializeComponent();
            ScatterPoints = new List<Point>();
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.LimeGreen);
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

            var stats = GetDataStatistics(ScatterPoints);

            foreach (var point in ScatterPoints)
            {
                // Get the data scaled to the data itself
                Double xScale = (Double)(point.X - stats.xMin) / stats.xRange;
                Double yScale = (Double)(point.Y - stats.yMin) / stats.yRange;
                // Convert and scale to the drawing
                Int32 x = (Int32)(xScale * axisWidth);
                Int32 y = (Int32)(yScale * axisHeight);

                Point chartPoint = new(x + axisOrigin, axisYEnd - y);
                g.DrawEllipse(pen, new Rectangle(chartPoint, new Size(2, 2)));
            }
        }

        private (Int32 xMin, Int32 xMax, Int32 xRange, Int32 yMin, Int32 yMax, Int32 yRange) GetDataStatistics(IEnumerable<Point> dataseries)
        {
            (Int32 xMin, Int32 xMax, Int32 xRange, Int32 yMin, Int32 yMax, Int32 yRange) stats = new(Int32.MaxValue, Int32.MinValue, 0, Int32.MaxValue, Int32.MinValue, 0);
            foreach (var point in dataseries)
            {
                if (point.X < stats.xMin) stats.xMin = point.X;
                if (point.X > stats.xMax) stats.xMax = point.X;
                if (point.Y < stats.yMin) stats.yMin = point.Y;
                if (point.Y > stats.yMax) stats.yMax = point.Y;
            }
            stats.xRange = stats.xMax - stats.xMin;
            stats.yRange = stats.yMax - stats.yMin;
            return stats;
        }

        internal void AddScatterPoint(Int32 x, Int32 y)
        {
            ScatterPoints.Add(new Point(x, y));
        }
    }
}
