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
    public partial class settings : UserControl
    {
        public settings()
        {
            InitializeComponent();
        }

        private void guna2RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if(guna2RadioButton1.Checked==true)
            {
                Settings.Default["selectedtheme"] = "Default";
            }
            else if(guna2RadioButton2.Checked == true)
            {
                Settings.Default["selectedtheme"] = "Blue";
            }
            else
            {
                Settings.Default["selectedtheme"] = "Pride";
            }
            Settings.Default.Save();
            if(DialogResult.OK == MessageBox.Show(@"A restart is required to change the theme!", @"Info", MessageBoxButtons.OK))
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }
        private void settings_Load(object sender, EventArgs e)
        {
            this.BackColor = Mootube.backcolor;
            label1.ForeColor = Mootube.textcolor;
            label2.ForeColor = Mootube.textcolor;
            guna2CircleButton1.FillColor = Mootube.buttonfillcolor;
            guna2CircleButton1.ForeColor = Mootube.buttonforecolor;
            guna2CircleButton1.BorderColor = Mootube.buttonforecolor;
            if (Settings.Default["selectedtheme"].ToString() == "Default")
            {
                guna2RadioButton1.Checked = true;
            }
            else if (Settings.Default["selectedtheme"].ToString() == "Blue")
            {
                guna2RadioButton2.Checked = true;
            }
            else
            {
                guna2RadioButton3.Checked = true;
            }
        }
    }
}
