using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmEnviarSunat : Form
    {
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string vAlmacen = "";

        public static string nIdVenta = "";
        public static string nomXml = "";
        public static string vAlm = "";
        public static string vDoc = "";

        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();
        private ClsEnvio ObjEnvio = new ClsEnvio();
        private ClsDocumento ObjDoc = new ClsDocumento();

        private DataSet datos;

        public FrmEnviarSunat()
        {
            InitializeComponent();
        }

        private void ColorEstado()
        {
            int n = 0;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                string vEstado = (row.Cells[5].Value.ToString());
                //int vDife = int.Parse(row.Cells[13].Value.ToString());

                if (vEstado.Equals("ACEPTADO"))
                {
                }
                else
                {
                    Grid1.Rows[n].DefaultCellStyle.ForeColor = Color.Red;
                }
                n += 1;
            }
        }

        private void CargarConsulta()
        {
            string vFecha1;
            string vFecha2;
            vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
            vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
            string vTDoc = "";
            if (ObjDoc.BuscarDocNomMod(comboBox4.Text.ToString().Trim(), "VEN"))
            {
                vTDoc = ObjDoc.Codigo.ToString();
            }

            vAlmacen = comboBox3.SelectedValue.ToString(); //comboBox3.Text.ToString().Trim();
            string vSerie = textBox3.Text;
            string vNumero = textBox4.Text;
            datos = csql.dataset_cadena("Call SpEnvioConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','" + vTDoc.ToString().Trim() + "','" + vSerie.ToString().Trim() + "','" + vNumero.ToString().Trim() + "','" + vAlmacen.ToString().Trim() + "','" + rucEmpresa.ToString() + "')");
            Grid1.Rows.Clear();
            if ((datos.Tables.Count !=0)&&(datos.Tables[0].Rows.Count>0)) // if((ds.Tables.Count != 0)&&(ds.Tables[0].Rows.Count > 0))
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    
                        this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), fila[5].ToString().Trim(), fila[6].ToString().Trim(), Boolean.FalseString, fila[7].ToString().Trim(), fila[8].ToString().Trim(), fila[9].ToString().Trim(), fila[10].ToString().Trim() });
                    
                }
            }
            ColorEstado();
            label28.Text = "Total documentos Enviados: " + Grid1.RowCount;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmEnviarSunat_Load(object sender, EventArgs e)
        {
            try
            {
                string vModulo = "VEN";
                DataSet datosTDoc = csql.dataset("Call SpDocBusElectronicos('" + vModulo.ToString() + "')");

                if (datosTDoc.Tables[0].Rows.Count > 0)
                {
                    comboBox4.Items.Add("");
                    foreach (DataRow fila in datosTDoc.Tables[0].Rows)
                    {
                        comboBox4.Items.Add(fila[1].ToString());
                    }
                }

                string tipPresDoc = "1";
                //Carga Almacen
                DataSet datosAlm = csql.dataset("Call SpAlmacenGen('" + rucEmpresa.ToString() + "')");
                comboBox3.DisplayMember = "nombre";
                comboBox3.ValueMember = "CodAlmacen";
                comboBox3.DataSource = datosAlm.Tables[0];
                comboBox3.Text = nomAlmacen.ToString();

                string vFecha1;
                string vFecha2;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();
                vFecha2 = DTP2.Value.Day.ToString("00") + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Year.ToString();
                vAlmacen = comboBox3.SelectedValue.ToString();
                datos = csql.dataset_cadena("Call SpEnvioConsulta('" + vFecha1.ToString() + "','" + vFecha2.ToString() + "','','','','" + vAlmacen.ToString().Trim() + "','" + rucEmpresa.ToString() + "')");
                Grid1.Rows.Clear();
                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        this.Grid1.Rows.Add(new[] { fila[0].ToString(), fila[1].ToString(), fila[2].ToString(), fila[3].ToString(), fila[4].ToString(), fila[5].ToString().Trim(), fila[6].ToString().Trim(), Boolean.FalseString, fila[7].ToString().Trim(), fila[8].ToString().Trim(), fila[9].ToString().Trim(), fila[10].ToString().Trim() });
                    }
                }

                //CargarConsulta();
                ColorEstado();
                label28.Text = "Total documentos Enviados: " + Grid1.RowCount;
                //Generar ToolTip
                ToolTip toolTip1 = new ToolTip();

                // Set up the delays for the ToolTip.
                toolTip1.AutoPopDelay = 5000;
                toolTip1.InitialDelay = 300;
                toolTip1.ReshowDelay = 500;
                // Force the ToolTip text to be displayed whether or not the form is active.
                toolTip1.ShowAlways = true;

                // Set up the ToolTip text for the Button and Checkbox.
                toolTip1.SetToolTip(this.button1, "Buscar");
                toolTip1.SetToolTip(this.button2, "Realizar envío a SUNAT");
                toolTip1.SetToolTip(this.button3, "Descargar archivo XML");
                toolTip1.SetToolTip(this.button4, "Generar archivo XML");
                toolTip1.SetToolTip(this.button7, "Salir");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargarConsulta();
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
                    textBox3.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            {
                //try
                //{
                //Valida numeros
                if (//char.IsLetter(e.KeyChar) || //Letras
                char.IsSymbol(e.KeyChar) || //Símbolos
                char.IsWhiteSpace(e.KeyChar) || //Espaçio
                char.IsPunctuation(e.KeyChar)) //Pontuacion
                    e.Handled = true;

                if (e.KeyChar == 13)
                {
                    double Net = 0;
                    Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                    if (Net.ToString().Trim().Equals("0"))
                        textBox3.Text = "";
                    else
                        textBox3.Text = Net.ToString("000").Trim();

                    textBox4.Focus();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                string vSerie = Grid1.CurrentRow.Cells[2].Value.ToString();

                nIdVenta = Grid1.CurrentRow.Cells[10].Value.ToString();
                vAlm = Grid1.CurrentRow.Cells[8].Value.ToString();
                ObjGrabaXML.generaXMLFactura(nIdVenta, rucEmpresa, vAlm, false);
                MessageBox.Show("Archivo XML generado satisfactoriamente", "SISTEMA");
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                nIdVenta = Grid1.CurrentRow.Cells[10].Value.ToString();
                vAlm = Grid1.CurrentRow.Cells[8].Value.ToString();
                vDoc = Grid1.CurrentRow.Cells[11].Value.ToString();

                string vXml = Grid1.CurrentRow.Cells[6].Value.ToString();

                if (vXml.Equals("No"))
                {
                    MessageBox.Show("Debe generar el XML para realizar el envío", "SISTEMA");
                    return;
                }

                nIdVenta = Grid1.CurrentRow.Cells[10].Value.ToString();
                nomXml = Grid1.CurrentRow.Cells[2].Value.ToString();

                FrmEnviaXmlEnvio frmEnviaXmlEnvio = new FrmEnviaXmlEnvio();
                frmEnviaXmlEnvio.WindowState = FormWindowState.Normal;
                frmEnviaXmlEnvio.ShowDialog(this);
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
                nIdVenta = Grid1.CurrentRow.Cells[10].Value.ToString();
                nomXml = Grid1.CurrentRow.Cells[2].Value.ToString();
                vAlm = Grid1.CurrentRow.Cells[8].Value.ToString();
                if (!ObjEnvio.BuscarEnvioComp(nIdVenta, rucEmpresa, vAlm))
                {
                    return;
                }

                ObjGrabaXML.descargaXML(nomXml, ObjEnvio.ArchXml, rucEmpresa, vAlm);
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
            }
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog archivo = new SaveFileDialog();
                archivo.Filter = "Excel (*.xls)|*.xls";
                archivo.FileName = "Documentos Rechazados" + DateTime.Now.Date.ToShortDateString().Replace('/', '-');
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application aplicacion;
                    Microsoft.Office.Interop.Excel.Workbook libroDeTrabajo;
                    Microsoft.Office.Interop.Excel.Worksheet hojaDeTrabajo;

                    aplicacion = new Microsoft.Office.Interop.Excel.Application();
                    libroDeTrabajo = aplicacion.Workbooks.Add();
                    hojaDeTrabajo = (Microsoft.Office.Interop.Excel.Worksheet)libroDeTrabajo.Worksheets.get_Item(1);

                    hojaDeTrabajo.Cells[1, "A"] = Grid1.Columns[1].HeaderText;
                    hojaDeTrabajo.Cells[1, "B"] = Grid1.Columns[2].HeaderText;
                    hojaDeTrabajo.Cells[1, "C"] = Grid1.Columns[3].HeaderText;
                    hojaDeTrabajo.Cells[1, "D"] = Grid1.Columns[4].HeaderText;
                    hojaDeTrabajo.Cells[1, "E"] = Grid1.Columns[5].HeaderText;
                    hojaDeTrabajo.Cells[1, "F"] = Grid1.Columns[6].HeaderText;
                    hojaDeTrabajo.Cells[1, "G"] = Grid1.Columns[7].HeaderText;
                    hojaDeTrabajo.Cells[1, "H"] = Grid1.Columns[8].HeaderText;
                    hojaDeTrabajo.Cells[1, "I"] = Grid1.Columns[9].HeaderText;
                    hojaDeTrabajo.Cells[1, "J"] = Grid1.Columns[10].HeaderText;
                    hojaDeTrabajo.Cells[1, "K"] = Grid1.Columns[11].HeaderText;
                    hojaDeTrabajo.Cells[1, "L"] = Grid1.Columns[12].HeaderText;

                    hojaDeTrabajo.Columns[1].AutoFit();
                    hojaDeTrabajo.Columns[2].AutoFit();
                    hojaDeTrabajo.Columns[3].AutoFit();
                    hojaDeTrabajo.Columns[4].AutoFit();
                    hojaDeTrabajo.Columns[5].AutoFit();
                    hojaDeTrabajo.Columns[6].AutoFit();
                    hojaDeTrabajo.Columns[7].AutoFit();
                    hojaDeTrabajo.Columns[8].AutoFit();
                    hojaDeTrabajo.Columns[9].AutoFit();
                    hojaDeTrabajo.Columns[10].AutoFit();
                    hojaDeTrabajo.Columns[11].AutoFit();
                    hojaDeTrabajo.Columns[12].Autofit();

                    hojaDeTrabajo.Name = "DOCUMENTOS";

                    //Recorremos el DataGridView rellenando la hoja de trabajo
                    
                    for (int i = 0; i < Grid1.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < Grid1.Columns.Count; j++)
                        {
                            hojaDeTrabajo.Cells[i + 1, j + 1] = Grid1.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    libroDeTrabajo.SaveAs(archivo.FileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    libroDeTrabajo.Close(true);
                    aplicacion.Quit();
                    MessageBox.Show("DOCUMENTOS EXPORTADOS", "NANO");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar la informacion debido a: " + ex.ToString());
            }
        }
    }
}