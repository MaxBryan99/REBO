using SisBicimotoApp.Lib;
using System;
using System.Data;

namespace SisBicimotoApp.Clases
{
    internal class ClsVenta
    {
        public string IdVenta;
        public string Fecha;
        public string Cliente;
        public string TipDocCli;
        public string Doc;
        public string Serie;
        public string Numero;
        public string TMoneda;
        public string NPedido;
        public double TCambio;
        public string TVenta;
        public int NDias;
        public string FVence;
        public double TBruto;
        public double TExonerada;
        public double TInafecta;
        public double TGratuita;
        public double TIgv;
        public double Total;
        public string TEst;
        public string Est;
        public string Empresa;
        public string Almacen;
        public string Vendedor;
        public string Usuario;
        public string ArchXml;
        public string NomArchXml;
        public string UserCreacion;
        public string UserModi;
        public string FecCreacion;
        public string Egratuita;
        public string TComp;
        public double Dcto;
        public string ArchivoXml;
        public string ResumenFirma;
        public string ValorFirma;
        public string TipoPago;
        public string IdCajaApert;

        public ClsVenta()
        {
        }

        public ClsVenta(string IdVenta, string Fecha, string Cliente, string TipDocCli, string Doc, string Serie, string Numero, string TMoneda,
                        string NPedido, double TCambio, string TVenta, int NDias, string FVence, double TBruto, double TExonerada, double TInafecta,
                        double TGratuita, double TIgv, double Total, string TEst, string Est, string Empresa, string Almacen, string Vendedor,
                        string Usuario, string ArchXml, string NomArchXml, string UserCreacion, string UserModi, string FecCreacion, string Egratuita, string TComp, double Dcto, string ArchivoXml,
                        string ResumenFirma, string ValorFirma,string TipoPago, string IdCajaApert)
        {
            this.IdVenta = IdVenta;
            this.Fecha = Fecha;
            this.Cliente = Cliente;
            this.TipDocCli = TipDocCli;
            this.Doc = Doc;
            this.Serie = Serie;
            this.Numero = Numero;
            this.TMoneda = TMoneda;
            this.NPedido = NPedido;
            this.TCambio = TCambio;
            this.TVenta = TVenta;
            this.NDias = NDias;
            this.FVence = FVence;
            this.TBruto = TBruto;
            this.TExonerada = TExonerada;
            this.TInafecta = TInafecta;
            this.TGratuita = TGratuita;
            this.TIgv = TIgv;
            this.Total = Total;
            this.TEst = TEst;
            this.Est = Est;
            this.Empresa = Empresa;
            this.Almacen = Almacen;
            this.Vendedor = Vendedor;
            this.Usuario = Usuario;
            this.ArchXml = ArchXml;
            this.NomArchXml = NomArchXml;
            this.UserCreacion = UserCreacion;
            this.UserModi = UserModi;
            this.FecCreacion = FecCreacion;
            this.Egratuita = Egratuita;
            this.TComp = TComp;
            this.Dcto = Dcto;
            this.ArchivoXml = ArchivoXml;
            this.ResumenFirma = ResumenFirma;
            this.ValorFirma = ValorFirma;
            this.TipoPago = TipoPago;
            this.IdCajaApert = IdCajaApert;
        }

        public Boolean Crear()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpVentaCrear('" +
                                                        this.IdVenta.ToString() + "','" +
                                                        this.Fecha.ToString() + "','" +
                                                        this.Cliente.ToString() + "','" +
                                                        this.TipDocCli.ToString() + "','" +
                                                        this.Doc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.Numero.ToString() + "','" +
                                                        this.TMoneda.ToString() + "','" +
                                                        this.NPedido.ToString() + "'," +
                                                        this.TCambio + ",'" +
                                                        this.TVenta.ToString() + "'," +
                                                        this.NDias + ",'" +
                                                        this.FVence.ToString() + "'," +
                                                        this.TBruto + "," +
                                                        this.TExonerada + "," +
                                                        this.TInafecta + "," +
                                                        this.TGratuita + "," +
                                                        this.TIgv + "," +
                                                        this.Total + ",'" +
                                                        this.TEst.ToString() + "','" +
                                                        this.Est.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.Vendedor.ToString() + "','" +
                                                        this.Usuario.ToString() + "','" +
                                                        this.UserCreacion.ToString() + "','" +
                                                        this.Egratuita.ToString() + "','" +
                                                        this.TComp.ToString() + "'," +
                                                        this.Dcto + ",'" +
                                                        this.TipoPago.ToString() + "','" + 
                                                        this.IdCajaApert.ToString() + "')");

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

