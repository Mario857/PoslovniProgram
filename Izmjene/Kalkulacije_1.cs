using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Poslovni.Klase;

/// <summary>
/// Autor : Mario Lučki
/// Datum : 16.7.2018
/// Kalkulacije predstavljaju glavnu formu za zaprimanje robe, printanje naljepnica, unos, 
/// Projekt je startan iz hobija cilj nije zarada vec ucenje novih vjestina
/// 
/// DODATI :
/// 
/// Kompletno racunanje zaglavlja i unos zavisnih troskova kada je u pitanju dostava da se promjeni nabavna cijena artiklima
/// 
/// 
/// BUGOVI : 
/// na KUFU datum i odgoda placanja
/// Nekada dolazi do problema prilikom unosa artikla kada se izade sa edit moda ubaci se celija iznad ali ne svaki puta bug stvara neocekivani izlaz iz edita... trebalo bi rijesiti
/// </summary>

namespace Poslovni
{



    public partial class Kalkulacije : Form
    {


        public static string referenca_na_godinu = DateTime.UtcNow.Year.ToString();
        bool forcestop = false;
        bool LoadOver = false;
        public static int aktivna_kal = 1;


        public static float osnovica = 0;
        public static float pdv = 0;
        public float pdv_prosjek = 0;
        public static float ukupno = 0;
      //  Primka_zaglavlje primka_zaglavlje = new Primka_zaglavlje();
        SinkronizacijaSkladišta sk = new SinkronizacijaSkladišta(); //Neke funkcije iz ove klase bi trebalo pretvoriti u staticke da se ne mora kreirati instanca,ali radi ok

        public Kalkulacije()
        {
            InitializeComponent();
        }


        #region Racunanje podnozja 







        private void RačunajPodnozje()
        {
            int dijeli = 0;
            try
            {
                osnovica = 0;
                pdv = 0;
                pdv_prosjek = 0;
                ukupno = 0;
           

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (Convert.ToSingle(dataGridView1.Rows[i].Cells["kolicina"].Value) > 0)
                    {
                        osnovica += Convert.ToSingle(dataGridView1.Rows[i].Cells["vrijednost"].Value);
                        pdv_prosjek += Convert.ToSingle(dataGridView1.Rows[i].Cells["pdv"].Value);

                        dijeli++;
                    }
                    else if (Convert.ToSingle(dataGridView1.Rows[i].Cells["kolicina"].Value) < 0)
                    {
                            osnovica -= Convert.ToSingle(dataGridView1.Rows[i].Cells["vrijednost"].Value);
                            pdv_prosjek += Convert.ToSingle(dataGridView1.Rows[i].Cells["pdv"].Value);

                            dijeli++;
                    }
                }
                textBox3.Text = osnovica.ToString();

            }
            catch { };
            pdv_prosjek /= dijeli;

            pdv = osnovica * (pdv_prosjek / 100);
            textBox4.Text = pdv.ToString();

            ukupno = pdv + osnovica;
            textBox5.Text = ukupno.ToString();



            DataTable dt = new DataTable();
            dt.Columns.Add("nab_vrijednost");
            dt.Columns.Add("MP_vrijednost");
            dt.Columns.Add("zav_troskovi");

            DataRow dr = dt.NewRow();


            //dr["nab_vrijednost"] = String.Format("{0:#.00}", Convert.ToDecimal(osnovica));



            dr["MP_vrijednost"] = "";
            dr["zav_troskovi"] = "";


            dt.Rows.Add(dr);

            dataGridView4.DataSource = dt;
            dataGridView4.Rows[0].Cells["nab_vrijednost"].ReadOnly = true;
            dataGridView4.Rows[0].Cells["MP_vrijednost"].ReadOnly = true;

            dataGridView4.Columns["nab_vrijednost"].HeaderText = "Nabavna vrijednost";
            dataGridView4.Columns["MP_vrijednost"].HeaderText = "Maloprodajna vrijednost";
            dataGridView4.Columns["zav_troskovi"].HeaderText = "Zavisni troškovi";
            // textBox1.Text = DateTime.Parse(dt.Rows[aktivna_kal]["datum_primke"].ToString()).ToString("dd.MM.yyyy");


            SinkronizirajNaljepnice();
        }
        #endregion

