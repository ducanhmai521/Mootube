using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using YoutubeExplode;
using Microsoft.WindowsAPICodePack.Dialogs;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Converter;
using System.IO;
using VideoLibrary;

namespace Mootube.User_Controls
{
    public partial class homelanding : UserControl
    {
        public homelanding()
        {
            InitializeComponent();
        }
        private async void homelanding_Load(object sender, EventArgs e)
        {
            //theme
            this.BackColor = Mootube.backcolor;
            panel1.BackColor = Mootube.buttonfillcolor;
            label1.ForeColor = Mootube.textcolor;
            label2.ForeColor = Mootube.textcolor;
            label3.ForeColor = Mootube.textcolor;
            label4.ForeColor = Mootube.textcolor;
            label5.ForeColor = Mootube.textcolor;
            guna2Button1.FillColor = Mootube.buttonfillcolor;
            guna2Button1.ForeColor = Mootube.buttonforecolor;
            guna2Button2.FillColor = Mootube.buttonfillcolor;
            guna2Button2.ForeColor = Mootube.buttonforecolor;
            guna2RadioButton1.CheckedState.FillColor = Mootube.buttonforecolor;
            guna2RadioButton1.CheckedState.InnerColor = Mootube.buttonfillcolor;
            guna2RadioButton2.CheckedState.FillColor = Mootube.buttonforecolor;
            guna2RadioButton2.CheckedState.InnerColor = Mootube.buttonfillcolor;
            guna2ComboBox1.FillColor = Mootube.buttonfillcolor;
            guna2ComboBox1.BorderColor = Mootube.buttonforecolor;
            guna2ComboBox1.ForeColor = Mootube.buttonforecolor;
            guna2TextBox2.FillColor = Mootube.buttonfillcolor;
            guna2TextBox2.ForeColor = Mootube.buttonforecolor;
            guna2TextBox2.BorderColor = Mootube.buttonforecolor;
            guna2ProgressBar1.ProgressColor = Mootube.buttonfillcolor;
            guna2ProgressBar1.ProgressColor2 = Mootube.buttonfillcolor;
            guna2ProgressBar1.FillColor = Mootube.backcolor;

            guna2ProgressBar1.Visible = false;
            string username = Environment.UserName;
            System.IO.Directory.CreateDirectory(@"C:\Users\" + username + @"\Downloads\MooTube");
            guna2TextBox2.Text = @"C:\Users\" + username + @"\Downloads\MooTube";
            string ytlink = homemain.ytlink;
            var uri = new Uri(ytlink);
            // you can check host here => uri.Host <= "www.youtube.com"
            var query = HttpUtility.ParseQueryString(uri.Query);
            var videoId = string.Empty;
            if (query.AllKeys.Contains("v"))
            {
                videoId = query["v"];
            }
            else
            {
                videoId = uri.Segments.Last();
            }
            guna2PictureBox1.LoadAsync(@"https://i3.ytimg.com/vi/" + videoId + "/maxresdefault.jpg");
            guna2ComboBox1.SelectedIndex = 1;
            label2.Text = homemain.title;
            label4.Text = "Duration: " + homemain.dura;
            label3.Text = "Uploader: " + homemain.uploader;
        }

        private void guna2RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2RadioButton2.Checked==true)
            {
                guna2ComboBox1.Hide();
                this.panel4.Location = new Point(150, 218);
            }
            else
            {
                this.panel4.Location = new Point(150, 260);
                guna2ComboBox1.Show();
            } 
                
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            homemain uc = new homemain();
            Controls.Add(uc);
            uc.BringToFront();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                guna2TextBox2.Text= dialog.FileName;
            }
        }
        public void UpdateProgressBar(Guna.UI2.WinForms.Guna2ProgressBar pbar, int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                pbar.Value = i;
            }
        }
        public string ReplaceInvalidChars(string filename)
        {
            return string.Join("_", filename.Split(System.IO.Path.GetInvalidFileNameChars()));
        }
        private async void guna2Button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Downloading...";
            label5.Text = "Do not close this app until finished, speed depends on your video quality and computer speed.";
            var youtube = new YoutubeClient();
            UpdateProgressBar(guna2ProgressBar1, 0, 20);
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(homemain.ytlink);
            guna2ProgressBar1.Visible = true;
            publicfolpath = guna2TextBox2.Text;
            string title = ReplaceInvalidChars(homemain.title);
            if (guna2RadioButton2.Checked == true)
            {
                UpdateProgressBar(guna2ProgressBar1, 20, 40);
                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
                string temppath = guna2TextBox2.Text + "\\" + @"DownloadTemp" + @".mp3";
                string realpath = guna2TextBox2.Text + "\\" + title + @".mp3";
                publicpath = realpath;
                UpdateProgressBar(guna2ProgressBar1, 40, 70);
                await youtube.Videos.Streams.DownloadAsync(streamInfo, temppath);
                try
                {
                    System.IO.File.Move(temppath, realpath);
                    System.IO.File.Delete(temppath);
                    UpdateProgressBar(guna2ProgressBar1, 70, 100);
                    downcomp uc = new downcomp();
                    Controls.Add(uc);
                    uc.BringToFront();
                }
                catch
                {
                    MessageBox.Show(@"Unable to move temp file, maybe this video is already downloaded?", @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.IO.File.Delete(temppath);
                    guna2ProgressBar1.Value = 0;
                }
            } //mp3
            else if (guna2RadioButton1.Checked == true && guna2ComboBox1.SelectedIndex==2)
            {
                try
                {
                    UpdateProgressBar(guna2ProgressBar1, 20, 40);
                    var audioStreamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();
                    var videoStreamInfo = streamManifest.GetVideoStreams().First(s => s.VideoQuality.Label == "720p60");
                    var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
                    string temppath = guna2TextBox2.Text + "\\" + @"DownloadTemp" + @".mp4";
                    string realpath = guna2TextBox2.Text + "\\" + title + @".mp4";
                    publicpath = realpath;
                    UpdateProgressBar(guna2ProgressBar1, 40, 70);
                    await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(temppath).Build());
                    try
                    {
                        System.IO.File.Move(temppath, realpath);
                        System.IO.File.Delete(temppath);
                        UpdateProgressBar(guna2ProgressBar1, 70, 100);
                        downcomp uc = new downcomp();
                        Controls.Add(uc);
                        uc.BringToFront();
                    }
                    catch
                    {
                        MessageBox.Show(@"Unable to move temp file, maybe this video is already downloaded?", @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.IO.File.Delete(temppath);
                        guna2ProgressBar1.Value = 0;
                    }
                }
                catch
                {
                    try
                    {
                        UpdateProgressBar(guna2ProgressBar1, 20, 40);
                        var audioStreamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();
                        var videoStreamInfo = streamManifest.GetVideoStreams().First(s => s.VideoQuality.Label == "720p");
                        var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
                        string temppath = guna2TextBox2.Text + "\\" + @"DownloadTemp" + @".mp4";
                        string realpath = guna2TextBox2.Text + "\\" + title + @".mp4";
                        publicpath = realpath;
                        UpdateProgressBar(guna2ProgressBar1, 40, 70);
                        await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(temppath).Build());
                        try
                        {
                            System.IO.File.Move(temppath, realpath);
                            System.IO.File.Delete(temppath);
                            UpdateProgressBar(guna2ProgressBar1, 70, 100);
                            downcomp uc = new downcomp();
                            Controls.Add(uc);
                            uc.BringToFront();
                        }
                        catch
                        {
                            MessageBox.Show(@"Unable to move temp file, maybe this video is already downloaded?", @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            System.IO.File.Delete(temppath);
                            guna2ProgressBar1.Value = 0;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(@"This video is not available in 720p, please lower the quality to continue downloading.", @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }//mp4 medium 720p60/30
            else if (guna2RadioButton1.Checked == true && guna2ComboBox1.SelectedIndex == 1)
            {
                UpdateProgressBar(guna2ProgressBar1, 20, 40);
                var audioStreamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();
                try
                {
                    var videoStreamInfo = streamManifest.GetVideoStreams().First(s => s.VideoQuality.Label == "1080p60");
                    var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
                    string temppath = guna2TextBox2.Text + "\\" + @"DownloadTemp" + @".mp4";
                    string realpath = guna2TextBox2.Text + "\\" + title + @".mp4";
                    publicpath = realpath;
                    UpdateProgressBar(guna2ProgressBar1, 40, 70);
                    await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(temppath).Build());
                    try
                    {
                        System.IO.File.Move(temppath, realpath);
                        System.IO.File.Delete(temppath);
                        UpdateProgressBar(guna2ProgressBar1, 70, 100);
                        downcomp uc = new downcomp();
                        Controls.Add(uc);
                        uc.BringToFront();
                    }
                    catch
                    {
                        MessageBox.Show(@"Unable to move temp file, maybe this video is already downloaded?", @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.IO.File.Delete(temppath);
                        guna2ProgressBar1.Value = 0;
                    }
                }
                catch
                {
                    try
                    {
                        var videoStreamInfo = streamManifest.GetVideoStreams().First(s => s.VideoQuality.Label == "1080p");
                        var streamInfos = new IStreamInfo[] { audioStreamInfo, videoStreamInfo };
                        string temppath = guna2TextBox2.Text + "\\" + @"DownloadTemp" + @".mp4";
                        string realpath = guna2TextBox2.Text + "\\" + title + @".mp4";
                        publicpath = realpath;
                        UpdateProgressBar(guna2ProgressBar1, 40, 70);
                        await youtube.Videos.DownloadAsync(streamInfos, new ConversionRequestBuilder(temppath).Build());
                        try
                        {
                            System.IO.File.Move(temppath, realpath);
                            System.IO.File.Delete(temppath);
                            UpdateProgressBar(guna2ProgressBar1, 70, 100);
                            downcomp uc = new downcomp();
                            Controls.Add(uc);
                            uc.BringToFront();
                        }
                        catch
                        {
                            MessageBox.Show(@"Unable to move temp file, maybe this video is already downloaded?", @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            System.IO.File.Delete(temppath);
                            guna2ProgressBar1.Value = 0;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(@"This video is not available in 1080p, please lower the quality to continue downloading.", @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }//mp4 high 1080p60/30
            else if (guna2RadioButton1.Checked == true && guna2ComboBox1.SelectedIndex == 3)
            {
                UpdateProgressBar(guna2ProgressBar1, 20, 40);
                string path = guna2TextBox2.Text + "\\";
                SaveVideoToDisk(homemain.ytlink, path);
                UpdateProgressBar(guna2ProgressBar1, 40, 100);
                string realpath = guna2TextBox2.Text + "\\" + title + @".mp4";
                publicpath = realpath;
                downcomp uc = new downcomp();
                Controls.Add(uc);
                uc.BringToFront();
            }//mp4 low 480p30 
            else if (guna2RadioButton1.Checked == true && guna2ComboBox1.SelectedIndex == 0)
            {
                string temppath = guna2TextBox2.Text + "\\" + @"DownloadTemp" + @".mp4";
                string realpath = guna2TextBox2.Text + "\\" + title + @".mp4";
                publicpath = realpath;
                UpdateProgressBar(guna2ProgressBar1, 40, 70);
                await youtube.Videos.DownloadAsync(homemain.ytlink, temppath, o => o
                    .SetContainer("mp4") // override format
                    .SetPreset(ConversionPreset.UltraFast) // change preset
                );
                try
                {
                    System.IO.File.Move(temppath, realpath);
                    System.IO.File.Delete(temppath);
                    UpdateProgressBar(guna2ProgressBar1, 70, 100);
                    downcomp uc = new downcomp();
                    Controls.Add(uc);
                    uc.BringToFront();
                }
                catch
                {
                    MessageBox.Show(@"Unable to move temp file, maybe this video is already downloaded?", @"Failed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.IO.File.Delete(temppath);
                    guna2ProgressBar1.Value = 0;
                }
            } //max quality
            label5.Text = " ";
        }
        void SaveVideoToDisk(string link, string folpath)
        {
            var youTube = YouTube.Default;
            var video = youTube.GetVideo(link);
            File.WriteAllBytes(folpath + video.FullName, video.GetBytes());
        }
        public static string publicpath = "";
        public static string publicfolpath = "";
    }
}
