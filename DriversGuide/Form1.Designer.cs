namespace DriversGuide
{
    partial class DriversGuideMain
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
            this.txtMeasurement = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileEinlesenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.berechnungDurchführenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grafikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showGraphicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grafikSpielwieseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gPSSpielwieseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.auswertungToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überblickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtMeasurement
            // 
            this.txtMeasurement.Location = new System.Drawing.Point(0, 27);
            this.txtMeasurement.Multiline = true;
            this.txtMeasurement.Name = "txtMeasurement";
            this.txtMeasurement.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMeasurement.Size = new System.Drawing.Size(800, 425);
            this.txtMeasurement.TabIndex = 1;
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.grafikToolStripMenuItem,
            this.auswertungToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileEinlesenToolStripMenuItem,
            this.berechnungDurchführenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // fileEinlesenToolStripMenuItem
            // 
            this.fileEinlesenToolStripMenuItem.Name = "fileEinlesenToolStripMenuItem";
            this.fileEinlesenToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.fileEinlesenToolStripMenuItem.Text = "File einlesen...";
            this.fileEinlesenToolStripMenuItem.Click += new System.EventHandler(this.btnReadMeasuremntfile_Click);
            // 
            // berechnungDurchführenToolStripMenuItem
            // 
            this.berechnungDurchführenToolStripMenuItem.Enabled = false;
            this.berechnungDurchführenToolStripMenuItem.Name = "berechnungDurchführenToolStripMenuItem";
            this.berechnungDurchführenToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.berechnungDurchführenToolStripMenuItem.Text = "Berechnen...";
            this.berechnungDurchführenToolStripMenuItem.Click += new System.EventHandler(this.btnBerechnen_Click);
            // 
            // grafikToolStripMenuItem
            // 
            this.grafikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showGraphicToolStripMenuItem,
            this.grafikSpielwieseToolStripMenuItem,
            this.gPSSpielwieseToolStripMenuItem});
            this.grafikToolStripMenuItem.Enabled = false;
            this.grafikToolStripMenuItem.Name = "grafikToolStripMenuItem";
            this.grafikToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.grafikToolStripMenuItem.Text = "Grafik";
            // 
            // showGraphicToolStripMenuItem
            // 
            this.showGraphicToolStripMenuItem.Name = "showGraphicToolStripMenuItem";
            this.showGraphicToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.showGraphicToolStripMenuItem.Text = "Show Graphic";
            this.showGraphicToolStripMenuItem.Click += new System.EventHandler(this.btnGraphic_Click);
            // 
            // grafikSpielwieseToolStripMenuItem
            // 
            this.grafikSpielwieseToolStripMenuItem.Name = "grafikSpielwieseToolStripMenuItem";
            this.grafikSpielwieseToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.grafikSpielwieseToolStripMenuItem.Text = "Grafik Spielwiese";
            this.grafikSpielwieseToolStripMenuItem.Click += new System.EventHandler(this.btnSpielwiese_Click);
            // 
            // gPSSpielwieseToolStripMenuItem
            // 
            this.gPSSpielwieseToolStripMenuItem.Name = "gPSSpielwieseToolStripMenuItem";
            this.gPSSpielwieseToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.gPSSpielwieseToolStripMenuItem.Text = "GPS Spielwiese";
            this.gPSSpielwieseToolStripMenuItem.Click += new System.EventHandler(this.btnGPS_Click);
            // 
            // auswertungToolStripMenuItem
            // 
            this.auswertungToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.überblickToolStripMenuItem});
            this.auswertungToolStripMenuItem.Name = "auswertungToolStripMenuItem";
            this.auswertungToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.auswertungToolStripMenuItem.Text = "Auswertung";
            // 
            // überblickToolStripMenuItem
            // 
            this.überblickToolStripMenuItem.Name = "überblickToolStripMenuItem";
            this.überblickToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.überblickToolStripMenuItem.Text = "Überblick";
            this.überblickToolStripMenuItem.Click += new System.EventHandler(this.überblickToolStripMenuItem_Click);
            // 
            // DriversGuideMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtMeasurement);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DriversGuideMain";
            this.Text = "Drivers Guide";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtMeasurement;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileEinlesenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grafikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showGraphicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem grafikSpielwieseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gPSSpielwieseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem berechnungDurchführenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem auswertungToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem überblickToolStripMenuItem;
    }
}

