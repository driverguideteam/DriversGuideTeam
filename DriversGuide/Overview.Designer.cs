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
<<<<<<< HEAD
=======
            this.tTDistrUrb = new System.Windows.Forms.ToolTip(this.components);
            this.tTDistrRur = new System.Windows.Forms.ToolTip(this.components);
            this.tTDistrMot = new System.Windows.Forms.ToolTip(this.components);
            this.lblMaxSpeed = new System.Windows.Forms.Label();
            this.lblMaxSpeedVal = new System.Windows.Forms.Label();
            this.lblMaxSpCold = new System.Windows.Forms.Label();
            this.lblMaxSpColdVal = new System.Windows.Forms.Label();
            this.lblAvgSpCold = new System.Windows.Forms.Label();
            this.lblAvgSpColdVal = new System.Windows.Forms.Label();
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
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
            this.distUrban.MouseLeave += new System.EventHandler(this.distUrban_MouseLeave);
            this.distUrban.MouseHover += new System.EventHandler(this.distUrban_MouseHover);
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
            this.distRural.MouseLeave += new System.EventHandler(this.distRural_MouseLeave);
            this.distRural.MouseHover += new System.EventHandler(this.distRural_MouseHover);
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
            this.distMotorway.MouseLeave += new System.EventHandler(this.distMotorway_MouseLeave);
            this.distMotorway.MouseHover += new System.EventHandler(this.distMotorway_MouseHover);
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
<<<<<<< HEAD
<<<<<<< HEAD
            this.lblTripComplete.Location = new System.Drawing.Point(12, 167);
=======
            this.lblTripComplete.Location = new System.Drawing.Point(12, 148);
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
=======
            this.lblTripComplete.Location = new System.Drawing.Point(12, 167);
>>>>>>> parent of 1572168... Kogler 05.04 15:00
            this.lblTripComplete.Name = "lblTripComplete";
            this.lblTripComplete.Size = new System.Drawing.Size(84, 13);
            this.lblTripComplete.TabIndex = 9;
            this.lblTripComplete.Text = "Strecke gesamt:";
            // 
            // lblTripVal
            // 
            this.lblTripVal.AutoSize = true;
<<<<<<< HEAD
<<<<<<< HEAD
            this.lblTripVal.Location = new System.Drawing.Point(93, 167);
=======
            this.lblTripVal.Location = new System.Drawing.Point(138, 148);
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
=======
            this.lblTripVal.Location = new System.Drawing.Point(93, 167);
>>>>>>> parent of 1572168... Kogler 05.04 15:00
            this.lblTripVal.Name = "lblTripVal";
            this.lblTripVal.Size = new System.Drawing.Size(0, 13);
            this.lblTripVal.TabIndex = 10;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
<<<<<<< HEAD
<<<<<<< HEAD
            this.lblDuration.Location = new System.Drawing.Point(12, 189);
=======
            this.lblDuration.Location = new System.Drawing.Point(12, 170);
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
=======
            this.lblDuration.Location = new System.Drawing.Point(12, 189);
>>>>>>> parent of 1572168... Kogler 05.04 15:00
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(87, 13);
            this.lblDuration.TabIndex = 11;
            this.lblDuration.Text = "Dauer der Fahrt: ";
            // 
            // lblDurationVal
            // 
            this.lblDurationVal.AutoSize = true;
<<<<<<< HEAD
<<<<<<< HEAD
            this.lblDurationVal.Location = new System.Drawing.Point(93, 189);
=======
            this.lblDurationVal.Location = new System.Drawing.Point(138, 170);
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
=======
            this.lblDurationVal.Location = new System.Drawing.Point(93, 189);
>>>>>>> parent of 1572168... Kogler 05.04 15:00
            this.lblDurationVal.Name = "lblDurationVal";
            this.lblDurationVal.Size = new System.Drawing.Size(0, 13);
            this.lblDurationVal.TabIndex = 12;
            // 
            // lblTimeHold
            // 
            this.lblTimeHold.AutoSize = true;
<<<<<<< HEAD
<<<<<<< HEAD
            this.lblTimeHold.Location = new System.Drawing.Point(12, 216);
=======
            this.lblTimeHold.Location = new System.Drawing.Point(12, 197);
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
=======
            this.lblTimeHold.Location = new System.Drawing.Point(12, 216);
>>>>>>> parent of 1572168... Kogler 05.04 15:00
            this.lblTimeHold.Name = "lblTimeHold";
            this.lblTimeHold.Size = new System.Drawing.Size(54, 13);
            this.lblTimeHold.TabIndex = 13;
            this.lblTimeHold.Text = "Haltezeit: ";
            // 
            // lblTimeHoldVal
            // 
            this.lblTimeHoldVal.AutoSize = true;
<<<<<<< HEAD
<<<<<<< HEAD
            this.lblTimeHoldVal.Location = new System.Drawing.Point(93, 216);
            this.lblTimeHoldVal.Name = "lblTimeHoldVal";
            this.lblTimeHoldVal.Size = new System.Drawing.Size(0, 13);
            this.lblTimeHoldVal.TabIndex = 14;
=======
            this.lblTimeHoldVal.Location = new System.Drawing.Point(144, 197);
