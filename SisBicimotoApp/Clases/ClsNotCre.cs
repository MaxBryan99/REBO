using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsNotCre
    {
        public string IdNoCre;
        public string Fecha;
        public string Cliente;
        public string TipDocCli;
        public string Doc;
        public string Serie;
        public string Numero;
        public string TMotivo;
        public string TMoneda;
        public string IdVenta;
        public double TCambio;
        public string Observaciones;
        public double TBruto;
        public double TIgv;
        public double Total;
        public string Art;
        public string Empresa;
        public string Almacen;
        public string ArchXml;
        public string NomArchXml;
        public string UserCreacion;
        public string UserModi;

        public ClsNotCre()
        {
        }

        public ClsNotCre(string IdNoCre, string Fecha, string Cliente, string TipDocCli, string Doc,
                            string Serie, string Numero, string TMotivo, string TMoneda, string IdVenta,
                            double TCambio, string Observaciones, double TBruto, double TIgv,
                            double Total, string Art, string Empresa, string Almacen, string ArchXml,
                            string NomArchXml, string UserCreacion, string UserModi)
        {
            this.IdNoCre = IdNoCre;
            this.Fecha = Fecha;
            this.Cliente = Cliente;
            this.TipDocCli = TipDocCli;
            this.Doc = Doc;
            this.Serie = Serie;
            this.Numero = Numero;
            this.TMotivo = TMotivo;
            this.TMoneda = TMoneda;
            this.IdVenta = IdVenta;
            this.TCambio = TCambio;
            this.Observaciones = Observaciones;
            this.TBruto = TBruto;
            this.TIgv = TIgv;
            this.Total = Total;
            this.Art = Art;
            this.Empresa = Empresa;
            this.Almacen = Almacen;
            this.ArchXml = ArchXml;
            this.NomArchXml = NomArchXml;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpNotaCreCrear('" +
                                                        this.IdNoCre.ToString() + "','" +
                                                        this.Fecha.ToString() + "','" +
                                                        this.Cliente.ToString() + "','" +
                                                        this.TipDocCli.ToString() + "','" +
                                                        this.Doc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.Numero.ToString() + "','" +
                                                        this.TMotivo.ToString() + "','" +
                                                        this.TMoneda.ToString() + "','" +
                                                        this.IdVenta.ToString() + "'," +
                                                        this.TCambio + ",'" +
                                                        this.Observaciones.ToString() + "'," +
                                                        this.TBruto + "," +
                                                        this.TIgv + "," +
                                                        this.Total + ",'" +
                                                        this.Art.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.Almacen.ToString() + "','" +
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

            int resultado = csql.comando_cadena("Call SpNotaCreActualiza('" +
                                                this.IdNoCre.ToString() + "','" +
                                                this.Fecha.ToString() + "','" +
                                                this.Cliente.ToString() + "','" +
                                                this.TipDocCli.ToString() + "','" +
                                                this.Doc.ToString() + "','" +
                                                this.Serie.ToString() + "','" +
                                                this.Numero.ToString() + "','" +
                                                this.TMotivo.ToString() + "','" +
                                                this.TMoneda.ToString() + "','" +
                                                this.IdVenta.ToString() + "'," +
                                                this.TCambio + ",'" +
                                                this.Observaciones.ToString() + "'," +
                                                this.TBruto + "," +
                                                this.TIgv + "," +
                                                this.Total + ",'" +
                                                this.Art.ToString() + "','" +
                                                this.Empresa.ToString() + "','" +
                                                this.Almacen.ToString() + "','" +
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

        public Boolean BuscarNC(string vIdNC, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpNotaCreBuscar('" + vIdNC.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Fecha = fila[0].ToString();
                    this.Cliente = fila[1].ToString();
                    this.TipDocCli = fila[2].ToString();
                    this.Doc = fila[3].ToString();
                    this.Serie = fila[4].ToString();
                    this.Numero = fila[5].ToString();
                    this.TMotivo = fila[6].ToString();
                    this.TMoneda = fila[7].ToString();
                    this.IdVenta = fila[8].ToString();
                    this.TCambio = Double.Parse(fila[9].ToString().Equals("") ? "0" : fila[9].ToString());
                    this.Observaciones = fila[10].ToString();
                    this.TBruto = Double.Parse(fila[11].ToString().Equals("") ? "0" : fila[11].ToString());
                    this.TIgv = Double.Parse(fila[12].ToString().Equals("") ? "0" : fila[12].ToString());
                    this.Total = Double.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.Art = fila[14].ToString();
                    this.ArchXml = fila[16].ToString();
                    this.NomArchXml = fila[17].ToString(); ;
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean Eliminar(string vIdNC, string vAlmacen, string vRucEmpresa, string vUsuario)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpNotaCreElimina('" + vIdNC.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vUsuario.ToString() + "')");

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

        public Boolean Anular(string vIdNC, string vAlmacen, string vRucEmpresa, string vUsuario)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpNotaCreAnula('" + vIdNC.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vUsuario.ToString() + "')");

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