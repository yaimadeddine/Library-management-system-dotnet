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
namespace WinFormsApp2
{
    public partial class Ouvrage : Form
    {
        string MyConnection2 = "server=localhost;User ID=root;Password=;Database=lib";
        DataTable dataTable = new DataTable();
        private MySqlConnection maconnexion;
        public Ouvrage()
        {
           
             
            InitializeComponent();
           
            try
            {


                maconnexion = new MySqlConnection(MyConnection2);
                maconnexion.Open();
                string request = "select * from ouvrage";
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
                    dataGridView1.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray[4]);
                }
            

                maconnexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {


                string Query = "delete from ouvrage where id=" + textBox4.Text + "";
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


                string Query = "update ouvrage set titre='" + textBox1.Text + "', auteur='" + textBox2.Text + "',editeur'"+ textBox3.Text + "',date='"+ dateTimePicker1 + "' where id=" + textBox1.Text + "";
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
                string Query = "insert into ouvrage (titre,auteur,editeur,date) values('" + textBox1.Text + "','" + textBox2.Text + "','"+textBox3.Text+"','"+dateTimePicker1+"')";
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
            try
            {
                string Query = "insert into emprunt (client,ouvrage) values('" + textBox1.Text + "','" + textBox1.Text + "')";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MyConn2.Open();
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;

                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("Bien ajouter");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {


                string Query = "update emprunt set client='" + textBox1.Text + "', ouvrage='" + textBox2.Text + "' where id=" + textBox3.Text + "";
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
        private void Ouvrage_Load(object sender, EventArgs e) { 
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {


                string Query = "delete from ouvrage where id=" + textBox3.Text + "";
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            try
            {
                string Query = "insert into ouvrage (titre,auteur,editeur,date) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "')";
                MySqlConnection MyConn2 = new MySqlConnection(MyConnection2);
                MyConn2.Open();
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;

                MyReader2 = MyCommand2.ExecuteReader();
                MessageBox.Show("bien ajouter");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {


                string Query = "update ouvrage set titre='" + textBox1.Text + "', auteur='" + textBox2.Text + "',editeur'" + textBox3.Text + "',date='" + dateTimePicker1.Value.Date + "' where id=" + textBox4.Text + "";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
