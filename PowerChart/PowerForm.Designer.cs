using System.Drawing;
using System.Windows.Forms;

namespace PowerChart
{
    partial class PowerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlMain = new Panel();
            lblYAxis = new Label();
            lblXAxis = new Label();
            lblTitle = new Label();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(lblYAxis);
            pnlMain.Controls.Add(lblXAxis);
            pnlMain.Controls.Add(lblTitle);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(800, 450);
            pnlMain.TabIndex = 0;
            pnlMain.Paint += pnlMain_Paint;
            // 
            // lblYAxis
            // 
            lblYAxis.AutoSize = true;
            lblYAxis.Location = new Point(3, 200);
            lblYAxis.Name = "lblYAxis";
            lblYAxis.Size = new Size(50, 20);
            lblYAxis.TabIndex = 2;
            lblYAxis.Text = "Y-Axis";
            // 
            // lblXAxis
            // 
            lblXAxis.AutoSize = true;
            lblXAxis.Location = new Point(332, 407);
            lblXAxis.Name = "lblXAxis";
            lblXAxis.Size = new Size(51, 20);
            lblXAxis.TabIndex = 1;
            lblXAxis.Text = "X-Axis";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(345, 31);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(38, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Title";
            // 
            // PowerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlMain);
            Name = "PowerForm";
            Text = "PowerForm";
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlMain;
        private Label lblYAxis;
        private Label lblXAxis;
        private Label lblTitle;
    }
}