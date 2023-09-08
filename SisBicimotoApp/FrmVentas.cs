using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmVentas : Form, ICliente, IVendedor, IVenta
    {
        public IVenta Opener { get; set; }

        private string nomCodUser = FrmLogin.x_codigo_usuario;
        public static char nmVen = 'N';
        public static string nIdVenta = "";
        public static string Doc = "";
        public static string EstDoc = "A";
        public static string nomXml = "";
        public static string vDoc = "";
        public static string vAlm = "";
        public string vTipDoc = "";
        private string nParam = "";

        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string razonEmpresa = FrmLogin.x_NomEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string usuario = FrmLogin.x_codigo_usuario;
        public static string almacenSelec = "";
        public static string docproforma = "";
        public static string TipoPago = "";
        private string codModulo = "VEN";
        public static string vTDoc = "";
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsVendedor ObjVendedor = new ClsVendedor();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsEnvio ObjEnvio = new ClsEnvio();
        private ClsImprimir ObjImprimir = new ClsImprimir();
        private ClsRolUser ObjRolUser = new ClsRolUser();
        private ClsImprimirProforma ObjProfo = new ClsImprimirProforma();
        private ClsRol ObjRol = new ClsRol();

        private DataSet datos;

        public FrmVentas()
        {
            InitializeComponent();
        }

        #region ICliente Members

        public void SelectItem(string tipDoc, string codCli)
        {
            vTipDoc = tipDoc.ToString();
            //label13.Text = vTipDoc;
            textBox2.Text = codCli;
        }

        public void validarVenta(string vVal)
        {
        }

        #endregion ICliente Members

        #region IVendedor Members

        public void SelectItemVend(string codVend)
        {
            textBox1.Text = codVend;
        }

        #endregion IVendedor Members

        #region IVenta Members

        public void CargarConsulta(string validaAnulaElimina)
        {
            if (validaAnulaElimina.ToString().Equals("V"))
            {
                string vFecha1;
                string vFecha2;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
                vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
                string vCliente = textBox2.Text;
                string vVendedor = textBox1.Text;
                string vTVenta = comboBox1.Text.ToString().Trim();
                string vTMoneda = comboBox2.Text.ToString().Trim();
                string vTipoPago = cmbTipoPago.Text.ToString().Trim();
                string vTDoc = comboBox4.Text.ToString().Trim();
                string vAlmacen = comboBox3.SelectedValue.ToString(); //comboBox3.Text.ToString().Trim();
                string vSerie = textBox3.Text;
                string vNumero = textBox4.Text;
                datos = csql.dataset("Call SpVentaConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vCliente.ToString().Trim() + "','" + vVendedor.ToString().Trim() + "','" + vTVenta.ToString().Trim() + "','" + vTMoneda.ToString().Trim() + "','" + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + vSerie.ToString().Trim() + "','" + vNumero.ToString().Trim() + "','" + vTipoPago.ToString().Trim() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label28.Text = "Total documentos: " + Grid1.RowCount;
                SumaTotal();
            }
        }

        public void GuardarVenta(string cliente, string tipdoc, string serie, string comprobante, string tipoPago)
        {
            //MessageBox.Show("Llega " + serie, "SISTEMA");
        }

        public void GuardarProforma(string clinete, string tipdoc, string serie, string comprobante)
        {
        }

        public void validarPrecio(string vVal)
        {
        }

        #endregion IVenta Members

        private void BusCliente(string vRuc, string vRucEmpresa)
        {
            string vParam = "2";
            string vCodCat = "018";

            if (ObjCliente.BuscarCLiente(vRuc, vRucEmpresa))
            {
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjCliente.TipDoc.ToString(), vParam.ToString());
                vTipDoc = ObjDetCatalogo.DescCorta.ToString();
                label4.Text = ObjCliente.Nombre.ToString().Trim();
            }
            else
            {
                label4.Text = "";
                //label13.Text = "";
            }
        }

        private void BusVendedor(string vCodRuc, string vRucEmpresa)
        {
            if (ObjVendedor.BuscarVendedor(vCodRuc.ToString().Trim(), vRucEmpresa))
            {
                label11.Text = ObjVendedor.Nombre.ToString().Trim();
            }
            else
            {
                label11.Text = "";
            }
            if (vCodRuc.ToString().Equals(""))
            {
                label11.Text = "";
            }
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Fecha - Hora";
            Grid1.Columns[1].HeaderText = "Doc.";
            Grid1.Columns[2].HeaderText = "Número";
            Grid1.Columns[3].HeaderText = "Doc.Iden.";
            Grid1.Columns[4].HeaderText = "Cliente";
            //Grid1.Columns[5].HeaderText = "Moneda";
            Grid1.Columns[6].HeaderText = "Tipo";
            Grid1.Columns[7].HeaderText = "Est. Venta";
            Grid1.Columns[8].HeaderText = "Vendedor";
            //Grid1.Columns[9].HeaderText = "Almacén";
            Grid1.Columns[9].HeaderText = "T.Grav.";
            Grid1.Columns[10].HeaderText = "T.Exonerado";
            Grid1.Columns[11].HeaderText = "T.Inafecto";
            Grid1.Columns[12].HeaderText = "T.Gratuito";
            Grid1.Columns[13].HeaderText = "Igv";
            Grid1.Columns[14].HeaderText = "Total";
            Grid1.Columns[15].HeaderText = "IdVenta";
            Grid1.Columns[16].HeaderText = "Estado";
            Grid1.Columns[17].HeaderText = "Xml";
            Grid1.Columns[18].HeaderText = "CodDoc";
            Grid1.Columns[19].HeaderText = "Serie";
            Grid1.Columns[20].HeaderText = "Almacen";
            Grid1.Columns[21].HeaderText = "Empresa";
            Grid1.Columns[22].HeaderText = "Archivo XML";
            Grid1.Columns[23].HeaderText = "Tipo Pago";
            Grid1.Columns[15].Visible = false;
            Grid1.Columns[18].Visible = false;
            Grid1.Columns[19].Visible = false;
            Grid1.Columns[20].Visible = false;
            Grid1.Columns[21].Visible = false;
            Grid1.Columns[22].Visible = false;
            Grid1.Columns[10].Visible = false;
            Grid1.Columns[11].Visible = false;
            Grid1.Columns[12].Visible = false;
            Grid1.Columns[13].Visible = false;
            Grid1.Columns[9].Visible = false;
            Grid1.Columns[0].Width = 110;
            Grid1.Columns[1].Width = 38;
            Grid1.Columns[2].Width = 76;
            Grid1.Columns[3].Width = 65;
            Grid1.Columns[4].Width = 150;
            //Grid1.Columns[5].Width = 40;
            Grid1.Columns[6].Width = 50;
            Grid1.Columns[7].Width = 80;
            Grid1.Columns[8].Width = 65;
            //Grid1.Columns[9].Width = 65;
            Grid1.Columns[10].Width = 70;
            Grid1.Columns[11].Width = 15;
            Grid1.Columns[12].Width = 15;
            Grid1.Columns[13].Width = 40;
            Grid1.Columns[14].Width = 55;
            Grid1.Columns[16].Width = 77;
            Grid1.Columns[17].Width = 30;
            Grid1.Columns[23].Width = 80;
            Grid1.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[23].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[9].DefaultCellStyle.Format = "###,##0.0000";
            Grid1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[10].DefaultCellStyle.Format = "###,##0.0000";
            Grid1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[11].DefaultCellStyle.Format = "###,##0.0000";
            Grid1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[12].DefaultCellStyle.Format = "###,##0.0000";
            Grid1.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid1.Columns[13].DefaultCellStyle.Format = "###,##0.0000";
            Grid1.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[14].DefaultCellStyle.Format = "###,##0.00";
            Grid1.BorderStyle = BorderStyle.None;
            Grid1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            Grid1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Grid1.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            Grid1.DefaultCellStyle.SelectionForeColor = Color.White;
            Grid1.BackgroundColor = Color.White;

            Grid1.EnableHeadersVisualStyles = false;
            Grid1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            Grid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            Grid1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            label28.Text = "Total Productos: " + Grid1.RowCount;
        }

        private void SumaTotal()
        {
            if (ObjRolUser.BuscarRolUser(usuario))
            {
                string rol = ObjRolUser.IdRol.ToString().Trim();
                if (rol.Equals("001"))
                {
                    //Sumar Total
                    double sumaTotalGrav = 0;
                    foreach (DataGridViewRow row in Grid1.Rows)
                    {
                        if (row.Cells[16].Value.ToString() != "ANULADO")
                        {
                            sumaTotalGrav += Double.Parse(row.Cells[14].Value.ToString().Equals("") ? "0" : row.Cells[14].Value.ToString());
                        }
                    }
                    label8.Text = sumaTotalGrav.ToString("###,##0.00").Trim();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            try
            {
                string tipPresDoc = "1";
                //Carga Almacen
                DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
                comboBox3.DisplayMember = "nombre";
                comboBox3.ValueMember = "CodAlmacen";
                comboBox3.DataSource = datosAlm.Tables[0];
                comboBox3.Text = nomAlmacen.ToString();

                //Carga Moneda
                string codCatMon = "001";
                DataSet datosMon = csql.dataset_cadena("Call SpCargarDetCat('" + codCatMon + "','" + tipPresDoc + "')");

                if (datosMon.Tables[0].Rows.Count > 0)
                {
                    comboBox2.Items.Add("");
                    foreach (DataRow fila in datosMon.Tables[0].Rows)
                    {
                        comboBox2.Items.Add(fila[1].ToString());
                    }
                }

                //Tipo Venta
                string codCatTCom = "015";
                DataSet datosTCom = csql.dataset_cadena("Call SpCargarDetCat('" + codCatTCom + "','" + tipPresDoc + "')");

                if (datosTCom.Tables[0].Rows.Count > 0)
                {
                    comboBox1.Items.Add("");
                    foreach (DataRow fila in datosTCom.Tables[0].Rows)
                    {
                        comboBox1.Items.Add(fila[1].ToString());
                    }
                }

                //Carga Documento
                DataSet datosTDoc = csql.dataset_cadena("Call SpDocBusModulo('" + codModulo.ToString() + "')");

                if (datosTDoc.Tables[0].Rows.Count > 0)
                {
                    comboBox4.Items.Add("");
                    foreach (DataRow fila in datosTDoc.Tables[0].Rows)
                    {
                        comboBox4.Items.Add(fila[2].ToString());
                    }
                }

                //Buscar tipo de pago
                string codCatBarras = "028";
                string tipBarras = "1";
                DataSet datosbarras = csql.dataset("Call SpCargarDetCat('" + codCatBarras + "','" + tipBarras + "')");
                cmbTipoPago.Items.Add("");
                if (datosbarras.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datosbarras.Tables[0].Rows)
                    {
                        cmbTipoPago.Items.Add(fila[1].ToString());
                    }
                }


                string vFecha1;
                string vFecha2;
                string vFecha1Profo;
                string vFecha2Profo;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
                vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
                vFecha1Profo = "01/01/2020";
                vFecha2Profo = "30/12/2030";
                string vCliente = textBox2.Text;
                string vVendedor = textBox1.Text;
                string vTVenta = comboBox1.Text.ToString().Trim();
                string vTMoneda = comboBox2.Text.ToString().Trim();

                string vAlmacen = comboBox3.SelectedValue.ToString(); //comboBox3.Text.ToString().Trim();
                string vSerie = textBox3.Text;
                string vNumero = textBox4.Text;
                if (vTDoc.Equals(""))
                {
                    //original
                     datos = csql.dataset("Call SpVentaConsulta('" + vFecha1.ToString() + "', '" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','','','','','','','','','')");

                }
                else
                {
                    datos = csql.dataset("Call SpPedidoConsulta('" + vFecha1Profo.ToString() + "','" + vFecha2Profo.ToString() + "','" + rucEmpresa.ToString() + "','" + vCliente.ToString().Trim()
                    + "','" + vVendedor.ToString().Trim() + "','" + vTVenta.ToString().Trim() + "','" + vTMoneda.ToString().Trim() + "','"
                    + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + vSerie.ToString().Trim() + "','" + vNumero.ToString().Trim() + "')");
                }
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label28.Text = "Total documentos: " + Grid1.RowCount;
                SumaTotal();

                //Generar ToolTip
                ToolTip toolTip1 = new ToolTip();

                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 300;
                toolTip1.ReshowDelay = 500;
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;

                // Set up the ToolTip text for the Button and Checkbox.
                toolTip1.SetToolTip(this.button1, "Buscar");
                toolTip1.SetToolTip(this.button2, "Modificar Venta");
                toolTip1.SetToolTip(this.button3, "Anular/Eliminar Venta");
                toolTip1.SetToolTip(this.button4, "Registrar Venta");
                toolTip1.SetToolTip(this.button8, "Realizar envío a SUNAT");
                toolTip1.SetToolTip(this.button9, "Generar archivo XML");
                toolTip1.SetToolTip(this.button10, "Descargar archivo XML");
                toolTip1.SetToolTip(this.button11, "Reporte de Ventas");
                toolTip1.SetToolTip(this.button7, "Salir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            nParam = "29";
            //Puerto smtp
            string vDimension = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vDimension = ObjParametro.Valor;
            }
            //Validar el Rol del Usuario y el tipo de Servicio
            if (vDimension.Equals("1"))
            {
                this.StartPosition = FormStartPosition.CenterParent;
                this.CenterToParent();
                this.Size = new Size(1000, 600);
                this.Location = new Point(255, 70);

                Grid1.Columns[4].Width = 80;
                Grid1.Columns[7].Width = 50;
                Grid1.Columns[16].Width = 57;

                Grid1.Columns[1].Visible = false;
                Grid1.Columns[17].Visible = false;
                Grid1.Width = 900;
                Grid1.Height = 400;

                label11.Size = new Size(100, 28);
                label4.Size = new Size(100, 28);

                label7.Location = new Point(554, 23);
                label5.Location = new Point(554, 55);
                comboBox3.Location = new Point(630, 20);
                comboBox4.Location = new Point(639, 52);
                label19.Location = new Point(707, 55);
                cmbTipoPago.Location = new Point(788, 54);

                cmbTipoPago.Size = new Size(100, 27);

                button1.Location = new Point(900, 28);

                button4.Location = new Point(915, 90);
                button2.Location = new Point(915, 133);
                button3.Location = new Point(915, 180);
                button13.Location = new Point(915, 228);
                button9.Location = new Point(915, 276);
                button10.Location = new Point(915, 324);
                button8.Location = new Point(915, 372);
                button12.Location = new Point(915, 410);
                button11.Location = new Point(915, 458);
                button7.Location = new Point(915, 505);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            string vCliente = textBox2.Text;
            string vVendedor = label11.Text;
            string vTVenta = comboBox1.Text.ToString().Trim();
            string vTMoneda = comboBox2.Text.ToString().Trim();
            string vTipoPago = cmbTipoPago.Text.ToString().Trim();
            vTDoc = comboBox4.Text.ToString().Trim();
            string vAlmacen = comboBox3.SelectedValue.ToString(); //comboBox3.Text.ToString().Trim();
            string vSerie = textBox3.Text;
            string vNumero = textBox4.Text;
            if (vTDoc.Equals("PCL"))
            {
                datos = csql.dataset("Call SpPedidoConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vCliente.ToString().Trim()
                    + "','" + vVendedor.ToString().Trim() + "','" + vTVenta.ToString().Trim() + "','" + vTMoneda.ToString().Trim() + "','"
                    + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + vSerie.ToString().Trim() + "','" + vNumero.ToString().Trim() + "')");
            }
            else
            {
                datos = csql.dataset("Call SpVentaConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vCliente.ToString().Trim()
               + "','" + vVendedor.ToString().Trim() + "','" + vTVenta.ToString().Trim() + "','" + vTMoneda.ToString().Trim() + "','"
               + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + vSerie.ToString().Trim() + "','" + vNumero.ToString().Trim() + "','" + vTipoPago.ToString().Trim() + "')");
            }

            Grid1.DataSource = datos.Tables[0];
            Grilla();
            label28.Text = "Total documentos: " + Grid1.RowCount;
            SumaTotal();
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
                comboBox1.Focus();
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
                textBox2.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
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
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox4.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.Focus();
            }
        }

        private void DTP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DTP2.Focus();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBusCliente frmBusCliente = new FrmBusCliente();
            frmBusCliente.WindowState = FormWindowState.Normal;
            frmBusCliente.Opener = this;
            frmBusCliente.MdiParent = this.MdiParent;
            frmBusCliente.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string codRucCli = textBox2.Text.ToString().Trim();
            BusCliente(codRucCli, rucEmpresa);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmBusVendedor frmBusVendedor = new FrmBusVendedor();
            frmBusVendedor.WindowState = FormWindowState.Normal;
            frmBusVendedor.Opener = this;
            frmBusVendedor.MdiParent = this.MdiParent;
            frmBusVendedor.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string codVend = textBox1.Text.ToString().Trim();
            BusVendedor(codVend, rucEmpresa);
        }

        private void DTP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //EstDoc = "";
            nmVen = 'N';
            FrmAddVenta frmAddVenta = new FrmAddVenta();
            frmAddVenta.WindowState = FormWindowState.Normal;
            frmAddVenta.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                EstDoc = "A";
                if (Grid1.CurrentRow.Cells[16].Value.ToString().Equals("ANULADO"))
                {
                    //MessageBox.Show("Registro ANULADO, no se puede proceder a modificar", "SISTEMA");
                    //return;
                    EstDoc = "N";
                }

                Doc = Grid1.CurrentRow.Cells[1].Value.ToString() + " " + Grid1.CurrentRow.Cells[2].Value.ToString();
                FrmValidaVenta frmValidarVenta = new FrmValidaVenta();
                frmValidarVenta.WindowState = FormWindowState.Normal;
                frmValidarVenta.Opener = this;
                frmValidarVenta.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmVen = 'M';
            if (Grid1.RowCount > 0)
            {
                EstDoc = "A";
                if (Grid1.CurrentRow.Cells[16].Value.ToString().Equals("ANULADO"))
                {
                    EstDoc = "N";
                }

                if (Grid1.CurrentRow.Cells[7].Value.ToString().Equals("CANCELADO"))
                {
                    EstDoc = "E";
                }
                almacenSelec = Grid1.CurrentRow.Cells[20].Value.ToString();
                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                FrmAddVenta frmAddventa = new FrmAddVenta();
                frmAddventa.WindowState = FormWindowState.Normal;
                frmAddventa.Opener = this;
                frmAddventa.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                vDoc = Grid1.CurrentRow.Cells[18].Value.ToString();
                string vSerie = Grid1.CurrentRow.Cells[19].Value.ToString();
                string vMod = "VEN";
                if (ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
                {
                    if (!ObjDocumento.EnvSunat.Equals("S"))
                    {
                        MessageBox.Show("El comprobante no es un tipo de COMPROBANTE ELECTRONICO, Verifique!!", "SISTEMA");
                        return;
                    }
                }

                if (Grid1.CurrentRow.Cells[14].Value.ToString().Equals("Si"))
                {
                    MessageBox.Show("Archivo XML generado, no se puede proceder a generar XML", "SISTEMA");
                    return;
                }
                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                ObjGrabaXML.generaXMLFactura(nIdVenta, rucEmpresa, codAlmacen, false);
                MessageBox.Show("Archivo XML generado satisfactoriamente", "SISTEMA");
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //Valida numeros
            if (//char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                double Net = 0;
                Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox3.Text = "";
                else
                    textBox3.Text = Net.ToString("000").Trim();

                textBox4.Focus();
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
                    textBox4.Text = Net.ToString("00000000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox4.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button1.Focus();
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
                    textBox3.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox3.Focus();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                string nomXml = "";
                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                vDoc = Grid1.CurrentRow.Cells[18].Value.ToString();
                string vSerie = Grid1.CurrentRow.Cells[19].Value.ToString();
                vAlm = Grid1.CurrentRow.Cells[20].Value.ToString();
                string vMod = "VEN";
                if (ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
                {
                    if (!ObjDocumento.EnvSunat.Equals("S"))
                    {
                        MessageBox.Show("El comprobante no es un tipo de COMPROBANTE ELECTRONICO, Verifique!!", "SISTEMA");
                        return;
                    }
                }

                if (!ObjEnvio.BuscarEnvioComp(nIdVenta, rucEmpresa, vAlm))
                {
                    return;
                }

                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                nomXml = ObjEnvio.NomArchXml.ToString();
                ObjGrabaXML.descargaXML(nomXml, ObjEnvio.ArchXml, rucEmpresa, vAlm);
                string val = "V";
                CargarConsulta(val);
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button8_Click_1(object sender, EventArgs e)

        {
            if (Grid1.RowCount > 0)
            {
                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                vDoc = Grid1.CurrentRow.Cells[18].Value.ToString();
                string vSerie = Grid1.CurrentRow.Cells[19].Value.ToString();
                string vMod = "VEN";
                if (ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
                {
                    if (!ObjDocumento.EnvSunat.Equals("S"))
                    {
                        MessageBox.Show("El comprobante no es un tipo de COMPROBANTE ELECTRONICO, Verifique!!", "SISTEMA");
                        return;
                    }
                }

                if (Grid1.CurrentRow.Cells[14].Value.ToString().Equals("Si"))
                {
                    MessageBox.Show("Archivo XML generado, no se puede proceder a generar XML", "SISTEMA");
                    return;
                }
                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                ObjGrabaXML.generaXMLFactura(nIdVenta, rucEmpresa, codAlmacen, false);
                MessageBox.Show("Archivo XML generado satisfactoriamente", "SISTEMA");
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
            if (Grid1.RowCount > 0)
            {
                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                vDoc = Grid1.CurrentRow.Cells[18].Value.ToString();
                string vSerie = Grid1.CurrentRow.Cells[19].Value.ToString();
                vAlm = Grid1.CurrentRow.Cells[20].Value.ToString();
                string vMod = "VEN";
                if (ObjDocumento.BuscarDocSerieCodMod(vDoc, vSerie, vMod))
                {
                    if (!ObjDocumento.EnvSunat.Equals("S"))
                    {
                        MessageBox.Show("El comprobante no es un tipo de COMPROBANTE ELECTRONICO, Verifique!!", "SISTEMA");
                        return;
                    }
                }

                string vXml = Grid1.CurrentRow.Cells[22].Value.ToString();

                if (vXml.Equals("No"))
                {
                    MessageBox.Show("Debe generar el XML para realizar el envío", "SISTEMA");
                    return;
                }

                nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                nomXml = Grid1.CurrentRow.Cells[2].Value.ToString();

                FrmEnviaXml frmEnviaXml = new FrmEnviaXml();
                frmEnviaXml.WindowState = FormWindowState.Normal;
                frmEnviaXml.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {

                string sqlConsulta;
                CrystalDecisions.CrystalReports.Engine.ReportDocument reporte = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                string vFecha1;
                string vFecha2;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
                vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
                string vCliente = textBox2.Text;
                string vVendedor = textBox1.Text;
                string vTVenta = comboBox1.Text.ToString().Trim();
                string vTMoneda = comboBox2.Text.ToString().Trim();
                string vTDoc = comboBox4.Text.ToString().Trim();
                string vAlmacen = comboBox3.SelectedValue.ToString(); //comboBox3.Text.ToString().Trim();
                string vSerie = textBox3.Text;
                string vNumero = textBox4.Text;
                string vTipoPago = cmbTipoPago.Text.ToString().Trim();

                sqlConsulta = "Call SpVentaConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vCliente.ToString().Trim() + "','" + vVendedor.ToString().Trim() + "','" + vTVenta.ToString().Trim() + "','" + vTMoneda.ToString().Trim() + "','" + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + vSerie.ToString().Trim() + "','" + vNumero.ToString().Trim() + "','" + vTipoPago.ToString().Trim() + "')";
                DataSet datos = csql.dataset_cadena(sqlConsulta);

                reporte.Load(@"Reportes\RptVentas.rpt");

                reporte.SetDataSource(datos.Tables[0]);

                reporte.SetParameterValue("RucEmpresa", rucEmpresa.ToString().Trim());
                reporte.SetParameterValue("RazonEmpresa", razonEmpresa.ToString().Trim());
                reporte.SetParameterValue("Almacen", nomAlmacen.ToString().Trim());
                reporte.SetParameterValue("FInicio", vFecha1.ToString().Trim());
                reporte.SetParameterValue("FFin", vFecha2.ToString().Trim());
                reporte.SetParameterValue("STotal", label8.Text.ToString());

                FrmRptVentas frmReporte = new FrmRptVentas();
                frmReporte.PreparaReporte(reporte);
                frmReporte.ShowDialog();
            }
            else
            {
                MessageBox.Show("No existen resultados en la consulta", "SISTEMA");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {
                docproforma = Grid1.CurrentRow.Cells[1].Value.ToString();
                
                if (docproforma.Equals("BEL") || docproforma.Equals("FEL"))
                {
                    if (Grid1.RowCount > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                        vAlm = Grid1.CurrentRow.Cells[20].Value.ToString();
                        if (ObjVenta.BuscarVenta(nIdVenta, rucEmpresa.ToString(), vAlm.ToString()))
                        {
                        }
                        else
                        {
                            MessageBox.Show("Error Venta", "SISTEMA");
                            return;
                        }

                        string vMod = "VEN";

                        ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
                        if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
                        {
                            MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                            return;
                        }

                        ObjImprimir.ImprimirTicket(nIdVenta, rucEmpresa.ToString(), vAlm.ToString(), true, true);
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un registro", "SISTEMA");
                    }
                }
                else if (docproforma.Equals("PCL") )
                {
                    if (Grid1.RowCount > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
                        vAlm = Grid1.CurrentRow.Cells[20].Value.ToString();
                        if (ObjVenta.BuscarVentaProforma(nIdVenta, rucEmpresa.ToString(), vAlm.ToString()))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Error Venta", "SISTEMA");
                            return;
                        }

                        string vMod = "VEN";

                        ObjDocumento.BuscarDocNomMod(ObjVenta.Doc, vMod);
                        if (!ObjSerie.BuscarDocSerie(ObjDocumento.Codigo, ObjVenta.Serie))
                        {
                            MessageBox.Show("Error no se encontró el comprobante y la serie de la venta, VERIFIQUE!!!", "SISTEMA");
                            return;
                        }
                       
                        ObjProfo.ImprimirTicket(nIdVenta, rucEmpresa.ToString(), vAlm.ToString(), true, true);
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un registro", "SISTEMA");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            almacenSelec = Grid1.CurrentRow.Cells[20].Value.ToString();
            nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
            docproforma = Grid1.CurrentRow.Cells[1].Value.ToString();
            TipoPago = Grid1.CurrentRow.Cells[23].Value.ToString();
            FrmReimprimirComp frmReimprimirComp = new FrmReimprimirComp();
            frmReimprimirComp.WindowState = FormWindowState.Normal;
            frmReimprimirComp.ShowDialog(this);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            nmVen = 'M';
            almacenSelec = Grid1.CurrentRow.Cells[20].Value.ToString();
            nIdVenta = Grid1.CurrentRow.Cells[15].Value.ToString();
            FrmVentasEnLinea frmventasenlinea = new FrmVentasEnLinea();
            frmventasenlinea.WindowState = FormWindowState.Normal;
            //frmventasenlinea.MdiParent = this.MdiParent;
            frmventasenlinea.Show();
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}