        public Boolean CrearPedidoCliente()
        {
            Boolean res = false;
            int resultado = csql.comando_cadena("Call SpPedidoClienteCrear('" +
                                                        this.IdVenta.ToString() + "','" +
                                                        this.Fecha.ToString() + "','" +
                                                        this.Cliente.ToString() + "','" +
                                                        this.TipDocCli.ToString() + "','" +
                                                        this.Doc.ToString() + "','" +
                                                        this.Serie.ToString() + "','" +
                                                        this.Numero.ToString() + "','" +
                                                        this.TMoneda.ToString() + "','" +
                                                        this.NPedido.ToString() + "'," +
                                                        this.TCambio + ",'" +
                                                        this.TVenta.ToString() + "'," +
                                                        this.NDias + ",'" +
                                                        this.FVence.ToString() + "'," +
                                                        this.TBruto + "," +
                                                        this.TExonerada + "," +
                                                        this.TInafecta + "," +
                                                        this.TGratuita + "," +
                                                        this.TIgv + "," +
                                                        this.Total + ",'" +
                                                        this.TEst.ToString() + "','" +
                                                        this.Est.ToString() + "','" +
                                                        this.Empresa.ToString() + "','" +
                                                        this.Almacen.ToString() + "','" +
                                                        this.Vendedor.ToString() + "','" +
                                                        this.Usuario.ToString() + "','" +
                                                        this.UserCreacion.ToString() + "','" +
                                                        this.Egratuita.ToString() + "','" +
                                                        this.TComp.ToString() + "'," +
                                                        this.Dcto + ")");

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

            int resultado = csql.comando_cadena("Call SpVentaActualiza('" +
                                                this.IdVenta.ToString() + "','" +
                                                this.Fecha.ToString() + "','" +
                                                this.Cliente.ToString() + "','" +
                                                this.TipDocCli.ToString() + "','" +
                                                this.Doc.ToString() + "','" +
                                                this.Serie.ToString() + "','" +
                                                this.Numero.ToString() + "','" +
                                                this.TMoneda.ToString() + "','" +
                                                this.NPedido.ToString() + "'," +
                                                this.TCambio + ",'" +
                                                this.TVenta.ToString() + "'," +
                                                this.NDias + ",'" +
                                                this.FVence.ToString() + "'," +
                                                this.TBruto + "," +
                                                this.TExonerada + "," +
                                                this.TInafecta + "," +
                                                this.TGratuita + "," +
                                                this.TIgv + "," +
                                                this.Total + ",'" +
                                                this.TEst.ToString() + "','" +
                                                this.Est.ToString() + "','" +
                                                this.Empresa.ToString() + "','" +
                                                this.Almacen.ToString() + "','" +
                                                this.Vendedor.ToString() + "','" +
                                                this.Usuario.ToString() + "','" +
                                                this.UserModi.ToString() + "','" +
                                                this.Egratuita.ToString() + "','" +
                                                this.TComp.ToString() + "'," +
                                                this.Dcto + ",'" +
                                                this.TipoPago.ToString() + "','" +
                                                this.IdCajaApert.ToString() + "')");

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

        public Boolean ModificarPedidoCliente()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpPedidoClientedActualiza('" +
                                                this.IdVenta.ToString() + "','" +
                                                this.Fecha.ToString() + "','" +
                                                this.Cliente.ToString() + "','" +
                                                this.TipDocCli.ToString() + "','" +
                                                this.Doc.ToString() + "','" +
                                                this.Serie.ToString() + "','" +
                                                this.Numero.ToString() + "','" +
                                                this.TMoneda.ToString() + "','" +
                                                this.NPedido.ToString() + "'," +
                                                this.TCambio + ",'" +
                                                this.TVenta.ToString() + "'," +
                                                this.NDias + ",'" +
                                                this.FVence.ToString() + "'," +
                                                this.TBruto + "," +
                                                this.TExonerada + "," +
                                                this.TInafecta + "," +
                                                this.TGratuita + "," +
                                                this.TIgv + "," +
                                                this.Total + ",'" +
                                                this.TEst.ToString() + "','" +
                                                this.Est.ToString() + "','" +
                                                this.Empresa.ToString() + "','" +
                                                this.Almacen.ToString() + "','" +
                                                this.Vendedor.ToString() + "','" +
                                                this.Usuario.ToString() + "','" +
                                                this.UserModi.ToString() + "','" +
                                                this.Egratuita.ToString() + "','" +
                                                this.TComp.ToString() + "'," +
                                                this.Dcto + ")");

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

        public Boolean DevolverStock(string vIdVenta, string vAlmacen, string vRucEmpresa)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpDevolverStock('" + vIdVenta.ToString() + "','VENTA','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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

        public Boolean BuscarVenta(string vIdVenta, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpVentaBuscar('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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
                    this.TMoneda = fila[6].ToString();
                    this.NPedido = fila[7].ToString();
                    this.TCambio = Double.Parse(fila[8].ToString().Equals("") ? "0" : fila[8].ToString());
                    this.TVenta = fila[9].ToString();
                    this.NDias = Int32.Parse(fila[10].ToString().Equals("") ? "0" : fila[10].ToString());
                    this.FVence = fila[11].ToString();
                    this.TBruto = Double.Parse(fila[12].ToString().Equals("") ? "0" : fila[12].ToString());
                    this.TIgv = Double.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.Total = Double.Parse(fila[14].ToString().Equals("") ? "0" : fila[14].ToString());
                    //byte[] Bitsdatos = new byte[0];
                    //Bitsdatos = (byte[])fila[15];
                    this.ArchXml = fila[15].ToString();
                    this.NomArchXml = fila[16].ToString();
                    this.FecCreacion = fila[17].ToString();
                    this.Vendedor = fila[18].ToString();
                    this.TExonerada = Double.Parse(fila[19].ToString().Equals("") ? "0" : fila[19].ToString());
                    this.TInafecta = Double.Parse(fila[20].ToString().Equals("") ? "0" : fila[20].ToString());
                    this.TGratuita = Double.Parse(fila[21].ToString().Equals("") ? "0" : fila[21].ToString());
                    this.Egratuita = fila[22].ToString();
                    this.TComp = fila[23].ToString();
                    this.Dcto = Double.Parse(fila[24].ToString().Equals("") ? "0" : fila[24].ToString());
                    this.ArchivoXml = fila[25].ToString();
                    this.ResumenFirma = fila[26].ToString();
                    this.ValorFirma = fila[27].ToString();
                    this.TipoPago = fila[28].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarVentaProforma(string vIdVenta, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpPedidoBuscar('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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
                    this.TMoneda = fila[6].ToString();
                    this.NPedido = fila[7].ToString();
                    this.TCambio = Double.Parse(fila[8].ToString().Equals("") ? "0" : fila[8].ToString());
                    this.TVenta = fila[9].ToString();
                    this.NDias = Int32.Parse(fila[10].ToString().Equals("") ? "0" : fila[10].ToString());
                    this.FVence = fila[11].ToString();
                    this.TBruto = Double.Parse(fila[12].ToString().Equals("") ? "0" : fila[12].ToString());
                    this.TIgv = Double.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.Total = Double.Parse(fila[14].ToString().Equals("") ? "0" : fila[14].ToString());
                    //byte[] Bitsdatos = new byte[0];
                    //Bitsdatos = (byte[])fila[15];
                    this.ArchXml = fila[15].ToString();
                    this.NomArchXml = fila[16].ToString();
                    this.FecCreacion = fila[17].ToString();
                    this.Vendedor = fila[18].ToString();
                    this.TExonerada = Double.Parse(fila[19].ToString().Equals("") ? "0" : fila[19].ToString());
                    this.TInafecta = Double.Parse(fila[20].ToString().Equals("") ? "0" : fila[20].ToString());
                    this.TGratuita = Double.Parse(fila[21].ToString().Equals("") ? "0" : fila[21].ToString());
                    this.Egratuita = fila[22].ToString();
                    this.TComp = fila[23].ToString();
                    this.Dcto = Double.Parse(fila[24].ToString().Equals("") ? "0" : fila[24].ToString());
                    this.ArchivoXml = fila[25].ToString();
                    this.ResumenFirma = fila[26].ToString();
                    this.ValorFirma = fila[27].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarComprobate(string vDoc, string vSerie, string vNumero, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpVentaBusComprobante('" + vDoc.ToString() + "','" + vSerie.ToString() + "','" + vNumero.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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
                    this.TMoneda = fila[6].ToString();
                    this.NPedido = fila[7].ToString();
                    this.TCambio = Double.Parse(fila[8].ToString().Equals("") ? "0" : fila[8].ToString());
                    this.TVenta = fila[9].ToString();
                    this.NDias = Int32.Parse(fila[10].ToString().Equals("") ? "0" : fila[10].ToString());
                    this.FVence = fila[11].ToString();
                    this.TBruto = Double.Parse(fila[12].ToString().Equals("") ? "0" : fila[12].ToString());
                    this.TIgv = Double.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.Total = Double.Parse(fila[14].ToString().Equals("") ? "0" : fila[14].ToString());
                    //byte[] Bitsdatos = new byte[0];
                    //Bitsdatos = (byte[])fila[15];
                    this.ArchXml = fila[15].ToString();
                    this.NomArchXml = fila[16].ToString();
                    this.FecCreacion = fila[17].ToString();
                    this.Vendedor = fila[18].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean BuscarComprobateProforma(string vDoc, string vSerie, string vNumero, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpPedidoBusComprobante('" + vDoc.ToString() + "','" + vSerie.ToString() + "','" + vNumero.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

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
                    this.TMoneda = fila[6].ToString();
                    this.NPedido = fila[7].ToString();
                    this.TCambio = Double.Parse(fila[8].ToString().Equals("") ? "0" : fila[8].ToString());
                    this.TVenta = fila[9].ToString();
                    this.NDias = Int32.Parse(fila[10].ToString().Equals("") ? "0" : fila[10].ToString());
                    this.FVence = fila[11].ToString();
                    this.TBruto = Double.Parse(fila[12].ToString().Equals("") ? "0" : fila[12].ToString());
                    this.TIgv = Double.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.Total = Double.Parse(fila[14].ToString().Equals("") ? "0" : fila[14].ToString());
                    //byte[] Bitsdatos = new byte[0];
                    //Bitsdatos = (byte[])fila[15];
                    this.ArchXml = fila[15].ToString();
                    this.NomArchXml = fila[16].ToString();
                    this.FecCreacion = fila[17].ToString();
                    this.Vendedor = fila[18].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean Eliminar(string vIdVenta, string vAlmacen, string vRucEmpresa, string vUsuario)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpVentaElimina('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vUsuario.ToString() + "')");

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

        public Boolean Anular(string vIdVenta, string vAlmacen, string vRucEmpresa, string vUsuario)
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpVentaAnula('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "','" + vUsuario.ToString() + "')");

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

        public Boolean BuscarVentaData(string vIdVenta, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpVentaBuscarData('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    /*this.Fecha = fila[0].ToString();
                    this.Cliente = fila[1].ToString();
                    this.TipDocCli = fila[2].ToString();
                    this.Doc = fila[3].ToString();
                    this.Serie = fila[4].ToString();
                    this.Numero = fila[5].ToString();
                    this.TMoneda = fila[6].ToString();
                    this.NPedido = fila[7].ToString();
                    this.TCambio = Double.Parse(fila[8].ToString().Equals("") ? "0" : fila[8].ToString());
                    this.TVenta = fila[9].ToString();
                    this.NDias = Int32.Parse(fila[10].ToString().Equals("") ? "0" : fila[10].ToString());
                    this.FVence = fila[11].ToString();
                    this.TBruto = Double.Parse(fila[12].ToString().Equals("") ? "0" : fila[12].ToString());
                    this.TIgv = Double.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.Total = Double.Parse(fila[14].ToString().Equals("") ? "0" : fila[14].ToString());
                    //byte[] Bitsdatos = new byte[0];
                    //Bitsdatos = (byte[])fila[15];
                    this.ArchXml = fila[15].ToString();
                    this.NomArchXml = fila[16].ToString();
                    this.FecCreacion = fila[17].ToString();
                    this.Vendedor = fila[18].ToString();
                    this.TExonerada = Double.Parse(fila[19].ToString().Equals("") ? "0" : fila[19].ToString());
                    this.TInafecta = Double.Parse(fila[20].ToString().Equals("") ? "0" : fila[20].ToString());
                    this.TGratuita = Double.Parse(fila[21].ToString().Equals("") ? "0" : fila[21].ToString());
                    this.Egratuita = fila[22].ToString();
                    this.TComp = fila[23].ToString();
                    this.Dcto = Double.Parse(fila[24].ToString().Equals("") ? "0" : fila[24].ToString());
                    this.ArchivoXml = fila[25].ToString();
                    this.TEst = fila[26].ToString();*/
                    this.Fecha = fila[0].ToString();
                    this.Cliente = fila[1].ToString();
                    this.TipDocCli = fila[2].ToString();
                    this.Doc = fila[3].ToString();
                    this.Serie = fila[4].ToString();
                    this.Numero = fila[5].ToString();
                    this.TMoneda = fila[6].ToString();
                    this.NPedido = fila[7].ToString();
                    this.TCambio = Double.Parse(fila[8].ToString().Equals("") ? "0" : fila[8].ToString());
                    this.TVenta = fila[9].ToString();
                    this.NDias = int.Parse(fila[10].ToString().Equals("") ? "0" : fila[10].ToString());
                    this.FVence = fila[11].ToString();
                    this.TBruto = Double.Parse(fila[12].ToString().Equals("") ? "0" : fila[12].ToString());
                    this.TExonerada = Double.Parse(fila[13].ToString().Equals("") ? "0" : fila[13].ToString());
                    this.TInafecta = Double.Parse(fila[14].ToString().Equals("") ? "0" : fila[14].ToString());
                    this.TGratuita = Double.Parse(fila[15].ToString().Equals("") ? "0" : fila[15].ToString());
                    this.TIgv = Double.Parse(fila[16].ToString().Equals("") ? "0" : fila[16].ToString());
                    this.Total = Double.Parse(fila[17].ToString().Equals("") ? "0" : fila[17].ToString());
                    this.TEst = fila[18].ToString();
                    this.Est = fila[19].ToString();
                    this.Empresa = fila[20].ToString();
                    this.Almacen = fila[21].ToString();
                    this.Vendedor = fila[22].ToString();
                    this.Usuario = fila[23].ToString();
                    this.NomArchXml = fila[24].ToString();
                    this.FecCreacion = fila[25].ToString();
                    this.UserCreacion = fila[26].ToString();
                    this.Egratuita = fila[29].ToString();
                    this.TComp = fila[30].ToString();
                    this.Dcto = Double.Parse(fila[31].ToString().Equals("") ? "0" : fila[3].ToString());
                    this.ArchivoXml = fila[32].ToString();
                    res = true;
                }
            }
            else
            {
                //MessageBox.Show("Cliente no encontrado", "SISTEMA");
            }
            return res;
        }

        public Boolean ActualizarFirma()
        {
            Boolean res = false;

            int resultado = csql.comando_cadena("Call SpVentaActualizaFirma('" +
                                                this.IdVenta.ToString() + "','" +
                                                this.Empresa.ToString() + "','" +
                                                this.Almacen.ToString() + "','" +
                                                this.ResumenFirma.ToString() + "','" +
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

        public Boolean BuscarVentaDatosXml(string vIdVenta, string vRucEmpresa, string vAlmacen)
        {
            Boolean res = false;

            DataSet datos = csql.dataset_cadena("Call SpVentaBuscarDatosXml('" + vIdVenta.ToString() + "','" + vRucEmpresa.ToString() + "','" + vAlmacen.ToString() + "')");

            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.ArchivoXml = fila[0].ToString();
                    this.ResumenFirma = fila[1].ToString();
                    this.ValorFirma = fila[2].ToString();
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