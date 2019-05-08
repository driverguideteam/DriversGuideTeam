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
    public partial class Overview : Form
    {
        private DriversGuideMain MainForm;
        private Calculations Berechnungen = new Calculations();

        public Overview(DriversGuideMain caller)
        {
            MainForm = caller;
            InitializeComponent();
            ShowData();           
        }   

        private void ColorTextBoxTrip(TextBox txtbx, DataTable data, int index)
        {
            if (Convert.ToDouble(data.Rows[index]["Strecke"]) < 16)
                txtbx.BackColor = Color.Red;
            else if (Convert.ToDouble(data.Rows[index]["Strecke"]) <= 20)
<<<<<<< HEAD
                txtbx.BackColor = Color.Yellow;
            else
                txtbx.BackColor = Color.Green;
        }

        private void ShowData()
        {
            DataTable values = new DataTable();

            values = MainForm.GetValuesDataTable();

            distUrban.Text = Convert.ToDouble(values.Rows[1]["Verteilung"]).ToString("#.00");
            distRural.Text = Convert.ToDouble(values.Rows[2]["Verteilung"]).ToString("#.00");
            distMotorway.Text = Convert.ToDouble(values.Rows[3]["Verteilung"]).ToString("#.00");

            if (Convert.ToDouble(values.Rows[1]["Verteilung"]) > 44 || Convert.ToDouble(values.Rows[1]["Verteilung"]) < 29)
                distUrban.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[1]["Verteilung"]) >= 41.5d || Convert.ToDouble(values.Rows[1]["Verteilung"]) <= 31.5d)
                distUrban.BackColor = Color.Yellow;            
            else
                distUrban.BackColor = Color.Green;
=======
            {
                txtbx.BackColor = Color.Yellow;
                txtbx.ForeColor = Color.Black;
            }
            else
            {
                txtbx.BackColor = Color.Green;
                txtbx.ForeColor = Color.White;
            }
        }

        private void ShowData()
        {
            DataTable values = new DataTable();

            values = MainForm.GetValuesDataTable();

            distUrban.TextAlign = HorizontalAlignment.Center;
            distRural.TextAlign = HorizontalAlignment.Center;
            distMotorway.TextAlign = HorizontalAlignment.Center;

            distUrban.Text = Convert.ToDouble(values.Rows[1]["Verteilung"]).ToString("#.00") + "%";
            distRural.Text = Convert.ToDouble(values.Rows[2]["Verteilung"]).ToString("#.00") + "%";
            distMotorway.Text = Convert.ToDouble(values.Rows[3]["Verteilung"]).ToString("#.00") + "%";

            if (Convert.ToDouble(values.Rows[1]["Verteilung"]) > 44 || Convert.ToDouble(values.Rows[1]["Verteilung"]) < 29)
            {
                distUrban.BackColor = Color.Red;
                distUrban.ForeColor = Color.White;
            }
            else if (Convert.ToDouble(values.Rows[1]["Verteilung"]) >= 41.5d || Convert.ToDouble(values.Rows[1]["Verteilung"]) <= 31.5d)
            {
                distUrban.BackColor = Color.Yellow;
                distUrban.ForeColor = Color.Black;
            }
            else
            {
                distUrban.BackColor = Color.Green;
                distUrban.ForeColor = Color.White;
            }

            if (Convert.ToDouble(values.Rows[2]["Verteilung"]) > 43 || Convert.ToDouble(values.Rows[2]["Verteilung"]) < 23)
            {
                distRural.BackColor = Color.Red;
                distRural.ForeColor = Color.White;
            }
            else if (Convert.ToDouble(values.Rows[2]["Verteilung"]) >= 41.5d || Convert.ToDouble(values.Rows[2]["Verteilung"]) <= 31.5d)
            {
                distRural.BackColor = Color.Yellow;
                distRural.ForeColor = Color.Black;
            }
            else
            {
                distRural.BackColor = Color.Green;
                distRural.ForeColor = Color.White;
            }

            if (Convert.ToDouble(values.Rows[3]["Verteilung"]) > 43 || Convert.ToDouble(values.Rows[3]["Verteilung"]) < 23)
            {
                distMotorway.BackColor = Color.Red;
                distMotorway.ForeColor = Color.White;
            }
            else if (Convert.ToDouble(values.Rows[3]["Verteilung"]) >= 41.5d || Convert.ToDouble(values.Rows[3]["Verteilung"]) <= 31.5d)
            {
                distMotorway.BackColor = Color.Yellow;
                distMotorway.ForeColor = Color.Black;
            }
            else
            {
                distMotorway.BackColor = Color.Green;
                distMotorway.ForeColor = Color.White;
            }
                
            tripUrban.TextAlign = HorizontalAlignment.Center;
            tripRural.TextAlign = HorizontalAlignment.Center;
            tripMotorway.TextAlign = HorizontalAlignment.Center;

            tripUrban.Text = Convert.ToDouble(values.Rows[1]["Strecke"]).ToString("#.00") + " km";
            tripRural.Text = Convert.ToDouble(values.Rows[2]["Strecke"]).ToString("#.00") + " km";
            tripMotorway.Text = Convert.ToDouble(values.Rows[3]["Strecke"]).ToString("#.00") + " km";
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45

            if (Convert.ToDouble(values.Rows[2]["Verteilung"]) > 43 || Convert.ToDouble(values.Rows[2]["Verteilung"]) < 23)
                distRural.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[2]["Verteilung"]) >= 41.5d || Convert.ToDouble(values.Rows[2]["Verteilung"]) <= 31.5d)
                distRural.BackColor = Color.Yellow;
            else
                distRural.BackColor = Color.Green;

