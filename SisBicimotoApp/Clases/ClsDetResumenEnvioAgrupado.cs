using SisBicimotoApp.Lib;
using System;

namespace SisBicimotoApp.Clases
{
    public class ClsDetResumenEnvioAgrupado
    {
        public int Id;
        public string NDocResumen;
        public int NumId;
        public string TDoc { get; set; }
        public string Serie { get; set; }
        public int Inicio { get; set; }
        public int Fin { get; set; }
        public double Gravadas { get; set; }
        public double Exoneradas { get; set; }
        public double Gratuitas { get; set; }
        public double Igv { get; set; }
        public double SumaTotal { get; set; }
        public string Almacen { get; set; }
        public string RucEmpresa { get; set; }
        public string UserCreacion { get; set; }
        public string UserModi { get; set; }

        public string IdComp { get; set; }
        public int EstadoItem { get; set; }

        public ClsDetResumenEnvioAgrupado()
        {
        }

        public ClsDetResumenEnvioAgrupado(int Id, string NDocResumen, int NumId, string TDoc, string Serie, int Inicio,
                                        int Fin, double Gravadas, double Exoneradas, double Gratuitas, double Igv, double SumaTotal,
                                        string Almacen, string RucEmpresa, string UserCreacion, string UserModi, string IdComp, int EstadoItem)
        {
            this.Id = Id;
            this.NDocResumen = NDocResumen;
            this.NumId = NumId;
            this.TDoc = TDoc;
            this.Serie = Serie;
            this.Inicio = Inicio;
            this.Fin = Fin;
            this.Gravadas = Gravadas;
            this.Exoneradas = Exoneradas;
            this.Gratuitas = Gratuitas;
            this.Igv = Igv;
            this.SumaTotal = SumaTotal;
            this.Almacen = Almacen;
            this.RucEmpresa = RucEmpresa;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.IdComp = IdComp;
            this.EstadoItem = EstadoItem;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetResumenAgrupadoCrear(" +
                                                        this.Id.ToString() + ",'" +
                                                        this.NDocResumen.ToString() + "'," +
                                                        this.NumId.ToString() + ",'" +
                                                        this.TDoc.ToString() + "','" +
                                                        this.Serie.ToString() + "'," +
                                                        this.Inicio + "," +
                                                        this.Fin + "," +
                                                        this.Gravadas + "," +
                                                        this.Exoneradas + "," +
                                                        this.Gratuitas + "," +
                                                        this.Igv + "," +
                                                        this.SumaTotal + ",'" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.RucEmpresa.ToString() + "','" +
                                                        this.UserCreacion.ToString() + "','" +
                                                        this.IdComp.ToString() + "'," +
                                                        this.EstadoItem + ")");

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