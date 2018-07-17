using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Poslovni
{
    public partial class UnosPartnera : Form
    {
        public UnosPartnera()
        {
            InitializeComponent();
        }
        private void UnesiPartnera()
        {
            using (MySqlConnection mysql = new MySqlConnection(Login.constring))
            {
                mysql.Open();
                DataSet ds = new DataSet();
                string query1 = "SELECT * FROM poslovni_partner";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query1, mysql);
                mySqlDataAdapter.Fill(ds);

                DataTable dataTable = ds.Tables[0];

                int next = dataTable.Rows.Count + 1;


                string query2 = "INSERT INTO poslovni_partner (id_poslovni_partner,naziv,adresa,adresa_racuna,oib,telefon,telefax,korisnik_od,email,iban) VALUES(" + next +
                    ",'" + textBox1.Text +
                    "','" + textBox2.Text +
                    "','" + textBox3.Text +
                    "','" + textBox4.Text +
                    "','" + textBox5.Text +
                    "','" + textBox6.Text +
                    "','" + DateTime.UtcNow.Date +
                    "','" + textBox7.Text +
                    "','" + textBox8.Text +
                    "');";
               
                    MySqlCommand mySqlCommand = new MySqlCommand(query2, mysql);
                    mySqlCommand.ExecuteNonQuery();
                    textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
                

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UnesiPartnera();
            UcitajPartnere();
        }
        private void UcitajPartnere() {
            DataSet dataSet = new DataSet();
            using (MySqlConnection mysql = new MySqlConnection(Login.constring))
            {

                string query = "SELECT * FROM poslovni_partner";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysql);
                mySqlDataAdapter.Fill(dataSet);
            }


            dataGridView1.DataSource = dataSet.Tables[0];

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void UnosPartnera_Load(object sender, EventArgs e)
        {
            UcitajPartnere();


        }
    }
}
