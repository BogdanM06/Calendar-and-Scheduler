using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Azure.Identity;
using Microsoft.Identity.Client;
using System.Drawing.Text;
using System.Diagnostics.CodeAnalysis;
using System.DirectoryServices.ActiveDirectory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Data.SqlClient;

namespace Scheduler_NEA
{
    public partial class MainPage : Form
    {
        private string username;
        public MonthCalendar mCalendar;
        public WeekCalendar wCalendar;
        private string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";
        public MainPage(string u)
        {
            username = u;
            mCalendar = new MonthCalendar(username);
            wCalendar = new WeekCalendar(this, username);
            InitializeComponent();
            this.FormClosing += MainPage_FormClosing;
        }

        private void UserProfilePicture_Click(object sender, EventArgs e)
        {
            //user gets taken to the profile part of settings where they can change their pfp
            var pfp = new PfpSelection(this, username);
            pfp.Show();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            GetPfp();
            //turns images round
            UserProfilePicture.Region = RoundImage(UserProfilePicture);
            SettingsButton.Region = RoundImage(SettingsButton);
            //adds username to text under the profile picture
            CalUser.Text = username;
            //sets the end date to one minute after the start date
            EndDate.Value = StartDate.Value.AddMinutes(1);

            this.AddOwnedForm(wCalendar);
            //loads user month calendar
            this.AddOwnedForm(mCalendar);
            mCalendar.Show();
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        public void GetPfp()
        {
            string query = @"
SELECT ProfilePicture
FROM [User]
WHERE Username = @Username";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string ImageName = reader.GetString(0);
                                //finds the image corrsponding to the stored image name and assigns it to image
                                var image = (Image)Properties.Resources.ResourceManager.GetObject(ImageName);
                                if (image != null)
                                {
                                    //displays the profile picture in the picture box
                                    UserProfilePicture.Image = image;
                                }
                                else
                                {
                                    //sets the profile picture to nothing
                                    UserProfilePicture.Image = null;
                                }
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message(false, ex.Message);
                }
            }
        }

        public Region RoundImage(PictureBox pictureBox)
        {
            var graphicsPath = new GraphicsPath();
            //creates an elipse inside the picture box 
            graphicsPath.AddEllipse(0, 0, pictureBox.Width, pictureBox.Height);
            Region circRegion = new Region(graphicsPath);
            //returns the elipse 
            return circRegion;
        }

        public void UpdateSuggestions(string m)
        {
            //adds a suggestion to the box from the week calendar
            SuggestionBox.Text = m;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AddEventButton_Click(object sender, EventArgs e)
        {
            string eventName = EventName.Text;
            string Repeat = RepeatFrequency.SelectedItem?.ToString();
            DateTime startDate = StartDate.Value;
            DateTime endDate = EndDate.Value;
            bool Priority = PriorityCheck.Checked;
            bool AllDay = AllDayCheck.Checked;
            bool checkEventName = false;
            bool makesSense = true;

            if (Repeat == "Daily")
            {
                if((endDate - startDate).TotalHours >= 24 || startDate.Day != endDate.Day)
                {
                    makesSense = false;
                }
            }
            if (Repeat == "Weekly")
            {
                if(endDate >= startDate.AddDays(7))
                {
                    makesSense = false;
                }
            }
            if (Repeat == "Monthly")
            {
                if(endDate >= startDate.AddMonths(1))
                {
                    makesSense = false;
                }
            }
            if (Repeat == "Annually")
            {
                if (endDate >= startDate.AddYears(1))
                {
                    makesSense = false;
                }
            }

            //checks the event name is filled with at least one letter or number
            foreach (char c in eventName)
            {
                if (char.IsLetterOrDigit(c))
                {
                    checkEventName = true;
                    break;
                }
            }
            if (Repeat == null)
            {
                Message(false, "Repeat needs to be filled.");
            }
            if (checkEventName == false)
            {
                Message(false, "The event name is illegible.");
            }
            if (startDate > endDate)
            {
                Message(false, "The start date is later than the end date.");
            }
            if (makesSense == false)
            {
                Message(false, "Start and end time not compatible with repeat frequency");
            }
            

            if (Repeat != null && checkEventName == true && endDate > startDate && IsEventUnique(eventName) && makesSense)
            {
                //if every detail is filled in and the end time is greater than the start time the event is created
                AddEvent(eventName, startDate, endDate, Priority, AllDay, Repeat);
            }
        }

        //calls method to add event to database and resets calendars and add event display
        private void AddEvent(string n,DateTime s, DateTime e, bool p, bool a, string r)
        {
            //creates an event with the inputs above
            var Event = new Event(n, s, e, p, a, r);
            //adsds the event to database
            Event.AddToDatabase(username);
            //refreshes all the inputs for the 'Add' section
            EventName.Text = "";
            PriorityCheck.Checked = false;
            AllDayCheck.Checked = false;
            RepeatFrequency.SelectedIndex = -1;
            StartDate.Value = DateTime.Now;
            EndDate.Value = DateTime.Now.AddMinutes(1);
            //refreshes month calendar display
            mCalendar.DisplayDays();
            //refreshes week calendar display
            wCalendar.DisplayDays();
            //refreshes suggestions
            wCalendar.SuggestReschedule();
        }

        private bool IsEventUnique(string e)
        {
            //check there is an event by that name
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    //adds the username and eventname to table UserEvents
                    string Query = @"
SELECT COUNT(*)
FROM [Events]
WHERE EventName = @EventName";
                    using (var command = new SqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@EventName", e);
                        //if there any instances of the event returns true
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count == 0)
                        {
                            return true;
                        }
                        else
                        {
                            Message(false, "There is an event by that name. Choose a different name");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //shows message of error being present
                    Message(false, ex.ToString());
                    return false;
                }
            }
        }

