using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmBusCliente : Form
    {
        public ICliente Opener { get; set; }

        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmBusCliente()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Tip. Doc.";
            Grid1.Columns[1].HeaderText = "N° Doc.";
            Grid1.Columns[2].HeaderText = "Nombre";
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 80;
            Grid1.Columns[2].Width = 360;
            //Grid1.Columns[2].Visible = false;
            //Grid1.Columns[3].Visible = false;
            //Grid1.Columns[4].Visible = false;
        }

        public void CargarDatos()
        {
            int nVal = 2;
            datos = csql.dataset("Call SpClienteBusGen(" + nVal + ",'" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBusCliente_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            string tipDoc = Grid1.CurrentRow.Cells[0].Value.ToString();
            string codCli = Grid1.CurrentRow.Cells[1].Value.ToString();
            this.Opener.SelectItem(tipDoc, codCli);
            this.Close();
        }

        private void Grid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string tipDoc = Grid1.CurrentRow.Cells[0].Value.ToString();
                string codCli = Grid1.CurrentRow.Cells[1].Value.ToString();
                this.Opener.SelectItem(tipDoc, codCli);
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string nnombre = textBox2.Text.Trim();
            datos = csql.dataset("Call SpClienteBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Grid1.Focus();
            }
        }
    }
}