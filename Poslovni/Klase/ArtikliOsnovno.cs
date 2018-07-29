using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poslovni.Klase
{
    public class ArtikliOsnovno
    {
        public enum Zahtjev{
            sifra = 0,
            naziv = 1,
            grupa = 2,
            porez = 3,
            opis_artikla = 4,
        }

        public static List<Artikl> GetUneseniArtikli()
        {
            List<Artikl> artikl = new List<Artikl>();
            List<String> genericlist = new List<string>();
            genericlist.Clear();

            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
            {
                sqlcon.Open();
                string query = "SELECT * FROM uartikli";
                MySqlCommand mySqlCommand = new MySqlCommand(query, sqlcon);
                MySqlDataReader mySqlDataReader;
                mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    string opis_Artikla = "";
                    if (!mySqlDataReader.IsDBNull(8))
                    {
                        opis_Artikla = mySqlDataReader.GetString("opis_artikla");
                    }
                    

                    artikl.Add(new Artikl {sifra = mySqlDataReader.GetInt32("sifra") , naziv = mySqlDataReader.GetString("naziv"),vrsta = mySqlDataReader.GetString("vrsta"), poreza_grupa=mySqlDataReader.GetInt32("porezna_grupa"), opis_artikla = opis_Artikla });
                }
                 return artikl;
            }
          
        }

        public static int GetStopaFromSifra(long sifra)
        {
            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
            {
                sqlcon.Open();


                string query = "SELECT porezna_grupa FROM uartikli WHERE sifra="+sifra;
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query,sqlcon);

                DataSet ds = new DataSet();
                mySqlDataAdapter.Fill(ds);
                DataTable dt = ds.Tables[0];

                int i = 0;
                try
                {
                    i = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                catch{};


                string query2 = "SELECT * FROM stopa_poreza WHERE id_porez = " + i;
                MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(query2, sqlcon);
                DataSet ds2 = new DataSet();

                mySqlDataAdapter2.Fill(ds2);
                DataTable dt2 = ds2.Tables[0];
                var value = Convert.ToInt32(dt2.Rows[0]["iznos_stope"].ToString());
                return value;
            }
        }
    }
}
