using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using Poslovni.Klase;

namespace Poslovni
{
  
    public partial class PregledArtikla : Form
    {
        ArtikliAtributi artikliatributi = new ArtikliAtributi();
        public PregledArtikla()
        {
            InitializeComponent();
        }
        private void PostaviListu() {
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listView4.Items.Clear();

            foreach (String s in artikliatributi.GetGrupa())
            {
                listView1.Items.Add(s);
            }

            foreach (String s in artikliatributi.GetPodgrupa())
            {
                listView2.Items.Add(s);
            }

            foreach (String s in artikliatributi.GetOsobineArtikla())
            {
                listView3.Items.Add(s);
            }
            foreach (String s in artikliatributi.GetRobnaMarka())
            {
                listView4.Items.Add(s);
            }

        }


        private void PostaviTablicu() {
            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring)) {
                dataGridView1.DataSource = null;
                mysqlc.Open();
                string query = "SELECT * FROM uartikli";
                DataSet ds = new DataSet();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);
                mySqlDataAdapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;

                DataGridViewComboBoxColumn vrsta = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxColumn robna_marka = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxColumn osobine_artikla = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxColumn podgrupa = new DataGridViewComboBoxColumn();



                robna_marka.DataSource = artikliatributi.GetRobnaMarka();
                robna_marka.HeaderText = "Robna marka";
                robna_marka.DataPropertyName = "robna_marka";


                vrsta.DataSource = artikliatributi.GetGrupa();
                vrsta.HeaderText = "Vrsta robe";
                vrsta.DataPropertyName = "vrsta";

                osobine_artikla.DataSource = artikliatributi.GetOsobineArtikla();
                osobine_artikla.HeaderText = "Osobine artikla";
                osobine_artikla.DataPropertyName = "osobine_artikla";

                podgrupa.DataSource = artikliatributi.GetPodgrupa();
                podgrupa.HeaderText = "Podgrupa";
                podgrupa.DataPropertyName = "podgrupa";

                dataGridView1.Columns.AddRange(robna_marka,vrsta, osobine_artikla, podgrupa);






                dataGridView1.DataSource = dt;

