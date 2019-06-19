using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace DriversGuide
{
    public partial class DriversGuideApp : Form
    {
        //create the needed forms and classes
        StartScreen FormStart;
        GPS FormGPS;
        Dynamic FormDynamic;        
        Overview FormOverview;
        MeasurementFile Datei;
        Calculations Berechnung;
        Validation Gueltigkeit;

        //create dataTables for the individual datasets
        private DataTable test = new DataTable();
        private DataTable units = new DataTable();
        private DataTable urban = new DataTable();
        private DataTable rural = new DataTable();
        private DataTable motorway = new DataTable();
        private DataTable values = new DataTable();
        private DataTable errors = new DataTable();
        private DataTable tips = new DataTable();
        public DataTable ColumnHeaders;   //Datatable für Spaltenüberschriften

        //create a bitmap used for drawing the buttons
        Bitmap bmp;
        Graphics z;

        //define colors for the differnt states of the buttons
        Color enabled = Color.Teal;
        Color disabled = Color.Gray;

        //create needed variables
        bool inout = false;
        bool gpsActive = false;
        bool calc = false;
        bool valid = false;
        bool calcDone = false;
                 
        //when form gets initialized
        public DriversGuideApp(StartScreen caller)
        {
            //get reference to calling form and hide it
            FormStart = caller;
            FormStart.Hide();

            InitializeComponent();

            //initialize bitmap
            bmp = new Bitmap(btnReadFile.ClientSize.Width, btnReadFile.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            //init labels and center buttons
            lblHide.Parent = this;
            lblShow.Parent = this;
            lblHide.BringToFront();
            lblShow.BringToFront();
            CenterButtons();                       
        }

        //get a copy of the complete dataset
        public DataTable GetCompleteDataTable()
        {
            return test.Copy();
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

        //get a copy of the units dataset
        public DataTable GetUnitsDataTable()
        {
            return units.Copy();
        }

        //get a copy of the values dataset
        public DataTable GetValuesDataTable()
        {
            return values.Copy();
        }

        //get a copy of the errors dataTable
        public DataTable GetErrorsDataTable()
        {
            return errors.Copy();
        }

        //get a copy of the tips dataTable
        public DataTable GetTipsDataTable()
        {
            return tips.Copy();
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

        //get state of validation 
        //true = valid
        //false = invalid
        public bool GetValidation()
        {
            if (calc && valid)
                return true;
            else
                return false;
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

        //Calculates all needed data 
        private void DoCalculations()
        {
            //init the values dataTable
            InitValueData();

            //init new calculations and validation classes
            Berechnung = new Calculations();
            Gueltigkeit = new Validation();

            //create needed variables
            string column_speed = "OBD_Vehicle_Speed_(PID_13)";
            string column_acc = "ai";
            string column_dynamic = "a*v";
            string column_distance = "di";
            string column_time = "Time";
            string column_coolant = "OBD_Engine_Coolant_Temperature_(PID_5)";
            double avgUrban = 0, avgRural = 0, avgMotorway = 0;
            double distrUrban = 0, distrRural = 0, distrMotorway = 0;
            double tripUrban = 0, tripRural = 0, tripMotorway = 0;          

            //define error and tips list
            string[] errorMess = new string[3];
            string[] tipsMess = new string[3];

            //do all calculations and get state if all calculations conditions are met
            //get the interval dataTables
            //do all validity checks and get state if all criterias are met
            calc = Berechnung.CalcAll(test, column_speed, column_acc, column_dynamic, column_distance);
            Berechnung.GetIntervals(ref urban, ref rural, ref motorway);
            valid = Gueltigkeit.CheckValidity(test, column_speed, column_time, column_coolant, column_distance);

            //get error and tips dataTable form validation class
            //ans the error and tips list from calculations class
            errors = Gueltigkeit.GetErrors();
            tips = Gueltigkeit.GetTips();
            errorMess = Berechnung.GetErrors();
            tipsMess = Berechnung.GetTips();

            //add error list to error dataTable
            errors.Rows[0]["Other"] = errorMess[0];
            errors.Rows[1]["Other"] = errorMess[1];
            errors.Rows[2]["Other"] = errorMess[2];

            //add tips list to tips dataTable
            tips.Rows[0]["Other"] = tipsMess[0];
            tips.Rows[1]["Other"] = tipsMess[1];
            tips.Rows[2]["Other"] = tipsMess[2];

            //seperate the intervals from the colpete dataset
            //and add the correct units to the units dataTable
            Berechnung.SepIntervals(test, column_speed);        
            Berechnung.AddUnits(units);

            //Get average speeds and distributions for each interval
            Berechnung.GetAvgSpeed(ref avgUrban, ref avgRural, ref avgMotorway);
            Berechnung.GetTripInt(ref tripUrban, ref tripRural, ref tripMotorway);
            Gueltigkeit.GetDistribution(ref distrUrban, ref distrRural, ref distrMotorway);

            //fill values dataTable for later use of data
            values.Rows[1]["Geschwindigkeit"] = avgUrban;
            values.Rows[2]["Geschwindigkeit"] = avgRural;
            values.Rows[3]["Geschwindigkeit"] = avgMotorway;

            values.Rows[1]["Verteilung"] = distrUrban;
            values.Rows[2]["Verteilung"] = distrRural;
            values.Rows[3]["Verteilung"] = distrMotorway;

            values.Rows[0]["Strecke"] = (double)test.Compute("SUM([" + column_distance + "])", "") / 1000;
            values.Rows[1]["Strecke"] = tripUrban;
            values.Rows[2]["Strecke"] = tripRural;
            values.Rows[3]["Strecke"] = tripMotorway;

            values.Rows[0]["Dauer"] = Convert.ToDouble(test.Rows[test.Rows.Count - 1][column_time]) / 60000d;
            values.Rows[0]["Haltezeit"] = Gueltigkeit.GetHoldDurtation();
            values.Rows[0]["Hoechstgeschwindigkeit"] = Gueltigkeit.GetMaxSpeed();
            values.Rows[0]["Kaltstart Hoechstgeschwindigkeit"] = Gueltigkeit.GetMaxSpeedCold();
            values.Rows[0]["Kaltstart Durchschnittsgeschwindigkeit"] = Gueltigkeit.GetAvgSpeedCold();
            values.Rows[0]["Kaltstart Haltezeit"] = Gueltigkeit.GetTimeHoldCold();

            values.Rows[3]["Hoechstgeschwindigkeit"] = Gueltigkeit.GetTimeFasterHundred();
       
            //clear the panel and add the Overview user control to it
            pnlContent.Controls.Clear();
            FormOverview = new Overview(this);            
            FormOverview.AutoScroll = true;
            pnlContent.Controls.Add(FormOverview);            
            FormOverview.Show();
            FormOverview.Dock = DockStyle.Fill;

            //then enable the buttons in the sidepanel
            btnGraphic.Enabled = true;
            btnGPS.Enabled = true;
            btnOverview.Enabled = true;
            btnShowDynamic.Enabled = true;

            //color the show and hide labels
            lblHide.BackColor = Color.White;
            lblShow.BackColor = Color.White;
            calcDone = true;
        }

        //clear the panel and add the Overview user control to it
        private void btnOverview_Click(object sender, EventArgs e)
        {
            //by creating a new user control of the type Overview
            pnlContent.Controls.Clear();
            FormOverview = new Overview(this);            
            FormOverview.AutoScroll = true;
            pnlContent.Controls.Add(FormOverview);
            FormOverview.Show();
            FormOverview.Dock = DockStyle.Fill;
            lblHide.BackColor = Color.White;
            lblShow.BackColor = Color.White;
            gpsActive = false;
        }

        //clear the panel and add the GPS user control to it
        private void btnGPS_Click(object sender, EventArgs e)
        {
            //by creating a new user control of the type GPS
            pnlContent.Controls.Clear();
            FormGPS = new GPS(this);            
            FormGPS.AutoScroll = true;
            pnlContent.Controls.Add(FormGPS);            
            FormGPS.Show();
            FormGPS.Dock = DockStyle.Fill;            
            lblHide.BackColor = FormGPS.BackColor;
            gpsActive = true;
        }

        //open a select file dialog to get the file with the needed data
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            gpsActive = false;
            ofd.Filter = "Textdateien |*.txt| Alle Dateien|*.*";
            ofd.InitialDirectory = "C:\\Repositories\01_Doku\\Vorgabe\\";
            //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            ofd.Title = "Textdatei öffnen";
            ofd.FileName = "";

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Datei = new MeasurementFile(ofd.FileName);
                test = Datei.ConvertCSVtoDataTable();
                units = Datei.GetMeasurementUnits();  //Ausgabe der Einheiten 

                DoCalculations();  //Berechnungen durchführen                 
            }
            else
            {
                //Wen kein File eingelesen wurde den Benutzer darauf hinweisen
                MessageBox.Show("Bitte File einlesen!", "Fehler!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        //when button graphic gets clicked
        private void btnGraphic_Click(object sender, EventArgs e)
        {
            //Anzeige MessageBox falls Datatable leer!
            if (test.Rows.Count == 0)
            {
                MessageBox.Show("Keine Daten geladen!");
            }
            //Öffnen des Diagramm-Forms, falls Datatable gefüllt
            else
            {
                //Überprüfung, ob bereits Datenauswahl-Fenster geöffnet
                if (Application.OpenForms["Datenauswahl"] != null)
                    {
                        Application.OpenForms["Datenauswahl"].Activate();
                    }
                else
                {
                    Datenauswahl DataDiagram = new Datenauswahl(this);
                    DataDiagram.Show();
                }
            }
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
            pnlContent.Left = pnlSideBar.Width;
            pnlContent.Width = ClientSize.Width - pnlSideBar.Width;
        }

        //timer used for slowly moving the sidepanel in or out
        private void tmrFade_Tick(object sender, EventArgs e)
        {
            if(inout)
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
                    pnlContent.Left = 0;
                    pnlContent.Width = ClientSize.Width;
                    lblShow.Show();
                }
            }
        }

        //adds a marker at the transmitted coordinates to the map
        public void SetMarker(double latitude, double longitude)
        {
            if (gpsActive)
                FormGPS.SetMarker(latitude, longitude);
        }

        //when this form is closed, show calling form again
        private void DriversGuideApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.Show();
        }

        //when form is closing, close all active charts
        private void DriversGuideApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Schliessen ggf. noch offener Formen:
            if (Application.OpenForms["Datenauswahl"] != null)
            {
                Application.OpenForms["Datenauswahl"].Close();
            }

            string ofs = Application.OpenForms.Count.ToString();   //Anzahl der Geöffneten Forms
            int of = Convert.ToInt32(ofs);

            if (Application.OpenForms["PlotGraphic"] != null)
            {
                for(int i = 0; i<Convert.ToInt32(Application.OpenForms.Count.ToString()); i++)
                {
                    string fc = Application.OpenForms[i].ToString();
                    if (fc == "DriversGuide.PlotGraphic, Text: PlotGraphic")
                    {
                        Application.OpenForms[i].Close();
                        i -= 1;
                    }
                }
            }
        }

        //clear the panel and add the Dynamic user control to it
        private void btnShowDynamic_Click(object sender, EventArgs e)
        {
            //by creating a new user control of the type Dynamic
            pnlContent.Controls.Clear();
            FormDynamic = new Dynamic(this);
            FormDynamic.AutoScroll = true;
            pnlContent.Controls.Add(FormDynamic);
            FormDynamic.Show();
            FormDynamic.Dock = DockStyle.Fill;
            lblHide.BackColor = Color.White;
            lblShow.BackColor = Color.White;
            gpsActive = false;
        }

        //set background matching color of the button when the mouse leaves it
        private void btnReadFile_MouseLeave(object sender, EventArgs e)
        {
            btnReadFile.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        //set slightly different color of the button when the mouse enters it
        //to determine the size of the button
        private void btnReadFile_MouseEnter(object sender, EventArgs e)
        {
            btnReadFile.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        //paint the formated text from the bitmap into the button
        private void btnReadFile_Paint(object sender, PaintEventArgs e)
        {
            DrawInBitmap(btnReadFile, "File einlesen...", Color.Teal);
            Graphics g = e.Graphics;
            g.DrawImage(bmp, 0, 0);
        }

        //redraw content if button is resized
        private void btnReadFile_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnReadFile.ClientSize.Width, btnReadFile.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnReadFile.Invalidate();
        }

        //draws the wished title for the button to a bitmap
        private void DrawInBitmap(Panel caller, string text, Color color)
        {
            //clear graphics and enable smoother lines
            z.Clear(caller.BackColor);
            z.SmoothingMode = SmoothingMode.AntiAlias;

            //set width and height
            float breite = 194;
            float hoehe = 30;

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
        private void btnGraphic_MouseLeave(object sender, EventArgs e)
        {
            btnGraphic.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        //set slightly different color of the button when the mouse enters it
        //to determine the size of the button
        private void btnGraphic_MouseEnter(object sender, EventArgs e)
        {
            btnGraphic.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        //paint the formated text from the bitmap into the button
        private void btnGraphic_Paint(object sender, PaintEventArgs e)
        {
            //choose text color according to state
            if (btnGraphic.Enabled)
            {
                DrawInBitmap(btnGraphic, "Grafik", enabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
            else
            {
                DrawInBitmap(btnGraphic, "Grafik", disabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
        }

        //redraw content if button is resized
        private void btnGraphic_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnGraphic.ClientSize.Width, btnGraphic.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnGraphic.Invalidate();
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
                DrawInBitmap(btnOverview, "Überblick", enabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
            else
            {
                DrawInBitmap(btnOverview, "Überblick", disabled);
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
        private void btnShowDynamic_MouseLeave(object sender, EventArgs e)
        {
            btnShowDynamic.BackColor = ColorTranslator.FromHtml("#FF87CEFA");
        }

        //set slightly different color of the button when the mouse enters it
        //to determine the size of the button
        private void btnShowDynamic_MouseEnter(object sender, EventArgs e)
        {
            btnShowDynamic.BackColor = ColorTranslator.FromHtml("#7AB8DE");
        }

        //paint the formated text from the bitmap into the button
        private void btnShowDynamic_Paint(object sender, PaintEventArgs e)
        {
            //choose text color according to state
            if (btnShowDynamic.Enabled)
            {
                DrawInBitmap(btnShowDynamic, "Dynamik", enabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
            else
            {
                DrawInBitmap(btnShowDynamic, "Dynamik", disabled);
                Graphics g = e.Graphics;
                g.DrawImage(bmp, 0, 0);
            }
        }

        //redraw content if button is resized
        private void btnShowDynamic_Resize(object sender, EventArgs e)
        {
            bmp = new Bitmap(btnShowDynamic.ClientSize.Width, btnShowDynamic.ClientSize.Height);
            z = Graphics.FromImage(bmp);

            btnShowDynamic.Invalidate();
        }

        //make sure the buttons are always centered corretly in the middle of the side panel
        private void CenterButtons()
        {
            //calculate the postion of the middle and center buttons according to that
            int half = (ClientSize.Height - pnlLogo.Height) / 2 + pnlLogo.Height;

            btnReadFile.Top = half - 95;
            btnGraphic.Top = half - 55;
            btnGPS.Top = half - 15;
            btnOverview.Top = half + 25;
            btnShowDynamic.Top = half + 65;
        }

        //if the form is resized
        private void DriversGuideApp_Resize(object sender, EventArgs e)
        {
            //re-center buttons
            CenterButtons();
        }
    }
}
