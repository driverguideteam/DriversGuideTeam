﻿using System;
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
    public partial class DriversGuideApp : Form
    {
        GPS FormGPS;
        General FormGeneral;

        int mov = 0;
        int movX;
        int movY;

        MeasurementFile Datei;
        Calculations Berechnung;
        Validation Gueltigkeit;
        public DataTable test = new DataTable();   //Datatable öffentl. machen (für Grafik)
        public DataTable units = new DataTable();  //öffentl. Datatable für Einheiten erstellen (für Grafik)
        private DataTable urban = new DataTable();
        private DataTable rural = new DataTable();
        private DataTable motorway = new DataTable();
        private DataTable values = new DataTable();
        public DataTable ColumnHeaders;   //Datatable für Spaltenüberschriften
        //DataSet dx = new DataSet();

        public DriversGuideApp()
        {
            InitializeComponent();
            lblHide.Parent = this;
            lblShow.Parent = this;
            lblHide.BringToFront();
            lblShow.BringToFront();
            //General myForm = new General(MainForm);
            ////myForm.TopLevel = false;
            //myForm.AutoScroll = true;
            //pnlContent.Controls.Add(myForm);
            ////myForm.FormBorderStyle = FormBorderStyle.None;
            //myForm.Show();
        }

        public DataTable GetCompleteDataTable()
        {
            return test;
        }

        public DataTable GetUrbanDataTable()
        {
            return urban;
        }

        public DataTable GetRuralDataTable()
        {
            return rural;
        }

        public DataTable GetMotorwayDataTable()
        {
            return motorway;
        }

        public DataTable GetUnitsDataTable()
        {
            return units;
        }

        public DataTable GetValuesDataTable()
        {
            return values;
        }

        //Initualize the values DataTable
        //********************************************************************************************        
        private void InitValueData()
        {
            values.Columns.Add("Klasse", typeof(String));
            values.Columns.Add("Verteilung");
            values.Columns.Add("Geschwindigkeit");
            values.Columns.Add("Strecke");
            values.Columns.Add("Dauer");
            values.Columns.Add("Haltezeit");
            values.Columns.Add("Hoechstgeschwindigkeit");
            values.Columns.Add("Kaltstart Hoechstgeschwindigkeit");
            values.Columns.Add("Kaltstart Durchschnittsgeschwindigkeit");
            values.Columns.Add("Kaltstart Haltezeit");

            values.Rows.Add();
            values.Rows.Add();
            values.Rows.Add();
            values.Rows.Add();
            values.Rows[0]["Klasse"] = "Gesamt";
            values.Rows[1]["Klasse"] = "Stadt";
            values.Rows[2]["Klasse"] = "Land";
            values.Rows[3]["Klasse"] = "Autobahn";
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            FormGeneral = new General(this);
            //myForm.TopLevel = false;
            FormGeneral.AutoScroll = true;
            pnlContent.Controls.Add(FormGeneral);
            //myForm.FormBorderStyle = FormBorderStyle.None;
            FormGeneral.Show();
            FormGeneral.Dock = DockStyle.Fill;
            lblHide.BackColor = Color.White;
            lblShow.BackColor = Color.White;
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            FormGPS = new GPS(this);
            //myForm.TopLevel = false;
            FormGPS.AutoScroll = true;
            pnlContent.Controls.Add(FormGPS);
            //myForm.FormBorderStyle = FormBorderStyle.None;
            FormGPS.Show();
            FormGPS.Dock = DockStyle.Fill;
            lblHide.BackColor = FormGPS.BackColor;
        }

        private void btnReadFile_Click(object sender, EventArgs e)
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
                units = Datei.GetMeasurementUnits();  //Ausgabe der Einheiten 
                // var listofData = Datei.ReadMeasurementFile();

                //txtMeasurement.Text = listofData[1][1].ToString();
                //berechnungDurchführenToolStripMenuItem.Enabled = true;
                //txtMeasurement.Text = "Berechnung durchführen bevor Grafik - Zeichnen möglich ist!";
            }
            else
            {
                //txtMeasurement.Text = "Fail";
                MessageBox.Show("Fail!");

            }

            /*--------------------------------------------------------------------------------------------
             * Code für Testzwecke! Zur Ferstigstellung zu entfernen!!*/

            //Init the value DataTable
            InitValueData();
            Berechnung = new Calculations();
            Gueltigkeit = new Validation();
            bool test1, testdur;
            string column_speed = "OBD_Vehicle_Speed_(PID_13)";
            string column_acc = "ai";
            string column_dynamic = "a*v";
            string column_distance = "di";
            string column_time = "Time";
            string column_coolant = "OBD_Engine_Coolant_Temperature_(PID_5)";
            double avgUrban = 0, avgRural = 0, avgMotorway = 0;
            double distrUrban = 0, distrRural = 0, distrMotorway = 0;
            double tripUrban = 0, tripRural = 0, tripMotorway = 0;

            test1 = Berechnung.CalcAll(test, column_speed, column_acc, column_dynamic, column_distance);
            testdur = Gueltigkeit.CheckValidity(test, column_speed, column_time, column_coolant, column_distance);

            Berechnung.SepIntervals(test, column_speed);
            Berechnung.GetIntervals(ref urban, ref rural, ref motorway);
            Berechnung.AddUnits(units);

            Berechnung.GetAvgSpeed(ref avgUrban, ref avgRural, ref avgMotorway);
            Berechnung.GetTripInt(ref tripUrban, ref tripRural, ref tripMotorway);
            Gueltigkeit.GetDistribution(ref distrUrban, ref distrRural, ref distrMotorway);

            values.Rows[1]["Geschwindigkeit"] = avgUrban;
            values.Rows[2]["Geschwindigkeit"] = avgRural;
            values.Rows[3]["Geschwindigkeit"] = avgMotorway;

            values.Rows[1]["Verteilung"] = distrUrban;
            values.Rows[2]["Verteilung"] = distrRural;
            values.Rows[3]["Verteilung"] = distrMotorway;

            values.Rows[0]["Strecke"] = (double)test.Compute("SUM([" + column_distance + "])", "") / 1000;
            values.Rows[1]["Strecke"] = tripUrban;
            values.Rows[2]["Strecke"] = tripRural;
            values.Rows[3]["Strecke"] = tripMotorway;

            values.Rows[0]["Dauer"] = Convert.ToDouble(test.Rows[test.Rows.Count - 1][column_time]) / 60000d;
            values.Rows[0]["Haltezeit"] = Gueltigkeit.GetHoldDurtation();
            values.Rows[0]["Hoechstgeschwindigkeit"] = Gueltigkeit.GetMaxSpeed();
            values.Rows[0]["Kaltstart Hoechstgeschwindigkeit"] = Gueltigkeit.GetMaxSpeedCold();
            values.Rows[0]["Kaltstart Durchschnittsgeschwindigkeit"] = Gueltigkeit.GetAvgSpeedCold();
            values.Rows[0]["Kaltstart Haltezeit"] = Gueltigkeit.GetTimeHoldCold();

            //grafikToolStripMenuItem.Enabled = true;
            //txtMeasurement.Text = "Berechnung durchgeführt!";
            MessageBox.Show("Berechnung durchgeführt!");
            pnlContent.Controls.Clear();
            FormGeneral = new General(this);
            //myForm.TopLevel = false;
            FormGeneral.AutoScroll = true;
            pnlContent.Controls.Add(FormGeneral);
            //myForm.FormBorderStyle = FormBorderStyle.None;
            FormGeneral.Show();
            FormGeneral.Dock = DockStyle.Fill;
            /*--------------------------------------------------------------------------------------------*/
            btnGraphic.Enabled = true;
            btnGPS.Enabled = true;
            btnOverview.Enabled = true;
            lblHide.BackColor = Color.White;
            lblShow.BackColor = Color.White;
        }

        private void btnGraphic_Click(object sender, EventArgs e)
        {
            //Anzeige MessageBox falls Datatable leer!
            if (test.Rows.Count == 0)
            {
                MessageBox.Show("Keine Daten geladen!");
            }
            //Öffnen des Diagramm-Forms, falls Datatable gefüllt
            else
            {
                Datenauswahl DataDiagram = new Datenauswahl(this);
                DataDiagram.Show();
            }
        }

        private void lblHide_Click(object sender, EventArgs e)
        {
            pnlSideBar.Hide();
            lblHide.Hide();
            lblShow.Show();
            lblShow.Left = 3;
            pnlContent.Left = 0;
            pnlContent.Width = ClientSize.Width;
        }

        private void lblShow_Click(object sender, EventArgs e)
        {
            lblShow.Hide();
            lblHide.Show();
            pnlSideBar.Show();
            pnlContent.Left = pnlSideBar.Width;
            pnlContent.Width = ClientSize.Width - pnlSideBar.Width;
        }

    }
}
