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
        Graphics bar;
        Bitmap barBmp;
        Graphics strCont;
        Bitmap bmpStrCont;
        DataTable values;
        private bool topBottomSave;

        public OverviewLive(LiveMode caller)
        {
            FormLive = caller;

            InitializeComponent();

            barBmp = new Bitmap(picDistance.ClientSize.Width, picDistance.ClientSize.Height);
            bar = Graphics.FromImage(barBmp);
            bmpStrCont = new Bitmap(picGeneral.ClientSize.Width, picGeneral.ClientSize.Height);
            strCont = Graphics.FromImage(bmpStrCont);

            values = FormLive.GetValuesDataTable();
            topBottomSave = FormLive.topBottom;
        }

        private void DrawStringBitmap(string content, Color colorCont)
        {
            strCont.Clear(picGeneral.BackColor);

            float breite = picGeneral.ClientSize.Width;
            float hoehe = picGeneral.ClientSize.Height;

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmpStrCont.Width / breite, bmpStrCont.Height / hoehe);

            myMatrix.Translate(0, hoehe / 2 + 1, MatrixOrder.Prepend);

            strCont.Transform = myMatrix;

            Font typeTitle = new Font("Century Gothic", 16f, FontStyle.Bold);
            Font typeContent = new Font("Century Gothic", 14f);
            Brush styleTitle = new SolidBrush(Color.Black);
            Brush styleContent = new SolidBrush(colorCont);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            strCont.DrawString("Dauer: ", typeTitle, styleTitle, 0, 0, sf);
            strCont.DrawString(content, typeContent, styleContent, 75, 1, sf);
        }

        private void DrawBarBitmap(double val1, double val2, double val3, Color clr1, Color clr2, Color clr3, string heading, string unit, float[] borders)
        {
            bar.Clear(picDistance.BackColor);

            float breite = picDistance.ClientSize.Width;
            float hoehe = picDistance.ClientSize.Height;
            float offsetLeft = -29;

            float width1 = 2f * (float)val1;
            float width2 = 2f * (float)val2;
            float width3 = 2f * (float)val3;

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(barBmp.Width / breite, barBmp.Height / hoehe);

            myMatrix.Translate(100, hoehe / 2 + 1, MatrixOrder.Prepend);

            bar.Transform = myMatrix;

            Pen pLine = new Pen(Color.Black, 1f);
            Pen pBorder = new Pen(Color.Black, 1f);

            bar.DrawLine(pLine, -30, -50, -30, 50);

            bar.FillRectangle(new SolidBrush(clr1), offsetLeft, -40, width1, 20);
            bar.FillRectangle(new SolidBrush(clr2), offsetLeft, -10, width2, 20);
            bar.FillRectangle(new SolidBrush(clr3), offsetLeft, 20, width3, 20);

            bar.DrawLine(pBorder, 2 * borders[0] + offsetLeft, -42, 2 * borders[0] - 29, -18);
            if (borders[1] != 0)
                bar.DrawLine(pBorder, 2 * borders[1] + offsetLeft, -42, 2 * borders[1] - 29, -18);

            bar.DrawLine(pBorder, 2 * borders[2] + offsetLeft, -12, 2 * borders[2] - 29, 12);
            if (borders[3] != 0)
                bar.DrawLine(pBorder, 2 * borders[3] + offsetLeft, -12, 2 * borders[3] - 29, 12);

            bar.DrawLine(pBorder, 2 * borders[4] + offsetLeft, 18, 2 * borders[4] - 29, 42);
            if (borders[5] != 0)
                bar.DrawLine(pBorder, 2 * borders[5] + offsetLeft, 18, 2 * borders[5] - 29, 42);

            Font content = new Font("Century Gothic", 8f, FontStyle.Bold);
            Brush style = new SolidBrush(Color.Black);
            Font title = new Font("Century Gothic", 16f, FontStyle.Bold);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Center;

            bar.DrawString("Stadt", content, style, -32, -31, sf);
            bar.DrawString("Land", content, style, -32, -1, sf);
            bar.DrawString("Autobahn", content, style, -32, 29, sf);

            sf.Alignment = StringAlignment.Near;

            if (2 * borders[1] > width1)
                bar.DrawString(val1.ToString("0.0") + unit, content, style, 2 * borders[1] - 25, -31, sf);
            else
                bar.DrawString(val1.ToString("0.0") + unit, content, style, width1 - 25, -31, sf);

            if (2 * borders[3] > width2)
                bar.DrawString(val2.ToString("0.0") + unit, content, style, 2 * borders[3] - 25, -1, sf);
            else
                bar.DrawString(val2.ToString("0.0") + unit, content, style, width2 - 25, -1, sf);

            if (2 * borders[5] > width3)
                bar.DrawString(val3.ToString("0.0") + unit, content, style, 2 * borders[5] - 25, 29, sf);
            else
                bar.DrawString(val3.ToString("0.0") + unit, content, style, width3 - 25, 29, sf);

            sf.Alignment = StringAlignment.Center;

            bar.DrawString(heading, title, style, 0, -80, sf);
        }

        private void picDistance_Paint(object sender, PaintEventArgs e)
        {
            double distUrban = Convert.ToDouble(values.Rows[1]["Strecke"]);
            double distRural = Convert.ToDouble(values.Rows[2]["Strecke"]);
            double distMotorway = Convert.ToDouble(values.Rows[3]["Strecke"]);
            float[] borders = new float[6];

            borders[0] = 16;
            borders[1] = 0;
            borders[2] = 16;
            borders[3] = 0;
            borders[4] = 16;
            borders[5] = 0;

            DrawBarBitmap(distUrban, distRural, distMotorway, Color.IndianRed, Color.MediumSeaGreen, Color.LightSkyBlue, "Strecke", " km", borders);
            Graphics g = e.Graphics;
            g.DrawImage(barBmp, 0, 0);
        }

        private void picDistribution_Paint(object sender, PaintEventArgs e)
        {
            double distrUrban = Convert.ToDouble(values.Rows[1]["Verteilung"]);
            double distrRural = Convert.ToDouble(values.Rows[2]["Verteilung"]);
            double distrMotorway = Convert.ToDouble(values.Rows[3]["Verteilung"]);
            float[] borders = new float[6];

            borders[0] = 29;
            borders[1] = 44;
            borders[2] = 23;
            borders[3] = 43;
            borders[4] = 23;
            borders[5] = 43;

            DrawBarBitmap(distrUrban, distrRural, distrMotorway, Color.IndianRed, Color.MediumSeaGreen, Color.LightSkyBlue, "Proz. Verteilung", "%", borders);
            Graphics g = e.Graphics;
            g.DrawImage(barBmp, 0, 0);
        }

        private void OverviewLive_Click(object sender, EventArgs e)
        {
            FormLive.topBottom = topBottomSave;
        }

        private void picDistance_Click(object sender, EventArgs e)
        {
            FormLive.topBottom = topBottomSave;
        }

        private void picDistribution_Click(object sender, EventArgs e)
        {
            FormLive.topBottom = topBottomSave;
        }

        private void picGeneral_Click(object sender, EventArgs e)
        {
            FormLive.topBottom = topBottomSave;
        }

        private void picGeneral_Paint(object sender, PaintEventArgs e)
        {
            double timeElapsed = Convert.ToDouble(values.Rows[0]["Dauer"]);

            if (timeElapsed <= 95 || timeElapsed >= 115)
                DrawStringBitmap(timeElapsed.ToString("0.00") + " min", Color.Orange);
            else if (timeElapsed < 90 || timeElapsed > 120)
                DrawStringBitmap(timeElapsed.ToString("0.00") + " min", Color.Red);
            else
                DrawStringBitmap(timeElapsed.ToString("0.00") + " min", Color.Black);

            Graphics g = e.Graphics;
            g.DrawImage(bmpStrCont, 0, 0);
        }
    }
}
