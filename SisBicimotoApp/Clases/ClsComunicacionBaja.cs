using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsComunicacionBaja
    {
        public int Id;
        public string NDocBaja;
        public string Fecha;
        public int NumDocs;
        public string Ticket;
        public string CodError;
        public string MensajeError;
        public string MensajeRespuesta;
        public string ArchXml;
        public string NomArchXml;
        public string Est;
        public string RucEmpresa;
        public string UserCreacion;
        public string UserModi;

        public ClsComunicacionBaja()
        {
        }

        public ClsComunicacionBaja(int Id, string NDocBaja, string Fecha, int NumDocs, string Ticket, string CodError,
                                    string MensajeError, string MensajeRespuesta, string ArchXml, string NomArchXml, string Est,
                                    string RucEmpresa, string UserCreacion, string UserModi)
        {
            this.Id = Id;
            this.NDocBaja = NDocBaja;
            this.Fecha = Fecha;
            this.NumDocs = NumDocs;
            this.Ticket = Ticket;
            this.CodError = CodError;
            this.MensajeError = MensajeError;
            this.MensajeRespuesta = MensajeRespuesta;
            this.ArchXml = ArchXml;
            this.NomArchXml = NomArchXml;
            this.Est = Est;
            this.RucEmpresa = RucEmpresa;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public int ValorConteo(string vCod, string vRucEmpresa)
        {
            int nValor = 0;

            DataSet datos = csql.dataset_cadena("Call SpComunicacionBajaCont('" + vCod.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    nValor = int.Parse(fila[0].ToString());
                }
            }
            return nValor;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpComunicacionBajaCrear(" +
                                                        this.Id.ToString() + ",'" +
                                                        this.NDocBaja.ToString() + "','" +
                                                        this.Fecha.ToString() + "','" +
                                                        this.NumDocs.ToString() + "','" +
                                                        this.Est.ToString() + "','" +
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

        public Boolean Buscar(string vId, string vEnvio, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datosCom = csql.dataset_cadena("Call SpComunicacionBajaBuscar('" + vId.ToString() + "','" + vEnvio.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datosCom.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datosCom.Tables[0].Rows)
                {
                    this.Fecha = fila[0].ToString();
                    this.Id = int.Parse(fila[1].ToString());
                    this.NDocBaja = fila[2].ToString();
                    this.NumDocs = int.Parse(fila[3].ToString());
                    this.Ticket = fila[4].ToString();
                    this.CodError = fila[5].ToString();
                    this.MensajeError = fila[6].ToString();
                    this.MensajeRespuesta = fila[7].ToString();
                    this.ArchXml = fila[8].ToString();
                    this.NomArchXml = fila[9].ToString();
                    this.Est = fila[10].ToString();

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