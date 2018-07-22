using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Poslovni.Klase;
/*
    Mario Lučki 
    2.7.2018
    Povezuje bazu sa skladistem artikla 
*/


namespace Poslovni
{
    enum prolaz_kroz_artikle
    {
        sve_kalkulacije = 0,
        aktivna_kalkulacija = 1,
        nista = 2,
    }

    class SinkronizacijaSkladišta
    {
        List<int> _aktivni_artikli_sifra { get; set; }
        List<int> _uneseni_aktivni_artikli_sifra { get; set; }
        List<int> _uneseni_aktivni_artikli_sifra_zbirno { get; set; }
        List<Artikl> _uneseni_aktivni_artikli { get; set; }
        List<String> _kalkulacije_artikli_naziv { get; set; }
        List<String> _neaktivne_kalkulacije { get; set; }
        List<String> _alltables { get; set; }
        List<String> _godine { get; set; }

        public void ListAllTables()
        {
            _alltables = new List<string>();
            _alltables.Clear();
            try
            {
                using (MySqlConnection mysqlConn = new MySqlConnection(Login.constring))
                {
                    string query = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'TABLE_SCHEMA' ";
                    mysqlConn.Open();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlConn);
                    DataSet ds = new DataSet();
                    mySqlDataAdapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    string[] tables = new String[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        tables[i] = dt.Rows[i][2].ToString();

                    }
                    _alltables = tables.ToList();
                }
            }
            catch { };
        }
        private void SetGodine()
        {
            _godine = new List<String>();
            _godine.Clear();
            ListAllTables();

            var result = _alltables.Where(e => (e.StartsWith("primka_stavke")));
          
            foreach (var word in result)
            {
                _godine.Add(word.TrimStart("primka_stavke".ToCharArray()));
            }

            if (_godine.Count == 0) // This is default setting for year
                _godine.Add(DateTime.UtcNow.Year.ToString());
        }
        public List<String> GetNektivneKalkulacije()
        {
            return _neaktivne_kalkulacije;
        }
        private void PostaviNeaktivneKal()
        {
            _neaktivne_kalkulacije = new List<String>();
            _neaktivne_kalkulacije.Clear();

            for (int i = 0; i < _godine.Count; i++)
            {
                try
                {
                    using (MySqlConnection mysqlcon = new MySqlConnection(Login.constring))
                    {

                        mysqlcon.Open();

                        string query = "SELECT id_primka FROM primka" + _godine[i] + " WHERE aktivan=0";

                        DataSet ds = new DataSet();
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlcon);
                        mySqlDataAdapter.Fill(ds);

                        DataTable dt = ds.Tables[0];

                        for (int f = 0; f < dt.Rows.Count; f++)
                        {
                            _neaktivne_kalkulacije.Add(dt.Rows[f][0].ToString() + "/" + _godine[i]);
                        }

                    }

                }
                catch { }

            }
        }
        private bool CijenikPrazan() 
        {
            try
            {
                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                {
                    DataSet ds = new DataSet();
                    mysqlc.Open();
                    string query = "SELECT * FROM stanje_skladista";
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);
                    mySqlDataAdapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                        return false;
                    else
                        return true;
                }
            }
            catch
            {
                return true;
            };
        }
        private List<int> AktivnaKalSifreArtikla()
        {
            List<int> prividna_lista = new List<int>();
            prividna_lista.Clear();


            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
            {
                DataSet ds = new DataSet();
                mysqlc.Open();
                string query = "SELECT id_artikl FROM primka_stavke" + Kalkulacije.referenca_na_godinu + " WHERE id_primka=" + Kalkulacije.aktivna_kal;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);
                mySqlDataAdapter.Fill(ds);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    prividna_lista.Add(Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString()));
                }

            }
            return prividna_lista;
        }
        private void GenerirajCijenikIzAktivnihUDB(prolaz_kroz_artikle prolaz_Kroz_Artikle)
        {
            // TO BE ADDED
            if (CijenikPrazan())
            {
                if (prolaz_Kroz_Artikle == prolaz_kroz_artikle.sve_kalkulacije)
                {
                    MYSQLTableBuilder.CreateStanjeSkladista();


                   

                    for (int i = 0; i < _uneseni_aktivni_artikli_sifra_zbirno.Count; i++)
                    {
                        try
                        {


                            Artikl prividni_artikl = new Artikl();
                            prividni_artikl = Izracunaj_ulaz(_uneseni_aktivni_artikli_sifra_zbirno[i]);

                            if (prividni_artikl.sifra != 0)
                            {

                                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                                {
                                    mysqlc.Open();
                                    String query2 = "INSERT INTO stanje_skladista (sifra,naziv,stanje,MPC,MPC_Popust,vrsta,podgrupa,osobine_artikla,min_MPC,dobavljac) VALUES ('" + prividni_artikl.sifra + "','" + prividni_artikl.naziv + "','" + prividni_artikl.kolicina + "','" + prividni_artikl.MPC + "','" + prividni_artikl.popust + "','" + prividni_artikl.vrsta + "','" + prividni_artikl.podgrupa + "','" + prividni_artikl.osobine_artikla + "','" + prividni_artikl.min_mpc + "','" + prividni_artikl.dobavljac + "') ;";
                                    MySqlCommand mySqlCommand = new MySqlCommand(query2, mysqlc);
                                    mySqlCommand.ExecuteNonQuery();
                                }
                            }
                        }
                        catch { };
                    }
                }




            }
            else if (prolaz_Kroz_Artikle == prolaz_kroz_artikle.aktivna_kalkulacija)
            {

                try
                {

                    for (int i = 0; i < AktivnaKalSifreArtikla().Count; i++)
                    {



                        Artikl prividni_artikl = new Artikl();
                        prividni_artikl = Izracunaj_ulaz(AktivnaKalSifreArtikla()[i]); // Dodati ako nema na stanju racuna od trenutne kalkulacije kasnije nije presudno

                        if (prividni_artikl.sifra != 0)
                        {

                            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                            {
                                mysqlc.Open();
                                String query2 = "INSERT INTO stanje_skladista (sifra,naziv,stanje,MPC,MPC_Popust,vrsta,podgrupa,osobine_artikla,min_MPC,dobavljac) VALUES ('" + prividni_artikl.sifra + "','" + prividni_artikl.naziv + "','" + prividni_artikl.kolicina + "','" + prividni_artikl.MPC + "','" + prividni_artikl.popust + "','" + prividni_artikl.vrsta + "','" + prividni_artikl.podgrupa + "','" + prividni_artikl.osobine_artikla + "','" + prividni_artikl.min_mpc + "','" + prividni_artikl.dobavljac + "') ON DUPLICATE KEY UPDATE stanje='" + prividni_artikl.kolicina + "',MPC = '" + prividni_artikl.MPC + "' ,MPC_Popust = '" + prividni_artikl.popust + "', vrsta='" + prividni_artikl.vrsta + "',  podgrupa = '" + prividni_artikl.podgrupa + "',osobine_artikla='" + prividni_artikl.osobine_artikla + "', min_MPC='" + prividni_artikl.min_mpc + "', dobavljac='" + prividni_artikl.dobavljac + "';";



                                MySqlCommand mySqlCommand = new MySqlCommand(query2, mysqlc);
                                mySqlCommand.ExecuteNonQuery();
                            }
                        }

                    }
                }
                catch { };

            }

        }
        private void PostaviUneseneAktivneArtikle()
        {
            _uneseni_aktivni_artikli = new List<Artikl>();
            _uneseni_aktivni_artikli_sifra = new List<int>();
            _uneseni_aktivni_artikli_sifra.Clear();
            _uneseni_aktivni_artikli.Clear();

            for (int i = 0; i < _neaktivne_kalkulacije.Count; i++)
            {
                try
                {
                    using (MySqlConnection mysqlcon = new MySqlConnection(Login.constring))
                    {


                        mysqlcon.Open();

                        String[] temp = new String[2];
                        temp = _neaktivne_kalkulacije[i].Split('/');


                        string query = "SELECT id_artikl,NazivArtikla,kolicina,cijena,MPC,MPC_popust FROM primka_stavke" + temp[1] + " WHERE id_primka=" + temp[0];

                        DataSet ds = new DataSet();
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlcon);
                        mySqlDataAdapter.Fill(ds);

                        DataTable dt = ds.Tables[0];

                        for (int f = 0; f < dt.Rows.Count; f++)
                        {
                            _uneseni_aktivni_artikli_sifra.Add(Convert.ToInt32(dt.Rows[f][0].ToString()));

                            Artikl artikl = new Artikl();

                            artikl.sifra = Convert.ToInt32(dt.Rows[f][0].ToString());
                            artikl.naziv = dt.Rows[f][1].ToString();
                            artikl.kolicina = Convert.ToInt32(dt.Rows[f][2].ToString());
                            artikl.nab_cijena = Convert.ToSingle(dt.Rows[f][3].ToString());
                            artikl.MPC = Convert.ToSingle(dt.Rows[f][4].ToString());
                            artikl.popust = Convert.ToSingle(dt.Rows[f][5].ToString());
                            artikl.ulaz = _neaktivne_kalkulacije[i];
                            artikl.dobavljac = GetDobavljacFromDB(Convert.ToInt32(temp[0].ToString()), Convert.ToInt32(temp[1])).ToString();

                            //TOIMP
                            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                            {
                                DataSet dataSet2 = new DataSet();

                                string query2 = "SELECT * FROM uartikli WHERE sifra=" + artikl.sifra;
                                MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(query2, mysqlc);

                                mySqlDataAdapter2.Fill(dataSet2);
                                DataTable datatable2 = dataSet2.Tables[0];

                                artikl.vrsta = datatable2.Rows[0]["vrsta"].ToString();
                                artikl.podgrupa = datatable2.Rows[0]["podgrupa"].ToString();
                                artikl.robna_marka = datatable2.Rows[0]["robna_marka"].ToString();
                                artikl.osobine_artikla = datatable2.Rows[0]["osobine_artikla"].ToString();
                                artikl.opis_artikla = datatable2.Rows[0]["opis_artikla"].ToString();

                            }
                            _uneseni_aktivni_artikli.Add(artikl);
                        }
                    }
                }
                catch
                {

                };
            }

        }
        private String GetDobavljacFromDB(int id_primka, int godina)
        {
            String str = "";
            using (MySqlConnection mysqlcc = new MySqlConnection(Login.constring))
            {
                string query2 = "SELECT dobavljac FROM primka" + godina + " WHERE id_primka=" + id_primka;
                MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(query2, mysqlcc);
                DataSet ds2 = new DataSet();
                mySqlDataAdapter2.Fill(ds2);
                str = ds2.Tables[0].Rows[0][0].ToString();
            }
            return str;
        }
        public DataTable GeneriraniCijenik(Artikl artikl)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Ulaz", typeof(string));
            dataTable.Columns.Add("Sifra", typeof(int));
            dataTable.Columns.Add("Naziv", typeof(string));
            dataTable.Columns.Add("Kolicina", typeof(int));
            dataTable.Columns.Add("Nabavna_cijena", typeof(float));
            dataTable.Columns.Add("MPC", typeof(float));
            dataTable.Columns.Add("MPC_Popust", typeof(float));
            dataTable.Columns.Add("Dobavljac", typeof(String));
            try
            {
                for (int i = 0; i < _uneseni_aktivni_artikli.Count; i++)
                {

                    if (_uneseni_aktivni_artikli[i].sifra == artikl.sifra)
                    {

                        DataRow dataRow = dataTable.NewRow();
                        dataRow["Ulaz"] = _uneseni_aktivni_artikli[i].ulaz;
                        dataRow["Sifra"] = _uneseni_aktivni_artikli[i].sifra;
                        dataRow["Naziv"] = _uneseni_aktivni_artikli[i].naziv;
                        dataRow["Kolicina"] = _uneseni_aktivni_artikli[i].kolicina;
                        dataRow["Nabavna_cijena"] = _uneseni_aktivni_artikli[i].nab_cijena;
                        dataRow["MPC"] = _uneseni_aktivni_artikli[i].MPC;
                        dataRow["Dobavljac"] = _uneseni_aktivni_artikli[i].dobavljac;
                        dataRow["MPC_Popust"] = _uneseni_aktivni_artikli[i].popust;
                        dataTable.Rows.Add(dataRow);



                    }
                }
            }
            catch { };
            return dataTable;
        }
        private List<Artikl> UkupnoUneseniArtikli(long sifra)
        {
            return _uneseni_aktivni_artikli.Where(n => n.sifra == sifra).ToList();
        }
        public DataTable UlazuTablicu(long sifra)
        {
            DataTable dt = new DataTable();
            Artikl temop = new Artikl();
            temop = Izracunaj_ulaz(sifra);

            dt.Columns.Add("Sifra");
            dt.Columns.Add("Naziv");
            dt.Columns.Add("MPC");
            dt.Columns.Add("MPC_popust");
            dt.Columns.Add("Stanje");
            dt.Columns.Add("Dobavljac");

            DataRow toInsert = dt.NewRow();
            toInsert["Sifra"] = temop.sifra;
            toInsert["Naziv"] = temop.naziv;
            toInsert["MPC"] = temop.MPC;
            toInsert["MPC_popust"] = temop.popust;
            toInsert["Stanje"] = temop.kolicina;
            toInsert["Dobavljac"] = temop.dobavljac;

            dt.Rows.Add(toInsert);

            return dt;


        }
        public Artikl Izracunaj_ulaz(long sifra)
        {



            Artikl art = new Artikl();
            int j = 0;
    
            foreach (Artikl uneseni_art in UkupnoUneseniArtikli(sifra)) {
                try
                {
                    if (uneseni_art.sifra == sifra) {
                        if (uneseni_art.nab_cijena != 0) {


                           
                            art.sifra = uneseni_art.sifra;
                            art.naziv = uneseni_art.naziv;
                            art.dobavljac = uneseni_art.dobavljac;
                            art.MPC = uneseni_art.MPC;
                            art.popust = uneseni_art.popust;

                            art.vrsta = uneseni_art.vrsta;
                            art.podgrupa = uneseni_art.podgrupa;
                            art.opis_artikla = uneseni_art.opis_artikla;
                            art.robna_marka = uneseni_art.robna_marka;
                            art.kolicina += uneseni_art.kolicina;
                            art.nab_cijena += uneseni_art.nab_cijena;
                            art.nab_vrijednost += uneseni_art.nab_cijena * Math.Abs(uneseni_art.kolicina);

                            if (art.kolicina == 0)
                            {
                                art.nab_cijena = 0;
                                j = -1;
                            }
                            
                                j++;
                            

                        }
                        else if (uneseni_art.nab_cijena == 0)
                        {
                            art.MPC = uneseni_art.MPC;
                            art.popust = uneseni_art.popust;
                        }
                    }

                }
                catch { };

            }
            art.nab_cijena /= j;
            art.nab_vrijednost /= art.kolicina;

            var pdv_artikla = ArtikliOsnovno.GetStopaFromSifra(sifra);
             art.min_mpc = art.nab_vrijednost * (1 + ((float)pdv_artikla / 100));
            
            return art;
        }

      
        private void PostaviUnesenoAktivneArtikleZbirno()
        {
            _uneseni_aktivni_artikli_sifra_zbirno = new List<int>();
            _uneseni_aktivni_artikli_sifra_zbirno.Clear();

            _uneseni_aktivni_artikli_sifra_zbirno = _uneseni_aktivni_artikli_sifra.Distinct().ToList();

        }
        public List<int> GetUneseniAktivniArtikliSifra()
        {

            return _uneseni_aktivni_artikli_sifra;
        }
        public List<int> GetUneseniAktivniArtikliSifraZbirno()
        {
            return _uneseni_aktivni_artikli_sifra_zbirno;
        }
        public List<Artikl> GetUneseniAktivniArtikli()
        {
            return _uneseni_aktivni_artikli;
        }
        public void Sinkroniziraj(prolaz_kroz_artikle prolaz)
        {
            SetGodine();
            PostaviNeaktivneKal();
            PostaviUneseneAktivneArtikle();
            PostaviUnesenoAktivneArtikleZbirno();
            GenerirajCijenikIzAktivnihUDB(prolaz);

        }
    }
}
