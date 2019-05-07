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
        DriversGuideMain Form1Copy;                   //Verbindung zu Hauptform
        Datenauswahl AuswahlCopy;          //Verbindung zu Datenauswahlform
        DataTable tt = new DataTable();    //Erstellung neues Datatable
        DataTable units = new DataTable(); //Erstellung neues Datatable für Einheiten
        GraphicsCreate newchart = new GraphicsCreate();   //neue Grafik erstellen, Eigenschaften in GraphicsCreate festgelegt
        int i = 0;
        public bool ChartReady = false;   //bool-Wert für Timer

        public PlotGraphic(DriversGuideMain CreateForm)
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

        public void PlotGraphic_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;   //Fenster maximieren
            //this.MinimumSize = this.Size;
            //this.MaximumSize = this.Size;

            tt = Form1Copy.GetCompleteDataTable();   //Holen des Datatables
            tt = tt.Copy();                          //Kopie des Datatables

            for (int i = 0; i < tt.Rows.Count; i++)   //Umrechnung Zeit in sec
            {
                tt.Rows[i]["Time"] = Convert.ToInt64((tt.Rows[i]["Time"])) / 1000;
            }

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

            if (GewDaten1 != "" && GewDaten2 == "" && GewDaten3 == "" && GewDaten4 == "")
            {
                chart1.Height = pictureBox1.Height;
                chart1.Width = pictureBox1.Width;
                chart1.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y);
                DrawChart(chart1, GewDaten1);
                lblPos1.Location = new Point(chart1.Location.X + chart1.Width - 100, chart1.Location.Y + chart1.Height / 2 - 20);
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
                DrawChart(chart1, GewDaten1);
                LocateLabel(chart1, lblPos1);

                chart2.Height = pictureBox1.Height / 2;
                chart2.Width = pictureBox1.Width;
                chart2.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y + pictureBox1.Height - chart2.Height);
                DrawChart(chart2, GewDaten2);
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
                DrawChart(chart1, GewDaten1);
                LocateLabel(chart1, lblPos1);

                chart2.Height = pictureBox1.Height / 2;
                chart2.Width = pictureBox1.Width / 2;
                chart2.Location = new Point(pictureBox1.Location.X + pictureBox1.Width - chart2.Width,
                                            pictureBox1.Location.Y);
                DrawChart(chart2, GewDaten2);
                LocateLabel(chart2, lblPos2);

                chart3.Height = pictureBox1.Height / 2;
                chart3.Width = pictureBox1.Width;
                chart3.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y + pictureBox1.Height - chart3.Height);
                DrawChart(chart3, GewDaten3);
                LocateLabel(chart4, lblPos4);

                chart4.Visible = false;
            }
            else if (GewDaten1 != "" && GewDaten2 != "" && GewDaten3 != "" && GewDaten4 != "")
            {
                chart1.Height = pictureBox1.Height / 2;
                chart1.Width = pictureBox1.Width / 2;
                chart1.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y);
                DrawChart(chart1, GewDaten1);
                LocateLabel(chart1, lblPos1);

                chart2.Height = pictureBox1.Height / 2;
                chart2.Width = pictureBox1.Width / 2;
                chart2.Location = new Point(pictureBox1.Location.X + pictureBox1.Width - chart2.Width,
                                            pictureBox1.Location.Y);
                DrawChart(chart2, GewDaten2);
                LocateLabel(chart2, lblPos2);

                chart3.Height = pictureBox1.Height / 2;
                chart3.Width = pictureBox1.Width / 2;
                chart3.Location = new Point(pictureBox1.Location.X,
                                            pictureBox1.Location.Y + pictureBox1.Height - chart3.Height);
                DrawChart(chart3, GewDaten3);
                LocateLabel(chart3, lblPos3);

                chart4.Height = pictureBox1.Height / 2;
                chart4.Width = pictureBox1.Width / 2;
                chart4.Location = new Point(pictureBox1.Location.X + pictureBox1.Width - chart4.Width,
                                            pictureBox1.Location.Y + pictureBox1.Height - chart4.Height);
                DrawChart(chart4, GewDaten4);
                LocateLabel(chart4, lblPos4);
            }

            //Chart CreateChart = new Chart();
            //CreateChart.Show();

            tmrTimeSpan.Enabled = true;   //aktiviert Timer, um verstrichene Zeit zu messen
        }

        private void ClearChart(Chart chartname)
        {
            chartname.Series.Clear();       //Leeren der Daten für Diagramm
            chartname.ChartAreas.Clear();   //Löschen der Diagrammoberfläche
        }

        private void DrawChart(Chart chartname, string GewDat)
        {
            newchart.ConnectToForm1(Form1Copy);                     //Form1-Verlinkung zu GraphicsCreate
            newchart.GetChosenData(this);                           //erstellt Zugriff auf Datenauswahl-Feld
            newchart.SetChartProperties(ref chartname, GewDat);   //Festlegen der Chart-Eigenschaften
        }

        private void LocateLabel(Chart chartname, Label labelname)
        {
            labelname.Location = new Point(chartname.Location.X + chartname.Width - 100, chartname.Location.Y + chartname.Height / 2 - 20);
            labelname.Visible = true;
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
                labelname.Text = "x = " + xpos.ToString() + " " + xUnit + "\n" +
                              "y = " + ypos.ToString() + " " + yUnit;

                //lblPos.BackColor = Color.White;
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
                    labelname.Text = "x = " + xpos.ToString() + " " + xUnit + "\n" +
                                  "y = " + ypos.ToString() + " " + yUnit;
                }

                //lblPos.BackColor = Color.White;
            }
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten1 = GewDatenMZ[0];

            chart1.ChartAreas[GewDaten1].AxisX.ScaleView.Zoom(chart1.ChartAreas[GewDaten1].AxisX.Minimum, chart1.ChartAreas[GewDaten1].AxisX.Maximum);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            chart1.ChartAreas[GewDaten1].AxisY.ScaleView.Zoom(chart1.ChartAreas[GewDaten1].AxisY.Minimum, chart1.ChartAreas[GewDaten1].AxisY.Maximum);   //Startskalierung der y-Achse
        }

        private void chart2_MouseWheel(object sender, MouseEventArgs e)
        {
            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten2 = GewDatenMZ[1];

            chart2.ChartAreas[GewDaten2].AxisX.ScaleView.Zoom(chart2.ChartAreas[GewDaten2].AxisX.Minimum, chart2.ChartAreas[GewDaten2].AxisX.Maximum);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            chart2.ChartAreas[GewDaten2].AxisY.ScaleView.Zoom(chart2.ChartAreas[GewDaten2].AxisY.Minimum, chart2.ChartAreas[GewDaten2].AxisY.Maximum);   //Startskalierung der y-Achse
        }

        private void chart3_MouseWheel(object sender, MouseEventArgs e)
        {
            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten3 = GewDatenMZ[2];

            chart3.ChartAreas[GewDaten3].AxisX.ScaleView.Zoom(chart3.ChartAreas[GewDaten3].AxisX.Minimum, chart3.ChartAreas[GewDaten3].AxisX.Maximum);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            chart3.ChartAreas[GewDaten3].AxisY.ScaleView.Zoom(chart3.ChartAreas[GewDaten3].AxisY.Minimum, chart3.ChartAreas[GewDaten3].AxisY.Maximum);   //Startskalierung der y-Achse
        }

        private void chart4_MouseWheel(object sender, MouseEventArgs e)
        {
            string[] GewDatenMZ = GiveChosenData();   //gibt gewählten Datenreihen zurück
            string GewDaten4 = GewDatenMZ[3];

            chart4.ChartAreas[GewDaten4].AxisX.ScaleView.Zoom(chart4.ChartAreas[GewDaten4].AxisX.Minimum, chart4.ChartAreas[GewDaten4].AxisX.Maximum);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            chart4.ChartAreas[GewDaten4].AxisY.ScaleView.Zoom(chart4.ChartAreas[GewDaten4].AxisY.Minimum, chart4.ChartAreas[GewDaten4].AxisY.Maximum);   //Startskalierung der y-Achse
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
    }
}
