﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

namespace DriversGuide
{
    class MeasurementFile
    {
        public string Filename;

        private DataTable dt = new DataTable();
        private DataTable LiveSimulation = new DataTable();
        DataTable units = new DataTable();
        DataTable dheaders = new DataTable();
        int SimualtionRowCount = 0;
        int rowCount = 0;
       
        /*
         Einlesen des Messfiles + Convertierung in 2 Datentables "dt" und "units"
         dt: 
            Datatabel im Double Format mit allen Messwerten nach Zeit geordnet
            Auf der Time Spalte liegt der PrimaryKey
            Spaltenname sind die Erste Zeile im Messfile.
         units:
            Datentabel mit den Einheiten im String Format
            Spaltnennamen sind die Erste Zeile im Messfile
                    
         
         */

        public DataTable ConvertCSVtoDataTable()   
        {
            
            StreamReader sr = new StreamReader(Filename);
            string[] headers = sr.ReadLine().Split('\t');
            string[] Units = sr.ReadLine().Split('\t');
            
            foreach (string header in headers)
            {
                dt.Columns.Add(header).DataType=(typeof (double));
                units.Columns.Add(header);               

            }
            units.Rows.Add(Units);

            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), "\t"); //",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();

                if ((Convert.ToDouble(rows[0]) % 1000) == 0)
                {


                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = Convert.ToDouble(rows[i]);
                    }
                    dt.Rows.Add(dr);
                }
                else
                {
                    //Reihe verwerfen
                }
            }
           // dt.PrimaryKey = new DataColumn[] { dt.Columns["Time"] };
            sr.Close();
            dt.AcceptChanges();
            return dt.Copy();
            
        }
        public void CSVToTable()
        {

            StreamReader sr = new StreamReader(Filename);
            string[] headers = sr.ReadLine().Split('\t');
            string[] Units = sr.ReadLine().Split('\t');



            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), "\t"); //",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();

                if ((Convert.ToDouble(rows[0]) % 1000) == 0)
                {


                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = Convert.ToDouble(rows[i]);
                    }
                    dt.Rows.Add(dr);
                }
                else
                {
                    //Reihe verwerfen
                }
            }
            //dt.PrimaryKey = new DataColumn[] { dt.Columns["Time"] };
            sr.Close();
            dt.AcceptChanges();
            

        }

        public DataTable ConvertLiveCSVtoDataTable()
        {
            dt.Clear();

            StreamReader sr = new StreamReader(Filename);
            string[] headers = sr.ReadLine().Split('\t');
            string[] Units = sr.ReadLine().Split('\t');

           

            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), "\t"); //",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();

                if ((Convert.ToDouble(rows[0]) % 1000) == 0)
                {


                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = Convert.ToDouble(rows[i]);
                    }
                    dt.Rows.Add(dr);
                }
                else
                {
                    //Reihe verwerfen
                }
            }
          //  dt.PrimaryKey = new DataColumn[] { dt.Columns["Time"] };
            sr.Close();
            return dt.Copy();

        }

        public DataRow AddSimulationRows()
        {
            DataRow drs = dt.NewRow();

            if (SimualtionRowCount < dt.Rows.Count)
            {
                drs = dt.Rows[SimualtionRowCount];
                SimualtionRowCount++;
            }
            
            
            return drs;
        }

        public DataRow AddSLiveRows()
        {
            DataRow drs = dt.NewRow();

            if (rowCount < dt.Rows.Count)
            {
                drs = dt.Rows[rowCount];
                rowCount++;
            }


            return drs;
        }

        public DataTable GetMeasurementData()   //Rückgabe der Messdaten
        {
            return dt.Copy();
        }

        public DataTable GetMeasurementUnits()  //Rücgabe der Einheiten
        {
            return units.Copy();
        }

        /* public List<List<string>> ReadMeasurementFile()
         {

             String[] content = File.ReadAllLines(Filename);

           // var MeasurementData = new List<List<string>>();

             Parallel.For(0, content.First().Split('\t').Length, i => MeasurementData.Add(content.Select(x => x.Split('\t')[i]).ToList()));
             return MeasurementData;

         }
         */
        /*
        public void SortList(string Spaltenbezeichnung)
        {
            

            MeasurementData.Sort()
        }

    */



        public MeasurementFile (string a)
        {
            Filename = a;
            dt.Clear();
        }

        public MeasurementFile ()
        {

        }

    }
}
