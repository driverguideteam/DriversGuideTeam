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
using System.Windows.Forms.DataVisualization.Charting;   //für einige Chart-Optionen benötigt

namespace DriversGuide
{
    public partial class PlotGraphic : Form
    {
        DriversGuideApp Form1Copy;                   //Verbindung zu Hauptform
        Datenauswahl AuswahlCopy;          //Verbindung zu Datenauswahlform
        DataTable tt = new DataTable();    //Erstellung neues Datatable
        DataTable units = new DataTable(); //Erstellung neues Datatable für Einheiten
        GraphicsCreate newchart = new GraphicsCreate();   //neue Grafik erstellen, Eigenschaften in GraphicsCreate festgelegt
        int i = 0;
        public bool ChartReady = false;   //bool-Wert für Timer

        public PlotGraphic(DriversGuideApp CreateForm)
        {
            Form1Copy = CreateForm;   //Zugriff auf Hauptform
            InitializeComponent();
            this.chart1.MouseWheel += chart1_MouseWheel;   //Erstellen MouseWheel-Ereignis
            this.chart2.MouseWheel += chart2_MouseWheel;   //Erstellen MouseWheel-Ereignis
            this.chart3.MouseWheel += chart3_MouseWheel;   //Erstellen MouseWheel-Ereignis
            this.chart4.MouseWheel += chart4_MouseWheel;   //Erstellen MouseWheel-Ereignis
        }

        public void ConnectToDatenauswahl(Datenauswahl CreateForm)
        {
            AuswahlCopy = CreateForm;   //Zugriff auf Datenauswahlform
        }

        public string[] GiveChosenData()
        {
            string[] GewDaten = AuswahlCopy.ChosenData();
            return GewDaten;   //gibt die Namen der Datenreihen zurück, welche für die Grafik ausgewählt wurden (max. 4)
        }

        public string[] GetUnits(string GewDaten)   //liefert StringArray mit x- u. y-Einheiten
        {
            units = Form1Copy.GetUnitsDataTable();   //Kopie des Einheiten-Datatables

            //string xUnit = Convert.ToString((units.Rows[0]["Time"]));
            string xUnit = "sec";
            string yUnit = Convert.ToString((units.Rows[0][GewDaten]));

            string[] xyUnits = new string[] { xUnit, yUnit };   //erstellt StringArray mit x- u. y-Einheiten

            return xyUnits;   //liefert StingArray zurück
        }

        public string TimeInHhmmss(int seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            string hhmmss = time.ToString(@"hh\ \h\ mm\ \m\ ss\ \s");

            return hhmmss;
        }

        public DataTable AddTimeFormat(ref DataTable tt)
        {
            for (int i = 0; i < tt.Rows.Count; i++)   //Umrechnung Zeit in sec
            {
                tt.Rows[i]["Time"] = Convert.ToInt64((tt.Rows[i]["Time"])) / 1000;
            }

            tt.Columns.Add("TimeFormat", typeof(string));

            for (int i = 0; i < tt.Rows.Count; i++)   //Umrechnung Zeitformat
            {
               tt.Rows[i]["TimeFormat"] = TimeInHhmmss(Convert.ToInt32((tt.Rows[i]["Time"])));    
            }

            return tt;
        }

        public void PlotGraphic_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;   //Fenster maximieren
            //this.MinimumSize = this.Size;
            //this.MaximumSize = this.Size;

            tt = Form1Copy.GetCompleteDataTable();   //Holen des Datatables
            tt = tt.Copy();                          //Kopie des Datatables

            AddTimeFormat(ref tt);

            ClearChart(chart1);
            ClearChart(chart2);
            ClearChart(chart3);
            ClearChart(chart4);
            lblPos1.Visible = false;
            lblPos2.Visible = false;
            lblPos3.Visible = false;
            lblPos4.Visible = false;

            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten1 = GewDatenMZ[0];
            string GewDaten2 = GewDatenMZ[1];
            string GewDaten3 = GewDatenMZ[2];
            string GewDaten4 = GewDatenMZ[3];

