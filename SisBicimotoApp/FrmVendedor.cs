using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmVendedor : Form
    {
        private ClsVendedor ObjVendedor = new ClsVendedor();
        public static char nmVend = 'N';
        public static string cod = "";
        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmVendedor()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Nombre";
            Grid1.Columns[2].HeaderText = "Dirección";
            Grid1.Columns[3].HeaderText = "Teléfono";
            //Grid1.Columns[4].HeaderText = "Estado";
            Grid1.Columns[0].Width = 90;
            Grid1.Columns[1].Width = 320;
            Grid1.Columns[2].Width = 320;
            Grid1.Columns[3].Width = 150;
            //Grid1.Columns[4].Width = 70;
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpVendedorBusGen('" + rucEmpresa.ToString() + "')");
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
                        datos = csql.dataset("Call SpVendedorBusCodG('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "')");
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
                    datos = csql.dataset("Call SpVendedorBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nmVend = 'N';
            FrmAddVendedor frmAddVendedor = new FrmAddVendedor();
            frmAddVendedor.WindowState = FormWindowState.Normal;
            frmAddVendedor.MdiParent = this.MdiParent;
            frmAddVendedor.Show();
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
            nmVend = 'M';
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddVendedor frmAddVendedor = new FrmAddVendedor();
                frmAddVendedor.WindowState = FormWindowState.Normal;
                frmAddVendedor.MdiParent = this.MdiParent;
                frmAddVendedor.Show();
            }
            else
            {
                MessageBox.Show("No existen Clientes registrados", "SISTEMA");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                string nVendedor = Grid1.CurrentRow.Cells[1].Value.ToString();
                if (MessageBox.Show("¿Está seguro de querer eliminar el Vendedor: " + nVendedor + "?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ObjVendedor.CodVend = cod.ToString();

                    if (ObjVendedor.Eliminar_Vend(rucEmpresa.ToString()))
                    {
                        MessageBox.Show("Vendedor eliminado", "SISTEMA");
                        CargarDatos();
                        label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar Vendedor", "SISTEMA");
                    }
                }
            }
        }
    }
}