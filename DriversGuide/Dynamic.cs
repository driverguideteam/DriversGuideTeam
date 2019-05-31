using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DriversGuide
{
    public partial class Dynamic : UserControl
    {
        LiveMode FormLive;

        public Dynamic(LiveMode caller)
        {
            FormLive = caller;

            InitializeComponent();
        }
    }
}
