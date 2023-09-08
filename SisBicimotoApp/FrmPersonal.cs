using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmPersonal : Form
    {
        public static char nmPer = 'N';
        public static string cod = "";
        private DataSet datos;
        private int nVal;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmPersonal()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Nombre";
            Grid1.Columns[2].HeaderText = "Tipo Doc.";
            Grid1.Columns[3].HeaderText = "Nro. Doc.";
            Grid1.Columns[4].HeaderText = "Dirección";
            Grid1.Columns[5].HeaderText = "Teléfono";
            Grid1.Columns[6].HeaderText = "Cargo";
            Grid1.Columns[7].HeaderText = "Área";
            Grid1.Columns[8].HeaderText = "Estado";
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 200;
            Grid1.Columns[2].Width = 60;
            Grid1.Columns[3].Width = 60;
            Grid1.Columns[4].Width = 180;
            Grid1.Columns[5].Width = 90;
            Grid1.Columns[6].Width = 100;
            Grid1.Columns[7].Width = 100;
            Grid1.Columns[8].Width = 98;
            //Grid1.Columns[0].Visible = false;
        }

        public void CargarDatos()
        {
            nVal = checkBox1.Checked == true ? 0 : 1;
            datos = csql.dataset("Call SpPersonalBusGen(" + nVal + ",'" + rucEmpresa.ToString() + "')");
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
                        nVal = checkBox1.Checked == true ? 0 : 1;
                        datos = csql.dataset("Call SpPersonalBusCodG('" + codigo.ToString() + "'," + nVal + ",'" + rucEmpresa.ToString() + "')");
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
                    nVal = checkBox1.Checked == true ? 0 : 1;
                    datos = csql.dataset("Call SpPersonalBusNom('" + nnombre.ToString() + "'," + nVal + ",'" + rucEmpresa.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nmPer = 'N';
            FrmAddPersonal frmAddPersonal = new FrmAddPersonal();
            frmAddPersonal.WindowState = FormWindowState.Normal;
            frmAddPersonal.MdiParent = this.MdiParent;
            frmAddPersonal.Show();
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
            nmPer = 'M';
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddPersonal frmAddPersonal = new FrmAddPersonal();
                frmAddPersonal.WindowState = FormWindowState.Normal;
                frmAddPersonal.MdiParent = this.MdiParent;
                frmAddPersonal.Show();
            }
            else
            {
                MessageBox.Show("No existe Personal registrado", "SISTEMA");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            nVal = checkBox1.Checked == true ? 0 : 1;
            datos = csql.dataset("Call SpPersonalBusGen(" + nVal + ",'" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
            label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}