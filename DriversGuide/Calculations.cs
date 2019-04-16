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
        private double perUrban, perRural, perMotorway;
        private double RPAUrban, RPARural, RPAMotorway;
        private double distUrban, distRural, distMotorway;
        private DataTable urban;
        private DataTable rural;
        private DataTable motorway;

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
        private void CalcDistancesInterval (string column_distance)
        {
            distUrban = (double)urban.Compute("SUM([" + column_distance + "])", "");
            distRural = (double)rural.Compute("SUM([" + column_distance + "])", "");
            distMotorway = (double)motorway.Compute("SUM([" + column_distance + "])", "");
        }

        //Calculation of the required values for distance, acceleration and dynamic
        //Values are added to the DataTable dt as new columns
        //********************************************************************************************
        /*Parameters:
         *      - dt:       DataTable which contains complete dataset for calculations
         *      - column:   string with the name of the column by which the calculations are done
        */
        //********************************************************************************************
        public void CalcReq (ref DataTable dt, string column)
        {
            int firstRow = 0;
            int lastRow = dt.Rows.Count;
            double strecke, beschl, dynamik;

            //Add the new columns 
            dt.Columns.Add(("di"), typeof(System.Double));
            dt.Columns.Add(("ai"), typeof(System.Double));
            dt.Columns.Add(("a*v"), typeof(System.Double));

            //Add the associated unit to the first row
            //dt.Rows[0]["di"] = "m";
            //dt.Rows[0]["ai"] = "m/s^2";
            //dt.Rows[0]["a*v"] = "m^2/s^3";

            //Calculation of distance, acceleration and dynamic
            for (int i = firstRow; i < lastRow; i++)
            {
                //Calculate distance by integration
                strecke = Convert.ToDouble(dt.Rows[i][column]) / 3.6;
                dt.Rows[i]["di"] = strecke;               
                 
                //Calculate acceleration by derivation
                if (i == firstRow)
                {
                    beschl = Convert.ToDouble(dt.Rows[i + 1][column]) / (2 * 3.6);
                }
                else if (i == lastRow - 1)
                {
                    beschl = -Convert.ToDouble(dt.Rows[i - 1][column]) / (2 * 3.6);
                }
                else
                {
                    beschl = (Convert.ToDouble(dt.Rows[i + 1][column]) - Convert.ToDouble(dt.Rows[i - 1][column])) / (2 * 3.6);
                }           

                dt.Rows[i]["ai"] = beschl;

                //Calculate dynamic by multiplying velocity and acceleration value and dividing the product with 3.6
                dynamik = Convert.ToDouble(dt.Rows[i][column]) * Convert.ToDouble(dt.Rows[i]["ai"])/ 3.6;
                dt.Rows[i]["a*v"] = dynamik;                
            }
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
            urban = dt.Select("[" + column_speed + "] <=  60").CopyToDataTable();
            rural = dt.Select("[" + column_speed + "] >  60 AND [" + column_speed + "] <= 90").CopyToDataTable();
            motorway = dt.Select("[" + column_speed + "] >  90").CopyToDataTable();
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
                dt_Interval.Rows[i]["Perzentil"] = (double)i / (lastRow - 1) * 100;

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
                //if one entry mathes exactly 95 percent ..
                else if (Convert.ToDouble(dt_Interval.Rows[i]["Perzentil"]) == 95)
                {
                    // .. write it to the variable
                    dynNF = Convert.ToDouble(dt_Interval.Rows[i][column]);
                }
            }

            //return the 95 percentile dynamic value
            return dynNF;
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
        public double CalcRPA (DataTable dt_Interval, double distIntComplete, double deltaTime/*, string column_dynamic*/, string column_speed, string column_acc)
        {
            double sumDynamic = 0;
            
            //Sum up every product of velocity * acceleration in the interval
            sumDynamic = dt_Interval.AsEnumerable().Sum(r => ((r.Field<double>(column_speed) / 3.6) * r.Field<double>(column_acc)) * deltaTime);
            
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
            
            CalcReq(ref dt, column_speed);
            SortData(ref dt, column_speed);
            SepIntervals(dt, column_speed);
            CalcDistancesInterval(column_distance);
            CalcAvgSpeedInt(column_speed);
            oHdrd = PosCheck(column_acc);
            perUrban = CalcPercentile_Interval(ref urban, column_dynamic);
            perRural = CalcPercentile_Interval(ref rural, column_dynamic);
            perMotorway = CalcPercentile_Interval(ref motorway, column_dynamic);
            RPAUrban = CalcRPA(urban, distUrban, 0.5, column_speed, column_acc);
            RPARural = CalcRPA(rural, distRural, 0.5, column_speed, column_acc);
            RPAMotorway = CalcRPA(motorway, distMotorway, 0.5, column_speed, column_acc);
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
