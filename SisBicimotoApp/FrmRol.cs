using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmRol : Form
    {
        private DataSet datos;

        public static char nmRol = 'N';
        public static string vCodigo = "";

        public FrmRol()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "CÓDIGO";
            Grid1.Columns[1].HeaderText = "NOMBRE";
            Grid1.Columns[2].HeaderText = "N. CORTO";
            Grid1.Columns[3].HeaderText = "EST.";
            Grid1.Columns[0].Width = 75;
            Grid1.Columns[1].Width = 195;
            Grid1.Columns[2].Width = 90;
            Grid1.Columns[3].Width = 45;
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpRolGen()");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void FrmRol_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            nmRol = 'N';
            FrmAddRol frmAddRol = new FrmAddRol();
            frmAddRol.WindowState = FormWindowState.Normal;
            frmAddRol.MdiParent = this.MdiParent;
            frmAddRol.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nmRol = 'M';
                vCodigo = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddRol frmAddRol = new FrmAddRol();
                frmAddRol.WindowState = FormWindowState.Normal;
                frmAddRol.MdiParent = this.MdiParent;
                frmAddRol.Show();
            }
            else
            {
                MessageBox.Show("No existen Roles registrados", "SISTEMA");
            }
        }
    }
}