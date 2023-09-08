using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmBusPersonal : Form
    {
        public IPersonal Opener { get; set; }

        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmBusPersonal()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Nombre";
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 380;
            Grid1.Columns[2].Visible = false;
            Grid1.Columns[3].Visible = false;
            Grid1.Columns[4].Visible = false;
            Grid1.Columns[5].Visible = false;
            Grid1.Columns[6].Visible = false;
            Grid1.Columns[7].Visible = false;
            Grid1.Columns[8].Visible = false;
        }

        public void CargarDatos()
        {
            int nVal = 1;
            datos = csql.dataset("Call SpPersonalBusGen(" + nVal + ",'" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBusPersonal_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            string codPer = Grid1.CurrentRow.Cells[0].Value.ToString();
            this.Opener.SelectItem(codPer);
            this.Close();
        }

        private void Grid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string codPer = Grid1.CurrentRow.Cells[0].Value.ToString();
                this.Opener.SelectItem(codPer);
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string nnombre = textBox2.Text.Trim();
            int nVal = 1;
            datos = csql.dataset("Call SpPersonalBusNom('" + nnombre.ToString() + "'," + nVal + ",'" + rucEmpresa.ToString() + "')");
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
    }
}