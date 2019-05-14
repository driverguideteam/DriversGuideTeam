using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriversGuide
{
    public partial class General : UserControl
    {
        private DriversGuideApp MainForm;
        private Calculations Berechnungen = new Calculations();

        public General(DriversGuideApp caller)
        {
            MainForm = caller;
            InitializeComponent();
            ShowData();            
        }

        private void ColorTextBoxTrip(TextBox txtbx, DataTable data, int index)
        {
            txtbx.TextAlign = HorizontalAlignment.Center;

            if (Convert.ToDouble(data.Rows[index]["Strecke"]) < 16)
            {
                txtbx.BackColor = Color.Red;
                txtbx.ForeColor = Color.White;
            }
            else if (Convert.ToDouble(data.Rows[index]["Strecke"]) <= 20)
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

        private void ColorTextBox(TextBox txtbx, DataTable data, int index, string column, double lowRed, double highRed, double lowYellow, double highYellow)
        {
            txtbx.TextAlign = HorizontalAlignment.Center;

            if (Convert.ToDouble(data.Rows[index][column]) < lowRed || Convert.ToDouble(data.Rows[index][column]) > highRed)
            {
                txtbx.BackColor = Color.Red;
                txtbx.ForeColor = Color.White;
            }
            else if (Convert.ToDouble(data.Rows[index][column]) <= lowYellow || Convert.ToDouble(data.Rows[index][column]) >= highYellow)
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

            distUrban.Text = Convert.ToDouble(values.Rows[1]["Verteilung"]).ToString("#.00") + "%";
            distRural.Text = Convert.ToDouble(values.Rows[2]["Verteilung"]).ToString("#.00") + "%";
            distMotorway.Text = Convert.ToDouble(values.Rows[3]["Verteilung"]).ToString("#.00") + "%";

            ColorTextBox(distUrban, values, 1, "Verteilung", 29, 44, 31.5, 41.5);
            ColorTextBox(distRural, values, 2, "Verteilung", 23, 43, 31.5, 41.5);
            ColorTextBox(distMotorway, values, 3, "Verteilung", 23, 43, 31.5, 41.5);

            tripUrban.Text = Convert.ToDouble(values.Rows[1]["Strecke"]).ToString("#.00") + " km";
            tripRural.Text = Convert.ToDouble(values.Rows[2]["Strecke"]).ToString("#.00") + " km";
            tripMotorway.Text = Convert.ToDouble(values.Rows[3]["Strecke"]).ToString("#.00") + " km";

            ColorTextBoxTrip(tripUrban, values, 1);
            ColorTextBoxTrip(tripRural, values, 2);
            ColorTextBoxTrip(tripMotorway, values, 3);

            avgVRural.TextAlign = HorizontalAlignment.Center;
            avgVMotorway.TextAlign = HorizontalAlignment.Center;

            avgVUrban.Text = Convert.ToDouble(values.Rows[1]["Geschwindigkeit"]).ToString("#.00") + " km/h";
            avgVRural.Text = Convert.ToDouble(values.Rows[2]["Geschwindigkeit"]).ToString("#.00") + " km/h";
            avgVMotorway.Text = Convert.ToDouble(values.Rows[3]["Geschwindigkeit"]).ToString("#.00") + " km/h";

            ColorTextBox(avgVUrban, values, 1, "Geschwindigkeit", 15, 40, 21, 34);

            tBTrip.TextAlign = HorizontalAlignment.Center;

            tBTrip.Text = Convert.ToDouble(values.Rows[0]["Strecke"]).ToString("#.00") + " km";
            tBDuration.Text = Convert.ToDouble(values.Rows[0]["Dauer"]).ToString("#.00") + " min";
            ColorTextBox(tBDuration, values, 0, "Dauer", 90, 120, 97.5, 112.5);

            tBTimeHold.TextAlign = HorizontalAlignment.Center;
            tBMaxSpeed.TextAlign = HorizontalAlignment.Center;
            tBMaxSpColdVal.TextAlign = HorizontalAlignment.Center;
            tBAvgSpColdVal.TextAlign = HorizontalAlignment.Center;
            tBTimeHoldCold.TextAlign = HorizontalAlignment.Center;

            tBTimeHold.Text = Convert.ToDouble(values.Rows[0]["Haltezeit"]).ToString("#.00") + " min";
            tBMaxSpeed.Text = Convert.ToDouble(values.Rows[0]["Hoechstgeschwindigkeit"]).ToString("#.00") + " km/h";
            tBMaxSpColdVal.Text = Convert.ToDouble(values.Rows[0]["Kaltstart Hoechstgeschwindigkeit"]).ToString("#.00") + " km/h";
            tBAvgSpColdVal.Text = Convert.ToDouble(values.Rows[0]["Kaltstart Durchschnittsgeschwindigkeit"]).ToString("#.00") + " km/h";
            tBTimeHoldCold.Text = Convert.ToDouble(values.Rows[0]["Kaltstart Haltezeit"]).ToString("#.00") + " sec";
            tBFasterOH.Text = Convert.ToDouble(values.Rows[3]["Hoechstgeschwindigkeit"]).ToString("#.00") + " min";

            tBFasterOH.TextAlign = HorizontalAlignment.Center;

            if (Convert.ToDouble(values.Rows[3]["Hoechstgeschwindigkeit"]) < 5)
            {
                tBFasterOH.BackColor = Color.Red;
                tBFasterOH.ForeColor = Color.White;
            }
            else if (Convert.ToDouble(values.Rows[3]["Hoechstgeschwindigkeit"]) <= 7)
            {
                tBFasterOH.BackColor = Color.Yellow;
                tBFasterOH.ForeColor = Color.Black;
            }
            else
            {
                tBFasterOH.BackColor = Color.Green;
                tBFasterOH.ForeColor = Color.White;
            }
        }

        //private void distUrban_MouseHover(object sender, EventArgs e)
        //{
        //    tTDistrUrb.Show("Muss zwischen 29 und 44 % sein", distUrban);
        //}

        //private void distUrban_MouseLeave(object sender, EventArgs e)
        //{
        //    tTDistrUrb.Hide(distUrban);
        //}

        //private void distRural_MouseHover(object sender, EventArgs e)
        //{
        //    tTDistrRur.Show("Muss zwischen 23 und 43 % sein", distRural);
        //}

        //private void distRural_MouseLeave(object sender, EventArgs e)
        //{
        //    tTDistrRur.Hide(distRural);
        //}

        //private void distMotorway_MouseHover(object sender, EventArgs e)
        //{
        //    tTDistrMot.Show("Muss zwischen 23 und 43 % sein", distMotorway);
        //}

        //private void distMotorway_MouseLeave(object sender, EventArgs e)
        //{
        //    tTDistrMot.Hide(distMotorway);
        //}

    }
}
