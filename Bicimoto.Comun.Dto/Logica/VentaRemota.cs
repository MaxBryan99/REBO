using Bicimoto.Comun.Dto.Data;
using Bicimoto.Comun.Dto.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;

namespace Bicimoto.Comun.Dto.Logica
{
    public class VentaRemota : IVenta
    {
        public static string x_codigo_usuario = "";
        public static string x_nombre_usuario = "";
        public static string x_login_usuario = "";
        private string x_clave = "";
        public static string x_NomEmpresa = "";
        public static string x_RucEmpresa = "";
        public static string x_NomAlmacen = "";
        public static string x_CodAlmacen = "";
        public static string XServidor = "";
        public static string XDB = "";
        public static string XUser = "";
        public static string XPassword = "";
        public static string x_serie = "";
        public static string x_SslMode = "";
        public string error = "";

        public void EnviarCorreo(Venta _venta, List<string> ArchivoPedido_ = null)
        {
            //empieza coneccion
            string archivo = System.Environment.CurrentDirectory + @"\base.ini";

            //MessageBox.Show("Llega");
            try
            {
                //Inicializar servidor
                cini ciniarchivo = new cini(archivo);
                XServidor = ciniarchivo.ReadValue("BaseDatos", "Servidor", "localhost");
                XDB = ciniarchivo.ReadValue("BaseDatos", "Base", "bdbicimoto");
                XUser = ciniarchivo.ReadValue("BaseDatos", "Usuario", "root");
                XPassword = ciniarchivo.ReadValue("BaseDatos", "Password", "123server");
                x_SslMode = ciniarchivo.ReadValue("BaseDatos", "SslMode", "none");

                //this.x_dias=System.Convert.ToInt32(ciniarchivo.ReadValue("BaseDatos", "DiasLimiteRecibirDoc", "0"));
                //this.x_empresa=ciniarchivo.ReadValue("BaseDatos", "Empresa", "");
                //this.x_cambiarfecha=ciniarchivo.ReadValue("BaseDatos", "CambiarFechaTrabajo", "");
            }
            catch (System.Exception ex)
            {
            }

            //XPassword = cutil.encriptaclave(XPassword);

            csql.cadena_coneccion = "datasource=" + XServidor + ";username=" + XUser + ";password=" + XPassword + ";database=" + XDB + ";SslMode=" + x_SslMode;
            if (csql.conectar() == 0)
            {
                string Mensaje1 = "<p><strong>Estimado Cliente,&nbsp;<br />Sr(es). {0}<br />{1}&nbsp;{2}</strong>&nbsp;</p>";
                string Mensaje2 = "<p>Informamos a usted que el documento {3} ha sido emitido y se encuentra disponible.</p><table style='height: 103px;' width='688'><tbody><tr><td style='width: 683px;'><table style='border-collapse: collapse;' border='0' cellspacing='0' cellpadding='5'><tbody><tr><td style='width: 23px;'>&nbsp;</td><td style='width: 134px;'>Tipo</td><td style='width: 10px;'>:</td><td style='width: 480px;'>FACTURA ELECTRONICA</td></tr><tr><td style='width: 23px;'>&nbsp;</td><td style='width: 134px;'>N&uacute;mero</td><td style='width: 10px;'>:</td><td style='width: 480px;'>{4}</td></tr><tr><td style='width: 23px;'>&nbsp;</td><td style='width: 134px;'>Monto</td><td style='width: 10px;'>:</td><td style='width: 480px;'>S/ &nbsp;{5}</td></tr><tr><td style='width: 23px;'>&nbsp;</td><td style='width: 134px;'>Fecha Emisi&oacute;n</td><td style='width: 10px;'>:</td><td style='width: 480px;'>{6}</td></tr></tbody></table></td></tr></tbody></table><p>Saludos,<br /> <br /> <strong>BICIMOTO IMPORT E.I.R.L.</strong>&nbsp;</p>";
                string Mensaje = "";

                List<string> Archivo = new List<string>(); //lista de archivos a enviar

                string Nombre = "";
                string TipDoc = "";
                string NumDoc = "";
                string NumComp = "";
                string Monto = "";
                string Fecha = "";
                string To = "";
                string Subject = "";
                string From = "";
                string RutaArchivoPDF;
                string RutaArchivoXML;
                List<string> nArchivos = new List<string>();

                Parametro parametro = new Parametro();

                System.Net.Mail.MailMessage Email;

                //NumComp = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

                NumComp = "";

                Monto = _venta.Total.ToString("###,#0.00");

                Nombre = _venta.ObjCliente.Nombre;
                TipDoc = _venta.ObjCliente.TipDoc;
                NumDoc = _venta.ObjCliente.RucDni;
                Fecha = _venta.Fecha.ToString().Trim();
                Mensaje = Mensaje1 + Mensaje2;
                string resMensaje = string.Format(Mensaje, Nombre, TipDoc, NumDoc, NumComp, NumComp, Monto, Fecha);

                To = _venta.ObjCliente.Email;
                Subject = "Factura Electrónica";

                string vRuta = "";
                string nParam = "23";
                if (parametro.BuscarParametro(nParam))
                {
                    vRuta = parametro.Valor.ToString();
                }

                From = vRuta;
                try
                {
                    //creamos un objeto tipo MailMessage
                    //este objeto recibe el sujeto o persona que envia el mail,
                    //la direccion de procedencia, el asunto y el mensaje
                    Email = new System.Net.Mail.MailMessage(From, To, Subject, resMensaje);

                    //si viene archivo a adjuntar
                    //realizamos un recorrido por todos los adjuntos enviados en la lista
                    //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt
                    if (nArchivos != null)
                    {
                        //agregado de archivo
                        foreach (string arch in nArchivos)
                        {
                            //comprobamos si existe el archivo y lo agregamos a los adjuntos
                            if (System.IO.File.Exists(@arch))
                            {
                                Email.Attachments.Add(new Attachment(@arch));
                            }
                            else
                            {
                                //return false;
                            }
                        }
                    }
                    Email.Body = resMensaje; //Cuerpo del correo
                    Email.BodyEncoding = System.Text.Encoding.UTF8;
                    Email.IsBodyHtml = true; //definimos si el contenido sera html
                    Email.From = new MailAddress(From, "Bicimoto Import"); //definimos la direccion de procedencia
                                                                           //aqui creamos un objeto tipo SmtpClient el cual recibe el servidor que utilizaremos como smtp
                                                                           //en este caso me colgare de gmail
                    SmtpClient smtpMail = new SmtpClient("smtp.gmail.com", 587); //gmail
                                                                                 //SmtpClient smtpMail = new SmtpClient("smtp.live.com", 587); //hotmail
                    smtpMail.EnableSsl = true;//le definimos si es conexión ssl
                    smtpMail.UseDefaultCredentials = false; //le decimos que no utilice la credencial por defecto
                                                            //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("osw_zamorita20@hotmail.com", "123madai");
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("ozamorac@gmail.com", "123Mada@");
                    smtpMail.Credentials = credentials;
                    smtpMail.EnableSsl = true;
                    //enviamos el mail
                    smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpMail.Send(Email);
                    //eliminamos el objeto
                    /*
                     SmtpClient SmtpServer = new SmtpClient("smtp.live.com");
                     var mail = new MailMessage();
                     mail.From = new MailAddress(From, "Bicimoto Import");
                     mail.To.Add(To);
                     mail.Subject = Subject;
                     mail.IsBodyHtml = true;
                     string htmlBody;
                     htmlBody = resMensaje;
                     mail.Body = htmlBody;
                     mail.BodyEncoding = System.Text.Encoding.UTF8;
                     SmtpServer.Port = 587;
                     SmtpServer.UseDefaultCredentials = false;
                     SmtpServer.Credentials = new System.Net.NetworkCredential("osw_zamorita20@hotmail.com", "123madai");
                     SmtpServer.EnableSsl = true;
                     SmtpServer.Send(mail);*/

                    smtpMail.Dispose();

                    //regresamos true
                    //return true;
                }
                catch (Exception ex)
                {
                    //si ocurre un error regresamos false y el error
                    error = "Ocurrio un error: " + ex.Message;
                    //return false;
                }
                //throw new NotImplementedException();
            }
        }

