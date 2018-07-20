using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AutocompleteMenuNS;
using MySql.Data.MySqlClient;
using System.Media;
using System.Drawing;

namespace Poslovni
{
    public partial class Maloprodaja : Form
    {
        
        Klase.Racuni racuni = new Klase.Racuni();
        public Maloprodaja()
        {
            InitializeComponent();
        }
        private void SikronizirajListuRacuna(string datum) {
            racuni.Sinkroniziraj();
            listBox1.Items.Clear();

            foreach (Klase.Racun r in racuni.GetRacuni()) {
                if(r.datum_racuna == datum)
                    listBox1.Items.Add(r.id_racun);
            }

            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void PostaviImenaKorsinika()
        {
            comboBox1.Items.Clear();
            foreach (Klase.Korisnik kor in Klase.Korisnici.UneseniKorisnici())
            {
                comboBox1.Items.Add(kor.ime + " " + kor.prezime);
                if (kor.id == Login.logid)
                {
                    comboBox1.Text = kor.ime + " " + kor.prezime;
                }
            }
        }
        private void Maloprodaja_Load(object sender, EventArgs e)
        {

            SikronizirajListuRacuna(dateTimePicker1.Value.ToString("dd.M.yyyy"));
            PostaviImenaKorsinika();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //SikronizirajListuRacuna(dateTimePicker1.Value.ToString("dd.M.yyyy"));
        }

        private void dateTimePicker1_CloseUp(object sender, EventArgs e)
        {
            SikronizirajListuRacuna(dateTimePicker1.Value.ToString("dd.M.yyyy"));
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.UtcNow.Date;
            racuni.AddRacun();
            dateTimePicker1_CloseUp("", e);
        }




        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PostaviRacunaAsync(listBox1.Items[listBox1.SelectedIndex].ToString(), "");
            PostaviDizajn();
            ReindeksirajStavke();
            ZakljucajCelije();
            RacunajIznosRacuna();

        }
        private void ZakljucajCelije() {
            dataGridView1.ReadOnly = !racuni.ProvjeriAktivnost(Convert.ToInt32(listBox1.SelectedItem) - 1);

        }

        private void PostaviDizajn() {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dataGridView1.Columns["index_stavke"].HeaderText = "RB";
            dataGridView1.Columns["sifra"].HeaderText = "Šifra artikla";
            dataGridView1.Columns["sifra"].ValueType = typeof(long);
            dataGridView1.Columns["naziv"].HeaderText = "Naziv artikla";
            dataGridView1.Columns["MPC_popust"].HeaderText = "Popust";
            dataGridView1.Columns["MPC_Prodano"].HeaderText = "MPC";

            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {

                if (j == 0 || j == 3 || j == 4 || j == 5 ||j == 6)
                    dataGridView1.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                else
                {
                    dataGridView1.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                dataGridView1.Columns[j].FillWeight = 1;
            }

        }
        private async void PostaviRacunaAsync(string index_racuna,string referenca_na_godinu) {
            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
            {
                await mysqlc.OpenAsync();
                string query = "SELECT index_stavke,sifra,naziv,kolicina,MPC_Popust,MPC_Prodano FROM racuni_stavke" + referenca_na_godinu + " WHERE id_racun = "+index_racuna;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);
                DataSet dataSet = new DataSet();
                await mySqlDataAdapter.FillAsync(dataSet);

                dataGridView1.DataSource = dataSet.Tables[0];
            }
        }

