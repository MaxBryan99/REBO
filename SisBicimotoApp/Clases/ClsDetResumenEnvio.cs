using SisBicimotoApp.Lib;
using System;

namespace SisBicimotoApp.Clases
{
    public class ClsDetResumenEnvio
    {
        /*public int Id;
        public string NDocResumen;
        public int NumId;
        public string TDoc { get; set; }
        public string Serie { get; set; }
        public int Inicio { get; set; }
        public int Fin { get; set; }
        public double Gravadas { get; set; }
        public double Exoneradas { get; set; }
        public double Igv { get; set; }
        public double SumaTotal { get; set; }
        public string RucEmpresa { get; set; }
        public string UserCreacion { get; set; }
        public string UserModi { get; set; }*/

        public int Id;
        public string NDocResumen;
        public int NumId;
        public string TDoc { get; set; }
        public string Serie { get; set; }
        public string NumDoc { get; set; }
        public string IdComp { get; set; }
        public string CodEstado { get; set; }
        public string DocRelacionado { get; set; }
        public string Almacen { get; set; }
        public string RucEmpresa { get; set; }
        public string UserCreacion { get; set; }
        public string UserModi { get; set; }

        public ClsDetResumenEnvio()
        {
        }

        public ClsDetResumenEnvio(int Id, string NDocResumen, int NumId, string TDoc, string Serie, string NumDoc,
                                        string IdComp, string CodEstado, string DocRelacionado, string Almacen,
                                        string RucEmpresa, string UserCreacion, string UserModi)
        {
            this.Id = Id;
            this.NDocResumen = NDocResumen;
            this.NumId = NumId;
            this.TDoc = TDoc;
            this.Serie = Serie;
            this.NumDoc = NumDoc;
            this.IdComp = IdComp;
            this.CodEstado = CodEstado;
            this.DocRelacionado = DocRelacionado;
            this.Almacen = Almacen;
            this.RucEmpresa = RucEmpresa;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetResumenCrear(" +
                                                        this.Id.ToString() + ",'" +
                                                        this.NDocResumen.ToString() + "'," +
                                                        this.NumId.ToString() + ",'" +
                                                        this.TDoc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.NumDoc + "','" +
                                                        this.IdComp + "','" +
                                                        this.CodEstado + "','" +
                                                        this.DocRelacionado + "','" +
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
    }
}