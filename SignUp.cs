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
    public partial class SignUp : Form
    {
        private string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";
        public SignUp()
        {
            InitializeComponent();
            this.FormClosing += SignUp_FormClosing;
        }

        private void SignUp_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            //returns user to the login page
            new LoginSignin().Show();
            this.Hide();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string username = NewUsername.Text;
            string password = NewPassword.Text;
            string confirm = ConfirmPassword.Text;
            bool unique = true;
            
            string query = "SELECT Username FROM [User]";
            //checks the username is unique and there is not someone with the same username
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                try
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string u = reader.GetString(0); //username read from database
                                if (u == username)
                                {
                                    //if the entered username is already in the database, a user with that mae already exists
                                    unique = false;
                                    break;
                                }
                            }
                            reader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //shows message of error being present
                    Message(false, ex.ToString());
                }
            }

            //if username is not unique
            if (!unique)
            {
                Message(false, "Username is already taken. Choose a new one");
            }

            // if password is not repeated in confirm password
            if (password != confirm)
            {
                //toast message
                Message(false, "Passwords do not match");
            }

            //if password has fewer than 10 characters
            if (password.Length < 10)
            {
                Message(false, "Password is too short");
            }

            //analysing the password characters
            if (!SpecialCharCheck(password) || !NumCheck(password) || !UppercaseCheck(password))
            {
                Message(false, "Password does not meet requirements");
            }

            //creates event if password meets requirements above and a user with the same username does not already exist
            if (SpecialCharCheck(password) && NumCheck(password) && UppercaseCheck(password) && password == confirm && password.Length >= 10 && unique == true)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    try
                    {
                        string InsertQuery = @"
INSERT INTO [User] (Username, Password, ProfilePicture) 
VALUES (@Username, @Password, NULL)";
                        //adds the user's username and password to table User
                        using (var InsertCommand = new SqlCommand(InsertQuery, connection))
                        {
                            InsertCommand.Parameters.AddWithValue("@Username", username);
                            InsertCommand.Parameters.AddWithValue("@Password", password);
                            InsertCommand.ExecuteNonQuery();
                            Message(true, "Account saved in database");
                        }
                        //create a calendar for the user and go onto the homepage
                        this.Hide();
                        var MainPage = new MainPage(username);
                        MainPage.Show();
                    }
                    catch (Exception ex)
                    {
                        Message(false, ex.ToString());
                    }
                }
            }
        }

        private void Message(bool b, string m)
        {
            var toast = new ToastMessage(b, m);
            toast.Show();
        }

        private bool SpecialCharCheck(string p)
        {
            string specialChars = "@!#$%£*&^(){}[]=+-_:;.,<>/?~";
            foreach (char c in p)
            {
                if (specialChars.Contains(c))
                {
                    //returns true if the password contains any of the special characters above
                    return true;
                }
            }
            return false;
        }

        private bool NumCheck(string p)
        {
            foreach (char c in p)
            {
                if (char.IsDigit(c))
                {
                    return true;
                }
            }
            return false;
        }

        private bool UppercaseCheck(string p)
        {
            foreach (char c in p)
            {
                if (char.IsUpper(c))
                {
                    return true;
                }
            }
            return false;
        }

        private void Show_CheckedChanged(object sender, EventArgs e)
        {
            //if show is chceked the passwords are visible
            if (Show.Checked == true)
            {
                NewPassword.UseSystemPasswordChar = false;
                ConfirmPassword.UseSystemPasswordChar = false;
            }
            else
            {
                NewPassword.UseSystemPasswordChar = true;
                ConfirmPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
