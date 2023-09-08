using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using System.Net.Http;
using Bicimoto.Comun.Dto.Intercambio;
using Bicimoto.Comun.Dto.Modelos;
using Bicimoto.Datos;
using SisBicimotoApp.Lib;
using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using MySql.Data.MySqlClient;
using SisBicimotoApp.Properties;
using iTextSharp.text.pdf;
using Bicimoto.API;
using Bicimoto.Firmado;

namespace SisBicimotoApp.Clases
{
    public partial class ClsGrabaXML
    {
        ClsEnvio ObjEnvio = new ClsEnvio();
        ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        ClsVenta ObjVenta = new ClsVenta();
        ClsParametro ObjParametro = new ClsParametro();


        public static string vTrama = "";
        public static string vResumenFirma = "";
        //public static string vTramasinFirma = "";

        public IEnvio objEnvio { get; set; }

        #region Variables Privadas
        private DocumentoElectronico _documento;
        private ComunicacionBaja _documentoBaja;
        private ResumenDiarioNuevo _documentoResumen;
        private ResumenDiario _documentoResumenAgrupado;
        //private DocumentoResumenDetalle _documentoResumenDetalle;
        private Contribuyente _Emisor;
        private HttpClient _client;
        #endregion

        #region Propiedades
        public string RutaArchivo { get; set; }
        public string IdDocumento { get; set; }
        public string Respuesta { get; set; }

        public string IdEnvio { get; set; }
        #endregion

        public ClsGrabaXML()
        {

        }

