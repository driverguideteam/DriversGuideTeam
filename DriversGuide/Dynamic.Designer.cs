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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            chartArea1.Name = "ChartArea1";
            this.ChUrb.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.ChUrb.Legends.Add(legend1);
            this.ChUrb.Location = new System.Drawing.Point(0, 0);
            this.ChUrb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChUrb.Name = "ChUrb";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ChUrb.Series.Add(series1);
            this.ChUrb.Size = new System.Drawing.Size(232, 362);
            this.ChUrb.TabIndex = 0;
            this.ChUrb.Text = "chart1";
            this.ChUrb.Click += new System.EventHandler(this.ChUrb_Click);
            this.ChUrb.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChUrb_MouseMove);
            // 
            // ChRur
            // 
            chartArea2.Name = "ChartArea1";
            this.ChRur.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.ChRur.Legends.Add(legend2);
            this.ChRur.Location = new System.Drawing.Point(233, -2);
            this.ChRur.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChRur.Name = "ChRur";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.ChRur.Series.Add(series2);
            this.ChRur.Size = new System.Drawing.Size(232, 362);
            this.ChRur.TabIndex = 1;
            this.ChRur.Text = "chart2";
            this.ChRur.Click += new System.EventHandler(this.ChRur_Click);
            this.ChRur.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChRur_MouseMove);
            // 
            // ChMw
            // 
            chartArea3.Name = "ChartArea1";
            this.ChMw.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.ChMw.Legends.Add(legend3);
            this.ChMw.Location = new System.Drawing.Point(464, -2);
            this.ChMw.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChMw.Name = "ChMw";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.ChMw.Series.Add(series3);
            this.ChMw.Size = new System.Drawing.Size(236, 365);
            this.ChMw.TabIndex = 2;
            this.ChMw.Text = "chart3";
            this.ChMw.Click += new System.EventHandler(this.ChMw_Click);
            this.ChMw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChMw_MouseMove);
            // 
            // lblUrb
            // 
            this.lblUrb.AutoSize = true;
            this.lblUrb.Location = new System.Drawing.Point(176, 333);
            this.lblUrb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUrb.Name = "lblUrb";
            this.lblUrb.Size = new System.Drawing.Size(35, 13);
            this.lblUrb.TabIndex = 3;
            this.lblUrb.Text = "label1";
            // 
            // lblRur
            // 
            this.lblRur.AutoSize = true;
            this.lblRur.Location = new System.Drawing.Point(398, 333);
            this.lblRur.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRur.Name = "lblRur";
            this.lblRur.Size = new System.Drawing.Size(35, 13);
            this.lblRur.TabIndex = 4;
            this.lblRur.Text = "label1";
            // 
            // lblMw
            // 
            this.lblMw.AutoSize = true;
            this.lblMw.Location = new System.Drawing.Point(634, 333);
            this.lblMw.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMw.Name = "lblMw";
            this.lblMw.Size = new System.Drawing.Size(35, 13);
            this.lblMw.TabIndex = 5;
            this.lblMw.Text = "label1";
            // 
            // Dynamic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMw);
            this.Controls.Add(this.lblRur);
            this.Controls.Add(this.lblUrb);
            this.Controls.Add(this.ChMw);
            this.Controls.Add(this.ChRur);
            this.Controls.Add(this.ChUrb);
            this.Name = "Dynamic";
            this.Size = new System.Drawing.Size(699, 362);
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
