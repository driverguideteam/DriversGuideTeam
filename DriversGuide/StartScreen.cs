using System;
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
    public partial class StartScreen : Form
    {
        //called when program is started
        //init the start screen
        public StartScreen()
        {
            InitializeComponent();
        }

        //when evaluation button is clicked
        private void btnEval_Click(object sender, EventArgs e)
        {
            //create and show a new evaluation form (DriversGuideApp)
            DriversGuideApp FormEval = new DriversGuideApp(this);
            FormEval.Show();            
        }

        //when livemode button is clicked
        private void btnLive_Click(object sender, EventArgs e)
        {
            //create and show new livemode form (LiveMode)
            LiveMode FormLive = new LiveMode(this);
            FormLive.Show();
        }
    }
}
