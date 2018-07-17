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
using Poslovni.Klase;

namespace Poslovni
{
    public partial class UnosArtikla : Form
    {
        public ArtikliAtributi ArtikliAtributi = new ArtikliAtributi();
        public UnosArtikla()
        {
            InitializeComponent();
        }

        private void UnosArtikla_KeyDown(object sender, KeyEventArgs e)
        {
            
                
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down ||e.KeyCode == Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down) && textBox2.Text != "")
            {
                comboBox1.Focus();
            }
            if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control)
            {
                if (textBox2.Text != "")
                {
                    generate:
                    Random r = new Random();
                    int genrend = Math.Abs(r.Next() * 10000);
                    textBox1.Text = genrend.ToString();
                    using (MySqlConnection mySqlConnection = new MySqlConnection(Login.constring)) {
                        mySqlConnection.Open();
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("Select * FROM uartikli", mySqlConnection);
                        DataSet dataSet = new DataSet("uartikli");
                        mySqlDataAdapter.Fill(dataSet);

                        DataTable dataTable = dataSet.Tables[0];

                        for (int i = 0; i < dataTable.Rows.Count; i++) { // ovo osigurava unikatnost sifre koja je generirana
                            if (dataTable.Rows[i][0].ToString() == genrend.ToString()) 
                            {
                                goto generate;
                            }
                        }

                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        { // ovo osigurava unikatnost sifre koja je generirana
                            if (dataTable.Rows[i][1].ToString() == textBox2.Text)
                            {
                                MessageBox.Show("Vec postoji artikl toga naziva");
                                textBox2.Text = "";
                                textBox1.Text = "";
                            }
                        }





                        mySqlConnection.Close();
                    }


                   // if (genrend == exist) { } try new
                   
                    
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && comboBox1.Text != "" && comboBox2.Text != "")
            {
                using (MySqlConnection mySqlConnection = new MySqlConnection(Login.constring))

                {
                    string query = "INSERT INTO uartikli(sifra,naziv,barkod,podgrupa,vrsta,robna_marka,osobine_artikla,opis_artikla,porezna_grupa) VALUES ("+textBox1.Text+",'"+textBox2.Text+"','"+textBox3.Text+"','"+comboBox2.Text+"','"+comboBox1.Text+"','"+comboBox3.Text+"','"+comboBox4.Text+"','"+richTextBox1.Text+"','0')";
                    mySqlConnection.Open();

                    
                    MySqlCommand mySqlCommand = new MySqlCommand(query,mySqlConnection);
                    mySqlCommand.ExecuteNonQuery();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";
                    comboBox2.Text = "";


                    mySqlConnection.Close();

                }
               
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 1;
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {

        }
        void Inicijaliziraj_liste()
        {
            ArtikliAtributi.Init();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();


           foreach (string str in ArtikliAtributi.GetGrupa())
                comboBox1.Items.Add(str);

            foreach (string str in ArtikliAtributi.GetPodgrupa())
                comboBox2.Items.Add(str);

            foreach (string str in ArtikliAtributi.GetRobnaMarka())
                comboBox3.Items.Add(str);

            foreach (string str in ArtikliAtributi.GetOsobineArtikla())
                comboBox4.Items.Add(str);

        }
     

        
        private void UnosArtikla_Load(object sender, EventArgs e)
        {

                Inicijaliziraj_liste();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
    }
}
