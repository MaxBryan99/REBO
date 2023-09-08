using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmLinea : Form
    {
        private ClsTransportista ObjTransportista = new ClsTransportista();
        public static char nmLinea = 'N';
        public static string cod = "";
        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmLinea()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Nombre Linea";
            Grid1.Columns[2].HeaderText = "Familia";
            Grid1.Columns[0].Width = 90;
            Grid1.Columns[1].Width = 300;
            Grid1.Columns[2].Width = 310;
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpLineaBusGen('" + rucEmpresa.ToString() + "')");
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
                        datos = csql.dataset("Call SpLineaBusCodG('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "')");
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
                    datos = csql.dataset("Call SpLineaBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nmLinea = 'N';
            FrmAddLinea frmAddLinea = new FrmAddLinea();
            frmAddLinea.WindowState = FormWindowState.Normal;
            frmAddLinea.MdiParent = this.MdiParent;
            frmAddLinea.Show();
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
            nmLinea = 'M';
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddLinea frmAddLinea = new FrmAddLinea();
                frmAddLinea.WindowState = FormWindowState.Normal;
                frmAddLinea.MdiParent = this.MdiParent;
                frmAddLinea.Show();
            }
            else
            {
                MessageBox.Show("No existen Transportistas registrados", "SISTEMA");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                string nTransportista = Grid1.CurrentRow.Cells[1].Value.ToString();
                if (MessageBox.Show("¿Está seguro de querer eliminar el Transportista: " + nTransportista + "?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ObjTransportista.Ruc = cod.ToString();

                    if (ObjTransportista.Eliminar_Vend())
                    {
                        MessageBox.Show("Transportista eliminado", "SISTEMA");
                        CargarDatos();
                        label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar Transportista", "SISTEMA");
                    }
                }
            }*/
        }
    }
}