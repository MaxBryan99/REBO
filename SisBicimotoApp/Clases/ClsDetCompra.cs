using SisBicimotoApp.Lib;
using System;

namespace SisBicimotoApp.Clases
{
    internal class ClsDetCompra
    {
        public string IdCompra;
        public string CodArt;
        public string Unidad;
        public double PCosto;
        public double Cantidad;
        public double Igv;
        public double Dcto;
        public double PorDcto;
        public double Percep;
        public double Importe;
        public string Almacen;
        public string Empresa;
        public string UserCreacion;
        public string UserModi;

        public ClsDetCompra()
        {
        }

        public ClsDetCompra(string IdCompra, string CodArt, string Unidad, double PCosto, double Cantidad,
                            double Igv, double Dcto, double PorDcto, double Percep, double Importe,
                            string Almacen, string Empresa, string UserCreacion, string UserModi)
        {
            this.IdCompra = IdCompra;
            this.CodArt = CodArt;
            this.Unidad = Unidad;
            this.PCosto = PCosto;
            this.Cantidad = Cantidad;
            this.Igv = Igv;
            this.Dcto = Dcto;
            this.PorDcto = PorDcto;
            this.Percep = Percep;
            this.Importe = Importe;
            this.Almacen = Almacen;
            this.Empresa = Empresa;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetCompraCrear('" +
                                            this.IdCompra.ToString() + "','" +
                                            this.CodArt.ToString() + "','" +
                                            this.Unidad.ToString() + "'," +
                                            this.PCosto + "," +
                                            this.Cantidad + "," +
                                            this.Igv + "," +
                                            this.Dcto + "," +
                                            this.PorDcto + "," +
                                            this.Percep + "," +
                                            this.Importe + ",'" +
                                            this.Almacen.ToString() + "','" +
                                            this.Empresa.ToString() + "','" +
                                            this.UserCreacion.ToString() + "')");
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

        public Boolean Eliminar(string vIdCompra, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDetCompraElimina('" + vIdCompra.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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
    }
}