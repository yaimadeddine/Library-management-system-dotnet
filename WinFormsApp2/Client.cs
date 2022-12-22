using System;
using System.Collections;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp2
{
    public partial class Client : Form
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
                string request = "select * from client";
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
        public Client()
        {
            InitializeComponent();
            fetchdata();




        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""|| textBox2.Text=="" || textBox3.Text == "" || numericUpDown1.Text == "")
            {
                MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {

            
            try {
                int isblock = 0;
                if (checkBox1.Checked)
                {
                    isblock = 1;

                }
                
            string Query = "insert into client(id,nom,prenom,cin,isBlocked) values("+numericUpDown1.Text+",'"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"',"+isblock+")";
            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
            MyConn2.Open();
            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
            MySqlDataReader MyReader2;

            MyReader2 = MyCommand2.ExecuteReader();
            MessageBox.Show("Element bien ajouter");
                //fetchdata();
                Client client = new Client();
                this.Hide();
                client.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                numericUpDown1.Text = "";
                checkBox1.Checked = false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridView1.CurrentCell.RowIndex;
            if (s == 0)
            {
                MessageBox.Show("Veuillez Selectionner Un Ligne ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else { 
            try
            {

            
            string Query = "delete from client where id="+idactuelle+"";
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
                textBox3.Text = "";
                numericUpDown1.Text = "";
                checkBox1.Checked = false;
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
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {

                    try
                    {
                        int isblock = 0;
                        if (checkBox1.Checked)
                        {
                            isblock = 1;

                        }
                        string Query = "update client set nom= '" + textBox1.Text + "' ,prenom='" + textBox2.Text + "',cin='" + textBox3.Text + "',isBlocked=" + isblock + " where id= " + idactuelle + "";
                        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                        MyConn2.Open();
                        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                        MySqlDataReader MyReader2;

                        MyReader2 = MyCommand2.ExecuteReader();
                        MessageBox.Show("Bien modifier");
                        //fetchdata();
                        dataGridView1.CurrentRow.SetValues(idactuelle, textBox1.Text, textBox2.Text, textBox3.Text, isblock);



                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    textBox1.Text = "";
                    textBox2.Text = ""; 
                    textBox3.Text = "";
                    numericUpDown1.Text = "";
                    checkBox1.Checked = false;

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            this.Hide();
            a.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            idactuelle = row.Cells[0].Value.ToString();
            s = 1;
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[2].Value.ToString();
            textBox3.Text = row.Cells[3].Value.ToString();
            if (row.Cells[4].Value.ToString() == "True")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

        }
    }
}
