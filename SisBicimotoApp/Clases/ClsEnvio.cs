using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp.Clases
{
    internal class ClsEnvio
    {
        public string Id;
        public string Fecha;
        public string IdComp;
        public string Doc;
        public string Serie;
        public string Numero;
        public string Ticket;
        public string CodError;
        public string MensajeError;
        public string MensajeRespuesta;
        public string ArchXml;
        public string NomArchXml;
        public string Estado;
        public string Empresa;
        public string Almacen;
        public string UserCreacion;
        public string UserModi;
        public string ServSunat;

        public ClsEnvio()
        {
        }

        public ClsEnvio(string Id, string Fecha, string IdComp, string Doc, string Serie, string Numero, string Ticket,
                        string CodError, string MensajeError, string MensajeRespuesta, string ArchXml, string NomArchXml, string Estado, string Empresa, string Almacen,
                        string UserCreacion, string UserModi, string ServSunat)
        {
            this.Id = Id;
            this.Fecha = Fecha;
            this.IdComp = IdComp;
            this.Doc = Doc;
            this.Serie = Serie;
            this.Numero = Numero;
            this.Ticket = Ticket;
            this.CodError = CodError;
            this.MensajeError = MensajeError;
            this.MensajeRespuesta = MensajeRespuesta;
            this.ArchXml = ArchXml;
            this.NomArchXml = NomArchXml;
            this.Estado = Estado;
            this.Empresa = Empresa;
            this.Almacen = Almacen;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.ServSunat = ServSunat;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpEnvioCrear('" +
                                                        this.Id.ToString() + "','" +
                                                        this.Fecha.ToString() + "','" +
                                                        this.IdComp.ToString() + "','" +
                                                        this.Doc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.Numero.ToString() + "','" +
                                                        this.Ticket.ToString() + "','" +
                                                        this.CodError.ToString() + "','" +
                                                        this.MensajeError.ToString() + "','" +
                                                        this.MensajeRespuesta.ToString() + "','" +
                                                        this.ArchXml.ToString() + "','" +
                                                        this.NomArchXml.ToString() + "','" +
                                                        this.Estado.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.UserCreacion + "','" +
                                                        this.ServSunat.ToString() + "')");
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

        public Boolean BuscarEnvioComp(string vIdComp, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpEnvioBuscar('" + vIdComp.ToString() + "','" + vAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Id = fila[0].ToString();
                    this.Fecha = fila[1].ToString();
                    this.IdComp = fila[2].ToString();
                    this.Doc = fila[3].ToString();
                    this.Serie = fila[4].ToString();
                    this.Numero = fila[5].ToString();
                    this.Ticket = fila[6].ToString();
                    this.CodError = fila[7].ToString();
                    this.MensajeError = fila[8].ToString();
                    this.MensajeRespuesta = fila[9].ToString();
                    this.ArchXml = fila[10].ToString();
                    this.NomArchXml = fila[11].ToString();
                    this.Estado = fila[12].ToString();
                    this.Empresa = fila[13].ToString();
                    this.Almacen = fila[14].ToString();
                    res = true;
                }
            }
            else
            {
                MessageBox.Show("No se encontró datos de envío", "SISTEMA");
            }
            return res;
        }
    }
}