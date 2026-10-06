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
    public partial class InvitationMessage : UserControl
    {
        private Invitation Invitation;
        private string username;
        private MeetupHub MeetupHub;
        private WeekCalendar WeekCalendar;
        public InvitationMessage (MeetupHub m, WeekCalendar w, Invitation i, string u)
        {
            InitializeComponent();
            Invitation = i;
            username = u;
            MeetupHub = m;
            WeekCalendar = w;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void AcceptButton_Click(object sender, EventArgs e)
        {
            Status.Text = "STATUS: \nACCEPTED";
            Status.BackColor = Color.Green;
            //changes the invitation status to Accepted in the database
            Invitation.ChangeStatus("Accepted", username);
            Invitation.Accepted();
            Invitation.Delete();
            //waits 2 secs
            await Task.Delay(2000);
            //refreshes the meetup hub to no longer display the invitation and the week calendar to show added meetup event
            MeetupHub.DisplayInvites();
            WeekCalendar.DisplayDays();
        }

        private async void RejectButton_Click(object sender, EventArgs e)
        {
            Status.Text = "STATUS: \nDECLINED";
            Status.BackColor = Color.Red;
            //changes invitation status to Declined in the database
            Invitation.ChangeStatus("Rejected", username);
            Invitation.Delete();
            //wait 2 secs
            await Task.Delay(2000);
            //refreshes meetup hub so that the declined invitation is no longer displayed
            MeetupHub.DisplayInvites();
        }

        private void InvitationMessage_Load(object sender, EventArgs e)
        {
            DisplayInv();
        }

        private void DisplayInv()
        {
            Status.Text = "STATUS: \n" + Invitation.Status();
            DescBox.Text = Invitation.Desc();
        }
    }
}
