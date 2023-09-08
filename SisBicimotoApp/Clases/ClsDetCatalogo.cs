using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsDetCatalogo
    {
        public string CodCatalogo;
        public string CodDetCat;
        public string Descripcion;
        public string DescCorta;
        public string UserCreacion;
        public string UserModi;
        public string Est;

        public ClsDetCatalogo()
        {
        }

        public ClsDetCatalogo(string CodCatalogo, string CodDetCat, string Descripcion, string DescCorta, string UserCreacion,
                            string UserModi, string Est)
        {
            this.CodCatalogo = CodCatalogo;
            this.CodDetCat = CodDetCat;
            this.Descripcion = Descripcion;
            this.DescCorta = DescCorta;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.Est = Est;
        }

        public Boolean Crear()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDetCatalogoCrear('" +
                                            this.CodCatalogo.ToString() + "','" +
                                            this.Descripcion.ToString() + "','" +
                                            this.DescCorta.ToString() + "','" +
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

            int resultado = csql.comando_cadena("Call SpDetCatalogoActualiza('" +
                                            this.CodCatalogo.ToString() + "','" +
                                            this.CodDetCat.ToString() + "','" +
                                            this.Descripcion.ToString() + "','" +
                                            this.DescCorta.ToString() + "','" +
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

        public Boolean Eliminar()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDetCatalogoElimina('" + this.CodCatalogo.ToString() + "','" + this.CodDetCat.ToString() + "','" + this.UserModi.ToString() + "')");

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

        public Boolean BuscarDetCatalogoDes(string vCodDetCat, string vDesDetCatalogo, string vParam)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDetCatalogoBusDes('" + vCodDetCat.ToString() + "','" + vDesDetCatalogo.ToString() + "','" + vParam.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CodDetCat = fila[0].ToString();
                    this.Descripcion = fila[1].ToString();
                    this.DescCorta = fila[2].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Catálogo no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarDetCatalogoCod(string vCodDetCat, string vCodDetCatalogo)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDetCatalogoBusCod('" + vCodDetCat.ToString() + "','" + vCodDetCatalogo.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.CodDetCat = fila[0].ToString();
                    this.Descripcion = fila[1].ToString();
                    this.DescCorta = fila[2].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Catálogo no encontrado", "SISTEMA");
            }
            return res;
        }
    }
}