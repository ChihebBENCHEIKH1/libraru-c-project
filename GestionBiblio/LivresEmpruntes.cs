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
    public partial class LivresEmpruntes : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=test; UID=root; PASSWORD=";
        DataTable dataTable = new DataTable();
        private MySqlConnection maconnexion;


        public LivresEmpruntes()
        {

            InitializeComponent();
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox5.ReadOnly = true;
            dateTimePicker1.Enabled = false;
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

        private void button8_Click(object sender, EventArgs e)
        {
            // dataGridView1.Visible = true;
            // dataGridView2.Visible = false;
            dataGridView1.Visible = true;
            //dataGridView3.Visible = false;

            dataTable.Clear();
            dataGridView1.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            service service = new service();
            string request = "SELECT * FROM livres WHERE id in(SELECT idLivre from" +
                " adherent_livre a where a.idAdhrent=" + service.IdAdherent + ")";

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
                dataGridView1.Rows.Add(myArray[0], myArray[5], myArray[4], myArray[1],
                    myArray[2], myArray[3]);
            }

            maconnexion.Close();
        }

        private void LivresEmpruntes_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            EspaceUser espaceUser = new EspaceUser();
            this.Hide();
            espaceUser.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            int currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            LivreService livreService = new LivreService();
            livreService.Id = currRowIndex;
            textBox1.Text = row.Cells[3].Value.ToString();
            textBox2.Text = row.Cells[4].Value.ToString();
            textBox5.Text = row.Cells[2].Value.ToString();
            textBox3.Text = row.Cells[5].Value.ToString();
            string dateString = row.Cells[1].Value.ToString();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            MySqlCommand cmd = maconnexion.CreateCommand();
            LivreService livreService = new LivreService();
            service service = new service();
            cmd.CommandText = "delete from adherent_livre where idAdhrent =@idAdherent and " +
                "idLivre= @idLivre";
            cmd.Parameters.AddWithValue("@idAdherent", service.IdAdherent);
            cmd.Parameters.AddWithValue("@idLivre", livreService.Id);

            cmd.ExecuteNonQuery();
            textBox1.Clear(); textBox2.Clear(); textBox5.Clear(); textBox3.Clear();
            maconnexion.Close();

            dataGridView1.Visible = true;
        }
    }
}