        private void EditControl_Collection(DataGridViewEditingControlShowingEventArgs e) {
            var sifre = new AutoCompleteStringCollection();
            var nazivi = new AutoCompleteStringCollection();
            var StanjeSkladista = Klase.RacuniStavke.GetFromStanjeSkladista();

            var Sifre = from Dictionary<string, Artikl> dir in Klase.RacuniStavke.GetFromStanjeSkladista() select dir.Keys; //Get data from dictionary but select only keys (sifre)
            var naz = from Artikl artikl in StanjeSkladista.Values.ToList<Artikl>() select artikl.naziv; // Select nazivi from aktivni arikli

            sifre.AddRange(StanjeSkladista.Keys.ToArray()); // Dodaj sve nazive i sifre u autocomplete
            nazivi.AddRange(naz.ToArray());



            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                TextBox prodCode = e.Control as TextBox;
                if (prodCode != null)
                {
                    prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    prodCode.AutoCompleteCustomSource = sifre;
                    prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;

                }

            }
            else if  (dataGridView1.CurrentCell.ColumnIndex == 2)
                {
                    TextBox prodCode = e.Control as TextBox;
                    if (prodCode != null)
                    {
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = nazivi;
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





        private void EditControlShowing_event(object s,DataGridViewEditingControlShowingEventArgs e)
        {
            EditControl_Collection(e);
        }
        public void ReindeksirajStavke()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                //dataGridView1.Rows[i].Cells["grupa"].ReadOnly = true;
                if (i != dataGridView1.RowCount - 1)
                    dataGridView1.Rows[i].Cells[0].Value = (i + 1);
            }

        }
        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
          





            var stanjeSkladista = Klase.RacuniStavke.GetFromStanjeSkladista(); // Get dictionary from stanje skladista
            Artikl value = new Artikl(); // kreiraj instancu artikla artikl je struktura






