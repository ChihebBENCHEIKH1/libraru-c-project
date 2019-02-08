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
    public partial class gestionemp : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=tp_csharp_db; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        DataTable dataTable4 = new DataTable();

        int currRowIndex;
        String table = "emprunteurs";
        int ouvID;

        public gestionemp()
        {
            InitializeComponent();
            textBox5.Enabled = false;
            textBox3.Enabled = false;
            button1.Enabled = false;
            button6.Enabled = false;
            button9.Enabled = false;
            button12.Enabled = true;
            button13.Enabled = true;
            button11.Enabled = true;
            label11.Visible = false;


        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            dataGridView4.Visible = false;
            dataGridView3.Visible = false;

            dataTable.Clear();
            dataGridView1.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select id,date_emprunt, num_ouvrage,client,cin,delai,type_ouvrage from emprunteurs";
            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);

            int i;
            String[] myArray = new String[8];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }
                dataGridView1.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[6], myArray[3], myArray[4], myArray[5]);
            }
            maconnexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (textBox5.Text == "" || textBox1.Text == "" || textBox2.Text == "")
            {
                DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd-MM-yyyy";


                emprunt Emp = new emprunt(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), ouvID.ToString(), textBox1.Text, textBox2.Text, dateTimePicker1.Text, table);

                //dataGridView1.Rows.Add("", "", ouvID, table, textBox1.Text,textBox2.Text, dateTimePicker1.Text);
                dataGridView1.Rows.Add("", "" , ouvID, table, Emp.client, Emp.cin, Emp.delai);


                textBox1.Clear(); textBox2.Clear(); textBox5.Clear(); textBox3.Clear();

                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();

                cmd.CommandText = "INSERT INTO emprunteurs (client, cin,delai ,num_ouvrage,date_emprunt,type_ouvrage)" +
                    "VALUES(@client, @cin,@delai ,@num_ouvrage,@date_emprunt,@type_ouvrage)";
                cmd.Parameters.AddWithValue("@client", Emp.client);
                cmd.Parameters.AddWithValue("@cin", Emp.cin);
                cmd.Parameters.AddWithValue("@delai", Emp.delai);
                cmd.Parameters.AddWithValue("@num_ouvrage", Emp.getNumero());
                cmd.Parameters.AddWithValue("@date_emprunt", Emp.getDate());
                cmd.Parameters.AddWithValue("@type_ouvrage", table);
                
                cmd.ExecuteNonQuery();
                maconnexion.Close();

                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                dataGridView4.Visible = false;
                dataGridView3.Visible = false;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult dialogUpdate = MessageBox.Show("voulez-vous vraiment modifier les informations sur cette Emprunt", "Modifier une Emprunt", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (textBox5.Text == "" || textBox1.Text == "" || textBox2.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();

                    MySqlCommand cmd = maconnexion.CreateCommand();

                    cmd.CommandText = "UPDATE emprunteurs SET client= @client, cin=@cin , delai=@delai" +
                        " WHERE id=" + currRowIndex;
                    cmd.Parameters.AddWithValue("@client", textBox1.Text);
                    cmd.Parameters.AddWithValue("@cin", textBox2.Text);
                    cmd.Parameters.AddWithValue("@delai", dateTimePicker1.Text);

                    cmd.ExecuteNonQuery();
                    maconnexion.Close();
                    textBox1.Clear(); textBox2.Clear(); textBox5.Clear(); textBox3.Clear();
                    button6.Enabled = false;
                    button9.Enabled = false;

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            Console.WriteLine(rowIndex);
            DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment supprimer cette Emprunt", "Supprimer une Emprunt", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(rowIndex);
                button6.Enabled = false;
                button9.Enabled = false;
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "DELETE FROM emprunteurs WHERE id=" + currRowIndex;

                cmd.ExecuteNonQuery();
                maconnexion.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox5.Clear();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Emprunts DataGridView 
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[4].Value.ToString();
            textBox2.Text = row.Cells[5].Value.ToString();
            textBox3.Text = row.Cells[1].Value.ToString();
            textBox5.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[6].Value.ToString();
            button6.Enabled = true;
            button9.Enabled = true;
            button1.Enabled = false;
            label11.Visible= false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dashboard a = new dashboard();
            this.Hide();
            a.Show();
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

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Livre DataGridView
            table = "livres";
            label11.Visible = true;
            label11.Text = "-  Livre";
            DataGridViewRow row3 = this.dataGridView3.Rows[e.RowIndex];

            ouvID = Convert.ToInt32(row3.Cells[0].Value);
            textBox5.Text = row3.Cells[4].Value.ToString();

            dateTimePicker1.Text = row3.Cells[1].Value.ToString();


            button1.Enabled = true;
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Period DataGridView
            table = "periodiques";
            label11.Visible = true;
            label11.Text = "-  Periodiques";
            DataGridViewRow row4 = this.dataGridView4.Rows[e.RowIndex];

            ouvID = Convert.ToInt32(row4.Cells[0].Value);
            textBox5.Text = row4.Cells[3].Value.ToString();

            button1.Enabled = true;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            dataGridView4.Visible = false;
            dataGridView3.Visible = false;

            dataTable2.Clear();
            dataGridView2.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select id,date_emprunt, num_ouvrage,auteur,titre from cds";
            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable2);

            int i;
            String[] myArray = new String[5];
            foreach (DataRow dataRow in dataTable2.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }
                dataGridView2.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray[4]);
            }
            maconnexion.Close();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView4.Visible = true;
            dataGridView3.Visible = false;

            dataTable4.Clear();
            dataGridView4.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select id,date_emprunt, num_ouvrage, periodicite, numero, nom from periodiques";
            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable4);

            int i;
            String[] myArray = new String[6];
            foreach (DataRow dataRow in dataTable4.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }
                dataGridView4.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray[4], myArray[5]);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView4.Visible = false;
            dataGridView3.Visible = true;
            dataTable3.Clear();
            dataGridView3.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select id,date_emprunt, num_ouvrage,auteur, titre, editeur from livres";
            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable3);

            int i;
            String[] myArray = new String[6];
            foreach (DataRow dataRow in dataTable3.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }
                dataGridView3.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray[4], myArray[5]);
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            gestionemp a = new gestionemp();
            this.Hide();
            a.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer l'application ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //CDs DataGridView
            table = "cds";
            label11.Visible = true;
            label11.Text = "-  CD";
            DataGridViewRow row2 = this.dataGridView2.Rows[e.RowIndex];

            ouvID = Convert.ToInt32(row2.Cells[0].Value);
            textBox5.Text = row2.Cells[4].Value.ToString();

            button1.Enabled = true;

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
