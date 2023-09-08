using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SisBicimotoApp.Lib;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp.Clases
{
    class ClsSalida
    {
        public string Id;
        public string Fecha;
        public string Concepto;
        public string TipDoc;
        public string Serie;
        public string Numero;
        public string Referencia;
        public string Responsable;
        public string Almacen;
        public string Empresa;
        public string UserCreacion;
        public string UserModi;
        public string AfectaSt;
        public string Partida;
        public string Destino;
        public string Transporte;
        public string Chofer;
        public string Licencia;
        public string Vehiculo;
        public ClsSalida()
        {

        }

        public ClsSalida(string Id, string Fecha, string Concepto, string TipDoc, string Serie, string Numero,
                          string Referencia, string Responsable, string Almacen, string Empresa, string UserCreacion,
                          string UserModi, string AfectaSt, string Partida, string Destino, string Transporte, string Chofer,
                          string Licencia, string Vehiculo)
        {
            this.Id = Id;
            this.Fecha = Fecha;
            this.Concepto = Concepto;
            this.TipDoc = TipDoc;
            this.Serie = Serie;
            this.Numero = Numero;
            this.Referencia = Referencia;
            this.Responsable = Responsable;
            this.Almacen = Almacen;
            this.Empresa = Empresa;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.AfectaSt = AfectaSt;
            this.Partida = Partida;
            this.Destino = Destino;
            this.Transporte = Transporte;
            this.Chofer = Chofer;
            this.Licencia = Licencia;
            this.Vehiculo = Vehiculo;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpSalidaCrear('" +
                                                        this.Id.ToString() + "','" +
                                                        this.Fecha.ToString() + "','" +
                                                        this.Concepto.ToString() + "','" +
                                                        this.TipDoc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.Numero.ToString() + "','" +
                                                        this.Referencia.ToString() + "','" +
                                                        this.Responsable.ToString() + "','" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.UserCreacion.ToString() + "','" +
                                                        this.AfectaSt.ToString() + "','" +
                                                        this.Partida.ToString() + "','" +
                                                        this.Destino.ToString() + "')");
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

            int resultado = csql.comando_cadena("Call SpSalidaActualiza('" +
                                                        this.Id.ToString() + "','" +
                                                        this.Fecha.ToString() + "','" +
                                                        this.Concepto.ToString() + "','" +
                                                        this.TipDoc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.Numero.ToString() + "','" +
                                                        this.Referencia.ToString() + "','" +
                                                        this.Responsable.ToString() + "','" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.UserModi.ToString() + "','" +
                                                        this.AfectaSt.ToString() + "','" +
                                                        this.Partida.ToString() + "','" +
                                                        this.Destino.ToString() + "')");


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

        public Boolean BuscarSalida(string vId, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpSalidaBuscar('" + vId.ToString() + "','" + vAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Fecha = fila[0].ToString();
                    this.Concepto = fila[1].ToString();
                    this.TipDoc = fila[2].ToString();
                    this.Serie = fila[3].ToString();
                    this.Numero = fila[4].ToString();
                    this.Referencia = fila[5].ToString();
                    this.Responsable = fila[6].ToString();
                    this.Almacen = fila[7].ToString();
                    this.Empresa = fila[8].ToString();
                    this.AfectaSt = fila[9].ToString();
                    this.Partida = fila[10].ToString();
                    this.Destino = fila[11].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean DevolverStock(string vId, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDevolverStock('" + vId.ToString() + "','SALIDA','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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

        public Boolean Eliminar(string vId, string vAlmacen, string vRucEmpresa, string vUsuario)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpSalidaElimina('" + vId.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vUsuario.ToString() + "')");

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
