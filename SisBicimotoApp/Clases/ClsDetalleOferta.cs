using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisBicimotoApp.Clases
{
    internal class ClsDetalleOferta
    {
        public string IdOferta;
        public string Fecha;
        public string Descripcion;
        public string FechaInicio;
        public string FechaFin;
        public double PrecioOferta;
        public int cantidad;
        public string ParentArt;
        public string NombreArtDep;
        public ClsDetalleOferta()
        {
        }

        public ClsDetalleOferta(string IdOferta, string Fecha, string Descripcion, string FechaInicio, string FechaFin,
                                double PrecioOferta, int cantidad, string ParentArt)
        {
            this.IdOferta = IdOferta;
            this.Fecha = Fecha;
            this.Descripcion = Descripcion;
            this.FechaInicio = FechaInicio;
            this.FechaFin = FechaFin;
            this.PrecioOferta = PrecioOferta;
            this.cantidad = cantidad;
            this.ParentArt = ParentArt;
        }

        /* public Boolean BuscarOfertaArticulo(string vIdArticulo, string vFecha, string vRucEmpresa, string vAlmacen)
         {
             Boolean res = false;

             DataSet datos = csql.dataset_cadena("Call SpOfertaArticuloVerifica('" + vIdArticulo.ToString() + "','" + vFecha.ToString() + "','" + vRucEmpresa.ToString() + "', '" + vAlmacen + "')");

             if (datos.Tables[0].Rows.Count > 0)
             {
                 foreach (DataRow fila in datos.Tables[0].Rows)
                 {
                     this.IdOferta = fila[0].ToString();
                     this.Fecha = fila[1].ToString();
                     this.Descripcion = fila[2].ToString();
                     this.FechaInicio = fila[3].ToString();
                     this.FechaFin = fila[4].ToString();
                     this.PrecioOferta = Double.Parse(fila[5].ToString().Equals("") ? "0" : fila[5].ToString());
                     this.cantidad = int.Parse(fila[6].ToString().Equals("") ? "0" : fila[6].ToString());
                     this.ParentArt = fila[7].ToString();
                     this.NombreArtDep = fila[8].ToString();
                     res = true;
                 }
             }
             else
             {
                 //MessageBox.Show("", "SISTEMA");
             }

             return res;
         }*/

    }
}