            LocateCharts(GewDaten1, GewDaten2, GewDaten3, GewDaten4);

            //Chart CreateChart = new Chart();
            //CreateChart.Show();
        }

        private void ClearChart(Chart chartname)
        {
            chartname.Series.Clear();       //Leeren der Daten für Diagramm
            chartname.ChartAreas.Clear();   //Löschen der Diagrammoberfläche
        }

        private void DrawChart(Chart chartname, DataTable tt, string GewDat)
        {
            newchart.ConnectToForm1(Form1Copy);                                    //Form1-Verlinkung zu GraphicsCreate
            newchart.GetChosenData(this);                                          //erstellt Zugriff auf Datenauswahl-Feld
            newchart.SetChartProperties(ref chartname, tt,"Time", GewDat, this);   //Festlegen der Chart-Eigenschaften
            newchart.SetChartAxes(ref chartname, tt, "Time", GewDat, this);        //Festlegen der x-u.y-Achsen-Eigenschaften
        }

        private void LocateLabel(Chart chartname, Label labelname)
        {
            labelname.Location = new Point(chartname.Location.X + chartname.Width - 150, chartname.Location.Y + 10);
            labelname.Visible = false;
        }

        public void LocateCharts(string GewDaten1, string GewDaten2, string GewDaten3, string GewDaten4)
        {
            tt = Form1Copy.GetCompleteDataTable();   //Holen des Datatables
            tt = tt.Copy();                          //Kopie des Datatables

            AddTimeFormat(ref tt);

            if (GewDaten1 != "" && GewDaten2 == "" && GewDaten3 == "" && GewDaten4 == "")
            {
                chart1.Height = pictureBox1.Height;
                chart1.Width = pictureBox1.Width;
                chart1.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y);
                DrawChart(chart1, tt, GewDaten1);
                LocateLabel(chart1, lblPos1);
                lblPos1.Visible = true;

                chart2.Visible = false;
                chart3.Visible = false;
                chart4.Visible = false;
            }
            else if (GewDaten1 != "" && GewDaten2 != "" && GewDaten3 == "" && GewDaten4 == "")
            {
                chart1.Height = pictureBox1.Height / 2;
                chart1.Width = pictureBox1.Width;
                chart1.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y);
                DrawChart(chart1, tt, GewDaten1);
                LocateLabel(chart1, lblPos1);

