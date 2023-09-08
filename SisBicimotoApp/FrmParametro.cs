using MySql.Data.MySqlClient;
using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmParametro : Form
    {
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private string strMensaje = "";

        public FrmParametro()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength == 0 || textBox2.Equals("0.00"))
            {
                MessageBox.Show("Ingrese valor de IGV", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (textBox1.TextLength == 0 || textBox1.Equals("0.00"))
            {
                MessageBox.Show("Ingrese valor de ISC", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox3.TextLength == 0 || textBox3.Equals("0.00"))
            {
                MessageBox.Show("Ingrese valor de Detracción", "SISTEMA");
                textBox3.Focus();
                return;
            }

            if (comboBox3.Text == "")
            {
                MessageBox.Show("Ingrese tipo de moneda predeterminada", "SISTEMA");
                comboBox3.Focus();
                return;
            }

            if (comboBox1.Text == "")
            {
                MessageBox.Show("Ingrese comprobante predeterminado", "SISTEMA");
                comboBox1.Focus();
                return;
            }

            if (comboBox2.Text == "")
            {
                MessageBox.Show("Ingrese comprobante predeterminado para las ventas en línea", "SISTEMA");
                comboBox2.Focus();
                return;
            }

            if (comboBox4.Text == "")
            {
                MessageBox.Show("Seleccione si se aplica IGV", "SISTEMA");
                comboBox4.Focus();
                return;
            }

            if (comboBox5.Text == "")
            {
                MessageBox.Show("Seleccione el tipo de afectación del IGV", "SISTEMA");
                comboBox5.Focus();
                return;
            }

            if (textBox4.TextLength == 0 || textBox4.Equals("0"))
            {
                MessageBox.Show("Ingrese cantidad de artículos por comprobante", "SISTEMA");
                textBox4.Focus();
                return;
            }

            if (comboBox4.Text.Equals("SI") && comboBox6.SelectedValue.ToString().Equals("02"))
            {
                MessageBox.Show("Si el tipo de Venta a SUNAT es del valor EXONERADO no se puede afectar IGV, cambie la afectación del IGV a NO", "SISTEMA");
                comboBox4.Focus();
                return;
            }

            if (checkBox1.Checked == true && (textBox11.TextLength > 0 && textBox10.TextLength <= 11))
            {
                MessageBox.Show("Ingrese una Url de API de BD remota que sea válido", "SISTEMA");
                textBox11.Focus();
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar los parámetros del sistema", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            Boolean nValT = false;

            strMensaje = "";

            // Modificando Moneda Predeterminada
            string Usuario = FrmLogin.x_login_usuario;
            ObjParametro.Id = "1";
            ObjParametro.Valor = comboBox3.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = "No se registro correctamente el valor de Moneda" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Comprobante Predeterminado
            ObjParametro.Id = "2";
            ObjParametro.Valor = comboBox1.SelectedValue.ToString();
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el comprobante predeterminado" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Valor del Igv
            ObjParametro.Id = "3";
            ObjParametro.Valor = textBox2.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor del Igv" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Comprobante Predeterminado venta en linea
            ObjParametro.Id = "6";
            ObjParametro.Valor = comboBox2.SelectedValue.ToString();
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el comprobante predeterminado para las ventas en línea" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Aplicar Igv
            ObjParametro.Id = "7";
            ObjParametro.Valor = comboBox4.Text.Substring(0, 1);
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "Error al aplicar Igv" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Valor del ISC
            ObjParametro.Id = "8";
            ObjParametro.Valor = textBox1.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor del ISC" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Valor del Detraccion
            ObjParametro.Id = "9";
            ObjParametro.Valor = textBox3.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor de la Detracción" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Tipo de Afectación
            ObjParametro.Id = "11";
            ObjParametro.Valor = comboBox5.SelectedValue.ToString();
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "Error al tipo de Afectación";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            if (nValT)
            {
                MessageBox.Show("Datos registrados correctamente", "SISTEMA");
            }
            else
            {
                MessageBox.Show(strMensaje, "SISTEMA");
            }

            //Cantidad de Comprobantes
            ObjParametro.Id = "14";
            ObjParametro.Valor = textBox4.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor de la cantidad de comprobantes " + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Comprobante Predeterminado venta en linea
            ObjParametro.Id = "15";
            ObjParametro.Valor = comboBox6.SelectedValue.ToString();
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el tipo de Venta SUNAT predeterminado para las ventas en línea" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            //Parmetro de dimenciones para rebo
            ObjParametro.Id = "29";
            string vDimension = "";
            if (ckDimensiones.Checked)
            {
                vDimension = "1";
            }
            else
            {
                vDimension = "0";
            }
            ObjParametro.Valor = vDimension;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el tipo parametro de dimensión" + "\n";
            }
            

            try
            {
                string archivo = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivo);

                if (checkBox1.Checked)
                {
                    ciniarchivo.WriteValue("BDRemoto", "Acceso", "SI");
                    ciniarchivo.WriteValue("BDRemoto", "UrlBD", textBox11.Text);
                }
                else
                {
                    ciniarchivo.WriteValue("BDRemoto", "Acceso", "NO");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Hubo problemas al leer el archivo INI. " + ex.Message, "SISTEMA");
            }

            if (strMensaje.Equals(""))
            {
                MessageBox.Show("Datos grabados correctamente", "SISTEMA");
            }
            else
            {
                MessageBox.Show(strMensaje, "SISTEMA");
            }
        }

        private void FrmParametro_Load(object sender, EventArgs e)
        {
            //Cursor.Current = Cursors.WaitCursor;

            /*using (var ctx = new BicimotoDb())
            {
                direccionSunatBindingSource.DataSource = ctx.DireccionesSunat.ToList();
                direccionSunatBindingSource.ResetBindings(false);
            }*/
            //Carga Direcciones
            string codCatSunat = "023";
            string tipPresSunat = "1";
            DataSet datosSunat = csql.dataset("Call SpCargarDetCat('" + codCatSunat + "','" + tipPresSunat + "')");
            comboBox7.DisplayMember = "descripcion";
            comboBox7.ValueMember = "CodDetCat";
            comboBox7.DataSource = datosSunat.Tables[0];

            //Carga Moneda
            string codCatMon = "001";
            string tipPresMon = "2";
            DataSet datosMon = csql.dataset("Call SpCargarDetCat('" + codCatMon + "','" + tipPresMon + "')");
            comboBox3.DisplayMember = "DescCorta";
            comboBox3.ValueMember = "CodDetCat";
            comboBox3.DataSource = datosMon.Tables[0];

            string nParam = "1";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox3.Text = ObjParametro.Valor.ToString();
            }

            //Buscar comprobantes autocorrelativos
            string vModulo = "VEN";
            DataSet datosDoc = csql.dataset("Call SpDocBusModulo('" + vModulo.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "Codigo";
            comboBox1.DataSource = datosDoc.Tables[0];

            nParam = "2";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox1.SelectedValue = ObjParametro.Valor.ToString();
            }

            //Obtener IGV
            nParam = "3";
            double valIgv = 0;
            if (ObjParametro.BuscarParametro(nParam))
            {
                valIgv = Double.Parse(ObjParametro.Valor);
            }
            else
            {
                valIgv = 0;
            }

            if (valIgv.ToString().Trim().Equals("0"))
                textBox2.Text = "0.00";
            else
                textBox2.Text = valIgv.ToString("###,##0.00").Trim();

            //Buscar comprobantes autocorrelativos
            vModulo = "VEN";
            DataSet datosComp = csql.dataset("Call SpDocBusModulo('" + vModulo.ToString() + "')");
            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "Codigo";
            comboBox2.DataSource = datosComp.Tables[0];

            nParam = "6";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox2.SelectedValue = ObjParametro.Valor.ToString();
            }

            //Aplica IGV
            nParam = "7";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                if (ObjParametro.Valor.ToString().Equals("S"))
                {
                    comboBox4.SelectedIndex = 0;
                }
                else
                {
                    comboBox4.SelectedIndex = 1;
                }
            }

            //ISC
            nParam = "8";
            double vIsc = 0;
            if (ObjParametro.BuscarParametro(nParam))
            {
                vIsc = Double.Parse(ObjParametro.Valor);
            }
            else
            {
                vIsc = 0;
            }

            if (vIsc.ToString().Trim().Equals("0"))
                textBox1.Text = "0.00";
            else
                textBox1.Text = vIsc.ToString("###,##0.00").Trim();

            //DETRACCION
            nParam = "9";
            double vDetrac = 0;
            if (ObjParametro.BuscarParametro(nParam))
            {
                vDetrac = Double.Parse(ObjParametro.Valor);
            }
            else
            {
                vDetrac = 0;
            }

            if (vDetrac.ToString().Trim().Equals("0"))
                textBox3.Text = "0.00";
            else
                textBox3.Text = vDetrac.ToString("###,##0.00").Trim();

            //TIPO AFECTACION
            //Carga Moneda
            codCatMon = "019";
            tipPresMon = "1";
            DataSet datosAfec = csql.dataset("Call SpCargarDetCat('" + codCatMon + "','" + tipPresMon + "')");
            comboBox5.DisplayMember = "Descripcion";
            comboBox5.ValueMember = "CodDetCat";
            comboBox5.DataSource = datosAfec.Tables[0];

            nParam = "11";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox5.SelectedValue = ObjParametro.Valor.ToString();
            }

            //Cantidad de Articulos
            nParam = "14";
            double vCantidad = 0;
            if (ObjParametro.BuscarParametro(nParam))
            {
                vCantidad = Double.Parse(ObjParametro.Valor);
            }
            else
            {
                vCantidad = 0;
            }

            if (vCantidad.ToString().Trim().Equals("0"))
                textBox4.Text = "0";
            else
                textBox4.Text = vCantidad.ToString("###,##0").Trim();

            //Carga Tipo de Venta

            string codCatTVenta = "022";
            string tipVentaTip = "1";
            DataSet datosTipVenta = csql.dataset("Call SpCargarDetCat('" + codCatTVenta + "','" + tipVentaTip + "')");
            comboBox6.DisplayMember = "Descripcion";
            comboBox6.ValueMember = "CodDetCat";
            comboBox6.DataSource = datosTipVenta.Tables[0];

            nParam = "15";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox6.SelectedValue = ObjParametro.Valor.ToString();
            }

            nParam = "16";
            //Ruta del certificado
            string vRuta = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vRuta = ObjParametro.Valor;
            }

            textBox5.Text = vRuta.ToString();

            nParam = "17";
            //Pass del certificado
            string vPass = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vPass = ObjParametro.Valor;
            }
            textBox6.Text = vPass;

            nParam = "18";
            //RUC SOL
            string vRuc = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vRuc = ObjParametro.Valor;
            }
            textBox7.Text = vRuc;

            nParam = "19";
            //User SOL
            string vUserSOL = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vUserSOL = ObjParametro.Valor;
            }
            textBox8.Text = vUserSOL;

            nParam = "20";
            //User SOL
            string vPassSOL = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vPassSOL = ObjParametro.Valor;
            }
            textBox9.Text = vPassSOL;

            nParam = "21";
            //SERVICIO web
            string archivo = System.Environment.CurrentDirectory + @"\base.ini";
            cini ciniarchivo = new cini(archivo);
            string vUrlServ = "";
            string vServWeb = "";
            string vUrlApiSunat = "";
            string vAccesoBD = "";
            string vUrlApiBD = "";
            vServWeb = ciniarchivo.ReadValue("Configura", "Service", "");
            vUrlServ = ciniarchivo.ReadValue("Configura", "UrlService", "");
            vUrlApiSunat = ciniarchivo.ReadValue("Configura", "UrlApi", "");
            vAccesoBD = ciniarchivo.ReadValue("BDRemoto", "Acceso", "");
            vUrlApiBD = ciniarchivo.ReadValue("BDRemoto", "UrlBD", "");
            /*if (ObjParametro.BuscarParametro(nParam))
            {
                vServWeb = ObjParametro.Valor;
            }*/

            comboBox7.Text = vServWeb;
            textBox10.Text = vUrlApiSunat;
            if (vAccesoBD.Equals("SI"))
            {
                checkBox1.Checked = true;
                textBox11.Text = vUrlApiBD;
            }
            else
            {
                checkBox1.Checked = false;
                textBox11.Text = "";
            }

            nParam = "23";
            //Correo
            string vCorreo = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vCorreo = ObjParametro.Valor;
            }
            textBox13.Text = vCorreo;

            nParam = "24";
            //Pass Correo
            string vPassCorreo = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vPassCorreo = ObjParametro.Valor;
            }
            textBox12.Text = vPassCorreo;

            nParam = "25";
            //Puerto smtp
            string vPuertoSmtp = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vPuertoSmtp = ObjParametro.Valor;
            }
            textBox14.Text = vPuertoSmtp;

            nParam = "29";
            //Dimenciones
            string vDimension = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vDimension = ObjParametro.Valor;
                if (vDimension.Equals("1"))
                {
                    ckDimensiones.Checked = true;
                }
                else
                {
                    ckDimensiones.Checked = false;
                }
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
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
                textBox2.Focus();
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox4.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
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
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox5.Focus();
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox2.Text.ToString().Equals("") ? "0" : textBox2.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0.00"))
                    textBox2.Text = "";
                else
                    textBox2.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox2.Focus();
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0.00"))
                    textBox1.Text = "";
                else
                    textBox1.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox1.Focus();
            }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0.00"))
                    textBox3.Text = "";
                else
                    textBox3.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox3.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox5.TextLength == 0 || textBox5.Equals("0"))
            {
                MessageBox.Show("Ingrese ruta del Certificado Electrónico", "SISTEMA");
                textBox5.Focus();
                return;
            }

            if (textBox6.TextLength == 0 || textBox6.Equals("0"))
            {
                MessageBox.Show("Ingrese contraseña del Certificado Electrónico", "SISTEMA");
                textBox6.Focus();
                return;
            }

            if (textBox7.TextLength == 0 || textBox7.Equals("0"))
            {
                MessageBox.Show("Ingrese RUC del portal SOL - SUNAT", "SISTEMA");
                textBox7.Focus();
                return;
            }

            if (textBox8.TextLength == 0 || textBox8.Equals("0"))
            {
                MessageBox.Show("Ingrese usuario del portal SOL - SUNAT", "SISTEMA");
                textBox8.Focus();
                return;
            }

            if (textBox9.TextLength == 0 || textBox9.Equals("0"))
            {
                MessageBox.Show("Ingrese contraseña para el portal SOL - SUNAT", "SISTEMA");
                textBox9.Focus();
                return;
            }

            if (comboBox7.Text == "")
            {
                MessageBox.Show("Seleccione Url del Servicio", "SISTEMA");
                comboBox7.Focus();
                return;
            }

            if (textBox10.TextLength > 0 && textBox10.TextLength <= 10)
            {
                MessageBox.Show("Ingrese una Url de API que sea válido", "SISTEMA");
                textBox10.Focus();
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar los parámetros del sistema", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            Boolean nValT = false;
            strMensaje = "";

            // Modificando Ruta del Certificado
            string Usuario = FrmLogin.x_login_usuario;
            ObjParametro.Id = "16";
            /*string RutaArchivo = "";
            RutaArchivo = Path.Combine(textBox5.Text);
            ObjParametro.Valor = RutaArchivo.ToString();
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = "No se registro correctamente el valor de la ruta del certificado electrónico" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }*/

            using (MySqlConnection conn = GetNewConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE tblparametro set valor = @valor where Id = @Id";
                    cmd.Parameters.AddWithValue("@valor", textBox5.Text);
                    cmd.Parameters.AddWithValue("@Id", "16");
                    cmd.ExecuteNonQuery();
                }
            }

            // Modificando Pass del Certificado
            ObjParametro.Id = "17";
            ObjParametro.Valor = textBox6.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor de la contraseña del certificado electrónico" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            // Modificando RUC SOL
            ObjParametro.Id = "18";
            ObjParametro.Valor = textBox7.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor del RUC para el portal SOL - SUNAT" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            // Modificando usuario SOL
            ObjParametro.Id = "19";
            ObjParametro.Valor = textBox8.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor del usuario para el portal SOL - SUNAT" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            // Modificando pass SOL
            ObjParametro.Id = "20";
            ObjParametro.Valor = textBox9.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor de la contraseña para el portal SOL - SUNAT" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            // Modificando Correo
            ObjParametro.Id = "23";
            ObjParametro.Valor = textBox13.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor del correo" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            // Modificando Pass Correo
            ObjParametro.Id = "24";
            ObjParametro.Valor = textBox12.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor del password del correo" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            // Modificando Puerto Smptp
            ObjParametro.Id = "25";
            ObjParametro.Valor = textBox14.Text;
            ObjParametro.UserModi = Usuario.ToString();
            if (ObjParametro.Modificar())
            {
                nValT = true;
            }
            else
            {
                strMensaje = strMensaje + "No se registro correctamente el valor del puerto smtp del correo" + "\n";
                //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                //return;
            }

            // Modificando Servicio Web
            try
            {
                string archivo = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivo);

                ciniarchivo.WriteValue("Configura", "Service", comboBox7.Text);

                /*ObjParametro.Id = "21";
                ObjParametro.Valor = comboBox7.Text;
                ObjParametro.UserModi = Usuario.ToString();
                if (ObjParametro.Modificar())
                {
                    nValT = true;
                }
                else
                {
                    strMensaje = strMensaje + "No se registro correctamente el valor del servicio Web SUNAT" + "\n";
                    //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                    //return;
                }*/

                string vCodCat = "024";
                ObjDetCatalogo.BuscarDetCatalogoCod(vCodCat.ToString(), comboBox7.SelectedValue.ToString());

                ciniarchivo.WriteValue("Configura", "UrlService", ObjDetCatalogo.Descripcion);

                /*ObjParametro.Id = "22";
                ObjParametro.Valor = ObjDetCatalogo.Descripcion;
                ObjParametro.UserModi = Usuario.ToString();
                if (ObjParametro.Modificar())
                {
                    nValT = true;
                }
                else
                {
                    strMensaje = strMensaje + "No se registro correctamente el valor del servicio Web SUNAT" + "\n";
                    //MessageBox.Show("No se registro correctamente el valor de Moneda", "SISTEMA");
                    //return;
                }

                if (strMensaje.Equals(""))
                {
                    MessageBox.Show("Datos grabados correctamente", "SISTEMA");
                }
                else
                {
                    MessageBox.Show(strMensaje, "SISTEMA");
                }*/

                ciniarchivo.WriteValue("Configura", "UrlApi", textBox10.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Hubo problemas al leer el archivo INI. " + ex.Message, "SISTEMA");
            }
        }

        /*private string ValorSeleccionado()
        {
            var direccionSunat = direccionSunatBindingSource.Current as DireccionSunat;
            return direccionSunat == null ? string.Empty : direccionSunat.Descripcion;
        }*/

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivo |*.pfx";
            openFileDialog1.Title = "Abrir archivo";
            openFileDialog1.AddExtension = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //openFileDialog1.FileName = $"{vNomXml}.xml";
            //openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog1.FileName;
            }
        }

        private static MySqlConnection GetNewConnection()
        {
            //const string MySqlConnecionString = "Server=" + FrmLogin.XServidor + "; Database= " + FrmLogin.XDB + "; Username=" + FrmLogin.XUser + "; Password=" + FrmLogin.XPassword + ";";

            var conn = new MySqlConnection("Server=" + FrmLogin.XServidor + "; Database= " + FrmLogin.XDB + "; Username=" + FrmLogin.XUser + "; Password=" + FrmLogin.XPassword + ";SslMode=" + FrmLogin.x_SslMode + ";");
            conn.Open();
            return conn;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox11.Enabled = true;
            }
            else
            {
                textBox11.Enabled = true;
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;
        }
    }
}