        #region Sinkronizacija stavka kalkulacije
        private void SinkronizirajPrimku(int kalkulacija)
        {
            if (ProvjeriAktivnostKalkulacije(aktivna_kal) == true)
            { //Brisanje stavka samo ako je kalkulacija i dalje aktivna da ne bi doslo do brisanja ako konekcija pukne usred sto bi moglo napravi probleme kasnije
                using (MySqlConnection sqlconn = new MySqlConnection(Login.constring))
                {

                    sqlconn.Open();


                    string query = "DELETE FROM primka_stavke" + referenca_na_godinu + " WHERE id_primka = " + kalkulacija; //Brisi stavke gdje je primka 

                    MySqlCommand mySqlCommand = new MySqlCommand(query, sqlconn);
                    mySqlCommand.ExecuteNonQuery();


                    string query1 = "SELECT * FROM primka_stavke" + referenca_na_godinu + " WHERE id_primka = " + kalkulacija; // Trazi informacije o celijama ugl nazivi itd...
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query1, sqlconn);
                    DataSet ds = new DataSet();
                    ds.Clear();
                    mySqlDataAdapter.Fill(ds);

                    MySqlCommandBuilder cb = new MySqlCommandBuilder(mySqlDataAdapter);

                    mySqlDataAdapter.UpdateCommand = cb.GetUpdateCommand();

                    DataTable dt = ds.Tables[0];
                    dt.Clear();

                    //Malo drugaciji nacin izvedbe ovdje nego u drugim situacijama,ali pokazalo se brze
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {

                        try
                        {
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells["id_artikl"].Value) != 0)
                            {
                                dt.Rows.Add(new String[] { kalkulacija.ToString(), dataGridView1.Rows[i].Cells["id_artikl"].Value.ToString(), dataGridView1.Rows[i].Cells["kolicina"].Value.ToString(), dataGridView1.Rows[i].Cells["grupa"].Value.ToString(), dataGridView1.Rows[i].Cells["pdv"].Value.ToString(), dataGridView1.Rows[i].Cells["jed_mj"].Value.ToString(), dataGridView1.Rows[i].Cells["cijena"].Value.ToString(), dataGridView1.Rows[i].Cells["vrijednost"].Value.ToString(), dataGridView1.Rows[i].Cells["rabat"].Value.ToString(), dataGridView1.Rows[i].Cells["marza"].Value.ToString(), dataGridView1.Rows[i].Cells["RUC"].Value.ToString(), dataGridView1.Rows[i].Cells["MPC"].Value.ToString(), dataGridView1.Rows[i].Cells["Popust"].Value.ToString(), dataGridView1.Rows[i].Cells["MPC_Popust"].Value.ToString(), dataGridView1.Rows[i].Cells["NazivArtikla"].Value.ToString(), dataGridView1.Rows[i].Cells["id"].Value.ToString() });
                            }
                        }
                        catch { };
                    }


