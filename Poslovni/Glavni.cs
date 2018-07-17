using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poslovni
{
    public partial class Glavni : Form
    {
        public static string korisnickoime_prezime;

        public Glavni()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Korisnici korisnici = new Korisnici();
            korisnici.Show();
        }

        private void Glavni_Load(object sender, EventArgs e)
        {
            Klase.GlavnaKlasa.Glavni();
            bool isAdmin = false;


            foreach (Klase.Korisnik kor in Klase.Korisnici.UneseniKorisnici()) {
                if (kor.id == Login.logid) {
                    korisnickoime_prezime = kor.ime + " " + kor.prezime;
                    isAdmin = kor.isAdmin;
                    break;
                }
            }


            Admin.Enabled = isAdmin;
            Admin.Visible = isAdmin;



            Klase.MYSQLTableBuilder.CreatePoslovniPartner();
            Klase.MYSQLTableBuilder.CreateUneseniArtikli();
            Klase.MYSQLTableBuilder.CreatePrimkaTables();
            Klase.MYSQLTableBuilder.CreateStanjeSkladista();

            label2.Text = "SERVER : " +ServerPodaci.server+ "  KORISNIK :  " + ServerPodaci.sql_korisnik+ "  BAZA :  " + ServerPodaci.naziv_baze ;
        }

        private void Glavni_Leave(object sender, EventArgs e)
        {
            
        }

        private void Glavni_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void unosArtiklaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnosArtikla unos = new UnosArtikla();
            unos.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UnosArtikla unos = new UnosArtikla();
            unos.Show();
        }

        private void zaprimanje_Click(object sender, EventArgs e)
        {
            
            
        }

        private void kalkulacije_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is Kalkulacije)
                {
                    f.Focus();
                    return;
                }
            }

            new Kalkulacije().Show();
            
        }

        private void unosPartneraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnosPartnera unosPartnera = new UnosPartnera();
            unosPartnera.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PregledSkladista pregledSkladista = new PregledSkladista();
            pregledSkladista.Show();
        }

        private void maloprodaja_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f is Maloprodaja)
                {
                    f.Focus();
                    return;
                }
            }

            new Maloprodaja().Show();
        }

        private void pregledArtiklaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form f in Application.OpenForms) {
                if (f is PregledArtikla) {
                    f.Focus();
                    return;
                }
            }
            new PregledArtikla().Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PregledProdaje pregled_prodaje = new PregledProdaje();
            pregled_prodaje.Show();
        }
    }
}
