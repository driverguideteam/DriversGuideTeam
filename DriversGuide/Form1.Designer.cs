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
            this.btnSpielwiese = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReadMeasuremntfile
            // 
            this.btnReadMeasuremntfile.Location = new System.Drawing.Point(12, 12);
            this.btnReadMeasuremntfile.Name = "btnReadMeasuremntfile";
            this.btnReadMeasuremntfile.Size = new System.Drawing.Size(75, 23);
            this.btnReadMeasuremntfile.TabIndex = 0;
            this.btnReadMeasuremntfile.Text = "Read Measurement File";
            this.btnReadMeasuremntfile.UseVisualStyleBackColor = true;
            this.btnReadMeasuremntfile.Click += new System.EventHandler(this.btnReadMeasuremntfile_Click);
            // 
            // txtMeasurement
            // 
            this.txtMeasurement.Location = new System.Drawing.Point(12, 41);
            this.txtMeasurement.Multiline = true;
            this.txtMeasurement.Name = "txtMeasurement";
            this.txtMeasurement.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMeasurement.Size = new System.Drawing.Size(776, 351);
            this.txtMeasurement.TabIndex = 1;
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // btnGraphic
            // 
            this.btnGraphic.Location = new System.Drawing.Point(93, 12);
            this.btnGraphic.Name = "btnGraphic";
            this.btnGraphic.Size = new System.Drawing.Size(88, 23);
            this.btnGraphic.TabIndex = 2;
            this.btnGraphic.Text = "Show Graphic";
            this.btnGraphic.UseVisualStyleBackColor = true;
            this.btnGraphic.Click += new System.EventHandler(this.btnGraphic_Click);
            // 
            // btnSpielwiese
            // 
            this.btnSpielwiese.Location = new System.Drawing.Point(187, 12);
            this.btnSpielwiese.Name = "btnSpielwiese";
            this.btnSpielwiese.Size = new System.Drawing.Size(75, 23);
            this.btnSpielwiese.TabIndex = 3;
            this.btnSpielwiese.Text = "GrafikSpielwiese";
            this.btnSpielwiese.UseVisualStyleBackColor = true;
            this.btnSpielwiese.Click += new System.EventHandler(this.btnSpielwiese_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSpielwiese);
            this.Controls.Add(this.btnGraphic);
            this.Controls.Add(this.txtMeasurement);
            this.Controls.Add(this.btnReadMeasuremntfile);
            this.Name = "Form1";
            this.Text = "Drivers Guide";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnReadMeasuremntfile;
        private System.Windows.Forms.TextBox txtMeasurement;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnGraphic;
        private System.Windows.Forms.Button btnSpielwiese;
    }
}

