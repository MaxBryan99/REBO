using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsUsuario
    {
        public string IdUser;
        public string NomUser;
        public string Nombre;
        public string Apellido;
        public string Dni;
        public string Contraseña;
        public string Est;
        public string UserCreacion;
        public string UserModi;
        public string RucEmpresa;
        public string Serie;

        public ClsUsuario()
        {
        }

        public ClsUsuario(string IdUser, string NomUser, string Nombre, string Apellido, string Dni, string Contraseña,
                          string Est, string UserCreacion, string UserModi, string RucEmpresa, string Serie)
        {
            this.IdUser = IdUser;
            this.NomUser = NomUser;
            this.Nombre = Nombre;
            this.Apellido = Apellido;
            this.Dni = Dni;
            this.Contraseña = Contraseña;
            this.Est = Est;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.RucEmpresa = RucEmpresa;
            this.Serie = Serie;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpUsuarioCrear('" +
                                            this.NomUser.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.Apellido.ToString() + "','" +
                                            this.Dni.ToString() + "','" +
                                            this.Contraseña.ToString() + "','" +
                                            this.UserCreacion.ToString() + "','" +
                                            this.RucEmpresa.ToString() + "','" +
                                            this.Serie.ToString() + "')");

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

            int resultado = csql.comando_cadena("Call SpUsuarioActualiza('" +
                                                this.IdUser.ToString() + "','" +
                                                this.NomUser.ToString() + "','" +
                                                this.Nombre.ToString() + "','" +
                                                this.Apellido.ToString() + "','" +
                                                this.Dni.ToString() + "','" +
                                                this.Contraseña.ToString() + "','" +
                                                this.UserModi.ToString() + "','" +
                                                this.RucEmpresa.ToString() + "','" +
                                                this.Serie.ToString() + "')");

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

        public Boolean BuscarUsuario(string vIdUser, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUsuarioBusCod('" + vIdUser.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.IdUser = fila[0].ToString();
                    this.Dni = fila[1].ToString();
                    this.Nombre = fila[2].ToString();
                    this.Apellido = fila[3].ToString();
                    this.NomUser = fila[4].ToString();
                    this.Contraseña = fila[5].ToString();
                    this.Est = fila[6].ToString();
                    this.Serie = fila[7].ToString();

                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean ValidaUSer(string NomUser)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUsuarioValUsuario('" + NomUser.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscaUSer(string NomUser)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUsuarioValUsuario('" + NomUser.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.IdUser = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean ValidaDni(string vDni)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpUsuarioValDni('" + vDni.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
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