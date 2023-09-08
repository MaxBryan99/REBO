using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddPersonal : Form
    {
        private ClsPersonal ObjPersonal = new ClsPersonal();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmAddPersonal()
        {
            InitializeComponent();

            string Cod = FrmPersonal.cod.ToString();
            if (FrmPersonal.nmPer == 'M')
            {
                LlenarCampos(Cod);
            }
        }

        private void LlenarCampos(string InCod)
        {
            //try
            //{
            if (ObjPersonal.BuscarPersonal(InCod, rucEmpresa.ToString()))
            {
                textBox1.Text = ObjPersonal.Codigo.ToString().Trim();
                textBox2.Text = ObjPersonal.Nombre.ToString().Trim();
                textBox3.Text = ObjPersonal.Direccion.ToString().Trim();
                textBox4.Text = ObjPersonal.Telefono.ToString().Trim();
                textBox5.Text = ObjPersonal.Celular.ToString().Trim();
                textBox6.Text = ObjPersonal.NroDocumento.ToString().Trim();
                textBox7.Text = ObjPersonal.Email.ToString().Trim();
                DTP1.Text = ObjPersonal.FecIngreso.ToString().Trim();
                if (ObjPersonal.Est.Equals("N"))
                {
                    DTP2.Text = ObjPersonal.FecCese.ToString().Trim();
                    DTP2.Enabled = true;
                }
                //Sueldo Bruto
                double Net = 0;
                Net = double.Parse(ObjPersonal.SueldoBruto.ToString().Equals("") ? "0" : ObjPersonal.SueldoBruto.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox8.Text = "";
                else
                    textBox8.Text = Net.ToString("###,##0.00").Trim();

                textBox9.Text = ObjPersonal.Comentario.ToString().Trim();
                comboBox1.Text = ObjPersonal.TipoDocumento.ToString().Trim();
                comboBox2.Text = ObjPersonal.Profesion.ToString().Trim();
                comboBox3.Text = ObjPersonal.Cargo.ToString().Trim();
                comboBox4.Text = ObjPersonal.Area.ToString().Trim();
                checkBox1.Checked = ObjPersonal.Est.Equals("N") ? true : false;
            }
            else
            {
                MessageBox.Show("FALSE");
            }
            /*}
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
            //Carga Tipo de Documento
            string codCatTipDoc = "003";
            string tipPresDoc = "1";
            DataSet datos = csql.dataset_cadena("Call SpCargarDetCat('" + codCatTipDoc + "','" + tipPresDoc + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                comboBox1.Items.Add("");
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    comboBox1.Items.Add(fila[1].ToString());
                }
            }

            //Carga Profesion
            string codCatProfesion = "004";
            string tipPresProf = "1";
            DataSet datos1 = csql.dataset_cadena("Call SpCargarDetCat('" + codCatProfesion + "','" + tipPresProf + "')");

            if (datos1.Tables[0].Rows.Count > 0)
            {
                comboBox2.Items.Add("");
                foreach (DataRow fila in datos1.Tables[0].Rows)
                {
                    comboBox2.Items.Add(fila[1].ToString());
                }
            }

            //Carga Cargo
            string codCatCargo = "005";
            string tipPresCargo = "1";
            DataSet datos2 = csql.dataset_cadena("Call SpCargarDetCat('" + codCatCargo + "','" + tipPresCargo + "')");

            if (datos2.Tables[0].Rows.Count > 0)
            {
                comboBox3.Items.Add("");
                foreach (DataRow fila in datos2.Tables[0].Rows)
                {
                    comboBox3.Items.Add(fila[1].ToString());
                }
            }

            //Carga Area
            string codCatArea = "006";
            string tipPresArea = "1";
            DataSet datos3 = csql.dataset_cadena("Call SpCargarDetCat('" + codCatArea + "','" + tipPresArea + "')");

            if (datos3.Tables[0].Rows.Count > 0)
            {
                comboBox4.Items.Add("");
                foreach (DataRow fila in datos3.Tables[0].Rows)
                {
                    comboBox4.Items.Add(fila[1].ToString());
                }
            }

            if (FrmPersonal.nmPer == 'N')
            {
                this.Text = "Registrar Personal";
            }
            else
            {
                this.Text = "Modificar Personal";
                comboBox1.Text = ObjPersonal.TipoDocumento.ToString().Trim();
                comboBox2.Text = ObjPersonal.Profesion.ToString().Trim();
                comboBox3.Text = ObjPersonal.Cargo.ToString().Trim();
                comboBox4.Text = ObjPersonal.Area.ToString().Trim();
                textBox1.Enabled = false;
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
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox5.Focus();
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
                comboBox1.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
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
            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
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
                if (textBox6.Enabled == false)
                {
                    textBox7.Focus();
                }
                else
                {
                    textBox6.Focus();
                }
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox4.Focus();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button2.Focus();
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
            if (comboBox1.Text.Equals(""))
            {
                textBox6.Enabled = false;
            }
            else
            {
                textBox6.Enabled = true;
            }
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
            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre y Apellidos", "SISTEMA");
                textBox2.Focus();
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            ObjPersonal.Codigo = textBox1.Text.Trim();
            ObjPersonal.Nombre = textBox2.Text.Trim();
            ObjPersonal.Direccion = textBox3.Text.Trim();
            ObjPersonal.Telefono = textBox4.Text.Trim();
            ObjPersonal.Celular = textBox5.Text.Trim();
            ObjDetCatalogo.BuscarDetCatalogoDes("003", comboBox1.Text.Trim(), "1");
            ObjPersonal.TipoDocumento = comboBox1.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            ObjPersonal.NroDocumento = textBox6.Text.Trim();
            ObjPersonal.Email = textBox7.Text.Trim();
            ObjDetCatalogo.BuscarDetCatalogoDes("004", comboBox2.Text.Trim(), "1");
            ObjPersonal.Profesion = comboBox2.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            ObjDetCatalogo.BuscarDetCatalogoDes("005", comboBox3.Text.Trim(), "1");
            ObjPersonal.Cargo = comboBox3.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            ObjDetCatalogo.BuscarDetCatalogoDes("006", comboBox4.Text.Trim(), "1");
            ObjPersonal.Area = comboBox4.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            ObjPersonal.SueldoBruto = double.Parse(textBox8.Text.ToString().Equals("") ? "0" : textBox8.Text.ToString().Trim());
            ObjPersonal.FecIngreso = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
            ObjPersonal.Comentario = textBox9.Text.Trim();
            if (checkBox1.Checked == true)
            {
                ObjPersonal.FecCese = DTP2.Value.Year.ToString() + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Day.ToString("00");
                ObjPersonal.Est = "N";
            }
            else
            {
                ObjPersonal.Est = "A";
            }
            ObjPersonal.UserCreacion = Usuario.ToString().Trim();
            ObjPersonal.UserModi = Usuario.ToString().Trim();
            ObjPersonal.RucEmpresa = rucEmpresa.ToString();
            if (FrmPersonal.nmPer == 'N')
            {
                /*if (ObjPersonal.ValidarCliente(textBox1.Text.Trim()))
                {
                    MessageBox.Show("Personal ya existe, ingrese otro Cliente", "SISTEMA");
                    textBox1.Focus();
                    return;
                }*/

                if (ObjPersonal.Crear())
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
                if (ObjPersonal.Modificar())
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

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox8.Focus();
            }
        }

        private void textBox8_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox9.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DTP2.Enabled = true;
            }
            else
            {
                DTP2.Enabled = false;
            }
        }

        private void textBox8_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox8.Text.ToString().Equals("") ? "0" : textBox8.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox8.Text = "";
                else
                    textBox8.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox8.Focus();
            }
        }
    }
}