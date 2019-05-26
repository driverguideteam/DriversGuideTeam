﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriversGuide
{
    public partial class LiveMode : Form
    {
        StartScreen FormStart;
        GPS FormGPS;
        General FormGeneral;
        TestControl FormTest;
        bool topBottom = true;        

        public LiveMode(StartScreen caller)
        {
            FormStart = caller;
            FormStart.Hide();

            InitializeComponent();
        }

        private void pnlTopContent_Click(object sender, EventArgs e)
        {
            topBottom = true;
        }

        private void LiveMode_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormStart.Show();
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            if (topBottom)
            {
                pnlTopContent.Controls.Clear();
                FormTest = new TestControl();
                //myForm.TopLevel = false;
                FormTest.AutoScroll = true;
                pnlTopContent.Controls.Add(FormTest);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormTest.Show();
                FormTest.Dock = DockStyle.Fill;
            }
            else
            {
                pnlBottomContent.Controls.Clear();
                FormTest = new TestControl();
                //myForm.TopLevel = false;
                FormTest.AutoScroll = true;
                pnlBottomContent.Controls.Add(FormTest);
                //myForm.FormBorderStyle = FormBorderStyle.None;
                FormTest.Show();
                FormTest.Dock = DockStyle.Fill;
            }
        }

        private void pnlBottomContent_Click(object sender, EventArgs e)
        {
            topBottom = false;
        }
    }
}
