using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsTransportista
    {
        public string Ruc;
        public string Nombre;
        public string Direccion;
        public string Telefono;
        public string Est;
        public string UserCreacion;
        public string UserModi;
        public string RucEmpresa;

        public ClsTransportista()
        {
        }

        public ClsTransportista(string Ruc, string Nombre, string Direccion, string Telefono, string Est,
                                string UserCreacion, string UserModi, string RucEmpresa)
        {
            this.Ruc = Ruc;
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.Est = Est;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.RucEmpresa = RucEmpresa;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpTransportistaCrear('" +
                                            this.Ruc.ToString() + "','" +
                                            this.Nombre.ToString() + "','" +
                                            this.Direccion.ToString() + "','" +
                                            this.Telefono.ToString() + "','" +
                                            this.Est.ToString() + "','" +
                                            this.UserCreacion.ToString() + "','" +
                                            this.RucEmpresa.ToString() + "')");

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

            int resultado = csql.comando_cadena("Call SpTransportistaActualiza('" +
                                                this.Ruc.ToString() + "','" +
                                                this.Nombre.ToString() + "','" +
                                                this.Direccion.ToString() + "','" +
                                                this.Telefono.ToString() + "','" +
                                                this.Est.ToString() + "','" +
                                                this.UserModi.ToString() + "','" +
                                                this.RucEmpresa.ToString() + "')");

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

        public Boolean ValidarTransportista(string vCodTrans, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpTransportistaBusCod('" + vCodTrans.ToString() + "','" + vRucEmpresa.ToString() + "')");

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

        public Boolean BuscarTransportista(string vCodTransportista, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpTransportistaBusCod('" + vCodTransportista.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Ruc = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.Direccion = fila[2].ToString();
                    this.Telefono = fila[3].ToString();
                    this.Est = fila[4].ToString();
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