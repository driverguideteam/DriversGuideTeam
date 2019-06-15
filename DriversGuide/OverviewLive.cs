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
        private bool liveRunning = false;

        public OverviewLive(LiveMode caller, bool running)
        {
            FormLive = caller;
            liveRunning = running;
            InitializeComponent();

            barBmp = new Bitmap(picDistance.ClientSize.Width, picDistance.ClientSize.Height);
            bar = Graphics.FromImage(barBmp);
            bmpStrCont = new Bitmap(picGeneral.ClientSize.Width, picGeneral.ClientSize.Height);
            strCont = Graphics.FromImage(bmpStrCont);

            values = FormLive.GetValuesDataTable();
            topBottomSave = FormLive.topBottom;
        }

        public void RefreshData()
        {
            values = FormLive.GetValuesDataTable();
            picDistance.Invalidate();
            picDistribution.Invalidate();
            picGeneral.Invalidate();
        }

        private void DrawStringBitmap(double content, Color colorCont)
        {
            strCont.Clear(picGeneral.BackColor);
            string minutes;
            string seconds;

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

            minutes = Math.Truncate(content).ToString("#00");
            seconds = ((content - Convert.ToDouble(minutes))* 60).ToString("00");

            strCont.DrawString("Dauer: ", typeTitle, styleTitle, 0, 0, sf);
            strCont.DrawString(minutes + ":" + seconds + " min", typeContent, styleContent, 75, 1, sf);            
        }

        private void DrawStringBitmapExt(double content, double content2, Color colorCont)
        {
            strCont.Clear(picGeneral.BackColor);
            string minutes;
            string seconds;

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

            minutes = Math.Truncate(content).ToString("#00");
            seconds = ((content - Convert.ToDouble(minutes)) * 60).ToString("00");

            strCont.DrawString("Dauer: ", typeTitle, styleTitle, 0, -14, sf);
            strCont.DrawString(minutes + ":" + seconds + " min", typeContent, styleContent, 100, -13, sf);
            
            strCont.DrawString("Geschw.: ", typeTitle, styleTitle, 0, 10, sf);
            if (content2 < 100)
                strCont.DrawString(content2.ToString("#00.00") + " km/h", typeContent, styleTitle, 100, 11, sf);            
            else
                strCont.DrawString(content2.ToString("#00.0") + " km/h", typeContent, styleTitle, 100, 11, sf);
        }

        private void DrawBarBitmap(double val1, double val2, double val3, Color clr1, Color clr2, Color clr3, string heading, string unit, float[] borders)
        {
            bar.Clear(picDistance.BackColor);

            float breite = picDistance.ClientSize.Width;
            float hoehe = picDistance.ClientSize.Height;
            float offsetLeft = -29;
            float max = 110;
            float maxVal = 60;

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

            if (val1 <= maxVal)
                bar.FillRectangle(new SolidBrush(clr1), offsetLeft, -40, (float)val1 * max / maxVal, 20);
            else
                bar.FillRectangle(new SolidBrush(clr1), offsetLeft, -40, max, 20);

            if (val2 <= maxVal)
                bar.FillRectangle(new SolidBrush(clr2), offsetLeft, -10, (float)val2 * max / maxVal, 20);
            else
                bar.FillRectangle(new SolidBrush(clr2), offsetLeft, -10, max, 20);

            if (val3 <= maxVal)
                bar.FillRectangle(new SolidBrush(clr3), offsetLeft, 20, (float)val3 * max / maxVal, 20);
            else
                bar.FillRectangle(new SolidBrush(clr3), offsetLeft, 20, max, 20);
            ////bar.FillRectangle(new SolidBrush(clr1), offsetLeft, -40, width1, 20);           
            //bar.FillRectangle(new SolidBrush(clr2), offsetLeft, -10, width2, 20);
            //bar.FillRectangle(new SolidBrush(clr3), offsetLeft, 20, width3, 20);

            bar.DrawLine(pBorder, borders[0] * max / maxVal + offsetLeft, -42, borders[0] * max / maxVal + offsetLeft, -18);
            if (borders[1] != 0)
                bar.DrawLine(pBorder, borders[1] * max / maxVal + offsetLeft, -42, borders[1] * max / maxVal + offsetLeft, -18);

            bar.DrawLine(pBorder, borders[2] * max / maxVal + offsetLeft, -12, borders[2] * max / maxVal + offsetLeft, 12);
            if (borders[3] != 0)
                bar.DrawLine(pBorder, borders[3] * max / maxVal + offsetLeft, -12, borders[3] * max / maxVal + offsetLeft, 12);

            bar.DrawLine(pBorder, borders[4] * max / maxVal + offsetLeft, 18, borders[4] * max / maxVal + offsetLeft, 42);
            if (borders[5] != 0)
                bar.DrawLine(pBorder, borders[5] * max / maxVal + offsetLeft, 18, borders[5] * max / maxVal + offsetLeft, 42);

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

            if (borders[0] > val1)
                bar.DrawString(val1.ToString("0.0") + unit, content, style, borders[0] * max / maxVal - 25, -31, sf);
            else if (val1 <= maxVal)
                bar.DrawString(val1.ToString("0.0") + unit, content, style, (float)val1 * max / maxVal - 25, -31, sf);
            else
                bar.DrawString(val1.ToString("0.0") + unit, content, style, max - 25, -31, sf);

            if (borders[2] > val2)
                bar.DrawString(val2.ToString("0.0") + unit, content, style, borders[2] * max / maxVal - 25, -1, sf);
            else if (val2 <= maxVal)
                bar.DrawString(val2.ToString("0.0") + unit, content, style, (float)val2 * max / maxVal - 25, -1, sf);
            else
                bar.DrawString(val2.ToString("0.0") + unit, content, style, max - 25, -1, sf);

            if (borders[4] > val3)
                bar.DrawString(val3.ToString("0.0") + unit, content, style, borders[4] * max / maxVal - 25, 29, sf);
            else if (val3 <= maxVal)
                bar.DrawString(val3.ToString("0.0") + unit, content, style, (float)val3 * max / maxVal - 25, 29, sf);
            else
                bar.DrawString(val3.ToString("0.0") + unit, content, style, max - 25, 29, sf);            

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

            borders[0] = 44;
            borders[1] = 29;
            borders[2] = 43;
            borders[3] = 23;
            borders[4] = 43;
            borders[5] = 23;

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

            if (liveRunning)
            {
                double speed = Convert.ToDouble(values.Rows[0]["Geschwindigkeit"]);

                if (timeElapsed < 90 || timeElapsed > 120)
                    DrawStringBitmapExt(timeElapsed, speed, Color.Red);
                else if (timeElapsed <= 95 || timeElapsed >= 115)
                    DrawStringBitmapExt(timeElapsed, speed, Color.Orange);
                else
                    DrawStringBitmapExt(timeElapsed, speed, Color.Black);
            }
            else
            {
                if (timeElapsed < 90 || timeElapsed > 120)
                    DrawStringBitmap(timeElapsed, Color.Red);
                else if (timeElapsed <= 95 || timeElapsed >= 115)
                    DrawStringBitmap(timeElapsed, Color.Orange);
                else
                    DrawStringBitmap(timeElapsed, Color.Black);
            }

            Graphics g = e.Graphics;
            g.DrawImage(bmpStrCont, 0, 0);
        }
    }
}
