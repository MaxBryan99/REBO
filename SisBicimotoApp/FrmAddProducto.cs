using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddProducto : Form, IProveedor
    {
        public IProveedor Opener { get; set; }

        private ClsParametro ObjParametro = new ClsParametro();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsProducto ObjProducto = new ClsProducto();
        private ClsProveedor ObjProveedor = new ClsProveedor();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;

        public FrmAddProducto()
        {
            InitializeComponent();
        }

        #region IProveedor Members

        public void SelectItem(string codProv)
        {
            textBox9.Text = codProv;
        }

        #endregion IProveedor Members

        private void LlenarCampos(string InCod)
        {
            try
            {
                if (ObjProducto.BuscarProducto(InCod, rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    textBox1.Text = ObjProducto.CodArt.ToString().Trim();
                    textBox2.Text = ObjProducto.Nombre.ToString().Trim();
                    textBox3.Text = ObjProducto.CodInternac.ToString().Trim();
                    comboBox3.Text = ObjProducto.TipProducto.ToString().Trim();
                    comboBox6.Text = ObjProducto.CodMarca.ToString().Trim();
                    comboBox5.Text = ObjProducto.CodModelo.ToString().Trim();
                    comboBox1.Text = ObjProducto.CodFamilia.ToString().Trim();
                    comboBox2.Text = ObjProducto.CodLinea.ToString().Trim();
                    comboBox4.Text = ObjProducto.CodProced.ToString().Trim();
                    comboBox7.Text = ObjProducto.CodUnidad.ToString().Trim();
                    comboBox8.Text = ObjProducto.TipMoneda.ToString().Trim();

                    if (ObjProducto.GenStock.Equals("S"))
                    {
                        comboBox9.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox9.SelectedIndex = 1;
                    }
                    if (ObjProducto.VentaMin.Equals("S"))
                    {
                        comboBox12.SelectedIndex = 0;
                    }
                    else
                    {
                        comboBox12.SelectedIndex = 1;
                    }

                    //PorVenta
                    double PorVenta = 0;
                    PorVenta = double.Parse(ObjProducto.PorVenta.ToString().Equals("") ? "0" : ObjProducto.PorVenta.ToString().Trim());
                    if (PorVenta.ToString().Trim().Equals("0"))
                    {
                        textBox18.Text = "";
                    }
                    else
                    {
                        if(PorVenta == 25)
                        {
                            textBox18.Text = PorVenta.ToString("###,##0.00").Trim();
                            checkBox2.Checked = true;
                            comboBox14.SelectedIndex = 1;
                        }
                        else
                        {
                            if (PorVenta == 30)
                            {
                                textBox18.Text = PorVenta.ToString("###,##0.00").Trim();
                                checkBox2.Checked = true;
                                comboBox14.SelectedIndex = 2;
                            }
                            else
                            {
                                if (PorVenta == 35)
                                {
                                    textBox18.Text = PorVenta.ToString("###,##0.00").Trim();
                                    checkBox2.Checked = true;
                                    comboBox14.SelectedIndex = 3;
                                }
                                else
                                {
                                    if (PorVenta == 40)
                                    {
                                        textBox18.Text = PorVenta.ToString("###,##0.00").Trim();
                                        checkBox2.Checked = true;
                                        comboBox14.SelectedIndex = 4;
                                    }
                                    else
                                    {
                                        if (PorVenta == 45)
                                        {
                                            textBox18.Text = PorVenta.ToString("###,##0.00").Trim();
                                            checkBox2.Checked = true;
                                            comboBox14.SelectedIndex = 5;
                                        }
                                        else
                                        {
                                            checkBox2.Checked = true;
                                            textBox18.Text = PorVenta.ToString("###,##0.00").Trim();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Pcosto
                    double Pcosto = 0;
                    Pcosto = double.Parse(ObjProducto.PCosto.ToString().Equals("") ? "0" : ObjProducto.PCosto.ToString().Trim());
                    if (Pcosto.ToString().Trim().Equals("0"))
                        textBox5.Text = "";
                    else
                        textBox5.Text = Pcosto.ToString("###,##0.0000").Trim();
                    //PFOB
                    double PFOB = 0;
                    PFOB = double.Parse(ObjProducto.PFOB.ToString().Equals("") ? "0" : ObjProducto.PFOB.ToString().Trim());
                    if (PFOB.ToString().Trim().Equals("0"))
                        textBox15.Text = "";
                    else
                        textBox15.Text = PFOB.ToString("###,##0.0000").Trim();
                    //PCIF
                    double PCIF = 0;
                    PCIF = double.Parse(ObjProducto.PCIF.ToString().Equals("") ? "0" : ObjProducto.PCIF.ToString().Trim());
                    if (PCIF.ToString().Trim().Equals("0"))
                        textBox16.Text = "";
                    else
                        textBox16.Text = PCIF.ToString("###,##0.0000").Trim();

                    //Pventa
                    double Pventa = 0;
                    Pventa = double.Parse(ObjProducto.PVenta.ToString().Equals("") ? "0" : ObjProducto.PVenta.ToString().Trim());
                    if (Pventa.ToString().Trim().Equals("0"))
                        textBox6.Text = "";
                    else
                        textBox6.Text = Pventa.ToString("###,##0.0").Trim();

                    //Pmayorista
                    double Pmayorista = 0;
                    Pmayorista = double.Parse(ObjProducto.PMayorista.ToString().Equals("") ? "0" : ObjProducto.PMayorista.ToString().Trim());
                    if (Pmayorista.ToString().Trim().Equals("0"))
                        textBox8.Text = "";
                    else
                        textBox8.Text = Pmayorista.ToString("###,##0.0000").Trim();

                    //Pmayorista
                    double Pvolumen = 0;
                    Pvolumen = double.Parse(ObjProducto.PVolumen.ToString().Equals("") ? "0" : ObjProducto.PVolumen.ToString().Trim());
                    if (Pvolumen.ToString().Trim().Equals("0"))
                        textBox11.Text = "";
                    else
                        textBox11.Text = Pvolumen.ToString("###,##0.0000").Trim();

                    //Stminimo
                    double Stminimo = 0;
                    Stminimo = double.Parse(ObjProducto.StockMin.ToString().Equals("") ? "0" : ObjProducto.StockMin.ToString().Trim());
                    if (Stminimo.ToString().Trim().Equals("0"))
                        textBox4.Text = "";
                    else
                        textBox4.Text = Stminimo.ToString("").Trim();

                    //Venta Minima por Volumen
                    //Pmayorista
                    double CantVolumen = 0;
                    CantVolumen = double.Parse(ObjProducto.CantPrecioVolum.ToString().Equals("") ? "0" : ObjProducto.CantPrecioVolum.ToString().Trim());
                    if (CantVolumen.ToString().Trim().Equals("0"))
                        textBox17.Text = "";
                    else
                        textBox17.Text = CantVolumen.ToString("").Trim();
                    

                    textBox7.Text = ObjProducto.Ubicacion.ToString().Trim();
                    textBox9.Text = ObjProducto.Proveedor.ToString().Trim();
                    comboBox11.Text = ObjProducto.TSerie.ToString().Trim();
                    checkBox1.Checked = ObjProducto.IsVehiculo.Equals("S") ? true : false;
                    comboBox10.Text = ObjProducto.Est.ToString().Trim();

                    textBox10.Text = ObjProducto.Descricpcion.ToString().Trim();
                    byte[] Bitsdatos = new byte[0];
                    Bitsdatos = (byte[])ObjProducto.Image;
                    pictureBox1.Image = ImageHelper.ByteArrayToImage(Bitsdatos);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

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

        public byte[] imageToByteArray(System.Drawing.Image imagen)
        {
            if (imagen == null)// por si la imagen no es obligatoria en bb.dd.
            {
                return null;
            }
            MemoryStream ms = new MemoryStream();
            imagen.Save(ms, imagen.RawFormat);
            imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        private void BusProveedor(string vRuc, string vRucEmpresa)
        {
            if (ObjProveedor.BuscarProveedor(vRuc, vRucEmpresa))
            {
                label20.Text = ObjProveedor.Nombre.ToString().Trim();
            }
            else
            {
                label20.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog BuscarImagen = new OpenFileDialog();
                BuscarImagen.Filter = "Archivo JPG|*.jpg";
                BuscarImagen.FileName = "";
                BuscarImagen.Title = "Buscar Imagen de Producto";
                BuscarImagen.InitialDirectory = "C:\\";
                if (BuscarImagen.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = Image.FromFile(BuscarImagen.FileName);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = null;
        }

        private void FrmAddProducto_Load(object sender, EventArgs e)
        {
            //Carga Tipo de Producto
            string codCatTipPro = "012";
            string tipPresDoc = "1";
            DataSet datos = csql.dataset_cadena("Call SpCargarDetCat('" + codCatTipPro + "','" + tipPresDoc + "')");

                     comboBox3.Items.Add("");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    comboBox3.Items.Add(fila[1].ToString());
                }
            }

            //Carga Marca
            string codCatMarca = "010";
            DataSet datosMarca = csql.dataset_cadena("Call SpCargarDetCat('" + codCatMarca + "','" + tipPresDoc + "')");

            if (datosMarca.Tables[0].Rows.Count > 0)
            {
                comboBox6.Items.Add("");
                foreach (DataRow fila in datosMarca.Tables[0].Rows)
                {
                    comboBox6.Items.Add(fila[1].ToString());
                }
            }

            //Cargar ComboBox14
            comboBox14.Items.Add("");
            comboBox14.Items.Add("25");
            comboBox14.Items.Add("30");
            comboBox14.Items.Add("35");
            comboBox14.Items.Add("40");
            comboBox14.Items.Add("45");

            //Carga Modelo
            string codCatModel = "011";
            DataSet datosModel = csql.dataset_cadena("Call SpCargarDetCat('" + codCatModel + "','" + tipPresDoc + "')");

            if (datosModel.Tables[0].Rows.Count > 0)
            {
                comboBox5.Items.Add("");
                foreach (DataRow fila in datosModel.Tables[0].Rows)
                {
                    comboBox5.Items.Add(fila[1].ToString());
                }
            }

            //Carga Familia
            string codCatFam = "008";

            DataSet datosFam = csql.dataset("Call SpCargarDetCat('" + codCatFam + "','" + tipPresDoc + "')");
            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "CodDetCat";
            comboBox1.DataSource = datosFam.Tables[0];

            //Carga Procedencia
            string codCatProc = "009";
            DataSet datosProc = csql.dataset_cadena("Call SpCargarDetCat('" + codCatProc + "','" + tipPresDoc + "')");

            if (datosProc.Tables[0].Rows.Count > 0)
            {
                comboBox4.Items.Add("");
                foreach (DataRow fila in datosProc.Tables[0].Rows)
                {
                    comboBox4.Items.Add(fila[1].ToString());
                }
            }

            //Carga Unidad
            string codCatUnid = "013";
            DataSet datosUnd = csql.dataset_cadena("Call SpCargarDetCat('" + codCatUnid + "','" + tipPresDoc + "')");

            if (datosUnd.Tables[0].Rows.Count > 0)
            {
                comboBox7.Items.Add("");
                foreach (DataRow fila in datosUnd.Tables[0].Rows)
                {
                    comboBox7.Items.Add(fila[1].ToString());
                }
            }

            //Carga Moneda
            string codCatMon = "001";
            DataSet datosMon = csql.dataset_cadena("Call SpCargarDetCat('" + codCatMon + "','" + tipPresDoc + "')");

            if (datosMon.Tables[0].Rows.Count > 0)
            {
                comboBox8.Items.Add("");
                foreach (DataRow fila in datosMon.Tables[0].Rows)
                {
                    comboBox8.Items.Add(fila[1].ToString());
                }
            }

            comboBox9.SelectedIndex = 0;
            comboBox11.SelectedIndex = 0;
            comboBox10.SelectedIndex = 0;
            comboBox12.SelectedIndex = 0;
            comboBox13.SelectedIndex = 0;
            string nParam = "5";
            //Verificar Autogenerado de Producto
            if (ObjParametro.BuscarParametro(nParam))
            {
                if (ObjParametro.Valor.Equals("S"))
                {
                    textBox1.Enabled = false;
                    textBox2.Focus();
                }
                else
                {
                    textBox1.Enabled = true;
                    textBox1.Focus();
                }
            }

            //Si es modificacion se cargan valores de producto seleccionado
            string Cod = FrmArticulos.codArt.ToString();
            if (FrmArticulos.nmPro == 'M')
            {
                LlenarCampos(Cod);
                textBox1.Enabled = false;
                textBox2.Focus();
            }
            else
            {
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                textBox14.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Carga Linea
            ObjDetCatalogo.BuscarDetCatalogoCod("008", comboBox1.SelectedValue.ToString());
            string codFamilia = comboBox1.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();

            DataSet datosLin = csql.dataset("Call SpLineaBusFam('" + codFamilia.ToString() + "','" + rucEmpresa.ToString() + "')");
            comboBox2.DisplayMember = "Descripcion";
            comboBox2.ValueMember = "Codigo";
            comboBox2.DataSource = datosLin.Tables[0];
            comboBox2.Text = "";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar) || //Espaçio
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
                comboBox6.Focus();
            }
        }

        private void comboBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox5.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
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
                comboBox7.Focus();
            }
        }

        private void comboBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox8.Focus();
            }
        }

        private void comboBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox15.Focus();
            }
        }

        private void comboBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox5.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < textBox5.Text.Length; i++)
            {
                if (textBox5.Text[i] == '.')
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
                textBox6.Focus();
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

            for (int i = 0; i < textBox6.Text.Length; i++)
            {
                if (textBox6.Text[i] == '.')
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
                textBox8.Focus();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < textBox8.Text.Length; i++)
            {
                if (textBox8.Text[i] == '.')
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
                textBox11.Focus();
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
                comboBox12.Focus();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                checkBox1.Focus();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox7.Focus();
            }
        }

        private void comboBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox9.Focus();
            }
        }

        private void comboBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox10.Focus();
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
                    textBox5.Text = Net.ToString("###,##0.0000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox5.Focus();
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
                    textBox6.Text = Net.ToString("###,##0.0").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox6.Focus();
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
                    textBox8.Text = Net.ToString("###,##0.0000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox8.Focus();
            }
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
                if (textBox1.Enabled == true && (textBox1.TextLength == 0))
            {
                MessageBox.Show("Ingrese Código de Producto", "REBO");
                textBox1.Focus();
                return;
            }

            if (comboBox12.SelectedIndex.Equals(1))
            {

                textBox4.Enabled = false;

            }
            else if (comboBox12.SelectedIndex.Equals(2))
            {
                textBox4.Enabled = true;

            }

            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre de Producto", "REBO");
                textBox2.Focus();
                return;
            }

            if (comboBox3.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Tipo de Producto", "REBO");
                comboBox3.Focus();
                return;
            }

            if (comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Familia de Producto", "REBO");
                comboBox1.Focus();
                return;
            }

            if (comboBox8.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Tipo de Moneda", "REBO");
                comboBox8.Focus();
                return;
            }

            if (comboBox9.Text.Equals(""))
            {
                MessageBox.Show("Seleccione si el producto Genera Stock", "REBO");
                comboBox9.Focus();
                return;
            }

            if (textBox5.TextLength == 0)
            {
                MessageBox.Show("Ingrese Precio de Costo de Producto", "REBO");
                textBox5.Focus();
                return;
            }

            if (textBox6.TextLength == 0)
            {
                MessageBox.Show("Ingrese Precio de Venta de Producto", "SISTEMA");
                textBox6.Focus();
                return;
            }

            if (comboBox11.Text.Equals(""))
            {
                MessageBox.Show("Defina si el Producto tiene Serie", "SISTEMA");
                comboBox11.Focus();
                return;
            }

            if (comboBox10.Text.Equals(""))
            {
                MessageBox.Show("Seleccione Estado del Producto", "SISTEMA");
                comboBox10.Focus();
                return;
            }

            if (textBox9.TextLength > 0 && textBox9.TextLength < 11)
            {
                MessageBox.Show("Ingrese número de RUC completo", "SISTEMA");
                textBox4.Focus();
                return;
            }

            if (textBox18.TextLength == 0)
            {
                MessageBox.Show("Ingrese porcentaje de venta", "SISTEMA");
                textBox18.Focus();
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            ObjProducto.CodArt = textBox1.Text.Trim();
            ObjProducto.Nombre = textBox2.Text.Trim();
            ObjProducto.CodInternac = textBox3.Text.Trim();
            //Tipo de producto
            ObjDetCatalogo.BuscarDetCatalogoDes("012", comboBox3.Text.Trim(), "1");
            ObjProducto.TipProducto = comboBox3.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            //Marca
            ObjDetCatalogo.BuscarDetCatalogoDes("010", comboBox6.Text.Trim(), "1");
            ObjProducto.CodMarca = comboBox6.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            //Modelo
            ObjDetCatalogo.BuscarDetCatalogoDes("011", comboBox5.Text.Trim(), "1");
            ObjProducto.CodModelo = comboBox5.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            //Familia
            ObjProducto.CodFamilia = comboBox1.SelectedValue.ToString();
            //Linea
            if (!comboBox2.Text.Equals(""))
            {
                ObjProducto.CodLinea = comboBox2.SelectedValue.ToString();
            }
            //Procedencia
            ObjDetCatalogo.BuscarDetCatalogoDes("009", comboBox4.Text.Trim(), "1");
            ObjProducto.CodProced = comboBox4.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            //Unidad
            ObjDetCatalogo.BuscarDetCatalogoDes("013", comboBox7.Text.Trim(), "1");
            ObjProducto.CodUnidad = comboBox7.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            //Moneda
            ObjDetCatalogo.BuscarDetCatalogoDes("001", comboBox8.Text.Trim(), "1");
            ObjProducto.TipMoneda = comboBox8.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();

            ObjProducto.GenStock = comboBox9.Text.Substring(0, 1);
            ObjProducto.PCosto = double.Parse(textBox5.Text.ToString().Equals("") ? "0" : textBox5.Text.ToString().Trim());
            ObjProducto.PVenta = double.Parse(textBox6.Text.ToString().Equals("") ? "0" : textBox6.Text.ToString().Trim());
            ObjProducto.PMayorista = double.Parse(textBox8.Text.ToString().Equals("") ? "0" : textBox8.Text.ToString().Trim());
            ObjProducto.PVolumen = double.Parse(textBox11.Text.ToString().Equals("") ? "0" : textBox11.Text.ToString().Trim());
            ObjProducto.StockMin = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
            ObjProducto.Ubicacion = textBox7.Text.ToString().Trim();
            ObjProducto.Proveedor = textBox9.Text.ToString().Trim();
            ObjProducto.TSerie = comboBox11.Text.Substring(0, 1);
            ObjProducto.IsVehiculo = (checkBox1.Checked == true) ? "S" : "N";
            ObjProducto.Descricpcion = textBox10.Text.Trim();
            ObjProducto.Est = (comboBox10.Text.Substring(0, 1).Equals("A")) ? "A" : "N";
            ObjProducto.PorVenta = double.Parse(textBox18.Text.ToString().Equals("") ? "0" : textBox18.Text.ToString().Trim());

            //Imagen
            ObjProducto.RucEmpresa = rucEmpresa.ToString();
            ObjProducto.UserCreacion = Usuario.ToString().Trim();
            ObjProducto.UserModi = Usuario.ToString().Trim();
            ObjProducto.Almacen = codAlmacen.ToString().Trim();
            ObjProducto.PFOB = double.Parse(textBox15.Text.ToString().Equals("") ? "0" : textBox15.Text.ToString().Trim());
            ObjProducto.PCIF = double.Parse(textBox16.Text.ToString().Equals("") ? "0" : textBox16.Text.ToString().Trim());
            ObjProducto.VentaMin = comboBox12.Text.Substring(0, 1);
            ObjProducto.StockReal = comboBox13.Text.Substring(0, 1);
            ObjProducto.CantPrecioVolum = double.Parse(textBox17.Text.ToString().Equals("") ? "0" : textBox17.Text.ToString().Trim());
            
            if (FrmArticulos.nmPro == 'N')
            {
                if (ObjProducto.Crear())
                {
                    //if (pictureBox1.Image != null) {
                    ImageHelper.GuardarImagen(pictureBox1.Image, textBox1.Text.Trim(), rucEmpresa.ToString());
                    //}

                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    // this.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox8.Text = "";
                    textBox11.Text = "";
                    textBox15.Text = "";
                    textBox16.Text = "";
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjProducto.Modificar())
                {
                    //if (pictureBox1.Image != null)
                    //{
                    ImageHelper.GuardarImagen(pictureBox1.Image, textBox1.Text.Trim(), rucEmpresa.ToString());
                    //}
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBusProveedor frmBusProveedor = new FrmBusProveedor();
            frmBusProveedor.WindowState = FormWindowState.Normal;
            frmBusProveedor.Opener = this;
            frmBusProveedor.MdiParent = this.MdiParent;
            frmBusProveedor.Show();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
            {
                label20.Text = "";
            }
            else
            {
                string rucProv = textBox9.Text.ToString().Trim();
                BusProveedor(rucProv, rucEmpresa);
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < textBox11.Text.Length; i++)
            {
                if (textBox11.Text[i] == '.')
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
                textBox4.Focus();
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
                    textBox11.Text = Net.ToString("###,##0.0000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox11.Focus();
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox16.Focus();
            }
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox5.Focus();
            }
        }

        private void textBox15_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox15.Text.ToString().Equals("") ? "0" : textBox15.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox15.Text = "";
                else
                    textBox15.Text = Net.ToString("###,##0.0000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox15.Focus();
            }
        }

        private void textBox16_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox16.Text.ToString().Equals("") ? "0" : textBox16.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox16.Text = "";
                else
                    textBox16.Text = Net.ToString("###,##0.0000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox16.Focus();
            }
        }

        private void comboBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox11.Focus();
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
        }

        private void label20_Click(object sender, EventArgs e)
        {
        }

        private void textBox13_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox12_TextChanged(object sender, EventArgs e)
        {
            if (comboBox12.Text.ToString() == "Si")
            {

                textBox4.Enabled = true;

            }
            else 
            {
                textBox4.Enabled = false;

            }
        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox14_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true && comboBox14.SelectedItem != null)
            {
                double porventa = double.Parse(comboBox14.Text.ToString().Equals("") ? "0" : comboBox14.Text.ToString().Trim());
                if (comboBox14.Text.Equals("25"))
                {
                    textBox18.Text = porventa.ToString().Trim();
                }
                else
                {
                    if (comboBox14.Text.Equals("30"))
                    {
                        textBox18.Text = porventa.ToString().Trim();
                    }
                    else
                    {
                        if (comboBox14.Text.Equals("35"))
                        {
                            textBox18.Text = porventa.ToString().Trim();
                        }
                        else
                        {
                            if (comboBox14.Text.Equals("40"))
                            {
                                textBox18.Text = porventa.ToString().Trim();
                            }
                            else
                            {
                                if (comboBox14.Text.Equals("45"))
                                {
                                    textBox18.Text = porventa.ToString().Trim();
                                }
                                else
                                {
                                    textBox18.Text = "";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (checkBox2.Checked == false)
                {
                    textBox18.Text = "";
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true && comboBox14.SelectedItem != null)
            {
                double porventa = double.Parse(comboBox14.Text.ToString().Equals("") ? "0" : comboBox14.Text.ToString().Trim());
                if (comboBox14.Text.Equals("25"))
                {
                    textBox18.Text = porventa.ToString().Trim();
                }
                else
                {
                    if (comboBox14.Text.Equals("30"))
                    {
                        textBox18.Text = porventa.ToString().Trim();
                    }
                    else
                    {
                        if (comboBox14.Text.Equals("35"))
                        {
                            textBox18.Text = porventa.ToString().Trim();
                        }
                        else
                        {
                            if (comboBox14.Text.Equals("40"))
                            {
                                textBox18.Text = porventa.ToString().Trim();
                            }
                            else
                            {
                                if (comboBox14.Text.Equals("45"))
                                {
                                    textBox18.Text = porventa.ToString().Trim();
                                }
                                else
                                {
                                    textBox18.Text = "";
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (checkBox2.Checked == false)
                {
                    textBox18.Text = "";
                }
            }
        }

        private void textBox18_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox18.Text.ToString().Equals("") ? "0" : textBox18.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox18.Text = "";
                else
                    textBox18.Text = Net.ToString("###,##0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox18.Focus();
            }
        }

        private void textBox18_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (e.KeyChar == 8)
            {
                e.Handled = false;
                return;
            }

            bool IsDec = false;
            int nroDec = 0;

            for (int i = 0; i < textBox18.Text.Length; i++)
            {
                if (textBox18.Text[i] == '.')
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
                textBox18.Focus();
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            double PrecioCosto = Double.Parse(textBox5.Text.ToString().Equals("") ? "0" : textBox5.Text.ToString().Trim());
            double PocVenta = Double.Parse(textBox18.Text.ToString().Equals("") ? "0" : textBox18.Text.ToString().Trim());

            double PrecioVenta = 0;
            PrecioVenta = PrecioCosto + (PrecioCosto * PocVenta/100);
            textBox6.Text = PrecioVenta.ToString("###,##0.0").Trim();
        }
    }
}