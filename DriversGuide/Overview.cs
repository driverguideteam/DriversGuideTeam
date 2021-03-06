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
        //Set needed form references
        private DriversGuideApp MainForm;
        private Calculations Berechnungen = new Calculations();

        //create needed graphics and bitmaps
        Graphics bar;
        Bitmap barBmp;
        Graphics strCont;
        Bitmap bmpStrCont;
        Graphics strShortCont;
        Bitmap bmpShortStrCont;
        Graphics info;
        Bitmap bmpInfo;

        //create needed dataTables and lists
        DataTable values;
        DataTable errors;
        DataTable tips;
        List<string> columns = new List<string> {"Distance", "Distribution", "Duration", "Speeds", "ColdStart", "Other", "Urban", "Motorway"};
        List<string> errorsList = new List<string>();
        List<string> tipsList = new List<string>();

        //when the form is called by the evaluation form (DriversGuideApp)
        public Overview(DriversGuideApp caller)
        {
            //connect to DriversGuideApp in order to be able to retrieve information
            MainForm = caller;
            InitializeComponent();

            //initialize the needed bitmaps with the heights and widths of the corresponding pictureBoxes
            //initialize the corresponding graphis with the bitmaps
            barBmp = new Bitmap(picDistance.ClientSize.Width, picDistance.ClientSize.Height);
            bar = Graphics.FromImage(barBmp);
            bmpStrCont = new Bitmap(picGeneral.ClientSize.Width, picGeneral.ClientSize.Height);
            strCont = Graphics.FromImage(bmpStrCont);
            bmpShortStrCont = new Bitmap(picHeadingError.ClientSize.Width, picHeadingError.ClientSize.Height);
            strShortCont = Graphics.FromImage(bmpShortStrCont);
            bmpInfo = new Bitmap(picColdStart.ClientSize.Width, picColdStart.ClientSize.Height);
            info = Graphics.FromImage(bmpInfo);

            //retrieve the values, errors and tips dataTable from DriversGuideApp
            values = MainForm.GetValuesDataTable();            
            errors = MainForm.GetErrorsDataTable();
            tips = MainForm.GetTipsDataTable();

            //write messages from dataTabes errors and tips to lists
            GetMessages();
            
            //show errors only when ride is invalid
            //otherwise it would be empty
            if (!MainForm.GetValidation())
            {
                picHeadingError.Visible = true;
                errorMessages.Visible = true;
            }
        }

        //draw a duration string bitmap from contents
        private void DrawStringBitmap(double content1, string title1, string content2, string title2, Color colorCont)
        {
            //set the correct scale and transform init position
            strCont.Clear(picGeneral.BackColor);
            float breite = picGeneral.ClientSize.Width;
            float hoehe = picGeneral.ClientSize.Height;
            string minutes;
            string seconds;

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmpStrCont.Width / breite, bmpStrCont.Height / hoehe);

            myMatrix.Translate(0, hoehe / 2 + 1, MatrixOrder.Prepend);

            strCont.Transform = myMatrix;

            //set font type, style and color
            Font typeTitle = new Font("Century Gothic", 16f, FontStyle.Bold);
            Font typeContent = new Font("Century Gothic", 14f);
            Font typeState = new Font("Century Gothic", 14f, FontStyle.Underline);
            Brush styleTitle = new SolidBrush(Color.Black);
            Brush styleContent = new SolidBrush(Color.Black);
            Brush styleContent1 = new SolidBrush(colorCont);

            //set string format to needed format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            //seperate content (current time in minutes) into minutes and seconds
            minutes = Math.Truncate(content1).ToString("#00");
            seconds = ((content1 - Convert.ToDouble(minutes)) * 60).ToString("00");

            //draw elapsed time in minutes and seconds as well as the title "Dauer:"
            strCont.DrawString(title1, typeTitle, styleTitle, 0, -35, sf);
            strCont.DrawString(minutes + ":" + seconds + " min", typeContent, styleContent1, 90, -34, sf);

            //draw trip length in km as well as the title "Strecke:"
            strCont.DrawString(title2, typeTitle, styleTitle, 0, -10, sf);
            strCont.DrawString(content2, typeContent, styleContent, 90, -9, sf);

            //draw the state of the ride as well as the title "Status:"
            strCont.DrawString("Status: ", typeTitle, styleTitle, 0, 15, sf);
            if (MainForm.GetValidation())
                strCont.DrawString("Gültig!", typeState, new SolidBrush(Color.MediumSeaGreen), 90, 16, sf);
            else
                strCont.DrawString("Ungültig!", typeState, new SolidBrush(Color.IndianRed), 90, 16, sf);
        }

        //draw title string bitmaps for the headings of tips and errors content
        private void DrawShortStringBitmap(string heading)
        {
            //set the correct scale and transform init position
            strShortCont.Clear(picHeadingError.BackColor);
            float breite = picHeadingError.ClientSize.Width;
            float hoehe = picHeadingError.ClientSize.Height;            

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmpShortStrCont.Width / breite, bmpShortStrCont.Height / hoehe);

            myMatrix.Translate(0, hoehe / 2 + 1, MatrixOrder.Prepend);

            strShortCont.Transform = myMatrix;

            //set font type, style and color
            Font typeTitle = new Font("Century Gothic", 16f, FontStyle.Bold);            
            Brush styleTitle = new SolidBrush(Color.Black);

            //set string format to needed format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            //draw heading
            strShortCont.DrawString(heading, typeTitle, styleTitle, 0, -5, sf);         
        }

        //draw a three bar bar bitmap from val1, val2 and val3
        //val1, val2, val3:     values of the corresponding interval
        //clr1, clr2, clr3:     colors of the corresponding interval in which bar is drawn
        //heading:              heading to be drawn
        //unit:                 unit of the bar data
        //borders:              upper and lower boundries of the interval data (borders[0] upper boundry val1, borders[1] lower boundry val1, ...)
        private void DrawBarBitmap(double val1, double val2, double val3, Color clr1, Color clr2, Color clr3, string heading, string unit, float[] borders)
        {
            //set the correct scale and transform init position
            bar.Clear(picDistance.BackColor);

            float breite = picDistance.ClientSize.Width;
            float hoehe = picDistance.ClientSize.Height;
            float offsetLeft = -29;         //offset to begin at the correct position
            float max = 110;                //maximum displayed length
            float maxVal = 60;              //maximum displayed value that causes a change in length
                      
            Matrix myMatrix = new Matrix();
            myMatrix.Scale(barBmp.Width / breite, barBmp.Height / hoehe);

            myMatrix.Translate(100, hoehe / 2 + 1, MatrixOrder.Prepend);

            bar.Transform = myMatrix;

            //select the pens for drawing base structure
            Pen pLine = new Pen(Color.Black, 1f);
            Pen pBorder = new Pen(Color.Black, 1f);

            //draw base structure
            bar.DrawLine(pLine, -30, -50, -30, 50);

            //draw the bars in the length according to their values
            //limit their maximum length
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

            //draw upper and lower boundries
            //if lower is zero, it is not drawn at all
            bar.DrawLine(pBorder, borders[0] * max / maxVal + offsetLeft, -42, borders[0] * max / maxVal + offsetLeft, -18);
            if (borders[1] != 0)
                bar.DrawLine(pBorder, borders[1] * max / maxVal + offsetLeft, -42, borders[1] * max / maxVal + offsetLeft, -18);

            bar.DrawLine(pBorder, borders[2] * max / maxVal + offsetLeft, -12, borders[2] * max / maxVal + offsetLeft, 12);
            if (borders[3] != 0)
                bar.DrawLine(pBorder, borders[3] * max / maxVal + offsetLeft, -12, borders[3] * max / maxVal + offsetLeft, 12);

            bar.DrawLine(pBorder, borders[4] * max / maxVal + offsetLeft, 18, borders[4] * max / maxVal + offsetLeft, 42);
            if (borders[5] != 0)
                bar.DrawLine(pBorder, borders[5] * max / maxVal + offsetLeft, 18, borders[5] * max / maxVal + offsetLeft, 42);

            //set font, style and color for information strings
            Font content = new Font("Century Gothic", 8f, FontStyle.Bold);
            Brush style = new SolidBrush(Color.Black);
            Font title = new Font("Century Gothic", 16f, FontStyle.Bold);

            //set the correct string format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Far;
            sf.LineAlignment = StringAlignment.Center;

            //draw names for each bar
            bar.DrawString("Stadt", content, style, -32, -31, sf);
            bar.DrawString("Land", content, style, -32, -1, sf);
            bar.DrawString("Autobahn", content, style, -32, 29, sf);

            //change alignment of string
            sf.Alignment = StringAlignment.Near;

            //draw value into string at pleasant position
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

            //change alignment of string
            sf.Alignment = StringAlignment.Center;

            //draw title
            bar.DrawString(heading, title, style, 0, -80, sf);
        }

        //draw a three bar bitmap from val[0], val[1] and val[2]
        //val[]:                values of the corresponding interval
        //names[]:              sub headings of the bars
        //maxVal[]:             maximum values for each bar
        //borders[]:            upper and lower boundries of the interval data (borders[0] upper boundry val[0], borders[1] lower boundry val[0], ...)
        //heading:              heading to be drawn
        //unit:                 unit of the bar data          
        private void DrawInfoBitmap(double[] val, string[] names, float[] maxVal, float[] borders, string heading, string[] units)
        {
            //set the correct scale and transform init position
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

            myMatrix.Translate(60, hoehe / 2 + 1, MatrixOrder.Prepend);

            info.Transform = myMatrix;

            //set pens and brushes as well as font
            Pen pBorder = new Pen(Color.Black, 1f);
            Font fBord = new Font("Century Gothic", 8f, FontStyle.Bold);
            Brush stBord = new SolidBrush(Color.Black);

            //set the correct string format
            StringFormat sfBord = new StringFormat();
            sfBord.Alignment = StringAlignment.Center;
            sfBord.LineAlignment = StringAlignment.Center;

            //draw the three bar frames
            info.DrawRectangle(pBorder, 0, yVal, 70, 15);
            info.DrawRectangle(pBorder, 0, yVal1, 70, 15);
            info.DrawRectangle(pBorder, 0, yVal2, 70, 15);

            //set the color of each bar 
            //red if the value exceeds the corresponding border
            //green if not
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

            //fill the first bar according to the first value
            if (val[0] <= maxVal[0])
                info.FillRectangle(new SolidBrush(clr[0]), 1, yVal + 1, (float)val[0] * max / maxVal[0], 14);
            else
                info.FillRectangle(new SolidBrush(clr[0]), 1, yVal + 1, max, 14);

            //add the borders to the first bar
            info.DrawLine(pBorder, borders[0] * max / maxVal[0] + 1, yVal - 2, borders[0] * max / maxVal[0] + 1, yVal + 17);
            info.DrawString(borders[0].ToString("#"), fBord, stBord, borders[0] * max / maxVal[0] + 1, yVal + 23, sfBord);
            if (borders[1] != 0)
            {
                info.DrawLine(pBorder, borders[1] * max / maxVal[0] + 1, yVal - 2, borders[1] * max / maxVal[0] + 1, yVal + 17);
                info.DrawString(borders[1].ToString("#"), fBord, stBord, borders[1] * max / maxVal[0] + 1, yVal + 23, sfBord);
            }
            info.DrawString(val[0].ToString("0.00") + units[0], fBord, stBord, 75, yVal);

            //fill the second bar according to the first value
            if (val[1] <= maxVal[1])
                info.FillRectangle(new SolidBrush(clr[1]), 1, yVal1 + 1, (float)val[1] * max / maxVal[1], 14);
            else
                info.FillRectangle(new SolidBrush(clr[1]), 1, yVal1 + 1, max, 14);

            //add the borders to the second bar
            info.DrawLine(pBorder, borders[2] * max / maxVal[1] + 1, yVal1 - 2, borders[2] * max / maxVal[1] + 1, yVal1 + 17);
            info.DrawString(borders[2].ToString("#"), fBord, stBord, borders[2] * max / maxVal[1] + 1, yVal1 + 23, sfBord);
            if (borders[3] != 0)
            {
                info.DrawLine(pBorder, borders[3] * max / maxVal[1] + 1, yVal1 - 2, borders[3] * max / maxVal[1] + 1, yVal1 + 17);
                info.DrawString(borders[3].ToString("#"), fBord, stBord, borders[3] * max / maxVal[1] + 1, yVal1 + 23, sfBord);
            }
            info.DrawString(val[1].ToString("0.00") + units[1], fBord, stBord, 75, yVal1);

            //fill the third bar according to the first value
            if (val[2] <= maxVal[2])
                info.FillRectangle(new SolidBrush(clr[2]), 1, yVal2 + 1, (float)val[2] * max / maxVal[2], 14);
            else
                info.FillRectangle(new SolidBrush(clr[2]), 1, yVal2 + 1, max, 14);

            //add the borders to the third bar
            info.DrawLine(pBorder, borders[4] * max / maxVal[2] + 1, yVal2 - 2, borders[4] * max / maxVal[2] + 1, yVal2 + 17);
            info.DrawString(borders[4].ToString("#"), fBord, stBord, borders[4] * max / maxVal[2] + 1, yVal2 + 23, sfBord);
            if (borders[5] != 0)
            {
                info.DrawLine(pBorder, borders[5] * max / maxVal[2] + 1, yVal2 - 2, borders[5] * max / maxVal[2] + 1, yVal2 + 17);
                info.DrawString(borders[5].ToString("#"), fBord, stBord, borders[5] * max / maxVal[2] + 1, yVal2 + 23, sfBord);
            }
            info.DrawString(val[2].ToString("0.00") + units[2], fBord, stBord, 75, yVal2);

            //set a new font for the title and bar sub-headings
            Font title = new Font("Century Gothic", 16f, FontStyle.Bold);
            Font content = new Font("Century Gothic", 9f, FontStyle.Bold);
            Brush style = new SolidBrush(Color.Black);

            //set the correct string format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            //draw bar sub-headings and the overall heading
            info.DrawString(names[0], content, style, -2, yVal - 9, sf);
            info.DrawString(names[1], content, style, -2, yVal1 - 9, sf);
            info.DrawString(names[2], content, style, -2, yVal2 - 9, sf);
            info.DrawString(heading, title, style, -30, -85, sf);
        }

        //write messages from errors and tips lists from calculations
        //to the corresponding lists in this form
        private void GetMessages ()
        {
            //.. by going trhough every element in the tables 
            //and selecting only those, which contain information
            for (int i = 0; i < columns.Count; i++)
            {
                for (int j = 0; j < errors.Rows.Count; j++)
                {
                    if (errors.Rows[j][i].ToString() != "")                    
                        errorsList.Add(errors.Rows[j][i].ToString());                     
                }

                for (int j = 0; j < tips.Rows.Count; j++)
                {
                    if (tips.Rows[j][i].ToString() != "")
                        tipsList.Add(tips.Rows[j][i].ToString());
                }
            }

            //sort the two lists
            errorsList.Sort();
            tipsList.Sort();

            //format them in a pleasant way
            for (int i = 0; i < errorsList.Count; i++)
                errorMessages.Text += "- " + errorsList[i] + "\n";

            for (int i = 0; i < tipsList.Count; i++)
                tipsMessages.Text += "- " + tipsList[i] + "\n";
        }

        //paint barBitmap into distance pictureBox
        private void picDistance_Paint(object sender, PaintEventArgs e)
        {
            //get distances
            double distUrban = Convert.ToDouble(values.Rows[1]["Strecke"]);
            double distRural = Convert.ToDouble(values.Rows[2]["Strecke"]);
            double distMotorway = Convert.ToDouble(values.Rows[3]["Strecke"]);
            float[] borders = new float[6];

            //set corresponding boundries
            borders[0] = 16;
            borders[1] = 0;
            borders[2] = 16;
            borders[3] = 0;
            borders[4] = 16;
            borders[5] = 0;

            //draw bitmap into pictureBox at position (0, 0)
            DrawBarBitmap(distUrban, distRural, distMotorway, Color.IndianRed, Color.MediumSeaGreen, Color.LightSkyBlue, "Strecke", " km", borders);
            Graphics g = e.Graphics;
            g.DrawImage(barBmp, 0, 0);
        }

        //paint barBitmap into distribution pictureBox
        private void picDistribution_Paint(object sender, PaintEventArgs e)
        {
            //get distributions
            double distrUrban = Convert.ToDouble(values.Rows[1]["Verteilung"]);
            double distrRural = Convert.ToDouble(values.Rows[2]["Verteilung"]);
            double distrMotorway = Convert.ToDouble(values.Rows[3]["Verteilung"]);
            float[] borders = new float[6];

            //set corresponding borders
            borders[0] = 44;
            borders[1] = 29;
            borders[2] = 43;
            borders[3] = 23;
            borders[4] = 43;
            borders[5] = 23;

            //draw bitmap into pictureBox at position (0, 0)
            DrawBarBitmap(distrUrban, distrRural, distrMotorway, Color.IndianRed, Color.MediumSeaGreen, Color.LightSkyBlue, "Proz. Verteilung", "%", borders);
            Graphics g = e.Graphics;
            g.DrawImage(barBmp, 0, 0);
        }

        //paint string bitmap with duration, distance and state into General pictureBox
        private void picGeneral_Paint(object sender, PaintEventArgs e)
        {
            //get duration and distance
            double timeElapsed = Convert.ToDouble(values.Rows[0]["Dauer"]);
            double distance = Convert.ToDouble(values.Rows[0]["Strecke"]);

            //draw string bitmap to pictureBox
            //select correct color coding to show if in duration boundries
            if (timeElapsed <= 95 || timeElapsed >= 115)
                DrawStringBitmap(timeElapsed, "Dauer: ", distance.ToString("0.00") + " km", "Strecke:",  Color.Orange);
            else if (timeElapsed < 90 || timeElapsed > 120)
                DrawStringBitmap(timeElapsed, "Dauer: ", distance.ToString("0.00") + " km", "Strecke:", Color.Red);
            else
                DrawStringBitmap(timeElapsed, "Dauer: ", distance.ToString("0.00") + " km", "Strecke:", Color.Black);

            //paint bitmap
            Graphics g = e.Graphics;
            g.DrawImage(bmpStrCont, 0, 0);
        }

        //paint info bitmap into ColdStart pictureBox
        private void picColdStart_Paint(object sender, PaintEventArgs e)
        {
            //create neede variabled
            string[] names = new string[3];
            float[] borders = new float[6];
            float[] max = new float[6];
            double[] val = new double[6];
            string[] units = new string[3];

            //define the sub-headings list
            names[0] = "Durch. Geschw.";
            names[1] = "Max. Geschw";
            names[2] = "Haltezeit";

            //define borders
            borders[0] = 40;
            borders[1] = 15;
            borders[2] = 60;
            borders[3] = 0;
            borders[4] = 90;
            borders[5] = 0;

            //define maximum values and get actual values
            max[0] = 55;
            val[0] = Convert.ToDouble(values.Rows[0]["Kaltstart Durchschnittsgeschwindigkeit"]);
            max[1] = 75;
            val[1] = Convert.ToDouble(values.Rows[0]["Kaltstart Hoechstgeschwindigkeit"]);
            max[2] = 105;
            val[2] = Convert.ToDouble(values.Rows[0]["Kaltstart Haltezeit"]);

            //define units
            units[0] = " km/h";
            units[1] = " km/h";
            units[2] = " s";

            //paint bitmap
            DrawInfoBitmap(val, names, max, borders, "Kaltstart - Phase", units);
            Graphics g = e.Graphics;
            g.DrawImage(bmpInfo, 0, 0);
        }

        //paint short string bitmap error headings pictureBox
        private void picHeadingError_Paint(object sender, PaintEventArgs e)
        {
            //define heading and draw bitmap
            DrawShortStringBitmap("Ungültig weil:");

            //paint bitmap to picturebox
            Graphics g = e.Graphics;
            g.DrawImage(bmpShortStrCont, 0, 0);
        }

        //paint short string bitmap tips headings pictureBox
        private void picHeadingTip_Paint(object sender, PaintEventArgs e)
        {
            //define heading and draw bitmap
            DrawShortStringBitmap("Vorschläge:");

            //paint bitmap to picturebox
            Graphics g = e.Graphics;
            g.DrawImage(bmpShortStrCont, 0, 0);
        }
    }
}
