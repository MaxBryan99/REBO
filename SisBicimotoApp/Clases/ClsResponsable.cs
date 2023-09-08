using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsResponsable
    {
        public string Codigo;
        public string Nombre;
        public string Direccion;
        public string Telefono;
        public string UserCreacion;
        public string UserModi;
        public string Almacen;
        public string RucEmpresa;

        public ClsResponsable()
        {
        }

        public ClsResponsable(string Codigo, string Nombre, string Direccion, string Telefono,
                            string UserCreacion, string UserModi, string Almacen, string RucEmpresa)
        {
            this.Codigo = Codigo;
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.Almacen = Almacen;
            this.RucEmpresa = RucEmpresa;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpResponsableCrear('" +
                                            this.Codigo.ToString() + "','" +
                                            this.Almacen.ToString() + "','" +
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

        public Boolean Modificar()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpResponsableActualiza('" +
                                                this.Codigo.ToString() + "','" +
                                                this.Almacen.ToString() + "','" +
                                                this.RucEmpresa.ToString() + "','" +
                                                this.UserModi.ToString() + "')");

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

        public Boolean Eliminar_Resp(string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpResponsableElimina('" + this.Codigo.ToString() + "','" + vAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

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

        public Boolean ValidarResponsable(string vCodigo, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpResponsableBusCod('" + vCodigo.ToString() + "','" + vAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

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

        public Boolean BuscarResponsable(string vCodigo, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpResponsableBusCod('" + vCodigo.ToString().Trim() + "','" + vAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.Direccion = fila[2].ToString();
                    this.Telefono = fila[3].ToString();
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