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
            this.picHeadingError = new System.Windows.Forms.PictureBox();
            this.picHeadingTip = new System.Windows.Forms.PictureBox();
            this.tipsMessages = new System.Windows.Forms.RichTextBox();
            this.errorMessages = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistribution)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picColdStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeadingError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeadingTip)).BeginInit();
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
            // picHeadingError
            // 
            this.picHeadingError.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picHeadingError.Location = new System.Drawing.Point(536, 228);
            this.picHeadingError.Name = "picHeadingError";
            this.picHeadingError.Size = new System.Drawing.Size(260, 42);
            this.picHeadingError.TabIndex = 52;
            this.picHeadingError.TabStop = false;
            this.picHeadingError.Visible = false;
            this.picHeadingError.Paint += new System.Windows.Forms.PaintEventHandler(this.picHeadingError_Paint);
            // 
            // picHeadingTip
            // 
            this.picHeadingTip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picHeadingTip.Location = new System.Drawing.Point(270, 228);
            this.picHeadingTip.Name = "picHeadingTip";
            this.picHeadingTip.Size = new System.Drawing.Size(260, 42);
            this.picHeadingTip.TabIndex = 53;
            this.picHeadingTip.TabStop = false;
            this.picHeadingTip.Paint += new System.Windows.Forms.PaintEventHandler(this.picHeadingTip_Paint);
            // 
            // tipsMessages
            // 
            this.tipsMessages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tipsMessages.BackColor = System.Drawing.Color.White;
            this.tipsMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tipsMessages.Cursor = System.Windows.Forms.Cursors.Default;
            this.tipsMessages.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.tipsMessages.Location = new System.Drawing.Point(270, 270);
            this.tipsMessages.Name = "tipsMessages";
            this.tipsMessages.ReadOnly = true;
            this.tipsMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.tipsMessages.Size = new System.Drawing.Size(260, 158);
            this.tipsMessages.TabIndex = 56;
            this.tipsMessages.Text = "";
            // 
            // errorMessages
            // 
            this.errorMessages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.errorMessages.BackColor = System.Drawing.Color.White;
            this.errorMessages.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorMessages.Cursor = System.Windows.Forms.Cursors.Default;
            this.errorMessages.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.errorMessages.Location = new System.Drawing.Point(537, 270);
            this.errorMessages.Name = "errorMessages";
            this.errorMessages.ReadOnly = true;
            this.errorMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.errorMessages.Size = new System.Drawing.Size(260, 158);
            this.errorMessages.TabIndex = 57;
            this.errorMessages.Text = "";
            this.errorMessages.Visible = false;
            // 
            // Overview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.errorMessages);
            this.Controls.Add(this.tipsMessages);
            this.Controls.Add(this.picHeadingTip);
            this.Controls.Add(this.picHeadingError);
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
            ((System.ComponentModel.ISupportInitialize)(this.picHeadingError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeadingTip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picGeneral;
        private System.Windows.Forms.PictureBox picDistribution;
        private System.Windows.Forms.PictureBox picDistance;
        private System.Windows.Forms.PictureBox picColdStart;
        private System.Windows.Forms.PictureBox picHeadingError;
        private System.Windows.Forms.PictureBox picHeadingTip;
        private System.Windows.Forms.RichTextBox tipsMessages;
        private System.Windows.Forms.RichTextBox errorMessages;
    }
}
