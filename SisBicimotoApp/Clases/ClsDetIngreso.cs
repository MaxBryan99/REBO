using SisBicimotoApp.Lib;
using System;

namespace SisBicimotoApp.Clases
{
    internal class ClsDetIngreso
    {
        public string Id;
        public string Codigo;
        public string Marca;
        public string Proce;
        public string Unidad;
        public double Cantidad;
        public string Almacen;
        public string Empresa;
        public string UserCreacion;
        public string UserModi;

        public ClsDetIngreso()
        {
        }

        public ClsDetIngreso(string Id, string Codigo, string Marca, string Proce, string Unidad, double Cantidad,
                             string Almacen, string Empresa, string UserCreacion, string UserModi)
        {
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetIngresoCrear('" +
                                                        this.Id.ToString() + "','" +
                                                        this.Codigo.ToString() + "','" +
                                                        this.Marca.ToString() + "','" +
                                                        this.Proce.ToString() + "','" +
                                                        this.Unidad.ToString() + "'," +
                                                        this.Cantidad.ToString() + ",'" +
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

        public Boolean Eliminar(string vId, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDetIngresoElimina('" + vId.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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