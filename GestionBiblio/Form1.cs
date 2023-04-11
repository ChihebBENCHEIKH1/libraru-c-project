using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionBiblio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "SERVER=127.0.0.1; DATABASE=test; UID=root; PASSWORD=";
            string query = "SELECT idAdherent FROM adherents WHERE nom = @nom AND mdp = @mdp";
            int idAdherent = -1; // Initialize the ID to an invalid value

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nom", textBox1.Text);
                    command.Parameters.AddWithValue("@mdp", textBox2.Text);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idAdherent = reader.GetInt32(0); // Get the ID value from the first column
                        }
                    }
                }
            }

            if ( textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                dashboard a = new dashboard();
                this.Hide();
                a.Show();
            }
            else if(idAdherent != -1)
            {
                service service=new service();
                service.IdAdherent=idAdherent;
                EspaceUser user = new EspaceUser();
                this.Hide();
                user.Show();
            }
            
            else
            {
                MessageBox.Show("Veuillez vérifier vos informations de login ! ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer l'application ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Application.Exit();
            }
            else if (dialogClose == DialogResult.Cancel)
            {
                //do something else
            }

            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
