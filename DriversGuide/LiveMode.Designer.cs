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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveMode));
            this.pnlSideBar = new System.Windows.Forms.Panel();
            this.btnDynamic = new System.Windows.Forms.Panel();
            this.btnSimulation = new System.Windows.Forms.Panel();
            this.btnGPS = new System.Windows.Forms.Panel();
            this.btnOverview = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTopContent = new System.Windows.Forms.Panel();
            this.lblHide = new System.Windows.Forms.Label();
            this.lblShow = new System.Windows.Forms.Label();
            this.pnlBottomContent = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.tmrFade = new System.Windows.Forms.Timer(this.components);
            this.timerSimulation = new System.Windows.Forms.Timer(this.components);
            this.pnlSideBar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlTopContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSideBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlSideBar.Controls.Add(this.btnDynamic);
            this.pnlSideBar.Controls.Add(this.btnSimulation);
            this.pnlSideBar.Controls.Add(this.btnGPS);
            this.pnlSideBar.Controls.Add(this.btnOverview);
            this.pnlSideBar.Controls.Add(this.pnlLogo);
            this.pnlSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(109, 450);
            this.pnlSideBar.TabIndex = 32;
            // 
            // btnDynamic
            // 
            this.btnDynamic.Enabled = false;
            this.btnDynamic.Location = new System.Drawing.Point(0, 269);
            this.btnDynamic.Name = "btnDynamic";
            this.btnDynamic.Size = new System.Drawing.Size(109, 75);
            this.btnDynamic.TabIndex = 0;
            this.btnDynamic.Click += new System.EventHandler(this.btnDyn_Click);
            this.btnDynamic.Paint += new System.Windows.Forms.PaintEventHandler(this.btnDynamic_Paint);
            this.btnDynamic.MouseEnter += new System.EventHandler(this.btnDynamic_MouseEnter);
            this.btnDynamic.MouseLeave += new System.EventHandler(this.btnDynamic_MouseLeave);
            this.btnDynamic.Resize += new System.EventHandler(this.btnDynamic_Resize);
            // 
            // btnSimulation
            // 
            this.btnSimulation.Location = new System.Drawing.Point(0, 86);
            this.btnSimulation.Name = "btnSimulation";
            this.btnSimulation.Size = new System.Drawing.Size(109, 75);
            this.btnSimulation.TabIndex = 3;
            this.btnSimulation.Click += new System.EventHandler(this.btnSimulation_Click);
            this.btnSimulation.Paint += new System.Windows.Forms.PaintEventHandler(this.btnSimulation_Paint);
            this.btnSimulation.MouseEnter += new System.EventHandler(this.btnSimulation_MouseEnter);
            this.btnSimulation.MouseLeave += new System.EventHandler(this.btnSimulation_MouseLeave);
            this.btnSimulation.Resize += new System.EventHandler(this.btnSimulation_Resize);
            // 
            // btnGPS
            // 
            this.btnGPS.Enabled = false;
            this.btnGPS.Location = new System.Drawing.Point(0, 358);
            this.btnGPS.Name = "btnGPS";
            this.btnGPS.Size = new System.Drawing.Size(109, 75);
            this.btnGPS.TabIndex = 2;
            this.btnGPS.Click += new System.EventHandler(this.btnGPS_Click);
            this.btnGPS.Paint += new System.Windows.Forms.PaintEventHandler(this.btnGPS_Paint);
            this.btnGPS.MouseEnter += new System.EventHandler(this.btnGPS_MouseEnter);
            this.btnGPS.MouseLeave += new System.EventHandler(this.btnGPS_MouseLeave);
            this.btnGPS.Resize += new System.EventHandler(this.btnGPS_Resize);
            // 
            // btnOverview
            // 
            this.btnOverview.Enabled = false;
            this.btnOverview.Location = new System.Drawing.Point(0, 178);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(109, 75);
            this.btnOverview.TabIndex = 1;
            this.btnOverview.Click += new System.EventHandler(this.btnOverview_Click);
            this.btnOverview.Paint += new System.Windows.Forms.PaintEventHandler(this.btnOverview_Paint);
            this.btnOverview.MouseEnter += new System.EventHandler(this.btnOverview_MouseEnter);
            this.btnOverview.MouseLeave += new System.EventHandler(this.btnOverview_MouseLeave);
            this.btnOverview.Resize += new System.EventHandler(this.btnOverview_Resize);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.LightBlue;
            this.pnlLogo.Controls.Add(this.label1);
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(109, 69);
            this.pnlLogo.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drivers \r\nGuide";
            // 
            // pnlTopContent
            // 
            this.pnlTopContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTopContent.BackColor = System.Drawing.Color.White;
            this.pnlTopContent.Controls.Add(this.lblHide);
            this.pnlTopContent.Controls.Add(this.lblShow);
            this.pnlTopContent.Location = new System.Drawing.Point(109, 0);
            this.pnlTopContent.Name = "pnlTopContent";
            this.pnlTopContent.Size = new System.Drawing.Size(885, 225);
            this.pnlTopContent.TabIndex = 39;
            this.pnlTopContent.Click += new System.EventHandler(this.pnlTopContent_Click);
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
            // pnlBottomContent
            // 
            this.pnlBottomContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBottomContent.BackColor = System.Drawing.Color.White;
            this.pnlBottomContent.Location = new System.Drawing.Point(109, 225);
            this.pnlBottomContent.Name = "pnlBottomContent";
            this.pnlBottomContent.Size = new System.Drawing.Size(885, 225);
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
            // tmrFade
            // 
            this.tmrFade.Interval = 1;
            this.tmrFade.Tick += new System.EventHandler(this.tmrFade_Tick);
            // 
            // timerSimulation
            // 
            this.timerSimulation.Interval = 1;
            this.timerSimulation.Tick += new System.EventHandler(this.timerSimulation_Tick);
            // 
            // LiveMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(994, 450);
            this.Controls.Add(this.pnlBottomContent);
            this.Controls.Add(this.pnlTopContent);
            this.Controls.Add(this.pnlSideBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1010, 487);
            this.Name = "LiveMode";
            this.Text = "LiveMode";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LiveMode_FormClosed);
            this.Resize += new System.EventHandler(this.LiveMode_Resize);
            this.pnlSideBar.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.pnlTopContent.ResumeLayout(false);
            this.pnlTopContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTopContent;
        private System.Windows.Forms.Panel pnlBottomContent;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Panel btnOverview;
        private System.Windows.Forms.Panel btnGPS;
        private System.Windows.Forms.Label lblHide;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.Timer tmrFade;
        private System.Windows.Forms.Timer timerSimulation;
        private System.Windows.Forms.Panel btnSimulation;
        private System.Windows.Forms.Button btnDyn;
        private System.Windows.Forms.Panel btnDynamic;
    }
}