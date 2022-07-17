using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;

namespace Mootube.User_Controls
{
    public partial class homemain : UserControl
    {
        public homemain()
        {
            InitializeComponent();
            guna2WinProgressIndicator1.Hide();
        }
        public static string ytlink = "";
        public static string title = "";
        public static string uploader = "";
        public static TimeSpan? dura;
        private void addUserControl(UserControl userControl)
        {
            Controls.Add(userControl);
            userControl.BringToFront();
        }
        private async void guna2Button4_MouseClick(object sender, MouseEventArgs e)
        {
             try
             {
                label1.Text = "Checking entered link...";
                guna2WinProgressIndicator1.Show();
                ytlink = guna2TextBox1.Text;
                var youtube = new YoutubeClient();
                var video = await youtube.Videos.GetAsync(ytlink);
                title = video.Title;
                uploader = video.Author.ChannelTitle;
                dura = video.Duration;
                homelanding uc = new homelanding();
                addUserControl(uc);
                guna2WinProgressIndicator1.Hide();
            }
             catch
             {
                    label1.Text = "Failed! Please correct your link";
                    guna2WinProgressIndicator1.Hide();
             }
        }

        private async void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    label1.Text = "Checking entered link...";
                    guna2WinProgressIndicator1.Show();
                    ytlink = guna2TextBox1.Text;
                    var youtube = new YoutubeClient();
                    var video = await youtube.Videos.GetAsync(ytlink);
                    title = video.Title;
                    uploader = video.Author.ChannelTitle;
                    dura = video.Duration;
                    homelanding uc = new homelanding();
                    addUserControl(uc);
                    guna2WinProgressIndicator1.Hide();
                }
                catch
                {
                    label1.Text = "Failed! Please correct your link";
                    guna2WinProgressIndicator1.Hide();
                }
            }
        }

    }
}
