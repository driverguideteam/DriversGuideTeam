using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DriversGuide
{
    public partial class LiveMode : Form
    {
        StartScreen FormStart;
        GPS FormGPS;
        General FormGeneral;
        TestControl FormTest;
        Bitmap bmp;
        Graphics z;
        bool topBottom = true;
        MeasurementFile LiveDatei;
        private DataTable Dataset = new DataTable();
        bool inout = false;

        Color enabled = Color.Teal;
        Color disabled = Color.Gray;

        public LiveMode(StartScreen caller)
        {
            FormStart = caller;
            FormStart.Hide();

            InitializeComponent();
            bmp = new Bitmap(btn_Fileauswahl.ClientSize.Width, btn_Fileauswahl.ClientSize.Height);
            z = Graphics.FromImage(bmp);
            lblHide.Parent = this;
            lblShow.Parent = this;
            lblHide.BringToFront();
            lblShow.BringToFront();
        }

        private void pnlTopContent_Click(object sender, EventArgs e)
        {
            topBottom = true;
        }

        private void LiveMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.Show();
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            if (topBottom)
            {
                pnlTopContent.Controls.Clear();
                FormTest = new TestControl();
                //myForm.TopLevel = false;
                FormTest.AutoScroll = true;
                pnlTopContent.Controls.Add(FormTest);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormTest.Show();
                FormTest.Dock = DockStyle.Fill;
            }
            else
            {
                pnlBottomContent.Controls.Clear();
                FormTest = new TestControl();
                //myForm.TopLevel = false;
                FormTest.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormTest);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormTest.Show();
                FormTest.Dock = DockStyle.Fill;
            }
        }

        private void pnlBottomContent_Click(object sender, EventArgs e)
        {
            topBottom = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Dataset.Clear();
            Dataset = LiveDatei.ConvertLiveCSVtoDataTable();
        }

        private void btn_Fileauswahl_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Textdateien |*.txt| Alle Dateien|*.*";
            ofd.InitialDirectory = "C:\\Repositories\01_Doku\\Vorgabe\\";
            //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            ofd.Title = "Textdatei öffnen";
            ofd.FileName = "";

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                LiveDatei = new MeasurementFile(ofd.FileName);
                Dataset = LiveDatei.ConvertCSVtoDataTable();
            }
            timer1.Start();
        }

        private void btn_Fileauswahl_MouseLeave(object sender, EventArgs e)
        {
            btn_Fileauswahl.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        private void btn_Fileauswahl_MouseEnter(object sender, EventArgs e)
        {
            btn_Fileauswahl.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        private void btn_Fileauswahl_Paint(object sender, PaintEventArgs e)
        {
            if (btn_Fileauswahl.Enabled)
            {
                DrawInBitmap(btn_Fileauswahl, "File-Auswahl", enabled);
                Graphics g = e.Graphics;                
                g.DrawImage(bmp, 0, 0);                
            }
            else
            {
                DrawInBitmap(btn_Fileauswahl, "File-Auswahl", disabled);
                Graphics g = e.Graphics;                
                g.DrawImage(bmp, 0, 0);
            }
        }

        private void btn_Fileauswahl_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btn_Fileauswahl.ClientSize.Width, btn_Fileauswahl.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btn_Fileauswahl.Invalidate();
        }

        private void DrawInBitmap(Panel caller, string text, Color color)
        {
            z.Clear(caller.BackColor);
            z.SmoothingMode = SmoothingMode.AntiAlias;

            float breite = 109;
            float hoehe = 100;

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmp.Width / breite, bmp.Height / hoehe);

            myMatrix.Translate(breite / 2, hoehe / 2 + 1, MatrixOrder.Prepend);

            z.Transform = myMatrix;

            Font type = new Font("Century Gothic", 12f, FontStyle.Bold);
            Brush style = new SolidBrush(color);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            z.DrawString(text, type, style, new Point(0, 0), sf);
        }

        private void btnOverview_MouseLeave(object sender, EventArgs e)
        {
            btnOverview.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        private void btnOverview_MouseEnter(object sender, EventArgs e)
        {
            btnOverview.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        private void btnOverview_Paint(object sender, PaintEventArgs e)
        {
            if (btnOverview.Enabled)
            {
                DrawInBitmap(btnOverview, "Übersicht", enabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
            else
            {
                DrawInBitmap(btnOverview, "Übersicht", disabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
        }

        private void btnOverview_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnOverview.ClientSize.Width, btnOverview.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnOverview.Invalidate();
        }

        private void btnGPS_MouseLeave(object sender, EventArgs e)
        {
            btnGPS.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        private void btnGPS_MouseEnter(object sender, EventArgs e)
        {
            btnGPS.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        private void btnGPS_Paint(object sender, PaintEventArgs e)
        {
            if (btnGPS.Enabled)
            {
                DrawInBitmap(btnGPS, "GPS", enabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
            else
            {
                DrawInBitmap(btnGPS, "GPS", disabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
        }

        private void btnGPS_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnGPS.ClientSize.Width, btnGPS.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnGPS.Invalidate();
        }

        private void lblHide_Click(object sender, EventArgs e)
        {
            //pnlSideBar.Hide();
            tmrFade.Enabled = true;
            lblHide.Hide();
            //lblShow.Show();
            lblShow.Left = 3;
            //pnlContent.Left = 0;
            //pnlContent.Width = ClientSize.Width;
            tmrFade.Enabled = true;
        }

        private void lblShow_Click(object sender, EventArgs e)
        {
            tmrFade.Enabled = true;
            lblShow.Hide();
            lblHide.Show();
            //pnlSideBar.Show();
            pnlTopContent.Left = pnlSideBar.Width;
            pnlTopContent.Width = ClientSize.Width - pnlSideBar.Width;
            pnlBottomContent.Left = pnlSideBar.Width;
            pnlBottomContent.Width = ClientSize.Width - pnlSideBar.Width;
        }

        private void tmrFade_Tick(object sender, EventArgs e)
        {
            if (inout)
            {
                if (pnlSideBar.Location.X < 0)
                {
                    pnlSideBar.Left += 10;
                    //pnlContent.Left += 10;
                    //pnlContent.Width -= 10;
                }
                else
                {
                    tmrFade.Enabled = false;
                    inout = false;
                    //pnlSideBar.Hide(); 
                    //pnlContent.Left = pnlSideBar.Width;
                    //pnlContent.Width = ClientSize.Width - pnlSideBar.Width;
                }
            }
            else
            {
                if (pnlSideBar.Location.X > -pnlSideBar.Width)
                {
                    pnlSideBar.Left -= 10;
                    //pnlContent.Left -= 10;
                    //pnlContent.Width += 10;
                }
                else
                {
                    tmrFade.Enabled = false;
                    inout = true;
                    pnlTopContent.Left = 0;
                    pnlTopContent.Width = ClientSize.Width;
                    pnlBottomContent.Left = 0;
                    pnlBottomContent.Width = ClientSize.Width;
                    lblShow.Show();
                }
            }
        }
    }
}
