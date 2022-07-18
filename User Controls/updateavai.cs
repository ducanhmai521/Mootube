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
    public partial class updateavai : UserControl
    {
        bool accepted = false;
        public updateavai()
        {
            InitializeComponent();
        }

        private void updateavai_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            string version = fvi.FileVersion;
            AutoUpdater.DownloadPath = "update";
            label3.Text = "Your current version: " + version;
            label2.Text = "Latest version: " + updatehub.latver;
        }
        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.IsUpdateAvailable)
            {
                if (accepted == true)
                {
                    try
                    {
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            Application.Exit();
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            accepted = true;
            AutoUpdater.Start("https://raw.githubusercontent.com/ducanhmai521/Mootube/master/update.xml");
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            updatehub uc = new updatehub();
            addUserControl(uc);
        }
        private void addUserControl(UserControl userControl)
        {
            Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", "https://github.com/ducanhmai521/Mootube/releases");
        }
    }
}
