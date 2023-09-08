using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmResumenBolCPE : Form
    {
        public static string nNumRes = "";
        public static string nIdRes = "";
        public static string nomXml = "";
        public static string vAlm = "";

        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsResumenEnvio ObjResumenEnvio = new ClsResumenEnvio();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        private DataSet datos;

        public FrmResumenBolCPE()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Fecha Envío";
            Grid1.Columns[1].HeaderText = "Fec. Resumen";
            Grid1.Columns[2].HeaderText = "Número Doc.";
            //Grid1.Columns[2].HeaderText = "Cant. Docs.";
            Grid1.Columns[3].HeaderText = "N° Ticket";
            Grid1.Columns[4].HeaderText = "Respuesta de SUNAT";
            Grid1.Columns[5].HeaderText = "Doc";
            Grid1.Columns[6].HeaderText = "Id";
            Grid1.Columns[7].HeaderText = "Xml";
            Grid1.Columns[5].Visible = false;
            Grid1.Columns[6].Visible = false;
            Grid1.Columns[0].Width = 120;
            Grid1.Columns[1].Width = 100;
            Grid1.Columns[2].Width = 90;
            Grid1.Columns[3].Width = 100;
            Grid1.Columns[4].Width = 490;
            Grid1.Columns[7].Width = 50;
            Grid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        public void CargarConsulta()
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();

            datos = csql.dataset("Call SpResumenConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void FrmResumenBolCPE_Load(object sender, EventArgs e)
        {
            this.Text = "Resumenes de Boletas de Venta y Notas Vinculadas [" + FrmLogin.x_RucEmpresa.ToString() + "]";

            CargarConsulta();

            //Generar ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            //toolTip1.SetToolTip(this.button1, "Buscar Proveedor");
            toolTip1.SetToolTip(this.button7, "Nuevo Registro de Comunicación de Baja");
            toolTip1.SetToolTip(this.button2, "Descargar XML");
            toolTip1.SetToolTip(this.button3, "Volver a Enviar Registro");
            toolTip1.SetToolTip(this.button4, "Buscar Registros");
            toolTip1.SetToolTip(this.button5, "Anular Envío");
            toolTip1.SetToolTip(this.button8, "Salir");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();

            datos = csql.dataset("Call SpResumenConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmGeneraResumen frmGeneraResumen = new FrmGeneraResumen();
            frmGeneraResumen.WindowState = FormWindowState.Normal;
            //frmAddCompra.MdiParent = this.MdiParent;
            //frmAddCompra.Show();
            frmGeneraResumen.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nIdRes = Grid1.CurrentRow.Cells[6].Value.ToString();
                nNumRes = Grid1.CurrentRow.Cells[5].Value.ToString();

                if (Grid1.CurrentRow.Cells[7].Value.ToString().Equals("Si"))
                {
                    MessageBox.Show("Archivo XML generado, no se puede proceder a generar XML", "SISTEMA");
                    return;
                }
                ObjGrabaXML.generaXMLResumen1(nIdRes, nNumRes, rucEmpresa, false);
                MessageBox.Show("Archivo XML generado satisfactoriamente", "SISTEMA");
                //string val = "V";
                CargarConsulta();
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nIdRes = Grid1.CurrentRow.Cells[6].Value.ToString();
                nNumRes = Grid1.CurrentRow.Cells[5].Value.ToString();

                if (Grid1.CurrentRow.Cells[7].Value.ToString().Equals("No"))
                {
                    MessageBox.Show("Archivo XML no generado, no se puede proceder a descargar el XML", "SISTEMA");
                    return;
                }

                if (!ObjResumenEnvio.Buscar(nIdRes, nNumRes, rucEmpresa))
                {
                    return;
                }
                nomXml = Grid1.CurrentRow.Cells[2].Value.ToString();
                ObjGrabaXML.descargaXML(nomXml, ObjResumenEnvio.ArchXml, rucEmpresa, vAlm);
                CargarConsulta();
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nIdRes = Grid1.CurrentRow.Cells[6].Value.ToString();
                nNumRes = Grid1.CurrentRow.Cells[5].Value.ToString();
                nomXml = Grid1.CurrentRow.Cells[2].Value.ToString();
                if (Grid1.CurrentRow.Cells[7].Value.ToString().Equals("No"))
                {
                    MessageBox.Show("Archivo XML no generado, no se puede proceder a descargar el XML", "SISTEMA");
                    return;
                }

                if (!ObjResumenEnvio.Buscar(nIdRes, nNumRes, rucEmpresa))
                {
                    return;
                }

                FrmEnviaXmlRes frmEnviaXmlRes = new FrmEnviaXmlRes();
                frmEnviaXmlRes.WindowState = FormWindowState.Normal;
                //frmAddCompra.MdiParent = this.MdiParent;
                //frmAddCompra.Show();
                frmEnviaXmlRes.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }
    }
}