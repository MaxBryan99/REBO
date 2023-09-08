using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmBusResponsable : Form
    {
        public IResponsable Opener { get; set; }

        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;

        public FrmBusResponsable()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Nombre";
            Grid1.Columns[0].Width = 80;
            Grid1.Columns[1].Width = 360;
            Grid1.Columns[2].Visible = false;
            Grid1.Columns[3].Visible = false;
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpResponsableBusGen('" + codAlmacen.ToString() + "','" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void FrmBusVendedor_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            string codVend = Grid1.CurrentRow.Cells[0].Value.ToString();
            this.Opener.SelectItemVend(codVend);
            this.Close();
        }

        private void Grid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string codVend = Grid1.CurrentRow.Cells[0].Value.ToString();
                this.Opener.SelectItemVend(codVend);
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string nnombre = textBox2.Text.Trim();
            datos = csql.dataset("Call SpVendedorBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Grid1.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}