﻿using System;
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
        OverviewLive FormLiveOverview;             
        Bitmap bmp;
        Graphics z;
        bool topBottom = true;
        MeasurementFile LiveDatei;
        private DataTable Dataset = new DataTable();
        bool inout = false;
        bool gpsActive = false;

        Calculations Berechnung;
        Validation Gueltigkeit;

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
            CenterButtons();
        }

        public DataTable GetCompleteDataTable()
        {
            return Dataset;
        }

        private void pnlTopContent_Click(object sender, EventArgs e)
        {
            topBottom = true;
        }

        private void LiveMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.Show();
            timer1.Stop();
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            if (topBottom)
            {
                pnlTopContent.Controls.Clear();
                FormGPS = new GPS(this);
                //myForm.TopLevel = false;
                FormGPS.AutoScroll = true;
                pnlTopContent.Controls.Add(FormGPS);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormGPS.Show();
                FormGPS.Dock = DockStyle.Fill;
                lblHide.BackColor = FormGPS.BackColor;
                gpsActive = true;
            }
            else
            {
                pnlBottomContent.Controls.Clear();
                FormGPS = new GPS(this);
                //myForm.TopLevel = false;
                FormGPS.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormGPS);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormGPS.Show();
                FormGPS.Dock = DockStyle.Fill;
                lblHide.BackColor = FormGPS.BackColor;
                gpsActive = true;
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
            DoCalculations(false);
        }

        private void DoCalculations(bool first)
        {
            //InitValueData();
            Berechnung = new Calculations();
            Gueltigkeit = new Validation();
            string column_speed = "OBD_Vehicle_Speed_(PID_13)";
            string column_acc = "ai";
            string column_dynamic = "a*v";
            string column_distance = "di";
            string column_time = "Time";
            string column_coolant = "OBD_Engine_Coolant_Temperature_(PID_5)";
            double avgUrban = 0, avgRural = 0, avgMotorway = 0;
            double distrUrban = 0, distrRural = 0, distrMotorway = 0;
            double tripUrban = 0, tripRural = 0, tripMotorway = 0;
            DataTable urban_temp = new DataTable();
            DataTable rural_temp = new DataTable();
            DataTable motorway_temp = new DataTable();

            List<string> errors = new List<string>();

            Berechnung.CalcReq(ref Dataset, column_speed, first);

            //PerformMutliplikationOnColumn(ref test, column_acc, 2);

            ///*calc = */Berechnung.CalcAll(Dataset, column_speed, column_acc, column_dynamic, column_distance);
            //Berechnung.GetIntervals(ref urban_temp, ref rural_temp, ref motorway_temp);
            ///*valid = */Gueltigkeit.CheckValidity(Dataset, column_speed, column_time, column_coolant, column_distance);

            //errors = Gueltigkeit.GetErrors();

            //Berechnung.SepIntervals(test, column_speed);
            //Berechnung.GetIntervals(ref urban_temp, ref rural_temp, ref motorway_temp);
            //Berechnung.AddUnits(units);

            //Berechnung.GetAvgSpeed(ref avgUrban, ref avgRural, ref avgMotorway);
            //Berechnung.GetTripInt(ref tripUrban, ref tripRural, ref tripMotorway);
            //Gueltigkeit.GetDistribution(ref distrUrban, ref distrRural, ref distrMotorway);

            //values.Rows[1]["Geschwindigkeit"] = avgUrban;
            //values.Rows[2]["Geschwindigkeit"] = avgRural;
            //values.Rows[3]["Geschwindigkeit"] = avgMotorway;

            //values.Rows[1]["Verteilung"] = distrUrban;
            //values.Rows[2]["Verteilung"] = distrRural;
            //values.Rows[3]["Verteilung"] = distrMotorway;

            //values.Rows[0]["Strecke"] = (double)test.Compute("SUM([" + column_distance + "])", "") / 1000;
            //values.Rows[1]["Strecke"] = tripUrban;
            //values.Rows[2]["Strecke"] = tripRural;
            //values.Rows[3]["Strecke"] = tripMotorway;

            //values.Rows[0]["Dauer"] = Convert.ToDouble(test.Rows[test.Rows.Count - 1][column_time]) / 60000d;
            //values.Rows[0]["Haltezeit"] = Gueltigkeit.GetHoldDurtation();
            //values.Rows[0]["Hoechstgeschwindigkeit"] = Gueltigkeit.GetMaxSpeed();
            //values.Rows[0]["Kaltstart Hoechstgeschwindigkeit"] = Gueltigkeit.GetMaxSpeedCold();
            //values.Rows[0]["Kaltstart Durchschnittsgeschwindigkeit"] = Gueltigkeit.GetAvgSpeedCold();
            //values.Rows[0]["Kaltstart Haltezeit"] = Gueltigkeit.GetTimeHoldCold();

            //values.Rows[3]["Hoechstgeschwindigkeit"] = Gueltigkeit.GetTimeFasterHundred();

            ////grafikToolStripMenuItem.Enabled = true;
            ////txtMeasurement.Text = "Berechnung durchgeführt!";
            ////MessageBox.Show("Berechnung durchgeführt!");
            //pnlContent.Controls.Clear();
            //FormGeneral = new General(this);
            ////myForm.TopLevel = false;
            //FormGeneral.AutoScroll = true;
            //pnlContent.Controls.Add(FormGeneral);
            ////myForm.FormBorderStyle = FormBorderStyle.None;
            //FormGeneral.Show();
            //FormGeneral.Dock = DockStyle.Fill;
            //btnGraphic.Enabled = true;
            //btnGPS.Enabled = true;
            //btnOverview.Enabled = true;
            //btnShowDynamic.Enabled = true;
            //lblHide.BackColor = Color.White;
            //lblShow.BackColor = Color.White;
            //calcDone = true;

            btnGPS.Enabled = true;
            btnOverview.Enabled = true;
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

                DoCalculations(true);
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

        private void LiveMode_Resize(object sender, EventArgs e)
        {
            pnlTopContent.Height = ClientSize.Height / 2;
            pnlBottomContent.Top = ClientSize.Height / 2;
            pnlBottomContent.Height = ClientSize.Height / 2;

            CenterButtons();
        }

        private void CenterButtons()
        {
            int half = (ClientSize.Height - pnlLogo.Height) / 2 + pnlLogo.Height;

            btn_Fileauswahl.Top = half - 170;
            btnOverview.Top = half - 50;
            btnGPS.Top = half + 70;
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            if (topBottom)
            {
                pnlTopContent.Controls.Clear();
                FormLiveOverview = new OverviewLive(this);
                //myForm.TopLevel = false;
                FormLiveOverview.AutoScroll = true;
                pnlTopContent.Controls.Add(FormLiveOverview);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormLiveOverview.Show();
                FormLiveOverview.Dock = DockStyle.Fill;
                lblHide.BackColor = FormLiveOverview.BackColor;
            }
            else
            {
                pnlBottomContent.Controls.Clear();
                FormLiveOverview = new OverviewLive(this);
                //myForm.TopLevel = false;
                FormLiveOverview.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormLiveOverview);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormLiveOverview.Show();
                FormLiveOverview.Dock = DockStyle.Fill;
                lblHide.BackColor = FormLiveOverview.BackColor;
            }
        }
    }
}