        static string BytesToStringConverted(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        public async void generaXMLFactura(string vIdVenta, string vRucEmpresa, string vAlmacen, Boolean vEnvio)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //Invocar Venta
                ClsVenta ObjVenta = new ClsVenta();
                ClsDocumento ObjDocumento = new ClsDocumento();
                ClsEmpresa ObjEmpresa = new ClsEmpresa();
                ClsUbigeo ObjUbigeo = new ClsUbigeo();
                ClsCliente ObjCliente = new ClsCliente();
                ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
                ClsParametro ObjParametro = new ClsParametro();
                ClsSerie ObjSerie = new ClsSerie();
                ClsProducto ObjProducto = new ClsProducto();

                string codDoc = "";
                string vMod = "VEN";
                decimal vIgv = 0;
                decimal vIsc = 0;
                decimal vDetraccion = 0;

                //Cargar Datos de Empresa
                if (!ObjEmpresa.BuscarRuc(vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos de Empresa, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (!ObjVenta.BuscarVenta(vIdVenta, vRucEmpresa, vAlmacen))
                {
                    MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (!ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod))
                {
                    MessageBox.Show("Error no se encontró datos de comprobante de Venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    codDoc = ObjDocumento.Codigo;
                }
                //Aperturar Documento Electronico
                _documento = new DocumentoElectronico();
                switch (codDoc)
                {
                    case "013":
                        _documento.TipoDocumento = "03";
                        break;
                    case "014":
                        _documento.TipoDocumento = "01";
                        break;
                    default:
                        if (ObjDocumento.TipDocElectronico.Equals("F"))
                        {
                            _documento.TipoDocumento = "01";

                        }
                        else
                        {
                            if (ObjDocumento.TipDocElectronico.Equals("B"))
                            {
                                _documento.TipoDocumento = "03";

                            }
                            else
                            {
                                _documento.TipoDocumento = "03";
                            }
                        }

                        break;
                }

                //Emisor
                _documento.Emisor.TipoDocumento = "6";
                _documento.Emisor.NroDocumento = ObjEmpresa.Ruc;
                _documento.Emisor.NombreComercial = ObjEmpresa.Razon;
                _documento.Emisor.NombreLegal = ObjEmpresa.NombreLegal;
                _documento.Emisor.Ubigeo = ObjEmpresa.Ubigeo;
                _documento.Emisor.Direccion = ObjEmpresa.Direccion;
                if (!ObjUbigeo.BuscarDpto(ObjEmpresa.Region))
                {
                    MessageBox.Show("Error no se encontró datos de Departamento del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Departamento = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarProv(ObjEmpresa.Region, ObjEmpresa.Provincia))
                {
                    MessageBox.Show("Error no se encontró datos de Provincia del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Provincia = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarDist(ObjEmpresa.Region, ObjEmpresa.Provincia, ObjEmpresa.Distrito))
                {
                    MessageBox.Show("Error no se encontró datos de Distrito del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Distrito = ObjUbigeo.NOMBRE;
                }
                _documento.Emisor.Urbanizacion = ObjEmpresa.Urbanizacion;
                _documento.Emisor.CodDomicilioFiscal = "0000";
                //-----------------------------!!
                //Receptor
                string vParam = "2";
                string vCodCat = "018";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TipDocCli, vParam.ToString());

                //SE CAMBIO
                if (!ObjCliente.BuscarCLiente(ObjVenta.Cliente, vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos del Receptor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                _documento.Receptor.TipoDocumento = ObjDetCatalogo.CodDetCat;
                _documento.Receptor.NroDocumento = ObjVenta.Cliente.Trim();
                _documento.Receptor.NombreLegal = ObjCliente.Nombre.Trim();
                //-----------------------------!!
                //Datos del comprobante
                //Buscamos valores para el IGV, ISC y detraccion
                string nParam1 = "3";
                //igv
                if (ObjParametro.BuscarParametro(nParam1))
                {
                    vIgv = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                string nParam2 = "8";
                //isc
                if (ObjParametro.BuscarParametro(nParam2))
                {
                    vIsc = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                string nParam3 = "9";
                //detraccion
                if (ObjParametro.BuscarParametro(nParam3))
                {
                    vDetraccion = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                //-----------------------------!!
                vIgv = vIgv / 100;
                vIsc = vIsc / 100;
                vDetraccion = vDetraccion / 100;
                _documento.CalculoIgv = vIgv;
                _documento.CalculoIsc = vIsc;
                _documento.CalculoDetraccion = vDetraccion;

                //Fecha de Emision
                string dia = ObjVenta.Fecha.Substring(0, 2);
                string mes = ObjVenta.Fecha.Substring(3, 2);
                string anio = ObjVenta.Fecha.Substring(6, 4);
                string vFechaEmision = dia.ToString() + "/" + mes.ToString() + "/" + anio.ToString();
                _documento.FechaEmision = vFechaEmision;
                //Tipo de Operacion
                string nParam4 = "10";
                string vTipOperacion = "";
                if (ObjParametro.BuscarParametro(nParam4))
                {
                    vTipOperacion = ObjParametro.Valor.ToString();
                }
                _documento.TipoOperacion = vTipOperacion;

                //Moneda
                vParam = "2";
                vCodCat = "001";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda, vParam.ToString());

                if (!ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda, vParam.ToString()))
                {
                    MessageBox.Show("Error no se encontró tipo de moneda, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                if (ObjDetCatalogo.CodDetCat.Equals("001"))
                {
                    _documento.Moneda = "PEN";
                }
                else
                {
                    _documento.Moneda = "USD";
                }

                //Monto de la Percepción (Opcional)
                //_documento.MontoPercepcion = 0;

                //Monto de la Detraccion (Opcional)
                //_documento.MontoDetraccion = 0;

                //Descuento Global (Opcional)
                vParam = "1";
                vCodCat = "022";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TComp.ToString(), vParam.ToString());

                if (!ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TComp.ToString(), vParam.ToString()))
                {
                    MessageBox.Show("Error no se encontró tipo de venta SUNAT, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (ObjDetCatalogo.CodDetCat.Equals("04"))
                {
                    _documento.DescuentoGlobal = Decimal.Parse(ObjVenta.Dcto.ToString());
                }


                //Gravadas (Opcional)
                _documento.Gravadas = Decimal.Parse(ObjVenta.TBruto.ToString());

                //Exoneradas (Opcional)
                _documento.Exoneradas = Decimal.Parse(ObjVenta.TExonerada.ToString());

                //Inafectas (Opcional)
                _documento.Inafectas = Decimal.Parse(ObjVenta.TInafecta.ToString());

                //Gratitas (Opcional)
                _documento.Gratuitas = Decimal.Parse(ObjVenta.TGratuita.ToString());

                //Total IGV
                _documento.TotalIgv = Decimal.Parse(ObjVenta.TIgv.ToString());

                //Total ISC
                //_documento.TotalIsc = 0;

                //Total Otros Tributos (Opcional)
                //_documento.TotalOtrosTributos = 0;

                //Total Venta
                if (ObjVenta.Egratuita.Equals("1"))
                {
                    _documento.TotalVenta = 0;
                }
                else
                {
                    _documento.TotalVenta = Decimal.Parse(ObjVenta.Total.ToString());
                }


                //Monto en Letras
                _documento.MontoEnLetras = ClsConversiones.NumeroALetrasString(ObjVenta.Total.ToString());

                //Id del Comprobante
                ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
                {
                    MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                string vIdComprobante = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
                _documento.IdDocumento = vIdComprobante;

                //Items
                DataSet datos = csql.dataset_cadena("Call SpDetVentaBuscar('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");
                //Tipo de Precio 
                string nParamPrecio = "12";
                string vTipPrecio = "";
                if (ObjParametro.BuscarParametro(nParamPrecio))
                {
                    vTipPrecio = ObjParametro.Valor.ToString();
                }

                //Tipo de Impuesto 
                string nParamImp = "13";
                string vTipImp = "";
                if (ObjParametro.BuscarParametro(nParamImp))
                {
                    vTipImp = ObjParametro.Valor.ToString();
                }

                // Forma de Pago

                string nParamFormPago = "15";
                string formapago = ObjVenta.TVenta;
                if (ObjDetCatalogo.BuscarDetCatalogoCod(nParamFormPago, formapago))
                {
                    //_documento.forma
                }

                int vId = 1;
                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        var detalle = new DetalleDocumento();

                        detalle.Id = vId;
                        detalle.CodigoItem = fila[0].ToString();
                        if (!ObjProducto.BuscarProducto(fila[0].ToString(), vRucEmpresa, vAlmacen))
                        {
                            MessageBox.Show("Error no se encontró el producto " + fila[0].ToString() + ", VERIFIQUE!!!", "SISTEMA");
                            return;
                        }
                        detalle.Descripcion = ObjProducto.Nombre;
                        detalle.Cantidad = Decimal.Parse(fila[4].ToString());
                        detalle.UnidadMedida = "NIU";
                        /*detalle.PrecioUnitario = Decimal.Parse(fila[5].ToString());
                        detalle.PrecioReferencial = Decimal.Parse(fila[5].ToString());
                        detalle.Impuesto = Decimal.Parse(fila[6].ToString());
                        detalle.TotalVenta = Decimal.Parse(fila[7].ToString());
                        Decimal totalVenta = Decimal.Parse(fila[7].ToString());*/

                        if (Decimal.Parse(ObjVenta.TBruto.ToString()) == 0)
                        {
                            detalle.PrecioUnitario = Decimal.Parse(fila[5].ToString());
                            detalle.PrecioReferencial = Decimal.Parse(fila[5].ToString());
                            detalle.Impuesto = Decimal.Parse(fila[6].ToString());
                            detalle.TotalVenta = Decimal.Parse(fila[7].ToString());
                        }
                        else
                        {
                            Double totalVenta = Double.Parse(fila[7].ToString());
                            Decimal totalventadecimal = Math.Round(Convert.ToDecimal(totalVenta / 1.18), 2);
                            Decimal precioventa = Decimal.Parse(fila[5].ToString());
                            //Decimal ValorUnitario = Math.Round((precioventa * 100) / 118,2);
                            Decimal ValorUnitario = Math.Round(totalventadecimal / detalle.Cantidad, 4);
                            Decimal igvDetalle = Convert.ToDecimal(totalVenta) - totalventadecimal;
                            detalle.PrecioUnitario = ValorUnitario;
                            detalle.TotalVenta = totalventadecimal;
                            detalle.PrecioReferencial = precioventa;
                            detalle.Impuesto = igvDetalle;
                        }

                        detalle.TipoPrecio = fila[9].ToString();
                        if (ObjVenta.TComp.Equals("04"))
                        {
                            detalle.Descuento = Decimal.Parse(fila[11].ToString());
                        }
                        detalle.TipoImpuesto = fila[10].ToString();
                        _documento.Items.Add(detalle);
                        vId = vId + 1;
                    }
                }

                _documento.LineCountNumeric = Convert.ToString(_documento.Items.Count());

                //-------------------------------------------------//

                string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivoini);
                string vUrlApiSunat = "";
                vUrlApiSunat = ciniarchivo.ReadValue("Configura", "UrlApi", "");

                //var proxy = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BicimotoApi"]) };

                var proxy = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

                string metodoApi;
                switch (_documento.TipoDocumento)
                {
                    case "07":
                        metodoApi = "api/GenerarNotaCredito";
                        break;
                    case "08":
                        metodoApi = "api/GenerarNotaDebito";
                        break;
                    default:
                        metodoApi = "api/GenerarFactura";
                        break;
                }

                //var response = await proxy.PostAsJsonAsync(metodoApi, _documento);
                ISerializador serializador = new Serializador();

                var response = await new GenerarFactura(serializador).Post(_documento);

                //var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();

                /*if (!respuesta.Exito)
                    throw new ApplicationException(respuesta.MensajeError);*/

                if (!response.Exito)
                    throw new ApplicationException(response.MensajeError);

                vTrama = response.TramaXmlSinFirma;

                //Firmar
                //var tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(respuesta.TramaXmlSinFirma));

                string vRuta = "";
                string nParam = "16";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vRuta = ObjParametro.Valor.ToString();
                }

                string vPass = "";
                nParam = "17";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vPass = ObjParametro.Valor.ToString();
                }


                var firmadoRequest = new FirmadoRequest
                {
                    //TramaXmlSinFirma = respuesta.TramaXmlSinFirma,
                    TramaXmlSinFirma = response.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(vRuta.ToString())),
                    PasswordCertificado = vPass.ToString(),
                    //UnSoloNodoExtension = rbRetenciones.Checked || rbResumen.Checked
                };

                /*var jsonFirmado = await proxy.PostAsJsonAsync("api/Firmar", firmadoRequest);
                var respuestaFirmado = await jsonFirmado.Content.ReadAsAsync<FirmadoResponse>();*/

                ICertificador certificador = new Certificador();
                var respuestaFirmado = await new Firmar(certificador).Post(firmadoRequest);

                if (!respuestaFirmado.Exito)
                    throw new ApplicationException(respuestaFirmado.MensajeError);

                using (MySqlConnection conn = GetNewConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE tblventa set ArchXml = @Archivo, ArchivoXml = @ArchivoXml, NomArchXml = @NomArchXml, ResumenFirma = @ResumenFirma, ValorFirma = @ValorFirma where IdVenta = @IdVenta and Empresa = @numruc and Almacen = @Almacen";
                        cmd.Parameters.AddWithValue("@Archivo", respuestaFirmado.TramaXmlFirmado);
                        cmd.Parameters.AddWithValue("@NomArchXml", $"{_documento.IdDocumento}.xml");
                        cmd.Parameters.AddWithValue("@IdVenta", vIdVenta.ToString());
                        cmd.Parameters.AddWithValue("@numruc", vRucEmpresa.ToString());
                        cmd.Parameters.AddWithValue("@Almacen", vAlmacen.ToString());
                        cmd.Parameters.AddWithValue("@ArchivoXml", respuestaFirmado.TramaXmlFirmado);
                        cmd.Parameters.AddWithValue("@ResumenFirma", respuestaFirmado.ResumenFirma);
                        cmd.Parameters.AddWithValue("@ValorFirma", respuestaFirmado.ValorFirma);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (vEnvio)
                {
                    //Generando carpeta del dia
                    DateTime fechaHoy = DateTime.Now;
                    string fecha = fechaHoy.ToString("d");
                    string fechaAnio = fecha.Substring(6, 4);
                    string fechaMes = fecha.Substring(3, 2);
                    string fechaDia = fecha.Substring(0, 2);
                    string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                    string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"{rutafec}");

                    RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"{rutafec}",
                        $"{_documento.IdDocumento}.xml");

                    if (!(Directory.Exists(carpeta)))
                    {
                        Directory.CreateDirectory(carpeta);
                    }
                }
                else
                {
                    string fechaAnio = ObjVenta.Fecha.Substring(6, 4);
                    string fechaMes = ObjVenta.Fecha.Substring(3, 2);
                    string fechaDia = ObjVenta.Fecha.Substring(0, 2);
                    string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();
                    string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"{rutafec}");

                    RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"{rutafec}",
                        $"{_documento.IdDocumento}.xml");

                    if (!(Directory.Exists(carpeta)))
                    {
                        Directory.CreateDirectory(carpeta);
                    }

                }

                File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));
                vResumenFirma = respuestaFirmado.ResumenFirma;
                IdDocumento = _documento.IdDocumento;
                string vUsuario = FrmLogin.x_login_usuario;

                //vTrama = respuesta.TramaXmlSinFirma;

                if (vEnvio)
                {
                    string archivo = System.Environment.CurrentDirectory + @"\base.ini";

                    //MessageBox.Show("Llega");
                    try
                    {
                        enviarXML(_documento.TipoDocumento, vIdVenta, codDoc, ObjVenta.Serie, ObjVenta.Numero, _documento.IdDocumento, RutaArchivo, response.TramaXmlSinFirma, vRucEmpresa.ToString(), vAlmacen.ToString(), vUsuario);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Hubo problemas al leer el archivo INI. no se puede realizar el envío del comprobante electrónico" + ex.Message, "SISTEMA");
                    }
                }

                if (codDoc.Equals("014"))
                {
                    ObjCliente.BuscarCLiente(ObjVenta.Cliente, vRucEmpresa);

                    string vValor = "";
                    nParam = "24";
                    if (ObjParametro.BuscarParametro(nParam))
                    {
                        vValor = ObjParametro.Valor.ToString();
                    }

                    string nFrom = vValor;
                    string nPara = ObjCliente.Email;
                    string nMensaje = "";
                    string nAsunto = "Factura Eléctronica";

                    ClsEnviarCorreo ObjEnviarCorreo = new ClsEnviarCorreo(nFrom, nPara, nMensaje, nAsunto, vIdVenta, vRucEmpresa, vAlmacen);
                    if (ObjEnviarCorreo.enviaMail())
                    {

                    }
                    else
                    {

                    }

                }

                //DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public async void generaXMLNc(string vIdNc, string vRucEmpresa, string vAlmacen)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClsVenta ObjVenta = new ClsVenta();
                ClsNotCre ObjNotCre = new ClsNotCre();
                ClsDocumento ObjDocumento = new ClsDocumento();
                ClsEmpresa ObjEmpresa = new ClsEmpresa();
                ClsUbigeo ObjUbigeo = new ClsUbigeo();
                ClsCliente ObjCliente = new ClsCliente();
                ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
                ClsParametro ObjParametro = new ClsParametro();
                ClsSerie ObjSerie = new ClsSerie();
                ClsProducto ObjProducto = new ClsProducto();

                string codDoc = "";
                string vMod = "VEN";
                string vTDocRel = "";
                decimal vIgv = 0;
                decimal vIsc = 0;
                decimal vDetraccion = 0;

                //Cargar Datos de Empresa
                if (!ObjEmpresa.BuscarRuc(vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos de Empresa, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                if (!ObjNotCre.BuscarNC(vIdNc, vRucEmpresa, vAlmacen))
                {
                    MessageBox.Show("Error no se encontró datos de Nota de Crédito, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (!ObjDocumento.BuscarDocNCortoMod(ObjNotCre.Doc, vMod))
                {
                    MessageBox.Show("Error no se encontró datos de comprobante de Nota de Crédito, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    codDoc = ObjDocumento.Codigo;
                }

                if (!ObjVenta.BuscarVenta(ObjNotCre.IdVenta, vRucEmpresa, vAlmacen))
                {
                    MessageBox.Show("Error no se encontró datos de comprobante de afiliado a la Nota de Crédito, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                //Aperturar Documento Electronico
                _documento = new DocumentoElectronico();
                switch (codDoc)
                {
                    case "013":
                        _documento.TipoDocumento = "03";
                        break;
                    case "014":
                        _documento.TipoDocumento = "01";
                        break;
                    case "015":
                        _documento.TipoDocumento = "07";
                        break;
                }

                //Emisor
                _documento.Emisor.TipoDocumento = "6";
                _documento.Emisor.NroDocumento = ObjEmpresa.Ruc;
                _documento.Emisor.NombreComercial = ObjEmpresa.Razon;
                _documento.Emisor.NombreLegal = ObjEmpresa.NombreLegal;
                _documento.Emisor.Ubigeo = ObjEmpresa.Ubigeo;
                _documento.Emisor.Direccion = ObjEmpresa.Direccion;
                if (!ObjUbigeo.BuscarDpto(ObjEmpresa.Region))
                {
                    MessageBox.Show("Error no se encontró datos de Departamento del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Departamento = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarProv(ObjEmpresa.Region, ObjEmpresa.Provincia))
                {
                    MessageBox.Show("Error no se encontró datos de Provincia del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Provincia = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarDist(ObjEmpresa.Region, ObjEmpresa.Provincia, ObjEmpresa.Distrito))
                {
                    MessageBox.Show("Error no se encontró datos de Distrito del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Distrito = ObjUbigeo.NOMBRE;
                }
                _documento.Emisor.Urbanizacion = ObjEmpresa.Urbanizacion;
                //-----------------------------!!
                //Receptor
                string vParam = "2";
                string vCodCat = "018";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotCre.TipDocCli, vParam.ToString());

                //SE CAMBIO
                if (!ObjCliente.BuscarCLiente(ObjNotCre.Cliente, vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos del Receptor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                _documento.Receptor.TipoDocumento = ObjDetCatalogo.CodDetCat;
                _documento.Receptor.NroDocumento = ObjNotCre.Cliente;
                _documento.Receptor.NombreLegal = ObjCliente.Nombre;
                //-----------------------------!!
                //Datos del comprobante
                //Buscamos valores para el IGV, ISC y detraccion
                string nParam1 = "3";
                //igv
                if (ObjParametro.BuscarParametro(nParam1))
                {
                    vIgv = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                string nParam2 = "8";
                //isc
                if (ObjParametro.BuscarParametro(nParam2))
                {
                    vIsc = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                string nParam3 = "9";
                //detraccion
                if (ObjParametro.BuscarParametro(nParam3))
                {
                    vDetraccion = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                //-----------------------------!!
                vIgv = vIgv / 100;
                vIsc = vIsc / 100;
                vDetraccion = vDetraccion / 100;
                _documento.CalculoIgv = vIgv;
                _documento.CalculoIsc = vIsc;
                _documento.CalculoDetraccion = vDetraccion;

                //Fecha de Emision
                string dia = ObjNotCre.Fecha.Substring(0, 2);
                string mes = ObjNotCre.Fecha.Substring(3, 2);
                string anio = ObjNotCre.Fecha.Substring(6, 4);
                string vFechaEmision = dia.ToString() + "/" + mes.ToString() + "/" + anio.ToString();
                _documento.FechaEmision = vFechaEmision;
                //Tipo de Operacion
                string nParam4 = "10";
                string vTipOperacion = "";
                if (ObjParametro.BuscarParametro(nParam4))
                {
                    vTipOperacion = ObjParametro.Valor.ToString();
                }
                _documento.TipoOperacion = vTipOperacion;

                //Moneda
                vParam = "2";
                vCodCat = "001";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotCre.TMoneda, vParam.ToString());

                if (!ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotCre.TMoneda, vParam.ToString()))
                {
                    MessageBox.Show("Error no se encontró tipo de moneda, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (ObjDetCatalogo.CodDetCat.Equals("001"))
                {
                    _documento.Moneda = "PEN";
                }
                else
                {
                    _documento.Moneda = "USD";
                }

                //Monto de la Percepción (Opcional)
                //_documento.MontoPercepcion = 0;

                //Monto de la Detraccion (Opcional)
                //_documento.MontoDetraccion = 0;

                //Descuento Global (Opcional)
                //_documento.DescuentoGlobal = 0;

                //Gravadas (Opcional)
                _documento.Gravadas = Decimal.Parse(ObjNotCre.TBruto.ToString());

                //Exoneradas (Opcional)
                _documento.Exoneradas = 0;

                //Inafectas (Opcional)
                _documento.Inafectas = 0;

                //Gratitas (Opcional)
                _documento.Gratuitas = 0;

                //Total IGV
                _documento.TotalIgv = Decimal.Parse(ObjNotCre.TIgv.ToString());

                //Total ISC
                //_documento.TotalIsc = 0;

                //Total Otros Tributos (Opcional)
                //_documento.TotalOtrosTributos = 0;

                //Total Venta
                _documento.TotalVenta = Decimal.Parse(ObjNotCre.Total.ToString());

                //Monto en Letras
                //_documento.MontoEnLetras = ClsConversiones.NumeroALetrasString(ObjVenta.Total.ToString());

                //Id del Comprobante
                ObjDocumento.BuscarDocNCortoMod(ObjNotCre.Doc, vMod);
                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjNotCre.Serie))
                {
                    MessageBox.Show("Error no se encontró el comprobante y la serie de la Nota de Credito, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                string vIdComprobante = ObjSerie.PrefijoSerie + ObjNotCre.Serie + "-" + ObjNotCre.Numero;
                _documento.IdDocumento = vIdComprobante;

                //Items
                DataSet datosCre = csql.dataset_cadena("Call SpDetNotCreBuscar('" + vIdNc.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");
                //Tipo de Precio 
                string nParamPrecio = "12";
                string vTipPrecio = "";
                if (ObjParametro.BuscarParametro(nParamPrecio))
                {
                    vTipPrecio = ObjParametro.Valor.ToString();
                }

                //Tipo de Impuesto 
                string nParamImp = "13";
                string vTipImp = "";
                if (ObjParametro.BuscarParametro(nParamImp))
                {
                    vTipImp = ObjParametro.Valor.ToString();
                }
                vMod = "VEN";
                ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);

                //Serie de Documento de venta
                ClsSerie ObjSerie2 = new ClsSerie();
                string vPrefSerie = "";
                if (!ObjSerie2.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
                {
                    //MessageBox.Show("Error no se encontró el comprobante y la serie de la Nota de Credito, VERIFIQUE!!!", "SISTEMA");
                    //return;
                    vPrefSerie = "";
                }
                else
                {
                    vPrefSerie = ObjSerie2.PrefijoSerie;
                }

                var documentoRelacionado = new DocumentoRelacionado();

                //Documento Relacionado
                vMod = "VEN";
                ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
                switch (ObjDocumento.Codigo)
                {
                    case "013":
                        vTDocRel = "03";
                        break;
                    case "014":
                        vTDocRel = "01";
                        break;
                }

                documentoRelacionado.TipoDocumento = vTDocRel.ToString();
                documentoRelacionado.NroDocumento = vPrefSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

                _documento.Relacionados.Add(documentoRelacionado);

                var discrepancia = new Discrepancia();

                //Discrepancia
                //Buscando Motivo
                vParam = "1";
                vCodCat = "020";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotCre.TMotivo, vParam.ToString());

                discrepancia.Tipo = ObjDetCatalogo.CodDetCat;
                discrepancia.NroReferencia = vPrefSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
                discrepancia.Descripcion = ObjNotCre.Observaciones;

                _documento.Discrepancias.Add(discrepancia);

                int vId = 1;
                if (datosCre.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datosCre.Tables[0].Rows)
                    {
                        var detalle = new DetalleDocumento();

                        detalle.Id = vId;
                        detalle.CodigoItem = fila[0].ToString();
                        if (!ObjProducto.BuscarProducto(detalle.CodigoItem, vRucEmpresa, vAlmacen))
                        {
                            MessageBox.Show("Error no se encontró el producto " + fila[0].ToString() + ", VERIFIQUE!!!", "SISTEMA");
                            return;
                        }
                        detalle.Descripcion = ObjProducto.Nombre;
                        detalle.PrecioUnitario = Decimal.Parse(fila[5].ToString());
                        detalle.PrecioReferencial = Decimal.Parse(fila[5].ToString());
                        //detalle.TipoPrecio = fila[9].ToString();
                        detalle.Cantidad = Decimal.Parse(fila[6].ToString());
                        detalle.UnidadMedida = "NIU";
                        detalle.Impuesto = Decimal.Parse(fila[7].ToString()); ;
                        //detalle.TipoImpuesto = fila[10].ToString();
                        detalle.TotalVenta = Decimal.Parse(fila[8].ToString());
                        _documento.Items.Add(detalle);



                        vId = vId + 1;
                    }
                }
                else
                {
                    //Documento Relacionado

                    //Documento Relacionado
                    vMod = "VEN";
                    ObjDocumento.BuscarDocNCortoMod(ObjVenta.Doc, vMod);
                    switch (ObjDocumento.Codigo)
                    {
                        case "013":
                            vTDocRel = "03";
                            break;
                        case "014":
                            vTDocRel = "01";
                            break;
                    }

                    documentoRelacionado.TipoDocumento = vTDocRel.ToString();
                    documentoRelacionado.NroDocumento = vPrefSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
                    _documento.Relacionados.Add(documentoRelacionado);

                    //Discrepancia
                    //Buscando Motivo
                    vParam = "1";
                    vCodCat = "020";
                    ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotCre.TMotivo, vParam.ToString());
                    discrepancia.Tipo = ObjDetCatalogo.CodDetCat;
                    discrepancia.NroReferencia = vPrefSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
                    discrepancia.Descripcion = ObjNotCre.Observaciones;
                    _documento.Discrepancias.Add(discrepancia);
                }

                //-------------------------------------------------//

                string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivoini);
                string vUrlApiSunat = "";
                vUrlApiSunat = ciniarchivo.ReadValue("Configura", "UrlApi", "");

                //var proxy = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BicimotoApi"]) };

                var proxy = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

                string metodoApi;
                switch (_documento.TipoDocumento)
                {
                    case "07":
                        metodoApi = "api/GenerarNotaCredito";
                        break;
                    case "08":
                        metodoApi = "api/GenerarNotaDebito";
                        break;
                    default:
                        metodoApi = "api/GenerarFactura";
                        break;
                }

                var response = await proxy.PostAsJsonAsync(metodoApi, _documento);
                var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();
                if (!respuesta.Exito)
                    throw new ApplicationException(respuesta.MensajeError);

                //Generando carpeta del dia
                DateTime fechaHoy = DateTime.Now;
                string fecha = fechaHoy.ToString("d");
                string fechaAnio = fecha.Substring(6, 4);
                string fechaMes = fecha.Substring(3, 2);
                string fechaDia = fecha.Substring(0, 2);
                string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"NC", $"{rutafec}");

                RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"NC", $"{rutafec}",
                    $"{_documento.IdDocumento}.xml");

                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }

                /*using (MySqlConnection conn = GetNewConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE tblventa set ArchXml = @Archivo, NomArchXml = @NomArchXml where IdVenta = @IdVenta and Empresa = @numruc and Almacen = @Almacen";
                        cmd.Parameters.AddWithValue("@Archivo", respuesta.TramaXmlSinFirma);
                        cmd.Parameters.AddWithValue("@NomArchXml", $"{_documento.IdDocumento}.xml");
                        cmd.Parameters.AddWithValue("@IdNc", vIdNc.ToString());
                        cmd.Parameters.AddWithValue("@numruc", vRucEmpresa.ToString());
                        cmd.Parameters.AddWithValue("@Almacen", vAlmacen.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }*/

                vTrama = respuesta.TramaXmlSinFirma;

                //Firmar
                //var tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(respuesta.TramaXmlSinFirma));

                string vRuta = "";
                string nParam = "16";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vRuta = ObjParametro.Valor.ToString();
                }

                string vPass = "";
                nParam = "17";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vPass = ObjParametro.Valor.ToString();
                }


                var firmadoRequest = new FirmadoRequest
                {
                    //TramaXmlSinFirma = respuesta.TramaXmlSinFirma,
                    TramaXmlSinFirma = respuesta.TramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(vRuta.ToString())),
                    PasswordCertificado = vPass.ToString(),
                    //UnSoloNodoExtension = rbRetenciones.Checked || rbResumen.Checked
                };

                /*var jsonFirmado = await proxy.PostAsJsonAsync("api/Firmar", firmadoRequest);
                var respuestaFirmado = await jsonFirmado.Content.ReadAsAsync<FirmadoResponse>();*/

                ICertificador certificador = new Certificador();
                var respuestaFirmado = await new Firmar(certificador).Post(firmadoRequest);

                if (!respuestaFirmado.Exito)
                    throw new ApplicationException(respuestaFirmado.MensajeError);

                File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(respuestaFirmado.TramaXmlFirmado));

                IdDocumento = _documento.IdDocumento;

                //DialogResult = DialogResult.OK;
                string vUsuario = FrmLogin.x_login_usuario;

                string archivo = System.Environment.CurrentDirectory + @"\base.ini";

                //MessageBox.Show("Llega");
                try
                {

                    enviarXML(_documento.TipoDocumento, vIdNc, codDoc, ObjNotCre.Serie, ObjNotCre.Numero, _documento.IdDocumento, RutaArchivo, respuesta.TramaXmlSinFirma, vRucEmpresa.ToString(), vAlmacen.ToString(), vUsuario);
                    //enviarXML(_documento.TipoDocumento, vIdNc, codDoc, ObjNotCre.Serie, ObjNotCre.Numero, _documento.IdDocumento, RutaArchivo, respuesta.TramaXmlSinFirma, vRucEmpresa.ToString(), vAlmacen.ToString(), vUsuario);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Hubo problemas al leer el archivo INI. no se puede realizar el envío del comprobante electrónico" + ex.Message, "SISTEMA");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public async void generaXMLNd(string vIdNd, string vRucEmpresa, string vAlmacen)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClsVenta ObjVenta = new ClsVenta();
                ClsNotDeb ObjNotDeb = new ClsNotDeb();
                ClsDocumento ObjDocumento = new ClsDocumento();
                ClsEmpresa ObjEmpresa = new ClsEmpresa();
                ClsUbigeo ObjUbigeo = new ClsUbigeo();
                ClsCliente ObjCliente = new ClsCliente();
                ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
                ClsParametro ObjParametro = new ClsParametro();
                ClsSerie ObjSerie = new ClsSerie();
                ClsProducto ObjProducto = new ClsProducto();

                string codDoc = "";
                string vMod = "VEN";
                string vTDocRel = "";
                decimal vIgv = 0;
                decimal vIsc = 0;
                decimal vDetraccion = 0;

                //Cargar Datos de Empresa
                if (!ObjEmpresa.BuscarRuc(vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos de Empresa, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                if (!ObjNotDeb.BuscarND(vIdNd, vRucEmpresa, vAlmacen))
                {
                    MessageBox.Show("Error no se encontró datos de Nota de Crédito, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (!ObjDocumento.BuscarDocNCortoMod(ObjNotDeb.Doc, vMod))
                {
                    MessageBox.Show("Error no se encontró datos de comprobante de Nota de Dédito, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    codDoc = ObjDocumento.Codigo;
                }

                if (!ObjVenta.BuscarVenta(ObjNotDeb.IdVenta, vRucEmpresa, vAlmacen))
                {
                    MessageBox.Show("Error no se encontró datos de comprobante de afiliado a la Nota de Crédito, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                //Aperturar Documento Electronico
                _documento = new DocumentoElectronico();
                switch (codDoc)
                {
                    case "013":
                        _documento.TipoDocumento = "03";
                        break;
                    case "014":
                        _documento.TipoDocumento = "01";
                        break;
                    case "015":
                        _documento.TipoDocumento = "07";
                        break;
                    case "016":
                        _documento.TipoDocumento = "08";
                        break;
                }

                //Emisor
                _documento.Emisor.TipoDocumento = "6";
                _documento.Emisor.NroDocumento = ObjEmpresa.Ruc;
                _documento.Emisor.NombreComercial = ObjEmpresa.Razon;
                _documento.Emisor.NombreLegal = ObjEmpresa.NombreLegal;
                _documento.Emisor.Ubigeo = ObjEmpresa.Ubigeo;
                _documento.Emisor.Direccion = ObjEmpresa.Direccion;
                if (!ObjUbigeo.BuscarDpto(ObjEmpresa.Region))
                {
                    MessageBox.Show("Error no se encontró datos de Departamento del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Departamento = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarProv(ObjEmpresa.Region, ObjEmpresa.Provincia))
                {
                    MessageBox.Show("Error no se encontró datos de Provincia del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Provincia = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarDist(ObjEmpresa.Region, ObjEmpresa.Provincia, ObjEmpresa.Distrito))
                {
                    MessageBox.Show("Error no se encontró datos de Distrito del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _documento.Emisor.Distrito = ObjUbigeo.NOMBRE;
                }
                _documento.Emisor.Urbanizacion = ObjEmpresa.Urbanizacion;
                //-----------------------------!!
                //Receptor
                string vParam = "2";
                string vCodCat = "018";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotDeb.TipDocCli, vParam.ToString());

                //SE CAMBIO
                if (!ObjCliente.BuscarCLiente(ObjNotDeb.Cliente, vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos del Receptor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                _documento.Receptor.TipoDocumento = ObjDetCatalogo.CodDetCat;
                _documento.Receptor.NroDocumento = ObjNotDeb.Cliente;
                _documento.Receptor.NombreLegal = ObjCliente.Nombre;
                //-----------------------------!!
                //Datos del comprobante
                //Buscamos valores para el IGV, ISC y detraccion
                string nParam1 = "3";
                //igv
                if (ObjParametro.BuscarParametro(nParam1))
                {
                    vIgv = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                string nParam2 = "8";
                //isc
                if (ObjParametro.BuscarParametro(nParam2))
                {
                    vIsc = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                string nParam3 = "9";
                //detraccion
                if (ObjParametro.BuscarParametro(nParam3))
                {
                    vDetraccion = Decimal.Parse(ObjParametro.Valor.ToString());
                }
                //-----------------------------!!
                vIgv = vIgv / 100;
                vIsc = vIsc / 100;
                vDetraccion = vDetraccion / 100;
                _documento.CalculoIgv = vIgv;
                _documento.CalculoIsc = vIsc;
                _documento.CalculoDetraccion = vDetraccion;

                //Fecha de Emision
                string dia = ObjNotDeb.Fecha.Substring(0, 2);
                string mes = ObjNotDeb.Fecha.Substring(3, 2);
                string anio = ObjNotDeb.Fecha.Substring(6, 4);
                string vFechaEmision = dia.ToString() + "/" + mes.ToString() + "/" + anio.ToString();
                _documento.FechaEmision = vFechaEmision;
                //Tipo de Operacion
                string nParam4 = "10";
                string vTipOperacion = "";
                if (ObjParametro.BuscarParametro(nParam4))
                {
                    vTipOperacion = ObjParametro.Valor.ToString();
                }
                _documento.TipoOperacion = vTipOperacion;

                //Moneda
                vParam = "2";
                vCodCat = "001";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotDeb.TMoneda, vParam.ToString());

                if (!ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotDeb.TMoneda, vParam.ToString()))
                {
                    MessageBox.Show("Error no se encontró tipo de moneda, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (ObjDetCatalogo.CodDetCat.Equals("001"))
                {
                    _documento.Moneda = "PEN";
                }
                else
                {
                    _documento.Moneda = "USD";
                }

                //Monto de la Percepción (Opcional)
                //_documento.MontoPercepcion = 0;

                //Monto de la Detraccion (Opcional)
                //_documento.MontoDetraccion = 0;

                //Descuento Global (Opcional)
                //_documento.DescuentoGlobal = 0;

                //Gravadas (Opcional)
                _documento.Gravadas = Decimal.Parse(ObjNotDeb.TBruto.ToString());

                //Exoneradas (Opcional)
                _documento.Exoneradas = 0;

                //Inafectas (Opcional)
                _documento.Inafectas = 0;

                //Gratitas (Opcional)
                _documento.Gratuitas = 0;

                //Total IGV
                _documento.TotalIgv = Decimal.Parse(ObjNotDeb.TIgv.ToString());

                //Total ISC
                //_documento.TotalIsc = 0;

                //Total Otros Tributos (Opcional)
                //_documento.TotalOtrosTributos = 0;

                //Total Venta
                _documento.TotalVenta = Decimal.Parse(ObjNotDeb.Total.ToString());

                //Monto en Letras
                //_documento.MontoEnLetras = ClsConversiones.NumeroALetrasString(ObjVenta.Total.ToString());

                //Id del Comprobante
                ObjDocumento.BuscarDocNCortoMod(ObjNotDeb.Doc, vMod);
                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjNotDeb.Serie))
                {
                    MessageBox.Show("Error no se encontró el comprobante y la serie de la Nota de Credito, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                string vIdComprobante = ObjSerie.PrefijoSerie + ObjNotDeb.Serie + "-" + ObjNotDeb.Numero;
                _documento.IdDocumento = vIdComprobante;

                //Items
                DataSet datosDeb = csql.dataset_cadena("Call SpDetNotDebBuscar('" + vIdNd.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");
                //Tipo de Precio 
                string nParamPrecio = "12";
                string vTipPrecio = "";
                if (ObjParametro.BuscarParametro(nParamPrecio))
                {
                    vTipPrecio = ObjParametro.Valor.ToString();
                }

                //Tipo de Impuesto 
                string nParamImp = "13";
                string vTipImp = "";
                if (ObjParametro.BuscarParametro(nParamImp))
                {
                    vTipImp = ObjParametro.Valor.ToString();
                }
                vMod = "VEN";
                ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);

                //Serie de Documento de venta
                ClsSerie ObjSerie2 = new ClsSerie();
                string vPrefSerie = "";
                if (!ObjSerie2.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
                {
                    //MessageBox.Show("Error no se encontró el comprobante y la serie de la Nota de Credito, VERIFIQUE!!!", "SISTEMA");
                    //return;
                    vPrefSerie = "";
                }
                else
                {
                    vPrefSerie = ObjSerie2.PrefijoSerie;
                }

                var documentoRelacionado = new DocumentoRelacionado();

                //Documento Relacionado
                vMod = "VEN";
                ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
                switch (ObjDocumento.Codigo)
                {
                    case "013":
                        vTDocRel = "03";
                        break;
                    case "014":
                        vTDocRel = "01";
                        break;
                }

                documentoRelacionado.TipoDocumento = vTDocRel.ToString();
                documentoRelacionado.NroDocumento = vPrefSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

                _documento.Relacionados.Add(documentoRelacionado);

                var discrepancia = new Discrepancia();

                //Discrepancia
                //Buscando Motivo
                vParam = "1";
                vCodCat = "021";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotDeb.TMotivo, vParam.ToString());

                discrepancia.Tipo = ObjDetCatalogo.CodDetCat;
                discrepancia.NroReferencia = vPrefSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
                discrepancia.Descripcion = ObjNotDeb.Observaciones;

                _documento.Discrepancias.Add(discrepancia);

                int vId = 1;
                if (datosDeb.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datosDeb.Tables[0].Rows)
                    {
                        var detalle = new DetalleDocumento();

                        detalle.Id = vId;
                        detalle.CodigoItem = fila[0].ToString();
                        if (!ObjProducto.BuscarProducto(detalle.CodigoItem, vRucEmpresa, vAlmacen))
                        {
                            MessageBox.Show("Error no se encontró el producto " + fila[0].ToString() + ", VERIFIQUE!!!", "SISTEMA");
                            return;
                        }
                        detalle.Descripcion = ObjProducto.Nombre;
                        detalle.PrecioUnitario = Decimal.Parse(fila[5].ToString());
                        detalle.PrecioReferencial = Decimal.Parse(fila[5].ToString());
                        //detalle.TipoPrecio = fila[9].ToString();
                        detalle.Cantidad = Decimal.Parse(fila[6].ToString());
                        detalle.UnidadMedida = "NIU";
                        detalle.Impuesto = Decimal.Parse(fila[7].ToString());
                        //detalle.TipoImpuesto = fila[10].ToString();
                        detalle.TotalVenta = Decimal.Parse(fila[8].ToString());
                        _documento.Items.Add(detalle);



                        vId = vId + 1;
                    }
                }
                else
                {

                    var detalle = new DetalleDocumento();

                    detalle.Id = vId;
                    detalle.CodigoItem = "-";
                    /*if (!ObjProducto.BuscarProducto(detalle.CodigoItem, vRucEmpresa, vAlmacen))
                    {
                        MessageBox.Show("Error no se encontró el producto " + fila[0].ToString() + ", VERIFIQUE!!!", "SISTEMA");
                        return;
                    }*/
                    detalle.Descripcion = "-";
                    //detalle.PrecioUnitario = 0;
                    //detalle.PrecioReferencial = 0;
                    //detalle.TipoPrecio = fila[9].ToString();
                    //detalle.Cantidad = 0;
                    detalle.UnidadMedida = "-";
                    //detalle.Impuesto = 0;
                    //detalle.TipoImpuesto = fila[10].ToString();
                    //detalle.TotalVenta = 0;
                    _documento.Items.Add(detalle);

                    //Documento Relacionado
                    vMod = "VEN";
                    ObjDocumento.BuscarDocNCortoMod(ObjVenta.Doc, vMod);
                    switch (ObjDocumento.Codigo)
                    {
                        case "013":
                            vTDocRel = "03";
                            break;
                        case "014":
                            vTDocRel = "01";
                            break;
                    }
                    documentoRelacionado.TipoDocumento = vTDocRel.ToString();
                    documentoRelacionado.NroDocumento = vPrefSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
                    _documento.Relacionados.Add(documentoRelacionado);

                    //Discrepancia
                    //Buscando Motivo
                    vParam = "1";
                    vCodCat = "021";
                    ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjNotDeb.TMotivo, vParam.ToString());

                    discrepancia.Tipo = ObjDetCatalogo.CodDetCat;
                    discrepancia.NroReferencia = vPrefSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
                    discrepancia.Descripcion = ObjNotDeb.Observaciones;
                    _documento.Discrepancias.Add(discrepancia);
                }

                //-------------------------------------------------//

                string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivoini);
                string vUrlApiSunat = "";
                vUrlApiSunat = ciniarchivo.ReadValue("Configura", "UrlApi", "");

                //var proxy = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BicimotoApi"]) };

                var proxy = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

                string metodoApi;
                switch (_documento.TipoDocumento)
                {
                    case "07":
                        metodoApi = "api/GenerarNotaCredito";
                        break;
                    case "08":
                        metodoApi = "api/GenerarNotaDebito";
                        break;
                    default:
                        metodoApi = "api/GenerarFactura";
                        break;
                }

                var response = await proxy.PostAsJsonAsync(metodoApi, _documento);
                var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();
                if (!respuesta.Exito)
                    throw new ApplicationException(respuesta.MensajeError);

                //Generando carpeta del dia
                DateTime fechaHoy = DateTime.Now;
                string fecha = fechaHoy.ToString("d");
                string fechaAnio = fecha.Substring(6, 4);
                string fechaMes = fecha.Substring(3, 2);
                string fechaDia = fecha.Substring(0, 2);
                string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"ND", $"{rutafec}");

                RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"ND", $"{rutafec}",
                    $"{_documento.IdDocumento}.xml");

                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }

                /*using (MySqlConnection conn = GetNewConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE tblventa set ArchXml = @Archivo, NomArchXml = @NomArchXml where IdVenta = @IdVenta and Empresa = @numruc and Almacen = @Almacen";
                        cmd.Parameters.AddWithValue("@Archivo", respuesta.TramaXmlSinFirma);
                        cmd.Parameters.AddWithValue("@NomArchXml", $"{_documento.IdDocumento}.xml");
                        cmd.Parameters.AddWithValue("@IdNc", vIdNc.ToString());
                        cmd.Parameters.AddWithValue("@numruc", vRucEmpresa.ToString());
                        cmd.Parameters.AddWithValue("@Almacen", vAlmacen.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }*/

                File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(respuesta.TramaXmlSinFirma));

                IdDocumento = _documento.IdDocumento;
                string vUsuario = FrmLogin.x_login_usuario;

                string archivo = System.Environment.CurrentDirectory + @"\base.ini";

                //MessageBox.Show("Llega");
                try
                {
                    enviarXML(_documento.TipoDocumento, vIdNd, codDoc, ObjNotDeb.Serie, ObjNotDeb.Numero, _documento.IdDocumento, RutaArchivo, respuesta.TramaXmlSinFirma, vRucEmpresa.ToString(), vAlmacen.ToString(), vUsuario);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Hubo problemas al leer el archivo INI. no se puede realizar el envío del comprobante electrónico" + ex.Message, "SISTEMA");
                }

                //DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public async void generaXMLBaja(string vId, string vEnvio, string vRucEmpresa, Boolean vTEnvio)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClsEmpresa ObjEmpresa = new ClsEmpresa();
                ClsUbigeo ObjUbigeo = new ClsUbigeo();
                ClsDetComunicacionBaja ObjDetComunicacionBaja = new ClsDetComunicacionBaja();
                ClsDocumento ObjDocumento = new ClsDocumento();
                ClsSerie ObjSerie = new ClsSerie();

                //Cargar Datos de Empresa
                if (!ObjEmpresa.BuscarRuc(vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos de Empresa, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                //Aperturar Documento Baja
                _documentoBaja = new ComunicacionBaja();

                //Emisor

                _Emisor = new Contribuyente();

                _Emisor.TipoDocumento = "6";
                _Emisor.NroDocumento = ObjEmpresa.Ruc;
                _Emisor.NombreComercial = ObjEmpresa.Razon;
                _Emisor.NombreLegal = ObjEmpresa.NombreLegal;
                _Emisor.Ubigeo = ObjEmpresa.Ubigeo;
                _Emisor.Direccion = ObjEmpresa.Direccion;



                if (!ObjUbigeo.BuscarDpto(ObjEmpresa.Region))
                {
                    MessageBox.Show("Error no se encontró datos de Departamento del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Departamento = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarProv(ObjEmpresa.Region, ObjEmpresa.Provincia))
                {
                    MessageBox.Show("Error no se encontró datos de Provincia del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Provincia = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarDist(ObjEmpresa.Region, ObjEmpresa.Provincia, ObjEmpresa.Distrito))
                {
                    MessageBox.Show("Error no se encontró datos de Distrito del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Distrito = ObjUbigeo.NOMBRE;
                }
                _Emisor.Urbanizacion = ObjEmpresa.Urbanizacion;

                _documentoBaja.Emisor = _Emisor;

                string vFecha = "";
                DataSet datosCom = csql.dataset_cadena("Call SpComunicacionBajaBuscar('" + vId.ToString() + "','" + vEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");

                if (datosCom.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datosCom.Tables[0].Rows)
                    {
                        vFecha = fila[0].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron la información de la Comunicación de Baja", "SISTEMA");
                    return;
                }
                //Fecha
                string dia = vFecha.Substring(0, 2);
                string mes = vFecha.Substring(3, 2);
                string anio = vFecha.Substring(6, 4);
                string vFechaEmision = dia.ToString() + "/" + mes.ToString() + "/" + anio.ToString();

                _documentoBaja.FechaEmision = vFechaEmision;
                _documentoBaja.FechaReferencia = vFechaEmision;

                _documentoBaja.IdDocumento = vEnvio.ToString() + "-" + vId.ToString();

                //Detalles de Comunicación

                string vDoc = "";

                DataSet datos = csql.dataset_cadena("Call SpDetComunicacionBajaBuscar('" + vId.ToString() + "','" + vEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");
                List<DocumentoBaja> _listadetalleBaja = new List<DocumentoBaja>();

                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        var detalle = new DocumentoBaja();

                        detalle.Id = Int32.Parse(fila[2].ToString());
                        vDoc = fila[3].ToString();
                        switch (vDoc)
                        {
                            case "013":
                                detalle.TipoDocumento = "03";
                                break;
                            case "014":
                                detalle.TipoDocumento = "01";
                                break;
                        }
                        //buscra serie
                        //ObjDocumento.BuscarDocNomMod(fila[7].ToString(), "VEN");
                        if (!ObjSerie.BuscarDocSerie(fila[3].ToString(), fila[4].ToString()))
                        {
                            MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                            return;
                        }

                        detalle.Serie = ObjSerie.PrefijoSerie + fila[4].ToString();
                        detalle.Correlativo = fila[5].ToString();
                        detalle.MotivoBaja = fila[6].ToString();

                        _listadetalleBaja.Add(detalle);

                    }

                    _documentoBaja.Bajas = _listadetalleBaja;


                }
                else
                {
                    MessageBox.Show("No se encontraron detalles de la Comunicación de Baja", "SISTEMA");
                    return;
                }

                string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivoini);
                string vUrlApiSunat = "";
                vUrlApiSunat = ciniarchivo.ReadValue("Configura", "UrlApi", "");

                //var proxy = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BicimotoApi"]) };

                var proxy = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

                string metodoApi = "api/GenerarComunicacionBaja";

                var response = await proxy.PostAsJsonAsync(metodoApi, _documentoBaja);
                var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();
                if (!respuesta.Exito)
                    throw new ApplicationException(respuesta.MensajeError);

                if (vTEnvio)
                {
                    //Generando carpeta del dia
                    DateTime fechaHoy = DateTime.Now;
                    string fecha = fechaHoy.ToString("d");
                    string fechaAnio = fecha.Substring(6, 4);
                    string fechaMes = fecha.Substring(3, 2);
                    string fechaDia = fecha.Substring(0, 2);
                    string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                    string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLNB", $"{rutafec}");

                    RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLNB", $"{rutafec}",
                        $"{_documentoBaja.IdDocumento}.xml");

                    if (!(Directory.Exists(carpeta)))
                    {
                        Directory.CreateDirectory(carpeta);
                    }

                }
                else
                {
                    string fechaAnio = vFecha.Substring(6, 4);
                    string fechaMes = vFecha.Substring(3, 2);
                    string fechaDia = vFecha.Substring(0, 2);
                    string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                    string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLNB", $"{rutafec}");

                    RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLNB", $"{rutafec}",
                        $"{_documentoBaja.IdDocumento}.xml");

                    if (!(Directory.Exists(carpeta)))
                    {
                        Directory.CreateDirectory(carpeta);
                    }
                }



                File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(respuesta.TramaXmlSinFirma));

                string vTipDoc = "RA";
                string vUsuario = FrmLogin.x_login_usuario;

                if (vTEnvio)
                {
                    string archivo = System.Environment.CurrentDirectory + @"\base.ini";

                    //MessageBox.Show("Llega");
                    try
                    {

                        enviarXMLComunicacion(vTipDoc.ToString(), int.Parse(vId.ToString()), vEnvio.ToString(), RutaArchivo, respuesta.TramaXmlSinFirma, vRucEmpresa.ToString(), vUsuario);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Hubo problemas al leer el archivo INI. no se puede realizar el envío del comprobante electrónico" + ex.Message, "SISTEMA");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public async void generaXMLResumen1(string vId, string vEnvio, string vRucEmpresa, Boolean vTEnvio)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClsEmpresa ObjEmpresa = new ClsEmpresa();
                ClsUbigeo ObjUbigeo = new ClsUbigeo();
                ClsDetResumenEnvioAgrupado ObjDetResumenEnvio = new ClsDetResumenEnvioAgrupado();
                ClsDocumento ObjDocumento = new ClsDocumento();
                ClsSerie ObjSerie = new ClsSerie();

                //Cargar Datos de Empresa
                if (!ObjEmpresa.BuscarRuc(vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos de Empresa, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                //Aperturar Documento Baja
                _documentoResumenAgrupado = new ResumenDiario();

                //Emisor

                _Emisor = new Contribuyente();

                _Emisor.TipoDocumento = "6";
                _Emisor.NroDocumento = ObjEmpresa.Ruc;
                _Emisor.NombreComercial = ObjEmpresa.Razon;
                _Emisor.NombreLegal = ObjEmpresa.NombreLegal;
                _Emisor.Ubigeo = ObjEmpresa.Ubigeo;
                _Emisor.Direccion = ObjEmpresa.Direccion;



                if (!ObjUbigeo.BuscarDpto(ObjEmpresa.Region))
                {
                    MessageBox.Show("Error no se encontró datos de Departamento del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Departamento = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarProv(ObjEmpresa.Region, ObjEmpresa.Provincia))
                {
                    MessageBox.Show("Error no se encontró datos de Provincia del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Provincia = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarDist(ObjEmpresa.Region, ObjEmpresa.Provincia, ObjEmpresa.Distrito))
                {
                    MessageBox.Show("Error no se encontró datos de Distrito del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Distrito = ObjUbigeo.NOMBRE;
                }
                _Emisor.Urbanizacion = ObjEmpresa.Urbanizacion;

                _documentoResumenAgrupado.Emisor = _Emisor;

                string vFecha = "";
                DataSet datosCom = csql.dataset_cadena("Call SpResumenBuscar('" + vId.ToString() + "','" + vEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");

                if (datosCom.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datosCom.Tables[0].Rows)
                    {
                        vFecha = fila[0].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron la información de la Comunicación de Baja", "SISTEMA");
                    return;
                }

                string fecha = "";
                string fechaAnio = "";
                string fechaMes = "";
                string fechaDia = "";
                string fechaHoyEnvio = "";

                if (vTEnvio)
                {
                    //fecha = vFecha.ToString("d");
                    fechaAnio = vFecha.Substring(6, 4);
                    fechaMes = vFecha.Substring(3, 2);
                    fechaDia = vFecha.Substring(0, 2);


                }
                else
                {

                    //Generando carpeta fecha del dia
                    DateTime fechaHoy = DateTime.Now;
                    fecha = fechaHoy.ToString("d");
                    fechaAnio = fecha.Substring(6, 4);
                    fechaMes = fecha.Substring(3, 2);
                    fechaDia = fecha.Substring(0, 2);
                }

                string fechaHoyAnio = "";
                string fechaHoyMes = "";
                string fechaHoyDia = "";

                DateTime fechaEnvioHoy = DateTime.Now;
                fechaHoyEnvio = fechaEnvioHoy.ToString("d");
                fechaHoyAnio = fechaHoyEnvio.Substring(6, 4);
                fechaHoyMes = fechaHoyEnvio.Substring(3, 2);
                fechaHoyDia = fechaHoyEnvio.Substring(0, 2);

                //Fecha
                string dia = vFecha.Substring(0, 2);
                string mes = vFecha.Substring(3, 2);
                string anio = vFecha.Substring(6, 4);
                string vFechaEmision = dia.ToString() + "/" + mes.ToString() + "/" + anio.ToString();
                string vFechaHoy = fechaHoyDia.ToString() + "/" + fechaHoyMes.ToString() + "/" + fechaHoyAnio.ToString();
                _documentoResumenAgrupado.FechaEmision = vFechaHoy;
                _documentoResumenAgrupado.FechaReferencia = vFechaEmision;

                _documentoResumenAgrupado.IdDocumento = vEnvio.ToString() + "-" + vId.ToString();

                //Detalles de Comunicación

                string vDoc = "";

                DataSet datos = csql.dataset_cadena("Call SpDetResumenAgrupadoBuscar('" + vId.ToString() + "','" + vEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");
                List<GrupoResumen> _listadetalleResumen = new List<GrupoResumen>();

                ClsVenta ObjComprobanteBol = new ClsVenta();
                ClsVenta ObjComprobanteFac = new ClsVenta();
                ClsNotCre ObjComprobanteNc = new ClsNotCre();
                ClsNotDeb ObjComprobanteNd = new ClsNotDeb();

                if (datos.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        var detalle = new GrupoResumen();

                        detalle.Id = Int32.Parse(fila[2].ToString());
                        vDoc = fila[3].ToString();
                        switch (vDoc)
                        {
                            case "013":
                                detalle.TipoDocumento = "03";
                                break;
                            case "015":
                                detalle.TipoDocumento = "07";
                                break;
                            case "016":
                                detalle.TipoDocumento = "08";
                                break;
                        }

                        if (!ObjSerie.BuscarDocSerie(vDoc.ToString(), fila[4].ToString()))
                        {
                            MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                            return;
                        }

                        detalle.Serie = ObjSerie.PrefijoSerie + fila[4].ToString();

                        detalle.CorrelativoInicio = int.Parse(fila[5].ToString());
                        detalle.CorrelativoFin = int.Parse(fila[6].ToString());

                        detalle.Moneda = "PEN";

                        detalle.TotalVenta = Decimal.Parse(fila[11].ToString());
                        detalle.Gravadas = Decimal.Parse(fila[7].ToString());
                        detalle.Exoneradas = Decimal.Parse(fila[8].ToString());
                        detalle.Inafectas = 0;
                        detalle.Exportacion = 0;
                        detalle.Gratuitas = Decimal.Parse(fila[9].ToString());
                        detalle.TotalDescuentos = 0;
                        detalle.TotalIsc = 0;
                        detalle.TotalIgv = Decimal.Parse(fila[10].ToString());
                        detalle.TotalOtrosImpuestos = 0;

                        _listadetalleResumen.Add(detalle);

                    }

                    _documentoResumenAgrupado.Resumenes = _listadetalleResumen;

                }
                else
                {
                    MessageBox.Show("No se encontraron detalles de la Comunicación de Baja", "SISTEMA");
                    return;
                }

                string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivoini);
                string vUrlApiSunat = "";
                vUrlApiSunat = ciniarchivo.ReadValue("Configura", "UrlApi", "");

                //var proxy = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BicimotoApi"]) };

                var proxy = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

                string metodoApi = "api/GenerarResumenDiario/v1";

                var response = await proxy.PostAsJsonAsync(metodoApi, _documentoResumenAgrupado);

                var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();
                if (!respuesta.Exito)
                    throw new ApplicationException(respuesta.MensajeError);



                string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLRE", $"{rutafec}");

                RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLRE", $"{rutafec}",
                    $"{_documentoResumenAgrupado.IdDocumento}.xml");

                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }


                File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(respuesta.TramaXmlSinFirma));

                string vTipDoc = "RC";
                string vUsuario = FrmLogin.x_login_usuario;

                if (vTEnvio)
                {
                    string archivo = System.Environment.CurrentDirectory + @"\base.ini";

                    try
                    {
                        enviarXMLResumen(vTipDoc.ToString(), int.Parse(vId.ToString()), vEnvio.ToString(), RutaArchivo, respuesta.TramaXmlSinFirma, vRucEmpresa.ToString(), vUsuario);
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show("Hubo problemas al leer el archivo INI. no se puede realizar el envío del comprobante electrónico" + ex.Message, "SISTEMA");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public async void generaXMLResumen2(string vId, string vEnvio, string vRucEmpresa, bool vTEnvio)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                ClsEmpresa ObjEmpresa = new ClsEmpresa();
                ClsUbigeo ObjUbigeo = new ClsUbigeo();
                ClsDetResumenEnvio ObjDetResumenEnvio = new ClsDetResumenEnvio();
                ClsDocumento ObjDocumento = new ClsDocumento();
                ClsSerie ObjSerie = new ClsSerie();

                //Cargar Datos de Empresa
                if (!ObjEmpresa.BuscarRuc(vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos de Empresa, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                //Aperturar Documento Baja
                _documentoResumen = new ResumenDiarioNuevo();

                //Emisor

                _Emisor = new Contribuyente();

                _Emisor.TipoDocumento = "6";
                _Emisor.NroDocumento = ObjEmpresa.Ruc;
                _Emisor.NombreComercial = ObjEmpresa.Razon;
                _Emisor.NombreLegal = ObjEmpresa.NombreLegal;
                _Emisor.Ubigeo = ObjEmpresa.Ubigeo;
                _Emisor.Direccion = ObjEmpresa.Direccion;



                if (!ObjUbigeo.BuscarDpto(ObjEmpresa.Region))
                {
                    MessageBox.Show("Error no se encontró datos de Departamento del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Departamento = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarProv(ObjEmpresa.Region, ObjEmpresa.Provincia))
                {
                    MessageBox.Show("Error no se encontró datos de Provincia del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Provincia = ObjUbigeo.NOMBRE;
                }
                if (!ObjUbigeo.BuscarDist(ObjEmpresa.Region, ObjEmpresa.Provincia, ObjEmpresa.Distrito))
                {
                    MessageBox.Show("Error no se encontró datos de Distrito del Emisor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
                else
                {
                    _Emisor.Distrito = ObjUbigeo.NOMBRE;
                }
                _Emisor.Urbanizacion = ObjEmpresa.Urbanizacion;

                _documentoResumen.Emisor = _Emisor;

                string vFecha = "";
                DataSet datosCom = csql.dataset_cadena("Call SpResumenBuscar('" + vId.ToString() + "','" + vEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");

                if (datosCom.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datosCom.Tables[0].Rows)
                    {
                        vFecha = fila[0].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron la información del resumen diario", "SISTEMA");
                    return;
                }

                //Generando carpeta fecha del dia
                DateTime fechaHoy = DateTime.Now;
                string fecha = fechaHoy.ToString("d");
                string fechaAnio = fecha.Substring(6, 4);
                string fechaMes = fecha.Substring(3, 2);
                string fechaDia = fecha.Substring(0, 2);

                //Fecha
                string dia = vFecha.Substring(0, 2);
                string mes = vFecha.Substring(3, 2);
                string anio = vFecha.Substring(6, 4);
                string vFechaEmision = dia.ToString() + "/" + mes.ToString() + "/" + anio.ToString();
                string vFechaHoy = fechaDia.ToString() + "/" + fechaMes.ToString() + "/" + fechaAnio.ToString();
                _documentoResumen.FechaEmision = vFechaHoy;
                _documentoResumen.FechaReferencia = vFechaEmision;

                _documentoResumen.IdDocumento = vEnvio.ToString() + "-" + vId.ToString();

                //Detalles de Comunicación

                string vDoc = "";

                //DataSet datos = csql.dataset_cadena("Call SpDetResumenBuscar('" + vId.ToString() + "','" + vEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");
                DataSet datos = csql.dataset_cadena("Call SpDetResumenAgrupadoBuscar('" + vId.ToString() + "','" + vEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");
                List<GrupoResumenNuevo> _listadetalleResumen = new List<GrupoResumenNuevo>();

                ClsVenta ObjComprobanteBol = new ClsVenta();
                ClsVenta ObjComprobanteFac = new ClsVenta();
                ClsNotCre ObjComprobanteNc = new ClsNotCre();
                ClsNotDeb ObjComprobanteNd = new ClsNotDeb();

                string vParam = "";
                string vCodCat = "";

                if (datos.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        var detalle = new GrupoResumenNuevo();

                        detalle.Id = Int32.Parse(fila[2].ToString());
                        vDoc = fila[3].ToString();


                        switch (vDoc)
                        {
                            case "013":
                                string codAlmacen = FrmLogin.x_CodAlmacen;

                                detalle.TipoDocumento = "03";


                                if (!ObjComprobanteBol.BuscarVenta(fila[12].ToString(), vRucEmpresa, codAlmacen))
                                {
                                    MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                ObjDocumento.BuscarDocNomMod(ObjComprobanteBol.Doc, "VEN");
                                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjComprobanteBol.Serie))
                                {
                                    MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                detalle.IdDocumento = ObjSerie.PrefijoSerie.ToString() + fila[4].ToString() + "-" + fila[5].ToString();
                                detalle.NroDocumentoReceptor = ObjComprobanteBol.Cliente.ToString();

                                vParam = "2";
                                vCodCat = "018";

                                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjComprobanteBol.TipDocCli, vParam.ToString());


                                if (ObjComprobanteBol.Cliente.Substring(0, 1).Equals("C"))
                                {
                                    detalle.TipoDocumentoReceptor = "1";
                                }
                                else
                                {
                                    detalle.TipoDocumentoReceptor = ObjDetCatalogo.CodDetCat;
                                }


                                detalle.CodigoEstadoItem = int.Parse(fila[13].ToString()); // int.Parse(fila[7].ToString());

                                vParam = "2";
                                vCodCat = "001";
                                if (!ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjComprobanteBol.TMoneda, vParam.ToString()))
                                {
                                    MessageBox.Show("Error no se encontró tipo de moneda, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }
                                if (ObjDetCatalogo.CodDetCat.Equals("001"))
                                {
                                    detalle.Moneda = "PEN";
                                }
                                else
                                {
                                    detalle.Moneda = "USD";
                                }

                                detalle.TotalVenta = Decimal.Parse(ObjComprobanteBol.Total.ToString());
                                detalle.Gravadas = Decimal.Parse(ObjComprobanteBol.TBruto.ToString());
                                detalle.Exoneradas = Decimal.Parse(ObjComprobanteBol.TExonerada.ToString());
                                detalle.Inafectas = Decimal.Parse(ObjComprobanteBol.TInafecta.ToString());
                                detalle.Exportacion = 0;
                                detalle.Gratuitas = Decimal.Parse(ObjComprobanteBol.TGratuita.ToString());
                                detalle.TotalDescuentos = Decimal.Parse(ObjComprobanteBol.Dcto.ToString());
                                detalle.TotalIsc = 0;

                                //Obtener Tipo de afectación IGV
                                string nParam1 = "11";
                                string vTipAfecta = "";
                                double vIgv = 0;
                                double valIgv = 0;
                                double vImp = 0;
                                double vPrec = 0;

                                if (ObjParametro.BuscarParametro(nParam1))
                                {
                                    vTipAfecta = ObjParametro.Valor;
                                }

                                //igv
                                //Obtener IGV
                                string nParam = "3";
                                if (ObjParametro.BuscarParametro(nParam))
                                {
                                    valIgv = Double.Parse(ObjParametro.Valor.ToString());
                                }

                                if (vTipAfecta.Equals("001"))
                                {
                                    vImp = Double.Parse(ObjComprobanteBol.TExonerada.ToString().Equals("") ? "0" : ObjComprobanteBol.TExonerada.ToString());
                                    vIgv = (vImp * valIgv) / (100 + valIgv);

                                }
                                else
                                {
                                    vPrec = Double.Parse(ObjComprobanteBol.TExonerada.ToString().Equals("") ? "0" : ObjComprobanteBol.TExonerada.ToString());
                                    vIgv = (vPrec * valIgv) / (100);
                                    vImp = vPrec + vIgv;
                                }

                                detalle.TotalIgv = Decimal.Parse(vIgv.ToString());
                                //detalle.TotalIgv = Decimal.Parse(ObjComprobanteBol.TIgv.ToString());
                                detalle.TotalOtrosImpuestos = 0;

                                break;

                            case "015":
                                detalle.TipoDocumento = "07";


                                if (!ObjComprobanteNc.BuscarNC(fila[6].ToString(), vRucEmpresa, fila[9].ToString()))
                                {
                                    MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                ObjDocumento.BuscarDocNomMod(ObjComprobanteNc.Doc, "VEN");
                                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjComprobanteNc.Serie))
                                {
                                    MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                detalle.IdDocumento = ObjSerie.PrefijoSerie + fila[4].ToString() + "-" + fila[5].ToString();
                                detalle.NroDocumentoReceptor = ObjComprobanteNc.Cliente.ToString();

                                vParam = "2";
                                vCodCat = "018";

                                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjComprobanteNc.TipDocCli, vParam.ToString());

                                if (ObjComprobanteNc.Cliente.Substring(0, 1).Equals("C"))
                                {
                                    detalle.TipoDocumentoReceptor = "1";
                                }
                                else
                                {
                                    detalle.TipoDocumentoReceptor = ObjDetCatalogo.CodDetCat;
                                }

                                detalle.TipoDocumentoReceptor = ObjComprobanteNc.TipDocCli.ToString();
                                if (!ObjComprobanteFac.BuscarVenta(ObjComprobanteNc.IdVenta.ToString(), vRucEmpresa, fila[9].ToString()))
                                {
                                    MessageBox.Show("Error no se encontró datos de Venta de la nota de crédito, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }



                                if (!ObjDocumento.BuscarDocNomMod(ObjComprobanteFac.Doc.ToString(), "VEN"))
                                {
                                    MessageBox.Show("Error no se encontró datos de comprobante de Venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }
                                switch (ObjDocumento.Codigo.ToString())
                                {
                                    case "013":
                                        detalle.TipoDocumentoRelacionado = "03";
                                        break;
                                    case "014":
                                        detalle.TipoDocumentoRelacionado = "03";
                                        break;
                                }
                                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjComprobanteFac.Serie))
                                {
                                    MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                detalle.DocumentoRelacionado = ObjSerie.PrefijoSerie.ToString() + ObjComprobanteFac.Serie.ToString() + "-" + ObjComprobanteFac.Numero.ToString();

                                detalle.CodigoEstadoItem = int.Parse(fila[13].ToString());

                                vParam = "2";
                                vCodCat = "001";
                                if (!ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjComprobanteNc.TMoneda, vParam.ToString()))
                                {
                                    MessageBox.Show("Error no se encontró tipo de moneda, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }
                                if (ObjDetCatalogo.CodDetCat.Equals("001"))
                                {
                                    detalle.Moneda = "PEN";
                                }
                                else
                                {
                                    detalle.Moneda = "USD";
                                }

                                //detalle.Moneda = ObjComprobanteNc.TMoneda.ToString();
                                detalle.TotalVenta = Decimal.Parse(ObjComprobanteNc.Total.ToString());
                                detalle.Gravadas = Decimal.Parse(ObjComprobanteNc.TBruto.ToString());
                                detalle.Exoneradas = 0;
                                detalle.Inafectas = 0;
                                detalle.Exportacion = 0;
                                detalle.Gratuitas = 0;
                                detalle.TotalDescuentos = 0;
                                detalle.TotalIsc = 0;
                                detalle.TotalIgv = Decimal.Parse(ObjComprobanteNc.TIgv.ToString());
                                detalle.TotalOtrosImpuestos = 0;
                                break;
                            case "016":
                                detalle.TipoDocumento = "08";


                                if (!ObjComprobanteNd.BuscarND(fila[6].ToString(), vRucEmpresa, fila[9].ToString()))
                                {
                                    MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                ObjDocumento.BuscarDocNomMod(ObjComprobanteNd.Doc, "VEN");
                                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjComprobanteNd.Serie))
                                {
                                    MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                detalle.IdDocumento = ObjSerie.PrefijoSerie + fila[4].ToString() + "-" + fila[5].ToString();
                                detalle.NroDocumentoReceptor = ObjComprobanteNd.Cliente.ToString();

                                vParam = "2";
                                vCodCat = "018";

                                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjComprobanteNd.TipDocCli, vParam.ToString());

                                if (ObjComprobanteNd.Cliente.Substring(0, 1).Equals("C"))
                                {
                                    detalle.TipoDocumentoReceptor = "1";
                                }
                                else
                                {
                                    detalle.TipoDocumentoReceptor = ObjDetCatalogo.CodDetCat;
                                }



                                if (!ObjComprobanteFac.BuscarVenta(ObjComprobanteNd.IdVenta.ToString(), vRucEmpresa, fila[9].ToString()))
                                {
                                    MessageBox.Show("Error no se encontró datos de Venta de la nota de crédito, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                if (!ObjDocumento.BuscarDocNomMod(ObjComprobanteFac.Doc.ToString(), "VEN"))
                                {
                                    MessageBox.Show("Error no se encontró datos de comprobante de Venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }
                                switch (ObjDocumento.Codigo.ToString())
                                {
                                    case "013":
                                        detalle.TipoDocumentoRelacionado = "03";
                                        break;
                                    case "014":
                                        detalle.TipoDocumentoRelacionado = "03";
                                        break;
                                }
                                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjComprobanteFac.Serie))
                                {
                                    MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }

                                detalle.DocumentoRelacionado = ObjSerie.PrefijoSerie.ToString() + ObjComprobanteFac.Serie.ToString() + "-" + ObjComprobanteFac.Numero.ToString();

                                detalle.CodigoEstadoItem = int.Parse(fila[13].ToString());

                                vParam = "2";
                                vCodCat = "001";
                                if (!ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjComprobanteNd.TMoneda, vParam.ToString()))
                                {
                                    MessageBox.Show("Error no se encontró tipo de moneda, VERIFIQUE!!!", "SISTEMA");
                                    return;
                                }
                                if (ObjDetCatalogo.CodDetCat.Equals("001"))
                                {
                                    detalle.Moneda = "PEN";
                                }
                                else
                                {
                                    detalle.Moneda = "USD";
                                }

                                //detalle.Moneda = ObjComprobanteNd.TMoneda.ToString();
                                detalle.TotalVenta = Decimal.Parse(ObjComprobanteNd.Total.ToString());
                                detalle.Gravadas = Decimal.Parse(ObjComprobanteNd.TBruto.ToString());
                                detalle.Exoneradas = 0;
                                detalle.Inafectas = 0;
                                detalle.Exportacion = 0;
                                detalle.Gratuitas = 0;
                                detalle.TotalDescuentos = 0;
                                detalle.TotalIsc = 0;
                                detalle.TotalIgv = Decimal.Parse(ObjComprobanteNd.TIgv.ToString());
                                detalle.TotalOtrosImpuestos = 0;

                                break;
                        }
                        //buscra serie
                        //ObjDocumento.BuscarDocNomMod(fila[7].ToString(), "VEN");
                        /*if (!ObjSerie.BuscarDocSerie(fila[3].ToString(), fila[4].ToString()))
                        {
                            MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                            return;
                        }*/


                        _listadetalleResumen.Add(detalle);

                    }

                    _documentoResumen.Resumenes = _listadetalleResumen;

                }
                else
                {
                    MessageBox.Show("No se encontraron detalles del resumen diario", "SISTEMA");
                    return;
                }

                string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivoini);
                string vUrlApiSunat = "";
                vUrlApiSunat = ciniarchivo.ReadValue("Configura", "UrlApi", "");

                //var proxy = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BicimotoApi"]) };

                var proxy = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

                string metodoApi = "api/GenerarResumenDiario/v2";

                var response = await proxy.PostAsJsonAsync(metodoApi, _documentoResumen);
                var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();
                if (!respuesta.Exito)
                    throw new ApplicationException(respuesta.MensajeError);


                string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

                string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLRE", $"{rutafec}");

                RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XMLRE", $"{rutafec}",
                    $"{_documentoResumen.IdDocumento}.xml");

                if (!(Directory.Exists(carpeta)))
                {
                    Directory.CreateDirectory(carpeta);
                }

                File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(respuesta.TramaXmlSinFirma));

                string vTipDoc = "RC";
                string vUsuario = FrmLogin.x_login_usuario;

                if (vTEnvio)
                {
                    enviarXMLResumen(vTipDoc.ToString(), int.Parse(vId.ToString()), vEnvio.ToString(), RutaArchivo, respuesta.TramaXmlSinFirma, vRucEmpresa.ToString(), vUsuario);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public async void descargaXML(string vNomXml, string vArchXml, string vRucEmpresa, string vAlmacen)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Archivo xml|*.xml";
                saveFileDialog1.Title = "Cuardar archivo";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.InitialDirectory = "C:\\";
                saveFileDialog1.FileName = $"{vNomXml}.xml";
                saveFileDialog1.ShowDialog();

                RutaArchivo = saveFileDialog1.FileName;

                /*if (!ObjVenta.BuscarVenta(vIdVenta, vRucEmpresa, vAlmacen))
                {
                    MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }*/

                if (saveFileDialog1.FileName != "")
                {
                    //File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(respuesta.TramaXmlSinFirma));
                    File.WriteAllBytes(RutaArchivo, Convert.FromBase64String(vArchXml));
                }
                //IdDocumento = _documento.IdDocumento;

                //DialogResult = DialogResult.OK;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public async void enviarXML(string vTipDocSunat, string vIdComp, string vDoc, string vSerie, string vNumero, string vSerieCorr, string vXml, string vArchXml, string vEmpresa, string vAlmacen, string vUsuario)
        {
            string vresultado = "";
            ClsParametro ObjParametro = new ClsParametro();

            string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
            cini ciniarchivoini = new cini(archivoini);
            string vUrlApiSunat = "";
            vUrlApiSunat = ciniarchivoini.ReadValue("Configura", "UrlApi", "");

            _client = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string codigoTipoDoc = vTipDocSunat.ToString();

                var tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(vXml.ToString()));

                string vRuc = "";
                string nParam = "18";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vRuc = ObjParametro.Valor.ToString();
                }

                string vUserSol = "";
                nParam = "19";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vUserSol = ObjParametro.Valor.ToString();
                }

                string vPassSol = "";
                nParam = "20";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vPassSol = ObjParametro.Valor.ToString();
                }

                string vServerSunat = "";
                string vUrl = "";

                string archivo = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivo);

                vServerSunat = ciniarchivo.ReadValue("Configura", "Service", "");
                vUrl = ciniarchivo.ReadValue("Configura", "UrlService", "");

                var enviarDocumentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = vRuc,
                    //UsuarioSol = "SISBICI1",
                    //ClaveSol = "123server",
                    UsuarioSol = vUserSol,
                    ClaveSol = vPassSol,
                    /*UsuarioSol = "JB99AYBC",
                    ClaveSol = "p5vnyUrcW",*/
                    //HOMOLOGACION
                    //EndPointUrl = "https://www.sunat.gob.pe/ol-ti-itcpgem-sqa/billService",
                    //PRODUCION
                    //EndPointUrl = "https://e-factura.sunat.gob.pe/ol-ti-itcpfegem/billService",
                    //DESARROLLO
                    EndPointUrl = vUrl,
                    IdDocumento = vSerieCorr.ToString(),
                    TipoDocumento = codigoTipoDoc,
                    //TramaXmlFirmado = respuestaFirmado.TramaXmlFirmado
                    TramaXmlFirmado = tramaXmlSinFirma
                };

                //var apiMetodo = rbResumen.Checked ? "api/EnviarResumen" : "api/EnviarDocumento";
                var apiMetodo = "api/EnviarDocumento";
                var jsonEnvioDocumento = await _client.PostAsJsonAsync(apiMetodo, enviarDocumentoRequest);

                var respuestaEnvio = await jsonEnvioDocumento.Content.ReadAsAsync<EnviarDocumentoResponse>();

                //Grabando Envio
                DateTime fechaHoy = DateTime.Now;
                string fecha = fechaHoy.ToString("d");
                string fechaAnio = fecha.Substring(6, 4);
                string fechaMes = fecha.Substring(3, 2);
                string fechaDia = fecha.Substring(0, 2);
                string fecActual = fechaAnio.ToString() + "/" + fechaMes.ToString() + "/" + fechaDia.ToString();
                string hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + DateTime.Now.Second.ToString("D2");

                string nId = vIdComp.ToString() + vTipDocSunat.ToString() + fecActual.ToString() + hora.ToString();
                string vEst = "A";

                string vCodResp = "";
                string vError = "";
                string vMensaje = "";

                if (respuestaEnvio.CodigoRespuesta != null)
                {
                    vCodResp = respuestaEnvio.CodigoRespuesta.ToString();
                }

                if (respuestaEnvio.MensajeError != null)
                {
                    vError = respuestaEnvio.MensajeError.ToString();
                }

                if (respuestaEnvio.MensajeRespuesta != null)
                {
                    vMensaje = respuestaEnvio.MensajeRespuesta.ToString();
                }

                var rpta = (EnviarDocumentoResponse)respuestaEnvio;
                vresultado = $@"{Resources.procesoCorrecto}{Environment.NewLine}{rpta.MensajeRespuesta} siendo las {DateTime.Now}";

                Respuesta = $@"{vresultado.ToString()}{Environment.NewLine}{rpta.MensajeError}";

                /*if (vCodResp.Equals("") && respuestaEnvio.MensajeError != null && !respuestaEnvio.MensajeError.Equals(""))
                {
                    vError = "NO SE PUDO CONECTAR CON EL SERVICIO DE SUNAT";
                }*/

                ObjEnvio.Id = nId.ToString();
                ObjEnvio.Fecha = fecActual.ToString();
                ObjEnvio.IdComp = vIdComp.ToString();
                ObjEnvio.Doc = vDoc.ToString();
                ObjEnvio.Serie = vSerie.ToString();
                ObjEnvio.Numero = vNumero.ToString();
                ObjEnvio.Ticket = "";
                ObjEnvio.CodError = vCodResp.ToString(); //vError.ToString();
                ObjEnvio.MensajeError = vError.ToString().Replace("'", " "); //vMensaje.ToString();
                ObjEnvio.MensajeRespuesta = vresultado.ToString();
                ObjEnvio.ArchXml = vArchXml;
                ObjEnvio.NomArchXml = vSerieCorr;
                if ((vCodResp.Equals("") && vError.Equals("") && !vMensaje.Equals("")) || vCodResp.Equals("0"))
                {
                    vEst = "A";
                    ObjEnvio.Estado = vEst.ToString();

                }
                else
                {
                    vEst = "R";
                    ObjEnvio.Estado = vEst.ToString();
                }
                ObjEnvio.Empresa = vEmpresa.ToString();
                ObjEnvio.Almacen = vAlmacen.ToString();
                ObjEnvio.UserCreacion = vUsuario.ToString();
                ObjEnvio.ServSunat = vServerSunat.ToString();
                ObjEnvio.Crear();


                using (MySqlConnection conn = GetNewConnection())
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE tblenvio set ArchXml = @Archivo where Id = @Id and Empresa = @numruc and Almacen = @Almacen";
                        cmd.Parameters.AddWithValue("@Archivo", tramaXmlSinFirma);
                        cmd.Parameters.AddWithValue("@Id", nId.ToString().ToString());
                        cmd.Parameters.AddWithValue("@numruc", vEmpresa.ToString());
                        cmd.Parameters.AddWithValue("@Almacen", vAlmacen.ToString());
                        cmd.ExecuteNonQuery();
                    }
                }

                if (!respuestaEnvio.Exito)
                    throw new ApplicationException(respuestaEnvio.MensajeError);

                try
                {
                    string carpetaCDR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Program.CarpetaCdr}");

                    if (!(Directory.Exists(carpetaCDR)))
                    {
                        Directory.CreateDirectory(carpetaCDR);
                    }

                    string carpetaXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Program.CarpetaXml}");

                    if (!(Directory.Exists(carpetaXML)))
                    {
                        Directory.CreateDirectory(carpetaXML);
                    }

                    string RutaArchivoZIP = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Program.CarpetaCdr}", $"{respuestaEnvio.NombreArchivo}.zip");

                    File.WriteAllBytes($"{Program.CarpetaXml}\\{respuestaEnvio.NombreArchivo}.xml",
                                Convert.FromBase64String(tramaXmlSinFirma));

                    File.WriteAllBytes($"{Program.CarpetaCdr}\\R-{respuestaEnvio.NombreArchivo}.zip",
                        Convert.FromBase64String(respuestaEnvio.TramaZipCdr));

                    /*File.WriteAllBytes(RutaArchivoZIP,
                        Convert.FromBase64String(respuestaEnvio.TramaZipCdr));*/

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                IdEnvio = nId.ToString();
                objEnvio.ResultEnvio(IdEnvio, Respuesta);

            }
            catch (Exception ex)
            {
                //txtResult.Text = ex.Message;
                vresultado = ex.Message;
                Respuesta = vresultado.ToString();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }


        public async void enviarXMLComunicacion(string vTipDocSunat, int vId, string vIdComp, string vXml, string vArchXml, string vEmpresa, string vUsuario)
        {
            string vresultado = "";
            ClsParametro ObjParametro = new ClsParametro();

            string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
            cini ciniarchivoini = new cini(archivoini);
            string vUrlApiSunat = "";
            vUrlApiSunat = ciniarchivoini.ReadValue("Configura", "UrlApi", "");

            _client = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(vXml.ToString()));

                string vRuta = "";
                string nParam = "16";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vRuta = ObjParametro.Valor.ToString();
                }

                string vPass = "";
                nParam = "17";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vPass = ObjParametro.Valor.ToString();
                }

                var firmadoRequest = new FirmadoRequest
                {
                    TramaXmlSinFirma = tramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(vRuta.ToString())),
                    PasswordCertificado = vPass.ToString(),
                    UnSoloNodoExtension = true
                };

                var jsonFirmado = await _client.PostAsJsonAsync("api/Firmar", firmadoRequest);
                var respuestaFirmado = await jsonFirmado.Content.ReadAsAsync<FirmadoResponse>();
                if (!respuestaFirmado.Exito)
                    throw new ApplicationException(respuestaFirmado.MensajeError);

                string vRuc = "";
                nParam = "18";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vRuc = ObjParametro.Valor.ToString();
                }

                string vUserSol = "";
                nParam = "19";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vUserSol = ObjParametro.Valor.ToString();
                }

                string vPassSol = "";
                nParam = "20";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vPassSol = ObjParametro.Valor.ToString();
                }

                string vServerSunat = "";
                string vUrl = "";

                string archivo = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivo);

                vServerSunat = ciniarchivo.ReadValue("Configura", "Service", "");
                vUrl = ciniarchivo.ReadValue("Configura", "UrlService", "");

                /*            
                nParam = "22";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vUrl = ObjParametro.Valor.ToString();
                }*/

                var enviarDocumentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = vRuc,
                    //UsuarioSol = "SISBICI1",
                    //ClaveSol = "123server",
                    UsuarioSol = vUserSol,
                    ClaveSol = vPassSol,
                    /*UsuarioSol = "JB99AYBC",
                    ClaveSol = "p5vnyUrcW",*/
                    //HOMOLOGACION
                    //EndPointUrl = "https://www.sunat.gob.pe/ol-ti-itcpgem-sqa/billService",
                    //PRODUCION
                    //EndPointUrl = "https://e-factura.sunat.gob.pe/ol-ti-itcpfegem/billService",
                    //DESARROLLO
                    EndPointUrl = vUrl,
                    IdDocumento = vIdComp.ToString() + "-" + vId.ToString(),
                    TipoDocumento = vTipDocSunat,
                    TramaXmlFirmado = respuestaFirmado.TramaXmlFirmado
                };

                var apiMetodo = "api/EnviarResumen";
                var jsonEnvioDocumento = await _client.PostAsJsonAsync(apiMetodo, enviarDocumentoRequest);

                RespuestaComunConArchivo respuestaEnvio;

                respuestaEnvio = await jsonEnvioDocumento.Content.ReadAsAsync<EnviarResumenResponse>();
                var rpta = (EnviarResumenResponse)respuestaEnvio;
                string vMensaje = "";
                if (!rpta.Exito)
                {
                    vMensaje = rpta.MensajeError;
                    vMensaje = vMensaje.Replace("'", "");
                    using (MySqlConnection conn = GetNewConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "UPDATE tblcomunicabaja set Ticket = @nTicket, MensajeRespuesta = @Mensaje, ArchXml = @Archivo where Id = @Id and NDocBaja = @nNDocBaja and RucEmpresa = @numruc";
                            cmd.Parameters.AddWithValue("@nTicket", rpta.NroTicket.ToString());
                            cmd.Parameters.AddWithValue("@Mensaje", vMensaje);
                            cmd.Parameters.AddWithValue("@Archivo", firmadoRequest.TramaXmlSinFirma);
                            cmd.Parameters.AddWithValue("@Id", vId.ToString());
                            cmd.Parameters.AddWithValue("@nNDocBaja", vIdComp.ToString());
                            cmd.Parameters.AddWithValue("@numruc", vEmpresa.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    vMensaje = Resources.procesoCorrecto;
                    //"El XML no contiene el tag cac:TaxTotal/cac:TaxSubtotal/cbc:TaxAmount";
                    using (MySqlConnection conn = GetNewConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "UPDATE tblcomunicabaja set Ticket = @nTicket, MensajeRespuesta = @Mensaje, ArchXml = @Archivo where Id = @Id and NDocBaja = @nNDocBaja and RucEmpresa = @numruc";
                            cmd.Parameters.AddWithValue("@nTicket", rpta.NroTicket.ToString());
                            cmd.Parameters.AddWithValue("@Mensaje", vMensaje);
                            cmd.Parameters.AddWithValue("@Archivo", firmadoRequest.TramaXmlSinFirma);
                            cmd.Parameters.AddWithValue("@Id", vId.ToString());
                            cmd.Parameters.AddWithValue("@nNDocBaja", vIdComp.ToString());
                            cmd.Parameters.AddWithValue("@numruc", vEmpresa.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                //txtResult.Text = $@"{Resources.procesoCorrecto}{Environment.NewLine}{rpta.NroTicket}";

                vresultado = $@"{Resources.procesoCorrecto}{Environment.NewLine} N° de Ticket: {rpta.NroTicket} siendo las {DateTime.Now}";

                Respuesta = $@"{vresultado.ToString()}{Environment.NewLine}{rpta.MensajeError}";

                IdEnvio = vIdComp.ToString() + "-" + vId.ToString();
                objEnvio.ResultEnvio(IdEnvio, Respuesta);

                if (!respuestaEnvio.Exito)
                    using (MySqlConnection conn = GetNewConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "UPDATE tblcomunicabaja set Ticket = @nTicket, MensajeRespuesta = @Mensaje, ArchXml = @Archivo where Id = @Id and NDocBaja = @nNDocBaja and RucEmpresa = @numruc";
                            cmd.Parameters.AddWithValue("@nTicket", rpta.NroTicket.ToString());
                            cmd.Parameters.AddWithValue("@Mensaje", respuestaEnvio.MensajeError);
                            cmd.Parameters.AddWithValue("@Archivo", firmadoRequest.TramaXmlSinFirma);
                            cmd.Parameters.AddWithValue("@Id", vId.ToString());
                            cmd.Parameters.AddWithValue("@nNDocBaja", vIdComp.ToString());
                            cmd.Parameters.AddWithValue("@numruc", vEmpresa.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                throw new ApplicationException(respuestaEnvio.MensajeError);

            }
            catch (Exception ex)
            {
                //txtResult.Text = ex.Message;
                vresultado = ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        public async void enviarXMLResumen(string vTipDocSunat, int vId, string vIdComp, string vXml, string vArchXml, string vEmpresa, string vUsuario)
        {
            string vresultado = "";
            ClsParametro ObjParametro = new ClsParametro();

            string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
            cini ciniarchivoini = new cini(archivoini);
            string vUrlApiSunat = "";
            vUrlApiSunat = ciniarchivoini.ReadValue("Configura", "UrlApi", "");

            _client = new HttpClient { BaseAddress = new Uri(vUrlApiSunat) };

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                var tramaXmlSinFirma = Convert.ToBase64String(File.ReadAllBytes(vXml.ToString()));

                string vRuta = "";
                string nParam = "16";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vRuta = ObjParametro.Valor.ToString();
                }

                string vPass = "";
                nParam = "17";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vPass = ObjParametro.Valor.ToString();
                }


                var firmadoRequest = new FirmadoRequest
                {
                    TramaXmlSinFirma = tramaXmlSinFirma,
                    CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(vRuta.ToString())),
                    PasswordCertificado = vPass.ToString(),
                    UnSoloNodoExtension = true
                };

                var jsonFirmado = await _client.PostAsJsonAsync("api/Firmar", firmadoRequest);
                var respuestaFirmado = await jsonFirmado.Content.ReadAsAsync<FirmadoResponse>();
                if (!respuestaFirmado.Exito)
                    throw new ApplicationException(respuestaFirmado.MensajeError);

                string vRuc = "";
                nParam = "18";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vRuc = ObjParametro.Valor.ToString();
                }

                string vUserSol = "";
                nParam = "19";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vUserSol = ObjParametro.Valor.ToString();
                }

                string vPassSol = "";
                nParam = "20";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vPassSol = ObjParametro.Valor.ToString();
                }

                string vServerSunat = "";
                string vUrl = "";

                string archivo = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivo);

                vServerSunat = ciniarchivo.ReadValue("Configura", "Service", "");
                vUrl = ciniarchivo.ReadValue("Configura", "UrlService", "");

                /*            
                nParam = "22";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vUrl = ObjParametro.Valor.ToString();
                }*/

                var enviarDocumentoRequest = new EnviarDocumentoRequest
                {
                    Ruc = vRuc,
                    //UsuarioSol = "SISBICI1",
                    //ClaveSol = "123server",
                    UsuarioSol = vUserSol,
                    ClaveSol = vPassSol,
                    /*UsuarioSol = "JB99AYBC",
                    ClaveSol = "p5vnyUrcW",*/
                    //HOMOLOGACION
                    //EndPointUrl = "https://www.sunat.gob.pe/ol-ti-itcpgem-sqa/billService",
                    //PRODUCION
                    //EndPointUrl = "https://e-factura.sunat.gob.pe/ol-ti-itcpfegem/billService",
                    //DESARROLLO
                    EndPointUrl = vUrl,
                    IdDocumento = vIdComp.ToString() + "-" + vId.ToString(),
                    TipoDocumento = vTipDocSunat,
                    TramaXmlFirmado = respuestaFirmado.TramaXmlFirmado
                };


                var apiMetodo = "api/EnviarResumen";
                var jsonEnvioDocumento = await _client.PostAsJsonAsync(apiMetodo, enviarDocumentoRequest);

                RespuestaComunConArchivo respuestaEnvio;

                respuestaEnvio = await jsonEnvioDocumento.Content.ReadAsAsync<EnviarResumenResponse>();
                var rpta = (EnviarResumenResponse)respuestaEnvio;

                string vMensaje = "";
                if (!rpta.Exito)
                {
                    vMensaje = rpta.MensajeError;
                    vMensaje = vMensaje.Replace("'", "");
                    vMensaje = vMensaje.Replace("/", "");
                    //string valNroTicket = "";
                    /*if (rpta.NroTicket.ToString() == null)
                    {
                        valNroTicket = "";
                    } else
                    {
                        valNroTicket = rpta.NroTicket.ToString();
                    }*/
                    using (MySqlConnection conn = GetNewConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "UPDATE tblresumen set Ticket = @nTicket, MensajeRespuesta = @Mensaje, ArchXml = @Archivo where Id = @Id and NDocResumen = @nNDocResumen and RucEmpresa = @numruc";
                            cmd.Parameters.AddWithValue("@nTicket", rpta.NroTicket.ToString());
                            cmd.Parameters.AddWithValue("@Mensaje", vMensaje);
                            cmd.Parameters.AddWithValue("@Archivo", firmadoRequest.TramaXmlSinFirma);
                            cmd.Parameters.AddWithValue("@Id", vId.ToString());
                            cmd.Parameters.AddWithValue("@nNDocResumen", vIdComp.ToString());
                            cmd.Parameters.AddWithValue("@numruc", vEmpresa.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }

                }
                else
                {
                    vMensaje = Resources.procesoCorrecto;

                    using (MySqlConnection conn = GetNewConnection())
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "UPDATE tblresumen set Ticket = @nTicket, MensajeRespuesta = @Mensaje, ArchXml = @Archivo where Id = @Id and NDocResumen = @nNDocResumen and RucEmpresa = @numruc";
                            cmd.Parameters.AddWithValue("@nTicket", rpta.NroTicket.ToString());
                            cmd.Parameters.AddWithValue("@Mensaje", vMensaje);
                            cmd.Parameters.AddWithValue("@Archivo", firmadoRequest.TramaXmlSinFirma);
                            cmd.Parameters.AddWithValue("@Id", vId.ToString());
                            cmd.Parameters.AddWithValue("@nNDocResumen", vIdComp.ToString());
                            cmd.Parameters.AddWithValue("@numruc", vEmpresa.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                vresultado = $@"{Resources.procesoCorrecto}{Environment.NewLine}{rpta.NroTicket}";

                Respuesta = $@"{vresultado.ToString()}{Environment.NewLine}{rpta.MensajeError}";

                IdEnvio = vIdComp.ToString() + "-" + vId.ToString();
                objEnvio.ResultEnvio(IdEnvio, Respuesta);

                if (!respuestaEnvio.Exito)
                    throw new ApplicationException(respuestaEnvio.MensajeError);

                IdEnvio = vIdComp.ToString() + "-" + vId.ToString();
                objEnvio.ResultEnvio(IdEnvio, Respuesta);


            }
            catch (Exception ex)
            {
                //txtResult.Text = ex.Message;
                vresultado = ex.Message;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        static MySqlConnection GetNewConnection()
        {
            //const string MySqlConnecionString = "Server=" + FrmLogin.XServidor + "; Database= " + FrmLogin.XDB + "; Username=" + FrmLogin.XUser + "; Password=" + FrmLogin.XPassword + ";";

            var conn = new MySqlConnection("Server=" + FrmLogin.XServidor + "; Database= " + FrmLogin.XDB + "; Username=" + FrmLogin.XUser + "; Password=" + FrmLogin.XPassword + ";SslMode=" + FrmLogin.x_SslMode + ";");
            conn.Open();
            return conn;
        }

    }
}
