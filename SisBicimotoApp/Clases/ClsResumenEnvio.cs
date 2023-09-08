using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsResumenEnvio
    {
        public int Id { get; set; }
        public string NDocResumen { get; set; }
        public string Fecha { get; set; }
        public string Ticket { get; set; }
        public string CodError { get; set; }
        public string MensajeError { get; set; }
        public string MensajeRespuesta { get; set; }
        public string ArchXml { get; set; }
        public string NomArchXml { get; set; }
        public string Est { get; set; }
        public string RucEmpresa { get; set; }
        public string UserCreacion { get; set; }
        public string UserModi { get; set; }

        public ClsResumenEnvio()
        {
        }

        public ClsResumenEnvio(int Id, string NDocResumen, string Fecha, string Ticket, string CodError,
                                    string MensajeError, string MensajeRespuesta, string ArchXml, string NomArchXml, string Est,
                                    string RucEmpresa, string UserCreacion, string UserModi)
        {
            this.Id = Id;
            this.NDocResumen = NDocResumen;
            this.Fecha = Fecha;
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

            DataSet datos = csql.dataset_cadena("Call SpResumenCont('" + vCod.ToString() + "','" + vRucEmpresa.ToString() + "')");

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
            int resultado = csql.comando_cadena("Call SpResumenCrear(" +
                                                        this.Id.ToString() + ",'" +
                                                        this.NDocResumen.ToString() + "','" +
                                                        this.Fecha.ToString() + "','" +
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

        public Boolean Buscar(string vId, string vResumen, string vRucEmpresa)
        {
            Boolean res = false;

            DataSet datosCom = csql.dataset_cadena("Call SpResumenBuscar('" + vId.ToString() + "','" + vResumen.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datosCom.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datosCom.Tables[0].Rows)
                {
                    this.Fecha = fila[0].ToString();
                    this.Id = int.Parse(fila[1].ToString());
                    this.NDocResumen = fila[2].ToString();
                    this.Ticket = fila[3].ToString();
                    this.CodError = fila[4].ToString();
                    this.MensajeError = fila[5].ToString();
                    this.MensajeRespuesta = fila[6].ToString();
                    this.ArchXml = fila[7].ToString();
                    this.NomArchXml = fila[8].ToString();
                    this.Est = fila[9].ToString();

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