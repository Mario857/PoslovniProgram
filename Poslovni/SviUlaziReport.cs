using Microsoft.Reporting.WinForms;
using Poslovni.Klase;
using ReportBuilderEntities;
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
    
    public partial class SviUlaziReport : Form
    {
        SinkronizacijaSkladišta sk = new SinkronizacijaSkladišta();
        public int odb_sifra { get; set; } 
        public SviUlaziReport()
        {
            InitializeComponent();
        }

        private void SviUlaziReport_Load(object sender, EventArgs e)
        {
            sk.Sinkroniziraj(prolaz_kroz_artikle.nista);
            Artikl artikl = new Artikl();
            DataSet ds = new DataSet();
            artikl.sifra = odb_sifra;

            ds.Tables.Add(sk.GeneriraniCijenik(artikl));
            PostaviIzvjestaj(ds);

            this.reportViewer1.RefreshReport();
        }
        private void PostaviIzvjestaj(DataSet ds)
        {


            DataTable dt = ds.Tables[0];

            this.reportViewer1.Reset();

            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource(dt.TableName, dt));


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





            this.reportViewer1.LocalReport.LoadReportDefinition(ReportEngine.GenerateReport(reportBuilder));

            this.reportViewer1.RefreshReport();


        }
    }
}
