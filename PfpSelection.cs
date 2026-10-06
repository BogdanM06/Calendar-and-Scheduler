using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler_NEA
{
    public partial class PfpSelection : Form
    {
        private string username;
        private string? choice;
        private MainPage main;
        public PfpSelection(MainPage m, string u)
        {
            InitializeComponent();
            username = u;
            main = m;
        }

        private void PfpSelection_Load(object sender, EventArgs e)
        {
            pictureBox1.Region = main.RoundImage(pictureBox1);
            pictureBox2.Region = main.RoundImage(pictureBox2);
            pictureBox3.Region = main.RoundImage(pictureBox3);
            pictureBox4.Region = main.RoundImage(pictureBox4);
            pictureBox5.Region = main.RoundImage(pictureBox5);
            pictureBox6.Region = main.RoundImage(pictureBox6);
        }

        private void Message(bool t, string message)
        {
            var toast = new ToastMessage(t, message);
            toast.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //cat
            choice = "a_gracefully_aging_tabby_cat__its_fur_a_mix_of_sil_by_b0dean_dima0iy";
            SavePfp();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //blossom

            choice = "a_gracefully_blooming_cherry_blossom__delicate_pet_by_b0dean_dijtlr9";
            SavePfp();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            //sunflower
            choice = "a_sunflower__by_b0dean_dim9zbo";
            SavePfp();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //astronaut
            choice = "alchemyrefiner_alchemymagic_0_c8310571-54c2-4394-b763-b4352fa09e24_0";
            SavePfp();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //spaceship
            choice = "a_single_rocket_in_the_middle_of_the_standing_stil_by_b0dean_dijtih4";
            SavePfp();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            //dinosaur
            choice = "one_dinosaur_in_the_centre_of_the_image_the_dinosa_by_b0dean_dim9yez";
            SavePfp();
            this.Close();
        }

        public void SavePfp()
        {
            string ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\bogda\\OneDrive - Hills Road Sixth Form College\\Documents\\MySQLServer.mdf\";Integrated Security=True;Connect Timeout=30;Encrypt=True";
            string pfp = choice;
            string query = @"
UPDATE [User]
SET ProfilePicture = @profilepicture
WHERE Username = @Username";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@profilepicture", pfp);
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.ExecuteNonQuery();
                        Message(true, "Profile picture saved");
                        UpdatePfp();
                    }
                }
                catch (Exception ex)
                {
                    Message(false, ex.Message);
                    
                }
            }
        }

        private void UpdatePfp()
        {
            main.GetPfp();
        }
    }
}
