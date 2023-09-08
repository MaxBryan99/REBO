using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SisBicimotoApp.Lib;
using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;

namespace SisBicimotoApp
{
    public partial class FrmAddSalida : Form, IArticulo
    {
        public ISalida Opener { get; set; }
        string rucEmpresa = FrmLogin.x_RucEmpresa;
        string nomAlmacen = FrmLogin.x_NomAlmacen;
        string codAlmacen = FrmLogin.x_CodAlmacen;
        string codModulo = "SAL";
        string Cod = "";
        string vAfectaSt = "";
        ClsProducto ObjProducto = new ClsProducto();
        ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        ClsDocumento ObjDocumento = new ClsDocumento();
        ClsSalida ObjSalida = new ClsSalida();
        ClsDetSalida ObjDetSalida = new ClsDetSalida();
        ClsPersonal ObjPersonal = new ClsPersonal();
        ClsSerie ObjSerie = new ClsSerie();
        ClsTransportista ObjTransporte = new ClsTransportista();
        private ClsCodigoBarras ObjCodigoBarras = new ClsCodigoBarras();
        private string codigo = "";
        private string nombre = "";
        private string cantidad = "";


        public FrmAddSalida()
        {
            InitializeComponent();
        }

        #region IArticulo Members

        public void SelectItem(string codArt)
        {
            textBox3.Text = codArt;
        }

        #endregion

        #region ITransporte Members

        public void SelectItemTrans(string codProv)
        {
            textBox7.Text = codProv;
        }

        #endregion

        private void BusTransporte(string vRuc, string vRucEmpresa)
        {
            if (ObjTransporte.BuscarTransportista(vRuc, vRucEmpresa))
            {
                label21.Text = ObjTransporte.Nombre.ToString().Trim();
            }
            else
            {
                label21.Text = "";
            }
        }

        void LlenarCampos(string InCod)
        {
            try
            {
                string nCAlm = FrmSalidasAlm.cAlmacen.ToString();
                if (ObjSalida.BuscarSalida(InCod, nCAlm.ToString(), rucEmpresa.ToString()))
                {
                    DTP1.Text = ObjSalida.Fecha.ToString().Trim();
                    comboBox1.SelectedValue = ObjSalida.Almacen.ToString();
                    comboBox2.Text = ObjSalida.TipDoc.ToString().Trim();
                    textBox1.Text = ObjSalida.Serie.ToString().Trim();
                    textBox2.Text = ObjSalida.Numero.ToString().Trim();
                    textBox10.Text = ObjSalida.Referencia.ToString().Trim();
                    comboBox3.Text = ObjSalida.Concepto.ToString().Trim();
                    comboBox4.Text = ObjSalida.Responsable.ToString().Trim();
                    //Obtener Detalles
                    DataSet datos = csql.dataset_cadena("Call SpDetSalidaBuscar('" + InCod.ToString() + "','" + rucEmpresa.ToString() + "','" + nCAlm.ToString() + "')");
                    if (datos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in datos.Tables[0].Rows)
                        {
                            this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), fila[5].ToString(), fila[6].ToString(), fila[7].ToString(), fila[8].ToString() });
                        }

                    }
                    vAfectaSt = ObjSalida.AfectaSt.ToString();
                    if (ObjSalida.AfectaSt.Equals("0") || ObjSalida.AfectaSt == null || ObjSalida.AfectaSt.Equals(""))
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                    textBox5.Text = ObjSalida.Partida.ToString().Trim();
                    textBox6.Text = ObjSalida.Destino.ToString().Trim();
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

