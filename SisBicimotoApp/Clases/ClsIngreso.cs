using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsIngreso
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

        public ClsIngreso()
        {
        }

        public ClsIngreso(string Id, string Fecha, string Concepto, string TipDoc, string Serie, string Numero,
                          string Referencia, string Responsable, string Almacen, string Empresa, string UserCreacion,
                          string UserModi)
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
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpIngresoCrear('" +
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

            int resultado = csql.comando_cadena("Call SpIngresoActualiza('" +
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

        public Boolean BuscarIngreso(string vId, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpIngresoBuscar('" + vId.ToString() + "','" + vAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

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

            int resultado = csql.comando_cadena("Call SpDevolverStock('" + vId.ToString() + "','INGRESO','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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

            int resultado = csql.comando_cadena("Call SpIngresoElimina('" + vId.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vUsuario.ToString() + "')");

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