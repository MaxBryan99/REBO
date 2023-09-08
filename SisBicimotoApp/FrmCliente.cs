using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmCliente : Form
    {
        public static char nmCLi = 'N';
        public static string cod = "";
        public static string tipdoc = "";
        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmCliente()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Tip. Doc.";
            Grid1.Columns[1].HeaderText = "N° Doc.";
            Grid1.Columns[2].HeaderText = "Nombre/Razón Social";
            Grid1.Columns[3].HeaderText = "Dirección";
            Grid1.Columns[4].HeaderText = "Teléfono";
            Grid1.Columns[5].HeaderText = "Estado";
            Grid1.Columns[0].Width = 80;
            Grid1.Columns[1].Width = 90;
            Grid1.Columns[2].Width = 280;
            Grid1.Columns[3].Width = 300;
            Grid1.Columns[4].Width = 110;
            Grid1.Columns[5].Width = 70;
        }

        public void CargarDatos()
        {
            int nVal = 1;
            datos = csql.dataset("Call SpClienteBusGen(" + nVal + ",'" + rucEmpresa.ToString() + "')");
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
                        datos = csql.dataset("Call SpClienteBusCodG('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "')");
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
                    datos = csql.dataset("Call SpClienteBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nmCLi = 'N';
            FrmAddCliente frmAddCliente = new FrmAddCliente();
            frmAddCliente.WindowState = FormWindowState.Normal;
            frmAddCliente.MdiParent = this.MdiParent;
            frmAddCliente.Show();
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
            nmCLi = 'M';
            if (Grid1.RowCount > 0)
            {
                tipdoc = Grid1.CurrentRow.Cells[0].Value.ToString();
                cod = Grid1.CurrentRow.Cells[1].Value.ToString();
                FrmAddCliente frmAddCliente = new FrmAddCliente();
                frmAddCliente.WindowState = FormWindowState.Normal;
                frmAddCliente.MdiParent = this.MdiParent;
                frmAddCliente.Show();
            }
            else
            {
                MessageBox.Show("No existen Clientes registrados", "SISTEMA");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }
}