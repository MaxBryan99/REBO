using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsAlmacen
    {
        public string CodAlmacen;
        public string Nombre;
        public string RucEmpresa;
        public string Predeterminar;
        public string Est;
        public string UserCreacion;
        public string UserModi;

        public ClsAlmacen()
        {
        }

        public ClsAlmacen(string CodAlmacen, string Nombre, string RucEmpresa, string Predeterminar, string Est,
                          string UserCreacion, string UserModi)
        {
            this.CodAlmacen = CodAlmacen;
            this.Nombre = Nombre;
            this.RucEmpresa = RucEmpresa;
            this.Predeterminar = Predeterminar;
            this.Est = Est;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpAlmacenCrear('" +
                                            this.Nombre.ToString() + "','" +
                                            this.RucEmpresa.ToString() + "','" +
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

        public Boolean BuscarAlmacen(string vCodAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpAlmacenBuscar('" + vCodAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CodAlmacen = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.Predeterminar = fila[2].ToString();

                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }
    }
}