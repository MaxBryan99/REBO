using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddClienteVentLin : Form
    {
        public ICliente Opener { get; set; }

        private ClsCliente ObjCliente = new ClsCliente();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmAddClienteVentLin()
        {
            InitializeComponent();

            string Cod = FrmCliente.cod.ToString();
            string vTipDoc = FrmCliente.tipdoc.ToString();
            string vParam = "2";
            string vCodCat = "018";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), vTipDoc.ToString(), vParam.ToString());

            if (FrmCliente.nmCLi == 'M') //Si es modificacion busca la informacion del cliente seleccionado
            {
                LlenarCampos(ObjDetCatalogo.CodDetCat.ToString(), Cod);
            }
        }

        private void LlenarCampos(string tipDoc, string InCod)
        {
            /*try
            {
                if (ObjCliente.BuscarCLiente(InCod, rucEmpresa.ToString()))
                {
                    textBox1.Text = ObjCliente.RucDni.ToString().Trim();
                    comboBox5.Text = ObjCliente.TipDoc.ToString().Trim();
                    textBox2.Text = ObjCliente.Nombre.ToString().Trim();
                    textBox3.Text = ObjCliente.Direccion.ToString().Trim();
                    textBox4.Text = ObjCliente.Telefono.ToString().Trim();
                    textBox5.Text = ObjCliente.Celular.ToString().Trim();
                    textBox6.Text = ObjCliente.Contacto.ToString().Trim();
                    textBox7.Text = ObjCliente.Email.ToString().Trim();
                    textBox8.Text = ObjCliente.DireccionEnvio.ToString().Trim();
                    comboBox1.Text = ObjCliente.Region.ToString().Trim();
                    comboBox2.Text = ObjCliente.Provincia.ToString().Trim();
                    comboBox3.Text = ObjCliente.Distrito.ToString().Trim();
                    //Limite de Credito
                    double Net = 0;
                    Net = double.Parse(ObjCliente.LimCredito.ToString().Equals("") ? "0" : ObjCliente.LimCredito.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox9.Text = "";
                    else
                        textBox9.Text = Net.ToString("###,##0.00").Trim();
                    comboBox4.Text = ObjCliente.CodVendedor.ToString().Trim();
                    checkBox1.Checked = ObjCliente.Est.Equals("A") ? true : false;
                }
                else
                {
                    MessageBox.Show("FALSE");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se puede cargar datos: " + ex.Message);
            }*/
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
            //Carga Documento
            string codCatTipDoc = "018";
            string tipPresDoc = "2";
            DataSet datosDoc = csql.dataset_cadena("Call SpCargarDetCat('" + codCatTipDoc + "','" + tipPresDoc + "')");
            comboBox5.DisplayMember = "DescCorta";
            comboBox5.ValueMember = "CodDetCat";
            comboBox5.DataSource = datosDoc.Tables[0];

            //Carga Region
            /*DataSet datos = csql.dataset("Call SpCargarRegion()");
            comboBox1.DisplayMember = "NOMBRE";
            comboBox1.ValueMember = "CODDPTO";
            comboBox1.DataSource = datos.Tables[0];
            comboBox1.Text = "";*/

            //Carga Vendedor
            /*DataSet datosVend = csql.dataset("Call SpClienteVendedor('" + rucEmpresa.ToString() + "')");
            comboBox4.DisplayMember = "NOMBRE";
            comboBox4.ValueMember = "CODVEND";
            comboBox4.DataSource = datosVend.Tables[0];
            comboBox4.Text = "";*/

            /*if (FrmCliente.nmCLi == 'N') //Verifica si el suario ha seleccinado el boton Nuevo
            {
                this.Text = "Registrar Cliente";
            }
            else //Si no es NUevo entonces es Modificacion
            {
                this.Text = "Modificar Cliente";
                comboBox1.Text = ObjCliente.Region.ToString().Trim();
                comboBox2.Text = ObjCliente.Provincia.ToString().Trim();
                comboBox3.Text = ObjCliente.Distrito.ToString().Trim();
                comboBox4.Text = ObjCliente.CodVendedor.ToString().Trim();
                comboBox5.Text = ObjCliente.TipDoc.ToString().Trim();
                textBox1.Enabled = false;
                comboBox5.Enabled = false;
                textBox2.Focus();
            }*/
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
                button2.Focus();
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
                //textBox5.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                //textBox6.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //textBox7.Focus();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //textBox8.Focus();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //comboBox1.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //comboBox2.Focus();
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //comboBox3.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //textBox9.Focus();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
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
            if (comboBox5.Text == "")
            {
                MessageBox.Show("Ingrese Tipo de Documento de Cliente", "SISTEMA");
                comboBox5.Focus();
                return;
            }

            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nro. de Documento de Cliente", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (comboBox5.SelectedValue.Equals("1"))
            {
                if (textBox1.TextLength < 8)
                {
                    MessageBox.Show("Ingrese Ruc (11 dígitos) o DNI (8 dígitos)", "SISTEMA");
                    textBox1.Focus();
                    return;
                }
            }
            if (comboBox5.SelectedValue.Equals("6"))
            {
                if (textBox1.TextLength > 8 && textBox1.TextLength < 11)
                {
                    MessageBox.Show("Ingrese Ruc (11 dígitos)", "SISTEMA");
                    textBox1.Focus();
                    return;
                }
            }
            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre o Razón Social", "SISTEMA");
                textBox2.Focus();
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            ObjCliente.RucDni = textBox1.Text.Trim();
            ObjCliente.TipDoc = comboBox5.SelectedValue.ToString();
            ObjCliente.Nombre = textBox2.Text.Trim();
            ObjCliente.Direccion = textBox3.Text.Trim();
            ObjCliente.Telefono = "";
            ObjCliente.Celular = "";
            ObjCliente.Contacto = "";
            ObjCliente.Email = "";
            ObjCliente.DireccionEnvio = "";
            ObjCliente.Region = "";
            ObjCliente.Provincia = "";
            ObjCliente.Distrito = "";
            ObjCliente.LimCredito = 0;
            ObjCliente.CodVendedor = "";
            ObjCliente.UsuarioCrea = Usuario.ToString().Trim();
            ObjCliente.UsuarioModi = Usuario.ToString().Trim();
            ObjCliente.Est = "A";
            ObjCliente.RucEmpresa = rucEmpresa.ToString();

            if (ObjCliente.ValidarCliente(textBox1.Text.Trim(), rucEmpresa.ToString()))
            {
                MessageBox.Show("Cliente ya existe, ingrese otro Cliente", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (ObjCliente.Crear())
            {
                MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                string tipDoc = comboBox5.SelectedValue.ToString();
                string codCli = textBox1.Text.Trim();
                this.Opener.SelectItem(tipDoc, codCli);
                this.Close();
            }
            else
            {
                MessageBox.Show("No se registro correctamente", "SISTEMA");
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                comboBox5.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox1.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox2.Focus();
            }
            if (e.KeyCode == Keys.Down)
            {
                button2.Focus();
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedValue.ToString().Equals("1"))
            {
                textBox1.MaxLength = 8;
            }
            else
            {
                textBox1.MaxLength = 11;
            }
        }
    }
}