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
    public partial class UserControlDays : UserControl
    {
        public UserControlDays()
        {
            InitializeComponent();
        }

        private void UserControlDays_Load(object sender, EventArgs e)
        {

        }

        public void days(int ndays)
        {
            //puts the day number in the day textbox
            Day.Text = ndays.ToString();
        }

        public void WriteEvent(string n, DateTime s, DateTime e)
        {
            //adds events name and time to labels
            if (Event1.Text == "")
            {
                Event1.Text = $"{s:HH:mm}-{e:HH:mm} " + n;   
                Event1.Visible = true;
                
            }
            else if (Event2.Text == "")
            {
                Event2.Text = $"{s:HH:mm}-{e:HH:mm} " + n;
                Event2.Visible = true;
            }
            else if (Event3.Text == "")
            {
                Event3.Text = $"{s:HH:mm}-{e:HH:mm} " + n;
                Event3.Visible = true;
            }
            else if (Event4.Text == "")
            {
                Event4.Text = $"{s:HH:mm}-{e:HH:mm} " + n;
                Event4.Visible = true;
            }
            else
            {
                Event5.Text = $"{s:HH:mm}-{e:HH:mm} " + n;
                Event5.Visible = true;
            }
        }
    }
}
