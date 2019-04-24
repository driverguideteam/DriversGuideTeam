using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;   //für einige Chart-Optionen benötigt

namespace DriversGuide
{
    class GraphicsCreate
    {
        Form1 Form1Copy;
        PlotGraphic PlotCopy;
        DataTable tt = new DataTable();

        public void ConnectToForm1(Form1 CreateForm)
        {
            Form1Copy = CreateForm;   //Zugriff auf Hauptform
            tt = Form1Copy.test.Copy();
            for (int i = 0; i < tt.Rows.Count; i++)   //Umrechnung Zeit in sec
            {
                tt.Rows[i]["Time"] = Convert.ToInt64((tt.Rows[i]["Time"])) / 1000;
            }
        }

        public void GetChosenData(PlotGraphic ConnectForm)
        {
            PlotCopy = ConnectForm;   //Zugriff auf Datenauswahlform
        }

        public void Drawchart(ref Chart chartname)
        {
            string GewDaten = PlotCopy.GiveChosenData();

            chartname.Show();

            chartname.Series.Clear();
            chartname.ChartAreas.Clear();

            chartname.Series.Add(GewDaten);
            chartname.ChartAreas.Add(GewDaten);

            var Chart1 = chartname.ChartAreas[GewDaten];

            chartname.Series[GewDaten].ChartType = SeriesChartType.Spline;

            chartname.Invalidate();

            Chart1.CursorX.IsUserEnabled = true;   //aktiviert Cursor (roter Strich)
            Chart1.CursorX.IsUserSelectionEnabled = true;   //aktiviert Bereichsauswahl
            Chart1.AxisX.ScaleView.Zoomable = true;

            Chart1.CursorY.IsUserEnabled = true;   //aktiviert Cursor (roter Strich)
            Chart1.CursorY.IsUserSelectionEnabled = true;   //aktiviert Bereichsauswahl
            Chart1.AxisY.ScaleView.Zoomable = true;

            Chart1.AxisX.LabelStyle.Format = "";
            Chart1.AxisY.LabelStyle.Format = "";
            Chart1.AxisY.LabelStyle.IsEndLabelVisible = true;   //true: erster u. letzter Wert der Achsenbeschriftung werden angezeigt

            Chart1.AxisX.Minimum = Convert.ToInt64(tt.Rows[0]["Time"]);                 //Festlegung x-Achsen-Minimum
            Chart1.AxisX.Maximum = Convert.ToInt64(tt.Rows[tt.Rows.Count - 1]["Time"]);   //Festlegung x-Achsen-Maximum
                                                                                          //Chart1.AxisX.Interval = 300;                                              //Festlegung x-Achsen-Intervall

            //wird ohne Einstellung vom Programm selbst besser eingestellt:
            //Chart1.AxisY.Minimum = Convert.ToInt64(tt.Rows[0][GewDaten]);     //Festlegung y-Achsen-Minimum
            //Chart1.AxisY.Maximum = Convert.ToInt64(tt.Rows[xxx][GewDaten]);   //Festlegung y-Achsen-Maximum
            //Chart1.AxisY.Interval = 300;                                      //Festlegung y-Achsen-Intervall



            //evtl. später benötigte Funktionen:

            //Chart1.AxisX.ScaleView.Zoom(-100, 100);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            //Chart1.AxisY.ScaleView.Zoom(1.1, 1.1);   //Startskalierung der y-Achse

            //chart1.Series.Clear();   //Entfernen bestehender Datenpunkte
            //chart1.ChartAreas.Clear();   //Entfernen bestehender Diagrammbereiche
            //chart1.Series.Add("TestDaten1");   //Hinzufügen einer neuen Datenpunktserie
            //chart1.ChartAreas.Add("TestDaten1");   //Hinzufügen neuer Diagrammbereiche  
            //this.chart1.ChartAreas[0].AxisX.Maximum.ToString();   //liefert größten x-Wert nachdem gezeichnet wurde
            //Chart1.RecalculateAxesScale();
        }
    }
}