        private void BusProducto(string vProducto, string vRucEmpresa)
        {
            if (ObjProducto.BuscarProducto(vProducto, vRucEmpresa, codAlmacen))
            {
                label16.Text = ObjProducto.Nombre.ToString().Trim();
                label13.Text = ObjProducto.CodUnidad.ToString().Trim();
                label12.Text = ObjProducto.CodMarca.ToString().Trim();
                label8.Text = ObjProducto.CodProced.ToString().Trim();

                if (ObjProducto.GenStock.ToString().Equals("S") || ObjProducto.GenStock.ToString().Equals(""))
                {
                    //label34.Visible = false;
                    label34.Text = "S";
                }
                else
                {
                    //label34.Visible = false;
                    label34.Text = "N";

                }

                if (ObjProducto.TipProducto.ToString().Equals("PRODUCTO"))
                {
                    label33.Visible = false;
                }
                else
                {
                    label33.Visible = true;
                    label33.Text = "Producto Servicio";
                }

                //Buscar Stock
                if (ObjProducto.BuscarStock(vProducto, vRucEmpresa, codAlmacen))
                {
                    label17.Text = ObjProducto.Stock.ToString();
                }
                else
                {
                    label17.Text = "";
                }

            }
            else
            {
                label16.Text = "";
                label13.Text = "";
                label12.Text = "";
                label8.Text = "";
            }
        }

        private void FrmAddIngreso_Load(object sender, EventArgs e)
        {
            DataSet datosDoc = csql.dataset("Call SpDocBusModulo('" + codModulo.ToString() + "')");
            comboBox2.DisplayMember = "nombre";
            comboBox2.ValueMember = "Codigo";
            comboBox2.DataSource = datosDoc.Tables[0];

            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "CodAlmacen";
            comboBox1.DataSource = datosAlm.Tables[0];
            comboBox1.Text = nomAlmacen.ToString();

            //Carga Tipo Codigo de barras
            string codCatBarras = "027";
            string tipBarras = "1";
            DataSet datosbarras = csql.dataset("Call SpCargarDetCat('" + codCatBarras + "','" + tipBarras + "')");

            cmbTipCod.DisplayMember = "Descripcion";
            cmbTipCod.ValueMember = "CodDetCat";
            cmbTipCod.DataSource = datosbarras.Tables[0];

            //Carga Concepto
            string codCatMon = "017";
            string tipPresMon = "1";
            DataSet datosMon = csql.dataset("Call SpCargarDetCat('" + codCatMon + "','" + tipPresMon + "')");
            comboBox3.DisplayMember = "Descripcion";
            comboBox3.ValueMember = "CodDetCat";
            comboBox3.DataSource = datosMon.Tables[0];

            //Carga Responsable
            DataSet datosResp = csql.dataset("Call SpResponsableBusGen('" + codAlmacen.ToString() + "','" + rucEmpresa.ToString() + "')");

            if (datosResp.Tables[0].Rows.Count > 0)
            {
                comboBox4.Items.Add("");
                foreach (DataRow fila in datosResp.Tables[0].Rows)
                {
                    comboBox4.Items.Add(fila[1].ToString());
                }
            }

            Cod = FrmSalidasAlm.nId.ToString();
            if (FrmSalidasAlm.nmSal == 'M')
            {
                LlenarCampos(Cod);
            }

            if (FrmSalidasAlm.nmSal == 'M')
            {
                textBox2.Enabled = false;
            }
            else
            {
                textBox2.Enabled = true;
                //checkBox2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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


                string vSerie = "";
                string vDoc = "";
                string vMod = "SAL";
                vSerie = textBox1.Text.ToString().Trim();
                vDoc = comboBox2.SelectedValue.ToString();
                if (FrmSalidasAlm.nmSal == 'N')
                {
                    if (!vSerie.Equals(""))
                    {
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
                    else
                    {
                        textBox2.Enabled = true;
                        textBox2.Text = "";
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox1.Focus();
            }
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
                    textBox2.Text = Net.ToString("00000000000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox2.Focus();
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
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox10.Focus();
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox5.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
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
                button2.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox7.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label16.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Producto", "SISTEMA");
                textBox3.Focus();
                return;
            }

            if (textBox4.TextLength == 0)
            {
                MessageBox.Show("Ingrese Cantidad", "SISTEMA");
                textBox4.Focus();
                return;
            }

            string vParam = "1";
            string vCodCat = "010";
            string marca = "";
            if (ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label12.Text.ToString().Trim(), vParam.ToString()))
            {
                marca = ObjDetCatalogo.CodDetCat;
            }
            string unidad = "";
            vCodCat = "013";
            if (ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label13.Text.ToString().Trim(), vParam.ToString()))
            {
                unidad = ObjDetCatalogo.CodDetCat;
            }
            string proced = "";
            string vCodCatProc = "009";
            ClsDetCatalogo ObjDetCat2 = new ClsDetCatalogo();
            if (!label8.Text.Equals(""))
            {
                if (ObjDetCat2.BuscarDetCatalogoDes(vCodCatProc.ToString(), label8.Text.Trim(), vParam.ToString()))
                {
                    proced = ObjDetCat2.CodDetCat;
                }
            }



            //Valida si el producto existe
            Boolean existe = false;
            int nIndex = 0;
            int contador = 0;
            double cantArti = 0;

            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(textBox3.Text.ToString().Trim()))
                {
                    existe = true;
                    cantArti = Double.Parse(row.Cells[5].Value.ToString());

                    nIndex = contador;
                }
                contador += 1;
            }

