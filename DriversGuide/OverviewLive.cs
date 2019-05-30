using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DriversGuide
{
    public partial class OverviewLive : UserControl
    {
        LiveMode FormLive;
        Bitmap bmp;
        Graphics z;

        public OverviewLive(LiveMode caller)
        {
            FormLive = caller;

            InitializeComponent();
            bmp = new Bitmap(picTest.ClientSize.Width, picTest.ClientSize.Height);
            z = Graphics.FromImage(bmp);
        }

        private void DrawInBitmap()
        {
            z.Clear(picTest.BackColor);
            z.SmoothingMode = SmoothingMode.AntiAlias;

            float breite = 130;
            float hoehe = 130;

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmp.Width / breite, bmp.Height / hoehe);

            myMatrix.Translate(breite / 2, hoehe / 2 + 1, MatrixOrder.Prepend);

            z.Transform = myMatrix;

            Pen p1 = new Pen(Color.Black);
            Pen p2 = new Pen(Color.Blue);
            Pen p3 = new Pen(Color.Green);

            z.DrawPie(p1, 10, 10, 50, 50, 0, 30);

           
        }

        private void picTest_Paint(object sender, PaintEventArgs e)
        {
            DrawInBitmap();
            Graphics g = e.Graphics;
            g.DrawImage(bmp, 0, 0);
        }
    }
}
