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

        double distrUrban, distrRural, distrMotorway;
        double duration_hold = 0;
        double maxSpeed = 0;
        double maxSpeedCold = 0;
        double avgSpeedCold = 0;
        double holdTimeCold = 0;
        double fasterOnehundred = 0;
        List<string> error = new List<string>();

        DataTable errors = new DataTable();

        public void InitErrorsDt ()
        {
            errors.Columns.Add("Distance", typeof(string));
            errors.Columns.Add("Distribution", typeof(string));
            errors.Columns.Add("Duration", typeof(string));
            errors.Columns.Add("Speeds", typeof(string));
            errors.Columns.Add("ColdStart", typeof(string));
            errors.Columns.Add("Other", typeof(string));

            errors.Rows.Add();
            errors.Rows.Add();
            errors.Rows.Add();
            errors.Rows.Add();
        }

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
        private void CalcDistances(DataTable urban, DataTable rural, DataTable motorway, string column_distance, ref double[] distances)
        {
            if (urban.Rows.Count != 0)
                distances[0] = (double)urban.Compute("SUM([" + column_distance + "])", "") / 1000;
            else
                distances[0] = 0;

            if (rural.Rows.Count != 0)
                distances[1] = (double)rural.Compute("SUM([" + column_distance + "])", "") / 1000;
            else
                distances[1] = 0;

            if (motorway.Rows.Count != 0)
                distances[2] = (double)motorway.Compute("SUM([" + column_distance + "])", "") / 1000;
            else
                distances[2] = 0;
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
            duration_hold = 0;
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
            double min, duration, tooFast = 0;

            //Get maximum and minimum speed in interval
            maxSpeed = (double)dt_motorway.Compute("MAX([" + column_speed + "])", "");
            min = (double)dt_motorway.Compute("MIN([" + column_speed + "])", "");

            //Copy only entries with a speed value greater than 100 km/h to DataTable fasterOH
            //and calculate time driven with a speed greater 100 km/h
            fasterOH = dt_motorway.Select("[" + column_speed + "]" + " > 100").CopyToDataTable();       
            fasterOnehundred = (double)(fasterOH.Rows.Count - 1) / 60;         
            
            //if the maximum speed is greater 145 km/h and lower 160 km/h .. 
            if (maxSpeed >= 145 && maxSpeed <= 160)
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
            if (min > 90 && maxSpeed >= 110 && maxSpeed <= 145 && fasterOnehundred >= 5)
                return true;
            else if (min > 90 && maxSpeed >= 145 && maxSpeed <= 160 && fasterOnehundred >= 5 && tooFast <= 3)
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
            bool con1 = false;
            bool con2 = false;
            bool con3 = false;
            bool con4 = false;
            bool con5 = false;
            bool con6 = false;

            //Seperate DataTable into intervals, using the methods of the Calculations class's object
            //Get the now seperated intervals
            Berechnungen.SepIntervals(dt, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            //Calculate and get the distances driven per interval
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            //Calculate distance of complete trip
            if (dt.Rows.Count != 0)
                trip = (double)dt.Compute("SUM([" + column_distance + "])", "") / 1000;
            else
                trip = 0;

            //Calculate percentage per interval compared to complete trip
            distrUrban = distances[0] * 100 / trip;
            distrRural = distances[1] * 100 / trip;
            distrMotorway = distances[2] * 100 / trip;

            //if criteria are matched, return true, otherwise define errors
            if (distrUrban >= 29)
                con1 = true;
            else
                errors.Rows[0]["Distribution"] = "Stadtanteil zu gering";

            if (distrUrban <= 44)
                con2 = true;
            else
                errors.Rows[0]["Distribution"] = "Stadtanteil zu hoch";

            if (distrRural >= 23)
                con3 = true;
            else
                errors.Rows[1]["Distribution"] = "Landanteil zu gering";

            if (distrRural <= 43)
                con4 = true;
            else
                errors.Rows[1]["Distribution"] = "Landanteil zu hoch";

            if (distrMotorway >= 23)
                con5 = true;
            else
                errors.Rows[2]["Distribution"] = "Autobahnanteil zu gering";

            if (distrMotorway <= 43)
                con6 = true;
            else
                errors.Rows[2]["Distribution"] = "Autobahnanteil zu hoch";

            if (con1 && con2 && con3 && con4 && con5 && con6)
                return true;
            else return false;

            //if criteria are matched, return true
            //if (distrUrban >= 29 && distrUrban <= 44 && distrRural >= 23 && distrRural <= 43 && distrMotorway >= 23 && distrMotorway <= 43)
            //    return true;
            //else
            //    return false;
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
            bool con1 = false;
            bool con2 = false;
            bool con3 = false;
            bool con4 = false;
            bool con5 = false;
            bool con6 = false;

            //Calculate and get the distances driven per interval
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            //Calculate distance of complete trip
            trip = distances[0] + distances[1] + distances[2];

            //Calculate percentage per interval compared to complete trip
            distrUrban = distances[0] * 100 / trip;
            distrRural = distances[1] * 100 / trip;
            distrMotorway = distances[2] * 100 / trip;

            //if criteria are matched, return true, otherwise define errors
            if (distrUrban >= 29)
                con1 = true;
            else
                errors.Rows[0]["Distribution"] = "Stadtanteil zu gering";

            if (distrUrban <= 44)
                con2 = true;
            else
                errors.Rows[0]["Distribution"] = "Stadtanteil zu hoch";

            if (distrRural >= 23)
                con3 = true;
            else
                errors.Rows[1]["Distribution"] = "Landanteil zu gering";

            if (distrRural <= 43)
                con4 = true;
            else
                errors.Rows[1]["Distribution"] = "Landanteil zu hoch";

            if (distrMotorway >= 23)
                con5 = true;
            else
                errors.Rows[2]["Distribution"] = "Autobahnanteil zu gering";

            if (distrMotorway <= 43)
                con6 = true;
            else
                errors.Rows[2]["Distribution"] = "Autobahnanteil zu hoch";

            if (con1 && con2 && con3 && con4 && con5 && con6)
                return true;
            else return false;

            //if (distrUrban >= 29 && distrUrban <= 44 && distrRural >= 23 && distrRural <= 43 && distrMotorway >= 23 && distrMotorway <= 43)
            //    return true;
            //else
            //    return false;
        }

        //Get the distribution values per interval
        //********************************************************************************************
        /*Parameters:
         *      - urban:            Distribution in percent for the urban interval        
         *      - rural:            Distribution in percent for the rural interval
         *      - motorway:         Distribution in percent for the motorway interval
        */
        //********************************************************************************************
        public void GetDistribution (ref double urban, ref double rural, ref double motorway)
        {
            urban = distrUrban;
            rural = distrRural;
            motorway = distrMotorway;
        }

        //Get the hold time duration of the trip
        //********************************************************************************************
        /*Parameters:
         *      - duration_hold:     Hold time duration
        */
        //********************************************************************************************
        public double GetHoldDurtation()
        {
            return duration_hold;
        }

        //Get the maximum speed value
        //********************************************************************************************
        /*Parameters:
         *      - maxSpeed:          Maximum speed measured
        */
        //********************************************************************************************
        public double GetMaxSpeed()
        {
            return maxSpeed;
        }

        //Get the maximum speed value
        //********************************************************************************************
        /*Parameters:
         *      - maxSpeedCold:      Maximum speed in cold start phase measured
        */
        //********************************************************************************************
        public double GetMaxSpeedCold()
        {
            return maxSpeedCold;
        }

        //Get the average speed value in cold start phase
        //********************************************************************************************
        /*Parameters:
         *      - avgSpeedCold:      Average speed in cold start phase measured
        */
        //********************************************************************************************
        public double GetAvgSpeedCold()
        {
            return avgSpeedCold;
        }

        //Get the hold time during cold start phase
        //********************************************************************************************
        /*Parameters:
         *      - holdTimeCold:     Hold time duration
        */
        //********************************************************************************************
        public double GetTimeHoldCold()
        {
            return holdTimeCold;
        }

        //Get the time spent driving faster onehundred km/h
        //********************************************************************************************
        /*Parameters:
         *      - fasterOnehundred: Time droven faster than 100 km/h
        */
        //********************************************************************************************
        public double GetTimeFasterHundred()
        {
            return fasterOnehundred;
        }

        //Get list of error messages
        //********************************************************************************************
        /*Parameters:
         *      - error:            list with error messages
        */
        //********************************************************************************************
        public List<string> GetErrors1()
        {
            return error;
        }

        //Get dataTable with error messages
        //********************************************************************************************
        /*Parameters:
         *      - errors:           dataTable with error messages
        */
        //********************************************************************************************
        public DataTable GetErrors()
        {
            return errors.Copy();
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
            //if (CheckUrban(urban, column_speed, column_time) && CheckMotorway(motorway, column_speed))
            //    return true;
            //else
            //    return false;
            bool stateUrban = false;
            bool stateMotorway = false;

            if (CheckUrban(urban, column_speed, column_time))
                stateUrban = true;
            else
                stateUrban = false;

            if (CheckMotorway(motorway, column_speed))
                stateMotorway = true;
            else
                stateMotorway = false;

            if (stateUrban && stateMotorway)
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
            bool con1 = false;
            bool con2 = false;

            //Sort DataTable by time
            Berechnungen.SortData(ref dt, column_time);

            //Calculate the complete trip's duration in minutes
            duration = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][column_time]) / 60000;

            //if critera are matched return true, otherwise define error
            if (duration >= 90)
                con1 = true;
            else
                errors.Rows[0]["Duration"] = "Fahrzeit zu gering";

            if (duration <= 120)
                con2 = true;
            else
                errors.Rows[0]["Duration"] = "Fahrzeit zu lang";

            if (con1 && con2)
                return true;
            else
                return false;

            //if (duration >= 90 && duration <= 120)
            //    return true;
            //else
            //    return false;
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
            bool distUrban = false;
            bool distRural = false;
            bool distMotorway = false;

            //Seperate DataTable into intervals, using the methods of the Calculations class's object
            //Get the now seperated intervals
            Berechnungen.SepIntervals(dt, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            //Calculate and get the distances driven per interval
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            //if criteria are matched return true, otherwise define error
            if (distances[0] >= 16)
                distUrban = true;
            else
                errors.Rows[0]["Distance"] = "Strecke Stadt zu gering";

            if (distances[1] >= 16)
                distRural = true;
            else
                errors.Rows[1]["Distance"] = "Strecke Land zu gering";

            if (distances[2] >= 16)
                distMotorway = true;
            else
                errors.Rows[2]["Distance"] = "Strecke Autobahn zu gering";

            if (distUrban && distRural && distMotorway)
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
            bool distUrban = false;
            bool distRural = false;
            bool distMotorway = false;

            //Calculate and get the distances driven per interval
            CalcDistances(urban, rural, motorway, column_distance, ref distances);

            //if criteria are matched return true, otherwise define error
            if (distances[0] >= 16)
                distUrban = true;
            else
                errors.Rows[0]["Distance"] = "Strecke Stadt zu gering";

            if (distances[1] >= 16)
                distRural = true;
            else
                errors.Rows[1]["Distance"] = "Strecke Land zu gering";

            if (distances[2] >= 16)
                distMotorway = true;
            else
                errors.Rows[2]["Distance"] = "Strecke Autobahn zu gering";

            if (distUrban && distRural && distMotorway)
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
            bool con1 = false;
            bool con2 = false;
            bool con3 = false;
            bool con4 = false;
            bool con5 = false;

            int i = 0;
            double time_start = 0;

            //Sort DataTable by time
            //Copy only entires of first 5 minutes of trip to DataTable temp
            Berechnungen.SortData(ref dt, column_time);
            temp = dt.Select("[" + column_time + "] <=  300000").CopyToDataTable();

            //Check how much entries are in DataTable before coolant reaches 
            //70°C for the first time
            while (i < temp.Rows.Count - 1 && Convert.ToDouble(temp.Rows[i][column_coolant]) < 70)
                i++;

            //Copy only entries that were made before coolant reached 70°C
            temp = temp.Select("[" + column_time + "] <= " + temp.Rows[i][column_time]).CopyToDataTable();

            //Get maximum and minimum speed in cold start phase
            maxSpeedCold = (double)temp.Compute("MAX([" + column_speed + "])", "");
            avgSpeedCold = (double)temp.Compute("AVG([" + column_speed + "])", "");

            i = 0;

            //To calculate if drove off within 15s
            while (Convert.ToDouble(temp.Rows[i][column_speed]) < 1)
                i++;

            //start time in seconds after start
            time_start = Convert.ToDouble(temp.Rows[i][column_time]) / 1000;        

            //Copy only entries with a speed value lower 1 km/h
            temp = temp.Select("[" + column_speed + "]" + " < 1").CopyToDataTable();

            //Calculate hold time in seconds
            holdTimeCold = (double)(temp.Rows.Count - 1);

            //if criteria are matched return true, otherwise define error
            if (time_start <= 15)
                con1 = true;
            else
                errors.Rows[0]["ColdStart"] = "Zu spaet losgefahren";

            if (holdTimeCold <= 90)
                con2 = true;
            else
                errors.Rows[1]["ColdStart"] = "Standzeit zu lang";

            if (avgSpeedCold >= 15)
                con3 = true;
            else
                errors.Rows[2]["ColdStart"] = "Durchschnittsgeschwindigkeit zu gering";

            if (avgSpeedCold <= 40)
                con4 = true;
            else
                errors.Rows[2]["ColdStart"] = "Durchschnittsgeschwindigkeit zu hoch";

            if (maxSpeedCold <= 60)
                con5 = true;
            else
                errors.Rows[3]["ColdStart"] = "Zu schnell gefahren";

            if (con1 && con2 && con3 && con4 && con5)
                return true;
            else
                return false;
            //if criteria are matched, return true
            //if (time_start <= 15 && holdTimeCold <= 90 && avgSpeedCold >= 15 && avgSpeedCold <= 40 && maxSpeedCold <= 60)
            //    return true;
            //else
            //    return false;
        }

        public bool CheckAltitude(ref DataTable dt, string column_altitude, string column_distance, bool init)
        {
            double firstVal = Convert.ToDouble(dt.Rows[0][column_altitude]);
            double lastVal = Convert.ToDouble(dt.Rows[dt.Rows.Count - 1][column_altitude]);
            double distBefore = 0;
            double distAct = 0;
            double distAfter = 0;
            double heightBefore = 0;
            double heightAfter = 0;

            Berechnungen.SortData(ref dt, column_distance);

            int firstRow = 0;
            int lastRow = dt.Rows.Count - 1;

            if (init)
            {
                dt.Columns.Add("hInt", typeof(Double));
            }

            for (int i = firstRow; i <= lastRow; i++)
            {
                distAct += Convert.ToDouble(dt.Rows[i][column_distance]);
                distBefore = distAct - Convert.ToDouble(dt.Rows[i][column_distance]);
                if (i < lastRow)
                {
                    distAfter = distAct + Convert.ToDouble(dt.Rows[i + 1][column_distance]);
                    heightAfter = Convert.ToDouble(dt.Rows[i + 1][column_altitude]);
                }
                
                if (i > firstRow)
                    heightBefore = Convert.ToDouble(dt.Rows[i - 1][column_altitude]);

                dt.Rows[i]["hInt"] = heightBefore + ((heightAfter - heightBefore) / (distAfter - distBefore)) * (distAct - distBefore);              
            }

            if (Math.Abs(firstVal - lastVal) > 100)
                return false;
            else
                return true;
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

            bool stateDistance = false;
            bool stateDistribution = false;
            bool stateDuration = false;
            bool stateSpeed = false;
            bool stateCold = false;

            urban = dt.Clone();
            rural = dt.Clone();
            motorway = dt.Clone();
            InitErrorsDt();

            //Seperate DataTable into intervals, using the methods of the Calculations class's object
            //Get the now seperated intervals
            Berechnungen.SepIntervals(dt, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            //Call all methods for checking every criteria
            //if all of them return true, all criteria are matched and this
            //methode returns true
            //if (CheckDistanceComplete(dt, column_speed, column_distance) &&
            //    CheckDistributionComplete(dt, column_speed, column_distance) &&
            //    CheckDuration(dt, column_time) &&
            //    CheckSpeeds(urban, motorway, column_speed, column_time) &&
            //    CheckColdStart(dt, column_speed, column_time, column_coolant))
            //{
            //    return true;
            //}
            //else
            //    return false;

            if (CheckDistanceComplete(dt, column_speed, column_distance))
            {
                stateDistance = true;
            }
            else
            {
                stateDistance = false;
                error.Add("Distance error");
            }

            if (CheckDistributionComplete(dt, column_speed, column_distance))
            {
                stateDistribution = true;
            }
            else
            {
                stateDistribution = false;
                error.Add("Distribution error");
            }

            if (CheckDuration(dt, column_time))
            {
                stateDuration = true;
            }
            else
            {
                stateDuration = false;
                error.Add("Duration error");
            }

            if (CheckSpeeds(urban, motorway, column_speed, column_time))
            {
                stateSpeed = true;
            }
            else
            {
                stateSpeed = false;
                error.Add("Speed error");
            }

            if (CheckColdStart(dt, column_speed, column_time, column_coolant))
            {
                stateCold = true;
            }
            else
            {
                stateCold = false;
                error.Add("Cold Start error");
            }

            //CheckAltitude(ref dt, "GPS_Altitude", column_distance, true);

            if (stateDistance && stateDistribution && stateDuration && stateSpeed && stateCold)
                return true;
            else
                return false;
        }
    }
}
