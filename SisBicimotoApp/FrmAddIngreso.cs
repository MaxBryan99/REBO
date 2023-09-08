using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddIngreso : Form, IArticulo
    {
        public IIngreso Opener { get; set; }
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string codModulo = "ING";
        private string Cod = "";
        private ClsProducto ObjProducto = new ClsProducto();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsIngreso ObjIngreso = new ClsIngreso();
        private ClsDetIngreso ObjDetIngreso = new ClsDetIngreso();
        private ClsPersonal ObjPersonal = new ClsPersonal();
        private ClsCodigoBarras ObjCodigoBarras = new ClsCodigoBarras();
        private string codigo = "";
        private string nombre = "";
        private string cantidad = "";

        public FrmAddIngreso()
        {
            InitializeComponent();
        }

        #region IArticulo Members

        public void SelectItem(string codArt)
        {
            textBox3.Text = codArt;
        }

        #endregion IArticulo Members

        private void LlenarCampos(string InCod)
        {
            try
            {
                string nCAlm = FrmIngresosAlm.cAlmacen.ToString();
                if (ObjIngreso.BuscarIngreso(InCod, nCAlm.ToString(), rucEmpresa.ToString()))
                {
                    DTP1.Text = ObjIngreso.Fecha.ToString().Trim();
                    comboBox1.SelectedValue = ObjIngreso.Almacen.ToString();
                    comboBox2.Text = ObjIngreso.TipDoc.ToString().Trim();
                    textBox1.Text = ObjIngreso.Serie.ToString().Trim();
                    textBox2.Text = ObjIngreso.Numero.ToString().Trim();
                    textBox10.Text = ObjIngreso.Referencia.ToString().Trim();
                    comboBox3.Text = ObjIngreso.Concepto.ToString().Trim();
                    comboBox4.Text = ObjIngreso.Responsable.ToString().Trim();
                    //Obtener Detalles
                    DataSet datos = csql.dataset_cadena("Call SpDetIngresoBuscar('" + InCod.ToString() + "','" + rucEmpresa.ToString() + "','" + nCAlm.ToString() + "')");
                    if (datos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in datos.Tables[0].Rows)
                        {
                            this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), fila[5].ToString(), fila[6].ToString(), fila[7].ToString(), fila[8].ToString() });
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
            //Carga Documento
            DataSet datosTDoc = csql.dataset("Call SpDocBusModulo('" + codModulo.ToString() + "')");

            if (datosTDoc.Tables[0].Rows.Count > 0)
            {
                comboBox2.Items.Add("");
                foreach (DataRow fila in datosTDoc.Tables[0].Rows)
                {
                    comboBox2.Items.Add(fila[1].ToString());
                }
            }

            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "CodAlmacen";
            comboBox1.DataSource = datosAlm.Tables[0];
            comboBox1.Text = nomAlmacen.ToString();

            //Carga Concepto
            string codCatMon = "016";
            string tipPresMon = "1";
            DataSet datosMon = csql.dataset("Call SpCargarDetCat('" + codCatMon + "','" + tipPresMon + "')");
            comboBox3.DisplayMember = "Descripcion";
            comboBox3.ValueMember = "CodDetCat";
            comboBox3.DataSource = datosMon.Tables[0];

            //Carga Tipo Codigo de barras
            string codCatBarras = "027";
            string tipBarras = "1";
            DataSet datosbarras = csql.dataset("Call SpCargarDetCat('" + codCatBarras + "','" + tipBarras + "')");

            cmbTipCod.DisplayMember = "Descripcion";
            cmbTipCod.ValueMember = "CodDetCat";
            cmbTipCod.DataSource = datosbarras.Tables[0];

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

            Cod = FrmIngresosAlm.nId.ToString();
            if (FrmIngresosAlm.nmIng == 'M')
            {
                LlenarCampos(Cod);
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
                comboBox4.Focus();
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
                textBox3.Focus();
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
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label12.Text.ToString().Trim(), vParam.ToString());
            string marca = ObjDetCatalogo.CodDetCat;
            vCodCat = "013";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label13.Text.ToString().Trim(), vParam.ToString());
            string unidad = ObjDetCatalogo.CodDetCat;
            vCodCat = "009";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), label8.Text.ToString().Trim(), vParam.ToString());
            string proced = ObjDetCatalogo.CodDetCat;

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
                this.Grid1.Rows.Add(new[] { textBox3.Text.ToString(), label16.Text.ToString(), label12.Text.ToString(), label8.Text.ToString(), label13.Text.ToString(), textBox4.Text.ToString(), marca.ToString(), unidad.ToString(), proced.ToString() });
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
            frmBusArticulo.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                int index = Grid1.CurrentRow.Index;

                for (int i = dgvOculto.Rows.Count - 1; i >= 0; i--)
                {
                    DataGridViewRow row = dgvOculto.Rows[i];

                    if (Grid1.Rows[index].Cells[0].Value.ToString().Equals(row.Cells[0].Value.ToString()))
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
                MessageBox.Show("Ingrese concepto de Ingreso", "SISTEMA");
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

            if (MessageBox.Show("Datos Correctos, se procedera a registrar el ingreso a Almacén", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            string Usuario = FrmLogin.x_login_usuario;
            //Datos de Cabecera de Venta
            //Generar Id
            string nValFec = DTP1.Value.Year.ToString() + DTP1.Value.Month.ToString("000") + DTP1.Value.Day.ToString("000");
            string nId = "";
            //Buscar Codigo de Doc
            string vMod = "ING";
            string vNomDoc = comboBox2.Text;
            string codDoc = "";
            if (ObjDocumento.BuscarDocNomMod(vNomDoc, vMod))
            {
                codDoc = ObjDocumento.Codigo.ToString();
            }

            nId = nValFec + codDoc + textBox1.Text.ToString() + "-" + textBox2.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();

            if (FrmIngresosAlm.nmIng == 'M')
            {
                ObjIngreso.Id = Cod.ToString();
            }
            else
            {
                //Validamos que el ingreso no exista
                if (ObjIngreso.BuscarIngreso(nId.ToString(), codAlmacen.ToString(), rucEmpresa.ToString()))
                {
                    MessageBox.Show("Ingreso registrado, Verifique", "SISTEMA");
                    textBox1.Focus();
                    return;
                }

                ObjIngreso.Id = nId.ToString();
            }

            ObjIngreso.Fecha = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
            string vParam = "1";
            string vCodCat = "016";
            ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), comboBox3.Text.ToString().Trim(), vParam.ToString());
            ObjIngreso.Concepto = ObjDetCatalogo.CodDetCat;
            ObjIngreso.TipDoc = codDoc.ToString();
            ObjIngreso.Serie = textBox1.Text;
            ObjIngreso.Numero = textBox2.Text;
            ObjIngreso.Referencia = textBox10.Text;
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

            ObjIngreso.Responsable = codResp.ToString();
            ObjIngreso.Almacen = codAlmacen.ToString();
            ObjIngreso.Empresa = rucEmpresa.ToString();
            ObjIngreso.UserCreacion = Usuario.ToString();
            ObjIngreso.UserModi = Usuario.ToString();
            /*-------------------------------------------------------------------------*/
            /*Datos Ingreso Detalle-----------------------------------------------------*/
            //En caso sea una modificacion
            if (FrmIngresosAlm.nmIng == 'M')
            {
                ObjIngreso.DevolverStock(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
                ObjDetIngreso.Eliminar(Cod.ToString(), codAlmacen.ToString(), rucEmpresa.ToString());
            }
            //----------------------------------------------------------------------
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (FrmIngresosAlm.nmIng == 'M')
                {
                    ObjDetIngreso.Id = Cod.ToString();
                }
                else
                {
                    ObjDetIngreso.Id = nId.ToString();
                }
                ObjDetIngreso.Codigo = row.Cells[0].Value.ToString();
                ObjDetIngreso.Marca = row.Cells[6].Value.ToString().Trim();
                ObjDetIngreso.Unidad = row.Cells[8].Value.ToString().Trim();
                ObjDetIngreso.Proce = row.Cells[7].Value.ToString().Trim();
                ObjDetIngreso.Cantidad = Double.Parse(row.Cells[5].Value.ToString().Trim());
                ObjDetIngreso.Almacen = codAlmacen.ToString();
                ObjDetIngreso.Empresa = rucEmpresa.ToString();
                ObjDetIngreso.UserCreacion = Usuario.ToString();
                ObjDetIngreso.UserModi = Usuario.ToString();
                if (ObjDetIngreso.Crear())
                {
                }
                else
                {
                    MessageBox.Show("No se registro correctamente el detalle de Ingreso", "SISTEMA");
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
                ObjCodigoBarras.TipodeMovimiento = "I";

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

            if (FrmIngresosAlm.nmIng == 'N')
            {
                if (ObjIngreso.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    textBox10.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    Grid1.Rows.Clear();
                    dgvOculto.Rows.Clear();
                    textBox1.Focus();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjIngreso.Modificar())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    textBox10.Clear();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    string val = "V";
                    Opener.CargarConsulta(val);
                    dgvOculto.Rows.Clear();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
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

        private void tabControl1_Click(object sender, EventArgs e)
        {
            LimpiarBarras();
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