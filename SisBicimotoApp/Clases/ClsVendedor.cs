using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsVendedor
    {
        public string CodVend;
        public string Nombre;
        public string Direccion;
        public string Telefono;
        public string Zona;
        public string NomUser;
        public string Pass;
        public string UserCreacion;
        public string UserModi;
        public string RucEmpresa;

        public ClsVendedor()
        {
        }

        public ClsVendedor(string CodVend, string Nombre, string Direccion, string Telefono, string Zona, string NomUser, string Pass,
                            string UserCreacion, string UserModi, string RucEmpresa)
        {
            this.CodVend = CodVend;
            this.Nombre = Nombre;
            this.Direccion = Direccion;
            this.Telefono = Telefono;
            this.Zona = Zona;
            this.NomUser = NomUser;
            this.Pass = Pass;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.RucEmpresa = RucEmpresa;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpVendedorCrear('" +
                                            this.CodVend.ToString() + "','" +
                                            this.Zona.ToString() + "','" +
                                            this.NomUser.ToString() + "','" +
                                            this.Pass.ToString() + "','" +
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

            int resultado = csql.comando_cadena("Call SpVendedorActualiza('" +
                                                this.CodVend.ToString() + "','" +
                                                this.Zona.ToString() + "','" +
                                                this.NomUser.ToString() + "','" +
                                                this.Pass.ToString() + "','" +
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

        public Boolean Eliminar_Vend(string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpVendedorElimina('" + this.CodVend.ToString() + "','" + vRucEmpresa.ToString() + "')");

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

        public Boolean ValidarVendedor(string vCodVend, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpVendedorBusCod('" + vCodVend.ToString() + "','" + vRucEmpresa.ToString() + "')");

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

        public Boolean BuscarVendedor(string vCodVendedor, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpVendedorBusCod('" + vCodVendedor.ToString().Trim() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CodVend = fila[0].ToString();
                    this.Nombre = fila[1].ToString();
                    this.Direccion = fila[2].ToString();
                    this.Telefono = fila[3].ToString();
                    this.Zona = fila[4].ToString();
                    this.NomUser = fila[5].ToString();
                    this.Pass = fila[6].ToString();
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