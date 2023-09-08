using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace SisBicimotoApp.Clases
{
    public class ClsCreaFormato
    {
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsProducto ObjProducto = new ClsProducto();

        #region Propiedades

        public string RutaArchivo { get; set; }
        public string RutaArchivoDet { get; set; }

        #endregion Propiedades

        //public void generaFormato(string vIdVenta, string vRucEmpresa, string vAlmacen)
        public void generaFormato(string vIdVenta, string vEmpresa, string vAlmacen)
        {
            //Generando carpeta del dia
            DateTime fechaHoy = DateTime.Now;
            string fecha = fechaHoy.ToString("d");
            string fechaAnio = fecha.Substring(6, 4);
            string fechaMes = fecha.Substring(3, 2);
            string fechaDia = fecha.Substring(0, 2);
            string rutafec = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();

            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"FORMATOS", $"{rutafec}");

            if (!(Directory.Exists(carpeta)))
            {
                Directory.CreateDirectory(carpeta);
            }

            if (!ObjVenta.BuscarVenta(vIdVenta, vEmpresa, vAlmacen))
            {
                MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                return;
            }

            //CABECERA
            //Fecha de Emision
            string dia = ObjVenta.Fecha.Substring(0, 2);
            string mes = ObjVenta.Fecha.Substring(3, 2);
            string anio = ObjVenta.Fecha.Substring(6, 4);
            string vFecha = anio.ToString() + "-" + mes.ToString() + "-" + dia.ToString();

            //TIPO DEL DOCUMENTO DEL CLIENTE
            string vParam = "2";
            string vCodCat = "018";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TipDocCli, vParam.ToString());
            string vCodCli = ObjDetCatalogo.CodDetCat;
            //CLIENTE
            if (!ObjCliente.BuscarCLiente(ObjVenta.Cliente, vEmpresa))
            {
                MessageBox.Show("Error no se encontró datos del Receptor, VERIFIQUE!!!", "SISTEMA");
                return;
            }
            //Moneda
            vParam = "2";
            vCodCat = "001";
            string vMoneda = "";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda, vParam.ToString());

            if (!ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjVenta.TMoneda, vParam.ToString()))
            {
                MessageBox.Show("Error no se encontró tipo de moneda, VERIFIQUE!!!", "SISTEMA");
                return;
            }
            if (ObjDetCatalogo.CodDetCat.Equals("001"))
            {
                vMoneda = "PEN";
            }
            else
            {
                vMoneda = "USD";
            }
            string mensaje = "01" + "|" + vFecha.ToString() + "|" + "0" + "|" + vCodCli + "|" + ObjVenta.Cliente + "|" + ObjCliente.Nombre + "|" + vMoneda.ToString() + "|" + "0.00" + "|" + "0.00" + "|" + "0.00" + "|" + ObjVenta.TBruto.ToString("0.00") + "|" + ObjVenta.TInafecta.ToString("0.00") + "|" + ObjVenta.TExonerada.ToString("0.00") + "|" + ObjVenta.TIgv.ToString("0.00") + "|" + "0.00" + "|" + "0.00" + "|" + ObjVenta.Total.ToString("0.00") + "|";

            string codDoc = "";
            string vMod = "VEN";
            string vDoc = "";
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
                    vDoc = "03";
                    break;

                case "014":
                    vDoc = "01";
                    break;
            }

            if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
            {
                MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                return;
            }

            string vSerie = ObjSerie.PrefijoSerie + ObjVenta.Serie;

            string vNomArchivo = "";
            vNomArchivo = vEmpresa.ToString() + "-" + vDoc + "-" + vSerie + "-" + ObjVenta.Numero;
            RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"FORMATOS", $"{rutafec}",
                   $"{vNomArchivo}.CAB");

            File.WriteAllText(RutaArchivo, mensaje);

            //genera archivo de detalles
            //Items
            DataSet datos = csql.dataset_cadena("Call SpDetVentaBuscar('" + vIdVenta.ToString() + "','" + vEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            Decimal nCant = 0;
            Decimal nPu = 0;
            Decimal nIgv = 0;
            Decimal nTotal = 0;
            string mensajeDetalle = "";
            int n = 0;
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    if (!ObjProducto.BuscarProducto(fila[0].ToString(), vEmpresa, vAlmacen))
                    {
                        MessageBox.Show("Error no se encontró el producto " + fila[0].ToString() + ", VERIFIQUE!!!", "SISTEMA");
                        return;
                    }

                    nCant = Decimal.Parse(fila[4].ToString());
                    nPu = Decimal.Parse(fila[5].ToString());
                    nIgv = Decimal.Parse(fila[6].ToString());
                    nTotal = Decimal.Parse(fila[7].ToString());
                    if (n == 0)
                    {
                        mensajeDetalle = "NIU" + "|" + nCant.ToString("0.00") + "|" + fila[0].ToString() + "||" + ObjProducto.Nombre + "|" + nPu.ToString("0.00") + "|" + "0.00" + "|" + nIgv.ToString("0.00") + "|" + "20" + "|" + "0.00" + "|" + "01" + "|" + nTotal.ToString("0.00") + "|" + nTotal.ToString("0.00") + "|" + Environment.NewLine;
                    }
                    else
                    {
                        mensajeDetalle = mensajeDetalle + "NIU" + "|" + nCant.ToString("0.00") + "|" + fila[0].ToString() + "||" + ObjProducto.Nombre + "|" + nPu.ToString("0.00") + "|" + "0.00" + "|" + nIgv.ToString("0.00") + "|" + "20" + "|" + "0.00" + "|" + "01" + "|" + nTotal.ToString("0.00") + "|" + nTotal.ToString("0.00") + "|" + Environment.NewLine;
                    }
                    n += 1;
                }
            }
            string vNomArchivoDet = "";
            vNomArchivoDet = vEmpresa.ToString() + "-" + vDoc + "-" + vSerie + "-" + ObjVenta.Numero;
            RutaArchivoDet = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"FORMATOS", $"{rutafec}",
                   $"{vNomArchivoDet}.DET");

            File.WriteAllText(RutaArchivoDet, mensajeDetalle);
        }
    }
}