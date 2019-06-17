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
using System.Diagnostics;

namespace DriversGuide
{
    public partial class LiveMode : Form
    {
        StartScreen FormStart;
        GPS FormGPS;
        Dynamic FormDynamic;
        OverviewLive FormLiveOverview;             
        Bitmap bmp;
        Graphics z;
        public bool topBottom = true;
        MeasurementFile LiveDatei;
        private DataTable LiveDataset = new DataTable();       
        private DataTable urban = new DataTable();
        private DataTable rural = new DataTable();
        private DataTable motorway = new DataTable();

        private double perUrban;
        private double perRural;
        private double perMotorway;

        bool inout = false;
        bool gpsActive = false;
        private DataTable values = new DataTable();
        bool calc = false;
        bool valid = false;
        bool calcDone = false;
        bool live = false;
        int countData = 5;

        Calculations Berechnung;
        Validation Gueltigkeit;

        Color enabled = Color.Teal;
        Color disabled = Color.Gray;


        Stopwatch stopwatch = new Stopwatch();

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
            InitValueData();
        }

        private void InitValueData()
        {
            values.Columns.Clear();
            values.Rows.Clear();
            values.Columns.Add("Klasse", typeof(String));
            values.Columns.Add("Verteilung");
            values.Columns.Add("Geschwindigkeit");
            values.Columns.Add("Strecke");
            values.Columns.Add("Dauer");
            values.Columns.Add("Haltezeit");
            values.Columns.Add("Hoechstgeschwindigkeit");
            values.Columns.Add("Kaltstart Hoechstgeschwindigkeit");
            values.Columns.Add("Kaltstart Durchschnittsgeschwindigkeit");
            values.Columns.Add("Kaltstart Haltezeit");

            values.Rows.Add();
            values.Rows.Add();
            values.Rows.Add();
            values.Rows.Add();
            values.Rows[0]["Klasse"] = "Gesamt";
            values.Rows[1]["Klasse"] = "Stadt";
            values.Rows[2]["Klasse"] = "Land";
            values.Rows[3]["Klasse"] = "Autobahn";
        }

        public DataTable GetCompleteDataTable()
        {
            return LiveDataset.Copy();
        }

        public DataTable GetValuesDataTable()
        {
            return values.Copy();
        }

        public DataTable GetUrbanDataTable()
        {
            return urban.Copy();
        }

        public DataTable GetRuralDataTable()
        {
            return rural.Copy();
        }

        public DataTable GetMotorwayDataTable()
        {
            return motorway.Copy();
        }

        public void GetPercentiles(ref double percentileUrban, ref double percentileRural, ref double percentileMotorway)
        {
            if (calcDone)
                Berechnung.GetPercentiles(ref percentileUrban, ref percentileRural, ref percentileMotorway);
        }

        public void GetPercentileBorders(ref double borderUrban, ref double borderRural, ref double borderMotorway)
        {
            if (calcDone)
                Berechnung.GetRPA(ref borderUrban, ref borderRural, ref borderMotorway);
        }

        private void LiveMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.Show();
            timer1.Stop();
            timerSimulation.Stop();
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            if (topBottom)
            {
                pnlTopContent.Controls.Clear();
                FormGPS = new GPS(this, live);
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
                FormGPS = new GPS(this, live);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            LiveDataset.Clear();
            LiveDataset = LiveDatei.ConvertLiveCSVtoDataTable();
            DoCalculations(true);
        }        

