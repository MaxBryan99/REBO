using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsProductoJefe
    {
        public string CodArt;
        public double PVenta;
        public double PMayorista;
        public double PVolumen;
        public string RucEmpresa;
        public string Almacen;
        public string PromedioVenta;


        public ClsProductoJefe()
        {
        }

        public ClsProductoJefe(string codArt, double pVenta, double pMayorista, double pVolumen, string PromedioVenta, string rucEmpresa, string almacen)
        {
            this.CodArt = codArt;
            this.PVenta = pVenta;
            this.PVolumen = pVolumen;
            this.PMayorista = pMayorista;
            this.RucEmpresa = rucEmpresa;
            this.Almacen = almacen;
        }
        public Boolean ModificarPromedio(string vCodArt, string nPromediventa, string nRucEmpresa, string nAlmacen)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProductoActualizaPromedio('" +
                                                 vCodArt.ToString() + "','" +
                                                 nPromediventa.ToString() + "','" +
                                                 nRucEmpresa.ToString() + "','" +
                                                 nAlmacen.ToString() + "')");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean ModificarJEFE()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpProductoActualizaJEFE('" +
                                                this.CodArt.ToString() + "'," +
                                            this.PVenta + "," +
                                            this.PMayorista + "," +
                                            this.PVolumen + ",'" +
                                            this.PromedioVenta + "','" +
                                            this.RucEmpresa + "','" +
                                            this.Almacen.ToString() + "')");

            if (resultado > 0)
            {
                res = false;
            }
            else
            {
                res = true;
            }
            return res;
        }

        public Boolean ValidarProductoJefe(string vCodProducto, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpProductoBusCod('" + vCodProducto.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }
    }
}