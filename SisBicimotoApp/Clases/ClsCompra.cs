using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsCompra
    {
        public string IdCompra;
        public string FIngAlmacen;
        public string FIngSistema;
        public string FIngDoc;
        public string Doc;
        public string Serie;
        public string Numero;
        public string SerieGuia;
        public string NumGuia;
        public string NOrdenC;
        public string Proveedor;
        public string TMoneda;
        public double TCambio;
        public string TCompra;
        public int NDias;
        public string FVence;
        public double Flete;
        public double OtrosCargos;
        public string Observaciones;
        public double TBruto;
        public double TDcto;
        public double TIgv;
        public double TPercep;
        public double Total;
        public string TEst;
        public string Est;
        public string Empresa;
        public string Almacen;
        public string UserCreacion;
        public string UserModi;

        public ClsCompra()
        {
        }

        public ClsCompra(string IdCompra, string FIngAlmacen, string FIngSistema, string FIngDoc, string Doc, string Serie,
                            string Numero, string SerieGuia, string NumGuia, string NOrdenC, string Proveedor, string TMoneda,
                            double TCambio, string TCompra, int NDias, string FVence, double Flete, double OtrosCargos, string Observaciones,
                            double TBruto, double TDcto, double TIgv, double TPercep, double Total, string TEst, string Est, string Empresa,
                            string Almacen, string UserCreacion, string UserModi)
        {
            this.IdCompra = IdCompra;
            this.FIngAlmacen = FIngAlmacen;
            this.FIngSistema = FIngSistema;
            this.FIngDoc = FIngDoc;
            this.Doc = Doc;
            this.Serie = Serie;
            this.Numero = Numero;
            this.SerieGuia = SerieGuia;
            this.NumGuia = NumGuia;
            this.NOrdenC = NOrdenC;
            this.Proveedor = Proveedor;
            this.TMoneda = TMoneda;
            this.TCambio = TCambio;
            this.TCompra = TCompra;
            this.NDias = NDias;
            this.FVence = FVence;
            this.Flete = Flete;
            this.OtrosCargos = OtrosCargos;
            this.Observaciones = Observaciones;
            this.TBruto = TBruto;
            this.TDcto = TDcto;
            this.TIgv = TIgv;
            this.TPercep = TPercep;
            this.Total = Total;
            this.TEst = TEst;
            this.Est = Est;
            this.Empresa = Empresa;
            this.Almacen = Almacen;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpCompraCrear('" +
                                                        this.IdCompra.ToString() + "','" +
                                                        this.FIngAlmacen.ToString() + "','" +
                                                        this.FIngSistema.ToString() + "','" +
                                                        this.FIngDoc.ToString() + "','" +
                                                        this.Doc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.Numero.ToString() + "','" +
                                                        this.SerieGuia.ToString() + "','" +
                                                        this.NumGuia.ToString() + "','" +
                                                        this.NOrdenC.ToString() + "','" +
                                                        this.Proveedor.ToString() + "','" +
                                                        this.TMoneda.ToString() + "'," +
                                                        this.TCambio + ",'" +
                                                        this.TCompra + "'," +
                                                        this.NDias + ",'" +
                                                        this.FVence.ToString() + "'," +
                                                        this.Flete + "," +
                                                        this.OtrosCargos + ",'" +
                                                        this.Observaciones.ToString() + "'," +
                                                        this.TBruto + "," +
                                                        this.TDcto + "," +
                                                        this.TIgv + "," +
                                                        this.TPercep + "," +
                                                        this.Total + ",'" +
                                                        this.TEst.ToString() + "','" +
                                                        this.Est.ToString() + "','" +
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

            int resultado = csql.comando_cadena("Call SpCompraActualiza('" +
                                                this.IdCompra.ToString() + "','" +
                                                this.FIngAlmacen.ToString() + "','" +
                                                this.FIngSistema.ToString() + "','" +
                                                this.FIngDoc.ToString() + "','" +
                                                this.Doc.ToString() + "','" +
                                                this.Serie.ToString() + "','" +
                                                this.Numero.ToString() + "','" +
                                                this.SerieGuia.ToString() + "','" +
                                                this.NumGuia.ToString() + "','" +
                                                this.NOrdenC.ToString() + "','" +
                                                this.Proveedor.ToString() + "','" +
                                                this.TMoneda.ToString() + "'," +
                                                this.TCambio + ",'" +
                                                this.TCompra + "'," +
                                                this.NDias + ",'" +
                                                this.FVence.ToString() + "'," +
                                                this.Flete + "," +
                                                this.OtrosCargos + ",'" +
                                                this.Observaciones.ToString() + "'," +
                                                this.TBruto + "," +
                                                this.TDcto + "," +
                                                this.TIgv + "," +
                                                this.TPercep + "," +
                                                this.Total + ",'" +
                                                this.TEst.ToString() + "','" +
                                                this.Est.ToString() + "','" +
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

        public Boolean Eliminar(string vIdCompra, string vAlmacen, string vRucEmpresa, string vUsuario)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpCompraElimina('" + vIdCompra.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vUsuario.ToString() + "')");

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

        public Boolean Anular(string vIdCompra, string vAlmacen, string vRucEmpresa, string vUsuario)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpCompraAnula('" + vIdCompra.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vUsuario.ToString() + "')");

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

        public Boolean VerificaDetalle(string vIdCompra, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;
            string valor = "";

            DataSet datos = csql.dataset_cadena("Call SpCompraVerificaDetalle('" + vIdCompra.ToString() + "','" + vAlmacen.ToString() + "','" + vRucEmpresa.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    valor = fila[0].ToString();
                }
            }

            if (valor.ToString().Equals("1"))
            {
                res = true;
            }

            return res;
        }

        public Boolean BuscarCompra(string vIdCompra, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpCompraBuscar('" + vIdCompra.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.FIngAlmacen = fila[0].ToString();
                    this.FIngSistema = fila[1].ToString();
                    this.FIngDoc = fila[2].ToString();
                    this.Doc = fila[3].ToString();
                    this.Serie = fila[4].ToString();
                    this.Numero = fila[5].ToString();
                    this.SerieGuia = fila[6].ToString();
                    this.NumGuia = fila[7].ToString();
                    this.NOrdenC = fila[8].ToString();
                    this.Proveedor = fila[9].ToString();
                    this.TMoneda = fila[10].ToString();
                    this.TCambio = Double.Parse(fila[11].ToString().Equals("") ? "0" : fila[11].ToString());
                    this.TCompra = fila[12].ToString();
                    this.NDias = Int32.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.FVence = fila[14].ToString();
                    this.Flete = Double.Parse(fila[15].ToString().Equals("") ? "0" : fila[15].ToString());
                    this.OtrosCargos = Double.Parse(fila[16].ToString().Equals("") ? "0" : fila[16].ToString());
                    this.Observaciones = fila[17].ToString();
                    this.TBruto = Double.Parse(fila[18].ToString().Equals("") ? "0" : fila[18].ToString());
                    this.TDcto = Double.Parse(fila[19].ToString().Equals("") ? "0" : fila[19].ToString());
                    this.TIgv = Double.Parse(fila[20].ToString().Equals("") ? "0" : fila[20].ToString());
                    this.TPercep = Double.Parse(fila[21].ToString().Equals("") ? "0" : fila[21].ToString());
                    this.Total = Double.Parse(fila[22].ToString().Equals("") ? "0" : fila[22].ToString());
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean DevolverStock(string vIdCompra, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDevolverStock('" + vIdCompra.ToString() + "','COMPRA','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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