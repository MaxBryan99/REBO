using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmNotaCreditoSalAlm : Form
    {
        public static char nmNcv = 'N';

        public FrmNotaCreditoSalAlm()
        {
            InitializeComponent();
        }

        private void FrmNotaCreditoSalAlm_Load(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            nmNcv = 'N';
            FrmAddNotCredAlm frmAddNotCredAlm = new FrmAddNotCredAlm();
            frmAddNotCredAlm.WindowState = FormWindowState.Normal;
            //frmAddNotCredAlm.MdiParent = this.MdiParent;
            //frmAddCompra.Show();
            frmAddNotCredAlm.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}