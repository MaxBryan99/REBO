using SisBicimotoApp.Lib;
using System;

namespace SisBicimotoApp.Clases
{
    internal class ClsDetNotCre
    {
        public string IdNoCre;
        public string Codigo;
        public string Marca;
        public string Unidad;
        public string Proced;
        public double PVenta;
        public double Cantidad;
        public double Igv;
        public double Importe;
        public string Almacen;
        public string Empresa;
        public string UserCreacion;
        public string UserModi;

        public ClsDetNotCre()
        {
        }

        public ClsDetNotCre(string IdNoCre, string Codigo, string Marca, string Unidad, string Proced, double PVenta,
                            double Cantidad, double Igv, double Importe, string Almacen, string Empresa, string UserCreacion,
                            string UserModi)
        {
            this.IdNoCre = IdNoCre;
            this.Codigo = Codigo;
            this.Marca = Marca;
            this.Unidad = Unidad;
            this.Proced = Proced;
            this.PVenta = PVenta;
            this.Cantidad = Cantidad;
            this.Igv = Igv;
            this.Importe = Importe;
            this.Almacen = Almacen;
            this.Empresa = Empresa;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetNotCreCrear('" +
                                                        this.IdNoCre.ToString() + "','" +
                                                        this.Codigo.ToString() + "','" +
                                                        this.Marca.ToString() + "','" +
                                                        this.Unidad.ToString() + "','" +
                                                        this.Proced.ToString() + "'," +
                                                        this.PVenta + "," +
                                                        this.Cantidad + "," +
                                                        this.Igv + "," +
                                                        this.Importe + ",'" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.Almacen.ToString() + "','" +
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

        public Boolean Eliminar(string vIdNc, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDetNotCreElimina('" + vIdNc.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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