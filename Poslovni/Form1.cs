using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Poslovni
{
    public partial class Login : Form
    {
        public static int logid = 0;

        public static string constring = "";
       public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServerPodaci.server = textBox1.Text;
            ServerPodaci.naziv_baze = textBox2.Text;
            ServerPodaci.port_baze = textBox3.Text;
            ServerPodaci.sql_korisnik = textBox4.Text;
            ServerPodaci.sql_korsinik_lozinka = textBox5.Text;

            constring = "Server= " + ServerPodaci.server + ";Port= " + ServerPodaci.port_baze + ";Database=" + ServerPodaci.naziv_baze + ";Uid=" + ServerPodaci.sql_korisnik + ";Pwd=" + ServerPodaci.sql_korsinik_lozinka + ";Convert Zero Datetime=True;";
            Klase.MYSQLTableBuilder.CreateKorisnici();
            PostaviKorisnke();

            try
            {

                foreach (Klase.Korisnik kor in Klase.Korisnici.UneseniKorisnici())
                {
                    if (kor.username == comboBox1.Text && kor.password == password.Text)
                    {
                        MessageBox.Show("Ulogiran kao : " + kor.ime + " " + kor.prezime);
                        logid = kor.id;

                        password.Text = "";
                        this.Hide();
                        Glavni gl = new Glavni();
                        gl.Show();
                        break;
                    }
                    else if(kor.password != password.Text && kor.username != comboBox1.Text)
                    {
                        MessageBox.Show("Kriva zaporka ili korisnicko ime");
                        break;
                    }

                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            textBox1.Text ="127.0.0.1";
            textBox2.Text = "poslovniDatabase";
            textBox3.Text = "3306";
            textBox4.Text = "root";
            textBox5.Text = "";

            ServerPodaci.server = textBox1.Text;
            ServerPodaci.naziv_baze = textBox2.Text;
            ServerPodaci.port_baze = textBox3.Text;
            ServerPodaci.sql_korisnik = textBox4.Text;
            ServerPodaci.sql_korsinik_lozinka = textBox5.Text;



            constring = "Server= "+ServerPodaci.server+";Port= "+ServerPodaci.port_baze+";Database="+ ServerPodaci.naziv_baze + ";Uid="+ ServerPodaci.sql_korisnik + ";Pwd="+ ServerPodaci .sql_korsinik_lozinka+ ";Convert Zero Datetime=True;";

            Klase.MYSQLTableBuilder.CreateKorisnici();
            PostaviKorisnke();
        }
        private void PostaviKorisnke() {
            comboBox1.Items.Clear();

            foreach (Klase.Korisnik kor in Klase.Korisnici.UneseniKorisnici())
                comboBox1.Items.Add(kor.username);



           if(comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }


        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (password.Text != "")
                    button1_Click("", EventArgs.Empty);
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (comboBox1.Text != "")
                    password.Focus();
        }
    }
}
