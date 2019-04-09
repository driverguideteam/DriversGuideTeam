using System;
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

        DataTable dt = new DataTable();
        DataTable units = new DataTable();
       
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

        private DataTable ConvertCSVtoDataTable()   
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
                for (int i = 0; i < headers.Length; i++)    
                {
                    dr[i] = Convert.ToDouble( rows[i]);
                }
                dt.Rows.Add(dr);
            }
            dt.PrimaryKey = new DataColumn[] { dt.Columns["Time"] };
            return dt;
        }

        public DataTable GetMeasurementData()   //Rückgabe der Messdaten
        {
            return dt;
        }

        public DataTable GetMeasurementUnits()  //Rücgabe der Einheiten
        {
            return units;
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
        }

        public MeasurementFile ()
        {

        }

    }
}
