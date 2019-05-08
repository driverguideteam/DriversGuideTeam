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
            this.avgVUrban = new System.Windows.Forms.TextBox();
            this.distUrban = new System.Windows.Forms.TextBox();
            this.gBRural = new System.Windows.Forms.GroupBox();
            this.tripRural = new System.Windows.Forms.TextBox();
            this.avgVRural = new System.Windows.Forms.TextBox();
            this.distRural = new System.Windows.Forms.TextBox();
            this.gBMotorway = new System.Windows.Forms.GroupBox();
            this.tripMotorway = new System.Windows.Forms.TextBox();
            this.avgVMotorway = new System.Windows.Forms.TextBox();
            this.distMotorway = new System.Windows.Forms.TextBox();
            this.lblDistr = new System.Windows.Forms.Label();
            this.lblMeanV = new System.Windows.Forms.Label();
            this.lblTrip = new System.Windows.Forms.Label();
            this.lblTripComplete = new System.Windows.Forms.Label();
            this.lblTripVal = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDurationVal = new System.Windows.Forms.Label();
            this.lblTimeHold = new System.Windows.Forms.Label();
            this.lblTimeHoldVal = new System.Windows.Forms.Label();
            this.gBUrban.SuspendLayout();
            this.gBRural.SuspendLayout();
            this.gBMotorway.SuspendLayout();
            this.SuspendLayout();
            // 
            // gBUrban
            // 
            this.gBUrban.Controls.Add(this.tripUrban);
            this.gBUrban.Controls.Add(this.avgVUrban);
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
            // avgVUrban
            // 
            this.avgVUrban.Location = new System.Drawing.Point(6, 55);
            this.avgVUrban.Name = "avgVUrban";
            this.avgVUrban.ReadOnly = true;
            this.avgVUrban.Size = new System.Drawing.Size(78, 20);
            this.avgVUrban.TabIndex = 2;
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
            this.gBRural.Controls.Add(this.avgVRural);
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
            // avgVRural
            // 
            this.avgVRural.Location = new System.Drawing.Point(6, 55);
            this.avgVRural.Name = "avgVRural";
            this.avgVRural.ReadOnly = true;
            this.avgVRural.Size = new System.Drawing.Size(78, 20);
            this.avgVRural.TabIndex = 2;
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
            this.gBMotorway.Controls.Add(this.avgVMotorway);
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
            // avgVMotorway
            // 
            this.avgVMotorway.Location = new System.Drawing.Point(6, 55);
            this.avgVMotorway.Name = "avgVMotorway";
            this.avgVMotorway.ReadOnly = true;
            this.avgVMotorway.Size = new System.Drawing.Size(78, 20);
            this.avgVMotorway.TabIndex = 2;
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
            // lblTripComplete
            // 
            this.lblTripComplete.AutoSize = true;
            this.lblTripComplete.Location = new System.Drawing.Point(12, 167);
            this.lblTripComplete.Name = "lblTripComplete";
            this.lblTripComplete.Size = new System.Drawing.Size(84, 13);
            this.lblTripComplete.TabIndex = 9;
            this.lblTripComplete.Text = "Strecke gesamt:";
            // 
            // lblTripVal
            // 
            this.lblTripVal.AutoSize = true;
            this.lblTripVal.Location = new System.Drawing.Point(93, 167);
            this.lblTripVal.Name = "lblTripVal";
            this.lblTripVal.Size = new System.Drawing.Size(0, 13);
            this.lblTripVal.TabIndex = 10;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(12, 189);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(87, 13);
            this.lblDuration.TabIndex = 11;
            this.lblDuration.Text = "Dauer der Fahrt: ";
            // 
            // lblDurationVal
            // 
            this.lblDurationVal.AutoSize = true;
            this.lblDurationVal.Location = new System.Drawing.Point(93, 189);
            this.lblDurationVal.Name = "lblDurationVal";
            this.lblDurationVal.Size = new System.Drawing.Size(0, 13);
            this.lblDurationVal.TabIndex = 12;
            // 
            // lblTimeHold
            // 
            this.lblTimeHold.AutoSize = true;
            this.lblTimeHold.Location = new System.Drawing.Point(12, 216);
            this.lblTimeHold.Name = "lblTimeHold";
            this.lblTimeHold.Size = new System.Drawing.Size(54, 13);
            this.lblTimeHold.TabIndex = 13;
            this.lblTimeHold.Text = "Haltezeit: ";
            // 
            // lblTimeHoldVal
            // 
            this.lblTimeHoldVal.AutoSize = true;
            this.lblTimeHoldVal.Location = new System.Drawing.Point(93, 216);
            this.lblTimeHoldVal.Name = "lblTimeHoldVal";
            this.lblTimeHoldVal.Size = new System.Drawing.Size(0, 13);
            this.lblTimeHoldVal.TabIndex = 14;
            // 
            // Overview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 450);
            this.Controls.Add(this.lblTimeHoldVal);
            this.Controls.Add(this.lblTimeHold);
            this.Controls.Add(this.lblDurationVal);
            this.Controls.Add(this.lblDuration);
            this.Controls.Add(this.lblTripVal);
            this.Controls.Add(this.lblTripComplete);
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
        private System.Windows.Forms.TextBox avgVUrban;
        private System.Windows.Forms.GroupBox gBRural;
        private System.Windows.Forms.TextBox tripRural;
        private System.Windows.Forms.TextBox avgVRural;
        private System.Windows.Forms.TextBox distRural;
        private System.Windows.Forms.GroupBox gBMotorway;
        private System.Windows.Forms.TextBox tripMotorway;
        private System.Windows.Forms.TextBox avgVMotorway;
        private System.Windows.Forms.TextBox distMotorway;
        private System.Windows.Forms.Label lblDistr;
        private System.Windows.Forms.Label lblMeanV;
        private System.Windows.Forms.Label lblTrip;
        private System.Windows.Forms.Label lblTripComplete;
        private System.Windows.Forms.Label lblTripVal;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblDurationVal;
        private System.Windows.Forms.Label lblTimeHold;
        private System.Windows.Forms.Label lblTimeHoldVal;
    }
}