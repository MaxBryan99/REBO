using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmCompras : Form, IProveedor, ICompra
    {
        public ICompra Opener { get; set; }
        public static char nmCom = 'N';
        public static string cod = "";
        public static string nIdCompra = "";
        public static string Doc = "";
        public static string almacenSelec = "";

        //public static string nDoc = "";
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codModulo = "COM";
        private DataSet datos;
        private ClsProveedor ObjProveedor = new ClsProveedor();

        public FrmCompras()
        {
            InitializeComponent();
        }

        #region IProveedor Members

        public void SelectItem(string codProv)
        {
            textBox2.Text = codProv;
        }

        #endregion IProveedor Members

        #region ICompra Members

        public void CargarConsulta(string validaAnulaElimina)
        {
            if (validaAnulaElimina.ToString().Equals("V"))
            {
                string vFecha1;
                string vFecha2;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
                vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
                string vProveedor = textBox2.Text;
                string vTCompra = comboBox1.Text.ToString().Trim();
                string vTMoneda = comboBox2.Text.ToString().Trim();
                string vTDoc = comboBox4.Text.ToString().Trim();
                string vAlmacen = comboBox3.Text.ToString().Trim();
                datos = csql.dataset("Call SpCompraConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vProveedor.ToString().Trim() + "','" + vTCompra.ToString().Trim() + "','" + vTMoneda.ToString().Trim() + "','" + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                SumaTotal();
            }
        }

        #endregion ICompra Members

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Fec. Ingreso";
            Grid1.Columns[1].HeaderText = "Doc.";
            Grid1.Columns[2].HeaderText = "Número";
            Grid1.Columns[3].HeaderText = "Proveedor";
            Grid1.Columns[4].HeaderText = "Moneda";
            Grid1.Columns[5].HeaderText = "T. Compra";
            Grid1.Columns[6].HeaderText = "Est. Compra";
            Grid1.Columns[7].HeaderText = "Sub-Total";
            Grid1.Columns[8].HeaderText = "Desc.";
            Grid1.Columns[9].HeaderText = "Igv";
            Grid1.Columns[10].HeaderText = "Total";
            Grid1.Columns[11].HeaderText = "Almacén";
            Grid1.Columns[12].HeaderText = "IdCompra";
            Grid1.Columns[13].HeaderText = "Estado";
            Grid1.Columns[12].Visible = false;
            Grid1.Columns[14].Visible = false;
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 40;
            Grid1.Columns[2].Width = 90;
            Grid1.Columns[3].Width = 200;
            Grid1.Columns[4].Width = 60;
            Grid1.Columns[5].Width = 80;
            Grid1.Columns[6].Width = 90;
            Grid1.Columns[7].Width = 70;
            Grid1.Columns[8].Width = 50;
            Grid1.Columns[9].Width = 50;
            Grid1.Columns[10].Width = 70;
            Grid1.Columns[11].Width = 100;
            Grid1.Columns[13].Width = 70;
            Grid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[7].DefaultCellStyle.Format = "###,##0.000";
            Grid1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[8].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[9].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[10].DefaultCellStyle.Format = "###,##0.000";
        }

        private void SumaTotal()
        {
            //Sumar Total
            double sumaTotal = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells[13].Value.ToString() != "ANULADO")
                {
                    sumaTotal += Double.Parse(row.Cells[10].Value.ToString().Equals("") ? "0" : row.Cells[10].Value.ToString());
                }
            }
            label8.Text = sumaTotal.ToString("###,##0.000").Trim();
        }

        private void BusProveedor(string vRuc, string vRucEmpresa)
        {
            if (ObjProveedor.BuscarProveedor(vRuc, vRucEmpresa))
            {
                label4.Text = ObjProveedor.Nombre.ToString().Trim();
            }
            else
            {
                label4.Text = "";
            }
        }

        private void FrmCompras_Load(object sender, EventArgs e)
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

            //Tipo de Compra
            string codCatTCom = "014";
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

            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            string vProveedor = textBox2.Text;
            string vTCompra = comboBox1.Text.ToString().Trim();
            string vTMoneda = comboBox2.Text.ToString().Trim();
            string vTDoc = comboBox4.Text.ToString().Trim();
            string vAlmacen = comboBox3.Text.ToString().Trim();
            datos = csql.dataset("Call SpCompraConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vProveedor.ToString().Trim() + "','" + vTCompra.ToString().Trim() + "','" + vTMoneda.ToString().Trim() + "','" + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "')");

            Grid1.DataSource = datos.Tables[0];

            Grilla();
            SumaTotal();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBusProveedorCon frmBusProveedor = new FrmBusProveedorCon();
            frmBusProveedor.WindowState = FormWindowState.Normal;
            frmBusProveedor.Opener = this;
            frmBusProveedor.MdiParent = this.MdiParent;
            frmBusProveedor.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string rucProv = textBox2.Text.ToString().Trim();
            BusProveedor(rucProv, rucEmpresa);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string vFecha1;
                string vFecha2;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
                vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
                string vProveedor = textBox2.Text;
                string vTCompra = comboBox1.Text.ToString().Trim();
                string vTMoneda = comboBox2.Text.ToString().Trim();
                string vTDoc = comboBox4.Text.ToString().Trim();
                string vAlmacen = comboBox3.Text.ToString().Trim();
                datos = csql.dataset("Call SpCompraConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vProveedor.ToString().Trim() + "','" + vTCompra.ToString().Trim() + "','" + vTMoneda.ToString().Trim() + "','" + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "')");
                Grid1.DataSource = datos.Tables[0];
                label28.Text = "Total documentos: " + Grid1.RowCount;
                Grilla();
                SumaTotal();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error de Sistema " + ex.Message, "SISTEMA");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            nmCom = 'N';
            FrmAddCompra frmAddCompra = new FrmAddCompra();
            frmAddCompra.WindowState = FormWindowState.Normal;
            frmAddCompra.ShowDialog(this);
        }

        private void FrmCompras_Activated(object sender, EventArgs e)
        {
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

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmCom = 'M';
            if (Grid1.RowCount > 0)
            {
                if (Grid1.CurrentRow.Cells[13].Value.ToString().Equals("ANULADO"))
                {
                    MessageBox.Show("Registro ANULADO, no se puede proceder a modificar", "SISTEMA");
                    return;
                }
                else
                {
                    almacenSelec = Grid1.CurrentRow.Cells[14].Value.ToString();

                    nIdCompra = Grid1.CurrentRow.Cells[12].Value.ToString();
                    FrmAddCompra frmAddCompra = new FrmAddCompra();
                    frmAddCompra.WindowState = FormWindowState.Normal;
                    frmAddCompra.Opener = this;
                    frmAddCompra.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                if (Grid1.CurrentRow.Cells[13].Value.ToString().Equals("ANULADO"))
                {
                    MessageBox.Show("Registro ANULADO, no se puede proceder a ANULAR o ELIMINAR", "SISTEMA");
                    return;
                }
                else
                {
                    nIdCompra = Grid1.CurrentRow.Cells[12].Value.ToString();
                    Doc = Grid1.CurrentRow.Cells[1].Value.ToString() + " " + Grid1.CurrentRow.Cells[2].Value.ToString();
                    FrmValidaCompra frmValidaCompra = new FrmValidaCompra();
                    frmValidaCompra.WindowState = FormWindowState.Normal;
                    frmValidaCompra.Opener = this;
                    frmValidaCompra.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void DTP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                DTP2.Focus();
            }
        }

        private void DTP2_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}