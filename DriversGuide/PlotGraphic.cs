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
    public partial class PlotGraphic : Form
    {
        public PlotGraphic()
        {
            InitializeComponent();
        }

        Random z = new Random();

        //public double function(double x)
        //{
        //    //function: sin(x)                              //Testfunktion
        //    return (Math.Exp(Math.Sin(x)));
        //}

        public void PlotGraphic_Load(object sender, EventArgs e)
        {


            for (double i = -300; i <= 300; i += 0.5)   //füllen Datenpunke-Serie
            {
                chart1.Series["TestDaten1"].Points.AddXY(i, z.NextDouble() * 20);   //erzeugt Serie von Punkten mit denen gezeichnet wird
            }
            
            var Chart1 = chart1.ChartAreas["TestDaten1"];

            Chart1.AxisX.ScaleView.Zoom(-100, 100);   //Startskalierung der x-Achse - Festlegen der Startansicht, nicht des Koordinatensystems
            //Chart1.AxisY.ScaleView.Zoom(1.1, 1.1);   //Startskalierung der y-Achse

            chart1.Series["TestDaten1"].ChartType = SeriesChartType.Spline;   //legt Diagrammtyp fest -> smooth-lin

            Chart newch = new Chart();
            newch.Show();

            Button xzy = new Button();
            xzy.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GraphicsCreate newchart = new GraphicsCreate();
            newchart.Drawchart();
            GraphicsCreate newchart2 = new GraphicsCreate();
            newchart2.Drawchart();
        }
    }
}
