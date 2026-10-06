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
    public partial class UserControlWeekDay : UserControl
    {
        public UserControlWeekDay()
        {
            InitializeComponent();
        }

        public void days(int ndays)
        {
            Day.Text = ndays.ToString();
        }

        public void WriteEvent(DateTime s, DateTime e, string n)
        {
            int startpoint;
            int height;
            // every 30 mins will have a box height of 15.6 which rounds to 16
            const int per30 = 16;

            int dif = Convert.ToInt32((e - s).TotalMinutes);

            if ((int)Math.Floor(dif / 30.0)  == 0)
            {
                height = per30;
            }
            else
            {
                height = (int)Math.Ceiling(dif / 30.0) * per30;
            }

            int startpointref = Convert.ToInt32((s - s.Date).TotalMinutes);
            startpoint = (int)Math.Floor(startpointref / 30.0) * per30;

            Label label = new Label();
            label.Text = $"{n} {s:HH:mm} - {e:HH:mm}";
            label.Font = new Font("Segoe UI", 7, FontStyle.Regular);
            label.ForeColor = Color.Black;
            label.BackColor = Color.LightBlue;
            label.AutoSize = false;
            label.BorderStyle = BorderStyle.FixedSingle;
            label.Size = new Size(176, height);
            label.Location = new Point(0, startpoint + 20);

            // Add the label to the form
            this.Controls.Add(label);
            label.BringToFront();
        }

        public void WriteFriendEvent(DateTime s, DateTime e)
        {
            int startpoint;
            int height;
            // every 30 mins will have a box height of 15.6 which rounds to 16
            const int per30 = 16;

            int dif = Convert.ToInt32((e - s).TotalMinutes);

            if ((int)Math.Floor(dif / 30.0) == 0)
            {
                height = per30;
            }
            else
            {
                height = (int)Math.Ceiling(dif / 30.0) * per30;
            }

            int startpointref = Convert.ToInt32((s - s.Date).TotalMinutes);
            startpoint = (int)Math.Floor(startpointref / 30.0) * per30;

            Label label = new Label();
            label.ForeColor = Color.Black;
            label.BackColor = Color.FromArgb(111, 158, 232);
            label.AutoSize = false;
            label.BorderStyle = BorderStyle.None;
            label.Size = new Size(176, height);
            label.Location = new Point(0, startpoint + 20);

            // Add the label to the form
            this.Controls.Add(label);
            //send to background
            label.SendToBack();
        }
    
    }
}
