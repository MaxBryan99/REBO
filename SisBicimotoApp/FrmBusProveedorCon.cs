using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmBusProveedorCon : Form
    {
        public IProveedor Opener { get; set; }

        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmBusProveedorCon()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "RUC";
            Grid1.Columns[1].HeaderText = "Razón Social";
            Grid1.Columns[0].Width = 90;
            Grid1.Columns[1].Width = 440;
            Grid1.Columns[2].Visible = false;
            Grid1.Columns[3].Visible = false;
            Grid1.Columns[4].Visible = false;
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpProveedorBusGen('" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void FrmBusProveedor_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            string codProv = Grid1.CurrentRow.Cells[0].Value.ToString();
            this.Opener.SelectItem(codProv);
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string nnombre = textBox2.Text.Trim();
            datos = csql.dataset("Call SpProveedorBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "')");
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

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Grid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string codProv = Grid1.CurrentRow.Cells[0].Value.ToString();
                this.Opener.SelectItem(codProv);
                this.Close();
            }
        }
    }
}