        private void DoCalculations(bool first)
        {
            if (first)
                Berechnung = new Calculations();
           
            string column_speed = "OBD_Vehicle_Speed_(PID_13)";            
            string column_time = "Time";
            double distrUrban = 0, distrRural = 0, distrMotorway = 0;
            double tripUrban = 0, tripRural = 0, tripMotorway = 0, tripComplete = 0;
            DataTable urban_temp = new DataTable();
            DataTable rural_temp = new DataTable();
            DataTable motorway_temp = new DataTable();

            Berechnung.CalcReqLive(ref LiveDataset, column_speed, first);
            Berechnung.GetTripInt(ref tripUrban, ref tripRural, ref tripMotorway);
            tripComplete = tripUrban + tripRural + tripMotorway;
            Berechnung.CalcDistributionLive(tripUrban, tripRural, tripMotorway, tripComplete);
            Berechnung.GetDistribution(ref distrUrban, ref distrRural, ref distrMotorway);
            Berechnung.GetPercentiles(ref perUrban, ref perRural, ref perMotorway);

            if (countData == 20)
            {
                countData = 0;
                Berechnung.GetIntervals(ref urban, ref rural, ref motorway);
            }
            else
                countData++;
            
            values.Rows[1]["Verteilung"] = distrUrban;
            values.Rows[2]["Verteilung"] = distrRural;
            values.Rows[3]["Verteilung"] = distrMotorway;

            if (LiveDataset.Rows.Count != 0)
            {
                values.Rows[0]["Strecke"] = tripComplete;
                values.Rows[0]["Dauer"] = Convert.ToDouble(LiveDataset.Rows[LiveDataset.Rows.Count - 1][column_time]) / 60000d;
                values.Rows[0]["Geschwindigkeit"] = Convert.ToDouble(LiveDataset.Rows[LiveDataset.Rows.Count - 1][column_speed]);
            }
            else
            {
                values.Rows[0]["Strecke"] = 0;
                values.Rows[0]["Dauer"] = 0;
                values.Rows[0]["Geschwindigkeit"] = 0;
            }

            values.Rows[1]["Strecke"] = tripUrban;
            values.Rows[2]["Strecke"] = tripRural;
            values.Rows[3]["Strecke"] = tripMotorway;           


            if (first)
            {
                live = true;
                pnlTopContent.Controls.Clear();
                FormLiveOverview = new OverviewLive(this, true);                
                FormLiveOverview.AutoScroll = true;
                pnlTopContent.Controls.Add(FormLiveOverview);
                FormLiveOverview.Show();
                FormLiveOverview.Dock = DockStyle.Fill;
                lblHide.BackColor = FormLiveOverview.BackColor;
                topBottom = false;
                pnlBottomContent.Controls.Clear();

                FormGPS = new GPS(this, live);
                FormGPS.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormGPS);
                FormGPS.Show();
                FormGPS.Dock = DockStyle.Fill;
                lblHide.BackColor = FormGPS.BackColor;
                gpsActive = true;
                
                btnGPS.Enabled = true;
                btnOverview.Enabled = true;
                calcDone = true;
            }            
        }

        private void DoCalculationsStatic(bool first)
        {
            Berechnung = new Calculations();
            Gueltigkeit = new Validation();
            string column_speed = "OBD_Vehicle_Speed_(PID_13)";            
            string column_distance = "di";
            string column_time = "Time";
            double distrUrban = 0, distrRural = 0, distrMotorway = 0;
            double tripUrban = 0, tripRural = 0, tripMotorway = 0;
            DataTable urban_temp = new DataTable();
            DataTable rural_temp = new DataTable();
            DataTable motorway_temp = new DataTable();

            Berechnung.CalcReq(ref LiveDataset, column_speed, first);
            Berechnung.SepIntervals(LiveDataset, column_speed);
            Berechnung.CalcDistancesInterval(column_distance);
            Berechnung.GetTripInt(ref tripUrban, ref tripRural, ref tripMotorway);     
            Gueltigkeit.InitErrorsDt();            
            Gueltigkeit.CheckDistributionComplete(LiveDataset, column_speed, column_distance);
            Gueltigkeit.GetDistribution(ref distrUrban, ref distrRural, ref distrMotorway);

            values.Rows[1]["Verteilung"] = distrUrban;
            values.Rows[2]["Verteilung"] = distrRural;
            values.Rows[3]["Verteilung"] = distrMotorway;

            if (LiveDataset.Rows.Count != 0)
            {
                values.Rows[0]["Strecke"] = tripUrban + tripRural + tripMotorway;
                values.Rows[0]["Dauer"] = Convert.ToDouble(LiveDataset.Rows[LiveDataset.Rows.Count - 1][column_time]) / 60000d;
            }
            else
            {
                values.Rows[0]["Strecke"] = 0;
                values.Rows[0]["Dauer"] = 0;
            }

            values.Rows[1]["Strecke"] = tripUrban;
            values.Rows[2]["Strecke"] = tripRural;
            values.Rows[3]["Strecke"] = tripMotorway;

            if (first)
            {
                live = false;
                pnlTopContent.Controls.Clear();
                FormLiveOverview = new OverviewLive(this, false);                
                FormLiveOverview.AutoScroll = true;
                pnlTopContent.Controls.Add(FormLiveOverview);
                FormLiveOverview.Show();
                FormLiveOverview.Dock = DockStyle.Fill;
                lblHide.BackColor = FormLiveOverview.BackColor;
                
                topBottom = false;
                pnlBottomContent.Controls.Clear();
                FormGPS = new GPS(this, live);
                FormGPS.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormGPS);
                FormGPS.Show();
                FormGPS.Dock = DockStyle.Fill;
                lblHide.BackColor = FormGPS.BackColor;
                gpsActive = true;
                
                btnGPS.Enabled = true;
                btnOverview.Enabled = true;
                calcDone = true;
            }
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
                LiveDataset = LiveDatei.ConvertCSVtoDataTable();

                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();
                DoCalculationsStatic(true);
                //stopwatch.Stop();
                //MessageBox.Show("Time elapsed: " + stopwatch.Elapsed.ToString());
            }
            //timer1.Start();
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
            float hoehe = 75;

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

        private void btnSimulation_MouseLeave(object sender, EventArgs e)
        {
            btnSimulation.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        private void btnSimulation_MouseEnter(object sender, EventArgs e)
        {
            btnSimulation.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        private void btnSimulation_Paint(object sender, PaintEventArgs e)
        {
            if (btnSimulation.Enabled)
            {
                DrawInBitmap(btnSimulation, "Simulation", enabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
            else
            {
                DrawInBitmap(btnSimulation, "Simulation", disabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
        }

        private void btnSimulation_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnSimulation.ClientSize.Width, btnSimulation.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnSimulation.Invalidate();
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

            //btn_Fileauswahl.Top = half - 170;
            //btnOverview.Top = half - 50;
            //btnGPS.Top = half + 70;
            btn_Fileauswahl.Top = half - 175;
            btnSimulation.Top = half - 83;
            btnOverview.Top = half + 8;
            btnGPS.Top = half + 100;            
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            if (topBottom)
            {
                pnlTopContent.Controls.Clear();
                FormLiveOverview = new OverviewLive(this, live);
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
                FormLiveOverview = new OverviewLive(this, live);
                //myForm.TopLevel = false;
                FormLiveOverview.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormLiveOverview);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormLiveOverview.Show();
                FormLiveOverview.Dock = DockStyle.Fill;
                lblHide.BackColor = FormLiveOverview.BackColor;
            }
        }

        private void btnSimulation_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Textdateien |*.txt| Alle Dateien|*.*";
            ofd.InitialDirectory = "C:\\Repositories\01_Doku\\Vorgabe\\";
            //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            ofd.Title = "Textdatei öffnen";
            ofd.FileName = "";

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                LiveDatei = new MeasurementFile(ofd.FileName); // Datatable anlegen
                LiveDataset = LiveDatei.ConvertCSVtoDataTable(); // Datatable befüllen
                LiveDataset.Clear();        // Daten aus Table löschen
                                                                          
                stopwatch.Start();
                
                DoCalculations(true);
            }
            timerSimulation.Start(); // Simulation starten
           
           
          //BTN_Simulation Click und timerSimulation Tick sind das Problem  


        }

        private void timerSimulation_Tick(object sender, EventArgs e)
        {
            DataRow dr = LiveDataset.NewRow(); // Reihe dr Typedef
            dr = LiveDatei.AddSimulationRows(); // Reihe aus Live Datei auslesen
            if (!dr.IsNull(0))
            {
                LiveDataset.ImportRow(dr);   // Reihe zu LiveDataset hinzufügen
                DoCalculations(false);
            }
            else
            {
                timerSimulation.Stop();
                Berechnung.SepIntervals(LiveDataset, "OBD_Vehicle_Speed_(PID_13)");
                Berechnung.PosCheck("ai");
                Berechnung.GetIntervals(ref urban, ref rural, ref motorway);
                perUrban = Berechnung.CalcPercentile_Interval(ref urban, "a*v");
                perRural = Berechnung.CalcPercentile_Interval(ref rural, "a*v");
                perMotorway = Berechnung.CalcPercentile_Interval(ref motorway, "a*v");                
                stopwatch.Stop();
                MessageBox.Show("Time elapsed: " + stopwatch.Elapsed.ToString());
            }

            FormLiveOverview.RefreshData();
            FormGPS.RefreshMap();
        }

        private void btnDyn_Click(object sender, EventArgs e)
        {
            if (topBottom)
            {
                pnlTopContent.Controls.Clear();
                FormDynamic = new Dynamic(this, live);
                FormDynamic.AutoScroll = true;
                pnlTopContent.Controls.Add(FormDynamic);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormDynamic.Show();
                FormDynamic.Dock = DockStyle.Fill;
                lblHide.BackColor = FormDynamic.BackColor;
            }
            else
            {
                pnlBottomContent.Controls.Clear();
                FormDynamic = new Dynamic(this, live);
                //myForm.TopLevel = false;
                FormDynamic.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormDynamic);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormDynamic.Show();
                FormDynamic.Dock = DockStyle.Fill;
                lblHide.BackColor = FormDynamic.BackColor;
            }
        }
    }
}
