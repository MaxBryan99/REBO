using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddProveedor : Form
    {
        private ClsProveedor ObjProveedor = new ClsProveedor();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmAddProveedor()
        {
            InitializeComponent();

            string Cod = FrmProveedor.cod.ToString();
            if (FrmProveedor.nmPro == 'M')
            {
                LlenarCampos(Cod);
            }
        }

        private void LlenarCampos(string InCod)
        {
            try
            {
                if (ObjProveedor.BuscarProveedor(InCod, rucEmpresa))
                {
                    textBox1.Text = ObjProveedor.Ruc.ToString().Trim();
                    textBox2.Text = ObjProveedor.Nombre.ToString().Trim();
                    textBox3.Text = ObjProveedor.DireccionFiz.ToString().Trim();
                    textBox4.Text = ObjProveedor.DireccionPar.ToString().Trim();
                    textBox5.Text = ObjProveedor.Ciudad.ToString().Trim();
                    textBox6.Text = ObjProveedor.Telefono.ToString().Trim();
                    textBox7.Text = ObjProveedor.Fax.ToString().Trim();
                    textBox8.Text = ObjProveedor.Email.ToString().Trim();
                    textBox9.Text = ObjProveedor.NumCuenta1.ToString().Trim();
                    textBox10.Text = ObjProveedor.NumCuenta2.ToString().Trim();
                    textBox11.Text = ObjProveedor.Contacto.ToString().Trim();
                    textBox12.Text = ObjProveedor.TelfContacto.ToString().Trim();
                    textBox13.Text = ObjProveedor.Referencia.ToString().Trim();
                    comboBox1.Text = ObjProveedor.TipoMon1.ToString().Trim();
                    comboBox2.Text = ObjProveedor.Banco1.ToString().Trim();
                    comboBox3.Text = ObjProveedor.Banco2.ToString().Trim();
                    comboBox4.Text = ObjProveedor.TipoMon2.ToString().Trim();
                    checkBox1.Checked = ObjProveedor.Est.Equals("A") ? true : false;
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
            //Carga Tipo de Moneda
            string codCatMoneda = "001";
            string tipPresMon = "1";
            DataSet datos = csql.dataset("Call SpCargarDetCat('" + codCatMoneda + "','" + tipPresMon + "')");
            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "CodDetCat";
            comboBox1.DataSource = datos.Tables[0];
            comboBox1.Text = "";
            DataSet datos2 = csql.dataset("Call SpCargarDetCat('" + codCatMoneda + "','" + tipPresMon + "')");
            comboBox4.DisplayMember = "Descripcion";
            comboBox4.ValueMember = "CodDetCat";
            comboBox4.DataSource = datos2.Tables[0];
            comboBox4.Text = "";

            //Carga Banco
            string codCatBanco = "002";
            string tipPresBan = "1";
            DataSet datosBan = csql.dataset("Call SpCargarDetCat('" + codCatBanco + "','" + tipPresBan + "')");
            comboBox2.DisplayMember = "Descripcion";
            comboBox2.ValueMember = "CodDetCat";
            comboBox2.DataSource = datosBan.Tables[0];
            comboBox2.Text = "";
            DataSet datosBan2 = csql.dataset("Call SpCargarDetCat('" + codCatBanco + "','" + tipPresBan + "')");
            comboBox3.DisplayMember = "Descripcion";
            comboBox3.ValueMember = "CodDetCat";
            comboBox3.DataSource = datosBan2.Tables[0];
            comboBox3.Text = "";

            if (FrmProveedor.nmPro == 'N')
            {
                this.Text = "Registrar Proveedor";
            }
            else
            {
                this.Text = "Modificar Proveedor";
                comboBox1.Text = ObjProveedor.TipoMon1.ToString().Trim();
                comboBox2.Text = ObjProveedor.Banco1.ToString().Trim();
                comboBox3.Text = ObjProveedor.Banco2.ToString().Trim();
                comboBox4.Text = ObjProveedor.TipoMon2.ToString().Trim();
                textBox1.Enabled = true;
                textBox2.Focus();
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox7.Focus();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox8.Focus();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox9.Focus();
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox4.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox11.Focus();
            }
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
            if (textBox1.TextLength < 11)
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
            ObjProveedor.Ruc = textBox1.Text.Trim();
            ObjProveedor.Nombre = textBox2.Text.Trim();
            ObjProveedor.DireccionFiz = textBox3.Text.Trim();
            ObjProveedor.DireccionPar = textBox4.Text.Trim();
            ObjProveedor.Ciudad = textBox5.Text.Trim();
            ObjProveedor.Telefono = textBox6.Text.Trim();
            ObjProveedor.Fax = textBox7.Text.Trim();
            ObjProveedor.Email = textBox8.Text.Trim();
            ObjProveedor.TipoMon1 = comboBox1.Text.ToString().Equals("") ? "" : comboBox1.SelectedValue.ToString();
            ObjProveedor.NumCuenta1 = textBox9.Text.Trim();
            ObjProveedor.Banco1 = comboBox2.Text.ToString().Equals("") ? "" : comboBox2.SelectedValue.ToString();
            ObjProveedor.TipoMon2 = comboBox4.Text.ToString().Equals("") ? "" : comboBox4.SelectedValue.ToString();
            ObjProveedor.NumCuenta2 = textBox10.Text.Trim();
            ObjProveedor.Banco2 = comboBox3.Text.ToString().Equals("") ? "" : comboBox3.SelectedValue.ToString();
            ObjProveedor.Contacto = textBox11.Text.Trim();
            ObjProveedor.TelfContacto = textBox12.Text.Trim();
            ObjProveedor.Referencia = textBox13.Text.Trim();
            ObjProveedor.UsuarioCrea = Usuario.ToString().Trim();
            ObjProveedor.UsuarioModi = Usuario.ToString().Trim();
            ObjProveedor.Est = (checkBox1.Checked == true) ? "A" : "N";
            ObjProveedor.RucEmpresa = rucEmpresa.ToString();
            if (FrmProveedor.nmPro == 'N')
            {
                if (ObjProveedor.ValidarProveedor(textBox1.Text.Trim(), rucEmpresa.ToString()))
                {
                    MessageBox.Show("Proveedor ya existe, ingrese otro Proveedor", "SISTEMA");
                    textBox1.Focus();
                    return;
                }

                if (ObjProveedor.Crear())
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
                if (ObjProveedor.Modificar())
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox10.Focus();
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox3.Focus();
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox12.Focus();
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox13.Focus();
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }
    }
}