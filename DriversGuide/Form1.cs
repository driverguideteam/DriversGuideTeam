using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DriversGuide
{
    public partial class Form1 : Form
    {
        MeasurementFile Datei;
        Calculations Berechnung;
        Validation Gueltigkeit;
        //DataSet dx = new DataSet(); 
        DataTable test = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        public DataTable GetDataTable()
        {
            return test;
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
                //DataTable test = new DataTable();
                test = Datei.ConvertCSVtoDataTable();
                // var listofData = Datei.ReadMeasurementFile();

                //txtMeasurement.Text = listofData[1][1].ToString();
                Berechnung = new Calculations();
                Gueltigkeit = new Validation();
                bool test1, testdur;
                string column_speed = "OBD_Vehicle_Speed_(PID_13)";
                string column_acc = "ai";
                string column_dynamic = "a*v";
                string column_distance = "di";
                string column_time = "Time";
                string column_coolant = "OBD_Engine_Coolant_Temperature_(PID_5)";
                double RPA_city, nfPerc;
                double percUrb = 0, percRur = 0, percMotor = 0;

                DataTable city = new DataTable();
                DataTable land = new DataTable();
                DataTable autobahn = new DataTable();

                //Stopwatch watch = new Stopwatch();
                //watch.Start();

                test1 = Berechnung.CalcAll(test, column_speed, column_acc, column_dynamic, column_distance);
                testdur = Gueltigkeit.CheckValidity(test, column_speed, column_time, column_coolant, column_distance);

                //Berechnung.CalcReq(ref test, column_speed);
                //testdur = Gueltigkeit.CheckValidity(test, column_speed, column_time, column_coolant, column_distance);

                //Berechnung.SortData(ref test, column_speed);
                //Berechnung.SepIntervals(test, column_speed);
                //Berechnung.GetIntervals(ref city, ref land, ref autobahn);
                //testdur = Gueltigkeit.CheckDistanceIntervals(city, land, autobahn, column_distance);
                //testdur = Gueltigkeit.CheckDistributionIntervals(city, land, autobahn, column_distance);
                //testdur = Gueltigkeit.CheckDistributionComplete(test, column_speed, column_distance);

                //test1 = Berechnung.PosCheck(column_acc);
                //Berechnung.CalcAvgSpeedInt(column_speed);
                //Berechnung.GetIntervals(ref city, ref land, ref autobahn);
                //nfPerc = Berechnung.CalcPercentile_Interval(ref city, column_dynamic);
                //RPA_city = Berechnung.CalcRPA(test, city, column_speed, column_acc, column_distance);

                //testdur = Gueltigkeit.CheckDuration(test, column_time);
                //testdur = Gueltigkeit.CheckDistanceComplete(test, column_speed, column_distance);
                //testdur = Gueltigkeit.CheckDistributionComplete(test, column_speed, column_distance);

                //testdur = Gueltigkeit.CheckSpeeds(city, autobahn, column_speed, column_time);
                //testdur = Gueltigkeit.CheckColdStart(test, column_speed, column_time, column_coolant);
                //watch.Stop();
                //MessageBox.Show("Time spent: " + watch.Elapsed);
               
               
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

        private void btnSpielwiese_Click(object sender, EventArgs e)
        {
            ChartSpielwiese Test1 = new ChartSpielwiese();
            Test1.Show();
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            GPSVisualization FormGPS = new GPSVisualization(this);
            FormGPS.ShowDialog();
        }
    }
}
