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
            this.picColdStart = new System.Windows.Forms.PictureBox();
            this.dGV = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picColdStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).BeginInit();
            this.SuspendLayout();
            // 
            // picGeneral
            // 
            this.picGeneral.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picGeneral.Location = new System.Drawing.Point(536, 22);
            this.picGeneral.Name = "picGeneral";
            this.picGeneral.Size = new System.Drawing.Size(260, 200);
            this.picGeneral.TabIndex = 50;
            this.picGeneral.TabStop = false;
            this.picGeneral.Paint += new System.Windows.Forms.PaintEventHandler(this.picGeneral_Paint);
            // 
            // picDistribution
            // 
            this.picDistribution.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picDistribution.Location = new System.Drawing.Point(270, 22);
            this.picDistribution.Name = "picDistribution";
            this.picDistribution.Size = new System.Drawing.Size(260, 200);
            this.picDistribution.TabIndex = 49;
            this.picDistribution.TabStop = false;
            this.picDistribution.Paint += new System.Windows.Forms.PaintEventHandler(this.picDistribution_Paint);
            // 
            // picDistance
            // 
            this.picDistance.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picDistance.Location = new System.Drawing.Point(4, 22);
            this.picDistance.Name = "picDistance";
            this.picDistance.Size = new System.Drawing.Size(260, 200);
            this.picDistance.TabIndex = 48;
            this.picDistance.TabStop = false;
            this.picDistance.Paint += new System.Windows.Forms.PaintEventHandler(this.picDistance_Paint);
            // 
            // picColdStart
            // 
            this.picColdStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picColdStart.Location = new System.Drawing.Point(4, 228);
            this.picColdStart.Name = "picColdStart";
            this.picColdStart.Size = new System.Drawing.Size(260, 200);
            this.picColdStart.TabIndex = 51;
            this.picColdStart.TabStop = false;
            this.picColdStart.Paint += new System.Windows.Forms.PaintEventHandler(this.picColdStart_Paint);
            // 
            // dGV
            // 
            this.dGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV.Location = new System.Drawing.Point(270, 228);
            this.dGV.Name = "dGV";
            this.dGV.Size = new System.Drawing.Size(526, 200);
            this.dGV.TabIndex = 52;
            // 
            // Overview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dGV);
            this.Controls.Add(this.picColdStart);
            this.Controls.Add(this.picGeneral);
            this.Controls.Add(this.picDistribution);
            this.Controls.Add(this.picDistance);
            this.Name = "Overview";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistribution)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picColdStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picGeneral;
        private System.Windows.Forms.PictureBox picDistribution;
        private System.Windows.Forms.PictureBox picDistance;
        private System.Windows.Forms.PictureBox picColdStart;
        private System.Windows.Forms.DataGridView dGV;
    }
}
