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
        public StartScreen()
        {
            InitializeComponent();
        }

        private void btnEval_Click(object sender, EventArgs e)
        {
            DriversGuideApp FormEval = new DriversGuideApp(this);
            FormEval.Show();            
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            LiveMode FormLive = new LiveMode(this);
            FormLive.Show();
        }
    }
}
