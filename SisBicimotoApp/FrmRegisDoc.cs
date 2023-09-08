using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmRegisDoc : Form
    {
        ClsDocumento ObjDocumento = new ClsDocumento();
        public static char nmDoc = 'N';
        public static string cod = "";
        string rucEmpresa = FrmLogin.x_RucEmpresa;
        DataSet datos;
        int nVal;
        public FrmRegisDoc()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Documento";
            Grid1.Columns[2].HeaderText = "Nombre Corto";
            Grid1.Columns[3].HeaderText = "Módulo";
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 250;
            Grid1.Columns[2].Width = 120;
            Grid1.Columns[3].Width = 85;
        }

        public void CargarDatos()
        {
            nVal = checkBox1.Checked == true ? 0 : 1;
            datos = csql.dataset("Call SpDocBusGen(" + nVal + ")");         
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void FrmRegisDoc_Load(object sender, EventArgs e)
        {
            CargarDatos();
            label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nmDoc = 'N';
            FrmAddDocum frmAddProveedor = new FrmAddDocum();
            frmAddProveedor.WindowState = FormWindowState.Normal;
            frmAddProveedor.MdiParent = this.MdiParent;
            frmAddProveedor.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmDoc = 'M';
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddDocum frmAddProveedor = new FrmAddDocum();
            frmAddProveedor.WindowState = FormWindowState.Normal;
            frmAddProveedor.MdiParent = this.MdiParent;
            frmAddProveedor.Show();
            }
            else
            {
                MessageBox.Show("No existe Serie registrada", "SISTEMA");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            nmDoc = 'M';
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddDocSerie frmAddProveedor = new FrmAddDocSerie();
            frmAddProveedor.WindowState = FormWindowState.Normal;
            frmAddProveedor.MdiParent = this.MdiParent;
            frmAddProveedor.Show();
            }
            else
            {
                MessageBox.Show("No existe Serie registrada", "SISTEMA");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            nmDoc = 'N';
            FrmAddDocum frmAddDocum = new FrmAddDocum();
            //frmAddProveedor.WindowState = FormWindowState.Normal;
            //frmAddProveedor.MdiParent = this.MdiParent;
            frmAddDocum.ShowDialog(this);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            nmDoc = 'M';
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddDocum frmAddDocum = new FrmAddDocum();
                //frmAddProveedor.WindowState = FormWindowState.Normal;
                //frmAddProveedor.MdiParent = this.MdiParent;
                frmAddDocum.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("No existe Serie registrada", "SISTEMA");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cod = Grid1.CurrentRow.Cells[0].Value.ToString();
            FrmSerieDoc frmSerieDoc = new FrmSerieDoc();
            //frmAddProveedor.WindowState = FormWindowState.Normal;
            //frmAddProveedor.MdiParent = this.MdiParent;
            frmSerieDoc.ShowDialog(this);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                if (MessageBox.Show("¿Está seguro que desea eliminar este Documento", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                ClsDocumento ObjDoc = new ClsDocumento();
                if (ObjDoc.Eliminar(cod))
                {
                    MessageBox.Show("Documento Eliminado", "SISTEMA");
                    CargarDatos();
                } else
                {
                    MessageBox.Show("No se pudo eliminar Serie", "SISTEMA");
                }
            }
            else
            {
                MessageBox.Show("Seleccione una Serie", "SISTEMA");
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }
    }
}
