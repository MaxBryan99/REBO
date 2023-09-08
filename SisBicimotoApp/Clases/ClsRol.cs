using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsRol
    {
        public string Codigo;
        public string Nombre;
        public string NCorto;
        public string UserCreacion;
        public string UserModi;
        public string Est;

        public ClsRol()
        {
        }

        public ClsRol(string Codigo, string Nombre, string NCorto, string UserCreacion,
                            string UserModi, string Est)
        {
            this.Codigo = Codigo;
            this.Nombre = Nombre;
            this.NCorto = NCorto;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.Est = Est;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpRolCrear('" +
                                            this.Nombre.ToString() + "','" +
                                            this.NCorto.ToString() + "','" +
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

            int resultado = csql.comando_cadena("Call SpRolActualiza('" +
                                            this.Codigo.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.NCorto.ToString() + "','" +
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

        public Boolean BuscarRol(string vIdRol)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpRolBusCod('" + vIdRol.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.NCorto = fila[2].ToString();
                    this.Est = fila[3].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarRolNom(string vNomRol)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpRolBusNom('" + vNomRol.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.NCorto = fila[2].ToString();
                    this.Est = fila[3].ToString();
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