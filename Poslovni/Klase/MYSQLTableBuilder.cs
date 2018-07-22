using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Poslovni.Klase
{
    public class MYSQLTableBuilder
    {
        public static void CreatePrimkaTables()
        {
            using (MySqlConnection sqlcon = new MySqlConnection(Login.constring))
            {
                    sqlcon.Open();
                    
                    //Create primka stavke
                    string query1 = "CREATE TABLE IF NOT EXISTS `primka_stavke" + DateTime.UtcNow.Year + "` ( `id_primka` varchar(11) NOT NULL, `id_artikl` int(20) DEFAULT NULL, `kolicina` int(11) NOT NULL, `grupa` varchar(20) DEFAULT NULL, `pdv` int(11) DEFAULT '0', `jed_mj` VARCHAR(11) DEFAULT '0', `cijena` float(20,2) DEFAULT '0', `vrijednost` float(20,2) DEFAULT '0', `rabat` int(11) DEFAULT '0', `marza` float(20,2) DEFAULT '0', `RUC` float(20,2) DEFAULT '0', `MPC` float(20,2) DEFAULT '0', `Popust` int(11) DEFAULT '0', `MPC_Popust` float(20,2) DEFAULT '0', `NazivArtikla` varchar(30) NOT NULL, `id` int(30) NOT NULL, `unikatni_broj` int(11) NOT NULL AUTO_INCREMENT, PRIMARY KEY (`unikatni_broj`) ) ENGINE=InnoDB AUTO_INCREMENT=225 DEFAULT CHARSET=latin1";
                    MySqlCommand mySqlCommand = new MySqlCommand(query1, sqlcon);
                    mySqlCommand.ExecuteNonQuery();
  

                    //Create primka_zaglavlje
                    string query2 = "CREATE TABLE IF NOT EXISTS `primka" + DateTime.UtcNow.Year + "` ( `id_primka` varchar(11) NOT NULL, `dobavljac` varchar(20) NOT NULL, `p_kreirao` varchar(20) NOT NULL, `datum_primke` varchar(15) NOT NULL, `ukupno` int(20) DEFAULT NULL, `aktivan` bit(1) DEFAULT NULL, `temeljem` varchar(50) DEFAULT NULL, `napomena` varchar(50) DEFAULT NULL, `special_id` INT PRIMARY KEY AUTO_INCREMENT) ENGINE=InnoDB DEFAULT CHARSET=latin1";
                    MySqlCommand mySqlCommand2 = new MySqlCommand(query2, sqlcon);
                    mySqlCommand2.ExecuteNonQuery();
                
              
                    //Create kuf_zaglavlje
                    string query3 = "CREATE TABLE IF NOT EXISTS `kuf" + DateTime.UtcNow.Year + "` ( `br_kuf` int(11) NOT NULL AUTO_INCREMENT, `partner` varchar(50) NOT NULL, `napomena` varchar(50) NOT NULL, `br_u_dok` varchar(50) NOT NULL, `vrsta_r` varchar(10) NOT NULL, `datum_r` VARCHAR(20) NOT NULL, `rok_p` int(11) NOT NULL, `datum_v` VARCHAR(20) NOT NULL, `osnovica` varchar(20) NOT NULL, `porez` varchar(20) NOT NULL, `ukupno` varchar(20) NOT NULL, `poziv_na_br` varchar(100) NOT NULL, `povezujeprimku` int(35) DEFAULT NULL, PRIMARY KEY (`br_kuf`)) ENGINE=InnoDB DEFAULT CHARSET=latin1";
                    MySqlCommand mySqlCommand3 = new MySqlCommand(query3, sqlcon);
                    mySqlCommand3.ExecuteNonQuery();
                
               
            }
        }
        public static void CreateStanjeSkladista() {
            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
            {
                        mysqlc.Open();
                        String query1 = "CREATE TABLE IF NOT EXISTS stanje_skladista ( sifra INT(20) PRIMARY KEY , naziv VARCHAR(20), stanje VARCHAR(10), MPC VARCHAR(10), MPC_Popust VARCHAR(10), vrsta VARCHAR(20), podgrupa VARCHAR(20), osobine_artikla VARCHAR(20), min_MPC float(20,2), dobavljac VARCHAR(20) );";
                        MySqlCommand mySqlCommand = new MySqlCommand(query1, mysqlc);
                        mySqlCommand.ExecuteNonQuery();
             }
                
               
        }
        public static void CreateUneseniArtikli() {
            
                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                {
                    mysqlc.Open();
                    string query1 = "CREATE TABLE IF NOT EXISTS `uartikli` ( `sifra` int(11) NOT NULL, `naziv` varchar(20) NOT NULL, `barkod` varchar(20) NOT NULL, `podgrupa` varchar(20) NOT NULL, `vrsta` varchar(20) DEFAULT NULL, `porezna_grupa` varchar(100) DEFAULT NULL, `robna_marka` varchar(20) DEFAULT NULL, `osobine_artikla` varchar(20) DEFAULT NULL, `opis_artikla` varchar(50) DEFAULT NULL, PRIMARY KEY (`sifra`) ) ENGINE=InnoDB DEFAULT CHARSET=latin1";
                    MySqlCommand mySqlCommand = new MySqlCommand(query1, mysqlc);
                    mySqlCommand.ExecuteNonQuery();
                }
          
        }
        public static void CreatePoslovniPartner()
        {
            using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
            {
                mysqlc.Open();
                string query1 = "CREATE TABLE IF NOT EXISTS `poslovni_partner` ( `id_poslovni_partner` int(11) NOT NULL, `naziv` varchar(20) NOT NULL, `adresa` varchar(20) NOT NULL, `adresa_racuna` varchar(20) NOT NULL, `telefon` varchar(20) DEFAULT NULL, `telefax` varchar(20) DEFAULT NULL, `email` varchar(20) DEFAULT NULL, `korisnik_od` varchar(20) DEFAULT NULL, `oib` varchar(40) NOT NULL, `iban` varchar(20) DEFAULT NULL, PRIMARY KEY (`id_poslovni_partner`) ) ENGINE=InnoDB DEFAULT CHARSET=latin1";
                MySqlCommand mySqlCommand = new MySqlCommand(query1, mysqlc);
                mySqlCommand.ExecuteNonQuery();
            }

        }
        public static void CreateKorisnici() {
            try
            {
                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                {
                    mysqlc.Open();
                    string query1 = "CREATE TABLE IF NOT EXISTS `korisnici` ( `id` int(11) NOT NULL, `IME` varchar(50) DEFAULT NULL, `PREZIME` varchar(11) NOT NULL, `OIB` varchar(11) NOT NULL, `username` varchar(50) NOT NULL, `password` varchar(50) NOT NULL, `isadmin` bit(1) NOT NULL, PRIMARY KEY (`id`) ) ENGINE=InnoDB DEFAULT CHARSET=latin1";
                    MySqlCommand mySqlCommand = new MySqlCommand(query1, mysqlc);
                    mySqlCommand.ExecuteNonQuery();
                }
                if (Klase.Korisnici.UneseniKorisnici().Count == 0)
                {
                    using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                    {
                        mysqlc.Open();
                        string query1 = "INSERT INTO korisnici(id,IME,PREZIME,OIB,username,password,isadmin) VALUES('1','Administrator','Adm','123456789','Admin','Admin',1)";
                        MySqlCommand mySqlCommand = new MySqlCommand(query1, mysqlc);
                        mySqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex) {
                
            }
        }
    }
}
