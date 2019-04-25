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
        private DriversGuideMain MainForm;
        DataTable dataset = new DataTable();
        string lat = "GPS_Latitude";
        string lon = "GPS_Longitude";
        GMapControl tempMap = new GMapControl();
        GMapControl tempHyb = new GMapControl();
        GMapControl tempSat = new GMapControl();
        Calculations Berechnungen = new Calculations();

        public GPSVisualization(DriversGuideMain caller)
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
            List<PointLatLng> pointsUrban = new List<PointLatLng>();
            List<PointLatLng> pointsRural = new List<PointLatLng>();
            List<PointLatLng> pointsMotorway = new List<PointLatLng>();

            DataTable urban = new DataTable();
            DataTable rural = new DataTable();
            DataTable motorway = new DataTable();

            Berechnungen.SepIntervals(dataset, "OBD_Vehicle_Speed_(PID_13)");
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            Berechnungen.SortData(ref urban, "Time");
            Berechnungen.SortData(ref rural, "Time");
            Berechnungen.SortData(ref motorway, "Time");

            for (int i = 0; i < urban.Rows.Count; i++)
            {
                points.Add(new PointLatLng(Convert.ToDouble(urban.Rows[i][column_latitude]), Convert.ToDouble(urban.Rows[i][column_longitude])));

                if (i >= 1 && (Convert.ToDouble(urban.Rows[i]["Time"]) - (Convert.ToDouble(urban.Rows[(i - 1)]["Time"])) > 1000))
                {
                    points.RemoveAt(points.Count - 1);
                    GMapRoute route = new GMapRoute(points, "Urban");
                    route.Stroke = new Pen(Color.DarkRed, 3);
                    routes.Routes.Add(route);
                    gMap.Overlays.Add(routes);

                    points.Clear();
                }
            }

            GMapRoute routeA = new GMapRoute(points, "Urban");
            routeA.Stroke = new Pen(Color.DarkRed, 3);
            routes.Routes.Add(routeA);
            gMap.Overlays.Add(routes);

            points.Clear();

            for (int i = 0; i < rural.Rows.Count; i++)
            {
                points.Add(new PointLatLng(Convert.ToDouble(rural.Rows[i][column_latitude]), Convert.ToDouble(rural.Rows[i][column_longitude])));

                if (i >= 1 && (Convert.ToDouble(rural.Rows[i]["Time"]) - (Convert.ToDouble(rural.Rows[(i - 1)]["Time"])) > 1000))
                {
                    points.RemoveAt(points.Count - 1);
                    GMapRoute route = new GMapRoute(points, "Rural");
                    route.Stroke = new Pen(Color.Green, 3);
                    routes.Routes.Add(route);
                    gMap.Overlays.Add(routes);

                    points.Clear();
                }
            }

            GMapRoute routeB = new GMapRoute(points, "Rural");
            routeB.Stroke = new Pen(Color.Green, 3);
            routes.Routes.Add(routeB);
            gMap.Overlays.Add(routes);

            points.Clear();

            for (int i = 0; i < motorway.Rows.Count; i++)
            {
                points.Add(new PointLatLng(Convert.ToDouble(motorway.Rows[i][column_latitude]), Convert.ToDouble(motorway.Rows[i][column_longitude])));

                if (i >= 1 && (Convert.ToDouble(motorway.Rows[i]["Time"]) - (Convert.ToDouble(motorway.Rows[(i - 1)]["Time"])) > 1000))
                {
                    points.RemoveAt(points.Count - 1);
                    GMapRoute route = new GMapRoute(points, "motorway");
                    route.Stroke = new Pen(Color.Black, 3);
                    routes.Routes.Add(route);
                    gMap.Overlays.Add(routes);

                    points.Clear();
                }
            }

            GMapRoute routeC = new GMapRoute(points, "Motorway");
            routeC.Stroke = new Pen(Color.Black, 3);
            routes.Routes.Add(routeC);
            gMap.Overlays.Add(routes);

            //for (int i = 0; i < dataset.Rows.Count; i++)
            //{

            //    pointsUrban.Add(new PointLatLng(Convert.ToDouble(dataset.Rows[i][column_latitude]), Convert.ToDouble(dataset.Rows[i][column_longitude])));
            //}

            //GMapRoute routea = new GMapRoute(pointsUrban, "nn");
            //routea.Stroke = new Pen(Color.Blue, 1);
            //routes.Routes.Add(routea);
            //gMap.Overlays.Add(routes);

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
