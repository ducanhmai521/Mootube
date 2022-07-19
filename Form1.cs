using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mootube.User_Controls;
using AutoUpdaterDotNET;
using Mootube.Properties;

namespace Mootube
{
    public partial class Mootube : Form
    {
        public Mootube()
        {
            InitializeComponent();
        }
        private void addUserControl(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            homemain uc = new homemain();
            addUserControl(uc);
        }

        private void guna2Button2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2Button2.Checked == true)
            {
                settings uc = new settings();
                addUserControl(uc);
            }
        }

        private void guna2Button1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2Button1.Checked == true)
            {
                homemain uc = new homemain();
                addUserControl(uc);
            }    
        }

        private void guna2Button3_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2Button3.Checked == true)
            {
                about uc = new about();
                addUserControl(uc);
            }
        }
        public static Color backcolor = ColorTranslator.FromHtml("#EEFDEC");
        public static Color textcolor = Color.ForestGreen;
        public static Color buttonforecolor = Color.DarkGreen;
        public static Color buttonfillcolor = ColorTranslator.FromHtml("#CAFFC6");
        private void Mootube_Load(object sender, EventArgs e)
        {
            if (Settings.Default["selectedtheme"].ToString() == "Blue")
            {
                backcolor = Color.LightCyan;
                textcolor = Color.RoyalBlue;
                buttonforecolor = Color.RoyalBlue;
                buttonfillcolor = Color.LightBlue;
                //this
                panel1.BackColor = Color.PowderBlue;
                guna2Button1.CheckedState.FillColor = Color.LightSkyBlue;
                guna2Button2.CheckedState.FillColor = Color.LightSkyBlue;
                guna2Button3.CheckedState.FillColor = Color.LightSkyBlue;
                guna2Button4.CheckedState.FillColor = Color.LightSkyBlue;
            }
            else if (Settings.Default["selectedtheme"].ToString() == "Pride")
            {
                backcolor = Color.Cornsilk;
                textcolor = Color.ForestGreen;
                buttonforecolor = Color.ForestGreen;
                buttonfillcolor = Color.LemonChiffon;
                //this
                panel1.BackColor = ColorTranslator.FromHtml("#FFDCD2");
                guna2Button1.CheckedState.FillColor = Color.LightGreen;
                guna2Button2.CheckedState.FillColor = Color.Khaki;
                guna2Button3.CheckedState.FillColor = Color.LightSkyBlue;
                guna2Button4.CheckedState.FillColor = Color.Plum;
            }
            else if (Settings.Default["selectedtheme"].ToString() == "Dark")
            {
                backcolor = Color.Black;
                textcolor = Color.White;
                buttonforecolor = Color.White;
                buttonfillcolor = Color.DimGray;
                //this
                panel1.BackColor = ColorTranslator.FromHtml("#2a2a29");
                guna2Button1.CheckedState.FillColor = Color.Gray;
                guna2Button1.ForeColor = Color.White;
                guna2Button2.CheckedState.FillColor = Color.Gray;
                guna2Button2.ForeColor = Color.White;
                guna2Button3.CheckedState.FillColor = Color.Gray;
                guna2Button3.ForeColor = Color.White;
                guna2Button4.CheckedState.FillColor = Color.Gray;
                guna2Button4.ForeColor = Color.White;
            }
            this.BackColor = backcolor;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            string version = fvi.FileVersion;
            AutoUpdater.DownloadPath = "update";
            if (Settings.Default["checkforuponstart"].Equals(true))
            {
                AutoUpdater.Start("https://raw.githubusercontent.com/ducanhmai521/Mootube/master/update.xml");
            }    
        }
        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.IsUpdateAvailable)
            {
                updatehub.latver = args.CurrentVersion;
                guna2Button4.Checked = true;
                updateavai uc = new updateavai();
                addUserControl(uc);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            updatehub uc = new updatehub();
            addUserControl(uc);
        }
    }
}
