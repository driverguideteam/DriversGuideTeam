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
        //create the needed forms and classes
        StartScreen FormStart;
        GPS FormGPS;
        Dynamic FormDynamic;
        OverviewLive FormLiveOverview;
        MeasurementFile LiveDatei;
        Calculations Berechnung;
        Validation Gueltigkeit;

        //create dataTables for the individual datasets
        private DataTable LiveDataset = new DataTable();
        private DataTable urban = new DataTable();
        private DataTable rural = new DataTable();
        private DataTable motorway = new DataTable();
        private DataTable values = new DataTable();

        //create a bitmap used for drawing the buttons
        Bitmap bmp;
        Graphics z;

        //define colors for the differnt states of the buttons
        Color enabled = Color.Teal;
        Color disabled = Color.Gray;

        //create needed variables
        public bool topBottom = true;   
        private double perUrban;
        private double perRural;
        private double perMotorway;
        bool inout = false;
        bool gpsActive = false;        
        bool calc = false;
        bool valid = false;
        bool calcDone = false;
        bool live = false;
        int countData = 5;
        int ElapsedTicks = 0;        

        //when form gets initialized
        public LiveMode(StartScreen caller)
        {
            //get reference to calling form and hide it
            FormStart = caller;
            FormStart.Hide();

            InitializeComponent();

            //initialize bitmap
            bmp = new Bitmap(btnDynamic.ClientSize.Width, btnDynamic.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            //init labels and center buttons
            lblHide.Parent = this;
            lblShow.Parent = this;
            lblHide.BringToFront();
            lblShow.BringToFront();
            CenterButtons();

            //init the value dataTable
            InitValueData();
        }

        //define correct form of values dataTable
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

        //get a copy of the complete dataset
        public DataTable GetCompleteDataTable()
        {
            return LiveDataset.Copy();
        }

        //get a copy of the values dataset
        public DataTable GetValuesDataTable()
        {
            return values.Copy();
        }

        //get a copy of the urban dataset
        public DataTable GetUrbanDataTable()
        {
            return urban.Copy();
        }

        //get a copy of the rural dataset
        public DataTable GetRuralDataTable()
        {
            return rural.Copy();
        }

        //get a copy of the motorway dataset
        public DataTable GetMotorwayDataTable()
        {
            return motorway.Copy();
        }

        //get the percentile values for each interval
        public void GetPercentiles(ref double percentileUrban, ref double percentileRural, ref double percentileMotorway)
        {
            //.. by calling the getPercentiles methode of the calculations class after the calculation is done
            if (calcDone)
                Berechnung.GetPercentiles(ref percentileUrban, ref percentileRural, ref percentileMotorway);
        }

        //get the percentile borders for each interval
        public void GetPercentileBorders(ref double borderUrban, ref double borderRural, ref double borderMotorway)
        {
            //.. by calling the getBorderPercentile methode of the calculations class after the calculation is done
            if (calcDone)
                Berechnung.GetBordersPercentile(ref borderUrban, ref borderRural, ref borderMotorway);
        }

        //when this form is closed, show calling form again and stop timers
        private void LiveMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.Show();
            timer1.Stop();
            timerSimulation.Stop();
        }

        //used to set the correct position for each user control by storing the position 
        //to a variable in each user control
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

        //Calculates all needed data in running mode
        private void DoCalculations(bool first)
        {
            //if it is called for the first time, init a new calculations class
            if (first)
                Berechnung = new Calculations();

            //create needed variables
            string column_speed = "OBD_Vehicle_Speed_(PID_13)";
            string column_time = "Time";
            double distrUrban = 0, distrRural = 0, distrMotorway = 0;
            double tripUrban = 0, tripRural = 0, tripMotorway = 0, tripComplete = 0;

            //calculate requirements, get trip length per interval, calculate length of whole trip
            //calculate the distributions and get them by using the calculations class
            Berechnung.CalcReqLive(ref LiveDataset, column_speed, first);
            Berechnung.GetTripInt(ref tripUrban, ref tripRural, ref tripMotorway);
            tripComplete = tripUrban + tripRural + tripMotorway;
            Berechnung.CalcDistributionLive(tripUrban, tripRural, tripMotorway, tripComplete);
            Berechnung.GetDistribution(ref distrUrban, ref distrRural, ref distrMotorway);

            //every 20 counts the stored interval dataTables are retrieved from the calculations class
            if (countData == 20)
            {
                countData = 0;
                Berechnung.GetIntervals(ref urban, ref rural, ref motorway);
            }
            else
                countData++;

            //fill values dataTable for later use of data
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

            //if called the first time
            if (first)
            {
                //the Overview user control is added to the top panel
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

                //the GPS user control is added to the bottom panel
                FormGPS = new GPS(this, live);
                FormGPS.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormGPS);
                FormGPS.Show();
                FormGPS.Dock = DockStyle.Fill;
                gpsActive = true;

                //the Dynamic user control is initialized
                FormDynamic = new Dynamic(this);
                FormDynamic.AutoScroll = true;
                FormDynamic.Dock = DockStyle.Fill;

                //the Buttuons are actived
                btnGPS.Enabled = true;
                btnOverview.Enabled = true;
                btnDynamic.Enabled = true;
                calcDone = true;

                //set the correct positions the user controls are located
                SetCorrPos();
            }
        }

        //calculates all neede data in static mode
        private void DoCalculationsStatic(bool first)
        {
            //if it is called for the first time, init new calculations and validation classes
            if (first)
            {
                Berechnung = new Calculations();
                Gueltigkeit = new Validation();
            }

            //create needed variables
            string column_speed = "OBD_Vehicle_Speed_(PID_13)";
            string column_distance = "di";
            string column_acc = "ai";
            string column_dynamic = "a*v";
            string column_time = "Time";
            double distrUrban = 0, distrRural = 0, distrMotorway = 0;
            double tripUrban = 0, tripRural = 0, tripMotorway = 0;

            //calculate requirements, seperate dataset into intervals, calculate the trips per interval
            //get the trip values, delete all data with acceleration < 0.1 m/s^2, get the interval
            //and calculate the percentiles per interval by using the calculations class
            Berechnung.CalcReq(ref LiveDataset, column_speed, first);
            Berechnung.SepIntervals(LiveDataset, column_speed);
            Berechnung.CalcDistancesInterval(column_distance);
            Berechnung.GetTripInt(ref tripUrban, ref tripRural, ref tripMotorway);
            Berechnung.PosCheck(column_acc);
            Berechnung.GetIntervals(ref urban, ref rural, ref motorway);
            Berechnung.CalcPercentile_Complete(ref urban, ref rural, ref motorway, column_dynamic);

            //initialize error dataTable, check and get distribution values by using the 
            //validation class
            Gueltigkeit.InitErrorsDt();
            Gueltigkeit.CheckDistributionComplete(LiveDataset, column_speed, column_distance);
            Gueltigkeit.GetDistribution(ref distrUrban, ref distrRural, ref distrMotorway);

            //fill values dataTable for later use of data
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

            //if called the first time
            if (first)
            {
                //the Overview user control is added to the top panel
                live = false;
                pnlTopContent.Controls.Clear();
                FormLiveOverview = new OverviewLive(this, false);
                FormLiveOverview.AutoScroll = true;
                pnlTopContent.Controls.Add(FormLiveOverview);
                FormLiveOverview.Show();
                FormLiveOverview.Dock = DockStyle.Fill;
                lblHide.BackColor = FormLiveOverview.BackColor;

                //the GPS user control is added to the bottom panel
                topBottom = false;
                pnlBottomContent.Controls.Clear();
                FormGPS = new GPS(this, live);
                FormGPS.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormGPS);
                FormGPS.Show();
                FormGPS.Dock = DockStyle.Fill;
                gpsActive = true;

                //the Dynamic user control is initialized
                FormDynamic = new Dynamic(this);
                FormDynamic.AutoScroll = true;
                FormDynamic.Dock = DockStyle.Fill;

                //the Buttuons are actived
                btnGPS.Enabled = true;
                btnOverview.Enabled = true;
                btnDynamic.Enabled = true;
                calcDone = true;
            }
        }

        //when button is clicked, determine which panel the user has selected and draw the
        //GPS user control to this panel
        private void btnGPS_Click(object sender, EventArgs e)
        {
            //if GPS is already displayed swap panels if the user wants to
            //if not hide the user control that is selected and draw the GPS instead
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

        //get new data each tick of the timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            LiveDataset.Clear();
            LiveDataset = LiveDatei.ConvertLiveCSVtoDataTable();
            DoCalculations(true);
        }      

        //open a select file dialog to get the file with the needed data
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
                //if data is retrieved correctly
                //data is written to dataTable
                LiveDatei = new MeasurementFile(ofd.FileName);
                LiveDataset = LiveDatei.ConvertCSVtoDataTable();
                
                //and calculations are performed
                //as well as dynamic graphics are drawn
                DoCalculationsStatic(true);
                RedrawDynamics();
            }
            //timer1.Start();
        }

        //set background matching color of the button when the mouse leaves it
        private void btn_Fileauswahl_MouseLeave(object sender, EventArgs e)
        {
            btn_Fileauswahl.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        //set slightly different color of the button when the mouse enters it
        //to determine the size of the button
        private void btn_Fileauswahl_MouseEnter(object sender, EventArgs e)
        {
            btn_Fileauswahl.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        //paint the formated text from the bitmap into the button
        private void btn_Fileauswahl_Paint(object sender, PaintEventArgs e)
        {
            //choose text color according to state
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

        //redraw content if button is resized
        private void btn_Fileauswahl_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btn_Fileauswahl.ClientSize.Width, btn_Fileauswahl.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btn_Fileauswahl.Invalidate();
        }

        //draws the wished title for the button to a bitmap
        private void DrawInBitmap(Panel caller, string text, Color color)
        {
            //clear graphics and enable smoother lines
            z.Clear(caller.BackColor);
            z.SmoothingMode = SmoothingMode.AntiAlias;

            //set width and height
            float breite = 109;
            float hoehe = 60;

            //transform matrix and set new base point
            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmp.Width / breite, bmp.Height / hoehe);

            myMatrix.Translate(breite / 2, hoehe / 2 + 1, MatrixOrder.Prepend);

            z.Transform = myMatrix;

            //set type and style of the fond
            Font type = new Font("Century Gothic", 12f, FontStyle.Bold);
            Brush style = new SolidBrush(color);

            //set the format for the text
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            //draw the text to graphics
            z.DrawString(text, type, style, new Point(0, 0), sf);
        }

        //set background matching color of the button when the mouse leaves it
        private void btnOverview_MouseLeave(object sender, EventArgs e)
        {
            btnOverview.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        //set slightly different color of the button when the mouse enters it
        //to determine the size of the button
        private void btnOverview_MouseEnter(object sender, EventArgs e)
        {
            btnOverview.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        //paint the formated text from the bitmap into the button
        private void btnOverview_Paint(object sender, PaintEventArgs e)
        {
            //choose text color according to state
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

        //redraw content if button is resized
        private void btnOverview_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnOverview.ClientSize.Width, btnOverview.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnOverview.Invalidate();
        }

        //set background matching color of the button when the mouse leaves it
        private void btnGPS_MouseLeave(object sender, EventArgs e)
        {
            btnGPS.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        //set slightly different color of the button when the mouse enters it
        //to determine the size of the button
        private void btnGPS_MouseEnter(object sender, EventArgs e)
        {
            btnGPS.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        //paint the formated text from the bitmap into the button
        private void btnGPS_Paint(object sender, PaintEventArgs e)
        {
            //choose text color according to state
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

        //redraw content if button is resized
        private void btnGPS_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnGPS.ClientSize.Width, btnGPS.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnGPS.Invalidate();
        }

        //set background matching color of the button when the mouse leaves it
        private void btnSimulation_MouseLeave(object sender, EventArgs e)
        {
            btnSimulation.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        //set slightly different color of the button when the mouse enters it
        //to determine the size of the button
        private void btnSimulation_MouseEnter(object sender, EventArgs e)
        {
            btnSimulation.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        //paint the formated text from the bitmap into the button
        private void btnSimulation_Paint(object sender, PaintEventArgs e)
        {
            //choose text color according to state
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

        //redraw content if button is resized
        private void btnSimulation_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnSimulation.ClientSize.Width, btnSimulation.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnSimulation.Invalidate();
        }

        //when the hide label is clicked
        private void lblHide_Click(object sender, EventArgs e)
        {
            //slowely slide sidepanel to the left until it is no longer visible
            tmrFade.Enabled = true;
            lblHide.Hide();
            lblShow.Left = 3;
            tmrFade.Enabled = true;
        }

        //when label show is clicked
        private void lblShow_Click(object sender, EventArgs e)
        {
            //slowly slide the sidepanel back into the form until it reaches its 
            //initial position
            tmrFade.Enabled = true;
            lblShow.Hide();
            lblHide.Show();
            pnlTopContent.Left = pnlSideBar.Width;
            pnlTopContent.Width = ClientSize.Width - pnlSideBar.Width;
            pnlBottomContent.Left = pnlSideBar.Width;
            pnlBottomContent.Width = ClientSize.Width - pnlSideBar.Width;
        }

        //timer used for slowly moving the sidepanel in or out
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

        //if the form is resized
        private void LiveMode_Resize(object sender, EventArgs e)
        {
            //set the panels to the new sizes
            pnlTopContent.Height = ClientSize.Height / 2;
            pnlBottomContent.Top = ClientSize.Height / 2;
            pnlBottomContent.Height = ClientSize.Height / 2;

            //and re-center buttons
            CenterButtons();
        }

        //make sure the buttons are always centered corretly in the middle of the side panel
        private void CenterButtons()
        {
            //calculate the postion of the middle and center buttons according to that
            int half = (ClientSize.Height - pnlLogo.Height) / 2 + pnlLogo.Height;

            btn_Fileauswahl.Top = half - 178;
            btnSimulation.Top = half - 104;
            btnOverview.Top = half - 30;
            btnDynamic.Top = half + 44;
            btnGPS.Top = half + 118;            
        }

        //when button is clicked, determine which panel the user has selected and draw the
        //Overview user control to this panel
        private void btnOverview_Click(object sender, EventArgs e)
        {
            //if Overview is already displayed swap panels if the user wants to
            //if not hide the user control that is selected and draw the Overview instead
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

        //open a select file dialog to get the file with the needed data
        //then start a timer which adds a new datarow to the
        //dataset at each tick -> simulating a real drive
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
                              
                DoCalculations(true);       // Neue Zeile in die Berechnung einfließen lassen
            }

            timerSimulation.Start(); // Simulation starten   
        }

        //add new datarow to dataset each timer-tick
        private void timerSimulation_Tick(object sender, EventArgs e)
        {
            DataRow dr = LiveDataset.NewRow(); // Reihe dr Typedef
            dr = LiveDatei.AddSimulationRows(); // Reihe aus Live Datei auslesen
            if (!dr.IsNull(0))
            {
                LiveDataset.ImportRow(dr);   // Reihe zu LiveDataset hinzufügen
                DoCalculations(false);       // Berechnungen durchführen
            }
            else
            {
                // Wenn der die letzte Zeile eingelesen wurde
                // wird die Berechnung ein letztes Mal durchgeführt
                // und die user controls mit den neuen Daten aktualisiert
                timerSimulation.Stop();
                Berechnung.SepIntervals(LiveDataset, "OBD_Vehicle_Speed_(PID_13)");              
                Berechnung.PosCheck("ai");
                Berechnung.GetIntervals(ref urban, ref rural, ref motorway);
                Berechnung.CalcPercentile_Complete(ref urban, ref rural, ref motorway, "a*v");                
                FormGPS.SetRunningState(false);
                RedrawDynamics();
            }

            // Die user controls mit den neuen Daten aktualisieren
            FormLiveOverview.RefreshData();
            FormGPS.RefreshMap();

            // Zeichne die Dynamik Charts nur nach bestimmter Anzahl an ticks
            // um Ressourcen zu schonen
            ElapsedTicks += 1;
            int test = 5 * (int)Math.Round((ElapsedTicks + 2) / 5.0);
            if (ElapsedTicks == test)
            {
                RedrawDynamics();
            }
        }

        //redraw the dynamic charts
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

        //when button is clicked, determine which panel the user has selected and draw the
        //Dynamic user control to this panel
        private void btnDyn_Click(object sender, EventArgs e)
        {
            //if Dynamic is already displayed swap panels if the user wants to
            //if not hide the user control that is selected and draw the Dynamic instead
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

        //set background matching color of the button when the mouse leaves it
        private void btnDynamic_MouseLeave(object sender, EventArgs e)
        {
            btnDynamic.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        //set slightly different color of the button when the mouse enters it
        //to determine the size of the button
        private void btnDynamic_MouseEnter(object sender, EventArgs e)
        {
            btnDynamic.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        //paint the formated text from the bitmap into the button
        private void btnDynamic_Paint(object sender, PaintEventArgs e)
        {
            //choose text color according to state
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

        //redraw content if button is resized
        private void btnDynamic_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnDynamic.ClientSize.Width, btnDynamic.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnDynamic.Invalidate();
        }

        //if bottom panel is empty and clicked, set selected position to bottom
        private void pnlBottomContent_Click(object sender, EventArgs e)
        {
            topBottom = false;
        }

        //if top panel is empty and clicked, set selected position to top
        private void pnlTopContent_Click(object sender, EventArgs e)
        {
            topBottom = true;
        }        
    }
}
