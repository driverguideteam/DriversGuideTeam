namespace DriversGuide
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
            this.btnShowDynamic = new System.Windows.Forms.Panel();
            this.btnOverview = new System.Windows.Forms.Panel();
            this.btnGPS = new System.Windows.Forms.Panel();
            this.btnGraphic = new System.Windows.Forms.Panel();
            this.btnReadFile = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblHide = new System.Windows.Forms.Label();
            this.lblShow = new System.Windows.Forms.Label();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.tmrFade = new System.Windows.Forms.Timer(this.components);
            this.pnlSideBar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSideBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlSideBar.Controls.Add(this.btnShowDynamic);
            this.pnlSideBar.Controls.Add(this.btnOverview);
            this.pnlSideBar.Controls.Add(this.btnGPS);
            this.pnlSideBar.Controls.Add(this.btnGraphic);
            this.pnlSideBar.Controls.Add(this.btnReadFile);
            this.pnlSideBar.Controls.Add(this.pnlLogo);
            this.pnlSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(194, 450);
            this.pnlSideBar.TabIndex = 31;
            // 
            // btnShowDynamic
            // 
            this.btnShowDynamic.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnShowDynamic.Enabled = false;
            this.btnShowDynamic.Location = new System.Drawing.Point(0, 298);
            this.btnShowDynamic.Name = "btnShowDynamic";
            this.btnShowDynamic.Size = new System.Drawing.Size(194, 30);
            this.btnShowDynamic.TabIndex = 44;
            this.btnShowDynamic.Click += new System.EventHandler(this.btnShowDynamic_Click);
            this.btnShowDynamic.Paint += new System.Windows.Forms.PaintEventHandler(this.btnShowDynamic_Paint);
            this.btnShowDynamic.MouseEnter += new System.EventHandler(this.btnShowDynamic_MouseEnter);
            this.btnShowDynamic.MouseLeave += new System.EventHandler(this.btnShowDynamic_MouseLeave);
            this.btnShowDynamic.Resize += new System.EventHandler(this.btnShowDynamic_Resize);
            // 
            // btnOverview
            // 
            this.btnOverview.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnOverview.Enabled = false;
            this.btnOverview.Location = new System.Drawing.Point(0, 262);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(194, 30);
            this.btnOverview.TabIndex = 43;
            this.btnOverview.Click += new System.EventHandler(this.btnOverview_Click);
            this.btnOverview.Paint += new System.Windows.Forms.PaintEventHandler(this.btnOverview_Paint);
            this.btnOverview.MouseEnter += new System.EventHandler(this.btnOverview_MouseEnter);
            this.btnOverview.MouseLeave += new System.EventHandler(this.btnOverview_MouseLeave);
            this.btnOverview.Resize += new System.EventHandler(this.btnOverview_Resize);
            // 
            // btnGPS
            // 
            this.btnGPS.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGPS.Enabled = false;
            this.btnGPS.Location = new System.Drawing.Point(0, 226);
            this.btnGPS.Name = "btnGPS";
            this.btnGPS.Size = new System.Drawing.Size(194, 30);
            this.btnGPS.TabIndex = 42;
            this.btnGPS.Click += new System.EventHandler(this.btnGPS_Click);
            this.btnGPS.Paint += new System.Windows.Forms.PaintEventHandler(this.btnGPS_Paint);
            this.btnGPS.MouseEnter += new System.EventHandler(this.btnGPS_MouseEnter);
            this.btnGPS.MouseLeave += new System.EventHandler(this.btnGPS_MouseLeave);
            this.btnGPS.Resize += new System.EventHandler(this.btnGPS_Resize);
            // 
            // btnGraphic
            // 
            this.btnGraphic.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnGraphic.Enabled = false;
            this.btnGraphic.Location = new System.Drawing.Point(0, 190);
            this.btnGraphic.Name = "btnGraphic";
            this.btnGraphic.Size = new System.Drawing.Size(194, 30);
            this.btnGraphic.TabIndex = 41;
            this.btnGraphic.Click += new System.EventHandler(this.btnGraphic_Click);
            this.btnGraphic.Paint += new System.Windows.Forms.PaintEventHandler(this.btnGraphic_Paint);
            this.btnGraphic.MouseEnter += new System.EventHandler(this.btnGraphic_MouseEnter);
            this.btnGraphic.MouseLeave += new System.EventHandler(this.btnGraphic_MouseLeave);
            this.btnGraphic.Resize += new System.EventHandler(this.btnGraphic_Resize);
            // 
            // btnReadFile
            // 
            this.btnReadFile.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnReadFile.Location = new System.Drawing.Point(0, 155);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(194, 30);
            this.btnReadFile.TabIndex = 40;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            this.btnReadFile.Paint += new System.Windows.Forms.PaintEventHandler(this.btnReadFile_Paint);
            this.btnReadFile.MouseEnter += new System.EventHandler(this.btnReadFile_MouseEnter);
            this.btnReadFile.MouseLeave += new System.EventHandler(this.btnReadFile_MouseLeave);
            this.btnReadFile.Resize += new System.EventHandler(this.btnReadFile_Resize);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.LightBlue;
            this.pnlLogo.Controls.Add(this.label1);
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(194, 69);
            this.pnlLogo.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drivers Guide";
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.Controls.Add(this.lblHide);
            this.pnlContent.Controls.Add(this.lblShow);
            this.pnlContent.Location = new System.Drawing.Point(194, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(800, 450);
            this.pnlContent.TabIndex = 32;
            // 
            // lblHide
            // 
            this.lblHide.AutoSize = true;
            this.lblHide.BackColor = System.Drawing.Color.Transparent;
            this.lblHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHide.Location = new System.Drawing.Point(200, 9);
            this.lblHide.Name = "lblHide";
            this.lblHide.Size = new System.Drawing.Size(14, 13);
            this.lblHide.TabIndex = 36;
            this.lblHide.Text = "<";
            this.lblHide.Click += new System.EventHandler(this.lblHide_Click);
            // 
            // lblShow
            // 
            this.lblShow.AutoSize = true;
            this.lblShow.BackColor = System.Drawing.Color.Transparent;
            this.lblShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShow.Location = new System.Drawing.Point(6, 9);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(14, 13);
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
            // DriversGuideApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(994, 450);
            this.Controls.Add(this.pnlSideBar);
            this.Controls.Add(this.pnlContent);
            this.Name = "DriversGuideApp";
            this.Text = "Auswertung";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DriversGuideApp_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DriversGuideApp_FormClosed);
            this.Resize += new System.EventHandler(this.DriversGuideApp_Resize);
            this.pnlSideBar.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.Label lblHide;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer tmrFade;
        private System.Windows.Forms.Panel btnReadFile;
        private System.Windows.Forms.Panel btnGraphic;
        private System.Windows.Forms.Panel btnShowDynamic;
        private System.Windows.Forms.Panel btnOverview;
        private System.Windows.Forms.Panel btnGPS;
    }
}