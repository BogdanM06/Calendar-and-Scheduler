using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler_NEA
{
    public partial class ToastMessage : Form
    {
       private  int toastX, toastY;
        public ToastMessage(bool t, string message)
        {
            InitializeComponent();
            ToastMessageText.Text = message;
            if (t)
            {
                toastBorder.BackColor = Color.FromArgb(57, 155, 53);
            }
            else
            {
                toastBorder.BackColor = Color.FromArgb(227, 50, 45);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ToastMessage_Load(object sender, EventArgs e)
        {
            Position();
        }

        private void Position()
        {
            this.BringToFront();
            int ScreenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int ScreenWidth = Screen.PrimaryScreen.WorkingArea.Width;

            toastX = ScreenWidth - this.Width - 20;
            toastY = ScreenHeight - this.Height + 50;

            this.Location = new Point(toastX, toastY);
        }

        private void UpTimer_Tick(object sender, EventArgs e)
        {
            //used to make the toast message shoot up on the screen
            toastY -= 10;
            this.Location = new Point(toastX, toastY);
            if (toastY <= 897)
            {
                //stops message from rising on screen
                UpTimer.Stop();
                DownTimer.Start();
            }
        }

        int y = 50;
        private void DownTimer_Tick(object sender, EventArgs e)
        {
            //this starts a timer in which the toast message stands motionless.
            if (--y < 0)
            {
                //after the timer ends, the message descends
                this.Location = new Point(toastX, toastY += 10);
                if (toastY >= 1000)
                {
                    DownTimer.Stop();
                    y = 100;
                    //closes the message
                    this.Close();
                }

            }
        }
        private void ToastMessageText_Click(object sender, EventArgs e)
        {

        }
    }
}
