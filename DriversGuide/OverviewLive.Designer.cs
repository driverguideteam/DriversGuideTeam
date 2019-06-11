namespace DriversGuide
{
    partial class OverviewLive
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picGeneral = new System.Windows.Forms.PictureBox();
            this.picDistribution = new System.Windows.Forms.PictureBox();
            this.picDistance = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistance)).BeginInit();
            this.SuspendLayout();
            // 
            // picGeneral
            // 
            this.picGeneral.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picGeneral.Location = new System.Drawing.Point(578, 12);
            this.picGeneral.Name = "picGeneral";
            this.picGeneral.Size = new System.Drawing.Size(260, 200);
            this.picGeneral.TabIndex = 47;
            this.picGeneral.TabStop = false;
            this.picGeneral.Click += new System.EventHandler(this.picGeneral_Click);
            this.picGeneral.Paint += new System.Windows.Forms.PaintEventHandler(this.picGeneral_Paint);
            // 
            // picDistribution
            // 
            this.picDistribution.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picDistribution.Location = new System.Drawing.Point(312, 12);
            this.picDistribution.Name = "picDistribution";
            this.picDistribution.Size = new System.Drawing.Size(260, 200);
            this.picDistribution.TabIndex = 46;
            this.picDistribution.TabStop = false;
            this.picDistribution.Click += new System.EventHandler(this.picDistribution_Click);
            this.picDistribution.Paint += new System.Windows.Forms.PaintEventHandler(this.picDistribution_Paint);
            // 
            // picDistance
            // 
            this.picDistance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picDistance.Location = new System.Drawing.Point(46, 12);
            this.picDistance.Name = "picDistance";
            this.picDistance.Size = new System.Drawing.Size(260, 200);
            this.picDistance.TabIndex = 45;
            this.picDistance.TabStop = false;
            this.picDistance.Click += new System.EventHandler(this.picDistance_Click);
            this.picDistance.Paint += new System.Windows.Forms.PaintEventHandler(this.picDistance_Paint);
            // 
            // OverviewLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.picGeneral);
            this.Controls.Add(this.picDistribution);
            this.Controls.Add(this.picDistance);
            this.Name = "OverviewLive";
            this.Size = new System.Drawing.Size(885, 225);
            this.Click += new System.EventHandler(this.OverviewLive_Click);
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picDistance;
        private System.Windows.Forms.PictureBox picGeneral;
        private System.Windows.Forms.PictureBox picDistribution;
    }
}
