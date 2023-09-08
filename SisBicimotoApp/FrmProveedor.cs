using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmProveedor : Form
    {
        public static char nmPro = 'N';
        public static string cod = "";
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private DataSet datos;

        public FrmProveedor()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Ruc";
            Grid1.Columns[1].HeaderText = "Nombre/Razón Social";
            Grid1.Columns[2].HeaderText = "Dirección";
            Grid1.Columns[3].HeaderText = "Teléfono";
            Grid1.Columns[4].HeaderText = "Estado";
            Grid1.Columns[0].Width = 90;
            Grid1.Columns[1].Width = 300;
            Grid1.Columns[2].Width = 300;
            Grid1.Columns[3].Width = 120;
            Grid1.Columns[4].Width = 70;
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpProveedorBusGen('" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
            label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedIndex = cbBusqueda.SelectedIndex;
            if (cbBusqueda.SelectedItem == null)
            {
                CargarDatos();
            }
            else
            {
                if (selectedIndex.Equals(0))
                {
                    if (textBox1.TextLength > 0)
                    {
                        string codigo = textBox1.Text.Trim();
                        datos = csql.dataset("Call SpProveedorBusCodG('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                        label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                    }
                    else
                    {
                        CargarDatos();
                    }
                }
                if (selectedIndex.Equals(1))
                {
                    string nnombre = textBox1.Text.Trim();
                    datos = csql.dataset("Call SpProveedorBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nmPro = 'N';
            FrmAddProveedor frmAddProveedor = new FrmAddProveedor();
            frmAddProveedor.WindowState = FormWindowState.Normal;
            frmAddProveedor.MdiParent = this.MdiParent;
            frmAddProveedor.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button5.Focus();
            }
        }

        private void cbBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmPro = 'M';
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddProveedor frmAddProveedor = new FrmAddProveedor();
                frmAddProveedor.WindowState = FormWindowState.Normal;
                frmAddProveedor.MdiParent = this.MdiParent;
                frmAddProveedor.Show();
            }
            else
            {
                MessageBox.Show("No existen Proveedores registrados", "SISTEMA");
            }
        }
    }
}