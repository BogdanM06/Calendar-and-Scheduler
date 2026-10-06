using Azure.Core.Extensions;
using Microsoft.Data.SqlClient;
using System;
using System.CodeDom;
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
    public partial class WeekCalendar : Form
    {
        private string username;
        private int year;
        private int month;
        private int day;
        private DateTime StartOfWeek;
        private DateTime EndOfWeek;
        private MainPage Main;
        private string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";
        public WeekCalendar(MainPage main, string u)
        {
            username = u;
            Main = main;
            InitializeComponent();
        }

        private void WeekCalendar_Load(object sender, EventArgs e)
        {
            year = DateTime.Now.Year;
            month = DateTime.Now.Month;
            day = DateTime.Now.Day;

            //gets the date of the Monday of this week
            DayOfWeek Start = DayOfWeek.Monday;
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

            Position();
            DisplayDays();
            SuggestReschedule();
        }

        public int GetDayWeek(DayOfWeek day)
        {
            //Sunday is given value 0 by default
            if (day == DayOfWeek.Sunday)
            {
                //if it is Sunday it returns 7 so it is easier to work with when dealing with weekly events
                return 7;
            }
            else
            {
                //if it is not Sunday it returns the same value
                return Convert.ToInt32(day);
            }
        }

        private void Position()
        {
            //determines the form's position on the main page
            this.Location = new Point(580, 150);
        }
        
        private List<Event> GetEvents(string u)
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
                        CreateList.Parameters.AddWithValue("@StartDate", StartOfWeek); //Monday 00:00
                        CreateList.Parameters.AddWithValue("@EndDate", EndOfWeek.AddHours(23).AddMinutes(59)); //Sunday 23:59

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
                    Console.WriteLine(ex.Message);
                }
            }
            return ue;
        }

        //gets all the user's repeated events
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

                                //creates event
                                var e = new Event(en, st, et, p, ad, r);
                                //adds event to list
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

        //gets a list of friends of the user
        public List<string> GetUserFriends(string u)
        {
            List<string> friends = new List<string>();
            string query = @"
SELECT FriendUsername
FROM [UserFriends]
WHERE Username = @Username";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (var CreateList = new SqlCommand(query, connection))
                    {
                        CreateList.Parameters.AddWithValue("@Username", u);

                        using (SqlDataReader reader = CreateList.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //get the name of each friend and adds it to list
                                friends.Add(reader.GetString(0));
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
            return friends;
            
        }

        public void DisplayDays()
        {
            var UserW = GetEvents(username);
            var UserRepeated = GetRepeatEvents(username);
            var UserFriends = GetUserFriends(username);
            
            //resets display
            MondayAllDay.Text = "";
            TuesdayAllDay.Text = "";
            WednesdayAllDay.Text = "";
            ThursdayAllDay.Text = "";
            FridayAllDay.Text = "";
            SaturdayAllDay.Text = "";
            SundayAllDay.Text = "";
            
            //resets the week display
            FlowLayoutPanelWeek.Controls.Clear();

            for (int i = 0; i < 7; i++)
            {
                var userControlWeekDay = new UserControlWeekDay();
                userControlWeekDay.days(StartOfWeek.AddDays(i).Day);

                //displays all-day events
                foreach (Event e in UserW.ToList())
                {
                    if (e.aD() == true && e.r() == "None")
                    {
                        if (e.sT().Date == StartOfWeek.Date)
                        {
                            MondayAllDay.Text = MondayAllDay.Text + " " + e.eName();
                        }
                        if (e.sT().Date == StartOfWeek.AddDays(1).Date)
                        {
                            TuesdayAllDay.Text = TuesdayAllDay.Text + " " + e.eName();
                        }
                        if (e.sT().Date == StartOfWeek.AddDays(2).Date)
                        {
                            WednesdayAllDay.Text = WednesdayAllDay.Text + " " + e.eName();
                        }
                        if (e.sT().Date == StartOfWeek.AddDays(3).Date)
                        {
                            ThursdayAllDay.Text = ThursdayAllDay.Text + " " + e.eName();
                        }
                        if (e.sT().Date == StartOfWeek.AddDays(4).Date)
                        {
                            FridayAllDay.Text = FridayAllDay.Text + " " + e.eName();
                        }
                        if (e.sT().Date == StartOfWeek.AddDays(5).Date)
                        {
                            SaturdayAllDay.Text = SaturdayAllDay.Text + " " + e.eName();
                        }
                        if (e.sT().Date == StartOfWeek.AddDays(6).Date)
                        {
                            SundayAllDay.Text = SundayAllDay.Text + " " + e.eName();
                        }
                        //removes event from list as it has been dealt with
                        UserW.Remove(e);
                    }
                }

                //displays repeated all-day events 
                foreach (Event e in UserRepeated.ToList())
                {
                    if (e.aD() == true)
                    {
                        if (e.r() == "Daily")
                        {
                            MondayAllDay.Text = MondayAllDay.Text + " " + e.eName();
                            TuesdayAllDay.Text = TuesdayAllDay.Text + " " + e.eName();
                            WednesdayAllDay.Text = WednesdayAllDay.Text + " " + e.eName();
                            ThursdayAllDay.Text = ThursdayAllDay.Text + " " + e.eName();
                            FridayAllDay.Text = FridayAllDay.Text + " " + e.eName();
                            SaturdayAllDay.Text = SaturdayAllDay.Text + " " + e.eName();
                            SundayAllDay.Text = SundayAllDay.Text + " " + e.eName();
                        }
                        if (e.r() == "Weekly")
                        {
                            if(e.sT().DayOfWeek == DayOfWeek.Monday)
                            {
                                MondayAllDay.Text = MondayAllDay.Text + " " + e.eName();
                            }
                            if (e.sT().DayOfWeek == DayOfWeek.Tuesday)
                            {
                                TuesdayAllDay.Text = TuesdayAllDay.Text + " " + e.eName();
                            }
                            if (e.sT().DayOfWeek == DayOfWeek.Wednesday)
                            {
                                WednesdayAllDay.Text = WednesdayAllDay.Text + " " + e.eName();
                            }
                            if (e.sT().DayOfWeek == DayOfWeek.Thursday)
                            {
                                ThursdayAllDay.Text = ThursdayAllDay.Text + " " + e.eName();
                            }
                            if (e.sT().DayOfWeek == DayOfWeek.Friday)
                            {
                                FridayAllDay.Text = FridayAllDay.Text + " " + e.eName();
                            }
                            if (e.sT().DayOfWeek == DayOfWeek.Saturday)
                            {
                                SaturdayAllDay.Text = SaturdayAllDay.Text + " " + e.eName();
                            }
                            if (e.sT().DayOfWeek == DayOfWeek.Sunday)
                            {
                                SundayAllDay.Text = SundayAllDay.Text + " " + e.eName();
                            }
                        }
                        if (e.r() == "Monthly")
                        {
                            if (e.sT().Day >= StartOfWeek.Day || e.sT().Day <= EndOfWeek.Day)
                            {
                                if (e.sT().DayOfWeek == DayOfWeek.Monday)
                                {
                                     MondayAllDay.Text = MondayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Tuesday)
                                {
                                    TuesdayAllDay.Text = TuesdayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Wednesday)
                                {
                                    WednesdayAllDay.Text = WednesdayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Thursday)
                                {
                                    ThursdayAllDay.Text = ThursdayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Friday)
                                {
                                    FridayAllDay.Text = FridayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Saturday)
                                {
                                    SaturdayAllDay.Text = SaturdayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Sunday)
                                {
                                    SundayAllDay.Text = SundayAllDay.Text + " " + e.eName();
                                }
                            }
                        }
                        if (e.r() == "Annually")
                        {
                            if (e.sT().Month == StartOfWeek.Month && e.sT().Day >= StartOfWeek.Day || e.sT().Month == EndOfWeek.Month && e.sT().Day <= EndOfWeek.Day)
                            {
                                if (e.sT().DayOfWeek == DayOfWeek.Monday)
                                {
                                    MondayAllDay.Text = MondayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Tuesday)
                                {
                                    TuesdayAllDay.Text = TuesdayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Wednesday)
                                {
                                    WednesdayAllDay.Text = WednesdayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Thursday)
                                {
                                    ThursdayAllDay.Text = ThursdayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Friday)
                                {
                                    FridayAllDay.Text = FridayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Saturday)
                                {
                                    SaturdayAllDay.Text = SaturdayAllDay.Text + " " + e.eName();
                                }
                                if (e.sT().DayOfWeek == DayOfWeek.Sunday)
                                {
                                    SundayAllDay.Text = SundayAllDay.Text + " " + e.eName();
                                }
                            }
                        }
                        //removes event from list as it has already been dealt with
                        UserRepeated.Remove(e);
                    }
                }

                //creates a list of events for that week
                var eventForDay = UserW.Where(e => e.sT().Date == StartOfWeek.AddDays(i).Date || e.eT().Date == StartOfWeek.AddDays(i).Date || (e.sT().Date <= StartOfWeek.AddDays(i).Date && e.eT() >= StartOfWeek.AddDays(i).Date)).ToList();
                
                foreach (Event e in eventForDay)
                {
                    if (e.r() == "None")
                    {
                        if (e.sT().Date == e.eT().Date)
                        {
                            userControlWeekDay.WriteEvent(e.sT(), e.eT(), e.eName());
                        }
                        //multi-day events
                        else
                        {
                            //the event begins on this day but ends at another time
                            if (e.sT().Date == StartOfWeek.AddDays(i).Date)
                            {
                                DateTime endofday = e.sT().Date.AddHours(23).AddMinutes(59);
                                userControlWeekDay.WriteEvent(e.sT(), endofday, e.eName());
                            }
                            //the event ends on this day but began at another time
                            else if (e.eT().Date == StartOfWeek.AddDays(i).Date)
                            {
                                DateTime startofday = e.eT().Date;
                                userControlWeekDay.WriteEvent(startofday, e.eT(), e.eName());
                            }
                            //the event began on a day before and ends on a day after
                            else
                            {
                                DateTime startofday = StartOfWeek.AddDays(i).Date;
                                DateTime endofday = startofday.AddHours(23).AddMinutes(59);
                                userControlWeekDay.WriteEvent(startofday, endofday, e.eName());
                            }
                        }
                    }
                }
                
                //deals with the repeat events
                foreach (Event e in UserRepeated)
                {
                    if (e.r() == "Daily")
                    {
                        userControlWeekDay.WriteEvent(e.sT(), e.eT(), e.eName());
                    }
                    if (e.r() == "Weekly")
                    {
                        if (e.sT().Date == e.eT().Date)
                        {
                            if (e.sT().DayOfWeek == StartOfWeek.AddDays(i).DayOfWeek)
                            {
                                userControlWeekDay.WriteEvent(e.sT(), e.eT(), e.eName());
                            }
                        }
                        //multiday events
                        else
                        {
                            //event begins on day but ends on another
                            if (e.sT().DayOfWeek == StartOfWeek.AddDays(i).DayOfWeek)
                            {
                                DateTime endofday = e.sT().Date.AddHours(23).AddMinutes(59);
                                //writes event 
                                userControlWeekDay.WriteEvent(e.sT(), endofday, e.eName());
                            }
                            //event ends on this day but began on another day
                            else if (e.eT().DayOfWeek == StartOfWeek.AddDays(i).DayOfWeek)
                            {
                                DateTime startofday = e.eT().Date;
                                //writes event from 00:00 to event end time 
                                userControlWeekDay.WriteEvent(startofday, e.eT(), e.eName());
                            }
                            //event takes place on day but does not begin or end on it
                            else if (GetDayWeek(e.sT().DayOfWeek) < GetDayWeek(StartOfWeek.AddDays(i).DayOfWeek) && GetDayWeek(e.eT().DayOfWeek) > GetDayWeek(StartOfWeek.AddDays(i).DayOfWeek))
                            {
                                DateTime startofday = StartOfWeek.AddDays(i).Date;
                                DateTime endofday = startofday.AddHours(23).AddMinutes(59);
                                //writes event from 00:00 to 23:59 on day
                                userControlWeekDay.WriteEvent(startofday,endofday, e.eName());
                            }
                        }
                    }
                    if (e.r() == "Monthly")
                    {
                        if (e.sT().Date == e.eT().Date)
                        {
                            if (e.sT().Day == StartOfWeek.AddDays(i).Day)
                            {
                                userControlWeekDay.WriteEvent(e.sT(), e.eT(), e.eName());
                            }
                        }
                        //multi-day events 
                        else
                        {
                            //event begins on day but ends on another day
                            if (e.sT().Day == StartOfWeek.AddDays(i).Day)
                            {
                                //writes event from start to the end of the day
                                userControlWeekDay.WriteEvent(e.sT(), e.sT().Date.AddHours(23).AddMinutes(59), e.eName());
                            }
                            if (e.eT().Day == StartOfWeek.AddDays(i).Day)
                            {
                                //writes event from start of day 00:00 to event end
                                userControlWeekDay.WriteEvent(e.eT().Date, e.eT(), e.eName());
                            }
                            if (e.sT().Day < StartOfWeek.AddDays(i).Day && e.eT().Day > StartOfWeek.AddDays(i).Day)
                            {
                                DateTime startofday = StartOfWeek.AddDays(i).Date;
                                DateTime endofday = startofday.AddHours(23).AddMinutes(59);
                                //writes event from start of day to end of day 00:00 - 23:59
                                userControlWeekDay.WriteEvent(startofday, endofday, e.eName());
                            }
                        }
                    }
                    if (e.r() == "Annually")
                    {
                        if (e.sT().Date == e.eT().Date)
                        {
                            if (e.sT().Day == StartOfWeek.AddDays(i).Day && e.sT().Month == StartOfWeek.AddDays(i).Month)
                            {
                                userControlWeekDay.WriteEvent(e.sT(), e.eT(), e.eName());
                            }
                        }
                        else
                        {
                            if (e.sT().Day == StartOfWeek.AddDays(i).Day && e.sT().Month == StartOfWeek.AddDays(i).Month)
                            {
                                userControlWeekDay.WriteEvent(e.sT(), e.sT().Date.AddHours(23).AddMinutes(59), e.eName());
                            }
                            else if (e.eT().Day == StartOfWeek.AddDays(i).Day && e.eT().Month == StartOfWeek.AddDays(i).Month)
                            {
                                userControlWeekDay.WriteEvent(e.eT().Date, e.eT(), e.eName());
                            }
                            else if ((e.sT().Day < StartOfWeek.AddDays(i).Day && e.sT().Month == StartOfWeek.AddDays(i).Month) && (e.eT().Day > StartOfWeek.AddDays(i).Day && e.eT().Month == StartOfWeek.AddDays(i).Month))
                            {
                                DateTime startofday = StartOfWeek.AddDays(i).Date;
                                DateTime endofday = startofday.AddHours(23).AddMinutes(59);
                                userControlWeekDay.WriteEvent(startofday, endofday, e.eName());
                            }
                        }
                    }
                }

                //this gets the events of the friend's and displays them on the calendar
                foreach (string n in UserFriends)
                {
                    var FriendWeek = GetEvents(n);
                    var FriendRepeated = GetRepeatEvents(n);
                    var eventForDay2 = FriendWeek.Where(e => e.sT().Date == StartOfWeek.AddDays(i).Date || e.eT().Date == StartOfWeek.AddDays(i).Date || (e.sT().Date < StartOfWeek.AddDays(i).Date && e.eT() > StartOfWeek.AddDays(i).Date)).ToList();

                    foreach (Event e in eventForDay2)
                    {
                        if (e.r() == "None")
                        {
                            if (e.sT().Date == e.eT().Date)
                            {
                                userControlWeekDay.WriteFriendEvent(e.sT(), e.eT());
                            }
                            //multi-day events
                            else
                            {
                                //the event begins on this day but ends at another time
                                if (e.sT().Date == StartOfWeek.AddDays(i).Date)
                                {
                                    DateTime endofday = e.sT().Date.AddHours(23).AddMinutes(59);
                                    userControlWeekDay.WriteFriendEvent(e.sT(), endofday);
                                }
                                //the event ends on this day but began at another time
                                else if (e.eT().Date == StartOfWeek.AddDays(i).Date)
                                {
                                    DateTime startofday = e.eT().Date;
                                    userControlWeekDay.WriteFriendEvent(startofday, e.eT());
                                }
                                //the event began on a day before and ends on a day after
                                else
                                {
                                    DateTime startofday = StartOfWeek.AddDays(i).Date;
                                    DateTime endofday = startofday.AddHours(23).AddMinutes(59);
                                    userControlWeekDay.WriteFriendEvent(startofday, endofday);
                                }
                            }
                        }
                    }

                    //deals with the repeat events
                    foreach (Event e in FriendRepeated)
                    {
                        if (e.r() == "Daily")
                        {
                            userControlWeekDay.WriteFriendEvent(e.sT(), e.eT());
                        }
                        if (e.r() == "Weekly")
                        {
                            if (e.sT().Date == e.eT().Date)
                            {
                                if (e.sT().DayOfWeek == StartOfWeek.AddDays(i).DayOfWeek)
                                {
                                    userControlWeekDay.WriteFriendEvent(e.sT(), e.eT());
                                }
                            }
                            //multiday events
                            else
                            {
                                //event begins on day but ends on another
                                if (e.sT().DayOfWeek == StartOfWeek.AddDays(i).DayOfWeek)
                                {
                                    DateTime endofday = e.sT().Date.AddHours(23).AddMinutes(59);
                                    userControlWeekDay.WriteFriendEvent(e.sT(), endofday);
                                }
                                //event ends on this day but began on another day
                                else if (e.eT().DayOfWeek == StartOfWeek.AddDays(i).DayOfWeek)
                                {
                                    DateTime startofday = e.eT().Date;
                                    userControlWeekDay.WriteFriendEvent(startofday, e.eT());
                                }
                                //event begins and ends on different days
                                else if (GetDayWeek(e.sT().DayOfWeek) < GetDayWeek(StartOfWeek.AddDays(i).DayOfWeek) && GetDayWeek(e.eT().DayOfWeek) > GetDayWeek(StartOfWeek.AddDays(i).DayOfWeek))
                                {
                                    DateTime startofday = StartOfWeek.AddDays(i).Date;
                                    DateTime endofday = startofday.AddHours(23).AddMinutes(59);
                                    //writes event from 00:00 - 23:59 on day
                                    userControlWeekDay.WriteFriendEvent(startofday, endofday);
                                }
                            }
                        }
                        if (e.r() == "Monthly")
                        {
                            if (e.sT().Date == e.eT().Date)
                            {
                                if (e.sT().Day == StartOfWeek.AddDays(i).Day)
                                {
                                    userControlWeekDay.WriteFriendEvent(e.sT(), e.eT());
                                }
                            }
                            else
                            {
                                if (e.sT().Day == StartOfWeek.AddDays(i).Day)
                                {
                                    userControlWeekDay.WriteFriendEvent(e.sT(), e.sT().Date.AddHours(23).AddMinutes(59));
                                }
                                if (e.eT().Day == StartOfWeek.AddDays(i).Day)
                                {
                                    userControlWeekDay.WriteFriendEvent(e.eT().Date, e.eT());
                                }
                                if (e.sT().Day < StartOfWeek.AddDays(i).Day && e.eT().Day > StartOfWeek.AddDays(i).Day)
                                {
                                    DateTime startofday = StartOfWeek.AddDays(i).Date;
                                    DateTime endofday = startofday.AddHours(23).AddMinutes(59);
                                    userControlWeekDay.WriteFriendEvent(startofday, endofday);
                                }
                            }
                        }
                        if (e.r() == "Annually")
                        {
                            if (e.sT().Date == e.eT().Date)
                            {
                                if (e.sT().Day == StartOfWeek.AddDays(i).Day && e.sT().Month == StartOfWeek.AddDays(i).Month)
                                {
                                    userControlWeekDay.WriteFriendEvent(e.sT(), e.eT());
                                }
                            }
                            else
                            {
                                if (e.sT().Day == StartOfWeek.AddDays(i).Day && e.sT().Month == StartOfWeek.AddDays(i).Month)
                                {
                                    userControlWeekDay.WriteFriendEvent(e.sT(), e.sT().Date.AddHours(23).AddMinutes(59));
                                }
                                else if (e.eT().Day == StartOfWeek.AddDays(i).Day && e.eT().Month == StartOfWeek.AddDays(i).Month)
                                {
                                    userControlWeekDay.WriteFriendEvent(e.eT().Date, e.eT());
                                }
                                else if ((e.sT().Day < StartOfWeek.AddDays(i).Day && e.sT().Month == StartOfWeek.AddDays(i).Month) && (e.eT().Day > StartOfWeek.AddDays(i).Day && e.eT().Month == StartOfWeek.AddDays(i).Month))
                                {
                                    DateTime startofday = StartOfWeek.AddDays(i).Date;
                                    DateTime endofday = startofday.AddHours(23).AddMinutes(59);
                                    userControlWeekDay.WriteFriendEvent(startofday, endofday);
                                }
                            }
                        }
                    }
                }
                //adds day to the display
                FlowLayoutPanelWeek.Controls.Add(userControlWeekDay);
            }
        }

        private void Message(bool t, string m)
        {
            //toast message used to show messages to user
            var ToastMessage = new ToastMessage(t, m);
            ToastMessage.Show();
        }

        public void SuggestReschedule()
        {
            var userEvents = GetEvents(username); 
            var friendEvents = GetFriendsEvents();
            var friendRepeated = GetFriendsRepeatEvents();
            
            List<(DateTime start, DateTime end)> friendsBusySlots = FindFriendsBusyTime(friendEvents, friendRepeated);
            //HashSet used so that the same suggestions are not added multiple times
            HashSet<string> SuggestedEvents = new HashSet<string>();

            List<string> suggestions = new List<string>();
            for (int i = 0; i < 7; i++)
            {
                //gets users events for the day and ensures they are not repeat events
                var userEventsToday = userEvents.Where(e => e.sT().Date == StartOfWeek.AddDays(i).Date && e.sT().Day == e.eT().Day && e.r() == "None" && e.aD() == false).ToList();
                //gets the busy slots on that day
                var BusySlotsToday = friendsBusySlots.Where(slot => slot.start.Date == StartOfWeek.AddDays(i).Date).ToList();
                foreach (var u in userEventsToday)
                {
                    //checks if event is not priority
                    if (u.p() == false)
                    {
                        //presumes the events is during a free period
                        bool FreePeriod = true;
                        //checks if event is happening during busy time
                        foreach (var slot in BusySlotsToday)
                        {
                            if (u.sT() <= slot.end && u.eT() >= slot.start)
                            {
                                FreePeriod = false;
                                break;
                            }
                        }
                        //if the event is during a free period and has not already been recommended to be moved
                        if (FreePeriod && SuggestedEvents.Contains(u.eName()) == false)
                        {
                            //adds suggestion to HashSet
                            suggestions.Add($"Consider moving '{u.eName()}' from {u.sT().ToString("HH:mm")} to another time.");
                            SuggestedEvents.Add(u.eName());
                        }
                    }
                }
            }
            //show suggestion in SuGestionBox on the mainpage
            if (suggestions.Count > 0)
            {
                Main.UpdateSuggestions(string.Join("\n", suggestions));
            }
            else
            {
                //displays message if there are no suggestions
                Main.UpdateSuggestions("Free time with friends maximised.");  
            }
        }
        
        private List<Event> GetFriendsEvents()
        {
            var UserFriends = GetUserFriends(username);
            List<Event> friendevents = new List<Event>();
            
            foreach (string n in UserFriends)
            {
                var userfriendsevents = GetEvents(n);
                if(userfriendsevents != null)
                {
                    //adds all friends' events into the same list so it's easier to manipulate
                    friendevents.AddRange(userfriendsevents);
                }
            }
            //returns friends' events
            return friendevents;
        }

        //gets repeated user events
        private List<Event> GetFriendsRepeatEvents()
        {
            var UserFriends = GetUserFriends(username);
            List<Event> FriendRepeatEvents = new List<Event>();

            foreach (string n in UserFriends)
            {
                var rEvents = GetRepeatEvents(n);
                if (rEvents != null)
                {
                    //adds friends' repeat events to a list
                    FriendRepeatEvents.AddRange(rEvents);
                }
            }
            //returns friends' repeat events
            return FriendRepeatEvents;
        }

        private List<(DateTime start, DateTime end)> FindFriendsBusyTime(List<Event> FriendsEvents, List<Event> FriendRepeat)
        {
            List<(DateTime start, DateTime end)> BusySlots = new List<(DateTime, DateTime)>();

            for (int i = 0; i < 7; i++)
            {
                DateTime dayStart = StartOfWeek.AddDays(i).Date;
                DateTime dayEnd = StartOfWeek.AddDays(i).Date.AddHours(23).AddMinutes(59);
                var DayEvents = FriendsEvents.Where(e => e.sT() >= dayStart && e.eT() <= dayEnd).ToList();
                DateTime current = dayStart;
                
                //checks to see which repeated user events fall on the current week
                foreach (var e in FriendRepeat)
                {
                    if (e.sT().Date == e.eT().Date)
                    {
                        if (IsEventRepeatingOnDate(e, dayStart))
                        {
                            DateTime eventStart = new DateTime(dayStart.Year, dayStart.Month, dayStart.Day, e.sT().Hour, e.sT().Minute, 0);
                            DateTime eventEnd = eventStart.Add(e.eT() - e.sT());
                            //creates a new event based on the old event but it starts on the current week
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
                            else if (GetDayWeek(e.sT().DayOfWeek) < GetDayWeek(dayStart.DayOfWeek) && GetDayWeek(e.eT().DayOfWeek) > GetDayWeek(dayStart.DayOfWeek))
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
                
                //making sure events in DayEvents ordered by start time so that algorithm can go through events chronologically
                DayEvents = DayEvents.OrderBy(e => e.sT()).ToList();
                
                //set pointers for the start and end of busy times
                DateTime? busyStart = null;
                DateTime? busyEnd = null;

                foreach (var e in DayEvents)
                {
                    if (busyStart == null)
                    {
                        // First event of the day sets the busy period
                        busyStart = e.sT();
                        busyEnd = e.eT();
                    }
                    else
                    {
                        if (e.sT() <= busyEnd)
                        {
                            // Overlapping event, extend the busy period
                            if (e.eT() > busyEnd)
                            {
                                //sets busyEnd to the end of the overlapping event
                                busyEnd = e.eT();
                            }
                        }
                        else
                        {
                            // Non-overlapping event, save the current busy slot and start a new one
                            BusySlots.Add((busyStart.Value, busyEnd.Value));
                            busyStart = e.sT();
                            busyEnd = e.eT();
                        }
                    }
                }

                if (busyStart != null)
                {
                    BusySlots.Add((busyStart.Value, busyEnd.Value));
                }
            }
            return BusySlots;
        }

        //returns true if an event begins and ends on same date
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
    }
}
