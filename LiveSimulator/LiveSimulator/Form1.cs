using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LiveSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FileStream fsr;
        FileStream fsw;
        StreamReader sr;
        StreamWriter sw;


        private void btnFileAuswahl_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Textdateien |*.txt| Alle Dateien|*.*";
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            ofd.Title = "Textdatei öffnen";
            ofd.FileName = "";

            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                

                //Stream zum Lesen öffnen
                fsr = new FileStream(ofd.FileName, FileMode.Open);

                //Objekt zum Lesen erzeugen
                sr = new StreamReader(fsr);

             /*   while (!sr.EndOfStream)
                {
                    zeile = sr.ReadLine();
                    alles += zeile;
                }
            
                txt.Text = alles;
                sr.Close();
                fs.Close();
                */
            }
        }

        private void btnSpeicherpfad_Click(object sender, EventArgs e)
        {
            sfd.Filter = "Textdateien |*.txt| Alle Dateien|*.*";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            sfd.Title = "Textdatei Speichern";
            sfd.FileName = "";

            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string zeile;
                string alles = "";

                //Stream zum Lesen öffnen
                fsw = new FileStream(ofd.FileName, FileMode.Open);

                //Objekt zum Lesen erzeugen
                sw = new StreamWriter(fsw);
                //  DialogResult dr = sfd.ShowDialog();
            }
        }

        private void btnStartSimulation_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string zeile;
            string alles = "";


            while (!sr.EndOfStream)
            {
                zeile = sr.ReadLine();
                alles += zeile;
                txt.Text = alles;
            }

            txt.Text = alles;
            sr.Close();
            sw.Close();
            fsr.Close();
            fsw.Close();

        }
    }
}
