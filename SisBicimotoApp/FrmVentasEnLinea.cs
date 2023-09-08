using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmVentasEnLinea : Form, IVenta, IArticulo
    {
        DataSet datos;
        string IdApertura = null;
        public IVenta Opener { get; set; }

        private PrintDocument doc = new PrintDocument();
        private ClsRol ObjRol = new ClsRol();
        private string nomCodUser = FrmLogin.x_codigo_usuario;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomEmpresa = FrmLogin.x_NomEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string nomService = FrmLogin.XServicio;
        private string Almacen = FrmLogin.x_CodAlmacen;
        private string monPred = "";
        public static string x_Serie = "";
        public static Double v_Total = 0;
        public static Double cantOferta = 0;
        public static int totArticulo = 0;
        public static Boolean validaIgv = false;
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsProducto ObjProducto = new ClsProducto();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsDetVenta ObjDetVenta = new ClsDetVenta();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsTipoCambio ObjTipoCambio = new ClsTipoCambio();
        private ClsImprimir ObjImprimir = new ClsImprimir();
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsUsuario ObjUsuario = new ClsUsuario();
        private ClsRolUser ObjRolUser = new ClsRolUser();
        private ClsCreaFormato ObjFormato = new ClsCreaFormato();
        private ClsEnvioRemoto ObjEnvioRemoto = new ClsEnvioRemoto();
        private ClsDetalleOferta ObjDetalleOferta = new ClsDetalleOferta();

        private string Cod = "";

        private string nParam = "";
        public static string detser = "";

        private double SumaTotal = 0;

        public FrmVentasEnLinea()
        {
            InitializeComponent();
        }

        #region IArticulo Members

        public void SelectItem(string codArt)
        {
            textBox2.Text = codArt;
            textBox4.Focus();
        }

        #endregion IArticulo Members

        #region IVenta Members

        public void buttonGuardar()
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Serie", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (Grid1.RowCount == 0)
            {
                MessageBox.Show("Ingrese Artículos", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (Grid1.RowCount == 0)
            {
                MessageBox.Show("Ingrese Artículos", "SISTEMA");
                textBox2.Focus();
                return;
            }

            datos = csql.dataset("select IdCajaApert from tblaperturaturno where Estado = 'A'");
            if (datos.Tables[0].Rows.Count > 0)
            {
                
            }
            else
            {
                MessageBox.Show("Aperture un turno para proceder a la venta", "SISTEMA");
                return;
            }



            v_Total = Double.Parse(label27.Text);

            totArticulo = Grid1.RowCount;
            x_Serie = textBox1.Text;

            //Verificar oferta
            string message = "";
            string fechaActual = DateTime.Now.ToString("G");
            string codArt = "";
            string nombreArt = "";
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                
            }

            FrmCancelarVenta frmCancelarVenta = new FrmCancelarVenta();
            frmCancelarVenta.WindowState = FormWindowState.Normal;
            frmCancelarVenta.OpenerVenta = this;
            frmCancelarVenta.ShowDialog(this);
        }

        public void ImprimirEnviar(string comprobante, string vIdVenta)
        {
            bool docSerieCodMod = ObjDocumento.BuscarDocSerieCodMod(ObjVenta.Doc, ObjVenta.Serie, "VEN");
            bool docEncontrado = ObjDocumento.BuscarDoc(comprobante.ToString());

            if (docSerieCodMod && docEncontrado && ObjDocumento.Imp.Equals("S"))
            {
                ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, Almacen, false, false);
            }
        }


        public void GuardarVenta(string cliente, string tipdoc, string serie, string comprobante, string tipoPago)
        {
            string Usuario = FrmLogin.x_login_usuario;
            //Datos de Cabecera de Venta
            //Generamos codigo de venta
            string vIdVenta = "";
            if (cliente.Equals(""))
            {
                cliente = "C0000000001";
            }

            //Verifica si aplica IGV
            string vAplicaIgv = "";
            nParam = "7";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vAplicaIgv = ObjParametro.Valor;
            }
            else
            {
                vAplicaIgv = "";
            }

            //string vMod = "VEN";
            //string codComp = "";
            int vNumSerie = 0;
            string vNumero = "";

            if (ObjSerie.BuscarDocSerie(comprobante, serie))
            {
                vNumSerie = ObjSerie.Numero;
            }

            vNumSerie = vNumSerie + 1;

            if (ObjSerie.ActualizaCorrela(comprobante, serie))
            {
            }
            else
            {
                return;
            }
            vNumero = vNumSerie.ToString("00000000").Trim();

            vIdVenta = cliente.ToString().Trim() + comprobante.ToString() + serie.ToString() + "-" + vNumero.ToString() + Almacen.ToString() + rucEmpresa.ToString();

            ObjVenta.IdVenta = vIdVenta.ToString();
            string dia = label5.Text.Substring(0, 2);
            string mes = label5.Text.Substring(3, 2);
            string anio = label5.Text.Substring(6, 4);
            ObjVenta.Fecha = anio.ToString() + "/" + mes.ToString() + "/" + dia.ToString();
            ObjVenta.Doc = comprobante.ToString();
            ObjVenta.Serie = serie.ToString();
            ObjVenta.Numero = vNumero.ToString();
            ObjVenta.NPedido = "";
            ObjVenta.Cliente = cliente.ToString();
            ObjVenta.TipDocCli = tipdoc.ToString();
            ObjVenta.TMoneda = monPred.ToString();
            ObjVenta.TCambio = Double.Parse(label36.Text.ToString().Equals("") ? "0" : label36.Text.ToString().Trim());
            ObjVenta.TVenta = "001";
            ObjVenta.NDias = 0;
            string aniofv = "1900";
            string mesfv = "01";
            string diafv = "01";
            ObjVenta.FVence = aniofv.ToString() + "/" + mesfv.ToString() + "/" + diafv.ToString();
            ObjVenta.TEst = "C";
            if (vAplicaIgv.Equals("S"))
            {
                if (!checkBox1.Checked == true)
                {
                    ObjVenta.TComp = "01"; //GRAVADA
                    ObjVenta.TBruto = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                    ObjVenta.TExonerada = 0;
                    ObjVenta.TIgv = Double.Parse(label26.Text.ToString().Equals("") ? "0" : label26.Text.ToString().Trim());
                    ObjVenta.TGratuita = 0;
                    ObjVenta.Egratuita = "0";
                }
                else
                {
                    ObjVenta.TBruto = 0;
                    ObjVenta.TExonerada = 0;
                    ObjVenta.TIgv = 0;
                    ObjVenta.TGratuita = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                    ObjVenta.Egratuita = "1";
                    ObjVenta.TComp = "03"; //GRATUITA
                }
            }
            else
            {
                if (!checkBox1.Checked == true)
                {
                    ObjVenta.TExonerada = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                    ObjVenta.TBruto = 0;
                    ObjVenta.TIgv = 0;
                    ObjVenta.Egratuita = "0";
                    ObjVenta.TComp = "02"; //EXONERADA
                }
                else
                {
                    ObjVenta.TBruto = 0;
                    ObjVenta.TExonerada = 0;
                    ObjVenta.TIgv = 0;
                    ObjVenta.TGratuita = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                    ObjVenta.Egratuita = "1";
                    ObjVenta.TComp = "03"; //GRATUITA
                }
            }

            ObjVenta.Total = Double.Parse(label27.Text.ToString().Equals("") ? "0" : label27.Text.ToString().Trim());
            ObjVenta.Est = "A";
            ObjVenta.Empresa = rucEmpresa.ToString();
            ObjVenta.Almacen = Almacen.ToString();
            ObjVenta.Vendedor = Usuario.ToString();
            ObjVenta.Usuario = Usuario.ToString();
            ObjVenta.UserCreacion = Usuario.ToString();
            ObjVenta.UserModi = Usuario.ToString();
            ObjVenta.TipoPago = tipoPago.ToString();

            string IdCajaApert = "";
            datos = csql.dataset("select IdCajaApert from tblaperturaturno where Estado = 'A'");
            IdCajaApert = datos.Tables[0].Rows[0][0].ToString();
            ObjVenta.IdCajaApert = IdCajaApert;

            /*-------------------------------------------------------------------------*/
            /*Datos Venta Detalle-----------------------------------------------------*/
            int nOrden = 1;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                ObjDetVenta.IdVenta = vIdVenta.ToString();
                ObjDetVenta.Codigo = row.Cells[0].Value.ToString();
                ObjDetVenta.Marca = row.Cells[2].Value.ToString();
                ObjDetVenta.Unidad = row.Cells[3].Value.ToString();
                ObjDetVenta.Proced = row.Cells[4].Value.ToString();
                ObjDetVenta.PVenta = Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());
                ObjDetVenta.Cantidad = Int32.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString());
                ObjDetVenta.Igv = Double.Parse(row.Cells[7].Value.ToString().Equals("") ? "0" : row.Cells[7].Value.ToString());
                ObjDetVenta.Importe = Double.Parse(row.Cells[8].Value.ToString().Equals("") ? "0" : row.Cells[8].Value.ToString());
                ObjDetVenta.Almacen = Almacen.ToString();
                ObjDetVenta.Empresa = rucEmpresa.ToString();
                ObjDetVenta.TipPrecio = row.Cells[12].Value.ToString();

                if (checkBox1.Checked == true)
                {
                    ObjDetVenta.TipImpuesto = "11";
                }
                else
                {
                    ObjDetVenta.TipImpuesto = row.Cells[13].Value.ToString();
                }

                ObjDetVenta.UserCreacion = Usuario.ToString();
                ObjDetVenta.UserModi = Usuario.ToString();
                ObjDetVenta.Norden = nOrden;
                ObjDetVenta.DescripServ = row.Cells[14].Value.ToString();
                ObjDetVenta.Est = "A";
                nOrden += 1;
                if (ObjDetVenta.Crear())
                {
                }
                else
                {
                    MessageBox.Show("No se registro correctamente el detalle de la compra", "SISTEMA");
                    return;
                }
            }

            /*-------------------------------------------------------------------------*/

            if (ObjVenta.Crear())
            {
                ImprimirEnviar(comprobante, vIdVenta);

                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    string codigo = "";
                    string codigoBarras = "";
                    int limite;

                    if (row.Cells["Barras"].Value == null)
                    {
                        continue;
                    }


                    // Codigo de barras actualiza
                    codigo = row.Cells["Código"].Value.ToString();
                    codigoBarras = row.Cells["Barras"].Value.ToString();
                    limite = Convert.ToInt32(row.Cells["CANTIDAD"].Value);
                    csql.dataset_cadena("Call SpActCodBarras('" + codigo + "','" + codigoBarras + "','" + limite + "')");

                }

                if (ObjDocumento.EnvSunat.Equals("S"))
                {
                    ObjGrabaXML.generaXMLFactura(ObjVenta.IdVenta, rucEmpresa, Almacen, true); //Se cambia a False

                }

                MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");

                textBox2.Clear();
                textBox3.Clear();
                Grid1.Rows.Clear();
                textBox4.Text = "";
                textBox4.Enabled = false;
                label22.Text = "";
                label26.Text = "";
                label27.Text = "";
                checkBox1.Checked = false;
                label28.Text = "Total documentos: ";
                textBox2.Focus();
                label43.Text = "";
                textBox6.Clear();
                this.Dispose();
                FrmVentas.nmVen = 'N';
                FrmVentasEnLinea childForm = new FrmVentasEnLinea();
                childForm.WindowState = FormWindowState.Normal;
            }
            else
            {
                MessageBox.Show("No se registro correctamente", "SISTEMA");
            }
        }

        public void GuardarProforma(string cliente, string tipdoc, string serie, string comprobante)
        {
            string Usuario = FrmLogin.x_login_usuario;
            //Datos de Cabecera de Venta
            //Generamos codigo de venta
            string vIdVenta = "";
            if (cliente.Equals(""))
            {
                cliente = "C0000000001";
            }

            //Verifica si aplica IGV
            string vAplicaIgv = "";
            nParam = "7";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vAplicaIgv = ObjParametro.Valor;
            }
            else
            {
                vAplicaIgv = "";
            }

            //string vMod = "VEN";
            //string codComp = "";
            int vNumSerie = 0;
            string vNumero = "";
            
            if (ObjSerie.BuscarDocSerie(comprobante, serie))
            {
                vNumSerie = ObjSerie.Numero;
            }

            vNumSerie = vNumSerie + 1;

            if (ObjSerie.ActualizaCorrela(comprobante, serie))
            {
            }
            else
            {
                return;
            }
            vNumero = vNumSerie.ToString("00000000").Trim();

            vIdVenta = cliente.ToString().Trim() + comprobante.ToString() + serie.ToString() + "-" + vNumero.ToString() + Almacen.ToString() + rucEmpresa.ToString();

            ObjVenta.IdVenta = vIdVenta.ToString();
            string dia = label5.Text.Substring(0, 2);
            string mes = label5.Text.Substring(3, 2);
            string anio = label5.Text.Substring(6, 4);
            ObjVenta.Fecha = anio.ToString() + "/" + mes.ToString() + "/" + dia.ToString();
            ObjVenta.Doc = comprobante.ToString();
            ObjVenta.Serie = serie.ToString();
            ObjVenta.Numero = vNumero.ToString();
            ObjVenta.NPedido = "";
            ObjVenta.Cliente = cliente.ToString();
            ObjVenta.TipDocCli = tipdoc.ToString();
            ObjVenta.TMoneda = monPred.ToString();
            ObjVenta.TCambio = Double.Parse(label36.Text.ToString().Equals("") ? "0" : label36.Text.ToString().Trim());
            ObjVenta.TVenta = "001";
            ObjVenta.NDias = 0; ObjVenta.FVence = ""; ObjVenta.TEst = "C";
            if (vAplicaIgv.Equals("S"))
            {
                if (!checkBox1.Checked == true)
                {
                    ObjVenta.TComp = "01"; //GRAVADA
                    ObjVenta.TBruto = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                    ObjVenta.TExonerada = 0;
                    ObjVenta.TIgv = Double.Parse(label26.Text.ToString().Equals("") ? "0" : label26.Text.ToString().Trim());
                    ObjVenta.TGratuita = 0;
                    ObjVenta.Egratuita = "0";
                }
                else
                {
                    ObjVenta.TBruto = 0;
                    ObjVenta.TExonerada = 0;
                    ObjVenta.TIgv = 0;
                    ObjVenta.TGratuita = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                    ObjVenta.Egratuita = "1";
                    ObjVenta.TComp = "03"; //GRATUITA
                }
            }
            else
            {
                if (!checkBox1.Checked == true)
                {
                    ObjVenta.TExonerada = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                    ObjVenta.TBruto = 0;
                    ObjVenta.TIgv = 0;
                    ObjVenta.Egratuita = "0";
                    ObjVenta.TComp = "02"; //EXONERADA
                }
                else
                {
                    ObjVenta.TBruto = 0;
                    ObjVenta.TExonerada = 0;
                    ObjVenta.TIgv = 0;
                    ObjVenta.TGratuita = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                    ObjVenta.Egratuita = "1";
                    ObjVenta.TComp = "03"; //GRATUITA
                }
            }

            ObjVenta.Total = Double.Parse(label27.Text.ToString().Equals("") ? "0" : label27.Text.ToString().Trim());
            ObjVenta.Est = "A";
            ObjVenta.Empresa = rucEmpresa.ToString();
            ObjVenta.Almacen = Almacen.ToString();
            ObjVenta.Vendedor = Usuario.ToString();
            ObjVenta.Usuario = Usuario.ToString();
            ObjVenta.UserCreacion = Usuario.ToString();
            ObjVenta.UserModi = Usuario.ToString();
            /*-------------------------------------------------------------------------*/
            /*Datos Venta Detalle-----------------------------------------------------*/
            int nOrden = 1;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                ObjDetVenta.IdVenta = vIdVenta.ToString();
                ObjDetVenta.Codigo = row.Cells[0].Value.ToString();
                ObjDetVenta.Marca = row.Cells[9].Value.ToString();
                ObjDetVenta.Unidad = row.Cells[10].Value.ToString();
                ObjDetVenta.Proced = row.Cells[11].Value.ToString();
                ObjDetVenta.PVenta = Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());
                ObjDetVenta.Cantidad = Int32.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString());
                ObjDetVenta.Igv = Double.Parse(row.Cells[7].Value.ToString().Equals("") ? "0" : row.Cells[7].Value.ToString());
                ObjDetVenta.Importe = Double.Parse(row.Cells[8].Value.ToString().Equals("") ? "0" : row.Cells[8].Value.ToString());
                ObjDetVenta.Almacen = Almacen.ToString();
                ObjDetVenta.Empresa = rucEmpresa.ToString();
                ObjDetVenta.TipPrecio = row.Cells[12].Value.ToString();
                if (checkBox1.Checked == true)
                {
                    ObjDetVenta.TipImpuesto = "11";
                }
                else
                {
                    ObjDetVenta.TipImpuesto = row.Cells[13].Value.ToString();
                }

                ObjDetVenta.UserCreacion = Usuario.ToString();
                ObjDetVenta.UserModi = Usuario.ToString();
                ObjDetVenta.Norden = nOrden;
                ObjDetVenta.DescripServ = row.Cells[14].Value.ToString();
                ObjDetVenta.Est = "A";
                nOrden += 1;
                if (ObjDetVenta.CrearPedidoCliente())
                {
                }
                else
                {
                    MessageBox.Show("No se registro correctamente el detalle de la compra", "SISTEMA");
                    return;
                }
            }

            /*-------------------------------------------------------------------------*/

            if (ObjVenta.CrearPedidoCliente())
            {
                string vMod = "VEN";
                if (ObjDocumento.BuscarDocSerieCodMod(ObjVenta.Doc, ObjVenta.Serie, vMod))
                {
                    
                }

               

                MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");

                textBox2.Clear();
                textBox3.Clear();
                Grid1.Rows.Clear();
                textBox4.Text = "";
                textBox4.Enabled = false;
                label22.Text = "";
                label26.Text = "";
                label27.Text = "";
                checkBox1.Checked = false;
                label28.Text = "Total documentos: ";
                textBox2.Focus();
                label42.Text = "";
                textBox6.Clear();
            }
            else
            {
                MessageBox.Show("No se registro correctamente", "SISTEMA");
            }
        }

        public void CargarConsulta(string validaAnulaElimina)
        {
        }

        public void validarVenta(string vVal)
        {
            if (vVal.ToString().Equals("V"))
            {
                FrmCancelarVenta frmCancelarVenta = new FrmCancelarVenta();
                frmCancelarVenta.WindowState = FormWindowState.Normal;
                frmCancelarVenta.OpenerVenta = this;
                frmCancelarVenta.ShowDialog(this);
            }
        }

        public void validarPrecio(string vVal)
        {
            if (vVal.ToString().Equals("S"))
            {
                textBox4.Enabled = true;
                textBox4.SelectionLength = textBox4.TextLength;
                textBox4.Focus();
            }
        }

        #endregion IVenta Members

        private void BusProducto(string vProducto, string vRucEmpresa)
        {
            if (ObjProducto.BuscarProductoActivo(vProducto, vRucEmpresa, Almacen))
            {
                label11.Text = ObjProducto.Nombre.ToString().Trim();
                label18.Text = ObjProducto.CodUnidad.ToString().Trim();
                label16.Text = ObjProducto.CodMarca.ToString().Trim();
                label12.Text = ObjProducto.CodProced.ToString().Trim();
                lblcod.Text = ObjProducto.CodBarras.ToString().Trim();

                if (ObjProducto.GenStock.ToString().Equals("S") || ObjProducto.GenStock.ToString().Equals(""))
                {
                    label34.Text = "S";
                }
                else
                {
                    label34.Text = "N";
                }

                if (ObjProducto.TipProducto.ToString().Equals("PRODUCTO"))
                {
                    label33.Visible = false;
                    textBox5.Enabled = false;
                    //textBox4.Enabled = true;
                }
                else
                {
                    label33.Visible = true;
                    label33.Text = "Producto Servicio";
                    textBox5.Enabled = true;
                    textBox4.Enabled = true;
                }

                int caseSwitch = comboBox1.SelectedIndex;
                switch (caseSwitch)
                {
                    case 0:
                        textBox4.Text = ObjProducto.PVenta.ToString();
                        break;

                    case 1:
                        textBox4.Text = ObjProducto.PMayorista.ToString();
                        break;

                    case 2:
                        textBox4.Text = ObjProducto.PVolumen.ToString();
                        break;

                    case 3:
                        textBox4.Text = ObjProducto.PVenta.ToString();
                        break;

                    default:
                        textBox4.Text = "";
                        break;
                }

                double Net = 0;
                Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox4.Text = "";
                else
                    textBox4.Text = Net.ToString("###,##0.00").Trim();

                //Buscar Stock
                if (ObjProducto.BuscarStock(vProducto, vRucEmpresa, Almacen))
                {
                    label13.Text = ObjProducto.Stock.ToString();
                }
                else
                {
                    label13.Text = "";
                }

                string nParam = "23";
                //Verificar Moneda Predertimanada
                if (ObjParametro.BuscarParametro(nParam))
                {
                    if (ObjParametro.Valor.Equals("S"))
                    {
                        textBox4.Enabled = true;
                    }
                    else
                    {
                        textBox4.Enabled = false;
                    }
                }

                CalcularImportes();

                //Buscar Ofertas
                string fechaActual = DateTime.Now.ToString("G");
                textBox4.Focus();
            }
            else
            {
                label11.Text = "";
                label18.Text = "";
                label16.Text = "";
                label12.Text = "";
                label13.Text = "";
                textBox5.Text = "";
                textBox3.Text = "";
                textBox5.Enabled = false;
                lblMensajeOferta.Visible = false;
                lblMensajeOferta.Text = "";
            }
        }

        private void BusProductoPorCodBarras(string vRucEmpresa, string vCodBarras)
        {
            if (ObjProducto.BuscarProductoPorCodBarrasActivo(vRucEmpresa, Almacen, vCodBarras))
            {
                label11.Text = ObjProducto.Nombre.ToString().Trim();
                label18.Text = ObjProducto.CodUnidad.ToString().Trim();
                label16.Text = ObjProducto.CodMarca.ToString().Trim();
                label12.Text = ObjProducto.CodProced.ToString().Trim();
                lblcod.Text = ObjProducto.CodArt.ToString().Trim();

                if (ObjProducto.GenStock.ToString().Equals("S") || ObjProducto.GenStock.ToString().Equals(""))
                {
                    label34.Text = "S";
                }
                else
                {
                    label34.Text = "N";
                }

                if (ObjProducto.TipProducto.ToString().Equals("PRODUCTO"))
                {
                    label33.Visible = false;
                    textBox5.Enabled = false;
                    //textBox4.Enabled = true;
                }
                else
                {
                    label33.Visible = true;
                    label33.Text = "Producto Servicio";
                    textBox5.Enabled = true;
                    textBox4.Enabled = true;
                }

                int caseSwitch = comboBox1.SelectedIndex;
                switch (caseSwitch)
                {
                    case 0:
                        textBox4.Text = ObjProducto.PVenta.ToString();
                        break;

                    case 1:
                        textBox4.Text = ObjProducto.PMayorista.ToString();
                        break;

                    case 2:
                        textBox4.Text = ObjProducto.PVolumen.ToString();
                        break;

                    case 3:
                        textBox4.Text = ObjProducto.PVenta.ToString();
                        break;

                    default:
                        textBox4.Text = "";
                        break;
                }

                double Net = 0;
                Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox4.Text = "";
                else
                    textBox4.Text = Net.ToString("###,##0.00").Trim();

                string nParam = "23";
                //Verificar Moneda Predertimanada
                if (ObjParametro.BuscarParametro(nParam))
                {
                    if (ObjParametro.Valor.Equals("S"))
                    {
                        textBox4.Enabled = true;
                    }
                    else
                    {
                        textBox4.Enabled = false;
                    }
                }

                //Buscar Ofertas
                string fechaActual = DateTime.Now.ToString("G");
                textBox4.Focus();
            }
            else
            {
                label11.Text = "";
                label18.Text = "";
                label16.Text = "";
                label12.Text = "";
                label13.Text = "";
                textBox5.Text = "";
                textBox3.Text = "";
                textBox5.Enabled = false;
                lblMensajeOferta.Visible = false;
                lblMensajeOferta.Text = "";
            }
        }

        private void CalcularImportes()
        {

            double vPventa = 0;
            double vCantidad = 0;
            double vImporte = 0;
            vPventa = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
            vCantidad = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());

            vImporte = (vPventa * vCantidad);
            double valImp = Double.Parse(vImporte.ToString("N1").Trim());
            if (vImporte.ToString().Trim().Equals("0"))
                label30.Text = "";
            else
                label30.Text = valImp.ToString("###,##0.00").Trim();
        }

        private void CalcularImportesBarras()
        {
            double vPventa = 0;
            double vCantidad = 0;
            double vImporte = 0;
            vPventa = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
            vCantidad = 1;
            double valImp = 0;


            string codigobarras = textBox2.Text;

            Boolean sehizo = false;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells["Barras"].Value.ToString() == codigobarras)
                {
                    double cantidadgrilla = double.Parse(row.Cells["CANTIDAD"].Value.ToString());
                    double cantidadtotal = vCantidad + cantidadgrilla;
                    vImporte = (vPventa * cantidadtotal);
                    valImp = Double.Parse(vImporte.ToString("N1").Trim());
                    if (vImporte.ToString().Trim().Equals("0"))
                        label30.Text = "";
                    else
                        label30.Text = valImp.ToString("###,##0.00").Trim();
                    sehizo = true;
                    break;
                }
            }

            if (sehizo == false)
            {
                //vImporte = Math.Round((vPventa * vCantidad), 1);
                vImporte = (vPventa * vCantidad);
                valImp = Double.Parse(vImporte.ToString("N1").Trim());
                if (vImporte.ToString().Trim().Equals("0"))
                    label30.Text = "";
                else
                    label30.Text = valImp.ToString("###,##0.00").Trim();
            }


        }

        private void calculaTotales()
        {
            string vAplicaIgv = "";
            //Totales
            double sumaTotal = 0;
            double sumaIgv = 0;
            double sumaBruto = 0;

            //Obtener si aplica IGV
            nParam = "7";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vAplicaIgv = ObjParametro.Valor;
            }
            else
            {
                vAplicaIgv = "";
            }

            foreach (DataGridViewRow row in Grid1.Rows)
            {
                sumaIgv += Double.Parse(row.Cells[7].Value.ToString().Equals("") ? "0" : row.Cells[7].Value.ToString());
                sumaTotal += Double.Parse(row.Cells[8].Value.ToString().Equals("") ? "0" : row.Cells[8].Value.ToString());
            }

            double valIgv = 0;
            if (vAplicaIgv.Equals("S"))
            {
                //Obtener IGV
                string nParam = "3";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    valIgv = Double.Parse(ObjParametro.Valor);
                }
                else
                {
                    valIgv = 0;
                }

                string nParam1 = "11";
                string vTipAfecta = "";
                double valIgvCal = 0;
                double vSubTotal = 0;
                if (ObjParametro.BuscarParametro(nParam1))
                {
                    vTipAfecta = ObjParametro.Valor;
                }
                else
                {
                    MessageBox.Show("Error no se puede aplicar IGV verifique configuración de parámetros", "SISTEMA");
                    return;
                }
                if (checkBox1.Checked == true)
                {
                    label26.Text = sumaIgv.ToString("###,##0.00").Trim();
                    label27.Text = sumaTotal.ToString("###,##0.00").Trim();
                    sumaBruto = Double.Parse(label27.Text.ToString().Equals("") ? "0" : label27.Text.ToString()) - Double.Parse(label26.Text.ToString().Equals("") ? "0" : label26.Text.ToString());
                    label22.Text = sumaBruto.ToString("###,##0.00").Trim();

                }
                else
                {
                    if (vTipAfecta.Equals("001"))
                    {
                        valIgvCal = (sumaTotal * valIgv) / (100 + valIgv);
                        vSubTotal = sumaTotal - valIgvCal;
                        label26.Text = valIgvCal.ToString("###,##0.00").Trim();
                        label27.Text = sumaTotal.ToString("###,##0.00").Trim();
                        label22.Text = vSubTotal.ToString("###,##0.00").Trim();

                    }
                    else
                    {
                        valIgvCal = (sumaTotal * valIgv) / (100);
                        vSubTotal = sumaTotal;
                        sumaTotal = sumaTotal + valIgvCal;
                        label26.Text = valIgvCal.ToString("###,##0.00").Trim();
                        label27.Text = sumaTotal.ToString("###,##0.00").Trim();
                        label22.Text = vSubTotal.ToString("###,##0.00").Trim();

                    }
                }
            }
            else
            {
                label26.Text = sumaIgv.ToString("###,##0.00").Trim();
                label27.Text = sumaTotal.ToString("###,##0.00").Trim();
                sumaBruto = Double.Parse(label27.Text.ToString().Equals("") ? "0" : label27.Text.ToString()) - Double.Parse(label26.Text.ToString().Equals("") ? "0" : label26.Text.ToString());
                label22.Text = sumaBruto.ToString("###,##0.00").Trim();

            }
        }



        private void FrmVentasEnLinea_Load(object sender, EventArgs e)
        {
            nParam = "29";
            //Puerto smtp
            string vDimension = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vDimension = ObjParametro.Valor;
            }

            string vIdUser = nomCodUser;
            if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
            {
                if (ObjRol.BuscarRol(ObjRolUser.IdRol))
                {
                    string valiUserRol = ObjRol.Nombre.ToString();

                    //Validar el Rol del Usuario y el tipo de Servicio
                        if (vDimension.Equals("1"))
                        {
                            this.StartPosition = FormStartPosition.CenterParent;
                            this.CenterToParent();
                            this.Size = new Size(1000, 600);
                            //this.Location = new Point(255, 70);

                            label28.Location = new Point(15, 530);
                            label25.Location = new Point(204, 530);
                            label22.Location = new Point(304, 530);
                            label24.Location = new Point(459, 530);
                            label26.Location = new Point(556, 530);
                            label23.Location = new Point(694, 530);
                            label27.Location = new Point(796, 530);

                            label41.Location = new Point(650, 213);
                            textBox6.Location = new Point(650, 263);
                            label42.Location = new Point(650, 337);
                            label43.Location = new Point(650, 381);

                            button5.Location = new Point(838, 215);
                            button6.Location = new Point(838, 285);
                            button12.Location = new Point(838, 355);
                            button8.Location = new Point(838, 438);

                            Grid1.Columns[1].Width = 250;
                            Grid1.Width = 630;
                            Grid1.Height = 300;
                        }
                }

            }
            else
            {
                MessageBox.Show("Fallo en la Validacion de Usuarios" + "\n" +
                                          "Por Favor Comunicarse con el Area de SOPORTE", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (FrmVentas.nmVen == 'M')
            {
                Cod = FrmVentas.nIdVenta.ToString();
                LlenarCampos(Cod);
            }
            else
            {
                label2.Text = nomEmpresa;
                label3.Text = nomAlmacen;
                labelService.Text = nomService;
                textBox1.Text = FrmLogin.x_serie;

                //Precios
                comboBox1.Items.Add("P.Unitario");
                comboBox1.Items.Add("P.Mayorista");
                comboBox1.Items.Add("P.Volumen");
                comboBox1.Items.Add("P.Oferta");
                comboBox1.SelectedIndex = 0;

                //Carga Tipo de Cambio
                if (ObjTipoCambio.BuscarTipoCambio())
                {
                    double tCambio = ObjTipoCambio.Valor;
                    label36.Text = tCambio.ToString("###,##0.000").Trim();
                }
                else
                {
                    MessageBox.Show("FALSE");
                }

                string nParam = "1";
                //Verificar Moneda Predertimanada
                if (ObjParametro.BuscarParametro(nParam))
                {
                    //monPred = ObjParametro.Valor.ToString();
                    string vParam = "2";
                    string vCodCat = "001";
                    ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjParametro.Valor.ToString(), vParam.ToString());
                    monPred = ObjDetCatalogo.CodDetCat;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToString("G");
            string vComp = "";
            //Comprobante Predeterminado
            nParam = "6";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vComp = ObjParametro.Valor;
            }
            else
            {
                vComp = "";
            }
        }

        private int validaCantEnTextBox(string codigoProducto)
        {
            DataSet cantidadBarras = csql.dataset_cadena("Call SpValidarCantBText('" + codigoProducto + "')");
            object cantiiB = cantidadBarras.Tables[0].Rows[0][0];
            
            return int.Parse(cantiiB.ToString());
        }

        private string validaEstadoProducto(string codigoProducto)
        {
            DataSet EstadoProducto = csql.dataset_cadena("Call SpValidaEstado('" + codigoProducto + "')");
            object est = EstadoProducto.Tables[0].Rows[0][0];

            return est.ToString();
        }
        

        private int validaCantCodBarras(string codigin, string codigoBarras)
        {
            DataSet cantidadEnGrilla = csql.dataset_cadena("Call SpValidaCantCodBarras('" + codigin + "','" + codigoBarras + "')");
            object valor = cantidadEnGrilla.Tables[0].Rows[0][0];

            return int.Parse(valor.ToString());
        }

        private void llenadoGrillaCodBarra()
        {
            string codprd = lblcod.Text.ToString();
            string codigo = textBox2.Text.ToString();
            string producto = label11.Text.ToString();
            string unidad = label18.Text.ToString();
            string nomMarca = label16.Text.ToString();
            string pVenta = "";
            string vCantidad = "";
            string vDcto = "";
            string vIgv = "";
            double valIgv = 0;
            double igvItem = 0;
            //string vPercep = "";
            string vImp = "";
            //string vPorDesc = "";
            //Obtener IGV
            string nParam = "3";
            double precioventa = double.Parse(textBox4.Text.ToString());
            double cantidad = 1;
            double igv = 0;
            string vParam = "1";
            string vCodCat = "010";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label16.Text.ToString().Trim(), vParam.ToString());
            string marca = ObjDetCatalogo.CodDetCat;
            vCodCat = "013";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label18.Text.ToString().Trim(), vParam.ToString());
            string codunidad = ObjDetCatalogo.CodDetCat;
            vCodCat = "009";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label12.Text.ToString().Trim(), vParam.ToString());
            string proced = ObjDetCatalogo.CodDetCat;

            if (ObjParametro.BuscarParametro(nParam))
            {
                valIgv = Double.Parse(ObjParametro.Valor);
            }
            else
            {
                valIgv = 0;
            }

            if (precioventa.ToString().Trim().Equals("0"))
                pVenta = "";
            else
                pVenta = precioventa.ToString("###,##0.0000").Trim();

            if (cantidad.ToString().Trim().Equals("0"))
                vCantidad = "";
            else
                vCantidad = cantidad.ToString().Trim();

            if (igv.ToString().Trim().Equals("0"))
                vIgv = "";
            else
                vIgv = igv.ToString("###,##0.00").Trim();

            double vImporte = (precioventa * cantidad);
            label30.Text = vImporte.ToString();
            double importe = double.Parse(label30.Text.ToString());

            if (importe.ToString().Trim().Equals("0"))
                vImp = "";
            else
                vImp = importe.ToString("###,##0.00").Trim();

            //Validar IGV
            Boolean tieneIgv = false;
            validaIgv = false;
            if (Grid1.RowCount > 0)
            {
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    if (row.Cells[7].Value.ToString().Equals(""))
                    {
                        tieneIgv = false;
                    }
                    else
                    {
                        tieneIgv = true;
                    }
                }

                if (igv > 0 && tieneIgv == false)
                {
                    MessageBox.Show("La lista de productos no tiene montos de Igv afectos, no puede ingresar productos afectos a IGV", "SISTEMA");
                    validaIgv = true;
                    return;
                }
            }
            else
            {
                if (igv > 0 && checkBox1.Checked == true)
                {
                    MessageBox.Show("La opcion de afectar IGV a compra esta activa, no se puede ingresar productos afectos a IGV", "SISTEMA");
                    validaIgv = true;
                    return;
                }
            }

            //Valida si el producto existe
            Boolean existe = false;
            double vPrecio = 0;
            int nIndex = 0;
            int contador = 0;
            double cantArti = 0;
            double preUnit = 0;
            //double valPorDesc = 0;

            foreach (DataGridViewRow row in Grid1.Rows)
            {
                vPrecio = Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());

                string codigin = row.Cells["Código"].Value.ToString();
                string codigoBarras = row.Cells["Barras"].Value.ToString();

                if (row.Cells[15].Value.ToString().Equals(textBox2.Text.ToString()) && vPrecio == precioventa && int.Parse(row.Cells[5].Value.ToString()) < validaCantCodBarras(codigin, codigoBarras)) //&& pordesc == valPorDesc)
                {
                    existe = true;
                    cantArti = Double.Parse(row.Cells[5].Value.ToString());

                    preUnit = Double.Parse(row.Cells[6].Value.ToString());
                    nIndex = contador;
                }
                else if (row.Cells[15].Value.ToString().Equals(textBox2.Text.ToString()) && vPrecio == precioventa)
                {
                    existe = true;
                    cantArti = Double.Parse(row.Cells[5].Value.ToString()) - 1;

                    preUnit = Double.Parse(row.Cells[6].Value.ToString());
                    nIndex = contador;
                    MessageBox.Show("El artículo " + row.Cells[1].Value.ToString() + " no cuenta con más stock", "SISTEMA");
                    MessageBox.Show("El artículo " + row.Cells[1].Value.ToString() + " no cuenta con más stock", "SISTEMA");
                }

                contador += 1;

            }
            double newCant = 0;
            double newImporte = 0;
            //double newDesc = 0;
            if (existe)
            {
                newCant = cantidad + cantArti;
                newImporte = preUnit * newCant;
                //Igv
                if (igv > 0)
                {
                    igvItem = (newImporte * valIgv) / 100;
                }
                //Percepcion
                newImporte = newImporte + igvItem; //+ percep - newDesc;
                Grid1[5, nIndex].Value = newCant;

                Grid1[7, nIndex].Value = igvItem == 0 ? "" : igvItem.ToString("###,##0.00").Trim();
                Grid1[8, nIndex].Value = newImporte.ToString("###,##0.00").Trim();
            }
            else
            {
                this.Grid1.Rows.Add(new[] { codprd, producto, nomMarca, unidad, label12.Text, vCantidad, pVenta, vIgv, vImp, marca, codunidad, proced, "01", "20", textBox5.Text, codigo });
            }

            calculaTotales();

            label28.Text = "Total documentos: " + Grid1.RowCount;
            calculaTotales();

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox4.Enabled = false;
            comboBox1.SelectedIndex = 0;
            //label31.Text = "";
            textBox2.Focus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string codProducto = textBox2.Text.ToString().Trim();
            BusProducto(codProducto, rucEmpresa);

            if (codProducto.Trim().Length > 12)
            {
                if (validaEstadoProducto(codProducto).Equals("A"))
                {

                }
                else
                {
                    MessageBox.Show("El producto está Inactivo", "SISTEMA");
                    textBox2.Text = "";
                    return;
                }
                if (validaCantEnTextBox(codProducto) > 0)
                {
                    string codBarras = textBox2.Text.ToString().Trim();
                    BusProductoPorCodBarras(rucEmpresa, codBarras);
                    llenadoGrillaCodBarra();
                }
                else
                {
                    //textBox2.Text = "";
                    MessageBox.Show("El producto no cuenta con stock de codigo de barras o el codigo de barras aún no ha sido registrado", "SISTEMA");
                    textBox2.Text = "";
                    MessageBox.Show("El producto no cuenta con stock de codigo de barras o el codigo de barras aún no ha sido registrado", "SISTEMA");
                }
            }
            
            if (codProducto.Trim().Length == 7 && validaCantEnTextBox(codProducto) > 0 || codProducto.Trim().Length == 8 && validaCantEnTextBox(codProducto) > 0
                || codProducto.Trim().Length == 12 && validaCantEnTextBox(codProducto) > 0)
            {
                if (validaEstadoProducto(codProducto).Equals("A"))
                {

                }
                else
                {
                    MessageBox.Show("El producto está Inactivo", "SISTEMA");
                    textBox2.Text = "";
                    return;
                }
                if (validaCantEnTextBox(codProducto) > 0)
                {
                    string codBarras = textBox2.Text.ToString().Trim();
                    BusProductoPorCodBarras(rucEmpresa, codBarras);
                    llenadoGrillaCodBarra();
                }
                else
                {
                    //textBox2.Text = "";
                    MessageBox.Show("El producto no cuenta con stock de codigo de barras o el codigo de barras aún no ha sido registrado", "SISTEMA");
                    textBox2.Text = "";
                    MessageBox.Show("El producto no cuenta con stock de codigo de barras o el codigo de barras aún no ha sido registrado", "SISTEMA");
                }
            }
            else
            {

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0)
            {
                if (ObjProducto.BuscarProducto(textBox2.Text, rucEmpresa, Almacen))
                {
                    int caseSwitch = comboBox1.SelectedIndex;
                    switch (caseSwitch)
                    {
                        case 0:
                            textBox4.Text = ObjProducto.PVenta.ToString();
                            break;

                        case 1:
                            textBox4.Text = ObjProducto.PMayorista.ToString();
                            break;

                        case 2:
                            textBox4.Text = ObjProducto.PVolumen.ToString();
                            break;

                        case 3:
                            if (lblMensajeOferta.Visible == false)
                            {
                                MessageBox.Show("Este artículo no tiene ofertas", "SISTEMA");
                                textBox4.Text = "";
                            }
                            else
                            {
                                textBox4.Text = ObjProducto.PVenta.ToString();
                            }
                            break;

                        default:
                            textBox4.Text = "";
                            break;
                    }



                    double Net = 0;
                    Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox4.Text = "";
                    else
                        textBox4.Text = Net.ToString("###,##0.00").Trim();


                }
                else
                {
                    textBox4.Text = "";
                    //CalcularIgv();
                }
            }

            if (comboBox1.SelectedItem.Equals("P.Volumen"))
            {
                textBox3.Text = ObjProducto.CantPrecioVolum.ToString();
            }

            if (comboBox1.SelectedIndex == 3)
            {
                if (cantOferta != 0)
                {
                    textBox3.Text = cantOferta.ToString();
                }
                else
                {
                    textBox3.Text = "";
                }

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (//char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox2.SelectionStart = 0;
                textBox2.SelectionLength = textBox2.TextLength;
                textBox2.Focus();
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox1.Text = "";
                else
                    textBox1.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox1.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (textBox4.Enabled == true)
                {
                    textBox4.SelectionStart = 0;
                    textBox4.SelectionLength = textBox4.TextLength;
                    textBox4.Focus();
                }
                else
                {
                    textBox3.SelectionStart = 0;
                    textBox3.SelectionLength = textBox3.TextLength;
                    textBox3.Focus();
                }
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
                char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            double cantidad;
            cantidad = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());

            if (cantidad < ObjProducto.CantPrecioVolum)
            {
                if (textBox2.TextLength > 0)
                {
                    if (ObjProducto.BuscarProducto(textBox2.Text, rucEmpresa, Almacen))
                    {
                        int caseSwitch = comboBox1.SelectedIndex;
                        switch (caseSwitch)
                        {
                            case 0:
                                textBox4.Text = ObjProducto.PVenta.ToString();
                                break;

                            case 1:
                                textBox4.Text = ObjProducto.PMayorista.ToString();
                                break;

                            case 2:
                                textBox4.Text = "";
                                break;
                            case 3:
                                textBox4.Text = ObjProducto.PVenta.ToString();
                                break;

                            default:
                                textBox4.Text = "";
                                break;
                        }



                        double Net = 0;
                        Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            textBox4.Text = "";
                        else
                            textBox4.Text = Net.ToString("###,##0.00").Trim();


                    }
                    else
                    {
                        textBox4.Text = "";
                    }
                }
            }
            else
            {
                if (textBox2.TextLength > 0)
                {
                    if (ObjProducto.BuscarProducto(textBox2.Text, rucEmpresa, Almacen))
                    {
                        int caseSwitch = comboBox1.SelectedIndex;
                        switch (caseSwitch)
                        {
                            case 0:
                                textBox4.Text = ObjProducto.PVenta.ToString();
                                break;

                            case 1:
                                textBox4.Text = ObjProducto.PMayorista.ToString();
                                break;

                            case 2:
                                textBox4.Text = ObjProducto.PVolumen.ToString();
                                break;

                            case 3:
                                textBox4.Text = ObjProducto.PVenta.ToString();
                                break;

                            default:
                                textBox4.Text = "";
                                break;
                        }



                        double Net = 0;
                        Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            textBox4.Text = "";
                        else
                            textBox4.Text = Net.ToString("###,##0.00").Trim();


                    }
                    else
                    {
                        textBox4.Text = "";
                    }
                }
            }


            CalcularImportes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            detser = textBox5.Text;
            if (label11.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Producto", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (textBox3.TextLength == 0)
            {
                MessageBox.Show("Ingrese Cantidad", "SISTEMA");
                textBox3.Focus();
                return;
            }

            if (comboBox1.Text == "" || textBox4.Text == "" || textBox4.Text.Equals("0.00"))
            {
                MessageBox.Show("Ingrese Precio", "SISTEMA");
                comboBox1.Focus();
                return;
            }

            if (textBox5.Text == "" && textBox5.Enabled == true)
            {
                MessageBox.Show("Ingrese Descripción del servicio", "SISTEMA");
                textBox5.Focus();
                return;
            }


            //Cantidad de Articulos
            double CantGrid1 = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells[5].Value.ToString().Equals(label13.ToString()))
                {
                    CantGrid1 = Double.Parse(row.Cells[5].Value.ToString());
                }
            }

            double vCantidad = Double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            double cant = CantGrid1;

            if (label33.Visible == false)
            {
                double valStock = Double.Parse(label13.Text.ToString().Equals("") ? "0" : label13.Text.ToString().Trim());

                if (((ObjProducto.VentaMin.Equals("S") || (vCantidad > valStock))))
                {
                    if (label34.Text == "S")
                    {
                        if (valStock == 0 || (vCantidad > valStock))
                        {
                            MessageBox.Show("STOCK NO DISPONIBLE, VERIFIQUE", "SISTEMA");
                            textBox2.Focus();
                            return;
                        }

                        if (cant > valStock)
                        {
                            MessageBox.Show("STOCK NO DISPONIBLE, VERIFIQUE", "SISTEMA");
                            textBox2.Focus();
                            return;
                        }
                    }
                }
                if (cant > valStock)
                {
                    MessageBox.Show("STOCK NO DISPONIBLE, VERIFIQUE", "SISTEMA");
                    textBox2.Focus();
                    return;
                }
            }

            CalcularImportes(); //Calcular importe si o si

            string vParam = "1";
            string vCodCat = "010";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label16.Text.ToString().Trim(), vParam.ToString());
            string marca = ObjDetCatalogo.CodDetCat;
            vCodCat = "013";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label18.Text.ToString().Trim(), vParam.ToString());
            string unidad = ObjDetCatalogo.CodDetCat;
            vCodCat = "009";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label12.Text.ToString().Trim(), vParam.ToString());
            string proced = ObjDetCatalogo.CodDetCat;

            //Ofertas
            if (lblMensajeOferta.Visible)
            {
                string message = "";
                if (!ObjDetalleOferta.ParentArt.Equals(""))
                {
                    message = "Este Artículo tiene oferta dependiente de otro Artículo \n";
                    message = message + "Producto dependiente: " + ObjDetalleOferta.NombreArtDep + "\n";
                    message = message + "La VENTA no se podra realizar si no si ingresa el producto dependiente";
                    MessageBox.Show(message, "SISTEMA");
                }

                if (ObjDetalleOferta.cantidad != 0 && (comboBox1.SelectedItem.Equals("P.Oferta")))

                {
                    if (vCantidad < Double.Parse(ObjDetalleOferta.cantidad.ToString()))
                    {
                        message = "La cantidad ingresada debe ser igual o mayor para la oferta de este Artículo \n";
                        message = message + "Cantidad de oferta: " + ObjDetalleOferta.cantidad;
                        MessageBox.Show(message, "SISTEMA");
                        textBox3.Focus();
                        return;
                    }
                }

            }

            //Valida si el producto existe
            Boolean existe = false;
            double vPrecio = 0;
            int nIndex = 0;
            int contador = 0;
            double cantArti = 0;
            double preUnit = 0;
            double preVenta = 0;
            double valIgv = 0;
            preVenta = Double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString());

            foreach (DataGridViewRow row in Grid1.Rows)
            {
                vPrecio = Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());
                if (row.Cells[0].Value.ToString().Equals(textBox2.Text.ToString().Trim()) && vPrecio == preVenta) //&& pordesc == valPorDesc)
                {
                    //Al comentar se reemplaza la cantidad existente por el nuevo
                    existe = true;
                    cantArti = Double.Parse(row.Cells[5].Value.ToString());

                    preUnit = Double.Parse(row.Cells[6].Value.ToString());
                    nIndex = contador;
                }
                contador += 1;
            }

            //Obtener IGV
            nParam = "3";
            if (ObjParametro.BuscarParametro(nParam))
            {
                valIgv = Double.Parse(ObjParametro.Valor);
            }
            else
            {
                valIgv = 0;
            }
            //Verifica si aplica IGV
            string vAplicaIgv = "";
            nParam = "7";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vAplicaIgv = ObjParametro.Valor;
            }
            else
            {
                vAplicaIgv = "";
            }

            double newCant = 0;
            double newImporte = 0;
            double igvItem = 0;
            double cantidad = Double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString());
            double igv = 0;
            if (existe)
            {
                newCant = cantidad + cantArti;
                newImporte = preUnit * newCant;
                //Igv
                if (igv > 0)
                {
                    igvItem = (newImporte * valIgv) / 100;
                }

                newImporte = newImporte + igvItem; //+ percep - newDesc;
                Grid1[5, nIndex].Value = newCant;

                Grid1[7, nIndex].Value = igvItem == 0 ? "" : igvItem.ToString("###,##0.00").Trim();

                Grid1[8, nIndex].Value = newImporte.ToString("###,##0.00").Trim();
            }
            else
            {
                double vImp = 0;
                double vIgv = 0;
                double vPrec = 0;
                string vTipPrecio = "";
                string vTipImp = "";
                if (vAplicaIgv.Equals("S"))
                {
                    vTipPrecio = "01";
                    if (checkBox1.Checked == true)
                    {
                        vTipImp = "11";
                    }
                    else
                    {
                        vTipImp = "10";
                    }

                    //Obtener Tipo de afectación IGV
                    string nParam1 = "11";
                    string vTipAfecta = "";

                    if (ObjParametro.BuscarParametro(nParam1))
                    {
                        vTipAfecta = ObjParametro.Valor;
                    }
                    else
                    {
                        MessageBox.Show("Error no se puede aplicar IGV verifique configuración de parámetros", "SISTEMA");
                        return;
                    }

                    if (!checkBox1.Checked == true)
                    {
                        if (vTipAfecta.Equals("001"))
                        {
                            vImp = Double.Parse(label30.Text.ToString().Equals("") ? "0" : label30.Text.ToString());
                            vIgv = (vImp * valIgv) / (100 + valIgv);
                        }
                        else
                        {
                            vPrec = Double.Parse(label30.Text.ToString().Equals("") ? "0" : label30.Text.ToString());
                            vIgv = (vPrec * valIgv) / (100);
                            vImp = vPrec + vIgv;
                        }
                    }
                }
                else
                {
                    vTipPrecio = "01";
                    if (checkBox1.Checked == true)
                    {
                        vTipImp = "11";
                    }
                    else
                    {
                        vTipImp = "20";
                    }
                }

                this.Grid1.Rows.Add(new[] { textBox2.Text.ToString(),
                                            label11.Text.ToString(),
                                            label16.Text.ToString(),
                                            label18.Text.ToString(),
                                            label12.Text.ToString(),
                                            textBox3.Text.ToString(),
                                            textBox4.Text.ToString(),
                                            vIgv.ToString(),
                                            label30.Text.ToString().Equals("") ? "0" : label30.Text.ToString(),
                                            marca.ToString(),
                                            unidad.ToString(),
                                            proced.ToString(),
                                            vTipPrecio.ToString(),
                                            vTipImp.ToString(),
                                            textBox5.Text,
                                            lblcod.Text.ToString()
                });
            }

            label28.Text = "Total documentos: " + Grid1.RowCount;
            calculaTotales();

            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox4.Enabled = false;
            comboBox1.SelectedIndex = 0;
            //label31.Text = "";
            textBox2.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                int index = Grid1.CurrentRow.Index;
                Grid1.Rows.RemoveAt(index);
                calculaTotales();
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            buttonGuardar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
            //Buscar Rol del Usuario por IDuser
            string vIdUser = nomCodUser;
            if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
            {
                if (ObjRol.BuscarRol(ObjRolUser.IdRol))
                {
                    string valiUserRol = ObjRol.Nombre.ToString();

                    //Validar el Rol del Usuario y el tipo de Servicio
                    if (valiUserRol.ToString().Equals("VENDEDOR") && nomService.ToString().Equals("PRODUCCION") || valiUserRol.ToString().Equals("ADMINISTRADOR"))
                    {
                        frmBusArticulo.WindowState = FormWindowState.Normal;
                        frmBusArticulo.Opener = this;
                        frmBusArticulo.StartPosition = FormStartPosition.CenterScreen;
                        frmBusArticulo.Show();
                    }
                    else if (nomService.ToString() != "PRODUCCION")
                    {
                        MessageBox.Show("Usted esta usando el Servicio de SUNAT como " + nomService.ToString() + "\n" +
                                    "Por Favor Comunicarse con el Area de SOPORTE", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (valiUserRol.ToString() != "VENDEDOR")
                    {
                        if (valiUserRol.ToString().Equals("ALMACENERO"))
                        {
                            MessageBox.Show("Usted es un Usuario ALMACENERO" + "\n" +
                                        "Por Favor Comunicarse con el Area de SOPORTE", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (valiUserRol.ToString().Equals("004"))
                        {
                            MessageBox.Show("Usted es un Usuario REPARTIDOR" + "\n" +
                                        "Por Favor Comunicarse con el Area de SOPORTE", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else if (valiUserRol.ToString().Equals("005"))
                        {
                            MessageBox.Show("Usted es un Usuario CONTABILIDAD" + "\n" +
                                        "Por Favor Comunicarse con el Area de SOPORTE", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                    }

                }

            }
            else
            {
                MessageBox.Show("Fallo en la Validacion de Usuarios" + "\n" +
                                          "Por Favor Comunicarse con el Area de SOPORTE", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Escape)
            {
                textBox2.Clear();
                textBox3.Clear();
                Grid1.Rows.Clear();
                textBox4.Text = "";
                textBox4.Enabled = false;
                label22.Text = "";
                label26.Text = "";
                label27.Text = "";
                comboBox1.SelectedIndex = 0;
            }

            if (e.KeyCode == Keys.Down)
            {
                textBox2.Focus();
            }

            if (e.KeyCode == Keys.F3)
            {
                //Valida si el usuario es ADMINISTRADOR
                string Usuario = FrmLogin.x_login_usuario;
                string vIdUser = "";
                if (ObjUsuario.BuscaUSer(Usuario.ToString()))
                {
                    vIdUser = ObjUsuario.IdUser.ToString();
                }
                else
                {
                    vIdUser = "";
                }
                string vIdRol = "";
                if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
                {
                    vIdRol = ObjRolUser.IdRol;
                }
                else
                {
                    vIdRol = "";
                }

                if (vIdRol.Equals("001"))
                {
                    //textBox4.Enabled = true;
                    textBox4.SelectionStart = 0;
                    textBox4.SelectionLength = textBox4.TextLength;
                    textBox4.Focus();
                }
                else
                {
                    FrmValidarPrecio frmValidarPfecio = new FrmValidarPrecio();
                    frmValidarPfecio.WindowState = FormWindowState.Normal;
                    frmValidarPfecio.Opener = this;
                    frmValidarPfecio.ShowDialog(this);
                }
            }

            if (e.KeyCode == Keys.F4)
            {
                buttonGuardar();
            }

            if (e.KeyCode == Keys.F5)
            {
                FrmValidarPrecio frmValidarPrecio = new FrmValidarPrecio();
                frmValidarPrecio.WindowState = FormWindowState.Normal;
                frmValidarPrecio.Opener = this;
                frmValidarPrecio.ShowDialog(this);
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (Grid1.RowCount > 0)
                {
                    if (MessageBox.Show("¿Está seguro de descartar la venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                    Grid1.Rows.Clear();
                    textBox4.Text = "";
                    //textBox4.Enabled = false;
                    label22.Text = "";
                    label26.Text = "";
                    label27.Text = "";
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    this.Close();
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                comboBox1.Focus();
            }

            if (e.KeyCode == Keys.Up)
            {
                textBox1.Focus();
            }

            if (e.KeyCode == Keys.F3)
            {
                int caseSwitch = comboBox1.SelectedIndex;
                switch (caseSwitch)
                {
                    case 0:
                        comboBox1.SelectedIndex += 1;
                        break;

                    case 1:
                        comboBox1.SelectedIndex += 1;
                        break;

                    case 2:
                        comboBox1.SelectedIndex = 0;
                        break;

                    default:
                        comboBox1.SelectedIndex = 0;
                        break;
                }
            }

            if (e.KeyCode == Keys.F4)
            {
                buttonGuardar();
            }

            if (e.KeyCode == Keys.F5)
            {
                FrmValidarPrecio frmValidarPrecio = new FrmValidarPrecio();
                frmValidarPrecio.WindowState = FormWindowState.Normal;
                frmValidarPrecio.Opener = this;
                frmValidarPrecio.ShowDialog(this);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (Grid1.RowCount > 0)
                {
                    if (MessageBox.Show("¿Está seguro de descartar la venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                    Grid1.Rows.Clear();
                    textBox4.Text = "";
                    //textBox4.Enabled = false;
                    label22.Text = "";
                    label26.Text = "";
                    label27.Text = "";
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    this.Close();
                }
            }

            if (e.KeyCode == Keys.F3)
            {
                //Valida si el usuario es ADMINISTRADOR
                string Usuario = FrmLogin.x_login_usuario;
                string vIdUser = "";
                if (ObjUsuario.BuscaUSer(Usuario.ToString()))
                {
                    vIdUser = ObjUsuario.IdUser.ToString();
                }
                else
                {
                    vIdUser = "";
                }
                string vIdRol = "";
                if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
                {
                    vIdRol = ObjRolUser.IdRol;
                }
                else
                {
                    vIdRol = "";
                }

                if (vIdRol.Equals("001"))
                {
                    //textBox4.Enabled = true;
                    textBox4.SelectionStart = 0;
                    textBox4.SelectionLength = textBox4.TextLength;
                    textBox4.Focus();
                }
                else
                {
                    FrmValidarPrecio frmValidarPfecio = new FrmValidarPrecio();
                    frmValidarPfecio.WindowState = FormWindowState.Normal;
                    frmValidarPfecio.Opener = this;
                    frmValidarPfecio.ShowDialog(this);
                }
            }

            if (e.KeyCode == Keys.F4)
            {
                buttonGuardar();
            }

            if (e.KeyCode == Keys.F5)
            {
                FrmValidarPrecio frmValidarPrecio = new FrmValidarPrecio();
                frmValidarPrecio.WindowState = FormWindowState.Normal;
                frmValidarPrecio.Opener = this;
                frmValidarPrecio.ShowDialog(this);
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (Grid1.RowCount > 0)
                {
                    if (MessageBox.Show("¿Está seguro de descartar la venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                    Grid1.Rows.Clear();
                    textBox4.Text = "";
                    //textBox4.Enabled = false;
                    label22.Text = "";
                    label26.Text = "";
                    label27.Text = "";
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    this.Close();
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                if (textBox4.Enabled == true)
                {
                    textBox4.Focus();
                }
                else
                {
                    comboBox1.Focus();
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                button2.Focus();
            }

            if (e.KeyCode == Keys.F3)
            {
                //Valida si el usuario es ADMINISTRADOR
                int caseSwitch = comboBox1.SelectedIndex;
                switch (caseSwitch)
                {
                    case 0:
                        comboBox1.SelectedIndex += 1;
                        break;

                    case 1:
                        comboBox1.SelectedIndex += 1;
                        break;

                    case 2:
                        comboBox1.SelectedIndex = 0;
                        break;

                    default:
                        comboBox1.SelectedIndex = 0;
                        break;
                }
            }

            if (e.KeyCode == Keys.F4)
            {
                buttonGuardar();
            }

            if (e.KeyCode == Keys.F5)
            {
                FrmValidarPrecio frmValidarPrecio = new FrmValidarPrecio();
                frmValidarPrecio.WindowState = FormWindowState.Normal;
                frmValidarPrecio.Opener = this;
                frmValidarPrecio.ShowDialog(this);
            }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (Grid1.RowCount > 0)
                {
                    if (MessageBox.Show("¿Está seguro de descartar la venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                    Grid1.Rows.Clear();
                    textBox4.Text = "";
                    //textBox4.Enabled = false;
                    label22.Text = "";
                    label26.Text = "";
                    label27.Text = "";
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    this.Close();
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                textBox3.Focus();
            }

            if (e.KeyCode == Keys.F3)
            {
                int caseSwitch = comboBox1.SelectedIndex;
                switch (caseSwitch)
                {
                    case 0:
                        comboBox1.SelectedIndex += 1;
                        break;

                    case 1:
                        comboBox1.SelectedIndex += 1;
                        break;

                    case 2:
                        comboBox1.SelectedIndex = 0;
                        break;

                    default:
                        comboBox1.SelectedIndex = 0;
                        break;
                }
            }

            if (e.KeyCode == Keys.F4)
            {
                buttonGuardar();
            }

            if (e.KeyCode == Keys.F5)
            {
                FrmValidarPrecio frmValidarPrecio = new FrmValidarPrecio();
                frmValidarPrecio.WindowState = FormWindowState.Normal;
                frmValidarPrecio.Opener = this;
                frmValidarPrecio.ShowDialog(this);
            }
        }

        private void button5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("¿Está seguro de descartar la venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                textBox2.Clear();
                textBox3.Clear();
                Grid1.Rows.Clear();
                textBox4.Text = "";
                label22.Text = "";
                label26.Text = "";
                label27.Text = "";
            }
        }

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("¿Está seguro de descartar la venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                textBox2.Clear();
                textBox3.Clear();
                Grid1.Rows.Clear();
                textBox4.Text = "";
                label22.Text = "";
                label26.Text = "";
                label27.Text = "";
            }
        }

        private void Grid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (Grid1.RowCount > 0)
                {
                    int index = Grid1.CurrentRow.Index;
                    Grid1.Rows.RemoveAt(index);
                    calculaTotales();
                }
                else
                {
                }
            }

            if (e.KeyCode == Keys.F4)
            {
                buttonGuardar();
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("¿Está seguro de descartar la venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                textBox2.Clear();
                textBox3.Clear();
                Grid1.Rows.Clear();
                textBox4.Text = "";
                label22.Text = "";
                label26.Text = "";
                label27.Text = "";
                textBox2.Focus();
            }
        }

        private void Grid1_RowDefaultCellStyleChanged(object sender, DataGridViewRowEventArgs e)
        {
        }

        private void FrmVentasEnLinea_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) // || //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox4.Text = "";
                else
                    textBox4.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox4.Focus();
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.F2)
            {
                FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
                frmBusArticulo.WindowState = FormWindowState.Normal;
                frmBusArticulo.Opener = this;
                frmBusArticulo.ShowDialog(this);
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (Grid1.RowCount > 0)
                {
                    if (MessageBox.Show("¿Está seguro de descartar la venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    textBox2.Clear();
                    textBox3.Clear();
                    Grid1.Rows.Clear();
                    textBox4.Text = "";
                    //textBox4.Enabled = false;
                    label22.Text = "";
                    label26.Text = "";
                    label27.Text = "";
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    this.Close();
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                comboBox1.Focus();
            }

            if (e.KeyCode == Keys.Down)
            {
                textBox3.Focus();
            }

            if (e.KeyCode == Keys.F3)
            {
                //Valida si el usuario es ADMINISTRADOR
                string Usuario = FrmLogin.x_login_usuario;
                string vIdUser = "";
                if (ObjUsuario.BuscaUSer(Usuario.ToString()))
                {
                    vIdUser = ObjUsuario.IdUser.ToString();
                }
                else
                {
                    vIdUser = "";
                }
                string vIdRol = "";
                if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
                {
                    vIdRol = ObjRolUser.IdRol;
                }
                else
                {
                    vIdRol = "";
                }

                if (vIdRol.Equals("001"))
                {
                    textBox4.Enabled = true;
                    textBox4.SelectionStart = 0;
                    textBox4.SelectionLength = textBox4.TextLength;
                    textBox4.Focus();
                }
                else
                {
                    FrmValidarPrecio frmValidarPfecio = new FrmValidarPrecio();
                    frmValidarPfecio.WindowState = FormWindowState.Normal;
                    frmValidarPfecio.Opener = this;
                    frmValidarPfecio.ShowDialog(this);
                }
            }

            if (e.KeyCode == Keys.F4)
            {
                buttonGuardar();
            }

            if (e.KeyCode == Keys.F5)
            {
                FrmValidarPrecio frmValidarPrecio = new FrmValidarPrecio();
                frmValidarPrecio.WindowState = FormWindowState.Normal;
                frmValidarPrecio.Opener = this;
                frmValidarPrecio.ShowDialog(this);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CalcularImportes();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string vId = "20351992094014F12-0000002410420352471578";
            ObjFormato.generaFormato(vId, rucEmpresa, Almacen);
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            //Toma el valor del codigo haciendo doble clic en la grilla, y lo muestra en los textBox's
            String codProducto = Grid1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = codProducto;
            BusProducto(codProducto, rucEmpresa);
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void label40_Click(object sender, EventArgs e)
        {
        }

        private void button12_Click(object sender, EventArgs e)
        {
            FrmVentas.vTDoc = "PCL";

            FrmVentas frmventas = new FrmVentas();
            frmventas.WindowState = FormWindowState.Normal;
            frmventas.Show();
        }

        private void LlenarCampos(string InCod)
        {
            label2.Text = nomEmpresa;
            label3.Text = nomAlmacen;
            textBox1.Text = FrmLogin.x_serie;

            //Precios
            comboBox1.Items.Add("P.Unitario");
            comboBox1.Items.Add("P.Mayorista");
            comboBox1.Items.Add("P.Volumen");
            comboBox1.SelectedIndex = 0;

            //Carga Tipo de Cambio
            if (ObjTipoCambio.BuscarTipoCambio())
            {
                double tCambio = ObjTipoCambio.Valor;
                label36.Text = tCambio.ToString("###,##0.000").Trim();
            }
            else
            {
                MessageBox.Show("FALSE");
            }

            string nParam = "1";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                //monPred = ObjParametro.Valor.ToString();
                string vParam = "2";
                string vCodCat = "001";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjParametro.Valor.ToString(), vParam.ToString());
                monPred = ObjDetCatalogo.CodDetCat;
            }
            try
            {
                double Net = 0;

                //Total bruto
                Net = double.Parse(ObjVenta.TBruto.ToString().Equals("") ? "0" : ObjVenta.TBruto.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                {
                    //label20.Text = "0.00";
                    Net = double.Parse(ObjVenta.TExonerada.ToString().Equals("") ? "0" : ObjVenta.TExonerada.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                    {
                        Net = double.Parse(ObjVenta.TInafecta.ToString().Equals("") ? "0" : ObjVenta.TInafecta.ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                        {
                            Net = double.Parse(ObjVenta.TGratuita.ToString().Equals("") ? "0" : ObjVenta.TGratuita.ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                            {
                                label22.Text = "0.000";
                            }
                            else
                            {
                                label22.Text = Net.ToString("###,##0.0000").Trim();
                            }
                        }
                        else
                        {
                            label22.Text = Net.ToString("###,##0.0000").Trim();
                        }
                    }
                    else
                    {
                        label22.Text = Net.ToString("###,##0.0000").Trim();
                    }
                }
                else
                {
                    label22.Text = Net.ToString("###,##0.0000").Trim();
                }
                //Total Igv
                Net = double.Parse(ObjVenta.TIgv.ToString().Equals("") ? "0" : ObjVenta.TIgv.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    label26.Text = "0.00";
                else
                    label26.Text = Net.ToString("###,##0.00").Trim();

                //Importe Total
                Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    label27.Text = "0.00";
                else
                    label27.Text = Net.ToString("###,##0.00").Trim();

                //Obtener Detalles
                DataSet datos = csql.dataset_cadena("Call SpDetPedidoClienteBuscar('" + InCod.ToString() + "','" + rucEmpresa.ToString() + "','" + Almacen.ToString() + "')");
                string vPunit = "";
                string vIgv = "";
                string vImporte = "";
                string vDcto = "";
                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        Net = double.Parse(fila[5].ToString().Equals("") ? "0" : fila[5].ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            vPunit = "";
                        else
                            vPunit = Net.ToString("###,##0").Trim();

                        Net = double.Parse(fila[6].ToString().Equals("") ? "0" : fila[6].ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            vIgv = "";
                        else
                            vIgv = Net.ToString("###,##0.00").Trim();

                        Net = double.Parse(fila[7].ToString().Equals("") ? "0" : fila[7].ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            vImporte = "";
                        else
                            vImporte = Net.ToString("###,##0.00").Trim();

                        Net = double.Parse(fila[11].ToString().Equals("") ? "0" : fila[11].ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            vDcto = "";
                        else
                            vDcto = Net.ToString("###,##0.0000").Trim();
                        this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), vPunit, vIgv, "", vImporte, "", "", "", fila[8].ToString(), fila[9].ToString(), "" });
                    }
                    calculaTotales();
                }
                else
                {
                    MessageBox.Show("FALSE");
                    this.Close();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se puede cargar datos: " + ex.Message);
                this.Close();
            }
            FrmVentas.nIdVenta.Equals(" ");
        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblMensajeOferta.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            lblMensajeOferta.ForeColor = System.Drawing.Color.Red;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            if (!label27.Text.ToString().Equals(""))
            {
                double sumTotal = double.Parse(label27.Text.ToString());
                double montoIngresado = Double.Parse(textBox6.Text.ToString().Equals("") ? "0" : textBox6.Text.ToString().Trim());

                double vueltoTotal = 0;
                if (!label27.Text.ToString().Equals("") && montoIngresado > sumTotal)
                {

                    vueltoTotal = montoIngresado - sumTotal;
                    label43.Text = vueltoTotal.ToString("###,##0.00").Trim();
                }
                else
                {
                    label43.Text = "";
                }
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }
    }
}