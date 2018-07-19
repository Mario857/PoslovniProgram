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
using System.Text.RegularExpressions;
using System.Data.Linq.SqlClient;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using Microsoft.Reporting;
using Poslovni.Klase;
using ReportBuilderEntities;

namespace Poslovni
{
    public partial class PregledSkladista : Form
    {
        Klase.ArtikliAtributi ArtikliAtributi = new Klase.ArtikliAtributi();
        public PregledSkladista()
        {
            InitializeComponent();
        }

        public string QueryBuilder(string pretrazi,string atribut, string podgrupa,bool pod_enb,string robna_marka,bool rob_enb,string grupa_robe,bool grup_enb,string osobine_artikla,bool osobine_enb,string dobavljac,bool dob_enb,[Optional] bool aktivni) {
            
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("SELECT * FROM stanje_skladista WHERE ");
            sb.Append(atribut);
            sb.Append(" LIKE '%" + pretrazi + "%'");


            if (podgrupa != "" && pod_enb) {
                sb.Append(" AND podgrupa = '" + podgrupa+ "' ");
            }
            if (robna_marka != "" && rob_enb) {
                sb.Append(" AND robna_marka = '" + robna_marka + "' ");
            }
            if (grupa_robe != "" && grup_enb) {
                sb.Append(" AND vrsta = '" + grupa_robe + "' ");
            }
            if (grupa_robe != "" && osobine_enb)
            {
                sb.Append(" AND osobine_artikla = '" + osobine_artikla + "' ");
            }
            if (dobavljac != "" && dob_enb) {
                sb.Append(" AND dobavljac = '" + dobavljac + "' ");
            }
            if (aktivni == true)
            {
                sb.Append(" OR stanje <= '" + 0 + "' ");
            }
            else {
                sb.Append(" AND stanje > '" + 0 + "' ");
            }

            
            return sb.ToString();
        }
        public async void Pretraga(string pretrazi) {
            try
            {
                using (MySqlConnection mysql = new MySqlConnection(Login.constring))
                {
                    DataSet ds = new DataSet();
                    mysql.Open();

                    //string query = "SELECT * FROM stanje_skladista WHERE "+comboBox1.Text+" LIKE '%"+pretrazi+"%'" ;
                    var query = QueryBuilder(pretrazi, comboBox1.Text,textBox4.Text, textBox4.Enabled, textBox2.Text,textBox2.Enabled,textBox6.Text,textBox6.Enabled,textBox3.Text,textBox3.Enabled,textBox7.Text,textBox7.Enabled,checkBox6.Checked);
                   

                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(query, mysql);
                     await mySqlDataAdapter.FillAsync(ds);
                    PostaviIzvjestaj(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch { };
        }
        private void OnemoguciTextBoxove() {
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;

            textBox6.Enabled = false;
            textBox7.Enabled = false;

            comboBox1.SelectedItem = "naziv";
        }


        private void DodajAutoComNaTextBoxove() {
            ArtikliAtributi.Init();

            //ROBNA MARKA SET
            AutoCompleteStringCollection autoCompleteRobnaMarka = new AutoCompleteStringCollection();
           
            foreach (string s in ArtikliAtributi.GetRobnaMarka())
            {
                autoCompleteRobnaMarka.Add(s);
         
            }

            textBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox2.AutoCompleteCustomSource = autoCompleteRobnaMarka;

            //
            AutoCompleteStringCollection autoCompleteOsobineArtikla= new AutoCompleteStringCollection();

            foreach (string s in ArtikliAtributi.GetOsobineArtikla())
            {
                autoCompleteOsobineArtikla.Add(s);

            }

            textBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox3.AutoCompleteCustomSource = autoCompleteOsobineArtikla;


            AutoCompleteStringCollection autoCompletePodgrupeArtikla = new AutoCompleteStringCollection();

            foreach (string s in ArtikliAtributi.GetPodgrupa())
            {
                autoCompletePodgrupeArtikla.Add(s);

            }

            textBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox4.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox4.AutoCompleteCustomSource = autoCompletePodgrupeArtikla;


            AutoCompleteStringCollection autoCompleteVrstaRobe = new AutoCompleteStringCollection();

            foreach (string s in ArtikliAtributi.GetGrupa())
            {
                autoCompleteVrstaRobe.Add(s);

            }

            textBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox6.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox6.AutoCompleteCustomSource = autoCompleteVrstaRobe;



           



            AutoCompleteStringCollection autoCompletepartneri = new AutoCompleteStringCollection();

            foreach (string s in Klase.Partneri.IzlistajPartnere())
            {
                autoCompletepartneri.Add(s);

            }

            textBox7.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox7.AutoCompleteCustomSource = autoCompletepartneri;

        }
        private void PostaviIzvjestaj(DataSet ds) {

            if (tabControl1.SelectedIndex == 1)
            {
                DataTable dt = ds.Tables[0];

                reportViewer1.Reset();
                //          this.reportViewer1.ProcessingMode = ProcessingMode.Local;

                //          this.reportViewer1.LocalReport.ReportPath = AppDomain.CurrentDomain.BaseDirectory + "ssrsExport.rdlc";

                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource(dt.TableName, dt));


                ReportBuilder reportBuilder = new ReportBuilder();
                reportBuilder.DataSource = ds;

                reportBuilder.Page = new ReportPage();
                ReportSections reportFooter = new ReportSections();
                ReportItems reportFooterItems = new ReportItems();
                ReportTextBoxControl[] footerTxt = new ReportTextBoxControl[3];
                footerTxt[0] = new ReportTextBoxControl() { Name = "txtCopyright", ValueOrExpression = new string[] { string.Format("Copyright {0}", DateTime.Now.Year) } };
                footerTxt[1] = new ReportTextBoxControl() { Name = "ExecutionTime", ValueOrExpression = new string[] { "Izvjestaj generiran " + DateTime.Now.ToString() } };
                footerTxt[2] = new ReportTextBoxControl() { Name = "PageNumber", ValueOrExpression = new string[] { "Stranica ", ReportGlobalParameters.CurrentPageNumber, " of ", ReportGlobalParameters.TotalPages } };

                reportFooterItems.TextBoxControls = footerTxt;
                reportFooter.ReportControlItems = reportFooterItems;
                reportBuilder.Page.ReportFooter = reportFooter;

                ReportSections reportHeader = new ReportSections();
                reportHeader.Size = new ReportScale();
                reportHeader.Size.Height = 0.05;

                ReportItems reportHeaderItems = new ReportItems();
                ReportTextBoxControl[] headerTxt = new ReportTextBoxControl[1];
                headerTxt[0] = new ReportTextBoxControl() { Name = "txtReportTitle", ValueOrExpression = new string[] { "Stanje na dan : " + DateTime.Now.ToString() } };

                reportHeaderItems.TextBoxControls = headerTxt;
                reportHeader.ReportControlItems = reportHeaderItems;
                reportBuilder.Page.ReportHeader = reportHeader;

                reportViewer1.LocalReport.LoadReportDefinition(ReportEngine.GenerateReport(reportBuilder));

                reportViewer1.RefreshReport();
            }
            
        }
        private void PregledSkladista_Load(object sender, EventArgs e)
        {
            OnemoguciTextBoxove();
            DodajAutoComNaTextBoxove();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pretraga(textBox1.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox3.Enabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Enabled = checkBox5.Checked;
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            var matches = ArtikliAtributi.GetRobnaMarka().Where(p => Regex.IsMatch(p.ToString().ToUpperInvariant(), "^" + textBox2.Text.ToUpperInvariant()));
            try
            {
                textBox2.Text = matches.ToList()[0]; // BEST MATCH
            }
            catch {
                textBox2.Text = "";
                checkBox1.Checked = false;
            }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            var matches = ArtikliAtributi.GetOsobineArtikla().Where(p => Regex.IsMatch(p.ToString().ToUpperInvariant(), "^" + textBox3.Text.ToUpperInvariant()));
            try
            {
                textBox3.Text = matches.ToList()[0]; // BEST MATCH
            }
            catch

            {
                textBox3.Text = "";
                checkBox2.Checked = false;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            Pretraga(textBox1.Text);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
   
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pretraga(textBox1.Text);
        }

    }
}
