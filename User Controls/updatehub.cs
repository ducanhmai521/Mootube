using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoUpdaterDotNET;

namespace Mootube.User_Controls
{
    public partial class updatehub : UserControl
    {
        public updatehub()
        {
            InitializeComponent();
        }

        private void updatehub_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            string version = fvi.FileVersion;
            AutoUpdater.DownloadPath = "update";
            label3.Text = "Your current version: " + version;
        }
        public static string latver;
        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.IsUpdateAvailable)
            {
                updateavai uc = new updateavai();
                addUserControl(uc);
            }
            else
            {
                label3.Text = "You're using the latest version!";
            }    
        }
        private void addUserControl(UserControl userControl)
        {
            Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://raw.githubusercontent.com/ducanhmai521/Mootube/master/update.xml");
        }
    }
}
