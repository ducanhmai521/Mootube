using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mootube.User_Controls
{
    public partial class downcomp : UserControl
    {
        public downcomp()
        {
            InitializeComponent();
        }

        private void downcomp_Load(object sender, EventArgs e)
        {
            label3.Text = "File Location: " + homelanding.publicpath;
            long fileSizeibBytes = GetFileSize(homelanding.publicpath);
            long fileSizeibMbs = fileSizeibBytes / (1024 * 1024);
            label2.Text = "File size: " + fileSizeibMbs + " MB";
            if (homelanding.publicpath.EndsWith("4"))
            {
                label4.Text = "File type: Video (MP4)";
            }
            else
            {
                label4.Text = "File type: Sound (MP3)";
            }    
        }
        static long GetFileSize(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                return new FileInfo(FilePath).Length;
            }
            return 0;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            homemain uc = new homemain();
            Controls.Add(uc);
            uc.BringToFront();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", homelanding.publicfolpath);
        }
    }
}
