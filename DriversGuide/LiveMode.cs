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
        int ElapsedTicks = 0;

        Calculations Berechnung;
        Validation Gueltigkeit;

        Color enabled = Color.Teal;
        Color disabled = Color.Gray;

        public LiveMode(StartScreen caller)
        {
            FormStart = caller;
            FormStart.Hide();

            InitializeComponent();
            bmp = new Bitmap(btnDynamic.ClientSize.Width, btnDynamic.ClientSize.Height);
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
                Berechnung.GetBordersPercentile(ref borderUrban, ref borderRural, ref borderMotorway);
        }

        private void LiveMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.Show();
            timer1.Stop();
            timerSimulation.Stop();
        }

        private void SetCorrPos()
        {
            if (pnlTopContent.Controls[0] == FormLiveOverview)
                FormLiveOverview.SetTopBottom(true);
            else if (pnlTopContent.Controls[0] == FormDynamic)
                FormDynamic.SetTopBottom(true);
            else if (pnlTopContent.Controls[0] == FormGPS)
                FormGPS.SetTopBottom(true);

            if (pnlBottomContent.Controls[0] == FormLiveOverview)
                FormLiveOverview.SetTopBottom(false);
            else if (pnlBottomContent.Controls[0] == FormDynamic)
                FormDynamic.SetTopBottom(false);
            else if (pnlBottomContent.Controls[0] == FormGPS)
                FormGPS.SetTopBottom(false);
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            if (pnlTopContent.Controls[0] != FormGPS && pnlBottomContent.Controls[0] != FormGPS)
            {
                if (topBottom)
                {
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(FormGPS);

                    SetCorrPos();

                    lblHide.BackColor = FormGPS.BackColor;
                    gpsActive = true;
                }
                else
                {
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(FormGPS);

                    SetCorrPos();

                    lblHide.BackColor = FormGPS.BackColor;
                    gpsActive = true;
                }
            }
            else
            {
                if (topBottom && pnlTopContent.Controls[0] != FormGPS)
                {
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(pnlTopContent.Controls[0]);
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(FormGPS);

                    SetCorrPos();
                    gpsActive = true;
                }
                else if (!topBottom && pnlBottomContent.Controls[0] != FormGPS)
                {
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(pnlBottomContent.Controls[0]);
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(FormGPS);

                    SetCorrPos();
                    gpsActive = true;
                }
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
                gpsActive = true;

                FormDynamic = new Dynamic(this);
                FormDynamic.AutoScroll = true;
                FormDynamic.Dock = DockStyle.Fill;

                btnGPS.Enabled = true;
                btnOverview.Enabled = true;
                btnDynamic.Enabled = true;
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
                gpsActive = true;

                FormDynamic = new Dynamic(this);
                FormDynamic.AutoScroll = true;
                FormDynamic.Dock = DockStyle.Fill;

                btnGPS.Enabled = true;
                btnOverview.Enabled = true;
                btnDynamic.Enabled = true;
                calcDone = true;
            }
        }

        //private void btn_Fileauswahl_Click(object sender, EventArgs e)
        //{
        //    ofd.Filter = "Textdateien |*.txt| Alle Dateien|*.*";
        //    ofd.InitialDirectory = "C:\\Repositories\01_Doku\\Vorgabe\\";
        //    //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        //    ofd.Title = "Textdatei öffnen";
        //    ofd.FileName = "";

        //    DialogResult dr = ofd.ShowDialog();
        //    if (dr == DialogResult.OK)
        //    {
        //        LiveDatei = new MeasurementFile(ofd.FileName);
        //        LiveDataset = LiveDatei.ConvertCSVtoDataTable();

        //        //Stopwatch stopwatch = new Stopwatch();
        //        //stopwatch.Start();
        //        DoCalculationsStatic(true);
        //        //stopwatch.Stop();
        //        //MessageBox.Show("Time elapsed: " + stopwatch.Elapsed.ToString());
        //    }
        //    //timer1.Start();
        //}

        //private void btn_Fileauswahl_MouseLeave(object sender, EventArgs e)
        //{
        //    btn_Fileauswahl.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        //}

        //private void btn_Fileauswahl_MouseEnter(object sender, EventArgs e)
        //{
        //    btn_Fileauswahl.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        //}

        //private void btn_Fileauswahl_Paint(object sender, PaintEventArgs e)
        //{
        //    if (btn_Fileauswahl.Enabled)
        //    {
        //        DrawInBitmap(btn_Fileauswahl, "File-Auswahl", enabled);
        //        Graphics g = e.Graphics;                
        //        g.DrawImage(bmp, 0, 0);                
        //    }
        //    else
        //    {
        //        DrawInBitmap(btn_Fileauswahl, "File-Auswahl", disabled);
        //        Graphics g = e.Graphics;                
        //        g.DrawImage(bmp, 0, 0);
        //    }
        //}

        //private void btn_Fileauswahl_Resize(object sender, EventArgs e)
        //{
        //    bmp = new Bitmap(btn_Fileauswahl.ClientSize.Width, btnFileauswahl.ClientSize.Height);
        //    z = Graphics.FromImage(bmp);

        //    btnFileauswahl.Invalidate();
        //}

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
            tmrFade.Enabled = true;
            lblHide.Hide();
            lblShow.Left = 3;
            tmrFade.Enabled = true;
        }

        private void lblShow_Click(object sender, EventArgs e)
        {
            tmrFade.Enabled = true;
            lblShow.Hide();
            lblHide.Show();
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
                }
                else
                {
                    tmrFade.Enabled = false;
                    inout = false;
                }
            }
            else
            {
                if (pnlSideBar.Location.X > -pnlSideBar.Width)
                {
                    pnlSideBar.Left -= 10;
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
            
            btnSimulation.Top = half - 175;
            btnOverview.Top = half - 83;
            btnDynamic.Top = half + 8;
            btnGPS.Top = half + 100;            
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            if (pnlTopContent.Controls[0] != FormLiveOverview && pnlBottomContent.Controls[0] != FormLiveOverview)
            {
                if (topBottom)
                {
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(FormLiveOverview);

                    SetCorrPos();

                    lblHide.BackColor = FormLiveOverview.BackColor;
                }
                else
                {
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(FormLiveOverview);

                    SetCorrPos();
                }
            }
            else
            {
                if (topBottom && pnlTopContent.Controls[0] != FormLiveOverview)
                {
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(pnlTopContent.Controls[0]);
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(FormLiveOverview);

                    SetCorrPos();

                    lblHide.BackColor = FormLiveOverview.BackColor;
                }
                else if (!topBottom && pnlBottomContent.Controls[0] != FormLiveOverview)
                {
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(pnlBottomContent.Controls[0]);
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(FormLiveOverview);

                    SetCorrPos();
                }

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
                FormGPS.SetRunningState(false);
                FormGPS.RefreshMap();
                RedrawDynamics();
            }

            FormLiveOverview.RefreshData();
            FormGPS.RefreshMap();

            ElapsedTicks += 1;
            int test = 5 * (int)Math.Round((ElapsedTicks + 2) / 5.0);
            if (ElapsedTicks == test)
            {
                RedrawDynamics();
            }
        }

        private void RedrawDynamics()
        {
            double urbmax = 0;      //Maximalwert a*v Stadt
            double rurmax = 0;      //Maximalwert a*v Freiland
            double mwmax = 0;       //Maximalwert a*v Autobahn
            double ymax = 0;        //Maximalwert a*v generell
            double purb = 0;        //Perzentilwert Stadt
            double prur = 0;        //Perzentilwert Freiland
            double pmw = 0;         //Perzentilwert Autobahn
            double borderUrb = 0;   //Perzentilgrenzwert Stadt
            double borderRur = 0;   //Perzentilgrenzwert Freiland
            double borderMw = 0;    //Perzentilgrenzwert Autobahn

            DataTable dturb = GetUrbanDataTable();
            DataTable dtrur = GetRuralDataTable();
            DataTable dtmw = GetMotorwayDataTable();

            GetPercentiles(ref purb, ref prur, ref pmw);                        //Zuweisung Perzentilwerte
            GetPercentileBorders(ref borderUrb, ref borderRur, ref borderMw);   //Zuweisung Perzentilgrenzwerte

            if (dturb.Rows.Count != 0)
                urbmax = Convert.ToDouble(dturb.Rows[dturb.Rows.Count - 1]["a*v"]);   //Max-Wert a*v Stadt
            if (dtrur.Rows.Count != 0)
                rurmax = Convert.ToDouble(dtrur.Rows[dtrur.Rows.Count - 1]["a*v"]);   //Max-Wert a*v Land
            if (dtmw.Rows.Count != 0)
                mwmax = Convert.ToDouble(dtmw.Rows[dtmw.Rows.Count - 1]["a*v"]);      //Max-Wert a*v Autobahn

            ymax = 5 * (int)Math.Round((Math.Max(urbmax, Math.Max(rurmax, mwmax) + 3) / 5.0));  //Max-Wert a*v generell, auf nächste 5 aufgerundet
            if (FormDynamic != null)
            {
                FormDynamic.ClearAndSetChartsLive(ymax);
                FormDynamic.FillCharts(dturb, dtrur, dtmw, ymax,
                                       purb, prur, pmw,
                                       borderUrb, borderRur, borderMw);
            }
        }

        private void btnDyn_Click(object sender, EventArgs e)
        {
            if (pnlTopContent.Controls[0] != FormDynamic && pnlBottomContent.Controls[0] != FormDynamic)
            {
                if (topBottom)
                {
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(FormDynamic);

                    SetCorrPos();

                    lblHide.BackColor = FormDynamic.BackColor;
                }
                else
                {
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(FormDynamic);

                    SetCorrPos();
                }
            }
            else
            {
                if (topBottom && pnlTopContent.Controls[0] != FormDynamic)
                {
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(pnlTopContent.Controls[0]);
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(FormDynamic);

                    SetCorrPos();

                    lblHide.BackColor = FormDynamic.BackColor;
                }
                else if (!topBottom && pnlBottomContent.Controls[0] != FormDynamic)
                {
                    pnlTopContent.Controls.Clear();
                    pnlTopContent.Controls.Add(pnlBottomContent.Controls[0]);
                    pnlBottomContent.Controls.Clear();
                    pnlBottomContent.Controls.Add(FormDynamic);

                    SetCorrPos();
                }
            }
        }

        private void btnDynamic_MouseLeave(object sender, EventArgs e)
        {
            btnDynamic.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        private void btnDynamic_MouseEnter(object sender, EventArgs e)
        {
            btnDynamic.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        private void btnDynamic_Paint(object sender, PaintEventArgs e)
        {
            if (btnDynamic.Enabled)
            {
                DrawInBitmap(btnDynamic, "Dynamik", enabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
            else
            {
                DrawInBitmap(btnDynamic, "Dynamik", disabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
        }

        private void btnDynamic_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnDynamic.ClientSize.Width, btnDynamic.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnDynamic.Invalidate();
        }

        private void pnlBottomContent_Click(object sender, EventArgs e)
        {
            topBottom = false;
        }

        private void pnlTopContent_Click(object sender, EventArgs e)
        {
            topBottom = true;
        }
    }
}