=======
            this.lblTimeHoldVal.Location = new System.Drawing.Point(93, 216);
>>>>>>> parent of 1572168... Kogler 05.04 15:00
            this.lblTimeHoldVal.Name = "lblTimeHoldVal";
            this.lblTimeHoldVal.Size = new System.Drawing.Size(0, 13);
            this.lblTimeHoldVal.TabIndex = 14;
            // 
            // lblMaxSpeed
            // 
            this.lblMaxSpeed.AutoSize = true;
            this.lblMaxSpeed.Location = new System.Drawing.Point(12, 240);
            this.lblMaxSpeed.Name = "lblMaxSpeed";
            this.lblMaxSpeed.Size = new System.Drawing.Size(126, 13);
            this.lblMaxSpeed.TabIndex = 15;
            this.lblMaxSpeed.Text = "Hoechstgeschwindigkeit:";
            // 
            // lblMaxSpeedVal
            // 
            this.lblMaxSpeedVal.AutoSize = true;
            this.lblMaxSpeedVal.Location = new System.Drawing.Point(144, 240);
            this.lblMaxSpeedVal.Name = "lblMaxSpeedVal";
            this.lblMaxSpeedVal.Size = new System.Drawing.Size(0, 13);
            this.lblMaxSpeedVal.TabIndex = 16;
            // 
            // lblMaxSpCold
            // 
            this.lblMaxSpCold.AutoSize = true;
            this.lblMaxSpCold.Location = new System.Drawing.Point(275, 167);
            this.lblMaxSpCold.Name = "lblMaxSpCold";
            this.lblMaxSpCold.Size = new System.Drawing.Size(167, 13);
            this.lblMaxSpCold.TabIndex = 17;
            this.lblMaxSpCold.Text = "Hoechstgeschwindigkeit Kaltstart:";
            // 
            // lblMaxSpColdVal
            // 
            this.lblMaxSpColdVal.AutoSize = true;
            this.lblMaxSpColdVal.Location = new System.Drawing.Point(448, 167);
            this.lblMaxSpColdVal.Name = "lblMaxSpColdVal";
            this.lblMaxSpColdVal.Size = new System.Drawing.Size(0, 13);
            this.lblMaxSpColdVal.TabIndex = 18;
            // 
            // lblAvgSpCold
            // 
            this.lblAvgSpCold.AutoSize = true;
            this.lblAvgSpCold.Location = new System.Drawing.Point(275, 189);
            this.lblAvgSpCold.Name = "lblAvgSpCold";
            this.lblAvgSpCold.Size = new System.Drawing.Size(192, 13);
            this.lblAvgSpCold.TabIndex = 19;
            this.lblAvgSpCold.Text = "Durchschnittsgeschwindigkeit Kaltstart:";
            // 
            // lblAvgSpColdVal
            // 
            this.lblAvgSpColdVal.AutoSize = true;
            this.lblAvgSpColdVal.Location = new System.Drawing.Point(473, 189);
            this.lblAvgSpColdVal.Name = "lblAvgSpColdVal";
            this.lblAvgSpColdVal.Size = new System.Drawing.Size(0, 13);
            this.lblAvgSpColdVal.TabIndex = 20;
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
            // 
            // Overview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< HEAD
<<<<<<< HEAD
            this.ClientSize = new System.Drawing.Size(543, 450);
=======
            this.ClientSize = new System.Drawing.Size(770, 304);
=======
            this.ClientSize = new System.Drawing.Size(543, 450);
>>>>>>> parent of 1572168... Kogler 05.04 15:00
            this.Controls.Add(this.lblAvgSpColdVal);
            this.Controls.Add(this.lblAvgSpCold);
            this.Controls.Add(this.lblMaxSpColdVal);
            this.Controls.Add(this.lblMaxSpCold);
            this.Controls.Add(this.lblMaxSpeedVal);
            this.Controls.Add(this.lblMaxSpeed);
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
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
<<<<<<< HEAD
=======
        private System.Windows.Forms.ToolTip tTDistrUrb;
        private System.Windows.Forms.ToolTip tTDistrRur;
        private System.Windows.Forms.ToolTip tTDistrMot;
        private System.Windows.Forms.Label lblMaxSpeed;
        private System.Windows.Forms.Label lblMaxSpeedVal;
        private System.Windows.Forms.Label lblMaxSpCold;
        private System.Windows.Forms.Label lblMaxSpColdVal;
        private System.Windows.Forms.Label lblAvgSpCold;
        private System.Windows.Forms.Label lblAvgSpColdVal;
<<<<<<< HEAD
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl12;
        private System.Windows.Forms.Label lbl10;
        private System.Windows.Forms.Label lbl8;
        private System.Windows.Forms.Label lbl11;
        private System.Windows.Forms.Label lbl9;
        private System.Windows.Forms.Label lbl7;
        private System.Windows.Forms.Label lbl18;
        private System.Windows.Forms.Label lbl16;
        private System.Windows.Forms.Label lbl14;
        private System.Windows.Forms.Label lbl17;
        private System.Windows.Forms.Label lbl15;
        private System.Windows.Forms.Label lbl13;
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
=======
>>>>>>> parent of 1572168... Kogler 05.04 15:00
    }
}