        public void Generar(Venta _venta)
        {
            csql.cadena_coneccion = ConfigurationManager.ConnectionStrings["BicimotoLocalDb"].ConnectionString;

            if (csql.conectar() == 0)
            {
                string dia = _venta.Fecha.Substring(0, 2);
                string mes = _venta.Fecha.Substring(3, 2);
                string anio = _venta.Fecha.Substring(6, 4);
                string hora = _venta.FecCreacion.Substring(11, 8);
                string vfecha = anio.ToString() + "/" + mes.ToString() + "/" + dia.ToString() + " " + hora;

                int resultado = csql.comando_cadena("Call SpVentaCrearRemoto('" +
                                                        _venta.IdVenta + "','" +
                                                        vfecha + "','" +
                                                        _venta.ObjCliente.RucDni + "','" +
                                                        _venta.TipDocCli + "','" +
                                                        _venta.Doc + "','" +
                                                        _venta.Serie + "','" +
                                                        _venta.Numero + "','" +
                                                        _venta.TMoneda + "','" +
                                                        _venta.NPedido + "'," +
                                                        _venta.TCambio + ",'" +
                                                        _venta.TVenta + "'," +
                                                        _venta.NDias + ",'" +
                                                        _venta.FVence + "'," +
                                                        _venta.TBruto + "," +
                                                        _venta.TExonerada + "," +
                                                        _venta.TInafecta + "," +
                                                        _venta.TGratuita + "," +
                                                        _venta.TIgv + "," +
                                                        _venta.Total + ",'" +
                                                        _venta.TEst + "','" +
                                                        _venta.Est + "','" +
                                                        _venta.Empresa + "','" +
                                                        _venta.Almacen + "','" +
                                                        _venta.Vendedor + "','" +
                                                        _venta.Usuario + "','" +
                                                        _venta.UserCreacion + "','" +
                                                        _venta.Egratuita + "','" +
                                                        _venta.TComp + "'," +
                                                        _venta.Dcto + ",'" +
                                                        vfecha.ToString() + "','" +
                                                        _venta.ArchivoXml + "','" +
                                                        _venta.NomArchXml + "')");
                if (resultado > 0)
                {
                    //res = false;
                }
                else
                {
                    //res = true;
                }

                int resultadoCli = csql.comando_cadena("Call SpClienteCrearRemoto('" +
                                                      _venta.ObjCliente.RucDni + "','" +
                                                      _venta.ObjCliente.TipDoc + "','" +
                                                      _venta.ObjCliente.Nombre + "','" +
                                                      _venta.ObjCliente.Direccion + "','" +
                                                      _venta.ObjCliente.Telefono + "','" +
                                                      _venta.ObjCliente.Celular + "','" +
                                                      _venta.ObjCliente.Contacto + "','" +
                                                      _venta.ObjCliente.Email + "','" +
                                                      _venta.ObjCliente.DireccionEnvio + "','" +
                                                      _venta.ObjCliente.Region + "','" +
                                                      _venta.ObjCliente.Provincia + "','" +
                                                      _venta.ObjCliente.Distrito + "'," +
                                                      _venta.ObjCliente.LimCredito + ",'" +
                                                      _venta.ObjCliente.CodVendedor + "','" +
                                                      _venta.ObjCliente.UserCreacion + "','" +
                                                      _venta.ObjCliente.FecCreacion + "','" +
                                                      _venta.ObjCliente.Est + "','" +
                                                      _venta.ObjCliente.RucEmpresa + "','" +
                                                      _venta.IdVenta + "')");

                if (resultadoCli > 0)
                {
                    //res = false;
                }
                else
                {
                    //res = true;
                }

                //Grabando Detalles
                string horaItem = "";
                string vfechaItem = "";
                foreach (var items in _venta.Items)
                {
                    horaItem = items.FecCreacion.Substring(11, 8);
                    vfechaItem = anio.ToString() + "/" + mes.ToString() + "/" + dia.ToString() + " " + horaItem;
                    int resultadoItem = csql.comando_cadena("Call SpDetVentaCrearRemoto('" +
                                                        _venta.IdVenta + "','" +
                                                        items.Codigo + "','" +
                                                        items.Marca + "','" +
                                                        items.Unidad + "','" +
                                                        items.Proced + "'," +
                                                        items.PVenta + "," +
                                                        items.Cantidad + "," +
                                                        items.Dcto + "," +
                                                        items.Igv + "," +
                                                        items.Importe + ",'" +
                                                        items.Almacen + "','" +
                                                        items.Empresa + "','" +
                                                        items.TipPrecio + "','" +
                                                        items.TipImpuesto + "','" +
                                                        items.UserCreacion + "'," +
                                                        items.Norden + ",'" +
                                                        vfechaItem.ToString() + "')");
                    if (resultadoItem > 0)
                    {
                        //res = false;
                    }
                    else
                    {
                        //res = true;
                    }
                }
            }
            else
            {
            }

            //throw new NotImplementedException();
        }
    }
}