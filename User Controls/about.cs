using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mootube.Properties;

namespace Mootube.User_Controls
{
    public partial class about : UserControl
    {
        public about()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "https://github.com/ducanhmai521/Mootube");
        }

        private void about_Load(object sender, EventArgs e)
        {
            this.BackColor = Mootube.backcolor;
            label1.ForeColor = Mootube.textcolor;
            label2.ForeColor = Mootube.textcolor;
            linkLabel1.LinkColor = Mootube.textcolor;
        }
    }
}
