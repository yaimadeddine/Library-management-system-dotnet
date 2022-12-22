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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using K4os.Compression.LZ4.Internal;

namespace WinFormsApp2
{
    public partial class Emprunt : Form
    {
        int currRowIndex;
        int s = 0;
        string[] clientCIN = new string[100];
        string idactuelle;
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=library";
        DataTable dataTable = new DataTable();
        DataTable dataTable1 = new DataTable();
        private MySqlConnection maconnexion1;
        private MySqlConnection maconnexion;
        public void fetchdata()
        {
            try
            {
                this.dataGridView1.DataSource = null;


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select * from emprunt";
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
        public void fetchdataClient()
        {
            try
            {
                maconnexion1 = new MySqlConnection(MyConnection2);
                maconnexion1.Open();
                string request = "select cin from client where isBlocked=0";
                MySqlCommand cmd1 = new MySqlCommand(request, maconnexion);
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                da1.Fill(dataTable1);
                foreach (DataRow dataRow in dataTable1.Rows)
                {
                 
                    foreach (var item in dataRow.ItemArray)
                    {
                        comboBox2.Items.Add(item.ToString());
                 
                    }
                    //  dataGridView1.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray[4]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Emprunt()
        {
           





          
            InitializeComponent();
            fetchdata();
            fetchdataClient();
        }
        private void Emprunt_Load(object sender, EventArgs e)
        {

        }




        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                string Query = "delete from emprunt where client=" + comboBox2.Text + "";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MyConn2.Open();
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;

                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Bien supprimer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {


                string Query = "update emprunt set client=" + comboBox2.Text + ", ouvrage=" + numericUpDown2.Text + " where client=" + comboBox2.Text + "";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MyConn2.Open();
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;

                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Bien modifier");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "insert into emprunt (client,ouvrage) values('" + comboBox2.Text + "','" + comboBox2.Text + "')";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MyConn2.Open();
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;

                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("cnx bien faite");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 a = new Form2();
            this.Hide();
            a.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            if (comboBox2.Text == "" || numericUpDown2.Text == "" || numericUpDown1.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                if (dateTimePicker1.Value.Date.CompareTo(dateTimePicker2.Value.Date) > 0)
                {
                    MessageBox.Show("Date début est supprieur  que date fin", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

              
                }
                else
                {

                    try
                    {




                        string Query = "insert into emprunt(id,Cin,DateDebut,IdOuvrage,DateFin,TypeOuvrage) values(" + numericUpDown1.Text + ",'" + comboBox2.Text + "','" + dateTimePicker1.Value.ToString("d-MMM-yyyy") + "','" + numericUpDown2.Text + "','" + dateTimePicker2.Value.ToString("d - MMM - yyyy") + "','" + comboBox1.Text + "')";
                        string QueryClient = "update client set isBlocked=" + 1 + " where Cin='" + comboBox2.Text + "'";
                        string QueryOuvrage;
                        if (comboBox1.Text == "Livre")
                        {
                            QueryOuvrage = "update livre set count=count-1 where id=" + numericUpDown2.Text + "";

                        }
                        else if (comboBox1.Text == "Periodique")
                        {
                            QueryOuvrage = "update periodique set count=count-1 where id=" + numericUpDown2.Text + "";


                        }
                        else
                        {
                            QueryOuvrage = "update cd set count=count-1 where id=" + numericUpDown2.Text + "";


                        }
                        MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                        MyConn2.Open();
                        //Block client:
                        MySqlCommand MyCommand1 = new MySqlCommand(QueryClient, MyConn2);
                        MySqlDataReader MyReader1;

                        MyReader1 = MyCommand1.ExecuteReader();
                        MyReader1.Close();
                        //Block ouvrage:
                        MySqlCommand MyCommand3 = new MySqlCommand(QueryOuvrage, MyConn2);
                        MySqlDataReader MyReader3;

                        MyReader3 = MyCommand3.ExecuteReader();
                        MyReader3.Close();
                        //Requet Final
                        MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                        MySqlDataReader MyReader2;

                        MyReader2 = MyCommand2.ExecuteReader();
                        MessageBox.Show("Element bien ajouter");
                        //fetchdata();
                        Emprunt emprunt = new Emprunt();
                        this.Hide();
                        emprunt.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    numericUpDown2.Text = "";
                    numericUpDown1.Text = "";
                    comboBox1.Text = "";
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (s == 0)
            {
                MessageBox.Show("Veuillez Selectionner Un Ligne ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                if (comboBox2.Text == "" || numericUpDown2.Text == "" || comboBox1.Text == "")
                {
                    MessageBox.Show("Veuillez Remplir tous les champs ", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    if (dateTimePicker1.Value.Date.CompareTo(dateTimePicker2.Value.Date) > 0)
                    {
                        MessageBox.Show("Date début est supprieur  que date fin", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                    }
                    else
                    {

                        try
                        {
                            string Query1 = "update client set isBlocked=" + 1 + " where Cin='" + comboBox2.Text + "'";
                            string Query2;
                            if (comboBox1.Text == "Livre")
                            {
                                Query2 = "update livre set count=count-1 where id=" + numericUpDown2.Text + "";

                            }
                            else if (comboBox1.Text == "Periodique")
                            {
                                Query2 = "update periodique set count=count-1 where id=" + numericUpDown2.Text + "";


                            }
                            else
                            {
                                Query2 = "update cd set count=count-1 where id=" + numericUpDown2.Text + "";


                            }

                            string Query = "update emprunt set Cin= '" + comboBox2.Text + "' ,DateDebut='" + dateTimePicker1.Value.ToString("d-MMM-yyyy") + "',DateFin='" + dateTimePicker2.Value.ToString("d-MMM-yyyy") + "',IdOuvrage='" + numericUpDown2.Text + "',TypeOuvrage='" + comboBox1.Text + "' where id= " + idactuelle + "";
                            MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                            MyConn2.Open();
                            MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                            MySqlDataReader MyReader2;

                            MyReader2 = MyCommand2.ExecuteReader();
                            MyReader2.Close();
                            //Block client:
                            MySqlCommand MyCommand1 = new MySqlCommand(Query1, MyConn2);
                            MySqlDataReader MyReader1;

                            MyReader1 = MyCommand1.ExecuteReader();
                            MyReader1.Close();
                            //Block ouvrage:
                            MySqlCommand MyCommand3 = new MySqlCommand(Query2, MyConn2);
                            MySqlDataReader MyReader3;

                            MyReader3 = MyCommand3.ExecuteReader();
                            MyReader3.Close();
                            MessageBox.Show("Bien modifier");
                            //fetchdata();
                            dataGridView1.CurrentRow.SetValues(idactuelle, comboBox2.Text, dateTimePicker1.Value.ToString("d-MMM-yyyy"), dateTimePicker2.Value.ToString("d-MMM-yyyy"), numericUpDown2.Text, comboBox1.Text);



                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        comboBox2.Text = "";
                        numericUpDown2.Text = "";
                        numericUpDown1.Text = "";
                        comboBox1.Text = "";
                    }
                }
            }

            }

        private void button3_Click_1(object sender, EventArgs e)
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


                    string Query = "delete from emprunt where id=" + idactuelle + "";
                    string Query1 = "update client set isBlocked=" + 0 + " where Cin='" + comboBox2.Text + "'";
                    string Query2;
                    if (comboBox1.Text == "Livre")
                    {
                        Query2 = "update livre set count=count+1 where id=" + numericUpDown2.Text + "";

                    }
                    else if (comboBox1.Text == "Periodique")
                    {
                        Query2 = "update periodique set count=count+1 where id=" + numericUpDown2.Text + "";


                    }
                    else
                    {
                        Query2 = "update cd set count=count+1 where id=" + numericUpDown2.Text + "";


                    }
                    MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                    MyConn2.Open();
                    //Block client:
                    MySqlCommand MyCommand1 = new MySqlCommand(Query1, MyConn2);
                    MySqlDataReader MyReader1;

                    MyReader1 = MyCommand1.ExecuteReader();
                    MyReader1.Close();
                    //Block ouvrage:
                    MySqlCommand MyCommand3 = new MySqlCommand(Query2, MyConn2);
                    MySqlDataReader MyReader3;

                    MyReader3 = MyCommand3.ExecuteReader();
                    MyReader3.Close();
      
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
                comboBox2.Text = "";
                numericUpDown2.Text = "";
                numericUpDown1.Text = "";
                comboBox1.Text = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                currRowIndex = Convert.ToInt32(row.Cells[0].Value);
            idactuelle = row.Cells[0].Value.ToString();
            s = 1;
            comboBox2.Text = row.Cells[1].Value.ToString();
                numericUpDown2.Text = row.Cells[4].Value.ToString();
            comboBox1.Text = row.Cells[5].Value.ToString();




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
