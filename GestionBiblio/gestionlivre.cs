using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace GestionBiblio
{
    public partial class gestionlivre : Form
    {

        string parametres = "SERVER=127.0.0.1; DATABASE=test; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;
        DataTable dataTable = new DataTable();
        int currRowIndex;


        public gestionlivre()
        {
            InitializeComponent();

            button2.Enabled = false;
            button9.Enabled = false;
        }

        private void gestionlivre_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[3].Value.ToString();
            textBox2.Text = row.Cells[4].Value.ToString();
            textBox3.Text = row.Cells[5].Value.ToString();
            textBox5.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = Convert.ToString(row.Cells[1].Value);

            button2.Enabled = true;
            button9.Enabled = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox5.Clear();

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" )
            {
                DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                livre L = new livre(dateTimePicker1.Text, textBox5.Text, textBox1.Text, textBox2.Text, textBox3.Text);

                dataGridView2.Rows.Add("",dateTimePicker1.Text, textBox5.Text, textBox1.Text, textBox2.Text, textBox3.Text);
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox5.Clear();


                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "INSERT INTO livres (auteur, titre, editeur,num_ouvrage,date_emprunt)" +
                    "VALUES(@auteur, @titre,@editeur, @num_ouvrage,@date_emprunt)";
                cmd.Parameters.AddWithValue("@auteur", L.auteur);
                cmd.Parameters.AddWithValue("@titre", L.titre);
                cmd.Parameters.AddWithValue("@editeur", L.editeur);
                cmd.Parameters.AddWithValue("@num_ouvrage", L.getNumero());
                cmd.Parameters.AddWithValue("@date_emprunt", L.getDate());
                cmd.ExecuteNonQuery();
                maconnexion.Close();
            }


        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            gestionlivre a = new gestionlivre();
            this.Hide();
            a.Show();
        }

       
        private void label6_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer déconnecter ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            int rowIndex = dataGridView2.CurrentCell.RowIndex;

            DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment supprimer ce Livre", "Supprimer un Livre", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {
                dataGridView2.Rows.RemoveAt(rowIndex);
                button2.Enabled = false;
                button9.Enabled = false;
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "DELETE FROM livres WHERE id=" + currRowIndex;
                cmd.ExecuteNonQuery();
                maconnexion.Close();

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dashboard a = new dashboard();
            this.Hide();
            a.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataTable.Clear();
            dataGridView2.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select id,date_emprunt, num_ouvrage,auteur, titre, editeur from livres";
            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);

            int i;
            String[] myArray = new String[6];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }
                dataGridView2.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray[4], myArray[5]);                
            }

            //dataGridView2.DataSource = dataTable;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("voulez-vous vraiment modifier les informations sur ce livre ", "Modifier un livre", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox5.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    int rowIndex = dataGridView2.CurrentCell.RowIndex;

                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();

                    MySqlCommand cmd = maconnexion.CreateCommand();

                    cmd.CommandText = "UPDATE livres SET auteur= @auteur, titre=@titre, editeur=@editeur,num_ouvrage=@num_ouvrage,date_emprunt=@date_emprunt" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@editeur", textBox3.Text);
                    cmd.Parameters.AddWithValue("@titre", textBox2.Text);
                    cmd.Parameters.AddWithValue("@auteur", textBox1.Text);
                    cmd.Parameters.AddWithValue("@num_ouvrage", textBox5.Text);
                    cmd.Parameters.AddWithValue("@date_emprunt", dateTimePicker1.Text);


                    cmd.ExecuteNonQuery();
                    maconnexion.Close();
                    textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox5.Clear();
                    button2.Enabled = false;
                    button9.Enabled = false;

                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            gestionemp a = new gestionemp();
            this.Hide();
            a.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            gestionemp gestionemp=new gestionemp();
            this.Hide();
            gestionemp.Show();
        }
    }
}
