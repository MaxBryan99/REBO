using SisBicimotoApp.Clases;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddTransportista : Form
    {
        private ClsTransportista ObjTransportista = new ClsTransportista();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmAddTransportista()
        {
            InitializeComponent();

            string Cod = FrmTransportista.cod.ToString();
            if (FrmTransportista.nmTrans == 'M')
            {
                LlenarCampos(Cod);
            }
        }

        private void LlenarCampos(string InCod)
        {
            try
            {
                if (ObjTransportista.BuscarTransportista(InCod, rucEmpresa.ToString()))
                {
                    textBox1.Text = ObjTransportista.Ruc.ToString().Trim();
                    textBox2.Text = ObjTransportista.Nombre.ToString().Trim();
                    textBox3.Text = ObjTransportista.Direccion.ToString().Trim();
                    textBox4.Text = ObjTransportista.Telefono.ToString().Trim();
                    checkBox1.Checked = ObjTransportista.Est.Equals("A") ? true : false;
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

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddCliente_Load(object sender, EventArgs e)
        {
            if (FrmTransportista.nmTrans == 'N')
            {
                this.Text = "Registrar Transportista";
            }
            else
            {
                this.Text = "Modificar Transportista";
                textBox1.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
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
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox9_Validated(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Ruc", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox1.TextLength < 11)
            {
                MessageBox.Show("Ingrese Ruc (11 dígitos)", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre o Razón Social", "SISTEMA");
                textBox2.Focus();
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            ObjTransportista.Ruc = textBox1.Text.Trim();
            ObjTransportista.Nombre = textBox2.Text.Trim();
            ObjTransportista.Direccion = textBox3.Text.Trim();
            ObjTransportista.Telefono = textBox4.Text.Trim();
            ObjTransportista.UserCreacion = Usuario.ToString().Trim();
            ObjTransportista.UserModi = Usuario.ToString().Trim();
            ObjTransportista.Est = (checkBox1.Checked == true) ? "A" : "N";
            ObjTransportista.RucEmpresa = rucEmpresa.ToString();
            if (FrmTransportista.nmTrans == 'N')
            {
                if (ObjTransportista.ValidarTransportista(textBox1.Text.Trim(), rucEmpresa.ToString()))
                {
                    MessageBox.Show("Transportista ya existe, ingrese otro Cliente", "SISTEMA");
                    textBox1.Focus();
                    return;
                }

                if (ObjTransportista.Crear())
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
                if (ObjTransportista.Modificar())
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

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }
    }
}