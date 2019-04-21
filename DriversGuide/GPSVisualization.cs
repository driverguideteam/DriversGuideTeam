using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;

namespace DriversGuide
{
    public partial class GPSVisualization : Form
    {
        private Form1 MainForm;
        DataTable dataset = new DataTable();
        string lat = "GPS_Latitude";
        string lon = "GPS_Longitude";
        GMap.NET.WindowsForms.GMapControl tempMap = new GMap.NET.WindowsForms.GMapControl();
        GMap.NET.WindowsForms.GMapControl tempHyb = new GMap.NET.WindowsForms.GMapControl();
        GMap.NET.WindowsForms.GMapControl tempSat = new GMap.NET.WindowsForms.GMapControl();

        public GPSVisualization(Form1 caller)
        {
            MainForm = caller;
            MainForm.Hide();
            dataset = MainForm.GetDataTable();
            InitializeComponent();
            InitMap();
            CenterMap(lat, lon);
            AddRoute(lat, lon);
            gMap.ContextMenuStrip = conMenMap;
        }

        private void InitMap()
        {
            gMap.Width = this.ClientSize.Width;
            gMap.Height = this.ClientSize.Height;

            tempMap.MapProvider = GoogleMapProvider.Instance;
            tempHyb.MapProvider = GoogleHybridMapProvider.Instance;
            tempSat.MapProvider = GoogleSatelliteMapProvider.Instance;
            googleToolStripMenuItem.Checked = true;

            gMap.MapProvider = tempMap.MapProvider;
            GMaps.Instance.Mode = AccessMode.ServerOnly;        
            gMap.ShowCenter = false;
            gMap.DragButton = MouseButtons.Left;
        }

        private void CenterMap(string column_latitude, string column_longitude)
        {
            double maxLat, maxLon, minLat, minLon;
            double centerLat, centerLon;

            maxLat = (double)dataset.Compute("MAX([" + column_latitude + "])", "");
            minLat = (double)dataset.Compute("MIN([" + column_latitude + "])", "");

            maxLon = (double)dataset.Compute("MAX([" + column_longitude + "])", "");
            minLon = (double)dataset.Compute("MIN([" + column_longitude + "])", "");

            centerLat = (maxLat - minLat) / 2 + minLat;
            centerLon = (maxLon - minLon) / 2 + minLon;

            gMap.Position = new GMap.NET.PointLatLng(centerLat, centerLon);
        }

        private void AddRoute(string column_latitude, string column_longitude)
        {
            GMapOverlay routes = new GMapOverlay("routes");
            List<PointLatLng> points = new List<PointLatLng>();            

            for (int i = 0; i < dataset.Rows.Count; i++)
            {
                points.Add(new PointLatLng(Convert.ToDouble(dataset.Rows[i][column_latitude]), Convert.ToDouble(dataset.Rows[i][column_longitude])));
            }
            
            GMapRoute route = new GMapRoute(points, "Testdrive");
            route.Stroke = new Pen(Color.DarkRed, 3);
            routes.Routes.Add(route);
            gMap.Overlays.Add(routes);

            //MainForm.Controls["txtMeasurement"].Text = "//Gemessene Distanz anhand GPS Datenauswertung:\n" + route.Distance.ToString();
        }

        private void GPSVisualization_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.Show();
        }

        private void karteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = tempMap.MapProvider;
            karteToolStripMenuItem.Checked = true;
            satelitToolStripMenuItem.Checked = false;
            hybridToolStripMenuItem.Checked = false;
        }

        private void satelitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = tempSat.MapProvider;
            satelitToolStripMenuItem.Checked = true;
            karteToolStripMenuItem.Checked = false;
            hybridToolStripMenuItem.Checked = false;
        }

        private void zurückZurRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CenterMap(lat, lon);
        }

        private void zoomZurücksetzenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMap.Zoom = 11;
        }

        private void googleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempMap.MapProvider = GoogleMapProvider.Instance;
            tempHyb.MapProvider = GoogleHybridMapProvider.Instance;
            tempSat.MapProvider = GoogleSatelliteMapProvider.Instance;
            googleToolStripMenuItem.Checked = true;
            bingToolStripMenuItem.Checked = false;
            
            if (satelitToolStripMenuItem.Checked)
                gMap.MapProvider = tempSat.MapProvider;
            else if (karteToolStripMenuItem.Checked)
                gMap.MapProvider = tempMap.MapProvider;
            else if (hybridToolStripMenuItem.Checked)
                gMap.MapProvider = tempHyb.MapProvider;            
        }

        private void hybridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = tempHyb.MapProvider;
            hybridToolStripMenuItem.Checked = true;
            satelitToolStripMenuItem.Checked = false;
            karteToolStripMenuItem.Checked = false;
        }

        private void bingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tempMap.MapProvider = BingMapProvider.Instance;
            tempHyb.MapProvider = BingHybridMapProvider.Instance;
            tempSat.MapProvider = BingSatelliteMapProvider.Instance;
            bingToolStripMenuItem.Checked = true;
            googleToolStripMenuItem.Checked = false;         
            
            if (satelitToolStripMenuItem.Checked)
                gMap.MapProvider = tempSat.MapProvider;
            else if (karteToolStripMenuItem.Checked)
                gMap.MapProvider = tempMap.MapProvider;
            else if (hybridToolStripMenuItem.Checked)
                gMap.MapProvider = tempHyb.MapProvider;            
        }

    }
}
