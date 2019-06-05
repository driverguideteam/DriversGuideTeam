﻿using System;
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
    public partial class Overview : UserControl
    {
        private DriversGuideApp MainForm;
        private Calculations Berechnungen = new Calculations();
        Graphics bar;
        Bitmap barBmp;
        Graphics strCont;
        Bitmap bmpStrCont;
        Graphics info;
        Bitmap bmpInfo;
        DataTable values;

        public Overview(DriversGuideApp caller)
        {
            MainForm = caller;
            InitializeComponent();

            barBmp = new Bitmap(picDistance.ClientSize.Width, picDistance.ClientSize.Height);
            bar = Graphics.FromImage(barBmp);
            bmpStrCont = new Bitmap(picGeneral.ClientSize.Width, picGeneral.ClientSize.Height);
            strCont = Graphics.FromImage(bmpStrCont);
            bmpInfo = new Bitmap(picColdStart.ClientSize.Width, picColdStart.ClientSize.Height);
            info = Graphics.FromImage(bmpInfo);

            values = MainForm.GetValuesDataTable();
            //ShowData();
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
                bar.DrawString(val1.ToString("#.0") + unit, content, style, 2 * borders[1] - 25, -31, sf);
            else
                bar.DrawString(val1.ToString("#.0") + unit, content, style, width1 - 25, -31, sf);

            if (2 * borders[3] > width2)
                bar.DrawString(val2.ToString("#.0") + unit, content, style, 2 * borders[3] - 25, -1, sf);
            else
                bar.DrawString(val2.ToString("#.0") + unit, content, style, width2 - 25, -1, sf);

            if (2 * borders[5] > width3)
                bar.DrawString(val3.ToString("#.0") + unit, content, style, 2 * borders[5] - 25, 29, sf);
            else
                bar.DrawString(val3.ToString("#.0") + unit, content, style, width3 - 25, 29, sf);

            sf.Alignment = StringAlignment.Center;

            bar.DrawString(heading, title, style, 0, -80, sf);
        }

        private void DrawInfoBitmap(double[] val, string[] names, float[] maxVal, float[] borders, string heading, string[] units)
        {
            info.Clear(picColdStart.BackColor);

            float breite = picColdStart.ClientSize.Width;
            float hoehe = picColdStart.ClientSize.Height;
            float max = 69;
            float yVal = -40;
            float yVal1 = yVal + 50;
            float yVal2 = yVal1 + 50;
            Color[] clr = new Color[3];

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmpInfo.Width / breite, bmpInfo.Height / hoehe);

            myMatrix.Translate(10, hoehe / 2 + 1, MatrixOrder.Prepend);

            info.Transform = myMatrix;

            Pen pBorder = new Pen(Color.Black, 1f);
            Font fBord = new Font("Century Gothic", 8f, FontStyle.Bold);
            Brush stBord = new SolidBrush(Color.Black);

            StringFormat sfBord = new StringFormat();
            sfBord.Alignment = StringAlignment.Center;
            sfBord.LineAlignment = StringAlignment.Center;

            info.DrawRectangle(pBorder, 0, yVal, 70, 15);
            info.DrawRectangle(pBorder, 0, yVal1, 70, 15);
            info.DrawRectangle(pBorder, 0, yVal2, 70, 15);

            if (val[0] > borders[0])
                clr[0] = Color.IndianRed;
            else
                clr[0] = Color.MediumSeaGreen;

            if (val[1] > borders[2])
                clr[1] = Color.IndianRed;
            else
                clr[1] = Color.MediumSeaGreen;

            if (val[2] > borders[4])
                clr[2] = Color.IndianRed;
            else
                clr[2] = Color.MediumSeaGreen;

            if (val[0] <= maxVal[0])
                info.FillRectangle(new SolidBrush(clr[0]), 1, yVal + 1, (float)val[0] * max / maxVal[0], 14);
            else
                info.FillRectangle(new SolidBrush(clr[0]), 1, yVal + 1, max, 14);

            info.DrawLine(pBorder, borders[0] * max / maxVal[0] + 1, yVal - 2, borders[0] * max / maxVal[0] + 1, yVal + 17);
            info.DrawString(borders[0].ToString("#"), fBord, stBord, borders[0] * max / maxVal[0] + 1, yVal + 23, sfBord);
            if (borders[1] != 0)
            {
                info.DrawLine(pBorder, borders[1] * max / maxVal[0] + 1, yVal - 2, borders[1] * max / maxVal[0] + 1, yVal + 17);
                info.DrawString(borders[1].ToString("#"), fBord, stBord, borders[1] * max / maxVal[0] + 1, yVal + 23, sfBord);
            }
            info.DrawString(val[0].ToString("#.00") + units[0], fBord, stBord, 75, yVal);

            if (val[1] <= maxVal[1])
                info.FillRectangle(new SolidBrush(clr[1]), 1, yVal1 + 1, (float)val[1] * max / maxVal[1], 14);
            else
                info.FillRectangle(new SolidBrush(clr[1]), 1, yVal1 + 1, max, 14);

            info.DrawLine(pBorder, borders[2] * max / maxVal[1] + 1, yVal1 - 2, borders[2] * max / maxVal[1] + 1, yVal1 + 17);
            info.DrawString(borders[2].ToString("#"), fBord, stBord, borders[2] * max / maxVal[1] + 1, yVal1 + 23, sfBord);
            if (borders[3] != 0)
            {
                info.DrawLine(pBorder, borders[3] * max / maxVal[1] + 1, yVal1 - 2, borders[3] * max / maxVal[1] + 1, yVal1 + 17);
                info.DrawString(borders[3].ToString("#"), fBord, stBord, borders[3] * max / maxVal[1] + 1, yVal1 + 23, sfBord);
            }
            info.DrawString(val[1].ToString("#.00") + units[1], fBord, stBord, 75, yVal1);

            if (val[2] <= maxVal[2])
                info.FillRectangle(new SolidBrush(clr[2]), 1, yVal2 + 1, (float)val[2] * max / maxVal[2], 14);
            else
                info.FillRectangle(new SolidBrush(clr[2]), 1, yVal2 + 1, max, 14);

            info.DrawLine(pBorder, borders[4] * max / maxVal[2] + 1, yVal2 - 2, borders[4] * max / maxVal[2] + 1, yVal2 + 17);
            info.DrawString(borders[4].ToString("#"), fBord, stBord, borders[4] * max / maxVal[2] + 1, yVal2 + 23, sfBord);
            if (borders[5] != 0)
            {
                info.DrawLine(pBorder, borders[5] * max / maxVal[2] + 1, yVal2 - 2, borders[5] * max / maxVal[2] + 1, yVal2 + 17);
                info.DrawString(borders[5].ToString("#"), fBord, stBord, borders[5] * max / maxVal[2] + 1, yVal2 + 23, sfBord);
            }
            info.DrawString(val[2].ToString("#.00") + units[2], fBord, stBord, 75, yVal2);


            Font title = new Font("Century Gothic", 16f, FontStyle.Bold);
            Font content = new Font("Century Gothic", 9f, FontStyle.Bold);
            Brush style = new SolidBrush(Color.Black);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            info.DrawString(names[0], content, style, -2, yVal - 9, sf);
            info.DrawString(names[1], content, style, -2, yVal1 - 9, sf);
            info.DrawString(names[2], content, style, -2, yVal2 - 9, sf);
            info.DrawString(heading, title, style, 0, -70, sf);
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

        private void picGeneral_Paint(object sender, PaintEventArgs e)
        {
            double timeElapsed = Convert.ToDouble(values.Rows[0]["Dauer"]);

            if (timeElapsed <= 95 || timeElapsed >= 115)
                DrawStringBitmap(timeElapsed.ToString("#.00") + " min", Color.Orange);
            else if (timeElapsed < 90 || timeElapsed > 120)
                DrawStringBitmap(timeElapsed.ToString("#.00") + " min", Color.Red);
            else
                DrawStringBitmap(timeElapsed.ToString("#.00") + " min", Color.Black);

            Graphics g = e.Graphics;
            g.DrawImage(bmpStrCont, 0, 0);
        }

        private void picColdStart_Paint(object sender, PaintEventArgs e)
        {
            string[] names = new string[3];
            float[] borders = new float[6];
            float[] max = new float[6];
            double[] val = new double[6];
            string[] units = new string[3];

            names[0] = "Durch. Geschw.";
            names[1] = "Max. Geschw";
            names[2] = "Haltezeit";
            borders[0] = 40;
            borders[1] = 15;
            borders[2] = 60;
            borders[3] = 0;
            borders[4] = 90;
            borders[5] = 0;
            max[0] = 55;
            val[0] = Convert.ToDouble(values.Rows[0]["Kaltstart Durchschnittsgeschwindigkeit"]);
            max[1] = 75;
            val[1] = Convert.ToDouble(values.Rows[0]["Kaltstart Hoechstgeschwindigkeit"]);
            max[2] = 105;
            val[2] = Convert.ToDouble(values.Rows[0]["Kaltstart Haltezeit"]);
            units[0] = " km/h";
            units[1] = " km/h";
            units[2] = " s";

            DrawInfoBitmap(val, names, max, borders, "Test", units);
            Graphics g = e.Graphics;
            g.DrawImage(bmpInfo, 0, 0);
        }
    }
}