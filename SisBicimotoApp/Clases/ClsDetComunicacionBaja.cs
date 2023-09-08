using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsDetComunicacionBaja
    {
        public int Id;
        public string NDocBaja;
        public int NumId;
        public string TipDoc;
        public string Serie;
        public string NumDoc;
        public string MotivoBaja;
        public string IdVenta;
        public string RucEmpresa;
        public string UserCreacion;
        public string UserModi;

        public ClsDetComunicacionBaja()
        {
        }

        public ClsDetComunicacionBaja(int Id, string NDocBaja, int NumId, string TipDoc, string Serie, string NumDoc,
                                        string MotivoBaja, string IdVenta, string RucEmpresa, string UserCreacion, string UserModi)
        {
            this.Id = Id;
            this.NDocBaja = NDocBaja;
            this.NumId = NumId;
            this.TipDoc = TipDoc;
            this.Serie = Serie;
            this.NumDoc = NumDoc;
            this.MotivoBaja = MotivoBaja;
            this.IdVenta = IdVenta;
            this.RucEmpresa = RucEmpresa;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpDetComunicacionBajaCrear(" +
                                                        this.Id.ToString() + ",'" +
                                                        this.NDocBaja.ToString() + "'," +
                                                        this.NumId.ToString() + ",'" +
                                                        this.TipDoc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.NumDoc.ToString() + "','" +
                                                        this.MotivoBaja.ToString() + "','" +
                                                        this.IdVenta.ToString() + "','" +
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

        public Boolean BuscarVenta(string vIdVenta, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDetComunicacionBajaBuscarVenta('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    res = true;
                }
            }
            else
            {
                res = false;
            }
            return res;
        }

        public Boolean BuscarDetalle(int vId, string vDocEnvio, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpDetComunicacionBajaBuscar('" + vId.ToString() + "','" + vDocEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Id = Int32.Parse(fila[0].ToString());
                    this.NDocBaja = fila[1].ToString();
                    this.NumId = Int32.Parse(fila[2].ToString());
                    this.TipDoc = fila[3].ToString();
                    this.Serie = fila[4].ToString();
                    this.NumDoc = fila[5].ToString();
                    this.MotivoBaja = fila[6].ToString();
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