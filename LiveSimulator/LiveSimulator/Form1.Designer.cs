namespace LiveSimulator
{
    partial class Form1
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
            this.btnFileAuswahl = new System.Windows.Forms.Button();
            this.txt = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnSpeicherpfad = new System.Windows.Forms.Button();
            this.btnStartSimulation = new System.Windows.Forms.Button();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtSpeicherintervall = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFileAuswahl
            // 
            this.btnFileAuswahl.Location = new System.Drawing.Point(12, 12);
            this.btnFileAuswahl.Name = "btnFileAuswahl";
            this.btnFileAuswahl.Size = new System.Drawing.Size(134, 23);
            this.btnFileAuswahl.TabIndex = 0;
            this.btnFileAuswahl.Text = "File zum Auslesen auswählen";
            this.btnFileAuswahl.UseVisualStyleBackColor = true;
            this.btnFileAuswahl.Click += new System.EventHandler(this.btnFileAuswahl_Click);
            // 
            // txt
            // 
            this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt.Location = new System.Drawing.Point(12, 77);
            this.txt.Multiline = true;
            this.txt.Name = "txt";
            this.txt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt.Size = new System.Drawing.Size(776, 361);
            this.txt.TabIndex = 1;
            // 
            // ofd
            // 
            this.ofd.FileName = "ofd";
            // 
            // btnSpeicherpfad
            // 
            this.btnSpeicherpfad.Location = new System.Drawing.Point(152, 12);
            this.btnSpeicherpfad.Name = "btnSpeicherpfad";
            this.btnSpeicherpfad.Size = new System.Drawing.Size(228, 23);
            this.btnSpeicherpfad.TabIndex = 2;
            this.btnSpeicherpfad.Text = "Speicherpfad des neuen Files auswählen";
            this.btnSpeicherpfad.UseVisualStyleBackColor = true;
            this.btnSpeicherpfad.Click += new System.EventHandler(this.btnSpeicherpfad_Click);
            // 
            // btnStartSimulation
            // 
            this.btnStartSimulation.Location = new System.Drawing.Point(386, 12);
            this.btnStartSimulation.Name = "btnStartSimulation";
            this.btnStartSimulation.Size = new System.Drawing.Size(185, 23);
            this.btnStartSimulation.TabIndex = 3;
            this.btnStartSimulation.Text = "Simulation Starten";
            this.btnStartSimulation.UseVisualStyleBackColor = true;
            this.btnStartSimulation.Click += new System.EventHandler(this.btnStartSimulation_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtSpeicherintervall
            // 
            this.txtSpeicherintervall.Location = new System.Drawing.Point(708, 15);
            this.txtSpeicherintervall.Name = "txtSpeicherintervall";
            this.txtSpeicherintervall.Size = new System.Drawing.Size(80, 20);
            this.txtSpeicherintervall.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(577, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Speicherintervall (500ms)";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(12, 41);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(134, 23);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Simulation Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSpeicherintervall);
            this.Controls.Add(this.btnStartSimulation);
            this.Controls.Add(this.btnSpeicherpfad);
            this.Controls.Add(this.txt);
            this.Controls.Add(this.btnFileAuswahl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFileAuswahl;
        private System.Windows.Forms.TextBox txt;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnSpeicherpfad;
        private System.Windows.Forms.Button btnStartSimulation;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtSpeicherintervall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStop;
    }
}

