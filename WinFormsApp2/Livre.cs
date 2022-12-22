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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WinFormsApp2
{
    public partial class Livre : Form
    {
        int currRowIndex;
        int s = 0;
        string idactuelle;
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=library";
        DataTable dataTable = new DataTable();
        private MySqlConnection maconnexion;
        public void fetchdata()
        {
            try
            {
                this.dataGridView1.DataSource = null;


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select * from livre";
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
                    //  dataGridView1.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray[4]);
                }
                dataGridView1.DataSource = dataTable;
                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Livre()
        {
            InitializeComponent();
            fetchdata();
        }

        private void Livre_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || numericUpDown1.Text == "" || textBox4.Text == "" || numericUpDown2.Text == "")
            {
                MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {


                try
                {
                   

                    string Query = "insert into livre(id,auteur,titre,editeur,DateEdition,count) values(" + numericUpDown1.Text + ",'" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','"+ dateTimePicker2.Value.ToString("d-MMM-yyyy") + "'," + numericUpDown2.Text + ")";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Element bien ajouter");
                    //fetchdata();
                    Livre a = new Livre();
                    this.Hide();
                    a.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                textBox1.Text = "";
                textBox2.Text = "";
                numericUpDown1.Text = "";
                textBox4.Text = "";
                numericUpDown2.Text = "";
            }
            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                currRowIndex = Convert.ToInt32(row.Cells[0].Value);
                idactuelle = row.Cells[0].Value.ToString();
                s = 1;
                textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            textBox4.Text = row.Cells[3].Value.ToString();
            numericUpDown2.Text = row.Cells[5].Value.ToString();





        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            if (s == 0)
            {
                MessageBox.Show("Veuillez Selectionner Un Ligne ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                try
                {


                    string Query = "delete from livre where id=" + idactuelle + "";
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                    MySqlDataReader MyReader2;

                    MyReader2 = MyCommand2.ExecuteReader();
                    MessageBox.Show("Bien supprimer");
                    dataGridView1.Rows.RemoveAt(rowIndex);
                    //fetchdata();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                textBox1.Text = "";
                textBox2.Text = "";
                numericUpDown1.Text = "";
                textBox4.Text = "";
                numericUpDown2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (s == 0)
            {
                MessageBox.Show("Veuillez Selectionner Un Ligne ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                if (textBox1.Text == "" || textBox2.Text == "" || textBox4.Text == "" || numericUpDown2.Text == "")
                {
                    MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    try
                    {

                        string Query = "update livre set auteur= '" + textBox1.Text + "' ,titre='" + textBox2.Text + "',editeur='" + textBox4.Text + "',DateEdition='" + dateTimePicker2.Value.ToString("d-MMM-yyyy") + "',count=" + numericUpDown2.Text + " where id= " + idactuelle + "";
                        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                        MyConn2.Open();
                        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                        MySqlDataReader MyReader2;

                        MyReader2 = MyCommand2.ExecuteReader();
                        MessageBox.Show("Bien modifier");
                        //fetchdata();
                        dataGridView1.CurrentRow.SetValues(idactuelle, textBox1.Text, textBox2.Text, textBox4.Text, dateTimePicker2.Value.ToString("d-MMM-yyyy"), numericUpDown2.Text);



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    textBox1.Text = "";
                    textBox2.Text = "";
                    numericUpDown1.Text = "";
                    textBox4.Text = "";
                    numericUpDown2.Text = "";

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            this.Hide();
            a.Show();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
