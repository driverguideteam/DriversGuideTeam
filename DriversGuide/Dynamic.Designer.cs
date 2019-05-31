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
            this.ChUrb.Name = "ChUrb";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ChUrb.Series.Add(series1);
            this.ChUrb.Size = new System.Drawing.Size(310, 446);
            this.ChUrb.TabIndex = 0;
            this.ChUrb.Text = "chart1";
            // 
            // ChRur
            // 
            chartArea2.Name = "ChartArea1";
            this.ChRur.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.ChRur.Legends.Add(legend2);
            this.ChRur.Location = new System.Drawing.Point(311, -3);
            this.ChRur.Name = "ChRur";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.ChRur.Series.Add(series2);
            this.ChRur.Size = new System.Drawing.Size(310, 446);
            this.ChRur.TabIndex = 1;
            this.ChRur.Text = "chart2";
            // 
            // ChMw
            // 
            chartArea3.Name = "ChartArea1";
            this.ChMw.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.ChMw.Legends.Add(legend3);
            this.ChMw.Location = new System.Drawing.Point(622, 0);
            this.ChMw.Name = "ChMw";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.ChMw.Series.Add(series3);
            this.ChMw.Size = new System.Drawing.Size(310, 446);
            this.ChMw.TabIndex = 2;
            this.ChMw.Text = "chart3";
            // 
            // Dynamic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChMw);
            this.Controls.Add(this.ChRur);
            this.Controls.Add(this.ChUrb);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Dynamic";
            this.Size = new System.Drawing.Size(932, 446);
            this.Load += new System.EventHandler(this.Dynamic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChUrb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChRur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChMw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChUrb;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChRur;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChMw;
    }
}
