using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmReimprimirComp : Form
    {
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsRol ObjRol = new ClsRol();
        private ClsCliente ObjCliente = new ClsCliente();
        private ClsVenta ObjVenta = new ClsVenta();
        private ClsImprimir ObjImprimir = new ClsImprimir();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsImprimirProforma ObjProfo = new ClsImprimirProforma();
        private ClsAlmacen ObjAlmacen = new ClsAlmacen();
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsSerie ObjSerie = new ClsSerie();
        private ClsRolUser ObjRolUser = new ClsRolUser();

        DataSet datos;

        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        private string usuario = FrmLogin.x_codigo_usuario;
        private string nomCodUser = FrmLogin.x_codigo_usuario;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string Cod = "";
        private string vIdVenta = ""; //Id de venta
        private string vDocu = "";
        private string TipDoc = "";
        //string vParam = "";

        public FrmReimprimirComp()
        {
            InitializeComponent();
        }

        private void buscarDocAsociado(string idVenta, string vRuc, string vAlmacen)
        {
            string cDoc = comboBox1.SelectedValue.ToString();
            if (ObjVenta.BuscarComprobate(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
            {
                DateTime vFecha = DateTime.Parse(ObjVenta.Fecha.ToString());
                textBox10.Text = String.Format("{0:dd/MM/yyyy}", vFecha);
                textBox2.Text = ObjVenta.TipDocCli.ToString();
                textBox1.Text = ObjVenta.Cliente.ToString();
                if (ObjVenta.Cliente.ToString().Equals("C0000000001"))
                {
                    textBox2.Text = "";
                    textBox1.Text = "";
                }

                //Cliente
                if (ObjCliente.BuscarCLiente(ObjVenta.Cliente.ToString(), rucEmpresa))
                {
                    textBox5.Text = ObjCliente.Nombre.ToString();
                    textBox6.Text = ObjCliente.Direccion.ToString();
                }
                else
                {
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                textBox7.Text = ObjVenta.TBruto.ToString();
                double Net = 0;
                //Total bruto
                Net = double.Parse(ObjVenta.TBruto.ToString().Equals("") ? "0" : ObjVenta.TBruto.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox7.Text = "0.00";
                else
                    textBox7.Text = Net.ToString("###,##0.0000").Trim();
                //Total Igv
                Net = double.Parse(ObjVenta.TIgv.ToString().Equals("") ? "0" : ObjVenta.TIgv.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox8.Text = "0.00";
                else
                    textBox8.Text = Net.ToString("###,##0.0000").Trim();

                //Importe Total
                Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox9.Text = "0.00";
                else
                    textBox9.Text = Net.ToString("###,##0.0000").Trim();
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para el Comprobante ingresado, verifique.", "SISTEMA"
                    );
                /*textBox7.Text = "";
                CalculaTotal();
                Grid1.Rows.Clear();*/
            }
        }

        private void buscarDocAsociadoProforma(string idVenta, string vRuc, string vAlmacen)
        {
            string cDoc = comboBox1.SelectedValue.ToString();
            if (ObjVenta.BuscarComprobateProforma(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
            {
                DateTime vFecha = DateTime.Parse(ObjVenta.Fecha.ToString());
                textBox10.Text = String.Format("{0:dd/MM/yyyy}", vFecha);
                textBox2.Text = ObjVenta.TipDocCli.ToString();
                textBox1.Text = ObjVenta.Cliente.ToString();
                if (ObjVenta.Cliente.ToString().Equals("C0000000001"))
                {
                    textBox2.Text = "";
                    textBox1.Text = "";
                }

                //Cliente
                if (ObjCliente.BuscarCLiente(ObjVenta.Cliente.ToString(), rucEmpresa))
                {
                    textBox5.Text = ObjCliente.Nombre.ToString();
                    textBox6.Text = ObjCliente.Direccion.ToString();
                }
                else
                {
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
                textBox7.Text = ObjVenta.TBruto.ToString();
                double Net = 0;
                //Total bruto
                Net = double.Parse(ObjVenta.TBruto.ToString().Equals("") ? "0" : ObjVenta.TBruto.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox7.Text = "0.00";
                else
                    textBox7.Text = Net.ToString("###,##0.0000").Trim();
                //Total Igv
                Net = double.Parse(ObjVenta.TIgv.ToString().Equals("") ? "0" : ObjVenta.TIgv.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox8.Text = "0.00";
                else
                    textBox8.Text = Net.ToString("###,##0.0000").Trim();

                //Importe Total
                Net = double.Parse(ObjVenta.Total.ToString().Equals("") ? "0" : ObjVenta.Total.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox9.Text = "0.00";
                else
                    textBox9.Text = Net.ToString("###,##0.0000").Trim();
            }
            else
            {
                MessageBox.Show("No se encontraron resultados para el Comprobante ingresado, verifique.", "SISTEMA");
                /*textBox7.Text = "";
                CalculaTotal();
                Grid1.Rows.Clear();*/
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmReimprimirComp_Load(object sender, EventArgs e)
        {
            //Carga Formato de Impresion
            string codFormImp = "025";
            string tipPresDoc = "1";
            DataSet datosFormato = csql.dataset_cadena("Call SpCargarDetCat('" + codFormImp + "','" + tipPresDoc + "')");

            if (datosFormato.Tables[0].Rows.Count > 0)
            {
                comboBox2.Items.Add("");
                foreach (DataRow fila in datosFormato.Tables[0].Rows)
                {
                    comboBox2.Items.Add(fila[1].ToString());
                }
            }

            comboBox3.Items.Add("");

            foreach (String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBox3.Items.Add(strPrinter);
            }

            Cod = FrmVentas.nIdVenta.ToString();
            TipDoc = FrmVentas.TipoPago.ToString();
            string vModulo = "VEN";

            DataSet datosDoc = csql.dataset("Call SpDocBusElectronicos('" + vModulo.ToString() + "')");
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "Codigo";
            comboBox1.DataSource = datosDoc.Tables[0];

            if (!Cod.Equals(""))
            {
                LlenarCampos(Cod);
                buscarDocAsociado(Cod.ToString(), rucEmpresa.ToString(), codAlmacen.ToString());
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
                textBox3.Focus();
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
                //Datos de venta

                string cDoc = comboBox1.SelectedValue.ToString();

                if (cDoc.Equals("028"))
                {
                    if (!ObjVenta.BuscarComprobateProforma(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
                    {
                        MessageBox.Show("Error al buscar Comprobante", "SISTEMA");
                        textBox4.Focus();
                        return;
                    }
                }
                else
                {
                    if (!ObjVenta.BuscarComprobate(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
                    {
                        MessageBox.Show("Error al buscar Comprobante", "SISTEMA");
                        textBox4.Focus();
                        return;
                    }
                }

                string cCliente = "";
                if (ObjVenta.Cliente.ToString().Equals("C0000000001"))
                {
                    cCliente = ObjVenta.Cliente.ToString();
                    vIdVenta = cCliente.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();
                }
                else
                {
                    vIdVenta = textBox1.Text.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();
                }

                string TipoPago = "";
                datos = csql.dataset("select TipoPago from tblventa where IdVenta = '" + vIdVenta + "'");
                TipoPago = datos.Tables[0].Rows[0][0].ToString();
                string vIdUser = nomCodUser;
                if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
                {
                    if (ObjRol.BuscarRol(ObjRolUser.IdRol))
                    {
                        string valiUserRol = ObjRol.Nombre.ToString();

                        //Validar el Rol del Usuario y el tipo de Servicio
                        if (valiUserRol.ToString().Equals("VENDEDOR"))
                        {
                            if (TipoPago.ToString().Equals("001") || TipoPago.ToString().Equals("002"))
                            {
                                buscarDocAsociado(vIdVenta.ToString(), rucEmpresa.ToString(), codAlmacen.ToString());
                            }
                            else
                            {
                                MessageBox.Show("Solo puedes reimprimir YAPE y PLIN");
                                textBox4.Focus();
                                return;
                            }
                        }
                    }
                }
                buscarDocAsociado(vIdVenta.ToString(), rucEmpresa.ToString(), codAlmacen.ToString());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Mensaje: " + ex.Message, "SISTEMA");
                //textBox3.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
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
            //Datos de venta

            string cDoc = comboBox1.SelectedValue.ToString();

            if (cDoc.Equals("028"))
            {
                if (!ObjVenta.BuscarComprobateProforma(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    MessageBox.Show("Error al buscar Comprobante", "SISTEMA");
                    textBox4.Focus();
                    return;
                }
            }
            else
            {
                if (!ObjVenta.BuscarComprobate(cDoc.ToString(), textBox4.Text.ToString(), textBox3.Text.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    MessageBox.Show("Error al buscar Comprobante", "SISTEMA");
                    textBox4.Focus();
                    return;
                }
            }

            string cCliente = "";
            if (ObjVenta.Cliente.ToString().Equals("C0000000001"))
            {
                cCliente = ObjVenta.Cliente.ToString();
                vIdVenta = cCliente.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();
            }
            else
            {
                vIdVenta = textBox1.Text.ToString() + comboBox1.SelectedValue.ToString() + textBox4.Text.ToString() + "-" + textBox3.Text.ToString() + codAlmacen.ToString() + rucEmpresa.ToString();
            }

            string TipoPago = "";
            datos = csql.dataset("select TipoPago from tblventa where IdVenta = '"+ vIdVenta +"'");
            TipoPago = datos.Tables[0].Rows[0][0].ToString();

            string vIdUser = nomCodUser;
            if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
            {
                if (ObjRol.BuscarRol(ObjRolUser.IdRol))
                {
                    string valiUserRol = ObjRol.Nombre.ToString();

                    //Validar el Rol del Usuario y el tipo de Servicio
                    if (valiUserRol.ToString().Equals("VENDEDOR"))
                    {
                        if (TipoPago.ToString().Equals("001") || TipoPago.ToString().Equals("002"))
                        {
                            if (cDoc.Equals("028"))
                            {
                                ObjProfo.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                            }
                            else
                            {
                                string comprobante = comboBox1.SelectedValue.ToString();

                                if (ObjDocumento.BuscarDoc(comprobante.ToString()))// rol admin , verifique el formato
                                {
                                    if (ObjRolUser.BuscarRolUser(usuario))
                                    {
                                        string rol = ObjRolUser.IdRol.ToString().Trim();
                                        if (rol.Equals("001"))
                                        {
                                            if (ObjDocumento.Imp.Equals("S"))

                                            {//aca
                                                if (ObjSerie.BuscarDocSerie(comprobante.ToString(), textBox4.Text.ToString().Trim()))

                                                {
                                                    if (comboBox2.Text.Equals(("")))
                                                    {
                                                        MessageBox.Show("Seleccione Tipo de Formato de Impresión");
                                                        comboBox2.Focus();
                                                        return;
                                                    }

                                                    if (comboBox3.Text.Equals(("")))
                                                    {
                                                        MessageBox.Show("Seleccione Impresora");
                                                        comboBox2.Focus();
                                                        return;
                                                    }

                                                    if ((comboBox2.Text.Equals("") && comboBox2.Text == null) || (comboBox3.Text.Equals("") && comboBox3.Text == null))
                                                    {
                                                        if (comboBox2.Text.Equals("FORMATO TICKET") || comboBox2.Text.Equals("") || comboBox2.Text == null)
                                                        {
                                                            ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                        }
                                                        else
                                                        {
                                                            ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, "");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (comboBox2.Text.Equals("FORMATO TICKET") || comboBox2.Text.Equals("") || comboBox2.Text == null)
                                                        {
                                                            ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                        }
                                                        else
                                                        {
                                                            ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, comboBox3.Text);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (comboBox2.Text.Equals("FORMATO TICKET") || comboBox2.Text.Equals("") || comboBox2.Text == null)
                                                    {
                                                        ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                    }
                                                    else
                                                    {
                                                        ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, "");
                                                    }
                                                }
                                            }//aca
                                        }
                                        else
                                        {
                                            if (ObjDocumento.Imp.Equals("S"))

                                            {//aca
                                                if (ObjSerie.BuscarDocSerie(comprobante.ToString(), textBox4.Text.ToString().Trim()))
                                                {
                                                    if ((ObjSerie.Formato_Imp.Equals("") && ObjSerie.Formato_Imp == null) || (ObjSerie.Impresora.Equals("") && ObjSerie.Impresora == null))
                                                    {
                                                        if (ObjDocumento.Formato_Imp.Equals("001") || ObjDocumento.Formato_Imp.Equals("") || ObjDocumento.Formato_Imp == null)
                                                        {
                                                            ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                        }
                                                        else
                                                        {
                                                            ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, "");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (ObjSerie.Formato_Imp.Equals("001") || ObjSerie.Formato_Imp.Equals("") || ObjSerie.Formato_Imp == null)
                                                        {
                                                            ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                        }
                                                        else
                                                        {
                                                            ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, ObjSerie.Impresora);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (ObjDocumento.Formato_Imp.Equals("001") || ObjDocumento.Formato_Imp.Equals("") || ObjDocumento.Formato_Imp == null)
                                                    {
                                                        ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                    }
                                                    else
                                                    {
                                                        ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, "");
                                                    }
                                                }
                                            }//aca
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Solo puedes reimprimir YAPE y PLIN");
                            textBox4.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (cDoc.Equals("028"))
                        {
                            ObjProfo.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                        }
                        else
                        {
                            string comprobante = comboBox1.SelectedValue.ToString();

                            if (ObjDocumento.BuscarDoc(comprobante.ToString()))// rol admin , verifique el formato
                            {
                                if (ObjRolUser.BuscarRolUser(usuario))
                                {
                                    string rol = ObjRolUser.IdRol.ToString().Trim();
                                    if (rol.Equals("001"))
                                    {
                                        if (ObjDocumento.Imp.Equals("S"))

                                        {//aca
                                            if (ObjSerie.BuscarDocSerie(comprobante.ToString(), textBox4.Text.ToString().Trim()))

                                            {
                                                if (comboBox2.Text.Equals(("")))
                                                {
                                                    MessageBox.Show("Seleccione Tipo de Formato de Impresión");
                                                    comboBox2.Focus();
                                                    return;
                                                }

                                                if (comboBox3.Text.Equals(("")))
                                                {
                                                    MessageBox.Show("Seleccione Impresora");
                                                    comboBox2.Focus();
                                                    return;
                                                }

                                                if ((comboBox2.Text.Equals("") && comboBox2.Text == null) || (comboBox3.Text.Equals("") && comboBox3.Text == null))
                                                {
                                                    if (comboBox2.Text.Equals("FORMATO TICKET") || comboBox2.Text.Equals("") || comboBox2.Text == null)
                                                    {
                                                        ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                    }
                                                    else
                                                    {
                                                        ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, "");
                                                    }
                                                }
                                                else
                                                {
                                                    if (comboBox2.Text.Equals("FORMATO TICKET") || comboBox2.Text.Equals("") || comboBox2.Text == null)
                                                    {
                                                        ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                    }
                                                    else
                                                    {
                                                        ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, comboBox3.Text);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (comboBox2.Text.Equals("FORMATO TICKET") || comboBox2.Text.Equals("") || comboBox2.Text == null)
                                                {
                                                    ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                }
                                                else
                                                {
                                                    ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, "");
                                                }
                                            }
                                        }//aca
                                    }
                                    else
                                    {
                                        if (ObjDocumento.Imp.Equals("S"))

                                        {//aca
                                            if (ObjSerie.BuscarDocSerie(comprobante.ToString(), textBox4.Text.ToString().Trim()))
                                            {
                                                if ((ObjSerie.Formato_Imp.Equals("") && ObjSerie.Formato_Imp == null) || (ObjSerie.Impresora.Equals("") && ObjSerie.Impresora == null))
                                                {
                                                    if (ObjDocumento.Formato_Imp.Equals("001") || ObjDocumento.Formato_Imp.Equals("") || ObjDocumento.Formato_Imp == null)
                                                    {
                                                        ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                    }
                                                    else
                                                    {
                                                        ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, "");
                                                    }
                                                }
                                                else
                                                {
                                                    if (ObjSerie.Formato_Imp.Equals("001") || ObjSerie.Formato_Imp.Equals("") || ObjSerie.Formato_Imp == null)
                                                    {
                                                        ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                    }
                                                    else
                                                    {
                                                        ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, ObjSerie.Impresora);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (ObjDocumento.Formato_Imp.Equals("001") || ObjDocumento.Formato_Imp.Equals("") || ObjDocumento.Formato_Imp == null)
                                                {
                                                    ObjImprimir.ImprimirTicket(vIdVenta, rucEmpresa, codAlmacen, false, false);
                                                }
                                                else
                                                {
                                                    ObjImprimir.ImprimirNormal(vIdVenta, rucEmpresa, codAlmacen, false, false, "");
                                                }
                                            }
                                        }//aca
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Fallo en la Validacion de Usuarios" + "\n" +
                                          "Por Favor Comunicarse con el Area de SOPORTE", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MessageBox.Show("Comprobante enviado a impresora", "SISTEMA");

            textBox10.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox4.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void Formaimpresion(string InCod, string serie)
        {
            string nomFormato = "";
            if (ObjSerie.BuscarDocSerie(InCod, serie))
            {
                string vCodCat = "025";
                if (ObjDetCatalogo.BuscarDetCatalogoCod(vCodCat.ToString(), ObjSerie.Formato_Imp.ToString()))
                {
                    nomFormato = ObjDetCatalogo.Descripcion.ToString();
                }
                else
                {
                    nomFormato = "";
                }

                comboBox2.Text = nomFormato;
                comboBox3.Text = ObjSerie.Impresora.ToString();
            }
            else
            {
                MessageBox.Show("FALSE");
            }
        }

        private void LlenarCampos(String vIdVenta)
        {
            try
            {
                string tipodoc;
                if (ObjVenta.BuscarVenta(vIdVenta, rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    textBox3.Text = ObjVenta.Numero.ToString().Trim();
                    textBox4.Text = ObjVenta.Serie.ToString().Trim();
                    comboBox1.Text = ObjVenta.Doc.ToString().Trim();

                    if (comboBox1.Text.Equals("BOLETA DE VENTA ELECTRONICA"))
                    {
                        tipodoc = "013";

                        Formaimpresion(tipodoc, textBox4.Text);
                    }
                    else if (comboBox1.Text.Equals("FACTURA DE VENTA ELECTRONICA"))
                    {
                        tipodoc = "014";

                        Formaimpresion(tipodoc, textBox4.Text);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se puede cargar datos: " + ex.Message);
                this.Close();
            }
        }

        private void LlenarCamposProforma(String vIdVenta)
        {
            try
            {
                if (ObjVenta.BuscarVentaProforma(vIdVenta, rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    textBox3.Text = ObjVenta.Numero.ToString().Trim();
                    textBox4.Text = ObjVenta.Serie.ToString().Trim();
                    comboBox1.Text = ObjVenta.Doc.ToString().Trim();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se puede cargar datos: " + ex.Message);
                this.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }
    }
}