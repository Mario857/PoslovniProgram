using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Poslovni
{
    class Primka_zaglavlje
    {
       static int _id_primka { get; set; }
        static string _dobavljac { get; set; }
        static string _datum { get; set; }
        static string _temeljem { get; set; }
        static string _napomena { get; set; }
        static bool _aktivna { get; set; }
        static int _zadnja_kal { get; set; }

        public static void SetIDPrimka(int id_primka)
        {
            _id_primka = id_primka;
        }
        public static void RequestFill()
        {
            try
            {
                using (MySqlConnection mysqlcon = new MySqlConnection(Login.constring))
                {
                    mysqlcon.Open();
                    string query = "SELECT * FROM primka" + Kalkulacije.referenca_na_godinu + " WHERE id_primka=" + _id_primka;
                    DataSet ds = new DataSet();
                    ds.Clear();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlcon);
                    mySqlDataAdapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    _dobavljac = dt.Rows[0]["dobavljac"].ToString();
                    _datum = DateTime.Parse(dt.Rows[0]["datum_primke"].ToString()).ToString("dd.MM.yyyy");
                    _temeljem = dt.Rows[0]["temeljem"].ToString();
                    _napomena = dt.Rows[0]["napomena"].ToString();
                    _aktivna = Convert.ToBoolean(Convert.ToInt32(dt.Rows[0]["aktivan"].ToString()));
                    _zadnja_kal = dt.Rows.Count;
                }
            }
            catch
            {
                _dobavljac = "";
                _datum = "";
                _temeljem = "";
                _napomena = "";
                _aktivna = false;
            }
        }
        public static int GetIdPrimka()
        {
            return _id_primka;
        }
        public static string GetDobavljac()
        {
            return _dobavljac;
        }
        public static string GetTemeljem()
        {
            return _temeljem;
        }
        public static string GetDatum()
        {
            return _datum;
        }
        public static string GetNapomena()
        {
            return _napomena;
        }
        public static bool GetAktivnost()
        {
            return _aktivna;
        }
        public static int GetZadnjaKal()
        {
            using (MySqlConnection mysqlcon = new MySqlConnection(Login.constring))
            {
                mysqlcon.Open();
                string query = "SELECT * FROM primka" + Kalkulacije.referenca_na_godinu;
                DataSet ds = new DataSet();
                ds.Clear();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlcon);
                mySqlDataAdapter.Fill(ds);

                DataTable dt = ds.Tables[0];
                _zadnja_kal = dt.Rows.Count;
            }
            return _zadnja_kal;
        }
    }

}
