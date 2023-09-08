using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SisBicimotoApp.Lib;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp.Clases
{
    class ClsDetSalida
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
        public string AfectaSt;
        public ClsDetSalida()
        {

        }

        public ClsDetSalida(string Id, string Codigo, string Marca, string Proce, string Unidad, double Cantidad,
                             string Almacen, string Empresa, string UserCreacion, string UserModi, string AfectaSt)
        {

        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetSalidaCrear('" +
                                                        this.Id.ToString() + "','" +
                                                        this.Codigo.ToString() + "','" +
                                                        this.Marca.ToString() + "','" +
                                                        this.Proce.ToString() + "','" +
                                                        this.Unidad.ToString() + "'," +
                                                        this.Cantidad.ToString() + ",'" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.UserCreacion.ToString() + "','" +
                                                        this.AfectaSt.ToString() + "')");

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

            int resultado = csql.comando_cadena("Call SpDetSalidaElimina('" + vId.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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
