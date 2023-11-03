using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerChart
{
    /// <summary>
    /// Form to hold the Chart.
    /// </summary>
    public partial class PowerForm : Form
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public PowerForm()
        {
            InitializeComponent();
        }

        private void PowerForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.LimeGreen);
            pen.Width = 2;
            Point P1 = new Point(0, this.Height);
            Point P2 = new Point(this.Width, 0);

            g.DrawLine(pen, P1, P2);
        }
    }
}
