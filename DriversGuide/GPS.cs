using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
    public partial class GPS : UserControl
    {
        private DriversGuideApp MainForm;
        private LiveMode LiveForm;
        DataTable dataset = new DataTable();
        string lat = "GPS_Latitude";
        string lon = "GPS_Longitude";
        string speed = "OBD_Vehicle_Speed_(PID_13)";
        string time = "Time";
        GMapProvider provHyb, provMap, provSat;
        Calculations Berechnungen = new Calculations();
        double ZoomLive = 10;
        double ZoomNormal = 11;
        bool live = false;
        bool topBottomSave;       

        private GMapOverlay Live = new GMapOverlay("routeLive");
        private GMapRoute routeLiveUrban = new GMapRoute("LiveUrban");
        private GMapRoute routeLiveRural = new GMapRoute("LiveRural");
        private GMapRoute routeLiveMotorway = new GMapRoute("LiveMotorway");
        GMapOverlay temp = new GMapOverlay("temp");
        List<PointLatLng> dataPoints = new List<PointLatLng>();
        double lastUrban = 0;
        double countUrban = 0;
        double lastRural = 0;
        double countRural = 0;
        double lastMotorway = 0;
        double countMotorway = 0;

        Pen colorUrban = new Pen(Color.IndianRed, 4);
        Pen colorRural = new Pen(Color.MediumSeaGreen, 4);
        Pen colorMotorway = new Pen(Color.LightSkyBlue, 4);

        GMapOverlay markers = new GMapOverlay("markers");
        GMapMarker currPos;
        //    new PointLatLng(Convert.ToDouble(motorway.Rows[motorway.Rows.Count - 1][column_latitude]), Convert.ToDouble(motorway.Rows[motorway.Rows.Count - 1][column_longitude])),
        //    GMarkerGoogleType.blue_pushpin);
        //markers.Markers.Add(marker);        

        public GPS(DriversGuideApp caller)
        {
            MainForm = caller;
            dataset = MainForm.GetCompleteDataTable();
            InitializeComponent();
            InitMap();
            CenterMap(lat, lon);
            AddRoute(lat, lon, speed, time);
            gMap.ContextMenuStrip = conMenMap;

            gMap.Overlays.Add(markers);
            gMap.Zoom = ZoomNormal;
            live = false;
        }

        public GPS(LiveMode caller)
        {
            LiveForm = caller;            
            dataset = LiveForm.GetCompleteDataTable();
            InitializeComponent();
            InitMap();
            live = true;
            CenterMap(lat, lon);
            routeLiveUrban.Stroke = colorUrban;
            routeLiveRural.Stroke = colorRural;
            routeLiveMotorway.Stroke = colorMotorway;
            AddRouteLive(lat, lon, speed, time);
            //AddRoute(lat, lon, speed, time);
            gMap.Overlays.Add(Live);
            gMap.ContextMenuStrip = conMenMap;

            gMap.Overlays.Add(markers);
            gMap.Zoom = ZoomLive;           

            topBottomSave = LiveForm.topBottom;
        }

        public void RefreshMap()
        {
            if (live)
                dataset = LiveForm.GetCompleteDataTable();
            else
                dataset = MainForm.GetCompleteDataTable();

            CenterMap(lat, lon);
            AddRouteLive(lat, lon, speed, time);
        }

        public void AddMap()
        {
            AddRoute(lat, lon, speed, time);
        }

        private void InitMap()
        {
            gMap.Width = this.ClientSize.Width;
            gMap.Height = this.ClientSize.Height;

            provMap = GoogleMapProvider.Instance;
            provHyb = GoogleHybridMapProvider.Instance;
            provSat = GoogleSatelliteMapProvider.Instance;
            googleToolStripMenuItem.Checked = true;

            gMap.MapProvider = provMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            gMap.ShowCenter = false;
            gMap.DragButton = MouseButtons.Left;
        }

        private void CenterMap(string column_latitude, string column_longitude)
        {
            double maxLat, maxLon, minLat, minLon;
            double centerLat, centerLon;
            
            if (dataset.Rows.Count <= 0)
            {
                centerLat = 47.07995;
                centerLon = 15.9152;
            }
            else if (live)
            {
                centerLat = (double)dataset.Rows[dataset.Rows.Count-1][column_latitude];
                centerLon = (double)dataset.Rows[dataset.Rows.Count - 1][column_longitude];
            }
            else
            {
                maxLat = (double)dataset.Compute("MAX([" + column_latitude + "])", "");
                minLat = (double)dataset.Compute("MIN([" + column_latitude + "])", "");

                maxLon = (double)dataset.Compute("MAX([" + column_longitude + "])", "");
                minLon = (double)dataset.Compute("MIN([" + column_longitude + "])", "");

                centerLat = (maxLat - minLat) / 2 + minLat;
                centerLon = (maxLon - minLon) / 2 + minLon;
            }
                                          

            gMap.Position = new PointLatLng(centerLat, centerLon);
        }

        private void AddRouteLive(string column_latitude, string column_longitude, string column_speed, string column_time)
        {
            PointLatLng data = new PointLatLng();

            if (dataset.Rows.Count > 0)
            {
                //data.Add(new PointLatLng(Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 2][column_latitude]), Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 2][column_longitude])));
                data.Lat = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_latitude]);
                data.Lng = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_longitude]);
                //routeLive.Points.Add(data);
                //routeLive.Points.Add(data[1]);

                if (Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_speed]) <= 60)
                {
                    if (Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_time]) - 1000 == lastUrban)
                    {
                        routeLiveUrban.Points.Add(data);
                        dataPoints.Add(data);
                    }
                    else
                    {
                        temp.Routes.Add(new GMapRoute(dataPoints, "Urban" + countUrban.ToString()));
                        temp.Routes[temp.Routes.Count - 1].Stroke = colorUrban;
                        routeLiveUrban.Clear();
                        routeLiveUrban.Name = "LiveUrban" + countUrban.ToString();
                        dataPoints.Clear();
                        countUrban++;
                    }
                    lastUrban = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_time]);
                }
                else if (Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_speed]) <= 90)
                {
                    if (Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_time]) - 1000 == lastRural)
                    {
                        routeLiveRural.Points.Add(data);
                        dataPoints.Add(data);
                    }
                    else
                    {
                        temp.Routes.Add(new GMapRoute(dataPoints, "Rural" + countRural.ToString()));
                        temp.Routes[temp.Routes.Count - 1].Stroke = colorRural;
                        routeLiveRural.Clear();
                        routeLiveRural.Name = "LiveUrban" + countRural.ToString();
                        dataPoints.Clear();
                        countRural++;
                    }

                    lastRural = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_time]);
                }
                else
                {
                    if (Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_time]) - 1000 == lastMotorway)
                    {
                        routeLiveMotorway.Points.Add(data);
                        dataPoints.Add(data);
                    }
                    else
                    {
                        temp.Routes.Add(new GMapRoute(dataPoints, "Motorway" + countMotorway.ToString()));
                        temp.Routes[temp.Routes.Count - 1].Stroke = colorMotorway;
                        routeLiveMotorway.Clear();
                        routeLiveMotorway.Name = "LiveMotorway" + countMotorway.ToString();
                        dataPoints.Clear();
                        countMotorway++;
                    }

                    lastMotorway = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_time]);
                }

                Live.Clear();
                //Live = temp;
                //for (int i = 0; i < routeList.Count; i++)
                //    Live.Routes.Add(routeList[i]);
              
                Live.Routes.Add(routeLiveUrban);
                Live.Routes.Add(routeLiveRural);
                Live.Routes.Add(routeLiveMotorway);
                for (int i = 0; i < temp.Routes.Count; i++)
                    Live.Routes.Add(temp.Routes[i]);                
            }            
        }

        private void AddRoute(string column_latitude, string column_longitude, string column_speed, string column_time)
        {
            GMapOverlay routes = new GMapOverlay("routes");
            List<PointLatLng> points = new List<PointLatLng>();
            List<PointLatLng> pointsUrban = new List<PointLatLng>();
            List<PointLatLng> pointsRural = new List<PointLatLng>();
            List<PointLatLng> pointsMotorway = new List<PointLatLng>();

            DataTable urban = new DataTable();
            DataTable rural = new DataTable();
            DataTable motorway = new DataTable();
            DataTable dt = new DataTable();

            //DataRow[] dr;

            Berechnungen.SepIntervals(dataset, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            Berechnungen.SortData(ref urban, column_time);
            Berechnungen.SortData(ref rural, column_time);
            Berechnungen.SortData(ref motorway, column_time);

            for (int i = 0; i < urban.Rows.Count; i++)
            {
                points.Add(new PointLatLng(Convert.ToDouble(urban.Rows[i][column_latitude]), Convert.ToDouble(urban.Rows[i][column_longitude])));

                if (i >= 1 && (Convert.ToDouble(urban.Rows[i][column_time]) - (Convert.ToDouble(urban.Rows[(i - 1)][column_time])) > 1000))
                {
                    dt = rural.Select("[" + column_time + "] = " + (Convert.ToInt32(urban.Rows[i - 1][column_time]) + 1000)).CopyToDataTable();

                    points.RemoveAt(points.Count - 1);
                    points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));

                    GMapRoute route = new GMapRoute(points, "Urban");
                    route.Stroke = new Pen(Color.IndianRed, 4);
                    routes.Routes.Add(route);

                    points.Clear();
                    points.Add(new PointLatLng(Convert.ToDouble(urban.Rows[i][column_latitude]), Convert.ToDouble(urban.Rows[i][column_longitude])));
                }
            }

            GMapRoute routeA = new GMapRoute(points, "Urban");
            routeA.Stroke = new Pen(Color.IndianRed, 4);
            routes.Routes.Add(routeA);           

            points.Clear();

            for (int i = 0; i < rural.Rows.Count; i++)
            {
                points.Add(new PointLatLng(Convert.ToDouble(rural.Rows[i][column_latitude]), Convert.ToDouble(rural.Rows[i][column_longitude])));

                if (i >= 1 && (Convert.ToDouble(rural.Rows[i][column_time]) - (Convert.ToDouble(rural.Rows[(i - 1)][column_time])) > 1000))
                {
                    if (Convert.ToDouble(rural.Rows[i - 1]["ai"]) < 0)
                        dt = urban.Select("[" + column_time + "] = " + (Convert.ToInt32(rural.Rows[i - 1][column_time]) + 1000)).CopyToDataTable();
                    else
                        dt = motorway.Select("[" + column_time + "] = " + (Convert.ToInt32(rural.Rows[i - 1][column_time]) + 1000)).CopyToDataTable();

                    points.RemoveAt(points.Count - 1);
                    points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));

                    GMapRoute route = new GMapRoute(points, "Rural");
                    route.Stroke = new Pen(Color.MediumSeaGreen, 4);
                    routes.Routes.Add(route);

                    points.Clear();
                    points.Add(new PointLatLng(Convert.ToDouble(rural.Rows[i][column_latitude]), Convert.ToDouble(rural.Rows[i][column_longitude])));
                }
            }
            //dr = urban.Select("[" + column_time + "] = " + (Convert.ToInt32(rural.Rows[rural.Rows.Count - 1][column_time]) + 1000));
            if (rural.Rows.Count > 0)
            {
                dt = urban.Select("[" + column_time + "] = " + (Convert.ToInt32(rural.Rows[rural.Rows.Count - 1][column_time]) + 1000)).CopyToDataTable();
                points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));
            }

            GMapRoute routeB = new GMapRoute(points, "Rural");
            routeB.Stroke = new Pen(Color.MediumSeaGreen, 4);
            routes.Routes.Add(routeB);

            points.Clear();

            for (int i = 0; i < motorway.Rows.Count; i++)
            {
                points.Add(new PointLatLng(Convert.ToDouble(motorway.Rows[i][column_latitude]), Convert.ToDouble(motorway.Rows[i][column_longitude])));

                if (i >= 1 && (Convert.ToDouble(motorway.Rows[i][column_time]) - (Convert.ToDouble(motorway.Rows[(i - 1)][column_time])) > 1000))
                {
                    dt = rural.Select("[" + column_time + "] = " + (Convert.ToInt32(motorway.Rows[i - 1][column_time]) + 1000)).CopyToDataTable();

                    points.RemoveAt(points.Count - 1);
                    points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));

                    GMapRoute route = new GMapRoute(points, "motorway");
                    route.Stroke = new Pen(Color.LightSkyBlue, 4);
                    routes.Routes.Add(route);

                    points.Clear();
                    points.Add(new PointLatLng(Convert.ToDouble(motorway.Rows[i][column_latitude]), Convert.ToDouble(motorway.Rows[i][column_longitude])));
                }
            }
            //dr = rural.Select("[" + column_time + "] = " + (Convert.ToInt32(motorway.Rows[motorway.Rows.Count - 1][column_time]) + 1000));
            if (motorway.Rows.Count > 0)
            {
                dt = rural.Select("[" + column_time + "] = " + (Convert.ToInt32(motorway.Rows[motorway.Rows.Count - 1][column_time]) + 1000)).CopyToDataTable();
                points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));
            }

            GMapRoute routeC = new GMapRoute(points, "Motorway");
            routeC.Stroke = new Pen(Color.LightSkyBlue, 4);
            routes.Routes.Add(routeC);
            gMap.Overlays.Add(routes);

            //GMapOverlay markers = new GMapOverlay("markers");
            //GMapMarker marker = new GMarkerGoogle(
            //    new PointLatLng(Convert.ToDouble(motorway.Rows[motorway.Rows.Count - 1][column_latitude]), Convert.ToDouble(motorway.Rows[motorway.Rows.Count - 1][column_longitude])),
            //    GMarkerGoogleType.blue_pushpin);
            //markers.Markers.Add(marker);
            //gMap.Overlays.Add(markers);

            //points.Clear();
            //for (int i = 0; i < dataset.Rows.Count; i++)
            //{
            //    points.Add(new PointLatLng(Convert.ToDouble(dataset.Rows[i][column_latitude]), Convert.ToDouble(dataset.Rows[i][column_longitude])));
            //}

            //GMapRoute routee = new GMapRoute(points, "Color Coded Trip");
            //routee.Stroke = new Pen(Color.Blue, 1);
            //routes.Routes.Add(routee);
            //gMap.Overlays.Add(routes);

            //MainForm.Controls["txtMeasurement"].Text = "//Gemessene Distanz anhand GPS Datenauswertung:\n" + routee.Distance.ToString();
        }

        private void GPSVisualization_FormClosed(object sender, FormClosedEventArgs e)
        {
            //MainForm.Show();
        }

        private void karteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = provMap;
            karteToolStripMenuItem.Checked = true;
            satelitToolStripMenuItem.Checked = false;
            hybridToolStripMenuItem.Checked = false;
        }

        private void satelitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = provSat;
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
            if (live)
            {
                if (ClientSize.Height >= 300)
                    gMap.Zoom = ZoomNormal;
                else
                    gMap.Zoom = ZoomLive;
            }                
            else
                gMap.Zoom = ZoomNormal;
        }

        private void googleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            provMap = GoogleMapProvider.Instance;
            provHyb = GoogleHybridMapProvider.Instance;
            provSat = GoogleSatelliteMapProvider.Instance;
            googleToolStripMenuItem.Checked = true;
            bingToolStripMenuItem.Checked = false;

            if (satelitToolStripMenuItem.Checked)
                gMap.MapProvider = provSat;
            else if (karteToolStripMenuItem.Checked)
                gMap.MapProvider = provMap;
            else if (hybridToolStripMenuItem.Checked)
                gMap.MapProvider = provHyb;
        }

        private void hybridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gMap.MapProvider = provHyb;
            hybridToolStripMenuItem.Checked = true;
            satelitToolStripMenuItem.Checked = false;
            karteToolStripMenuItem.Checked = false;
        }

        private void gMap_Click(object sender, EventArgs e)
        {
            if (live)
                LiveForm.topBottom = topBottomSave;
        }

        private void bingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            provMap = BingMapProvider.Instance;
            provHyb = BingHybridMapProvider.Instance;
            provSat = BingSatelliteMapProvider.Instance;
            bingToolStripMenuItem.Checked = true;
            googleToolStripMenuItem.Checked = false;

            if (satelitToolStripMenuItem.Checked)
                gMap.MapProvider = provSat;
            else if (karteToolStripMenuItem.Checked)
                gMap.MapProvider = provMap;
            else if (hybridToolStripMenuItem.Checked)
                gMap.MapProvider = provHyb;
        }

        public void SetMarker(double latitude, double longitude)
        {
            markers.Clear();
            currPos = new GMarkerGoogle(new PointLatLng(latitude, longitude), GMarkerGoogleType.red_small);
            markers.Markers.Add(currPos);
        }
    }
}