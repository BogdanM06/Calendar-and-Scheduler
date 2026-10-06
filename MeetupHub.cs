using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Scheduler_NEA
{
    public partial class MeetupHub : Form
    {
        private WeekCalendar wcal;
        private string username;
        private DateTime StartOfWeek;
        private DateTime EndOfWeek;
        private string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";
        public MeetupHub(WeekCalendar w, string u)
        {
            InitializeComponent();
            wcal = w;
            username = u;
        }

        private void MeetupHub_Load(object sender, EventArgs e)
        {
            //gets the date of the Monday of this week
            DayOfWeek Start = DayOfWeek.Monday;
            int month = DateTime.Now.Month;
            int Diff = DateTime.Now.DayOfWeek - Start;
            if (Diff < 0)
            {
                Diff += 7;
            }
            StartOfWeek = DateTime.Now.AddDays(-Diff).Date;

            if (StartOfWeek.Month != month)
            {
                month = StartOfWeek.Month;
            }
            //gets the date of the Sunday of this week
            EndOfWeek = StartOfWeek.AddDays(6);
            
            //gets list of user's friends
            List<string> userFriends = wcal.GetUserFriends(username);
            FriendSelectCheckBox.Items.Clear();
            //adds each friend names to the check box list
            FriendSelectCheckBox.Items.AddRange(userFriends.ToArray());
            DisplayInvites();
        }

        private void FindFreeTimeButton_Click(object sender, EventArgs e)
        {
            //clears the label
            AvailableTimeBox.Text = "";
            //clears combo box
            ChooseTimeBox.Items.Clear();
            //non-repeated events for week
            var totalweek = new List<Event>();
            var totalrepeat = new List<Event>();
            //used to store repeat friend events
            var UserWeek = GetWeekEvents(username);

            if (GetRepeatEvents(username) != null)
            {
                totalrepeat.AddRange(GetRepeatEvents(username));
            }

            if (UserWeek != null)
            {
                totalweek.AddRange(UserWeek);
            }

            foreach (string friend in FriendSelectCheckBox.CheckedItems)
            {
                List<Event> friendEvents = GetWeekEvents(friend);
                if (friendEvents != null)
                {
                    totalweek.AddRange(friendEvents);
                }
                List<Event> fr = GetRepeatEvents(friend);
                if (fr != null)
                {
                    totalrepeat.AddRange(fr);
                }
            }

            var FreeTime = FindFreeTime(totalweek, totalrepeat);

            if (FreeTime.Any())
            {
                foreach (var slot in FreeTime)
                {
                    //adds each free slot to the label below the check box list so user can see them
                    string freeslot = $"{slot.start:ddd HH:mm} - {slot.end:HH:mm}";
                    AvailableTimeBox.Text += $"{freeslot}, ";
                    ChooseTimeBox.Items.Add(freeslot);
                }
            }
            else
            {
                AvailableTimeBox.Text = "There are no free time slots this week.";
            }
        }
 
        private List<(DateTime start, DateTime end)> FindFreeTime(List<Event> AllList, List<Event> AllRepeat)
        {
            List<(DateTime start, DateTime end)> FreeSlots = new List<(DateTime, DateTime)>();

            for (int i = 0; i < 7; i++)
            {
                DateTime dayStart = StartOfWeek.AddDays(i).Date;
                DateTime dayEnd = dayStart.AddHours(23).AddMinutes(59);
                var Events = AllList.Where(e => e.sT().Date == dayStart || e.eT().Date == dayStart || (e.sT().Date < dayStart && e.eT() > dayStart && e.r() == "None")).ToList();
                //used to store the events of the day
                var DayEvents = new List<Event>();
                //makes all the events are start and begin on the same day
                foreach (Event e in Events)
                {
                    if (e.sT().Date != e.eT().Date)
                    {
                        //event only starts on this day
                        if (e.sT().Date == dayStart)
                        {
                            DateTime eventStart = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.sT().Hour, e.sT().Minute, 0);
                            DayEvents.Add(new Event(e.eName(), eventStart, dayEnd, e.p(), e.aD(), e.r()));
                        }
                        //event ends on this day
                        else if (e.eT().Date == dayStart)
                        {
                            DateTime eventEnd = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.eT().Hour, e.eT().Minute, 0);
                            DayEvents.Add(new Event(e.eName(), dayStart, eventEnd, e.p(), e.aD(), e.r()));
                        }
                        //event must not end or begin on this day, using the day as a middle day 
                        else
                        {
                            DayEvents.Add(new Event(e.eName(), dayStart, dayEnd, e.p(), e.aD(), e.r()));
                        }
                    }
                    else
                    {
                        DayEvents.Add(new Event(e.eName(), e.sT(), e.eT(), e.p(), e.aD(), e.r()));
                    }
                }

                //dealing with repeat events
                //checks to see which repeated user events fall on the current week
                foreach (Event e in AllRepeat)
                {
                    if (e.sT().Date == e.eT().Date)
                    {
                        if (IsEventRepeatingOnDate(e, dayStart))
                        {
                            DateTime eventStart = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.sT().Hour, e.sT().Minute, 0);
                            DateTime eventEnd = eventStart.Add(e.eT() - e.sT());
                            DayEvents.Add(new Event(e.eName(), eventStart, eventEnd, e.p(), e.aD(), e.r()));
                        }
                    }
                    else
                    {
                        if (e.r() == "Weekly")
                        {
                            if (e.sT().DayOfWeek == dayStart.DayOfWeek)
                            {
                                //creates an event for the day from the start time to midnight on the day the evnt begins
                                DateTime eventStart = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.sT().Hour, e.sT().Minute, 0);
                                DayEvents.Add(new Event(e.eName(), eventStart, dayEnd, e.p(), e.aD(), e.r()));
                            }
                            else if (e.eT().DayOfWeek == dayStart.DayOfWeek)
                            {
                                //creates an event for the day from beginning of day to the end of the event on the day the event ends
                                DateTime eventEnd = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.eT().Hour, e.eT().Minute, 0);
                                DayEvents.Add(new Event(e.eName(), dayStart, eventEnd, e.p(), e.aD(), e.r()));
                            }
                            else if (wcal.GetDayWeek(e.sT().DayOfWeek) < wcal.GetDayWeek(dayStart.DayOfWeek) && wcal.GetDayWeek(e.eT().DayOfWeek) > wcal.GetDayWeek(dayStart.DayOfWeek))
                            {
                                //this event began on a day before and ends on a day after so this creates an event that lasts the entire day as the original event take up the entire day
                                DayEvents.Add(new Event(e.eName(), dayStart, dayEnd, e.p(), e.aD(), e.r()));
                            }
                        }
                        if (e.r() == "Monthly")
                        {
                            if (e.sT().Day == dayStart.Day)
                            {
                                DateTime eventStart = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.sT().Hour, e.sT().Minute, 0);
                                DayEvents.Add(new Event(e.eName(), eventStart, dayEnd, e.p(), e.aD(), e.r()));
                            }
                            else if (e.eT().Day == dayStart.Day)
                            {
                                DateTime eventEnd = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.eT().Hour, e.eT().Minute, 0);
                                DayEvents.Add(new Event(e.eName(), dayStart, eventEnd, e.p(), e.aD(), e.r()));
                            }
                            else if (e.sT().Day < dayStart.Day && e.eT().Day > dayStart.Day)
                            {
                                DayEvents.Add(new Event(e.eName(), dayStart, dayEnd, e.p(), e.aD(), e.r()));
                            }
                        }
                        if (e.r() == "Annually")
                        {
                            if (e.sT().Day == dayStart.Day && e.sT().Month == dayStart.Month)
                            {
                                DateTime eventStart = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.sT().Hour, e.sT().Minute, 0);
                                DayEvents.Add(new Event(e.eName(), eventStart, dayEnd, e.p(), e.aD(), e.r()));
                            }
                            else if (e.eT().Day == dayStart.Day && e.eT().Month == dayStart.Month)
                            {
                                DateTime eventEnd = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.eT().Hour, e.eT().Minute, 0);
                                DayEvents.Add(new Event(e.eName(), dayStart, eventEnd, e.p(), e.aD(), e.r()));
                            }
                            else if ((e.sT().Day < dayStart.Day && e.sT().Month == dayStart.Month) && (e.eT().Day > dayStart.Day && e.eT().Month == dayStart.Month))
                            {
                                DayEvents.Add(new Event(e.eName(), dayStart, dayEnd, e.p(), e.aD(), e.r()));
                            }
                        }

                    }
                }
                //sets start to 8 AM and 10 PM so user is not recommended ridiculous times to meet up with friends like 2 AM - 4 AM
                DateTime start = dayStart.AddHours(8);
                DateTime end = dayStart.AddHours(22);
                //order the list by event start
                DayEvents = DayEvents.OrderBy(e => e.sT()).ToList();

                //a variable used to traverse the list and find time in-between start and end times 
                DateTime current = start;
                foreach (var e in DayEvents)
                {
                    if (e.sT() < current && e.eT() > current)
                    {
                        //sets current to event end if an event begins before and ends after current
                        current = e.eT();
                        if (current >= end)
                        {
                            //exits loop if current is greater than or equal to end (10 PM)
                            break;
                        }
                        continue;
                    }
                    //for events that begin after current
                    else if (e.sT() >= current)
                    {
                        DateTime SlotEnd = e.sT();
                        //if event start is after 10PM sets slotEnd to 10PM
                        if (SlotEnd > end)
                        {
                            SlotEnd = end;
                        }
                        //if difference between event start and current is greater than two hours add free slot
                        if ((SlotEnd - current).TotalHours >= 2)
                        {
                            FreeSlots.Add((current, SlotEnd));
                        }
                        //set current to the end of the event
                        current = e.eT();
                        if (current >= end)
                        {
                            //exits loop if current is greater than or equal to 10PM
                            break;
                        }
                    }
                    //if event end is greater than current
                    else if (e.eT() > current)
                    {
                        //set current to end time
                        current = e.eT();
                        if (current >= end)
                        {
                            //leave loop if current is later than or equal to 10PM
                            break;
                        }
                    }
                }
                //checks for free time left over at the end of the day
                if (current < end && (end - current).TotalHours >= 2)
                {
                    FreeSlots.Add((current, end));
                }
            }
            return FreeSlots;
        }

        private List<Event> GetWeekEvents(string u)
        {
            var ue = new List<Event>();
            //gets the user events for that week
            string query = @"
SELECT Events.EventName, Events.EventStart, Events.EventEnd, Events.Priority, Events.AllDay, Events.Repeat
FROM [Events], [UserEvents]
WHERE Events.EventName = UserEvents.EventName
AND UserEvents.Username = @Username
AND Events.EventStart >= @StartDate
AND Events.EventStart <= @EndDate";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (var CreateList = new SqlCommand(query, connection))
                    {
                        CreateList.Parameters.AddWithValue("@Username", u);
                        CreateList.Parameters.AddWithValue("@StartDate", StartOfWeek);
                        CreateList.Parameters.AddWithValue("@EndDate", EndOfWeek.AddHours(23).AddMinutes(59));

                        using (SqlDataReader reader = CreateList.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //get the value of each event of the user
                                string en = reader.GetString(0);
                                DateTime st = reader.GetDateTime(1);
                                DateTime et = reader.GetDateTime(2);
                                bool p = reader.GetBoolean(3);
                                bool ad = reader.GetBoolean(4);
                                string r = reader.GetString(5);

                                //adds event to list
                                var e = new Event(en, st, et, p, ad, r);
                                ue.Add(e);
                            }
                            reader.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ue;
        }

        private List<Event> GetRepeatEvents(string u)
        {
            var ure = new List<Event>();
            string query = @"
    SELECT Events.EventName, Events.EventStart, Events.EventEnd, Events.Priority, Events.AllDay, Events.Repeat
    FROM [Events], [UserEvents]
    WHERE Events.EventName = UserEvents.EventName
    AND UserEvents.Username = @Username
    AND Events.Repeat <> 'None'";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (SqlCommand CreateList = new SqlCommand(query, connection))
                    {
                        CreateList.Parameters.AddWithValue("@Username", u);
                        using (SqlDataReader reader = CreateList.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //get the value of each event of the user
                                string en = reader.GetString(0);
                                DateTime st = reader.GetDateTime(1);
                                DateTime et = reader.GetDateTime(2);
                                bool p = reader.GetBoolean(3);
                                bool ad = reader.GetBoolean(4);
                                string r = reader.GetString(5);

                                //adds event to list
                                var e = new Event(en, st, et, p, ad, r);
                                ure.Add(e);
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return ure;
        }

        //checks if repeat events lie on a day
        private bool IsEventRepeatingOnDate(Event e, DateTime date)
        {
            if (e.r() == "Daily")
            {
                return true;
            }
            if (e.r() == "Weekly" && e.sT().DayOfWeek == date.DayOfWeek)
            {
                return true;
            }
            if (e.r() == "Monthly" && e.sT().Day == date.Day)
            {
                return true;
            }
            if (e.r() == "Annually" && e.sT().Month == date.Month && e.sT().Day == date.Day)
            {
                return true;
            }
            return false;
        }


        private void SendInv_Click(object sender, EventArgs e)
        {
            //create an invitation
            if (ChooseTimeBox.SelectedItem != null)
            {
                string eventTime = ChooseTimeBox.SelectedItem.ToString();
                string[] parts = eventTime.Split(' ');
                
                //splits the eventTime into strings [Mon] [10:00] [-] [12:00]
                string dayname = parts[0]; //first part is the name of the day for example 'Mon'
                string start = parts[1]; //second part is the start time for example 10:00
                string end = parts[3]; // fourth part is end time for example 12:00

                //maps days to the integer needed to be added to StartOfWeek to get the date
                var DayAdd = new Dictionary<string, int>
                {
                    { "Mon", 0 },
                    { "Tue", 1 },
                    { "Wed", 2 },
                    { "Thu", 3 },
                    { "Fri", 4 },
                    { "Sat", 5 },
                    { "Sun", 6 }
                };
                
                int addDays = DayAdd[dayname];
                DateTime date = StartOfWeek.AddDays(addDays);


                DateTime startTime = DateTime.ParseExact($"{date:dd-MM-yyyy} {start}", "dd-MM-yyyy HH:mm", null);
                DateTime endTime = DateTime.ParseExact($"{date:dd-MM-yyyy} {end}", "dd-MM-yyyy HH:mm", null);

                //contains the names of all the friends added to an event
                string invitedFriends = username;

                foreach(string friend in FriendSelectCheckBox.CheckedItems)
                {
                    //adds each friend to list of participants
                    invitedFriends += ", " + friend;
                }

                foreach(string friend in FriendSelectCheckBox.CheckedItems)
                {
                    //removes user from invitedFriends
                    string minusUser = ", " + friend;
                    string descript = invitedFriends.Replace(minusUser, "");
                    //create invitation
                    var inv = new Invitation(username, friend, $"Meet with {invitedFriends}", startTime, endTime, $"Meet with {descript} from {startTime: dd/MM/yy HH:mm} - {endTime: HH:mm}", "Pending");
                    //saves invitation in database
                    inv.SaveInv();
                }               
            }
            else
            {
                Message(false, "Please select a time slot");
            }
        }

        public void DisplayInvites()
        {
            var UserInvs = GetUserInvitations(username);
            //resets invitations display box
            InvitationOutputBox.Controls.Clear();
            foreach (Invitation i in UserInvs)
            {
                //creates an InvitationMessage for each invitation a user has
                var InvMsg = new InvitationMessage(this, wcal, i, username);
                InvitationOutputBox.Controls.Add(InvMsg);
            }
        }

        private List<Invitation> GetUserInvitations(string u)
        {
            var uInv = new List<Invitation>();
            //gets the user events for that week
            string query = @"
SELECT * FROM [Invitations]
WHERE Invitee = @Invitee";
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Invitee", u);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //get the value of each invite of the user
                                string invr = reader.GetString(0);
                                string inve = reader.GetString(1);
                                string n = reader.GetString(2);
                                DateTime st = reader.GetDateTime(3);
                                DateTime et = reader.GetDateTime(4);
                                string desc = reader.GetString(5);
                                string status = reader.GetString(6);
                                
                                //adds invite to list
                                var inv = new Invitation(invr, inve, n, st, et, desc, status);
                                uInv.Add(inv);
                            }
                            reader.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return uInv;
        }


        private void Message(bool t, string msg)
        {
            //outputs message for user to see
            var toast = new ToastMessage(t, msg);
            toast.Show();
        }
    }
} 
