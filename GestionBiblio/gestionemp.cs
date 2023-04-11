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
        string parametres = "SERVER=127.0.0.1; DATABASE=test; UID=root; PASSWORD=";
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
            textBox5.Enabled = true;
            button1.Enabled = true;
            button6.Enabled = true;
            button9.Enabled = true;
            label11.Visible = false;


        }

        private void button8_Click(object sender, EventArgs e)
        {
           // dataGridView1.Visible = true;
           // dataGridView2.Visible = false;
            dataGridView4.Visible = true;
            //dataGridView3.Visible = false;

            dataTable.Clear();
            dataGridView4.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select idAdherent,nom,cin,date_naissance from adherents";
            
            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);

            string request1 = "SELECT count(idAdhrent) FROM `adherent_livre` WHERE idAdhrent=";
            int i;
            String[] myArray = new String[4];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                    

                }
                request1 = request1 + myArray[0];
                MySqlCommand cmdOfCount = new MySqlCommand(request1, maconnexion);
                int countDesLivres = Convert.ToInt32(cmdOfCount.ExecuteScalar());
                dataGridView4.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], countDesLivres);
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
                //dataGridView4.Rows.Add("", "" , ouvID, table, Emp.client, Emp.cin, Emp.delai);


               

                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();

                cmd.CommandText = "INSERT INTO adherents (idAdherent,nom,cin,date_naissance,mdp) " +
                    "VALUES(NULL,@nom, @cin,@date ,@mdp)";
                cmd.Parameters.AddWithValue("@nom", textBox1.Text);
                cmd.Parameters.AddWithValue("@cin", textBox2.Text);
                cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@mdp", textBox5.Text);
                
                cmd.ExecuteNonQuery();
                textBox1.Clear(); textBox2.Clear(); textBox5.Clear();
                maconnexion.Close();

                dataGridView4.Visible = true;
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

                    cmd.CommandText = "UPDATE adherents SET nom=@nom" +
                        ",cin=@cin,date_naissance=@date,mdp=@mdp WHERE idAdherent=" +
                         + currRowIndex;
                    cmd.Parameters.AddWithValue("@nom", textBox1.Text);
                    cmd.Parameters.AddWithValue("@cin", textBox2.Text);
                    cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@mdp", textBox5.Text);

                    cmd.ExecuteNonQuery();
                    maconnexion.Close();
                    textBox1.Clear(); textBox2.Clear(); textBox5.Clear(); 
                    button6.Enabled = true;
                    button9.Enabled = true;

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView4.CurrentCell.RowIndex;
            Console.WriteLine(rowIndex);
            DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment supprimer cette Emprunt", "Supprimer une Emprunt", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {
                dataGridView4.Rows.RemoveAt(rowIndex);
                button6.Enabled = true;
                button9.Enabled = true;
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "DELETE FROM adherents WHERE idAdherent=" + currRowIndex;

                cmd.ExecuteNonQuery();
                maconnexion.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox5.Clear();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Emprunts DataGridView 
           /* DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[4].Value.ToString();
            textBox2.Text = row.Cells[5].Value.ToString();
            textBox3.Text = row.Cells[1].Value.ToString();
            textBox5.Text = row.Cells[2].Value.ToString();
            dateTimePicker1.Text = row.Cells[6].Value.ToString();
            button6.Enabled = true;
            button9.Enabled = true;
            button1.Enabled = false;
            label11.Visible= false;*/
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dashboard a = new dashboard();
            this.Hide();
            a.Show();
        }

      
        private void button4_Click(object sender, EventArgs e)
        {
            gestionlivre a = new gestionlivre();
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
            DataGridViewRow row = this.dataGridView4.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            string dateString = row.Cells[3].Value.ToString();
            DateTime dateValue;
            if (DateTime.TryParse(dateString, out dateValue))
            {
                // The string was successfully converted to a DateTime object.
                dateTimePicker1.Value = dateValue;
            }
            else
            {
                // The string was not in a valid DateTime format.
                // Handle the error as appropriate.
                MessageBox.Show("Invalid date format.");
            }
            button6.Enabled = true;
            button9.Enabled = true;
            button1.Enabled = true;
            label11.Visible = false;

            button1.Enabled = true;

        }

   

       

        private void button12_Click(object sender, EventArgs e)
        {

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
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer déconnecter ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Form1 form1 = new Form1();
                this.Hide();
                form1.Show();
            }
        }

       
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void gestionemp_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
