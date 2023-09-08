using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmBusFamilia : Form
    {
        public IFamilia Opener { get; set; }

        private DataSet datos;

        public FrmBusFamilia()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Descripción";
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 320;
            Grid1.Columns[2].Visible = false;
        }

        public void CargarDatos()
        {
            string nCodCat = "008";
            datos = csql.dataset("Call SpDetCatalogoBusGen('" + nCodCat + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBusFamilia_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            string codFam = Grid1.CurrentRow.Cells[0].Value.ToString();
            this.Opener.SelectItem(codFam);
            this.Close();
        }

        private void Grid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string codFam = Grid1.CurrentRow.Cells[0].Value.ToString();
                this.Opener.SelectItem(codFam);
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string nCodCat = "008";
            string nnombre = textBox2.Text.Trim();
            string nVal = "1";
            datos = csql.dataset("Call SpDetCatalogoBusDes('" + nCodCat.ToString() + "','" + nnombre.ToString() + "'," + nVal + ")");
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
    }
}