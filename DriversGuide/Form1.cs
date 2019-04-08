using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriversGuide
{
    public partial class Form1 : Form
    {
        MeasurementFile Datei;
        Calculations Berechnung;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnReadMeasuremntfile_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Textdateien |*.txt| Alle Dateien|*.*";
            ofd.InitialDirectory = "C:\\Repositories\01_Doku\\Vorgabe\\";
            //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            ofd.Title = "Textdatei öffnen";
            ofd.FileName = "";

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Datei = new MeasurementFile(ofd.FileName);
                DataTable test = new DataTable();
                test = Datei.ConvertCSVtoDataTable();
                // var listofData = Datei.ReadMeasurementFile();

                //txtMeasurement.Text = listofData[1][1].ToString();
                Berechnung = new Calculations();
                bool test1;
                string column_speed = "OBD_Vehicle_Speed_(PID_13)";
                string column_acc = "ai";
                string column_dynamic = "a*v";
                string column_distance = "di";
                double RPA_city, nfPerc; ;

                DataTable city = new DataTable();
                DataTable land = new DataTable();
                DataTable autobahn = new DataTable();                

                Berechnung.CalcReq(test, column_speed);
                Berechnung.SortData(ref test, column_speed, true);
                Berechnung.SepIntervals(test, column_speed);
                test1 = Berechnung.PosCheck(column_acc);
                Berechnung.CalcAvgSpeedInt(column_speed);                
                Berechnung.GetIntervals(ref city, ref land, ref autobahn);
                nfPerc = Berechnung.CalcPercentile_Interval(ref city, column_dynamic);            
                RPA_city = Berechnung.CalcRPA(test, city, column_speed, column_acc, column_distance);
            }
            else
            {
                txtMeasurement.Text = "Fail";
            }
        }

        private void btnGraphic_Click(object sender, EventArgs e)
        {
           PlotGraphic NewDiagram = new PlotGraphic();
            NewDiagram.Show();

        }
    }
}
