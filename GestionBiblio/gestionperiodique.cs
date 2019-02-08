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
    public partial class gestionperiodique : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=tp_csharp_db; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;
        DataTable dataTable = new DataTable();
        int currRowIndex;

        public gestionperiodique()
        {
            InitializeComponent();
            button1.Enabled = false;
            button9.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gestionperiodique_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox5.Clear();

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView2.Rows[e.RowIndex];

            currRowIndex = Convert.ToInt32( row.Cells[0].Value);
            textBox1.Text = row.Cells[4].Value.ToString();
            textBox2.Text = row.Cells[3].Value.ToString();
            textBox3.Text = row.Cells[5].Value.ToString();
            textBox5.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[1].Value.ToString();
            button1.Enabled = true;
            button9.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {


            if (textBox5.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                periodique P = new periodique(dateTimePicker1.Text, textBox5.Text, textBox1.Text, textBox2.Text, textBox3.Text);

                dataGridView2.Rows.Add("",dateTimePicker1.Text, textBox5.Text, textBox2.Text, textBox1.Text, textBox3.Text);
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox5.Clear();


                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "INSERT INTO periodiques (nom, periodicite, numero,num_ouvrage,date_emprunt)" +
                    "VALUES(@nom, @periodicite ,@numero , @num_ouvrage,@date_emprunt)";
                cmd.Parameters.AddWithValue("@nom", P.nom);
                cmd.Parameters.AddWithValue("@periodicite", P.periodicite);
                cmd.Parameters.AddWithValue("@numero", P.numéro);
                cmd.Parameters.AddWithValue("@num_ouvrage", P.getNumero());
                cmd.Parameters.AddWithValue("@date_emprunt", P.getDate());
                cmd.ExecuteNonQuery();
                maconnexion.Close();
            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            gestionperiodique a = new gestionperiodique();
            this.Hide();
            a.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            gestionlivre a = new gestionlivre();
            this.Hide();
            a.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gestioncd a = new gestioncd();
            this.Hide();
            a.Show();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer l'application ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Application.Exit();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView2.CurrentCell.RowIndex;

            DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment supprimer cette Périodique", "Supprimer une Périodique", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {
                dataGridView2.Rows.RemoveAt(rowIndex);
                button1.Enabled = false;
                button9.Enabled = false;
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "DELETE FROM periodiques WHERE id=" + currRowIndex;
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
            string request = "select id,date_emprunt, num_ouvrage, periodicite, numero, nom from periodiques";
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
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("voulez-vous vraiment modifier les informations sur cette periodique ", "Modifier une Periodique", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox5.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();

                    MySqlCommand cmd = maconnexion.CreateCommand();
                    cmd.CommandText = "UPDATE periodiques SET nom= @nom, periodicite=@periodicite, numero=@numero,num_ouvrage=@num_ouvrage,date_emprunt=@date_emprunt" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@nom", textBox3.Text);
                    cmd.Parameters.AddWithValue("@periodicite", textBox2.Text);
                    cmd.Parameters.AddWithValue("@numero", textBox1.Text);
                    cmd.Parameters.AddWithValue("@num_ouvrage", textBox5.Text);
                    cmd.Parameters.AddWithValue("@date_emprunt", dateTimePicker1.Text);

                    cmd.ExecuteNonQuery();
                    maconnexion.Close();
                    textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox5.Clear();
                    button1.Enabled = false;
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
    }
}
