using Microsoft.Data.SqlClient;
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
    public partial class FriendAdd : Form
    {
        private string username;
        private string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";
        private WeekCalendar wcal;

        public FriendAdd(string u, WeekCalendar w)
        {
            InitializeComponent();
            username = u;
            wcal = w;
        }

        private void SendReqButton_Click(object sender, EventArgs e)
        {
            string fUsername = FriendUsername.Text;
            //establishes the user they are friending is real
            if (fUsername != username)
            {
                if (UNameExists(fUsername))
                {
                    //checking if the users are already friends
                    if (CheckFriends(fUsername) == false)
                    {
                        //if not friends they are added to database
                        AddFriends(fUsername);
                        Message(true, "Friend added");
                        //updates weekly calendar with the new friend's events
                        wcal.DisplayDays();
                        //updates the reschedule suggestions
                        wcal.SuggestReschedule();
                    }
                    else
                    {
                        Message(false, "You are already friends");
                    }
                }
                else
                {
                    Message(false, "User does not exist");
                }
            }
            else
            {
                Message(false, "You cannot friend yourself");
            }
        }

        //checks user exists
        private bool UNameExists(string u)
        {
            bool exists = false;
            string query = "SELECT Username FROM [User]";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string uName = reader.GetString(0); //username from database
                                if (uName == u)
                                {
                                    //if there is a user by that name in the database the user exists
                                    exists = true;
                                    break;
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
                return exists;
            }

        }

        //adds new friends to database
        private void AddFriends(string u)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    string InsertQuery = @"
INSERT INTO [UserFriends] (Username, FriendUsername) 
VALUES (@Username, @FriendUsername)";
                    using (var InsertCommand = new SqlCommand(InsertQuery, connection))
                    {
                        InsertCommand.Parameters.AddWithValue("@Username", username);
                        InsertCommand.Parameters.AddWithValue("@FriendUsername", u);
                        //adds the user in the username column and friend username in the friend column
                        InsertCommand.ExecuteNonQuery();
                    }
                    using (var InsertCommand = new SqlCommand(InsertQuery, connection))
                    {
                        InsertCommand.Parameters.AddWithValue("@Username", u);
                        InsertCommand.Parameters.AddWithValue("@FriendUsername", username);
                        //adds friend username in username column and user in friend column
                        InsertCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    //shows message of error being present
                    Message(false, ex.Message);
                    
                }
            }
        }

        //checks if users are already friends
        private bool CheckFriends(string u)
        {
            bool arefriends = false;
            string query = @"
SELECT COUNT(*)
FROM [UserFriends]
WHERE Username = @Username
AND FriendUsername = @FriendUsername";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@FriendUsername", u);
                        // if count is greater than 0 users are friends so should not be readded to database
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            arefriends = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    Message(false, e.Message);
                }
                return arefriends;
            }
        }

        private void Message(bool t, string msg)
        {
            //creates toast message to output messages
            var toast = new ToastMessage(t, msg);
            toast.Show();
        }
    }
}
