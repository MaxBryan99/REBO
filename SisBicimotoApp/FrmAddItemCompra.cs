using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddItemCompra : Form, IArticulo
    {
        //public IAddItem Opener { get; set; }

        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private ClsProducto ObjProducto = new ClsProducto();
        private ClsParametro ObjParametro = new ClsParametro();
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string nParam = "";

        public FrmAddItemCompra()
        {
            InitializeComponent();
        }

        #region IArticulo Members

        public void SelectItem(string codArt)
        {
            textBox2.Text = codArt;
        }

        #endregion IArticulo Members

        private void CalcularImportes()
        {
            double vPcosto = 0;
            double vCantidad = 0;
            double vPorDcto = 0;
            double vValDcto = 0;
            double vImporte = 0;
            double vIgv = 0;
            double vPercep = 0;
            double vValorIgv = 0;

            if (textBox3.TextLength > 0)
            {
                vPcosto = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());

                vImporte = (vPcosto * vCantidad);

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

                    double vPrec = 0;

                    vValorIgv = (vImporte * vIgv) / (100 + vIgv);
                    vPrec = (vImporte - vValorIgv) / vCantidad;
                    label13.Text = vValorIgv.ToString("###,##0.0000").Trim();
                    textBox3.Text = vPrec.ToString("###,##0.0000").Trim();
                    label9.Text = vImporte.ToString("###,##0.0000").Trim();

                }
                else
                {
                    vPcosto = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                    vIgv = double.Parse(label13.Text.ToString().Equals("") ? "0" : label13.Text.ToString().Trim());
                    vPcosto = vPcosto + vIgv;
                    vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());

                    label13.Text = "";

                    vImporte = (vPcosto * vCantidad);

                    if (vImporte.ToString().Trim().Equals("0"))
                        label9.Text = "";
                    else
                        label9.Text = vImporte.ToString("###,##0.0000").Trim();
                }
            }
        }

        private void CalcularIgv()
        {
            double vIgv = 0;
            double vTIgv = 0;
            double vImporte = 0;
            double vPcosto = 0;
            double vCantidad = 0;
            double vDcto = 0;
            double vPrec = 0;
            vPcosto = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            vCantidad = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
            //vImporte = vPcosto * vCantidad;
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

            vTIgv = (vImporte * vIgv) / (100 + vIgv);
            vPrec = (vImporte - vTIgv) / vCantidad;
            label13.Text = vTIgv.ToString("###,##0.00").Trim();
            textBox3.Text = vPrec.ToString("###,##0.00").Trim();
            label9.Text = vImporte.ToString("###,##0.00").Trim();
        }

        private void BusProducto(string vProducto, string vRucEmpresa)
        {
            if (ObjProducto.BuscarProductoActivo(vProducto, vRucEmpresa, codAlmacen))
            {
                label4.Text = ObjProducto.Nombre.ToString().Trim();
                label1.Text = ObjProducto.CodUnidad.ToString().Trim();
                label18.Text = ObjProducto.CodMarca.ToString().Trim();
                label20.Text = ObjProducto.CodModelo.ToString().Trim();

                CalcularImportes();
            }
            else
            {
                label4.Text = "";
                label1.Text = "";
                label18.Text = "";
                label20.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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
                MessageBox.Show("Ingrese Precio unitario de Producto", "SISTEMA");
                textBox3.Focus();
                return;
            }

            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Cantidad", "SISTEMA");
                textBox1.Focus();
                return;
            }
            double vCantidad = Double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
            double vPCosto = Double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
            double vIgv = Double.Parse(label13.Text.ToString().Equals("") ? "0" : label13.Text.ToString().Trim());
            double vTImporte = Double.Parse(label9.Text.ToString().Equals("") ? "0" : label9.Text.ToString().Trim());
            double vPercep = Double.Parse(label15.Text.ToString().Equals("") ? "0" : label15.Text.ToString().Trim());
            double vDcto = Double.Parse(label11.Text.ToString().Equals("") ? "0" : label11.Text.ToString().Trim());
            double vPorDcto = Double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
            IAddItemCompra parent = this.Owner as IAddItemCompra;
            parent.AddNewItem(textBox2.Text.ToString(), label4.Text.ToString(), label1.Text.ToString(), vPCosto, vCantidad, vIgv, vPorDcto, vDcto, vPercep, vTImporte);

            if (FrmAddCompra.validaIgv == false)
            {
                //Limpiar
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                textBox2.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string codProducto = textBox2.Text.ToString().Trim();
            BusProducto(codProducto, rucEmpresa);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsWhiteSpace(e.KeyChar) || //Espaçio
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
                //textBox4.Focus();
                button1.Focus();
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
                    textBox3.Text = Net.ToString("###,##0.0000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
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

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
            frmBusArticulo.WindowState = FormWindowState.Normal;
            frmBusArticulo.Opener = this;
            frmBusArticulo.ShowDialog(this);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //CalcularIgv();
            CalcularImportes();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //CalcularIgv();
            CalcularImportes();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //CalcularIgv();
            CalcularImportes();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CalcularImportes();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                CalcularImportes();
            }
            else
            {
                label15.Text = "";
                CalcularImportes();
            }
        }

        private void FrmAddItemCompra_Load(object sender, EventArgs e)
        {
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
    }
}