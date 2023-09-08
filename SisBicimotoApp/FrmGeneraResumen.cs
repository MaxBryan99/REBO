using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmGeneraResumen : Form, IAddComprobante
    {
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        public static string valorFecha = "";

        private ClsResumenEnvio ObjResumenEnvio = new ClsResumenEnvio();

        //ClsDetResumenEnvio ObjDetResumenEnvio = new ClsDetResumenEnvio();
        private ClsDetResumenEnvioAgrupado ObjDetResumenEnvio = new ClsDetResumenEnvioAgrupado();

        private ClsVenta ObjVenta = new ClsVenta();
        private ClsNotCre ObjNotCre = new ClsNotCre();
        private ClsNotDeb ObjNotDeb = new ClsNotDeb();
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsParametro ObjParametro = new ClsParametro();

        public FrmGeneraResumen()
        {
            InitializeComponent();
        }

        #region IAddComprobante Members

        public void AddNewItem(string IdComprobante)
        {
            DataSet datosComp = csql.dataset_cadena("Call SpResumenComprobanteIdComp('" + IdComprobante.ToString() + "','" + codAlmacen.ToString() + "','" + rucEmpresa.ToString() + "')");

            if (datosComp.Tables[0].Rows.Count > 0)
            {
                int nRow = 0;
                double Net = 0;
                string vTotal = "";
                string codCatEst = "026";
                string tipPres = "1";

                foreach (DataRow fila in datosComp.Tables[0].Rows)
                {
                    Net = double.Parse(fila[4].ToString().Equals("") ? "0" : fila[4].ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        vTotal = "";
                    else
                        vTotal = Net.ToString("###,##0.00").Trim();

                    this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), "", vTotal, Boolean.TrueString, fila[6].ToString(), fila[7].ToString(), fila[8].ToString(), fila[9].ToString(), fila[10].ToString(), fila[11].ToString(), fila[12].ToString() });

                    DataGridViewRow row = Grid1.Rows[nRow];

                    //
                    // Se selecciona la celda del checkbox
                    //

                    DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;

                    if (fila[5].ToString().Equals("1"))
                    {
                        row.DefaultCellStyle.BackColor = Color.DarkOrange;
                        //
                    }

                    cellSelecion.ReadOnly = true;

                    DataGridViewComboBoxCell combo = Grid1.Rows[nRow].Cells[4] as DataGridViewComboBoxCell;
                    DataSet datosEst = csql.dataset("Call SpCargarDetCat('" + codCatEst + "','" + tipPres + "')");
                    combo.DisplayMember = "Descripcion";
                    combo.ValueMember = "CodDetCat";
                    combo.DataSource = datosEst.Tables[0];

                    nRow += 1;
                }
            }
            else
            {
                MessageBox.Show("No se puede cargar datos", "SISTEMA");
                return;
            }
        }

        #endregion IAddComprobante Members

        private void VerificaSel()
        {
            int n = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[6];

                if (chk.Value == null)
                    chk.Value = Boolean.FalseString;

                if (chk.Value.Equals(Boolean.TrueString))
                {
                    n += 1;
                }
            }

            label5.Text = "Total documentos seleccionados: " + n;
        }


     
        private void CargarDoc()
        {
            try
            {
                string vFecha;
                vFecha = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();

                ////Cargar Columna Combo
                //string codCatEst = "026";
                //string tipPres = "1";
                //DataSet datosEst = csql.dataset("Call SpCargarDetCat('" + codCatEst + "','" + tipPres + "')");
                //DataGridViewComboBoxColumn dgvCmb = Grid1.Columns[4] as DataGridViewComboBoxColumn;
                //var dgvCmb = (DataGridViewComboBoxColumn)Grid1.Columns["Est"];
                ////SIUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU
                //DataGridViewComboBoxColumn dgvCmb;
                //dgvCmb = Grid1.Columns[4] as DataGridViewComboBoxColumn;
                //Grid1.AutoGenerateColumns = false;
                //dgvCmb.DisplayMember = "Descripcion";
                //dgvCmb.ValueMember = "CodDetCat";
                //dgvCmb.DataSource = datosEst.Tables[0];
                //List<DataGridViewComboBoxColumn> items = new List<DataGridViewComboBoxColumn>();

                DataSet datos = csql.dataset_cadena("Call SpResumenComprobante('" + vFecha.ToString() + "','" + rucEmpresa.ToString() + "')");

                if (datos.Tables[0].Rows.Count > 0)
                {
                    int nRow = 0;
                    double Net = 0;
                    string vTotal = "";
             

                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {

                        Net = double.Parse(fila[5].ToString().Equals("") ? "0" : fila[5].ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            vTotal = "";
                        else
                            vTotal = Net.ToString("###,##0.00").Trim();

                        this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), vTotal, Boolean.TrueString, fila[6].ToString(), fila[7].ToString(), fila[8].ToString(), fila[9].ToString(), fila[10].ToString(), fila[11].ToString(), fila[12].ToString() });
                                               
                        DataGridViewRow row = Grid1.Rows[nRow];


                        //string idstado = "";
                        DataGridViewComboBoxCell comboBoxCell = (row.Cells[4] as DataGridViewComboBoxCell);
                        comboBoxCell.Items.Add("Adicionar");
                        comboBoxCell.Items.Add("Anular");
                        if (fila[4].ToString().Equals("A"))
                        {
                            comboBoxCell.Value = "Adicionar";
                        }
                        else
                            comboBoxCell.Value = "Anular";



                        //Se selecciona la celda del checkbox



                        DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;

                        if (fila[6].ToString().Equals("1"))
                        {
                            row.DefaultCellStyle.BackColor = Color.DarkOrange;
                            //
                        }

                        cellSelecion.ReadOnly = false;

                        nRow += 1;
                    }
                }

                Grid1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Grid1_EditingControlShowing);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se puede cargar datos: " + ex.Message);
            }
        }

        private Boolean validaComp()
        {
            Boolean nvalor = false;
            int n = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                string vIdComp = row.Cells[7].Value.ToString();

                DataSet datosIdComp = csql.dataset_cadena("Call SpDetResumenAgrupadoBuscarIdComp('" + vIdComp.ToString() + "','" + rucEmpresa.ToString() + "')");

                if (datosIdComp.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datosIdComp.Tables[0].Rows)
                    {
                        if (!row.Cells[4].Value.Equals("3"))
                        {
                            nvalor = true;
                        }
                    }
                }
                else
                {
                    nvalor = false;
                }

                n += 1;
            }

            return nvalor;
        }

        private void DataGridView1OnRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            Grid1.Rows[e.RowIndex].Cells[4].Value = -1;
        }

        private void FrmGeneraResumen_Load(object sender, EventArgs e)
        {
            //string nParam = "21";
            //SERVICIO web
            //string vServWeb = "";
            /*if (ObjParametro.BuscarParametro(nParam))
            {
                vServWeb = ObjParametro.Valor;
            }
            textBox2.Text = vServWeb;*/

           try
            {
                string archivo = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivo);
                string vServWeb = "";
                vServWeb = ciniarchivo.ReadValue("Configura", "Service", "");
                textBox2.Text = vServWeb;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Hubo problemas al leer el archivo INI. " + ex.Message, "SISTEMA");
            }

            CargarDoc();

            label2.Text = "Total documentos: " + Grid1.RowCount;

            VerificaSel();

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

            toolTip1.SetToolTip(this.button1, "Enviar Resumen");
            toolTip1.SetToolTip(this.button8, "Salir");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Detecta si se ha seleccionado el header de la grilla
            //
            /*if (e.RowIndex == -1)
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

            /*if (cellSelecion.Value == null)
                cellSelecion.Value = Boolean.FalseString;
            switch (cellSelecion.Value.ToString())
            {
                case "True":
                    cellSelecion.Value = Boolean.FalseString;
                    //Verifica Seleccionados
                    VerificaSel();
                    break;

                case "False":
                    cellSelecion.Value = Boolean.TrueString;
                    //Verifica Seleccionados
                    VerificaSel();
                    break;
            }
        }*/
        }

        private void Grid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Solo se trabaja ante los cambios en la columan de los checkbox
            //
            //if (Grid1.Columns[e.ColumnIndex].Name == "SELECCIONAR")
            //{
            //
            // Se toma la fila seleccionada
            //
            //-DataGridViewRow row = Grid1.Rows[e.RowIndex];

            //
            // Se selecciona la celda del checkbox
            //
            //-DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;

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

            //-}
        }

        private void Grid1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (Grid1.IsCurrentCellDirty)
            {
                Grid1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Grid1.Rows.Clear();
            CargarDoc();

            label2.Text = "Total documentos: " + Grid1.RowCount;

            VerificaSel();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    //
                    // Se recupera el campo que representa el checkbox, y se valida la seleccion
                    // agregandola a la lista temporal
                    //
                    DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;
                    cellSelecion.Value = Boolean.TrueString;
                }
            }
            else
            {
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    //
                    // Se recupera el campo que representa el checkbox, y se valida la seleccion
                    // agregandola a la lista temporal
                    //
                    DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;
                    cellSelecion.Value = Boolean.FalseString;
                }
            }

            VerificaSel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            List<ClsResumen> items = new List<ClsResumen>();
            List<ClsResumenComprobante> itemsComprobante = new List<ClsResumenComprobante>();
            string vFecha;
            vFecha = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            valorFecha = vFecha;
            DataSet datos = csql.dataset_cadena("Call SpResumenAgrupaComprobante('" + vFecha.ToString() + "','" + rucEmpresa.ToString() + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                int nRow = 1;
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    ClsResumen ObjResumen = new ClsResumen()
                    {
                        Item = nRow,
                        TipoComprobante = fila[0].ToString(),
                        Serie = fila[1].ToString(),
                        Inicio = int.Parse(fila[2].ToString()),
                        Fin = int.Parse(fila[3].ToString()),
                        Gravadas = Double.Parse(fila[4].ToString()),
                        Exoneradas = Double.Parse(fila[5].ToString()),
                        Gratuitas = Double.Parse(fila[6].ToString()),
                        Igv = Double.Parse(fila[7].ToString()),
                        SumaTotal = Double.Parse(fila[8].ToString()),
                        Total = Double.Parse(fila[9].ToString()),
                        TDoc = fila[10].ToString(),
                        Conteo = int.Parse(fila[11].ToString()),
                        Dife = int.Parse(fila[12].ToString()),
                    };
                    items.Add(ObjResumen);
                    nRow += 1;
                }

                //Comprobantes
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    ClsResumenComprobante ObjResumenComprobante = new ClsResumenComprobante()
                    {
                        TipoComprobante = row.Cells[7].Value.ToString(),
                        Serie = row.Cells[8].Value.ToString(),
                        Numero = row.Cells[9].Value.ToString(),
                        IdComp = row.Cells[6].Value.ToString(),
                        Almacen = row.Cells[10].Value.ToString(),
                        Empresa = rucEmpresa.ToString(),
                    };

                    itemsComprobante.Add(ObjResumenComprobante);
                }
            }

            FrmResumenDetalleEnvio frmResumenDetalleEnvio = new FrmResumenDetalleEnvio(items, itemsComprobante);
            frmResumenDetalleEnvio.WindowState = FormWindowState.Normal;
            frmResumenDetalleEnvio.ShowDialog(this);
            */
            /*if (validaComp())
            {
                MessageBox.Show("Existen comprobantes que ya fueron enviados, debe envíar como Eliminado, VERIFIQUE!!!", "SISTEMA");
                return;
            }*/

            if (MessageBox.Show("Datos Correctos, se procedera a registrar el envío del Resumen de Comprobantes", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (Grid1.RowCount > 0)
            {
                DateTime fechaHoy = DateTime.Now;
                string fechahoyEnv = fechaHoy.ToString("d");
                string fechahoyAnio = fechahoyEnv.Substring(6, 4);
                string fechahoyMes = fechahoyEnv.Substring(3, 2);
                string fechahoyDia = fechahoyEnv.Substring(0, 2);

                string vFecha1;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();

                string hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + DateTime.Now.Second.ToString("D2");

                string vCodEnvio = "RC" + "-" + DTP1.Value.Year.ToString() + DTP1.Value.Month.ToString("00") + DTP1.Value.Day.ToString("00");

               // string vCodEnvio = "RC" + "-" + fechahoyAnio.ToString() + fechahoyMes.ToString() + fechahoyDia.ToString();

                int nValor = ObjResumenEnvio.ValorConteo(vCodEnvio, rucEmpresa);

                string Usuario = FrmLogin.x_login_usuario;

                ObjResumenEnvio.Id = nValor + 1;
                ObjResumenEnvio.NDocResumen = vCodEnvio.ToString();
                ObjResumenEnvio.Fecha = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
                ObjResumenEnvio.Est = "A";
                ObjResumenEnvio.RucEmpresa = rucEmpresa;
                ObjResumenEnvio.UserCreacion = Usuario.ToString();

                //Detalles

                int n = 1;
                string vTip = "";
                string vIdComp = "";
                string vAlmacen = "";

                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    ObjDetResumenEnvio.Id = nValor + 1;
                    ObjDetResumenEnvio.NDocResumen = vCodEnvio.ToString();
                    ObjDetResumenEnvio.NumId = n;
                    ObjDetResumenEnvio.TDoc = row.Cells[8].Value.ToString();
                    ObjDetResumenEnvio.Serie = row.Cells[9].Value.ToString();
                    ObjDetResumenEnvio.Inicio = int.Parse(row.Cells[10].Value.ToString());
                    ObjDetResumenEnvio.Fin = int.Parse(row.Cells[10].Value.ToString());

                    vTip = row.Cells[13].Value.ToString();
                    vAlmacen = row.Cells[12].Value.ToString();
                    vIdComp = row.Cells[7].Value.ToString();
                    if (row.Cells[4].Value.ToString().Equals("") || row.Cells[4].Value.ToString().Equals(""))
                    {
                        ObjDetResumenEnvio.EstadoItem = 1;
                    }
                    else if (row.Cells[4].Value.ToString() == "Adicionar")
                    {
                        ObjDetResumenEnvio.EstadoItem = 1;
                    }
                    else
                    {
                        ObjDetResumenEnvio.EstadoItem = 3;
                    }

                    //Se busca comprobante
                    switch (vTip)
                    {
                        

                        case "1":
                            if (!ObjVenta.BuscarVenta(vIdComp, rucEmpresa, vAlmacen))
                            {
                                MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                                return;
                            }
                            ObjDetResumenEnvio.Gravadas = Double.Parse(ObjVenta.TBruto.ToString());
                            ObjDetResumenEnvio.Exoneradas = Double.Parse(ObjVenta.TExonerada.ToString());
                            ObjDetResumenEnvio.Gratuitas = Double.Parse(ObjVenta.TGratuita.ToString());
                            ObjDetResumenEnvio.Igv = Double.Parse(ObjVenta.TIgv.ToString().Equals("0") ? "0" : ObjVenta.TIgv.ToString());
                            ObjDetResumenEnvio.SumaTotal = Double.Parse(ObjVenta.Total.ToString());
                            ObjDetResumenEnvio.IdComp = vIdComp;
                            break;

                        case "2":
                            if (!ObjNotCre.BuscarNC(vIdComp, rucEmpresa, vAlmacen))
                            {
                                MessageBox.Show("Error no se encontró datos de Nota de Crédito, VERIFIQUE!!!", "SISTEMA");
                                return;
                            }
                            ObjDetResumenEnvio.Gravadas = Double.Parse(ObjNotCre.TBruto.ToString());
                            ObjDetResumenEnvio.Exoneradas = 0;
                            ObjDetResumenEnvio.Gratuitas = 0;
                            ObjDetResumenEnvio.Igv = Double.Parse(ObjNotCre.TIgv.ToString());
                            ObjDetResumenEnvio.SumaTotal = Double.Parse(ObjNotCre.Total.ToString());
                            ObjDetResumenEnvio.IdComp = vIdComp;
                            break;

                        case "3":
                            if (!ObjNotDeb.BuscarND(vIdComp, rucEmpresa, vAlmacen))
                            {
                                MessageBox.Show("Error no se encontró datos de Nota de Crédito, VERIFIQUE!!!", "SISTEMA");
                                return;
                            }
                            ObjDetResumenEnvio.Gravadas = Double.Parse(ObjNotDeb.TBruto.ToString());
                            ObjDetResumenEnvio.Exoneradas = 0;
                            ObjDetResumenEnvio.Gratuitas = 0;
                            ObjDetResumenEnvio.Igv = Double.Parse(ObjNotDeb.TIgv.ToString());
                            ObjDetResumenEnvio.SumaTotal = Double.Parse(ObjNotDeb.Total.ToString());
                            ObjDetResumenEnvio.IdComp = vIdComp;
                            break;

                        default:

                            if (!ObjVenta.BuscarVenta(vIdComp, rucEmpresa, vAlmacen))
                            {
                                MessageBox.Show("Error no se encontró datos de Venta, VERIFIQUE!!!", "SISTEMA");
                                return;
                            }
                            ObjDetResumenEnvio.Gravadas = Double.Parse(ObjVenta.TBruto.ToString());
                            ObjDetResumenEnvio.Exoneradas = Double.Parse(ObjVenta.TExonerada.ToString());
                            ObjDetResumenEnvio.Gratuitas = Double.Parse(ObjVenta.TGratuita.ToString());
                            ObjDetResumenEnvio.Igv = Double.Parse(ObjVenta.TIgv.ToString().Equals("0") ? "0" : ObjVenta.TIgv.ToString());
                            ObjDetResumenEnvio.SumaTotal = Double.Parse(ObjVenta.Total.ToString());
                            ObjDetResumenEnvio.IdComp = vIdComp;
                            break;
                    }

                    //-------------------------------

                    ObjDetResumenEnvio.Almacen = row.Cells[12].Value.ToString();
                    ObjDetResumenEnvio.RucEmpresa = rucEmpresa;
                    ObjDetResumenEnvio.UserCreacion = Usuario.ToString();
                    n += 1;
                    if (ObjDetResumenEnvio.Crear())
                    {
                    }
                    else
                    {
                        MessageBox.Show("No se puede crear registro de detalle de Comunicación, VERIFIQUE!!!", "SISTEMA");
                        return;
                    }
                }

                if (ObjResumenEnvio.Crear())
                {
                    //Grabar XML
                    ObjGrabaXML.generaXMLResumen2(ObjResumenEnvio.Id.ToString(), vCodEnvio.ToString(), rucEmpresa, true);

                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                }
                else
                {
                    MessageBox.Show("No se puede crear registro de Comunicación, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar resumen comprobantes", "SISTEMA");
                return;
            }
        }

        private void DTP1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void Grid1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            /////////DataGridViewComboBoxEditingControl combo = e.Control as DataGridViewComboBoxEditingControl;
            //ComboBox combo = e.Control as ComboBox;

           /**** if (combo != null)
            {*///
                //
                // se remueve el handler previo que pudiera tener asociado, a causa ediciones previas de la celda
                // evitando asi que se ejecuten varias veces el evento
                //
            ////////    combo.SelectedIndex = 0;
            
                //combo.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);

                // Add the event handler.
                //combo.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            ///////////}
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((ComboBox)sender).BackColor = (Color)((ComboBox)sender).SelectedItem;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                int index = Grid1.CurrentRow.Index;
                Grid1.Rows.RemoveAt(index);
                label2.Text = "Total documentos: " + Grid1.RowCount;
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
                return;
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmAddComprobante frmAddComprobante = new FrmAddComprobante();
            frmAddComprobante.WindowState = FormWindowState.Normal;
            frmAddComprobante.ShowDialog(this);
        }

        private void Grid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
       
        }

        private void Grid1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            
        }

        private void Grid1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void Grid1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Estado"].Value = "Adicionar";
        }
    }
}