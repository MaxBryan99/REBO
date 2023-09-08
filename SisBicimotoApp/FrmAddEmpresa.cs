using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddEmpresa : Form
    {
        //Instaciamos Clase Empresa
        private ClsEmpresa ObjEmpresa = new ClsEmpresa();

        private ClsUbigeo ObjUbigeo = new ClsUbigeo();

        public FrmAddEmpresa()
        {
            InitializeComponent();

            if (FrmEmpresa.nmEmp == 'M') //Si es modificacion busca la informacion de la empresa seleccionado
            {
                LlenarCampos(FrmEmpresa.codRuc);
            }
        }

        private void LlenarCampos(string vRuc) //Metodo para buscar una Empresa y llenar los campos
        {
            try
            {
                if (ObjEmpresa.BuscarRuc(vRuc.ToString()))
                {
                    textBox1.Text = ObjEmpresa.Ruc.ToString().Trim();
                    textBox2.Text = ObjEmpresa.Razon.ToString().Trim();
                    textBox3.Text = ObjEmpresa.NombreLegal.ToString().Trim();
                    textBox4.Text = ObjEmpresa.Direccion.ToString().Trim();
                    textBox6.Text = ObjEmpresa.Ubicacion.ToString().Trim();

                    comboBox1.Text = ObjEmpresa.Region.ToString().Trim();
                    comboBox2.Text = ObjEmpresa.Provincia.ToString().Trim();
                    comboBox3.Text = ObjEmpresa.Distrito.ToString().Trim();

                    textBox9.Text = ObjEmpresa.Urbanizacion.ToString().Trim();
                    textBox12.Text = ObjEmpresa.Email.ToString().Trim();
                    //textBox12.Text = ObjEmpresa.Email.ToString().Equals(null) ? "" : ObjEmpresa.Email.ToString().Trim();
                    textBox11.Text = ObjEmpresa.Telefono.ToString().Trim();
                    textBox10.Text = ObjEmpresa.Celular.ToString().Trim();
                    textBox13.Text = ObjEmpresa.Representante.ToString().Trim();
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

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void FormAddEmpresa_Load(object sender, EventArgs e)
        {
            //Carga Region
            DataSet datos = csql.dataset("Call SpCargarRegion()");
            comboBox1.DisplayMember = "NOMBRE";
            comboBox1.ValueMember = "CODDPTO";
            comboBox1.DataSource = datos.Tables[0];
            comboBox1.Text = "";

            if (FrmEmpresa.nmEmp == 'N') //Verifica si el suario ha seleccinado el boton Nuevo
            {
                this.Text = "Registrar Empresa";
            }
            else //Si no es NUevo entonces es Modificacion
            {
                this.Text = "Modificar Empresa";
                comboBox1.Text = ObjEmpresa.Region.ToString().Trim();
                comboBox2.Text = ObjEmpresa.Provincia.ToString().Trim();
                comboBox3.Text = ObjEmpresa.Distrito.ToString().Trim();
                textBox1.Enabled = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Equals(""))
                {
                    string val = "0";
                    DataSet datos1 = csql.dataset("Call SpCargarProvincia('" + val + "')");
                    comboBox2.DisplayMember = "NOMBRE";
                    comboBox2.ValueMember = "CODPROV";
                    comboBox2.DataSource = datos1.Tables[0];
                    comboBox2.Text = "";
                }
                else
                {
                    DataSet datos1 = csql.dataset("Call SpCargarProvincia('" + comboBox1.SelectedValue.ToString() + "')");
                    comboBox2.DisplayMember = "NOMBRE";
                    comboBox2.ValueMember = "CODPROV";
                    comboBox2.DataSource = datos1.Tables[0];
                    comboBox2.Text = "";
                }
            }
            catch (System.Exception ex)
            {
                string val = "0";
                DataSet datos1 = csql.dataset("Call SpCargarProvincia('" + val + "')");
                comboBox2.DisplayMember = "NOMBRE";
                comboBox2.ValueMember = "CODPROV";
                comboBox2.DataSource = datos1.Tables[0];
                comboBox2.Text = "";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text.Equals(""))
                {
                    string val1 = "0";
                    string val2 = "0";
                    DataSet datos2 = csql.dataset("Call SpCargarDistrito('" + val1 + "', '" + val2 + "')");
                    comboBox3.DisplayMember = "NOMBRE";
                    comboBox3.ValueMember = "CODDIST";
                    comboBox3.DataSource = datos2.Tables[0];
                    comboBox3.Text = "";
                }
                else
                {
                    DataSet datos2 = csql.dataset("Call SpCargarDistrito('" + comboBox1.SelectedValue.ToString() + "', '" + comboBox2.SelectedValue.ToString() + "')");
                    comboBox3.DisplayMember = "NOMBRE";
                    comboBox3.ValueMember = "CODDIST";
                    comboBox3.DataSource = datos2.Tables[0];
                    comboBox3.Text = "";
                }
            }
            catch (System.Exception ex)
            {
                string val1 = "0";
                string val2 = "0";
                DataSet datos2 = csql.dataset("Call SpCargarDistrito('" + val1 + "', '" + val2 + "')");
                comboBox3.DisplayMember = "NOMBRE";
                comboBox3.ValueMember = "CODDIST";
                comboBox3.DataSource = datos2.Tables[0];
                comboBox3.Text = "";
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true; // hasta aqui se valida y se dan opciones de ingreso de datos solo numeros

            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true; // hasta aqui se valida y se dan opciones de ingreso de datos solo numeros
            //Focus al siguiente objeto cuando se presiona ENTER
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
                e.Handled = true; // hasta aqui se valida y se dan opciones de ingreso de datos solo numeros
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                textBox13.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                textBox6.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                textBox4.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                comboBox3.Focus();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                textBox9.Focus();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                textBox12.Focus();
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                textBox11.Focus();
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Focus al siguiente objeto cuando se presiona ENTER
            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Validamos que se ingrese el RUC, que el campo no este vacio
            if (textBox1.TextLength < 11)
            {
                MessageBox.Show("Ingrese Nro. de RUC", "SISTEMA");
                textBox1.Focus();
                return;
            }

            //Validamos que se ingrese la razon social de la empresa
            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Razón Social de la Empresa", "SISTEMA");
                textBox2.Focus();
                return;
            }

            //Validamos que se ingrese la dirección de la empresa
            if (textBox4.TextLength == 0)
            {
                MessageBox.Show("Ingrese Dirección de la Empresa", "SISTEMA");
                textBox4.Focus();
                return;
            }

            //Preguntamos al usuario si esta seguro de grabar los datos
            //Pregunta de confirmacion
            if (MessageBox.Show("¿Datos Correctos?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //cargamos el valor delusuario logueado en el sistema
            string Usuario = FrmLogin.x_login_usuario;
            //Cargamos datos en los atributos del objeto instanciado
            ObjEmpresa.Ruc = textBox1.Text.Trim(); //Grabamos Ruc
            ObjEmpresa.Razon = textBox2.Text.Trim(); //Grabamos Razon Social
            ObjEmpresa.NombreLegal = textBox3.Text.Trim(); //Grabamos Nombre Legal
            ObjEmpresa.Direccion = textBox4.Text.Trim(); //Grabamos Dirección
            ObjEmpresa.Ubicacion = textBox6.Text.Trim(); //Grabamos Ubicación
            //ObjEmpresa.Region = comboBox1.Text.Trim(); //Grabamos Región// Grabacion de NANO para verificar
            ObjEmpresa.Region = comboBox1.Text.ToString().Equals("") ? "" : comboBox1.SelectedValue.ToString(); //Grabamos Región
            //ObjEmpresa.Provincia = comboBox2.Text.Trim(); //Grabamos Provincia// Grabacion de NANO para verificar
            ObjEmpresa.Provincia = comboBox2.Text.ToString().Equals("") ? "" : comboBox2.SelectedValue.ToString(); //Grabamos Provincia
            //ObjEmpresa.Distrito = comboBox3.Text.Trim(); //Grabamos Distrito// Grabacion de NANO para verificar
            ObjEmpresa.Distrito = comboBox3.Text.ToString().Equals("") ? "" : comboBox3.SelectedValue.ToString(); //Grabamos Distrito
            //Creamos variable de ubigeo
            string cUbigeo = ObjEmpresa.Region + ObjEmpresa.Provincia + ObjEmpresa.Distrito;
            ObjEmpresa.Ubigeo = cUbigeo.ToString().Trim();
            ObjEmpresa.Urbanizacion = textBox9.Text.Trim(); //Grabamos Urbanización
            ObjEmpresa.Email = textBox12.Text.Trim(); //Grabamos Email
            ObjEmpresa.Telefono = textBox11.Text.Trim(); //Grabamos Teléfono
            ObjEmpresa.Celular = textBox10.Text.Trim(); //Grabamos Celular
            ObjEmpresa.Representante = textBox13.Text.Trim(); //Grabamos Representante
            ObjEmpresa.UsuarioCrea = Usuario.ToString().Trim();
            ObjEmpresa.UsuarioModi = Usuario.ToString().Trim();
            //Llamamos al metodor de crear paa que grabe el registro
            //Si existe un error nos muestra mensaje de error
            if (FrmEmpresa.nmEmp == 'N')
            {
                if (ObjEmpresa.Crear())
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
                if (ObjEmpresa.Modificar())
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
    }
}