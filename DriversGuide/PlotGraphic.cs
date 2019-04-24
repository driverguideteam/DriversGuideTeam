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

        public PlotGraphic(Form1 CreateForm)
        {
            Form1Copy = CreateForm;   //Zugriff auf Hauptform
            InitializeComponent();
        }

        public void GetChosenData(Datenauswahl CreateForm)
        {
            AuswahlCopy = CreateForm;   //Zugriff auf Datenauswahlform
        }

        public string GiveChosenData()
        {
            string GewDaten = AuswahlCopy.ChosenData();
            return GewDaten;
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

            GraphicsCreate newchart = new GraphicsCreate();   //neue Grafik erstellen, Eigenschaften in GraphicsCreate festgelegt
            newchart.ConnectToForm1(Form1Copy);   //Form1-Verlinkung zu GraphicsCreate
            newchart.GetChosenData(this);
            newchart.Drawchart(ref chart1);

            string GewDaten = GiveChosenData();

            for (int i = 0; i < tt.Rows.Count; i++)   //füllen Datenpunke-Serie
            {
                chart1.Series[GewDaten].Points.AddXY(Convert.ToInt64(tt.Rows[i]["Time"]), Convert.ToDouble(tt.Rows[i][GewDaten]));
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }

            //Chart CreateChart = new Chart();
            //CreateChart.Show();

        }

        private void chart1_MouseClick_1(object sender, MouseEventArgs e)
        {
            lblPos.Visible = true;

            string GewDaten = GiveChosenData();

            var xv = chart1.ChartAreas[GewDaten].AxisX.PixelPositionToValue(e.X);
            Series S = chart1.Series[GewDaten];
            DataPoint pNext = S.Points.Select(x => x).Where(x => x.XValue <= xv).DefaultIfEmpty(S.Points.Last()).Last();

            lblPos.Text = "x = " + pNext.XValue.ToString() + "\n" +
                          "y = " + Math.Round(pNext.YValues[0], 2).ToString();

            lblPos.Location = new Point(e.X + 20, e.Y - 40);
        }
    }
}
