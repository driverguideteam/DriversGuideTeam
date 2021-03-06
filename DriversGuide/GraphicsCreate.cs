﻿using System;
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
        DriversGuideApp Form1Copy;           //Zugriff auf Hauptform
        PlotGraphic PlotCopy;                //Zugriff auf PlotCopy
        DataTable tt = new DataTable();      //Erstellung neues Datatable
        public string xp;                    //Variable für x-Position
        public string yp;                    //Variable für y-Position

        public void ConnectToForm1(DriversGuideApp CreateForm)   //Verbindung mit Hauptform
        {
            Form1Copy = CreateForm;                  //Zugriff auf Hauptform
            tt = Form1Copy.GetCompleteDataTable();   //Holen des Datatables
            tt = tt.Copy();

            for (int i = 0; i < tt.Rows.Count; i++)   //Umrechnung Zeit in sec
            {
                tt.Rows[i]["Time"] = Convert.ToInt64((tt.Rows[i]["Time"])) / 1000;
            }
        }

        public void GetChosenData(PlotGraphic ConnectForm)
        {
            PlotCopy = ConnectForm;   //Zugriff auf Datenauswahlform
        }

        public void SetChartProperties(ref Chart chartname, DataTable tt, string xGewDat, string GewDaten, PlotGraphic plc)
        {
            //Einstellen der Diagrammeigenschaften:

            chartname.Show();                          //Anzeigen der Grafik
            chartname.Series.Clear();                  //Löschen evtl. bestehender Datenpunktreihe
            chartname.ChartAreas.Clear();              //Löschen evtl. bestehender Grafikoberflächen
            chartname.Series.Add(GewDaten);            //Hinzufügen einer neuen Datenpunktreihe
            chartname.ChartAreas.Add(GewDaten);        //Hinzufügen einer neuen Grafikoberfläche
            
            for (int i = 0; i < tt.Rows.Count; i+=4)   //füllen Datenpunke-Serie
            {
                chartname.Series[GewDaten].Points.AddXY(Convert.ToDouble(tt.Rows[i][xGewDat]), Convert.ToDouble(tt.Rows[i][GewDaten]));
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }

            chartname.Series[GewDaten].ChartType = SeriesChartType.Spline;   //Festlegen des Diagrammtypes (hier Smooth-Line)

            var Chart1 = chartname.ChartAreas[GewDaten];         //dient nur der Verkürzung folgender Programmzeilen
            Chart1.CursorX.IsUserEnabled = true;                 //aktiviert Cursor
            Chart1.CursorX.IsUserSelectionEnabled = true;        //aktiviert Bereichsauswahl
            Chart1.CursorX.SelectionColor = Color.LightGray;     //Farbe Bereichsauswahl
            Chart1.CursorX.Interval = 1;                         //Intervall des Cursors
            Chart1.AxisX.ScaleView.Zoomable = true;              //Aktivierung Zoom-Möglichkeit
            Chart1.CursorX.LineColor = Color.DarkOrange;         //Linienfarbe Cursor
            Chart1.CursorX.LineWidth = 1;                        //Liniendicke Cursor
            Chart1.CursorX.LineDashStyle = ChartDashStyle.Solid; //Linienart Cursor

            Chart1.CursorY.IsUserEnabled = true;                 //aktiviert Cursor
            Chart1.CursorY.IsUserSelectionEnabled = true;        //aktiviert Bereichsauswahl
            Chart1.CursorY.SelectionColor = Color.LightGray;     //Farbe Bereichsauswahl
            Chart1.CursorY.Interval = 1;                         //Intervall des Cursors
            Chart1.AxisY.ScaleView.Zoomable = true;              //Aktivierung Zoom-Möglichkeit
            Chart1.CursorY.LineColor = Color.DarkOrange;         //Linienfarbe Cursor
            Chart1.CursorY.LineWidth = 1;                        //Liniendicke Cursor
            Chart1.CursorY.LineDashStyle = ChartDashStyle.Solid; //Linienart Cursor

            Chart1.AxisX.Minimum = Convert.ToInt64(tt.Rows[0][xGewDat]);                   //Festlegung x-Achsen-Minimum
            Chart1.AxisX.Maximum = Convert.ToInt64(tt.Rows[tt.Rows.Count - 1][xGewDat]);   //Festlegung x-Achsen-Maximum
            Chart1.AxisX.Interval = 1000;                                                  //Festlegung x-Achsen-Intervall

            chartname.Invalidate();
        }

        public void SetChartAxes(ref Chart chartname, DataTable tt, string xGewDat, string GewDaten, PlotGraphic plc)
        {            
            //Festlegen Achseneinstellungen:

            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen

            string xUnit = plc.GetUnits(GewDaten)[0];   //liefert Einheit der x-Achse
            string yUnit = plc.GetUnits(GewDaten)[1];   //liefert Einheit der y-Achse

            //chartname.Titles.Add("Test").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
            Chart1.AxisX.Title = xGewDat + " in " + xUnit;                     //Beschriftung der x-Achse
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

        public double ActualXPosition(ref Chart chartname, MouseEventArgs e, string GewDaten)
        {
            //gibt den x-Wert der aktuellen Mausposition zurück

            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen
            try
            {
                if (Chart1.AxisX.PixelPositionToValue(e.X) >= 0)       //iefert nur Werte, wenn Maus innerhalb des Charts
                {
                    var xv = Chart1.AxisX.PixelPositionToValue(e.X);   //liefert aktuelle Mausposition
                    Series S = chartname.Series[GewDaten];
                    DataPoint pNext = S.Points.Select(x => x).Where(x => x.XValue <= xv).DefaultIfEmpty(S.Points.Last()).Last();
                    //liefert nächstgelegenen Datenpunkt zu aktueller Mausposition

                    xp = pNext.XValue.ToString();                      //liefert aktuellen x-Wert
                    yp = Math.Round(pNext.YValues[0], 2).ToString();   //liefert aktuellen y-Wert gerundet
                }
            }
            catch { }

            double Xpos = Convert.ToDouble(xp);     //Konvertierung x-Wert in Double
            //double Ypos = Convert.ToDouble(yp);   //Konvertierung y-Wert in Double

            //double[] Cpos = new double[] { Xpos, Ypos };   //erstellt DoubleArray mit x- u. y-Wert

            return Xpos;   //liefert aktuellen x-Wert (in Double)
        }

        public double FindYValue(Chart chartname, double xp, string GewDaten)
        {
            //gibt y-Wert zu gegebeneb x-Wert einer gesuchten Datenreihe zurück
            Series S = chartname.Series[GewDaten];
            DataPoint pNext = S.Points.Select(x => x).Where(x => x.XValue <= xp).DefaultIfEmpty(S.Points.Last()).Last();

            yp = Math.Round(pNext.YValues[0], 2).ToString();   //liefert aktuellen y-Wert gerundet
            double YNpos = Convert.ToDouble(yp);               //Konvertierung y-Wert in Double
            return YNpos;                                      //liefert Double-Wert zurück
        }
    }
}
