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
        //Variables and DataTables to store average values and DataTabels to
        private double avgSpeed_urban, avgSpeed_rural, avgSpeed_motorway;        
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
            //DataTable temp = dt.Clone();

            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (Convert.ToDouble(dt.Rows[i][column]) >= 0.1)
            //    {
            //        temp.ImportRow(dt.Rows[i]);
            //    }
            //}

            //dt = temp.Copy();            

            dt = dt.Select("[" + column + "] >= 0.1").CopyToDataTable();

            if (dt.Rows.Count >= 100)
                return true;
            else
                return false;
        }

        //Calculate the average of a specific column within a Datatable dt and return it
        //********************************************************************************************
        /*Parameters:
         *      - dt:       DataTable which contains dataset or column for calculating average
         *      - column:   string with the name of the column by which to calculate average
        */
        //********************************************************************************************
        //private double CalcAvg (DataTable dt, string column)
        //{
        //    //double avgVal = 0;

        //    //for (int i = 0; i < dt.Rows.Count; i++)
        //    //    avgVal += Convert.ToDouble(dt.Rows[i][column]);

        //    //avgVal /= dt.Rows.Count;

        //    //return avgVal;

        //    return (double)dt.Compute("AVG([" + column + "])", "");
        //}

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
        public void SepIntervals (DataTable dt, string column)
        {
            //Clone the structure of dt to the intervall DataTables
            //urban = dt.Clone();
            //rural = dt.Clone();
            //motorway = dt.Clone();          

            urban = dt.Select("[" + column + "] <=  60").CopyToDataTable();
            rural = dt.Select("[" + column + "] >  60 AND [" + column + "] <= 90").CopyToDataTable();
            motorway = dt.Select("[" + column + "] >  90").CopyToDataTable();
            //Seperate the DataTable dt into intervalls and store the seperatet data to the new intervalls
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (Convert.ToDouble(dt.Rows[i][column]) <= 60)
            //        urban.ImportRow(dt.Rows[i]);
            //    else if (Convert.ToDouble(dt.Rows[i][column]) > 60 && Convert.ToDouble(dt.Rows[i][column]) <= 90)
            //        rural.ImportRow(dt.Rows[i]);
            //    else
            //        motorway.ImportRow(dt.Rows[i]);
            //}
        }

        //Call method makePosCheckCount(...) and check if every intervall has more than 100 entries left after deleting negativ acceleration values  
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
            //avgSpeed_urban = CalcAvg(urban, column);
            //avgSpeed_rural = CalcAvg(rural, column);
            //avgSpeed_motorway = CalcAvg(motorway, column);
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

            dt_Interval.Columns.Add("Perzentil");

            SortData(ref dt_Interval, column);

            for (int i = firstRow; i < lastRow; i++)
            {
                dt_Interval.Rows[i]["Perzentil"] = (double)i / (lastRow - 1) * 100;

                if (Convert.ToDouble(dt_Interval.Rows[i]["Perzentil"]) > 95 && Convert.ToDouble(dt_Interval.Rows[i-1]["Perzentil"]) < 95)
                {
                    double x1, x2, y1, y2;

                    x1 = Convert.ToDouble(dt_Interval.Rows[i - 1]["Perzentil"]);
                    x2 = Convert.ToDouble(dt_Interval.Rows[i]["Perzentil"]);
                    y1 = Convert.ToDouble(dt_Interval.Rows[i - 1][column]);
                    y2 = Convert.ToDouble(dt_Interval.Rows[i][column]);

                    dynNF = y1 + (((95 - x1) / (x2 - x1)) * (y2 - y1));
                }
                else if (Convert.ToDouble(dt_Interval.Rows[i]["Perzentil"]) == 95)
                {
                    dynNF = Convert.ToDouble(dt_Interval.Rows[i][column]);
                }
            }

            return dynNF;
        }

        //Calculate RPA value for each intervall
        //********************************************************************************************
        /*Parameters:
         *      - dt_complete:      DataTable with the complete dataset 
         *      - dt_interval:     DataTable with the interval data
         *      - column_speed:     string with the name of the speed column
         *      - column_acc:       string with the name of the acceleration column
         *      - column_distance:  string with the name of the distance column
        */
        //********************************************************************************************
        public double CalcRPA(DataTable dt_complete, DataTable dt_Interval, string column_speed, string column_acc, string column_distance)
        {
            double sumDynamic = 0;
            double sumDistance = 0;

            //Sum up every product of velocity * acceleration of the intervall
            for (int i = 0; i < dt_Interval.Rows.Count; i++)
            {
                sumDynamic += (Convert.ToDouble(dt_Interval.Rows[i][column_speed]) * Convert.ToDouble(dt_Interval.Rows[i][column_acc]));
            }

            //Sum up the distance over the whole dataset
            for (int i = 0; i < dt_complete.Rows.Count; i++)
            {
                sumDistance += (Convert.ToDouble(dt_complete.Rows[i][column_distance]));
            }
            //Kommentar
            //Divide the sum of dynamic by the sum of distance
            return (sumDynamic / sumDistance);
        }
    }
}
