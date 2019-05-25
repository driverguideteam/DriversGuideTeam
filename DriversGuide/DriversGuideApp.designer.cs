﻿namespace DriversGuide
{
    partial class DriversGuideApp
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
            this.pnlSideBar = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnGraphic = new System.Windows.Forms.Button();
            this.btnOverview = new System.Windows.Forms.Button();
            this.btnGPS = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblHide = new System.Windows.Forms.Label();
            this.lblShow = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.tmrFade = new System.Windows.Forms.Timer(this.components);
            this.btnShowDynamic = new System.Windows.Forms.Button();
            this.pnlSideBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSideBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlSideBar.Controls.Add(this.btnShowDynamic);
            this.pnlSideBar.Controls.Add(this.panel1);
            this.pnlSideBar.Controls.Add(this.btnReadFile);
            this.pnlSideBar.Controls.Add(this.btnGraphic);
            this.pnlSideBar.Controls.Add(this.btnOverview);
            this.pnlSideBar.Controls.Add(this.btnGPS);
            this.pnlSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnlSideBar.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(259, 554);
            this.pnlSideBar.TabIndex = 31;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 85);
            this.panel1.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drivers Guide";
            // 
            // btnReadFile
            // 
            this.btnReadFile.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnReadFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnReadFile.FlatAppearance.BorderSize = 0;
            this.btnReadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadFile.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadFile.ForeColor = System.Drawing.Color.Teal;
            this.btnReadFile.Location = new System.Drawing.Point(0, 192);
            this.btnReadFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(259, 36);
            this.btnReadFile.TabIndex = 35;
            this.btnReadFile.Text = "File einlesen...";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnGraphic
            // 
            this.btnGraphic.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGraphic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnGraphic.Enabled = false;
            this.btnGraphic.FlatAppearance.BorderSize = 0;
            this.btnGraphic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGraphic.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGraphic.ForeColor = System.Drawing.Color.Teal;
            this.btnGraphic.Location = new System.Drawing.Point(0, 235);
            this.btnGraphic.Margin = new System.Windows.Forms.Padding(4);
            this.btnGraphic.Name = "btnGraphic";
            this.btnGraphic.Size = new System.Drawing.Size(259, 36);
            this.btnGraphic.TabIndex = 34;
            this.btnGraphic.Text = "Grafik";
            this.btnGraphic.UseVisualStyleBackColor = true;
            this.btnGraphic.Click += new System.EventHandler(this.btnGraphic_Click);
            // 
            // btnOverview
            // 
            this.btnOverview.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOverview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOverview.Enabled = false;
            this.btnOverview.FlatAppearance.BorderSize = 0;
            this.btnOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOverview.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOverview.ForeColor = System.Drawing.Color.Teal;
            this.btnOverview.Location = new System.Drawing.Point(0, 321);
            this.btnOverview.Margin = new System.Windows.Forms.Padding(4);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(259, 36);
            this.btnOverview.TabIndex = 33;
            this.btnOverview.Text = "Überblick";
            this.btnOverview.UseVisualStyleBackColor = true;
            this.btnOverview.Click += new System.EventHandler(this.btnOverview_Click);
            // 
            // btnGPS
            // 
            this.btnGPS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGPS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnGPS.Enabled = false;
            this.btnGPS.FlatAppearance.BorderSize = 0;
            this.btnGPS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGPS.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGPS.ForeColor = System.Drawing.Color.Teal;
            this.btnGPS.Location = new System.Drawing.Point(0, 278);
            this.btnGPS.Margin = new System.Windows.Forms.Padding(4);
            this.btnGPS.Name = "btnGPS";
            this.btnGPS.Size = new System.Drawing.Size(259, 36);
            this.btnGPS.TabIndex = 32;
            this.btnGPS.Text = "GPS";
            this.btnGPS.UseVisualStyleBackColor = true;
            this.btnGPS.Click += new System.EventHandler(this.btnGPS_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.Controls.Add(this.lblHide);
            this.pnlContent.Controls.Add(this.lblShow);
            this.pnlContent.Location = new System.Drawing.Point(259, 0);
            this.pnlContent.Margin = new System.Windows.Forms.Padding(4);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1067, 554);
            this.pnlContent.TabIndex = 32;
            // 
            // lblHide
            // 
            this.lblHide.AutoSize = true;
            this.lblHide.BackColor = System.Drawing.Color.Transparent;
            this.lblHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHide.Location = new System.Drawing.Point(267, 11);
            this.lblHide.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHide.Name = "lblHide";
            this.lblHide.Size = new System.Drawing.Size(17, 17);
            this.lblHide.TabIndex = 36;
            this.lblHide.Text = "<";
            this.lblHide.Click += new System.EventHandler(this.lblHide_Click);
            // 
            // lblShow
            // 
            this.lblShow.AutoSize = true;
            this.lblShow.BackColor = System.Drawing.Color.Transparent;
            this.lblShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow.Location = new System.Drawing.Point(8, 11);
            this.lblShow.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(17, 17);
            this.lblShow.TabIndex = 37;
            this.lblShow.Text = ">";
            this.lblShow.Visible = false;
            this.lblShow.Click += new System.EventHandler(this.lblShow_Click);
            // 
            // ofd
            // 
            this.ofd.FileName = "openFileDialog1";
            // 
            // tmrFade
            // 
            this.tmrFade.Interval = 1;
            this.tmrFade.Tick += new System.EventHandler(this.tmrFade_Tick);
            // 
            // btnShowDynamic
            // 
            this.btnShowDynamic.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnShowDynamic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnShowDynamic.FlatAppearance.BorderSize = 0;
            this.btnShowDynamic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowDynamic.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowDynamic.ForeColor = System.Drawing.Color.Teal;
            this.btnShowDynamic.Location = new System.Drawing.Point(0, 365);
            this.btnShowDynamic.Margin = new System.Windows.Forms.Padding(4);
            this.btnShowDynamic.Name = "btnShowDynamic";
            this.btnShowDynamic.Size = new System.Drawing.Size(259, 36);
            this.btnShowDynamic.TabIndex = 39;
            this.btnShowDynamic.Text = "Dynamik";
            this.btnShowDynamic.UseVisualStyleBackColor = true;
            this.btnShowDynamic.Click += new System.EventHandler(this.btnShowDynamic_Click);
            // 
            // DriversGuideApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1325, 554);
            this.Controls.Add(this.pnlSideBar);
            this.Controls.Add(this.pnlContent);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DriversGuideApp";
            this.Text = "DriversGuideApp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DriversGuideApp_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DriversGuideApp_FormClosed);
            this.pnlSideBar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Button btnOverview;
        private System.Windows.Forms.Button btnGPS;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btnGraphic;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.Label lblHide;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrFade;
        private System.Windows.Forms.Button btnShowDynamic;
    }
}