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
    public partial class Korisnici : Form
    {
     
        public Korisnici()
        {
            InitializeComponent();
        }
        private void FillTextBoxWithData(int _id)
        {
           
            try
            {

                MySqlConnection mySqlConnection = new MySqlConnection(Login.constring);
                mySqlConnection.Open();
                string query = "select * from korisnici WHERE id = " +_id;
                MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    string id = mySqlDataReader.GetString("id");
                    textBox1.Text = id;
                    string ime = mySqlDataReader.GetString("IME");
                    textBox2.Text = ime;
                    string prezime = mySqlDataReader.GetString("PREZIME");
                    textBox3.Text = prezime;
                    string oib = mySqlDataReader.GetString("OIB");
                    textBox4.Text = oib;
                    string username = mySqlDataReader.GetString("username");
                    textBox5.Text = username;
                    string password = mySqlDataReader.GetString("password");
                    textBox6.Text = password;
                    bool isadmin = mySqlDataReader.GetBoolean("isadmin");
                    checkBox1.Checked = isadmin;

                }
                
            }

            catch (Exception a)
            {
                MessageBox.Show(a.Message);


            }
        }
            private void Sinkroniziraj()
        {
            try
            {
                this.korisnici_grid.Rows.Clear();
                MySqlConnection mySqlConnection = new MySqlConnection(Login.constring);
                mySqlConnection.Open();
                string query = "select * from korisnici";
                MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    string id = mySqlDataReader.GetString("id");
                    string ime = mySqlDataReader.GetString("IME");
                    string prezime = mySqlDataReader.GetString("PREZIME");
                    string oib = mySqlDataReader.GetString("OIB");
                    string username = mySqlDataReader.GetString("username");
                    string password = mySqlDataReader.GetString("password");
                    bool isadmin = mySqlDataReader.GetBoolean("isadmin");
                    this.korisnici_grid.Rows.Add(id, ime, prezime, oib, username, password, isadmin);

                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);

            }

        }
        private void Korisnici_Load(object sender, EventArgs e)
        {
            FillTextBoxWithData(Login.logid);
            
            Sinkroniziraj();

        }

        private void korisnici_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void korisnici_grid_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void korisnici_grid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public void UpdateData()
        {
            
                MySqlConnection mySqlConnection = new MySqlConnection(Login.constring);
                mySqlConnection.Open();
                string query = "UPDATE korisnici SET IME='"+ textBox2.Text +"', PREZIME = '"+textBox3.Text+"', OIB = '"+textBox4.Text+"' , username = '"+textBox5.Text+"' , password = '"+textBox6.Text+"' ,isadmin ="+checkBox1.Checked+" WHERE ID=" + Convert.ToInt32(textBox1.Text);
                MySqlCommand cmd = new MySqlCommand(query,mySqlConnection);
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
           
                


        }

        private void update_Click(object sender, EventArgs e)
        {
            UpdateData();
            Sinkroniziraj();
        }

        private void korisnici_grid_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void korisnici_grid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
            
        }

        private void adduser_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection mySqlConnection = new MySqlConnection(Login.constring);
                mySqlConnection.Open();
                string query = "INSERT INTO korisnici (ID,IME,PREZIME,OIB,username,password,isadmin) VALUES (" + Convert.ToInt32(textBox1.Text) + ",'" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "',"+checkBox1.Checked+");";
                MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                Sinkroniziraj();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "...Updating data instead");
                UpdateData();
                Sinkroniziraj();
            }

        }
        private void RemoveUSR(int id)
        {
            if (id != 1)
            {
                try
                {

                    MySqlConnection mySqlConnection = new MySqlConnection(Login.constring);
                    mySqlConnection.Open();
                    string query = "DELETE FROM korisnici WHERE ID = " + id;
                    MySqlCommand cmd = new MySqlCommand(query, mySqlConnection);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("ROOT USER CAN'T BE REMOVED");
            }
        }
        private void remuser_Click(object sender, EventArgs e)
        {
            RemoveUSR(Convert.ToInt32(textBox1.Text));
            FillTextBoxWithData(Convert.ToInt32(textBox1.Text) - 1);


            Sinkroniziraj();
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox5.Focus();
            }
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox6.Focus();
            }
        }

        private void korisnici_grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void korisnici_grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void korisnici_grid_SelectionChanged(object sender, EventArgs e)
        {
            if (korisnici_grid.CurrentCell.Value == null)
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = "";
            else
            FillTextBoxWithData(korisnici_grid.CurrentRow.Index + 1);
        }
    }
}
