using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.IO;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmEnviaXmlBaja : Form, IEnvio
    {
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsComunicacionBaja ObjComunicacionBaja = new ClsComunicacionBaja();
        private ClsEnvio ObjEnvio = new ClsEnvio();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public string RutaArchivo { get; set; }

        public FrmEnviaXmlBaja()
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

        private void FrmEnviaXmlBaja_Load(object sender, EventArgs e)
        {
            string nIdEnvio = "";
            string nNumEnvio = "";
            string nomXml = "";
            nIdEnvio = FrmComunicacionBaja.nIdEnv;
            nomXml = FrmComunicacionBaja.nomXml;
            nNumEnvio = FrmComunicacionBaja.nNumEnv;

            label4.Text = nIdEnvio;
            textBox3.Text = nomXml;

            textBox2.Text = "COMUNICACION DE BAJA";

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

            /*string nParam = "21";
            //SERVICIO web
            string vServWeb = "";
            if (ObjParametro.BuscarParametro(nParam))
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string TipoDocumento = "";
                string nomXml = "";

                if (!ObjComunicacionBaja.Buscar(label4.Text.ToString(), FrmComunicacionBaja.nNumEnv, rucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos de envío de comunicación de Baja, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                TipoDocumento = "RA";

                string Trama = "";
                string Ruta = "";
                string fechaAnio = ObjComunicacionBaja.Fecha.Substring(6, 4);
                string fechaMes = ObjComunicacionBaja.Fecha.Substring(3, 2);
                string fechaDia = ObjComunicacionBaja.Fecha.Substring(0, 2);
                string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLNB", $"{rutafec}",
                                    $"{textBox3.Text.ToString()}.xml");

                /*File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(ObjVenta.ArchivoXml));*/

                string vUsuario = FrmLogin.x_login_usuario;

                if (ObjComunicacionBaja.ArchXml.Length > 0)
                {
                    nomXml = ObjComunicacionBaja.ArchXml;
                    /*if (ObjEnvio.BuscarEnvioComp(label4.Text.ToString(), rucEmpresa, FrmVentas.vAlm))
                    {
                        nomXml = ObjEnvio.NomArchXml.ToString();
                    }*/

                    ObjGrabaXML.objEnvio = this;
                    ObjGrabaXML.enviarXMLComunicacion(TipoDocumento, int.Parse(FrmComunicacionBaja.nIdEnv), FrmComunicacionBaja.nNumEnv, RutaArchivo, nomXml, rucEmpresa.ToString(), vUsuario);
                }
                else
                {
                    ObjGrabaXML.generaXMLBaja(label4.Text.ToString(), FrmComunicacionBaja.nNumEnv, rucEmpresa, false);

                    ClsComunicacionBaja ObjComunicacionBaja1 = new ClsComunicacionBaja();
                    if (ObjComunicacionBaja1.Buscar(label4.Text.ToString(), FrmComunicacionBaja.nNumEnv, rucEmpresa))
                    {
                    }

                    nomXml = ObjComunicacionBaja1.ArchXml;

                    //Trama = ClsGrabaXML.vTrama;
                    //Ruta = ObjGrabaXML.RutaArchivo;
                    ObjGrabaXML.objEnvio = this;
                    ObjGrabaXML.enviarXMLComunicacion(TipoDocumento, int.Parse(FrmComunicacionBaja.nIdEnv), FrmComunicacionBaja.nNumEnv, RutaArchivo, nomXml, rucEmpresa.ToString(), vUsuario);
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}