                this.dataGridView1.Columns[3].Visible = false;
                this.dataGridView1.Columns[4].Visible = false;
                this.dataGridView1.Columns[6].Visible = false;
                this.dataGridView1.Columns[7].Visible = false;
            }



        }
        void Pretraga(string like)
        {
            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
            {
                dataGridView1.DataSource = null; 
                mysqlc.Open();
                string query = "SELECT * FROM uartikli WHERE naziv LIKE '%" + like + "%'";
                DataSet ds = new DataSet();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);
                mySqlDataAdapter.Fill(ds);
                DataTable dt = ds.Tables[0];
                dataGridView1.DataSource = dt;

                DataGridViewComboBoxColumn vrsta = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxColumn robna_marka = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxColumn osobine_artikla = new DataGridViewComboBoxColumn();
                DataGridViewComboBoxColumn podgrupa = new DataGridViewComboBoxColumn();



                robna_marka.DataSource = artikliatributi.GetRobnaMarka();
                robna_marka.HeaderText = "Robna marka";
                robna_marka.DataPropertyName = "robna_marka";

    
                vrsta.DataSource = artikliatributi.GetGrupa();
                vrsta.HeaderText = "Vrsta robe";
                vrsta.DataPropertyName = "vrsta";

     
                osobine_artikla.DataSource = artikliatributi.GetOsobineArtikla();
                osobine_artikla.HeaderText = "Osobine artikla";
                osobine_artikla.DataPropertyName = "osobine_artikla";

                podgrupa.DataSource = artikliatributi.GetPodgrupa();
                podgrupa.HeaderText = "Podgrupa";
                podgrupa.DataPropertyName = "podgrupa";


                dataGridView1.Columns.AddRange(robna_marka, vrsta, osobine_artikla, podgrupa);






                dataGridView1.DataSource = dt;

                this.dataGridView1.Columns[3].Visible = false;
                this.dataGridView1.Columns[4].Visible = false;
                this.dataGridView1.Columns[6].Visible = false;
                this.dataGridView1.Columns[7].Visible = false;


            }
        }
        private void PregledArtikla_Load(object sender, EventArgs e)
        {
            artikliatributi.Init();
            PostaviListu();
            PostaviTablicu();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            textBox2.Text = listView1.SelectedItems[0].Text;
        }

        private void button3_Click(object sender, EventArgs e) // NOVA GRUPA CLICK
        {
            if (textBox2.Text != "")
            {
                listView1.Items.Add(textBox2.Text);
                artikliatributi.SetGrupa(listView1.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(0);
            }
            else {
                MessageBox.Show("Unesite ispravan naziv grupe");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "") // IZMJENI CLICK
            {
                listView1.Items[listView1.FocusedItem.Index].SubItems[0].Text = textBox2.Text;

                artikliatributi.SetGrupa(listView1.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

               artikliatributi.UpdateFileWithValues(0);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "") // IZBRISI ODABRANU
            {
                listView1.Items.RemoveAt(listView1.SelectedItems[0].Index);

                artikliatributi.SetGrupa(listView1.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(0);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                listView2.Items.Add(textBox3.Text);
                artikliatributi.SetPodgrupa(listView2.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(1);
            }
            else
            {
                MessageBox.Show("Unesite ispravan naziv podgrupe");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "") // IZMJENI CLICK
            {
                listView2.Items[listView2.FocusedItem.Index].SubItems[0].Text = textBox3.Text;

                artikliatributi.SetPodgrupa(listView2.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "") // IZBRISI ODABRANU
            {
                listView2.Items.RemoveAt(listView2.SelectedItems[0].Index);

                artikliatributi.SetPodgrupa(listView2.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(1);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView2_Click(object sender, EventArgs e)
        {
            textBox3.Text = listView2.SelectedItems[0].Text;
        }

        private void button10_Click(object sender, EventArgs e) // OSOBINE ARTIKLA NOVO 
        {
            if (textBox4.Text != "")
            {
                listView3.Items.Add(textBox4.Text);
                artikliatributi.SetOsobineArtikla(listView3.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(2);
            }
            else
            {
                MessageBox.Show("Unesite ispravanu osobinu artikla");
            }
        }

        private void button11_Click(object sender, EventArgs e) // OSOBINE ARTIKLA IZMJ
        {
            if (textBox4.Text != "") // IZMJENI CLICK
            {
                listView3.Items[listView3.FocusedItem.Index].SubItems[0].Text = textBox4.Text;

                artikliatributi.SetOsobineArtikla(listView3.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(2);
            }
        }

        private void button9_Click(object sender, EventArgs e) // OSOBINE ARTIKLA IZBRISI
        {
            if (textBox4.Text != "") // IZBRISI ODABRANU
            {
                listView3.Items.RemoveAt(listView3.SelectedItems[0].Index);

                artikliatributi.SetOsobineArtikla(listView3.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(2);
            }
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView3_Click(object sender, EventArgs e)
        {
            textBox4.Text = listView3.SelectedItems[0].Text;
        }

        private void button13_Click(object sender, EventArgs e) // NOV ROBNA MARKA
        {
            if (textBox5.Text != "")
            {
                listView4.Items.Add(textBox5.Text);
                artikliatributi.SetRobnaMarka(listView4.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(3);
            }
            else
            {
                MessageBox.Show("Unesite ispravanu robnu marku");
            }
        }

        private void button14_Click(object sender, EventArgs e) // IZMJ 
        {
            if (textBox5.Text != "") // IZMJENI CLICK
            {
                listView4.Items[listView4.FocusedItem.Index].SubItems[0].Text = textBox5.Text;

                artikliatributi.SetRobnaMarka(listView4.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(3);
            }
        }

        private void button12_Click(object sender, EventArgs e) // IZBRISI
        {
            if (textBox5.Text != "") // IZBRISI ODABRANU
            {
                listView4.Items.RemoveAt(listView4.SelectedItems[0].Index);

                artikliatributi.SetRobnaMarka(listView4.Items.Cast<ListViewItem>().Select(item => item.Text).ToList());

                artikliatributi.UpdateFileWithValues(3);
            }
        }

        private void listView4_Click(object sender, EventArgs e)
        {
            textBox5.Text = listView4.SelectedItems[0].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pretraga(textBox1.Text);

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
            
        }
    }
}
