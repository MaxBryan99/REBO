using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmIngresosAlm : Form, IResponsable, IIngreso
    {
        public IIngreso Opener { get; set; }
        public static char nmIng = 'N';
        public static string nId = "";
        public static string cAlmacen = "";
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string codModulo = "ING";
        private ClsResponsable ObjResponsable = new ClsResponsable();
        private DataSet datos;

        public FrmIngresosAlm()
        {
            InitializeComponent();
        }

        #region IResponsable Members

        public void SelectItemVend(string codResp)
        {
            textBox2.Text = codResp;
        }

        #endregion IResponsable Members

        #region IIngreso Members

        public void CargarConsulta(string vValor)
        {
            if (vValor.ToString().Equals("V"))
            {
                string vFecha1;
                string vFecha2;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
                vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
                string vResp = textBox2.Text;
                string vTDoc = comboBox4.Text.ToString().Trim();
                string vAlmacen = comboBox3.Text.ToString().Trim();
                datos = csql.dataset("Call SpIngresoConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + vResp.ToString() + "','" + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + rucEmpresa.ToString().Trim() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
            }
        }

        #endregion IIngreso Members

        private void BusResponsable(string vCodResp, string vRucEmpresa)
        {
            if (ObjResponsable.BuscarResponsable(vCodResp.ToString().Trim(), codAlmacen.ToString(), vRucEmpresa))
            {
                label4.Text = ObjResponsable.Nombre.ToString().Trim();
            }
            else
            {
                label4.Text = "";
            }
            if (vCodResp.ToString().Equals(""))
            {
                label4.Text = "";
            }
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Fecha";
            Grid1.Columns[1].HeaderText = "Doc.";
            Grid1.Columns[2].HeaderText = "Número";
            Grid1.Columns[3].HeaderText = "Concepto";
            Grid1.Columns[4].HeaderText = "Responsable";
            Grid1.Columns[5].HeaderText = "Referencia";
            Grid1.Columns[6].Visible = false;
            Grid1.Columns[7].Visible = false;
            Grid1.Columns[0].Width = 75;
            Grid1.Columns[1].Width = 110;
            Grid1.Columns[2].Width = 100;
            Grid1.Columns[3].Width = 170;
            Grid1.Columns[4].Width = 190;
            Grid1.Columns[5].Width = 170;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            nmIng = 'N';
            FrmAddIngreso frmAddIngreso = new FrmAddIngreso();
            frmAddIngreso.WindowState = FormWindowState.Normal;
            frmAddIngreso.ShowDialog(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmIngresosAlm_Load(object sender, EventArgs e)
        {
            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox3.DisplayMember = "nombre";
            comboBox3.ValueMember = "CodAlmacen";
            comboBox3.DataSource = datosAlm.Tables[0];
            comboBox3.Text = nomAlmacen.ToString();

            //Carga Documento
            DataSet datosTDoc = csql.dataset_cadena("Call SpDocBusModulo('" + codModulo.ToString() + "')");

            if (datosTDoc.Tables[0].Rows.Count > 0)
            {
                comboBox4.Items.Add("");
                foreach (DataRow fila in datosTDoc.Tables[0].Rows)
                {
                    comboBox4.Items.Add(fila[1].ToString());
                }
            }

            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            string vResp = textBox2.Text;
            string vTDoc = comboBox4.Text.ToString().Trim();
            string vAlmacen = comboBox3.Text.ToString().Trim();
            datos = csql.dataset("Call SpIngresoConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + vResp.ToString() + "','" + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + rucEmpresa.ToString().Trim() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            string vResp = textBox2.Text;
            string vTDoc = comboBox4.Text.ToString().Trim();
            string vAlmacen = comboBox3.Text.ToString().Trim();
            datos = csql.dataset("Call SpIngresoConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + vResp.ToString() + "','" + vTDoc.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + rucEmpresa.ToString().Trim() + "')");
            Grid1.DataSource = datos.Tables[0];
            label28.Text = "Total Documentos: " + Grid1.RowCount;
            Grilla();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string codResp = textBox2.Text.ToString().Trim();
            BusResponsable(codResp, rucEmpresa);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBusResponsable frmBusResponsable = new FrmBusResponsable();
            frmBusResponsable.WindowState = FormWindowState.Normal;
            frmBusResponsable.Opener = this;
            frmBusResponsable.MdiParent = this.MdiParent;
            frmBusResponsable.Show();
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
                comboBox4.Focus();
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
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
                button1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmIng = 'M';
            if (Grid1.RowCount > 0)
            {
                nId = Grid1.CurrentRow.Cells[6].Value.ToString();
                cAlmacen = Grid1.CurrentRow.Cells[7].Value.ToString();
                FrmAddIngreso frmAddIngreso = new FrmAddIngreso();
                frmAddIngreso.WindowState = FormWindowState.Normal;
                frmAddIngreso.Opener = this;
                frmAddIngreso.ShowDialog(this);
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
                nId = Grid1.CurrentRow.Cells[6].Value.ToString();
                FrmValidaIngreso frmValidaIngreso = new FrmValidaIngreso();
                frmValidaIngreso.WindowState = FormWindowState.Normal;
                frmValidaIngreso.Opener = this;
                frmValidaIngreso.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }
    }
}