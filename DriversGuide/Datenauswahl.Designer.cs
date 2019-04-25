namespace DriversGuide
{
    partial class Datenauswahl
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
            this.lblDatenauswahl = new System.Windows.Forms.Label();
            this.lstChooseData = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lblDatenauswahl
            // 
            this.lblDatenauswahl.AutoSize = true;
            this.lblDatenauswahl.Location = new System.Drawing.Point(240, 9);
            this.lblDatenauswahl.Name = "lblDatenauswahl";
            this.lblDatenauswahl.Size = new System.Drawing.Size(332, 17);
            this.lblDatenauswahl.TabIndex = 11;
            this.lblDatenauswahl.Text = "Wählen Sie Daten zur Anzeige per Doppelklick aus!";
            // 
            // lstChooseData
            // 
            this.lstChooseData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstChooseData.FormattingEnabled = true;
            this.lstChooseData.ItemHeight = 16;
            this.lstChooseData.Location = new System.Drawing.Point(3, 38);
            this.lstChooseData.Name = "lstChooseData";
            this.lstChooseData.Size = new System.Drawing.Size(794, 404);
            this.lstChooseData.TabIndex = 10;
            this.lstChooseData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstChooseData_MouseDoubleClick);
            // 
            // Datenauswahl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblDatenauswahl);
            this.Controls.Add(this.lstChooseData);
            this.Name = "Datenauswahl";
            this.Text = "Datenauswahl";
            this.Load += new System.EventHandler(this.Datenauswahl_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDatenauswahl;
        private System.Windows.Forms.ListBox lstChooseData;
    }
}