using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddVenta : Form, ICliente, IAddItemVenta
    {
        public IVenta Opener { get; set; }
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string codModulo = "VEN";
        private string valModTVen = "";
        private string Cod = "";
        private string vTipPrecio = "";
        private string vTipImp = "";
        private string vAplicaIgv = "";
        public static Boolean validaIgv = false;
        public static string vTipVentaS = "";
        public static int totArticulo = 0;
        public static string TDoc = "";
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsTipoCambio ObjTipoCambio = new ClsTipoCambio();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsVendedor ObjVendedor = new ClsVendedor();
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsDetVenta ObjDetVenta = new ClsDetVenta();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsAlmacen ObjAlmacen = new ClsAlmacen();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsImprimir ObjImprimir = new ClsImprimir();
        private ClsCreaFormato ObjFormato = new ClsCreaFormato();

        public FrmAddVenta()
        {
            InitializeComponent();
        }

        #region ICliente Members

        public void SelectItem(string tipCod, string codCli)
        {
            comboBox5.Text = tipCod;
            textBox10.Text = codCli;
        }

        #endregion ICliente Members

        #region IAddItem Members

        public void AddNewItemVenta(string codPro, string nomPro, string undMed, string marca, double preVenta, double cantidad, double Dcto, double igv, double importe, string proced)
        {
            string codigo = codPro.ToString();
            string producto = nomPro.ToString();
            string unidad = undMed.ToString();
            string nomMarca = marca.ToString();
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
            if (ObjParametro.BuscarParametro(nParam))
            {
                valIgv = Double.Parse(ObjParametro.Valor);
            }
            else
            {
                valIgv = 0;
            }

            if (preVenta.ToString().Trim().Equals("0"))
                pVenta = "";
            else
                pVenta = preVenta.ToString("###,##0.0000").Trim();

            if (cantidad.ToString().Trim().Equals("0"))
                vCantidad = "";
            else
                vCantidad = cantidad.ToString().Trim();

            if (igv.ToString().Trim().Equals("0"))
                vIgv = "";
            else
                vIgv = igv.ToString("###,##0.00").Trim();

            /*if (percep.ToString().Trim().Equals("0"))
                vPercep = "";
            else
                vPercep = percep.ToString("###,##0.00").Trim();*/

            if (importe.ToString().Trim().Equals("0"))
                vImp = "";
            else
                vImp = importe.ToString("###,##0.00").Trim();

            if (!Dcto.ToString().Trim().Equals("0"))
                vDcto = Dcto.ToString("###,##0.0000").Trim();

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

                if (igv == 0 && tieneIgv == true)
                {
                    MessageBox.Show("La lista de productos tiene montos de Igv afectos, debe ingresar productos afectos a IGV", "SISTEMA");
                    validaIgv = true;
                    return;
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
                vPrecio = Double.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString());
                //valPorDesc = Double.Parse(row.Cells[9].Value.ToString().Equals("") ? "0" : row.Cells[9].Value.ToString());
                if (row.Cells[0].Value.ToString().Equals(codPro) && vPrecio == preVenta) //&& pordesc == valPorDesc)
                {
                    existe = true;
                    cantArti = Double.Parse(row.Cells[4].Value.ToString());

                    preUnit = Double.Parse(row.Cells[5].Value.ToString());
                    nIndex = contador;
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
                /*if (percep > 0)
                {
                    percep = newImporte * 0.02;
                }

                if (pordesc > 0)
                {
                    newDesc = (newImporte * pordesc) / 100;
                }*/
                newImporte = newImporte + igvItem; //+ percep - newDesc;
                Grid1[4, nIndex].Value = newCant;

                Grid1[6, nIndex].Value = igvItem == 0 ? "" : igvItem.ToString("###,##0.00").Trim();
                //Grid1[7, nIndex].Value = percep == 0 ? "" : percep.ToString("###,##0.00").Trim();
                Grid1[7, nIndex].Value = newImporte.ToString("###,##0.00").Trim();
            }
            else
            {
                //this.Grid1.Rows.Add(new[] { codigo, producto, unidad, vCantidad, pCosto, vDcto, vIgv, vPercep, vImp, vPorDesc });
                this.Grid1.Rows.Add(new[] { codigo, producto, nomMarca, unidad, vCantidad, pVenta, vDcto, vIgv, vImp, proced });
            }

            calculaTotales();

            if (checkBox1.Checked == true)
            {
                calcularIgv();
            }

            TDoc = comboBox2.SelectedValue.ToString();
            totArticulo = Grid1.RowCount;
        }

        #endregion IAddItem Members

        private void BusCliente(string vTipDoc, string vCodCliente, string vRucEmpresa)
        {
            if (ObjCliente.BuscarCLiente(vCodCliente, vRucEmpresa))
            {
                textBox6.Text = ObjCliente.Nombre.ToString().Trim();
                textBox7.Text = ObjCliente.Direccion.ToString().Trim();
                comboBox5.Text = ObjCliente.TipDoc.ToString().Trim();
                textBox6.Enabled = false;
                string codVend = "";
                //Buscar Vendedor Zona
                if (!ObjCliente.CodVendedor.ToString().Equals(""))
                {
                    DataSet datos = csql.dataset_cadena("Call SpVendedorBusNom('" + ObjCliente.CodVendedor.ToString() + "','" + rucEmpresa.ToString() + "')");
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        codVend = fila[0].ToString();
                    }
                    if (ObjVendedor.BuscarVendedor(codVend, vRucEmpresa))
                    {
                        label9.Text = ObjVendedor.Zona.ToString();
                    }
                    else
                    {
                        label9.Text = "";
                    }
                }
                else
                {
                    label9.Text = "";
                }
            }
            else
            {
                textBox6.Text = "";
                textBox7.Text = "";
                textBox6.Enabled = true;
                label9.Text = "";
            }
        }

        private void LlenarCampos(string InCod)
        {
            try
            {
                codAlmacen = FrmVentas.almacenSelec.ToString();
                if (ObjVenta.BuscarVenta(InCod, rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    if (ObjAlmacen.BuscarAlmacen(codAlmacen.ToString(), rucEmpresa.ToString()))
                    {
                        comboBox1.Text = ObjAlmacen.Nombre.ToString().Trim();
                    }
                    else
                    {
                        MessageBox.Show("Error no se puede cargar Almacén", "SISTEMA");
                        this.Close();
                    }

                    DTP1.Text = ObjVenta.Fecha.ToString().Trim();
                    comboBox2.Text = ObjVenta.Doc.ToString().Trim();
                    comboBox6.Text = ObjVenta.TComp.ToString().Trim();
                    textBox1.Text = ObjVenta.Serie.ToString().Trim();
                    textBox2.Text = ObjVenta.Numero.ToString().Trim();
                    textBox3.Text = ObjVenta.NPedido.ToString().Trim();
                    cmbTipoPago.Text = ObjVenta.TipoPago.ToString().Trim();
                    //Tipo de cambio
                    double Net = 0;
                    Net = double.Parse(ObjVenta.TCambio.ToString().Equals("") ? "0" : ObjVenta.TCambio.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox4.Text = "";
                    else
                        textBox4.Text = Net.ToString("###,##0.000").Trim();
                    textBox10.Text = ObjVenta.Cliente.ToString().Trim();
                    comboBox5.Text = ObjVenta.TipDocCli.ToString().Trim();
                    comboBox3.Text = ObjVenta.TMoneda.ToString().Trim();
                    textBox5.Text = ObjVenta.NDias.ToString().Trim();
                    if (ObjVenta.TVenta.ToString().Equals("CREDITO"))
                    {
                        DTP2.Text = ObjVenta.FVence.ToString();
                        comboBox4.Text = ObjVenta.TVenta.ToString().Trim();
                        comboBox4.SelectedValue = "002";
                        DTP2.Enabled = true;
                        textBox5.Enabled = true;
                        valModTVen = "CREDITO";
                    }
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
                                    label20.Text = "0.000";
                                }
                                else
                                {
                                    label20.Text = Net.ToString("###,##0.0000").Trim();
                                }
                            }
                            else
                            {
                                label20.Text = Net.ToString("###,##0.0000").Trim();
                            }
                        }
                        else
                        {
                            label20.Text = Net.ToString("###,##0.0000").Trim();
                        }
                    }
                    else
                    {
                        label20.Text = Net.ToString("###,##0.0000").Trim();
                    }
                    //Total Igv
                    Net = double.Parse(ObjVenta.TIgv.ToString().Equals("") ? "0" : ObjVenta.TIgv.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label19.Text = "0.00";
                    else
                        label19.Text = Net.ToString("###,##0.00").Trim();

                    //Dcto
                    Net = double.Parse(ObjVenta.Dcto.ToString().Equals("") ? "0" : ObjVenta.Dcto.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label16.Text = "0.00";
                    else
                        label16.Text = Net.ToString("###,##0.00").Trim();

                    if (ObjVenta.TIgv > 0)
                    {
                        checkBox1.Checked = true;
                    }
                    //Importe Total
                    Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label18.Text = "0.00";
                    else
                        label18.Text = Net.ToString("###,##0.00").Trim();

                    //Obtener Detalles
                    DataSet datos = csql.dataset_cadena("Call SpDetVentaBuscar('" + InCod.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
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
                                vPunit = Net.ToString("###,##0.0000").Trim();

                            Net = double.Parse(fila[6].ToString().Equals("") ? "0" : fila[6].ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                                vIgv = "";
                            else
                                vIgv = Net.ToString("###,##0.0000").Trim();

                            Net = double.Parse(fila[7].ToString().Equals("") ? "0" : fila[7].ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                                vImporte = "";
                            else
                                vImporte = Net.ToString("###,##0.0000").Trim();

                            Net = double.Parse(fila[11].ToString().Equals("") ? "0" : fila[11].ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                                vDcto = "";
                            else
                                vDcto = Net.ToString("###,##0.0000").Trim();

                            this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), vPunit, vDcto, vIgv, vImporte, fila[2].ToString() });
                        }
                    }
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
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            vTipVentaS = comboBox6.SelectedValue.ToString();
            FrmAddItemVenta frmAddItemVenta = new FrmAddItemVenta();
            frmAddItemVenta.WindowState = FormWindowState.Normal;
            frmAddItemVenta.ShowDialog(this);
        }

        private void calculaTotales()
        {
            //Totales
            double sumaTotal = 0;
            double sumaIgv = 0;
            double sumaDcto = 0;
            double sumaBruto = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                sumaDcto += Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());
                sumaIgv += Double.Parse(row.Cells[7].Value.ToString().Equals("") ? "0" : row.Cells[7].Value.ToString());
                //sumaPercep += Double.Parse(row.Cells[7].Value.ToString().Equals("") ? "0" : row.Cells[7].Value.ToString());
                sumaTotal += Double.Parse(row.Cells[8].Value.ToString().Equals("") ? "0" : row.Cells[8].Value.ToString());
            }
            label16.Text = sumaDcto.ToString("###,##0.000").Trim();
            label19.Text = sumaIgv.ToString("###,##0.00").Trim();
            label18.Text = sumaTotal.ToString("###,##0.00").Trim();
            sumaBruto = Double.Parse(label18.Text.ToString().Equals("") ? "0" : label18.Text.ToString()) - Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString());
            label20.Text = sumaBruto.ToString("###,##0.0000").Trim();
            label28.Text = "Total Productos: " + Grid1.RowCount;
        }

        private void calcularIgv()
        {
            double ImporteTotal = 0;
            double Igv = 0;
            double Bruto = 0;
            double valIgv = 0;

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
            if (ObjParametro.BuscarParametro(nParam1))
            {
                vTipAfecta = ObjParametro.Valor;
            }
            else
            {
                MessageBox.Show("Error no se puede aplicar IGV verifique configuración de parámetros", "SISTEMA");
                return;
            }
            if (vTipAfecta.Equals("001"))
            {
                ImporteTotal = Double.Parse(label18.Text.ToString().Equals("") ? "0" : label18.Text.ToString());
                Bruto = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString());
                Igv = (ImporteTotal * valIgv) / (100 + valIgv);
                label19.Text = Igv.ToString("###,##0.00").Trim();
                Bruto = ImporteTotal - Igv;
                label20.Text = Bruto.ToString("###,##0.00").Trim();
            }
            else
            {
                Bruto = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString());
                Igv = (Bruto * valIgv) / 100;
                label19.Text = Igv.ToString("###,##0.00").Trim();
                ImporteTotal = Bruto + Igv;
                label18.Text = ImporteTotal.ToString("###,##0.00").Trim();
            }
        }

        private void FrmAddVenta_Load(object sender, EventArgs e)
        {
            totArticulo = 0;
            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "CodAlmacen";
            comboBox1.DataSource = datosAlm.Tables[0];
            comboBox1.Text = nomAlmacen.ToString();


            //Buscar tipo de pago
            //Carga Tipo Codigo de barras
            string codCatBarras = "028";
            string tipBarras = "1";
            DataSet datosbarras = csql.dataset("Call SpCargarDetCat('" + codCatBarras + "','" + tipBarras + "')");

            cmbTipoPago.DisplayMember = "Descripcion";
            cmbTipoPago.ValueMember = "CodDetCat";
            cmbTipoPago.DataSource = datosbarras.Tables[0];

            //Carga Tipo de Venta

            string codCatTVenta = "022";
            string tipVentaTip = "1";
            DataSet datosTipVenta = csql.dataset("Call SpCargarDetCat('" + codCatTVenta + "','" + tipVentaTip + "')");
            comboBox6.DisplayMember = "Descripcion";
            comboBox6.ValueMember = "CodDetCat";
            comboBox6.DataSource = datosTipVenta.Tables[0];

            string nParam = "15";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox6.SelectedValue = ObjParametro.Valor.ToString();
            }

            //Carga Documento
            DataSet datosDoc = csql.dataset("Call SpDocBusModulo('" + codModulo.ToString() + "')");
            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "Codigo";
            comboBox2.DataSource = datosDoc.Tables[0];

            nParam = "2";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox2.SelectedValue = ObjParametro.Valor.ToString();
            }

            //Carga Moneda
            string codCatMon = "001";
            string tipPresMon = "2";
            DataSet datosMon = csql.dataset("Call SpCargarDetCat('" + codCatMon + "','" + tipPresMon + "')");
            comboBox3.DisplayMember = "DescCorta";
            comboBox3.ValueMember = "CodDetCat";
            comboBox3.DataSource = datosMon.Tables[0];

            nParam = "1";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox3.Text = ObjParametro.Valor.ToString();
            }

            //Carga Tipo Venta
            string codCatTVen = "015";
            string tipPresVen = "1";
            DataSet datosTCom = csql.dataset("Call SpCargarDetCat('" + codCatTVen + "','" + tipPresVen + "')");

            comboBox4.DisplayMember = "Descripcion";
            comboBox4.ValueMember = "CodDetCat";
            comboBox4.DataSource = datosTCom.Tables[0];

            if (valModTVen.Equals("CREDITO"))
            {
                comboBox4.Text = "CREDITO";
            }
            else
            {
                comboBox4.Text = "CONTADO";
                textBox5.Enabled = false;
                DTP2.Enabled = false;
            }

            //Carga Tipo de Cambio
            if (ObjTipoCambio.BuscarTipoCambio())
            {
                double tCambio = ObjTipoCambio.Valor;
                textBox4.Text = tCambio.ToString("###,##0.000").Trim();
            }
            else
            {
                MessageBox.Show("FALSE");
            }

            //Carga Documento
            string codCatTipDoc = "018";
            string tipPresDoc = "2";
            DataSet datosDocu = csql.dataset_cadena("Call SpCargarDetCat('" + codCatTipDoc + "','" + tipPresDoc + "')");
            comboBox5.DisplayMember = "DescCorta";
            comboBox5.ValueMember = "CodDetCat";
            comboBox5.DataSource = datosDocu.Tables[0];

            //Generar ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            //toolTip1.SetToolTip(this.button1, "Buscar Proveedor");
            toolTip1.SetToolTip(this.button2, "Nuevo Cliente");
            toolTip1.SetToolTip(this.button3, "Buscar Cliente");
            toolTip1.SetToolTip(this.button4, "Editar Registro");
            toolTip1.SetToolTip(this.button5, "Eliminar Registro");
            toolTip1.SetToolTip(this.button6, "Grabar Venta");
            toolTip1.SetToolTip(this.button8, "Salir");

            Cod = FrmVentas.nIdVenta.ToString();
            if (FrmVentas.nmVen == 'M')
            {
                LlenarCampos(Cod);
            }
            //Si esta Anulado o Eliminado se muestra el documento deshabilitado
            if (FrmVentas.EstDoc.Equals("N") || FrmVentas.EstDoc.Equals("E") && FrmVentas.nmVen == 'M')
            {
                comboBox1.Enabled = false;
                DTP1.Enabled = false;
                comboBox2.Enabled = false;
                textBox2.Enabled = false;
                textBox1.Enabled = false;
                textBox3.Enabled = false;
                textBox7.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                textBox10.Enabled = true;
                comboBox3.Enabled = false;
                textBox4.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
                textBox5.Enabled = false;
                DTP2.Enabled = false;
                //Grid1.Enabled = false;
                button6.Enabled = true;
                button5.Enabled = false;
                button4.Enabled = false;
                button7.Enabled = true;
                checkBox1.Enabled = false;
                //checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                //button8.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
                DTP1.Enabled = true;
                comboBox2.Enabled = true;
                textBox2.Enabled = true;
                textBox1.Enabled = true;
                textBox3.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                textBox10.Enabled = true;
                comboBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox7.Enabled = true;
                comboBox4.Enabled = true;
                comboBox6.Enabled = true;
                //textBox5.Enabled = true;
                //DTP2.Enabled = true;
                //Grid1.Enabled = false;
                button6.Enabled = true;
                button5.Enabled = true;
                button4.Enabled = true;
                button7.Enabled = true;
                checkBox1.Enabled = true;
                //checkBox2.Enabled = true;
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //Valida numeros
                if (//char.IsLetter(e.KeyChar) || //Letras
                char.IsSymbol(e.KeyChar) || //Símbolos
                char.IsWhiteSpace(e.KeyChar) || //Espaçio
                char.IsPunctuation(e.KeyChar)) //Pontuacion
                    e.Handled = true;

                if (e.KeyChar == 13)
                {
                    double Net = 0;
                    Net = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox1.Text = "";
                    else
                        textBox1.Text = Net.ToString("000").Trim();

                    string vSerie = "";
                    string vDoc = "";
                    string vMod = "VEN";
                    vSerie = textBox1.Text.ToString().Trim();
                    vDoc = comboBox2.SelectedValue.ToString();

                    if (ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
                    {
                        if (ObjSerie.BuscarDocSerie(vDoc, vSerie))
                        {
                            if (ObjSerie.Correla.Equals("S"))
                            {
                                textBox2.Enabled = false;
                                textBox2.Text = "Autogenerado";
                                textBox3.Focus();
                            }
                            else
                            {
                                textBox2.Enabled = true;
                                textBox2.Text = "";
                                textBox2.Focus();
                            }
                        }
                        else
                        {
                            textBox2.Enabled = true;
                            textBox2.Text = "";
                            textBox2.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Esta Serie no esta registrada para el Comprobante seleccionado", "SISTEMA");
                        textBox2.Enabled = true;
                        textBox2.Text = "";
                        textBox1.Focus();
                        return;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox1.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox3.Focus();
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
                comboBox5.Focus();
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                if (textBox6.Enabled == false)
                {
                    comboBox3.Focus();
                }
                else
                {
                    textBox6.Focus();
                }
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < textBox4.Text.Length; i++)
            {
                if (textBox4.Text[i] == '.')
                    IsDec = true;

                if (IsDec && nroDec++ >= 2)
                {
                    e.Handled = true;
                    //return;
                }
            }

            if (e.KeyChar >= 48 && e.KeyChar <= 57)
                e.Handled = false;
            else if (e.KeyChar == 46)
                e.Handled = (IsDec) ? true : false;
            else
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox4.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (comboBox4.SelectedValue.ToString().Equals("001"))
                {
                    button7.Focus();
                }
                else
                {
                    textBox5.Focus();
                }
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                DTP2.Focus();
            }
        }

        private void DTP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
            }
        }

        private void DTP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button7.Focus();
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
           
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox2.Text.ToString().Equals("") ? "0" : textBox2.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox2.Text = "";
                else
                    textBox2.Text = Net.ToString("00000000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox2.Focus();
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
                    textBox4.Text = Net.ToString("###,##0.000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox4.Focus();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                int index = Grid1.CurrentRow.Index;
                Grid1.Rows.RemoveAt(index);
                calculaTotales();
                if (checkBox1.Checked == true)
                {
                    calcularIgv();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string vDoc2 = comboBox2.SelectedValue.ToString();
            string vPref2 = "";

            if (ObjSerie.BuscarDocSerie(vDoc2, textBox1.Text))
            {
                vPref2 = ObjSerie.PrefijoSerie;

                if (vPref2.Equals("F") && (textBox10.TextLength < 11))
                {
                    MessageBox.Show("Debe seleccionar un número de RUC para este comprobante", "SISTEMA");
                    textBox10.Focus();
                    return;
                }
            }

            if (textBox10.TextLength > 8 && textBox10.TextLength < 11)
            {
                MessageBox.Show("Ingrese Codigo de Cliente (RUC) válido", "SISTEMA");
                textBox10.Focus();
                return;
            }

            if (textBox10.TextLength == 0 && textBox10.TextLength == 11)
            {
                MessageBox.Show("Ingrese Codigo de Cliente", "SISTEMA");
                textBox10.Focus();
                return;
            }

            if (textBox1.TextLength == 0 || textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Serie y Nro. de Comprobante", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox4.TextLength == 0)
            {
                MessageBox.Show("Ingrese Tipo de Cambio", "SISTEMA");
                textBox4.Focus();
                return;
            }

            if (comboBox4.SelectedValue.ToString().Equals("002"))
            {
                if (textBox5.TextLength == 0)
                {
                    MessageBox.Show("Ingrese días de crédito", "SISTEMA");
                    textBox5.Focus();
                    return;
                }
            }

            if (comboBox6.SelectedValue.ToString().Equals("04"))
            {
                double nDesc = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                if (nDesc == 0)
                {
                    MessageBox.Show("El tipo de venta es con descuento global se debe ingresar los descuentos del comprobante", "SISTEMA");
                    //textBox5.Focus();
                    return;
                }
            }
            else
            {
                double nDesc = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                if (nDesc > 0)
                {
                    MessageBox.Show("El tipo de venta no es con descuento global no debe ingresar valores para los descuentos", "SISTEMA");
                    //textBox5.Focus();
                    return;
                }
            }

            if (comboBox6.SelectedValue.ToString().Equals("02"))
            {
                double nVIgv = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString().Trim());
                if (nVIgv > 0)
                {
                    MessageBox.Show("El tipo de venta es exonerado no permite valores con IGV", "SISTEMA");
                    //textBox5.Focus();
                    return;
                }
            }

            //Valida serie de documento
            string vSerie = "";
            string vDoc = "";
            string vMod = "VEN";
            string vPref = "";
            vSerie = textBox1.Text.ToString().Trim();
            vDoc = comboBox2.SelectedValue.ToString();

            if (!ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
            {
                MessageBox.Show("Esta Serie no esta registrada para el Comprobante seleccionado", "SISTEMA");
                textBox1.Focus();
                return;
            }
            else
            {
                if (ObjSerie.BuscarDocSerie(vDoc, vSerie))
                {
                    vPref = ObjSerie.PrefijoSerie;
                }

                if (vPref.Equals("F") && (textBox10.TextLength < 11))
                {
                    MessageBox.Show("Debe seleccionar un número de RUC para este comprobante", "SISTEMA");
                    textBox10.Focus();
                    return;
                }

                if (vPref.Equals("B") && (textBox10.TextLength == 11))
                {
                    MessageBox.Show("No puede seleccionar RUC para este comprobante", "SISTEMA");
                    textBox10.Focus();
                    return;
                }
            }

            if (comboBox6.Text.Equals(""))
            {
                MessageBox.Show("Seleccione el tipo de venta a SUNAT", "SISTEMA");
                comboBox6.Focus();
                return;
            }

            if (Grid1.RowCount <= 0)
            {
                MessageBox.Show("Ingrese Artículos", "SISTEMA");
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar la Venta", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            //Datos de Cabecera de Venta
            //Generamos codigo de venta
            string vIdVenta = "";
            //Verificamos si es autonumerico la serie
            string codComp = comboBox2.SelectedValue.ToString();
            int vNumSerie = 0;
            string vNumero = "";
            //Verificamos cliente y creamos
            string vTipDoc = "";
            string vNume = "";
            if (textBox6.Enabled == true && textBox6.TextLength > 0 && textBox10.TextLength == 0)
            {
                int vLong = textBox10.TextLength;
                switch (vLong)
                {
                    case 8:
                        ObjCliente.TipDoc = "1";
                        vTipDoc = "1";
                        ObjCliente.RucDni = textBox10.Text.ToString();
                        break;

                    case 11:
                        ObjCliente.TipDoc = "6";
                        vTipDoc = "6";
                        ObjCliente.RucDni = textBox10.Text.ToString();
                        break;

                    default:

                        ObjCliente.TipDoc = "6";
                        vTipDoc = "";
                        //Buscar ultimo cliente
                        int vValor = 0;

                        if (ObjCliente.BuscarCLienteNum(rucEmpresa.ToString()))
                        {
                            vValor = ObjCliente.ValorCli + 1;
                        }

                        vNume = "C" + vValor.ToString("0000000000").Trim();
                        //------------------
                        ObjCliente.RucDni = vNume.ToString();
                        //textBox10.Text = ObjCliente.RucDni;
                        break;
                }
                ObjCliente.Nombre = textBox6.Text;
                ObjCliente.Direccion = textBox7.Text;
                ObjCliente.Telefono = "";
                ObjCliente.Celular = "";
                ObjCliente.Contacto = "";
                ObjCliente.Email = "";
                ObjCliente.DireccionEnvio = "";
                ObjCliente.Region = "";
                ObjCliente.Provincia = "";
                ObjCliente.Distrito = "";
                ObjCliente.LimCredito = 0;
                ObjCliente.CodVendedor = "";
                ObjCliente.Est = "A";
                ObjCliente.UsuarioCrea = Usuario.ToString();
                ObjCliente.UsuarioModi = Usuario.ToString();
                ObjCliente.RucEmpresa = rucEmpresa.ToString();
                if (ObjCliente.ValidarCliente(ObjCliente.RucDni, rucEmpresa.ToString()))
                {
                    MessageBox.Show("Cliente ya existe, ingrese otro Cliente", "SISTEMA");
                    textBox10.Focus();
                    return;
                }

                if (ObjCliente.Crear())
                {
                    textBox10.Text = vNume.ToString();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente el cliente", "SISTEMA");
                    return;
                }
            }
            else
            {
                if (textBox10.TextLength == 0)
                {
                    textBox10.Text = "C0000000001";
                    vTipDoc = "1";
                }
            }

            //-----------------------------------------------
            if (textBox2.Enabled == false)
            {
                if (ObjSerie.BuscarDocSerie(codComp, vSerie))
                {
                    vNumSerie = ObjSerie.Numero;
                }

                vNumSerie = vNumSerie + 1;

                if (ObjSerie.ActualizaCorrela(codComp, vSerie))
                {
                }
                else
                {
                    return;
                }
                vNumero = vNumSerie.ToString("00000000").Trim();
            }
            else
            {
                vNumero = textBox2.Text.ToString();
            }
            vIdVenta = textBox10.Text.ToString() + comboBox2.SelectedValue.ToString() + textBox1.Text.ToString() + "-" + vNumero.ToString() + comboBox1.SelectedValue.ToString() + rucEmpresa.ToString();
            if (FrmVentas.nmVen == 'M')
            {
                //Cod.ToString()
                ObjVenta.IdVenta = Cod.ToString();
            }
            else
            {
                string cDoc = comboBox2.SelectedValue.ToString();
                if (ObjVenta.BuscarComprobate(cDoc.ToString(), textBox1.Text.ToString(), textBox2.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    MessageBox.Show("Comprobante registrado, Verifique", "SISTEMA");
                    textBox1.Focus();
                    return;
                }

                ObjVenta.IdVenta = vIdVenta.ToString();
            }
            ObjVenta.Fecha = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
            ObjVenta.Doc = comboBox2.SelectedValue.ToString();
            ObjVenta.Serie = textBox1.Text.ToString();
            ObjVenta.Numero = vNumero.ToString();
            ObjVenta.NPedido = textBox3.Text.ToString();
            ObjVenta.Cliente = textBox10.Text.ToString();
            if (vTipDoc.Equals(""))
            {
                ObjVenta.TipDocCli = comboBox5.SelectedValue.ToString();
            }
            else
            {
                ObjVenta.TipDocCli = vTipDoc.ToString();
            }

            ObjVenta.TMoneda = comboBox3.SelectedValue.ToString();
            ObjVenta.TCambio = Double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
            ObjVenta.TVenta = comboBox4.SelectedValue.ToString();
            ObjVenta.TComp = comboBox6.SelectedValue.ToString();
            ObjVenta.TipoPago = null;
            string aniofv = "1900";
            string mesfv = "01";
            string diafv = "01";
            if (comboBox4.SelectedValue.ToString().Equals("002"))
            {
                ObjVenta.NDias = Int32.Parse(textBox5.Text.ToString().Equals("") ? "0" : textBox5.Text.ToString().Trim());
                ObjVenta.FVence = DTP2.Value.Year.ToString() + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Day.ToString("00");
                ObjVenta.TEst = "P";
            }
            else { ObjVenta.NDias = 0; ObjVenta.FVence = aniofv.ToString() + "/" + mesfv.ToString() + "/" + diafv.ToString(); ObjVenta.TEst = "C"; }
            //Verifica si aplica IGV
            string vAplicaIgv = "";
            string nParam = "7";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vAplicaIgv = ObjParametro.Valor;
            }
            else
            {
                vAplicaIgv = "";
            }

            if (vAplicaIgv.Equals("S"))
            {
                if (!comboBox6.SelectedValue.ToString().Equals("03"))
                {
                    if (comboBox6.SelectedValue.ToString().Equals("02"))
                    {
                        ObjVenta.TBruto = 0;
                        ObjVenta.TExonerada = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                        ObjVenta.TIgv = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString().Trim());
                        ObjVenta.TGratuita = 0;
                        ObjVenta.Egratuita = "0";
                        ObjVenta.Dcto = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                    }
                    else
                    {
                        if (comboBox6.SelectedValue.ToString().Equals("01"))
                        {
                            ObjVenta.TBruto = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                            ObjVenta.TExonerada = 0;
                            ObjVenta.TIgv = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString().Trim());
                            ObjVenta.TGratuita = 0;
                            ObjVenta.Egratuita = "0";
                            ObjVenta.Dcto = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                        }
                        else
                        {
                            ObjVenta.TBruto = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                            ObjVenta.TExonerada = 0;
                            ObjVenta.TIgv = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString().Trim());
                            ObjVenta.TGratuita = 0;
                            ObjVenta.Egratuita = "0";
                            ObjVenta.Dcto = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                        }
                    }
                }
                else
                {
                    ObjVenta.TBruto = 0;
                    ObjVenta.TExonerada = 0;
                    ObjVenta.TIgv = 0;
                    ObjVenta.TGratuita = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                    ObjVenta.Egratuita = "1";
                    ObjVenta.Dcto = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                }
            }
            else
            {
                if (!comboBox6.SelectedValue.ToString().Equals("03"))
                {
                    if (comboBox6.SelectedValue.ToString().Equals("02"))
                    {
                        ObjVenta.TBruto = 0;
                        ObjVenta.TExonerada = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                        ObjVenta.TIgv = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString().Trim());
                        ObjVenta.TGratuita = 0;
                        ObjVenta.Egratuita = "0";
                        ObjVenta.Dcto = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                    }
                    else
                    {
                        if (comboBox6.SelectedValue.ToString().Equals("01"))
                        {
                            ObjVenta.TBruto = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                            ObjVenta.TExonerada = 0;
                            ObjVenta.TIgv = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString().Trim());
                            ObjVenta.TGratuita = 0;
                            ObjVenta.Egratuita = "0";
                            ObjVenta.Dcto = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                        }
                        else
                        {
                            ObjVenta.TBruto = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                            ObjVenta.TExonerada = 0;
                            ObjVenta.TIgv = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString().Trim());
                            ObjVenta.TGratuita = 0;
                            ObjVenta.Egratuita = "0";
                            ObjVenta.Dcto = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                        }
                    }
                }
                else
                {
                    ObjVenta.TBruto = 0;
                    ObjVenta.TExonerada = 0;
                    ObjVenta.TIgv = 0;
                    ObjVenta.TGratuita = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                    ObjVenta.Egratuita = "1";
                    ObjVenta.Dcto = Double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                }
            }
            ObjVenta.Total = Double.Parse(label18.Text.ToString().Equals("") ? "0" : label18.Text.ToString().Trim());
            if (checkBox3.Checked == true)
            {
                ObjVenta.Est = "N";
            }
            else
            {
                ObjVenta.Est = "A";
            }

            ObjVenta.Empresa = rucEmpresa.ToString();
            ObjVenta.Almacen = codAlmacen.ToString();
            ObjVenta.Vendedor = "ADMIN";
            ObjVenta.Usuario = Usuario.ToString();
            ObjVenta.UserCreacion = Usuario.ToString();
            ObjVenta.UserModi = Usuario.ToString();
            /*-------------------------------------------------------------------------*/
            /*Datos Venta Detalle-----------------------------------------------------*/
            //En caso sea una modificacion
            if (FrmVentas.nmVen == 'M')
            {
                ObjVenta.DevolverStock(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
                ObjDetVenta.Eliminar(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
            }
            //----------------------------------------------------------------------
            int nOrden = 1;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (FrmVentas.nmVen == 'M')
                {
                    ObjDetVenta.IdVenta = Cod.ToString();
                }
                else
                {
                    ObjDetVenta.IdVenta = vIdVenta.ToString();
                }
                ObjDetVenta.Codigo = row.Cells[0].Value.ToString();
                string vParam = "1";
                string vCodCat = "010";
                if (ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), row.Cells[2].Value.ToString().Trim(), vParam.ToString()))
                {
                    ObjDetVenta.Marca = ObjDetCatalogo.CodDetCat;
                }
                else
                {
                    ObjDetVenta.Marca = "";
                }

                vCodCat = "013";
                if (ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), row.Cells[3].Value.ToString().Trim(), vParam.ToString()))
                {
                    ObjDetVenta.Unidad = ObjDetCatalogo.CodDetCat;
                }
                else
                {
                    ObjDetVenta.Unidad = "";
                }

                vCodCat = "009";
                //Verifica si aplica IGV
                vAplicaIgv = "";
                nParam = "7";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vAplicaIgv = ObjParametro.Valor;
                }
                else
                {
                    vAplicaIgv = "";
                }

                if (vAplicaIgv.Equals("S"))
                {
                    vTipPrecio = "01";
                    if (comboBox6.SelectedValue.ToString().Equals("03"))
                    {
                        vTipImp = "11";
                    }
                    else
                    {
                        vTipImp = "10";
                    }
                }
                else
                {
                    vTipPrecio = "01";
                    if (comboBox6.SelectedValue.ToString().Equals("03"))
                    {
                        vTipImp = "11";
                    }
                    else
                    {
                        vTipImp = "20";
                    }
                }

                if (ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), row.Cells[9].Value.ToString().Trim(), vParam.ToString()))
                {
                    ObjDetVenta.Proced = ObjDetCatalogo.CodDetCat;
                }
                else
                {
                    ObjDetVenta.Proced = "";
                }

                ObjDetVenta.PVenta = Double.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString());
                ObjDetVenta.Cantidad = Int32.Parse(row.Cells[4].Value.ToString().Equals("") ? "0" : row.Cells[4].Value.ToString());
                ObjDetVenta.Igv = Double.Parse(row.Cells[7].Value.ToString().Equals("") ? "0" : row.Cells[7].Value.ToString());
                ObjDetVenta.Dcto = Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());
                ObjDetVenta.Importe = Double.Parse(row.Cells[8].Value.ToString().Equals("") ? "0" : row.Cells[8].Value.ToString());
                ObjDetVenta.TipPrecio = vTipPrecio.ToString();
                ObjDetVenta.TipImpuesto = vTipImp.ToString();
                ObjDetVenta.Almacen = codAlmacen.ToString();
                ObjDetVenta.Empresa = rucEmpresa.ToString();
                ObjDetVenta.UserCreacion = Usuario.ToString();
                ObjDetVenta.UserModi = Usuario.ToString();
                ObjDetVenta.Norden = nOrden;
                ObjDetVenta.DescripServ = ""; //Reemplazar por Descripcion de Servicio
                if (checkBox3.Checked == true)
                {
                    ObjDetVenta.Est = "N";
                }
                else
                {
                    ObjDetVenta.Est = "A";
                }

                nOrden += 1;
                if (!ObjDetVenta.Crear())
                {
                    MessageBox.Show("No se registro correctamente el detalle de la compra", "SISTEMA");
                    return;
                }
            }

            /*-------------------------------------------------------------------------*/

            if (FrmVentas.nmVen == 'N')
            {
                if (ObjVenta.Crear())
                {
                    string comprobante = comboBox2.SelectedValue.ToString();

                    if (ObjDocumento.BuscarDoc(comprobante.ToString()))
                    {
                        if (ObjDocumento.Imp.Equals("S"))
                        {
                            if (ObjSerie.BuscarDocSerie(comprobante.ToString(), textBox1.Text.ToString().Trim()))
                            {
                                if ((ObjSerie.Formato_Imp.Equals("") && ObjSerie.Formato_Imp == null) || (ObjSerie.Impresora.Equals("") && ObjSerie.Impresora == null))
                                {
                                    if (ObjDocumento.Formato_Imp.Equals("001") || ObjDocumento.Formato_Imp.Equals("") || ObjDocumento.Formato_Imp == null)
                                    {
                                        ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                    }
                                    else
                                    {
                                        ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false,"");
                                    }
                                } else
                                {
                                    if (ObjSerie.Formato_Imp.Equals("001") || ObjSerie.Formato_Imp.Equals("") || ObjSerie.Formato_Imp == null)
                                    {
                                        ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                    }
                                    else
                                    {
                                        ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, ObjSerie.Impresora);
                                    }
                                }
                            } else
                            {
                                if (ObjDocumento.Formato_Imp.Equals("001") || ObjDocumento.Formato_Imp.Equals("") || ObjDocumento.Formato_Imp == null)
                                {
                                    ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                }
                                else
                                {
                                    ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false,"");
                                }
                            }
                            
                        }
                    }

                    if (ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
                    {
                        if (ObjDocumento.EnvSunat.Equals("S"))
                        {
                            ObjGrabaXML.generaXMLFactura(ObjVenta.IdVenta, rucEmpresa, codAlmacen, true);
                            //ObjFormato.generaFormato(ObjVenta.IdVenta, rucEmpresa, codAlmacen);
                        }
                    }

                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    //this.Close();
                    textBox10.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    //textBox4.Clear();
                    textBox5.Clear();
                    Grid1.Rows.Clear();
                    label18.Text = "";
                    label19.Text = "";
                    label20.Text = "";
                    checkBox1.Checked = false;
                    //checkBox2.Checked = false;
                    checkBox3.Checked = false;
                    textBox6.Enabled = true;
                    textBox7.Enabled = true;
                    textBox2.Enabled = true;
                    totArticulo = 0;
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjVenta.Modificar())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    textBox10.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    //textBox4.Clear();
                    textBox5.Clear();
                    Grid1.Rows.Clear();
                    label18.Text = "";
                    label19.Text = "";
                    label20.Text = "";
                    checkBox1.Checked = false;
                    string val = "V";
                    Opener.CargarConsulta(val);
                    totArticulo = 0;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            string tipDoc = comboBox2.SelectedValue.ToString();
            string codCli = textBox10.Text.ToString().Trim();
            BusCliente(tipDoc, codCli, rucEmpresa);
            //Solo para comprobante electronico
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBusCliente frmBusCliente = new FrmBusCliente();
            frmBusCliente.WindowState = FormWindowState.Normal;
            frmBusCliente.Opener = this;
            frmBusCliente.MdiParent = this.MdiParent;
            frmBusCliente.ShowDialog();
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue.ToString().Equals("001"))
            {
                textBox5.Enabled = false;
                DTP2.Enabled = false;
            }
            else
            {
                textBox5.Enabled = true;
                DTP2.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedValue.ToString().Equals("001"))
            {
                textBox5.Enabled = false;
                DTP2.Enabled = false;
            }
            else
            {
                textBox5.Enabled = true;
                DTP2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmCliente.nmCLi = 'N';
            FrmAddCliente frmAddCliente = new FrmAddCliente();
            frmAddCliente.WindowState = FormWindowState.Normal;
            frmAddCliente.ShowDialog(this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox10.Focus();
            }
        }

        private void comboBox2_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                string vSerie = "";
                string vDoc = "";
                string vMod = "VEN";
                vSerie = textBox1.Text.ToString().Trim();
                vDoc = comboBox2.SelectedValue.ToString();

                if (ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
                {
                    if (ObjSerie.BuscarDocSerie(vDoc, vSerie))
                    {
                        if (ObjSerie.Correla.Equals("S"))
                        {
                            textBox2.Enabled = false;
                            textBox2.Text = "Autogenerado";
                        }
                        else
                        {
                            textBox2.Enabled = true;
                            textBox2.Text = "";
                        }
                    }
                    else
                    {
                        textBox2.Enabled = true;
                        textBox2.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("La Serie ingresada no esta registrada para el Comprobante seleccionado", "SISTEMA");
                    return;
                }
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {
            bool tieneIgv = false;

            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    if (row.Cells[6].Value.ToString().Equals(""))
                    {
                        tieneIgv = false;
                    }
                    else
                    {
                        tieneIgv = true;
                    }
                }

                if (tieneIgv == true)
                {
                    MessageBox.Show("La lista de productos tiene montos de Igv afectos, no se puede afectar IGV", "SISTEMA");
                    checkBox1.Checked = false;
                    return;
                }
                else
                {
                    calcularIgv();
                }
            }
            else
            {
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
                if (vTipAfecta.Equals("001"))
                {
                    double ImporteTotal = 0;
                    double Igv = 0;
                    double Bruto = 0;

                    ImporteTotal = Double.Parse(label18.Text.ToString().Equals("") ? "0" : label18.Text.ToString());
                    Igv = 0;
                    label19.Text = Igv.ToString("###,##0.00").Trim();
                    Bruto = ImporteTotal - Igv;
                    label20.Text = Bruto.ToString("###,##0.00").Trim();
                }
                else
                {
                    double ImporteTotal = 0;

                    double Igv = 0;
                    double Bruto = 0;

                    Bruto = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString());
                    Igv = 0;
                    label19.Text = Igv.ToString("###,##0.00").Trim();

                    ImporteTotal = Bruto + Igv;
                    label18.Text = ImporteTotal.ToString("###,##0.00").Trim();
                }
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                FrmBusCliente frmBusCliente = new FrmBusCliente();
                frmBusCliente.WindowState = FormWindowState.Normal;
                frmBusCliente.Opener = this;
                frmBusCliente.MdiParent = this.MdiParent;
                frmBusCliente.ShowDialog();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox7.Focus();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox3.Focus();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button6_Validated(object sender, EventArgs e)
        {
        }

        private void textBox10_Validated(object sender, EventArgs e)
        {
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}