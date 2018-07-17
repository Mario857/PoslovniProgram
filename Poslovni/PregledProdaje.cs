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
    public partial class PregledProdaje : Form
    {
        public PregledProdaje()
        {
            InitializeComponent();
        }

        private void PregledProdaje_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
