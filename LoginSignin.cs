using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler_NEA
{
    public partial class LoginSignin : Form
    {

        public LoginSignin()
        {
            InitializeComponent();
            this.FormClosing += LoginSignin_FormClosing;
        }

        private void LoginSignin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //submit button
        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameInput.Text;
            string password = PasswordInput.Text;
            string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    string query = "SELECT Username, Password FROM [User]";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //reads all the usernames and passwords in the database
                            while (reader.Read())
                            {
                                string u = reader.GetString(0); //username
                                string p = reader.GetString(1); // corresponding password

                                //if the username and the corresponding password are the same the user exists
                                if (u == username && p == password)
                                {
                                    //user is sent to their homepage 
                                    var MainPage = new MainPage(username);
                                    MainPage.Show();
                                    this.Hide();
                                    break;
                                }
                                else
                                {
                                    //the username and password do not match
                                    Message(false, "Invalid password or username");
                                }
                            }
                            reader.Close();
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Message(false, ex.ToString());
                }
            }
        }

        private void SignUp_Click(object sender, EventArgs e)
        {

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void BackgroundSegment_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            //takes user onto the sign up page
            new SignUp().Show();
            this.Hide();
        }
        private void Message(bool b, string m)
        {
            //creates toast message to output messages
            var toast = new ToastMessage(b, m);
            toast.Show();
        }

        private void Show_CheckedChanged(object sender, EventArgs e)
        {
            //if show is checked the password is shown
            if (Show.Checked == true)
            {
                PasswordInput.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordInput.UseSystemPasswordChar = true;
            }
        }
    }
}
