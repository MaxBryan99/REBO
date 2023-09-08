using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.IO;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmEnviaXml : Form, IEnvio
    {
        //public IEnvio ResultEnvio { get; set; }

        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsEnvio ObjEnvio = new ClsEnvio();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public string RutaArchivo { get; set; }

        public FrmEnviaXml()
        {
            InitializeComponent();
        }

        #region IEnvio Members

        public void ResultEnvio(string IdEnvio, string vRespuesta)
        {
            //comboBox1.Text = tipCod;
            textBox1.Text = vRespuesta;
        }

        #endregion IEnvio Members

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmEnviaXml_Load(object sender, EventArgs e)
        {
            string nIdVenta = "";
            string nomXml = "";
            string vDoc = "";

            /*if (FrmVentas.nIdVenta == "" && FrmVentas.nomXml=="" && FrmVentas.vDoc =="")
            {
                nIdVenta = FrmEnviarSunat.nIdVenta;
                nomXml = FrmEnviarSunat.nomXml;
                vDoc = FrmEnviarSunat.vDoc;
            } else
            {*/
            nIdVenta = FrmVentas.nIdVenta;
            nomXml = FrmVentas.nomXml;
            vDoc = FrmVentas.vDoc;
            //}

            label4.Text = nIdVenta;
            textBox3.Text = nomXml;

            if (ObjDocumento.BuscarDoc(vDoc))
            {
                textBox2.Text = ObjDocumento.Nombre.ToString();
            }
            else
            {
                textBox2.Text = "";
            }

            //string nParam = "21";
            //SERVICIO web
            try
            {
                string archivo = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivo);
                string vServWeb = "";
                vServWeb = ciniarchivo.ReadValue("Configura", "Service", "");
                textBox7.Text = vServWeb;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Hubo problemas al leer el archivo INI. " + ex.Message, "SISTEMA");
            }
            /*if (ObjParametro.BuscarParametro(nParam))
            {
                vServWeb = ObjParametro.Valor;
            }*/

            /*if (ObjParametro.BuscarParametro(nParam))
            {
                vServWeb = ObjParametro.Valor;
            }
            textBox7.Text = vServWeb;*/

            //Generar ToolTip

            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 300;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.button1, "Enviar");
            toolTip1.SetToolTip(this.button2, "Salir");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox7.Text.Length == 0)
                {
                    MessageBox.Show("No hay un servicio de SUNAT seleccionado por favor verifique el archivo de configuración.", "SISTEMA");
                    return;
                }
                if (!ObjVenta.BuscarVenta(label4.Text.ToString(), rucEmpresa, FrmVentas.vAlm))
                {
                    MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                string codDoc = "";
                string vMod = "VEN";
                string TipoDocumento = "";

                if (!ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod))
                {
                    MessageBox.Show("Error no se encontró datos de comprobante de Venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    codDoc = ObjDocumento.Codigo;
                }

                switch (codDoc)
                {
                    case "013":
                        TipoDocumento = "03";
                        break;

                    case "014":
                        TipoDocumento = "01";
                        break;
                }

                string Trama = "";
                string Ruta = "";
                string fechaAnio = ObjVenta.Fecha.Substring(6, 4);
                string fechaMes = ObjVenta.Fecha.Substring(3, 2);
                string fechaDia = ObjVenta.Fecha.Substring(0, 2);
                string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"{rutafec}",
                                    $"{textBox3.Text.ToString()}.xml");

                /*File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(ObjVenta.ArchivoXml));*/

                string vUsuario = FrmLogin.x_login_usuario;

                if (ObjVenta.ArchivoXml.Length > 0)
                {
                    ClsVenta ObjVenta1 = new ClsVenta();
                    if (ObjVenta1.BuscarVenta(label4.Text.ToString(), rucEmpresa, FrmVentas.vAlm))
                    {
                        //MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                        //return;
                    }

                    string nomXml = ObjVenta1.ArchivoXml;
                    /*if (ObjEnvio.BuscarEnvioComp(label4.Text.ToString(), rucEmpresa, FrmVentas.vAlm))
                    {
                        nomXml = ObjEnvio.NomArchXml.ToString();
                    }*/

                    ObjGrabaXML.objEnvio = this;
                    ObjGrabaXML.enviarXML(TipoDocumento, label4.Text.ToString(), codDoc, ObjVenta.Serie, ObjVenta.Numero, textBox3.Text.ToString(), RutaArchivo, nomXml, rucEmpresa.ToString(), FrmVentas.vAlm, vUsuario);
                }
                else
                {
                    ObjGrabaXML.generaXMLFactura(label4.Text.ToString(), rucEmpresa, FrmVentas.vAlm, false);

                    ClsVenta ObjVenta1 = new ClsVenta();
                    if (ObjVenta1.BuscarVenta(label4.Text.ToString(), rucEmpresa, FrmVentas.vAlm))
                    {
                        //MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                        //return;
                    }

                    //Trama = ClsGrabaXML.vTrama;
                    //Ruta = ObjGrabaXML.RutaArchivo;
                    ObjGrabaXML.objEnvio = this;
                    ObjGrabaXML.enviarXML(TipoDocumento, label4.Text.ToString(), codDoc, ObjVenta.Serie, ObjVenta.Numero, textBox3.Text.ToString(), RutaArchivo, ObjVenta1.ArchivoXml, rucEmpresa.ToString(), FrmVentas.vAlm, vUsuario);
                }

                //textBox1.Text = ObjGrabaXML.Respuesta;
            }
            catch (Exception ex)
            {
                textBox1.Text = ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
    }
}