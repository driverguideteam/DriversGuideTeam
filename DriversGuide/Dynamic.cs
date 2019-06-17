using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DriversGuide
{
    public partial class Dynamic : UserControl
    {
        private LiveMode FormLive;
        private DriversGuideApp MainForm;
        GraphicsCreate newchart = new GraphicsCreate();   //neue Grafik erstellen, Eigenschaften in GraphicsCreate festgelegt
        PlotGraphic PlotCopy;
        bool live = false;
        bool liveRun = false;
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

        DataTable dturb;
        DataTable dtrur;
        DataTable dtmw;
        DataTable units;


        public Dynamic(LiveMode caller, bool liveRunning)
        {
            FormLive = caller;
            InitializeComponent();
            live = true;
            if (liveRunning)
            {
                liveRun = true;

            }
            else
            {
                //???
            }
        }

        public Dynamic(DriversGuideApp caller2)
        {
            MainForm = caller2;
            InitializeComponent();
            live = false;
        }


        public void GetChosenData(PlotGraphic ConnectForm)
        {
            PlotCopy = ConnectForm;   //Zugriff auf Datenauswahlform
        }

        public string[] GetUnits(string xGewDat, string GewDaten)   //liefert StringArray mit x- u. y-Einheiten
        {
            DataTable units = MainForm.GetUnitsDataTable();

            string xUnit = Convert.ToString((units.Rows[0][xGewDat]));    //liefert Einheit der x-Achse
            string yUnit = Convert.ToString((units.Rows[0][GewDaten]));   //liefert Einheit der y-Achse

            string[] xyUnits = new string[] { xUnit, yUnit };   //erstellt StringArray mit x- u. y-Einheiten

            return xyUnits;   //liefert StingArray zurück
        }

        private void Dynamic_Load(object sender, EventArgs e)
        {
            lblUrb.Visible = false;
            lblRur.Visible = false;
            lblMw.Visible = false;

            LocateCharts(ChUrb, ChRur, ChMw);   //Positionierung der charts
            LocateLabels(lblUrb, lblRur, lblMw, ChUrb, ChRur, ChMw);   //Positionierung der labels

            if (live == false)
            {
                this.ChUrb.MouseWheel += ChUrb_MouseWheel;   //Erstellen MouseWheel-Ereignis
                this.ChRur.MouseWheel += ChRur_MouseWheel;   //Erstellen MouseWheel-Ereignis
                this.ChMw.MouseWheel += ChMw_MouseWheel;     //Erstellen MouseWheel-Ereignis

                dturb = MainForm.GetUrbanDataTable();
                dtrur = MainForm.GetRuralDataTable();
                dtmw = MainForm.GetMotorwayDataTable();
                units = MainForm.GetUnitsDataTable();

                MainForm.GetPercentiles(ref purb, ref prur, ref pmw);                        //Zuweisung Perzentilwerte
                MainForm.GetPercentileBorders(ref borderUrb, ref borderRur, ref borderMw);   //Zuweisung Perzentilgrenzwerte

                DrawCharts(dturb, dtrur, dtmw, purb, prur, pmw, borderUrb, borderRur, borderMw);
                FillCharts(dturb, dtrur, dtmw, ymax, purb, prur, pmw, borderUrb, borderRur, borderMw);
                EnableCursor(ref ChUrb, "a*v");
                EnableCursor(ref ChRur, "a*v");
                EnableCursor(ref ChMw, "a*v");
            }
            if (live == true)
            {
                dturb = FormLive.GetUrbanDataTable();
                dtrur = FormLive.GetRuralDataTable();
                dtmw = FormLive.GetMotorwayDataTable();
                //units = MainForm.GetUnitsDataTable();

                FormLive.GetPercentiles(ref purb, ref prur, ref pmw);                        //Zuweisung Perzentilwerte
                FormLive.GetPercentileBorders(ref borderUrb, ref borderRur, ref borderMw);   //Zuweisung Perzentilgrenzwerte

                DrawCharts(dturb, dtrur, dtmw, purb, prur, pmw, borderUrb, borderRur, borderMw);
            }
        }

        private void DrawCharts(DataTable dturb, DataTable dtrur, DataTable dtmw, double purb, double prur, double pmw, double borderUrb, double borderRur, double borderMw)
        {   //Setzt die Grundeinstellungen der Charts, ohne Daten einzuzeichnen

            if (dturb.Rows.Count != 0)
                urbmax = Convert.ToDouble(dturb.Rows[dturb.Rows.Count - 1]["a*v"]);   //Max-Wert a*v Stadt
            if (dtrur.Rows.Count != 0)
                rurmax = Convert.ToDouble(dtrur.Rows[dtrur.Rows.Count - 1]["a*v"]);   //Max-Wert a*v Land
            if (dtmw.Rows.Count != 0)
                mwmax = Convert.ToDouble(dtmw.Rows[dtmw.Rows.Count - 1]["a*v"]);      //Max-Wert a*v Autobahn

            ymax = 5 * (int)Math.Round((Math.Max(urbmax, Math.Max(rurmax, mwmax) + 3) / 5.0));  //Max-Wert a*v generell, auf nächste 5 aufgerundet

            ChUrb.Titles.Add("Dynamik Stadt").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
            SetChartProperties(ref ChUrb, dturb, "Perzentil", "a*v", ymax);

            ChRur.Titles.Add("Dynamik Freiland").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
            SetChartProperties(ref ChRur, dtrur, "Perzentil", "a*v", ymax);

            ChMw.Titles.Add("Dynamik Autobahn").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
            SetChartProperties(ref ChMw, dtmw, "Perzentil", "a*v", ymax);
        }

        public void ClearAndSetChartsLive(double ymax)
        {
            SetChartProperties(ref ChUrb, dturb, "Perzentil", "a*v", ymax);
            SetChartProperties(ref ChRur, dtrur, "Perzentil", "a*v", ymax);
            SetChartProperties(ref ChMw, dtmw, "Perzentil", "a*v", ymax);
        }

        private void SetChartProperties(ref Chart chartname, DataTable tt, string xGewDat, string GewDaten, double ymax)
        {   //Löschen bestehender und hinzufügen neuer Datenpunktreihe u. Grafikoberfläche
            chartname.Show();   //Anzeigen der Grafik

            chartname.Series.Clear();       //Löschen evtl. bestehender Datenpunktreihe
            chartname.ChartAreas.Clear();   //Löschen evtl. bestehender Grafikoberflächen

            chartname.Series.Add(GewDaten);       //Hinzufügen einer neuen Datenpunktreihe
            chartname.ChartAreas.Add(GewDaten);   //Hinzufügen einer neuen Grafikoberfläche 

            chartname.Series[GewDaten].IsVisibleInLegend = false;   //Ausblenden legende

            string xUnit = "";
            string yUnit = "";

            //Festelegung Einheiten:
            if (live == false)
            {
                DataTable units = MainForm.GetUnitsDataTable();
                xUnit = GetUnits(xGewDat, GewDaten)[0];   //liefert Einheit der x-Achse
                yUnit = GetUnits(xGewDat, GewDaten)[1];   //liefert Einheit der y-Achse
            }
            if (live == true)
            {
                xUnit = "%";
                yUnit = "m²/s³";
            }

            //Achseneistellungen festlegen:
            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen
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

            Chart1.AxisX.Minimum = 0;                       //Festlegung y-Achsen-Minimum
            Chart1.AxisX.Maximum = 100;   //Festlegung y-Achsen-Maximum
            Chart1.AxisX.Interval = 20;                                      //Festlegung x-Achsen-Intervall

            Chart1.AxisY.Minimum = 0;                       //Festlegung y-Achsen-Minimum
            Chart1.AxisY.Maximum = Convert.ToInt64(ymax);   //Festlegung y-Achsen-Maximum
            Chart1.AxisY.Interval = 5;                      //Festlegung x-Achsen-Intervall
        }

        public void FillCharts(DataTable dturb, DataTable dtrur, DataTable dtmw, double ymax, double purb, double prur, double pmw, double borderUrb, double borderRur, double borderMw)
        {   //Füllen der drei Charts, je nachdem, wie die Datatables aus Live verfügbar sind (leer od. nicht)
            if (dturb.Rows.Count != 0)   //Prüfung, ob Datatable leer ist
            {
                Draw95PLine(ChUrb, ymax, purb);             //Zeichnen der 95%-Linie
                DrawPerzentilValue(ChUrb, ymax, purb);      //Zeichnen des Perzentilwertes bei 95 %
                DrawDynamicLimit(ChUrb, ymax, borderUrb);   //Zeichnen des Perzentilgrenzwertes
                AddDataPoints(dturb, ref ChUrb, "Perzentil", "a*v");

                if (dtrur.Rows.Count == 0)
                {
                    ChRur.Series["a*v"].Points.AddXY(0, 0);
                }
                if (dtmw.Rows.Count == 0)
                {
                    ChMw.Series["a*v"].Points.AddXY(0, 0);
                }
            }
            if (dtrur.Rows.Count != 0)   //Prüfung, ob Datatable leer ist
            {
                Draw95PLine(ChRur, ymax, prur);             //Zeichnen der 95%-Linie
                DrawPerzentilValue(ChRur, ymax, prur);      //Zeichnen des Perzentilwertes bei 95 %
                DrawDynamicLimit(ChRur, ymax, borderRur);   //Zeichnen des Perzentilgrenzwertes
                AddDataPoints(dtrur, ref ChRur, "Perzentil", "a*v");

                if (dtmw.Rows.Count == 0)
                {
                    ChMw.Series["a*v"].Points.AddXY(0, 0);
                }
            }
            if (dtmw.Rows.Count != 0)   //Prüfung, ob Datatable leer ist
            {
                Draw95PLine(ChMw, ymax, pmw);               //Zeichnen der 95%-Linie
                DrawPerzentilValue(ChMw, ymax, pmw);        //Zeichnen des Perzentilwertes bei 95 %
                DrawDynamicLimit(ChMw, ymax, borderMw);     //Zeichnen des Perzentilgrenzwertes
                AddDataPoints(dtmw, ref ChMw, "Perzentil", "a*v");
            }
        }

        public void AddDataPoints(DataTable tt, ref Chart chartname, string xGewDat, string GewDaten)
        {   //Hinzufügen der Datenreihe, die gezeichnet wird:
            for (int i = 0; i < tt.Rows.Count; i += 4)   //füllen Datenpunke-Serie
            {
                chartname.Series[GewDaten].Points.AddXY(Convert.ToDouble(tt.Rows[i][xGewDat]), Convert.ToDouble(tt.Rows[i][GewDaten]));
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }
            chartname.Series[GewDaten].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line)
            chartname.Invalidate();
        }


        private void Draw95PLine(Chart chartname, double ymax, double perzval)   //Zeichnen der 95%-Linie
        {
            chartname.Series.Add("95PLine");       //Hinzufügen der 95%-Linie
            for (int i = 0; i <= ymax; i += 1)   //füllen Datenpunke-Serie
            {
                chartname.Series["95PLine"].Points.AddXY(95, i);
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }
            chartname.Series["95PLine"].Color = Color.Red;
            //chartname.Series["95PLine"].AxisLabel = "Test";
            chartname.Series["95PLine"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line)
            chartname.Series["95PLine"].IsVisibleInLegend = false;   //Ausblenden Chartseries-Name

            chartname.Series.Add("95pLbl");                                  //Hinzufügen  Punkt für 95 % Label
            if (live == true)
            {
                chartname.Series["95pLbl"].Points.AddXY(88.5, 2);
                chartname.Series["95pLbl"].LabelAngle = -0;
            }
            if (live == false)
            {
                chartname.Series["95pLbl"].Points.AddXY(92, 2);
                chartname.Series["95pLbl"].LabelAngle = -90;
            }
            chartname.Series["95pLbl"].Color = Color.White;
            //chartname.Series["95pLbl"].AxisLabel = "Test";
            chartname.Series["95pLbl"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["95pLbl"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name
            chartname.Series["95pLbl"].Label = "95 %";
            chartname.Series["95pLbl"].LabelBackColor = Color.Transparent;
            chartname.Series["95pLbl"].LabelForeColor = Color.Red;
            chartname.Series["95pLbl"].SmartLabelStyle.Enabled = false;
            chartname.Series["95pLbl"].ChartType = SeriesChartType.Column;
            chartname.Series["95pLbl"]["LabelStyle"] = "Center";
            chartname.Series["95pLbl"]["PointWidth"] = "0.7";
            chartname.Series["95pLbl"].IsValueShownAsLabel = true;
            chartname.Series["95pLbl"].Font = new System.Drawing.Font("Arial", 10);
            chartname.Series["95pLbl"]["LabelStyle"] = "Center";

            chartname.Invalidate();
        }

        private void DrawPerzentilValue(Chart chartname, double ymax, double perzval)   //Zeichnen des Perzentilwertes bei 95 %
        {
            chartname.Series.Add("PerzVal");       //Hinzufügen des Perzentilwertes
            for (int i = 0; i <= 95; i += 1)   //füllen Datenpunke-Serie
            {
                chartname.Series["PerzVal"].Points.AddXY(i, perzval);
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }
            chartname.Series["PerzVal"].Color = Color.Red;
            //chartname.Series["PerzVal"].AxisLabel = "Test";
            chartname.Series["PerzVal"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["PerzVal"].IsVisibleInLegend = false;   //Ausblenden Chartseries-Name

            chartname.Series.Add("PerzVLbl");                                  //Hinzufügen Punkt für Perzentilwert Label
            chartname.Series["PerzVLbl"].Points.AddXY(5, perzval);
            chartname.Series["PerzVLbl"].Color = Color.White;
            //chartname.Series["95pLbl"].AxisLabel = "Test";
            chartname.Series["PerzVLbl"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["PerzVLbl"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name
            chartname.Series["PerzVLbl"].Label = Math.Round(perzval, 1).ToString("F1") + " m²/s³";
            chartname.Series["PerzVLbl"].LabelForeColor = Color.Red;
            chartname.Series["PerzVLbl"].LabelBackColor = Color.White;
            chartname.Series["PerzVLbl"].LabelAngle = 0;
            chartname.Series["PerzVLbl"].SmartLabelStyle.Enabled = false;
            chartname.Series["PerzVLbl"].ChartType = SeriesChartType.Column;
            chartname.Series["PerzVLbl"]["LabelStyle"] = "Center";
            chartname.Series["PerzVLbl"]["PointWidth"] = "0.7";
            chartname.Series["PerzVLbl"].IsValueShownAsLabel = true;
            chartname.Series["PerzVLbl"].Font = new System.Drawing.Font("Arial", 10);
            chartname.Series["PerzVLbl"]["LabelStyle"] = "Right";

            chartname.Invalidate();
        }

        private void DrawDynamicLimit(Chart chartname, double ymax, double dynborder)   //Zeichnen der 95%-Linie und des Perzentilwertes
        {
            chartname.Series.Add("DynLim");       //Hinzufügen des Perzentilwertes

            for (int i = 0; i <= 95; i += 1)   //füllen Datenpunke-Serie
            {
                chartname.Series["DynLim"].Points.AddXY(i, dynborder);
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }

            chartname.Series["DynLim"].Color = Color.Red;
            //chartname.Series["DynLim"].AxisLabel = "Test";
            chartname.Series["DynLim"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["DynLim"].IsVisibleInLegend = false;   //Ausblenden Chartseries-Name

            chartname.Series.Add("DynLimLbl");                                  //Hinzufügen Punkt für Perzentilwert Label
            chartname.Series["DynLimLbl"].Points.AddXY(5, dynborder);
            chartname.Series["DynLimLbl"].Color = Color.White;
            //chartname.Series["DynLimLbl"].AxisLabel = "Test";
            chartname.Series["DynLimLbl"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["DynLimLbl"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name
            chartname.Series["DynLimLbl"].Label = Math.Round(dynborder, 1).ToString("F1") + " m²/s³";
            chartname.Series["DynLimLbl"].LabelForeColor = Color.Red;
            chartname.Series["DynLimLbl"].LabelBackColor = Color.White;
            chartname.Series["DynLimLbl"].LabelAngle = 0;
            chartname.Series["DynLimLbl"].SmartLabelStyle.Enabled = false;
            chartname.Series["DynLimLbl"].ChartType = SeriesChartType.Column;
            chartname.Series["DynLimLbl"]["LabelStyle"] = "Center";
            chartname.Series["DynLimLbl"]["PointWidth"] = "0.7";
            chartname.Series["DynLimLbl"].IsValueShownAsLabel = true;
            chartname.Series["DynLimLbl"].Font = new System.Drawing.Font("Arial", 10);
            chartname.Series["DynLimLbl"]["LabelStyle"] = "Right";

            chartname.Invalidate();
        }

        private void EnableCursor(ref Chart chartname, string GewDaten)
        {   //Aktivierung und Einstellung Cursor
            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen

            Chart1.CursorX.IsUserEnabled = true;                 //aktiviert Cursor
            Chart1.CursorX.IsUserSelectionEnabled = true;        //aktiviert Bereichsauswahl
            Chart1.CursorX.SelectionColor = Color.LightGray;     //Farbe Bereichsauswahl
            Chart1.CursorX.Interval = 1;                         //Intervall des Cursors
            Chart1.AxisX.ScaleView.Zoomable = true;
            Chart1.CursorX.LineColor = Color.DarkOrange;         //Linienfarbe Cursor
            Chart1.CursorX.LineWidth = 1;                        //Liniendicke Cursor
            Chart1.CursorX.LineDashStyle = ChartDashStyle.Solid; //Linienart Cursor

            Chart1.CursorY.IsUserEnabled = true;                 //aktiviert Cursor
            Chart1.CursorY.IsUserSelectionEnabled = true;        //aktiviert Bereichsauswahl
            Chart1.CursorY.SelectionColor = Color.LightGray;     //Farbe Bereichsauswahl
            Chart1.CursorY.Interval = 1;                         //Intervall des Cursors
            Chart1.AxisY.ScaleView.Zoomable = true;
            Chart1.CursorY.LineColor = Color.DarkOrange;         //Linienfarbe Cursor
            Chart1.CursorY.LineWidth = 1;                        //Liniendicke Cursor
            Chart1.CursorY.LineDashStyle = ChartDashStyle.Solid; //Linienart Cursor
        }

        private void LocateCharts(Chart chartname1, Chart chartname2, Chart chartname3)
        {   //Positionierung der charts:
            chartname1.Location = new Point(0, 0);
            chartname1.Width = this.Width / 3;
            chartname1.Height = this.Height - 10;
            chartname2.Location = new Point(chartname1.Width, 0);
            chartname2.Width = this.Width / 3;
            chartname2.Height = this.Height - 10;
            chartname3.Location = new Point(chartname1.Width + chartname2.Width, 0);
            chartname3.Width = this.Width / 3;
            chartname3.Height = this.Height - 10;
        }

        private void LocateLabels(Label labelname1, Label labelname2, Label labelname3, Chart chartname1, Chart chartname2, Chart chartname3)
        {   //Positionierung der labels:
            labelname1.Location = new Point(chartname1.Location.X + (chartname1.Width / 2) - 60, chartname1.Height - 10);   //Positionierung Label
            labelname2.Location = new Point(chartname2.Location.X + (chartname2.Width / 2) - 60, chartname2.Height - 10);   //Positionierung Label
            labelname3.Location = new Point(chartname3.Location.X + (chartname3.Width / 2) - 60, chartname3.Height - 10);      //Positionierung Label
        }

        private void Dynamic_SizeChanged(object sender, EventArgs e)
        {
            LocateCharts(ChUrb, ChRur, ChMw);   //Positionierung der charts
            LocateLabels(lblUrb, lblRur, lblMw, ChUrb, ChRur, ChMw);   //Positionierung der labels
        }

        private void MoveCursor(Chart chartname, Label labelname, MouseEventArgs e, DataTable tt, string xGewDat, string GewDaten, string xunit, string yunit)
        {
            //bei Bewegen der Maus wird der Cursor auf die aktuelle Mausposition gestellt und 
            //die dazugehörigen x- u. y-Werte angezeigt

            //wird erst ausgeführt nachdem gewisse Zeit verstrichen ist, da ansonsten noch nicht alle
            //Datenpunkte in der Grafik gezeichnet wurden, was zu einer Fehlermeldung führt

            double xpos = newchart.ActualXPosition(ref chartname, e, GewDaten);
            double ypos = newchart.FindYValue(chartname, xpos, GewDaten);

            chartname.ChartAreas[GewDaten].CursorX.Position = xpos;
            chartname.ChartAreas[GewDaten].CursorY.Position = ypos;

            labelname.Visible = true;
            labelname.Text = "x = " + Math.Round(xpos, 1).ToString("F1") + " " + xunit + "    " +
                             "y = " + ypos.ToString("F2") + " " + yunit;

            //lblPos.BackColor = Color.White;
        }

        private void ChUrb_MouseMove(object sender, MouseEventArgs e)
        {
            if (live == false)
            {
                DataTable dturb = MainForm.GetUrbanDataTable();
                DataTable units = MainForm.GetUnitsDataTable();
                string xUnit = GetUnits("Perzentil", "a*v")[0];
                string yUnit = GetUnits("Perzentil", "a*v")[1];
                MoveCursor(ChUrb, lblUrb, e, dturb, "Perzentil", "a*v", xUnit, yUnit);
                lblUrb.Visible = true;
            }
        }

        private void ChRur_MouseMove(object sender, MouseEventArgs e)
        {
            if (live == false)
            {
                DataTable dtrur = MainForm.GetRuralDataTable();
                DataTable units = MainForm.GetUnitsDataTable();
                string xUnit = GetUnits("Perzentil", "a*v")[0];
                string yUnit = GetUnits("Perzentil", "a*v")[1];
                MoveCursor(ChRur, lblRur, e, dtrur, "Perzentil", "a*v", xUnit, yUnit);
                lblRur.Visible = true;
            }
        }

        private void ChMw_MouseMove(object sender, MouseEventArgs e)
        {
            if(live == false)
            {
                DataTable dtmw = MainForm.GetMotorwayDataTable();
                DataTable units = MainForm.GetUnitsDataTable();
                string xUnit = GetUnits("Perzentil", "a*v")[0];
                string yUnit = GetUnits("Perzentil", "a*v")[1];
                MoveCursor(ChMw, lblMw, e, dtmw, "Perzentil", "a*v", xUnit, yUnit);
                lblMw.Visible = true;
            }
        }

        private void SetStartScale(Chart chartname, string GewDatenX)
        {
            chartname.ChartAreas[GewDatenX].AxisY.ScaleView.ZoomReset(0);   //Startskalierung der y-Achse
            chartname.ChartAreas[GewDatenX].AxisX.ScaleView.ZoomReset(0);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems

        }

        private void ChUrb_MouseWheel(object sender, MouseEventArgs e)
        {
            SetStartScale(ChUrb, "a*v");
        }

        private void ChRur_MouseWheel(object sender, MouseEventArgs e)
        {
            SetStartScale(ChRur, "a*v");
        }

        private void ChMw_MouseWheel(object sender, MouseEventArgs e)
        {
            SetStartScale(ChMw, "a*v");
        }

        //private void DeactSV(Chart chartname, string GewDaten)  //Deaktiviert Bereichsauswahl in Chart
        //{
        //    chartname.ChartAreas[GewDaten].AxisX.ScaleView.Zoomable = false;
        //    chartname.ChartAreas[GewDaten].AxisY.ScaleView.Zoomable = false;
        //}
    }
}

