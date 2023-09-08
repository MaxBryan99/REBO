using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsRolMenu
    {
        public string IdRol;
        public string IdMenu;
        public string UserCreacion;
        public string UserModi;

        public ClsRolMenu()
        {
        }

        public ClsRolMenu(string IdRol, string IdMenu, string UserCreacion, string UserModi)
        {
            this.IdRol = IdRol;
            this.IdMenu = IdMenu;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpRolMenuCrear('" +
                                            this.IdRol.ToString() + "','" +
                                            this.IdMenu.ToString() + "','" +
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

        public Boolean Eliminar(string vIdRol)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpRolMenuEliminar('" +
                                            vIdRol.ToString() + "')");
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

        public Boolean BuscarMenuValor(string vIdRol, string vIdMenu)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpRolMenuBusRol('" +
                                            vIdRol.ToString() + "','" +
                                            vIdMenu.ToString() + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.IdRol = fila[0].ToString();
                    this.IdMenu = fila[1].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarMenuUser(string vIdUser, string vIdNMenu)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpRolMenuBusUser('" +
                                            vIdUser.ToString() + "','" +
                                            vIdNMenu.ToString() + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                res = true;
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }
    }
}