        private void Message(bool b, string m)
        {
            //creates toast message to output messages
            var toast = new ToastMessage(b, m);
            this.AddOwnedForm(toast);
            toast.BringToFront();
            toast.Show();

        }

        private void AllDayCheck_CheckedChanged(object sender, EventArgs e)
        {
            bool AllDay = AllDayCheck.Checked;
            if (AllDay)
            {
                DateTime Beginning = StartDate.Value.Date;
                StartDate.Value = Beginning;
                //sets the end time to 11:59 and the start time to 00:00 on the same day
                DateTime End = StartDate.Value.Date;
                EndDate.Value = End.AddHours(23).AddMinutes(59);

                //disables start date and end date so they cannot be altered
                StartDate.Enabled = false;
                EndDate.Enabled = false;
            }
            else
            {
                //enables start date and end date is all-day is unticked
                StartDate.Enabled = true;
                EndDate.Enabled = true;
            }
        }
        
        private void WeekCalendar_Click(object sender, EventArgs e)
        {
            //week calendar display
            wCalendar.Show();
            mCalendar.Hide();
        }

        private void MonthCalendar_Click(object sender, EventArgs e)
        {
            //month calendar display
            mCalendar.Show();
            wCalendar.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private bool CheckEvent()
        {
            //check there is an event by that name
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    //adds the username and eventname to table UserEvents
                    string Query = @"
SELECT COUNT(*)
FROM [UserEvents]
WHERE Username = @Username
AND EventName = @EventName";
                    using (var command = new SqlCommand(Query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@EventName", DEvent.Text);
                        //if there any instances of the event returns true
                        int count = Convert.ToInt32(command.ExecuteScalar());
                        if (count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            Message(false, "There is no event by that name");
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    //shows message of error being present
                    Message(false, ex.ToString());
                    return false;
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            bool CheckEventName = false;

            foreach (char c in DEvent.Text)
            {
                //checks a valid event name is inputted 
                if (char.IsLetterOrDigit(c))
                {
                    CheckEventName = true;
                    break;
                }
                else
                {
                    Message(false, "Enter a valid event name");
                }
            }


            if (CheckEventName && CheckEvent())
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();

                        // Delete the event from UserEvents table
                        string DeleteQuery1 = @"
DELETE FROM [UserEvents] 
WHERE Username = @Username AND EventName = @EventName";

                        using (var DeleteCommand1 = new SqlCommand(DeleteQuery1, connection))
                        {
                            DeleteCommand1.Parameters.AddWithValue("@Username", username);
                            DeleteCommand1.Parameters.AddWithValue("@EventName", DEvent.Text);
                            DeleteCommand1.ExecuteNonQuery();
                        }

                        // Delete the event from Events table
                        string DeleteQuery2 = @"
DELETE FROM [Events] 
WHERE EventName = @EventName";

                        using (var DeleteCommand2 = new SqlCommand(DeleteQuery2, connection))
                        {
                            DeleteCommand2.Parameters.AddWithValue("@EventName", DEvent.Text);
                            DeleteCommand2.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Show error message if something goes wrong
                        Message(false, ex.ToString());
                    }
                }
                
                //resets week calendar and month calendar display
                wCalendar.DisplayDays();
                mCalendar.DisplayDays();
                wCalendar.SuggestReschedule();
            }

            DEvent.Text = "";
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            //turns visibility to the opposite of what it was 
            SettingsPanel.Visible = !SettingsPanel.Visible;

            if (SettingsPanel.Visible)
            {
                SettingsPanel.BringToFront();
            }
            else
            {
                SettingsPanel.SendToBack();
            }
        }

        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            //change password form
            var pChange = new PasswordChangeForm(username);
            pChange.Show();
        }

        private void ChangePfpButton_Click_1(object sender, EventArgs e)
        {
            //change pfp
            var pfp = new PfpSelection(this, username);
            pfp.Show();
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            //returns to login page
            new LoginSignin().Show();
            wCalendar.Close();
            mCalendar.Close();
            this.Hide();
        }

        private void FriendAddButton_Click(object sender, EventArgs e)
        {
            //add friend
            var FriendAdd = new FriendAdd(username, wCalendar);
            FriendAdd.Show();
        }

        private void MeetUpButton_Click(object sender, EventArgs e)
        {
            var MeetUpHub = new MeetupHub(wCalendar, username);
            MeetUpHub.Show();
        }
    }
}