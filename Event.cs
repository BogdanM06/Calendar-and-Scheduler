using Azure.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Scheduler_NEA
{

    public class Event
    {
        private string EventName;
        private DateTime StartTime;
        private DateTime EndTime;
        private bool Priority;
        private bool AllDay;
        private string Repeat;
        protected string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";

        public Event(string name, DateTime start, DateTime end, bool prior, bool aday, string r)
        {
            EventName = name;
            StartTime = start;
            EndTime = end;
            Priority = prior;
            AllDay = aday;
            Repeat = r;
        }

        //returns the event name
        public string eName()
        {
            return EventName;
        }

        //returns the start date and time for an event
        public DateTime sT()
        { 
            return StartTime; 
        }

        //returns the end date and time for an event
        public DateTime eT()
        {
            return EndTime;
        }

        //returns true if the event is priority
        public bool p()
        {
            return Priority;
        }

        //returns true if the event is all-day
        public bool aD()
        {
            return AllDay;
        }

        //returns the repeat frequency: "None", "Weekly", "Monthly" or "Annually"
        public string r()
        {
            return Repeat;
        }


        public void AddToDatabase(string u)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    //adds the username and eventname to table UserEvents
                    string InsertQuery = @"
INSERT INTO [UserEvents] (Username, EventName) 
VALUES (@Username, @EventName)";
                    using (var InsertCommand = new SqlCommand(InsertQuery, connection))
                    {
                        //replaces placeholder strings with values that I want to use
                        InsertCommand.Parameters.AddWithValue("@Username", u);
                        InsertCommand.Parameters.AddWithValue("@EventName", EventName);
                        //executes command
                        InsertCommand.ExecuteNonQuery();
                        Message(true, "Event saved");
                    }
                    //adds the username and eventname to table Events
                    string InsertQuery2 = @"
INSERT INTO [Events] (EventName, EventStart, EventEnd, Priority, AllDay, Repeat) 
VALUES (@EventName, @EventStart, @EventEnd, @Priority, @AllDay, @Repeat)";
                    using (var InsertCommand2 = new SqlCommand(InsertQuery2, connection))
                    {
                        //replaces placeholder strings with values that I want to use
                        InsertCommand2.Parameters.AddWithValue("@EventName", EventName);
                        InsertCommand2.Parameters.AddWithValue("@EventStart", StartTime);
                        InsertCommand2.Parameters.AddWithValue("@EventEnd", EndTime);
                        InsertCommand2.Parameters.AddWithValue("@Priority", Priority);
                        InsertCommand2.Parameters.AddWithValue("@AllDay", AllDay);
                        InsertCommand2.Parameters.AddWithValue("@Repeat", Repeat);
                        //executes command
                        InsertCommand2.ExecuteNonQuery();
                        Message(true, "Event saved");
                    }
                }
                catch (Exception ex)
                {
                    Message(false, ex.Message);
                }
            }
        }

        protected void Message(bool b, string m)
        {
            //creates a toast message to output messages
            var ToastMessage = new ToastMessage(b, m);
            ToastMessage.Show();
        }
    }
}