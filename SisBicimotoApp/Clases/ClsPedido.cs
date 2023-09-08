using SisBicimotoApp.Lib;
using System;

namespace SisBicimotoApp.Clases
{
    internal class ClsPedido
    {
        private string Almacen;
        private string Empresa;
        private string Estado;
        private string FecCreacion;
        private string Fecha;
        private string FecModi;
        private string IdPedido;
        private string Numero;
        private string Referencia;
        private string Serie;
        private string TipDoc;
        private string UserCreacion;
        private string UserModi;

        public ClsPedido()
        {
        }

        public ClsPedido(string IdPedido, string fecha, string TipDoc, string Serie, string Numero, string Referencia, string Estado, string Almacen, string Empresa, string FecCreacion,
            string UserCreacion, string FecModi, string UserModi)
        {
            this.IdPedido = IdPedido;
            this.Fecha = fecha;
            this.TipDoc = TipDoc;
            this.Serie = Serie;
            this.Numero = Numero;
            this.Referencia = Referencia;
            this.Estado = Estado;
            this.Almacen = Almacen;
            this.Empresa = Empresa;
            this.FecCreacion = FecCreacion;
            this.UserCreacion = UserCreacion;
            this.FecModi = FecModi;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpPedidoCrear('"
                                                + this.IdPedido.ToString()
                                                + "' , '"
                                                + this.Fecha.ToString()
                                                + "' , '"
                                                + this.TipDoc.ToString()
                                                + "' , '"
                                                + this.Serie.ToString()
                                                + "' , '"
                                                + this.Numero.ToString()
                                                + "' , '"
                                                + this.Referencia.ToString()
                                                + "' , '"
                                                + this.Estado.ToString()
                                                + "' , '"
                                                + this.Almacen.ToString()
                                                + "' , '"
                                                + this.Empresa.ToString()
                                                + "' , '"
                                                + this.FecCreacion.ToString()
                                                + "' , '"
                                                + this.UserCreacion.ToString()
                                                + "' , '"
                                                + this.FecModi.ToString()
                                                + "' , '"
                                                + this.UserModi.ToString()
                                                + "')");

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

            int resultado = csql.comando_cadena("Call SpSalidaActualiza('"
                                                + this.IdPedido.ToString()
                                                + "' , '"
                                                + this.Fecha.ToString()
                                                + "' , '"
                                                + this.TipDoc.ToString()
                                                + "' , '"
                                                + this.Serie.ToString()
                                                + "' , '"
                                                + this.Numero.ToString()
                                                + "' , '"
                                                + this.Referencia.ToString()
                                                + "' , '"
                                                + this.Estado.ToString()
                                                + "' , '"
                                                + this.Almacen.ToString()
                                                + "' , '"
                                                + this.Empresa.ToString()
                                                + "' , '"
                                                + this.FecCreacion.ToString()
                                                + "' , '"
                                                + this.UserCreacion.ToString()
                                                + "' , '"
                                                + this.FecModi.ToString()
                                                + "' , '"
                                                + this.UserModi.ToString()
                                                + "')");

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