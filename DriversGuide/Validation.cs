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
    class Validation
    {
        Calculations Berechnungen = new Calculations();

        private void CalcDistances (DataTable urban, DataTable rural, DataTable motorway, string column_distance, ref double[] distances)
        {
            distances[0] = (double)urban.Compute("SUM([" + column_distance + "])", "") / 1000;           
            distances[1] = (double)rural.Compute("SUM([" + column_distance + "])", "") / 1000;
            distances[2] = (double)motorway.Compute("SUM([" + column_distance + "])", "") / 1000;
        }

        private bool CheckUrban (DataTable dt, string column_speed, string column_time)
        {
            //DataTable temp = dt.Clone();

            double avgSpeed = 0;
            double duration_interval = 0;
            double duration_hold = 0;
            double duration_ratio = 0;
            int countTime = 1;
            
            //avgSpeed = CalcSum(dt, column_speed) / dt.Rows.Count;
            avgSpeed = (double)dt.Compute("SUM([" + column_speed + "])", "") / dt.Rows.Count;
            duration_interval = (double)(dt.Rows.Count * 500 - 500) / 60000;            

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (Convert.ToDouble(dt.Rows[i][column_speed]) < 1)
            //    {
            //        temp.ImportRow(dt.Rows[i]);
            //    }
            //}

            //dt = temp.Copy();

            dt = dt.Select("[" + column_speed + "]" + " < 1").CopyToDataTable();
            Berechnungen.SortData(ref dt, column_time, false);

            //Zur Berechnung ob einzelne Haltezeit länger als 300s war
            for (int i = 1; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDouble(dt.Rows[i][column_time]) - Convert.ToDouble(dt.Rows[i - 1][column_time]) == 500)
                    countTime++;
                else
                    countTime = 1;
            }

            duration_hold = (double)(dt.Rows.Count * 500 - 500) / 60000;           

            //for (int i = 0; i < dt.Rows.Count; i++)                   //Überlegung zu realisierung der aufeinanderfolgenden Standzeitberechnung von 300s / 5min

            duration_ratio = duration_hold * 100 / duration_interval;

            if (avgSpeed >= 15 && avgSpeed <= 40 && duration_ratio >= 6 && duration_ratio <= 30 && countTime <= 120)
                return true;
            else
                return false;
        }

        private bool CheckMotorway (DataTable dt, string column_speed)
        {
            DataTable fasterOH = new DataTable();
            DataTable fasterHFF = new DataTable();
            fasterOH = dt.Clone();
            fasterHFF = dt.Clone();

            double max, min, duration, fasterOnehundred, tooFast = 0;

            max = (double)dt.Compute("MAX([" + column_speed + "])", "");
            min = (double)dt.Compute("MIN([" + column_speed + "])", "");

            fasterOH = dt.Select("[" + column_speed + "]" + " > 100").CopyToDataTable();       
            fasterOnehundred = (double)(fasterOH.Rows.Count * 500 - 500) / 60000;         
                 
            if (max >= 145 && max <= 160)
            {
                fasterHFF = dt.Select("[" + column_speed + "]" + " >= 145").CopyToDataTable();
                duration = (double)(dt.Rows.Count * 500 - 500) / 60000;
                tooFast = (double)(fasterHFF.Rows.Count * 500 - 500) / 60000;
                tooFast *= 100 / duration;
            }

            if (min > 90 && max >= 110 && max <= 145 && fasterOnehundred >= 5)
                return true;
            else if (min > 90 && max >= 145 && max <= 160 && fasterOnehundred >= 5 && tooFast <= 3)
                return true;
            else
                return false;
        }

        public bool CheckDistributionComplete (DataTable dt, string column_speed, string column_distance)
        {
            DataTable urban = new DataTable();
            DataTable rural = new DataTable();
            DataTable motorway = new DataTable();
            double trip = 0;
            double[] distances = new double[3];

            Berechnungen.SepIntervals(dt, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            trip = (double)dt.Compute("SUM([" + column_distance + "])", "") / 1000;

            distances[0] *= 100 / trip;
            distances[1] *= 100 / trip;
            distances[2] *= 100 / trip;

            if (distances[0] >= 29 && distances[1] <= 44 && distances[1] >= 23 && distances[1] <= 43 && distances[2] >= 23 && distances[2] <= 43)
                return true;
            else
                return false;
        }

        public bool CheckDistributionIntervals(DataTable urban, DataTable rural, DataTable motorway, string column_distance)
        {
            double trip = 0;
            double[] distances = new double[3];
            
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            trip = distances[0] + distances[1] + distances[2];

            distances[0] *= 100 / trip;
            distances[1] *= 100 / trip;
            distances[2] *= 100 / trip;

            if (distances[0] >= 29 && distances[1] <= 44 && distances[1] >= 23 && distances[1] <= 43 && distances[2] >= 23 && distances[2] <= 43)
                return true;
            else
                return false;
        }

        public bool CheckSpeeds(DataTable urban, DataTable motorway, string column_speed, string column_time)
        {
            //double avgSpeed_urban, avgSpeed_rural, avgSpeed_motorway;

            //avgSpeed_urban = CalcSum(urban, column_speed) / urban.Rows.Count;
            //avgSpeed_rural = CalcSum(rural, column_speed) / rural.Rows.Count;
            //avgSpeed_motorway = CalcSum(motorway, column_speed) / motorway.Rows.Count;

            if (CheckUrban(urban, column_speed, column_time) && CheckMotorway(motorway, column_speed))
                return true;
            else
                return false;
        }

        public bool CheckDuration(DataTable dt, string column)
        {
            double duration = 0;

            Berechnungen.SortData(ref dt, column, false);

            duration = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][column]) / 60000;        //duration in minutes

            if (duration >= 90 && duration <= 120)
                return true;
            else
                return false;
        }

        public bool CheckDistanceComplete(DataTable dt, string column_speed, string column_distance)  
        {
            DataTable urban = new DataTable();
            DataTable rural = new DataTable();
            DataTable motorway = new DataTable();

            Berechnungen.SepIntervals(dt, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            double[] distances = new double[3];

            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            if (distances[0] >= 16 && distances[1] >= 16 && distances[2] >= 16)
                return true;
            else
                return false;
        }

        public bool CheckDistanceIntervals(DataTable urban, DataTable rural, DataTable motorway, string column_distance)
        {
            double[] distances = new double[3];

            CalcDistances(urban, rural, motorway, column_distance, ref distances);
            
            if (distances[0] >= 16 && distances[1] >= 16 && distances[2] >= 16)
                return true;
            else
                return false;
        }

        public bool CheckColdStart(DataTable dt, string column_speed, string column_time, string column_coolant)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();

            int i = 0;
            double max = 0, avg = 0, time_hold = 0, time_start = 0;

            Berechnungen.SortData(ref dt, column_time, false);
            temp = dt.Select("[" + column_time + "]" + " <=  300000").CopyToDataTable();

            max = (double)temp.Compute("MAX([" + column_speed + "])", "");
            avg = (double)temp.Compute("AVG([" + column_speed + "])", "");

            //Zur Berechnung ob innerhalb von 15s losgefahren
            while (Convert.ToDouble(temp.Rows[i][column_speed]) < 1)
                i++;

            time_start = Convert.ToDouble(temp.Rows[i][column_time]) / 1000;        //Startzeit in Sekunden nach Beginn

            temp = temp.Select("[" + column_speed + "]" + " < 1").CopyToDataTable();

            time_hold = (double)(temp.Rows.Count * 500 - 500) / 1000;       //Haltezeit in sekunden

            if (time_start <= 15 && time_hold <= 90 && avg >= 15 && avg <= 40 && max <= 60)
                return true;
            else
                return false;
        }

        //public void CheckAltitude()
    }
}
