using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using tessnet2;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SisBicimotoApp
{
    public partial class FrmCancelarVenta : Form, ICliente
    {
        public ICliente Opener { get; set; }

        public IVenta OpenerVenta { get; set; }

        private ClsParametro ObjParametro = new ClsParametro();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsApiExterna ApiExterna = new ClsApiExterna();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string Usuario = FrmLogin.x_login_usuario;
        public static char nmCLi = 'N';
        private string captcha = "";
        private CookieContainer cokkie = new CookieContainer();

        private string vComp = "";
        private Double vTotal = 0;

        public FrmCancelarVenta()
        {
            InitializeComponent();
        }

        #region ICliente Members

        public void SelectItem(string tipCod, string codCli)
        {
            comboBox1.Text = tipCod;
            textBox2.Text = codCli;
        }

        #endregion ICliente Members

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BusCliente(string vTipDoc, string vRuc, string vRucEmpresa)
        {
            if (ObjCliente.BuscarCLiente(vRuc, vRucEmpresa))
            {
                comboBox1.Text = ObjCliente.TipDoc.ToString().Trim();
                textBox1.Text = ObjCliente.Nombre.ToString().Trim();
                textBox3.Text = ObjCliente.Direccion.ToString().Trim();
                textBox4.Text = ObjCliente.Telefono.ToString().Trim();
            }
            else
            {
                textBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void BusRuc(string vTipDoc)
        {
            if (ApiExterna.ObtenerRazonSocial(vTipDoc))
            {
                textBox1.Text = ApiExterna.nombreORazonSocial.ToString().Trim();
                textBox3.Text = ApiExterna.direccion.ToString().Trim();
                string estado = ApiExterna.estado.ToString().Trim();
                if (!estado.Equals("ACTIVO"))
                {
                    MessageBox.Show("El numero de RUC se encuentra Inactivo", "SISTEMA");
                }
            }
            else
            {
                textBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void BusDni(string vTipDoc)
        {
            if (ApiExterna.ObtenerNombreCompleto(vTipDoc))
            {
                textBox1.Text = ApiExterna.nombreCompleto.ToString().Trim();
            }
            else
            {
                textBox1.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void FrmCancelarVenta_Load(object sender, EventArgs e)
        {
            label14.Text = FrmLogin.x_login_usuario;


            //Buscar comprobantes autocorrelativos
            string vModulo = "VEN";
            DataSet datosDoc = csql.dataset("Call SpDocBusElectronicos('" + vModulo.ToString() + "')");
            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "Codigo";
            comboBox2.DataSource = datosDoc.Tables[0];

            string nParam = "6";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox2.SelectedValue = ObjParametro.Valor.ToString();
                //comboBox2.Enabled = false;
                if (comboBox2.Text.Equals(""))
                {
                    //datosDoc = csql.dataset("Call SpDocBusAutonumerico('" + vModulo.ToString() + "')");
                    datosDoc = csql.dataset("Call SpDocBusElectronicos('" + vModulo.ToString() + "')");
                    comboBox2.DisplayMember = "nombre";
                    comboBox2.ValueMember = "Codigo";
                    comboBox2.DataSource = datosDoc.Tables[0];
                }
            }
            //Buscar tipo de Venta
            string vCodCat = "015";
            string vCodDetCat = "001";
            ObjDetCatalogo.BuscarDetCatalogoCod(vCodCat.ToString(), vCodDetCat.ToString());
            label7.Text = ObjDetCatalogo.Descripcion;


            //Buscar tipo de pago
            //Carga Tipo Codigo de barras
            string codCatBarras = "028";
            string tipBarras = "1";
            DataSet datosbarras = csql.dataset("Call SpCargarDetCat('" + codCatBarras + "','" + tipBarras + "')");

            cmbPago.DisplayMember = "Descripcion";
            cmbPago.ValueMember = "CodDetCat";
            cmbPago.DataSource = datosbarras.Tables[0];

            //Cargar N° de Serie
            label11.Text = FrmVentasEnLinea.x_Serie.ToString();

            //Buscar Autogenerado
            vComp = comboBox2.SelectedValue.ToString();
            string vSerie = label11.Text.ToString().Trim();
            if (ObjSerie.BuscarDocSerie(vComp, vSerie))
            {
                if (ObjSerie.Correla.Equals("S"))
                {
                    label12.Text = "AUTOGENERADO";
                }
                else
                {
                    label12.Text = "NO ESPECIFICA";
                }
            }
            else
            {
                label12.Text = "NO ESPECIFICA";
            }

            //Carga Documento
            string codCatTipDoc = "018";
            string tipPresDoc = "2";
            DataSet datosDocu = csql.dataset_cadena("Call SpCargarDetCat('" + codCatTipDoc + "','" + tipPresDoc + "')");
            comboBox1.DisplayMember = "DescCorta";
            comboBox1.ValueMember = "CodDetCat";
            comboBox1.DataSource = datosDocu.Tables[0];

            //comboBox2.Focus();
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
                if (textBox1.Enabled == true)
                {
                    textBox1.Focus();
                }
                else
                {
                    button6.Focus();
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string tipDoc = comboBox1.SelectedValue.ToString();
            string codRucCli = textBox2.Text.ToString().Trim();

            BusCliente(tipDoc, codRucCli, rucEmpresa);

            //Solo para comprobante electronico

            if (textBox2.TextLength == 11)
            {
                comboBox2.SelectedValue = "014";
            }
            else
            {
                comboBox2.SelectedValue = "013";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBusCliente frmBusCliente = new FrmBusCliente();
            frmBusCliente.WindowState = FormWindowState.Normal;
            frmBusCliente.Opener = this;
            frmBusCliente.MdiParent = this.MdiParent;
            frmBusCliente.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0 && textBox2.TextLength < 8)
            {
                MessageBox.Show("Ingrese Codigo de Cliente (DNI) válido", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (textBox1.TextLength == 0 && textBox2.TextLength == 8)
            {
                MessageBox.Show("Ingrese Codigo de Cliente ", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox2.TextLength > 8 && textBox2.TextLength < 11)
            {
                MessageBox.Show("Ingrese Codigo de Cliente (RUC) válido", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (textBox2.TextLength == 0 && textBox2.TextLength == 11)
            {
                MessageBox.Show("Ingrese Codigo de Cliente", "SISTEMA");
                textBox2.Focus();
                return;
            }

            vTotal = FrmVentasEnLinea.v_Total;
            if (vTotal > 500 && textBox2.TextLength == 0 && textBox4.TextLength == 0)
            {
                MessageBox.Show("Monto Mayor a S/.500, Por favor ingresar N° Doc de Cliente y su numero de teléfono", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (label12.Text.Equals("NO ESPECIFICA"))
            {
                MessageBox.Show("Seleccione un número de serie autocorrelativo para el comprobante seleccionado", "SISTEMA");
                comboBox2.Focus();
                return;
            }

            string vDoc = comboBox2.SelectedValue.ToString();
            string vPref = "";

            if (ObjSerie.BuscarDocSerie(vDoc, label11.Text))
            {
                vPref = ObjSerie.PrefijoSerie;

                if (vPref.Equals("F") && (textBox2.TextLength < 11))
                {
                    MessageBox.Show("Debe seleccionar un número de RUC para este comprobante", "SISTEMA");
                    textBox2.Focus();
                    return;
                }
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar la Venta", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            string cliente = "";
            string serie = "";
            string comprobante = "";
            string vTipDocCliente = "";
            string tipoPago = "";
            //string vVN = "";
            string vNombre = "";
            string vApel = "";
            string vCelular = "";
            cliente = textBox2.Text.Trim();
            serie = label11.Text.Trim();
            comprobante = comboBox2.SelectedValue.ToString();
            vTipDocCliente = comboBox1.SelectedValue.ToString();
            tipoPago = cmbPago.SelectedValue.ToString();
            vNombre = textBox1.Text;
            vApel = textBox3.Text;
            vCelular = textBox4.Text;

            if (textBox1.Enabled == true && textBox1.TextLength > 0)
            {
                int vLong = cliente.Length;
                switch (vLong)
                {
                    case 8:
                        ObjCliente.TipDoc = "1";
                        vTipDocCliente = "1";
                        ObjCliente.RucDni = cliente.ToString();
                        break;

                    case 11:
                        ObjCliente.TipDoc = "6";
                        vTipDocCliente = "6";
                        ObjCliente.RucDni = cliente.ToString();
                        break;

                    case 0:
                        ObjCliente.TipDoc = "0";
                        vTipDocCliente = "0";
                        ObjCliente.RucDni = cliente.ToString();
                        break;

                    default:
                        if (textBox1.TextLength > 2)
                        {
                            vTipDocCliente = "0";
                            int vValor = 0;
                            ObjCliente.TipDoc = "0";
                            ObjCliente.RucDni = cliente.ToString();
                            string vNumero = "";
                            if (ObjCliente.BuscarCLienteNum(rucEmpresa.ToString()))
                            {
                                vValor = ObjCliente.ValorCli + 1;
                            }

                            vNumero = "C" + vValor.ToString("0000000000").Trim();
                            //------------------
                            ObjCliente.RucDni = vNumero.ToString();
                            cliente = ObjCliente.RucDni;
                        }
                        else
                        {
                            ObjCliente.TipDoc = "0";
                            vTipDocCliente = " ";
                            //Buscar ultimo cliente
                            int vValor = 0;
                            string vNumero = "";
                            if (ObjCliente.BuscarCLienteNum(rucEmpresa.ToString()))
                            {
                                vValor = ObjCliente.ValorCli + 1;
                            }

                            vNumero = "C" + vValor.ToString("0000000000").Trim();
                            //------------------
                            ObjCliente.RucDni = vNumero.ToString();
                            cliente = ObjCliente.RucDni;
                        }
                        break;
                }
                ObjCliente.Nombre = vNombre.ToString();
                ObjCliente.Direccion = vApel.ToString();
                ObjCliente.Telefono = vCelular.ToString(); 
                ObjCliente.Celular = vCelular.ToString();
                ObjCliente.Contacto = "";
                ObjCliente.Email = "";
                ObjCliente.DireccionEnvio = "";
                ObjCliente.Region = "";
                ObjCliente.Provincia = "";
                ObjCliente.Distrito = "";
                ObjCliente.LimCredito = 0;
                ObjCliente.CodVendedor = "";
                ObjCliente.Est = "A";
                ObjCliente.UsuarioCrea = Usuario.ToString();
                ObjCliente.UsuarioModi = Usuario.ToString();
                ObjCliente.RucEmpresa = rucEmpresa.ToString();
                ObjCliente.TipDocAnt = vTipDocCliente;
                ObjCliente.rucdniAnt = textBox2.Text;

                FrmCancelarVenta.nmCLi = 'N';
                if (ObjCliente.ValidarCliente(textBox2.Text.Trim(), rucEmpresa.ToString()))
                {
                     
                }else
                {
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
            }
            OpenerVenta.GuardarVenta(cliente, vTipDocCliente, serie, comprobante, tipoPago);
            this.Close();
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Buscar Autogenerado
            if (!comboBox2.Text.Equals(""))
            {
                vComp = comboBox2.SelectedValue.ToString();
                string vSerie = label11.Text.ToString().Trim();
                if (ObjSerie.BuscarDocSerie(vComp, vSerie))
                {
                    if (ObjSerie.Correla.Equals("S"))
                    {
                        label12.Text = "AUTOGENERADO";
                    }
                    else
                    {
                        label12.Text = "NO ESPECIFICA";
                    }
                }
                else
                {
                    label12.Text = "NO ESPECIFICA";
                }
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmBusCliente frmBusCliente = new FrmBusCliente();
                frmBusCliente.WindowState = FormWindowState.Normal;
                frmBusCliente.Opener = this;
                frmBusCliente.MdiParent = this.MdiParent;
                frmBusCliente.Show();
            }

        }

        private void label14_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmAddClienteVentLin frmAddClienteVentLin = new FrmAddClienteVentLin();
            frmAddClienteVentLin.WindowState = FormWindowState.Normal;
            frmAddClienteVentLin.Opener = this;
            frmAddClienteVentLin.MdiParent = this.MdiParent;
            frmAddClienteVentLin.ShowDialog(this);
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                FrmAddClienteVentLin frmAddClienteVentLin = new FrmAddClienteVentLin();
                frmAddClienteVentLin.WindowState = FormWindowState.Normal;
                frmAddClienteVentLin.Opener = this;
                frmAddClienteVentLin.MdiParent = this.MdiParent;
                frmAddClienteVentLin.ShowDialog(this);
            }
        }

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox2.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (//char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
                                        //char.IsWhiteSpace(e.KeyChar))  //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pintuacion
                e.Handled = true;
            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button6.Focus();
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
        }

        private void label15_Click(object sender, EventArgs e)
        {
        }

        private void genCaptcha()
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                request.CookieContainer = cokkie;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                var image = new Bitmap(responseStream);
                var ocr = new Tesseract();
                ocr.Init(@"D:\Bicimoto Repositorio\bicimotopucallpa\SisBicimotoApp\Content\tessdata", "eng", false);

                var result = ocr.DoOCR(image, Rectangle.Empty);
                foreach (Word word in result)
                {
                    captcha = word.Text;
                }
            }
            catch (Exception ex)
            {
                //mensaje de error
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


                File.ReadLines(lineRead)
                .SkipWhile(line => !line.Contains("nombre"))
                .Skip(1) // optional
                .TakeWhile(line => !line.Contains("ruc"));



                while (lineRead.Contains("  "))
                 {
                     lineRead = lineRead.Replace("  ", " ");
                 }

                return lineRead;

            }
            catch (Exception ex)
            {
                return "NO SE ENCONTRO RESULTADO" +  ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string codRucCli = textBox2.Text.ToString().Trim();
                BusRuc(codRucCli);
            }
            catch (Exception ex)
            {
                MessageBox.Show("El Servicio de Consulta RUC SUNAT no se encuentra disponible, intentelo mas tarde. " + ex.Message);
            }
        }

        public static void DeserializeJsonFile(string datos)
        {
            //var Datos = JsonConvert.DeserializeObject(datos);
            JsonTextReader lee = new JsonTextReader(new StringReader(datos));
            string nombre = string.Empty;

            while (lee.Read())
            {
                if(string.IsNullOrEmpty(nombre))
                {
                   if(lee.Value != null && lee.TokenType == JsonToken.Date)
                    {
                        nombre = DateTime.Parse(lee.Value.ToString()).ToShortDateString();
                    }
                }
               
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            try
            {
                string codRucCli = textBox2.Text.ToString().Trim();
                BusDni(codRucCli);
            }
            catch (Exception ex)
            {
                MessageBox.Show("El Servicio de Consulta DNI RENIEC no se encuentra disponible, intentelo mas tarde. " + ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Para emitir una proforma, debe ingresar DNI o RUC del cliente", "SISTEMA");
                textBox2.Focus();
                return;
            }
            vTotal = FrmVentasEnLinea.v_Total;
            if (vTotal > 500 && textBox2.TextLength == 0 && textBox4.TextLength == 0)
            {
                MessageBox.Show("Monto Mayor a S/.500, Por favor ingresar N° Doc de Cliente y su numero de teléfono", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (textBox2.TextLength == 8 && textBox4.TextLength == 0)
            {
               MessageBox.Show("Ingrese número de Teléfono del cliente" , "SISTEMA");
               textBox4.Focus();
               return;
                
            }

            if (textBox2.TextLength == 11 && textBox4.TextLength == 0)
            {
                MessageBox.Show("Ingrese número de Teléfono del cliente" , "SISTEMA");
                textBox4.Focus();
                return;
            }

            string vDoc = "028";
            string vPref = "";

            if (ObjSerie.BuscarDocSerie(vDoc, label11.Text))
            {
                vPref = ObjSerie.PrefijoSerie;

                if (vPref.Equals("F") && (textBox2.TextLength < 11))
                {
                    MessageBox.Show("Debe seleccionar un número de RUC para este comprobante", "SISTEMA");
                    textBox2.Focus();
                    return;
                }
            }

            if (MessageBox.Show("Datos Correctos, se procedera a guardar la Venta", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            string cliente = "";
            string serie = "";
            string comprobante = "";
            string vTipDoc = "";
            //string vVN = "";
            string vNombre = "";
            string vApel = "";
            string vCelular = "";
            cliente = textBox2.Text.Trim();
            serie = "001";
            comprobante = "028";
            vTipDoc = comboBox1.SelectedValue.ToString();
            vNombre = textBox1.Text;
            vApel = textBox3.Text;
            vCelular = textBox4.Text;

            if (textBox1.Enabled == true && textBox1.TextLength > 0)
            {
                int vLong = cliente.Length;
                switch (vLong)
                {
                    case 8:
                        ObjCliente.TipDoc = "1";
                        vTipDoc = "1";
                        ObjCliente.RucDni = cliente.ToString();
                        break;

                    case 11:
                        ObjCliente.TipDoc = "6";
                        vTipDoc = "6";
                        ObjCliente.RucDni = cliente.ToString();
                        break;

                    default:
                        if (textBox1.TextLength > 2)
                        {
                            vTipDoc = "1";
                            int vValor = 0;
                            ObjCliente.TipDoc = "6";
                            ObjCliente.RucDni = cliente.ToString();
                            string vNumero = "";
                            if (ObjCliente.BuscarCLienteNum(rucEmpresa.ToString()))
                            {
                                vValor = ObjCliente.ValorCli + 1;
                            }

                            vNumero = "C" + vValor.ToString("0000000000").Trim();
                            //------------------
                            ObjCliente.RucDni = vNumero.ToString();
                            cliente = ObjCliente.RucDni;
                        }
                        else
                        {
                            ObjCliente.TipDoc = "6";
                            vTipDoc = "";
                            //Buscar ultimo cliente
                            int vValor = 0;
                            string vNumero = "";
                            if (ObjCliente.BuscarCLienteNum(rucEmpresa.ToString()))
                            {
                                vValor = ObjCliente.ValorCli + 1;
                            }

                            vNumero = "C" + vValor.ToString("0000000000").Trim();
                            //------------------
                            ObjCliente.RucDni = vNumero.ToString();
                            cliente = ObjCliente.RucDni;
                        }
                        break;
                }
                ObjCliente.Nombre = vNombre.ToString();
                ObjCliente.Direccion = vApel.ToString();
                ObjCliente.Telefono = "";
                ObjCliente.Celular = vCelular.ToString();
                ObjCliente.Contacto = "";
                ObjCliente.Email = "";
                ObjCliente.DireccionEnvio = "";
                ObjCliente.Region = "";
                ObjCliente.Provincia = "";
                ObjCliente.Distrito = "";
                ObjCliente.LimCredito = 0;
                ObjCliente.CodVendedor = "";
                ObjCliente.Est = "A";
                ObjCliente.UsuarioCrea = Usuario.ToString();
                ObjCliente.UsuarioModi = Usuario.ToString();
                ObjCliente.RucEmpresa = rucEmpresa.ToString();
                ObjCliente.TipDocAnt = vTipDoc;
                ObjCliente.rucdniAnt = textBox2.Text;
                if (checkBox1.Checked == false)
                {
                    FrmCancelarVenta.nmCLi = 'M';
                }
                else if (checkBox1.Checked == true)
                {
                    FrmCancelarVenta.nmCLi = 'N';
                    if (ObjCliente.ValidarCliente(textBox2.Text.Trim(), rucEmpresa.ToString()))
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
            }

            this.Close();
            OpenerVenta.GuardarProforma(cliente, vTipDoc, serie, comprobante);
        }

        private void button12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                textBox2.Focus();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        //{
        //    return true;
        //}
    }
}