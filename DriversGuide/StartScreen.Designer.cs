namespace DriversGuide
{
    partial class StartScreen
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
            this.btnEval = new System.Windows.Forms.Button();
            this.btnLive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEval
            // 
            this.btnEval.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnEval.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnEval.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEval.FlatAppearance.BorderSize = 0;
            this.btnEval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEval.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEval.ForeColor = System.Drawing.Color.Black;
            this.btnEval.Location = new System.Drawing.Point(48, 42);
            this.btnEval.Name = "btnEval";
            this.btnEval.Size = new System.Drawing.Size(214, 158);
            this.btnEval.TabIndex = 36;
            this.btnEval.Text = "Auswertung";
            this.btnEval.UseVisualStyleBackColor = false;
            this.btnEval.Click += new System.EventHandler(this.btnEval_Click);
            // 
            // btnLive
            // 
            this.btnLive.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnLive.BackColor = System.Drawing.Color.LightSkyBlue;
            this.btnLive.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnLive.FlatAppearance.BorderSize = 0;
            this.btnLive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLive.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLive.ForeColor = System.Drawing.Color.Black;
            this.btnLive.Location = new System.Drawing.Point(305, 42);
            this.btnLive.Name = "btnLive";
            this.btnLive.Size = new System.Drawing.Size(214, 158);
            this.btnLive.TabIndex = 37;
            this.btnLive.Text = "Live - Modus";
            this.btnLive.UseVisualStyleBackColor = false;
            this.btnLive.Click += new System.EventHandler(this.btnLive_Click);
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(561, 248);
            this.Controls.Add(this.btnLive);
            this.Controls.Add(this.btnEval);
            this.Name = "StartScreen";
            this.Text = "Drivers Guide";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEval;
        private System.Windows.Forms.Button btnLive;
    }
}