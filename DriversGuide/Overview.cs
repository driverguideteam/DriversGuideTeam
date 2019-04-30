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

        private void ShowData()
        {
            DataTable values = new DataTable();

            values = MainForm.GetValuesDataTable();

            distUrban.Text = Convert.ToDouble(values.Rows[1]["Verteilung"]).ToString("#.00");
            distRural.Text = Convert.ToDouble(values.Rows[2]["Verteilung"]).ToString("#.00");
            distMotorway.Text = Convert.ToDouble(values.Rows[3]["Verteilung"]).ToString("#.00");

            if (Convert.ToDouble(values.Rows[1]["Verteilung"]) > 44 && Convert.ToDouble(values.Rows[1]["Verteilung"]) < 29)
                distUrban.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[1]["Verteilung"]) >= 41.5d && Convert.ToDouble(values.Rows[1]["Verteilung"]) <= 31.5d)
                distUrban.BackColor = Color.Yellow;            
            else
                distUrban.BackColor = Color.Green;

            if (Convert.ToDouble(values.Rows[2]["Verteilung"]) > 43 && Convert.ToDouble(values.Rows[2]["Verteilung"]) < 23)
                distRural.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[2]["Verteilung"]) >= 41.5d && Convert.ToDouble(values.Rows[2]["Verteilung"]) <= 31.5d)
                distRural.BackColor = Color.Yellow;
            else
                distRural.BackColor = Color.Green;

            if (Convert.ToDouble(values.Rows[3]["Verteilung"]) > 43 && Convert.ToDouble(values.Rows[3]["Verteilung"]) < 23)
                distMotorway.BackColor = Color.Red;
            else if (Convert.ToDouble(values.Rows[3]["Verteilung"]) >= 41.5d && Convert.ToDouble(values.Rows[3]["Verteilung"]) <= 31.5d)
                distMotorway.BackColor = Color.Yellow;
            else
                distMotorway.BackColor = Color.Green;


            tripUrban.Text = Convert.ToDouble(values.Rows[1]["Strecke"]).ToString("#.00");
            tripRural.Text = Convert.ToDouble(values.Rows[2]["Strecke"]).ToString("#.00");
            tripMotorway.Text = Convert.ToDouble(values.Rows[3]["Strecke"]).ToString("#.00");
        }

    }
}
