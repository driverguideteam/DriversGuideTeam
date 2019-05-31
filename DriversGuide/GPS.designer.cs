namespace DriversGuide
{
    partial class GPS
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gMap = new GMap.NET.WindowsForms.GMapControl();
            this.conMenMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.karteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satelitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hybridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kartenquelleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zurückZurRouteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomZurücksetzenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conMenMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // gMap
            // 
            this.gMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gMap.Bearing = 0F;
            this.gMap.CanDragMap = true;
            this.gMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMap.GrayScaleMode = false;
            this.gMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMap.LevelsKeepInMemmory = 5;
            this.gMap.Location = new System.Drawing.Point(0, 0);
            this.gMap.MarkersEnabled = true;
            this.gMap.MaxZoom = 18;
            this.gMap.MinZoom = 2;
            this.gMap.MouseWheelZoomEnabled = true;
            this.gMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMap.Name = "gMap";
            this.gMap.NegativeMode = false;
            this.gMap.PolygonsEnabled = true;
            this.gMap.RetryLoadTile = 0;
            this.gMap.RoutesEnabled = true;
            this.gMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMap.ShowTileGridLines = false;
            this.gMap.Size = new System.Drawing.Size(800, 450);
            this.gMap.TabIndex = 1;
            this.gMap.Zoom = 11D;
            this.gMap.Click += new System.EventHandler(this.gMap_Click);
            // 
            // conMenMap
            // 
            this.conMenMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.karteToolStripMenuItem,
            this.satelitToolStripMenuItem,
            this.hybridToolStripMenuItem,
            this.kartenquelleToolStripMenuItem,
            this.zurückZurRouteToolStripMenuItem,
            this.zoomZurücksetzenToolStripMenuItem});
            this.conMenMap.Name = "conMenMap";
            this.conMenMap.Size = new System.Drawing.Size(178, 136);
            // 
            // karteToolStripMenuItem
            // 
            this.karteToolStripMenuItem.Checked = true;
            this.karteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.karteToolStripMenuItem.Name = "karteToolStripMenuItem";
            this.karteToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.karteToolStripMenuItem.Text = "Karte";
            this.karteToolStripMenuItem.Click += new System.EventHandler(this.karteToolStripMenuItem_Click);
            // 
            // satelitToolStripMenuItem
            // 
            this.satelitToolStripMenuItem.Name = "satelitToolStripMenuItem";
            this.satelitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.satelitToolStripMenuItem.Text = "Satelit";
            this.satelitToolStripMenuItem.Click += new System.EventHandler(this.satelitToolStripMenuItem_Click);
            // 
            // hybridToolStripMenuItem
            // 
            this.hybridToolStripMenuItem.Name = "hybridToolStripMenuItem";
            this.hybridToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.hybridToolStripMenuItem.Text = "Hybrid";
            this.hybridToolStripMenuItem.Click += new System.EventHandler(this.hybridToolStripMenuItem_Click);
            // 
            // kartenquelleToolStripMenuItem
            // 
            this.kartenquelleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.googleToolStripMenuItem,
            this.bingToolStripMenuItem});
            this.kartenquelleToolStripMenuItem.Name = "kartenquelleToolStripMenuItem";
            this.kartenquelleToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.kartenquelleToolStripMenuItem.Text = "Kartenquelle";
            // 
            // googleToolStripMenuItem
            // 
            this.googleToolStripMenuItem.Name = "googleToolStripMenuItem";
            this.googleToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.googleToolStripMenuItem.Text = "Google";
            this.googleToolStripMenuItem.Click += new System.EventHandler(this.googleToolStripMenuItem_Click);
            // 
            // bingToolStripMenuItem
            // 
            this.bingToolStripMenuItem.Name = "bingToolStripMenuItem";
            this.bingToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.bingToolStripMenuItem.Text = "Bing";
            this.bingToolStripMenuItem.Click += new System.EventHandler(this.bingToolStripMenuItem_Click);
            // 
            // zurückZurRouteToolStripMenuItem
            // 
            this.zurückZurRouteToolStripMenuItem.Name = "zurückZurRouteToolStripMenuItem";
            this.zurückZurRouteToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.zurückZurRouteToolStripMenuItem.Text = "Zurück zur Route";
            this.zurückZurRouteToolStripMenuItem.Click += new System.EventHandler(this.zurückZurRouteToolStripMenuItem_Click);
            // 
            // zoomZurücksetzenToolStripMenuItem
            // 
            this.zoomZurücksetzenToolStripMenuItem.Name = "zoomZurücksetzenToolStripMenuItem";
            this.zoomZurücksetzenToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.zoomZurücksetzenToolStripMenuItem.Text = "Zoom zurücksetzen";
            this.zoomZurücksetzenToolStripMenuItem.Click += new System.EventHandler(this.zoomZurücksetzenToolStripMenuItem_Click);
            // 
            // GPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gMap);
            this.Name = "GPS";
            this.Size = new System.Drawing.Size(800, 450);
            this.conMenMap.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMap;
        private System.Windows.Forms.ContextMenuStrip conMenMap;
        private System.Windows.Forms.ToolStripMenuItem karteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satelitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hybridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kartenquelleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem googleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zurückZurRouteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomZurücksetzenToolStripMenuItem;
    }
}
