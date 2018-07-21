using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Poslovni
{
    public partial class KUF : Form
    {
        public class KUF_Postavke 
        {
            int _kalkulacija { get; set; }
            int _br_kuf { get; set; }


            string _partner { get; set; }
            string _brudokumenta { get; set; }
            string _napomena { get; set; }
            string _poziv_na_br { get; set; }
            string _dater { get; set; }
            UInt32 _rokp { get; set; }
            string _datev { get; set; }

            float _osnovica { get; set; }
            float _porez { get; set; }
            float _ukupno { get; set; }
            List<String> _vrsta_plac { get; set; }
            String _odabrani_rac { get; set; }
            public bool isFirstCreate = false;


            // SET
            public KUF_Postavke() {

            }
            public void SetOdbRac(string odb) {
                _odabrani_rac = odb;
            }
            public void SetNapomena(String napomena) {
                _napomena = napomena;
            }
            public void SetVrstaPlac(List<String> vrsta_plac) {
                _vrsta_plac = vrsta_plac;
            }
            public void SetPartner(string partner)
            {
                _partner = partner;
            }
            public void SetBrUDok(string brudokumenta)
            {
                _brudokumenta = brudokumenta;
            }
            public void SetDatumR(string dater)
            {
                _dater = dater;
            }
            public void SetRokP(UInt32 rokp)
            {
                _rokp = rokp;
            }
            public void SetDatumV(string datev)
            {
                _datev = datev;
            }
            public void SetOsnovica(float osnovica)
            {
                _osnovica = osnovica;
            }
            public void SetPorez(float porez)
            {
                _porez = porez;
            }
            public void SetUkupno(float ukupno)
            {
                _ukupno = ukupno;

            }
            public void SetPozivNaBr(string poziv_na_br)
            {
              _poziv_na_br = poziv_na_br;

            }

            public void SetKUFbr(int br_kuf)
            {
                _br_kuf = br_kuf;
            }
            public void SetKalkulacija(int kalkulacija)
            {
                
                    _kalkulacija = kalkulacija;

            }
            // GET VALUES
        
            public string GetOdbRac()
            {
                return _odabrani_rac;
            }
            public String GetNapomena() {
                return _napomena;
            }
            public List<String> GetVrstaPlac() {
                return _vrsta_plac;
            }
            public string GetPartner()
            {
               return _partner;
            }
            public string GetBrUDok()
            {
               return  _brudokumenta;
            }
            public string GetDatumR()
            {
               return _dater ;
            }
            public UInt32 GetRokP()
            {
               return _rokp;
            }
            public string GetDatumV()
            {
                return _datev;
            }
            public float GetOsnovica()
            {
                return _osnovica;
            }
            public float GetPorez()
            {
                return _porez;
            }
            public float GetUkupno()
            {
                return _ukupno;

            }
            public string GetPozivNaBr()
            {
                return _poziv_na_br;
            }

            public int GetKalkulacija()
            {
                  return  _kalkulacija;

            }
            public int GetKUFbr()
            {
                
                  return _br_kuf;
            }



            // INIT AND UPDATE TABLE
            public bool Exists(int povezuje_kal)
            {
                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                {
                    mysqlc.Open();
                    string query = "SELECT * FROM kuf" + Kalkulacije.referenca_na_godinu + " WHERE povezujeprimku =" + povezuje_kal;

                    MySqlDataAdapter mySqlCommand = new MySqlDataAdapter(query, mysqlc);
                    DataSet ds = new DataSet();
                    mySqlCommand.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            public void Init() //INIT ONLY ON FIRST RUN ELSE PULL FROM KUF
            {
                SetKalkulacija(Kalkulacije.aktivna_kal);
                SetVrstaPlac(new List<string>() { "R1", "R2" });
                if (!Exists(GetKalkulacija()))
                {

                    using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                    {
                        mysqlc.Open();

                        string query = "SELECT * FROM primka" + Kalkulacije.referenca_na_godinu + " WHERE id_primka=" + GetKalkulacija();

                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);
                        DataSet ds = new DataSet();
                        mySqlDataAdapter.Fill(ds);


                        SetBrUDok(ds.Tables[0].Rows[0]["temeljem"].ToString());
                        SetPartner(ds.Tables[0].Rows[0]["dobavljac"].ToString());
                        SetDatumR(ds.Tables[0].Rows[0]["datum_primke"].ToString());
                         
                        SetDatumV(ds.Tables[0].Rows[0]["datum_primke"].ToString());

                        SetRokP(0);
                        
                        SetOdbRac(GetVrstaPlac()[0]);
                        SetOsnovica(Kalkulacije.osnovica);
                        SetPorez(Kalkulacije.pdv);
                        SetUkupno(Kalkulacije.ukupno);
                        SetNapomena("");
                        SetPozivNaBr("");
                        
                        string query1 = "INSERT INTO kuf" + Kalkulacije.referenca_na_godinu + "(partner,napomena,br_u_dok,vrsta_r,datum_r,rok_p,datum_v,osnovica,porez,ukupno,poziv_na_br,povezujeprimku) VALUES ('" + GetPartner() + "','" + GetNapomena() + "','" + GetBrUDok() + "','" + GetVrstaPlac()[0] + "','"+GetDatumR() + "','" + GetRokP()+ "','" + GetDatumV() + "','" + GetOsnovica()+ "','" + GetPorez() + "','" + GetUkupno() + "','" + GetPozivNaBr()+ "'," + GetKalkulacija() + ")";

                        MySqlCommand mySqlCommand = new MySqlCommand(query1, mysqlc);
                        mySqlCommand.ExecuteNonQuery();

                        string query2 = "SELECT * FROM kuf" + Kalkulacije.referenca_na_godinu + " WHERE povezujeprimku=" + GetKalkulacija();
                        MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(query2, mysqlc);
                        DataSet ds2 = new DataSet();
                        mySqlDataAdapter2.Fill(ds2);

                        SetKUFbr(Convert.ToInt32(ds2.Tables[0].Rows[0]["br_kuf"].ToString()));
                        isFirstCreate = true;
                    }
                    
                }
                else {
                    using (MySqlConnection mysqlc = new MySqlConnection(Login.constring)) {

                        string query2 = "SELECT * FROM kuf" + Kalkulacije.referenca_na_godinu + " WHERE povezujeprimku=" + GetKalkulacija();
                        MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter(query2, mysqlc);
                        DataSet ds2 = new DataSet();
                        mySqlDataAdapter2.Fill(ds2);

                        SetKUFbr(Convert.ToInt32(ds2.Tables[0].Rows[0]["br_kuf"].ToString()));
                        SetPartner(ds2.Tables[0].Rows[0]["partner"].ToString());
                        SetNapomena(ds2.Tables[0].Rows[0]["napomena"].ToString());
                        SetBrUDok(ds2.Tables[0].Rows[0]["br_u_dok"].ToString());
                        SetOdbRac(ds2.Tables[0].Rows[0]["vrsta_r"].ToString());

                        SetDatumR(ds2.Tables[0].Rows[0]["datum_r"].ToString());
                        SetDatumV(ds2.Tables[0].Rows[0]["datum_v"].ToString());


                        SetOsnovica(Convert.ToSingle(ds2.Tables[0].Rows[0]["osnovica"].ToString()));
                        SetPorez(Convert.ToSingle(ds2.Tables[0].Rows[0]["porez"].ToString()));
                        SetUkupno(Convert.ToSingle(ds2.Tables[0].Rows[0]["ukupno"].ToString()));
                        SetPozivNaBr(ds2.Tables[0].Rows[0]["poziv_na_br"].ToString());
                        isFirstCreate = false;
                    }
                    
                   
                }

            
            }
            public void UpdateKUF()
            { 
                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                {
                    mysqlc.Open();


                    string query = "UPDATE kuf"+Kalkulacije.referenca_na_godinu+" SET partner='"+GetPartner()+ "', napomena='" + GetNapomena() + "', br_u_dok='" + GetBrUDok() + "', vrsta_r='" +GetOdbRac()+ "', datum_r='" + GetDatumR() + "', rok_p='" + GetRokP() + "', datum_v='" + GetDatumV() + "',osnovica='" + GetOsnovica() + "', porez='" + GetPorez() + "',ukupno='" + GetUkupno()+ "', poziv_na_br='" + GetPozivNaBr() + "' WHERE povezujeprimku= " + GetKalkulacija();

                    MySqlCommand mySqlCommand = new MySqlCommand(query, mysqlc);

                    mySqlCommand.ExecuteNonQuery();

           
                }
            }
            public void GetChanges() {
                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring))
                {
                    mysqlc.Open();

                    string query = "SELECT * FROM primka" + Kalkulacije.referenca_na_godinu + " WHERE id_primka=" + GetKalkulacija();

                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysqlc);
                    DataSet ds = new DataSet();
                    mySqlDataAdapter.Fill(ds);
                    SetBrUDok(ds.Tables[0].Rows[0]["temeljem"].ToString());

                    SetPartner(ds.Tables[0].Rows[0]["dobavljac"].ToString());
                    SetDatumR(ds.Tables[0].Rows[0]["datum_primke"].ToString());
                    SetDatumV(ds.Tables[0].Rows[0]["datum_primke"].ToString());
                    SetOsnovica(Kalkulacije.osnovica);
                    SetPorez(Kalkulacije.pdv);
                    SetUkupno(Kalkulacije.ukupno);
                }
            }

        }
        KUF_Postavke kuf_Postavke = new KUF_Postavke();

        public KUF()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (kuf_Postavke.isFirstCreate == false)
            {
                this.Close();
            }
            else {
                using (MySqlConnection mysqlc = new MySqlConnection(Login.constring)) {
                    mysqlc.Open();
                    string query = "DELETE FROM kuf" + Kalkulacije.referenca_na_godinu + " WHERE povezujeprimku=" + kuf_Postavke.GetKalkulacija();
                    MySqlCommand mySqlCommand = new MySqlCommand(query, mysqlc);
                    mySqlCommand.ExecuteNonQuery();

                    this.Close();
                }
            }
        }

        private void SetDataToTextBox()
        {
            kuf_Postavke.Init();
        
           
            this.textBox4.Text = kuf_Postavke.GetBrUDok().ToString(); 
            this.textBox2.Text = kuf_Postavke.GetPartner().ToString();
            this.textBox3.Text = kuf_Postavke.GetKalkulacija().ToString();

            this.textBox1.Text = kuf_Postavke.GetKUFbr().ToString();
            this.textBox13.Text = kuf_Postavke.GetNapomena().ToString();
            this.textBox6.Text = kuf_Postavke.GetPozivNaBr().ToString();



            this.textBox10.Text = kuf_Postavke.GetOsnovica().ToString();
            this.textBox11.Text = kuf_Postavke.GetPorez().ToString();
            this.textBox12.Text = kuf_Postavke.GetUkupno().ToString();


            // SET VRSTA RAČUNA
            comboBox1.Items.Clear();
            foreach (String s in kuf_Postavke.GetVrstaPlac()) {
                comboBox1.Items.Add(s);
            }
            this.comboBox1.Text = kuf_Postavke.GetOdbRac().ToString();



            datumRacuna.Value =DateTime.Parse(kuf_Postavke.GetDatumR()).Date;
            datumValute.Value =DateTime.Parse(kuf_Postavke.GetDatumV()).Date;
        }

        private void KUF_Load(object sender, EventArgs e)
        {
            SetDataToTextBox();

            textBox2.Focus();
            
        }

     
       

        private void button1_Click(object sender, EventArgs e)
        {
            kuf_Postavke.SetPartner(textBox2.Text);
            kuf_Postavke.SetNapomena(textBox13.Text);
            kuf_Postavke.SetBrUDok(textBox4.Text);
            kuf_Postavke.SetOdbRac(comboBox1.Text);

            kuf_Postavke.SetDatumR(datumRacuna.Value.Date.ToString());
            kuf_Postavke.SetDatumV(datumValute.Value.Date.ToString());

            kuf_Postavke.SetOsnovica(Convert.ToSingle(textBox10.Text));
            kuf_Postavke.SetPorez(Convert.ToSingle(textBox11.Text));
            kuf_Postavke.SetUkupno(Convert.ToSingle(textBox12.Text));
            kuf_Postavke.SetPozivNaBr(textBox6.Text);
            kuf_Postavke.UpdateKUF();

            var KALFORM = Application.OpenForms.OfType<Kalkulacije>().Single();
            KALFORM.PostaviKuf();


            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox13.Focus();
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox4.Focus();
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                comboBox1.Focus();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                datumRacuna.Focus();
                
            }
        }
 
        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                datumValute.Focus();
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox11.Focus();
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox5.Focus();
        }

        private void textBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox6.Focus();
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                textBox12.Focus();
        }

        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            
            
            
        }

        private void maskedTextBox2_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_Validating(object sender, CancelEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            kuf_Postavke.GetChanges();


            this.textBox4.Text = kuf_Postavke.GetBrUDok().ToString();
            this.textBox2.Text = kuf_Postavke.GetPartner().ToString();
            this.textBox3.Text = kuf_Postavke.GetKalkulacija().ToString();
            this.textBox10.Text = kuf_Postavke.GetOsnovica().ToString();
            this.textBox11.Text = kuf_Postavke.GetPorez().ToString();
            this.textBox12.Text = kuf_Postavke.GetUkupno().ToString();
        }
    }
} 
 