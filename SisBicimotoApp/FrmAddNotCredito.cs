using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddNotCredito : Form, ICliente, ISelNotCredito
    {
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsNotCre ObjNotCre = new ClsNotCre();
        private ClsDetNotCre ObjDetNotCre = new ClsDetNotCre();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsTipoCambio ObjTipoCambio = new ClsTipoCambio();
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsDocumento ObjDocumento = new ClsDocumento();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string codNC = "015"; //Codigo de documento de nota de credito
        private string vIdVenta = ""; //Id de venta
        private string vParam = "";
        private string Cod = "";
        private string vProd = "F";

        public static string nCodigo = "";
        public static string nProducto = "";
        public static string nUnidad = "";
        public static string nCantidad = "";

        private string cCodDocCli = "";

        public FrmAddNotCredito()
        {
            InitializeComponent();

            /* foreach (TabPage tabPage in this.tabControl1.TabPages)
             {
                 tabPage.EnabledChanged += new EventHandler(this.tabPage_EnabledChanged);
             }
             this.tabControl1.TabPages[1].Enabled = true;*/
        }

        #region ICliente Members

        public void SelectItem(string tipCod, string codCli)
        {
            //comboBox1.Text = tipCod;
            textBox10.Text = codCli;
        }

        #endregion ICliente Members

        #region ISelNotCredito Members

        private void tabPage_EnabledChanged(object sender, EventArgs e)
        {
            TabPage tabPage = sender as TabPage;
            TabControl tabControl = tabPage.Parent as TabControl;
            tabControl.Invalidate(tabPage.ClientRectangle);
        }

        public void SelectItemCantidad(double cant, string codigo)
        {
            //textBox10.Text = cant.ToString();

            int nIndex = 0;
            int contador = 0;
            double nImporte = 0;
            double nPrecio = 0;
            string nCod = "";
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                nIndex = contador;
                nCod = Grid1[0, nIndex].Value.ToString();
                if (codigo.Equals(nCod.ToString()))
                {
                    Grid1[3, nIndex].Value = cant == 0 ? "" : cant.ToString().Trim();
                    nPrecio = double.Parse(Grid1[4, nIndex].Value.ToString());
                    nImporte = cant * nPrecio;

                    //Obtener si aplica IGV
                    string nParam = "7";
                    string vAplicaIgv = "";
                    double valIgv = 0;
                    double valIgvCal = 0;

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
                        string nParam1 = "11";
                        string vTipAfecta = "";

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

                        if (vTipAfecta.Equals("01"))
                        {
                            valIgvCal = (nImporte * valIgv) / (100 + valIgv);
                            vSubTotal = nImporte;
                            nImporte = vSubTotal - valIgvCal;
                            //label17.Text = valIgvCal.ToString("###,##0.00").Trim();
                            //label11.Text = sTotal.ToString("###,##0.00").Trim();
                            Grid1[5, nIndex].Value = valIgvCal == 0 ? "" : valIgvCal.ToString("###,##0.00").Trim();
                            Grid1[6, nIndex].Value = nImporte == 0 ? "" : nImporte.ToString("###,##0.00").Trim();
                            //label22.Text = vSubTotal.ToString("###,##0.00").Trim();
                        }
                        else
                        {
                            valIgvCal = (nImporte * valIgv) / (100);
                            vSubTotal = nImporte;
                            nImporte = nImporte + valIgvCal;
                            //label17.Text = valIgvCal.ToString("###,##0.00").Trim();
                            //label11.Text = sTotal.ToString("###,##0.00").Trim();
                            Grid1[5, nIndex].Value = valIgvCal == 0 ? "" : valIgvCal.ToString("###,##0.00").Trim();
                            Grid1[6, nIndex].Value = nImporte == 0 ? "" : nImporte.ToString("###,##0.00").Trim();
                            //label22.Text = vSubTotal.ToString("###,##0.00").Trim();
                        }
                    }
                    else
                    {
                        valIgvCal = 0;
                        Grid1[5, nIndex].Value = valIgvCal == 0 ? "" : valIgvCal.ToString("###,##0.00").Trim();
                        Grid1[6, nIndex].Value = nImporte == 0 ? "" : nImporte.ToString("###,##0.00").Trim();
                    }
                }

                contador = contador + 1;
            }
            CalculaTotal();
        }

        #endregion ISelNotCredito Members

        private void LlenarCampos(string InCod)
        {
            try
            {
                if (ObjNotCre.BuscarNC(InCod, rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    DTP1.Text = ObjNotCre.Fecha.ToString().Trim();
                    comboBox1.Text = ObjNotCre.Doc.ToString().Trim();
                    textBox1.Text = ObjNotCre.Serie.ToString().Trim();
                    textBox2.Text = ObjNotCre.Numero.ToString().Trim();
                    //Tipo de cambio
                    double Net = 0;
                    Net = double.Parse(ObjNotCre.TCambio.ToString().Equals("") ? "0" : ObjNotCre.TCambio.ToString().Trim());
                    textBox4.Text = Net.ToString("###,##0.000").Trim();
                    textBox10.Text = ObjNotCre.Cliente.ToString().Trim();
                    cCodDocCli = ObjNotCre.TipDocCli.ToString().Trim();
                    comboBox5.Text = ObjNotCre.TMotivo.ToString().Trim();
                    comboBox2.Text = ObjNotCre.TMoneda.ToString().Trim();

                    textBox8.Text = ObjNotCre.Observaciones.ToString();
                    //Importe
                    Net = double.Parse(ObjNotCre.TBruto.ToString().Equals("") ? "0" : ObjNotCre.Total.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox6.Text = "0.00";
                    else
                        textBox6.Text = Net.ToString("###,##0.00").Trim();

                    //Igv
                    Net = double.Parse(ObjNotCre.TIgv.ToString().Equals("") ? "0" : ObjNotCre.TIgv.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label17.Text = "0.00";
                    else
                        label17.Text = Net.ToString("###,##0.00").Trim();

                    //Importe Total
                    Net = double.Parse(ObjNotCre.Total.ToString().Equals("") ? "0" : ObjNotCre.Total.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        label11.Text = "0.00";
                    else
                        label11.Text = Net.ToString("###,##0.00").Trim();

                    //Obtener Datos de Documento Asociado
                    if (ObjVenta.BuscarVenta(ObjNotCre.IdVenta.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
                    {
                        textBox7.Text = ObjVenta.Fecha.ToString().Trim();
                        comboBox1.Text = ObjVenta.Doc.ToString().Trim();
                        textBox4.Text = ObjVenta.Serie.ToString().Trim();
                        textBox3.Text = ObjVenta.Numero.ToString().Trim();
                    }
                    else
                    {
                        //MessageBox.Show("FALSE");
                    }

                    if (ObjNotCre.Art.Equals("1"))
                    {
                        checkBox1.Checked = true;
                        vProd = "T";
                        //this.tabControl1.TabPages[1].Enabled = true;
                        // tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
                    }
                    else
                    {
                        vProd = "F";
                        checkBox1.Checked = false;
                        // this.tabControl1.TabPages[1].Enabled = false;
                        // tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
                        buscarDocAsociado(ObjNotCre.IdVenta.ToString(), rucEmpresa.ToString(), codAlmacen.ToString());
                    }

                    //Grid1.Rows.Clear();

                    if (ObjNotCre.Art.Equals("1"))
                    {
                        //Obtener Detalles
                        DataSet datos = csql.dataset_cadena("Call SpDetNotCreBuscarVentaMarca('" + ObjNotCre.IdVenta.ToString() + "','" + InCod.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                        string vPunit = "";
                        string vIgv = "";
                        string vImporte = "";
                        if (datos.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow fila in datos.Tables[0].Rows)
                            {
                                Net = double.Parse(fila[4].ToString().Equals("") ? "0" : fila[4].ToString().Trim());
                                if (Net.ToString().Trim().Equals("0"))
                                    vPunit = "";
                                else
                                    vPunit = Net.ToString("###,##0.00").Trim();

                                Net = double.Parse(fila[5].ToString().Equals("") ? "0" : fila[5].ToString().Trim());
                                if (Net.ToString().Trim().Equals("0"))
                                    vIgv = "";
                                else
                                    vIgv = Net.ToString("###,##0.000").Trim();

                                Net = double.Parse(fila[6].ToString().Equals("") ? "0" : fila[6].ToString().Trim());
                                if (Net.ToString().Trim().Equals("0"))
                                    vImporte = "";
                                else
                                    vImporte = Net.ToString("###,##0.000").Trim();

                                this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), vPunit, vIgv, vImporte, fila[7].ToString().Equals("S") ? Boolean.TrueString : Boolean.FalseString, fila[8].ToString(), fila[9].ToString() });
                            }
                        }

                        //tabPage2.Enabled = true;
                    }
                    else
                    {
                        // tabPage2.Enabled = false;
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

        private Boolean validaDocAsociado(string idVenta, string vRuc, string vAlmacen)
        {
            Boolean res = false;
            if (ObjVenta.BuscarVenta(idVenta.ToString(), vRuc.ToString(), vAlmacen.ToString()))
            {
                res = true;
            }

            return res;
        }

        private void buscarDocAsociado(string idVenta, string vRuc, string vAlmacen)
        {
            string cDoc = comboBox1.SelectedValue.ToString();
            if (ObjVenta.BuscarComprobate(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
            {
                DateTime vFecha = DateTime.Parse(ObjVenta.Fecha.ToString());
                textBox7.Text = String.Format("{0:dd/MM/yyyy}", vFecha);

                idVenta = ObjVenta.Cliente.ToString() + idVenta.ToString();

                double Net = 0;
                Grid1.Rows.Clear();
                //Obtener Detalles
                DataSet datos = csql.dataset_cadena("Call SpDetVentaBuscar('" + idVenta.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                string vPunit = "";
                string vIgv = "";
                string vImporte = "";

                textBox10.Text = ObjVenta.Cliente.ToString();

                cCodDocCli = ObjVenta.TipDocCli.ToString();

                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        Net = double.Parse(fila[5].ToString().Equals("") ? "0" : fila[5].ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            vPunit = "";
                        else
                            vPunit = Net.ToString("###,##0.00").Trim();

                        Net = double.Parse(fila[6].ToString().Equals("") ? "0" : fila[6].ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            vIgv = "";
                        else
                            vIgv = Net.ToString("###,##0.000").Trim();

                        Net = double.Parse(fila[7].ToString().Equals("") ? "0" : fila[7].ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            vImporte = "";
                        else
                            vImporte = Net.ToString("###,##0.000").Trim();

                        string vCodCat = "";
                        //Marca
                        vParam = "1";
                        vCodCat = "010";
                        ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), fila[2].ToString().Trim(), vParam.ToString());
                        string codMarca = ObjDetCatalogo.CodDetCat;

                        //Procedencia
                        vCodCat = "009";
                        ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), fila[8].ToString().Trim(), vParam.ToString());
                        string codProc = ObjDetCatalogo.CodDetCat;

                        this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString() + " - " + fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), vPunit, vIgv, vImporte, Boolean.FalseString, codMarca.ToString(), codProc.ToString(), fila[8].ToString().Trim() });
                    }
                }
                //CalculaTotal();
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para el Documento Asociado, verifique el cliente y los datos del documento asociado", "SISTEMA");
                textBox7.Text = "";
                textBox10.Text = "";
                CalculaTotal();
                Grid1.Rows.Clear();
            }
        }

        private void FormatGridChk()
        {
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[7];

                if (chk.Value == null)
                    chk.Value = false;

                if (chk.Value.Equals("True"))
                {
                    chk.Value = true;
                }
                else
                {
                    chk.Value = false;
                }
            }
        }

        private void CalculaTotal()
        {
            //formatear
            //FormatGridChk();
            //Sumar Total
            double sumaTotal = 0;

            foreach (DataGridViewRow row in Grid1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[7];

                if (chk.Value == null)
                    chk.Value = Boolean.FalseString;

                if (chk.Value.Equals(Boolean.TrueString))
                {
                    sumaTotal += Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString());
                }
            }

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

            double valIgv = 0;

            if (vAplicaIgv.Equals("S"))
            {
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

                //Obtener Tipo de afectación IGV
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
                if (vTipAfecta.Equals("001"))
                {
                    valIgvCal = (sumaTotal * valIgv) / (100 + valIgv);
                    vSubTotal = sumaTotal - valIgvCal;
                    label17.Text = valIgvCal.ToString("###,##0.00").Trim();
                    label11.Text = sumaTotal.ToString("###,##0.00").Trim();
                    textBox6.Text = vSubTotal.ToString("###,##0.00").Trim();
                }
                else
                {
                    valIgvCal = (sumaTotal * valIgv) / (100);
                    vSubTotal = sumaTotal;
                    sumaTotal = sumaTotal + valIgvCal;
                    label17.Text = valIgvCal.ToString("###,##0.00").Trim();
                    label11.Text = sumaTotal.ToString("###,##0.00").Trim();
                    textBox6.Text = vSubTotal.ToString("###,##0.00").Trim();
                }
            }
            else
            {
                label11.Text = sumaTotal.ToString("###,##0.00").Trim();
                label17.Text = "0.00";
                textBox6.Text = sumaTotal.ToString("###,##0.00").Trim();
            }
        }

        private void FrmAddNotCredito_Load(object sender, EventArgs e)
        {
            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox3.DisplayMember = "nombre";
            comboBox3.ValueMember = "CodAlmacen";
            comboBox3.DataSource = datosAlm.Tables[0];
            comboBox3.Text = nomAlmacen.ToString();

            //Carga Moneda
            string codCatMon = "001";
            string tipPresMon = "2";
            DataSet datosMon = csql.dataset("Call SpCargarDetCat('" + codCatMon + "','" + tipPresMon + "')");
            comboBox2.DisplayMember = "DescCorta";
            comboBox2.ValueMember = "CodDetCat";
            comboBox2.DataSource = datosMon.Tables[0];

            string nParam = "1";
            //Verificar Moneda Predertimanada
            if (ObjParametro.BuscarParametro(nParam))
            {
                comboBox2.Text = ObjParametro.Valor.ToString();
            }

            //Carga Motivo
            string codCatMotNc = "020";
            string tipPresDoc = "1";
            DataSet datosTCom = csql.dataset_cadena("Call SpCargarDetCat('" + codCatMotNc + "','" + tipPresDoc + "')");

            if (datosTCom.Tables[0].Rows.Count > 0)
            {
                comboBox5.Items.Add("");
                foreach (DataRow fila in datosTCom.Tables[0].Rows)
                {
                    comboBox5.Items.Add(fila[1].ToString());
                }
            }

            //Carga Documento
            string codModulo = "VEN";
            /*DataSet datosTDoc = csql.dataset_cadena("Call SpDocBusModulo('" + codModulo.ToString() + "')");

            if (datosTDoc.Tables[0].Rows.Count > 0)
            {
                comboBox1.Items.Add("");
                foreach (DataRow fila in datosTDoc.Tables[0].Rows)
                {
                    comboBox1.Items.Add(fila[2].ToString());
                }
            }*/
            //Carga Documento
            DataSet datosDoc = csql.dataset("Call SpDocBusModulo('" + codModulo.ToString() + "')");
            comboBox1.DisplayMember = "ncorto";
            comboBox1.ValueMember = "Codigo";
            comboBox1.DataSource = datosDoc.Tables[0];

            //Carga Tipo de Cambio
            if (ObjTipoCambio.BuscarTipoCambio())
            {
                double tCambio = ObjTipoCambio.Valor;
                textBox5.Text = tCambio.ToString("###,##0.000").Trim();
            }
            else
            {
                MessageBox.Show("FALSE");
            }

            Cod = FrmNotaCredito.nIdNc.ToString();
            if (FrmNotaCredito.nmNcv == 'M')
            {
                LlenarCampos(Cod);

                //CalculaTotal();
            }
            else
            {
                CalculaTotal();
            }

            //tabPage2.Enabled = false;

            //Generar ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 300;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.button6, "Grabar Nota de Crédito");
            toolTip1.SetToolTip(this.button7, "Cambiar Cantidad de Ítem");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTP1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Grid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void Grid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nCodigo = Grid1.CurrentRow.Cells[0].Value.ToString();
                nProducto = Grid1.CurrentRow.Cells[1].Value.ToString();
                nUnidad = Grid1.CurrentRow.Cells[2].Value.ToString();
                nCantidad = Grid1.CurrentRow.Cells[3].Value.ToString();

                FrmSelItemNotCredito frmSelItemNotCredito = new FrmSelItemNotCredito();
                frmSelItemNotCredito.WindowState = FormWindowState.Normal;
                frmSelItemNotCredito.Opener = this;
                frmSelItemNotCredito.MdiParent = this.MdiParent;
                frmSelItemNotCredito.Show();
            }
        }

        private void Grid1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nCodigo = Grid1.CurrentRow.Cells[0].Value.ToString();
                nProducto = Grid1.CurrentRow.Cells[1].Value.ToString();
                nUnidad = Grid1.CurrentRow.Cells[2].Value.ToString();
                nCantidad = Grid1.CurrentRow.Cells[3].Value.ToString();

                FrmSelItemNotCredito frmSelItemNotCredito = new FrmSelItemNotCredito();
                frmSelItemNotCredito.WindowState = FormWindowState.Normal;
                frmSelItemNotCredito.Opener = this;
                frmSelItemNotCredito.MdiParent = this.MdiParent;
                frmSelItemNotCredito.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0 || textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Serie y Nro. de Comprobante de Nota de Crédito", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (label16.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Cliente", "SISTEMA");
                textBox10.Focus();
                return;
            }

            if (comboBox5.Text == "")
            {
                MessageBox.Show("Ingrese Motivo de Nota de Crédito", "SISTEMA");
                comboBox5.Focus();
                return;
            }

            if (comboBox1.Text == "")
            {
                MessageBox.Show("Ingrese Tipo de documento de referencia", "SISTEMA");
                comboBox5.Focus();
                return;
            }

            //if (textBox8.TextLength == 0)
            //{
            //    MessageBox.Show("Ingrese observación de Nota de Crédito", "SISTEMA");
            //    textBox1.Focus();
            //    return;
            //}

            if (textBox4.TextLength == 0 || textBox3.TextLength == 0)
            {
                MessageBox.Show("Ingrese Serie y Nro. de Comprobante de referencia", "SISTEMA");
                textBox4.Focus();
                return;
            }

            string nSerie = textBox1.Text;

            if (!ObjSerie.BuscarDocSerie("015", nSerie.ToString()))
            {
                MessageBox.Show("No se encontró el comprobante y la serie de la Nota de Credito, VERIFIQUE!!!", "SISTEMA");
                return;
            }

            //if (checkBox1.Checked == true)
            //{
            if (Grid1.RowCount <= 0)
            {
                MessageBox.Show("Ingrese Artículos", "SISTEMA");
                return;
            }
            //}

            vIdVenta = textBox10.Text.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();

            if (!validaDocAsociado(vIdVenta.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
            {
                MessageBox.Show("No se encontró documento asociado, por favor verifique.", "SISTEMA");
                return;
            }

            if (label11.Text.ToString().Equals("0.00"))
            {
                MessageBox.Show("Ingrese importe de la Nota de Crédito", "SISTEMA");
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar la Nota de Crédito", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //Inicia
            string Usuario = FrmLogin.x_login_usuario;
            vIdVenta = textBox10.Text.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();
            //Genera Id de nota de credito
            string vIdNoCre = "";
            if (FrmNotaCredito.nmNcv == 'N')
            {
                vIdNoCre = textBox10.Text.ToString() + codNC.ToString() + textBox1.Text.ToString() + "-" + textBox2.Text.ToString() + comboBox3.SelectedValue.ToString() + rucEmpresa.ToString();
                ObjNotCre.IdNoCre = vIdNoCre.ToString().Trim();
            }
            else
            {
                ObjNotCre.IdNoCre = Cod.ToString();
            }
            ObjNotCre.Fecha = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
            ObjNotCre.Cliente = textBox10.Text.ToString();
            vParam = "2";
            string vCodCat = "018";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), cCodDocCli.ToString().Trim(), vParam.ToString());
            ObjNotCre.TipDocCli = ObjDetCatalogo.CodDetCat.ToString();
            ObjNotCre.Doc = codNC.ToString();
            ObjNotCre.Serie = textBox1.Text.ToString();
            ObjNotCre.Numero = textBox2.Text.ToString();
            //Busca Motivo
            vParam = "1";
            vCodCat = "020";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), comboBox5.Text.ToString().Trim(), vParam.ToString());
            ObjNotCre.TMotivo = ObjDetCatalogo.CodDetCat.ToString();
            ObjNotCre.TMoneda = comboBox2.SelectedValue.ToString();
            ObjNotCre.IdVenta = vIdVenta.ToString().Trim();
            ObjNotCre.TCambio = Double.Parse(textBox5.Text.ToString().Equals("") ? "0" : textBox5.Text.ToString().Trim());
            ObjNotCre.Observaciones = comboBox5.Text.ToString();
            ObjNotCre.TBruto = Double.Parse(label11.Text.ToString().Equals("") ? "0" : label11.Text.ToString().Trim());
            ObjNotCre.TIgv = Double.Parse(label17.Text.ToString().Equals("") ? "0" : label17.Text.ToString().Trim());
            ObjNotCre.Total = Double.Parse(label11.Text.ToString().Equals("") ? "0" : label11.Text.ToString().Trim());
            ObjNotCre.Art = (checkBox1.Checked == true) ? "1" : "0";
            ObjNotCre.Empresa = rucEmpresa.ToString();
            ObjNotCre.Almacen = codAlmacen.ToString();
            if (FrmNotaCredito.nmNcv == 'N')
            {
                ObjNotCre.UserCreacion = Usuario.ToString();
            }
            else
            {
                ObjNotCre.UserModi = Usuario.ToString();
            }

            //if (checkBox1.Checked == true)
            //{
            //Si es una modificacion
            if (FrmNotaCredito.nmNcv == 'M')
            {
                ObjDetNotCre.Eliminar(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
            }

            /*Datos Detalle-----------------------------------------------------*/
            //Solo grabamos lo que estan marcados
            vParam = "1";
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[7];

                if (chk.Value.Equals(Boolean.TrueString))
                {
                    if (FrmNotaCredito.nmNcv == 'N')
                    {
                        ObjDetNotCre.IdNoCre = vIdNoCre.ToString().Trim();
                    }
                    else
                    {
                        ObjDetNotCre.IdNoCre = Cod.ToString().Trim(); //Se debe cambiar
                    }

                    ObjDetNotCre.Codigo = row.Cells[0].Value.ToString();
                    ObjDetNotCre.Marca = row.Cells[8].Value.ToString();
                    vCodCat = "013";
                    ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), row.Cells[2].Value.ToString().Trim(), vParam.ToString());
                    ObjDetNotCre.Unidad = ObjDetCatalogo.CodDetCat;
                    ObjDetNotCre.Proced = row.Cells[8].Value.ToString();
                    ObjDetNotCre.Cantidad = Double.Parse(row.Cells[3].Value.ToString());
                    ObjDetNotCre.PVenta = Double.Parse(row.Cells[4].Value.ToString());
                    ObjDetNotCre.Igv = Double.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString().ToString().Trim());
                    ObjDetNotCre.Importe = Double.Parse(row.Cells[6].Value.ToString().Equals("") ? "0" : row.Cells[6].Value.ToString().ToString().Trim());
                    ObjDetNotCre.Almacen = codAlmacen.ToString();
                    ObjDetNotCre.Empresa = rucEmpresa.ToString();
                    ObjDetNotCre.UserCreacion = Usuario.ToString();
                    if (ObjDetNotCre.Crear())
                    {
                    }
                    else
                    {
                        MessageBox.Show("No se registro correctamente el detalle de la Nota de Crédito", "SISTEMA");
                        return;
                    }
                }
            }
            //}
            if (FrmNotaCredito.nmNcv == 'N')
            {
                if (ObjNotCre.Crear())
                {
                    //Grabar XML
                    ObjGrabaXML.generaXMLNc(ObjNotCre.IdNoCre, rucEmpresa, codAlmacen);

                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    //this.Close();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox10.Clear();
                    label17.Text = "";
                    label11.Text = "";
                    textBox10.Clear();
                    comboBox5.Text = "";
                    Grid1.Rows.Clear();
                    checkBox1.Checked = false;
                    DTP1.Focus();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjNotCre.Modificar())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox6.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox10.Clear();
                    comboBox5.Text = "";
                    Grid1.Rows.Clear();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (textBox4.TextLength == 0 || textBox3.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Serie y Nro. de Comprobante de referencia", "SISTEMA");
                    checkBox1.Checked = false;
                    return;
                }

                // tabPage2.Enabled = true;
                vProd = "T";
                //Grid1.Rows.Clear();
                //button3.PerformClick();
            }
            else
            {
                //tabPage2.Enabled = false;
                vProd = "F";
                //Grid1.Rows.Clear();
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabControl = sender as TabControl;
            TabPage tabPage = tabControl.TabPages[e.Index];
            Rectangle tabRectangle = tabControl.GetTabRect(e.Index);

            if (tabControl.Alignment == TabAlignment.Left || tabControl.Alignment == TabAlignment.Right)
            {
                float rotateAngle = 90;
                if (tabControl.Alignment == TabAlignment.Left)
                {
                    rotateAngle = 270;
                }

                PointF cp = new PointF(tabRectangle.Left + (tabRectangle.Width / 2), tabRectangle.Top + (tabRectangle.Height / 2));
                e.Graphics.TranslateTransform(cp.X, cp.Y);
                e.Graphics.RotateTransform(rotateAngle);
                tabRectangle = new Rectangle(-(tabRectangle.Height / 2), -(tabRectangle.Width / 2), tabRectangle.Height, tabRectangle.Width);
            }
        }

        /*  using (SolidBrush foreBrush = new SolidBrush(tabPage.ForeColor))
          {
              using (SolidBrush backBrush = new SolidBrush(tabPage.BackColor))
              {
                  //Si es modificacion y ademas existen productos
                  if (vProd.Equals("T"))
                  {
                      tabControl1.TabPages[1].Enabled = true;
                  } else
                  {
                      tabControl1.TabPages[1].Enabled = false;
                  }
                  ////////////////////

                  if (!tabPage.Enabled)
                  {
                      foreBrush.Color = SystemColors.GrayText;
                  }

                  e.Graphics.FillRectangle(backBrush, tabRectangle);

                  using (StringFormat stringFormat = new StringFormat())
                  {
                      stringFormat.Alignment = StringAlignment.Center;
                      stringFormat.LineAlignment = StringAlignment.Center;
                      e.Graphics.DrawString(tabPage.Text, e.Font, foreBrush, tabRectangle, stringFormat);
                  }
              }
          }

          e.Graphics.ResetTransform();
      }*/

        /*private void tabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            TabPage tabPage = sender as TabPage;
            if (tabPage != null)
            {
                tabPage.EnabledChanged -= new EventHandler(this.tabPage_EnabledChanged);
            }
        }*/

        /*  private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
          {
              TabPage tabPage = sender as TabPage;
              if (tabPage != null)
              {
                  tabPage.EnabledChanged += new EventHandler(this.tabPage_EnabledChanged);
              }
          }*/

        private void DTP1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            //if (//char.IsLetter(e.KeyChar) || //Letras
            //char.IsSymbol(e.KeyChar) || //Símbolos
            //char.IsWhiteSpace(e.KeyChar) || //Espaçio
            //char.IsPunctuation(e.KeyChar)) //Pontuacion
            //    e.Handled = true;

            //if (e.KeyChar == 13)
            //{
            //    double Net = 0;
            //    Net = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
            //    if (Net.ToString().Trim().Equals("0"))
            //        textBox1.Text = "";
            //    else
            //        textBox1.Text = Net.ToString("0000").Trim();

            //    if (e.KeyChar == 13)
            //    {
            //        textBox2.Focus();
            //    }
            //}
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox1.Focus();
            }
        }

        private void textBox10_KeyPress_1(object sender, KeyPressEventArgs e)
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

        private void textBox10_TextChanged_1(object sender, EventArgs e)
        {
            string vRuc = textBox10.Text;
            if (ObjCliente.BuscarCLiente(vRuc, rucEmpresa))
            {
                label16.Text = ObjCliente.Nombre.ToString().Trim();
                /*if (label16.Text.Substring(0,1).Equals("C"))
                {
                    cCodDocCli = "DNI";
                } else
                {
                    cCodDocCli = ObjCliente.TipDoc.ToString().Trim();
                }*/
            }
            else
            {
                label16.Text = "";
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FrmBusCliente frmBusCliente = new FrmBusCliente();
            frmBusCliente.WindowState = FormWindowState.Normal;
            frmBusCliente.Opener = this;
            frmBusCliente.MdiParent = this.MdiParent;
            frmBusCliente.Show();
        }

        private void comboBox5_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
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
                Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox4.Text = "";
                else
                    textBox4.Text = Net.ToString("000").Trim();

                if (e.KeyChar == 13)
                {
                    textBox3.Focus();
                }
            }
        }

        private void textBox4_Validated_1(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox4.Text = "";
                else
                    textBox4.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox4.Focus();
            }
        }

        private void textBox1_Validated_1(object sender, EventArgs e)
        {
            //try
            //{
            //    double Net = 0;
            //    Net = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
            //    if (Net.ToString().Trim().Equals("0"))
            //        textBox1.Text = "";
            //    else
            //        textBox1.Text = Net.ToString("000").Trim();

            //    string vSerie = "";
            //    string vDoc = "";
            //    string vMod = "VEN";
            //    vSerie = textBox1.Text.ToString().Trim();
            //    vDoc = "015";
            //    if (!vSerie.Equals(""))
            //    {
            //        if (ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
            //        {
            //            if (ObjSerie.BuscarDocSerie(vDoc, vSerie))
            //            {
            //                if (ObjSerie.Correla.Equals("S"))
            //                {
            //                    textBox2.Enabled = false;
            //                    textBox2.Text = "Autogenerado";
            //                    textBox3.Focus();
            //                }
            //                else
            //                {
            //                    textBox2.Enabled = true;
            //                    textBox2.Text = "";
            //                    textBox2.Focus();
            //                }
            //            }
            //            else
            //            {
            //                textBox2.Enabled = true;
            //                textBox2.Text = "";
            //                textBox2.Focus();
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("Esta Serie no esta registrada para el Comprobante seleccionado", "SISTEMA");
            //            textBox2.Enabled = true;
            //            textBox2.Text = "";
            //            textBox1.Focus();
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        textBox2.Enabled = true;
            //        textBox2.Text = "";
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
            //    textBox1.Focus();
            //}
        }

        private void textBox2_Validated_1(object sender, EventArgs e)
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

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button3.Focus();
            }
        }

        private void textBox3_Validated_1(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox3.Text = "";
                else
                    textBox3.Text = Net.ToString("00000000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox3.Focus();
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Equals(""))
                {
                    MessageBox.Show("Ingrese Tipo de Documento Asociado", "SISTEMA");
                    comboBox1.Focus();
                    return;
                }

                if (textBox4.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Serie de Documento Asociado", "SISTEMA");
                    textBox4.Focus();
                    return;
                }

                if (textBox3.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Número de Documento Asociado", "SISTEMA");
                    textBox3.Focus();
                    return;
                }
                //Datos de venta

                vIdVenta = textBox10.Text.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();

                buscarDocAsociado(vIdVenta.ToString(), rucEmpresa.ToString(), codAlmacen.ToString());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Mensaje: " + ex.Message, "SISTEMA");
                //textBox3.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < textBox3.Text.Length; i++)
            {
                if (textBox3.Text[i] == '.')
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
                button6.Focus();
            }
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox6.Text.ToString().Equals("") ? "0" : textBox6.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox6.Text = "";
                else
                    textBox6.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox6.Focus();
            }
        }

        private void Grid1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Solo se trabaja ante los cambios en la columan de los checkbox
            //
            if (Grid1.Columns[e.ColumnIndex].Name == "SELECCIONAR")
            {
                //
                // Se toma la fila seleccionada
                //
                DataGridViewRow row = Grid1.Rows[e.RowIndex];

                //
                // Se selecciona la celda del checkbox
                //
                DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;

                //
                // Se valida si esta checkeada
                //
                //if (Convert.ToBoolean(cellSelecion.Value))
                //{
                //string mensaje = "Mensaje de Error"; //string.Format("Evento CellValueChanged.\n\nSe ha seccionado, \nDescripcion: '{0}', \nPrecio Unitario: '{1}', \nMedida: '{2}'",
                /*row.Cells["Descripcion"].Value,
                row.Cells["PrecioUnitario"].Value,
                row.Cells["UnidadMedida"].Value);*/

                //MessageBox.Show(mensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //}
            }
        }

        private void Grid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Detecta si se ha seleccionado el header de la grilla
            //
            if (e.RowIndex == -1)
                return;

            if (Grid1.Columns[e.ColumnIndex].Name == "SELECCIONAR")
            {
                //
                // Se toma la fila seleccionada
                //
                DataGridViewRow row = Grid1.Rows[e.RowIndex];

                //
                // Se selecciona la celda del checkbox
                //
                DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;
                //cellSelecion.FalseValue = 0;
                //cellSelecion.TrueValue = 1;
                /*if (Convert.ToBoolean(cellSelecion.Value))
                    row.DefaultCellStyle.BackColor = Color.Green;
                else
                    row.DefaultCellStyle.BackColor = Color.White;*/
                if (cellSelecion.Value == null)
                    cellSelecion.Value = Boolean.FalseString;
                switch (cellSelecion.Value.ToString())
                {
                    case "True":
                        cellSelecion.Value = Boolean.FalseString;
                        //Calcula Total
                        CalculaTotal();
                        break;

                    case "False":
                        cellSelecion.Value = Boolean.TrueString;
                        //Calcula Total
                        CalculaTotal();
                        break;
                }
            }
        }

        private void Grid1_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (Grid1.IsCurrentCellDirty)
            {
                Grid1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //Obtener si aplica IGV
            string nParam = "7";
            string vAplicaIgv = "";
            double sTotal = 0;

            sTotal = double.Parse(textBox6.Text.ToString().Equals("") ? "0" : textBox6.Text.ToString().Trim());

            if (ObjParametro.BuscarParametro(nParam))
            {
                vAplicaIgv = ObjParametro.Valor;
            }
            else
            {
                vAplicaIgv = "";
            }

            double valIgv = 0;

            if (vAplicaIgv.Equals("S"))
            {
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

                if (vTipAfecta.Equals("01"))
                {
                    valIgvCal = (sTotal * valIgv) / (100 + valIgv);
                    vSubTotal = sTotal;
                    sTotal = vSubTotal - valIgvCal;
                    label17.Text = valIgvCal.ToString("###,##0.00").Trim();
                    label11.Text = sTotal.ToString("###,##0.00").Trim();
                    //label22.Text = vSubTotal.ToString("###,##0.00").Trim();
                }
                else
                {
                    valIgvCal = (sTotal * valIgv) / (100);
                    vSubTotal = sTotal;
                    sTotal = sTotal + valIgvCal;
                    label17.Text = valIgvCal.ToString("###,##0.00").Trim();
                    label11.Text = sTotal.ToString("###,##0.00").Trim();
                    //label22.Text = vSubTotal.ToString("###,##0.00").Trim();
                }
            }
            else
            {
                label17.Text = "0.00";
                label11.Text = sTotal.ToString("###,##0.00").Trim();
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nCodigo = Grid1.CurrentRow.Cells[0].Value.ToString();
                nProducto = Grid1.CurrentRow.Cells[1].Value.ToString();
                nUnidad = Grid1.CurrentRow.Cells[2].Value.ToString();
                nCantidad = Grid1.CurrentRow.Cells[3].Value.ToString();

                FrmSelItemNotCredito frmSelItemNotCredito = new FrmSelItemNotCredito();
                frmSelItemNotCredito.WindowState = FormWindowState.Normal;
                frmSelItemNotCredito.Opener = this;
                frmSelItemNotCredito.MdiParent = this.MdiParent;
                frmSelItemNotCredito.Show();
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void checkBox1_Validated(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button7_Click_2(object sender, EventArgs e)
        {
            //no tiene codigo de validación
        }

        //********************
    }
}