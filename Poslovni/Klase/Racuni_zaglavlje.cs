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
   
    public class Racuni {
        private List<Racun> listRacuni { get; set; }
        private List<Artikl> artikli_na_stanju { get; set; } // MOGUCE JE ODBRATI ARTIKLE KOJI SU NA STANJU 
        private string godina_ref { get; set; }

        public void PostaviGodinu(string ref_god) {
            godina_ref = ref_god;
        }

        public int Count() {
            using (MySqlConnection mysql = new MySqlConnection(Login.constring)) {
                string query = "SELECT * FROM racuni_zaglavlje" + godina_ref;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysql);
                DataSet dataSet = new DataSet();
                dataSet.Clear();
                mySqlDataAdapter.Fill(dataSet);

                return dataSet.Tables[0].Rows.Count;
            }
        }
        public void FillRacuniList() {
            listRacuni = new List<Racun>();
            listRacuni.Clear();

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
                        datum_racuna = dataSet.Tables[0].Rows[i]["datum_racuna"].ToString() // DODATI OSTALO
                };
                    listRacuni.Add(racun);
                }
            }
        }
        public List<Racun> GetRacuni() {
            return listRacuni;
        }

        public void Sinkroniziraj() {
            PostaviGodinu("");// will be added later on
            FillRacuniList();
        }


        public void AddRacun()
        {
            Sinkroniziraj();
            Racun zadnji_racun = new Racun();
            zadnji_racun = GetRacuni()[Count() - 1];

           
                using (MySqlConnection mysql = new MySqlConnection(Login.constring))
                {
                    mysql.Open();
                    string query = "INSERT INTO racuni_zaglavlje(id_racun,id_kreirao,id_vrsta_placanja,datum_racuna,aktivan,vrijeme_izrade)" + godina_ref + " VALUES('" + (Count() + 1) + "','" + Login.logid + "','" + 0 + "','" + DateTime.Today.Date.ToString("dd.M.yyyy") + "','"+0+"','" + DateTime.UtcNow.TimeOfDay.ToString() + "')";
                    MySqlCommand mySqlCommand = new MySqlCommand(query, mysql);
                    mySqlCommand.ExecuteNonQuery();
                }
            Sinkroniziraj();

        }
    };
}
