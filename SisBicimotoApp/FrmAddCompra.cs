using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddCompra : Form, IProveedor, IAddItemCompra
    {
        //public IAddItem Opener { get; set; }
        public ICompra Opener { get; set; }

        public static Boolean validaIgv = false;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string codModulo = "COM";
        private string valModTCpm = "";
        private string Cod = "";
        private string Cod2 = "";
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsProveedor ObjProveedor = new ClsProveedor();
        private ClsTipoCambio ObjTipoCambio = new ClsTipoCambio();
        private ClsCompra ObjCompra = new ClsCompra();
        private ClsDetCompra ObjDetCompra = new ClsDetCompra();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsCodigoBarras ObjCodigoBarras = new ClsCodigoBarras();
        private ClsAlmacen ObjAlmacen = new ClsAlmacen();
        private ClsProducto ObjProducto = new ClsProducto();
        private string codigo = "";
        private string nombre = "";
        private string cantidad = "";

        public FrmAddCompra()
        {
            InitializeComponent();
        }

        #region IAddItem Members

        /*public void AddNewItem(DataGridViewRow row)
        {
            string item = row.Cells["item"].Value.ToString();
            string desc = row.Cells["Desc"].Value.ToString();

            this.Grid1.Rows.Add(new[] { item, desc });
        }*/

        public void AddNewItem(string codPro, string nomPro, string undMed, double preCosto, double cantidad, double igv, double pordesc, double desc, double percep, double importe)
        {
            string codigo = codPro.ToString();
            string producto = nomPro.ToString();
            string unidad = undMed.ToString();
            string pCosto = "";
            string vCantidad = "";
            string vDcto = "";
            string vIgv = "";
            double valIgv = 0;
            double igvItem = 0;
            string vPercep = "";
            string vImp = "";
            string vPorDesc = "";
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

            if (preCosto.ToString().Trim().Equals("0"))
                pCosto = "";
            else
                pCosto = preCosto.ToString("###,##0.0000").Trim();

            if (cantidad.ToString().Trim().Equals("0"))
                vCantidad = "";
            else
                vCantidad = cantidad.ToString().Trim();

            if (desc.ToString().Trim().Equals("0"))
                vDcto = "";
            else
                vDcto = desc.ToString("###,##0.00").Trim();

            if (igv.ToString().Trim().Equals("0"))
                vIgv = "";
            else
                vIgv = igv.ToString("###,##0.0000").Trim();

            if (percep.ToString().Trim().Equals("0"))
                vPercep = "";
            else
                vPercep = percep.ToString("###,##0.0000").Trim();

            if (importe.ToString().Trim().Equals("0"))
                vImp = "";
            else
                vImp = importe.ToString("###,##0.000").Trim();

            if (pordesc.ToString().Trim().Equals("0"))
                vPorDesc = "";
            else
                vPorDesc = pordesc.ToString().Trim();

            //Validar IGV
            Boolean tieneIgv = false;
            validaIgv = false;
            if (Grid1.RowCount > 0)
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
            double vCosto = 0;
            int nIndex = 0;
            int contador = 0;
            double cantArti = 0;
            double preUnit = 0;
            double valPorDesc = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                vCosto = Double.Parse(row.Cells[4].Value.ToString().Equals("") ? "0" : row.Cells[4].Value.ToString());
                valPorDesc = Double.Parse(row.Cells[9].Value.ToString().Equals("") ? "0" : row.Cells[9].Value.ToString());
                if (row.Cells[0].Value.ToString().Equals(codPro) && vCosto == preCosto && pordesc == valPorDesc)
                {
                    existe = true;
                    cantArti = Double.Parse(row.Cells[3].Value.ToString());

                    preUnit = Double.Parse(row.Cells[4].Value.ToString());
                    nIndex = contador;
                }
                contador += contador + 1;
            }
            double newCant = 0;
            double newImporte = 0;
            double newDesc = 0;
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
                if (percep > 0)
                {
                    percep = newImporte * 0.02;
                }

                if (pordesc > 0)
                {
                    newDesc = (newImporte * pordesc) / 100;
                }
                newImporte = newImporte + igvItem + percep - newDesc;
                Grid1[3, nIndex].Value = newCant;

                Grid1[6, nIndex].Value = igvItem == 0 ? "" : igvItem.ToString("###,##0.0000").Trim();
                Grid1[7, nIndex].Value = percep == 0 ? "" : percep.ToString("###,##0.0000").Trim();
                Grid1[8, nIndex].Value = newImporte.ToString("###,##0.0000").Trim();
            }
            else
            {
                this.Grid1.Rows.Add(new[] { codigo, producto, unidad, vCantidad, pCosto, vDcto, vIgv, vPercep, vImp, vPorDesc });
            }

            calculaTotales();

            if (checkBox1.Checked == true)
            {
                calcularIgv();
            }
        }

        #endregion IAddItem Members

        #region IProveedor Members

        public void SelectItem(string codProv)
        {
            textBox10.Text = codProv;
        }

        #endregion IProveedor Members

        private void LlenarCampos(string InCod)
        {
            try
            {
                codAlmacen = FrmCompras.almacenSelec;
                if (ObjCompra.BuscarCompra(InCod, rucEmpresa.ToString(), codAlmacen.ToString()))
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
                    DTP2.Text = ObjCompra.FIngAlmacen.ToString().Trim();
                    DTP3.Text = ObjCompra.FIngSistema.ToString().Trim();
                    DTP1.Text = ObjCompra.FIngDoc.ToString().Trim();
                    comboBox2.Text = ObjCompra.Doc.ToString().Trim();
                    textBox2.Text = ObjCompra.Serie.ToString().Trim();
                    textBox1.Text = ObjCompra.Numero.ToString().Trim();
                    textBox5.Text = ObjCompra.SerieGuia.ToString().Trim();
                    textBox4.Text = ObjCompra.NumGuia.ToString().Trim();
                    textBox6.Text = ObjCompra.NOrdenC.ToString().Trim();
                    textBox10.Text = ObjCompra.Proveedor.ToString().Trim();
                    comboBox3.Text = ObjCompra.TMoneda.ToString().Trim();

                    //Tipo de cambio
                    double Net = 0;
                    Net = double.Parse(ObjCompra.TCambio.ToString().Equals("") ? "0" : ObjCompra.TCambio.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox3.Text = "";
                    else
                        textBox3.Text = Net.ToString("###,##0.00").Trim();

                    textBox7.Text = ObjCompra.NDias.ToString().Trim();
                    if (ObjCompra.TCompra.ToString().Equals("CRE"))
                    {
                        DTP4.Text = ObjCompra.FVence.ToString();
                        comboBox4.Text = ObjCompra.TCompra.ToString().Trim();
                        comboBox4.SelectedValue = "002";
                        DTP4.Enabled = true;
                        textBox7.Enabled = true;
                        valModTCpm = "CRE";
                    }
                    //Flete
                    Net = double.Parse(ObjCompra.Flete.ToString().Equals("") ? "0" : ObjCompra.Flete.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox8.Text = "";
                    else
                        textBox8.Text = Net.ToString("###,##0.00").Trim();
                    //Otros Cargos
                    Net = double.Parse(ObjCompra.OtrosCargos.ToString().Equals("") ? "0" : ObjCompra.OtrosCargos.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox11.Text = "";
                    else
                        textBox11.Text = Net.ToString("###,##0.00").Trim();

                    textBox9.Text = ObjCompra.Observaciones.ToString().Trim();
                    //Total bruto
                    Net = double.Parse(ObjCompra.TBruto.ToString().Equals("") ? "0" : ObjCompra.TBruto.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label22.Text = "0.0000";
                    else
                        label22.Text = Net.ToString("###,##0.0000").Trim();
                    //Total Dcto
                    Net = double.Parse(ObjCompra.TDcto.ToString().Equals("") ? "0" : ObjCompra.TDcto.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label21.Text = "0.0000";
                    else
                        label21.Text = Net.ToString("###,##0.0000").Trim();
                    //Total Igv
                    Net = double.Parse(ObjCompra.TIgv.ToString().Equals("") ? "0" : ObjCompra.TIgv.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label20.Text = "0.0000";
                    else
                        label20.Text = Net.ToString("###,##0.0000").Trim();
                    if (ObjCompra.TIgv > 0)
                    {
                        checkBox1.Checked = true;
                    }
                    //Total Percep
                    Net = double.Parse(ObjCompra.TPercep.ToString().Equals("") ? "0" : ObjCompra.TPercep.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label19.Text = "0.0000";
                    else
                        label19.Text = Net.ToString("###,##0.0000").Trim();
                    //Importe Total
                    Net = double.Parse(ObjCompra.Total.ToString().Equals("") ? "0" : ObjCompra.Total.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label18.Text = "0.0000";
                    else
                        label18.Text = Net.ToString("###,##0.0000").Trim();

                    //Obtener Detalles
                    DataSet datos = csql.dataset_cadena("Call SpDetCompraBuscar('" + InCod.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                    string vPunit = "";
                    string vDcto = "";
                    string vIgv = "";
                    string vPercep = "";
                    string vImporte = "";
                    if (datos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in datos.Tables[0].Rows)
                        {
                            Net = double.Parse(fila[4].ToString().Equals("") ? "0" : fila[4].ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                                vPunit = "";
                            else
                                vPunit = Net.ToString("###,##0.0000").Trim();

                            Net = double.Parse(fila[5].ToString().Equals("") ? "0" : fila[5].ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                                vDcto = "";
                            else
                                vDcto = Net.ToString("###,##0.0000").Trim();

                            Net = double.Parse(fila[6].ToString().Equals("") ? "0" : fila[6].ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                                vIgv = "";
                            else
                                vIgv = Net.ToString("###,##0.0000").Trim();

                            Net = double.Parse(fila[7].ToString().Equals("") ? "0" : fila[7].ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                                vPercep = "";
                            else
                                vPercep = Net.ToString("###,##0.0000").Trim();

                            Net = double.Parse(fila[8].ToString().Equals("") ? "0" : fila[8].ToString().Trim());
                            if (Net.ToString().Trim().Equals("0"))
                                vImporte = "";
                            else
                                vImporte = Net.ToString("###,##0.0000").Trim();

                            this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), vPunit, vDcto, vIgv, vPercep, vImporte, fila[9].ToString() });
                        }
                    }


                }
                else
                {
                    MessageBox.Show("FALSE");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se puede cargar datos: " + ex.Message);
            }
        }



        private void calculaTotales()
        {
            //Totales
            double sumaTotal = 0;
            double sumaPercep = 0;
            double sumaIgv = 0;
            double sumaDcto = 0;
            double sumaBruto = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                sumaDcto += Double.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString());
                sumaIgv += Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());
                sumaPercep += Double.Parse(row.Cells[7].Value.ToString().Equals("") ? "0" : row.Cells[7].Value.ToString());
                sumaTotal += Double.Parse(row.Cells[8].Value.ToString().Equals("") ? "0" : row.Cells[8].Value.ToString());
            }
            label21.Text = sumaDcto.ToString("###,##0.0000").Trim();
            label20.Text = sumaIgv.ToString("###,##0.000").Trim();
            label19.Text = sumaPercep.ToString("###,##0.0000").Trim();
            label18.Text = sumaTotal.ToString("###,##0.000").Trim();
            sumaBruto = Double.Parse(label18.Text.ToString().Equals("") ? "0" : label18.Text.ToString()) - Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString()) - Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString()) + Double.Parse(label21.Text.ToString().Equals("") ? "0" : label21.Text.ToString());
            label22.Text = sumaBruto.ToString("###,##0.000").Trim();
            label28.Text = "Total Productos: " + Grid1.RowCount;
        }

        private void calcularIgv()
        {
            double ImporteTotal = 0;
            double Percep = 0;
            double Igv = 0;
            double Dcto = 0;
            double Bruto = 0;
            double valIgv = 0;
            double vValorIgv = 0;

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

            if (checkBox1.Checked == true)
            {
                Dcto = Double.Parse(label21.Text.ToString().Equals("") ? "0" : label21.Text.ToString());
                Percep = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString());
                ImporteTotal = Double.Parse(label18.Text.ToString().Equals("") ? "0" : label18.Text.ToString());

                vValorIgv = (ImporteTotal * valIgv) / (100 + valIgv);
                //vPrec = (vImporte - vValorIgv) / vCantidad;
                Bruto = ImporteTotal - vValorIgv;
                label22.Text = Bruto.ToString("###,##0.0000").Trim();
                label20.Text = vValorIgv.ToString("###,##0.0000").Trim();
                //textBox3.Text = vPrec.ToString("###,##0.00").Trim();
                label18.Text = ImporteTotal.ToString("###,##0.0000").Trim();
            }
            else
            {
                Dcto = Double.Parse(label21.Text.ToString().Equals("") ? "0" : label21.Text.ToString());
                Percep = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString());
                ImporteTotal = Double.Parse(label18.Text.ToString().Equals("") ? "0" : label18.Text.ToString());
                vValorIgv = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString());
                Bruto = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString());

                Bruto = Bruto + vValorIgv;
                label22.Text = Bruto.ToString("###,##0.0000").Trim();
                vValorIgv = 0;
                label20.Text = vValorIgv.ToString("###,##0.0000").Trim();
            }
        }

        private void BusProveedor(string vRuc, string vRucEmpresa)
        {
            if (ObjProveedor.BuscarProveedor(vRuc, vRucEmpresa))
            {
                label16.Text = ObjProveedor.Nombre.ToString().Trim();
            }
            else
            {
                label16.Text = "";
            }
        }

        private void FrmAddCompra_Load(object sender, EventArgs e)
        {
            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "CodAlmacen";
            comboBox1.DataSource = datosAlm.Tables[0];
            comboBox1.Text = nomAlmacen.ToString();

            //Carga Documento
            DataSet datosDoc = csql.dataset("Call SpDocBusModulo('" + codModulo.ToString() + "')");
            comboBox2.DisplayMember = "ncorto";
            comboBox2.ValueMember = "Codigo";
            comboBox2.DataSource = datosDoc.Tables[0];

            //Carga Moneda
            string codCatMon = "001";
            string tipPresMon = "2";
            DataSet datosMon = csql.dataset("Call SpCargarDetCat('" + codCatMon + "','" + tipPresMon + "')");

            comboBox3.DisplayMember = "DescCorta";
            comboBox3.ValueMember = "CodDetCat";
            comboBox3.DataSource = datosMon.Tables[0];

            string nParam = "1";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox3.Text = ObjParametro.Valor.ToString();
            }

            //Carga Tipo Compra
            string codCatTCom = "014";
            string tipPresCom = "2";
            DataSet datosTCom = csql.dataset("Call SpCargarDetCat('" + codCatTCom + "','" + tipPresCom + "')");

            comboBox4.DisplayMember = "DescCorta";
            comboBox4.ValueMember = "CodDetCat";
            comboBox4.DataSource = datosTCom.Tables[0];

            //Carga Tipo Codigo de barras
            string codCatBarras = "027";
            string tipBarras = "1";
            DataSet datosbarras = csql.dataset("Call SpCargarDetCat('" + codCatBarras + "','" + tipBarras + "')");

            cmbTipCod.DisplayMember = "Descripcion";
            cmbTipCod.ValueMember = "CodDetCat";
            cmbTipCod.DataSource = datosbarras.Tables[0];

            if (valModTCpm.Equals("CRE"))
            {
                comboBox4.Text = "CRE";
            }

            //Carga Tipo de Cambio
            if (ObjTipoCambio.BuscarTipoCambio())
            {
                double tCambio = ObjTipoCambio.Valor;
                textBox3.Text = tCambio.ToString("###,##0.000").Trim();
            }
            else
            {
                MessageBox.Show("FALSE");
            }

            //Generar ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.button1, "Buscar Proveedor");
            toolTip1.SetToolTip(this.button2, "Nuevo Proveedor");
            toolTip1.SetToolTip(this.button3, "Agregar Producto");
            toolTip1.SetToolTip(this.button4, "Editar Registro");
            toolTip1.SetToolTip(this.button5, "Eliminar Registro");
            toolTip1.SetToolTip(this.button6, "Grabar Compra");
            toolTip1.SetToolTip(this.button7, "Salir");

            Cod = FrmCompras.nIdCompra.ToString();
            if (FrmCompras.nmCom == 'M')
            {
                LlenarCampos(Cod);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmAddItemCompra frmAddItemCompra = new FrmAddItemCompra();
            frmAddItemCompra.WindowState = FormWindowState.Normal;
            frmAddItemCompra.ShowDialog(this);
        }

        private void DTP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DTP2.Focus();
            }
        }

        private void DTP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox10.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBusProveedorCon frmBusProveedor = new FrmBusProveedorCon();
            frmBusProveedor.WindowState = FormWindowState.Normal;
            frmBusProveedor.Opener = this;
            frmBusProveedor.MdiParent = this.MdiParent;
            frmBusProveedor.ShowDialog();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            string rucProv = textBox10.Text.ToString().Trim();
            BusProveedor(rucProv, rucEmpresa);
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
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //PUntuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox3.SelectionStart = 0;
                textBox3.SelectionLength = textBox3.TextLength;
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox5.Focus();
            }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox3.Text = "";
                else
                    textBox3.Text = Net.ToString("###,##0.000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox3.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox6.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox4.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox4.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                if (comboBox4.SelectedValue.ToString().Equals("001"))
                {
                    textBox8.Focus();
                }
                else
                {
                    textBox7.Focus();
                }
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                DTP4.Focus();
            }
        }

        private void DTP4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox8.Focus();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox11.Focus();
            }
        }

        private void textBox8_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox8.Text.ToString().Equals("") ? "0" : textBox8.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox8.Text = "";
                else
                    textBox8.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox8.Focus();
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox9.Focus();
            }
        }

        private void textBox11_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox11.Text.ToString().Equals("") ? "0" : textBox11.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox11.Text = "";
                else
                    textBox11.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox11.Focus();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button3.Focus();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            try
            {

            }
            catch (System.Exception ex)
            {

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
                    textBox1.Text = Net.ToString("00000000000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox1.Focus();
            }
        }

        private void textBox5_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox5.Text.ToString().Equals("") ? "0" : textBox5.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox5.Text = "";
                else
                    textBox5.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox5.Focus();
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
                    textBox4.Text = Net.ToString("00000000000").Trim();
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
                
                
                calculaTotales();
                if (checkBox1.Checked == true)
                {
                    calcularIgv();
                }               

                for (int i = dgvOculto.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dgvOculto.Rows[i];
                    
                    if (Grid1.Rows[index].Cells[0].Value.ToString().Equals(row.Cells[0].Value.ToString())  && Double.Parse(Grid1.Rows[index].Cells[4].Value.ToString()) == Double.Parse(row.Cells[4].Value.ToString()))
                    {
                        dgvOculto.Rows.Remove(row);
                    }
                    else
                    {

                    }
                }

                Grid1.Rows.RemoveAt(index);



            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bool tieneIgv = false;

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
                return;
            }
            else
            {
                calcularIgv();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmProveedor.nmPro = 'N';
            FrmAddProveedor frmAddProveedor = new FrmAddProveedor();
            frmAddProveedor.WindowState = FormWindowState.Normal;
            frmAddProveedor.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (label16.Text.Equals(""))
                {
                    MessageBox.Show("Ingrese Proveedor", "SISTEMA");
                    textBox10.Focus();
                    return;
                }

                if (textBox2.TextLength == 0 || textBox1.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Serie y Nro. de Comprobante", "SISTEMA");
                    textBox2.Focus();
                    return;
                }

                if (textBox3.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Tipo de Cambio", "SISTEMA");
                    textBox3.Focus();
                    return;
                }

                if (comboBox4.SelectedValue.ToString().Equals("002"))
                {
                    if (textBox7.TextLength == 0)
                    {
                        MessageBox.Show("Ingrese días de crédito", "SISTEMA");
                        textBox7.Focus();
                        return;
                    }
                }

                if (Grid1.RowCount <= 0)
                {
                    MessageBox.Show("Ingrese Artículos", "SISTEMA");
                    return;
                }

                if (MessageBox.Show("Datos Correctos, se procedera a registrar la compra", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                string Usuario = FrmLogin.x_login_usuario;
                /*Datos Compra Cabecera-----------------------------------------------------*/
                //Generamos codigo de compra
                string vIdCompra = "";
                vIdCompra = textBox10.Text.ToString() + comboBox2.SelectedValue.ToString() + textBox2.Text.ToString() + "-" + textBox1.Text.ToString() + comboBox1.SelectedValue.ToString() + rucEmpresa.ToString() + "-" + DTP3.Value.ToString("yyMMddss");
                if (FrmCompras.nmCom == 'M')
                {
                    //Cod.ToString()
                    ObjCompra.IdCompra = Cod.ToString();
                }
                else
                {
                    ObjCompra.IdCompra = vIdCompra.ToString();
                }
                ObjCompra.FIngAlmacen = DTP2.Value.Year.ToString() + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Day.ToString("00");
                ObjCompra.FIngSistema = DTP3.Value.Year.ToString() + "/" + DTP3.Value.Month.ToString("00") + "/" + DTP3.Value.Day.ToString("00");
                ObjCompra.FIngDoc = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
                ObjCompra.Doc = comboBox2.SelectedValue.ToString();
                ObjCompra.Serie = textBox2.Text.ToString();
                ObjCompra.Numero = textBox1.Text.ToString();
                ObjCompra.SerieGuia = textBox5.Text.ToString();
                ObjCompra.NumGuia = textBox4.Text.ToString();
                ObjCompra.NOrdenC = textBox6.Text.ToString();
                ObjCompra.Proveedor = textBox10.Text.ToString();
                ObjCompra.TMoneda = comboBox3.SelectedValue.ToString();
                ObjCompra.TCambio = Double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                ObjCompra.TCompra = comboBox4.SelectedValue.ToString();
                string aniofv = "1900";
                string mesfv = "01";
                string diafv = "01";
                if (comboBox4.SelectedValue.ToString().Equals("002"))
                {
                    ObjCompra.NDias = Int32.Parse(textBox7.Text.ToString().Equals("") ? "0" : textBox7.Text.ToString().Trim());
                    ObjCompra.FVence = DTP4.Value.Year.ToString() + "/" + DTP4.Value.Month.ToString("00") + "/" + DTP4.Value.Day.ToString("00");
                    ObjCompra.TEst = "P";
                }
                else { ObjCompra.NDias = 0; ObjCompra.FVence = aniofv.ToString() + "/" + mesfv.ToString() + "/" + diafv.ToString(); ObjCompra.TEst = "C"; }
                ObjCompra.Flete = Double.Parse(textBox8.Text.ToString().Equals("") ? "0" : textBox8.Text.ToString().Trim());
                ObjCompra.OtrosCargos = Double.Parse(textBox11.Text.ToString().Equals("") ? "0" : textBox11.Text.ToString().Trim());
                ObjCompra.Observaciones = textBox9.Text.ToString();
                ObjCompra.TBruto = Double.Parse(label22.Text.ToString().Equals("") ? "0" : label22.Text.ToString().Trim());
                ObjCompra.TDcto = Double.Parse(label21.Text.ToString().Equals("") ? "0" : label21.Text.ToString().Trim());
                ObjCompra.TIgv = Double.Parse(label20.Text.ToString().Equals("") ? "0" : label20.Text.ToString().Trim());
                ObjCompra.TPercep = Double.Parse(label19.Text.ToString().Equals("") ? "0" : label19.Text.ToString().Trim());
                ObjCompra.Total = Double.Parse(label18.Text.ToString().Equals("") ? "0" : label18.Text.ToString().Trim());
                ObjCompra.Est = "A";
                ObjCompra.Empresa = rucEmpresa.ToString();
                ObjCompra.Almacen = codAlmacen.ToString();
                ObjCompra.UserCreacion = Usuario.ToString();
                ObjCompra.UserModi = Usuario.ToString();
                /*-------------------------------------------------------------------------*/
                /*Datos Compra Detalle-----------------------------------------------------*/
                //En caso sea una modificacion
                if (FrmCompras.nmCom == 'M')
                {
                    ObjCompra.DevolverStock(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
                    ObjDetCompra.Eliminar(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
                }
                //----------------------------------------------------------------------
                string vMensajeCosto = "";
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    if (FrmCompras.nmCom == 'M')
                    {
                        ObjDetCompra.IdCompra = Cod.ToString();
                    }
                    else
                    {
                        ObjDetCompra.IdCompra = vIdCompra.ToString();
                    }
                    ObjDetCompra.CodArt = row.Cells[0].Value.ToString();
                    ObjProducto.BuscarProductoActivo(row.Cells[0].Value.ToString(), rucEmpresa, codAlmacen);

                    string vParam = "1";
                    string vCodCat = "013";
                    ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjProducto.CodUnidad.ToString(), vParam.ToString());
                    ObjDetCompra.Unidad = ObjDetCatalogo.CodDetCat.Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
                    ObjDetCompra.PCosto = Double.Parse(row.Cells[4].Value.ToString().Equals("") ? "0" : row.Cells[4].Value.ToString());
                    ObjDetCompra.Igv = Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());
                    //Actualizando Costo en Artículos
                    if (FrmCompras.nmCom == 'N')
                    {
                        if (ObjProducto.ActualizaCosto(ObjDetCompra.CodArt, (ObjDetCompra.PCosto + ObjDetCompra.Igv), rucEmpresa.ToString(), codAlmacen.ToString(), Usuario.ToString()))
                        {
                            //
                        }
                        else
                        {
                            vMensajeCosto = ", " + vMensajeCosto + ObjDetCompra.CodArt;
                        }
                    }
                    ObjDetCompra.Cantidad = Int32.Parse(row.Cells[3].Value.ToString().Equals("") ? "0" : row.Cells[3].Value.ToString());
                    ObjDetCompra.Dcto = Double.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString());
                    ObjDetCompra.PorDcto = Double.Parse(row.Cells[9].Value.ToString().Equals("") ? "0" : row.Cells[9].Value.ToString());
                    ObjDetCompra.Percep = Double.Parse(row.Cells[7].Value.ToString().Equals("") ? "0" : row.Cells[7].Value.ToString());
                    ObjDetCompra.Importe = Double.Parse(row.Cells[8].Value.ToString().Equals("") ? "0" : row.Cells[8].Value.ToString());
                    ObjDetCompra.Almacen = codAlmacen.ToString();
                    ObjDetCompra.Empresa = rucEmpresa.ToString();
                    ObjDetCompra.UserCreacion = Usuario.ToString();
                    ObjDetCompra.UserModi = Usuario.ToString();


                    if (ObjDetCompra.Crear())
                    {
                    }
                    else
                    {
                        MessageBox.Show("No se registro correctamente el detalle de la compra", "SISTEMA");
                        return;
                    }
                }


                foreach (DataGridViewRow row in dgvOculto.Rows)
                {

                   
                    ObjCodigoBarras.Codigo = row.Cells[0].Value.ToString();
                    ObjCodigoBarras.TipCod = row.Cells[3].Value.ToString();
                    ObjCodigoBarras.CodigoBarras = row.Cells[1].Value.ToString();
                    ObjCodigoBarras.Empresa = rucEmpresa.ToString();
                    ObjCodigoBarras.UserCreacion = Usuario.ToString();
                    ObjCodigoBarras.TipodeMovimiento = "C";
                        

                    if (ObjCodigoBarras.Crear())
                    {
                    }
                    else
                    {
                        MessageBox.Show("No se registro correctamente los codigos de barras", "SISTEMA");
                        return;
                    }
                    
                }

                /*-------------------------------------------------------------------------*/

                if (FrmCompras.nmCom == 'N')
                {
                    if (ObjCompra.Crear())
                    {
                        if (vMensajeCosto != "")
                        {
                            MessageBox.Show("No se actualizaron los costos de los siguientes productos: " + vMensajeCosto, "SISTEMA");
                        }

                        MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                        //this.Close();
                        textBox10.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        textBox7.Clear();
                        textBox8.Clear();
                        textBox9.Clear();
                        textBox11.Clear();
                        Grid1.Rows.Clear();
                        dgvOculto.Rows.Clear();
                        label18.Text = "";
                        label19.Text = "";
                        label20.Text = "";
                        label21.Text = "";
                        label22.Text = "";
                        checkBox1.Checked = false;

                        textBox10.Focus();
                    }
                    else
                    {
                        MessageBox.Show("No se registro correctamente", "SISTEMA");
                    }
                }
                else
                {
                    if (ObjCompra.Modificar())
                    {
                        MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                        textBox10.Clear();
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox4.Clear();
                        textBox5.Clear();
                        textBox6.Clear();
                        textBox7.Clear();
                        textBox8.Clear();
                        textBox9.Clear();
                        textBox11.Clear();
                        Grid1.Rows.Clear();
                        label18.Text = "";
                        label19.Text = "";
                        label20.Text = "";
                        label21.Text = "";
                        label22.Text = "";
                        checkBox1.Checked = false;
                        string val = "V";
                        Opener.CargarConsulta(val);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se registro correctamente", "SISTEMA");
                    }
                }
            }


            catch (System.Exception ex)
            {
                MessageBox.Show("Error de Sistema " + ex.Message, "SISTEMA");
            }

        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if (valModTCpm.Equals("") || valModTCpm.Equals("CON"))
            {
                if (comboBox4.SelectedValue.ToString().Equals("001"))
                {
                    textBox7.Enabled = false;
                    DTP4.Enabled = false;
                }
                else
                {
                    textBox7.Enabled = true;
                    DTP4.Enabled = true;
                }
            }
            else
            {
                //comboBox4.Text = "CREA";
                textBox7.Enabled = true;
                DTP4.Enabled = true;
            }
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void label18_Click(object sender, EventArgs e)
        {
        }


        private void DTP1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            string codoculto = "";
            string codengrilla = Grid1.CurrentRow.Cells[0].Value.ToString();
            string codbarroculto = "";
            
            foreach (DataGridViewRow row in dgvOculto.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(codengrilla.ToString()))
                {
                    codoculto = row.Cells[0].Value.ToString();
                    break;
                }
            }

            if (!codoculto.Equals(""))
            {
                //se cambia de tabpage
                tabControl1.SelectedTab = tabPage2;
                txtbarras.Focus();

                //se toma los valores de la fila seleccionada
                codigo = Grid1.CurrentRow.Cells[0].Value.ToString();
                nombre = Grid1.CurrentRow.Cells[1].Value.ToString();
                cantidad = Grid1.CurrentRow.Cells[3].Value.ToString();
                txtpreciouni.Text = Grid1.CurrentRow.Cells[4].Value.ToString();

                //se setean los datos en el tabpage2
                txtCod.Text = codigo;
                txtcantidad.Text = cantidad;
                txtdescrip.Text = nombre;

                //se habilitan los controles
                txtbarras.Enabled = true;
                cmbTipCod.Enabled = false;
                btnAnadir.Enabled = true;
                //txtcantidad.Enabled = true;


                foreach (DataGridViewRow row in dgvOculto.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(codengrilla.ToString()) && Double.Parse(row.Cells[4].Value.ToString()) == Double.Parse(txtpreciouni.Text.ToString()))
                    {
                        this.dgBarras.Rows.Add(row.Cells[0].Value.ToString(),
                            row.Cells[1].Value.ToString(),
                            row.Cells[2].Value).ToString();
                    }
                }

                txtbarras.Focus();
            }
            else
            {
                //se cambia de tabpage
                tabControl1.SelectedTab = tabPage2;
                txtbarras.Focus();

                //se toma los valores de la fila seleccionada
                codigo = Grid1.CurrentRow.Cells[0].Value.ToString();
                nombre = Grid1.CurrentRow.Cells[1].Value.ToString();
                cantidad = Grid1.CurrentRow.Cells[3].Value.ToString();
                txtpreciouni.Text = Grid1.CurrentRow.Cells[4].Value.ToString();

                //se setean los datos en el tabpage2
                txtCod.Text = codigo;
                txtcantidad.Text = cantidad;
                txtdescrip.Text = nombre;

                //se habilitan los controles
                txtbarras.Enabled = true;
                cmbTipCod.Enabled = true;
                btnAnadir.Enabled = true;
                txtbarras.Focus();
            }
        }


        private void LimpiarBarras()
        {
            txtCod.Text = "";
            txtbarras.Text = "";
            txtcantidad.Text = "";
            txtdescrip.Text = "";
            cmbTipCod.Text = "";
            txtpreciouni.Text = "";
            dgBarras.Rows.Clear();
        }

        private void btnAnadir_Click(object sender, EventArgs e)
        {

            if (txtbarras.Text.Equals(""))
            {
                MessageBox.Show("Ingrese un codigo de barras para el producto", "SISTEMA");
                txtbarras.Focus();
                return;
            }

            string codProducto = txtbarras.Text.ToString().Trim();

            if (codProducto.Trim().Length < 7)
            {
                txtbarras.Text = "";
                MessageBox.Show("El código de barras debe ser mayor de 6 dígitos", "SISTEMA");
                return;
            }

            if (txtcantidad.Text.Equals(""))
            {
                MessageBox.Show("Ingrese una cantidad para el producto", "SISTEMA");
                txtcantidad.Focus();
                return;
            }

            string codigin = txtCod.Text.ToString();
            string codigoBarras = txtbarras.Text.ToString();
            DataSet exiteEnCodBarras = csql.dataset_cadena("Call SpValidaCodBarras('" + codigin + "','" + codigoBarras + "')");
            object valor = exiteEnCodBarras.Tables[0].Rows[0][0];
            string existeCod = valor.ToString();

            if (existeCod.ToString().Equals("EXISTE"))
            {
                MessageBox.Show("El codigo de barras ya está relacionado con un producto", "SISTEMA");
                return;
            }

            string codoculto = "";
            string codengrilla = Grid1.CurrentRow.Cells[0].Value.ToString();
            int cant;
            Int64 codigoBarra;

            foreach (DataGridViewRow row in dgvOculto.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(codengrilla.ToString()))
                {
                    codoculto = row.Cells[0].Value.ToString();
                    break;
                }
            }

            if (!codoculto.Equals(""))
            {
                for (int i = dgvOculto.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dgvOculto.Rows[i];
                    if (row.Cells[0].Value.ToString().Equals(codengrilla.ToString()) && Double.Parse(row.Cells[4].Value.ToString()) == Double.Parse(txtpreciouni.Text.ToString()))
                    {
                        dgvOculto.Rows.Remove(row);
                    }
                }
                dgBarras.Rows.Clear();
                

                codigo = Grid1.CurrentRow.Cells[0].Value.ToString();
                nombre = Grid1.CurrentRow.Cells[1].Value.ToString();

                cant = int.Parse(txtcantidad.Text.ToString());
                codigoBarra = Int64.Parse(txtbarras.Text);

                if (cmbTipCod.Text.Equals("Cod. Unico"))
                {
                    for (int i = 1; i <= cant; i++)
                    {
                        dgBarras.Rows.Add(codigo, txtbarras.Text.ToString(), nombre, cmbTipCod.SelectedValue.ToString());
                        dgvOculto.Rows.Add(codigo, txtbarras.Text.ToString(), nombre, cmbTipCod.SelectedValue.ToString(), txtpreciouni.Text.ToString());
                    }
                }
                else if (cmbTipCod.Text.Equals("Cod. Secuencial"))
                {
                    for (int i = 1; i <= cant; i++)
                    {
                        dgBarras.Rows.Add(codigo, codigoBarra, nombre, cmbTipCod.SelectedValue.ToString());
                        dgvOculto.Rows.Add(codigo, codigoBarra, nombre, cmbTipCod.SelectedValue.ToString());
                        codigoBarra++;
                    }
                }
                txtcantidad.Enabled = false;
                txtbarras.Enabled = false;

            }
            else
            {
                cant = int.Parse(txtcantidad.Text.ToString());

                codigo = Grid1.CurrentRow.Cells[0].Value.ToString();
                nombre = Grid1.CurrentRow.Cells[1].Value.ToString();

                codigoBarra = Int64.Parse(txtbarras.Text);

                if (cmbTipCod.Text.Equals("Cod. Unico"))
                {
                    for (int i = 1; i <= cant; i++)
                    {
                        dgBarras.Rows.Add(codigo, txtbarras.Text.ToString(), nombre, cmbTipCod.SelectedValue.ToString());
                        dgvOculto.Rows.Add(codigo, txtbarras.Text.ToString(), nombre, cmbTipCod.SelectedValue.ToString(),txtpreciouni.Text.ToString());
                    }
                }
                else if (cmbTipCod.Text.Equals("Cod. Secuencial"))
                {
                    for (int i = 1; i <= cant; i++)
                    {
                        dgBarras.Rows.Add(codigo, codigoBarra, nombre, cmbTipCod.SelectedValue.ToString());
                        dgvOculto.Rows.Add(codigo, codigoBarra, nombre, cmbTipCod.SelectedValue.ToString());
                        codigoBarra++;
                    }
                }
            }
        }

        private void cmbTipCod_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void txtbarras_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            LimpiarBarras();
        }

        
    }
}