                    mySqlDataAdapter.Update(dt); 
                }
            }
            RačunajPodnozje();


        }
        #endregion

        #region postavi boju celije marza za vrijednosti ....
        private void Stiliziraj()
        {
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                try
                {
                    if (Convert.ToSingle(dataGridView1.Rows[i].Cells["marza"].Value) > 0)
                        dataGridView1.Rows[i].Cells["marza"].Style.BackColor = Color.YellowGreen;
                    else
                        dataGridView1.Rows[i].Cells["marza"].Style.BackColor = Color.IndianRed;
                }
                catch { };
            }
        }
        #endregion

        #region Provjera aktivnosti kalkulacije
        private bool ProvjeriAktivnostKalkulacije(int kal)
        {
            Primka_zaglavlje.SetIDPrimka(kal);
            Primka_zaglavlje.RequestFill();

            return Primka_zaglavlje.GetAktivnost();
        }
        #endregion

        #region Ucitavanje stavki kalkulacije
        private void UcitajStavke(int kalkulacija)
        {

            try
            {
                using (MySqlConnection sqlconn = new MySqlConnection(Login.constring))
                {
                    sqlconn.Open();
                    string query = "SELECT id,id_artikl,NazivArtikla,kolicina,grupa,pdv,jed_mj,cijena,vrijednost,rabat,marza,RUC,MPC,Popust,MPC_Popust FROM primka_stavke" + referenca_na_godinu + " WHERE id_primka = " + kalkulacija;
                    DataSet primka_stavke = new DataSet();
                    primka_stavke.Clear();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlconn);

                    mySqlDataAdapter.Fill(primka_stavke);
                    DataTable primka_stavke_table;
                    primka_stavke_table = primka_stavke.Tables[0];

                    dataGridView1.DataSource = primka_stavke_table;
                    dataGridView1.Show();
                    Stiliziraj();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            SinkronizirajPrimku(aktivna_kal); 

            textBox6.Text = aktivna_kal.ToString();

            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (ProvjeriAktivnostKalkulacije(aktivna_kal))
                {
                    dataGridView1.Columns["NazivArtikla"].ReadOnly = !ProvjeriAktivnostKalkulacije(aktivna_kal);
                    dataGridView1.AllowUserToDeleteRows = true;
                    pictureBox1.Image = Poslovni.Properties.Resources.if_bullet_green_84433; // Postavljanje slike
                }
                else
                {
                    //ZAKLJUCANA
                    dataGridView1.Columns[i].ReadOnly = true;
                    dataGridView1.AllowUserToDeleteRows = false;
                    pictureBox1.Image = Poslovni.Properties.Resources.if_bullet_red_84435;
                }
            }

        }
        #endregion

        #region Sinkronizacija zaglavlja kalkulacije 
        private void SinkronizirajKal()
        {
            using (MySqlConnection sqlconn = new MySqlConnection(Login.constring))
            {
                sqlconn.Open();
                string query = "SELECT * FROM primka" + referenca_na_godinu;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlconn);

                try
                {
                    DataSet ds = new DataSet();
                    ds.Clear();
                    mySqlDataAdapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    listBox1.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                        listBox1.Items.Add(dt.Rows[i]["id_primka"].ToString() + "  " + dt.Rows[i]["dobavljac"].ToString() + "    " + DateTime.Parse(dt.Rows[i]["datum_primke"].ToString()).ToString("dd.MM.yyyy"));

                }
                catch { };

            }
        }
        #endregion

        #region Zakljucavanje celija kada je kalkulacija nekativna ili kada nema vrijednosti itd...
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
        #endregion

        #region Uglavnom stiliziranje koda postavljanje celija da se prilagode neke po velicini teskta neke da ispune povrsinu, promjena njihovih naziva

        private void PrilagodiCelije()
        {


         
                dataGridView1.Columns["id"].ReadOnly = true;
           
            

            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {

                if (j == 0 || j == 1 || j == 2 || j == 4 || j == 14)
                    dataGridView1.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                else
                {
                    dataGridView1.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                dataGridView1.Columns[j].FillWeight = 1;
            }


            dataGridView1.Columns["id_artikl"].HeaderText = "Šifra";
            dataGridView1.Columns["marza"].HeaderText = "M%";
            dataGridView1.Columns["MPC_popust"].HeaderText = "MPC Popust";
            dataGridView1.Columns["kolicina"].HeaderText = "Količina";
            dataGridView1.Columns["rabat"].HeaderText = "R%";


            dataGridView1.Columns["grupa"].ReadOnly = true;
            dataGridView1.Columns["id_artikl"].ReadOnly = true;
            dataGridView1.Columns["marza"].ReadOnly = true;
            dataGridView1.Columns["RUC"].ReadOnly = true;
            dataGridView1.Columns["id"].ReadOnly = true;

            dataGridView1.Columns["vrijednost"].DefaultCellStyle.Format = "C";
            dataGridView1.Columns["cijena"].DefaultCellStyle.Format = "C";
            dataGridView1.Columns["MPC"].DefaultCellStyle.Format = "C";
            dataGridView1.Columns["MPC_popust"].DefaultCellStyle.Format = "C";
            dataGridView1.Columns["marza"].DefaultCellStyle.Format = "n1";
            dataGridView1.Columns["RUC"].DefaultCellStyle.Format = "n1";

            dataGridView1.DataError += dataGridView1_DataError;

            Stiliziraj();
        }
        #endregion

        private void dataGridView1_DataError(object sender,
    DataGridViewDataErrorEventArgs e)
        {
            // If the data source raises an exception when a cell value is 
            // commited, display an error message.
            if (e.Exception != null &&
                e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Value must be unique.");
            }
        }

        #region Postavljanje elementa zaglavlja texboxovi, partneri itd...

        private void PostaviElementeZaglavnja()
        {
            textBox8.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox8.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col.AddRange(Klase.Partneri.IzlistajPartnere().ToArray());
            textBox8.AutoCompleteCustomSource = col;



            Primka_zaglavlje.SetIDPrimka(aktivna_kal);
            Primka_zaglavlje.RequestFill();

            textBox8.Text = Primka_zaglavlje.GetDobavljac();
            textBox2.Text = Primka_zaglavlje.GetTemeljem();
            textBox7.Text = Primka_zaglavlje.GetNapomena();

        }
        #endregion

        #region Postavi naljepnice u datagrid, nazivi celija itd
        private void SinkronizirajNaljepnice()
        {


            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
            {
                string query = "SELECT id_artikl,NazivArtikla,kolicina,MPC FROM primka_stavke" + referenca_na_godinu + " WHERE id_primka=" + aktivna_kal;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);

                DataSet ds = new DataSet();
                ds.Clear();
                mySqlDataAdapter.Fill(ds);

                DataTable dt = ds.Tables[0];
                dt.Columns.Add("ZA_ISPIS", typeof(int));



                dataGridView3.DataSource = dt;

                dataGridView3.Columns["id_artikl"].ReadOnly = true;
                dataGridView3.Columns["NazivArtikla"].ReadOnly = true;
                dataGridView3.Columns["kolicina"].ReadOnly = true;
                dataGridView3.Columns["MPC"].ReadOnly = true;

                dataGridView3.Columns["id_artikl"].HeaderText = "Šifra";
                dataGridView3.Columns["NazivArtikla"].HeaderText = "Naziv artikla";
                dataGridView3.Columns["kolicina"].HeaderText = "Ulaz";
                dataGridView3.Columns["MPC"].HeaderText = "MPC";
                dataGridView3.Columns["ZA_ISPIS"].HeaderText = "Za ispis";
            }


        }
        #endregion


        #region Generiraj stanje u bazi, ovo ima odredenih problema kada se prvi puta otvori u novoj godini,ne izvrsi se potrebno dva puta pokreniti kal formu,ali sve ostalo ok
        private void GernerirajStanje()
        {
           Task task = Task.Run(() =>  sk.Sinkroniziraj(prolaz_kroz_artikle.sve_kalkulacije));
           task.Wait();
        }
        #endregion


        #region Kada se forma prvi puta otvori postavi sve sto je potrebno

        private void Kalkulacije_Load(object sender, EventArgs e)
        {
            PostaviGodinu();
            PostaviElementeZaglavnja();
            UcitajListuPrimki();
            SinkronizirajKal();
            UcitajStavke(aktivna_kal);
            ReindeksirajStavke();
            PrilagodiCelije();
            Stiliziraj();



            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            SinkronizirajNaljepnice();

            GernerirajStanje();
            PostaviBarkodove();
            LoadOver = true;
        }
        #endregion

        #region Izlistaje sve tablice u bazi ovo se ponavlja u sinkronaziaciji skladista mozda treba to ujediniti,ali ok

        public List<String> ListAllTables()
        {
            try
            {
                using (MySqlConnection mysqlConn = new MySqlConnection(Login.constring))
                {
                    string query = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = '"+ ServerPodaci.naziv_baze + "' ";
                    mysqlConn.Open();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlConn);
                    DataSet ds = new DataSet();
                    ds.Clear();
                    mySqlDataAdapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    string[] tables = new String[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        tables[i] = dt.Rows[i][2].ToString();

                    }
                    return tables.ToList();
                }
            }
            catch { return new List<string>(); }

        }
        #endregion


        #region Postavljanje godine iako ovo nije bas najbolji nacin pretrage po tablicma, malo je labavo sve to skupa,ali radi
        private void PostaviGodinu()
        {
            List<String> _godine = new List<String>();
            _godine.Clear();


            var result = ListAllTables().Where(e => (e.StartsWith("primka_stavke")));

            foreach (var word in result)
            {

                _godine.Add(word.TrimStart("primka_stavke".ToCharArray()));

            }



            try
            {
                if (_godine[_godine.Count - 1] != DateTime.Now.Year.ToString())
                {
                    _godine.Add(DateTime.Now.Year.ToString());
                }
            }
            catch
            {
                _godine.Add(DateTime.Now.Year.ToString());
            };



            comboBox1.ImeMode = ImeMode.HangulFull;
            comboBox1.Items.Clear();

            foreach (var god in _godine)
            {
                comboBox1.Items.Add(god);
            }


            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;

        }
        #endregion

        #region Zatrazi pocetni naziv prilikom editiranja, nisam siguran da li uopce ovo gdje koristnim
        string starting_state = "";

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            starting_state = "";
            try
            {
                starting_state = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch
            {

            }
        }
        #endregion

        #region Postavljanje indexa liste... ima i boljih nacina,ali ovo je ok

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
        #endregion


        #region Autocomplete na celiji, sugeriraj po nazivi iako bi mozda bilo dobro da se moze i po sifri unositi
        private void SuggestStaro(DataGridViewEditingControlShowingEventArgs e)
        {

            var source = new AutoCompleteStringCollection();
            source.AddRange(Klase.ArtikliOsnovno.GetUneseniArtikli(Klase.ArtikliOsnovno.Zahtjev.naziv).ToArray());

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
        #endregion


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            SuggestStaro(e); // Trebalo bi dodati multicolumn autocomplete,ali funkcija pritiska tipke na enter za unos stvara odredene probleme... 

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
        #region  Racunaje podnozja osnovice,pdv...
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
        #endregion


        #region Unos artikla prilikom svake izmjene u celiji trazi,unesi,racunaj,stiliziraj,sinkroniziraj
        private void Reconstruction(DataGridViewCellEventArgs e)
        {
            try
            {

                dataGridView1.Rows[e.RowIndex].Cells[1].Value = Klase.ArtikliOsnovno.GetUneseniArtikli(Klase.ArtikliOsnovno.Zahtjev.sifra)[setindextomylist(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), Klase.ArtikliOsnovno.GetUneseniArtikli(Klase.ArtikliOsnovno.Zahtjev.naziv))];
                dataGridView1.Rows[e.RowIndex].Cells[4].Value = Klase.ArtikliOsnovno.GetUneseniArtikli(Klase.ArtikliOsnovno.Zahtjev.grupa)[setindextomylist(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), Klase.ArtikliOsnovno.GetUneseniArtikli(Klase.ArtikliOsnovno.Zahtjev.naziv))];
                dataGridView1.Rows[e.RowIndex].Cells[5].Value = Klase.ArtikliOsnovno.GetStopaFromSifra(Convert.ToInt64(dataGridView1.Rows[e.RowIndex].Cells[1].Value));
                dataGridView1.Rows[e.RowIndex].Cells[6].Value = "kom";


            }
            catch(Exception ex)
            {
                dataGridView1.Rows[e.RowIndex].Cells[1].Value = 123456789;
            }
            try
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() == "")
                    dataGridView1.Rows[e.RowIndex].Cells[3].Value = "0";

            }
            catch(Exception ex) {
               
            };


            try
            {

                if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value) == 123456789)
                {
                    DialogResult dialogResult = MessageBox.Show("Unos artikla", "Stvar ne postoji u bazi\n Unesi artikl u bazu?", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        UnosArtikla unosArtikla = new UnosArtikla();
                        unosArtikla.Show();
                    }
                }
            }
            catch { };
          
            

            Izracunaj();

            ZaključajCelije();

            
            List<string> Sifre = new List<string>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells.Count >= 2 &&
                    item.Cells[1].Value != null)
                {
                    Sifre.Add(item.Cells[1].Value.ToString());
                }
            }

            try
            {
                var multipleItems = Sifre.FindAll(x => x.ToString() == dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()); // This could trow exception if no items

                if (multipleItems.Count > 1)
                {
                    MessageBox.Show("Nije dozvoljeno unjeti dva ista artikla!");

                    dataGridView1.Rows[e.RowIndex].Cells[1].Value = 123456789;

                    dataGridView1.Rows[e.RowIndex].Cells[2].Value = "";

                    ZaključajCelije();
                }
            }
            catch(Exception ex) { };

            try
            {

                if (Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value) == 123456789)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
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
            SinkronizirajPrimku(aktivna_kal);

           

            Stiliziraj();
        }
        #endregion

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            Reconstruction(e);
           
        }
        #region Ne koristi se u aplikaciji jos
        private void tabControl2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion

        #region Pritiskom na tipku enter prodi kroz stavke (pojednostavi unos) iako ovo stvara odredene probleme treba nekada ugasiti zato sto drugi elementi ne mogu pristupiti tipkovnici

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
        #endregion

        #region Ne koristi se
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {




        }
        #endregion

        #region Kada Korisnik izbrise redak reindeksiraj stavke i sinkroniziraj izmjenu
        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            ReindeksirajStavke();
            SinkronizirajPrimku(aktivna_kal);
        }
        private void UcitajListuPrimki()
        {
            ReindeksirajPrimke();

        }
        #endregion

        #region reindeksiranje stavka prilikom brisanja iako je dopusteno brisanje samo zadnje da ne dode do mjesanja kufa s drugim kal
        private void ReindeksirajPrimke()
        {
            using (MySqlConnection mysql = new MySqlConnection(Login.constring))
            {
                try
                {
                    mysql.Open();
                    string query = "SELECT * FROM primka" + referenca_na_godinu;
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysql);

                    DataSet ds = new DataSet();
                    ds.Clear();
                    mySqlDataAdapter.Fill(ds);

                    DataTable dt = ds.Tables[0];



                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string query1 = "UPDATE primka" + referenca_na_godinu + " SET id_primka = '" + (i + 1) + "' WHERE id_primka= '" + dt.Rows[i]["id_primka"].ToString() + "';";

                        MySqlCommand mySqlCommand = new MySqlCommand(query1, mysql);
                        mySqlCommand.ExecuteNonQuery();
                    }

                }
                catch (Exception exception)
                {

                    MessageBox.Show(exception.ToString());
                }
            }

        }
        #endregion

        #region otvaranje kalkulacije, unos u bazu
        private void nova_kal_Click(object sender, EventArgs e)
        {

            if (textBox8.Text != "")
            {
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;


                using (MySqlConnection mysql = new MySqlConnection(Login.constring))
                {
                    try
                    {
                        SinkronizirajKal();
                        string query1 = "SELECT * FROM primka" + referenca_na_godinu;
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query1, mysql);

                        DataSet ds = new DataSet();
                        ds.Clear();
                        mySqlDataAdapter.Fill(ds);

                        DataTable dt = ds.Tables[0];


                        int primkabr = dt.Rows.Count + 1;


                        mysql.Open();
                        string query = "INSERT INTO primka" + referenca_na_godinu + "(id_primka,dobavljac,p_kreirao,datum_primke,ukupno,aktivan,temeljem) VALUES (" + primkabr + ",'" + textBox8.Text + "','" + Login.logid + "','" + DateTime.Today + "',0,1, '" + textBox2.Text + "')";


                        MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                        mySqlCommand.ExecuteNonQuery();

                        SinkronizirajKal();
                        PostaviGodinu();
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;

                    }
                    catch { };
                }
            }

        }
        #endregion

        #region postavljanje stavka kada se promjeni odabrani 'item' na listboxu odnosno kalkulacija

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {



            try
            {
                string[] sp = listBox1.Items[listBox1.SelectedIndex].ToString().Split(' ');





                aktivna_kal = Convert.ToInt32(sp[0]);


                UcitajStavke(aktivna_kal);

                dataGridView1.Sort(dataGridView1.Columns["id"], ListSortDirection.Ascending);

                PrilagodiCelije();



                PostaviElementeZaglavnja();

                PostaviKuf();




                Primka_zaglavlje.SetIDPrimka(aktivna_kal);
                Primka_zaglavlje.RequestFill();
                textBox1.Text = Primka_zaglavlje.GetDatum();
                PostaviPartnerTextboxAktivnost();
            }
            catch { };
        }
        #endregion


        #region Postavljanje knjige ulaznih racuna
        public void PostaviKuf()
        {
            using (MySqlConnection mysqlcon = new MySqlConnection(Login.constring))
            {
                mysqlcon.Open();

                string query = "SELECT * FROM kuf" + referenca_na_godinu + " WHERE povezujeprimku = " + aktivna_kal;

                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlcon);

                DataSet ds = new DataSet();
                ds.Clear();
                mySqlDataAdapter.Fill(ds);


                dataGridView2.DataSource = ds.Tables[0];




            }
        }
        #endregion


        #region kada je kalkulacija neaktivna odnosno zaknjizena onemoguci textboxve (ne moze se mjenati partner ako je neaktivna)
        private void PostaviPartnerTextboxAktivnost()
        {
            textBox8.Enabled = ProvjeriAktivnostKalkulacije(aktivna_kal);
            textBox2.Enabled = ProvjeriAktivnostKalkulacije(aktivna_kal);
            textBox7.Enabled = ProvjeriAktivnostKalkulacije(aktivna_kal);
        }
        #endregion


        #region Spremanje kalkulacije partnera itd u bazu
        private void spremi_Click(object sender, EventArgs e)
        {
            if (ProvjeriAktivnostKalkulacije(aktivna_kal))
            {
                using (MySqlConnection mysql = new MySqlConnection(Login.constring))
                {


                    mysql.Open();

                    string query = "UPDATE primka" + referenca_na_godinu + " SET dobavljac = '" + textBox8.Text + "',temeljem  = '" + textBox2.Text + "',napomena = '" + textBox7.Text + "' WHERE id_primka = " + aktivna_kal;

                    MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);

                    mySqlCommand.ExecuteNonQuery();

                    SinkronizirajKal();




                    listBox1.SelectedIndex = Primka_zaglavlje.GetIdPrimka() - 1;

                }

            }
        }

        #endregion

        #region Ne koristi se
        private void listBox1_ValueMemberChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void listBox1_SizeChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Postavljanje boja listboxa zaknjizeno crno nezaknjizeno crveno
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

            if (ProvjeriAktivnostKalkulacije(e.Index + 1))
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Red), e.Bounds, StringFormat.GenericDefault);
            else
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);






            e.DrawFocusRectangle();
        }
        #endregion

        #region Brisanje kalkulacije
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (ProvjeriAktivnostKalkulacije(aktivna_kal))
            {
                if (aktivna_kal == Primka_zaglavlje.GetZadnjaKal())
                {

                    using (MySqlConnection mysql = new MySqlConnection(Login.constring))
                    {
                        mysql.Open();



                        string query = "" +
                            "DELETE FROM primka_stavke" + referenca_na_godinu + " WHERE id_primka = " + aktivna_kal +
                            "; DELETE  FROM primka" + referenca_na_godinu + " WHERE id_primka = " + aktivna_kal +
                            "; DELETE  FROM kuf" + referenca_na_godinu + " WHERE povezujeprimku = " + aktivna_kal
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
        #endregion


        #region Ciscenje prije knjizenja
        private void DataCleanUp()
        {
            spremi_Click(String.Empty, EventArgs.Empty);
            listBox1.SetSelected(aktivna_kal - 1, true);
            ReindeksirajStavke();
        }
        #endregion

        #region Knjizenje kalkulacije

        private void print_kal_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "Nepoznati dobavljač")
            {
                if (dataGridView1.Rows.Count <= 1)
                    MessageBox.Show("Nije dopušteno knjiziti praznu primku");
                else
                {
                    DataCleanUp();
                    using (MySqlConnection mysql = new MySqlConnection(Login.constring))

                    {
                        mysql.Open();

                        string query = "UPDATE primka" + referenca_na_godinu + " SET aktivan = 0 WHERE id_primka = " + aktivna_kal;

                        MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                        mySqlCommand.ExecuteNonQuery();
                        listBox1.SetSelected(aktivna_kal - 1, true);


                        MessageBox.Show("Kalkulacija uspjesno proknjižena");
                        sk.Sinkroniziraj(prolaz_kroz_artikle.aktivna_kalkulacija);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nepoznati dobavljač");
            }

        }
        #endregion

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            forcestop = true;


        }

 

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //
        #region Ispis kalkulacije pretvaranje u html i kreiranje web preglednika koji vrsi ispis
        private StringBuilder DataGridtoHTML(DataGridView dg)
        {
            StringBuilder strB = new StringBuilder();
            strB.AppendLine("<html><body><center>");

            strB.AppendLine("<H4>" + "KALKULACIJA : " + Primka_zaglavlje.GetIdPrimka() + "      Dobavljac : " + Primka_zaglavlje.GetDobavljac() + "      Datum : " + Primka_zaglavlje.GetDatum() + " </H4>");


            //create html & table
            strB.AppendLine("<" +
                          "table border='1' cellpadding='0' cellspacing='0'>");
            strB.AppendLine("<tr>");
            //cteate table header
            for (int i = 0; i < dg.Columns.Count; i++)
            {
                strB.AppendLine("<td align='center' valign='middle'>" +
                               dg.Columns[i].HeaderText + "</td>");
            }
            //create table body
            strB.AppendLine("<tr>");
            for (int i = 0; i < dg.Rows.Count; i++)
            {
                strB.AppendLine("<tr>");
                foreach (DataGridViewCell dgvc in dg.Rows[i].Cells)
                {
                    try
                    {
                        strB.AppendLine("<td align='center' valign='middle'>" +
                                        dgvc.Value.ToString() + "</td>");
                    }
                    catch { };
                }
                strB.AppendLine("</tr>");

            }
            //table footer & end of html file
            strB.AppendLine("</table></center></body></html>");
            return strB;
        }








        WebBrowser myWebBrowser = new WebBrowser();
        private void button3_Click(object sender, EventArgs e)
        {
            string temp_path = AppDomain.CurrentDomain.BaseDirectory + "/" + "template.html";
            //File.Create(temp_path);


            using (StreamWriter sw = new StreamWriter(temp_path))
            {

                sw.Write(DataGridtoHTML(dataGridView1));
            }


            myWebBrowser.DocumentCompleted += myWebBrowser_DocumentCompleted;
            myWebBrowser.DocumentText = System.IO.File.ReadAllText(temp_path);

            File.Delete(temp_path);
        }

        private void myWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            myWebBrowser.Print();
        }


        #endregion

        #region GeneriranjeCjenika i Ulaza
        private async void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Artikl artikl = new Artikl();

            try
            {
                artikl.sifra = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["id_artikl"].Value);
                dataGridView5.DataSource = await Task.Run(() => sk.GeneriraniCijenik(artikl));
                dataGridView6.DataSource = await Task.Run(() => sk.UlazuTablicu(artikl.sifra));
            }
            catch
            {
            };

            dataGridView1.EndEdit();
            forcestop = false;
        }

        #endregion

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            forcestop = true;
        }

        #region otvaranje knjige ulaznih racuna (prozor)
        private void OpenKUFFORM()
        {
            if (Kalkulacije.ukupno != 0)
            {
                if (listBox1.Items.Count > 0)
                {
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is KUF)
                        {
                            f.Focus();
                            return;
                        }
                    }

                    new KUF().Show();
                }
            }
            else
            {

                MessageBox.Show("Nije moguće unjeti kuf ako je nabavna cijena 0");
            }

        }
        #endregion


        private void button2_Click(object sender, EventArgs e)
        {
            OpenKUFFORM();
        }

        #region kada korisnik napusti odabir partnera ako uneseni partner odgovara nekom partneru odaberi ga ako ne odgovara izbaci nepoznati dobavljac koji onda kasnije osigurava da se ne moze knjiziti ako je nepoznati

        private void textBox8_Leave(object sender, EventArgs e)
        {
            string search = textBox8.Text;
            string result;
            try
            {
                result = Klase.Partneri.IzlistajPartnere().Single(s => s == search);
            }
            catch
            {
                result = "";
            }


            if (result == "")
            {
                textBox8.Text = "Nepoznati dobavljač";
            }
            else
            {
                textBox8.Text = result;

            }
        }
        #endregion

        #region Fokusiranje kada se pritisne tipka enter na textboxovima
        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {

                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {

                textBox7.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {

                textBox8.Focus();
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {

                textBox2.Focus();
            }
        }
        #endregion

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OpenKUFFORM();
        }


        #region Postavljanje ispisa naljepnica tipke itd...

        private void button5_Click(object sender, EventArgs e)
        {
            // POSTAVI SVE ISPISE U 0
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView3.Rows[i].Cells["ZA_ISPIS"].Value = 0;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            // POSTAVI SVE ISPISE U 1
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView3.Rows[i].Cells["ZA_ISPIS"].Value = dataGridView3.Rows[i].Cells["kolicina"].Value;
            }

        }
        #endregion

        private void dataGridView1_LostFocus(object sender, EventArgs e)
        {

        }

        #region Postavljanje reference na godinu

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LoadOver == true)
            {

                referenca_na_godinu = comboBox1.Text;
                aktivna_kal = listBox1.Items.Count - 1;
                SinkronizirajKal();

                try
                {
                    listBox1.SetSelected(listBox1.Items.Count - 1, true);
                }
                catch { }
            }
        }
        #endregion

        #region Kada se forma zatvori postavi staticne varijable u pocetne vrijednosti
        private void Kalkulacije_FormClosed(object sender, FormClosedEventArgs e)
        {
            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            aktivna_kal = -1;
        }
        #endregion


        #region Ne koristi se ili nije vazno brisanje izaziva odredene probleme

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {

            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void Kalkulacije_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }


        #endregion

        #region Napravi report kada se klikne dva puta na celiju ulaza
        private void dataGridView5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SviUlaziReport sviUlaziReport = new SviUlaziReport();
            sviUlaziReport.odb_sifra = Convert.ToInt32(dataGridView5.Rows[e.RowIndex].Cells[1].Value);
            sviUlaziReport.Show();
            sviUlaziReport.Focus();
        }
        #endregion

        #region Nacin printanja naljepnica
        public List<Artikl> artikli_za_ispis = new List<Artikl>();
        private string UbaciElemente() // Ubacuje element u body htmla
        {

            StringBuilder stringBuilder = new StringBuilder();
            int p = 0;
            foreach (Artikl art in artikli_za_ispis)
            {
                stringBuilder.AppendLine(Klase.PrinterNaljepnica.UbaciBarKod("barcode" + p, art.ispisna_pozicija, art, aktivna_kal.ToString()));
                p++;
            }
            return stringBuilder.ToString();
        }
        private string UbaciElementeSkripta() // Ubacuje elemente js u skripte za svaki kreirani element
        {
            StringBuilder stringBuilder = new StringBuilder();
            int p = 0;
            foreach (Artikl art in artikli_za_ispis)
            {
                stringBuilder.AppendLine(Klase.PrinterNaljepnica.UbaciSkriptuZaElement("barcode" + p));
                p++;
            }
            return stringBuilder.ToString();
        }

        #endregion


        #region Ispis naljepnica ovo zavrsavam upravo
        private void button4_Click(object sender, EventArgs e)
        {
            


            #region Artikli koji se printaju
            artikli_za_ispis.Clear();

            if (Convert.ToInt32(numericUpDown1.Value) > 0)
            {
                int na_poziciju = (Convert.ToInt32(numericUpDown1.Value) - 1);
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    if(!(dataGridView3.Rows[i].Cells["ZA_ISPIS"].Value is DBNull))
                    {
                        if (Convert.ToInt32(dataGridView3.Rows[i].Cells["ZA_ISPIS"].Value) < 0)
                        {
                            MessageBox.Show("Ispis nije moguć vrijednosti ispisa nisu toćne");
                            return;
                        }

                        for (int j = 0; j < Convert.ToInt32(dataGridView3.Rows[i].Cells["ZA_ISPIS"].Value); j++)
                        {
                            if ((na_poziciju % 2) == 0)
                                artikli_za_ispis.Add(new Artikl { naziv = dataGridView3.Rows[i].Cells["NazivArtikla"].Value.ToString(), sifra = Convert.ToInt64(dataGridView3.Rows[i].Cells["id_artikl"].Value), MPC = Convert.ToSingle(dataGridView3.Rows[i].Cells["MPC"].Value), ispisna_pozicija = new Point(0, (na_poziciju) * 100) });

                            else if ((na_poziciju % 2) == 1)
                                artikli_za_ispis.Add(new Artikl { naziv = dataGridView3.Rows[i].Cells["NazivArtikla"].Value.ToString(), sifra = Convert.ToInt64(dataGridView3.Rows[i].Cells["id_artikl"].Value), MPC = Convert.ToSingle(dataGridView3.Rows[i].Cells["MPC"].Value), ispisna_pozicija = new Point(320, (na_poziciju - 1) * 100) });

                            na_poziciju++;
                        }


                    }
                    else {
                        MessageBox.Show("Ispis nije moguć vrijednosti ispisa nisu toćne");
                        return;
                    }
                }

                #endregion


                UbaciElem ubaciElem = new UbaciElem(UbaciElemente); // Koristi funkcijski pointer tako da se moze direkno promjeniti na koji nacin se ispisuje u funkcijama iznad itd.
                UbaciElemSkripte ubaciElemSkripte = new UbaciElemSkripte(UbaciElementeSkripta);
                PrinterNaljepnica.KreirajTestno(ubaciElem, ubaciElemSkripte);
            }
            else {
                MessageBox.Show("Krivi unos pozicije naljepnice");
            }
        }
        private void PostaviBarkodove()
        {
            foreach (string barkod in Enum.GetNames(typeof(Barkod))){
                comboBox2.Items.Add(barkod);
            }

        }
        #endregion
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
    
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // POSTAVI SVE ISPISE U 0
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView3.Rows[i].Cells["ZA_ISPIS"].Value = 1;
            }

        }
    }

}
 