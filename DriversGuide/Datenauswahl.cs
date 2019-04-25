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
    public partial class Datenauswahl : Form
    {
        Form1 Form1Copy;                  //Verbindung zu Hauptform
        DataTable tt = new DataTable();   //Erstellung neues Datatable
        string[] Titles;                  //Erstellung neues StringArray für Spaltenüberschriften

        public Datenauswahl(Form1 CreateForm)
        {
            Form1Copy = CreateForm;   //Zugriff auf Hauptform
            InitializeComponent();
        }

        public string ChosenData()   //Rückgabe gewählte Daten in Liste
        {
            string cdat = lstChooseData.SelectedItem.ToString();
            return cdat;
        }

        private void Datenauswahl_Load_1(object sender, EventArgs e)
        {
            tt = Form1Copy.test.Copy();         //Kopie des Datatables
            Titles = Form1Copy.ColumnHeaders;   //Kopie der Spaltenüberschriften

            for (int i = 1; i < Titles.Length; i++)   //Füllen der Datenauswahlliste mit Spaltenüberschriften
            {
                lstChooseData.Items.Add(Titles[i]);
            }
            lstChooseData.SelectedIndex = 0;   //Starteinstellung gewählter Index
        }

        private void lstChooseData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PlotGraphic NewDiagram = new PlotGraphic(Form1Copy);   //Erstellung neue DiagrammForm mit Verbingdung zu Hauptform
            NewDiagram.GetChosenData(this);                        //Verbindung der DiagrammForm zur DatenauswahlListe
            NewDiagram.Show();                                     //Anzeige Diagramm
        }
    }
}
