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
        //create needed Forms
        private DriversGuideApp MainForm;
        private LiveMode LiveForm;

        //DataTable to store dataset
        private DataTable dataset = new DataTable();

        //often needed columns from dataset store in strings for easier use
        string lat = "GPS_Latitude";
        string lon = "GPS_Longitude";
        string speed = "OBD_Vehicle_Speed_(PID_13)";
        string time = "Time";
        
        //create calculations to access methods
        Calculations Berechnungen = new Calculations();

        //set needed variables
        double ZoomLive = 10;
        double ZoomNormal = 11;
        bool live = false;
        bool liveRun = false;
        bool topBottomSave;

        //create needed variables for map
        private GMapProvider provHyb, provMap, provSat;
        private GMapOverlay Live = new GMapOverlay("routeLive");
        private GMapOverlay driven = new GMapOverlay("routeDriven");
        private GMapOverlay markers = new GMapOverlay("markers");
        private GMapRoute routeLiveUrban = new GMapRoute("LiveUrban");
        private GMapRoute routeLiveRural = new GMapRoute("LiveRural");
        private GMapRoute routeLiveMotorway = new GMapRoute("LiveMotorway");
        private GMapMarker currPos;        
        double lastUrban = 0;
        double countUrban = 0;
        double lastRural = 0;
        double countRural = 0;
        double lastMotorway = 0;
        double countMotorway = 0;
        List<PointLatLng> dataPoints = new List<PointLatLng>();

        //define colors for the different intervals
        Pen colorUrban = new Pen(Color.IndianRed, 4);
        Pen colorRural = new Pen(Color.MediumSeaGreen, 4);
        Pen colorMotorway = new Pen(Color.LightSkyBlue, 4);

        //constructor if caller is DriversGuideApp
        public GPS(DriversGuideApp caller)
        {
            InitializeComponent();

            //connect to DriversGuideApp Form
            MainForm = caller;

            //get data from it
            dataset = MainForm.GetCompleteDataTable();
            
            //init the map, center it and add the route
            InitMap();
            CenterMap(lat, lon);
            AddRoute(lat, lon, speed, time);

            //add ContextMenuStrip
            gMap.ContextMenuStrip = conMenMap;

            //add markers to the overlays for the map
            //set zoom
            //set that simulation is not running
            gMap.Overlays.Add(markers);
            gMap.Zoom = ZoomNormal;
            live = false;
        }

        //constructor if caller is LiveMode
        public GPS(LiveMode caller, bool liveRunning)
        {
            InitializeComponent();

            //connect to LiveMode Form
            LiveForm = caller;  
            
            //get data from it
            dataset = LiveForm.GetCompleteDataTable();
            
            //init map
            //set live to true
            //center map
            InitMap();
            live = true;
            CenterMap(lat, lon);

            //if simulation is running
            //else complete route is added at once
            if (liveRunning)
            {
                //set variable to indicate that
                liveRun = true;

                //set colors of routes corresponding to the intervals
                routeLiveUrban.Stroke = colorUrban;
                routeLiveRural.Stroke = colorRural;
                routeLiveMotorway.Stroke = colorMotorway;

                //add the first datapoint to the route
                AddRouteLive(lat, lon, speed, time);

                //add the routes to the overlays on the map
                gMap.Overlays.Add(Live);
                gMap.Overlays.Add(driven);
            }
            else
                AddRoute(lat, lon, speed, time);

            //add ContextMenuStrip
            gMap.ContextMenuStrip = conMenMap;

            //add markers to the overlays for the map
            //set zoom
            gMap.Overlays.Add(markers);
            gMap.Zoom = ZoomLive;

            //remember current position on LiveForm
            topBottomSave = LiveForm.topBottom;
        }

        //refresh the map
        public void RefreshMap()
        {
            //get new data from active form
            if (live)
                dataset = LiveForm.GetCompleteDataTable();
            else
                dataset = MainForm.GetCompleteDataTable();

            //center map to the new position
            //add new data to route
            CenterMap(lat, lon);
            AddRouteLive(lat, lon, speed, time);
        }

        //remember the current position on LiveForm
        public void SetTopBottom(bool pos)
        {
            topBottomSave = pos;
        }

        //set if simulation is running or not
        public void SetRunningState(bool running)
        {
            liveRun = running;
        }        

        //init the map
        private void InitMap()
        {
            //set heigth and width
            gMap.Width = this.ClientSize.Width;
            gMap.Height = this.ClientSize.Height;

            //set the map types later available to google maps
            provMap = GoogleMapProvider.Instance;
            provHyb = GoogleHybridMapProvider.Instance;
            provSat = GoogleSatelliteMapProvider.Instance;
            googleToolStripMenuItem.Checked = true;

            //init map with map type
            gMap.MapProvider = provMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            //don't show center
            //set to move the map with the left mouse button
            gMap.ShowCenter = false;
            gMap.DragButton = MouseButtons.Left;
        }

        //center map
        private void CenterMap(string column_latitude, string column_longitude)
        {
            //create needed variabled
            double maxLat, maxLon, minLat, minLon;
            double centerLat, centerLon;
            
            //check if dataset contains rows
            if (dataset.Rows.Count <= 0)
            {
                //if not, center map to Graz
                centerLat = 47.07995;
                centerLon = 15.9152;
            }
            else if (liveRun)
            {
                //if it does and simulation is running
                //center it to the latest datapoint
                centerLat = (double)dataset.Rows[dataset.Rows.Count - 1][column_latitude];
                centerLon = (double)dataset.Rows[dataset.Rows.Count - 1][column_longitude];
            }
            else
            {
                //if it does and simulation is not running
                //center it to the middle of the route
                maxLat = (double)dataset.Compute("MAX([" + column_latitude + "])", "");
                minLat = (double)dataset.Compute("MIN([" + column_latitude + "])", "");

                maxLon = (double)dataset.Compute("MAX([" + column_longitude + "])", "");
                minLon = (double)dataset.Compute("MIN([" + column_longitude + "])", "");

                centerLat = (maxLat - minLat) / 2 + minLat;
                centerLon = (maxLon - minLon) / 2 + minLon;
            }
                                          
            //set the center on the map
            gMap.Position = new PointLatLng(centerLat, centerLon);
        }

        //add route datapoint after datapoint
        private void AddRouteLive(string column_latitude, string column_longitude, string column_speed, string column_time)
        {
            //create a variable to store latitude and longitude to
            PointLatLng data = new PointLatLng();            

            //check if dataset contains rows
            if (dataset.Rows.Count > 0)
            {
                //get current time and speed
                double time = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_time]);
                double speed = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_speed]);
                
                //add latitude and longitude to data point and dataPoints list
                data.Lat = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_latitude]);
                data.Lng = Convert.ToDouble(dataset.Rows[dataset.Rows.Count - 1][column_longitude]);
                dataPoints.Add(data);

                //check to which interval the latest data belongs
                //and add latest data to corresponding route
                if (speed <= 60)
                {
                    if (time - 1000 == lastUrban)
                    {
                        routeLiveUrban.Points.Add(data);
                    }
                    else if (time - 1000 == lastRural)
                    {
                        //needed to get a smooth route without blank spaces in between
                        driven.Routes.Add(new GMapRoute(dataPoints, "Rural" + countRural.ToString()));
                        driven.Routes[driven.Routes.Count - 1].Stroke = colorRural;
                        routeLiveRural.Clear();
                        routeLiveRural.Name = "LiveUrban" + countRural.ToString();
                        routeLiveUrban.Points.Add(dataPoints[dataPoints.Count - 1]);
                        dataPoints.Clear();
                        dataPoints.Add(routeLiveUrban.Points[routeLiveUrban.Points.Count - 1]);
                        countRural++;                        
                    }
                    else
                    { 
                        driven.Routes.Add(new GMapRoute(dataPoints, "Urban" + countUrban.ToString()));
                        driven.Routes[driven.Routes.Count - 1].Stroke = colorUrban;
                        routeLiveUrban.Clear();
                        routeLiveUrban.Name = "LiveUrban" + countUrban.ToString();
                        dataPoints.Clear();
                        countUrban++;
                    }
                    lastUrban = time;
                }
                else if (speed <= 90)
                {
                    if (time - 1000 == lastRural)
                    {
                        routeLiveRural.Points.Add(data);
                    }
                    else if (time - 1000 == lastUrban)
                    {
                        //needed to get a smooth route without blank spaces in between
                        driven.Routes.Add(new GMapRoute(dataPoints, "Urban" + countUrban.ToString()));
                        driven.Routes[driven.Routes.Count - 1].Stroke = colorUrban;
                        routeLiveUrban.Clear();
                        routeLiveUrban.Name = "LiveUrban" + countUrban.ToString();
                        routeLiveRural.Points.Add(dataPoints[dataPoints.Count - 1]);
                        dataPoints.Clear();
                        dataPoints.Add(routeLiveRural.Points[routeLiveRural.Points.Count - 1]);
                        countUrban++;
                    }
                    else if (time - 1000 == lastMotorway)
                    {
                        //needed to get a smooth route without blank spaces in between
                        driven.Routes.Add(new GMapRoute(dataPoints, "Motorway" + countMotorway.ToString()));
                        driven.Routes[driven.Routes.Count - 1].Stroke = colorMotorway;
                        routeLiveMotorway.Clear();
                        routeLiveMotorway.Name = "LiveMotorway" + countMotorway.ToString();
                        routeLiveRural.Points.Add(dataPoints[dataPoints.Count - 1]);
                        dataPoints.Clear();
                        dataPoints.Add(routeLiveRural.Points[routeLiveRural.Points.Count - 1]);
                        countMotorway++;
                    }
                    else
                    {
                        driven.Routes.Add(new GMapRoute(dataPoints, "Rural" + countRural.ToString()));
                        driven.Routes[driven.Routes.Count - 1].Stroke = colorRural;
                        routeLiveRural.Clear();
                        routeLiveRural.Name = "LiveUrban" + countRural.ToString();
                        dataPoints.Clear();
                        countRural++;
                    }

                    lastRural = time;
                }
                else
                {
                    if (time - 1000 == lastMotorway)
                    {
                        routeLiveMotorway.Points.Add(data);                        
                    }
                    else if (time - 1000 == lastRural)
                    {
                        //needed to get a smooth route without blank spaces in between
                        driven.Routes.Add(new GMapRoute(dataPoints, "Rural" + countRural.ToString()));
                        driven.Routes[driven.Routes.Count - 1].Stroke = colorRural;
                        routeLiveRural.Clear();
                        routeLiveRural.Name = "LiveUrban" + countRural.ToString();
                        routeLiveMotorway.Points.Add(dataPoints[dataPoints.Count - 1]);
                        dataPoints.Clear();
                        dataPoints.Add(routeLiveMotorway.Points[routeLiveMotorway.Points.Count - 1]);
                        countRural++;
                    }
                    else
                    {
                        driven.Routes.Add(new GMapRoute(dataPoints, "Motorway" + countMotorway.ToString()));
                        driven.Routes[driven.Routes.Count - 1].Stroke = colorMotorway;
                        routeLiveMotorway.Clear();
                        routeLiveMotorway.Name = "LiveMotorway" + countMotorway.ToString();
                        dataPoints.Clear();
                        countMotorway++;
                    }

                    lastMotorway = time;
                }

                //clear overlay with current route
                Live.Clear();
                
                //add current routes
                Live.Routes.Add(routeLiveUrban);
                Live.Routes.Add(routeLiveRural);
                Live.Routes.Add(routeLiveMotorway);                               
            }            
        }

        //add route all at once
        private void AddRoute(string column_latitude, string column_longitude, string column_speed, string column_time)
        {
            //create new overlay and needed datapoint lists for each interval
            GMapOverlay routes = new GMapOverlay("routes");
            List<PointLatLng> points = new List<PointLatLng>();
            List<PointLatLng> pointsUrban = new List<PointLatLng>();
            List<PointLatLng> pointsRural = new List<PointLatLng>();
            List<PointLatLng> pointsMotorway = new List<PointLatLng>();

            //create new dataTables to store interval data and temporary data to
            DataTable urban = new DataTable();
            DataTable rural = new DataTable();
            DataTable motorway = new DataTable();
            DataTable dt = new DataTable();

            //call calculations to seperate data into intervals and store them into the new dataTables
            Berechnungen.SepIntervals(dataset, column_speed);
            Berechnungen.GetIntervals(ref urban, ref rural, ref motorway);

            //sort data regarding time
            Berechnungen.SortData(ref urban, column_time);
            Berechnungen.SortData(ref rural, column_time);
            Berechnungen.SortData(ref motorway, column_time);

            //get urban datapoints and draw route
            for (int i = 0; i < urban.Rows.Count; i++)
            {
                //add positions
                points.Add(new PointLatLng(Convert.ToDouble(urban.Rows[i][column_latitude]), Convert.ToDouble(urban.Rows[i][column_longitude])));

                if (i >= 1 && (Convert.ToDouble(urban.Rows[i][column_time]) - (Convert.ToDouble(urban.Rows[(i - 1)][column_time])) > 1000))
                {
                    //needed to fill the blank spaces between routes from different intervals
                    //add needed datapoints from rural interval
                    dt = rural.Select("[" + column_time + "] = " + (Convert.ToInt32(urban.Rows[i - 1][column_time]) + 1000)).CopyToDataTable();

                    points.RemoveAt(points.Count - 1);
                    points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));

                    //add current route to route list and color it correct
                    GMapRoute route = new GMapRoute(points, "Urban");
                    route.Stroke = new Pen(Color.IndianRed, 4);
                    routes.Routes.Add(route);

                    //clear datapoints and begin new route
                    points.Clear();
                    points.Add(new PointLatLng(Convert.ToDouble(urban.Rows[i][column_latitude]), Convert.ToDouble(urban.Rows[i][column_longitude])));
                }
            }

            //add last route to route list and color it corret
            GMapRoute routeA = new GMapRoute(points, "Urban");
            routeA.Stroke = new Pen(Color.IndianRed, 4);
            routes.Routes.Add(routeA);           

            //clear datapoints
            points.Clear();
            
            //get rural datapoints and draw route
            for (int i = 0; i < rural.Rows.Count; i++)
            {
                //add positions
                points.Add(new PointLatLng(Convert.ToDouble(rural.Rows[i][column_latitude]), Convert.ToDouble(rural.Rows[i][column_longitude])));

                if (i >= 1 && (Convert.ToDouble(rural.Rows[i][column_time]) - (Convert.ToDouble(rural.Rows[(i - 1)][column_time])) > 1000))
                {
                    //needed to fill the blank spaces between routes from different intervals
                    //add needed datapoints from urban or motorway interval corresponding to which interval the driver was in before
                    if (Convert.ToDouble(rural.Rows[i - 1]["ai"]) < 0)
                        dt = urban.Select("[" + column_time + "] = " + (Convert.ToInt32(rural.Rows[i - 1][column_time]) + 1000)).CopyToDataTable();
                    else
                        dt = motorway.Select("[" + column_time + "] = " + (Convert.ToInt32(rural.Rows[i - 1][column_time]) + 1000)).CopyToDataTable();

                    points.RemoveAt(points.Count - 1);
                    points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));

                    //add current route to route list and color it correct
                    GMapRoute route = new GMapRoute(points, "Rural");
                    route.Stroke = new Pen(Color.MediumSeaGreen, 4);
                    routes.Routes.Add(route);

                    //clear datapoints and begin new route
                    points.Clear();
                    points.Add(new PointLatLng(Convert.ToDouble(rural.Rows[i][column_latitude]), Convert.ToDouble(rural.Rows[i][column_longitude])));
                }
            }

            //needed to fill the blank spaces between routes from different intervals
            //add needed datapoints from urban interval
            if (rural.Rows.Count > 0)
            {
                dt = urban.Select("[" + column_time + "] = " + (Convert.ToInt32(rural.Rows[rural.Rows.Count - 1][column_time]) + 1000)).CopyToDataTable();
                points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));
            }

            //add last route to overlays and color it corret
            GMapRoute routeB = new GMapRoute(points, "Rural");
            routeB.Stroke = new Pen(Color.MediumSeaGreen, 4);
            routes.Routes.Add(routeB);

            //clear datapoints
            points.Clear();

            //get motorway datapoints and draw route
            for (int i = 0; i < motorway.Rows.Count; i++)
            {
                //add positions
                points.Add(new PointLatLng(Convert.ToDouble(motorway.Rows[i][column_latitude]), Convert.ToDouble(motorway.Rows[i][column_longitude])));

                //needed to fill the blank spaces between routes from different intervals
                //add needed datapoints from rural interval
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

            //needed to fill the blank spaces between routes from different intervals
            //add needed datapoints from rural interval
            if (motorway.Rows.Count > 0)
            {
                dt = rural.Select("[" + column_time + "] = " + (Convert.ToInt32(motorway.Rows[motorway.Rows.Count - 1][column_time]) + 1000)).CopyToDataTable();
                points.Add(new PointLatLng(Convert.ToDouble(dt.Rows[0][column_latitude]), Convert.ToDouble(dt.Rows[0][column_longitude])));
            }

            //add current route to route list and color it correct
            GMapRoute routeC = new GMapRoute(points, "Motorway");
            routeC.Stroke = new Pen(Color.LightSkyBlue, 4);
            routes.Routes.Add(routeC);

            //add routes to overlay
            gMap.Overlays.Add(routes);
        }

        //init toolStripMenuItem karte
        private void karteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set mapProvider to map
            gMap.MapProvider = provMap;
            karteToolStripMenuItem.Checked = true;
            satelitToolStripMenuItem.Checked = false;
            hybridToolStripMenuItem.Checked = false;
        }

        //init toolStripMenuItem satelite
        private void satelitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set mapProvider to satelite
            gMap.MapProvider = provSat;
            satelitToolStripMenuItem.Checked = true;
            karteToolStripMenuItem.Checked = false;
            hybridToolStripMenuItem.Checked = false;
        }

        //center map when clicked
        private void zurückZurRouteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CenterMap(lat, lon);
        }

        //reset zoom in dependence of client size
        private void zoomZurücksetzenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (live)
            {
                //if in live mode, determine the size of the window and set zoom in dependence of it
                if (ClientSize.Height >= 300)
                    gMap.Zoom = ZoomNormal;
                else
                    gMap.Zoom = ZoomLive;
            }                
            else
                gMap.Zoom = ZoomNormal;
        }

        //set the mapProvider to google maps
        private void googleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            provMap = GoogleMapProvider.Instance;
            provHyb = GoogleHybridMapProvider.Instance;
            provSat = GoogleSatelliteMapProvider.Instance;
            googleToolStripMenuItem.Checked = true;
            bingToolStripMenuItem.Checked = false;

            //set map types to new provider
            if (satelitToolStripMenuItem.Checked)
                gMap.MapProvider = provSat;
            else if (karteToolStripMenuItem.Checked)
                gMap.MapProvider = provMap;
            else if (hybridToolStripMenuItem.Checked)
                gMap.MapProvider = provHyb;
        }

        //init toolStripMenuItem hybrid
        private void hybridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //set mapProvider to hybrid
            gMap.MapProvider = provHyb;
            hybridToolStripMenuItem.Checked = true;
            satelitToolStripMenuItem.Checked = false;
            karteToolStripMenuItem.Checked = false;
        }

        //if map is clicked, hand LiveForm the remembered position 
        private void gMap_Click(object sender, EventArgs e)
        {
            if (live)
                LiveForm.topBottom = topBottomSave;
        }

        //set the mapProvider to bing maps
        private void bingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            provMap = BingMapProvider.Instance;
            provHyb = BingHybridMapProvider.Instance;
            provSat = BingSatelliteMapProvider.Instance;
            bingToolStripMenuItem.Checked = true;
            googleToolStripMenuItem.Checked = false;

            //set map types to new provider
            if (satelitToolStripMenuItem.Checked)
                gMap.MapProvider = provSat;
            else if (karteToolStripMenuItem.Checked)
                gMap.MapProvider = provMap;
            else if (hybridToolStripMenuItem.Checked)
                gMap.MapProvider = provHyb;
        }

        //set a marker at the position transmitted from caller
        public void SetMarker(double latitude, double longitude)
        {
            //delete all other markers
            markers.Clear();

            //set new marker at transmitted position and add it to the overlay
            currPos = new GMarkerGoogle(new PointLatLng(latitude, longitude), GMarkerGoogleType.red_small);
            markers.Markers.Add(currPos);
        }
    }
}