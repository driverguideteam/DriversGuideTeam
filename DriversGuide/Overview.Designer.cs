namespace DriversGuide
{
    partial class Overview
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
            this.gBUrban = new System.Windows.Forms.GroupBox();
            this.tripUrban = new System.Windows.Forms.TextBox();
            this.meanVUrban = new System.Windows.Forms.TextBox();
            this.distUrban = new System.Windows.Forms.TextBox();
            this.gBRural = new System.Windows.Forms.GroupBox();
            this.tripRural = new System.Windows.Forms.TextBox();
            this.meanVRural = new System.Windows.Forms.TextBox();
            this.distRural = new System.Windows.Forms.TextBox();
            this.gBMotorway = new System.Windows.Forms.GroupBox();
            this.tripMotorway = new System.Windows.Forms.TextBox();
            this.meanVMotorway = new System.Windows.Forms.TextBox();
            this.distMotorway = new System.Windows.Forms.TextBox();
            this.lblDistr = new System.Windows.Forms.Label();
            this.lblMeanV = new System.Windows.Forms.Label();
            this.lblTrip = new System.Windows.Forms.Label();
            this.gBUrban.SuspendLayout();
            this.gBRural.SuspendLayout();
            this.gBMotorway.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBUrban
            // 
            this.gBUrban.Controls.Add(this.tripUrban);
            this.gBUrban.Controls.Add(this.meanVUrban);
            this.gBUrban.Controls.Add(this.distUrban);
            this.gBUrban.Location = new System.Drawing.Point(186, 23);
            this.gBUrban.Name = "gBUrban";
            this.gBUrban.Size = new System.Drawing.Size(91, 111);
            this.gBUrban.TabIndex = 0;
            this.gBUrban.TabStop = false;
            this.gBUrban.Text = "Stadt";
            // 
            // tripUrban
            // 
            this.tripUrban.Location = new System.Drawing.Point(6, 81);
            this.tripUrban.Name = "tripUrban";
            this.tripUrban.ReadOnly = true;
            this.tripUrban.Size = new System.Drawing.Size(78, 20);
            this.tripUrban.TabIndex = 3;
            // 
            // meanVUrban
            // 
            this.meanVUrban.Location = new System.Drawing.Point(6, 55);
            this.meanVUrban.Name = "meanVUrban";
            this.meanVUrban.ReadOnly = true;
            this.meanVUrban.Size = new System.Drawing.Size(78, 20);
            this.meanVUrban.TabIndex = 2;
            // 
            // distUrban
            // 
            this.distUrban.BackColor = System.Drawing.SystemColors.Control;
            this.distUrban.Location = new System.Drawing.Point(6, 29);
            this.distUrban.Name = "distUrban";
            this.distUrban.ReadOnly = true;
            this.distUrban.Size = new System.Drawing.Size(78, 20);
            this.distUrban.TabIndex = 1;
            // 
            // gBRural
            // 
            this.gBRural.Controls.Add(this.tripRural);
            this.gBRural.Controls.Add(this.meanVRural);
            this.gBRural.Controls.Add(this.distRural);
            this.gBRural.Location = new System.Drawing.Point(300, 23);
            this.gBRural.Name = "gBRural";
            this.gBRural.Size = new System.Drawing.Size(91, 111);
            this.gBRural.TabIndex = 4;
            this.gBRural.TabStop = false;
            this.gBRural.Text = "Land";
            // 
            // tripRural
            // 
            this.tripRural.Location = new System.Drawing.Point(6, 81);
            this.tripRural.Name = "tripRural";
            this.tripRural.ReadOnly = true;
            this.tripRural.Size = new System.Drawing.Size(78, 20);
            this.tripRural.TabIndex = 3;
            // 
            // meanVRural
            // 
            this.meanVRural.Location = new System.Drawing.Point(6, 55);
            this.meanVRural.Name = "meanVRural";
            this.meanVRural.ReadOnly = true;
            this.meanVRural.Size = new System.Drawing.Size(78, 20);
            this.meanVRural.TabIndex = 2;
            // 
            // distRural
            // 
            this.distRural.Location = new System.Drawing.Point(6, 29);
            this.distRural.Name = "distRural";
            this.distRural.ReadOnly = true;
            this.distRural.Size = new System.Drawing.Size(78, 20);
            this.distRural.TabIndex = 1;
            // 
            // gBMotorway
            // 
            this.gBMotorway.Controls.Add(this.tripMotorway);
            this.gBMotorway.Controls.Add(this.meanVMotorway);
            this.gBMotorway.Controls.Add(this.distMotorway);
            this.gBMotorway.Location = new System.Drawing.Point(415, 23);
            this.gBMotorway.Name = "gBMotorway";
            this.gBMotorway.Size = new System.Drawing.Size(91, 111);
            this.gBMotorway.TabIndex = 5;
            this.gBMotorway.TabStop = false;
            this.gBMotorway.Text = "Autobahn";
            // 
            // tripMotorway
            // 
            this.tripMotorway.Location = new System.Drawing.Point(6, 81);
            this.tripMotorway.Name = "tripMotorway";
            this.tripMotorway.ReadOnly = true;
            this.tripMotorway.Size = new System.Drawing.Size(78, 20);
            this.tripMotorway.TabIndex = 3;
            // 
            // meanVMotorway
            // 
            this.meanVMotorway.Location = new System.Drawing.Point(6, 55);
            this.meanVMotorway.Name = "meanVMotorway";
            this.meanVMotorway.ReadOnly = true;
            this.meanVMotorway.Size = new System.Drawing.Size(78, 20);
            this.meanVMotorway.TabIndex = 2;
            // 
            // distMotorway
            // 
            this.distMotorway.Location = new System.Drawing.Point(6, 29);
            this.distMotorway.Name = "distMotorway";
            this.distMotorway.ReadOnly = true;
            this.distMotorway.Size = new System.Drawing.Size(78, 20);
            this.distMotorway.TabIndex = 1;
            // 
            // lblDistr
            // 
            this.lblDistr.AutoSize = true;
            this.lblDistr.Location = new System.Drawing.Point(12, 55);
            this.lblDistr.Name = "lblDistr";
            this.lblDistr.Size = new System.Drawing.Size(118, 13);
            this.lblDistr.TabIndex = 6;
            this.lblDistr.Text = "Prozentuelle Verteilung:";
            // 
            // lblMeanV
            // 
            this.lblMeanV.AutoSize = true;
            this.lblMeanV.Location = new System.Drawing.Point(12, 81);
            this.lblMeanV.Name = "lblMeanV";
            this.lblMeanV.Size = new System.Drawing.Size(151, 13);
            this.lblMeanV.TabIndex = 7;
            this.lblMeanV.Text = "Durchschnittsgeschwindigkeit:";
            // 
            // lblTrip
            // 
            this.lblTrip.AutoSize = true;
            this.lblTrip.Location = new System.Drawing.Point(12, 107);
            this.lblTrip.Name = "lblTrip";
            this.lblTrip.Size = new System.Drawing.Size(47, 13);
            this.lblTrip.TabIndex = 8;
            this.lblTrip.Text = "Strecke:";
            // 
            // Overview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 450);
            this.Controls.Add(this.lblTrip);
            this.Controls.Add(this.lblMeanV);
            this.Controls.Add(this.lblDistr);
            this.Controls.Add(this.gBMotorway);
            this.Controls.Add(this.gBRural);
            this.Controls.Add(this.gBUrban);
            this.Name = "Overview";
            this.Text = "Overview";
            this.gBUrban.ResumeLayout(false);
            this.gBUrban.PerformLayout();
            this.gBRural.ResumeLayout(false);
            this.gBRural.PerformLayout();
            this.gBMotorway.ResumeLayout(false);
            this.gBMotorway.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gBUrban;
        private System.Windows.Forms.TextBox distUrban;
        private System.Windows.Forms.TextBox tripUrban;
        private System.Windows.Forms.TextBox meanVUrban;
        private System.Windows.Forms.GroupBox gBRural;
        private System.Windows.Forms.TextBox tripRural;
        private System.Windows.Forms.TextBox meanVRural;
        private System.Windows.Forms.TextBox distRural;
        private System.Windows.Forms.GroupBox gBMotorway;
        private System.Windows.Forms.TextBox tripMotorway;
        private System.Windows.Forms.TextBox meanVMotorway;
        private System.Windows.Forms.TextBox distMotorway;
        private System.Windows.Forms.Label lblDistr;
        private System.Windows.Forms.Label lblMeanV;
        private System.Windows.Forms.Label lblTrip;
    }
}