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
        DriversGuideMain Form1Copy;          //Zugriff auf Hauptform
        PlotGraphic PlotCopy;                //Zugriff auf PlotCopy
        DataTable tt = new DataTable();      //Erstellung neues Datatable
        public string xp;
        public string yp;

        public void ConnectToForm1(DriversGuideMain CreateForm)
        {
            Form1Copy = CreateForm;       //Zugriff auf Hauptform
            tt = Form1Copy.test.Copy();   //Kopie des Datatables

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
            string GewDaten = PlotCopy.GiveChosenData();   //gibt gewählte Datenreihe zurück

            chartname.Show();   //Anzeigen der Grafik

            chartname.Series.Clear();       //Löschen evtl. bestehender Datenpunktreihe
            chartname.ChartAreas.Clear();   //Löschen evtl. bestehender Grafikoberflächen

            chartname.Series.Add(GewDaten);       //Hinzufügen einer neuen Datenpunktreihe
            chartname.ChartAreas.Add(GewDaten);   //Hinzufügen einer neuen Grafikoberfläche

            chartname.Series[GewDaten].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line)

            chartname.Invalidate();

            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen

            //Chart1.CursorX.IsUserEnabled = true;               //aktiviert Cursor (roter Strich)
            Chart1.CursorX.IsUserSelectionEnabled = true;        //aktiviert Bereichsauswahl
            Chart1.CursorX.Interval = 0;                         //Intervall des Cursors
            Chart1.AxisX.ScaleView.Zoomable = true;
            Chart1.CursorX.LineColor = Color.Red;                //Linienfarbe Cursor
            Chart1.CursorX.LineWidth = 1;                        //Liniendicke Cursor
            Chart1.CursorX.LineDashStyle = ChartDashStyle.Solid; //Linienart Cursor

            //Chart1.CursorY.IsUserEnabled = true;               //aktiviert Cursor (roter Strich)
            Chart1.CursorY.IsUserSelectionEnabled = true;        //aktiviert Bereichsauswahl
            Chart1.CursorY.Interval = 0;                         //Intervall des Cursors
            Chart1.AxisY.ScaleView.Zoomable = true;
            Chart1.CursorY.LineColor = Color.Red;                //Linienfarbe Cursor
            Chart1.CursorY.LineWidth = 1;                        //Liniendicke Cursor
            Chart1.CursorY.LineDashStyle = ChartDashStyle.Solid; //Linienart Cursor

            Chart1.AxisX.Minimum = Convert.ToInt64(tt.Rows[0]["Time"]);                 //Festlegung x-Achsen-Minimum
            Chart1.AxisX.Maximum = Convert.ToInt64(tt.Rows[tt.Rows.Count - 1]["Time"]); //Festlegung x-Achsen-Maximum
            //Chart1.AxisX.Interval = 300;                                              //Festlegung x-Achsen-Intervall

            string xUnit = PlotCopy.GetUnits()[0];   //liefert Einheit der x-Achse
            string yUnit = PlotCopy.GetUnits()[1];   //liefert Einheit der y-Achse

            //chartname.Titles.Add("Test").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
            Chart1.AxisX.Title = "Time" + " in " + xUnit;                     //Beschriftung der x-Achse
            Chart1.AxisX.TitleAlignment = StringAlignment.Center;             //Ausrichtung der x-Achsen-Beschriftung
            Chart1.AxisX.TextOrientation = TextOrientation.Horizontal;        //Orientierung der x-Achsen-Beschriftung
            Chart1.AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);   //Schriftart der x-Achsen-Beschriftung
            Chart1.AxisX.LabelStyle.Format = "";                              //Achsenbeschriftungsformat

            Chart1.AxisY.Title = GewDaten + " in " + yUnit;                   //Beschriftung der y-Achse
            Chart1.AxisY.TitleAlignment = StringAlignment.Center;             //Ausrichtung der y-Achsen-Beschriftung
            Chart1.AxisY.TextOrientation = TextOrientation.Rotated270;        //Orientierung der y-Achsen-Beschriftung
            Chart1.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);   //Schriftart der y-Achsen-Beschriftung
            Chart1.AxisY.LabelStyle.Format = "";                              //Achsenbeschriftungsformat
            Chart1.AxisY.LabelStyle.IsEndLabelVisible = true;                 //true: erster u. letzter Wert der Achsenbeschriftung werden angezeigt

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

        public double[] ActualPosition(ref Chart chartname, MouseEventArgs e)
        {
            //gibt die x- u. y-Werte der aktuellen Mausposition zurück

            string GewDaten = PlotCopy.GiveChosenData();   //gibt gewählte Datenreihe zurück
            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen
            if (Chart1.AxisX.PixelPositionToValue(e.X) >= 0)   //iefert nur Werte, wenn Maus innerhalb des Charts
            {
                var xv = Chart1.AxisX.PixelPositionToValue(e.X);   //liefert aktuelle Mausposition
                Series S = chartname.Series[GewDaten];
                DataPoint pNext = S.Points.Select(x => x).Where(x => x.XValue <= xv).DefaultIfEmpty(S.Points.Last()).Last();
                //liefert nächstgelegenen Datenpunkt zu aktueller Mausposition

                xp = pNext.XValue.ToString();                      //liefert aktuellen x-Wert
                yp = Math.Round(pNext.YValues[0], 2).ToString();   //liefert aktuellen y-Wert gerundet
            }

            double Xpos = Convert.ToDouble(xp);   //Konvertierung x-Wert in Double
            double Ypos = Convert.ToDouble(yp);   //Konvertierung y-Wert in Double

            double[] Cpos = new double[] { Xpos, Ypos };   //erstellt DoubleArray mit x- u. y-Wert

            return Cpos;   //liefert DoubleArray zurück
        }
    }
}
