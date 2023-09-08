using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmMovCaja : Form
    {
        DataSet datos;
        public static char nmMov = 'N';
        public static string nIdMov = "";
        ClsMovCaja ObjMovCaja = new ClsMovCaja();

        public FrmMovCaja()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Id";
            Grid1.Columns[1].HeaderText = "Fecha";
            Grid1.Columns[2].HeaderText = "Movimiento";
            Grid1.Columns[3].HeaderText = "Descripción";
            Grid1.Columns[4].HeaderText = "Estado";
            Grid1.Columns[5].HeaderText = "Monto";
            Grid1.Columns[0].Visible = false;
            Grid1.Columns[1].Width = 80;
            Grid1.Columns[2].Width = 190;
            Grid1.Columns[3].Width = 220;
            Grid1.Columns[4].Width = 215;
            Grid1.Columns[5].Width = 80;
            Grid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[5].DefaultCellStyle.Format = "###,##0.00";
        }

        private void BuscarMovCaja()
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            datos = csql.dataset("Call SpMovCajaConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + comboBox1.Text + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
            SumaTotal();
        }

        private void SumaTotal()
        {
            //Sumar Total
            double sumaTotalIng = 0;
            double sumaTotalEgr = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells[2].Value.ToString().Equals("INGRESO"))
                {
                    sumaTotalIng += Double.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString());
                }
                if (row.Cells[2].Value.ToString().Equals("EGRESO"))
                {
                    sumaTotalEgr += Double.Parse(row.Cells[5].Value.ToString().Equals("") ? "0" : row.Cells[5].Value.ToString());
                }
            }
            label8.Text = sumaTotalIng.ToString("###,##0.00").Trim();
            label5.Text = sumaTotalEgr.ToString("###,##0.00").Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmMov = 'N';
            FrmGastosCaja frmGastosCaja = new FrmGastosCaja();
            frmGastosCaja.WindowState = FormWindowState.Normal;
            //frmAddIngreso.MdiParent = this.MdiParent;
            //frmAddIngreso.Show();
            frmGastosCaja.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            nmMov = 'M';
            if (Grid1.RowCount > 0)
            {
                if (Grid1.CurrentRow.Cells[4].Value.ToString().Equals("PROCESADO"))
                {
                    MessageBox.Show("Registro PROCESADO, no se puede proceder a modificar", "Sistema");
                    return;
                }
                else
                {
                    nIdMov = Grid1.CurrentRow.Cells[0].Value.ToString();
                    FrmGastosCaja frmGastosCaja = new FrmGastosCaja();
                    frmGastosCaja.WindowState = FormWindowState.Normal;
                    //frmAddIngreso.MdiParent = this.MdiParent;
                    //frmAddIngreso.Show();
                    frmGastosCaja.ShowDialog(this);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                if (Grid1.CurrentRow.Cells[4].Value.ToString().Equals("PROCESADO"))
                {
                    MessageBox.Show("Registro PROCESADO, no se puede proceder a modificar", "Sistema");
                    return;
                }
                else
                {
                    nIdMov = Grid1.CurrentRow.Cells[0].Value.ToString();

                    if (MessageBox.Show("Esta seguro que desea eliminar el registro seleccionado", "Sistema", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }

                    ObjMovCaja.Eliminar(nIdMov);

                    MessageBox.Show("Registro eliminado correctamente", "Sistema");
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void FrmMovCaja_Load(object sender, EventArgs e)
        {
            //Carga Ingresos
            comboBox1.Items.Add("");
            comboBox1.Items.Add("INGRESO");
            comboBox1.Items.Add("EGRESO");

            BuscarMovCaja();

            //Generar ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 300;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.button2, "Nuevo Movimiento");
            toolTip1.SetToolTip(this.button3, "Modificar Movimiento");
            toolTip1.SetToolTip(this.button4, "Eliminar Movimiento");
            toolTip1.SetToolTip(this.button1, "Buscar");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuscarMovCaja();
        }
    }
}
