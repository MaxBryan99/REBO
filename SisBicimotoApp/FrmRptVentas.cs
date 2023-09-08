using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmRptVentas : Form
    {
        public FrmRptVentas()
        {
            InitializeComponent();
        }

        public void PreparaReporte(CrystalDecisions.CrystalReports.Engine.ReportDocument aReporte)
        {
            crystalReportViewer1.ReportSource = aReporte;

        }
        private void FrmRptVentas_Load(object sender, EventArgs e)
        {

        }
    }
}
