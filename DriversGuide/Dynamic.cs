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
        LiveMode FormLive;
        private DriversGuideApp MainForm;
        GraphicsCreate newchart = new GraphicsCreate();   //neue Grafik erstellen, Eigenschaften in GraphicsCreate festgelegt
        PlotGraphic PlotCopy;

        public Dynamic(LiveMode caller)
        {
            FormLive = caller;

            InitializeComponent();
        }

        public Dynamic(DriversGuideApp caller2)
        {
            MainForm = caller2;
            InitializeComponent();
        }

        public void GetChosenData(PlotGraphic ConnectForm)
        {
            PlotCopy = ConnectForm;   //Zugriff auf Datenauswahlform
        }

        private void Dynamic_Load(object sender, EventArgs e)
        {
            DataTable dturb = MainForm.GetUrbanDataTable();
            DataTable dtrur = MainForm.GetRuralDataTable();
            DataTable dtmw = MainForm.GetMotorwayDataTable();



            newchart.SetChartProperties(ref ChUrb, dturb, "Perzentil", "a*v", PlotCopy);
            newchart.SetChartProperties(ref ChRur, dtrur, "Perzentil", "a*v", PlotCopy);
            newchart.SetChartProperties(ref ChMw, dtmw, "Perzentil", "a*v", PlotCopy);
            //SetChAxes(ref ChUrb, dturb, "Perzentil", "a*v", PlotCopy);
        }

        private void SetChAxes(ref Chart chartname, DataTable tt, string xGewDat, string GewDaten)
        {
            var Chart1 = chartname.ChartAreas[GewDaten];   //dient nur der Verkürzung folgender Programmzeilen

            string xUnit = "";//plc.GetUnits(GewDaten)[0];   //liefert Einheit der x-Achse
            string yUnit = "";//plc.GetUnits(GewDaten)[1];   //liefert Einheit der y-Achse

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
        }
    }
}