            if (e.ColumnIndex == 1)     // if editing sifra
            {

                bool hasValue = stanjeSkladista.TryGetValue(Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), out value);
                if (hasValue)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = stanjeSkladista[Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)].naziv; // Postavi naziv tamo gdje stanje skladista zadovoljava sifru (KeyValue)
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value.ToString() == "") // Uglavnom ako je prvi unos postavi neke defualtne vrijenosti
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = 1;

                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value.ToString() == "")
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = 0;

                   
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 4].Value = stanjeSkladista[Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value)].MPC;
                }
                else
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                }

            }

            else if (e.ColumnIndex == 2)    // if editing naziv
            {

                    var prividnasifra = stanjeSkladista.Where(ar => ar.Value.naziv.ToUpper() == Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value).ToUpper()).ToList();

                    //Prividna sifra pretrazuje sifru artikla koji je unesen u drugu celiju naravno kako bi pretraga bila valjana postavljam sva slova velikim tiskanim

                    if (prividnasifra.Count > 0)
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value = prividnasifra[0].Key; //Ugl first or default

                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value.ToString() == "")
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = 1;

                        if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value.ToString() == "")
                            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 2].Value = 0;


                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex + 3].Value = stanjeSkladista[Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value)].MPC;
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = prividnasifra[0].Value.naziv;
                     }
                    else
                    {
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    }
            }

            else if (e.ColumnIndex == 3)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                }
            }
            else if (e.ColumnIndex == 4) {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                }
            }
            else if (e.ColumnIndex == 5)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = stanjeSkladista[Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value)].MPC;
                }
            }



            ReindeksirajStavke();
            if (!RowContainsEmptyValues(dataGridView1, e.RowIndex))
                 SikronizirajStavkeRacuna();

            RacunajIznosRacuna();
        }

        private bool RowContainsEmptyValues(DataGridView dataGridViewx,int rowindex) {
            try
            {
                for (int i = 0; i < dataGridViewx.ColumnCount; i++)
                {
                    if (dataGridViewx.Rows[rowindex].Cells[i].Value.ToString() == "")
                        return true;
                }
            }
            catch { return false; }
            return false; 
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if Enter is pressed

            if (keyData == Keys.Enter && dataGridView1.IsCurrentCellInEditMode == false)
            {

                // If there isn't any selected row, do nothing
                if (dataGridView1.CurrentRow == null)
                {
                    return true;
                }

                // Display first cell's value


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
                            default:
                                newColumn = dataGridView1.CurrentCell.ColumnIndex + 1;
                                break;
                        }




                    

                    dataGridView1.CurrentCell = dataGridView1.Rows[newRow].Cells[newColumn];

                }

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is ZakljuciRacun)
                {
                    f.Focus();
                    return;
                }
            }
            new ZakljuciRacun().Show();
        }

        private void NoviRacunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Button1_Click("", EventArgs.Empty);
        }
   
        private void SikronizirajStavkeRacuna()
        {

            if (racuni.ProvjeriAktivnost(Convert.ToInt32(listBox1.SelectedItem ) - 1) == true)
            { // ONLY DELETE DATA FROM TABLES IF ACTIVE OTHERWISE JUST READ
                using (MySqlConnection sqlconn = new MySqlConnection(Login.constring))
                {

                    sqlconn.Open();
                    string referenca_na_godinu = ""; // TO BE CHANGED LATER

                    string query = "DELETE  FROM racuni_stavke" + referenca_na_godinu + " WHERE id_racun = " + Convert.ToInt32(listBox1.SelectedItem);

                    MySqlCommand mySqlCommand = new MySqlCommand(query, sqlconn);
                    mySqlCommand.ExecuteNonQuery();


                    string query1 = "SELECT * FROM racuni_stavke" + referenca_na_godinu + " WHERE id_racun = " + Convert.ToInt32(listBox1.SelectedItem);
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query1, sqlconn);
                    DataSet ds = new DataSet();
                    ds.Clear();
                    mySqlDataAdapter.Fill(ds);

                    MySqlCommandBuilder cb = new MySqlCommandBuilder(mySqlDataAdapter);

                    mySqlDataAdapter.UpdateCommand = cb.GetUpdateCommand();

                    DataTable dt = ds.Tables[0];
                    dt.Clear();

                    var StanjeSkladista = Klase.RacuniStavke.GetFromStanjeSkladista();
                   
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {

                        try
                        {
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells["sifra"].Value) != 0)
                            {
                                Artikl art = new Artikl();
                                StanjeSkladista.TryGetValue(dataGridView1.Rows[i].Cells["sifra"].Value.ToString(), out art);

                                
                                dt.Rows.Add(new String[] {
                                    listBox1.SelectedItem.ToString() ,
                                    dataGridView1.Rows[i].Cells["sifra"].Value.ToString(),
                                    dataGridView1.Rows[i].Cells["naziv"].Value.ToString(),
                                    dataGridView1.Rows[i].Cells["kolicina"].Value.ToString(),
                                    art.min_mpc.ToString(),
                                    art.MPC.ToString(),
                                    dataGridView1.Rows[i].Cells["MPC_Popust"].Value.ToString(),
                                    dataGridView1.Rows[i].Cells["MPC_Prodano"].Value.ToString(),

                                    dataGridView1.Rows[i].Cells["index_stavke"].Value.ToString()
                                });


                            }


                            mySqlDataAdapter.Update(dt); //Updating the values but I only want to update if not the same
                        }
                        catch { };
                        }
                }
            }

        }

        private void dataGridView1_Sinkroniziraj(object sender, DataGridViewRowEventArgs e)
        {
            ReindeksirajStavke();
            SikronizirajStavkeRacuna();
            RacunajIznosRacuna();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }


        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
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


            if (racuni.ProvjeriAktivnost(Convert.ToInt16(listBox1.Items[e.Index].ToString()) - 1))
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Red), e.Bounds, StringFormat.GenericDefault);
            else
                e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds, StringFormat.GenericDefault);


            e.DrawFocusRectangle();
        }



        private void RacunajIznosRacuna() {
            decimal ukupno = 0;
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells["MPC_Prodano"].Value != null && dataGridView1.Rows[i].Cells["kolicina"].Value != null)
                    {
                        ukupno += (Convert.ToDecimal(dataGridView1.Rows[i].Cells["kolicina"].Value.ToString()) * Convert.ToDecimal(dataGridView1.Rows[i].Cells["MPC_Prodano"].Value.ToString()));
                    }

                }
            }
            catch {
                ukupno = 0.00M;
            }

            label1.Text = ukupno + " Kn";
        }

        private void izbrisiTekućiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? brojItemaUracunu = listBox1.Items.Count - 1;

            if (brojItemaUracunu.HasValue && dateTimePicker1.Value.Date == DateTime.UtcNow.Date)
            {
                if ((listBox1.SelectedIndex == brojItemaUracunu) && !racuni.ProvjeriAktivnost(listBox1.SelectedIndex -1)) // Dodati provjeru aktivnosti
                {
                    racuni.IzbrisiTekuci();
                    SikronizirajListuRacuna(dateTimePicker1.Value.ToString("dd.M.yyyy"));
                }
                else
                {
                    MessageBox.Show("Nije moguce brisanje");
                }
            }  
        }
    }
}
