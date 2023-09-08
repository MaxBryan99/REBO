using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisBicimotoApp.Clases
{
    internal class ClsCodigoBarras
    {
        public string Codigo;
        public string TipCod;
        public string CodigoBarras;
        public string Empresa;
        public string FecCreacion;
        public string UserCreacion;
        public string FecModi;
        public string UserModi;
        public string TipodeMovimiento;
        public string Cantidad;



        public ClsCodigoBarras()
        {

        }

        public ClsCodigoBarras(string Codigo, string TipCod, string CodigoBarras, string Empresa, string FecCreacion, string UserCreacion, string FecModi, string UserModi, string TipodeMovimiento, string Cantidad)

        {
            this.Codigo = Codigo;
            this.TipCod = TipCod;
            this.CodigoBarras = CodigoBarras;
            this.Empresa = Empresa;
            this.FecCreacion = FecCreacion;
            this.UserCreacion = UserCreacion;
            this.FecModi = FecModi;
            this.UserModi = UserModi;
            this.TipodeMovimiento = TipodeMovimiento;
            this.Cantidad = Cantidad;

        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpCodigoBarrasCrear('" +
                                            this.Codigo.ToString() + "','" +
                                            this.TipCod.ToString() + "','" +
                                            this.Empresa.ToString() + "','" +
                                            this.CodigoBarras.ToString() + "','" +
                                            this.UserCreacion.ToString() + "','" +
                                            this.TipodeMovimiento.ToString() + "')");
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

        public Boolean salida()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpCodigoBarrasSalida('" +
                                            this.Codigo.ToString() + "','" +
                                            this.TipCod.ToString() + "','" +
                                            this.Empresa.ToString() + "','" +
                                            this.CodigoBarras.ToString() + "','" +
                                            this.UserCreacion.ToString() + "','" +
                                            this.Cantidad.ToString() + "','" +
                                            this.TipodeMovimiento.ToString() + "')");
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
