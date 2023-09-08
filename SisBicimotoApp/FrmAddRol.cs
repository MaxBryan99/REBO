using SisBicimotoApp.Clases;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddRol : Form
    {
        private ClsRol ObjRol = new ClsRol();

        public FrmAddRol()
        {
            InitializeComponent();

            if (FrmRol.nmRol == 'M')
            {
                LlenarCampos(FrmRol.vCodigo.ToString());
            }
        }

        private void LlenarCampos(string vIdRol)
        {
            try
            {
                if (ObjRol.BuscarRol(vIdRol))
                {
                    textBox1.Text = ObjRol.Nombre.ToString().Trim();
                    textBox2.Text = ObjRol.NCorto.ToString().Trim();
                    label1.Text = ObjRol.Codigo.ToString().Trim();
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

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre de Rol", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre Corto de Rol", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar los datos", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            ObjRol.Nombre = textBox1.Text;
            ObjRol.NCorto = textBox2.Text;
            string Usuario = FrmLogin.x_login_usuario;
            ObjRol.UserCreacion = Usuario.ToString();
            ObjRol.UserModi = Usuario.ToString();

            if (FrmRol.nmRol == 'N')
            {
                if (ObjRol.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                ObjRol.Codigo = label1.Text;
                if (ObjRol.Modificar())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}