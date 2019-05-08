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

            if (Convert.ToDouble(values.Rows[2]["Verteilung"]) > 43 || Convert.ToDouble(values.Rows[2]["Verteilung"]) < 23)
                distRural.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[2]["Verteilung"]) >= 41.5d || Convert.ToDouble(values.Rows[2]["Verteilung"]) <= 31.5d)
                distRural.BackColor = Color.Yellow;
            else
                distRural.BackColor = Color.Green;

            if (Convert.ToDouble(values.Rows[3]["Verteilung"]) > 43 || Convert.ToDouble(values.Rows[3]["Verteilung"]) < 23)
                distMotorway.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[3]["Verteilung"]) >= 41.5d || Convert.ToDouble(values.Rows[3]["Verteilung"]) <= 31.5d)
                distMotorway.BackColor = Color.Yellow;
            else
                distMotorway.BackColor = Color.Green;


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

            if (Convert.ToDouble(values.Rows[0]["Dauer"]) < 90 || Convert.ToDouble(values.Rows[0]["Dauer"]) > 120)
                lblDurationVal.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[0]["Dauer"]) <= 97.5d || Convert.ToDouble(values.Rows[0]["Dauer"]) > 112.5d)
                lblDurationVal.BackColor = Color.Yellow;
            else
                lblDurationVal.BackColor = Color.Green;

            lblTimeHoldVal.Text = Convert.ToDouble(values.Rows[0]["Haltezeit"]).ToString("#.00") + " min";
        }
    }
}
