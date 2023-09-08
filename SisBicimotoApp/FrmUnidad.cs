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
    public partial class FrmUnidad : Form
    {
        public static char nmUnd = 'N';
        public static string cod = "";
        DataSet datos;
        ClsUnidad ObjUnidad = new ClsUnidad();

        public FrmUnidad()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Descripción";
            Grid1.Columns[0].Width = 80;
            Grid1.Columns[1].Width = 348;
            Grid1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Grid1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpUnidadGen()");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmUnidad_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            nmUnd = 'N';
            FrmAddUnidad frmAddUnidad = new FrmAddUnidad();
            frmAddUnidad.WindowState = FormWindowState.Normal;
            frmAddUnidad.MdiParent = this.MdiParent;
            frmAddUnidad.Show();
        }

        private void btnModi_Click(object sender, EventArgs e)
        {
            nmUnd = 'M';
            if (Grid1.RowCount > 0)
            {
                cod = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddUnidad frmAddUnidad = new FrmAddUnidad();
                frmAddUnidad.WindowState = FormWindowState.Normal;
                frmAddUnidad.MdiParent = this.MdiParent;
                frmAddUnidad.Show();
            }
            else
            {
                MessageBox.Show("No existen unidades registrados", "SISTEMA");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            cod = Grid1.CurrentRow.Cells[0].Value.ToString();

            if (MessageBox.Show("¿Está seguro de querer eliminar la unidad con código: " + cod + "?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (ObjUnidad.Eliminar_Est(cod.ToString()))
                {
                    MessageBox.Show("Unidad eliminada", "SISTEMA");
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("No se pudo eliminar Unidad", "SISTEMA");
                }
            }
        }

        private void btnBusqueda_Click(object sender, EventArgs e)
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
                    if (txtBusqueda.TextLength > 0)
                    {
                        string codigo = txtBusqueda.Text.Trim();
                        datos = csql.dataset("Call SpUnidadBusCodG('" + codigo.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                    }
                    else
                    {
                        CargarDatos();
                    }
                }
                if (selectedIndex.Equals(1))
                {
                    string nnombre = txtBusqueda.Text.Trim();
                    datos = csql.dataset("Call SpUnidadBusNom('" + nnombre.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                }
            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnBusqueda.Focus();
            }
        }
    }
}