            double newCant = 0;
            double cantidad = Double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString());
            if (existe)
            {
                newCant = cantidad + cantArti;

                Grid1[5, nIndex].Value = newCant;

            }
            else
            {
                this.Grid1.Rows.Add(new[] { textBox3.Text.ToString(), label16.Text.ToString(), label12.Text.ToString(), label8.Text.ToString(), label13.Text.ToString(), textBox4.Text.ToString(), marca.ToString(), proced.ToString(), unidad.ToString() });
            }

            textBox3.Text = "";
            textBox4.Text = "";
            textBox3.Focus();
            label28.Text = "Total Productos: " + Grid1.RowCount;

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string codProducto = textBox3.Text.ToString().Trim();
            BusProducto(codProducto, rucEmpresa);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
            frmBusArticulo.WindowState = FormWindowState.Normal;
            frmBusArticulo.Opener = this;
            //frmAddItemCompra.MdiParent = this.MdiParent;
            //frmAddItemCompra.Show();
            frmBusArticulo.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                int index = Grid1.CurrentRow.Index;
                Grid1.Rows.RemoveAt(index);
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Documento", "SISTEMA");
                comboBox2.Focus();
                return;
            }

            if (textBox1.TextLength == 0 || textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Serie y Nro. de Documento", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (comboBox3.Text.Equals(""))
            {
                MessageBox.Show("Ingrese concepto de Salida", "SISTEMA");
                comboBox3.Focus();
                return;
            }

            if (comboBox4.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Responsable", "SISTEMA");
                comboBox4.Focus();
                return;
            }

            if (Grid1.RowCount <= 0)
            {
                MessageBox.Show("Ingrese Artículos", "SISTEMA");
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar la salida del Almacén", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            string Usuario = FrmLogin.x_login_usuario;
            //Datos de Cabecera de Venta
            //Generar Id
            string nValFec = DTP1.Value.Year.ToString() + DTP1.Value.Month.ToString("000") + DTP1.Value.Day.ToString("000");
            string nId = "";
            //Buscar Codigo de Doc
            string vMod = "SAL";
            string vNomDoc = comboBox2.Text;
            string codDoc = "";
            if (ObjDocumento.BuscarDocNomMod(vNomDoc, vMod))
            {
                codDoc = ObjDocumento.Codigo.ToString();
            }

            string vSerie = "";
            vSerie = textBox1.Text.ToString().Trim();
            string codComp = comboBox2.SelectedValue.ToString();
            int vNumSerie = 0;
            string vNumero = "";
            if (textBox2.Enabled == false && textBox2.Text == "AUTOGENERADO")
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

            nId = nValFec + codDoc + textBox1.Text.ToString() + "-" + vNumero.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();

            if (FrmSalidasAlm.nmSal == 'M')
            {
                ObjSalida.Id = Cod.ToString();
            }
            else
            {
                ObjSalida.Id = nId.ToString();
            }


            ObjSalida.Fecha = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
            string vParam = "1";
            string vCodCat = "017";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), comboBox3.Text.ToString().Trim(), vParam.ToString());
            ObjSalida.Concepto = ObjDetCatalogo.CodDetCat;
            ObjSalida.TipDoc = codDoc.ToString();
            ObjSalida.Serie = textBox1.Text;
            ObjSalida.Numero = vNumero;
            ObjSalida.Referencia = textBox10.Text;
            //Buscar Responsable
            string nnombre = comboBox4.Text;
            int nVal = 1;
            string codResp = "";
            DataSet datos = csql.dataset("Call SpPersonalBusNom('" + nnombre.ToString() + "'," + nVal + ",'" + rucEmpresa.ToString() + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    codResp = fila[0].ToString();
                }
            }

            ObjSalida.Responsable = codResp.ToString();
            ObjSalida.Almacen = codAlmacen.ToString();
            ObjSalida.Empresa = rucEmpresa.ToString();
            ObjSalida.UserCreacion = Usuario.ToString();
            ObjSalida.UserModi = Usuario.ToString();
            ObjSalida.AfectaSt = checkBox1.Checked == true ? "0" : "1";
            ObjSalida.Partida = textBox5.Text;
            ObjSalida.Destino = textBox6.Text;
            ObjSalida.Transporte = textBox7.Text;
            ObjSalida.Chofer = textBox8.Text;
            ObjSalida.Licencia = textBox9.Text;
            ObjSalida.Vehiculo = textBox11.Text;
            /*-------------------------------------------------------------------------*/
            /*Datos Salida Detalle-----------------------------------------------------*/
            //En caso sea una modificacion
            if (FrmSalidasAlm.nmSal == 'M')
            {
                if (vAfectaSt.Equals("0"))
                {
                    ObjSalida.DevolverStock(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
                }

                ObjDetSalida.Eliminar(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
            }
            //----------------------------------------------------------------------
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (FrmSalidasAlm.nmSal == 'M')
                {
                    ObjDetSalida.Id = Cod.ToString();
                }
                else
                {
                    ObjDetSalida.Id = nId.ToString();
                }
                ObjDetSalida.Codigo = row.Cells[0].Value.ToString();
                ObjDetSalida.Marca = row.Cells[6].Value.ToString().Trim();
                ObjDetSalida.Unidad = row.Cells[8].Value.ToString().Trim();
                ObjDetSalida.Proce = row.Cells[7].Value.ToString().Trim();
                ObjDetSalida.Cantidad = Double.Parse(row.Cells[5].Value.ToString().Trim());
                ObjCodigoBarras.Cantidad = row.Cells[5].Value.ToString().Trim();
                ObjDetSalida.Almacen = codAlmacen.ToString();
                ObjDetSalida.Empresa = rucEmpresa.ToString();
                ObjDetSalida.UserCreacion = Usuario.ToString();
                ObjDetSalida.UserModi = Usuario.ToString();
                ObjDetSalida.AfectaSt = checkBox1.Checked == true ? "0" : "1";
                if (ObjDetSalida.Crear())
                {

                }
                else
                {
                    MessageBox.Show("No se registro correctamente el detalle de Salida", "SISTEMA");
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
                ObjCodigoBarras.TipodeMovimiento = "S";

                if (ObjCodigoBarras.salida())
                {
                }
                else
                {
                    MessageBox.Show("No se registro correctamente los codigos de barras", "SISTEMA");
                    return;
                }

            }
            /*-------------------------------------------------------------------------*/

            if (FrmSalidasAlm.nmSal == 'N')
            {
                if (ObjSalida.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    //this.Close();
                    textBox10.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox7.Clear();
                    textBox8.Clear();
                    textBox9.Clear();
                    textBox11.Clear();
                    Grid1.Rows.Clear();
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjSalida.Modificar())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    textBox10.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
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

        private void textBox3_Validated(object sender, EventArgs e)
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

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox4.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox6.Focus();
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox9.Focus();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox8.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string rucProv = textBox7.Text.ToString().Trim();
            BusTransporte(rucProv, rucEmpresa);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox11.Focus();
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
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
                    //codbarroculto = row.Cells[1].Value.ToString();
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
                cantidad = Grid1.CurrentRow.Cells[5].Value.ToString();
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
                    if (row.Cells[0].Value.ToString().Equals(codengrilla.ToString()))
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
                cantidad = Grid1.CurrentRow.Cells[5].Value.ToString();
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
                    if (row.Cells[0].Value.ToString().Equals(codengrilla.ToString()))
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
            }
        }
    }
}
