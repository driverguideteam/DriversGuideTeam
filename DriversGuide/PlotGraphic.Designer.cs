namespace DriversGuide
{
    partial class PlotGraphic
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tmrTimeSpan = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblPos1 = new System.Windows.Forms.Label();
            this.lblPos2 = new System.Windows.Forms.Label();
            this.lblPos3 = new System.Windows.Forms.Label();
            this.lblPos4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrTimeSpan
            // 
            this.tmrTimeSpan.Interval = 1000;
            this.tmrTimeSpan.Tick += new System.EventHandler(this.tmrTimeSpan_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1182, 553);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // chart2
            // 
            this.chart2.AllowDrop = true;
            this.chart2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea1.CursorX.Interval = 0.1D;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.Interval = 0.1D;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "TestDaten1";
            this.chart2.ChartAreas.Add(chartArea1);
            this.chart2.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chart2.DataSource = this.dataSet1BindingSource;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chart2.Legends.Add(legend1);
            this.chart2.Location = new System.Drawing.Point(602, 0);
            this.chart2.Name = "chart2";
            series1.ChartArea = "TestDaten1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "TestDaten1";
            this.chart2.Series.Add(series1);
            this.chart2.Size = new System.Drawing.Size(580, 275);
            this.chart2.TabIndex = 5;
            this.chart2.Text = "chart2";
            this.chart2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart2_MouseMove_1);
            // 
            // chart1
            // 
            this.chart1.AllowDrop = true;
            this.chart1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chart1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea2.CursorX.Interval = 0.1D;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.CursorY.Interval = 0.1D;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.Name = "TestDaten1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chart1.DataSource = this.dataSet1BindingSource;
            legend2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series2.ChartArea = "TestDaten1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "TestDaten1";
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(580, 275);
            this.chart1.TabIndex = 6;
            this.chart1.Text = "chart1";
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove_1);
            // 
            // chart3
            // 
            this.chart3.AllowDrop = true;
            this.chart3.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea3.CursorX.Interval = 0.1D;
            chartArea3.CursorX.IsUserEnabled = true;
            chartArea3.CursorX.IsUserSelectionEnabled = true;
            chartArea3.CursorY.Interval = 0.1D;
            chartArea3.CursorY.IsUserEnabled = true;
            chartArea3.CursorY.IsUserSelectionEnabled = true;
            chartArea3.Name = "TestDaten1";
            this.chart3.ChartAreas.Add(chartArea3);
            this.chart3.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chart3.DataSource = this.dataSet1BindingSource;
            legend3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend3.Name = "Legend1";
            this.chart3.Legends.Add(legend3);
            this.chart3.Location = new System.Drawing.Point(0, 278);
            this.chart3.Name = "chart3";
            series3.ChartArea = "TestDaten1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series3.Legend = "Legend1";
            series3.Name = "TestDaten1";
            this.chart3.Series.Add(series3);
            this.chart3.Size = new System.Drawing.Size(580, 275);
            this.chart3.TabIndex = 9;
            this.chart3.Text = "chart3";
            this.chart3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart3_MouseMove_1);
            // 
            // chart4
            // 
            this.chart4.AllowDrop = true;
            this.chart4.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.Center;
            chartArea4.CursorX.Interval = 0.1D;
            chartArea4.CursorX.IsUserEnabled = true;
            chartArea4.CursorX.IsUserSelectionEnabled = true;
            chartArea4.CursorY.Interval = 0.1D;
            chartArea4.CursorY.IsUserEnabled = true;
            chartArea4.CursorY.IsUserSelectionEnabled = true;
            chartArea4.Name = "TestDaten1";
            this.chart4.ChartAreas.Add(chartArea4);
            this.chart4.Cursor = System.Windows.Forms.Cursors.Cross;
            this.chart4.DataSource = this.dataSet1BindingSource;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend4.Name = "Legend1";
            this.chart4.Legends.Add(legend4);
            this.chart4.Location = new System.Drawing.Point(602, 278);
            this.chart4.Name = "chart4";
            series4.ChartArea = "TestDaten1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series4.Legend = "Legend1";
            series4.Name = "TestDaten1";
            this.chart4.Series.Add(series4);
            this.chart4.Size = new System.Drawing.Size(580, 275);
            this.chart4.TabIndex = 10;
            this.chart4.Text = "chart4";
            this.chart4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart4_MouseMove_1);
            // 
            // lblPos1
            // 
            this.lblPos1.AutoSize = true;
            this.lblPos1.BackColor = System.Drawing.Color.White;
            this.lblPos1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPos1.Location = new System.Drawing.Point(479, 124);
            this.lblPos1.Name = "lblPos1";
            this.lblPos1.Size = new System.Drawing.Size(60, 24);
            this.lblPos1.TabIndex = 11;
            this.lblPos1.Text = "label1";
            // 
            // lblPos2
            // 
            this.lblPos2.AutoSize = true;
            this.lblPos2.BackColor = System.Drawing.Color.White;
            this.lblPos2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPos2.Location = new System.Drawing.Point(1068, 124);
            this.lblPos2.Name = "lblPos2";
            this.lblPos2.Size = new System.Drawing.Size(60, 24);
            this.lblPos2.TabIndex = 12;
            this.lblPos2.Text = "label2";
            // 
            // lblPos3
            // 
            this.lblPos3.AutoSize = true;
            this.lblPos3.BackColor = System.Drawing.Color.White;
            this.lblPos3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPos3.Location = new System.Drawing.Point(479, 416);
            this.lblPos3.Name = "lblPos3";
            this.lblPos3.Size = new System.Drawing.Size(60, 24);
            this.lblPos3.TabIndex = 13;
            this.lblPos3.Text = "label3";
            // 
            // lblPos4
            // 
            this.lblPos4.AutoSize = true;
            this.lblPos4.BackColor = System.Drawing.Color.White;
            this.lblPos4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPos4.Location = new System.Drawing.Point(1068, 416);
            this.lblPos4.Name = "lblPos4";
            this.lblPos4.Size = new System.Drawing.Size(60, 24);
            this.lblPos4.TabIndex = 14;
            this.lblPos4.Text = "label4";
            // 
            // PlotGraphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 553);
            this.Controls.Add(this.lblPos4);
            this.Controls.Add(this.lblPos3);
            this.Controls.Add(this.lblPos2);
            this.Controls.Add(this.lblPos1);
            this.Controls.Add(this.chart4);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PlotGraphic";
            this.Text = "PlotGraphic";
            this.Load += new System.EventHandler(this.PlotGraphic_Load);
            this.Resize += new System.EventHandler(this.PlotGraphic_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.BindingSource dataSet1BindingSource;
        private System.Windows.Forms.Timer tmrTimeSpan;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        private System.Windows.Forms.Label lblPos1;
        private System.Windows.Forms.Label lblPos2;
        private System.Windows.Forms.Label lblPos3;
        private System.Windows.Forms.Label lblPos4;
    }
}