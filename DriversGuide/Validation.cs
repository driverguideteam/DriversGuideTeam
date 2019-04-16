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
        //Create new object of class Calculations
        //needed for various methods in it
        Calculations Berechnungen = new Calculations();

        //Calculate distances drove by interval, return them in array as values in kilometers
        //********************************************************************************************
        /*Parameters:
         *      - urban:            DataTable with the urban dataset
         *      - rural:            DataTable with the rural dataset
         *      - motorway:         DataTable with the motorway dataset
         *      - column_distance:  string with the name of the distance column
         *      - distances:        double array for storing and handing back distance values
        */
        //********************************************************************************************
        private void CalcDistances (DataTable urban, DataTable rural, DataTable motorway, string column_distance, ref double[] distances)
        {
            distances[0] = (double)urban.Compute("SUM([" + column_distance + "])", "") / 1000;           
            distances[1] = (double)rural.Compute("SUM([" + column_distance + "])", "") / 1000;
            distances[2] = (double)motorway.Compute("SUM([" + column_distance + "])", "") / 1000;
        }

        //Check if urban criteria are matched
        //********************************************************************************************
        /*Parameters:
         *      - dt_urban:         DataTable with the urban dataset
         *      - column_speed:     string with the name of the speed column
         *      - column_time:      string with the name of the time column
        */
        //********************************************************************************************
        private bool CheckUrban (DataTable dt_urban, string column_speed, string column_time)
        {
            double avgSpeed = 0;
            double duration_interval = 0;
            double duration_hold = 0;
            double duration_ratio = 0;
            int countTime = 1;
            
            //Calculate the average speed in the interval and the duration of the trip in minutes
            avgSpeed = (double)dt_urban.Compute("SUM([" + column_speed + "])", "") / dt_urban.Rows.Count;
            duration_interval = (double)(dt_urban.Rows.Count - 1) / 60;

            //Copy only entries with a speed value lower than 1 km/h to DataTable
            //and sort DataTable by time
            dt_urban = dt_urban.Select("[" + column_speed + "]" + " < 1").CopyToDataTable();
            Berechnungen.SortData(ref dt_urban, column_time);

            //Check if single hold time was longer as 300 seconds
            for (int i = 1; i < dt_urban.Rows.Count; i++)
            {
                if (Convert.ToDouble(dt_urban.Rows[i][column_time]) - Convert.ToDouble(dt_urban.Rows[i - 1][column_time]) == 1000)
                    countTime++;
                else
                    countTime = 1;
            }

            //Calculate value of longest hold time 
            //and the ratio of the longest hold time to the duration of trip
            duration_hold = (double)(dt_urban.Rows.Count - 1) / 60;        
            duration_ratio = duration_hold * 100 / duration_interval;

            //if urban criteria matched return true
            if (avgSpeed >= 15 && avgSpeed <= 40 && duration_ratio >= 6 && duration_ratio <= 30 && countTime <= 120)
                return true;
            else
                return false;
        }

        //Check if motorway criteria are matched
        //********************************************************************************************
        /*Parameters:
         *      - dt_motorway:      DataTable with the motorway dataset
         *      - column_speed:     string with the name of the speed column
        */
        //********************************************************************************************
        private bool CheckMotorway (DataTable dt_motorway, string column_speed)
        {
            DataTable fasterOH = new DataTable();
            DataTable fasterHFF = new DataTable();
            fasterOH = dt_motorway.Clone();
            fasterHFF = dt_motorway.Clone();
            double max, min, duration, fasterOnehundred, tooFast = 0;

            //Get maximum and minimum speed in interval
            max = (double)dt_motorway.Compute("MAX([" + column_speed + "])", "");
            min = (double)dt_motorway.Compute("MIN([" + column_speed + "])", "");

            //Copy only entries with a speed value greater than 100 km/h to DataTable fasterOH
            //and calculate time driven with a speed greater 100 km/h
            fasterOH = dt_motorway.Select("[" + column_speed + "]" + " > 100").CopyToDataTable();       
            fasterOnehundred = (double)(fasterOH.Rows.Count - 1) / 60;         
            
            //if the maximum speed is greater 145 km/h and lower 160 km/h .. 
            if (max >= 145 && max <= 160)
            {
                // .. Copy only entries with a speed value greater than 145 km/h to DataTable fasterHFF
                //Calculate duration of motorway trip
                //Calculate time driven with a speed greater 145 km/h .. 
                // .. calculate ratio of time driven to fast in comparison to the duration
                fasterHFF = dt_motorway.Select("[" + column_speed + "]" + " >= 145").CopyToDataTable();
                duration = (double)(dt_motorway.Rows.Count - 1) / 60;
                tooFast = (double)(fasterHFF.Rows.Count - 1) / 60;
                tooFast *= 100 / duration;
            }

            //if criteria are matched return true
            if (min > 90 && max >= 110 && max <= 145 && fasterOnehundred >= 5)
                return true;
            else if (min > 90 && max >= 145 && max <= 160 && fasterOnehundred >= 5 && tooFast <= 3)
                return true;
            else
                return false;
        }

        //Check the percentage of the distances per interval compared to complete trip
        //Input only complete dataset
        //********************************************************************************************
        /*Parameters:
         *      - dt:               DataTable with the complete dataset
         *      - column_speed:     string with the name of the speed column
         *      - column_distance:  string with the name of the distance column
        */
        //********************************************************************************************
        public bool CheckDistributionComplete (DataTable dt, string column_speed, string column_distance)
        {
            DataTable urban = new DataTable();
            DataTable rural = new DataTable();
            DataTable motorway = new DataTable();
            double trip = 0;
            double[] distances = new double[3];

            //Seperate DataTable into intervals, using the methods of the Calculations class's object
            //Get the now seperated intervals
            Berechnungen.SepIntervals(dt, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            //Calculate and get the distances driven per interval
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            //Calculate distance of complete trip
            trip = (double)dt.Compute("SUM([" + column_distance + "])", "") / 1000;

            //Calculate percentage per interval compared to complete trip
            distances[0] *= 100 / trip;
            distances[1] *= 100 / trip;
            distances[2] *= 100 / trip;

            //if criteria are matched, return true
            if (distances[0] >= 29 && distances[1] <= 44 && distances[1] >= 23 && distances[1] <= 43 && distances[2] >= 23 && distances[2] <= 43)
                return true;
            else
                return false;
        }

        //Check the percentage of the distances per interval compared to complete trip
        //Input already seperated intervals
        //********************************************************************************************
        /*Parameters:
         *      - urban:            DataTable with the urban dataset
         *      - rural:            DataTable with the rural dataset
         *      - motorway:         DataTable with the motorway dataset
         *      - column_distance:  string with the name of the distance column
        */
        //********************************************************************************************
        public bool CheckDistributionIntervals(DataTable urban, DataTable rural, DataTable motorway, string column_distance)
        {
            double trip = 0;
            double[] distances = new double[3];

            //Calculate and get the distances driven per interval
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            //Calculate distance of complete trip
            trip = distances[0] + distances[1] + distances[2];

            //Calculate percentage per interval compared to complete trip
            distances[0] *= 100 / trip;
            distances[1] *= 100 / trip;
            distances[2] *= 100 / trip;

            //if criteria are matched, return true
            if (distances[0] >= 29 && distances[1] <= 44 && distances[1] >= 23 && distances[1] <= 43 && distances[2] >= 23 && distances[2] <= 43)
                return true;
            else
                return false;
        }

        //Check if speed criteria are matched
        //********************************************************************************************
        /*Parameters:
         *      - urban:            DataTable with the urban dataset        
         *      - motorway:         DataTable with the motorway dataset
         *      - column_speed:     string with the name of the speed column
         *      - column_time:      string with the name of the time column
        */
        //********************************************************************************************
        public bool CheckSpeeds(DataTable urban, DataTable motorway, string column_speed, string column_time)
        {
            //Call methods for cheching urban and motorway criteria, if they both return true,
            //this method returns true
            if (CheckUrban(urban, column_speed, column_time) && CheckMotorway(motorway, column_speed))
                return true;
            else
                return false;
        }

        //Check if duration criteria are matched
        //********************************************************************************************
        /*Parameters:
         *      - dt:               DataTable with the complete dataset
         *      - column_time:      string with the name of the time column
        */
        //********************************************************************************************
        public bool CheckDuration(DataTable dt, string column_time)
        {
            double duration = 0;

            //Sort DataTable by time
            Berechnungen.SortData(ref dt, column_time);

            //Calculate the complete trip's duration in minutes
            duration = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][column_time]) / 60000;

            //if critera are matched return true
            if (duration >= 90 && duration <= 120)
                return true;
            else
                return false;
        }

        //Check if distances per interval are greater than 16 kilometers
        //Input only complete dataset
        //********************************************************************************************
        /*Parameters:
         *      - dt:               DataTable with the complete dataset
         *      - column_speed:     string with the name of the speed column
         *      - column_distance:  string with the name of the distance column
        */
        //********************************************************************************************
        public bool CheckDistanceComplete(DataTable dt, string column_speed, string column_distance)  
        {
            DataTable urban = new DataTable();
            DataTable rural = new DataTable();
            DataTable motorway = new DataTable();
            double[] distances = new double[3];

            //Seperate DataTable into intervals, using the methods of the Calculations class's object
            //Get the now seperated intervals
            Berechnungen.SepIntervals(dt, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            //Calculate and get the distances driven per interval
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            //if criteria are matched return true
            if (distances[0] >= 16 && distances[1] >= 16 && distances[2] >= 16)
                return true;
            else
                return false;
        }

        //Check if distances per interval are greater than 16 kilometers
        //Input already seperated intervals
        //********************************************************************************************
        /*Parameters:
         *      - urban:            DataTable with the urban dataset
         *      - rural:            DataTable with the rural dataset
         *      - motorway:         DataTable with the motorway dataset
         *      - column_distance:  string with the name of the distance column
        */
        //********************************************************************************************
        public bool CheckDistanceIntervals(DataTable urban, DataTable rural, DataTable motorway, string column_distance)
        {
            double[] distances = new double[3];

            //Calculate and get the distances driven per interval
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            //if criteria are matched return true
            if (distances[0] >= 16 && distances[1] >= 16 && distances[2] >= 16)
                return true;
            else
                return false;
        }

        //Check if cold start criteria are matched
        //********************************************************************************************
        /*Parameters:
         *      - dt:               DataTable with the complete dataset
         *      - column_speed:     string with the name of the speed column
         *      - column_time:      string with the name of the time column
         *      - column_coolant:   string with the name of the coolant column
        */
        //********************************************************************************************
        public bool CheckColdStart(DataTable dt, string column_speed, string column_time, string column_coolant)
        {
            DataTable temp = new DataTable();
            temp = dt.Clone();            

            int i = 0;
            double max = 0, avg = 0, time_hold = 0, time_start = 0;

            //Sort DataTable by time
            //Copy only entires of first 5 minutes of trip to DataTable temp
            Berechnungen.SortData(ref dt, column_time);
            temp = dt.Select("[" + column_time + "] <=  300000").CopyToDataTable();

            //Check how much entries are in DataTable before coolant reaches 
            //70°C for the first time
            while (Convert.ToDouble(temp.Rows[i][column_coolant]) < 70)
                i++;

            //Copy only entries that were made before coolant reached 70°C
            temp = temp.Select("[" + column_time + "] <= " + temp.Rows[i][column_time]).CopyToDataTable();

            //Get maximum and minimum speed in cold start phase
            max = (double)temp.Compute("MAX([" + column_speed + "])", "");
            avg = (double)temp.Compute("AVG([" + column_speed + "])", "");

            i = 0;

            //To calculate if drove off within 15s
            while (Convert.ToDouble(temp.Rows[i][column_speed]) < 1)
                i++;

            //start time in seconds after start
            time_start = Convert.ToDouble(temp.Rows[i][column_time]) / 1000;        

            //Copy only entries with a speed value lower 1 km/h
            temp = temp.Select("[" + column_speed + "]" + " < 1").CopyToDataTable();

            //Calculate hold time in seconds
            time_hold = (double)(temp.Rows.Count - 1);      

            //if criteria are matched, return true
            if (time_start <= 15 && time_hold <= 90 && avg >= 15 && avg <= 40 && max <= 60)
                return true;
            else
                return false;
        }

        //One method for checking everything with one call
        //********************************************************************************************
        /*Parameters:
         *      - dt:               DataTable with the complete dataset
         *      - column_speed:     string with the name of the speed column
         *      - column_time:      string with the name of the time column
         *      - column_coolant:   string with the name of the coolant column
         *      - column_distance:  string with the name of the distance column
        */
        //********************************************************************************************
        public bool CheckValidity(DataTable dt, string column_speed, string column_time, string column_coolant, string column_distance)
        {
            DataTable urban = new DataTable();
            DataTable rural = new DataTable();
            DataTable motorway = new DataTable();

            urban = dt.Clone();
            rural = dt.Clone();
            motorway = dt.Clone();

            //Seperate DataTable into intervals, using the methods of the Calculations class's object
            //Get the now seperated intervals
            Berechnungen.SepIntervals(dt, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            //Call all methods for checking every criteria
            //if all of them return true, all criteria are matched and this
            //mthode returns true
            if (CheckDistanceComplete(dt, column_speed, column_distance) &&
                CheckDistributionComplete(dt, column_speed, column_distance) &&
                CheckDuration(dt, column_time) &&
                CheckSpeeds(urban, motorway, column_speed, column_time) &&
                CheckColdStart(dt, column_speed, column_time, column_coolant))
            {
                return true;
            }
            else
                return false;
        }

        //public void CheckAltitude()
    }
}
