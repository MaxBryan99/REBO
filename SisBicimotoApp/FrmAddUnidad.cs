using SisBicimotoApp.Clases;
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
    public partial class FrmAddUnidad : Form
    {
        ClsUnidad ObjUnidad = new ClsUnidad();
        public FrmAddUnidad()
        {
            InitializeComponent();
            string Cod = FrmUnidad.cod.ToString();
            if (FrmUnidad.nmUnd == 'M')
            {
                LlenarCampos(Cod);
            }
        }

        void LlenarCampos(string InCod)
        {
            try
            {
                if (ObjUnidad.BuscarUnidad(InCod))
                {
                    txtCodigo.Text = InCod.ToString();
                    txtNombre.Text = ObjUnidad.Nombre.ToString().Trim();
                }
                else
                {
                    MessageBox.Show("FALSE");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se puede cargar datos: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddUnidad_Load(object sender, EventArgs e)
        {
            if (FrmUnidad.nmUnd == 'N')
            {
                this.Text = "Registrar Unidad";
                this.txtCodigo.Enabled = false;
                this.txtCodigo.Text = "Autogenerado";
            }
            else
            {
                this.Text = "Modificar Unidad";
                this.txtCodigo.Enabled = false;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnGuardar.Focus();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (FrmUnidad.nmUnd == 'N')
            {
                if (txtNombre.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Nombre", "SISTEMA");
                    txtNombre.Focus();
                    return;
                }

                ObjUnidad.Nombre = txtNombre.Text.Trim();

                if (ObjUnidad.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }

            if (FrmUnidad.nmUnd == 'M')
            {
                if (txtNombre.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Nombre", "SISTEMA");
                    txtNombre.Focus();
                    return;
                }

                ObjUnidad.Nombre = txtNombre.Text.Trim();

                if (ObjUnidad.Modificar(txtCodigo.Text.ToString().Trim()))
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se actualizó correctamente", "SISTEMA");
                }
            }
        }
    }
}
