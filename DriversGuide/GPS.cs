﻿using System;
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
        DataTable dataset = new DataTable();
        string lat = "GPS_Latitude";
        string lon = "GPS_Longitude";
        string speed = "OBD_Vehicle_Speed_(PID_13)";
        string time = "Time";
        GMapProvider provHyb, provMap, provSat;
        Calculations Berechnungen = new Calculations();

        public GPS(DriversGuideApp caller)
        {
            MainForm = caller;
            //MainForm.Hide();
            dataset = MainForm.GetCompleteDataTable();
            InitializeComponent();
            InitMap();
            CenterMap(lat, lon);
            AddRoute(lat, lon, speed, time);
            gMap.ContextMenuStrip = conMenMap;
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

            maxLat = (double)dataset.Compute("MAX([" + column_latitude + "])", "");
            minLat = (double)dataset.Compute("MIN([" + column_latitude + "])", "");

            maxLon = (double)dataset.Compute("MAX([" + column_longitude + "])", "");
            minLon = (double)dataset.Compute("MIN([" + column_longitude + "])", "");

            centerLat = (maxLat - minLat) / 2 + minLat;
            centerLon = (maxLon - minLon) / 2 + minLon;

            gMap.Position = new PointLatLng(centerLat, centerLon);
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
            dt = urban.Select("[" + column_time + "] = " + (Convert.ToInt32(rural.Rows[rural.Rows.Count - 1][column_time]) + 1000)).CopyToDataTable();
            points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));

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
            dt = rural.Select("[" + column_time + "] = " + (Convert.ToInt32(motorway.Rows[motorway.Rows.Count - 1][column_time]) + 1000)).CopyToDataTable();
            points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));

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
            gMap.Zoom = 11;
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

    }
}