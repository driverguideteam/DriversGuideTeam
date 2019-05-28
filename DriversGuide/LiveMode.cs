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
    public partial class LiveMode : Form
    {
        StartScreen FormStart;
        GPS FormGPS;
        General FormGeneral;
        TestControl FormTest;
        bool topBottom = true;
        MeasurementFile LiveDatei;
        private DataTable Dataset = new DataTable();

        public LiveMode(StartScreen caller)
        {
            FormStart = caller;
            FormStart.Hide();

            InitializeComponent();
        }

        private void pnlTopContent_Click(object sender, EventArgs e)
        {
            topBottom = true;
        }

        private void LiveMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.Show();
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            if (topBottom)
            {
                pnlTopContent.Controls.Clear();
                FormTest = new TestControl();
                //myForm.TopLevel = false;
                FormTest.AutoScroll = true;
                pnlTopContent.Controls.Add(FormTest);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormTest.Show();
                FormTest.Dock = DockStyle.Fill;
            }
            else
            {
                pnlBottomContent.Controls.Clear();
                FormTest = new TestControl();
                //myForm.TopLevel = false;
                FormTest.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormTest);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormTest.Show();
                FormTest.Dock = DockStyle.Fill;
            }
        }

        private void pnlBottomContent_Click(object sender, EventArgs e)
        {
            topBottom = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Dataset.Clear();
            Dataset = LiveDatei.ConvertLiveCSVtoDataTable();
        }

        private void btn_Fileauswahl_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Textdateien |*.txt| Alle Dateien|*.*";
            ofd.InitialDirectory = "C:\\Repositories\01_Doku\\Vorgabe\\";
            //ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            ofd.Title = "Textdatei öffnen";
            ofd.FileName = "";

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                LiveDatei = new MeasurementFile(ofd.FileName);
                Dataset = LiveDatei.ConvertCSVtoDataTable();
            }
            timer1.Start();
        }
    }
}
