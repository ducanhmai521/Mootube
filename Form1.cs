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
                homemain uc = new homemain();
                addUserControl(uc);
                //label1.Text = "Wanna know more 'bout me huh?";
            }
        }
    }
}
