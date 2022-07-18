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
        //private void Form1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    offset.X = e.X;
        //    offset.Y = e.Y;
        //    mouseDown = true;
        //}

        //private void Form1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if(mouseDown==true)
        //    {
        //        Point currentScreenPos = PointToScreen(e.Location);
        //        Location = new Point(currentScreenPos.X - offset.X, currentScreenPos.Y - offset.Y);
        //    }
        //}

        //private void Form1_MouseUp(object sender, MouseEventArgs e)
        //{
        //    mouseDown = false;
        //}
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

        private void Mootube_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            string version = fvi.FileVersion;
            AutoUpdater.DownloadPath = "update";
            AutoUpdater.Start("https://raw.githubusercontent.com/ducanhmai521/Mootube/master/update.xml");
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
