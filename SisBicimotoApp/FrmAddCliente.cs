using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddCliente : Form
    {
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private CookieContainer cokkie = new CookieContainer();

        public FrmAddCliente()
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
            try
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
            //Carga Documento
            string codCatTipDoc = "018";
            string tipPresDoc = "2";
            DataSet datosDoc = csql.dataset_cadena("Call SpCargarDetCat('" + codCatTipDoc + "','" + tipPresDoc + "')");
            comboBox5.DisplayMember = "DescCorta";
            comboBox5.ValueMember = "CodDetCat";
            comboBox5.DataSource = datosDoc.Tables[0];

            //Carga Region
            DataSet datos = csql.dataset("Call SpCargarRegion()");
            comboBox1.DisplayMember = "NOMBRE";
            comboBox1.ValueMember = "CODDPTO";
            comboBox1.DataSource = datos.Tables[0];
            comboBox1.Text = "";

            //Carga Vendedor
            DataSet datosVend = csql.dataset("Call SpClienteVendedor('" + rucEmpresa.ToString() + "')");
            comboBox4.DisplayMember = "NOMBRE";
            comboBox4.ValueMember = "CODVEND";
            comboBox4.DataSource = datosVend.Tables[0];
            comboBox4.Text = "";

            if (FrmCliente.nmCLi == 'N') //Verifica si el suario ha seleccinado el boton Nuevo
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
                // textBox1.Enabled = false;
                //comboBox5.Enabled = false;
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
                textBox6.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox7.Focus();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                comboBox2.Focus();
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
                textBox9.Focus();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < textBox9.Text.Length; i++)
            {
                if (textBox9.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    //return;
                }
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
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
        }

        private void comboBox1_Validated(object sender, EventArgs e)
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

        private void comboBox2_TextChanged(object sender, EventArgs e)
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

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox9_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox9.Text.ToString().Equals("") ? "0" : textBox9.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox9.Text = "";
                else
                    textBox9.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox9.Focus();
            }
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
            ObjCliente.Telefono = textBox4.Text.Trim();
            ObjCliente.Celular = textBox5.Text.Trim();
            ObjCliente.Contacto = textBox6.Text.Trim();
            ObjCliente.Email = textBox7.Text.Trim();
            ObjCliente.DireccionEnvio = textBox8.Text.Trim();
            ObjCliente.Region = comboBox1.Text.ToString().Equals("") ? "" : comboBox1.SelectedValue.ToString();
            ObjCliente.Provincia = comboBox2.Text.ToString().Equals("") ? "" : comboBox2.SelectedValue.ToString();
            ObjCliente.Distrito = comboBox3.Text.ToString().Equals("") ? "" : comboBox3.SelectedValue.ToString();
            ObjCliente.LimCredito = double.Parse(textBox9.Text.ToString().Equals("") ? "0" : textBox9.Text.ToString().Trim());
            ObjCliente.CodVendedor = comboBox4.Text.ToString().Equals("") ? "" : comboBox4.SelectedValue.ToString();
            ObjCliente.UsuarioCrea = Usuario.ToString().Trim();
            ObjCliente.UsuarioModi = Usuario.ToString().Trim();
            ObjCliente.Est = (checkBox1.Checked == true) ? "A" : "N";
            ObjCliente.RucEmpresa = rucEmpresa.ToString();
            string vParam = "2";
            string vCodCat = "018";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), FrmCliente.tipdoc, vParam.ToString());
            ObjCliente.TipDocAnt = ObjDetCatalogo.CodDetCat;
            ObjCliente.rucdniAnt = FrmCliente.cod;
            if (FrmCliente.nmCLi == 'N')
            {
                if (ObjCliente.ValidarCliente(textBox1.Text.Trim(), rucEmpresa.ToString()))
                {
                    MessageBox.Show("Cliente ya existe, ingrese otro Cliente", "SISTEMA");
                    textBox1.Focus();
                    return;
                }

                if (ObjCliente.Crear())
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
                if (ObjCliente.Modificar())
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

        private string getDatafromXML2(string lineRead)
        {
            try
            {
                string xmlcontenido = File.ReadAllText(lineRead);

                return xmlcontenido;

                /* lineRead = lineRead.Trim();
                 //lineRead = lineRead.Remove(0, len);
                 lineRead = lineRead.Replace("{", "");
                 lineRead = lineRead.Replace("}", "");
                 lineRead = lineRead.Replace("</td>", "");
                 lineRead = lineRead.Replace("\\u00d1", "Ñ");
                 lineRead = lineRead.Replace("\\u00ba", "º");
                 lineRead = lineRead.Replace("\"", "");
                 lineRead = lineRead.Replace(",", "");

                 while (lineRead.Contains("  "))
                 {
                     lineRead = lineRead.Replace("  ", " ");
                 }
                 return lineRead;*/
            }
            catch (Exception ex)
            {
                return "NO SE ENCONTRO RESULTADO";
            }
        }

        private string getDatafromXML(string lineRead, int len = 0)
        {
            try
            {
                lineRead = lineRead.Trim();
                lineRead = lineRead.Remove(0, len);
                lineRead = lineRead.Replace("</td>", "");
                lineRead = lineRead.Replace("\\u00d1", "Ñ");
                lineRead = lineRead.Replace("\\u00ba", "º");
                lineRead = lineRead.Replace("\"", "");
                lineRead = lineRead.Replace(",", "");

                while (lineRead.Contains("  "))
                {
                    lineRead = lineRead.Replace("  ", " ");
                }
                return lineRead;
            }
            catch (Exception ex)
            {
                return "NO SE ENCONTRO RESULTADO";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                /* Process.Start("http://e-consultaruc.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias");
                 ////genCaptcha();

                 // //string myurl = @"http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc=" + textBox2.Text.Trim() + "&codigo=" + captcha.Trim().ToUpper() + "&tipdoc=1";*/
                string myurl = @"https://api.sunat.cloud/ruc/" + textBox2.Text.Trim();
                ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myurl);
                myWebRequest.CookieContainer = cokkie;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myStream = myhttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myStream);
                string xDat = ""; int pos = 0;
                string razon = "";
                string direccion = "";

                while (!myStreamReader.EndOfStream)
                {
                    xDat = myStreamReader.ReadLine();
                    pos++;

                    switch (pos)
                    {
                        case 3:
                            razon = getDatafromXML(xDat, 15);
                            break;

                        case 11:
                            direccion = getDatafromXML(xDat, 20);
                            break;
                    }
                    /* switch (pos)
                     {
                         case 132:
                             textBox1.Text = getDatafromXML(xDat, 25);
                             break;

                         case 171:
                             textBox3.Text = getDatafromXML(xDat, 25);
                             break;
                     }*/
                }

                textBox2.Text = razon;
                textBox3.Text = direccion;
            }
            catch (Exception ex)
            {
                MessageBox.Show("El Servicio de Consulta RUC SUNAT no se encuentra disponible, intentelo mas tarde. " + ex.Message);
            }
        }

        public static bool ValidateServerCertificate(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                string myurl = @"https://py-devs.com/api/dni/" + textBox1.Text.Trim();// + @"/?format=api";///api/dni/71242488/?format=api
                ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myurl);
                myWebRequest.CookieContainer = cokkie;

                HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myStream = myhttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myStream);
                string xDat = ""; int pos = 0;
                while (!myStreamReader.EndOfStream)
                {
                    xDat = myStreamReader.ReadToEnd();
                    // textBox2.Text = getDatafromXML2(xDat);
                    pos++;
                    String apellidoPA = "";
                    String apellidoMA = "";
                    String Nombre = "";
                    String Consulta;
                    switch (pos)
                    {
                        case 4:
                            apellidoPA = getDatafromXML(xDat, 4);
                            break;

                        case 5:
                            apellidoMA = getDatafromXML(xDat, 5);
                            break;

                        case 6:
                            Nombre = getDatafromXML(xDat, 4);
                            break;
                    }
                    Consulta = apellidoPA + apellidoMA + Nombre;
                    textBox2.Text = Consulta;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("La consulta DNI no se encuentra disponible" + ex.Message);
            }
        }
    }
}