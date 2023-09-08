using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsLinea
    {
        public string Codigo;
        public string CodFamilia;
        public string Descripcion;
        public string UserCreacion;
        public string UserModi;
        public string RucEmpresa;

        public ClsLinea()
        {
        }

        public ClsLinea(string Codigo, string CodFamilia, string Descripcion, string UserCreacion,
                            string UserModi, string RucEmpresa)
        {
            this.Codigo = Codigo;
            this.CodFamilia = CodFamilia;
            this.Descripcion = Descripcion;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.RucEmpresa = RucEmpresa;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpLineaCrear('" +
                                            this.CodFamilia.ToString() + "','" +
                                            this.Descripcion.ToString() + "','" +
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

            int resultado = csql.comando_cadena("Call SpLineaActualiza('" +
                                            this.Codigo.ToString() + "','" +
                                            this.CodFamilia.ToString() + "','" +
                                            this.Descripcion.ToString() + "','" +
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

        public Boolean BuscarLinea(string vCodLinea, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpLineaBusCod('" + vCodLinea.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Codigo = fila[0].ToString();
                    this.Descripcion = fila[1].ToString();
                    this.CodFamilia = fila[2].ToString();
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