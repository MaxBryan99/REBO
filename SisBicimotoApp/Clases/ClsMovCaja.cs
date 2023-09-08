using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisBicimotoApp.Clases
{
    class ClsMovCaja
    {
        public string Id;
        public string Fecha;
        public string Descripcion;
        public double Monto;
        public string Tipo;
        public int Cort;
        public string UserCreacion;
        public string UserModif;

        public ClsMovCaja()
        {

        }

        public ClsMovCaja(string Id, string Fecha, string Descripcion, double Monto, string Tipo, int Cort, string UserCreacion, string UserModif)
        {
            this.Id = Id;
            this.Fecha = Fecha;
            this.Descripcion = Descripcion;
            this.Monto = Monto;
            this.Tipo = Tipo;
            this.Cort = Cort;
            this.UserCreacion = UserCreacion;
            this.UserModif = UserModif;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpMovCajaCrear('" + this.Id.ToString() + "','" +
                                                                           this.Fecha.ToString() + "','" +
                                                                           this.Descripcion + "'," +
                                                                           this.Monto + ",'" +
                                                                           this.Tipo + "','" +
                                                                           this.UserCreacion + "')");

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

        public Boolean Modificar()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpMovCajaActualiza('" + this.Id.ToString() + "','" +
                                                                           this.Fecha.ToString() + "','" +
                                                                           this.Descripcion + "'," +
                                                                           this.Monto + ",'" +
                                                                           this.Tipo + "','" +
                                                                           this.UserModif + "')");

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

        public Boolean BuscarMovimiento(string vId)
        {
            Boolean res = false;
            DataSet datos = csql.dataset_cadena("Call SpMovCajaBuscar('" + vId.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Id = fila[0].ToString();
                    this.Fecha = fila[1].ToString();
                    this.Tipo = fila[2].ToString();
                    this.Descripcion = fila[3].ToString();
                    this.Monto = Double.Parse(fila[5].ToString());
                    res = true;
                }
            }

            return res;
        }

        public Boolean Eliminar(string vId)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpMovCajaElimina('" + vId.ToString() + "')");

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
