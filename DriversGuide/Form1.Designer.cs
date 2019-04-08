namespace DriversGuide
{
    partial class Form1
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnReadMeasuremntfile = new System.Windows.Forms.Button();
            this.txtMeasurement = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnGraphic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReadMeasuremntfile
            // 
            this.btnReadMeasuremntfile.Location = new System.Drawing.Point(16, 15);
            this.btnReadMeasuremntfile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnReadMeasuremntfile.Name = "btnReadMeasuremntfile";
            this.btnReadMeasuremntfile.Size = new System.Drawing.Size(100, 28);
            this.btnReadMeasuremntfile.TabIndex = 0;
            this.btnReadMeasuremntfile.Text = "Read Measurement File";
            this.btnReadMeasuremntfile.UseVisualStyleBackColor = true;
            this.btnReadMeasuremntfile.Click += new System.EventHandler(this.btnReadMeasuremntfile_Click);
            // 
            // txtMeasurement
            // 
            this.txtMeasurement.Location = new System.Drawing.Point(16, 50);
            this.txtMeasurement.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMeasurement.Multiline = true;
            this.txtMeasurement.Name = "txtMeasurement";
            this.txtMeasurement.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMeasurement.Size = new System.Drawing.Size(1033, 431);
            this.txtMeasurement.TabIndex = 1;
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // btnGraphic
            // 
            this.btnGraphic.Location = new System.Drawing.Point(124, 15);
            this.btnGraphic.Margin = new System.Windows.Forms.Padding(4);
            this.btnGraphic.Name = "btnGraphic";
            this.btnGraphic.Size = new System.Drawing.Size(117, 28);
            this.btnGraphic.TabIndex = 2;
            this.btnGraphic.Text = "Show Graphic";
            this.btnGraphic.UseVisualStyleBackColor = true;
            this.btnGraphic.Click += new System.EventHandler(this.btnGraphic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnGraphic);
            this.Controls.Add(this.txtMeasurement);
            this.Controls.Add(this.btnReadMeasuremntfile);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnReadMeasuremntfile;
        private System.Windows.Forms.TextBox txtMeasurement;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnGraphic;
    }
}

