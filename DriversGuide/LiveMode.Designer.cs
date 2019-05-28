namespace DriversGuide
{
    partial class LiveMode
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
            this.pnlTopContent = new System.Windows.Forms.Panel();
            this.pnlBottomContent = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btn_Fileauswahl = new System.Windows.Forms.Panel();
            this.btnOverview = new System.Windows.Forms.Panel();
            this.btnGPS = new System.Windows.Forms.Panel();
            this.lblHide = new System.Windows.Forms.Label();
            this.lblShow = new System.Windows.Forms.Label();
            this.tmrFade = new System.Windows.Forms.Timer(this.components);
            this.pnlSideBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlTopContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSideBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlSideBar.Controls.Add(this.btnGPS);
            this.pnlSideBar.Controls.Add(this.btnOverview);
            this.pnlSideBar.Controls.Add(this.btn_Fileauswahl);
            this.pnlSideBar.Controls.Add(this.panel1);
            this.pnlSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(109, 450);
            this.pnlSideBar.TabIndex = 32;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(109, 69);
            this.panel1.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drivers \r\nGuide";
            // 
            // pnlTopContent
            // 
            this.pnlTopContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopContent.Controls.Add(this.lblHide);
            this.pnlTopContent.Controls.Add(this.lblShow);
            this.pnlTopContent.Location = new System.Drawing.Point(109, 0);
            this.pnlTopContent.Name = "pnlTopContent";
            this.pnlTopContent.Size = new System.Drawing.Size(563, 225);
            this.pnlTopContent.TabIndex = 39;
            this.pnlTopContent.Click += new System.EventHandler(this.pnlTopContent_Click);
            // 
            // pnlBottomContent
            // 
            this.pnlBottomContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomContent.Location = new System.Drawing.Point(109, 225);
            this.pnlBottomContent.Name = "pnlBottomContent";
            this.pnlBottomContent.Size = new System.Drawing.Size(563, 225);
            this.pnlBottomContent.TabIndex = 40;
            this.pnlBottomContent.Click += new System.EventHandler(this.pnlBottomContent_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ofd
            // 
            this.ofd.FileName = "ofd";
            // 
            // btn_Fileauswahl
            // 
            this.btn_Fileauswahl.Location = new System.Drawing.Point(0, 95);
            this.btn_Fileauswahl.Name = "btn_Fileauswahl";
            this.btn_Fileauswahl.Size = new System.Drawing.Size(109, 100);
            this.btn_Fileauswahl.TabIndex = 0;
            this.btn_Fileauswahl.Click += new System.EventHandler(this.btn_Fileauswahl_Click);
            this.btn_Fileauswahl.Paint += new System.Windows.Forms.PaintEventHandler(this.btn_Fileauswahl_Paint);
            this.btn_Fileauswahl.MouseEnter += new System.EventHandler(this.btn_Fileauswahl_MouseEnter);
            this.btn_Fileauswahl.MouseLeave += new System.EventHandler(this.btn_Fileauswahl_MouseLeave);
            this.btn_Fileauswahl.Resize += new System.EventHandler(this.btn_Fileauswahl_Resize);
            // 
            // btnOverview
            // 
            this.btnOverview.Location = new System.Drawing.Point(0, 211);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(109, 100);
            this.btnOverview.TabIndex = 1;
            this.btnOverview.Paint += new System.Windows.Forms.PaintEventHandler(this.btnOverview_Paint);
            this.btnOverview.MouseEnter += new System.EventHandler(this.btnOverview_MouseEnter);
            this.btnOverview.MouseLeave += new System.EventHandler(this.btnOverview_MouseLeave);
            this.btnOverview.Resize += new System.EventHandler(this.btnOverview_Resize);
            // 
            // btnGPS
            // 
            this.btnGPS.Location = new System.Drawing.Point(0, 328);
            this.btnGPS.Name = "btnGPS";
            this.btnGPS.Size = new System.Drawing.Size(109, 100);
            this.btnGPS.TabIndex = 2;
            this.btnGPS.Click += new System.EventHandler(this.btnGPS_Click);
            this.btnGPS.Paint += new System.Windows.Forms.PaintEventHandler(this.btnGPS_Paint);
            this.btnGPS.MouseEnter += new System.EventHandler(this.btnGPS_MouseEnter);
            this.btnGPS.MouseLeave += new System.EventHandler(this.btnGPS_MouseLeave);
            this.btnGPS.Resize += new System.EventHandler(this.btnGPS_Resize);
            // 
            // lblHide
            // 
            this.lblHide.AutoSize = true;
            this.lblHide.BackColor = System.Drawing.Color.Transparent;
            this.lblHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHide.Location = new System.Drawing.Point(115, 9);
            this.lblHide.Name = "lblHide";
            this.lblHide.Size = new System.Drawing.Size(14, 13);
            this.lblHide.TabIndex = 38;
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
            this.lblShow.TabIndex = 39;
            this.lblShow.Text = ">";
            this.lblShow.Visible = false;
            this.lblShow.Click += new System.EventHandler(this.lblShow_Click);
            // 
            // tmrFade
            // 
            this.tmrFade.Interval = 1;
            this.tmrFade.Tick += new System.EventHandler(this.tmrFade_Tick);
            // 
            // LiveMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(672, 450);
            this.Controls.Add(this.pnlBottomContent);
            this.Controls.Add(this.pnlTopContent);
            this.Controls.Add(this.pnlSideBar);
            this.Name = "LiveMode";
            this.Text = "LiveMode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LiveMode_FormClosed);
            this.pnlSideBar.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlTopContent.ResumeLayout(false);
            this.pnlTopContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTopContent;
        private System.Windows.Forms.Panel pnlBottomContent;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Panel btn_Fileauswahl;
        private System.Windows.Forms.Panel btnOverview;
        private System.Windows.Forms.Panel btnGPS;
        private System.Windows.Forms.Label lblHide;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.Timer tmrFade;
    }
}