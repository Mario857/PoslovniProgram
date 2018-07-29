using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poslovni.Klase
{
    public class Partner {
        public int id_poslovni_partner { get; set; }
        public string naziv_partnera { get; set; }
        public string oib { get; set; }
    }
    public class Partneri
    {
        public static List<Partner> IzlistajPartnere()
        {
            List<Partner> partnerilista = new List<Partner>();
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
                        partnerilista.Add(new Partner { naziv_partnera = dt.Rows[i]["naziv"].ToString(), id_poslovni_partner = Convert.ToInt32(dt.Rows[i]["id_poslovni_partner"].ToString()), oib = dt.Rows[i]["oib"].ToString() });

                    return partnerilista;
                }
            }
            catch
            {
                partnerilista.Clear();
                return partnerilista;
            }
        }
        public static string PronaciPartneraKojiPripada(string Like) {
            List<string> partnerilista = new List<string>();
            partnerilista.Clear();


            String rtstring = "";

            try
            {
                using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
                {


                    sqlcon.Open();
                    string query = "SELECT * FROM poslovni_partner WHERE naziv LIKE '%"+ Like + "%'; ";


                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, sqlcon);
                    DataSet ds = new DataSet();
                    mySqlDataAdapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    for (int i = 0; i < dt.Rows.Count; i++)
                        partnerilista.Add(dt.Rows[i]["naziv"].ToString());

                    rtstring = partnerilista[0];

                    return rtstring;
                }
            }
            catch
            {
                partnerilista.Clear();
                rtstring = IzlistajPartnere()[0].naziv_partnera;
                return rtstring;
            }
        }
    }
}
