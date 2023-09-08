using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;

namespace SisBicimotoApp.Clases
{
    public class ClsEnviarCorreo
    {
        private string From = ""; //de quien procede, puede ser un alias
        private string To;  //a quien vamos a enviar el mail
        private string Message;  //mensaje
        private string Subject; //asunto
        private string Idventa;
        private string Ruc;
        private string Almacen;

        private string Mensaje1 = "<p><strong>Estimado Cliente,&nbsp;<br />Sr(es). {0}<br />{1}&nbsp;{2}</strong>&nbsp;</p>";
        private string Mensaje2 = "<p>Informamos a usted que el documento {3} ha sido emitido y se encuentra disponible.</p><table style='height: 103px;' width='688'><tbody><tr><td style='width: 683px;'><table style='border-collapse: collapse;' border='0' cellspacing='0' cellpadding='5'><tbody><tr><td style='width: 23px;'>&nbsp;</td><td style='width: 134px;'>Tipo</td><td style='width: 10px;'>:</td><td style='width: 480px;'>FACTURA ELECTRONICA</td></tr><tr><td style='width: 23px;'>&nbsp;</td><td style='width: 134px;'>N&uacute;mero</td><td style='width: 10px;'>:</td><td style='width: 480px;'>{4}</td></tr><tr><td style='width: 23px;'>&nbsp;</td><td style='width: 134px;'>Monto</td><td style='width: 10px;'>:</td><td style='width: 480px;'>S/ &nbsp;{5}</td></tr><tr><td style='width: 23px;'>&nbsp;</td><td style='width: 134px;'>Fecha Emisi&oacute;n</td><td style='width: 10px;'>:</td><td style='width: 480px;'>{6}</td></tr></tbody></table></td></tr></tbody></table><p>Saludos,<br /> <br /> <strong>BICIMOTO IMPORT E.I.R.L.</strong>&nbsp;</p>";
        private string Mensaje = "";

        private List<string> Archivo = new List<string>(); //lista de archivos a enviar

        private System.Net.Mail.MailMessage Email;

        public string error = "";

        private ClsVenta ObjVenta = new ClsVenta();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsImprimir ObjImprimir = new ClsImprimir();
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="FROM">Procedencia</param>
        /// <param name="Para">Mail al cual se enviara</param>
        /// <param name="Mensaje">Mensaje del mail</param>
        /// <param name="Asunto">Asunto del mail</param>
        /// <param name="ArchivoPedido_">Archivo a adjuntar, no es obligatorio</param>
        public ClsEnviarCorreo(string FROM, string Para, string Mensaje, string Asunto, string nIdventa, string nRuc, string nAlmacen, List<string> ArchivoPedido_ = null)
        {
            From = FROM;
            To = Para;
            Message = Mensaje;
            Subject = Asunto;
            Idventa = nIdventa;
            Ruc = nRuc;
            Almacen = nAlmacen;
            Archivo = ArchivoPedido_;
        }

        /// <summary>
        /// metodo que envia el mail
        /// </summary>
        /// <returns></returns>
        public bool enviaMail()
        {
            string Nombre = "";
            string TipDoc = "";
            string NumDoc = "";
            string NumComp = "";
            string Monto = "";
            string Fecha = "";
            string RutaArchivoPDF;
            string RutaArchivoXML;
            List<string> nArchivos = new List<string>();

            if (!ObjVenta.BuscarVenta(Idventa, Ruc, Almacen))
            {
                MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                return false;
            }

            if (!ObjCliente.BuscarCLiente(ObjVenta.Cliente, Ruc))
            {
                MessageBox.Show("Error no se encontró datos del Receptor, VERIFIQUE!!!", "SISTEMA");
                return false;
            }

            string vMod = "VEN";
            ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
            if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
            {
                MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                return false;
            }

            NumComp = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

            Monto = ObjVenta.Total.ToString("###,#0.00");

            Nombre = ObjCliente.Nombre;
            TipDoc = ObjVenta.TipDocCli;
            NumDoc = ObjVenta.Cliente;
            Fecha = ObjVenta.Fecha.ToString().Trim();
            Mensaje = Mensaje1 + Mensaje2;
            string resMensaje = string.Format(Mensaje, Nombre, TipDoc, NumDoc, NumComp, NumComp, Monto, Fecha);

            Message = resMensaje;
            //una validación básica
            if (To.Trim().Equals("") || Message.Trim().Equals("") || Subject.Trim().Equals(""))
            {
                error = "El mail, el asunto y el mensaje son obligatorios";
                return false;
            }

            string fechaAnio = ObjVenta.Fecha.ToString().Substring(6, 4);
            string fechaMes = ObjVenta.Fecha.ToString().Substring(3, 2);
            string fechaDia = ObjVenta.Fecha.ToString().Substring(0, 2);
            string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

            RutaArchivoPDF = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"PDF", $"{rutafec}",
                        $"{NumComp}.pdf");

            if (!System.IO.File.Exists(RutaArchivoPDF))
            {
                ObjImprimir.ImprimirTicket(Idventa, Ruc, Almacen, true, false);
            }
            nArchivos.Add(RutaArchivoPDF);

            RutaArchivoXML = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"XML", $"{rutafec}",
                        $"{NumComp}.xml");

            if (!System.IO.File.Exists(RutaArchivoXML))
            {
                ObjGrabaXML.generaXMLFactura(Idventa, Ruc, Almacen, false);
            }
            nArchivos.Add(RutaArchivoXML);

            //Archivo.Add(RutaArchivoPDF);
            //aqui comenzamos el proceso
            //comienza-------------------------------------------------------------------------
            try
            {
                //creamos un objeto tipo MailMessage
                //este objeto recibe el sujeto o persona que envia el mail,
                //la direccion de procedencia, el asunto y el mensaje
                Email = new System.Net.Mail.MailMessage(From, To, Subject, Message);

                //si viene archivo a adjuntar
                //realizamos un recorrido por todos los adjuntos enviados en la lista
                //la lista se llena con direcciones fisicas, por ejemplo: c:/pato.txt
                if (nArchivos != null)
                {
                    //agregado de archivo
                    foreach (string archivo in nArchivos)
                    {
                        //comprobamos si existe el archivo y lo agregamos a los adjuntos
                        if (System.IO.File.Exists(@archivo))
                        {
                            Email.Attachments.Add(new Attachment(@archivo));
                        }
                        else
                        {
                            return false;
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
                return true;
            }
            catch (Exception ex)
            {
                //si ocurre un error regresamos false y el error
                error = "Ocurrio un error: " + ex.Message;
                return false;
            }

            //return false;
        }
    }
}