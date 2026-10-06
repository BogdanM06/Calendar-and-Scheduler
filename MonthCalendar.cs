using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler_NEA
{
    public partial class MonthCalendar : Form
    {
        private int year;
        private int month;
        private int day;
        private string username;
        private string ConnectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public MonthCalendar(string u)
        {
            username = u;
            InitializeComponent();
        }
        private void MonthCalendar_Load(object sender, EventArgs e)
        {
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;

            MonthLabel.Text = DateTime.Now.ToString("MMMM yyyy");
            Position();

            FlowPanelDates.Controls.Clear();
            DisplayDays();
        }

        //used to get a list of events where each event is in the month and year that is selected
        private List<Event> GetMonthEvents()
        {
            var ue = new List<Event>();
            string query = @"
SELECT Events.EventName, Events.EventStart, Events.EventEnd, Events.Priority, Events.AllDay, Events.Repeat
FROM [Events], [UserEvents]
WHERE Events.EventName = UserEvents.EventName
AND UserEvents.Username = @Username
AND YEAR(EventStart) = @Year
AND MONTH(EventStart) = @Month";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (SqlCommand CreateList = new SqlCommand(query, connection))
                    {
                        //adds values
                        CreateList.Parameters.AddWithValue("@Username", username);
                        CreateList.Parameters.AddWithValue("@Year", year);
                        CreateList.Parameters.AddWithValue("@Month", month);

                        using (SqlDataReader reader = CreateList.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //get the value of each event of the user
                                string en = reader.GetString(0); //event name
                                DateTime st = reader.GetDateTime(1); //start time
                                DateTime et = reader.GetDateTime(2); //end time
                                bool p = reader.GetBoolean(3); //priority
                                bool ad = reader.GetBoolean(4); //all-day
                                string r = reader.GetString(5); //repeat

                                //creates event
                                var e = new Event(en, st, et, p, ad, r);
                                //adds event to list
                                ue.Add(e);
                            }
                            reader.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    
                    Debug.WriteLine(false, ex.Message);
                }
            }
            return ue;
        }

        //used to get a user's repeated events
        private List<Event> GetRepeatEvents()
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
                        CreateList.Parameters.AddWithValue("@Username", username);
                        using (SqlDataReader reader = CreateList.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //get the value of each event of the user
                                string en = reader.GetString(0); //event name
                                DateTime st = reader.GetDateTime(1); //start time
                                DateTime et = reader.GetDateTime(2); //end time
                                bool p = reader.GetBoolean(3); //priority
                                bool ad = reader.GetBoolean(4); //all-day
                                string r = reader.GetString(5); //repeat

                                //creates an event
                                var e = new Event(en, st, et, p, ad, r);
                                //adds event to list
                                ure.Add(e);
                            }
                        }
                    }
                }
                catch (Exception e)
                { 
                    Debug.WriteLine(e.Message);
                }
            }
            return ure;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Position()
        {
            //positions the calendar at this point on the main page
            this.Location = new Point(580, 150);
        }

        public void DisplayDays()
        {
            var StartOfTheMonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int DayOfTheWeek = Convert.ToInt32(StartOfTheMonth.DayOfWeek.ToString("d"));
            
            //sets sunday to 7 instead of 0 
            if (DayOfTheWeek == 0)
            {
                DayOfTheWeek = 7;
            }

            //clears previous display
            FlowPanelDates.Controls.Clear();

            //adds empty spaces before the month starts
            for (int i = 1; i < DayOfTheWeek; i++)
            {
                BlankDays blankday = new BlankDays();
                FlowPanelDates.Controls.Add(blankday);
            }

            //creates a new list of the user's monthly events and weekly events
            var userMonthEvents = GetMonthEvents();
            var RepeatEvents = GetRepeatEvents();

            //iterates for the number of days in the month
            for (int i = 1;i <= days; i++)
            {
                //creates a UserControlDays for every day in the month
                var userControlDay = new UserControlDays();
                userControlDay.days(i);
                
                //sets the current day in a different colour so it is easy to know which day is today
                if (i == day && year == DateTime.Now.Year && month == DateTime.Now.Month)
                {
                    userControlDay.BackColor = Color.FromArgb(116, 199, 242);
                }

                var eventForDay = userMonthEvents.Where(e => e.sT().Day == i || e.eT().Day == i).ToList();
                foreach (Event e in eventForDay)
                {
                    //only deals with non-repeat events so events aren't dealt with twice
                    if (e.r() == "None")
                    {
                        //writes event name on day
                        if (e.sT().Date == e.eT().Date)
                        {
                            userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                        }
                        else
                        {
                            //writes event name on start day
                            if (e.sT().Day == i)
                            {
                                userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                            }
                            //writes end plus event name on end day
                            if (e.eT().Day == i)
                            {
                                userControlDay.WriteEvent("END:" + e.eName(), e.sT(), e.eT());
                            }
                        }
                    }
                }
                foreach (Event e in RepeatEvents)
                {
                    if (e.r() == "Daily")
                    {
                        //writes event name as it occurs on every day
                        userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                    }
                    if (e.r() == "Weekly")
                    {
                        // datetime of the day the loop is on
                        DateTime x = new DateTime(year, month, i);
                        //checks if the day of the repeated weekly event is the same as the day of i
                        if (e.sT().Date == e.eT().Date)
                        {
                            if (e.sT().DayOfWeek == x.DayOfWeek)
                            {
                                //writes an event if it lies on the same day
                                userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                            }
                        }
                        else
                        {
                            if (e.sT().DayOfWeek == x.DayOfWeek)
                            {
                                userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                            }
                            else if (e.eT().DayOfWeek == x.DayOfWeek)
                            {
                                userControlDay.WriteEvent("END:" + e.eName(), e.sT(), e.eT());
                            }
                        }
                    }
                    if (e.r() == "Monthly")
                    {
                        if (e.sT().Date == e.eT().Date)
                        {
                            if (e.sT().Day == i)
                            {
                                //writes event if the event lies on tha same day of month
                                userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                            }
                        }
                        else
                        {
                            if(e.sT().Day == i)
                            {
                                //writes event if event start lies on the same day of the month
                                userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                            }
                            else if (e.eT().Day == i)
                            {
                                //writes event if event end lies on the same day of the month
                                userControlDay.WriteEvent("END:" + e.eName(), e.sT(), e.eT());
                            }
                        }
                    }
                    if (e.r() == "Annually")
                    {
                        if (e.sT().Date == e.eT().Date)
                        {
                            if (e.sT().Day == i && e.sT().Month == month)
                            {
                                //writes event if it lies on the same month and day
                                userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                            }
                        }
                        else
                        {
                            if (e.sT().Day == i && e.sT().Month == month)
                            {
                                userControlDay.WriteEvent(e.eName(), e.sT(), e.eT());
                            }
                            else if (e.eT().Day == i && e.eT().Month == month)
                            {
                                userControlDay.WriteEvent("END:" + e.eName(), e.sT(), e.eT());
                            }
                        }

                    }
                }
                //adds the usercontrol day to the display on main page 
                FlowPanelDates.Controls.Add(userControlDay);
            }
        }

        private void MonthLabel_Click(object sender, EventArgs e)
        {

        }

        private void NextMonth_Click(object sender, EventArgs e)
        {
            //clear flow panel
            FlowPanelDates.Controls.Clear();

            //increments year and resets month to 1 
            if (month == 12)
            {
                month = 1;
                year++;
            }
            else
            {
                month++;
            }

            var datetime = new DateTime(year, month, 1);
            //resets month label to cureent month
            MonthLabel.Text = datetime.ToString("MMMM yyyy");
            //displays events for new month
            DisplayDays();
        }

        private void PrevMonth_Click(object sender, EventArgs e)
        {
            FlowPanelDates.Controls.Clear();
            //decrements year and sets month to 12
            if (month == 1)
            {
                month = 12;
                year--;
            }
            else
            {
                month--;
            }

            var datetime = new DateTime(year, month, 1);
            //resets month label to current month
            MonthLabel.Text = datetime.ToString("MMMM yyyy");
            //displays events for new month
            DisplayDays();
        }
    }
}
