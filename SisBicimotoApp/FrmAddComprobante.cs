using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddComprobante : Form
    {
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsImprimir ObjImprimir = new ClsImprimir();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsAlmacen ObjAlmacen = new ClsAlmacen();

        //ClsTipoCambio ObjTipoCambio = new ClsTipoCambio();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string Cod = "";
        private string vIdVenta = ""; //Id de venta

        public FrmAddComprobante()
        {
            InitializeComponent();
        }

        private void buscarDocAsociado(string idVenta, string vRuc, string vAlmacen)
        {
            string cDoc = comboBox1.SelectedValue.ToString();
            if (ObjVenta.BuscarComprobate(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
            {
                DateTime vFecha = DateTime.Parse(ObjVenta.Fecha.ToString());
                textBox10.Text = String.Format("{0:dd/MM/yyyy}", vFecha);
                textBox2.Text = ObjVenta.TipDocCli.ToString();
                textBox1.Text = ObjVenta.Cliente.ToString();
                if (ObjVenta.Cliente.ToString().Equals("C0000000001"))
                {
                    textBox2.Text = "";
                    textBox1.Text = "";
                }

                //Cliente
                if (ObjCliente.BuscarCLiente(ObjVenta.Cliente.ToString(), rucEmpresa))
                {
                    textBox5.Text = ObjCliente.Nombre.ToString();
                    textBox6.Text = ObjCliente.Direccion.ToString();
                }
                else
                {
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                textBox7.Text = ObjVenta.TBruto.ToString();
                double Net = 0;
                //Total bruto
                Net = double.Parse(ObjVenta.TBruto.ToString().Equals("") ? "0" : ObjVenta.TBruto.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox7.Text = "0.00";
                else
                    textBox7.Text = Net.ToString("###,##0.0000").Trim();
                //Total Igv
                Net = double.Parse(ObjVenta.TIgv.ToString().Equals("") ? "0" : ObjVenta.TIgv.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox8.Text = "0.00";
                else
                    textBox8.Text = Net.ToString("###,##0.0000").Trim();

                //Importe Total
                Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox9.Text = "0.00";
                else
                    textBox9.Text = Net.ToString("###,##0.0000").Trim();

                vIdVenta = ObjVenta.Cliente.ToString() + vIdVenta;
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para el Comprobante ingresado, verifique.", "SISTEMA");
                /*textBox7.Text = "";
                CalculaTotal();
                Grid1.Rows.Clear();*/
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddComprobante_Load(object sender, EventArgs e)
        {
            //Buscar comprobantes autocorrelativos
            string vModulo = "VEN";
            //DataSet datosDoc = csql.dataset("Call SpDocBusAutonumerico('" + vModulo.ToString() + "')");
            DataSet datosDoc = csql.dataset("Call SpDocBusElectronicos('" + vModulo.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "Codigo";
            comboBox1.DataSource = datosDoc.Tables[0];
            comboBox1.SelectedIndex = 0;
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
                    textBox4.Text = Net.ToString("000").Trim();
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
                textBox3.Focus();
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
                button3.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Equals(""))
                {
                    MessageBox.Show("Ingrese Tipo de Comprobante", "SISTEMA");
                    comboBox1.Focus();
                    return;
                }

                if (textBox4.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Serie de Comprobante", "SISTEMA");
                    textBox4.Focus();
                    return;
                }

                if (textBox3.TextLength == 0)
                {
                    MessageBox.Show("Ingrese Número de Comprobante", "SISTEMA");
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "")
            {
                MessageBox.Show("Ingrese Número de Comprobante", "SISTEMA");
                textBox4.Focus();
                return;
            }

            if (vIdVenta.Equals(""))
            {
                MessageBox.Show("Ingrese Número de Comprobante", "SISTEMA");
                textBox4.Focus();
                return;
            }

            IAddComprobante parent = this.Owner as IAddComprobante;
            parent.AddNewItem(vIdVenta.ToString());
            this.Close();
        }
    }
}