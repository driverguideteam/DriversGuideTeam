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
        DriversGuideApp Form1Copy;        //Verbindung zu Hauptform
        DataTable tt = new DataTable();   //Erstellung neues Datatable
        int AnzGewDaten = 0;              //Anzahl der ausgewählten Daten zur Anzeige

        public Datenauswahl(DriversGuideApp CreateForm)
        {
            Form1Copy = CreateForm;   //Zugriff auf Hauptform
            InitializeComponent();
        }

        public string[] ChosenData()   //Rückgabe gewählte Daten in Liste
        {
            string[] cdat = new string[] { "", "", "", "" };
            AnzGewDaten = lstChooseData.SelectedIndices.Count;

            if (AnzGewDaten == 1)
            {
                cdat[0] = lstChooseData.SelectedItems[0].ToString();
            }
            else if (AnzGewDaten == 2)
            {
                cdat[0] = lstChooseData.SelectedItems[0].ToString();
                cdat[1] = lstChooseData.SelectedItems[1].ToString();
            }
            else if (AnzGewDaten == 3)
            {
                cdat[0] = lstChooseData.SelectedItems[0].ToString();
                cdat[1] = lstChooseData.SelectedItems[1].ToString();
                cdat[2] = lstChooseData.SelectedItems[2].ToString();
            }
            else if (AnzGewDaten == 4)
            {
                cdat[0] = lstChooseData.SelectedItems[0].ToString();
                cdat[1] = lstChooseData.SelectedItems[1].ToString();
                cdat[2] = lstChooseData.SelectedItems[2].ToString();
                cdat[3] = lstChooseData.SelectedItems[3].ToString();
            }
            return cdat;
        }

        private void Datenauswahl_Load_1(object sender, EventArgs e)
        {
            tt = Form1Copy.GetCompleteDataTable();       //Kopie des Datatables
            DataColumn column;

            for (int i = 1; i < tt.Columns.Count; i++)   //Füllen der Datenauswahlliste mit Spaltenüberschriften
            {
                column = tt.Columns[i];
                lstChooseData.Items.Add(column.ColumnName);
            }

            try
            {
                //Entfernen der Zeit aus Datenauswahlliste (Zeit über Zeit zu zeichnen wäre unnötig)
                lstChooseData.Items.RemoveAt(lstChooseData.Items.IndexOf("AcqTime"));
            }
            catch { }
            lstChooseData.SelectedIndex = 0;   //Starteinstellung gewählter Index
        }

        private void lstChooseData_MouseDoubleClick(object sender, MouseEventArgs e)    //Öffnen der Diagramme per Doppelklick
        {
            PlotGraphic NewDiagram = new PlotGraphic(Form1Copy);   //Erstellung neue DiagrammForm mit Verbingdung zu Hauptform
            NewDiagram.ConnectToDatenauswahl(this);                //Verbindung der DiagrammForm zur DatenauswahlListe
            NewDiagram.Show();                                     //Anzeige Diagramm
        }

        private void lstChooseData_MouseDown_1(object sender, MouseEventArgs e)   //Auswahl von max. 4 Daten zu Anzeige
        {
            AnzGewDaten = lstChooseData.SelectedIndices.Count;
            Point pt = new Point(e.X, e.Y);
            //Retrieve the item at the specified location within the ListBox.
            int index = lstChooseData.IndexFromPoint(pt);

            if(index >= 0)
            {
                if (AnzGewDaten > 4)
                {
                    lstChooseData.SelectedIndices.Remove(index);
                    MessageBox.Show("Wählen Sie maximal 4 Daten zur Anzeige aus!");
                }
            }
        }

        private void btnShowGraphics_Click(object sender, EventArgs e)    //Öffnen der Diagramme über Button
        {
            AnzGewDaten = lstChooseData.SelectedIndices.Count;

            if (AnzGewDaten == 0)
            {
                MessageBox.Show("Bitte wählen Sie Daten aus!");
            }
            else
            {
                PlotGraphic NewDiagram = new PlotGraphic(Form1Copy);   //Erstellung neue DiagrammForm mit Verbingdung zu Hauptform
                NewDiagram.ConnectToDatenauswahl(this);                //Verbindung der DiagrammForm zur DatenauswahlListe
                NewDiagram.Show();                                     //Anzeige Diagramm    
            }
        }
    }
}
