using Bicimoto.Comun.Dto.Data;
using Bicimoto.Comun.Dto.Intercambio;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Net.Http;
using System.Windows.Forms;

namespace SisBicimotoApp.Clases
{
    public partial class ClsEnvioRemoto
    {
        #region Variables Privadas

        private Venta _venta;
        private HttpClient _client;

        #endregion Variables Privadas

        public ClsEnvioRemoto()
        {
        }

        public async void enviarVenta(string vIdVenta, string vRucEmpresa, string vAlmacen)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //Invocar Venta
                ClsVenta ObjVenta = new ClsVenta();
                ClsDocumento ObjDocumento = new ClsDocumento();
                ClsCliente ObjCliente = new ClsCliente();
                string codDoc = "";
                string vMod = "VEN";

                if (!ObjVenta.BuscarVentaData(vIdVenta, vRucEmpresa, vAlmacen))
                {
                    MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (!ObjCliente.BuscarCLienteData(ObjVenta.Cliente, vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos del Receptor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                
                _venta = new Venta();
                _venta.IdVenta = vIdVenta;
                _venta.Fecha = ObjVenta.Fecha;
                //_venta.Cliente = ObjVenta.Cliente;
                _venta.TipDocCli = ObjVenta.TipDocCli;
                //CLiente
                _venta.ObjCliente.RucDni = ObjCliente.RucDni;
                _venta.ObjCliente.TipDoc = ObjCliente.TipDoc;
                _venta.ObjCliente.Nombre = ObjCliente.Nombre;
                _venta.ObjCliente.Direccion = ObjCliente.Direccion;
                _venta.ObjCliente.Telefono = ObjCliente.Telefono;
                _venta.ObjCliente.Celular = ObjCliente.Celular;
                _venta.ObjCliente.Contacto = ObjCliente.Contacto;
                _venta.ObjCliente.Email = ObjCliente.Email;
                _venta.ObjCliente.DireccionEnvio = ObjCliente.DireccionEnvio;
                _venta.ObjCliente.Region = ObjCliente.Region;
                _venta.ObjCliente.Provincia = ObjCliente.Provincia;
                _venta.ObjCliente.Distrito = ObjCliente.Distrito;
                _venta.ObjCliente.LimCredito = ObjCliente.LimCredito;
                _venta.ObjCliente.CodVendedor = ObjCliente.CodVendedor;
                _venta.ObjCliente.Est = ObjCliente.Est;
                _venta.ObjCliente.FecCreacion = ObjCliente.FecCreacion;
                _venta.ObjCliente.UserCreacion = ObjCliente.UserCreacion;
                _venta.ObjCliente.FecModi = ObjCliente.FecModi;
                _venta.ObjCliente.UserModi = ObjCliente.UserModi;
                _venta.ObjCliente.RucEmpresa = ObjCliente.RucEmpresa;

                _venta.Doc = ObjVenta.Doc;
                _venta.Serie = ObjVenta.Serie;
                _venta.Numero = ObjVenta.Numero;
                _venta.TMoneda = ObjVenta.TMoneda;
                _venta.NPedido = ObjVenta.NPedido;
                _venta.TCambio = ObjVenta.TCambio;
                _venta.TVenta = ObjVenta.TVenta;
                _venta.NDias = ObjVenta.NDias;
                _venta.FVence = ObjVenta.FVence;
                _venta.TBruto = ObjVenta.TBruto;
                _venta.TExonerada = ObjVenta.TExonerada;
                _venta.TInafecta = ObjVenta.TInafecta;
                _venta.TGratuita = ObjVenta.TGratuita;
                _venta.TIgv = ObjVenta.TIgv;
                _venta.Total = ObjVenta.Total;
                _venta.TEst = ObjVenta.TEst;
                _venta.Est = ObjVenta.Est;
                _venta.Empresa = ObjVenta.Empresa;
                _venta.Almacen = ObjVenta.Almacen;
                _venta.Vendedor = ObjVenta.Vendedor;
                _venta.Usuario = ObjVenta.Usuario;
                _venta.NomArchXml = ObjVenta.NomArchXml;
                _venta.UserCreacion = ObjVenta.UserCreacion;
                _venta.FecCreacion = ObjVenta.FecCreacion;
                _venta.Egratuita = ObjVenta.Egratuita;
                _venta.TComp = ObjVenta.TComp;
                _venta.Dcto = ObjVenta.Dcto;
                _venta.ArchivoXml = ObjVenta.ArchivoXml;

                //Detalles
                DataSet datos = csql.dataset_cadena("Call SpDetVentaBuscarData('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        var detalle = new DetalleVenta();
                        detalle.IdVenta = fila[0].ToString();
                        detalle.Codigo = fila[1].ToString();
                        detalle.Marca = fila[2].ToString();
                        detalle.Unidad = fila[3].ToString();
                        detalle.Proced = fila[4].ToString();
                        detalle.PVenta = Convert.ToDouble(fila[5]);
                        detalle.Cantidad = Convert.ToDouble(fila[6]);
                        detalle.Dcto = Convert.ToDouble(fila[7]);
                        detalle.Igv = Convert.ToDouble(fila[8]);
                        detalle.Importe = Convert.ToDouble(fila[9]);
                        detalle.Almacen = fila[10].ToString();
                        detalle.Empresa = fila[11].ToString();
                        detalle.TipPrecio = fila[12].ToString();
                        detalle.TipImpuesto = fila[13].ToString();
                        detalle.FecCreacion = fila[14].ToString();
                        detalle.UserCreacion = fila[15].ToString();
                        detalle.FecModi = fila[16].ToString();
                        detalle.UserModi = fila[17].ToString();
                        detalle.Norden = Convert.ToInt32(fila[18]);
                        _venta.Items.Add(detalle);
                    }
                }

                string archivoini = System.Environment.CurrentDirectory + @"\base.ini";

                cini ciniarchivo = new cini(archivoini);

                string vUrlApiBd = "";

                vUrlApiBd = ciniarchivo.ReadValue("BDRemoto", "UrlBD", "");

                var proxy = new HttpClient { BaseAddress = new Uri(vUrlApiBd) };

                string metodoApi = "";
                metodoApi = "api/EnviarVenta";

                var response = await proxy.PostAsJsonAsync(metodoApi, _venta);

                var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();

                if (!respuesta.Exito)
                    throw new ApplicationException(respuesta.MensajeError);
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

        public async void enviarCorreo(string vIdVenta, string vRucEmpresa, string vAlmacen)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                //Invocar Venta
                ClsVenta ObjVenta = new ClsVenta();
                ClsDocumento ObjDocumento = new ClsDocumento();
                ClsCliente ObjCliente = new ClsCliente();
                string codDoc = "";
                string vMod = "VEN";

                if (!ObjVenta.BuscarVentaData(vIdVenta, vRucEmpresa, vAlmacen))
                {
                    MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                if (!ObjCliente.BuscarCLienteData(ObjVenta.Cliente, vRucEmpresa))
                {
                    MessageBox.Show("Error no se encontró datos del Receptor, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                _venta = new Venta();
                _venta.IdVenta = vIdVenta;
                _venta.Fecha = ObjVenta.Fecha;
                //_venta.Cliente = ObjVenta.Cliente;
                _venta.TipDocCli = ObjVenta.TipDocCli;
                //CLiente
                _venta.ObjCliente.RucDni = ObjCliente.RucDni;
                _venta.ObjCliente.TipDoc = ObjCliente.TipDoc;
                _venta.ObjCliente.Nombre = ObjCliente.Nombre;
                _venta.ObjCliente.Direccion = ObjCliente.Direccion;
                _venta.ObjCliente.Telefono = ObjCliente.Telefono;
                _venta.ObjCliente.Celular = ObjCliente.Celular;
                _venta.ObjCliente.Contacto = ObjCliente.Contacto;
                _venta.ObjCliente.Email = ObjCliente.Email;
                _venta.ObjCliente.DireccionEnvio = ObjCliente.DireccionEnvio;
                _venta.ObjCliente.Region = ObjCliente.Region;
                _venta.ObjCliente.Provincia = ObjCliente.Provincia;
                _venta.ObjCliente.Distrito = ObjCliente.Distrito;
                _venta.ObjCliente.LimCredito = ObjCliente.LimCredito;
                _venta.ObjCliente.CodVendedor = ObjCliente.CodVendedor;
                _venta.ObjCliente.Est = ObjCliente.Est;
                _venta.ObjCliente.FecCreacion = ObjCliente.FecCreacion;
                _venta.ObjCliente.UserCreacion = ObjCliente.UserCreacion;
                _venta.ObjCliente.FecModi = ObjCliente.FecModi;
                _venta.ObjCliente.UserModi = ObjCliente.UserModi;
                _venta.ObjCliente.RucEmpresa = ObjCliente.RucEmpresa;

                _venta.Doc = ObjVenta.Doc;
                _venta.Serie = ObjVenta.Serie;
                _venta.Numero = ObjVenta.Numero;
                _venta.TMoneda = ObjVenta.TMoneda;
                _venta.NPedido = ObjVenta.NPedido;
                _venta.TCambio = ObjVenta.TCambio;
                _venta.TVenta = ObjVenta.TVenta;
                _venta.NDias = ObjVenta.NDias;
                _venta.FVence = ObjVenta.FVence;
                _venta.TBruto = ObjVenta.TBruto;
                _venta.TExonerada = ObjVenta.TExonerada;
                _venta.TInafecta = ObjVenta.TInafecta;
                _venta.TGratuita = ObjVenta.TGratuita;
                _venta.TIgv = ObjVenta.TIgv;
                _venta.Total = ObjVenta.Total;
                _venta.TEst = ObjVenta.TEst;
                _venta.Est = ObjVenta.Est;
                _venta.Empresa = ObjVenta.Empresa;
                _venta.Almacen = ObjVenta.Almacen;
                _venta.Vendedor = ObjVenta.Vendedor;
                _venta.Usuario = ObjVenta.Usuario;
                _venta.NomArchXml = ObjVenta.NomArchXml;
                _venta.UserCreacion = ObjVenta.UserCreacion;
                _venta.FecCreacion = ObjVenta.FecCreacion;
                _venta.Egratuita = ObjVenta.Egratuita;
                _venta.TComp = ObjVenta.TComp;
                _venta.Dcto = ObjVenta.Dcto;
                _venta.ArchivoXml = ObjVenta.ArchivoXml;

                //Detalles
                DataSet datos = csql.dataset_cadena("Call SpDetVentaBuscarData('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        var detalle = new DetalleVenta();
                        detalle.IdVenta = fila[0].ToString();
                        detalle.Codigo = fila[1].ToString();
                        detalle.Marca = fila[2].ToString();
                        detalle.Unidad = fila[3].ToString();
                        detalle.Proced = fila[4].ToString();
                        detalle.PVenta = Convert.ToDouble(fila[5]);
                        detalle.Cantidad = Convert.ToDouble(fila[6]);
                        detalle.Dcto = Convert.ToDouble(fila[7]);
                        detalle.Igv = Convert.ToDouble(fila[8]);
                        detalle.Importe = Convert.ToDouble(fila[9]);
                        detalle.Almacen = fila[10].ToString();
                        detalle.Empresa = fila[11].ToString();
                        detalle.TipPrecio = fila[12].ToString();
                        detalle.TipImpuesto = fila[13].ToString();
                        detalle.FecCreacion = fila[14].ToString();
                        detalle.UserCreacion = fila[15].ToString();
                        detalle.FecModi = fila[16].ToString();
                        detalle.UserModi = fila[17].ToString();
                        detalle.Norden = Convert.ToInt32(fila[18]);
                        _venta.Items.Add(detalle);
                    }
                }

                string archivoini = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivoini = new cini(archivoini);
                string vUrlApi = "";
                vUrlApi = ciniarchivoini.ReadValue("Configura", "UrlApi", "");

                var proxy = new HttpClient { BaseAddress = new Uri(vUrlApi) };

                string metodoApi = "";
                metodoApi = "api/EnviarCorreo";

                var response = await proxy.PostAsJsonAsync(metodoApi, _venta);

                var respuesta = await response.Content.ReadAsAsync<DocumentoResponse>();

                if (!respuesta.Exito)
                    throw new ApplicationException(respuesta.MensajeError);
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
    }
}