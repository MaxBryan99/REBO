using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmKardex : Form
    {
        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string razonEmpresa = FrmLogin.x_NomEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private ClsProducto ObjProducto = new ClsProducto();

        public FrmKardex()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Familia";
            Grid1.Columns[1].HeaderText = "Línea";
            Grid1.Columns[2].HeaderText = "Artículo";
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
            Grid1.Columns[2].Width = 100;
            Grid1.Columns[3].Width = 300;
            Grid1.Columns[4].Width = 60;
            Grid1.Columns[5].Width = 80;
            Grid1.Columns[6].Width = 60;
            Grid1.Columns[7].Width = 70;
            Grid1.Columns[8].Width = 50;
            Grid1.Columns[9].Width = 50;
            Grid1.Columns[10].Width = 70;
            Grid1.Columns[11].Width = 170;
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

        private void FrmKardex_Load(object sender, EventArgs e)
        {
            //Carga Almacen
            DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "CodAlmacen";
            comboBox1.DataSource = datosAlm.Tables[0];
            comboBox1.Text = nomAlmacen.ToString();

            DataSet datos = csql.dataset_cadena("Call SpProductoBusConsulta_Kardex('" + rucEmpresa.ToString() + "','" + comboBox1.SelectedValue + "')");

            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Grid1.Rows.Add(new[] { fila[6].ToString(), fila[7].ToString(), fila[0].ToString(), fila[1].ToString(), Boolean.FalseString });
                }
            }
            label28.Text = "Total Productos: " + Grid1.RowCount;
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            if (Grid1.Columns[e.ColumnIndex].Name == "SELECCIONAR")
            {
                DataGridViewRow row = Grid1.Rows[e.RowIndex];

                DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;
                
                if (cellSelecion.Value == null)
                    cellSelecion.Value = Boolean.FalseString;
                switch (cellSelecion.Value.ToString())
                {
                    case "True":
                        cellSelecion.Value = Boolean.FalseString;
                        break;

                    case "False":
                        cellSelecion.Value = Boolean.TrueString;
                        break;
                }
            }
        }

        private void Grid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //
            // Solo se trabaja ante los cambios en la columan de los checkbox
            //
            if (Grid1.Columns[e.ColumnIndex].Name == "SELECCIONAR")
            {
                //
                // Se toma la fila seleccionada
                //
                DataGridViewRow row = Grid1.Rows[e.RowIndex];

                //
                // Se selecciona la celda del checkbox
                //
                DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;

                //
                // Se valida si esta checkeada
                //
            }
        }

        private void Grid1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (Grid1.IsCurrentCellDirty)
            {
                Grid1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    //
                    // Se recupera el campo que representa el checkbox, y se valida la seleccion
                    // agregandola a la lista temporal
                    //
                    DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;
                    cellSelecion.Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    //
                    // Se recupera el campo que representa el checkbox, y se valida la seleccion
                    // agregandola a la lista temporal
                    //
                    DataGridViewCheckBoxCell cellSelecion = row.Cells["SELECCIONAR"] as DataGridViewCheckBoxCell;
                    cellSelecion.Value = false;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.Rows.Clear();
            DataSet datos = csql.dataset_cadena("Call SpProductoBusConsulta_Kardex('" + rucEmpresa.ToString() + "','" + comboBox1.SelectedValue + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    this.Grid1.Rows.Add(new[] { fila[6].ToString(), fila[7].ToString(), fila[0].ToString(), fila[1].ToString(), Boolean.FalseString });
                }
            }
            label28.Text = "Total Productos: " + Grid1.RowCount;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean nValSel = false;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells[4].Value.ToString().Equals(Boolean.TrueString))
                {
                    nValSel = true;
                    break;
                }
            }

            if (nValSel == false)
            {
                MessageBox.Show("Seleccione al menos un Artículo", "SISTEMA");
                return;
            }

            Double valSalAnt = 0;
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            string vAlmacen = comboBox1.SelectedValue.ToString();

            int nIndex = 0;
            int contador = 0;
            string Usuario = FrmLogin.x_login_usuario;
            //Eliminamos temporal
            DataSet datosDel = csql.dataset_cadena("Delete from tbltempkardex");
            foreach (DataGridViewRow row1 in Grid1.Rows)
            {
                //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row1.Cells[4];
                nIndex = contador;
                if (row1.Cells[4].Value.ToString().Equals(Boolean.TrueString))
                {
                    DataSet datosK = csql.dataset_cadena("Call SpKardexSaldAnterior('" + Grid1[2, nIndex].Value.ToString() + "','" + vFecha1.ToString() + "','" + comboBox1.SelectedValue + "','" + rucEmpresa.ToString() + "')");

                    if (datosK.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in datosK.Tables[0].Rows)
                        {
                            valSalAnt = Double.Parse(fila[0].ToString());
                            //Genera Kardex
                            DataSet datosKardex = csql.dataset_cadena("Call SpKardex('" + Grid1[2, nIndex].Value.ToString() + "'," + valSalAnt + ",'" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + comboBox1.SelectedValue + "','" + rucEmpresa.ToString() + "','" + Usuario.ToString() + "')");
                        }
                    }
                    else
                    {
                    }
                }

                contador += 1;
            }

            MessageBox.Show("Datos Procesados", "SISTEMA");

            CrystalDecisions.CrystalReports.Engine.ReportDocument reporte = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            reporte.Load(@"Reportes\RptKardex.rpt");

            DataSet datos = csql.dataset_cadena("Select IdOrd, Fecha, Tip_Mov, Descripcion, Tip_doc, Numero, Ingreso, Salida, Codigo, Producto, Prec_costo, Pre_venta, TCambio, Stock, SalAnt, FecInicio, FecFinal, UserCreacion, FecCreacion  from TblTempKardex");
            // DataSet datos = csql.dataset_cadena("Select IdOrd, Fecha, Tip_Mov, Descripcion, Tip_doc, Numero, Ingreso, Salida, Codigo, Producto, Prec_costo, Pre_venta, TCambio, Stock, FecInicio, FecFinal, UserCreacion, FecCreacion  from TblTempKardex");
            reporte.SetDataSource(datos.Tables[0]);

            reporte.SetParameterValue("RucEmpresa", rucEmpresa.ToString().Trim());
            reporte.SetParameterValue("RazonEmpresa", razonEmpresa.ToString().Trim());
            reporte.SetParameterValue("FecInicio", vFecha1.ToString().Trim());
            reporte.SetParameterValue("FecFinal", vFecha2.ToString().Trim());
            reporte.SetParameterValue("Almacen", nomAlmacen.ToString().Trim());

            FrmRptKardexcs frmReporte = new FrmRptKardexcs();
            frmReporte.PreparaReporte(reporte);
            frmReporte.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Boolean nValSel = false;
            int nIndex = 0;
            int contador = 0;
            string vAlmacen = comboBox1.SelectedValue.ToString();

            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells[4].Value.ToString().Equals(Boolean.TrueString))
                {
                    nValSel = true;
                    break;
                }
            }

            if (nValSel == false)
            {
                MessageBox.Show("Seleccione al menos un Artículo", "SISTEMA");
                return;
            }

            if (MessageBox.Show("¿Está seguro de realizar el proceso de actualizacion del Stock almacén " + comboBox1.Text + "?. El proceso puede tardar varios minutos", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            foreach (DataGridViewRow row in Grid1.Rows)
            {
                //DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[4];
                nIndex = contador;
                if (row.Cells[4].Value.ToString().Equals(Boolean.TrueString))
                {
                    DataSet datosK = csql.dataset_cadena("CALL SpKardexRecalculaStockTotalArt('" + Grid1[2, nIndex].Value.ToString() + "','" + vAlmacen.ToString() + "','" + rucEmpresa.ToString() + "')");
                }

                contador += 1;
            }

            MessageBox.Show("Datos Procesados correctamente", "SISTEMA");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedIndex = cbBusqueda.SelectedIndex;
            if (cbBusqueda.SelectedItem == null)
            {
                DataSet datos = csql.dataset_cadena("Call SpProductoBusConsulta_Kardex('" + rucEmpresa.ToString() + "','" + comboBox1.SelectedValue + "')");
                Grid1.Rows.Clear();
                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        this.Grid1.Rows.Add(new[] { fila[6].ToString(), fila[7].ToString(), fila[0].ToString(), fila[1].ToString(), Boolean.FalseString });
                    }
                }
            }
            else
            {
                if (selectedIndex.Equals(0))
                {
                    if (textBox1.TextLength > 0)
                    {
                        string codigo = textBox1.Text.Trim();
                        datos = csql.dataset_cadena("Call SpProductoBusCod_Kardex('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "','" + comboBox1.SelectedValue + "')");
                        Grid1.Rows.Clear();
                        if (datos.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow fila in datos.Tables[0].Rows)
                            {
                                this.Grid1.Rows.Add(new[] { fila[6].ToString(), fila[7].ToString(), fila[0].ToString(), fila[1].ToString(), Boolean.FalseString });
                            }
                        }
                    }
                    else
                    {
                        DataSet datos = csql.dataset_cadena("Call SpProductoBusConsulta_Kardex('" + rucEmpresa.ToString() + "','" + comboBox1.SelectedValue + "')");
                        Grid1.Rows.Clear();
                        if (datos.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow fila in datos.Tables[0].Rows)
                            {
                                this.Grid1.Rows.Add(new[] { fila[6].ToString(), fila[7].ToString(), fila[0].ToString(), fila[1].ToString(), Boolean.FalseString });
                            }
                        }
                    }
                }
                if (selectedIndex.Equals(1))
                {
                    string nnombre = textBox1.Text.Trim();
                    datos = csql.dataset_cadena("Call SpProductoBusConsultaNom_Kardex('" + rucEmpresa.ToString() + "','" + comboBox1.SelectedValue + "','" + nnombre.ToString() + "')");
                    Grid1.Rows.Clear();
                    if (datos.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow fila in datos.Tables[0].Rows)
                        {
                            this.Grid1.Rows.Add(new[] { fila[6].ToString(), fila[7].ToString(), fila[0].ToString(), fila[1].ToString(), Boolean.FalseString, fila[0].ToString() });
                        }
                    }
                }
            }
            label28.Text = "Total Productos: " + Grid1.RowCount;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button5.Focus();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Boolean nValSel = false;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[4];

                if (row.Cells[4].Value.ToString().Equals(Boolean.TrueString))
                {
                    nValSel = true;
                    break;
                }
            }

            if (nValSel == false)
            {
                MessageBox.Show("Seleccione al menos un Artículo", "SISTEMA");
                return;
            }

            if (MessageBox.Show("¿Está seguro de realizar el proceso de actualizacion del Stock a 0 el almacén " + comboBox1.Text + "?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            int conta = 0;
            int nIndex1 = 0;
            string Usuario = FrmLogin.x_login_usuario;

            foreach (DataGridViewRow row1 in Grid1.Rows)
            {
                nIndex1 = conta;

                if (row1.Cells[4].Value.ToString().Equals(Boolean.TrueString))
                {
                    if (ObjProducto.ActualizaStockaCero(Grid1[2, nIndex1].Value.ToString(), rucEmpresa.ToString(), comboBox1.SelectedValue.ToString(), Usuario.ToString()))
                    {
                    }
                }

                conta += 1;
            }

            MessageBox.Show("Datos Procesados correctamente", "SISTEMA");
        }
    }
}