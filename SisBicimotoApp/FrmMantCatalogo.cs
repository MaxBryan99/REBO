using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmMantCatalogo : Form
    {
        private DataSet datos;
        public static string codCat = "";
        public static string NomCat = "";

        public FrmMantCatalogo()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Catálogo";
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 500;
            //Grid1.Columns[0].Visible = false;
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpCatalogoBusGen()");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMantCatalogo_Load(object sender, EventArgs e)
        {
            CargarDatos();
            label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string nnombre = textBox1.Text.Trim();
            datos = csql.dataset("Call SpCatalogoBusNom('" + nnombre.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
            label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button5.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                codCat = Grid1.CurrentRow.Cells[0].Value.ToString();
                NomCat = Grid1.CurrentRow.Cells[1].Value.ToString();
                FrmAddDesCatalogo frmAddDesCatalogo = new FrmAddDesCatalogo();
                frmAddDesCatalogo.WindowState = FormWindowState.Normal;
                frmAddDesCatalogo.MdiParent = this.MdiParent;
                frmAddDesCatalogo.Show();
            }
            else
            {
                MessageBox.Show("No existen Catálogos registrados", "SISTEMA");
            }
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}