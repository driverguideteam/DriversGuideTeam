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
                for (int i = 0; i < headers.Length; i++)    //Einheiten weglassen
                {
                    dr[i] = Convert.ToDouble( rows[i]);
                }
                dt.Rows.Add(dr);
            }
            dt.PrimaryKey = new DataColumn[] { dt.Columns["Time"] };
            return dt;
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
