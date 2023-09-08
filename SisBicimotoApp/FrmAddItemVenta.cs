using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddItemVenta : Form, IArticulo
    {
        private ClsProducto ObjProducto = new ClsProducto();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private ClsParametro ObjParametro = new ClsParametro();
        private string nParam = "";

        public FrmAddItemVenta()
        {
            InitializeComponent();
        }

        #region IArticulo Members

        public void SelectItem(string codArt)
        {
            textBox2.Text = codArt;
        }

        #endregion IArticulo Members

        private void BusProducto(string vProducto, string vRucEmpresa)
        {
            if (ObjProducto.BuscarProductoActivo(vProducto, vRucEmpresa, codAlmacen))
            {
                label4.Text = ObjProducto.Nombre.ToString().Trim();
                label1.Text = ObjProducto.CodUnidad.ToString().Trim();
                label12.Text = ObjProducto.CodMarca.ToString().Trim();
                label16.Text = ObjProducto.CodProced.ToString().Trim();
                if (ObjProducto.GenStock.ToString().Equals("S") || ObjProducto.GenStock.ToString().Equals(""))
                {
                    label34.Text = "S";
                }
                else
                {
                    label34.Text = "N";
                }

                //Buscar Tipo de Producto
                string vParam = "1";
                string vCodCat = "012";
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjProducto.TipProducto.ToString(), vParam.ToString());
                if (ObjDetCatalogo.CodDetCat.ToString().Equals("001"))
                {
                    label5.Visible = false;
                    label5.Text = "";
                }
                else
                {
                    label5.Visible = true;
                    label5.Text = "Producto Servicio";
                }

                ObtenerPrecios();

                //Buscar Stock
                if (ObjProducto.BuscarStock(vProducto, vRucEmpresa, codAlmacen))
                {
                    label10.Text = ObjProducto.Stock.ToString("###,##0.00");
                }
                else
                {
                    label10.Text = "";
                }

                //CalcularImportes();
            }
            else
            {
                label4.Text = "";
                label1.Text = "";
                label12.Text = "";
                label16.Text = "";
                textBox3.Text = "";
            }
        }

        private void CalcularImportes()
        {
            double vPventa = 0;
            double vCantidad = 0;
            double vImporte = 0;
            double vIgv = 0;
            double vValorIgv = 0;
            double vDcto = 0;
            vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
            vDcto = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());

            vImporte = (vPventa * vCantidad) - vDcto;

            if (checkBox1.Checked == true)
            {
                //CalcularIgv();
                //Obtener IGV
                nParam = "3";
                if (ObjParametro.BuscarParametro(nParam))
                {
                    vIgv = Double.Parse(ObjParametro.Valor);
                }
                else
                {
                    vIgv = 0;
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

                double vPrec = 0;

                if (vTipAfecta.Equals("001"))
                {
                    if (vImporte <= 0)
                    {
                        ObtenerPrecios();

                        vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                        vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
                        vDcto = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                        vValorIgv = 0;
                        label13.Text = vValorIgv.ToString("###,##0.00").Trim();
                        vImporte = (vPventa * vCantidad) - vDcto;
                        label9.Text = vImporte.ToString("###,##0.00").Trim();
                    }
                    else
                    {
                        ObtenerPrecios();
                        vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                        vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
                        vDcto = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());

                        vValorIgv = (vImporte * vIgv) / (100 + vIgv);
                        vPrec = (vImporte - vValorIgv + vDcto) / vCantidad;
                        label13.Text = vValorIgv.ToString("###,##0.00").Trim();
                        textBox3.Text = vPrec.ToString("###,##0.0000").Trim();
                        label9.Text = vImporte.ToString("###,##0.00").Trim();
                    }
                }
                else
                {
                    //ObtenerPrecios();

                    vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                    vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
                    vDcto = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());

                    vImporte = (vPventa * vCantidad) - vDcto;

                    vValorIgv = (vImporte * vIgv) / 100;
                    vImporte = vImporte + vValorIgv;

                    label13.Text = vValorIgv.ToString("###,##0.0000").Trim();

                    if (vImporte.ToString().Trim().Equals("0"))
                        label9.Text = "";
                    else
                        label9.Text = vImporte.ToString("###,##0.0000").Trim();
                }
            }
            else
            {
                label13.Text = "";

                vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
                vDcto = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());

                vImporte = (vPventa * vCantidad) - vDcto;

                if (vImporte.ToString().Trim().Equals("0"))
                    label9.Text = "";
                else
                    label9.Text = vImporte.ToString("###,##0.0000").Trim();

                if (textBox2.TextLength > 0)
                {
                    if (ObjProducto.BuscarProducto(textBox2.Text, rucEmpresa, codAlmacen))
                    {
                        int caseSwitch = comboBox1.SelectedIndex;
                        switch (caseSwitch)
                        {
                            case 0:
                                textBox3.Text = ObjProducto.PVenta.ToString();
                                break;

                            case 1:
                                textBox3.Text = ObjProducto.PMayorista.ToString();
                                break;

                            case 2:
                                textBox3.Text = ObjProducto.PVolumen.ToString();
                                break;

                            default:
                                textBox3.Text = "";
                                break;
                        }

                        double Net = 0;
                        Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                        if (Net.ToString().Trim().Equals("0"))
                            textBox3.Text = "";
                        else
                            textBox3.Text = Net.ToString("###,##0.00").Trim();

                        //CalcularImportes();
                    }
                    else
                    {
                        textBox3.Text = "";
                    }
                }
            }
        }

        private void ObtenerPrecios()
        {
            if (textBox2.TextLength > 0)
            {
                if (ObjProducto.BuscarProducto(textBox2.Text, rucEmpresa, codAlmacen))
                {
                    int caseSwitch = comboBox1.SelectedIndex;
                    switch (caseSwitch)
                    {
                        case 0:
                            textBox3.Text = ObjProducto.PVenta.ToString();
                            break;

                        case 1:
                            textBox3.Text = ObjProducto.PMayorista.ToString();
                            break;

                        case 2:
                            textBox3.Text = ObjProducto.PVolumen.ToString();
                            break;

                        default:
                            textBox3.Text = "";
                            break;
                    }

                    double Net = 0;
                    Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox3.Text = "";
                    else
                        textBox3.Text = Net.ToString("###,##0.00").Trim();
                }
                else
                {
                    textBox3.Text = "";
                }
            }
        }

        private void CalcularIgv()
        {
            double vIgv = 0;
            double vTIgv = 0;
            double vImporte = 0;
            double vPventa = 0;
            double vCantidad = 0;

            vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
            vImporte = double.Parse(label9.Text.ToString().Equals("") ? "0" : label9.Text.ToString().Trim());

            //Obtener IGV
            nParam = "3";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vIgv = Double.Parse(ObjParametro.Valor);
            }
            else
            {
                vIgv = 0;
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

            double vMIgv = 0;

            if (vTipAfecta.Equals("001"))
            {
                vTIgv = (vImporte * vIgv) / (100 + vIgv);
                if (vTIgv.ToString().Trim().Equals("0"))
                    label13.Text = "";
                else
                    label13.Text = vTIgv.ToString("###,##0.0000").Trim();

                vMIgv = vImporte - vTIgv;

                textBox3.Text = vMIgv.ToString("###,##0.0000");
                /*if (vImporte.ToString().Trim().Equals("0"))
                    label9.Text = "";
                else*/
                label9.Text = vImporte.ToString("###,##0.0000").Trim();
            }
            else
            {
                vTIgv = (vImporte * vIgv) / 100;
                if (vTIgv.ToString().Trim().Equals("0"))
                    label13.Text = "";
                else
                    label13.Text = vTIgv.ToString("###,##0.0000").Trim();

                if (vImporte.ToString().Trim().Equals("0"))
                    label9.Text = "";
                else
                    label9.Text = vImporte.ToString("###,##0.0000").Trim();
            }
        }

        private void FrmAddItemVenta_Load(object sender, EventArgs e)
        {
            //Precios
            comboBox1.Items.Add("P.Unitario");
            comboBox1.Items.Add("P.Mayorista");
            comboBox1.Items.Add("P.Volumen");
            comboBox1.SelectedIndex = 0;

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
                if (FrmAddVenta.vTipVentaS.Equals("03"))
                {
                    checkBox1.Checked = false;
                }
                else
                {
                    checkBox1.Checked = true;
                }
            }
            else
            {
                checkBox1.Checked = false;
            }

            if (FrmAddVenta.vTipVentaS.Equals("04"))
            {
                textBox4.Enabled = true;
            }
            else
            {
                textBox4.Enabled = false;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox3.SelectionStart = 0;
                textBox3.SelectionLength = textBox3.TextLength;
                textBox3.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string codProducto = textBox2.Text.ToString().Trim();
            BusProducto(codProducto, rucEmpresa);
        }

        private void button2_Click(object sender, EventArgs e)
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
                if (textBox4.Enabled == true)
                {
                    textBox4.Focus();
                }
                else
                {
                    button1.Focus();
                }
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 0)
            {
                if (ObjProducto.BuscarProducto(textBox2.Text, rucEmpresa, codAlmacen))
                {
                    int caseSwitch = comboBox1.SelectedIndex;
                    switch (caseSwitch)
                    {
                        case 0:
                            textBox3.Text = ObjProducto.PVenta.ToString();
                            break;

                        case 1:
                            textBox3.Text = ObjProducto.PMayorista.ToString();
                            break;

                        case 2:
                            textBox3.Text = ObjProducto.PVolumen.ToString();
                            break;

                        default:
                            textBox3.Text = "";
                            break;
                    }

                    double Net = 0;
                    Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox3.Text = "";
                    else
                        textBox3.Text = Net.ToString("###,##0.00").Trim();

                }
                else
                {
                    textBox3.Text = "";
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
                    textBox3.Text = Net.ToString("###,##0.0000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox3.Focus();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerPrecios();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //CalcularImportes();
            double vPventa = 0;
            double vCantidad = 0;
            double vImporte = 0;
            double vDcto = 0;
            vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
            vDcto = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());

            //vImporte = Math.Round((vPventa * vCantidad), 1);
            vImporte = (vPventa * vCantidad) - vDcto;
            double valImp = Double.Parse(vImporte.ToString("###,##0.0").Trim());
            if (valImp >= 0)
            {
                if (vImporte.ToString().Trim().Equals("0"))
                    label9.Text = "";
                else
                    label9.Text = valImp.ToString("###,##0.00").Trim();

                if (checkBox1.Checked == true)
                {
                    CalcularImportes();
                }
            }
            else
            {
                label9.Text = "";
                CalcularImportes();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (FrmAddVenta.vTipVentaS.Equals("02"))
                {
                    MessageBox.Show("No se puede aplicar IGV, el tipo de Venta a SUNAt es EXONERADO", "SISTEMA");
                    checkBox1.Checked = false;
                    return;
                }
            }

            CalcularImportes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label4.Text.Equals(""))
            {
                MessageBox.Show("Ingrese Producto", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (textBox3.TextLength == 0)
            {
                MessageBox.Show("Ingrese Precio de venta", "SISTEMA");
                textBox3.Focus();
                return;
            }

            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Cantidad", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (comboBox1.Text == "")
            {
                MessageBox.Show("Ingrese Precio", "SISTEMA");
                comboBox1.Focus();
                return;
            }

            double vPrecio = Double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());

            double vCantidad = Double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());

            double vDcto = Double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());

            if (vDcto > vPrecio)
            {
                MessageBox.Show("El descuento no puede ser mayor que el precio de venta", "SISTEMA");
                textBox4.Focus();
                return;
            }

            if (label5.Visible == false)
            {
                double valStock = Double.Parse(label10.Text.ToString().Equals("") ? "0" : label10.Text.ToString().Trim());

                if (label34.Text == "S")
                {
                    if (valStock == 0 || (vCantidad > valStock))
                    {
                        MessageBox.Show("Stock no disponible", "SISTEMA");
                        textBox2.Focus();
                        return;
                    }
                }
            }

            double vIgv = Double.Parse(label13.Text.ToString().Equals("") ? "0" : label13.Text.ToString().Trim());

            if (checkBox1.Checked == true && vIgv == 0)
            {
                CalcularImportes();
            }

            double vPVenta = Double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            vIgv = Double.Parse(label13.Text.ToString().Equals("") ? "0" : label13.Text.ToString().Trim());
            double vTImporte = Double.Parse(label9.Text.ToString().Equals("") ? "0" : label9.Text.ToString().Trim());
            IAddItemVenta parent = this.Owner as IAddItemVenta;
            parent.AddNewItemVenta(textBox2.Text.ToString(), label4.Text.ToString(), label1.Text.ToString(), label12.Text.ToString(), vPVenta, vCantidad, vDcto, vIgv, vTImporte, label16.Text.ToString());

            if (FrmAddVenta.validaIgv == false)
            {
                //Limpiar
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                label10.Text = "";

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
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }

                textBox2.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
            frmBusArticulo.WindowState = FormWindowState.Normal;
            frmBusArticulo.Opener = this;
            frmBusArticulo.ShowDialog(this);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //CalcularImportes();
            double vPventa = 0;
            double vCantidad = 0;
            double vImporte = 0;
            vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());

            //vImporte = Math.Round((vPventa * vCantidad), 1);
            vImporte = (vPventa * vCantidad);
            double valImp = Double.Parse(vImporte.ToString("###,##0.0").Trim());
            if (vImporte.ToString().Trim().Equals("0"))
                label9.Text = "";
            else
                label9.Text = valImp.ToString("###,##0.00").Trim();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
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

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            ObtenerPrecios();

            //CalcularImportes();
            double vPventa = 0;
            double vCantidad = 0;
            double vImporte = 0;
            double vDcto = 0;
            vPventa = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
            vDcto = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());

            //vImporte = Math.Round((vPventa * vCantidad), 1);
            vImporte = (vPventa * vCantidad) - vDcto;
            double valImp = Double.Parse(vImporte.ToString("###,##0.0").Trim());
            if (vImporte.ToString().Trim().Equals("0"))
                label9.Text = "";
            else
                label9.Text = valImp.ToString("###,##0.00").Trim();

            if (checkBox1.Checked == true)
            {
                CalcularImportes();
            }
        }
    }
}