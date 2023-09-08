using SisBicimotoApp.Lib;
using System;

namespace SisBicimotoApp.Clases
{
    internal class ClsDetVenta
    {
        public string IdVenta;
        public string Codigo;
        public string Marca;
        public string Unidad;
        public string Proced;
        public double PVenta;
        public double Cantidad;
        public double Dcto;
        public double Igv;
        public double Importe;
        public string Almacen;
        public string Empresa;
        public string TipPrecio;
        public string TipImpuesto;
        public string UserCreacion;
        public string UserModi;
        public string DescripServ;
        public int Norden;
        public string Est;

        public ClsDetVenta()
        {
        }

        public ClsDetVenta(string IdVenta, string Codigo, string Marca, string Unidad, string Proced, double PVenta, double Cantidad, double Dcto,
                            double Igv, double Importe, string Almacen, string Empresa, string TipPrecio, string TipImpuesto, string UserCreacion, string UserModi, int Norden, string DescripServ, String Est)
        {
            this.IdVenta = IdVenta;
            this.Codigo = Codigo;
            this.Marca = Marca;
            this.Unidad = Unidad;
            this.Proced = Proced;
            this.PVenta = PVenta;
            this.Cantidad = Cantidad;
            this.Dcto = Dcto;
            this.Igv = Igv;
            this.Importe = Importe;
            this.Almacen = Almacen;
            this.Empresa = Empresa;
            this.TipPrecio = TipPrecio;
            this.TipImpuesto = TipImpuesto;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.Norden = Norden;
            this.DescripServ = DescripServ;
            this.Est = Est;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetVentaCrear('" +
                                                        this.IdVenta.ToString() + "','" +
                                                        this.Codigo.ToString() + "','" +
                                                        this.Marca.ToString() + "','" +
                                                        this.Unidad.ToString() + "','" +
                                                        this.Proced.ToString() + "'," +
                                                        this.PVenta + "," +
                                                        this.Cantidad + "," +
                                                        this.Dcto + "," +
                                                        this.Igv + "," +
                                                        this.Importe + ",'" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.TipPrecio.ToString() + "','" +
                                                        this.TipImpuesto.ToString() + "','" +
                                                        this.UserCreacion.ToString() + "'," +
                                                        this.Norden + ",'" +
                                                        this.DescripServ + "','" +
                                                        this.Est + "')");

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

        public Boolean CrearPedidoCliente()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetPedidoClienteCrear('" +
                                                        this.IdVenta.ToString() + "','" +
                                                        this.Codigo.ToString() + "','" +
                                                        this.Marca.ToString() + "','" +
                                                        this.Unidad.ToString() + "','" +
                                                        this.Proced.ToString() + "'," +
                                                        this.PVenta + "," +
                                                        this.Cantidad + "," +
                                                        this.Dcto + "," +
                                                        this.Igv + "," +
                                                        this.Importe + ",'" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.TipPrecio.ToString() + "','" +
                                                        this.TipImpuesto.ToString() + "','" +
                                                        this.UserCreacion.ToString() + "'," +
                                                        this.Norden + ",'" +
                                                        this.DescripServ + "','" +
                                                        this.Est + "')");

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

        public Boolean Eliminar(string vIdVenta, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDetVentaElimina('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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