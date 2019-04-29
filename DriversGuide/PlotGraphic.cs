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
        Form1 Form1Copy;                   //Verbindung zu Hauptform
        Datenauswahl AuswahlCopy;          //Verbindung zu Datenauswahlform
        DataTable tt = new DataTable();    //Erstellung neues Datatable
        GraphicsCreate newchart = new GraphicsCreate();   //neue Grafik erstellen, Eigenschaften in GraphicsCreate festgelegt
        int i = 0;
        public bool ChartReady = false;   //bool-Wert für Timer

        public PlotGraphic(Form1 CreateForm)
        {
            Form1Copy = CreateForm;   //Zugriff auf Hauptform
            InitializeComponent();
            this.chart1.MouseWheel += chart1_MouseWheel;   //Erstellen MouseWheel-Ereignis
        }

        public void GetChosenData(Datenauswahl CreateForm)
        {
            AuswahlCopy = CreateForm;   //Zugriff auf Datenauswahlform
        }

        public string GiveChosenData()
        {
            string GewDaten = AuswahlCopy.ChosenData();
            return GewDaten;   //gibt den Namen der Datenreihe zurück, welche für die Grafik ausgewählt wurde
        }

        public void PlotGraphic_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;   //Fenster maximieren
            //this.MinimumSize = this.Size;
            //this.MaximumSize = this.Size;

            lblPos.Visible = false;

            tt = Form1Copy.test.Copy();   //Kopie des Datatables

            for (int i = 0; i < tt.Rows.Count; i++)   //Umrechnung Zeit in sec
            {
                tt.Rows[i]["Time"] = Convert.ToInt64((tt.Rows[i]["Time"])) / 1000;
            }

            chart1.Series.Clear();       //Leeren der Daten für Diagramm
            chart1.ChartAreas.Clear();   //Löschen der Diagrammoberfläche

            newchart.ConnectToForm1(Form1Copy);   //Form1-Verlinkung zu GraphicsCreate
            newchart.GetChosenData(this);         //erstellt Zugriff auf Datenauswahl-Feld
            newchart.Drawchart(ref chart1);       //Festlegen der Chart-Eigenschaften

            string GewDaten = GiveChosenData();   //gibt gewählte Datenreihe zurück

            for (int i = 0; i < tt.Rows.Count; i++)   //füllen Datenpunke-Serie
            {
                chart1.Series[GewDaten].Points.AddXY(Convert.ToInt64(tt.Rows[i]["Time"]), Convert.ToDouble(tt.Rows[i][GewDaten]));
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }

            //Chart CreateChart = new Chart();
            //CreateChart.Show();

            tmrTimeSpan.Enabled = true;   //aktiviert Timer, um verstrichene Zeit zu messen
        }

        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            //bei Drehen des Mausrades wird die Start-Ansicht der Grafik eingestellt

            string GewDaten = GiveChosenData();   //gibt gewählte Datenreihe zurück

            chart1.ChartAreas[GewDaten].AxisX.ScaleView.Zoom(chart1.ChartAreas[GewDaten].AxisX.Minimum, chart1.ChartAreas[GewDaten].AxisX.Maximum);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            chart1.ChartAreas[GewDaten].AxisY.ScaleView.Zoom(chart1.ChartAreas[GewDaten].AxisY.Minimum, chart1.ChartAreas[GewDaten].AxisY.Maximum);   //Startskalierung der y-Achse
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            //bei Bewegen der Maus wird der Cursor auf die aktuelle Mausposition gestellt und 
            //die dazugehörigen x- u. y-Werte angezeigt

            if (ChartReady == true)
            {
                //wird erst ausgeführt nachdem gewisse Zeit verstrichen ist, da ansonsten noch nicht alle
                //Datenpunkte in der Grafik gezeichnet wurden, was zu einer Fehlermeldung führt

                double[] AP = newchart.ActualPosition(ref chart1, e);

                string GewDaten = GiveChosenData();

                chart1.ChartAreas[GewDaten].CursorX.Position = AP[0];
                chart1.ChartAreas[GewDaten].CursorY.Position = AP[1];

                lblPos.Visible = true;
                lblPos.Text = "x = " + AP[0].ToString() + "\n" +
                              "y = " + AP[1].ToString();
            }
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
    }
}
