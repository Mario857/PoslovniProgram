using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Poslovni.Klase
{
    public struct Racun {
        public int id_racun { get; set; }
        public int id_kreirao { get; set; }
        public int id_vrsta_placanja { get; set; }
        public bool aktivan { get; set; }
        public string datum_racuna { get; set; }
        public string vrijeme { get; set; }

        public decimal prodano_mpc { get; set; }
        public decimal pdv_ukupno { get; set; }
    }
    public struct Racun_stavka {
        public int index_stavke { get; set; }
        public int id_racun { get; set; }
        public int sifra { get; set; }
        public string naziv { get; set; }
        public int kolicina { get; set; }
        public float nab_cijena { get; set; }
        public float MPC { get; set; }
        public float MPC_Popust { get; set; }
        public float MPC_Prodano { get; set; }
    }

    public enum NacinPlacanja {
        gotovina = 0,
        kartica = 1,
        gotovina_i_kartica = 2,
        dvije_kartice = 3,
    }
    public struct R1_racun {
        string naziv_partnera;
        long oib;
        string adresa;
    }
    public struct Racun_kljucno {
        decimal pdv;
        decimal iznos;
        decimal osnovica;
        decimal primljeni_iznos;
    }

    public class Racuni {
        private static List<Racun> ListRacuni { get; set; }
        private static List<Artikl> artikli_na_stanju { get; set; } // MOGUCE JE ODBRATI ARTIKLE KOJI SU NA STANJU 
        private static string godina_ref { get; set; }

        public static void PostaviGodinu(string ref_god) {
            godina_ref = ref_god;
        }

        public static int Count() {
            using (MySqlConnection mysql = new MySqlConnection(Login.constring)) {
                string query = "SELECT * FROM racuni_zaglavlje" + godina_ref;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysql);
                DataSet dataSet = new DataSet();
                dataSet.Clear();
                mySqlDataAdapter.Fill(dataSet);

                return dataSet.Tables[0].Rows.Count;
            }
        }
        public static void FillRacuniList() {
            ListRacuni = new List<Racun>();
            ListRacuni.Clear();

            using (MySqlConnection mysql = new MySqlConnection(Login.constring))
            {
                string query = "SELECT * FROM racuni_zaglavlje" + godina_ref;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysql);
                DataSet dataSet = new DataSet();
                mySqlDataAdapter.Fill(dataSet);

                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    Racun racun = new Racun {
                        id_racun = Convert.ToInt32(dataSet.Tables[0].Rows[i]["id_racun"].ToString()),
                        id_kreirao = Convert.ToInt32(dataSet.Tables[0].Rows[i]["id_kreirao"].ToString()),
                        id_vrsta_placanja = Convert.ToInt32(dataSet.Tables[0].Rows[i]["id_vrsta_placanja"].ToString()),
                        datum_racuna = dataSet.Tables[0].Rows[i]["datum_racuna"].ToString(),
                        aktivan = Convert.ToBoolean(Convert.ToInt32(dataSet.Tables[0].Rows[i]["aktivan"].ToString())) 

                        // DODATI OSTALO
                    };
                    ListRacuni.Add(racun);
                }
            }
        }
        public static List<Racun> GetRacuni() {
            return ListRacuni;
        }

        public static void Sinkroniziraj() {
            PostaviGodinu("");// will be added later on
            FillRacuniList();
        }


        public static void AddRacun()
        {
            if (ProvjeriAktivnost(Count() - 1) == true)
                return;

            Sinkroniziraj();
                using (MySqlConnection mysql = new MySqlConnection(Login.constring))
                {
                    mysql.Open();
                    string query = "INSERT INTO racuni_zaglavlje(id_racun,id_kreirao,id_vrsta_placanja,datum_racuna,aktivan,vrijeme_izrade)" + godina_ref + " VALUES('" + (Count() + 1) + "','" + Login.logid + "','" + 0 + "','" + DateTime.Today.Date.ToString("dd.M.yyyy") + "','"+1+"','" + DateTime.UtcNow.TimeOfDay.ToString() + "')";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                    mySqlCommand.ExecuteNonQuery();
                }
            Sinkroniziraj();

        }
        public static void IzbrisiTekuci() {
            Sinkroniziraj();

            if (ProvjeriAktivnost(Count() - 1) == false)
                return;

            using (MySqlConnection mysql = new MySqlConnection(Login.constring))
            {
                mysql.Open();
                string query = "DELETE FROM racuni_stavke" + godina_ref + " WHERE id_racun = " + (Count() );
                MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                mySqlCommand.ExecuteNonQuery();
            }
            using (MySqlConnection mysql = new MySqlConnection(Login.constring))
            {
                mysql.Open();
                string query = "DELETE FROM racuni_zaglavlje"+ godina_ref + " WHERE id_racun = "+(Count());
                MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                mySqlCommand.ExecuteNonQuery();
            }





            Sinkroniziraj();

        }

        public static bool ProvjeriAktivnost(int id_racun) {
            try
            {
                return ListRacuni[id_racun].aktivan;
            }
            catch {
                return false;
            }
        }
        public static void ZakljuciRacun(NacinPlacanja np) {
            using (MySqlConnection mysql = new MySqlConnection(Login.constring)) {
                mysql.Open();
                string query = "UPDATE racuni_zaglavlje" + godina_ref + " SET aktivan=0 WHERE id_racun=" + (Count());
                MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                mySqlCommand.ExecuteNonQuery();
            }
        }
        public void ZakljuciRacun(NacinPlacanja nacin_pl, Racun_kljucno racun_Kljucno)
        {
            throw new NotImplementedException();
        }

        public void ZakljuciRacun(NacinPlacanja nacin_pl,Racun_kljucno racun_Kljucno,R1_racun r1_Racun) {
            throw new NotImplementedException();
        }
    }

}
