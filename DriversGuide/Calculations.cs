﻿using System;
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
    class Calculations
    {
        //Variables and DataTables to store average speed values, interval percentiles,
        //RPA values of the intervals, distances per interval and DataTables
        private double avgSpeed_urban, avgSpeed_rural, avgSpeed_motorway;
        private double speed_urban, speed_rural, speed_motorway;
        private double count_urban, count_rural, count_motorway;
        private double perUrban, perRural, perMotorway;
        private double RPAUrban, RPARural, RPAMotorway;
        private double distUrban, distRural, distMotorway;
        private double distrUrban, distrRural, distrMotorway;
        private string[] errors = new string[3];
        private string[] tips = new string[3];
        private DataTable urban;
        private DataTable rural;
        private DataTable motorway;

        double count = 0;

        //Delete every entry in DataTable < 0.1 and return number of remaining entries
        //********************************************************************************************
        /*Parameters:
         *      - dt:       DataTable which contains dataset or column to perform routine on
         *      - column:   string with the name of the column by which to perfom routine
        */
        //********************************************************************************************
        private bool MakePosCheckCount (ref DataTable dt, string column)  
        {
            //Copy only entries with an acceleration value greater than 0.1 m/s^2 to DataTable
            dt = dt.Select("[" + column + "] >= 0.1").CopyToDataTable();

            //if more than 100 entries remain, return true
            if (dt.Rows.Count >= 100)
                return true;
            else
                return false;
        }

        //Check if the data in the interval matches the 95 percentile and RPA criteria
        //if any of these criteria is not matched by the interval this method returns false
        //********************************************************************************************
        /*Parameters:
         *      - column_distance:   string with the name of the distance column
         *      - dt_interval:       DataTable which includes the interval data
         *      - avg_speed:         Average speed for this interval
         *      - percentileNF:      Ninetyfive Percentile for this interval
         *      - RPA_interval:      Relative Pos. Acc. value for this interval
        */
        //********************************************************************************************
        private bool CheckCritInterval (DataTable dt_interval, double avg_speed, double percentileNF, double RPA_interval)
        {
            if (avg_speed <= 74.6 && percentileNF > (0.136 * avg_speed + 14.44))
                return false;            
            else if (avg_speed > 74.6 && percentileNF > (0.0742 * avg_speed + 18.966))                
                return false;            
            else if (avg_speed <= 94.05 && RPA_interval < ((-0.0016 * avg_speed + 0.1755) / 3.6))
                return false;
            else if (avg_speed > 94.05 && RPA_interval < 0.025)
                return false;
            else
                return true;
        }

        //Call method CheckCritInterval(...) and check if criteria is matched in every interval
        //********************************************************************************************
        /*Parameters:
         *      - column_distance:   string with the name of the distance column    
        */
        //********************************************************************************************
        private bool CheckCriteria (DataTable urban, DataTable rural, DataTable motorway)
        {
            //If every interval matches criteria .. 
            if (CheckCritInterval(urban, avgSpeed_urban, perUrban, RPAUrban) &&
                CheckCritInterval(rural, avgSpeed_rural, perRural, RPARural) &&
                CheckCritInterval(motorway, avgSpeed_motorway, perMotorway, RPAMotorway))
            {
                // .. return true
                return true;
            }
            else
                return false;
        }

        //Calculate the average speeds per interval
        //********************************************************************************************
        /*Parameters:
         *      - column_distance:   string with the name of the distance column    
        */
        //********************************************************************************************
        public void CalcDistancesInterval (string column_distance)
        {
            if (urban.Rows.Count != 0)
                distUrban = (double)urban.Compute("SUM([" + column_distance + "])", "");
            else
                distUrban = 0;

            if (rural.Rows.Count != 0)
                distRural = (double)rural.Compute("SUM([" + column_distance + "])", "");
            else
                distRural = 0;

            if (motorway.Rows.Count != 0)
                distMotorway = (double)motorway.Compute("SUM([" + column_distance + "])", "");
            else
                distMotorway = 0;
        }

        //Perform a multiplikation by a choosen factor on the choosen column
        //only used for testing purposes
        //********************************************************************************************
        /*Parameters:
         *      - dt:               dataTable with the data
         *      - column:           which column the multiplikation is performed on
         *      - multiplier:       the faktor by which the multiplication is performed
        */
        //********************************************************************************************
        private void PerformMutliplikationOnColumn(ref DataTable dt, string column, double multiplier)
        {
            int firstRow = 0;
            int lastRow = dt.Rows.Count;
            double val;

            for (int i = firstRow; i < lastRow; i++)
            {
                //multiply each value in the choosen column by the choosen multiplier
                //and write it to the corresponding item in the dataTable
                val = Convert.ToDouble(dt.Rows[i][column]) * multiplier;
                dt.Rows[i][column] = val;
            }
        }

        //Calculation of the required values for distance, acceleration and dynamic
        //Values are added to the DataTable dt as new columns
        //********************************************************************************************
        /*Parameters:
         *      - dt:             DataTable which contains complete dataset for calculations
         *      - column_speed:   string with the name of the column by which the calculations are done
        */
        //********************************************************************************************
        public void CalcReq (ref DataTable dt, string column_speed, bool init)
        {
            int firstRow = 0;
            int lastRow = dt.Rows.Count;
            double strecke, beschl, dynamik;

            //Add the new columns 
            if (init)
            {
                dt.Columns.Add("di", typeof(Double));
                dt.Columns.Add("ai", typeof(Double));
                dt.Columns.Add("a*v", typeof(Double));
            }                     

            //Calculation of distance, acceleration and dynamic
            for (int i = firstRow; i < lastRow; i++)
            {
                //Calculate distance by integration
                strecke = Convert.ToDouble(dt.Rows[i][column_speed]) / 3.6;
                dt.Rows[i]["di"] = strecke;               
                 
                //Calculate acceleration by derivation
                if (lastRow > 1)
                {
                    if (i == firstRow)
                    {
                        beschl = Convert.ToDouble(dt.Rows[i + 1][column_speed]) / (2 * 3.6);
                    }
                    else if (i == lastRow - 1)
                    {
                        beschl = -Convert.ToDouble(dt.Rows[i - 1][column_speed]) / (2 * 3.6);
                    }
                    else
                    {
                        beschl = (Convert.ToDouble(dt.Rows[i + 1][column_speed]) - Convert.ToDouble(dt.Rows[i - 1][column_speed])) / (2 * 3.6);
                    }

                    dt.Rows[i]["ai"] = beschl;

                    //Calculate dynamic by multiplying velocity and acceleration value and dividing the product with 3.6
                    dynamik = Convert.ToDouble(dt.Rows[i][column_speed]) * Convert.ToDouble(dt.Rows[i]["ai"]) / 3.6;
                    dt.Rows[i]["a*v"] = dynamik;
                }                         
            }
        }

        //Calculation of the required values for distance optimized for LiveMode
        //Values are added to the DataTable dt as new columns
        //********************************************************************************************
        /*Parameters:
         *      - dt:             DataTable which contains complete dataset for calculations
         *      - column_speed:   string with the name of the column by which the calculations are done
         *      - init:           bool to determine if live Mode is running
         *      - interval:       double to determine how often percentiles are calculated
        */
        //********************************************************************************************
        public void CalcReqLive(ref DataTable dt, string column_speed, bool init, double interval)
        {
            int lastRow = dt.Rows.Count;
            double strecke;
            string column_dynamic = "a*v";
            string column_acc = "ai";

            //Add the new columns 
            if (init)
            {
                dt.Columns.Add("di", typeof(Double));
                dt.Columns.Add("ai", typeof(Double));
                dt.Columns.Add("a*v", typeof(Double));

                urban = dt.Clone();
                rural = dt.Clone();
                motorway = dt.Clone();
                
                //reset speed sums
                speed_urban = 0;
                speed_rural = 0;
                speed_motorway = 0;

                //reset data count for each interval
                count_urban = 0;
                count_rural = 0;
                count_motorway = 0;
            }

            //Calculation of distance
            if (lastRow > 0)
            {
                double speed = Convert.ToDouble(dt.Rows[lastRow - 1][column_speed]);

                strecke = speed / 3.6;
                dt.Rows[lastRow - 1]["di"] = strecke;

                //Add distance and speed to according interval
                if (speed <= 60)
                {
                    distUrban += strecke;
                    speed_urban += speed;
                    count_urban++;
                }
                else if (speed <= 90)
                {
                    distRural += strecke;
                    speed_rural += speed;
                    count_rural++;
                }
                else
                {
                    distMotorway += strecke;
                    speed_motorway += speed;
                    count_motorway++;
                }
            }

            if (lastRow > 1)
            {
                if (lastRow == 2)
                {
                    dt.Rows[lastRow - 2]["ai"] = Convert.ToDouble(dt.Rows[lastRow - 1][column_speed]) / (2 * 3.6);

                    //Calculate dynamic by multiplying velocity and acceleration value and dividing the product with 3.6
                    dt.Rows[lastRow - 2]["a*v"] = Convert.ToDouble(dt.Rows[lastRow - 2][column_speed]) * Convert.ToDouble(dt.Rows[lastRow - 2]["ai"]) / 3.6;
                }
                else
                {
                    dt.Rows[lastRow - 1]["ai"] = -Convert.ToDouble(dt.Rows[lastRow - 2][column_speed]) / (2 * 3.6);

                    //Calculate dynamic by multiplying velocity and acceleration value and dividing the product with 3.6
                    dt.Rows[lastRow - 1]["a*v"] = Convert.ToDouble(dt.Rows[lastRow - 1][column_speed]) * Convert.ToDouble(dt.Rows[lastRow - 1]["ai"]) / 3.6;
                }

                if (lastRow > 2)
                {
                    dt.Rows[lastRow - 2]["ai"] = (Convert.ToDouble(dt.Rows[lastRow - 1][column_speed]) - Convert.ToDouble(dt.Rows[lastRow - 3][column_speed])) / (2 * 3.6);

                    //Calculate dynamic by multiplying velocity and acceleration value and dividing the product with 3.6
                    dt.Rows[lastRow - 2]["a*v"] = Convert.ToDouble(dt.Rows[lastRow - 2][column_speed]) * Convert.ToDouble(dt.Rows[lastRow - 2]["ai"]) / 3.6;
                }                
            }

            //count data proccessed
            //if interval count data chunks are proccessed calculate percentiles per interval
            if (interval == 1 && count >= 20)
            {
                //seperate intervals
                SepIntervals(dt, column_speed);

                //make intervals positive
                if (urban.Rows.Count > 0)
                    MakePosCheckCount(ref urban, column_acc);

                if (rural.Rows.Count > 0)
                    MakePosCheckCount(ref rural, column_acc);

                if (motorway.Rows.Count > 0)
                    MakePosCheckCount(ref motorway, column_acc);

                //calculate the percentiles
                perUrban = CalcPercentile_Interval(ref urban, column_dynamic);
                perRural = CalcPercentile_Interval(ref rural, column_dynamic);
                perMotorway = CalcPercentile_Interval(ref motorway, column_dynamic);
            }
            if (interval > 1 && count == interval)
            {
                //reset count
                count = 0;
                
                //seperate intervals
                SepIntervals(dt, column_speed);

                //make intervals positive
                if (urban.Rows.Count > 0)
                    MakePosCheckCount(ref urban, column_acc);

                if (rural.Rows.Count > 0)
                    MakePosCheckCount(ref rural, column_acc);

                if (motorway.Rows.Count > 0)
                    MakePosCheckCount(ref motorway, column_acc);

                //calculate the percentiles
                perUrban = CalcPercentile_Interval(ref urban, column_dynamic);
                perRural = CalcPercentile_Interval(ref rural, column_dynamic);
                perMotorway = CalcPercentile_Interval(ref motorway, column_dynamic);
            }

            //calculate average speeds per interval
            avgSpeed_urban = speed_urban / count_urban;
            avgSpeed_rural = speed_rural / count_rural;
            avgSpeed_motorway = speed_motorway / count_motorway;

            count++;         
        }

        //Calc the percentage of the distances per interval compared to complete trip
        //Input only complete dataset, optimized for LiveMode
        //********************************************************************************************
        /*Parameters:
         *      - tripUrban:        double with the distance in urban interval
         *      - tripRural:        double with the distance in rural interval
         *      - tripMotorway:     double with the distance in motorway interval4
         *      - tripComplete:     double with the distance of the complete trip
        */
        //********************************************************************************************
        public void CalcDistributionLive(double tripUrban, double tripRural, double tripMotorway, double tripComplete)
        {
            //Calculate percentage per interval compared to complete trip
            distrUrban = tripUrban * 100 / tripComplete;
            distrRural = tripRural * 100 / tripComplete;
            distrMotorway = tripMotorway * 100 / tripComplete;
        }

        //Sort Data in DataTable according to a specific column by ascending order
        //********************************************************************************************
        /*Parameters:
         *      - dt:       DataTable which contains dataset or column to sort
         *      - column:   string with the name of the column by which to sort
         *      - units:    if true, the first row gets deletet from the dataTable
        */
        //********************************************************************************************
        public void SortData (ref DataTable dt, string column)
        {
            //Create temporary DataTable dtSort and clone the structure from DataTable dt
            //Set the datatype for the column to sort to double
            DataTable dtSort = dt.Clone();
            dtSort.Columns[column].DataType = Type.GetType("System.Double");

            foreach (DataRow dr in dt.Rows)
            {
                dtSort.ImportRow(dr);
            }
            dtSort.AcceptChanges();

            //Sort the Data in the specific column in ascending order
            DataView dv = dtSort.DefaultView;
            dv.Sort = column + " ASC";
            dtSort = dv.ToTable();

            //Overwrite Datatable dt with the sorted DataTable dtSort
            dt = dtSort.Copy();
        }

        //Seperate the DataTable into the intervalls city, rural and motorway
        //********************************************************************************************
        /*Parameters:
         *      - dt:       DataTable which contains dataset to seperate into intervals
         *      - column:   string with the name of the column by which to seperate
        */
        //********************************************************************************************
        public void SepIntervals (DataTable dt, string column_speed)
        {
            //Seperate DataTable dt into three intervals according to the speed values
            DataRow[] drU = dt.Select("[" + column_speed + "] <=  60");
            DataRow[] drR = dt.Select("[" + column_speed + "] >  60 AND [" + column_speed + "] <= 90");
            DataRow[] drM = dt.Select("[" + column_speed + "] >  90");
            if (drU.Length != 0)
                urban = dt.Select("[" + column_speed + "] <=  60").CopyToDataTable();
            else
                urban = dt.Clone();

            if (drR.Length != 0)
                rural = dt.Select("[" + column_speed + "] >  60 AND [" + column_speed + "] <= 90").CopyToDataTable();
            else
                rural = dt.Clone();

            if (drM.Length != 0)
                motorway = dt.Select("[" + column_speed + "] >  90").CopyToDataTable();
            else
                motorway = dt.Clone();
        }

        //Call method makePosCheckCount(...) and check if every interval has more than 100 entries
        //left after deleting negativ acceleration values  
        //********************************************************************************************
        /*Parameters:
         *      - column:       string with the name of the column used to perform check
        */
        //********************************************************************************************
        public bool PosCheck (string column)
        {
            if (MakePosCheckCount(ref urban, column) && MakePosCheckCount(ref rural, column) && MakePosCheckCount(ref motorway, column))
                return true;
            else
                return false;
        }

        //Add Units to unit DataTable
        //********************************************************************************************
        /*Parameters:
         *      - units:       DataTable which contains the units
        */
        //********************************************************************************************
        public void AddUnits (DataTable units)
        {
            //Add the new columns            
            units.Columns.Add("di");
            units.Columns.Add("ai");
            units.Columns.Add("a*v");
            units.Columns.Add("Perzentil");

            //Add the associated unit to the first row
            units.Rows[0]["di"] = "m";
            units.Rows[0]["ai"] = "m/s²";
            units.Rows[0]["a*v"] = "m²/s³";
            units.Rows[0]["Perzentil"] = "%";
        }

        //Calculate the average speed in every interval
        //********************************************************************************************
        /*Parameters:
         *      - column:       string with the name of the column used to calculate average
        */
        //********************************************************************************************
        public void CalcAvgSpeedInt (string column)
        {
            avgSpeed_urban = (double)urban.Compute("AVG([" + column + "])", "");
            avgSpeed_rural = (double)rural.Compute("AVG([" + column + "])", "");
            avgSpeed_motorway = (double)motorway.Compute("AVG([" + column + "])", "");
        }

        //Return the average speed values of each interval
        //********************************************************************************************
        /*Parameters:
         *      - avgUrban:         variable to store urban average interval speed
         *      - avgRural:         variable to store rural average interval speed
         *      - avgMotorway:      variable to store motorway average interval speed
        */
        //********************************************************************************************
        public void GetAvgSpeed (ref double avgUrban, ref double avgRural, ref double avgMotorway)
        {
            avgUrban = avgSpeed_urban;
            avgRural = avgSpeed_rural;
            avgMotorway = avgSpeed_motorway;
        }

        //Return the trip distance of each interval
        //********************************************************************************************
        /*Parameters:
         *      - urban:            variable to store urban average interval speed
         *      - rural:            variable to store rural average interval speed
         *      - motorway:         variable to store motorway average interval speed
        */
        //********************************************************************************************
        public void GetTripInt (ref double urban, ref double rural, ref double motorway)
        {
            urban = distUrban / 1000;
            rural = distRural / 1000;
            motorway = distMotorway / 1000;
        }

        //Return the interval DataTables
        //********************************************************************************************
        /*Parameters:
         *      - urban_data:       DataTable to store the urban interval data
         *      - rural_data:       DataTable to store the rural interval data
         *      - motorway_data:    DataTable to store the motorway interval data
        */
        //********************************************************************************************
        public void GetIntervals (ref DataTable urban_data, ref DataTable rural_data, ref DataTable motorway_data)
        {
            urban_data = urban.Copy();
            rural_data = rural.Copy();
            motorway_data = motorway.Copy();
        }

        //Return the error messages
        //********************************************************************************************
        /*Parameters:
         *      - errors:            string array with error messages
        */
        //********************************************************************************************
        public string[] GetErrors()
        {
            double bordUrban = 0;
            double bordRural = 0;
            double bordMotorway = 0;

            GetBordersPercentile(ref bordUrban, ref bordRural, ref bordMotorway);

            //determine if percentiles are above borders
            //if so return errors
            if (perUrban > bordUrban)
                errors[0] = "Stadt: Dynamik zu hoch";

            if (perRural > bordRural)
                errors[1] = "Land: Dynamik zu hoch";

            if (perMotorway > bordMotorway)
                errors[2] = "Autobahn: Dynamik zu hoch";

            return errors;
        }

        //Return the tips messages
        //********************************************************************************************
        /*Parameters:    
         *      - tips:              string array with tips messages
        */
        //********************************************************************************************
        public string[] GetTips()
        {
            double bordUrban = 0;
            double bordRural = 0;
            double bordMotorway = 0;

            GetBordersPercentile(ref bordUrban, ref bordRural, ref bordMotorway);

            //determine if percentiles are far from border
            //if so write tips
            if (perUrban <= bordUrban - 5)
                tips[0] = "Stadt: Dynamik erhöhen";

            if (perRural <= bordRural - 5)
                tips[1] = "Land: Dynamik erhöhen";

            if (perMotorway <= bordMotorway - 5)
                tips[2] = "Autobahn: Dynamik erhöhen";

            return tips;
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

        //Get the RPA values per interval
        //********************************************************************************************
        /*Parameters:
         *      - urban:            percentile border for the urban interval        
         *      - rural:            percentile border for the rural interval
         *      - motorway:         percentile border for the motorway interval
        */
        //********************************************************************************************
        public void GetBordersPercentile (ref double urban, ref double rural, ref double motorway)
        {
            if (avgSpeed_urban <= 74.6)
                urban = (0.136 * avgSpeed_urban + 14.44);
            else if (avgSpeed_urban > 74.6)
                urban = (0.0742 * avgSpeed_urban + 18.966);

            if (avgSpeed_rural <= 74.6)
                rural = (0.136 * avgSpeed_rural + 14.44);
            else if (avgSpeed_rural > 74.6)
                rural = (0.0742 * avgSpeed_rural + 18.966);

            if (avgSpeed_motorway <= 74.6)
                motorway = (0.136 * avgSpeed_motorway + 14.44);
            else if (avgSpeed_motorway > 74.6)
                motorway = (0.0742 * avgSpeed_motorway + 18.966);
        }

        //Calculate the percentile of each interval
        //********************************************************************************************
        /*Parameters:
         *      - dt_Interval:      DataTable with interval data
         *      - column:           string with the name of the column needed for the calculation
        */
        //********************************************************************************************
        public double CalcPercentile_Interval (ref DataTable dt_Interval, string column)
        {
            int firstRow = 0;
            int lastRow = dt_Interval.Rows.Count;
            double dynNF = 0;

            //Add new column "Perezentil" to dataTable
            dt_Interval.Columns.Add("Perzentil");

            //Sort Data asc according to the column needed
            SortData(ref dt_Interval, column);

            //Enter the correct percentile values for each row into the dataTable
            for (int i = firstRow; i < lastRow; i++)
            {
                dt_Interval.Rows[i]["Perzentil"] = (double)(i + 1) / lastRow * 100;

                //if no entry matches exactly 95 percent .. 
                if (Convert.ToDouble(dt_Interval.Rows[i]["Perzentil"]) > 95 && Convert.ToDouble(dt_Interval.Rows[i-1]["Perzentil"]) < 95)
                {
                    double x1, x2, y1, y2;

                    // .. interpolate the two surrounding entires and ..
                    x1 = Convert.ToDouble(dt_Interval.Rows[i - 1]["Perzentil"]);
                    x2 = Convert.ToDouble(dt_Interval.Rows[i]["Perzentil"]);
                    y1 = Convert.ToDouble(dt_Interval.Rows[i - 1][column]);
                    y2 = Convert.ToDouble(dt_Interval.Rows[i][column]);

                    // .. calculate the exact value of it 
                    dynNF = y1 + (((95 - x1) / (x2 - x1)) * (y2 - y1));
                }
                //if one entry matches exactly 95 percent ..
                else if (Convert.ToDouble(dt_Interval.Rows[i]["Perzentil"]) == 95)
                {
                    // .. write it to the variable
                    dynNF = Convert.ToDouble(dt_Interval.Rows[i][column]);
                }
            }

            //return the 95 percentile dynamic value
            return dynNF;
        }

        //Calculate the percentile for all three intervals by calling CalcPercentile_Interval methode
        //********************************************************************************************
        /*Parameters:
         *      - urban_data:       DataTable with urban data
         *      - rural_data:       DataTable with urban data
         *      - motorway_data:    DataTable with urban data
         *      - column:           string with the name of the column needed for the calculation
        */
        //********************************************************************************************
        public void CalcPercentile_Complete (ref DataTable urban_data, ref DataTable rural_data, ref DataTable motorway_data, string column)
        {
            perUrban = CalcPercentile_Interval(ref urban_data, column);
            perRural = CalcPercentile_Interval(ref rural_data, column);
            perMotorway = CalcPercentile_Interval(ref motorway_data, column);
        }

        //Calculate RPA value for each intervall
        //********************************************************************************************
        /*Parameters:
         *      - dt_complete:      DataTable with the complete dataset 
         *      - dt_interval:      DataTable with the interval data
         *      - deltaTime:        double with value of timedelta between entries
         *      - column_speed:     string with the name of the speed column
         *      - column_acc:       string with the name of the acceleration column
        */
        //********************************************************************************************
        public double CalcRPA (DataTable dt_Interval, double distIntComplete/*, string column_dynamic*/, string column_speed, string column_acc)
        {
            double sumDynamic = 0;
            
            //Sum up every product of velocity * acceleration in the interval
            sumDynamic = dt_Interval.AsEnumerable().Sum(r => ((r.Field<double>(column_speed) / 3.6) * r.Field<double>(column_acc)));
            
            //Divide the sum of dynamic by the sum of distance
            return (sumDynamic / distIntComplete);
        }

        //Return the 95% Percentile values
        //********************************************************************************************
        /*Parameters:
         *      - percentileUrban:       double variable to store urban percentile value
         *      - percentileRural:       double variable to store rural percentile value
         *      - percentileMotorway:    double variable to store motorway percentile value
        */
        //********************************************************************************************
        public void GetPercentiles (ref double percentileUrban, ref double percentileRural, ref double percentileMotorway)
        {
            percentileUrban = perUrban;
            percentileRural = perRural;
            percentileMotorway = perMotorway;
        }

        //One method for calculating everything with one call
        //and to check the fullfillment of the criteria (pecentile and RPA)
        //********************************************************************************************
        /*Parameters:
         *      - dt:               DataTable with the complete dataset
         *      - column_speed:     string with the name of the speed column
         *      - column_acc:       string with the name of the acceleration column
         *      - column_dynamic:   string with the name of the dynamic column
         *      - column_distance:  string with the name of the distance column
        */
        //********************************************************************************************
        public bool CalcAll (DataTable dt, string column_speed, string column_acc, string column_dynamic, string column_distance)
        {
            bool oHdrd, critMatch;       

            CalcReq(ref dt, column_speed, true);

            //PerformMutliplikationOnColumn(ref dt, column_speed, 2);

            SortData(ref dt, column_speed);
            SepIntervals(dt, column_speed);
            CalcDistancesInterval(column_distance);
            CalcAvgSpeedInt(column_speed);
            oHdrd = PosCheck(column_acc);
            perUrban = CalcPercentile_Interval(ref urban, column_dynamic);
            perRural = CalcPercentile_Interval(ref rural, column_dynamic);
            perMotorway = CalcPercentile_Interval(ref motorway, column_dynamic);
            RPAUrban = CalcRPA(urban, distUrban, column_speed, column_acc);
            RPARural = CalcRPA(rural, distRural, column_speed, column_acc);
            RPAMotorway = CalcRPA(motorway, distMotorway, column_speed, column_acc);
            critMatch = CheckCriteria(urban, rural, motorway);

            //return true if dataset in each interval is greater 100 entries
            //and percentile and RPA criteria are matched
            if (oHdrd && critMatch)
                return true;
            else
                return false;
        }
    }
}
