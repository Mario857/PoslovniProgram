using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poslovni.Klase
{
    public class Partneri
    {
        public static List<String> IzlistajPartnere()
        {
            List<String> partnerilista = new List<string>();
            partnerilista.Clear();
            try
            {
                using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
                {


                    sqlcon.Open();
                    string query = "SELECT * FROM poslovni_partner";


                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlcon);
                    DataSet ds = new DataSet();
                    mySqlDataAdapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    for (int i = 0; i < dt.Rows.Count; i++)
                        partnerilista.Add(dt.Rows[i]["naziv"].ToString());

                    return partnerilista;
                }
            }
            catch
            {
                partnerilista.Clear();
                return partnerilista;
            }
        }
    }
}
