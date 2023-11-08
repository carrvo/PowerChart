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
            Int32 axisHeight = pnlMain.Height - axisOrigin;
            Int32 axisWidth = pnlMain.Width - axisOrigin;

            // y axis
            g.DrawLine(pen, new Point(axisOrigin, axisOrigin), new Point(axisOrigin, axisHeight));

            // x axis
            g.DrawLine(pen, new Point(axisOrigin, axisHeight), new Point(axisWidth, axisHeight));

            foreach (var point in ScatterPoints)
            {
                Point chartPoint = new(point.X + axisOrigin, axisHeight - point.Y);
                g.DrawEllipse(pen, new Rectangle(chartPoint, new Size(2, 2)));
            }
        }

        internal void AddScatterPoint(Int32 x, Int32 y)
        {
            ScatterPoints.Add(new Point(x, y));
        }
    }
}
