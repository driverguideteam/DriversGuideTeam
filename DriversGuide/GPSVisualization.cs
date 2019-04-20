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

        public GPSVisualization(Form1 caller)
        {
            MainForm = caller;
            //MainForm.Hide();
            InitializeComponent();
            InitMap();
            AddRoute();
        }

        private void InitMap()
        {
            gMap.Width = this.ClientSize.Width;
            gMap.Height = this.ClientSize.Height;

            gMap.MapProvider = BingMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;        
            gMap.ShowCenter = false;
        }

        private void AddRoute()
        {
            GMapOverlay routes = new GMapOverlay("routes");
            List<PointLatLng> points = new List<PointLatLng>();

            string lat = "GPS_Latitude";
            string lon = "GPS_Longitude";

            gMap.Position = new GMap.NET.PointLatLng(Convert.ToDouble(MainForm.test.Rows[0][lat]), Convert.ToDouble(MainForm.test.Rows[0][lon]));

            for (int i = 0; i < MainForm.test.Rows.Count; i++)
            {
                points.Add(new PointLatLng(Convert.ToDouble(MainForm.test.Rows[i][lat]), Convert.ToDouble(MainForm.test.Rows[i][lon])));
            }
            
            GMapRoute route = new GMapRoute(points, "Testdrive");
            route.Stroke = new Pen(Color.DarkRed, 3);
            routes.Routes.Add(route);
            gMap.Overlays.Add(routes);

            //MainForm.Controls["txtMeasurement"].Text = "//Gemessene Distanz anhand GPS Datenauswertung:\n" + route.Distance.ToString();
        }
    }
}
