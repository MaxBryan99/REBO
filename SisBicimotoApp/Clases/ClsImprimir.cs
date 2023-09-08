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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace SisBicimotoApp.Clases
{
    internal class ClsImprimir
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

        public ClsImprimir()
        {
        }

        private async void Doc_PrintPage(Object sender, PrintPageEventArgs e)
        {
            if (ObjVenta.BuscarVenta(Id, RucEmpresa.ToString(), CodAlmacen.ToString()))
            {
            }
            else
            {
                MessageBox.Show("Error Venta nano", "ZZOFT");
                return;
            }

            if (ObjEmpresa.BuscarRuc(RucEmpresa.ToString()))
            {
            }
            else
            {
                MessageBox.Show("Error Empresa", "ZZOFT");
                return;
            }

            //Buscar documento de Venta
            if (ObjDocumento.BuscarDocNomMod(ObjVenta.Doc.ToString(), "VEN"))
            {
            }
            else
            {
                MessageBox.Show("Error Documento", "ZZOFT");
                return;
            }

            //Id del Comprobante
            string vMod = "VEN";
            ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
            if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
            {
                MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "ZZOFT");
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
                    MessageBox.Show("Error no se encontró el Cliente, VERIFIQUE!!!", "ZZOFT");
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
            Font letra2 = new Font("Lucida Console", 10);
            Font letra4 = new Font("Ebrima", 9 , FontStyle.Bold);
            Font letra5 = new Font("Ebrima", 9);
            Font letra6 = new Font("Arial", 6);//Segoe UI Semibold
            Font letra7 = new Font("Yu Gothic UI Semibold", 9);
            Font letra8 = new Font("Yu Gothic UI Semibold", 10);
            Font letra9 = new Font("Ebrima", 8);
            Font letra10 = new Font("Ebrima", 11);
            Font letraJosé = new Font("Ebrima", 8);

            int i = -4;//declaramos entero i,para autoaumento de las coordenadas
            int j = -2;

            string nomTelef = "Telf.:" + ObjEmpresa.Telefono.ToString().Trim();
            string sucursal = "REBO MARKET";
           
            e.Graphics.DrawString(sucursal, letra8, Brushes.Black, new Point(140, i += 11), format2);

            string nomTelefSisley = "Jr. 28 de julio Mz. 11 lote 1B " + " Telf.:" + ObjEmpresa.Telefono.ToString().Trim();
            e.Graphics.DrawString(nomTelefSisley, letra9, Brushes.Black, new Point(140, i += 15), format2);
            
            string nomRuc = "RUC: " + ObjEmpresa.Ruc.ToString().Trim();
            e.Graphics.DrawString(nomRuc, letra7, Brushes.Black, new Point(140, i += 11), format2);

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

            string fecEmision = "FECHA DE EMISIÓN\t: " + ObjVenta.FecCreacion.ToString().Trim();

            //Probando tabulacion
            // Prepare a StringFormat to use the tabs.
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    float[] tabs = { 3 };
                    string_format.SetTabStops(0, tabs);

                    e.Graphics.DrawString(fecEmision, letra9, Brushes.Black, -2, i += 15, string_format);
                }
            }

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

            //Por el momento será caja unica, para todos los locales
            string caja = "VENDEDOR\t: " + ObjVenta.Vendedor.ToString().ToUpper(); //Valor en duro
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    float[] tabs = { 102 };
                    string_format.SetTabStops(0, tabs);
                    e.Graphics.DrawString(caja, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }

            //sE OBTIENE TIPO DE MONEDA DE CATALOGO
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda.ToString(), vParam.ToString());

            string tipMoneda = "TIPO DE MONEDA\t: " + ObjDetCatalogo.Descripcion.ToString();
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    float[] tabs = { 102 };
                    string_format.SetTabStops(0, tabs);
                    e.Graphics.DrawString(tipMoneda, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }

            string vVendedor = "SERIE IMPRESORA\t  : "/* + ObjVenta.Vendedor.ToString() + "*/  + ObjSerie.NumSerieImp.ToString();
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    float[] tabs = { 6 };
                    string_format.SetTabStops(0, tabs);
                    e.Graphics.DrawString(vVendedor, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }
            string tipoventa = ObjVenta.TVenta.ToString();

            //Tipo de pago
            string vTipoPago = "TIPO DE PAGO\t  : " + ObjVenta.TipoPago; // valor en duro
            using (StringFormat string_format = new StringFormat())
            {
                using (Font font = new Font("Arial", 7))
                {
                    // Define the tab stops.
                    float[] tabs = { 96 };
                    string_format.SetTabStops(0, tabs);
                    e.Graphics.DrawString(vTipoPago, letra9, Brushes.Black, new Point(-2, i += 12), string_format);
                }
            }


            //Aca debe ir la numero de serie de la impresora

            Point pointSharilyn1 = new Point(0, (i += 17));
            Point pointSharilyn2 = new Point(300, i);

            e.Graphics.DrawLine(Pens.Black, pointSharilyn1, pointSharilyn2);
            string vCliente = "";
            if (nCliente == "CLIENTES VARIOS" || vCli.Equals("C"))
            {
                string vDoc = "IDENTIFICACIÓN      :";
                e.Graphics.DrawString(vDoc, letra9, Brushes.Black, new Point(-2, i += 2), format1);
            }
            else
            {
                string vDoc = "IDENTIFICACIÓN\t: " + ObjVenta.TipDocCli.ToString() + " - " + ObjVenta.Cliente.ToString();
                using (StringFormat string_format = new StringFormat())
                {
                    using (Font font = new Font("Arial", 7))
                    {
                        // Define the tab stops.
                        float[] tabs = { 102 };
                        string_format.SetTabStops(0, tabs);
                        e.Graphics.DrawString(vDoc, letra9, Brushes.Black, new Point(-2, i += 2), string_format);

                        vTab = tabs.Sum();
                    }
                }
            }
            int nTamCli = 0;
            if (nCliente == "CLIENTES VARIOS")
            {
                vCliente = "RAZÓN/NOMBRE\t    : " + nCliente;
                using (StringFormat string_format = new StringFormat())
                {
                    e.Graphics.DrawString(vCliente, letra9, Brushes.Black, new Point(-2, i += 12), format1);
                }
            }
            else
            {
                vCliente = "RAZÓN/NOMBRE\t    : " + ObjCliente.Nombre.ToString();
                using (StringFormat string_format = new StringFormat())
                {

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
            string vDireccion = "";
            int nTama = 0;

            if (nCliente == "CLIENTES VARIOS")
            {
                vDireccion = "DIRECCIÓN\t: ";

                using (StringFormat string_format = new StringFormat())
                {
                    using (Font font = new Font("Arial", 7))
                    {
                        // Define the tab stops.
                        float[] tabs = { 51 };
                        string_format.SetTabStops(0, tabs);
                        e.Graphics.DrawString(vDireccion, letra9, Brushes.Black, new Point(-2, i += 12), string_format);

                        vTab = tabs.Sum();
                    }
                }
            }
            else
            {
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
                }
                else
                {
                    e.Graphics.DrawString(vDireccion, letra9, Brushes.Black, new Point(-2, i += 12), format1);
                }
            }
            //Imprimiendo linea

            Point point1 = new Point(0, (i += 20));
            Point point2 = new Point(300, i);

            e.Graphics.DrawLine(Pens.Black, point1, point2);

            string Codigo = "Código";
            e.Graphics.DrawString(Codigo, letra4, Brushes.Black, new Point(-3, i += 0), format1);

            string vDesc = "Descripción";
            e.Graphics.DrawString(vDesc, letra4, Brushes.Black, new Point(65, i), format1);

            string vCant = "Cant   ";
            e.Graphics.DrawString(vCant, letra4, Brushes.Black, new Point(165, i), format1);

            string vPrec = " Precio";
            e.Graphics.DrawString(vPrec, letra4, Brushes.Black, new Point(205, i), format1);

            string vTotal = "TOTAL";
            e.Graphics.DrawString(vTotal, letra4, Brushes.Black, new Point(251, i /*-= 5*/), format1);

            Point point3 = new Point(0, (i += 17));
            Point point4 = new Point(300, i);

            e.Graphics.DrawLine(Pens.Black, point3, point4);

            //Se imprime articulos

            DataSet datos = csql.dataset_cadena("Call SpDetVentaBuscar('" + Id.ToString() + "','" + RucEmpresa.ToString() + "','" + CodAlmacen.ToString() + "')");
            string nArticulo = "";
            string vPunit = "";
            string vImporte = "";
            double Net = 0;

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
                    int nTamanio = det.Length;
                    int nTam = 0;
                    n += 1;

                    if (nArticulo.Length > 15)
                    {
                        nTam = nArticulo.Length - 15;

                        e.Graphics.DrawString(fila[0].ToString().Trim(), letra5, Brushes.Black, new Point(3, i += 3), format1);
                        e.Graphics.DrawString(nArticulo.Substring(0, 15), letraJosé, Brushes.Black, new Point(60, i), format1);

                        e.Graphics.DrawString(fila[4].ToString().Trim(), letra5, Brushes.Black, new Point(188, i), format3);
                        e.Graphics.DrawString(vPunit, letra5, Brushes.Black, new Point(237, i), format3);
                        e.Graphics.DrawString(vImporte, letra5, Brushes.Black, new Point(285, i), format3);
                        e.Graphics.DrawString(nArticulo.Substring(15, nTam), letraJosé, Brushes.Black, new Point(60, i += 12), format1);
                    }
                    else
                    {
                        e.Graphics.DrawString(fila[0].ToString().Trim(), letra5, Brushes.Black, new Point(3, i += 3), format1);
                        e.Graphics.DrawString(nArticulo, letraJosé, Brushes.Black, new Point(60, i), format1);
                        e.Graphics.DrawString(fila[4].ToString().Trim(), letra5, Brushes.Black, new Point(188, i), format3);
                        e.Graphics.DrawString(vImporte, letra5, Brushes.Black, new Point(285, i), format3);
                        e.Graphics.DrawString(vPunit, letra5, Brushes.Black, new Point(237, i), format3);
                    }

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

                    Point point5 = new Point(0, (i += 10));
                    Point point6 = new Point(300, i);

                    e.Graphics.DrawLine(Pens.Black, point5, point6);
                }
            }

            //TOTALES
            string opGravada = "";
            string opExonerada = "";
            string opInafecta = "";
            string vIgv = "";
            string vTTotal = "";
            e.Graphics.DrawString("OP. EXONERADA", letra4, Brushes.Black, new Point(0, i += 3), format1);
            Net = double.Parse(ObjVenta.TExonerada.ToString().Equals("") ? "0" : ObjVenta.TExonerada.ToString().Trim());
            if (Net.ToString().Trim().Equals("0"))
                opExonerada = Net.ToString("###,###0.00").Trim();//"0.00";
            else
                opExonerada = Net.ToString("###,####0.00").Trim();
            e.Graphics.DrawString("S/   ", letra4, Brushes.Black, new Point(210, i), format1);
            e.Graphics.DrawString(opExonerada, letra4, Brushes.Black, new Point(284, i), format3);

            e.Graphics.DrawString("OP. INAFECTA", letra4, Brushes.Black, new Point(0, i += 12), format1);
            Net = double.Parse(ObjVenta.TInafecta.ToString().Equals("") ? "0" : ObjVenta.TInafecta.ToString().Trim());
            if (Net.ToString().Trim().Equals("0"))
                opInafecta = Net.ToString("#,##0.00").Trim();//"0.00";
            else
                opInafecta = Net.ToString("##,##0.00").Trim();
            e.Graphics.DrawString("S/   ", letra4, Brushes.Black, new Point(210, i), format1);
            e.Graphics.DrawString(opInafecta, letra4, Brushes.Black, new Point(284, i), format3);

            e.Graphics.DrawString("OP. GRAVADA", letra4, Brushes.Black, new Point(0, i += 12), format1);
            Net = double.Parse(ObjVenta.TBruto.ToString().Equals("") ? "0" : ObjVenta.TBruto.ToString().Trim());
            if (Net.ToString().Trim().Equals("0"))
                opGravada = Net.ToString("#,##0.00").Trim();//"0.00";
            else
                opGravada = Net.ToString("#,##0.00").Trim();
            e.Graphics.DrawString("S/   ", letra4, Brushes.Black, new Point(210, i), format1);
            e.Graphics.DrawString(opGravada, letra4, Brushes.Black, new Point(284, i), format3);

            e.Graphics.DrawString("IGV", letra4, Brushes.Black, new Point(0, i += 12), format1);
            Net = double.Parse(ObjVenta.TIgv.ToString().Equals("") ? "0" : ObjVenta.TIgv.ToString().Trim());
            if (Net.ToString().Trim().Equals("0"))
                vIgv = Net.ToString("#,##0.00").Trim();//"0.00";
            else
                vIgv = Net.ToString("#,##0.00").Trim();
            e.Graphics.DrawString("S/   ", letra4, Brushes.Black, new Point(210, i), format1);
            e.Graphics.DrawString(vIgv, letra4, Brushes.Black, new Point(284, i), format3);

            e.Graphics.DrawString("IMPORTE TOTAL", letra4, Brushes.Black, new Point(0, i += 12), format1);
            Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
            if (Net.ToString().Trim().Equals("0"))
                vTTotal = Net.ToString("##,##0.00").Trim();//"0.00";
            else
                vTTotal = Net.ToString("##,##0.00").Trim();
            e.Graphics.DrawString("S/   ", letra4, Brushes.Black, new Point(210, i), format1);
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
            Point point8 = new Point(300, i);

            e.Graphics.DrawLine(Pens.Black, point7, point8);

            if (ObjDocumento.EnvSunat.Equals("S"))
            {
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

                }
                
                resumenFinal = "Código HASH : " + resumen.ToString();
                e.Graphics.DrawString(resumenFinal, letra9, Brushes.Black, new Point(-2, i += 5), format1);

                string nomdocumento = ObjEmpresa.Ruc + "-" + vIdComprobante;

                string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR", $"{nomdocumento}.jpg");

                if (System.IO.File.Exists(filename))
                {
                    Image img = Image.FromFile(filename);
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
                    image.Save(ruta, System.Drawing.Imaging.ImageFormat.Jpeg);

                    Image img = Image.FromFile(ruta);
                    e.Graphics.DrawImage(img, 100, i += 18, 85, 80);
                    i += 70;
                }

                //*******************************************************
            }

            //empieza impresion nano
            string mensajenano = "GRACIAS POR SU PREFERENCIA";
            e.Graphics.DrawString(mensajenano, letra4, Brushes.Black, new Point(130, i += 15), format2);
                        
            Point point9 = new Point(0, (i += 20));
            Point point10 = new Point(300, i);
            e.Graphics.DrawLine(Pens.Black, point9, point10);
            
        }

        public void ImprimirTicket(string IdVenta, string vEmpresa, string vAlmacen, Boolean isPdf, Boolean isDialog)
        {
            try
            {
                string defaultPrinter = string.Empty;

                Id = IdVenta.ToString();
                RucEmpresa = vEmpresa.ToString();
                CodAlmacen = vAlmacen.ToString();

                if (isPdf)
                {
                    string nomDoc = string.Empty;
                    defaultPrinter = "Microsoft Print to PDF";

                    doc.PrinterSettings.PrinterName = defaultPrinter;

                    if (ObjVenta.BuscarVenta(IdVenta, vEmpresa.ToString(), vAlmacen.ToString()))
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

                doc.PrintPage += new PrintPageEventHandler(Doc_PrintPage);
                doc.PrintController = new StandardPrintController();

                doc.Print();

            }
            finally
            {
                doc.Dispose();
            }
        }

        public void ImprimirNormal(string IdVenta, string vEmpresa, string vAlmacen, Boolean isPdf, Boolean isDialog, string printer)
        {

            string defaultPrinter = string.Empty;


            if (ObjVenta.BuscarVenta(IdVenta, vEmpresa.ToString(), vAlmacen.ToString()))
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

            if (ObjEmpresa.BuscarRuc(vEmpresa.ToString()))
            {

            }
            else
            {
                MessageBox.Show("Error Empresa", "SISTEMA");
                return;
            }

            ReportDocument reporte = new ReportDocument();

            reporte.Load(@"Reportes\RptComprobantes.rpt");
            //reporte.Load(@"..\..\Reportes\RptComprobante.rpt");

            string SQuery1 = "select ve.IdVenta, ve.Fecha, em.Ruc, em.Razon as RazonEmpresa, em.NombreLegal, em.Ubicacion as UbicacionEmpresa, em.Direccion as DirecEmpresa, em.Telefono as TelefEmpresa1, em.Telefono2 as TelefEmpresa2, em.Celular, em.Email, al.CodAlmacen, ";
            string SQuery2 = SQuery1 + " '' as DireccionLocal, (Select s.PrefijoSerie from tblserie s where s.Doc = ve.Doc and s.Serie = ve.Serie) PrefSerie, ";
            string SQuery3 = SQuery2 + "dc.nombre as NombreDoc, ve.Serie, ve.Numero, ve.FecCreacion, (Select dc.Descripcion from tbldetcatalogo dc where dc.CodCatalogo = '001' and CodDetCat = ve.TMoneda) as Moneda, ";
            string SQuery4 = SQuery3 + "ve.Vendedor, (CASE ltrim(rtrim(DATE_FORMAT(ve.FVence,'%d/%m/%Y'))) when '00/00/0000' then '  /  /    ' else ltrim(rtrim(DATE_FORMAT(ve.FVence,'%d/%m/%Y'))) END) as FVence, (select dt.Descripcion from tbldetcatalogo dt where dt.CodDetCat = ve.TVenta and dt.CodCatalogo = '015') as TipVenta, (Select s.NumSerieImp from tblserie s where s.Doc = ve.Doc and s.Serie = ve.Serie) SerieImp, ";
            string SQuery5 = SQuery4 + "(CASE SUBSTR(ve.Cliente, 1, 1) WHEN 'C' THEN '' ELSE (Select dcc.DescCorta from tbldetcatalogo dcc where dcc.CodCatalogo = '018' and dcc.CodDetCat = ve.TipDocCli) END) as TipDocCli, ";
            string SQuery6 = SQuery5 + "cl.TipDoc as TipDocCli, (CASE SUBSTR(ve.Cliente, 1, 1) WHEN 'C' THEN '' ELSE cl.RucDni END) as NumDoc, cl.Nombre, cl.Direccion as DirCliente, ve.TExonerada, ve.TInafecta, ve.TBruto, ve.TIgv, ve.Total, ve.ResumenFirma, dv.Codigo, (Select ar.nombre from tblarticulos ar where ar.CodArt = dv.Codigo AND ar.Almacen = ve.Almacen and ar.RucEmpresa = ve.Empresa) NomArt, ";
            string SQuery7 = SQuery6 + "(select DescCorta from tbldetcatalogo where CodCatalogo = '013' and CodDetCat = dv.Unidad) as Unidad, dv.marca, dv.Cantidad, dv.PVenta, dv.Importe, dv.Igv, IFNULL(dv.DescripServ, '') AS DescripServ, (Select ar.CodInternac from tblarticulos ar where ar.CodArt = dv.Codigo AND ar.Almacen = ve.Almacen and ar.RucEmpresa = ve.Empresa) as Codigo2 from tblventa ve ";
            string SQuery8 = SQuery7 + "INNER JOIN tbldetventa dv on ve.IdVenta = dv.IdVenta and dv.Almacen = ve.Almacen and dv.Empresa = ve.Empresa ";
            string SQuery9 = SQuery8 + "INNER JOIN tblcliente cl on cl.RucDni = ve.Cliente ";
            string SQuery10 = SQuery9 + "INNER JOIN tblempresa em on em.Ruc = ve.Empresa ";
            string SQuery11 = SQuery10 + "INNER JOIN tblalmacen al on al.CodAlmacen = ve.Almacen and al.RucEmpresa = ve.Empresa ";
            string SQuery12 = SQuery11 + "INNER JOIN tbldocumento dc on dc.Codigo = ve.Doc where ve.IdVenta = '" + IdVenta.ToString() + "' AND ve.Almacen = '" + vAlmacen.ToString() + "' and ve.Empresa = '" + vEmpresa.ToString() + "'";

            DataSet datosRep = csql.dataset_cadena(SQuery12);

            //string NomArchivo = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

            /*----------------------------*/

            reporte.SetDataSource(datosRep.Tables[0]);
            string ruta = "";
            string vIdComprobante = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;
            string nomdocumento = ObjEmpresa.Ruc + "-" + vIdComprobante;

            if (ObjDocumento.EnvSunat.Equals("S"))
            {

                string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR", $"{nomdocumento}.jpg");

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

                string carpetaqr = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR");

                if (!(Directory.Exists(carpetaqr)))
                {
                    Directory.CreateDirectory(carpetaqr);
                }


                ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"QR", $"{nomdocumento}.jpg");
                image.Save(ruta, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            string numLetra = ClsConversiones.NumeroALetrasString(ObjVenta.Total.ToString());
            reporte.SetParameterValue("NumLetra", numLetra);
            reporte.SetParameterValue("picturePath", ruta);

            string NombreImpresora = "";
            if (!printer.Equals(""))
            {
                NombreImpresora = printer;
            } else
            {
                
                //Busco la impresora por defecto
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    PrinterSettings a = new PrinterSettings();
                    a.PrinterName = PrinterSettings.InstalledPrinters[i].ToString();
                    if (a.IsDefaultPrinter)
                    {
                        NombreImpresora = PrinterSettings.InstalledPrinters[i].ToString();

                    }
                }
            }

            if (isPdf)
            {

                String NomArchivo = ObjSerie.PrefijoSerie + ObjVenta.Serie + "-" + ObjVenta.Numero;

                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();

                SaveFileDialog sdf = new SaveFileDialog();

                sdf.Filter = "PDF Files |*.pdf";
                sdf.FileName = $"{NomArchivo}.pdf";

                if (sdf.ShowDialog() == DialogResult.OK)
                {
                    CrDiskFileDestinationOptions.DiskFileName = sdf.FileName;

                    CrExportOptions = reporte.ExportOptions;
                    {
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                    }
                    reporte.Export();
                }
            }

            if (!isDialog)
            {
                reporte.PrintOptions.PrinterName = NombreImpresora;//Asigno la impresora
                reporte.PrintToPrinter(1, false, 0, 1);
            }

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