using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmRptKardexcs : Form
    {
        public FrmRptKardexcs()
        {
            InitializeComponent();
        }

        public void PreparaReporte(CrystalDecisions.CrystalReports.Engine.ReportDocument aReporte)
        {
            crystalReportViewer1.ReportSource = aReporte;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
        }
    }
}