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
        bool live = false;            //Variable, die anzeigt, ob Live-Modus aktiv
        private bool topBottomSave;   //Variable für Position im Live-Fenster
        double urbmax = 0;            //Maximalwert a*v Stadt
        double rurmax = 0;            //Maximalwert a*v Freiland
        double mwmax = 0;             //Maximalwert a*v Autobahn
        double ymax = 0;              //Maximalwert a*v generell
        double bordermax = 0;         //Höchster Perzentilgrenzwert
        double purb = 0;              //Perzentilwert Stadt
        double prur = 0;              //Perzentilwert Freiland
        double pmw = 0;               //Perzentilwert Autobahn
        double borderUrb = 0;         //Perzentilgrenzwert Stadt
        double borderRur = 0;         //Perzentilgrenzwert Freiland
        double borderMw = 0;          //Perzentilgrenzwert Autobahn

        DataTable dturb;        //Datatable Stadt
        DataTable dtrur;        //Datatable Freiland
        DataTable dtmw;         //Datatable Autobahn
        DataTable units;        //Datatable Einheiten

        public Dynamic(LiveMode caller)   //Aufruf Dynamic-Form im Live-Modus
        {
            FormLive = caller;
            InitializeComponent();
            live = true;                          //setzen der Variable auf true (für einige Funktionen, die nur im Live-Modus funktionieren)                 
            topBottomSave = FormLive.topBottom;   //Speichern der aktuellen Position im Live-Fenster
        }

        public Dynamic(DriversGuideApp caller2)   //Aufruf Dynamic-Form im Auswertungs-Modus
        {
            MainForm = caller2;
            InitializeComponent();
            live = false;   //Live-Modus nicht aktiv
        }

        public void SetTopBottom(bool pos)   //Speichern der aktuellen Position im Live-Fenster
        {
            topBottomSave = pos;
        }

        public string[] GetUnits(string xGewDat, string GewDaten)         //liefert StringArray mit x- u. y-Einheiten
        {
            DataTable units = MainForm.GetUnitsDataTable();               //Kopie der Einheiten-Datatable aus dem Hauptform

            string xUnit = Convert.ToString((units.Rows[0][xGewDat]));    //Einheit der x-Achse
            string yUnit = Convert.ToString((units.Rows[0][GewDaten]));   //Einheit der y-Achse

            string[] xyUnits = new string[] { xUnit, yUnit };             //erstellt StringArray mit x- u. y-Einheiten

            return xyUnits;                                               //liefert StingArray zurück
        }

        private void Dynamic_Load(object sender, EventArgs e)
        {
            //Deaktivierung Scrollbars
            this.AutoScroll = false;

            //Ausblenden der Labels zu Beginn
            lblUrb.Visible = false;
            lblRur.Visible = false;
            lblMw.Visible = false;

            //Positionierung der charts
            LocateCharts(ChUrb, ChRur, ChMw);
            //Positionierung der labels
            LocateLabels(lblUrb, lblRur, lblMw, ChUrb, ChRur, ChMw);

            if (live == false)  //nur im Auswertungsmodus
            {   
                //Erstellen MouseWheel-Ereignis für einzelne Charts
                this.ChUrb.MouseWheel += ChUrb_MouseWheel;
                this.ChRur.MouseWheel += ChRur_MouseWheel;
                this.ChMw.MouseWheel += ChMw_MouseWheel;

                //Kopie der Datatables aus dem Hauptform
                dturb = MainForm.GetUrbanDataTable();
                dtrur = MainForm.GetRuralDataTable();
                dtmw = MainForm.GetMotorwayDataTable();
                units = MainForm.GetUnitsDataTable();
                
                //Zuweisung Perzentilwerte
                MainForm.GetPercentiles(ref purb, ref prur, ref pmw);                       
                //Zuweisung Perzentilgrenzwerte
                MainForm.GetPercentileBorders(ref borderUrb, ref borderRur, ref borderMw);   
                
                //Zeichnen der Diagramme (Grundeinstellungen ohne Zeichnen von Daten)
                DrawCharts(dturb, dtrur, dtmw, purb, prur, pmw, borderUrb, borderRur, borderMw);
                //Füllen der Diagramme mit Daten
                FillCharts(dturb, dtrur, dtmw, ymax, purb, prur, pmw, borderUrb, borderRur, borderMw);

                //Aktivierung Cursor in Diagrammen
                EnableCursor(ref ChUrb, "a*v");
                EnableCursor(ref ChRur, "a*v");
                EnableCursor(ref ChMw, "a*v");
            }
            if (live == true)  //nur im Live-Modus
            {
                //Kopie der Datatables aus dem Live-Form
                dturb = FormLive.GetUrbanDataTable();
                dtrur = FormLive.GetRuralDataTable();
                dtmw = FormLive.GetMotorwayDataTable();

                //Zuweisung Perzentilwerte
                FormLive.GetPercentiles(ref purb, ref prur, ref pmw);
                //Zuweisung Perzentilgrenzwerte
                FormLive.GetPercentileBorders(ref borderUrb, ref borderRur, ref borderMw);

                //Zeichnen der Diagramme (Grundeinstellungen ohne Zeichnen von Daten)
                DrawCharts(dturb, dtrur, dtmw, purb, prur, pmw, borderUrb, borderRur, borderMw);
                //Füllen der Diagramme mit Daten erfolgt über Live-Form
            }
        }

        //Setzt die Grundeinstellungen der Charts, ohne Daten einzuzeichnen:
        private void DrawCharts(DataTable dturb, DataTable dtrur, DataTable dtmw, double purb, double prur, double pmw, double borderUrb, double borderRur, double borderMw)
        {     
            //Max-Wert a*v Stadt
            if (dturb.Rows.Count != 0)
                urbmax = Convert.ToDouble(dturb.Rows[dturb.Rows.Count - 1]["a*v"]);
            //Max-Wert a*v Land
            if (dtrur.Rows.Count != 0)
                rurmax = Convert.ToDouble(dtrur.Rows[dtrur.Rows.Count - 1]["a*v"]);
            //Max-Wert a*v Autobahn
            if (dtmw.Rows.Count != 0)
                mwmax = Convert.ToDouble(dtmw.Rows[dtmw.Rows.Count - 1]["a*v"]);
            
            //Max-Wert a*v generell, auf nächste 5 aufgerundet
            ymax = 5 * (int)Math.Round((Math.Max(urbmax, Math.Max(rurmax, mwmax)) + 3) / 5.0);
            //größter Perzentilgrenzwert, auf nächste 5 aufgerundet
            bordermax = 5 * (int)Math.Round((Math.Max(borderUrb, Math.Max(borderRur, borderMw)) + 3) / 5.0);

            //Festlegen y-Achsen-Maximum
            if(bordermax > ymax)
            {
                ymax = bordermax;
            }

            //Setzen der Diagrammtitel
            ChUrb.Titles.Add("Dynamik Stadt").Font = new Font("Arial", 10, FontStyle.Bold);
            ChRur.Titles.Add("Dynamik Freiland").Font = new Font("Arial", 10, FontStyle.Bold);
            ChMw.Titles.Add("Dynamik Autobahn").Font = new Font("Arial", 10, FontStyle.Bold);

            //Löschen bestehender und hinzufügen neuer Datenpunktreihe u. Grafikoberfläche
            SetChartProperties(ref ChUrb, dturb, "Perzentil", "a*v", ymax);
            SetChartProperties(ref ChRur, dtrur, "Perzentil", "a*v", ymax);
            SetChartProperties(ref ChMw, dtmw, "Perzentil", "a*v", ymax);
        }

        public void ClearAndSetChartsLive(double ymax)
        {
            //Löschen bestehender und hinzufügen neuer Datenpunktreihe u. Grafikoberfläche im Live-Modus
            SetChartProperties(ref ChUrb, dturb, "Perzentil", "a*v", ymax);
            SetChartProperties(ref ChRur, dtrur, "Perzentil", "a*v", ymax);
            SetChartProperties(ref ChMw, dtmw, "Perzentil", "a*v", ymax);
        }

        //Löschen bestehender und hinzufügen neuer Datenpunktreihe u. Grafikoberfläche:
        private void SetChartProperties(ref Chart chartname, DataTable tt, string xGewDat, string GewDaten, double ymax)
        {   
            chartname.Show();                     //Anzeigen der Grafik

            chartname.Series.Clear();             //Löschen evtl. bestehender Datenpunktreihe
            chartname.ChartAreas.Clear();         //Löschen evtl. bestehender Grafikoberflächen

            chartname.Series.Add(GewDaten);       //Hinzufügen einer (vorerst leeren) neuen Datenpunktreihe
            chartname.ChartAreas.Add(GewDaten);   //Hinzufügen einer neuen Grafikoberfläche 

            chartname.Series[GewDaten].IsVisibleInLegend = false;   //Ausblenden legende

            string xUnit = "";
            string yUnit = "";

            //Festelegung Einheiten:
            if (live == false)
            {
                DataTable units = MainForm.GetUnitsDataTable();
                xUnit = GetUnits(xGewDat, GewDaten)[0];   //Einheit der x-Achse
                yUnit = GetUnits(xGewDat, GewDaten)[1];   //Einheit der y-Achse
            }
            if (live == true)
            {
                xUnit = "%";       //Einheit der x-Achse
                yUnit = "m²/s³";   //Einheit der y-Achse
            }

            //Achseneistellungen festlegen:
            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen
            //chartname.Titles.Add("Test").Font = new Font("Arial", 10, FontStyle.Bold); //Chart Title
            Chart1.AxisX.Title = xGewDat + " in " + xUnit;                    //Beschriftung der x-Achse
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

            Chart1.AxisX.Minimum = 0;                       //x-Achsen-Minimum
            Chart1.AxisX.Maximum = 100;                     //x-Achsen-Maximum
            Chart1.AxisX.Interval = 20;                     //x-Achsen-Intervall

            Chart1.AxisY.Minimum = 0;                       //y-Achsen-Minimum
            Chart1.AxisY.Maximum = Convert.ToInt64(ymax);   //y-Achsen-Maximum
            Chart1.AxisY.Interval = 5;                      //x-Achsen-Intervall
        }

        public void FillCharts(DataTable dturb, DataTable dtrur, DataTable dtmw, double ymax, double purb, double prur, double pmw, double borderUrb, double borderRur, double borderMw)
        {   //Füllen der drei Charts, je nachdem, wie die Datatables aus Live verfügbar sind (leer od. nicht):

            //Prüfung, ob Datatable Stadt leer ist
            if (dturb.Rows.Count != 0)   
            {
                AddDataPoints(dturb, ref ChUrb, "Perzentil", "a*v");   //Zeichnen der Dynamikwerte
                Draw95PLine(ChUrb, ymax, purb, borderUrb);             //Zeichnen der 95%-Linie
                DrawPerzentilValue(ChUrb, ymax, purb, borderUrb);      //Zeichnen des Perzentilwertes bei 95 %
                DrawDynamicLimit(ChUrb, ymax, purb, borderUrb);        //Zeichnen des Perzentilgrenzwertes

                //Hinzufügen leere Datenreihe, falls Datatable Freiland bzw. Autobahn leer ist
                //(ansonsten würden die beiden Diagramme nicht angezeigt werden; sieht nicht gut aus)
                if (dtrur.Rows.Count == 0)  
                {
                    ChRur.Series["a*v"].Points.AddXY(0, 0);
                }
                if (dtmw.Rows.Count == 0)
                {
                    ChMw.Series["a*v"].Points.AddXY(0, 0);
                }
            }
            //Prüfung, ob Datatable Freiland leer ist
            if (dtrur.Rows.Count != 0)
            {
                AddDataPoints(dtrur, ref ChRur, "Perzentil", "a*v");   //Zeichnen der Dynamikwerte
                Draw95PLine(ChRur, ymax, prur, borderRur);             //Zeichnen der 95%-Linie
                DrawPerzentilValue(ChRur, ymax, prur, borderRur);      //Zeichnen des Perzentilwertes bei 95 %
                DrawDynamicLimit(ChRur, ymax, prur, borderRur);       //Zeichnen des Perzentilgrenzwertes

                //Hinzufügen leere Datenreihe, falls Datatable Autobahn leer ist
                //(ansonsten würde das Diagramm nicht angezeigt werden; sieht nicht gut aus)
                if (dtmw.Rows.Count == 0)
                {
                    ChMw.Series["a*v"].Points.AddXY(0, 0);
                }
            }
            //Prüfung, ob Datatable Autobahn leer ist
            if (dtmw.Rows.Count != 0) 
            {
                AddDataPoints(dtmw, ref ChMw, "Perzentil", "a*v");    //Zeichnen der Dynamikwerte
                Draw95PLine(ChMw, ymax, pmw, borderMw);               //Zeichnen der 95%-Linie
                DrawPerzentilValue(ChMw, ymax, pmw, borderMw);        //Zeichnen des Perzentilwertes bei 95 %
                DrawDynamicLimit(ChMw, ymax, pmw, borderMw);          //Zeichnen des Perzentilgrenzwertes
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


        private void Draw95PLine(Chart chartname, double ymax, double perzval, double dynborder)   //Zeichnen der 95%-Linie
        {
            //Hinzufügen der 95%-Linie
            chartname.Series.Add("95PLine");       
            for (int i = 0; i <= ymax; i += 1)    //füllen Datenpunke-Serie
            {
                chartname.Series["95PLine"].Points.AddXY(95, i);
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }
            chartname.Series["95PLine"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line)
            chartname.Series["95PLine"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name
    
            //Hinzufügen 95 % Label (durch Hinzufügen eines Punktes, der beschriftet wird)
            chartname.Series.Add("95pLbl");
            //Positinierung und Ausrichtung des Labels 
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

            chartname.Series["95pLbl"].Color = Color.Transparent;                     //Farbe des Punktes
            chartname.Series["95pLbl"].IsVisibleInLegend = false;                     //Ausblenden Chartseries-Name
            chartname.Series["95pLbl"].Label = "95 %";                                //Hinzufügen Label
            chartname.Series["95pLbl"].LabelBackColor = Color.Transparent;            //Hintergrundfarbe Label
            chartname.Series["95pLbl"].SmartLabelStyle.Enabled = false;               //Deaktivierung automatische Label-Positionierung
            chartname.Series["95pLbl"].ChartType = SeriesChartType.Column;            //Diagrammtyp
            chartname.Series["95pLbl"]["LabelStyle"] = "Center";                      //Label-Positionierung
            chartname.Series["95pLbl"].Font = new System.Drawing.Font("Arial", 10);   //Schriftart u. Schriftgröße
            //chartname.Series["95pLbl"]["PointWidth"] = "0.7";                
            //chartname.Series["95pLbl"].IsValueShownAsLabel = true;

            //Farbe Linie u. Label je nachdem, ob Perzentilgrenzwert überschritten oder nicht
            if (dynborder >= perzval)
            {
                chartname.Series["95PLine"].Color = Color.Green;           //Linienfarbe
                chartname.Series["95pLbl"].LabelForeColor = Color.Green;   //Labelfarbe (Schriftfarbe)
            }
            else
            {
                chartname.Series["95PLine"].Color = Color.Red;           //Linienfarbe
                chartname.Series["95pLbl"].LabelForeColor = Color.Red;   //Labelfarbe (Schriftfarbe)
            }
            chartname.Invalidate();
        }

        private void DrawPerzentilValue(Chart chartname, double ymax, double perzval, double dynborder)   //Zeichnen des Perzentilwertes bei 95 %
        {
            //Hinzufügen des Perzentilwertes
            chartname.Series.Add("PerzVal");       
            for (int i = 0; i <= 95; i += 1)   //füllen Datenpunke-Serie
            {
                chartname.Series["PerzVal"].Points.AddXY(i, perzval);
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }
            chartname.Series["PerzVal"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["PerzVal"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name

            //Hinzufügen Perzentilwert-Label (durch Hinzufügen eines Punktes, der beschriftet wird)
            chartname.Series.Add("PerzVLbl");             
            chartname.Series["PerzVLbl"].Points.AddXY(5, perzval);
            chartname.Series["PerzVLbl"].Color = Color.Transparent;                     //Farbe des Punktes
            chartname.Series["PerzVLbl"].IsVisibleInLegend = false;                     //Ausblenden Chartseries-Name
            chartname.Series["PerzVLbl"].LabelBackColor = Color.White;                  //Hintergrundfarbe Label
            chartname.Series["PerzVLbl"].LabelAngle = 0;                                //Ausrichtung Label
            chartname.Series["PerzVLbl"].SmartLabelStyle.Enabled = false;               //Deaktivierung automatische Label-Positionierung
            chartname.Series["PerzVLbl"].ChartType = SeriesChartType.Column;            //Diagrammtyp
            chartname.Series["PerzVLbl"].Font = new System.Drawing.Font("Arial", 10);   //Schriftart u. Schriftgröße
            if(live == true)
            {
                chartname.Series["PerzVLbl"].Label = "Aktueller Wert: " + Math.Round(perzval, 1).ToString("F1") + " m²/s³";   //Hinzufügen Label
            }
            else
            {
                chartname.Series["PerzVLbl"].Label = "95-%-Wert: " + Math.Round(perzval, 1).ToString("F1") + " m²/s³";   //Hinzufügen Label
            }
            //chartname.Series["PerzVLbl"]["PointWidth"] = "0.7";
            //chartname.Series["PerzVLbl"].IsValueShownAsLabel = true;

            //Label-Positionierung und -Farbe abhängig von aktuellem Perzentilwert u. Grenzwert (um Überlappung der Labels zu verhindern)
            double ValDiff = Math.Abs(dynborder - perzval);                     //Differenz Grenzwert u. aktueller Wert
            if (ValDiff > 3)
            {
                chartname.Series["PerzVLbl"]["LabelStyle"] = "Right";            //Label-Positionierung
                if (dynborder > perzval)
                {
                    chartname.Series["PerzVal"].Color = Color.Green;             //Linienfarbe 
                    chartname.Series["PerzVLbl"].LabelForeColor = Color.Green;   //Labelfarbe (Schriftfarbe)
                }
                else
                {
                    chartname.Series["PerzVal"].Color = Color.Red;               //Linienfarbe 
                    chartname.Series["PerzVLbl"].LabelForeColor = Color.Red;     //Labelfarbe (Schriftfarbe)
                }
            }
            else if (dynborder > perzval)
            {
                chartname.Series["PerzVLbl"]["LabelStyle"] = "BottomRight";      //Label-Positionierung
                chartname.Series["PerzVal"].Color = Color.Green;                 //Linienfarbe 
                chartname.Series["PerzVLbl"].LabelForeColor = Color.Green;       //Labelfarbe (Schriftfarbe)
            }
            else if (dynborder < perzval)
            {
                chartname.Series["PerzVLbl"]["LabelStyle"] = "TopRight";         //Label-Positionierung
                chartname.Series["PerzVal"].Color = Color.Red;                   //Linienfarbe 
                chartname.Series["PerzVLbl"].LabelForeColor = Color.Red;         //Labelfarbe (Schriftfarbe)
            }
            chartname.Invalidate();
        }

        private void DrawDynamicLimit(Chart chartname, double ymax, double perzval, double dynborder)   //Zeichnen des Dynamik-Grenzwertes (Perzentil-Grenzwert)
        {       
            //Hinzufügen des Perzentilgrenzwertes
            chartname.Series.Add("DynLim");

            for (int i = 0; i <= 95; i += 1)   //füllen Datenpunke-Serie
            {
                chartname.Series["DynLim"].Points.AddXY(i, dynborder);
                //erzeugt Serie von Punkten mit denen gezeichnet wird
            }
            chartname.Series["DynLim"].ChartType = SeriesChartType.Spline;   // Festlegen des Diagrammtypes (hier Smooth-Line) 
            chartname.Series["DynLim"].IsVisibleInLegend = false;            //Ausblenden Chartseries-Name
            
            //Hinzufügen Grenzwert-Label (durch Hinzufügen eines Punktes, der beschriftet wird)
            chartname.Series.Add("DynLimLbl");                                 
            chartname.Series["DynLimLbl"].Points.AddXY(5, dynborder);
            chartname.Series["DynLimLbl"].Color = Color.Transparent;                      //Farbe des Punktes
            chartname.Series["DynLimLbl"].IsVisibleInLegend = false;                      //Ausblenden Chartseries-Name
            chartname.Series["DynLimLbl"].Label = "Grenzwert: " + Math.Round(dynborder, 1).ToString("F1") + " m²/s³";   //Hinzufügen Label
            chartname.Series["DynLimLbl"].LabelBackColor = Color.White;                   //Hintergrundfarbe Label 
            chartname.Series["DynLimLbl"].LabelAngle = 0;                                 //Ausrichtung Label
            chartname.Series["DynLimLbl"].SmartLabelStyle.Enabled = false;                //Deaktivierung automatische Label-Positionierung
            chartname.Series["DynLimLbl"].ChartType = SeriesChartType.Column;             //Diagrammtyp
            chartname.Series["DynLimLbl"].Font = new System.Drawing.Font("Arial", 10);    //Schriftart u. Schriftgröße
            //chartname.Series["DynLimLbl"]["PointWidth"] = "0.7";
            //chartname.Series["DynLimLbl"].IsValueShownAsLabel = true;

            //Label-Positionierung und -Farbe abhängig von aktuellem Perzentilwert u. Grenzwert (um Überlappung der Labels zu verhindern)
            double ValDiff = Math.Abs(dynborder - perzval);                          //Differenz Grenzwert u. aktueller Wert
            if (ValDiff > 3)
            {
                chartname.Series["DynLimLbl"]["LabelStyle"] = "Right";            //Label-Positionierung
                if (dynborder > perzval)
                {
                    chartname.Series["DynLim"].Color = Color.Red;                 //Linienfarbe 
                    chartname.Series["DynLimLbl"].LabelForeColor = Color.Red;     //Labelfarbe (Schriftfarbe)
                }
                else
                {
                    chartname.Series["DynLim"].Color = Color.Green;               //Linienfarbe 
                    chartname.Series["DynLimLbl"].LabelForeColor = Color.Green;   //Labelfarbe (Schriftfarbe)
                }
            }
            else if (dynborder > perzval)
            {
                chartname.Series["DynLimLbl"]["LabelStyle"] = "TopRight";         //Label-Positionierung
                chartname.Series["DynLim"].Color = Color.Red;                     //Linienfarbe 
                chartname.Series["DynLimLbl"].LabelForeColor = Color.Red;         //Labelfarbe (Schriftfarbe)
            }
            else if (dynborder < perzval)
            {
                chartname.Series["DynLimLbl"]["LabelStyle"] = "BottomRight";      //Label-Positionierung
                chartname.Series["DynLim"].Color = Color.Green;                   //Linienfarbe
                chartname.Series["DynLimLbl"].LabelForeColor = Color.Green;       //Labelfarbe (Schriftfarbe)
            }
            chartname.Invalidate();
        }

        private void EnableCursor(ref Chart chartname, string GewDaten)
        {   //Aktivierung und Einstellung Cursor
            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen

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
            labelname3.Location = new Point(chartname3.Location.X + (chartname3.Width / 2) - 60, chartname3.Height - 10);   //Positionierung Label
        }

        private void Dynamic_SizeChanged(object sender, EventArgs e)
        {
            LocateCharts(ChUrb, ChRur, ChMw);                          //Positionierung der charts
            LocateLabels(lblUrb, lblRur, lblMw, ChUrb, ChRur, ChMw);   //Positionierung der labels
        }

        private void MoveCursor(Chart chartname, Label labelname, MouseEventArgs e, DataTable tt, string xGewDat, string GewDaten, string xunit, string yunit)
        {
            //bei Bewegen der Maus wird der Cursor auf die aktuelle Mausposition gestellt und 
            //die dazugehörigen x- u. y-Werte angezeigt

            //wird erst ausgeführt nachdem gewisse Zeit verstrichen ist, da ansonsten noch nicht alle
            //Datenpunkte in der Grafik gezeichnet wurden, was zu einer Fehlermeldung führt

            double xpos = newchart.ActualXPosition(ref chartname, e, GewDaten);   //aktueller x-Wert
            double ypos = newchart.FindYValue(chartname, xpos, GewDaten);         //aktueller y-Wert

            //setzen des Cursors auf aktuelle Werte
            chartname.ChartAreas[GewDaten].CursorX.Position = xpos;
            chartname.ChartAreas[GewDaten].CursorY.Position = ypos;

            //Label mit aktueller Positionsanzeige
            labelname.Text = "x = " + Math.Round(xpos, 1).ToString("F1") + " " + xunit + "    " +
                             "y = " + ypos.ToString("F2") + " " + yunit;
            labelname.Visible = true;
        }

        private void ChUrb_MouseMove(object sender, MouseEventArgs e)
        {
            //Cursorbewegung analog zu Mausbewegung im Diagramm
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
            //Cursorbewegung analog zu Mausbewegung im Diagramm
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
            //Cursorbewegung analog zu Mausbewegung im Diagramm
            if (live == false)
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
            //Startskalierung der Diagrammachsen bei Drehen des Mausrades
            SetStartScale(ChUrb, "a*v");
        }

        private void ChRur_MouseWheel(object sender, MouseEventArgs e)
        {
            //Startskalierung der Diagrammachsen bei Drehen des Mausrades
            SetStartScale(ChRur, "a*v");
        }

        private void ChMw_MouseWheel(object sender, MouseEventArgs e)
        {
            //Startskalierung der Diagrammachsen bei Drehen des Mausrades
            SetStartScale(ChMw, "a*v");
        }

        private void ChRur_Click(object sender, EventArgs e)
        {
            //Speichern der aktuellen Position im Live-Fenster
            if (live == true)
            {
                FormLive.topBottom = topBottomSave;
            }
        }

        private void ChUrb_Click(object sender, EventArgs e)
        {
            //Speichern der aktuellen Position im Live-Fenster
            if (live == true)
            {
                FormLive.topBottom = topBottomSave;
            }
        }

        private void ChMw_Click(object sender, EventArgs e)
        {
            //Speichern der aktuellen Position im Live-Fenster
            if (live == true)
            {
                FormLive.topBottom = topBottomSave;
            }
        }

        //private void DeactSV(Chart chartname, string GewDaten)  //Deaktiviert Bereichsauswahl in Chart
        //{
        //    chartname.ChartAreas[GewDaten].AxisX.ScaleView.Zoomable = false;
        //    chartname.ChartAreas[GewDaten].AxisY.ScaleView.Zoomable = false;
        //}
    }
}

