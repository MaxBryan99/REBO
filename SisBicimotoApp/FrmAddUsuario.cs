using SisBicimotoApp.Clases;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddUsuario : Form
    {
        private ClsUsuario ObjUsuario = new ClsUsuario();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmAddUsuario()
        {
            InitializeComponent();

            if (FrmUsuario.nmUsu == 'M')
            {
                LlenarCampos(FrmUsuario.idUser.ToString());
            }
        }

        private void LlenarCampos(string vIdUser)
        {
            try
            {
                if (ObjUsuario.BuscarUsuario(vIdUser, rucEmpresa.ToString()))
                {
                    textBox1.Text = ObjUsuario.Dni.ToString().Trim();
                    textBox2.Text = ObjUsuario.Nombre.ToString().Trim();
                    textBox3.Text = ObjUsuario.Apellido.ToString().Trim();
                    textBox4.Text = ObjUsuario.NomUser.ToString().Trim();
                    textBox5.Text = ObjUsuario.Contraseña.ToString().Trim();
                    label6.Text = ObjUsuario.IdUser.ToString().Trim();
                    label7.Text = ObjUsuario.Dni.ToString().Trim();
                    label8.Text = ObjUsuario.NomUser.ToString().Trim();
                    textBox6.Text = ObjUsuario.Serie.ToString().Trim();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                                          //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox5.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox6.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength < 8)
            {
                MessageBox.Show("Ingrese Nro. de DNI", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre de Usuario", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (textBox3.TextLength == 0)
            {
                MessageBox.Show("Ingrese Apellidos de Usuario", "SISTEMA");
                textBox3.Focus();
                return;
            }

            if (textBox4.TextLength == 0)
            {
                MessageBox.Show("Ingrese Usuario", "SISTEMA");
                textBox4.Focus();
                return;
            }

            if (textBox5.TextLength == 0)
            {
                MessageBox.Show("Ingrese Contraseña de Usuario", "SISTEMA");
                textBox5.Focus();
                return;
            }

            if ((FrmUsuario.nmUsu == 'M' && textBox1.Text != label7.Text) || (FrmUsuario.nmUsu == 'N'))
            {
                if (ObjUsuario.ValidaDni(textBox1.Text))
                {
                    MessageBox.Show("Número de DNI de Usuario ya se encuentra registrado, por favor ingrese otro número de DNI", "SISTEMA");
                    textBox1.Focus();
                    return;
                }
            }

            if ((FrmUsuario.nmUsu == 'N') || (FrmUsuario.nmUsu == 'M' && textBox4.Text != label8.Text))
            {
                if (ObjUsuario.ValidaUSer(textBox4.Text))
                {
                    MessageBox.Show("Nombre de Usuario ya se encuentra registrado, por favor ingrese otro nombre de usuario", "SISTEMA");
                    textBox4.Focus();
                    return;
                }
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar los datos", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            ObjUsuario.NomUser = textBox4.Text.Trim();
            ObjUsuario.Nombre = textBox2.Text.Trim();
            ObjUsuario.Apellido = textBox3.Text.Trim();
            ObjUsuario.Dni = textBox1.Text.Trim();
            ObjUsuario.Contraseña = textBox5.Text.Trim();
            ObjUsuario.UserCreacion = Usuario.ToString().Trim();
            ObjUsuario.UserModi = Usuario.ToString().Trim();
            ObjUsuario.RucEmpresa = rucEmpresa.ToString().Trim();
            ObjUsuario.Serie = textBox6.Text.Trim();

            if (FrmUsuario.nmUsu == 'N')
            {
                if (ObjUsuario.Crear())
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
                if (ObjUsuario.Modificar())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
        }

        private void FrmAddUsuario_Load(object sender, EventArgs e)
        {
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (//char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox6.Text.ToString().Equals("") ? "0" : textBox6.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox6.Text = "";
                else
                    textBox6.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox6.Focus();
            }
        }
    }
}