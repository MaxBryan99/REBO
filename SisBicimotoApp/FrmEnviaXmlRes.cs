using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.IO;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmEnviaXmlRes : Form, IEnvio
    {
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsResumenEnvio ObjResumenEnvio = new ClsResumenEnvio();
        private ClsEnvio ObjEnvio = new ClsEnvio();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public string RutaArchivo { get; set; }

        public FrmEnviaXmlRes()
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

        private void FrmEnviaXmlRes_Load(object sender, EventArgs e)
        {
            string nIdRes = "";
            string nNumRes = "";
            string nomXml = "";

            /*if (FrmVentas.nIdVenta == "" && FrmVentas.nomXml=="" && FrmVentas.vDoc =="")
            {
                nIdVenta = FrmEnviarSunat.nIdVenta;
                nomXml = FrmEnviarSunat.nomXml;
                vDoc = FrmEnviarSunat.vDoc;
            } else
            {*/
            nIdRes = FrmResumenBolCPE.nIdRes;
            nomXml = FrmResumenBolCPE.nomXml;
            nNumRes = FrmResumenBolCPE.nNumRes;
            //}

            label4.Text = nIdRes;
            textBox3.Text = nomXml;

            textBox2.Text = "RESUMEN DE BOLETAS";

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
            try
            {
                string TipoDocumento = "";
                string nomXml = "";

                if (!ObjResumenEnvio.Buscar(label4.Text.ToString(), FrmResumenBolCPE.nNumRes, rucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos de envío de Resumen, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                TipoDocumento = "RC";

                string Trama = "";
                string Ruta = "";
                string fechaAnio = ObjResumenEnvio.Fecha.Substring(6, 4);
                string fechaMes = ObjResumenEnvio.Fecha.Substring(3, 2);
                string fechaDia = ObjResumenEnvio.Fecha.Substring(0, 2);
                string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLRE", $"{rutafec}",
                                    $"{textBox3.Text.ToString()}.xml");

                /*File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(ObjVenta.ArchivoXml));*/

                string vUsuario = FrmLogin.x_login_usuario;

                if (ObjResumenEnvio.ArchXml.Length > 0)
                {
                    nomXml = ObjResumenEnvio.ArchXml;
                    /*if (ObjEnvio.BuscarEnvioComp(label4.Text.ToString(), rucEmpresa, FrmVentas.vAlm))
                    {
                        nomXml = ObjEnvio.NomArchXml.ToString();
                    }*/

                    ObjGrabaXML.objEnvio = this;
                    ObjGrabaXML.enviarXMLResumen(TipoDocumento, int.Parse(FrmResumenBolCPE.nIdRes), FrmResumenBolCPE.nNumRes, RutaArchivo, nomXml, rucEmpresa.ToString(), vUsuario);
                }
                else
                {
                    ObjGrabaXML.generaXMLResumen1(label4.Text.ToString(), FrmResumenBolCPE.nNumRes, rucEmpresa, false);

                    ClsResumenEnvio ObjResumenEnvio1 = new ClsResumenEnvio();
                    if (ObjResumenEnvio1.Buscar(label4.Text.ToString(), FrmResumenBolCPE.nNumRes, rucEmpresa))
                    {
                    }

                    nomXml = ObjResumenEnvio1.ArchXml;

                    //Trama = ClsGrabaXML.vTrama;
                    //Ruta = ObjGrabaXML.RutaArchivo;
                    ObjGrabaXML.objEnvio = this;
                    ObjGrabaXML.enviarXMLResumen(TipoDocumento, int.Parse(FrmResumenBolCPE.nIdRes), FrmResumenBolCPE.nNumRes, RutaArchivo, nomXml, rucEmpresa.ToString(), vUsuario);
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