<<<<<<< HEAD
            if (Convert.ToDouble(values.Rows[3]["Verteilung"]) > 43 || Convert.ToDouble(values.Rows[3]["Verteilung"]) < 23)
                distMotorway.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[3]["Verteilung"]) >= 41.5d || Convert.ToDouble(values.Rows[3]["Verteilung"]) <= 31.5d)
                distMotorway.BackColor = Color.Yellow;
            else
                distMotorway.BackColor = Color.Green;
=======
            avgVUrban.TextAlign = HorizontalAlignment.Center;
            avgVRural.TextAlign = HorizontalAlignment.Center;
            avgVMotorway.TextAlign = HorizontalAlignment.Center;
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45


<<<<<<< HEAD
            tripUrban.Text = Convert.ToDouble(values.Rows[1]["Strecke"]).ToString("#.00");
            tripRural.Text = Convert.ToDouble(values.Rows[2]["Strecke"]).ToString("#.00");
            tripMotorway.Text = Convert.ToDouble(values.Rows[3]["Strecke"]).ToString("#.00");

            ColorTextBoxTrip(tripUrban, values, 1);
            ColorTextBoxTrip(tripRural, values, 2);
            ColorTextBoxTrip(tripMotorway, values, 3);

            avgVUrban.Text = Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]).ToString("#.00");
            avgVRural.Text = Convert.ToDouble(values.Rows[2]["Geschwindigkeit"]).ToString("#.00");
            avgVMotorway.Text = Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]).ToString("#.00");

            if (Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]) > 40 || Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]) < 15)
                avgVUrban.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]) >= 34 || Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]) <= 21)
                avgVUrban.BackColor = Color.Yellow;
            else
                avgVUrban.BackColor = Color.Green;

            //if (Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]) > 40 && Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]) < 15)
            //    avgVMotorway.BackColor = Color.Red;
            //else if (Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]) >= 34 && Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]) <= 21)
            //    avgVMotorway.BackColor = Color.Yellow;
            //else
            //    avgVMotorway.BackColor = Color.Green;

            lblTripVal.Text = Convert.ToDouble(values.Rows[0]["Strecke"]).ToString("#.00") + " km";
            lblDurationVal.Text = Convert.ToDouble(values.Rows[0]["Dauer"]).ToString("#.00") + " min";

=======
            if (Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]) > 40 || Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]) < 15)
            {
                avgVUrban.BackColor = Color.Red;
                avgVUrban.ForeColor = Color.White;
            }
            else if (Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]) >= 34 || Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]) <= 21)
            {
                avgVUrban.BackColor = Color.Yellow;
                avgVUrban.ForeColor = Color.Black;
            }
            else
            {
                avgVUrban.BackColor = Color.Green;
                avgVUrban.ForeColor = Color.White;
            }

            //if (Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]) > 40 && Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]) < 15)
            //    avgVMotorway.BackColor = Color.Red;
            //else if (Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]) >= 34 && Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]) <= 21)
            //    avgVMotorway.BackColor = Color.Yellow;
            //else
            //    avgVMotorway.BackColor = Color.Green;

            lblTripVal.Text = Convert.ToDouble(values.Rows[0]["Strecke"]).ToString("#.00") + " km";
            lblDurationVal.Text = Convert.ToDouble(values.Rows[0]["Dauer"]).ToString("#.00") + " min";

>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
            if (Convert.ToDouble(values.Rows[0]["Dauer"]) < 90 || Convert.ToDouble(values.Rows[0]["Dauer"]) > 120)
                lblDurationVal.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[0]["Dauer"]) <= 97.5d || Convert.ToDouble(values.Rows[0]["Dauer"]) > 112.5d)
                lblDurationVal.BackColor = Color.Yellow;
            else
                lblDurationVal.BackColor = Color.Green;

            lblTimeHoldVal.Text = Convert.ToDouble(values.Rows[0]["Haltezeit"]).ToString("#.00") + " min";
<<<<<<< HEAD
=======
            lblMaxSpeedVal.Text = Convert.ToDouble(values.Rows[0]["Hoechstgeschwindigkeit"]).ToString("#.00") + " km/h";
            lblMaxSpColdVal.Text = Convert.ToDouble(values.Rows[0]["Kaltstart Hoechstgeschwindigkeit"]).ToString("#.00") + " km/h";
            lblAvgSpColdVal.Text = Convert.ToDouble(values.Rows[0]["Kaltstart Durchschnittsgeschwindigkeit"]).ToString("#.00") + " km/h";
>>>>>>> parent of 9ca23f3... Kogler 07.05 19:45
        }
<<<<<<< HEAD
=======

        private void distUrban_MouseHover(object sender, EventArgs e)
        {
            tTDistrUrb.Show("Muss zwischen 29 und 44 % sein", distUrban);
        }

        private void distUrban_MouseLeave(object sender, EventArgs e)
        {
            tTDistrUrb.Hide(distUrban);
        }

        private void distRural_MouseHover(object sender, EventArgs e)
        {
            tTDistrRur.Show("Muss zwischen 23 und 43 % sein", distRural);
        }

        private void distRural_MouseLeave(object sender, EventArgs e)
        {
            tTDistrRur.Hide(distRural);
        }

        private void distMotorway_MouseHover(object sender, EventArgs e)
        {
            tTDistrMot.Show("Muss zwischen 23 und 43 % sein", distMotorway);
        }

        private void distMotorway_MouseLeave(object sender, EventArgs e)
        {
            tTDistrMot.Hide(distMotorway);
        }
>>>>>>> parent of 1572168... Kogler 05.04 15:00
    }
}
