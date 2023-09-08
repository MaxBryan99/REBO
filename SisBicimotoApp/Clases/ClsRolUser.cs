using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsRolUser
    {
        public string IdRol;
        public string IdUsuario;
        public string UserCreacion;
        public string UserModi;

        public ClsRolUser()
        {
        }

        public ClsRolUser(string IdRol, string IdUsuario, string UserCreacion, string UserModi)
        {
            this.IdRol = IdRol;
            this.IdUsuario = IdUsuario;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpRolUserCrear('" +
                                            this.IdRol.ToString() + "','" +
                                            this.IdUsuario.ToString() + "','" +
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

            int resultado = csql.comando_cadena("Call SpRolUserActualiza('" +
                                            this.IdRol.ToString() + "','" +
                                            this.IdUsuario.ToString() + "','" +
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

        public Boolean BuscarRolUser(string vIdUser)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpRolBusUser('" + vIdUser.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.IdRol = fila[0].ToString();
                    this.IdUsuario = fila[1].ToString();
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