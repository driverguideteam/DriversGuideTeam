using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;   //für einige Chart-Optionen benötigt

namespace DriversGuide
{
    class GraphicsCreate
    {
        Random u = new Random();

        public void Drawchart ()
        {
            Chart chartname = new Chart();
            chartname.Show();


            chartname.Series.Clear();
            chartname.ChartAreas.Clear();

            chartname.Series.Add("TestDaten1");
            chartname.ChartAreas.Add("TestDaten1");

            for (double i = -300; i <= 300; i += 0.5)   //füllen Datenpunke-Serie
            {
                chartname.Series["TestDaten1"].Points.AddXY(i, u.NextDouble() * 20);   //erzeugt Serie von Punkten mit denen gezeichnet wird
            }

            var Chart1 = chartname.ChartAreas["TestDaten1"];

            Chart1.AxisX.ScaleView.Zoom(-100, 100);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            //Chart1.AxisY.ScaleView.Zoom(1.1, 1.1);   //Startskalierung der y-Achse

            chartname.Series["TestDaten1"].ChartType = SeriesChartType.Spline;

            chartname.Invalidate();
        }












    }
}
