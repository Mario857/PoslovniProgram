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
    public partial class ZakljuciRacun : Form
    {
        public ZakljuciRacun()
        {
            InitializeComponent();
        }

        private void otp_partneru_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = !otp_partneru.Checked;
            otp_partneru_naziv.Enabled = otp_partneru.Checked;
        }

        private void ZakljuciRacun_Load(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            otp_partneru_naziv.Enabled = false;
        }
    }
}