                chart2.Height = pictureBox1.Height / 2;
                chart2.Width = pictureBox1.Width;
                chart2.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y + pictureBox1.Height - chart2.Height);
                DrawChart(chart2, tt, GewDaten2);
                LocateLabel(chart2, lblPos2);

                chart3.Visible = false;
                chart4.Visible = false;
            }
            if (GewDaten1 != "" && GewDaten2 != "" && GewDaten3 != "" && GewDaten4 == "")
            {
                chart1.Height = pictureBox1.Height / 2;
                chart1.Width = pictureBox1.Width / 2;
                chart1.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y);
                DrawChart(chart1, tt, GewDaten1);
                LocateLabel(chart1, lblPos1);

                chart2.Height = pictureBox1.Height / 2;
                chart2.Width = pictureBox1.Width / 2;
                chart2.Location = new Point(pictureBox1.Location.X + pictureBox1.Width - chart2.Width,
                                            pictureBox1.Location.Y);
                DrawChart(chart2, tt, GewDaten2);
                LocateLabel(chart2, lblPos2);

                chart3.Height = pictureBox1.Height / 2;
                chart3.Width = pictureBox1.Width;
                chart3.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y + pictureBox1.Height - chart3.Height);
                DrawChart(chart3, tt, GewDaten3);
                LocateLabel(chart3, lblPos3);

                chart4.Visible = false;
            }
            else if (GewDaten1 != "" && GewDaten2 != "" && GewDaten3 != "" && GewDaten4 != "")
            {
                chart1.Height = pictureBox1.Height / 2;
                chart1.Width = pictureBox1.Width / 2;
                chart1.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y);
                DrawChart(chart1, tt, GewDaten1);
                LocateLabel(chart1, lblPos1);

                chart2.Height = pictureBox1.Height / 2;
                chart2.Width = pictureBox1.Width / 2;
                chart2.Location = new Point(pictureBox1.Location.X + pictureBox1.Width - chart2.Width,
                                            pictureBox1.Location.Y);
                DrawChart(chart2, tt, GewDaten2);
                LocateLabel(chart2, lblPos2);

                chart3.Height = pictureBox1.Height / 2;
                chart3.Width = pictureBox1.Width / 2;
                chart3.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y + pictureBox1.Height - chart3.Height);
                DrawChart(chart3, tt, GewDaten3);
                LocateLabel(chart3, lblPos3);

                chart4.Height = pictureBox1.Height / 2;
                chart4.Width = pictureBox1.Width / 2;
                chart4.Location = new Point(pictureBox1.Location.X + pictureBox1.Width - chart4.Width,
                                            pictureBox1.Location.Y + pictureBox1.Height - chart4.Height);
                DrawChart(chart4, tt, GewDaten4);
                LocateLabel(chart4, lblPos4);
            }
            tmrTimeSpan.Enabled = true;   //aktiviert Timer, um verstrichene Zeit zu messen
        }

        private void SetStartScale(Chart chartname, string GewDatenX)
        {
            chartname.ChartAreas[GewDatenX].AxisX.ScaleView.Zoom(chartname.ChartAreas[GewDatenX].AxisX.Minimum, chartname.ChartAreas[GewDatenX].AxisX.Maximum);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            chartname.ChartAreas[GewDatenX].AxisY.ScaleView.Zoom(chartname.ChartAreas[GewDatenX].AxisY.Minimum, chartname.ChartAreas[GewDatenX].AxisY.Maximum);   //Startskalierung der y-Achse
        }

        private void MoveCursor(Chart chartname, Label labelname, MouseEventArgs e, int x)
        {
            //bei Bewegen der Maus wird der Cursor auf die aktuelle Mausposition gestellt und 
            //die dazugehörigen x- u. y-Werte angezeigt

            if (ChartReady == true)
            {
                //wird erst ausgeführt nachdem gewisse Zeit verstrichen ist, da ansonsten noch nicht alle
                //Datenpunkte in der Grafik gezeichnet wurden, was zu einer Fehlermeldung führt

                string GewDaten = GiveChosenData()[x];

                double xpos = newchart.ActualXPosition(ref chartname, e, GewDaten);
                double ypos = newchart.FindYValue(chartname, xpos, GewDaten);

                chartname.ChartAreas[GewDaten].CursorX.Position = xpos;
                chartname.ChartAreas[GewDaten].CursorY.Position = ypos;

                string xUnit = GetUnits(GewDaten)[0];
                string yUnit = GetUnits(GewDaten)[1];

                labelname.Visible = true;
                labelname.Text = "x = " + tt.Rows[Convert.ToInt32(xpos)]["TimeFormat"] + "\n" +
                              "y = " + ypos.ToString() + " " + yUnit;

                //lblPos.BackColor = Color.White;

                double latitude = Convert.ToDouble(tt.Rows[Convert.ToInt32(xpos)]["GPS_Latitude"]);
                double longitude = Convert.ToDouble(tt.Rows[Convert.ToInt32(xpos)]["GPS_Longitude"]);

                Form1Copy.SetMarker(latitude, longitude);
            }
        }

        private void MoveOtherCursor(Chart groundchart, int x, Chart otherchart, int y, Label labelname, MouseEventArgs e)
        {
            //bei Bewegen der Maus wird der Cursor auf die aktuelle Mausposition gestellt und 
            //die dazugehörigen x- u. y-Werte angezeigt

            if (ChartReady == true)
            {
                //wird erst ausgeführt nachdem gewisse Zeit verstrichen ist, da ansonsten noch nicht alle
                //Datenpunkte in der Grafik gezeichnet wurden, was zu einer Fehlermeldung führt

                string GewDaten = GiveChosenData()[x];

                double xpos = newchart.ActualXPosition(ref groundchart, e, GewDaten);

                if (otherchart.Visible == true)
                {
                    string OtherGewDaten = GiveChosenData()[y];

                    double ypos = newchart.FindYValue(otherchart, xpos, OtherGewDaten);

                    otherchart.ChartAreas[OtherGewDaten].CursorX.Position = xpos;
                    otherchart.ChartAreas[OtherGewDaten].CursorY.Position = ypos;

                    string xUnit = GetUnits(OtherGewDaten)[0];
                    string yUnit = GetUnits(OtherGewDaten)[1];

                    labelname.Visible = true;
                    labelname.Text = "x = " + tt.Rows[Convert.ToInt32(xpos)]["TimeFormat"] + "\n" +
                                  "y = " + ypos.ToString() + " " + yUnit;
                }

                //lblPos.BackColor = Color.White;
            }
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten1 = GewDatenMZ[0];
            SetStartScale(chart1, GewDaten1);
        }

        private void chart2_MouseWheel(object sender, MouseEventArgs e)
        {
            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten2 = GewDatenMZ[1];
            SetStartScale(chart2, GewDaten2);
        }

        private void chart3_MouseWheel(object sender, MouseEventArgs e)
        {
            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten3 = GewDatenMZ[2];
            SetStartScale(chart3, GewDaten3);
        }

        private void chart4_MouseWheel(object sender, MouseEventArgs e)
        {
            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten4 = GewDatenMZ[3];
            SetStartScale(chart4, GewDaten4);
        }

        private void tmrTimeSpan_Tick(object sender, EventArgs e)
        {
            //Timer stellt nach 1 sec den Bool-Wert auf true
            //dann ist Cursor aktiv
            i++;

            if (i >= 1)
            {
                ChartReady = true;
                tmrTimeSpan.Enabled = false;
            }
        }

        private void chart1_MouseMove_1(object sender, MouseEventArgs e)
        {
            MoveCursor(chart1, lblPos1, e, 0);
            MoveOtherCursor(chart1, 0, chart2, 1, lblPos2, e);
            MoveOtherCursor(chart1, 0, chart3, 2, lblPos3, e);
            MoveOtherCursor(chart1, 0, chart4, 3, lblPos4, e);
        }

        private void chart2_MouseMove_1(object sender, MouseEventArgs e)
        {
            MoveCursor(chart2, lblPos2, e, 1);
            MoveOtherCursor(chart2, 1, chart1, 0, lblPos1, e);
            MoveOtherCursor(chart2, 1, chart3, 2, lblPos3, e);
            MoveOtherCursor(chart2, 1, chart4, 3, lblPos4, e);
        }

        private void chart3_MouseMove_1(object sender, MouseEventArgs e)
        {
            MoveCursor(chart3, lblPos3, e, 2);
            MoveOtherCursor(chart3, 2, chart1, 0, lblPos1, e);
            MoveOtherCursor(chart3, 2, chart2, 1, lblPos2, e);
            MoveOtherCursor(chart3, 2, chart4, 3, lblPos4, e);
        }

        private void chart4_MouseMove_1(object sender, MouseEventArgs e)
        {
            MoveCursor(chart4, lblPos4, e, 3);
            MoveOtherCursor(chart4, 3, chart1, 0, lblPos1, e);
            MoveOtherCursor(chart4, 3, chart2, 1, lblPos2, e);
            MoveOtherCursor(chart4, 3, chart3, 2, lblPos3, e);
        }

        private void PlotGraphic_Resize(object sender, EventArgs e)
        {
            if (ChartReady == true)
            {
                string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
                string GewDaten1 = GewDatenMZ[0];
                string GewDaten2 = GewDatenMZ[1];
                string GewDaten3 = GewDatenMZ[2];
                string GewDaten4 = GewDatenMZ[3];

                LocateCharts(GewDaten1, GewDaten2, GewDaten3, GewDaten4);
            }
            ChartReady = false;
        }

    }
}
