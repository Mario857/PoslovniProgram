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

  

    public partial class Kalkulacije : Form
    {

        bool forcestop = false;

        public int aktivna_kal = 1;

        DataSet primka_stavke = new DataSet();
        DataTable primka_stavke_table;

        public Kalkulacije()
        {
            InitializeComponent();
        }

        private List<String> GetUneseniArtikliNaziv()
        {
            List<String> list = new List<String>();
            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring)) {

                sqlcon.Open();
                string query = "SELECT * FROM uartikli";
                MySqlCommand mySqlCommand = new MySqlCommand(query, sqlcon);
                MySqlDataReader mySqlDataReader;
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    list.Add(mySqlDataReader.GetString("naziv"));
                }

                return list;
            }
        }
        private List<String> GetUneseniArtikliGrupa()
        {
            List<String> list = new List<String>();
            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
            {

                sqlcon.Open();
                string query = "SELECT * FROM uartikli";
                MySqlCommand mySqlCommand = new MySqlCommand(query, sqlcon);
                MySqlDataReader mySqlDataReader;
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    list.Add(mySqlDataReader.GetString("vrsta"));
                }

                return list;
            }
        }
        private List<String> GetUneseniArtikliSifra()
        {
            List<String> list = new List<String>();
            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
            {

                sqlcon.Open();
                string query = "SELECT * FROM uartikli";
                MySqlCommand mySqlCommand = new MySqlCommand(query, sqlcon);
                MySqlDataReader mySqlDataReader;
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    list.Add(mySqlDataReader.GetString("sifra"));
                }

                return list;
            }
        }
        private List<String> GetUneseniArtikliPoreza()
        {
            List<String> list = new List<String>();
            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
            {

                sqlcon.Open();
                string query = "SELECT * FROM uartikli";
                MySqlCommand mySqlCommand = new MySqlCommand(query, sqlcon);
                MySqlDataReader mySqlDataReader;
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    list.Add(mySqlDataReader.GetString("porezna_grupa"));
                }

                return list;
            }
        }
        private int GetStopaFromIndex(int i)
        {
            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
            {

                sqlcon.Open();
                string query = "SELECT * FROM stopa_poreza WHERE id_porez = " + i;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlcon);
                DataSet ds = new DataSet();
                mySqlDataAdapter.Fill(ds);
                DataTable dt = ds.Tables[0];

               return Convert.ToInt32(dt.Rows[0]["iznos_stope"].ToString());
            }
        }
        private void RačunajPodnozje()
        {
            float osnovica = 0;
            try
            {

                for (int i = 0; i < dataGridView1.RowCount; i++)
                    osnovica += Convert.ToSingle(dataGridView1.Rows[i].Cells["vrijednost"].Value);
                textBox3.Text = osnovica.ToString();

            }
            catch { };
            float pdv = osnovica * 0.25f;
            textBox4.Text = pdv.ToString();

            float ukupno = pdv + osnovica;
            textBox5.Text = ukupno.ToString();

        }
        private void SinkronizirajPrimku(int kalkulacija,bool brisi)
        {

            using (MySqlConnection sqlconn = new MySqlConnection(Login.constring))
            {
                Random random = new Random();



                sqlconn.Open();

                if (brisi == true) { 
                    string query = "DELETE FROM primka_stavke WHERE id_primka = " + kalkulacija;

                MySqlCommand mySqlCommand = new MySqlCommand(query, sqlconn);
                mySqlCommand.ExecuteNonQuery();
            }

                string query1 = "SELECT * FROM primka_stavke WHERE id_primka = " + kalkulacija;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query1, sqlconn);
                DataSet ds = new DataSet();
                mySqlDataAdapter.Fill(ds);

                MySqlCommandBuilder cb = new MySqlCommandBuilder(mySqlDataAdapter);

                mySqlDataAdapter.UpdateCommand = cb.GetUpdateCommand();

                DataTable dt = ds.Tables[0];




                for (int i = 0; i < dataGridView1.Rows.Count ; i++)
                {
                    try
                    {
                        if (Convert.ToInt32(dataGridView1.Rows[i].Cells["id_artikl"].Value) != 0)
                        {
                            int r = random.Next(1, 1234567891);
                            int g = random.Next(1, 123);
                            int P = r * g * random.Next(1, 321);
                            
                            dt.Rows.Add(new String[] { kalkulacija.ToString(), dataGridView1.Rows[i].Cells["id_artikl"].Value.ToString(), dataGridView1.Rows[i].Cells["kolicina"].Value.ToString(), dataGridView1.Rows[i].Cells["grupa"].Value.ToString(), dataGridView1.Rows[i].Cells["pdv"].Value.ToString(), dataGridView1.Rows[i].Cells["jed_mj"].Value.ToString(), dataGridView1.Rows[i].Cells["cijena"].Value.ToString(), dataGridView1.Rows[i].Cells["vrijednost"].Value.ToString(), dataGridView1.Rows[i].Cells["rabat"].Value.ToString(), dataGridView1.Rows[i].Cells["marza"].Value.ToString(), dataGridView1.Rows[i].Cells["RUC"].Value.ToString(), dataGridView1.Rows[i].Cells["MPC"].Value.ToString(), dataGridView1.Rows[i].Cells["Popust"].Value.ToString(), dataGridView1.Rows[i].Cells["MPC_Popust"].Value.ToString(), dataGridView1.Rows[i].Cells["NazivArtikla"].Value.ToString(), dataGridView1.Rows[i].Cells["id"].Value.ToString(), (10998877665544 - dt.Rows.Count - P).ToString() });
                            
                        }
                     }
                    catch { };
                 }

                mySqlDataAdapter.Update(dt);
            }
            RačunajPodnozje();


        }
        private bool ProvjeriAktivnostKalkulacije(int kal)
        {
            try
            {
                using (MySqlConnection mysql = new MySqlConnection(Login.constring))

                {
                    mysql.Open();
                    string query = "SELECT aktivan FROM primka WHERE id_primka =" + kal;
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysql);
                    DataSet ds = new DataSet();
                    mySqlDataAdapter.Fill(ds);
                    DataTable dt = ds.Tables[0];


                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        return true;
                    }
                    else if (dt.Rows[0][0].ToString() == "0")
                    {
                        return false;
                    }

                    return false;

                }
            }
            catch
            {
                return false;
            }
        }
        private void UcitajStavke(int kalkulacija,bool brisi)
        {
            
            try {
                using (MySqlConnection sqlconn = new MySqlConnection(Login.constring))
                {
                    sqlconn.Open();
                    string query = "SELECT id,id_artikl,NazivArtikla,kolicina,grupa,pdv,jed_mj,cijena,vrijednost,rabat,marza,RUC,MPC,Popust,MPC_Popust FROM primka_stavke WHERE id_primka = " + kalkulacija;

                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlconn);

                    mySqlDataAdapter.Fill(primka_stavke);
                    primka_stavke_table = primka_stavke.Tables[0];

                    dataGridView1.DataSource = primka_stavke_table;
                    dataGridView1.Show();
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
            SinkronizirajPrimku(aktivna_kal, brisi);
            textBox6.Text = aktivna_kal.ToString();

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (ProvjeriAktivnostKalkulacije(aktivna_kal))
                {
                    dataGridView1.Columns["NazivArtikla"].ReadOnly = !ProvjeriAktivnostKalkulacije(aktivna_kal);
                    pictureBox1.Image = Poslovni.Properties.Resources.if_bullet_green_84433;
                }
                else
                {
                    //ZAKLJUCANA
                    dataGridView1.Columns[i].ReadOnly = true;
                    pictureBox1.Image = Poslovni.Properties.Resources.if_bullet_red_84435;
                }
            }
            
        }

        private void SinkronizirajKal()
        {
           using (MySqlConnection sqlconn = new MySqlConnection(Login.constring))
           {
                sqlconn.Open();
                string query = "SELECT * FROM primka";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlconn);

                try
                {
                    DataSet ds = new DataSet();
                    mySqlDataAdapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    listBox1.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                        listBox1.Items.Add(dt.Rows[i]["id_primka"].ToString() + "  " + dt.Rows[i]["dobavljac"].ToString() + "    " + dt.Rows[i]["datum_primke"].ToString().TrimEnd(". 00:00".ToCharArray()));

                }
                catch {
                    listBox1.Items.Clear();
                    aktivna_kal = -1;
                }
                
           }
        }

        private void ZaključajCelije()
        {

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                try
                {
                    if ((Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == 123456789))
                    {
                        for (int cells = 3; cells < dataGridView1.ColumnCount; cells++)
                        {
                            dataGridView1.Rows[i].Cells[cells].ReadOnly = true;
                        }
                    }
                    else
                    {
                        for (int cells = 3; cells < dataGridView1.ColumnCount; cells++)
                        {
                            dataGridView1.Rows[i].Cells[cells].ReadOnly = false;
                        }
                    }
                }

                catch { };
            dataGridView1.Columns["id_artikl"].ReadOnly = true;
            dataGridView1.Columns["marza"].ReadOnly = true;
            dataGridView1.Columns["RUC"].ReadOnly = true;
        }
        int resizeor = 17;
        private void PrilagodiCelije()
        {
            dataGridView1.Columns[0].Width = 25 + resizeor;
            dataGridView1.Columns[1].Width = 80 + resizeor;
            dataGridView1.Columns[2].Width = 100 + resizeor;
            dataGridView1.Columns["kolicina"].Width = 55 + resizeor;
            dataGridView1.Columns["grupa"].Width = 55 + resizeor;
            dataGridView1.Columns["pdv"].Width = 55 + resizeor;
            dataGridView1.Columns["jed_mj"].Width = 55 + resizeor;
            dataGridView1.Columns["vrijednost"].Width = 55 + resizeor;
            dataGridView1.Columns["cijena"].Width = 55 + resizeor;
            dataGridView1.Columns["RUC"].Width = 35 + resizeor;
            dataGridView1.Columns["MPC"].Width = 35 + resizeor;
            dataGridView1.Columns["Popust"].Width = 55 + resizeor;
            dataGridView1.Columns["marza"].Width = 35+ resizeor;
            dataGridView1.Columns["rabat"].Width = 35 + resizeor;

           





            dataGridView1.Columns["id_artikl"].HeaderText = "Šifra";
            dataGridView1.Columns["marza"].HeaderText = "M%";
            dataGridView1.Columns["MPC_popust"].HeaderText = "MPC Popust";
            dataGridView1.Columns["kolicina"].HeaderText = "Količina";
            dataGridView1.Columns["rabat"].HeaderText = "R%";

            dataGridView1.Columns["id_artikl"].ReadOnly = true;
            dataGridView1.Columns["marza"].ReadOnly = true;
            dataGridView1.Columns["RUC"].ReadOnly = true;
            dataGridView1.Columns["id"].ReadOnly = true;

        }
        private void PostaviElementeZaglavnja()
        {
            textBox8.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox8.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
            {


                sqlcon.Open();
                string query = "SELECT * FROM poslovni_partner";


                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlcon);
                DataSet ds = new DataSet();
                mySqlDataAdapter.Fill(ds);

                DataTable dt = ds.Tables[0];

                for (int i = 0; i < dt.Rows.Count; i++)
                    col.Add(dt.Rows[i]["naziv"].ToString());

                textBox8.AutoCompleteCustomSource = col;
              
            }


        }
        private void Kalkulacije_Load(object sender, EventArgs e)
        {
            


            PostaviElementeZaglavnja();
            UcitajListuPrimki();


            SinkronizirajKal();
            UcitajStavke(aktivna_kal,true);
            ReindeksirajStavke();
            PrilagodiCelije();

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["id"].ReadOnly = true;
                dataGridView1.Rows[i].Cells["id"].Value = (i + 1);
            }
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            
        }
        private int setindextomylist(string e, List<String> a)
        {
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == e)
                {
                    return i;
                }
            }
            return 123456789;
        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var source = new AutoCompleteStringCollection();
            source.AddRange(GetUneseniArtikliNaziv().ToArray());

            if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                TextBox prodCode = e.Control as TextBox;
                if (prodCode != null)
                {
                    prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    prodCode.AutoCompleteCustomSource = source;
                    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;

                }

            }
            else
            {
                TextBox prodCode = e.Control as TextBox;
                if (prodCode != null)
                {
                    prodCode.AutoCompleteMode = AutoCompleteMode.None;
                }
            }
        }
        public void ReindeksirajStavke()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["id"].ReadOnly = true;
                //dataGridView1.Rows[i].Cells["grupa"].ReadOnly = true;
                if (i != dataGridView1.RowCount - 1)
                    dataGridView1.Rows[i].Cells["id"].Value = (i + 1);
            }
            ZaključajCelije();

        }
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            ReindeksirajStavke();




        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {

        }


        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {



        }
        private void Izracunaj()
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    if (dataGridView1.Rows[i].Cells["kolicina"].Value.ToString() != "" && dataGridView1.Rows[i].Cells["vrijednost"].Value.ToString() != "")
                    {
                        

                        dataGridView1.Rows[i].Cells["marza"].ReadOnly = true;
                        dataGridView1.Rows[i].Cells["RUC"].ReadOnly = true;

                      //  dataGridView1.Rows[i].Cells["marza"].Value = Convert.ToSingle(dataGridView1.Rows[i].Cells["MPC"].Value) / Convert.ToSingle(dataGridView1.Rows[i].Cells["cijena"].Value);
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells["cijena"].Value = 0;
                        dataGridView1.Rows[i].Cells["vrijednost"].Value = 0;
                        dataGridView1.Rows[i].Cells["marza"].Value = 0;
                        dataGridView1.Rows[i].Cells["RUC"].Value = 0;
                        dataGridView1.Rows[i].Cells["MPC"].Value = 0;
                        dataGridView1.Rows[i].Cells["Popust"].Value = 0;
                        dataGridView1.Rows[i].Cells["MPC_Popust"].Value = 0;
                        dataGridView1.Rows[i].Cells["rabat"].Value = 0;
                    }
            }
            catch { };
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value = GetUneseniArtikliSifra()[setindextomylist(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString(), GetUneseniArtikliNaziv())];
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[4].Value = GetUneseniArtikliGrupa()[setindextomylist(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString(), GetUneseniArtikliNaziv())];
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value = GetStopaFromIndex(0);
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value = 1;

           
            }
            catch
            {
                dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value = 123456789;
               
            }
            try
            {
                if (dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value.ToString() == "")
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value = "0";

            }
            catch { };
            try
            {
                if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[1].Value) == 123456789)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2];
                }
            }
            catch { };


            Izracunaj();

            ZaključajCelije();
            try
            {
                if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value) == 123456789) // if item is added dont allow multiple items
                {
                    MessageBox.Show("Item not found in base");
                    dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value = "";
                }
            }
            catch { };
            //MessageBox.Show(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString());

            try
            {
                int p = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                   
                    if ((dataGridView1.Rows[i].Cells[1].Value.ToString() == dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString()))
                    {
                        p++;
                        if (p > 1)
                        {
                            MessageBox.Show("Multiple items are not allowed!");

                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value = 123456789;
                            
                            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value = "";

                            if (Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value) == 123456789)
                            {
                                dataGridView1.Rows.RemoveAt(i);
                                
                            }

                            ZaključajCelije();
                            p = 0;
                        }
                    }
                }
            }
            catch { };
            try
            {

                if (dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].ColumnIndex == e.ColumnIndex)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["cijena"].Value = Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value) / Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value);

                    if (dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value.ToString() != "0")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["marza"].Value = (1 - ((Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["cijena"].Value) * (1 + (Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["pdv"].Value) / 100))) / Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value))) * 100;
                        dataGridView1.Rows[e.RowIndex].Cells["MPC_Popust"].Value = Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value) / (1 + (Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["Popust"].Value) / 100));
                    }
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["cijena"].ColumnIndex == e.ColumnIndex)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value = Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["cijena"].Value) * Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value);
                    if (dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value.ToString() != "0")
                    {
                        dataGridView1.Rows[e.RowIndex].Cells["marza"].Value = (1 - ((Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["cijena"].Value) * (1 + (Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["pdv"].Value) / 100))) / Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value))) * 100;
                        dataGridView1.Rows[e.RowIndex].Cells["MPC_Popust"].Value = Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value) / (1 + (Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["Popust"].Value) / 100));
                    }
                }
                else if (dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["kolicina"].ColumnIndex == e.ColumnIndex)
                    dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value = Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["cijena"].Value) * Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value);
                else if (dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value.ToString() != "0")
                {
                    dataGridView1.Rows[e.RowIndex].Cells["marza"].Value = (1 - ((Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["cijena"].Value) * (1 + (Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["pdv"].Value) / 100))) / Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value))) * 100;
                    dataGridView1.Rows[e.RowIndex].Cells["MPC_Popust"].Value = Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["MPC"].Value) / (1 + (Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["Popust"].Value) / 100));
                }
            }

          

            //else if (dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value.ToString() != "0" && dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].ColumnIndex == e.ColumnIndex)
            //    dataGridView1.Rows[e.RowIndex].Cells["cijena"].Value = Convert.ToSingle(dataGridView1.Rows[e.RowIndex].Cells["vrijednost"].Value) / Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["kolicina"].Value);


            catch { };
            SinkronizirajPrimku(aktivna_kal,true);


        }

        private void tabControl2_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        bool isAllowedtoSkip = true;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if Enter is pressed
            
            if (keyData == Keys.Enter && forcestop == false)
            {
                
                // If there isn't any selected row, do nothing
                if (dataGridView1.CurrentRow == null)
                {
                    return true;
                }

                // Display first cell's value

                if (isAllowedtoSkip)
                {

                    int newRow = 0;
                    int newColumn = 0;
                    if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)         // it's a last column, move to next row;
                    {
                        
                        newRow = dataGridView1.CurrentCell.RowIndex + 1;
                        newColumn = 2;

                        if (newRow == dataGridView1.RowCount)
                            return false;
                    }
                    else
                    {
                        newRow = dataGridView1.CurrentCell.RowIndex;

                        switch (dataGridView1.CurrentCell.ColumnIndex)
                        {
                            case 3:
                                newColumn = dataGridView1.CurrentCell.ColumnIndex + 5;
                                
                                break;
                            case 9:
                                
                                newColumn = dataGridView1.CurrentCell.ColumnIndex + 3;

                                break;
                            default:
                                newColumn = dataGridView1.CurrentCell.ColumnIndex + 1;
                                break;
                        }


                       

                    }

                    dataGridView1.CurrentCell = dataGridView1.Rows[newRow].Cells[newColumn];
                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {


            

        }

        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            ReindeksirajStavke();
            SinkronizirajPrimku(aktivna_kal,true);
        }
        private void UcitajListuPrimki()
        {
            ReindeksirajPrimke();

        }
        private void ReindeksirajPrimke()
        {
            using (MySqlConnection mysql = new MySqlConnection(Login.constring))
            {
                try
                {
                    mysql.Open();
                    string query = "SELECT * FROM primka";
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysql);

                    DataSet ds = new DataSet();
                    mySqlDataAdapter.Fill(ds);

                    DataTable dt = ds.Tables[0];



                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string query1 = "UPDATE primka SET id_primka = '" +(i + 1)+ "' WHERE id_primka= '" +dt.Rows[i]["id_primka"].ToString() +"';";

                        MySqlCommand mySqlCommand = new MySqlCommand(query1, mysql);
                        mySqlCommand.ExecuteNonQuery();
                    }
                    
                }
                catch (Exception exception) {

                    MessageBox.Show(exception.ToString());
                        }
            }

        }
        private void nova_kal_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {




                using (MySqlConnection mysql = new MySqlConnection(Login.constring))
                {
                    try
                    {
                        string query1 = "SELECT * FROM primka";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query1, mysql);

                        DataSet ds = new DataSet();
                        mySqlDataAdapter.Fill(ds);

                        DataTable dt = ds.Tables[0];


                        int primkabr = dt.Rows.Count + 1;


                        mysql.Open();
                        string query = "INSERT INTO primka(id_primka,dobavljac,p_kreirao,datum_primke,ukupno,aktivan,temeljem) VALUES (" + primkabr + ",'" + textBox8.Text + "','" + Login.logid + "','" + DateTime.Today + "',0,1, '"+textBox2.Text+"')";


                        MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                        mySqlCommand.ExecuteNonQuery();
                        SinkronizirajKal();

                       

                    }
                    catch { };
                }
            }
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



            try
            {
                string[] sp = listBox1.Items[listBox1.SelectedIndex].ToString().Split(' ');





                aktivna_kal = Convert.ToInt32(sp[0]);
               
                while (dataGridView1.Rows.Count > 0 && dataGridView1.Rows[0].IsNewRow == false)

                {

                    dataGridView1.Rows.RemoveAt(0);

                }

                    dataGridView1.DataSource = null;
                    UcitajStavke(aktivna_kal, true);
                    dataGridView1.Sort(dataGridView1.Columns["id"], ListSortDirection.Ascending);
                

                   // ReindeksirajStavke();
                    PrilagodiCelije();
                    

                //UcitajStavke(aktivna_kal, true);
                    

            }
            catch { };
        }

        private void spremi_Click(object sender, EventArgs e)
        {
           
        }

        private void listBox1_ValueMemberChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
           
        }
       
        private void listBox1_SizeChanged(object sender, EventArgs e)
        {
          
        }
        private void listBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            

            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.Red), e.Bounds);
            ListBox lb = (ListBox)sender;
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e = new DrawItemEventArgs(e.Graphics,
                                          e.Font,
                                          e.Bounds,
                                          e.Index,
                                          e.State ^ DrawItemState.Selected,
                                          e.ForeColor,
                                          Color.GreenYellow);//Choose the color

            e.DrawBackground();

            //    e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);

            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);
            


           
            


            e.DrawFocusRectangle();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (ProvjeriAktivnostKalkulacije(aktivna_kal) )
            {
                if (aktivna_kal == listBox1.Items.Count)
                {

                    using (MySqlConnection mysql = new MySqlConnection(Login.constring))
                    {
                        mysql.Open();



                        string query = "" +
                            "DELETE FROM primka_stavke WHERE id_primka = " + aktivna_kal +
                            "; DELETE  FROM primka WHERE id_primka = " + aktivna_kal
                            ;


                        MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                        mySqlCommand.ExecuteNonQuery();

                        listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;

                    }
                }
                else
                {
                    MessageBox.Show("Dopušteno je brisanje samo zadnje kreirane kalkulacije");
                }

            }
            else
            {
                MessageBox.Show("Kalkulacija je proknjizena nije moguće brisanje");
            }
        }

        private void print_kal_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysql = new MySqlConnection(Login.constring))

            {
                mysql.Open();

                string query = "UPDATE primka SET aktivan = 0 WHERE id_primka = " + aktivna_kal;

                MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                mySqlCommand.ExecuteNonQuery();
                listBox1.SetSelected(aktivna_kal - 1,true);

                MessageBox.Show("Kalkulacija uspjesno proknjižena");
            }
        }
        /*
        private void dozvoljenoupisti(string o_text)
        {
            string text = o_text;
            char[] text2 = text.ToCharArray();
         

            for (int i = 0; i < textBox8.Text.Length; i++)
            {
                char[] text_1 = textBox8.Text.ToCharArray();
                if (text_1.Length <= text2.Length)
                {
                    if (text_1[i] != text2[i])
                    {
                        textBox8.Text = "";
                    }
                }
                else
                {
                    textBox8.Text = text;
                }
            }
        }
        */
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            forcestop = true;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            using (MySqlConnection mysql = new MySqlConnection(Login.constring))
            {
                string query = "SELECT * FROM primka_stavke WHERE id_primka = " + aktivna_kal;
                MySqlDataAdapter mySqlCommand = new MySqlDataAdapter(query, mysql);
                DataSet ds = new DataSet();
                mySqlCommand.Fill(ds);

                DataTable dt = ds.Tables[0];


            
            using (Graphics g = e.Graphics)
            {

                Font FontNormal = new Font("Verdana", 15);

                g.DrawString("Kalkulacija : " + aktivna_kal, FontNormal, Brushes.Black, 0, 0, new StringFormat());
                g.DrawString("DATUM : " + DateTime.UtcNow.Date, FontNormal, Brushes.Black, 250, 0, new StringFormat());

                    for(int i = 0; i< dt.Rows.Count; i++)
                    g.DrawString(dt.Rows[i]["id"].ToString() + "  "+ dt.Rows[i]["id_artikl"].ToString() + "  " + dt.Rows[i]["NazivArtikla"].ToString() + "  " + dt.Rows[i]["kolicina"].ToString(), FontNormal, Brushes.Black, 250, 50 + (30 * (i + 1)), new StringFormat());
                }

            }

            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            forcestop = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            forcestop = true;
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            forcestop = true;
        }
    }
    
}
