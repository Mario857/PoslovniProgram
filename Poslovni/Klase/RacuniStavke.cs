using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace Poslovni.Klase
{
    public class RacuniStavke 
    {

        public static Dictionary<string, Artikl> GetFromStanjeSkladista() {
            Dictionary<string, Artikl> temp = new Dictionary<string, Artikl>();
            temp.Clear();

            try
            {

                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                {
                    mysqlc.Open();
                    string query = "SELECT * FROM stanje_skladista";
  
                    DataSet ds = new DataSet();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);
                    mySqlDataAdapter.Fill(ds);
                    DataTable dt = ds.Tables[0]; // Select from stanje_skladista then convert that to dictionary 

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        temp.Add(dt.Rows[i]["sifra"].ToString(), new Artikl() {
                            sifra = Convert.ToInt32(dt.Rows[i]["sifra"]),
                            naziv = dt.Rows[i]["naziv"].ToString(),
                            kolicina = Convert.ToInt32(dt.Rows[i]["stanje"]),
                            MPC = Convert.ToSingle(dt.Rows[i]["MPC"]),
                            min_mpc = Convert.ToSingle(dt.Rows[i]["min_MPC"]),
                            
                        });
                        // Add more if needed these are key elements to create reports later
                    }
                }
            }
            catch (Exception) // Table might not exist in database then you cant select from anything
            {
                
            }
            return temp;//Return temp
}

        public static List<Racun_stavka> SviRacuniStavke() {


            return new List<Racun_stavka>() { };
        }
    }
}
