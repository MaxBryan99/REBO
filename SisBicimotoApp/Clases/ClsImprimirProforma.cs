using Bicimoto.API;
using Bicimoto.Comun.Dto.Intercambio;
using Bicimoto.Firmado;
using MySql.Data.MySqlClient;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace SisBicimotoApp.Clases
{
    internal class ClsImprimirProforma
    {
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsDetVenta ObjDetVenta = new ClsDetVenta();
        private ClsEmpresa ObjEmpresa = new ClsEmpresa();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsAlmacen ObjAlmacen = new ClsAlmacen();
        private ClsProducto ObjProducto = new ClsProducto();
        private ClsVenta ObjVentaFirma = new ClsVenta();
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsNotCre ObjNotCre = new ClsNotCre();

        private PrintDocument doc = new PrintDocument();
        private string Id = "";
        private string RucEmpresa = "";
        private string CodAlmacen = "";
        private string vResumenFirma = "";
        private float vTab = 0;

        public ClsImprimirProforma()
        {
        }

        private async void Doc_PrintPage_Proforma(Object sender, PrintPageEventArgs e)
        {
            if (ObjVenta.BuscarVentaProforma(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
            {
            }
            else
            {
                MessageBox.Show("Error Venta nano", "SISTEMA");
                return;
            }

            if (ObjEmpresa.BuscarRuc(RucEmpresa.ToString()))
            {
            }
            else
            {
                MessageBox.Show("Error Empresa", "SISTEMA");
                return;
            }

            //Buscar documento de Venta
            if (ObjDocumento.BuscarDocNomMod(ObjVenta.Doc.ToString(), "VEN"))
            {
            }
            else
            {
                MessageBox.Show("Error Documento", "SISTEMA");
                return;
            }

            //Id del Comprobante
            string vMod = "VEN";
            ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
            if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
            {
                MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                return;
            }
            string nCliente = "";
            if (ObjVenta.Cliente.ToString().Equals("C0000000001"))
            {
                nCliente = "CLIENTES VARIOS";
            }
            else
            {
                //Cliente
                if (!ObjCliente.BuscarCLiente(ObjVenta.Cliente.ToString(), RucEmpresa.ToString()))
                {
                    MessageBox.Show("Error no se encontró el Cliente, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
            }

            // Creamos rectangle1
            float x = 10.0F;
            float y = 245.0F;
            float width = 260.0F;
            float height = 50.0F;
            RectangleF drawRect1 = new RectangleF(x, y, width, height);

            // Creamos rectangle2
            float x1 = 10.0F;
            float y2 = 280.0F;
            float width1 = 260.0F;
            float height2 = 50.0F;
            RectangleF drawRect2 = new RectangleF(x1, y2, width1, height2);

            // Creamos rectangle3
            float x3 = 10.0F;
            float y4 = 485.0F;
            float width3 = 270.0F;
            float height4 = 70.0F;
            RectangleF drawRect3 = new RectangleF(x3, y4, width3, height4);

            //----------------------
            StringFormat format2 = new StringFormat();//declara un nuevo formato de texto
            format2.Alignment = StringAlignment.Center;//elegimos la posicion del texto ,horizontalmente,en este caso centrado

            StringFormat format1 = new StringFormat(StringFormatFlags.DisplayFormatControl);//Alinea de Izquierda a derecha
            StringFormat format3 = new StringFormat(StringFormatFlags.DirectionRightToLeft);//Alinea de derecha a izquierda
                                                                                            //StringFormat format4 = new StringFormat(StringFormatFlags.);//

            //Declaramos un tipo de letra
            Font letra = new Font("Calibri", 9F);
            Font letra1 = new Font("Segoe UI Semibold", 7);
            //Font letra1 = new Font("Gill Sans MT", 8);
            Font letra2 = new Font("Lucida Console", 10);
            Font letra4 = new Font("Ebrima", 9);
            // Font letra4 = new Font("Gill Sans MT", 9);
            Font letra5 = new Font("Ebrima", 9);
            Font letra6 = new Font("Arial", 6);//Segoe UI Semibold
            Font letra7 = new Font("Yu Gothic UI Semibold", 9);
            Font letra8 = new Font("Yu Gothic UI Semibold", 10);
            Font letra9 = new Font("Ebrima", 8);
            Font letra10 = new Font("Ebrima", 11);
            //Font letra8 = new Font("Lucida Sans Unicode", 10);

            int i = -2;//declaramos entero i,para autoaumento de las coordenadas

           // e.Graphics.DrawString(ObjEmpresa.Razon.ToString().Trim(), letra10, Brushes.Black, new Point(140, i), format2);
            //e.Graphics.DrawString(ObjEmpresa.Direccion.ToString().Trim(), letra4, Brushes.Black, new Point(30, i += 15), format1);
           // e.Graphics.DrawString(ObjEmpresa.Direccion.ToString().Trim(), letra1, Brushes.Black, new Point(-2, i +=16), format1);
            //e.Graphics.DrawString(ObjEmpresa.Ubicacion.ToString().Trim(), letra1, Brushes.Black, new Point(114, i), format1);
          //  string nomTelef = "Telf.:" + ObjEmpresa.Telefono.ToString().Trim();
            /*int tlenum = 0;
            if (nomTelef.Length > 19)
            {
                tlenum = nomTelef.Length - 19;
                e.Graphics.DrawString(nomTelef.Substring(0, 19), letra4, Brushes.Black, new Point(137, i += 14), format1);
                e.Graphics.DrawString(nomTelef.Substring(19, tlenum), letra4, Brushes.Black, new Point(3, i += 9), format1);
            }
            else
            {
                e.Graphics.DrawString(nomTelef, letra4, Brushes.Black, new Point(0, i += 14), format1);
            }*/
            //e.Graphics.DrawString(nomTelef, letra1, Brushes.Black, new Point(106, i), format1); //new Point(-2, i += 10), format1);
           // string nomTelefSisley = "Jr. 28 de julio Mz. 11 lote 1B " + " Telf.:" + ObjEmpresa.Telefono2.ToString().Trim();
           // e.Graphics.DrawString(nomTelefSisley, letra1, Brushes.Black, new Point(5, i += 10), format1);// new Point(145, i /*+= 12*/), format1);
           //e.Graphics.DrawString(ObjEmpresa.Ubicacion.ToString().Trim(), letra1, Brushes.Black, new Point(140, i += 10), format2);
           // string nomRuc = "RUC: " + ObjEmpresa.Ruc.ToString().Trim();
            //e.Graphics.DrawString(nomRuc, letra7, Brushes.Black, new Point(140, i += 10), format2);

            string vDocumento = ObjDocumento.Nombre.ToString().Trim();
            string vCli = ObjVenta.Cliente.Substring(0, 1);
            if (vCli.Equals("C"))
            {
                e.Graphics.DrawString(vDocumento, letra8, Brushes.Black, new Point(142, i += 13), format2);
            }
            else
            {
                if (ObjCliente.RucDni.ToString().Length == 11 && ObjCliente.TipDoc.ToString().Equals("RUC"))
                {
                    e.Graphics.DrawString(vDocumento, letra8, Brushes.Black, new Point(142, i += 15), format2);
                }
                else
                {
                    e.Graphics.DrawString(vDocumento, letra8, Brushes.Black, new Point(142, i += 15), format2);
                }
            }

            //Numero de documento
            string vIdComprobante = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
            e.Graphics.DrawString(vIdComprobante, letra, Brushes.Black, new Point(145, i += 15), format2);

            // string fecEmision = "FECHA DE EMISIÓN\t: " + ObjVenta.Fecha.ToString().Trim() + "  -  " + ObjVenta.FecCreacion.ToString().Substring(11, 13);
            string fecEmision = "FECHA DE EMISIÓN\t: " + ObjVenta.FecCreacion.ToString().Trim();

            //Probando tabulacion
            // Prepare a StringFormat to use the tabs.
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 3 };
                    string_format.SetTabStops(0, tabs);
                    //e.Graphics.DrawString(fecEmision, letra4, Brushes.Black, new Point(-2, i += 20), string_format);

                    e.Graphics.DrawString(fecEmision, letra9, Brushes.Black, -2, i += 15, string_format);
                }
            }
            //cruzamos los dedos a q salga esta huevaada
            // e.Graphics.DrawString(fecEmision, letra4, Brushes.Black, new Point(-2, i += 20), format1);

            /* string horaEmision = "HORA DE EMISION\t : " + ObjVenta.FecCreacion.ToString().Substring(11, 13);

              using (StringFormat string_format = new StringFormat())
              {
                  using (Font font = new Font("Arial", 7))
                  {
                      // Define the tab stops.
                      //float[] tabs = { 450, 75, 75 };
                      float[] tabs = {9};
                      string_format.SetTabStops(0, tabs);
                      e.Graphics.DrawString(horaEmision, letra4, Brushes.Black, new Point(-2, i += 12), string_format);
                  }
              }*/

            //Sera el codigo del almacén
            string vParam = "2";
            string vCodCat = "001";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda.ToString(), vParam.ToString());

            string localAlm = "LOCAL\t: " + CodAlmacen.ToString();//+ "  " + " CAJA: " + "001" + "  " + "MONEDA: " + ObjDetCatalogo.Descripcion.ToString() + "  " + " V:  " + ObjVenta.Vendedor.ToString();

            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 102 };
                    string_format.SetTabStops(0, tabs);
                    e.Graphics.DrawString(localAlm, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }

            // e.Graphics.DrawString(localAlm, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            //Por el momento será caja unica, para todos los locales
            string caja = "VENDEDOR\t: " + ObjVenta.Vendedor.ToString(); //Valor en duro
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 102 };
                    string_format.SetTabStops(0, tabs);
                    // e.Graphics.DrawString(caja, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            // e.Graphics.DrawString(caja, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            //sE OBTIENE TIPO DE MONEDA DE CATALOGO
            // string vParam = "2";
            // string vCodCat = "001";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda.ToString(), vParam.ToString());

            string tipMoneda = "TIPO DE MONEDA\t: " + ObjDetCatalogo.Descripcion.ToString();
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 102 };
                    string_format.SetTabStops(0, tabs);
                    //  e.Graphics.DrawString(tipMoneda, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            //e.Graphics.DrawString(tipMoneda, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            string vVendedor = "SERIE IMPRESORA\t  : "/* + ObjVenta.Vendedor.ToString() + "*/  + ObjSerie.NumSerieImp.ToString();
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 6 };
                    string_format.SetTabStops(0, tabs);
                    //  e.Graphics.DrawString(vVendedor, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            // e.Graphics.DrawString(vVendedor, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            //Por el momento será cajero unico para todos los locales
            string vCajero = "CAJERO\t  : " + "001"; // valor en duro
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 96 };
                    string_format.SetTabStops(0, tabs);
                    // e.Graphics.DrawString(vCajero, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            // e.Graphics.DrawString(vCajero, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            //Aca debe ir la numero de serie de la impresora
            /* string numSerie = "NÚMERO DE SERIE\t : " + ObjSerie.NumSerieImp.ToString(); // configurar la tabla TblSerie (impresora)
             using (StringFormat string_format = new StringFormat())
             {
                 using (Font font = new Font("Arial", 7))
                 {
                     // Define the tab stops.
                     //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 10 };
                     string_format.SetTabStops(0, tabs);
                     e.Graphics.DrawString(numSerie, letra4, Brushes.Black, new Point(-2, i += 12), string_format);
                 }
             }*/

            Point pointSharilyn1 = new Point(0, (i += 17));
            Point pointSharilyn2 = new Point(1050, i);

            e.Graphics.DrawLine(Pens.Black, pointSharilyn1, pointSharilyn2);
            string vCliente = "";
            //e.Graphics.DrawString(numSerie, letra4, Brushes.Black, new Point(-2, i += 12), format1);
            if (nCliente == "CLIENTES VARIOS" || vCli.Equals("C"))
            {
                string vDoc = "IDENTIFICACIÓN      :";
                e.Graphics.DrawString(vDoc, letra9, Brushes.Black, new Point(-2, i += 2), format1);
                /* using (StringFormat string_format = new StringFormat())
                 {
                     using (Font font = new Font("Arial", 7))
                     {
                         // Define the tab stops.
                         //float[] tabs = { 450, 75, 75 };
                         float[] tabs = {21};

                         string_format.SetTabStops(0, tabs);
                         e.Graphics.DrawString(vDoc, letra9, Brushes.Black, new Point(-2, i += 12), string_format);

                         vTab = tabs.Sum();
                     }
                 }*/
            }
            else
            {
                string vDoc = "IDENTIFICACIÓN\t: " + ObjVenta.TipDocCli.ToString() + " - " + ObjVenta.Cliente.ToString();
                using (StringFormat string_format = new StringFormat())
                {
                    using (Font font = new Font("Arial", 7))
                    {
                        // Define the tab stops.
                        //float[] tabs = { 450, 75, 75 };
                        float[] tabs = { 102 };
                        string_format.SetTabStops(0, tabs);
                        e.Graphics.DrawString(vDoc, letra9, Brushes.Black, new Point(-2, i += 2), string_format);

                        vTab = tabs.Sum();
                    }
                }
            }
            //e.Graphics.DrawString(vDoc, letra4, Brushes.Black, new Point(-2, i += 12), format1);
            int nTamCli = 0;
            if (nCliente == "CLIENTES VARIOS")
            {
                vCliente = "RAZÓN/NOMBRE\t    : " + nCliente;
                using (StringFormat string_format = new StringFormat())
                {
                    e.Graphics.DrawString(vCliente, letra9, Brushes.Black, new Point(-2, i += 12), format1);
                    /* using (Font font = new Font("Arial", 7))
                     {
                         // Define the tab stops.
                         //float[] tabs = { 450, 75, 75 };
                         float[] tabs = {20};
                         string_format.SetTabStops(0, tabs);
                         e.Graphics.DrawString(vCliente, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                     }*/

                    // e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
                }
                //vCliente = "CLIENTE : " + nCliente;

                // e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
            }
            else
            {
                vCliente = "RAZÓN/NOMBRE\t    : " + ObjCliente.Nombre.ToString();
                // e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
                using (StringFormat string_format = new StringFormat())
                {
                    /*using (Font font = new Font("Arial", 7))
                    {
                        if (vCliente.Length > 40)
                        {
                            nTamCli = vCliente.Length - 40;
                            e.Graphics.DrawString(vCliente.Substring(0, 40), letra4, Brushes.Black, new Point(-2, i += 12), format1);

                            e.Graphics.DrawString(vCliente.Substring(40, nTamCli), letra4, Brushes.Black, new Point(-2, i += 10), format1);
                        }
                        else
                        {
                            e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
                            vTab = tabs.Sum();
                        }
                        */

                    // e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
                }
                if (vCliente.Length > 46)
                {
                    nTamCli = vCliente.Length - 46;
                    e.Graphics.DrawString(vCliente.Substring(0, 46), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                    e.Graphics.DrawString(vCliente.Substring(46, nTamCli), letra9, Brushes.Black, new Point(105, i += 10), format1);
                }
                else
                {
                    e.Graphics.DrawString(vCliente, letra9, Brushes.Black, new Point(-2, i += 12), format1);
                }
            }
            // string vCliente = "SEÑOR(A) : " + ObjCliente.Nombre.ToString();
            //e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
            string vDireccion = "";
            int nTama = 0;

            if (nCliente == "CLIENTES VARIOS")
            {
                vDireccion = "DIRECCIÓN\t: ";
                //e.Graphics.DrawString(vDireccion, letra4, Brushes.Black, new Point(-2, i += 12), format1);

                using (StringFormat string_format = new StringFormat())
                {
                    using (Font font = new Font("Arial", 7))
                    {
                        // Define the tab stops.
                        //float[] tabs = { 450, 75, 75 };
                        float[] tabs = { 51 };
                        string_format.SetTabStops(0, tabs);
                        e.Graphics.DrawString(vDireccion, letra9, Brushes.Black, new Point(-2, i += 12), string_format);

                        vTab = tabs.Sum();
                    }
                }
            }
            else
            {
                //e.Graphics.DrawString(vDireccion, letra4, Brushes.Black, new Point(-2, i += 12), format1);

                vDireccion = "DIRECCIÓN               : " + ObjCliente.Direccion.ToString();
                if (vDireccion.Length > 53)
                {
                    if (vDireccion.Length <= 90)
                    {
                        nTama = vDireccion.Length - 53;
                        e.Graphics.DrawString(vDireccion.Substring(0, 53), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                        e.Graphics.DrawString(vDireccion.Substring(53, nTama), letra9, Brushes.Black, new Point(105, i += 10), format1);
                    }
                    else if (vDireccion.Length < 100)
                    {
                        int nTama1 = vDireccion.Length - 83;
                        e.Graphics.DrawString(vDireccion.Substring(0, 55), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                        e.Graphics.DrawString(vDireccion.Substring(55, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(83, nTama1), letra9, Brushes.Black, new Point(105, i += 10), format1);
                    }
                    else if (vDireccion.Length < 130)
                    {
                        int nTama1 = vDireccion.Length - 111;
                        e.Graphics.DrawString(vDireccion.Substring(0, 55), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                        e.Graphics.DrawString(vDireccion.Substring(55, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(83, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(111, nTama1), letra9, Brushes.Black, new Point(105, i += 10), format1);
                    }
                    else
                    {
                        int nTama1 = vDireccion.Length - 139;
                        e.Graphics.DrawString(vDireccion.Substring(0, 55), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                        e.Graphics.DrawString(vDireccion.Substring(55, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(83, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(111, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(139, nTama1), letra9, Brushes.Black, new Point(105, i += 10), format1);
                    }
                    //nTama = vDireccion.Length - 46;

                    /*e.Graphics.DrawString(vDireccion.Substring(0, 43), letra4, Brushes.Black, new Point(-2, i += 12), format1);
                    e.Graphics.DrawString(vDireccion.Substring(43,40), letra4, Brushes.Black, new Point(-2, i += 10), format1);
                    e.Graphics.DrawString(vDireccion.Substring(83, nTama), letra4, Brushes.Black, new Point(-2, i += 10), format1);*/

                    /* e.Graphics.DrawString(vDireccion.Substring(0, 46), letra4, Brushes.Black, new Point(-2, i += 12), format1);
                     e.Graphics.DrawString(vDireccion.Substring(46, nTama), letra4, Brushes.Black, new Point(-2, i += 10), format1);*/
                }
                else
                {
                    e.Graphics.DrawString(vDireccion, letra9, Brushes.Black, new Point(-2, i += 12), format1);
                }
            }
            //Imprimiendo linea

            Point point1 = new Point(0, (i += 20));
            Point point2 = new Point(1050, i);

            e.Graphics.DrawLine(Pens.Black, point1, point2);

            string Codi = "Código";
            e.Graphics.DrawString(Codi, letra9, Brushes.Black, new Point(-2, i += 0), format1);

            string vDesc = "Descripción";
            e.Graphics.DrawString(vDesc, letra9, Brushes.Black, new Point(60, i), format1);

            string vCant = "Cant   ";
            e.Graphics.DrawString(vCant, letra9, Brushes.Black, new Point(158, i), format1);

            string vPrec = " Precio";
            e.Graphics.DrawString(vPrec, letra9, Brushes.Black, new Point(196, i), format1);

            string vTotal = "TOTAL";
            e.Graphics.DrawString(vTotal, letra9, Brushes.Black, new Point(248, i /*-= 5*/), format1);

            Point point3 = new Point(0, (i += 17));
            Point point4 = new Point(1000, i);

            e.Graphics.DrawLine(Pens.Black, point3, point4);

            //Se imprime articulos

            DataSet datos = csql.dataset_cadena("Call SpDetProformaBuscar('" + Id.ToString() + "','" + RucEmpresa.ToString() + "','" + CodAlmacen.ToString() + "')");
            string nArticulo = "";
            //string vIgv = "";
            string vPunit = "";
            string vImporte = "";
            double Net = 0;
            bool nCheckDet = false;

            int n = 0;
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    nArticulo = fila[1].ToString().Trim();
                    Net = double.Parse(fila[5].ToString().Equals("") ? "0" : fila[5].ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        vPunit = "";
                    else
                        vPunit = Net.ToString("#,##0.00").Trim();

                    Net = double.Parse(fila[7].ToString().Equals("") ? "0" : fila[7].ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        vImporte = "";
                    else
                        vImporte = Net.ToString(" #,##0.00").Trim();

                    //Imprimimos
                    string det = fila[12].ToString();
                    int nLenght = 40;
                    int nTamanio = det.Length;
                    int nTam = 0;
                    n += 1;
                    // string vMulti = fila[4].ToString().Trim() + " x " + vPunit.ToString();

                    if (nArticulo.Length > 20)
                    {
                        nTam = nArticulo.Length - 20;
                        e.Graphics.DrawString(fila[0].ToString().Trim(), letra9, Brushes.Black, new Point(3, i += 3), format1);
                        e.Graphics.DrawString(nArticulo.Substring(0, 20), letra9, Brushes.Black, new Point(35, i), format1);

                        e.Graphics.DrawString(fila[4].ToString().Trim(), letra9, Brushes.Black, new Point(183, i), format3);
                        e.Graphics.DrawString(vPunit, letra9, Brushes.Black, new Point(235, i), format3);
                        e.Graphics.DrawString(vImporte, letra9, Brushes.Black, new Point(285, i), format3);
                        e.Graphics.DrawString(nArticulo.Substring(20, nTam), letra9, Brushes.Black, new Point(35, i += 12), format1);
                    }
                    else
                    {
                        e.Graphics.DrawString(fila[0].ToString().Trim(), letra9, Brushes.Black, new Point(3, i += 3), format1);
                        e.Graphics.DrawString(nArticulo, letra9, Brushes.Black, new Point(35, i), format1);
                        e.Graphics.DrawString(fila[4].ToString().Trim(), letra9, Brushes.Black, new Point(183, i), format3);
                        e.Graphics.DrawString(vPunit, letra9, Brushes.Black, new Point(235, i), format3);
                        e.Graphics.DrawString(vImporte, letra9, Brushes.Black, new Point(286, i), format3);
                    }

                    /*  if (nArticulo.Length > 20)
                      {
                          e.Graphics.DrawString(fila[4].ToString().Trim(), letra5, Brushes.Black, new Point(198, i), format3);
                          e.Graphics.DrawString(vPunit, letra5, Brushes.Black, new Point(238, i), format3);
                      }
                      else
                      {
                          e.Graphics.DrawString(fila[4].ToString().Trim(), letra5, Brushes.Black, new Point(198, i), format3);
                          e.Graphics.DrawString(vPunit, letra5, Brushes.Black, new Point(239, i), format3);
                      }*/

                    if (det.Length > 40)
                    {
                        nTamanio = det.Length - 40;

                        e.Graphics.DrawString(det.Substring(0, 29), letra9, Brushes.Black, new Point(0, i += 5), format1);
                        e.Graphics.DrawString(det.Substring(29, nTamanio), letra9, Brushes.Black, new Point(0, i += 10), format1);
                    }
                    else
                    {
                        e.Graphics.DrawString(det, letra9, Brushes.Black, new Point(0, i += 10), format1);
                    }
                    /* if (det.Length > 40)
                     {
                         nLenght = 40;
                         nTam = 0;
                         do
                         {
                             e.Graphics.DrawString(det.Substring(nTam, nLenght), letra4, Brushes.Black, new Point(0, i += 5), format1);
                             nTam = nTam + 40;
                         } while ((nTam + 40) < det.Length);

                         e.Graphics.DrawString(det.Substring(nTam, (det.Length - nTam)), letra4, Brushes.Black, new Point(0, i += 5), format1);
                     }
                     else
                     {
                         e.Graphics.DrawString(det.ToString(), letra4, Brushes.Black, new Point(0, i += 10), format1);
                     }*/

                    /*  string det = fila[12].ToString();
                      int nLenght = 0;
                      int nTamanio = det.Length;*/
                    Point point5 = new Point(0, (i += 12));
                    Point point6 = new Point(1000, i);

                    e.Graphics.DrawLine(Pens.Black, point5, point6);
                }

                /*n += 1;
                string vMulti = fila[4].ToString().Trim() + " x " + vPunit.ToString();
                if (n == 0)
                {
                    e.Graphics.DrawString(vMulti, letra4, Brushes.Black, new Point(135, i /*+= 12), format1);
                }
                else
                {
                    e.Graphics.DrawString(vMulti, letra4, Brushes.Black, new Point(135, i += 10), format1);
                }
                e.Graphics.DrawString(vImporte, letra4, Brushes.Black, new Point(247, i), format3);
            }*/
            }

            //ClsConversiones.NumeroALetrasString(ObjVenta.Total.ToString());
            //TOTALES
            string opGravada = "";
            string opExonerada = "";
            string opInafecta = "";
            string vIgv = "";
            string vTTotal = "";
            //e.Graphics.DrawString("OP. EXONERADA", letra4, Brushes.Black, new Point(0, i += 3), format1);
            //e.Graphics.DrawString("0.00", letra4, Brushes.Black, new Point(245, i), format3);
            //Net = double.Parse(ObjVenta.TExonerada.ToString().Equals("") ? "0" : ObjVenta.TExonerada.ToString().Trim());
            //if (Net.ToString().Trim().Equals("0"))
            //    opExonerada = Net.ToString("#,##0.00").Trim();//"0.00";
            //else
            //    opExonerada = Net.ToString("#,##0.00").Trim();
            ////e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            ////e.Graphics.DrawString(opExonerada, letra4, Brushes.Black, new Point(284, i), format3);
            //// opExonerada = Net.ToString("#S/ #,##0.00").Trim();
            ////e.Graphics.DrawString(opExonerada, letra4, Brushes.Black, new Point(284, i), format3);

            ////e.Graphics.DrawString("OP. INAFECTA", letra4, Brushes.Black, new Point(0, i += 12), format1);
            ////e.Graphics.DrawString("0.00", letra4, Brushes.Black, new Point(245, i), format3);
            //Net = double.Parse(ObjVenta.TInafecta.ToString().Equals("") ? "0" : ObjVenta.TInafecta.ToString().Trim());
            //if (Net.ToString().Trim().Equals("0"))
            //    opInafecta = Net.ToString("#,##0.00").Trim();//"0.00";
            //else
            //    opInafecta = Net.ToString("##,##0.00").Trim();
            ////e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            ////e.Graphics.DrawString(opInafecta, letra4, Brushes.Black, new Point(284, i), format3);

            ////e.Graphics.DrawString("OP. GRAVADA", letra4, Brushes.Black, new Point(0, i += 12), format1);
            //Net = double.Parse(ObjVenta.TBruto.ToString().Equals("") ? "0" : ObjVenta.TBruto.ToString().Trim());
            //if (Net.ToString().Trim().Equals("0"))
            //    opGravada = Net.ToString("#,##0.00").Trim();//"0.00";
            //else
            //    opGravada = Net.ToString("#,##0.00").Trim();
            ////e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            ////opGravada = Net.ToString("#S/ #,##0.00").Trim();
            ////e.Graphics.DrawString(opGravada, letra4, Brushes.Black, new Point(284, i), format3);

            ////e.Graphics.DrawString("IGV", letra4, Brushes.Black, new Point(0, i += 12), format1);
            //Net = double.Parse(ObjVenta.TIgv.ToString().Equals("") ? "0" : ObjVenta.TIgv.ToString().Trim());
            //if (Net.ToString().Trim().Equals("0"))
            //    vIgv = Net.ToString("#,##0.00").Trim();//"0.00";
            //else
            //    vIgv = Net.ToString("#,##0.00").Trim();
            //e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            //vIgv = Net.ToString("#S/ #,##0.00").Trim();
            //e.Graphics.DrawString(vIgv, letra4, Brushes.Black, new Point(284, i), format3);

            e.Graphics.DrawString("IMPORTE TOTAL", letra4, Brushes.Black, new Point(0, i += 3), format1);
            Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
            if (Net.ToString().Trim().Equals("0"))
                vTTotal = Net.ToString("##,##0.00").Trim();//"0.00";
            else
                vTTotal = Net.ToString("##,##0.00").Trim();
            e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            // vTTotal = Net.ToString("#S/ ##,##0.00").Trim();
            e.Graphics.DrawString(vTTotal, letra4, Brushes.Black, new Point(284, i), format3);

            string numLetra = "SON: " + ClsConversiones.NumeroALetrasString(ObjVenta.Total.ToString());
            int lenum = 0;
            if (numLetra.Length > 44)
            {
                lenum = numLetra.Length - 44;
                e.Graphics.DrawString(numLetra.Substring(0, 44), letra9, Brushes.Black, new Point(0, i += 14), format1);
                e.Graphics.DrawString(numLetra.Substring(44, lenum), letra9, Brushes.Black, new Point(0, i += 10), format1);
            }
            else
            {
                e.Graphics.DrawString(numLetra, letra9, Brushes.Black, new Point(0, i += 14), format1);
            }

            Point point7 = new Point(0, (i += 18));
            Point point8 = new Point(1000, i);

            e.Graphics.DrawLine(Pens.Black, point7, point8);

            if (ObjDocumento.EnvSunat.Equals("S"))
            {
                //string resumen = "V. RESUMEN: " + ObjVenta.ResumenFirma;
                //*******************************************************
                string resumen = "";
                string resumenFinal = "";
                if (ClsGrabaXML.vResumenFirma.Equals(""))
                {
                    if (ObjVenta.ResumenFirma.Equals("") || ObjVenta.ResumenFirma == null)
                    {
                        if (ObjVentaFirma.BuscarVentaDatosXml(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
                        {
                            resumen = ObjVentaFirma.ResumenFirma;
                        }
                    }
                    else
                    {
                        resumen = ObjVenta.ResumenFirma;
                    }
                }
                else
                {
                    resumen = ClsGrabaXML.vResumenFirma;
                }

                if (resumen.Equals(""))
                {
                    ClsVenta ObjVenta2 = new ClsVenta();
                    if (!ObjVenta2.BuscarVenta(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
                    {
                        MessageBox.Show("Error no se encontró datos de comprobante de Venta, VERIFIQUE!!!", "SISTEMA");
                        return;
                    }

                    ClsGrabaXML ObjXml = new ClsGrabaXML();
                    //string vFirma = await ObjXml.GeneraXMLFirma(ObjVenta.ArchivoXml, Id, RucEmpresa.ToString(), CodAlmacen.ToString());

                    if (resumen.Equals(""))
                    {
                        ClsVenta ObjVentaXml2 = new ClsVenta();
                        if (ObjVentaXml2.BuscarVentaDatosXml(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
                        {
                            resumen = ObjVentaXml2.ResumenFirma;
                        }
                        else
                        {
                            if (vResumenFirma.Equals(""))
                            {
                                resumen = ClsGrabaXML.vResumenFirma;
                            }
                            else
                            {
                                resumen = vResumenFirma;
                            }
                        }
                    }
                    else
                    {
                        resumen = vResumenFirma;
                    }

                    //} else
                    //{
                    //resumenFinal = "V. RESUMEN: " + resumen.ToString();

                    //e.Graphics.DrawString(resumenFinal, letra4, Brushes.Black, new Point(0, i += 9), format1);
                }

                resumenFinal = "Código HASH : " + resumen.ToString();
                e.Graphics.DrawString(resumenFinal, letra9, Brushes.Black, new Point(-2, i += 5), format1);

                string nomdocumento = ObjEmpresa.Ruc + "-" + vIdComprobante;

                string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR", $"{nomdocumento}.jpg");

                //System.Threading.Thread.Sleep(7000);
                //MessageBox.Show(filename, "SISTEMA");

                if (System.IO.File.Exists(filename))
                {
                    Image img = Image.FromFile(filename);
                    //e.Graphics.DrawImage(img, 55, i += 18, 130, 80);
                    e.Graphics.DrawImage(img, 100, i += 18, 85, 80);
                    i += 70;
                }
                else
                {
                    vCodCat = "018";
                    ObjDetCatalogo.BuscarDetCatalogoCod(vCodCat.ToString(), ObjVenta.TipDocCli.ToString());

                    //Volvemos a generar codigo QR
                    string datosQR;
                    string serie = ObjSerie.PrefijoSerie + ObjVenta.Serie;
                    datosQR = ObjEmpresa.Ruc.ToString() + "|" + ObjDocumento.NCorto.ToString() + "|" + serie + "-" + ObjVenta.Numero + "|" + ObjVenta.TIgv.ToString() + "|" + ObjVenta.Total.ToString() + "|" +
                              DateTime.Parse(ObjVenta.Fecha).ToString("yyyy-MM-dd") + "|" + ObjVenta.TipDocCli.ToString() + "|" +
                              ObjVenta.Cliente.ToString();
                    //Generar QR
                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    int scale = 4; // Convert.ToInt16(txtSize.Text);
                    qrCodeEncoder.QRCodeScale = scale;
                    int version = 6;
                    qrCodeEncoder.QRCodeVersion = version;

                    Image image;
                    String data = datosQR.ToString();
                    image = qrCodeEncoder.Encode(data);

                    nomdocumento = ObjEmpresa.Ruc.ToString() + "-" + ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

                    string carpetaqr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR");

                    if (!(Directory.Exists(carpetaqr)))
                    {
                        Directory.CreateDirectory(carpetaqr);
                    }

                    string ruta = "";
                    ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR", $"{nomdocumento}.jpg");
                    //bm.Save(ruta, System.Drawing.Imaging.ImageFormat.Jpeg);
                    image.Save(ruta, System.Drawing.Imaging.ImageFormat.Jpeg);

                    Image img = Image.FromFile(ruta);
                    e.Graphics.DrawImage(img, 100, i += 18, 85, 80);
                    //e.Graphics.DrawImage(img, 55, i += 18, 130, 80);
                    i += 70;
                }

                //*******************************************************
            }

            //empieza impresion nano
            /* string mensajenano = "BIENES TRANSFERIDOS EN LA AMAZONIA";
             string mensajenano2 = "PARA SER CONSUMIDOS EN LA MISMA";
             e.Graphics.DrawString(mensajenano, letra4, Brushes.Black, new Point(120, i += 12), format2);
             e.Graphics.DrawString(mensajenano2, letra4, Brushes.Black, new Point(120, i += 12), format2);*/

            //string nanosms = "AUTORIZADO MEDIANTE RESOLUCION";
            //string nanosms2 = "NRO. 152-005-0000085/SUNAT";
            //string nanosms3 = "REPRESENTACION IMPRESA DEL DOCUMENTO";
            //string nanosms4 = "DE VENTA ELECTRONICA";
            //string consulta = "Consulte su B/F Electronica en el sgte enlace:";
            //string consulta2 = "Usted podrá verificar si una Factura Electrónica ";
            //string consulta3 = "o una Boleta Electrónica se encuentran";
            // string consulta4 = "registradas y/o informadas a SUNAT.";
            //  string consulta5 = "https://20352471578.operador.pe/buscar";
            /*string agradecimiento = "VISITE BICIMOTO";
            string agradecimiento2 = "TENEMOS LOS MEJORES PRECIOS ";
            string agradecimiento3 = "DEL MERCADO";*/
            //e.Graphics.DrawString(nanosms, letra4, Brushes.Black, new Point(145, i += 12), format2);
            //e.Graphics.DrawString(nanosms2, letra4, Brushes.Black, new Point(145, i += 12), format2);
            //e.Graphics.DrawString(nanosms3, letra4, Brushes.Black, new Point(145, i += 12), format2);
            //e.Graphics.DrawString(nanosms4, letra4, Brushes.Black, new Point(145, i += 12), format2);
            //e.Graphics.DrawString(consulta, letra9, Brushes.Black, new Point(145, i += 15), format2);
            // e.Graphics.DrawString(consulta2, letra4, Brushes.Black, new Point(120, i += 12), format2);
            // e.Graphics.DrawString(consulta3, letra4, Brushes.Black, new Point(120, i += 12), format2);
            // e.Graphics.DrawString(consulta4, letra4, Brushes.Black, new Point(120, i += 12), format2);
            // e.Graphics.DrawString(consulta5, letra9, Brushes.Black, new Point(145, i += 12), format2);
            /*e.Graphics.DrawString(agradecimiento, letra5, Brushes.Black, new Point(120, i += 20), format2);
            e.Graphics.DrawString(agradecimiento2, letra5, Brushes.Black, new Point(120, i += 12), format2);
            e.Graphics.DrawString(agradecimiento3, letra5, Brushes.Black, new Point(120, i += 12), format2);*/

            //Point point9 = new Point(0, (i += 20));
            //Point point10 = new Point(1000, i);
            //e.Graphics.DrawLine(Pens.Black, point9, point10);

            string profo1 = "OBSERVACIONES";
            string profo2 = "1. La proforma tiene una validez de 7 días ";//
            string profo3 = "    a la fecha emitida.";
            string profo4 = "2. Proforma sujeta a posible variación "; //La proforma no garantiza la disponiblidad del stock y los precios.
            string profo5 = "    del stock y los precios."; //( 
            // string profo6 = "3. Los precios dados en la proforma son el día de la misma."; //Todo cambio o devolucíon debe efectuarse el mismo de compra.
            //tring profo7 = "efectuarse durante el mismo día de compra.";
            // string profo8 = "4. Debes presentar tu boleta o factura electrónica."; 

            e.Graphics.DrawString(profo1, letra4, Brushes.Black, new Point(145, i += 0), format2);
            e.Graphics.DrawString(profo2, letra9, Brushes.Black, new Point(25, i += 15), format1);
            e.Graphics.DrawString(profo3, letra9, Brushes.Black, new Point(25, i += 12), format1);
            e.Graphics.DrawString(profo4, letra9, Brushes.Black, new Point(25, i += 12), format1);
            e.Graphics.DrawString(profo5, letra9, Brushes.Black, new Point(25, i += 12), format1);
            //e.Graphics.DrawString(profo6, letra9, Brushes.Black, new Point(25, i += 12), format1);
            //e.Graphics.DrawString(profo7, letra9, Brushes.Black, new Point(25, i += 12), format1);
            // e.Graphics.DrawString(profo8, letra9, Brushes.Black, new Point(25, i += 12), format1);
            Point point11 = new Point(0, (i += 20));
            Point point12 = new Point(1000, i);
            e.Graphics.DrawLine(Pens.Black, point11, point12);
            //+++++++++++++++++++++++++++++++++++++++++++++++++

            e.Graphics.DrawString("", letra4, Brushes.Black, new Point(0, i += 15), format1);
            e.Graphics.DrawString("", letra4, Brushes.Black, new Point(0, i += 15), format1);
            e.Graphics.DrawString("", letra4, Brushes.Black, new Point(0, i += 15), format1);
            e.Graphics.DrawString("", letra4, Brushes.Black, new Point(0, i += 15), format1);
        }

        private async void Doc_PrintPage(Object sender, PrintPageEventArgs e)
        {
            if (ObjVenta.BuscarVentaProforma(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
            {
            }
            else
            {
                MessageBox.Show("Error Venta nano", "SISTEMA");
                return;
            }

            if (ObjEmpresa.BuscarRuc(RucEmpresa.ToString()))
            {
            }
            else
            {
                MessageBox.Show("Error Empresa", "SISTEMA");
                return;
            }

            //Buscar documento de Venta
            if (ObjDocumento.BuscarDocNomMod(ObjVenta.Doc.ToString(), "VEN"))
            {
            }
            else
            {
                MessageBox.Show("Error Documento", "SISTEMA");
                return;
            }

            //Id del Comprobante
            string vMod = "VEN";
            ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
            if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
            {
                MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                return;
            }
            string nCliente = "";
            if (ObjVenta.Cliente.ToString().Equals("C0000000001"))
            {
                nCliente = "CLIENTES VARIOS";
            }
            else
            {
                //Cliente
                if (!ObjCliente.BuscarCLiente(ObjVenta.Cliente.ToString(), RucEmpresa.ToString()))
                {
                    MessageBox.Show("Error no se encontró el Cliente, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
            }

            // Creamos rectangle1
            float x = 10.0F;
            float y = 245.0F;
            float width = 260.0F;
            float height = 50.0F;
            RectangleF drawRect1 = new RectangleF(x, y, width, height);

            // Creamos rectangle2
            float x1 = 10.0F;
            float y2 = 280.0F;
            float width1 = 260.0F;
            float height2 = 50.0F;
            RectangleF drawRect2 = new RectangleF(x1, y2, width1, height2);

            // Creamos rectangle3
            float x3 = 10.0F;
            float y4 = 485.0F;
            float width3 = 270.0F;
            float height4 = 70.0F;
            RectangleF drawRect3 = new RectangleF(x3, y4, width3, height4);

            //----------------------
            StringFormat format2 = new StringFormat();//declara un nuevo formato de texto
            format2.Alignment = StringAlignment.Center;//elegimos la posicion del texto ,horizontalmente,en este caso centrado

            StringFormat format1 = new StringFormat(StringFormatFlags.DisplayFormatControl);//Alinea de Izquierda a derecha
            StringFormat format3 = new StringFormat(StringFormatFlags.DirectionRightToLeft);//Alinea de derecha a izquierda
                                                                                            //StringFormat format4 = new StringFormat(StringFormatFlags.);//

            //Declaramos un tipo de letra
            Font letra = new Font("Calibri", 9F);
            Font letra1 = new Font("Segoe UI Semibold", 7);
            //Font letra1 = new Font("Gill Sans MT", 8);
            Font letra2 = new Font("Lucida Console", 10);
            Font letra4 = new Font("Ebrima", 9);
            // Font letra4 = new Font("Gill Sans MT", 9);
            Font letra5 = new Font("Ebrima", 9);
            Font letra6 = new Font("Arial", 6);//Segoe UI Semibold
            Font letra7 = new Font("Yu Gothic UI Semibold", 9);
            Font letra8 = new Font("Yu Gothic UI Semibold", 10);
            Font letra9 = new Font("Ebrima", 8);
            Font letra10 = new Font("Ebrima", 11);
            //Font letra8 = new Font("Lucida Sans Unicode", 10);

            int i = -2;//declaramos entero i,para autoaumento de las coordenadas

            //e.Graphics.DrawString(ObjEmpresa.Razon.ToString().Trim(), letra2, Brushes.Black, new Point(110, i), format2);
            //e.Graphics.DrawString(ObjEmpresa.Direccion.ToString().Trim(), letra4, Brushes.Black, new Point(30, i += 15), format1);
            e.Graphics.DrawString(ObjEmpresa.Direccion.ToString().Trim(), letra1, Brushes.Black, new Point(-2, i), format1);
            //e.Graphics.DrawString(ObjEmpresa.Ubicacion.ToString().Trim(), letra1, Brushes.Black, new Point(114, i), format1);
            string nomTelef = "Telf.:" + ObjEmpresa.Telefono.ToString().Trim();
            /*int tlenum = 0;
            if (nomTelef.Length > 19)
            {
                tlenum = nomTelef.Length - 19;
                e.Graphics.DrawString(nomTelef.Substring(0, 19), letra4, Brushes.Black, new Point(137, i += 14), format1);
                e.Graphics.DrawString(nomTelef.Substring(19, tlenum), letra4, Brushes.Black, new Point(3, i += 9), format1);
            }
            else
            {
                e.Graphics.DrawString(nomTelef, letra4, Brushes.Black, new Point(0, i += 14), format1);
            }*/
            e.Graphics.DrawString(nomTelef, letra1, Brushes.Black, new Point(106, i), format1); //new Point(-2, i += 10), format1);
            string nomTelefSisley = "Sucursal: Jr.Guillermo Sisley 340 Telf.:" + ObjEmpresa.Telefono2.ToString().Trim();
            e.Graphics.DrawString(nomTelefSisley, letra1, Brushes.Black, new Point(5, i += 10), format1);// new Point(145, i /*+= 12*/), format1);
            e.Graphics.DrawString(ObjEmpresa.Ubicacion.ToString().Trim(), letra1, Brushes.Black, new Point(140, i += 10), format2);
            string nomRuc = "RUC: " + ObjEmpresa.Ruc.ToString().Trim();
            e.Graphics.DrawString(nomRuc, letra7, Brushes.Black, new Point(140, i += 10), format2);

            string vDocumento = ObjDocumento.Nombre.ToString().Trim();
            string vCli = ObjVenta.Cliente.Substring(0, 1);
            if (vCli.Equals("C"))
            {
                e.Graphics.DrawString(vDocumento, letra8, Brushes.Black, new Point(142, i += 13), format2);
            }
            else
            {
                if (ObjCliente.RucDni.ToString().Length == 11 && ObjCliente.TipDoc.ToString().Equals("RUC"))
                {
                    e.Graphics.DrawString(vDocumento, letra8, Brushes.Black, new Point(142, i += 15), format2);
                }
                else
                {
                    e.Graphics.DrawString(vDocumento, letra8, Brushes.Black, new Point(142, i += 15), format2);
                }
            }

            //Numero de documento
            string vIdComprobante = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
            e.Graphics.DrawString(vIdComprobante, letra, Brushes.Black, new Point(145, i += 15), format2);

            // string fecEmision = "FECHA DE EMISIÓN\t: " + ObjVenta.Fecha.ToString().Trim() + "  -  " + ObjVenta.FecCreacion.ToString().Substring(11, 13);
            string fecEmision = "FECHA DE EMISIÓN\t: " + ObjVenta.FecCreacion.ToString().Trim();

            //Probando tabulacion
            // Prepare a StringFormat to use the tabs.
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 3 };
                    string_format.SetTabStops(0, tabs);
                    //e.Graphics.DrawString(fecEmision, letra4, Brushes.Black, new Point(-2, i += 20), string_format);

                    e.Graphics.DrawString(fecEmision, letra9, Brushes.Black, -2, i += 15, string_format);
                }
            }
            //cruzamos los dedos a q salga esta huevaada
            // e.Graphics.DrawString(fecEmision, letra4, Brushes.Black, new Point(-2, i += 20), format1);

            /* string horaEmision = "HORA DE EMISION\t : " + ObjVenta.FecCreacion.ToString().Substring(11, 13);

              using (StringFormat string_format = new StringFormat())
              {
                  using (Font font = new Font("Arial", 7))
                  {
                      // Define the tab stops.
                      //float[] tabs = { 450, 75, 75 };
                      float[] tabs = {9};
                      string_format.SetTabStops(0, tabs);
                      e.Graphics.DrawString(horaEmision, letra4, Brushes.Black, new Point(-2, i += 12), string_format);
                  }
              }*/

            //Sera el codigo del almacén
            string vParam = "2";
            string vCodCat = "001";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda.ToString(), vParam.ToString());

            string localAlm = "LOCAL\t: " + CodAlmacen.ToString();//+ "  " + " CAJA: " + "001" + "  " + "MONEDA: " + ObjDetCatalogo.Descripcion.ToString() + "  " + " V:  " + ObjVenta.Vendedor.ToString();

            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 102 };
                    string_format.SetTabStops(0, tabs);
                    e.Graphics.DrawString(localAlm, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }

            // e.Graphics.DrawString(localAlm, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            //Por el momento será caja unica, para todos los locales
            string caja = "CAJA\t: " + "001"; //Valor en duro
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 102 };
                    string_format.SetTabStops(0, tabs);
                    // e.Graphics.DrawString(caja, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            // e.Graphics.DrawString(caja, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            //sE OBTIENE TIPO DE MONEDA DE CATALOGO
            // string vParam = "2";
            // string vCodCat = "001";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda.ToString(), vParam.ToString());

            string tipMoneda = "TIPO DE MONEDA\t: " + ObjDetCatalogo.Descripcion.ToString();
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 102 };
                    string_format.SetTabStops(0, tabs);
                    //  e.Graphics.DrawString(tipMoneda, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            //e.Graphics.DrawString(tipMoneda, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            string vVendedor = "SERIE IMPRESORA\t  : "/* + ObjVenta.Vendedor.ToString() + "*/  + ObjSerie.NumSerieImp.ToString();
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 6 };
                    string_format.SetTabStops(0, tabs);
                    //  e.Graphics.DrawString(vVendedor, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            // e.Graphics.DrawString(vVendedor, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            //Por el momento será cajero unico para todos los locales
            string vCajero = "CAJERO\t  : " + "001"; // valor en duro
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 96 };
                    string_format.SetTabStops(0, tabs);
                    // e.Graphics.DrawString(vCajero, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            // e.Graphics.DrawString(vCajero, letra4, Brushes.Black, new Point(-2, i += 12), format1);

            //Aca debe ir la numero de serie de la impresora
            /* string numSerie = "NÚMERO DE SERIE\t : " + ObjSerie.NumSerieImp.ToString(); // configurar la tabla TblSerie (impresora)
             using (StringFormat string_format = new StringFormat())
             {
                 using (Font font = new Font("Arial", 7))
                 {
                     // Define the tab stops.
                     //float[] tabs = { 450, 75, 75 };
                    float[] tabs = { 10 };
                     string_format.SetTabStops(0, tabs);
                     e.Graphics.DrawString(numSerie, letra4, Brushes.Black, new Point(-2, i += 12), string_format);
                 }
             }*/

            Point pointSharilyn1 = new Point(0, (i += 17));
            Point pointSharilyn2 = new Point(1050, i);

            e.Graphics.DrawLine(Pens.Black, pointSharilyn1, pointSharilyn2);
            string vCliente = "";
            //e.Graphics.DrawString(numSerie, letra4, Brushes.Black, new Point(-2, i += 12), format1);
            if (nCliente == "CLIENTES VARIOS" || vCli.Equals("C"))
            {
                string vDoc = "IDENTIFICACIÓN      :";
                e.Graphics.DrawString(vDoc, letra9, Brushes.Black, new Point(-2, i += 2), format1);
                /* using (StringFormat string_format = new StringFormat())
                 {
                     using (Font font = new Font("Arial", 7))
                     {
                         // Define the tab stops.
                         //float[] tabs = { 450, 75, 75 };
                         float[] tabs = {21};

                         string_format.SetTabStops(0, tabs);
                         e.Graphics.DrawString(vDoc, letra9, Brushes.Black, new Point(-2, i += 12), string_format);

                         vTab = tabs.Sum();
                     }
                 }*/
            }
            else
            {
                string vDoc = "IDENTIFICACIÓN\t: " + ObjVenta.TipDocCli.ToString() + " - " + ObjVenta.Cliente.ToString();
                using (StringFormat string_format = new StringFormat())
                {
                    using (Font font = new Font("Arial", 7))
                    {
                        // Define the tab stops.
                        //float[] tabs = { 450, 75, 75 };
                        float[] tabs = { 102 };
                        string_format.SetTabStops(0, tabs);
                        e.Graphics.DrawString(vDoc, letra9, Brushes.Black, new Point(-2, i += 2), string_format);

                        vTab = tabs.Sum();
                    }
                }
            }
            //e.Graphics.DrawString(vDoc, letra4, Brushes.Black, new Point(-2, i += 12), format1);
            int nTamCli = 0;
            if (nCliente == "CLIENTES VARIOS")
            {
                vCliente = "RAZÓN/NOMBRE\t    : " + nCliente;
                using (StringFormat string_format = new StringFormat())
                {
                    e.Graphics.DrawString(vCliente, letra9, Brushes.Black, new Point(-2, i += 12), format1);
                    /* using (Font font = new Font("Arial", 7))
                     {
                         // Define the tab stops.
                         //float[] tabs = { 450, 75, 75 };
                         float[] tabs = {20};
                         string_format.SetTabStops(0, tabs);
                         e.Graphics.DrawString(vCliente, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                     }*/

                    // e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
                }
                //vCliente = "CLIENTE : " + nCliente;

                // e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
            }
            else
            {
                vCliente = "RAZÓN/NOMBRE\t    : " + ObjCliente.Nombre.ToString();
                // e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
                using (StringFormat string_format = new StringFormat())
                {
                    /*using (Font font = new Font("Arial", 7))
                    {
                        if (vCliente.Length > 40)
                        {
                            nTamCli = vCliente.Length - 40;
                            e.Graphics.DrawString(vCliente.Substring(0, 40), letra4, Brushes.Black, new Point(-2, i += 12), format1);

                            e.Graphics.DrawString(vCliente.Substring(40, nTamCli), letra4, Brushes.Black, new Point(-2, i += 10), format1);
                        }
                        else
                        {
                            e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
                            vTab = tabs.Sum();
                        }
                        */

                    // e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
                }
                if (vCliente.Length > 46)
                {
                    nTamCli = vCliente.Length - 46;
                    e.Graphics.DrawString(vCliente.Substring(0, 46), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                    e.Graphics.DrawString(vCliente.Substring(46, nTamCli), letra9, Brushes.Black, new Point(105, i += 10), format1);
                }
                else
                {
                    e.Graphics.DrawString(vCliente, letra9, Brushes.Black, new Point(-2, i += 12), format1);
                }
            }
            // string vCliente = "SEÑOR(A) : " + ObjCliente.Nombre.ToString();
            //e.Graphics.DrawString(vCliente, letra4, Brushes.Black, new Point(-2, i += 12), format1);
            string vDireccion = "";
            int nTama = 0;

            if (nCliente == "CLIENTES VARIOS")
            {
                vDireccion = "DIRECCIÓN\t: ";
                //e.Graphics.DrawString(vDireccion, letra4, Brushes.Black, new Point(-2, i += 12), format1);

                using (StringFormat string_format = new StringFormat())
                {
                    using (Font font = new Font("Arial", 7))
                    {
                        // Define the tab stops.
                        //float[] tabs = { 450, 75, 75 };
                        float[] tabs = { 51 };
                        string_format.SetTabStops(0, tabs);
                        e.Graphics.DrawString(vDireccion, letra9, Brushes.Black, new Point(-2, i += 12), string_format);

                        vTab = tabs.Sum();
                    }
                }
            }
            else
            {
                //e.Graphics.DrawString(vDireccion, letra4, Brushes.Black, new Point(-2, i += 12), format1);

                vDireccion = "DIRECCIÓN               : " + ObjCliente.Direccion.ToString();
                if (vDireccion.Length > 53)
                {
                    if (vDireccion.Length <= 90)
                    {
                        nTama = vDireccion.Length - 53;
                        e.Graphics.DrawString(vDireccion.Substring(0, 53), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                        e.Graphics.DrawString(vDireccion.Substring(53, nTama), letra9, Brushes.Black, new Point(105, i += 10), format1);
                    }
                    else if (vDireccion.Length < 100)
                    {
                        int nTama1 = vDireccion.Length - 83;
                        e.Graphics.DrawString(vDireccion.Substring(0, 55), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                        e.Graphics.DrawString(vDireccion.Substring(55, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(83, nTama1), letra9, Brushes.Black, new Point(105, i += 10), format1);
                    }
                    else if (vDireccion.Length < 130)
                    {
                        int nTama1 = vDireccion.Length - 111;
                        e.Graphics.DrawString(vDireccion.Substring(0, 55), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                        e.Graphics.DrawString(vDireccion.Substring(55, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(83, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(111, nTama1), letra9, Brushes.Black, new Point(105, i += 10), format1);
                    }
                    else
                    {
                        int nTama1 = vDireccion.Length - 139;
                        e.Graphics.DrawString(vDireccion.Substring(0, 55), letra9, Brushes.Black, new Point(-2, i += 12), format1);
                        e.Graphics.DrawString(vDireccion.Substring(55, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(83, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(111, 28), letra9, Brushes.Black, new Point(105, i += 10), format1);
                        e.Graphics.DrawString(vDireccion.Substring(139, nTama1), letra9, Brushes.Black, new Point(105, i += 10), format1);
                    }
                    //nTama = vDireccion.Length - 46;

                    /*e.Graphics.DrawString(vDireccion.Substring(0, 43), letra4, Brushes.Black, new Point(-2, i += 12), format1);
                    e.Graphics.DrawString(vDireccion.Substring(43,40), letra4, Brushes.Black, new Point(-2, i += 10), format1);
                    e.Graphics.DrawString(vDireccion.Substring(83, nTama), letra4, Brushes.Black, new Point(-2, i += 10), format1);*/

                    /* e.Graphics.DrawString(vDireccion.Substring(0, 46), letra4, Brushes.Black, new Point(-2, i += 12), format1);
                     e.Graphics.DrawString(vDireccion.Substring(46, nTama), letra4, Brushes.Black, new Point(-2, i += 10), format1);*/
                }
                else
                {
                    e.Graphics.DrawString(vDireccion, letra9, Brushes.Black, new Point(-2, i += 12), format1);
                }
            }
            //Imprimiendo linea

            Point point1 = new Point(0, (i += 20));
            Point point2 = new Point(1050, i);

            e.Graphics.DrawLine(Pens.Black, point1, point2);

            string Codi = "Código";
            e.Graphics.DrawString(Codi, letra9, Brushes.Black, new Point(-2, i += 0), format1);

            string vDesc = "Descripción";
            e.Graphics.DrawString(vDesc, letra9, Brushes.Black, new Point(60, i), format1);

            string vCant = "Cant   ";
            e.Graphics.DrawString(vCant, letra9, Brushes.Black, new Point(158, i), format1);

            string vPrec = " Precio";
            e.Graphics.DrawString(vPrec, letra9, Brushes.Black, new Point(196, i), format1);

            string vTotal = "TOTAL";
            e.Graphics.DrawString(vTotal, letra9, Brushes.Black, new Point(248, i /*-= 5*/), format1);

            Point point3 = new Point(0, (i += 17));
            Point point4 = new Point(1000, i);

            e.Graphics.DrawLine(Pens.Black, point3, point4);

            //Se imprime articulos

            DataSet datos = csql.dataset_cadena("Call SpDetProformaBuscar('" + Id.ToString() + "','" + RucEmpresa.ToString() + "','" + CodAlmacen.ToString() + "')");
            string nArticulo = "";
            //string vIgv = "";
            string vPunit = "";
            string vImporte = "";
            double Net = 0;
            bool nCheckDet = false;

            int n = 0;
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    nArticulo = fila[1].ToString().Trim();
                    Net = double.Parse(fila[5].ToString().Equals("") ? "0" : fila[5].ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        vPunit = "";
                    else
                        vPunit = Net.ToString("#,##0.00").Trim();

                    Net = double.Parse(fila[7].ToString().Equals("") ? "0" : fila[7].ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        vImporte = "";
                    else
                        vImporte = Net.ToString(" #,##0.00").Trim();

                    //Imprimimos
                    string det = fila[12].ToString();
                    int nLenght = 40;
                    int nTamanio = det.Length;
                    int nTam = 0;
                    n += 1;
                    // string vMulti = fila[4].ToString().Trim() + " x " + vPunit.ToString();

                    if (nArticulo.Length > 20)
                    {
                        nTam = nArticulo.Length - 20;
                        e.Graphics.DrawString(fila[0].ToString().Trim(), letra9, Brushes.Black, new Point(3, i += 3), format1);
                        e.Graphics.DrawString(nArticulo.Substring(0, 20), letra9, Brushes.Black, new Point(35, i), format1);

                        e.Graphics.DrawString(fila[4].ToString().Trim(), letra9, Brushes.Black, new Point(183, i), format3);
                        e.Graphics.DrawString(vPunit, letra9, Brushes.Black, new Point(235, i), format3);
                        e.Graphics.DrawString(vImporte, letra9, Brushes.Black, new Point(285, i), format3);
                        e.Graphics.DrawString(nArticulo.Substring(20, nTam), letra9, Brushes.Black, new Point(35, i += 12), format1);
                    }
                    else
                    {
                        e.Graphics.DrawString(fila[0].ToString().Trim(), letra9, Brushes.Black, new Point(3, i += 3), format1);
                        e.Graphics.DrawString(nArticulo, letra9, Brushes.Black, new Point(35, i), format1);
                        e.Graphics.DrawString(fila[4].ToString().Trim(), letra9, Brushes.Black, new Point(183, i), format3);
                        e.Graphics.DrawString(vPunit, letra9, Brushes.Black, new Point(235, i), format3);
                        e.Graphics.DrawString(vImporte, letra9, Brushes.Black, new Point(286, i), format3);
                    }

                    /*  if (nArticulo.Length > 20)
                      {
                          e.Graphics.DrawString(fila[4].ToString().Trim(), letra5, Brushes.Black, new Point(198, i), format3);
                          e.Graphics.DrawString(vPunit, letra5, Brushes.Black, new Point(238, i), format3);
                      }
                      else
                      {
                          e.Graphics.DrawString(fila[4].ToString().Trim(), letra5, Brushes.Black, new Point(198, i), format3);
                          e.Graphics.DrawString(vPunit, letra5, Brushes.Black, new Point(239, i), format3);
                      }*/

                    if (det.Length > 40)
                    {
                        nTamanio = det.Length - 40;

                        e.Graphics.DrawString(det.Substring(0, 29), letra4, Brushes.Black, new Point(0, i += 5), format1);
                        e.Graphics.DrawString(det.Substring(29, nTamanio), letra4, Brushes.Black, new Point(0, i += 10), format1);
                    }
                    else
                    {
                        e.Graphics.DrawString(det, letra4, Brushes.Black, new Point(0, i += 10), format1);
                    }
                    /* if (det.Length > 40)
                     {
                         nLenght = 40;
                         nTam = 0;
                         do
                         {
                             e.Graphics.DrawString(det.Substring(nTam, nLenght), letra4, Brushes.Black, new Point(0, i += 5), format1);
                             nTam = nTam + 40;
                         } while ((nTam + 40) < det.Length);

                         e.Graphics.DrawString(det.Substring(nTam, (det.Length - nTam)), letra4, Brushes.Black, new Point(0, i += 5), format1);
                     }
                     else
                     {
                         e.Graphics.DrawString(det.ToString(), letra4, Brushes.Black, new Point(0, i += 10), format1);
                     }*/

                    /*  string det = fila[12].ToString();
                      int nLenght = 0;
                      int nTamanio = det.Length;*/
                    Point point5 = new Point(0, (i += 12));
                    Point point6 = new Point(1000, i);

                    e.Graphics.DrawLine(Pens.Black, point5, point6);
                }

                /*n += 1;
                string vMulti = fila[4].ToString().Trim() + " x " + vPunit.ToString();
                if (n == 0)
                {
                    e.Graphics.DrawString(vMulti, letra4, Brushes.Black, new Point(135, i /*+= 12), format1);
                }
                else
                {
                    e.Graphics.DrawString(vMulti, letra4, Brushes.Black, new Point(135, i += 10), format1);
                }
                e.Graphics.DrawString(vImporte, letra4, Brushes.Black, new Point(247, i), format3);
            }*/
            }

            //ClsConversiones.NumeroALetrasString(ObjVenta.Total.ToString());
            //TOTALES
            string opGravada = "";
            string opExonerada = "";
            string opInafecta = "";
            string vIgv = "";
            string vTTotal = "";
            //e.Graphics.DrawString("OP. EXONERADA", letra4, Brushes.Black, new Point(0, i += 3), format1);
            //e.Graphics.DrawString("0.00", letra4, Brushes.Black, new Point(245, i), format3);
            //Net = double.Parse(ObjVenta.TExonerada.ToString().Equals("") ? "0" : ObjVenta.TExonerada.ToString().Trim());
            //if (Net.ToString().Trim().Equals("0"))
            //    opExonerada = Net.ToString("#,##0.00").Trim();//"0.00";
            //else
            //    opExonerada = Net.ToString("#,##0.00").Trim();
            ////e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            ////e.Graphics.DrawString(opExonerada, letra4, Brushes.Black, new Point(284, i), format3);
            //// opExonerada = Net.ToString("#S/ #,##0.00").Trim();
            ////e.Graphics.DrawString(opExonerada, letra4, Brushes.Black, new Point(284, i), format3);

            ////e.Graphics.DrawString("OP. INAFECTA", letra4, Brushes.Black, new Point(0, i += 12), format1);
            ////e.Graphics.DrawString("0.00", letra4, Brushes.Black, new Point(245, i), format3);
            //Net = double.Parse(ObjVenta.TInafecta.ToString().Equals("") ? "0" : ObjVenta.TInafecta.ToString().Trim());
            //if (Net.ToString().Trim().Equals("0"))
            //    opInafecta = Net.ToString("#,##0.00").Trim();//"0.00";
            //else
            //    opInafecta = Net.ToString("##,##0.00").Trim();
            ////e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            ////e.Graphics.DrawString(opInafecta, letra4, Brushes.Black, new Point(284, i), format3);

            ////e.Graphics.DrawString("OP. GRAVADA", letra4, Brushes.Black, new Point(0, i += 12), format1);
            //Net = double.Parse(ObjVenta.TBruto.ToString().Equals("") ? "0" : ObjVenta.TBruto.ToString().Trim());
            //if (Net.ToString().Trim().Equals("0"))
            //    opGravada = Net.ToString("#,##0.00").Trim();//"0.00";
            //else
            //    opGravada = Net.ToString("#,##0.00").Trim();
            ////e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            ////opGravada = Net.ToString("#S/ #,##0.00").Trim();
            ////e.Graphics.DrawString(opGravada, letra4, Brushes.Black, new Point(284, i), format3);

            ////e.Graphics.DrawString("IGV", letra4, Brushes.Black, new Point(0, i += 12), format1);
            //Net = double.Parse(ObjVenta.TIgv.ToString().Equals("") ? "0" : ObjVenta.TIgv.ToString().Trim());
            //if (Net.ToString().Trim().Equals("0"))
            //    vIgv = Net.ToString("#,##0.00").Trim();//"0.00";
            //else
            //    vIgv = Net.ToString("#,##0.00").Trim();
            //e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            //vIgv = Net.ToString("#S/ #,##0.00").Trim();
            //e.Graphics.DrawString(vIgv, letra4, Brushes.Black, new Point(284, i), format3);

            e.Graphics.DrawString("IMPORTE TOTAL", letra4, Brushes.Black, new Point(0, i += 3), format1);
            Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
            if (Net.ToString().Trim().Equals("0"))
                vTTotal = Net.ToString("##,##0.00").Trim();//"0.00";
            else
                vTTotal = Net.ToString("##,##0.00").Trim();
            e.Graphics.DrawString("S/", letra4, Brushes.Black, new Point(220, i), format1);
            // vTTotal = Net.ToString("#S/ ##,##0.00").Trim();
            e.Graphics.DrawString(vTTotal, letra4, Brushes.Black, new Point(284, i), format3);

            string numLetra = "SON: " + ClsConversiones.NumeroALetrasString(ObjVenta.Total.ToString());
            int lenum = 0;
            if (numLetra.Length > 44)
            {
                lenum = numLetra.Length - 44;
                e.Graphics.DrawString(numLetra.Substring(0, 44), letra9, Brushes.Black, new Point(0, i += 14), format1);
                e.Graphics.DrawString(numLetra.Substring(44, lenum), letra9, Brushes.Black, new Point(0, i += 10), format1);
            }
            else
            {
                e.Graphics.DrawString(numLetra, letra9, Brushes.Black, new Point(0, i += 14), format1);
            }

            Point point7 = new Point(0, (i += 18));
            Point point8 = new Point(1000, i);

            e.Graphics.DrawLine(Pens.Black, point7, point8);

            if (ObjDocumento.EnvSunat.Equals("S"))
            {
                //string resumen = "V. RESUMEN: " + ObjVenta.ResumenFirma;
                //*******************************************************
                string resumen = "";
                string resumenFinal = "";
                if (ClsGrabaXML.vResumenFirma.Equals(""))
                {
                    if (ObjVenta.ResumenFirma.Equals("") || ObjVenta.ResumenFirma == null)
                    {
                        if (ObjVentaFirma.BuscarVentaDatosXml(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
                        {
                            resumen = ObjVentaFirma.ResumenFirma;
                        }
                    }
                    else
                    {
                        resumen = ObjVenta.ResumenFirma;
                    }
                }
                else
                {
                    resumen = ClsGrabaXML.vResumenFirma;
                }

                if (resumen.Equals(""))
                {
                    ClsVenta ObjVenta2 = new ClsVenta();
                    if (!ObjVenta2.BuscarVenta(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
                    {
                        MessageBox.Show("Error no se encontró datos de comprobante de Venta, VERIFIQUE!!!", "SISTEMA");
                        return;
                    }

                    ClsGrabaXML ObjXml = new ClsGrabaXML();
                    //string vFirma = await ObjXml.GeneraXMLFirma(ObjVenta.ArchivoXml, Id, RucEmpresa.ToString(), CodAlmacen.ToString());

                    if (resumen.Equals(""))
                    {
                        ClsVenta ObjVentaXml2 = new ClsVenta();
                        if (ObjVentaXml2.BuscarVentaDatosXml(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
                        {
                            resumen = ObjVentaXml2.ResumenFirma;
                        }
                        else
                        {
                            if (vResumenFirma.Equals(""))
                            {
                                resumen = ClsGrabaXML.vResumenFirma;
                            }
                            else
                            {
                                resumen = vResumenFirma;
                            }
                        }
                    }
                    else
                    {
                        resumen = vResumenFirma;
                    }

                    //} else
                    //{
                    //resumenFinal = "V. RESUMEN: " + resumen.ToString();

                    //e.Graphics.DrawString(resumenFinal, letra4, Brushes.Black, new Point(0, i += 9), format1);
                }

                resumenFinal = "Código HASH : " + resumen.ToString();
                e.Graphics.DrawString(resumenFinal, letra9, Brushes.Black, new Point(-2, i += 5), format1);

                string nomdocumento = ObjEmpresa.Ruc + "-" + vIdComprobante;

                string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR", $"{nomdocumento}.jpg");

                //System.Threading.Thread.Sleep(7000);
                //MessageBox.Show(filename, "SISTEMA");

                if (System.IO.File.Exists(filename))
                {
                    Image img = Image.FromFile(filename);
                    //e.Graphics.DrawImage(img, 55, i += 18, 130, 80);
                    e.Graphics.DrawImage(img, 100, i += 18, 85, 80);
                    i += 70;
                }
                else
                {
                    vCodCat = "018";
                    ObjDetCatalogo.BuscarDetCatalogoCod(vCodCat.ToString(), ObjVenta.TipDocCli.ToString());

                    //Volvemos a generar codigo QR
                    string datosQR;
                    string serie = ObjSerie.PrefijoSerie + ObjVenta.Serie;
                    datosQR = ObjEmpresa.Ruc.ToString() + "|" + ObjDocumento.NCorto.ToString() + "|" + serie + "-" + ObjVenta.Numero + "|" + ObjVenta.TIgv.ToString() + "|" + ObjVenta.Total.ToString() + "|" +
                              DateTime.Parse(ObjVenta.Fecha).ToString("yyyy-MM-dd") + "|" + ObjVenta.TipDocCli.ToString() + "|" +
                              ObjVenta.Cliente.ToString();
                    //Generar QR
                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    int scale = 4; // Convert.ToInt16(txtSize.Text);
                    qrCodeEncoder.QRCodeScale = scale;
                    int version = 6;
                    qrCodeEncoder.QRCodeVersion = version;

                    Image image;
                    String data = datosQR.ToString();
                    image = qrCodeEncoder.Encode(data);

                    nomdocumento = ObjEmpresa.Ruc.ToString() + "-" + ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

                    string carpetaqr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR");

                    if (!(Directory.Exists(carpetaqr)))
                    {
                        Directory.CreateDirectory(carpetaqr);
                    }

                    string ruta = "";
                    ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR", $"{nomdocumento}.jpg");
                    //bm.Save(ruta, System.Drawing.Imaging.ImageFormat.Jpeg);
                    image.Save(ruta, System.Drawing.Imaging.ImageFormat.Jpeg);

                    Image img = Image.FromFile(ruta);
                    e.Graphics.DrawImage(img, 100, i += 18, 85, 80);
                    //e.Graphics.DrawImage(img, 55, i += 18, 130, 80);
                    i += 70;
                }

                //*******************************************************
            }

            //empieza impresion nano
            /* string mensajenano = "BIENES TRANSFERIDOS EN LA AMAZONIA";
             string mensajenano2 = "PARA SER CONSUMIDOS EN LA MISMA";
             e.Graphics.DrawString(mensajenano, letra4, Brushes.Black, new Point(120, i += 12), format2);
             e.Graphics.DrawString(mensajenano2, letra4, Brushes.Black, new Point(120, i += 12), format2);*/

            //string nanosms = "AUTORIZADO MEDIANTE RESOLUCION";
            //string nanosms2 = "NRO. 152-005-0000085/SUNAT";
            //string nanosms3 = "REPRESENTACION IMPRESA DEL DOCUMENTO";
            //string nanosms4 = "DE VENTA ELECTRONICA";
            //string consulta = "Consulte su B/F Electronica en el sgte enlace:";
            //string consulta2 = "Usted podrá verificar si una Factura Electrónica ";
            //string consulta3 = "o una Boleta Electrónica se encuentran";
            // string consulta4 = "registradas y/o informadas a SUNAT.";
            //  string consulta5 = "https://20352471578.operador.pe/buscar";
            /*string agradecimiento = "VISITE BICIMOTO";
            string agradecimiento2 = "TENEMOS LOS MEJORES PRECIOS ";
            string agradecimiento3 = "DEL MERCADO";*/
            //e.Graphics.DrawString(nanosms, letra4, Brushes.Black, new Point(145, i += 12), format2);
            //e.Graphics.DrawString(nanosms2, letra4, Brushes.Black, new Point(145, i += 12), format2);
            //e.Graphics.DrawString(nanosms3, letra4, Brushes.Black, new Point(145, i += 12), format2);
            //e.Graphics.DrawString(nanosms4, letra4, Brushes.Black, new Point(145, i += 12), format2);
            //e.Graphics.DrawString(consulta, letra9, Brushes.Black, new Point(145, i += 15), format2);
            // e.Graphics.DrawString(consulta2, letra4, Brushes.Black, new Point(120, i += 12), format2);
            // e.Graphics.DrawString(consulta3, letra4, Brushes.Black, new Point(120, i += 12), format2);
            // e.Graphics.DrawString(consulta4, letra4, Brushes.Black, new Point(120, i += 12), format2);
            // e.Graphics.DrawString(consulta5, letra9, Brushes.Black, new Point(145, i += 12), format2);
            /*e.Graphics.DrawString(agradecimiento, letra5, Brushes.Black, new Point(120, i += 20), format2);
            e.Graphics.DrawString(agradecimiento2, letra5, Brushes.Black, new Point(120, i += 12), format2);
            e.Graphics.DrawString(agradecimiento3, letra5, Brushes.Black, new Point(120, i += 12), format2);*/

            //Point point9 = new Point(0, (i += 20));
            //Point point10 = new Point(1000, i);
            //e.Graphics.DrawLine(Pens.Black, point9, point10);

            string profo1 = "OBSERVACIONES";
            string profo2 = "1. La proforma tiene una validez de 7 días ";//
            string profo3 = "    a la fecha emitida.";
            string profo4 = "2. Proforma sujeta a posible variación "; //La proforma no garantiza la disponiblidad del stock y los precios.
            string profo5 = "    del stock y los precios."; //( 
            // string profo6 = "3. Los precios dados en la proforma son el día de la misma."; //Todo cambio o devolucíon debe efectuarse el mismo de compra.
            //tring profo7 = "efectuarse durante el mismo día de compra.";
            // string profo8 = "4. Debes presentar tu boleta o factura electrónica."; 

            e.Graphics.DrawString(profo1, letra4, Brushes.Black, new Point(145, i += 0), format2);
            e.Graphics.DrawString(profo2, letra9, Brushes.Black, new Point(25, i += 15), format1);
            e.Graphics.DrawString(profo3, letra9, Brushes.Black, new Point(25, i += 12), format1);
            e.Graphics.DrawString(profo4, letra9, Brushes.Black, new Point(25, i += 12), format1);
            e.Graphics.DrawString(profo5, letra9, Brushes.Black, new Point(25, i += 12), format1);
            //e.Graphics.DrawString(profo6, letra9, Brushes.Black, new Point(25, i += 12), format1);
            //e.Graphics.DrawString(profo7, letra9, Brushes.Black, new Point(25, i += 12), format1);
            // e.Graphics.DrawString(profo8, letra9, Brushes.Black, new Point(25, i += 12), format1);
            Point point11 = new Point(0, (i += 20));
            Point point12 = new Point(1000, i);
            e.Graphics.DrawLine(Pens.Black, point11, point12);
            //+++++++++++++++++++++++++++++++++++++++++++++++++

            e.Graphics.DrawString("", letra4, Brushes.Black, new Point(0, i += 15), format1);
            e.Graphics.DrawString("", letra4, Brushes.Black, new Point(0, i += 15), format1);
            e.Graphics.DrawString("", letra4, Brushes.Black, new Point(0, i += 15), format1);
            e.Graphics.DrawString("", letra4, Brushes.Black, new Point(0, i += 15), format1);
        }

        public void ImprimirTicket(string IdVenta, string vEmpresa, string vAlmacen, Boolean isPdf, Boolean isDialog)
        {
            string defaultPrinter = string.Empty;

            //foreach (string strPrinter in PrinterSettings.InstalledPrinters)
            //{
            //if (strPrinter.Equals("Laser jet 5500"))
            //{
            //defaultPrinter = strPrinter;
            //break;
            //}
            //}

            Id = IdVenta.ToString();
            RucEmpresa = vEmpresa.ToString();
            CodAlmacen = vAlmacen.ToString();

            if (isPdf)
            {
                string nomDoc = string.Empty;
                defaultPrinter = "Microsoft Print to PDF";

                doc.PrinterSettings.PrinterName = defaultPrinter;

                if (ObjVenta.BuscarVentaProforma(IdVenta, vEmpresa.ToString(), vAlmacen.ToString()))
                {
                }
                else
                {
                    MessageBox.Show("Error Venta", "SISTEMA");
                    return;
                }

                string vMod = "VEN";
                ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
                if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
                {
                    MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                nomDoc = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

                if (isDialog)
                {
                    SaveFileDialog sdf = new SaveFileDialog();

                    sdf.Filter = "PDF Files |*.pdf";
                    sdf.FileName = $"{nomDoc}.pdf";
                    if (sdf.ShowDialog() == DialogResult.OK)
                    {
                        //doc.PrinterSettings.PrintToFile = true;

                        doc.PrinterSettings.PrintFileName = sdf.FileName;
                    }

                    doc.PrinterSettings.PrintToFile = true;
                }
                else
                {
                    defaultPrinter = "Microsoft Print to PDF";

                    doc.PrinterSettings.PrinterName = defaultPrinter;

                    //Generando carpeta del dia
                    DateTime fechaHoy = DateTime.Now;
                    string fecha = ObjVenta.Fecha.ToString();
                    string fechaAnio = fecha.Substring(6, 4);
                    string fechaMes = fecha.Substring(3, 2);
                    string fechaDia = fecha.Substring(0, 2);
                    string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();
                    string RutaArchivo;
                    string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"PDF", $"{rutafec}");

                    RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"PDF", $"{rutafec}",
                        $"{nomDoc}.pdf");

                    if (!(Directory.Exists(carpeta)))
                    {
                        Directory.CreateDirectory(carpeta);
                    }

                    doc.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("PaperA4", 80, 297);

                    doc.PrinterSettings.PrintFileName = RutaArchivo;
                    doc.PrinterSettings.PrintToFile = true;
                }
            }

            doc.PrintPage += new PrintPageEventHandler(Doc_PrintPage_Proforma);

            doc.PrintController = new StandardPrintController();

            doc.Print();
        }

        private static MySqlConnection GetNewConnection()
        {
            //const string MySqlConnecionString = "Server=" + FrmLogin.XServidor + "; Database= " + FrmLogin.XDB + "; Username=" + FrmLogin.XUser + "; Password=" + FrmLogin.XPassword + ";";

            var conn = new MySqlConnection("Server=" + FrmLogin.XServidor + "; Database= " + FrmLogin.XDB + "; Username=" + FrmLogin.XUser + "; Password=" + FrmLogin.XPassword + ";SslMode=" + FrmLogin.x_SslMode + ";");
            conn.Open();
            return conn;
        }

        private async System.Threading.Tasks.Task GeneraFirmaAsync(string vTrama, string Id, string Ruc, string Almacen)
        {
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
                TramaXmlSinFirma = ClsGrabaXML.vTrama,
                CertificadoDigital = Convert.ToBase64String(File.ReadAllBytes(vRuta.ToString())),
                PasswordCertificado = vPass.ToString(),
                //UnSoloNodoExtension = rbRetenciones.Checked || rbResumen.Checked
            };

            ICertificador certificador = new Certificador();

            var respuestaFirmado = await new Firmar(certificador).Post(firmadoRequest);

            string respta = respuestaFirmado.ResumenFirma;

            if (!respuestaFirmado.Exito)
                throw new ApplicationException(respuestaFirmado.MensajeError);

            using (MySqlConnection conn = GetNewConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE tblventa set ResumenFirma = @ResumenFirma, ValorFirma = @ValorFirma where IdVenta = @Id and Empresa = @numruc and Almacen = @Almacen";
                    cmd.Parameters.AddWithValue("@Id", Id.ToString());
                    cmd.Parameters.AddWithValue("@numruc", Ruc.ToString());
                    cmd.Parameters.AddWithValue("@Almacen", Almacen.ToString());
                    cmd.Parameters.AddWithValue("@ArchivoXml", respuestaFirmado.TramaXmlFirmado);
                    cmd.Parameters.AddWithValue("@ResumenFirma", respuestaFirmado.ResumenFirma);
                    cmd.Parameters.AddWithValue("@ValorFirma", respuestaFirmado.ValorFirma);
                    cmd.ExecuteNonQuery();
                }
            }
            vResumenFirma = respta;
        }
    }
}