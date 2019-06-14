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

            string xUnit = Convert.ToString((units.Rows[0][xGewDat]));   //liefert Einheit der x-Achse
            string yUnit = Convert.ToString((units.Rows[0][GewDaten]));   //liefert Einheit der y-Achse

            string[] xyUnits = new string[] { xUnit, yUnit };   //erstellt StringArray mit x- u. y-Einheiten

            return xyUnits;   //liefert StingArray zurück
        }

        private void Dynamic_Load(object sender, EventArgs e)
        {
            this.ChUrb.MouseWheel += ChUrb_MouseWheel;   //Erstellen MouseWheel-Ereignis
            this.ChRur.MouseWheel += ChRur_MouseWheel;   //Erstellen MouseWheel-Ereignis
            this.ChMw.MouseWheel += ChMw_MouseWheel;   //Erstellen MouseWheel-Ereignis

            lblUrb.Visible = false;
            lblRur.Visible = false;
            lblMw.Visible = false;

            if (live == false)
            {
                DataTable dturb = MainForm.GetUrbanDataTable();
                DataTable dtrur = MainForm.GetRuralDataTable();
                DataTable dtmw = MainForm.GetMotorwayDataTable();
                DataTable units = MainForm.GetUnitsDataTable();

                double purb = 0;   //Perzentilwert Stadt
                double prur = 0;   //Perzentilwert Land
                double pmw = 0;    //Perzentilwert Autobahn

                MainForm.GetPercentiles(ref purb, ref prur, ref pmw);   //Zuweisung Perzentilwerte

                double borderUrb = 0;
                double borderRur = 0;
                double borderMw = 0;

                MainForm.GetPercentileBorders(ref borderUrb, ref borderRur, ref borderMw);

                DrawDynamic(dturb, dtrur, dtmw, units, purb, prur, pmw, borderUrb, borderRur, borderMw);
            }

        }

        private void DrawDynamic(DataTable dturb, DataTable dtrur, DataTable dtmw, DataTable units, double purb, double prur, double pmw, double borderUrb, double borderRur, double borderMw)
        {
            newchart.SetChartProperties(ref ChUrb, dturb, "Perzentil", "a*v", PlotCopy);
            ChUrb.Titles.Add("Dynamik Stadt").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
            newchart.SetChartProperties(ref ChRur, dtrur, "Perzentil", "a*v", PlotCopy);
            ChRur.Titles.Add("Dynamik Freiland").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
            newchart.SetChartProperties(ref ChMw, dtmw, "Perzentil", "a*v", PlotCopy);
            ChMw.Titles.Add("Dynamik Autobahn").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title

            double urbmax = Convert.ToDouble(dturb.Rows[dturb.Rows.Count - 1]["a*v"]);   //Max-Wert a*v Stadt
            double rurmax = Convert.ToDouble(dtrur.Rows[dtrur.Rows.Count - 1]["a*v"]);   //Max-Wert a*v Land
            double mwmax = Convert.ToDouble(dtmw.Rows[dtmw.Rows.Count - 1]["a*v"]);      //Max-Wert a*v Autobahn

            double ymax = 5 * (int)Math.Round((Math.Max(urbmax, Math.Max(rurmax, mwmax) + 3) / 5.0));  //Max-Wert a*v auf nächste 5 aufgerundet

            SetChAxes(ref ChUrb, dturb, "Perzentil", "a*v", ymax);
            SetChAxes(ref ChRur, dtrur, "Perzentil", "a*v", ymax);
            SetChAxes(ref ChMw, dtmw, "Perzentil", "a*v", ymax);

            Draw95PLine(ChUrb, ymax, purb);             //Zeichnen der 95%-Linie
            Draw95PLine(ChRur, ymax, prur);             //Zeichnen der 95%-Linie
            Draw95PLine(ChMw, ymax, pmw);               //Zeichnen der 95%-Linie
            DrawPerzentilValue(ChUrb, ymax, purb);      //Zeichnen des Perzentilwertes bei 95 %
            DrawPerzentilValue(ChRur, ymax, prur);      //Zeichnen des Perzentilwertes bei 95 %
            DrawPerzentilValue(ChMw, ymax, pmw);        //Zeichnen des Perzentilwertes bei 95 %
            DrawDynamicLimit(ChUrb, ymax, borderUrb);   //Zeichnen des Perzentilgrenzwertes
            DrawDynamicLimit(ChRur, ymax, borderRur);   //Zeichnen des Perzentilgrenzwertes
            DrawDynamicLimit(ChMw, ymax, borderMw);     //Zeichnen des Perzentilgrenzwertes


            ChUrb.Series[0].IsVisibleInLegend = false;   //Ausblenden Chartseries-Name
            ChRur.Series[0].IsVisibleInLegend = false;   //Ausblenden Chartseries-Name
            ChMw.Series[0].IsVisibleInLegend = false;    //Ausblenden Chartseries-Name
            DeactSV(ChUrb, "a*v");  //Deaktiviert Bereichsauswahl in Chart
            DeactSV(ChRur, "a*v");  //Deaktiviert Bereichsauswahl in Chart
            DeactSV(ChMw, "a*v");  //Deaktiviert Bereichsauswahl in Chart
        }

        private void DeactSV(Chart chartname, string GewDaten)  //Deaktiviert Bereichsauswahl in Chart
        {
            chartname.ChartAreas[GewDaten].AxisX.ScaleView.Zoomable = false;
            chartname.ChartAreas[GewDaten].AxisY.ScaleView.Zoomable = false;
        }

        private void SetChAxes(ref Chart chartname, DataTable tt, string xGewDat, string GewDaten, double ymax)
        {
            DataTable units = MainForm.GetUnitsDataTable();
            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen

            string xUnit = GetUnits(xGewDat, GewDaten)[0];   //liefert Einheit der x-Achse
            string yUnit = GetUnits(xGewDat, GewDaten)[1];   //liefert Einheit der y-Achse

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

            Chart1.AxisX.Minimum = Convert.ToInt64(tt.Rows[0][xGewDat]);                 //Festlegung x-Achsen-Minimum
            Chart1.AxisX.Maximum = Convert.ToInt64(tt.Rows[tt.Rows.Count - 1][xGewDat]);   //Festlegung x-Achsen-Maximum
            Chart1.AxisX.Interval = 20;                                                  //Festlegung x-Achsen-Intervall

            Chart1.AxisY.Minimum = 0;                       //Festlegung y-Achsen-Minimum
            Chart1.AxisY.Maximum = Convert.ToInt64(ymax);   //Festlegung y-Achsen-Maximum
            Chart1.AxisY.Interval = 5;                      //Festlegung x-Achsen-Intervall

        }

        private void Dynamic_SizeChanged(object sender, EventArgs e)
        {
            ChUrb.Location = new Point(0, 0);   //Positionierung u. Größeneinstellung Chart
            ChUrb.Width = this.Width / 3;
            ChUrb.Height = this.Height;
            ChRur.Location = new Point(ChUrb.Width, 0);   //Positionierung u. Größeneinstellung Chart
            ChRur.Width = this.Width / 3;
            ChRur.Height = this.Height;
            ChMw.Location = new Point(ChUrb.Width + ChRur.Width, 0);   //Positionierung u. Größeneinstellung Chart
            ChMw.Width = this.Width / 3;
            ChMw.Height = this.Height;

            lblUrb.Location = new Point(ChUrb.Location.X + ChUrb.Width / 4 * 3, ChUrb.Height - 40);   //Positionierung Label
            lblRur.Location = new Point(ChRur.Location.X + ChRur.Width / 4 * 3, ChRur.Height - 40);   //Positionierung Label
            lblMw.Location = new Point(ChMw.Location.X + ChMw.Width / 4 * 3, ChMw.Height - 40);      //Positionierung Label
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
            labelname.Text = "x = " + Math.Round(xpos, 1).ToString() + " " + xunit + "\n" +
                            "y = " + ypos.ToString() + " " + yunit;

            //lblPos.BackColor = Color.White;
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
            chartname.Series["95pLbl"].Points.AddXY(92, 2);
            chartname.Series["95pLbl"].Color = Color.White;
            //chartname.Series["95pLbl"].AxisLabel = "Test";
            chartname.Series["95pLbl"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["95pLbl"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name
            chartname.Series["95pLbl"].Label = "95 %";
            chartname.Series["95pLbl"].LabelForeColor = Color.Red;
            chartname.Series["95pLbl"].LabelAngle = -90;
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
            chartname.Invalidate();

            chartname.Series.Add("PerzVLbl");                                  //Hinzufügen Punkt für Perzentilwert Label
            chartname.Series["PerzVLbl"].Points.AddXY(0, perzval + 1);
            chartname.Series["PerzVLbl"].Color = Color.White;
            //chartname.Series["95pLbl"].AxisLabel = "Test";
            chartname.Series["PerzVLbl"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["PerzVLbl"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name
            chartname.Series["PerzVLbl"].Label = Math.Round(perzval, 1).ToString() + " m²/s³";
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
            chartname.Invalidate();

            chartname.Series.Add("DynLimLbl");                                  //Hinzufügen Punkt für Perzentilwert Label
            chartname.Series["DynLimLbl"].Points.AddXY(0, dynborder + 1);
            chartname.Series["DynLimLbl"].Color = Color.White;
            //chartname.Series["DynLimLbl"].AxisLabel = "Test";
            chartname.Series["DynLimLbl"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["DynLimLbl"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name
            chartname.Series["DynLimLbl"].Label = Math.Round(dynborder, 1).ToString() + " m²/s³";
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

        private void ChUrb_MouseMove(object sender, MouseEventArgs e)
        {
            DataTable dturb = MainForm.GetUrbanDataTable();
            DataTable units = MainForm.GetUnitsDataTable();
            string xUnit = GetUnits("Perzentil", "a*v")[0];
            string yUnit = GetUnits("Perzentil", "a*v")[1];
            MoveCursor(ChUrb, lblUrb, e, dturb, "Perzentil", "a*v", xUnit, yUnit);
            lblUrb.Visible = true;
        }

        private void ChRur_MouseMove(object sender, MouseEventArgs e)
        {
            DataTable dtrur = MainForm.GetRuralDataTable();
            DataTable units = MainForm.GetUnitsDataTable();
            string xUnit = GetUnits("Perzentil", "a*v")[0];
            string yUnit = GetUnits("Perzentil", "a*v")[1];
            MoveCursor(ChRur, lblRur, e, dtrur, "Perzentil", "a*v", xUnit, yUnit);
            lblRur.Visible = true;
        }

        private void ChMw_MouseMove(object sender, MouseEventArgs e)
        {
            DataTable dtmw = MainForm.GetMotorwayDataTable();
            DataTable units = MainForm.GetUnitsDataTable();
            string xUnit = GetUnits("Perzentil", "a*v")[0];
            string yUnit = GetUnits("Perzentil", "a*v")[1];
            MoveCursor(ChMw, lblMw, e, dtmw, "Perzentil", "a*v", xUnit, yUnit);
            lblMw.Visible = true;
        }

        private void SetStartScale(Chart chartname, string GewDatenX)
        {
            chartname.ChartAreas[GewDatenX].AxisX.ScaleView.Zoom(chartname.ChartAreas[GewDatenX].AxisX.Minimum, chartname.ChartAreas[GewDatenX].AxisX.Maximum);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            chartname.ChartAreas[GewDatenX].AxisY.ScaleView.Zoom(chartname.ChartAreas[GewDatenX].AxisY.Minimum, chartname.ChartAreas[GewDatenX].AxisY.Maximum);   //Startskalierung der y-Achse
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

        //private void DrawLive(object sender, EventArgs e)
        //{
        //    lblUrb.Visible = false;
        //    lblRur.Visible = false;
        //    lblMw.Visible = false;

        //    DataTable dturb = FormLive.GetUrbanDataTable();
        //    DataTable dtrur = FormLive.GetRuralDataTable();
        //    DataTable dtmw = FormLive.GetMotorwayDataTable();
        //    DataTable units = FormLive.GetUnitsDataTable();

        //    newchart.SetChartProperties(ref ChUrb, dturb, "Perzentil", "a*v", PlotCopy);
        //    ChUrb.Titles.Add("Dynamik Stadt").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
        //    newchart.SetChartProperties(ref ChRur, dtrur, "Perzentil", "a*v", PlotCopy);
        //    ChRur.Titles.Add("Dynamik Freiland").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
        //    newchart.SetChartProperties(ref ChMw, dtmw, "Perzentil", "a*v", PlotCopy);
        //    ChMw.Titles.Add("Dynamik Autobahn").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title

        //    double urbmax = Convert.ToDouble(dturb.Rows[dturb.Rows.Count - 1]["a*v"]);   //Max-Wert a*v Stadt
        //    double rurmax = Convert.ToDouble(dtrur.Rows[dtrur.Rows.Count - 1]["a*v"]);   //Max-Wert a*v Land
        //    double mwmax = Convert.ToDouble(dtmw.Rows[dtmw.Rows.Count - 1]["a*v"]);      //Max-Wert a*v Autobahn

        //    double ymax = 5 * (int)Math.Round((Math.Max(urbmax, Math.Max(rurmax, mwmax) + 3) / 5.0));  //Max-Wert a*v auf nächste 5 aufgerundet

        //    SetChAxes(ref ChUrb, dturb, "Perzentil", "a*v", ymax);
        //    SetChAxes(ref ChRur, dtrur, "Perzentil", "a*v", ymax);
        //    SetChAxes(ref ChMw, dtmw, "Perzentil", "a*v", ymax);

        //    double purb = 0;   //Perzentilwert Stadt
        //    double prur = 0;   //Perzentilwert Land
        //    double pmw = 0;    //Perzentilwert Autobahn

        //    FormLive.GetPercentiles(ref purb, ref prur, ref pmw);   //Zuweisung Perzentilwerte

        //    double borderUrb = 0;
        //    double borderRur = 0;
        //    double borderMw = 0;

        //    FormLive.GetPercentileBorders(ref borderUrb, ref borderRur, ref borderMw);

        //    Draw95PLine(ChUrb, ymax, purb);             //Zeichnen der 95%-Linie
        //    Draw95PLine(ChRur, ymax, prur);             //Zeichnen der 95%-Linie
        //    Draw95PLine(ChMw, ymax, pmw);               //Zeichnen der 95%-Linie
        //    DrawPerzentilValue(ChUrb, ymax, purb);      //Zeichnen des Perzentilwertes bei 95 %
        //    DrawPerzentilValue(ChRur, ymax, prur);      //Zeichnen des Perzentilwertes bei 95 %
        //    DrawPerzentilValue(ChMw, ymax, pmw);        //Zeichnen des Perzentilwertes bei 95 %
        //    DrawDynamicLimit(ChUrb, ymax, borderUrb);   //Zeichnen des Perzentilgrenzwertes
        //    DrawDynamicLimit(ChRur, ymax, borderRur);   //Zeichnen des Perzentilgrenzwertes
        //    DrawDynamicLimit(ChMw, ymax, borderMw);     //Zeichnen des Perzentilgrenzwertes


        //    ChUrb.Series[0].IsVisibleInLegend = false;   //Ausblenden Chartseries-Name
        //    ChRur.Series[0].IsVisibleInLegend = false;   //Ausblenden Chartseries-Name
        //    ChMw.Series[0].IsVisibleInLegend = false;    //Ausblenden Chartseries-Name
        //    DeactSV(ChUrb, "a*v");  //Deaktiviert Bereichsauswahl in Chart
        //    DeactSV(ChRur, "a*v");  //Deaktiviert Bereichsauswahl in Chart
        //    DeactSV(ChMw, "a*v");  //Deaktiviert Bereichsauswahl in Chart
        //}
    }
}

