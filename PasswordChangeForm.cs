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
    public partial class PasswordChangeForm : Form
    {
        private string username;
        private string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";
        public PasswordChangeForm(string u)
        {
            InitializeComponent();
            username = u;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            string password = PasswordTextBox.Text;
            string confirm = CPasswordTextBox.Text;

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

            if (SpecialCharCheck(password) && NumCheck(password) && UppercaseCheck(password) && password == confirm && password.Length >= 10)
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        //adds the user's username and password to table User
                        string query = @"
UPDATE [User]
SET Password = @Password
WHERE Username = @Username";
                        using (var cmd = new SqlCommand(query, connection))
                        {

                            cmd.Parameters.AddWithValue("@Password", password);
                            cmd.Parameters.AddWithValue("@Username", username);
                            cmd.ExecuteNonQuery();
                            Message(true, "Pasword updated");
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Message(false, ex.ToString());
                    }
                }
            }
        }

        private bool SpecialCharCheck(string p)
        {
            string specialChars = "@!#$%£*&^(){}[]=+-_:;.,<>/?~";
            foreach (char c in p)
            {
                if (specialChars.Contains(c))
                {
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

        private void Message(bool t, string msg)
        {
            var toast = new ToastMessage(t, msg);
            toast.Show();
        }

        private void Show_CheckedChanged(object sender, EventArgs e)
        {
            //if show is chceked the passwords are visible
            if (Show.Checked == true)
            {
                PasswordTextBox.UseSystemPasswordChar = false;
                CPasswordTextBox.UseSystemPasswordChar = false;
            }
            else
            {
                PasswordTextBox.UseSystemPasswordChar = true;
                CPasswordTextBox.UseSystemPasswordChar = true;
            }
        }
    }
}
