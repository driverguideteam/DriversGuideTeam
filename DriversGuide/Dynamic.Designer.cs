namespace DriversGuide
{
    partial class Dynamic
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend7 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend8 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ChUrb = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChRur = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ChMw = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblUrb = new System.Windows.Forms.Label();
            this.lblRur = new System.Windows.Forms.Label();
            this.lblMw = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ChUrb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChRur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChMw)).BeginInit();
            this.SuspendLayout();
            // 
            // ChUrb
            // 
            chartArea7.Name = "ChartArea1";
            this.ChUrb.ChartAreas.Add(chartArea7);
            legend7.Name = "Legend1";
            this.ChUrb.Legends.Add(legend7);
            this.ChUrb.Location = new System.Drawing.Point(0, 0);
            this.ChUrb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChUrb.Name = "ChUrb";
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Series1";
            this.ChUrb.Series.Add(series7);
            this.ChUrb.Size = new System.Drawing.Size(309, 446);
            this.ChUrb.TabIndex = 0;
            this.ChUrb.Text = "chart1";
            this.ChUrb.Click += new System.EventHandler(this.ChUrb_Click);
            this.ChUrb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChUrb_MouseMove);
            // 
            // ChRur
            // 
            chartArea8.Name = "ChartArea1";
            this.ChRur.ChartAreas.Add(chartArea8);
            legend8.Name = "Legend1";
            this.ChRur.Legends.Add(legend8);
            this.ChRur.Location = new System.Drawing.Point(311, -2);
            this.ChRur.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChRur.Name = "ChRur";
            series8.ChartArea = "ChartArea1";
            series8.Legend = "Legend1";
            series8.Name = "Series1";
            this.ChRur.Series.Add(series8);
            this.ChRur.Size = new System.Drawing.Size(309, 446);
            this.ChRur.TabIndex = 1;
            this.ChRur.Text = "chart2";
            this.ChRur.Click += new System.EventHandler(this.ChRur_Click);
            this.ChRur.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChRur_MouseMove);
            // 
            // ChMw
            // 
            chartArea9.Name = "ChartArea1";
            this.ChMw.ChartAreas.Add(chartArea9);
            legend9.Name = "Legend1";
            this.ChMw.Legends.Add(legend9);
            this.ChMw.Location = new System.Drawing.Point(619, -2);
            this.ChMw.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChMw.Name = "ChMw";
            series9.ChartArea = "ChartArea1";
            series9.Legend = "Legend1";
            series9.Name = "Series1";
            this.ChMw.Series.Add(series9);
            this.ChMw.Size = new System.Drawing.Size(315, 449);
            this.ChMw.TabIndex = 2;
            this.ChMw.Text = "chart3";
            this.ChMw.Click += new System.EventHandler(this.ChMw_Click);
            this.ChMw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChMw_MouseMove);
            // 
            // lblUrb
            // 
            this.lblUrb.AutoSize = true;
            this.lblUrb.Location = new System.Drawing.Point(213, 410);
            this.lblUrb.Name = "lblUrb";
            this.lblUrb.Size = new System.Drawing.Size(45, 17);
            this.lblUrb.TabIndex = 3;
            this.lblUrb.Text = "lblUrb";
            // 
            // lblRur
            // 
            this.lblRur.AutoSize = true;
            this.lblRur.Location = new System.Drawing.Point(531, 410);
            this.lblRur.Name = "lblRur";
            this.lblRur.Size = new System.Drawing.Size(45, 17);
            this.lblRur.TabIndex = 4;
            this.lblRur.Text = "lblRur";
            // 
            // lblMw
            // 
            this.lblMw.AutoSize = true;
            this.lblMw.Location = new System.Drawing.Point(820, 410);
            this.lblMw.Name = "lblMw";
            this.lblMw.Size = new System.Drawing.Size(42, 17);
            this.lblMw.TabIndex = 5;
            this.lblMw.Text = "lblMw";
            // 
            // Dynamic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMw);
            this.Controls.Add(this.lblRur);
            this.Controls.Add(this.lblUrb);
            this.Controls.Add(this.ChMw);
            this.Controls.Add(this.ChRur);
            this.Controls.Add(this.ChUrb);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Dynamic";
            this.Size = new System.Drawing.Size(932, 446);
            this.Load += new System.EventHandler(this.Dynamic_Load);
            this.SizeChanged += new System.EventHandler(this.Dynamic_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ChUrb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChRur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChMw)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChUrb;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChRur;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChMw;
        private System.Windows.Forms.Label lblUrb;
        private System.Windows.Forms.Label lblRur;
        private System.Windows.Forms.Label lblMw;
    }
}
