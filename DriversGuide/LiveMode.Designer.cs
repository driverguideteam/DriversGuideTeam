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
            this.pnlSideBar = new System.Windows.Forms.Panel();
            this.btnGPS = new System.Windows.Forms.Button();
            this.btnOverview = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTopContent = new System.Windows.Forms.Panel();
            this.pnlBottomContent = new System.Windows.Forms.Panel();
            this.pnlSideBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSideBar
            // 
            this.pnlSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSideBar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlSideBar.Controls.Add(this.btnGPS);
            this.pnlSideBar.Controls.Add(this.btnOverview);
            this.pnlSideBar.Controls.Add(this.panel1);
            this.pnlSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnlSideBar.Name = "pnlSideBar";
            this.pnlSideBar.Size = new System.Drawing.Size(109, 450);
            this.pnlSideBar.TabIndex = 32;
            // 
            // btnGPS
            // 
            this.btnGPS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnGPS.BackColor = System.Drawing.Color.White;
            this.btnGPS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnGPS.FlatAppearance.BorderSize = 0;
            this.btnGPS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGPS.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGPS.ForeColor = System.Drawing.Color.Teal;
            this.btnGPS.Location = new System.Drawing.Point(3, 273);
            this.btnGPS.Name = "btnGPS";
            this.btnGPS.Size = new System.Drawing.Size(103, 126);
            this.btnGPS.TabIndex = 40;
            this.btnGPS.Text = "GPS";
            this.btnGPS.UseVisualStyleBackColor = false;
            this.btnGPS.Click += new System.EventHandler(this.btnGPS_Click);
            // 
            // btnOverview
            // 
            this.btnOverview.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOverview.BackColor = System.Drawing.Color.White;
            this.btnOverview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnOverview.FlatAppearance.BorderSize = 0;
            this.btnOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOverview.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOverview.ForeColor = System.Drawing.Color.Teal;
            this.btnOverview.Location = new System.Drawing.Point(3, 110);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(103, 126);
            this.btnOverview.TabIndex = 39;
            this.btnOverview.Text = "Übersicht";
            this.btnOverview.UseVisualStyleBackColor = false;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSideBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlTopContent;
        private System.Windows.Forms.Panel pnlBottomContent;
        private System.Windows.Forms.Button btnGPS;
        private System.Windows.Forms.Button btnOverview;
    }
}