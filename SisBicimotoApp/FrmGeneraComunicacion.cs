using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmGeneraComunicacion : Form
    {
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsComunicacionBaja ObjComunicacionBaja = new ClsComunicacionBaja();
        private ClsDetComunicacionBaja ObjDetComunicacionBaja = new ClsDetComunicacionBaja();
        private ClsGrabaXML ObjGrabaXML = new ClsGrabaXML();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;

        private string codModulo = "VEN";
        private string vIdVenta = "";

        public FrmGeneraComunicacion()
        {
            InitializeComponent();
        }

        private void buscarDocAsociado(string idVenta, string vRuc, string vAlmacen)
        {
            string cDoc = comboBox1.SelectedValue.ToString();
            if (ObjVenta.BuscarComprobate(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
            {
                textBox10.Text = ObjVenta.Cliente.ToString();
                vIdVenta = textBox10.Text.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();
                label9.Text = vIdVenta.ToString();

                DateTime vFecha = DateTime.Parse(ObjVenta.Fecha.ToString());
                textBox7.Text = String.Format("{0:dd/MM/yyyy}", vFecha);
                label6.Text = ObjVenta.TipDocCli.ToString();

                /*if (ObjVenta.Cliente.ToString().Equals("C0000000001"))
                {
                    label6.Text = "";
                    textBox1.Text = "C0000000001";
                }*/

                //Cliente
                if (ObjCliente.BuscarCLiente(ObjVenta.Cliente.ToString(), rucEmpresa))
                {
                    label16.Text = ObjCliente.Nombre.ToString();
                }
                else
                {
                    label16.Text = "";
                }
                double Net = 0;
                //Importe Total
                Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox1.Text = "0.00";
                else
                    textBox1.Text = Net.ToString("###,##0.00").Trim();

                if (ObjDetComunicacionBaja.BuscarVenta(vIdVenta.ToString(), rucEmpresa.ToString()))
                {
                    MessageBox.Show("El Comprobante ya tiene baja registrada, verifique.", "SISTEMA");
                    textBox4.Text = "";
                    textBox3.Text = "";
                    textBox7.Text = "";
                    textBox1.Text = "";
                    textBox10.Text = "";
                    label16.Text = "";
                    textBox4.Focus();
                    //return;
                }
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para el Comprobante ingresado, verifique.", "SISTEMA");
                /*textBox7.Text = "";
                CalculaTotal();
                Grid1.Rows.Clear();*/
            }
        }

        private void FrmGeneraComunicacion_Load(object sender, EventArgs e)
        {
            //string nParam = "21";
            //SERVICIO web
            //string vServWeb = "";
            /*if (ObjParametro.BuscarParametro(nParam))
            {
                vServWeb = ObjParametro.Valor;
            }
            textBox2.Text = vServWeb;*/

            try
            {
                string archivo = System.Environment.CurrentDirectory + @"\base.ini";
                cini ciniarchivo = new cini(archivo);
                string vServWeb = "";
                vServWeb = ciniarchivo.ReadValue("Configura", "Service", "");
                textBox2.Text = vServWeb;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Hubo problemas al leer el archivo INI. " + ex.Message, "SISTEMA");
            }

            //Carga Documento
            DataSet datosDoc = csql.dataset("Call SpDocBusElectronicos('" + codModulo.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "Codigo";
            comboBox1.DataSource = datosDoc.Tables[0];

            label5.Text = "Total documentos seleccionados: " + Grid1.RowCount;

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
            toolTip1.SetToolTip(this.button5, "Eliminar Registro");
            toolTip1.SetToolTip(this.button2, "Aceptar");
            toolTip1.SetToolTip(this.button1, "Enviar Comunicación de Baja");
            toolTip1.SetToolTip(this.button8, "Salir");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
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

            if (textBox7.TextLength == 0 || textBox1.TextLength == 0 || textBox10.TextLength == 0)
            {
                MessageBox.Show("Ingrese Datos de Comprobante", "SISTEMA");
                textBox4.Focus();
                return;
            }

            //Valida datos de Comprobante
            string cDoc = comboBox1.SelectedValue.ToString();
            if (ObjVenta.BuscarComprobate(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
            {
                vIdVenta = textBox10.Text.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();
                //label9.Text = vIdVenta.ToString();

                if (label9.Text != vIdVenta.ToString())
                {
                    MessageBox.Show("Los datos del Comprobante no conciden con el número de comprobante ingresado " + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + " por favor realize la búsqueda nuevamente", "SISTEMA");
                    return;
                }
            }
            else
            {
                //MessageBox.Show("No se encontraron resultados para el Comprobante ingresado, verifique.", "SISTEMA");
            }

            //Valida si el comprobante existe
            Boolean existe = false;
            int nIndex = 0;
            int contador = 0;

            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells[5].Value.ToString().Equals(label9.Text.ToString().Trim()))
                {
                    existe = true;
                    nIndex = contador;
                }
                contador += contador + 1;
            }

            if (existe)
            {
                MessageBox.Show("Comprobante ya existe en la lista", "SISTEMA");
                return;
            }
            string NDoc = "";
            //Selecciona Documento NCorto
            if (ObjDocumento.BuscarDoc(comboBox1.SelectedValue.ToString()))
            {
                NDoc = ObjDocumento.NCorto.ToString();
            }
            this.Grid1.Rows.Add(new[] { textBox7.Text.ToString(),
                                            NDoc.ToString() + " " + textBox4.Text.ToString() +"-" + textBox3.Text.ToString(),
                                            textBox10.Text.ToString(),
                                            label16.Text.ToString(),
                                            textBox1.Text.ToString(),
                                            label9.Text.ToString(),
                                            comboBox1.SelectedValue.ToString(),
                                            textBox4.Text.ToString(),
                                            textBox3.Text.ToString()});

            label5.Text = "Total documentos seleccionados: " + Grid1.RowCount;

            textBox4.Text = "";
            textBox3.Text = "";
            textBox7.Text = "";
            textBox1.Text = "";
            textBox10.Text = "";
            label16.Text = "";
            textBox4.Focus();
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

                vIdVenta = textBox10.Text.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();

                buscarDocAsociado(vIdVenta.ToString(), rucEmpresa.ToString(), codAlmacen.ToString());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Mensaje: " + ex.Message, "SISTEMA");
                //textBox3.Focus();
            }
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
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

        private void textBox4_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (//char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                int index = Grid1.CurrentRow.Index;
                Grid1.Rows.RemoveAt(index);
                label5.Text = "Total documentos seleccionados: " + Grid1.RowCount;
            }
            else
            {
                MessageBox.Show("Seleccione un registro", "SISTEMA");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Datos Correctos, se procedera a registrar el envío de la Comunicación de Baja", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (Grid1.RowCount > 0)
            {
                string vFecha1;
                vFecha1 = DTP1.Value.Day.ToString("00") + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Year.ToString();

                /*DateTime fechaHoy = DateTime.Now;
                string fecha = fechaHoy.ToString("d");
                string fechaAnio = fecha.Substring(6, 4);
                string fechaMes = fecha.Substring(3, 2);
                string fechaDia = fecha.Substring(0, 2);
                string fecActual = fechaAnio.ToString() + "/" + fechaMes.ToString() + "/" + fechaDia.ToString();*/
                string hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + DateTime.Now.Second.ToString("D2");

                //string vCodEnvio = "20352471578" + "-" + "RA" + "-" + DTP1.Value.Year.ToString() + DTP1.Value.Month.ToString("00") + DTP1.Value.Day.ToString("00");

                string vCodEnvio = "RA" + "-" + DTP1.Value.Year.ToString() + DTP1.Value.Month.ToString("00") + DTP1.Value.Day.ToString("00");

                int nValor = ObjComunicacionBaja.ValorConteo(vCodEnvio, rucEmpresa);
                string Usuario = FrmLogin.x_login_usuario;

                ObjComunicacionBaja.Id = nValor + 1;
                ObjComunicacionBaja.NDocBaja = vCodEnvio.ToString();
                ObjComunicacionBaja.Fecha = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
                ObjComunicacionBaja.NumDocs = Grid1.RowCount;
                ObjComunicacionBaja.Est = "A";
                ObjComunicacionBaja.RucEmpresa = rucEmpresa;
                ObjComunicacionBaja.UserCreacion = Usuario.ToString();

                //Detalles
                int n = 1;
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    ObjDetComunicacionBaja.Id = nValor + 1;
                    ObjDetComunicacionBaja.NDocBaja = vCodEnvio.ToString();
                    ObjDetComunicacionBaja.NumId = n;
                    ObjDetComunicacionBaja.TipDoc = row.Cells[6].Value.ToString();
                    ObjDetComunicacionBaja.Serie = row.Cells[7].Value.ToString();
                    ObjDetComunicacionBaja.NumDoc = row.Cells[8].Value.ToString();
                    ObjDetComunicacionBaja.MotivoBaja = "ANULACION";
                    ObjDetComunicacionBaja.IdVenta = row.Cells[5].Value.ToString();
                    ObjDetComunicacionBaja.RucEmpresa = rucEmpresa;
                    ObjDetComunicacionBaja.UserCreacion = Usuario.ToString();
                    n += 1;

                    if (ObjDetComunicacionBaja.Crear())
                    {
                    }
                    else
                    {
                        MessageBox.Show("No se puede crear registro de detalle de Comunicación, VERIFIQUE!!!", "SISTEMA");
                        return;
                    }
                }

                if (ObjComunicacionBaja.Crear())
                {
                    //Grabar XML
                    ObjGrabaXML.generaXMLBaja(ObjDetComunicacionBaja.Id.ToString(), vCodEnvio.ToString(), rucEmpresa, true);

                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                }
                else
                {
                    MessageBox.Show("No se puede crear registro de Comunicación, VERIFIQUE!!!", "SISTEMA");
                    return;
                }

                textBox4.Text = "";
                textBox3.Text = "";
                textBox7.Text = "";
                textBox1.Text = "";
                textBox10.Text = "";
                label16.Text = "";
                Grid1.Rows.Clear();
                label5.Text = "Total documentos seleccionados: " + Grid1.RowCount;
            }
            else
            {
                MessageBox.Show("Debe ingresar comprobantes", "SISTEMA");
                return;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }
    }
}