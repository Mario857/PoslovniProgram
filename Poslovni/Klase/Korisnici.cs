using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Poslovni.Klase
{
    class Korisnik {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string oib { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
    }
    class Korisnici
    {
        public static List<Korisnik> UneseniKorisnici() {
            List<Korisnik> korisniks = new List<Korisnik>();
            try
            {
             
                korisniks.Clear();
                MySqlConnection mySqlConnection = new MySqlConnection(Login.constring);
                mySqlConnection.Open();
                string query = "Select * from korisnici";
                MySqlCommand mySqlCommand = new MySqlCommand(query, mySqlConnection);
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

                while (mySqlDataReader.Read())
                {
                    int Id = mySqlDataReader.GetInt32("id");
                    string Ime = mySqlDataReader.GetString("IME");
                    string Prezime = mySqlDataReader.GetString("PREZIME");
                    string Oib = mySqlDataReader.GetString("OIB");
                    string Username = mySqlDataReader.GetString("username");
                    string Password = mySqlDataReader.GetString("password");
                    bool IsAdmin = mySqlDataReader.GetBoolean("isadmin");

                    korisniks.Add(new Korisnik { id = Id, ime = Ime, prezime = Prezime, oib = Oib, username = Username, password = Password, isAdmin = IsAdmin });
                }
            }
            catch { };
            return korisniks;
        }
    }
}
