using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmListaDePedidos : Form
    {
        public static string almacenSelec = "";

        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codModulo = "PED";

        public FrmListaDePedidos()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmAddPedido frmAddPedido = new FrmAddPedido();
            frmAddPedido.WindowState = FormWindowState.Normal;
            frmAddPedido.ShowDialog(this);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            FrmRevisaPedido childForm = new FrmRevisaPedido();
            childForm.WindowState = FormWindowState.Normal;
            childForm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmListaDePedidos_Load(object sender, EventArgs e)
        {
            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox3.DisplayMember = "nombre";
            comboBox3.ValueMember = "CodAlmacen";
            comboBox3.DataSource = datosAlm.Tables[0];
            comboBox3.Text = nomAlmacen.ToString();
        }
    }
}