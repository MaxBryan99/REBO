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
    public partial class FrmRptArticulos : Form
    {
        public FrmRptArticulos()
        {
            InitializeComponent();
        }

        public void PreparaReporte(CrystalDecisions.CrystalReports.Engine.ReportDocument aReporte)
        {
            crystalReportViewer1.ReportSource = aReporte;

        }

        private void FrmRptArticulos_Load(object sender, EventArgs e)
        {

        }
    }
}
