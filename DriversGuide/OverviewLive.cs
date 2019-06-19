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
        //Set needed form reference
        LiveMode FormLive;

        //create needed graphics and bitmaps
        Graphics bar;
        Bitmap barBmp;
        Graphics strCont;
        Bitmap bmpStrCont;
        DataTable values;

        //create needed variables
        private bool topBottomSave;
        private bool liveRunning = false;

        public OverviewLive(LiveMode caller, bool running)
        {
            //connect to FormLive in order to be able to retrieve information
            FormLive = caller;
            liveRunning = running;
            InitializeComponent();

            //initialize the needed bitmaps with the heights and widths of the corresponding pictureBoxes
            //initialize the corresponding graphis with the bitmaps
            barBmp = new Bitmap(picDistance.ClientSize.Width, picDistance.ClientSize.Height);
            bar = Graphics.FromImage(barBmp);
            bmpStrCont = new Bitmap(picGeneral.ClientSize.Width, picGeneral.ClientSize.Height);
            strCont = Graphics.FromImage(bmpStrCont);

            //retrieve values dataTabel from FormLive
            values = FormLive.GetValuesDataTable();

            //remember current position on FormLive
            topBottomSave = FormLive.topBottom;
        }

        //renews data and refreshes the whole interface when called
        public void RefreshData()
        {            
            values = FormLive.GetValuesDataTable();
            picDistance.Invalidate();
            picDistribution.Invalidate();
            picGeneral.Invalidate();
        }

        //set remembered position in FormLive to needed position (bool pos)
        public void SetTopBottom(bool pos)
        {
            topBottomSave = pos;
        }

        //draw a duration string bitmap from content (in minutes)
        private void DrawStringBitmap(double content, Color colorCont)
        {
            //set the correct scale and transform init position
            strCont.Clear(picGeneral.BackColor);
            string minutes;
            string seconds;

            float breite = picGeneral.ClientSize.Width;
            float hoehe = picGeneral.ClientSize.Height;

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmpStrCont.Width / breite, bmpStrCont.Height / hoehe);

            myMatrix.Translate(0, hoehe / 2 + 1, MatrixOrder.Prepend);

            strCont.Transform = myMatrix;

            //set font type, style and color
            Font typeTitle = new Font("Century Gothic", 16f, FontStyle.Bold);
            Font typeContent = new Font("Century Gothic", 14f);
            Brush styleTitle = new SolidBrush(Color.Black);
            Brush styleContent = new SolidBrush(colorCont);

            //set string format to needed format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            //seperate content (current time in minutes) into minutes and seconds
            minutes = Math.Truncate(content).ToString("#00");
            seconds = ((content - Convert.ToDouble(minutes))* 60).ToString("00");

            //draw elapsed time in minutes and seconds as well as the title "Dauer:"
            strCont.DrawString("Dauer: ", typeTitle, styleTitle, 0, 0, sf);
            strCont.DrawString(minutes + ":" + seconds + " min", typeContent, styleContent, 75, 1, sf);            
        }

        //extended version of DrawStringBitmap, includes the current speed of the vehicle (content 2) in km/h
        private void DrawStringBitmapExt(double content, double content2, Color colorCont)
        {
            //set the correct scale and transform init position
            strCont.Clear(picGeneral.BackColor);
            string minutes;
            string seconds;

            float breite = picGeneral.ClientSize.Width;
            float hoehe = picGeneral.ClientSize.Height;

            Matrix myMatrix = new Matrix();
            myMatrix.Scale(bmpStrCont.Width / breite, bmpStrCont.Height / hoehe);

            myMatrix.Translate(0, hoehe / 2 + 1, MatrixOrder.Prepend);

            strCont.Transform = myMatrix;

            //set font type, style and color
            Font typeTitle = new Font("Century Gothic", 16f, FontStyle.Bold);
            Font typeContent = new Font("Century Gothic", 14f);
            Brush styleTitle = new SolidBrush(Color.Black);
            Brush styleContent = new SolidBrush(colorCont);

            //set string format to needed format
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Center;

            //seperate content (current time in minutes) into minutes and seconds
            minutes = Math.Truncate(content).ToString("#00");
            seconds = ((content - Convert.ToDouble(minutes)) * 60).ToString("00");

            //draw elapsed time in minutes and seconds as well as the title "Dauer:"
            strCont.DrawString("Dauer: ", typeTitle, styleTitle, 0, -14, sf);
            strCont.DrawString(minutes + ":" + seconds + " min", typeContent, styleContent, 100, -13, sf);
            
            //draw current speed and title "Geschw.:"
            strCont.DrawString("Geschw.: ", typeTitle, styleTitle, 0, 10, sf);

            //format current speed to match with of elapsed time
            if (content2 < 100)
                strCont.DrawString(content2.ToString("#00.00") + " km/h", typeContent, styleTitle, 100, 11, sf);            
            else
                strCont.DrawString(content2.ToString("#00.0") + " km/h", typeContent, styleTitle, 100, 11, sf);
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
            float offsetLeft = -29;             //offset to begin at the correct position
            float max = 110;                    //maximum displayed length
            float maxVal = 60;                  //maximum displayed value that causes a change in length

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

        //paint barBitmap into distance pictureBox
        private void picDistance_Paint(object sender, PaintEventArgs e)
        {
            //get current distances
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
            //get current distributions
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

        //if Form is clicked, hand FormLive the remembered position
        //needed to determine where to put new Subform
        private void OverviewLive_Click(object sender, EventArgs e)
        {
            FormLive.topBottom = topBottomSave;
        }

        //if pictureBox is clicked, hand FormLive the remembered position 
        private void picDistance_Click(object sender, EventArgs e)
        {
            FormLive.topBottom = topBottomSave;
        }

        //if pictureBox is clicked, hand FormLive the remembered position 
        private void picDistribution_Click(object sender, EventArgs e)
        {
            FormLive.topBottom = topBottomSave;
        }

        //if pictureBox is clicked, hand FormLive the remembered position 
        private void picGeneral_Click(object sender, EventArgs e)
        {
            FormLive.topBottom = topBottomSave;
        }

        //paint string bitmap with elapsed time and current speed into General pictureBox
        private void picGeneral_Paint(object sender, PaintEventArgs e)
        {
            //get current duration
            double timeElapsed = Convert.ToDouble(values.Rows[0]["Dauer"]);            

            //determine if simulation is running
            if (liveRunning)
            {
                //... if so, get current speed
                double speed = Convert.ToDouble(values.Rows[0]["Geschwindigkeit"]);

                //and draw extended string bitmap to pictureBox
                //select correct color coding to show if in duration boundries
                if (timeElapsed < 90 || timeElapsed > 120)
                    DrawStringBitmapExt(timeElapsed, speed, Color.Red);
                else if (timeElapsed <= 95 || timeElapsed >= 115)
                    DrawStringBitmapExt(timeElapsed, speed, Color.Orange);
                else
                    DrawStringBitmapExt(timeElapsed, speed, Color.Black);
            }
            else
            {
                //.. if not, draw simple string bitmap to pictureBox
                //select correct color coding to show if in duration boundries
                if (timeElapsed < 90 || timeElapsed > 120)
                    DrawStringBitmap(timeElapsed, Color.Red);
                else if (timeElapsed <= 95 || timeElapsed >= 115)
                    DrawStringBitmap(timeElapsed, Color.Orange);
                else
                    DrawStringBitmap(timeElapsed, Color.Black);
            }

            //paint bitmap
            Graphics g = e.Graphics;
            g.DrawImage(bmpStrCont, 0, 0);
        }
    }
}
