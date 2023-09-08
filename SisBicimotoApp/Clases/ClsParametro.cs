using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsParametro
    {
        public string Id;
        public string Descripcion;
        public string Valor;
        public string UserCreacion;
        public string UserModi;

        public ClsParametro()
        {
        }

        public ClsParametro(string Id, string Descripcion, string Valor,
                            string UserCreacion, string UserModi)
        {
            this.Id = Id;
            this.Descripcion = Descripcion;
            this.Valor = Valor;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean BuscarParametro(string vId)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpParametroBusCod('" + vId.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Id = fila[0].ToString();
                    this.Descripcion = fila[1].ToString();
                    this.Valor = fila[2].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean Modificar()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpParametroActualiza('" +
                                            this.Id.ToString() + "','" +
                                            this.Valor.ToString() + "','" +
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
    }
}