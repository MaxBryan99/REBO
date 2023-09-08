using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddPedido : Form, IArticulo
    {
        private string codModulo = "PED";
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsProducto ObjProducto = new ClsProducto();
        private ClsSerie ObjSerie = new ClsSerie();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string UserPedido = FrmLogin.x_login_usuario;

        public FrmAddPedido()
        {
            InitializeComponent();
        }

        public void SelectItem(string CodArt)
        {
            textBox3.Text = CodArt;
        }

        private void BusProducto(string vProducto, string vRucEmpresa)
        {
            if (ObjProducto.BuscarProducto(vProducto, vRucEmpresa, comboBox3.SelectedValue.ToString()))
            {
                label18.Text = ObjProducto.Nombre.ToString().Trim();

                label12.Text = ObjProducto.CodMarca.ToString().Trim();
                lblPCosto.Text = ObjProducto.PCosto.ToString().Trim();
                lblPMayor.Text = ObjProducto.PMayorista.ToString().Trim();
                lblPmenor.Text = ObjProducto.PVenta.ToString().Trim();
                lblFOB.Text = ObjProducto.PFOB.ToString().Trim();

                //Buscar Stock
                if (ObjProducto.BuscarStock_2(vProducto))
                {
                    label17.Text = ObjProducto.Stock_CC.ToString();
                    label7.Text = ObjProducto.Stock_Almacen.ToString();
                }
                else
                {
                    label17.Text = "";
                    label7.Text = "";
                }
            }
            else
            {
                label17.Text = "";
                label18.Text = "";
                lblPmenor.Text = "";
                lblPMayor.Text = "";
                lblPCosto.Text = "";
                label12.Text = "";
                label7.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBusArticulo frmBusArticulo = new FrmBusArticulo();
            frmBusArticulo.WindowState = FormWindowState.Normal;
            frmBusArticulo.Opener = this;
            //frmAddItemCompra.MdiParent = this.MdiParent;
            //frmAddItemCompra.Show();
            frmBusArticulo.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddPedido_Load(object sender, EventArgs e)
        {
            textBox3.Focus();
            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox3.DisplayMember = "nombre";
            comboBox3.ValueMember = "CodAlmacen";
            comboBox3.DataSource = datosAlm.Tables[0];
            comboBox3.Text = nomAlmacen.ToString();

            //Buscar comprobantes autocorrelativos

            //DataSet datosDoc = csql.dataset("Call SpDocBusAutonumerico('" + vModulo.ToString() + "')");
            DataSet datosDoc = csql.dataset("Call SpDocBusElectronicos('" + codModulo.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "Codigo";
            comboBox1.DataSource = datosDoc.Tables[0];

            String vComp;
            vComp = comboBox1.SelectedValue.ToString();
            DataSet datosSerie = csql.dataset("Call SpSerieBusDoc('" + vComp.ToString() + "')");
            comboBox2.DisplayMember = "serie";
            comboBox2.ValueMember = "Serie";
            comboBox2.DataSource = datosSerie.Tables[0];
            string vSerie = "";
            vSerie = comboBox2.SelectedValue.ToString();
            label16.Text = vSerie;
            if (ObjSerie.BuscarDocSerie(vComp, vSerie))
            {
                if (ObjSerie.Correla.Equals("S"))
                {
                    label5.Text = "AUTOGENERADO";
                }
                else
                {
                    label5.Text = "NO ESPECIFICA";
                }
            }
            else
            {
                label5.Text = "NO ESPECIFICA";
            }
        }

        private void label16_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(label16.Text.ToString().Equals("") ? "0" : label16.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    label16.Text = "";
                else
                    label16.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string codProducto = textBox3.Text.ToString().Trim();
            BusProducto(codProducto, rucEmpresa);
        }

        private void button8_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)

        {
            this.Grid1.Rows.Add(new[] {
                                            textBox3.Text.ToString(),
                                            label18.Text.ToString(),
                                            label17.Text.ToString(),
                                            label7.Text.ToString(),
                                            lblFOB.Text.ToString(),
                                            lblPCosto.Text.ToString(),
                                            lblPMayor.Text.ToString(),
                                            lblPmenor.Text.ToString() });

            label28.Text = "Total documentos: " + Grid1.RowCount;
            textBox3.Text = "";
            textBox3.Focus();
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
                button2.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                int index = Grid1.CurrentRow.Index;
                Grid1.Rows.RemoveAt(index);
                label28.Text = "Total documentos: " + Grid1.RowCount;
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
                return;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
        }
    }
}