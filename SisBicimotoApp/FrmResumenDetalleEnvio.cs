using SisBicimotoApp.Clases;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmResumenDetalleEnvio : Form
    {
        private ClsResumenEnvio ObjResumenEnvio = new ClsResumenEnvio();
        private ClsDetResumenEnvioAgrupado ObjDetResumenEnvio = new ClsDetResumenEnvioAgrupado();
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        private List<ClsResumen> _listResumen = null;
        private List<ClsResumenComprobante> _listComprobante = null;

        public FrmResumenDetalleEnvio()
        {
            InitializeComponent();
        }

        public FrmResumenDetalleEnvio(List<ClsResumen> items, List<ClsResumenComprobante> itemsComprobante)
            : this()
        {
            this._listResumen = items;
            this._listComprobante = itemsComprobante;
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "N°";
            Grid1.Columns[1].HeaderText = "Tipo Comprobante";
            Grid1.Columns[2].HeaderText = "Serie";
            Grid1.Columns[3].HeaderText = "Inicio de Rango";
            Grid1.Columns[4].HeaderText = "Fin de Rango";
            Grid1.Columns[5].HeaderText = "Total Op. Grav.";
            Grid1.Columns[6].HeaderText = "Total Op. Exon.";
            Grid1.Columns[7].HeaderText = "Total Op. Grat.";
            Grid1.Columns[8].HeaderText = "Total IGV";
            Grid1.Columns[9].HeaderText = "Importe Total";
            Grid1.Columns[10].HeaderText = "Total";
            Grid1.Columns[11].HeaderText = "TDOC";
            Grid1.Columns[12].HeaderText = "Conteo";
            Grid1.Columns[13].HeaderText = "Dife";
            Grid1.Columns[9].Visible = false;
            Grid1.Columns[11].Visible = false;
            Grid1.Columns[12].Visible = false;
            Grid1.Columns[13].Visible = false;
            Grid1.Columns[0].Width = 40;
            Grid1.Columns[1].Width = 200;
            Grid1.Columns[2].Width = 50;
            Grid1.Columns[3].Width = 75;
            Grid1.Columns[4].Width = 75;
            Grid1.Columns[5].Width = 83;
            Grid1.Columns[6].Width = 83;
            Grid1.Columns[8].Width = 83;
            Grid1.Columns[9].Width = 83;
            Grid1.Columns[10].Width = 83;
            Grid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[5].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[6].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[7].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[8].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[9].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[10].DefaultCellStyle.Format = "###,##0.00";
            //this.Grid1.DefaultCellStyle.Font = new Font("Arial", 8.25F, GraphicsUnit.Pixel);
        }

        private void verificaCorr()
        {
            int n = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                int vConteo = int.Parse(row.Cells[12].Value.ToString());
                int vDife = int.Parse(row.Cells[13].Value.ToString());

                if (vDife > vConteo)
                {
                    Grid1.Rows[n].DefaultCellStyle.ForeColor = Color.Red;
                }
                n += 1;
            }
        }

        private Boolean validaCorr()
        {
            Boolean nvalor = false;
            int n = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                int vConteo = int.Parse(row.Cells[12].Value.ToString());
                int vDife = int.Parse(row.Cells[13].Value.ToString());

                if (vDife > vConteo)
                {
                    nvalor = true;
                }
                n += 1;
            }

            return nvalor;
        }

        private void FrmResumenDetalleEnvio_Load(object sender, EventArgs e)
        {
            //
            // Cargo los items provenientes del constructor con DataRows
            //
            /*if (_listDataRow != null)
            {
                foreach (DataRow row in _listDataRow)
                {
                    Grid1.Rows.Add(row.ItemArray);
                }
            }*/
            label5.Text = rucEmpresa.ToString();
            label6.Text = FrmLogin.x_NomEmpresa;
            DTP1.Text = FrmGeneraResumen.valorFecha.ToString();
            if (_listResumen != null)
            {
                //Grid1.AutoGenerateColumns = false;
                Grid1.DataSource = _listResumen;
            }

            Grilla();
            verificaCorr();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validaCorr())
            {
                MessageBox.Show("Algunos comprobantes no tienen la numeración correlativa que corresponde, Verifique los comprobantes marcados de color rojo", "SISTEMA");
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar el envío del Resumen de Comprobantes", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (Grid1.RowCount > 0)
            {
                string vFecha1;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();

                string hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + DateTime.Now.Second.ToString("D2");

                string vCodEnvio = "RC" + "-" + DTP1.Value.Year.ToString() + DTP1.Value.Month.ToString("00") + DTP1.Value.Day.ToString("00");

                int nValor = ObjResumenEnvio.ValorConteo(vCodEnvio, rucEmpresa);

                string Usuario = FrmLogin.x_login_usuario;

                ObjResumenEnvio.Id = nValor + 1;
                ObjResumenEnvio.NDocResumen = vCodEnvio.ToString();
                ObjResumenEnvio.Fecha = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
                ObjResumenEnvio.Est = "A";
                ObjResumenEnvio.RucEmpresa = rucEmpresa;
                ObjResumenEnvio.UserCreacion = Usuario.ToString();

                //Detalles
                int n = 1;
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    ObjDetResumenEnvio.Id = nValor + 1;
                    ObjDetResumenEnvio.NDocResumen = vCodEnvio.ToString();
                    ObjDetResumenEnvio.NumId = n;
                    ObjDetResumenEnvio.TDoc = row.Cells[11].Value.ToString();
                    ObjDetResumenEnvio.Serie = row.Cells[2].Value.ToString();
                    ObjDetResumenEnvio.Inicio = int.Parse(row.Cells[3].Value.ToString());
                    ObjDetResumenEnvio.Fin = int.Parse(row.Cells[4].Value.ToString());
                    ObjDetResumenEnvio.Gravadas = Double.Parse(row.Cells[5].Value.ToString());
                    ObjDetResumenEnvio.Exoneradas = Double.Parse(row.Cells[6].Value.ToString());
                    ObjDetResumenEnvio.Gratuitas = Double.Parse(row.Cells[7].Value.ToString());
                    ObjDetResumenEnvio.Igv = Double.Parse(row.Cells[8].Value.ToString());
                    ObjDetResumenEnvio.SumaTotal = Double.Parse(row.Cells[9].Value.ToString());
                    ObjDetResumenEnvio.RucEmpresa = rucEmpresa;
                    ObjDetResumenEnvio.UserCreacion = Usuario.ToString();

                    n += 1;
                    if (ObjDetResumenEnvio.Crear())
                    {
                    }
                    else
                    {
                        MessageBox.Show("No se puede crear registro de detalle de Comunicación, VERIFIQUE!!!", "SISTEMA");
                        return;
                    }
                }

                if (ObjResumenEnvio.Crear())
                {
                    //Grabar XML
                    ObjGrabaXML.generaXMLResumen1(ObjResumenEnvio.Id.ToString(), vCodEnvio.ToString(), rucEmpresa, true);

                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                }
                else
                {
                    MessageBox.Show("No se puede crear registro de Comunicación, VERIFIQUE!!!", "SISTEMA");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar resumen comprobantes", "SISTEMA");
                return;
            }

            this.Close();
        }
    }

    /*public class Resumen
    {
        public int Item { get; set; }
        public string TipoComprobante { get; set; }
        public string Serie { get; set; }
        public int Inicio { get; set; }
        public int Fin { get; set; }
        public double Gravadas { get; set; }
        public double Exoneradas { get; set; }
        public double Igv { get; set; }
        public double Total { get; set; }
        public string TDoc { get; set; }
        public int Conteo { get; set; }
        public int Dife { get; set; }
    }*/
}