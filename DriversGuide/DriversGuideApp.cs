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
    public partial class DriversGuideApp : Form
    {
        private DriversGuideMain MainForm;
        private Calculations Berechnungen = new Calculations();

        public DriversGuideApp(DriversGuideMain caller)
        {
            InitializeComponent();
            MainForm = caller;

            //General myForm = new General(MainForm);
            ////myForm.TopLevel = false;
            //myForm.AutoScroll = true;
            //pnlContent.Controls.Add(myForm);
            ////myForm.FormBorderStyle = FormBorderStyle.None;
            //myForm.Show();
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            General myForm = new General(MainForm);
            //myForm.TopLevel = false;
            myForm.AutoScroll = true;
            pnlContent.Controls.Add(myForm);
            //myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Show();
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            pnlContent.Controls.Clear();
            GPS myForm = new GPS(MainForm);
            //myForm.TopLevel = false;
            myForm.AutoScroll = true;
            pnlContent.Controls.Add(myForm);
            //myForm.FormBorderStyle = FormBorderStyle.None;
            myForm.Show();
        }
    }
}
