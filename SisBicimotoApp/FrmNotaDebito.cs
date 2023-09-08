using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmNotaDebito : Form, ICliente, INotDeb
    {
        public INotDeb Opener { get; set; }
        public static char nmNdv = 'N';
        public static string nIdNd = "";
        private string codModulo = "NDV";
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        public string vTipDoc = "";
        public static string Doc = "";

        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsCliente ObjCliente = new ClsCliente();

        private DataSet datos;

        public FrmNotaDebito()
        {
            InitializeComponent();
        }

        #region ICliente Members

        public void SelectItem(string tipDoc, string codCli)
        {
            vTipDoc = tipDoc.ToString();
            label13.Text = vTipDoc;
            textBox2.Text = codCli;
        }

        #endregion ICliente Members

        #region INotDeb Members

        public void CargarConsulta(string validaAnulaElimina)
        {
            if (validaAnulaElimina.ToString().Equals("V"))
            {
                string vFecha1;
                string vFecha2;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
                vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
                string vCliente = textBox2.Text;
                string vSerieNc = textBox5.Text;
                string vNumNc = textBox4.Text;
                string vTDoc = comboBox4.Text.ToString().Trim();
                string vSerieVe = textBox1.Text;
                string vNumVe = textBox3.Text;
                string vAlmacen = comboBox3.SelectedValue.ToString(); //comboBox3.Text.ToString().Trim();
                datos = csql.dataset("Call SpNotaDebVentaConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vCliente.ToString().Trim() + "','" + vSerieNc.ToString().Trim() + "','" + vNumNc.ToString().Trim() + "','" + vTDoc.ToString().Trim() + "','" + vSerieVe.ToString().Trim() + "','" + vNumVe.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "')");
                Grid1.DataSource = datos.Tables[0];

                Grilla();
            }
        }

        #endregion INotDeb Members

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Fecha";
            Grid1.Columns[1].HeaderText = "Doc.Cliente";
            Grid1.Columns[2].HeaderText = "Cliente";
            Grid1.Columns[3].HeaderText = "N° Nota Deb.";
            Grid1.Columns[4].HeaderText = "Motivo";
            Grid1.Columns[5].HeaderText = "Doc. Venta";
            Grid1.Columns[6].HeaderText = "Moneda";
            Grid1.Columns[7].HeaderText = "Importe";
            Grid1.Columns[8].HeaderText = "Estado";
            Grid1.Columns[9].HeaderText = "IdNoDeb";
            Grid1.Columns[0].Width = 95;
            Grid1.Columns[1].Width = 100;
            Grid1.Columns[2].Width = 202;
            Grid1.Columns[3].Width = 120;
            Grid1.Columns[4].Width = 100;
            Grid1.Columns[5].Width = 100;
            Grid1.Columns[6].Width = 90;
            Grid1.Columns[7].Width = 75;
            Grid1.Columns[8].Width = 70;
            Grid1.Columns[9].Visible = false;
            Grid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[7].DefaultCellStyle.Format = "###,##0.00";
        }

        private void BusCliente(string vRuc, string vRucEmpresa)
        {
            string vParam = "2";
            string vCodCat = "018";

            if (ObjCliente.BuscarCLiente(vRuc, vRucEmpresa))
            {
                ObjDetCatalogo.BuscarDetCatalogoDes(vCodCat.ToString(), ObjCliente.TipDoc.ToString(), vParam.ToString());
                vTipDoc = ObjDetCatalogo.DescCorta.ToString();
                label13.Text = vTipDoc.ToString().Trim();
                label4.Text = ObjCliente.Nombre.ToString().Trim();
            }
            else
            {
                label4.Text = "";
                label13.Text = "";
            }
        }

        private void FrmNotaCredito_Load(object sender, EventArgs e)
        {
            string codModuloVen = "VEN";
            //Carga Documento
            DataSet datosTDoc = csql.dataset_cadena("Call SpDocBusModulo('" + codModuloVen.ToString() + "')");

            if (datosTDoc.Tables[0].Rows.Count > 0)
            {
                comboBox4.Items.Add("");
                foreach (DataRow fila in datosTDoc.Tables[0].Rows)
                {
                    comboBox4.Items.Add(fila[2].ToString());
                }
            }

            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox3.DisplayMember = "nombre";
            comboBox3.ValueMember = "CodAlmacen";
            comboBox3.DataSource = datosAlm.Tables[0];
            comboBox3.Text = nomAlmacen.ToString();

            //Consulta General
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            string vCliente = textBox2.Text;
            string vSerieNc = textBox5.Text;
            string vNumNc = textBox4.Text;
            string vTDoc = comboBox4.Text.ToString().Trim();
            string vSerieVe = textBox1.Text;
            string vNumVe = textBox3.Text;
            string vAlmacen = comboBox3.SelectedValue.ToString(); //comboBox3.Text.ToString().Trim();
            datos = csql.dataset("Call SpNotaDebVentaConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vCliente.ToString().Trim() + "','" + vSerieNc.ToString().Trim() + "','" + vNumNc.ToString().Trim() + "','" + vTDoc.ToString().Trim() + "','" + vSerieVe.ToString().Trim() + "','" + vNumVe.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "')");
            Grid1.DataSource = datos.Tables[0];

            Grilla();
        }

        private void label6_Click(object sender, EventArgs e)
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
                textBox5.Focus();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string codRucCli = textBox2.Text.ToString().Trim();
            BusCliente(codRucCli, rucEmpresa);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
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
                comboBox4.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
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
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.Focus();
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

        private void button4_Click(object sender, EventArgs e)
        {
            nmNdv = 'N';
            FrmAddNotDebito frmAddNotDebito = new FrmAddNotDebito();
            frmAddNotDebito.WindowState = FormWindowState.Normal;
            //frmAddNotDebito.MdiParent = this.MdiParent;
            //frmAddNotDebito.Show();
            frmAddNotDebito.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            string vCliente = textBox2.Text;
            string vSerieNc = textBox5.Text;
            string vNumNc = textBox4.Text;
            string vTDoc = comboBox4.Text.ToString().Trim();
            string vSerieVe = textBox1.Text;
            string vNumVe = textBox3.Text;
            string vAlmacen = comboBox3.SelectedValue.ToString(); //comboBox3.Text.ToString().Trim();
            datos = csql.dataset("Call SpNotaDebVentaConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "','" + vCliente.ToString().Trim() + "','" + vSerieNc.ToString().Trim() + "','" + vNumNc.ToString().Trim() + "','" + vTDoc.ToString().Trim() + "','" + vSerieVe.ToString().Trim() + "','" + vNumVe.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "')");
            Grid1.DataSource = datos.Tables[0];

            Grilla();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nmNdv = 'M';
                nIdNd = Grid1.CurrentRow.Cells[9].Value.ToString();
                FrmAddNotDebito frmAddNotDebito = new FrmAddNotDebito();
                frmAddNotDebito.WindowState = FormWindowState.Normal;
                //frmAddCompra.MdiParent = this.MdiParent;
                //frmAddCompra.Show();
                frmAddNotDebito.ShowDialog(this);
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
                if (Grid1.CurrentRow.Cells[8].Value.ToString().Equals("ANULADO"))
                {
                    MessageBox.Show("Registro ANULADO, no se puede proceder a ANULAR o ELIMINAR", "SISTEMA");
                    return;
                }
                else
                {
                    nIdNd = Grid1.CurrentRow.Cells[9].Value.ToString();
                    Doc = Grid1.CurrentRow.Cells[3].Value.ToString(); //+ " " + Grid1.CurrentRow.Cells[2].Value.ToString();
                    FrmValidaNDebito frmValidaNDebito = new FrmValidaNDebito();
                    frmValidaNDebito.WindowState = FormWindowState.Normal;
                    frmValidaNDebito.Opener = this;
                    frmValidaNDebito.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }
    }
}