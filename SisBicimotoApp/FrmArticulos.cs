using CrystalDecisions.Shared.Json;
using Microsoft.Office.Interop.Excel;
using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using Point = System.Drawing.Point;

namespace SisBicimotoApp
{
    public partial class FrmArticulos : Form, IProveedor
    {
        public IProveedor Opener { get; set; }

        private string nomCodUser = FrmLogin.x_codigo_usuario;
        private ClsRolUser ObjRolUser = new ClsRolUser();
        private ClsParametro ObjParametro = new ClsParametro();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsTipoCambio ObjTipoCambio = new ClsTipoCambio();
        private ClsProveedor ObjProveedor = new ClsProveedor();

        public static char nmPro = 'N';

        private DataSet datos;
        private ClsRol ObjRol = new ClsRol();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        public static string codArt = "";
        public static string COSTO = "";
        public static string CIF = "";
        public static string PMAYOR = "";
        public static string PVENTA = "";
        public static string PVOLUMEN = "";
        public static int meses = 0;
        public static string nIdCompra = "";
        public static string nIdVenta = "";
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        public static char nmCom = 'N';
        public static char nmVen = 'N';
        private string nParam = "";

        private ClsProducto ObjProducto = new ClsProducto();
        private ClsProductoJefe ObjProductojefe = new ClsProductoJefe();

        public FrmArticulos()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[1].HeaderText = "Código";
            Grid1.Columns[2].HeaderText = "Nombre Producto";
            Grid1.Columns[3].HeaderText = "PCosto";
            Grid1.Columns[4].HeaderText = "Familia";
            // Grid1.Columns[5].HeaderText = "Línea";
            Grid1.Columns[5].HeaderText = "Estado";
            Grid1.Columns[6].HeaderText = "Stock";
            Grid1.Columns[7].HeaderText = "PCIF";
            Grid1.Columns[8].HeaderText = "P.Compra";
            Grid1.Columns[9].HeaderText = "P.Venta";
            Grid1.Columns[10].HeaderText = "PVolumen";
            Grid1.Columns[11].HeaderText = "Stock Real";
            Grid1.Columns[12].HeaderText = "Fecha Compra";
            Grid1.Columns[13].HeaderText = "Fecha Venta";
            Grid1.Columns[14].HeaderText = "RUC";
            Grid1.Columns[15].HeaderText = "Proveedor";
            Grid1.Columns[16].HeaderText = "Prom Venta";
            Grid1.Columns[17].HeaderText = "Cant Vol";
            Grid1.Columns[18].HeaderText = "Ubicacion";
            Grid1.Columns[20].HeaderText = "V.Min";
            //Grid1.Columns[18].HeaderText = "Ubicación";
            Grid1.Columns[1].Width = 52;
            Grid1.Columns[2].Width = 285;
            Grid1.Columns[3].Width = 80;
            Grid1.Columns[4].Width = 110;
            //Grid1.Columns[5].Width = 189;
            Grid1.Columns[5].Width = 60;
            Grid1.Columns[6].Width = 60;
            Grid1.Columns[7].Width = 60;
            Grid1.Columns[8].Width = 60;
            Grid1.Columns[9].Width = 60;
            Grid1.Columns[11].Width = 75;
            Grid1.Columns[12].Width = 100;
            Grid1.Columns[13].Width = 100;
            Grid1.Columns[18].Width = 75;
            Grid1.Columns[19].Width = 60;
            Grid1.Columns[20].Width = 60;
            //Grid1.Columns[18].Width = 100;
            Grid1.Columns[3].Visible = false;
            Grid1.Columns[7].Visible = false;
            Grid1.Columns[17].Visible = false;
            Grid1.Columns[16].Visible = false;
            Grid1.Columns[10].Visible = false;
            Grid1.Columns[12].Visible = false;
            Grid1.Columns[13].Visible = false;
            Grid1.Columns[14].Visible = false;
            Grid1.Columns[15].Visible = false;
            Grid1.Columns[18].Visible = false;

            //Grid1.Columns[16].Visible = false;
            Grid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //Grid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[20].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.BorderStyle = BorderStyle.None;
            Grid1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            Grid1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Grid1.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            Grid1.DefaultCellStyle.SelectionForeColor = Color.White;
            Grid1.BackgroundColor = Color.White;

            Grid1.EnableHeadersVisualStyles = false;
            Grid1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            Grid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            Grid1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        public void Grilla2()
        {
            Grid2.Columns[1].HeaderText = "id";
            Grid2.Columns[2].HeaderText = "Fecha";
            Grid2.Columns[3].HeaderText = "Movim.";
            Grid2.Columns[4].HeaderText = "Doc.";
            Grid2.Columns[5].HeaderText = "Número";
            Grid2.Columns[6].HeaderText = "Razón";
            Grid2.Columns[7].HeaderText = "Ingreso";
            Grid2.Columns[8].HeaderText = "Salida";
            Grid2.Columns[9].HeaderText = "CodArt";
            Grid2.Columns[10].HeaderText = "Precio Costo";
            Grid2.Columns[11].HeaderText = "Precio Vendido";
            Grid2.Columns[12].HeaderText = "Fecha Creacion";


            Grid2.Columns[1].Width = 50;
            Grid2.Columns[2].Width = 70;
            Grid2.Columns[3].Width = 55;
            Grid2.Columns[4].Width = 35;
            Grid2.Columns[5].Width = 120;
            Grid2.Columns[6].Width = 85;
            Grid2.Columns[7].Width = 70;
            Grid2.Columns[8].Width = 75;
            Grid2.Columns[9].Width = 50;
            Grid2.Columns[10].Width = 190;
            Grid2.Columns[11].Width = 198;
            Grid2.Columns[12].Width = 150;

            Grid2.Columns[1].Visible = false;
            Grid2.Columns[12].Visible = false;

            Grid2.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid2.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid2.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid2.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid2.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid2.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid2.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid2.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid2.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid2.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid2.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Grid2.BorderStyle = BorderStyle.None;
            Grid2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            Grid2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Grid2.DefaultCellStyle.SelectionBackColor = Color.DarkSlateBlue;
            Grid2.DefaultCellStyle.SelectionForeColor = Color.White;
            Grid2.BackgroundColor = Color.White;

            Grid2.EnableHeadersVisualStyles = false;
            Grid2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            Grid2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            Grid2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void LlenarCampos(string codart)
        {
            try
            {
                if (ObjProducto.BuscarProducto(codArt, rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    byte[] Bitsdatos = new byte[0];
                    Bitsdatos = (byte[])ObjProducto.Image;
                    pictureBox1.Image = ImageHelper.ByteArrayToImage(Bitsdatos);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    MessageBox.Show("FALSE");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("No se puede cargar datos: " + ex.Message);
            }
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpProductosBusGen('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void FrmArticulos_Load(object sender, EventArgs e)
        {
            nParam = "29";
            //Puerto smtp
            string vDimension = "";
            if (ObjParametro.BuscarParametro(nParam))
            {
                vDimension = ObjParametro.Valor;
            }

            string vIdUser = nomCodUser;
            if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
            {
                if (ObjRol.BuscarRol(ObjRolUser.IdRol))
                {
                    string valiUserRol = ObjRol.Nombre.ToString();

                    //Validar el Rol del Usuario y el tipo de Servicio
                        if (vDimension.Equals("1"))
                        {
                            this.StartPosition = FormStartPosition.CenterParent;
                            this.CenterToParent();
                            this.Size = new Size(1000, 670);
                            //this.Location = new Point(255, 50);

                            groupBox2.Location = new Point(27, 799);
                            groupBox3.Location = new Point(28, 468);

                            button3.Location = new Point(21, 422);
                            button12.Location = new Point(121, 422);
                            button17.Location = new Point(212, 422);
                            button15.Location = new Point(340, 422);
                            label3.Location = new Point(435, 432);
                            button1.Location = new Point(534, 416);
                            button13.Location = new Point(641, 422);
                            button10.Location = new Point(880, 418);

                            Grid1.Width = 910;
                            Grid1.Height = 250;
                        }
                }

            }
            else
            {
                MessageBox.Show("Fallo en la Validacion de Usuarios" + "\n" +
                                          "Por Favor Comunicarse con el Area de SOPORTE", "SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (ObjTipoCambio.BuscarTipoCambio())
            {
                double tCambio = ObjTipoCambio.Valor;
                textBox16.Text = tCambio.ToString("###,##0.000").Trim();
            }
            //Carga Ubicacion
           ;
            DataSet datos = csql.dataset_cadena("Call SpArticuloUbicacion('" + codAlmacen.ToString() + "','" + rucEmpresa.ToString() + "')");

            comboBox5.Items.Add("");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    comboBox5.Items.Add(fila[0].ToString());
                }
            }
            CargarDatos();

            label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();

            label22.Text = nomAlmacen.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nmPro = 'N';
            FrmAddProducto frmAddProducto = new FrmAddProducto();
            frmAddProducto.WindowState = FormWindowState.Normal;
            frmAddProducto.MdiParent = this.MdiParent;
            frmAddProducto.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                //button5.Focus();
                Grid1.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                codArt = Grid1.CurrentRow.Cells[1].Value.ToString();
                if (MessageBox.Show("¿Está seguro que desea eliminar este producto?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                //Validar Movimientos
                DataSet datosMov = csql.dataset_cadena("Call SpProductoValidaMov('" + codArt.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                string vMensajeMov = "";
                if (datosMov.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datosMov.Tables[0].Rows)
                    {
                        vMensajeMov = fila[0].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No existen Movimientos", "SISTEMA");
                }
                string Usuario = FrmLogin.x_login_usuario;
                if (vMensajeMov.Equals("0"))
                {
                    if (ObjProducto.Eliminar(codArt.ToString(), codAlmacen.ToString(), rucEmpresa.ToString()))
                    {
                        MessageBox.Show("Producto eliminado correctamente", "SISTEMA");
                        CargarDatos();
                    }
                    else
                    {
                        MessageBox.Show("No se realizó la eliminación del producto. Verifique", "SISTEMA");
                    }
                }
                else
                {
                    MessageBox.Show("No se realizó la eliminación del producto. Verifique..." + vMensajeMov, "SISTEMA");
                }
            }
            else
            {
                MessageBox.Show("No existen Productos registrados", "SISTEMA");
            }
        }

        private void cbBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string nnombre = textBox1.Text.Trim();
                datos = csql.dataset("Call SpProductoBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
            }
            else
            {
                CargarDatos();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string codigo = textBox2.Text.Trim();
                datos = csql.dataset("Call SpProductoBusCodG('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
            }
            else
            {
                CargarDatos();
            }
        }

        private void textBox2_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = textBox1.TextLength;
                textBox1.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmBusProveedor frmBusProveedor = new FrmBusProveedor();
            frmBusProveedor.WindowState = FormWindowState.Normal;
            frmBusProveedor.Opener = this;
            frmBusProveedor.MdiParent = this.MdiParent;
            frmBusProveedor.Show();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string rucProv = textBox9.Text.ToString().Trim();
            BusProveedor(rucProv, rucEmpresa);
        }

        private void BusProveedor(string vRuc, string rucEmpresa)
        {
            if (ObjProveedor.BuscarProveedor(vRuc, rucEmpresa))
            {
                label20.Text = ObjProveedor.Nombre.ToString().Trim();
            }
            else
            {
                label20.Text = "";
            }
            if (checkBox1.Checked == true)
            {
                try
                {
                    string codigo = txtAutoexplicativo3.Text.Trim();
                    datos = csql.dataset("Call SpArtProveeBus('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();

                    label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("No se encontro proveedor: " + ex.Message);
                }
            }
            else
            {
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //PUntuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button5.Focus();
            }
        }

        public void SelectItem(string codProv)
        {
            txtAutoexplicativo3.Text = codProv;
        }

        private void textBox9_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void label20_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            label20.Text = "";
            textBox1.Text = "";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox9.Text = "";
            label20.Text = "";
            textBox2.Text = "";
        }

        private void Grid1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Grid1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            nmPro = 'M';
            if (Grid1.RowCount > 0)
            {
                codArt = Grid1.CurrentRow.Cells[1].Value.ToString();
                FrmAddProducto frmAddProducto = new FrmAddProducto();
                frmAddProducto.WindowState = FormWindowState.Normal;
                frmAddProducto.MdiParent = this.MdiParent;
                frmAddProducto.Show();
            }
            else
            {
                MessageBox.Show("No existen Productos registrados", "SISTEMA");
            }
        }
        private void CalculaGananciaTipoCambio(string cambio)
        {
            var rowsCount = Grid1.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;
            string StockArt = "";
            double stock = 0;
            double pventa = 0;
            double pmayor = 0;
            double cif = 0;
            double porcentaje = 0;
            double ganancia = 0;
            double pvolumen = 0;
            double pcosto = 0;
            double cambiod = 0;
            codArt = Grid1.CurrentRow.Cells[1].Value.ToString();
            CIF = Grid1.CurrentRow.Cells[7].Value.ToString();
            PVENTA = Grid1.CurrentRow.Cells[9].Value.ToString();
            PMAYOR = Grid1.CurrentRow.Cells[8].Value.ToString();
            PVOLUMEN = Grid1.CurrentRow.Cells[10].Value.ToString();
            COSTO = Grid1.CurrentRow.Cells[3].Value.ToString();
            pcosto = double.Parse(COSTO.Equals("") ? "0" : COSTO);
            pventa = double.Parse(PVENTA.Equals("") ? "0" : PVENTA);
            pmayor = double.Parse(PMAYOR.Equals("") ? "0" : PMAYOR);
            cif = double.Parse(CIF.Equals("") ? "0" : CIF);
            pvolumen = double.Parse(PVOLUMEN.Equals("") ? "0" : PVOLUMEN);
            textBox17.Text = cif.ToString("0.0000");
            textBox11.Text = pventa.ToString("0.00");
            textBox7.Text = pmayor.ToString("0.00");
            textBox15.Text = pvolumen.ToString("0.00");
            cambiod = double.Parse(cambio.Equals("") ? "0" : cambio);
            textBox5.Text = (cif * cambiod).ToString("#.###");
            label5.Text = Grid1.CurrentRow.Cells[2].Value.ToString();
            label6.Text = Grid1.CurrentRow.Cells[6].Value.ToString();
            label5.Text = Grid1.CurrentRow.Cells[2].Value.ToString();
            textBox18.Text = cambio;
            StockArt = Grid1.CurrentRow.Cells[6].Value.ToString();
            stock = double.Parse(StockArt.Equals("") ? "0" : StockArt);

            if (stock <= 0)
            {
                label6.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
                label4.ForeColor = Color.Red;
                label18.ForeColor = Color.Red;
            }
            else
            {
                label6.ForeColor = Color.Green;
                label5.ForeColor = Color.Green;
                label4.ForeColor = Color.Green;
                label18.ForeColor = Color.Green;
            }

            pictureBox1.Image = null;
            if (pcosto.Equals("0"))
            {
                textBox6.Text = "";
                ganancia = pmayor;
                textBox12.Text = ganancia.ToString();
            }
            else
            {
                porcentaje = ((pmayor / pcosto) * 100) - 100;
                ganancia = pcosto * (porcentaje / 100);
                textBox6.Text = porcentaje.ToString("0.00");
                textBox12.Text = ganancia.ToString("0.0000");
            }
        }
        private void CalculaGanancia2()
        {
            var rowsCount = Grid1.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;
            string StockArt = "";
            double stock = 0;
            double pventa = 0;
            double pmayor = 0;
            double cif = 0;
            double porcentaje = 0;
            double ganancia = 0;
            double pvolumen = 0;
            double pcosto = 0;
            codArt = Grid1.CurrentRow.Cells[1].Value.ToString();
            CIF = Grid1.CurrentRow.Cells[7].Value.ToString();
            //PVENTA = Grid1.CurrentRow.Cells[9].Value.ToString().Equals("") ? "0":Grid1.CurrentRow.Cells[9].Value.ToString();
            PVENTA = Grid1.CurrentRow.Cells[9].Value.ToString();
            PMAYOR = Grid1.CurrentRow.Cells[8].Value.ToString();
            PVOLUMEN = Grid1.CurrentRow.Cells[10].Value.ToString();
            COSTO = Grid1.CurrentRow.Cells[3].Value.ToString();
            pcosto = double.Parse(COSTO.Equals("") ? "0" : COSTO);
            pventa = double.Parse(PVENTA.Equals("") ? "0" : PVENTA);
            pmayor = double.Parse(PMAYOR.Equals("") ? "0" : PMAYOR);
            cif = double.Parse(CIF.Equals("") ? "0" : CIF);
            pvolumen = double.Parse(PVOLUMEN.Equals("") ? "0" : PVOLUMEN);
            textBox17.Text = cif.ToString("0.0000");
            textBox11.Text = pventa.ToString("0.00");
            textBox7.Text = pmayor.ToString("0.00");
            textBox15.Text = pvolumen.ToString("0.00");
            textBox5.Text = Grid1.CurrentRow.Cells[3].Value.ToString();
            label5.Text = Grid1.CurrentRow.Cells[2].Value.ToString();
            label6.Text = Grid1.CurrentRow.Cells[6].Value.ToString();
            label5.Text = Grid1.CurrentRow.Cells[2].Value.ToString();
            textBox18.Text = (pcosto / cif).ToString("0.000");
            StockArt = Grid1.CurrentRow.Cells[6].Value.ToString();
            stock = double.Parse(StockArt.Equals("") ? "0" : StockArt);

            if (stock <= 0)
            {
                label6.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
                label4.ForeColor = Color.Red;
                label18.ForeColor = Color.Red;
            }
            else
            {
                label6.ForeColor = Color.Green;
                label5.ForeColor = Color.Green;
                label4.ForeColor = Color.Green;
                label18.ForeColor = Color.Green;
            }

            pictureBox1.Image = null;
            if (pcosto.Equals("0"))
            {
                textBox6.Text = "";
                ganancia = pmayor;
                textBox12.Text = ganancia.ToString();
            }
            else
            {
                porcentaje = ((pmayor / pcosto) * 100) - 100;
                ganancia = pcosto * (porcentaje / 100);
                textBox6.Text = porcentaje.ToString("0.00");
                textBox12.Text = ganancia.ToString("0.0000");
            }
        }
        private void Grid1_SelectionChanged(object sender, EventArgs e)
        {
            CheckActivo();
            LlenarCampos(codArt);

            if (codArt != "")
            {
                BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
                Codigo.IncludeLabel = true;
                ; pictureBox2.BackgroundImage = Codigo.Encode(BarcodeLib.TYPE.CODE128, codArt, Color.Black, Color.White, 170, 60);
            }

            CargaCompraVenta();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
            textBox12.Text = "";
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox12.Text = "";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void textBox12_Click(object sender, EventArgs e)
        {
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
        }

        private void textBox12_Validated(object sender, EventArgs e)
        {
        }

        private void textBox7_Validated(object sender, EventArgs e)
        {
        }

        private void textBox11_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox11.Text.ToString().Equals("") ? "0" : textBox11.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox11.Text = "";
                else
                    textBox11.Text = Net.ToString("0.00").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox11.Focus();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            nmPro = 'M';
            string Usuario = FrmLogin.x_login_usuario;
            string rucEmpresa = FrmLogin.x_RucEmpresa;
            string codAlmacen = FrmLogin.x_CodAlmacen;
            codArt = Grid1.CurrentRow.Cells[1].Value.ToString();
            ObjProductojefe.CodArt = codArt;
            ObjProductojefe.Almacen = codAlmacen;
            ObjProductojefe.RucEmpresa = rucEmpresa;
            ObjProductojefe.PVenta = double.Parse(textBox11.Text.ToString().Equals("") ? "0" : textBox11.Text.ToString().Trim());
            ObjProductojefe.PMayorista = double.Parse(textBox7.Text.ToString().Equals("") ? "0" : textBox7.Text.ToString().Trim());
            ObjProductojefe.PVolumen = double.Parse(textBox15.Text.ToString().Equals("") ? "0" : textBox15.Text.ToString().Trim());

            int nMes = 3;
            if (ObjProducto.PromedioVenta(codArt, rucEmpresa.ToString(), codAlmacen.ToString(), nMes))
            {
                ObjProductojefe.PromedioVenta = ObjProducto.CantidadProd.ToString();
                //textBox4.Text = ObjProducto.TotalProm.ToString();
            }

            if (FrmArticulos.nmPro == 'N')
            {
                if (ObjProductojefe.ValidarProductoJefe(codArt, rucEmpresa.ToString(), codAlmacen.ToString()))
                {
                    MessageBox.Show("Producto ya existe, ingrese otro Producto", "SISTEMA");
                    textBox1.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjProductojefe.ModificarJEFE())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e)
        {
            String vFecha1;
            String vFecha2;
            vFecha1 = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
            vFecha2 = DTP2.Value.Year.ToString() + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Day.ToString("00");

            if (ObjProducto.PromedioVentaFecha(codArt, rucEmpresa.ToString(), codAlmacen.ToString(), vFecha1.ToString(), vFecha2.ToString()))
            {
                textBox13.Text = ObjProducto.CantidadProd.ToString();
                textBox14.Text = ObjProducto.TotalProm.ToString();
            }
            else
            {
                textBox13.Text = "";
                textBox14.Text = "";
            }
        }

        private void Grid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void CalcularGanancia()
        {
            double Npmayor;
            double cifp;
            double porcentaje = 0;
            double ganancia = 0;
            double Nporcentaje = 0;
            double Npremay = 0;
            cifp = double.Parse(COSTO.Equals("") ? "0" : COSTO);
            if (textBox7.Text.Equals("NAN"))
            {
                Npmayor = 0;
            }
            else
            {
                Npmayor = double.Parse(textBox7.Text.Equals("") ? "0" : textBox7.Text);
            }
            
            
            Nporcentaje = double.Parse(textBox6.Text.Equals("") ? "0" : textBox6.Text);
            if (COSTO.Equals("0") || textBox6.Text.Equals(""))
            {
                porcentaje = ((Npmayor / cifp) * 100) - 100;
                ganancia = cifp * (porcentaje / 100);
                textBox6.Text = porcentaje.ToString("0.00");
                textBox12.Text = ganancia.ToString("0.00");
            }
            else
            {
                ganancia = cifp * Nporcentaje / 100;
                Npremay = cifp + ganancia;
                textBox7.Text = Npremay.ToString("0.00");
                textBox12.Text = ganancia.ToString("0.00");
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            CalcularGanancia();
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
                        
        }

        private void Grid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Grid1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            
        }

        private void ExportarGrillaExcel(DataGridView grd)
        {
            //Recorremos el DataGridView rellenando la hoja de trabajo
            Boolean nValSel = false;
            foreach (DataGridViewRow row in Grid1.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(Boolean.TrueString))
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
            //METODO QUE SOLO EXPORTA 1
            Excel.Application excel = new Excel.Application();
            Workbook vb = excel.Workbooks.Add(XlSheetType.xlWorksheet);
            Worksheet ws = (Worksheet)excel.ActiveSheet;
            excel.Visible = true;

            ws.Cells[1, 1] = "Codigo";
            ws.Cells[1, 2] = "Nombre";
            ws.Cells[1, 3] = "Venta Trimestral";
            ws.Cells[1, 4] = "Promedio Trimestral";
            ws.Cells[1, 5] = "STOCK";
            ws.Cells[1, 6] = "Precio CIF";
            ws.Cells[1, 7] = "Porcentaje de Ganancia";
            ws.Cells[1, 8] = "Monto de Ganancia";
            ws.Cells[1, 9] = "Precio por Mayor";
            ws.Cells[1, 10] = "Precio por Menor";

            ws.Cells[2, 1] = codArt;
            ws.Cells[2, 2] = label5.Text;
            ws.Cells[2, 3] = textBox4.Text;
            ws.Cells[2, 4] = textBox10.Text;
            ws.Cells[2, 5] = label6.Text;
            ws.Cells[2, 6] = textBox5.Text;
            ws.Cells[2, 7] = textBox6.Text;
            ws.Cells[2, 8] = textBox12.Text;
            ws.Cells[2, 9] = textBox7.Text;
            ws.Cells[2, 10] = textBox11.Text;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog archivo = new SaveFileDialog();
                archivo.Filter = "Excel (*.xls)|*.xls";
                archivo.FileName = label20.Text + DateTime.Now.Date.ToShortDateString().Replace('/', '-');
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
                    hojaDeTrabajo.Cells[1, "M"] = Grid1.Columns[13].HeaderText;
                    hojaDeTrabajo.Cells[1, "N"] = Grid1.Columns[14].HeaderText;
                    hojaDeTrabajo.Cells[1, "O"] = Grid1.Columns[15].HeaderText;
                    hojaDeTrabajo.Cells[1, "P"] = Grid1.Columns[16].HeaderText;
                    hojaDeTrabajo.Cells[1, "Q"] = Grid1.Columns[17].HeaderText;
                    hojaDeTrabajo.Cells[1, "R"] = Grid1.Columns[18].HeaderText;

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
                    hojaDeTrabajo.Columns[12].AutoFit();
                    hojaDeTrabajo.Columns[13].AutoFit();
                    hojaDeTrabajo.Columns[14].AutoFit();
                    hojaDeTrabajo.Columns[15].AutoFit();
                    hojaDeTrabajo.Columns[16].Autofit();
                    hojaDeTrabajo.Columns[17].Autofit();
                    hojaDeTrabajo.Columns[18].Autofit();

                    hojaDeTrabajo.Name = "PRODUCTOS";

                    //Recorremos el DataGridView rellenando la hoja de trabajo
                    for (int i = 0; i < Grid1.Rows.Count; i++)
                    {
                        for (int j = 0; j < Grid1.Columns.Count; j++)
                        {
                            if (Grid1.Rows[i].Cells[j].Value != null)
                            {
                                hojaDeTrabajo.Cells[i + 2, j] = Grid1.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                    }
                    libroDeTrabajo.SaveAs(archivo.FileName,
                        Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    libroDeTrabajo.Close(true);
                    aplicacion.Quit();
                    MessageBox.Show("PRODUCTOS EXPORTADOS", "NANO");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar la informacion debido a: " + ex.ToString());
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    DataGridViewCheckBoxCell cellSelecion = row.Cells["Select"] as DataGridViewCheckBoxCell;
                    cellSelecion.Value = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in Grid1.Rows)
                {
                    DataGridViewCheckBoxCell cellSelecion = row.Cells["Select"] as DataGridViewCheckBoxCell;
                    cellSelecion.Value = false;
                }
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
        }

        private void txtAutoexplicativo2_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string nnombre = txtAutoexplicativo2.Text.Trim();
                datos = csql.dataset("Call SpProductoBusNom('" + nnombre.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
            }
            else
            {
                CargarDatos();
            }
        }

        private void txtAutoexplicativo1_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string codigo = txtAutoexplicativo1.Text.Trim();
                datos = csql.dataset("Call SpProductoBusCodG('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
            }
            else
            {
                CargarDatos();
            }
        }

        private void txtAutoexplicativo3_TextChanged(object sender, EventArgs e)
        {
            string rucProv = txtAutoexplicativo3.Text.ToString().Trim();
            BusProveedor(rucProv, rucEmpresa);
        }

        private void txtAutoexplicativo2_Click(object sender, EventArgs e)
        {
            txtAutoexplicativo1.Text = "";
            txtAutoexplicativo3.Text = "";
            label20.Text = "";
        }

        private void txtAutoexplicativo1_Click(object sender, EventArgs e)
        {
            txtAutoexplicativo2.Text = "";
            txtAutoexplicativo3.Text = "";
            label20.Text = "";
        }

        private void txtAutoexplicativo3_Click(object sender, EventArgs e)
        {
            txtAutoexplicativo2.Text = "";
            txtAutoexplicativo1.Text = "";
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            System.Drawing.Image imgFinal = (System.Drawing.Image)pictureBox2.BackgroundImage.Clone();

            SaveFileDialog CajaDeDiaologoGuardar = new SaveFileDialog();
            CajaDeDiaologoGuardar.AddExtension = true;
            CajaDeDiaologoGuardar.FileName = label5.Text;
            CajaDeDiaologoGuardar.Filter = "Image PNG (*.png)|*.png";
            CajaDeDiaologoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(CajaDeDiaologoGuardar.FileName))
            {
                imgFinal.Save(CajaDeDiaologoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            nmPro = 'M';
            if (Grid1.RowCount > 0)
            {
                codArt = Grid1.CurrentRow.Cells[1].Value.ToString();
                FrmAddProducto frmAddProducto = new FrmAddProducto();
                frmAddProducto.WindowState = FormWindowState.Normal;
                frmAddProducto.MdiParent = this.MdiParent;
                frmAddProducto.Show();
            }
            else
            {
                MessageBox.Show("No existen Productos registrados", "SISTEMA");
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            nmPro = 'M';
            string rucEmpresa = FrmLogin.x_RucEmpresa;
            string codAlmacen = FrmLogin.x_CodAlmacen;
            
            ObjProductojefe.Almacen = codAlmacen;
            ObjProductojefe.RucEmpresa = rucEmpresa;

            int nIndex = 0;
            int contador = 0;
            foreach (DataGridViewRow row1 in Grid1.Rows)
            {
                
                nIndex = contador;

                int nMes = 3;
                if (ObjProducto.PromedioVenta(Grid1[1, nIndex].Value.ToString(), rucEmpresa.ToString(), codAlmacen.ToString(), nMes))
                {
                    ObjProductojefe.PromedioVenta = ObjProducto.CantidadProd.ToString();
                }
                if (FrmArticulos.nmPro == 'N')
                {
                    if (ObjProductojefe.ValidarProductoJefe(Grid1[1, nIndex].Value.ToString(), rucEmpresa.ToString(), codAlmacen.ToString()))
                    {
                        MessageBox.Show("Producto ya existe, ingrese otro Producto", "SISTEMA");
                        textBox1.Focus();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("No se registro correctamente", "SISTEMA");
                    }
                }
                else
                {
                    if (ObjProductojefe.ModificarPromedio(Grid1[1, nIndex].Value.ToString(), ObjProductojefe.PromedioVenta, rucEmpresa.ToString(), codAlmacen.ToString()))
                    {
                        //MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    }
                    else
                    {
                        MessageBox.Show("No se modificó correctamente", "SISTEMA");
                    }
                }

                contador += 1;
            }

            MessageBox.Show("Datos actualizados correctamente", "SISTEMA");
            
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }


        private void calculaCIF()
        {
            double tcambio = 0;
            double cif = 0;
            double pcosto = 0;

            pcosto = Convert.ToDouble(textBox5.Text);
            tcambio = Convert.ToDouble(textBox18.Text);

            cif = pcosto / tcambio;

            textBox17.Text = cif.ToString("#.##");

        }

        private void calcularCosto()
        {
            double tcambio = 0;
            double cif = 0;
            double pcosto = 0;

            cif = Convert.ToDouble(textBox17.Text);
            tcambio = Convert.ToDouble(textBox5.Text);

            pcosto = cif * tcambio;

            textBox18.Text = pcosto.ToString("#.##");
        }

        private void txtAutoexplicativo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
                char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string codigo = "S";
                datos = csql.dataset("Call SpProductoStockReal('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
            }
            else
            {
                CargarDatos();
            }
        }

        public void CargaCompraVenta()
        {
        }

        private void button15_Click(object sender, EventArgs e)
        {
            codArt = Grid1.CurrentRow.Cells[1].Value.ToString();
            String vFecha1;
            String vFecha2;
            vFecha1 = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
            vFecha2 = DTP2.Value.Year.ToString() + "/" + DTP2.Value.Month.ToString("00") + "/" + DTP2.Value.Day.ToString("00");
            datos = csql.dataset("Call SpProductoCompraVenta('" + codArt.ToString() + "', '" + vFecha1.ToString() + "','" + vFecha2.ToString() + "', '" + rucEmpresa.ToString() + "')");
            Grid2.DataSource = datos.Tables[0];
            Grilla2();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string TipoMov;
            FrmCompras.nmCom = 'M';
            FrmVentas.nmVen = 'M';
            TipoMov = Grid2.CurrentRow.Cells[3].Value.ToString();
            if (Grid2.RowCount > 0)
            {
                if (TipoMov == "COMPRA")
                {
                    FrmCompras.almacenSelec = "001";
                    FrmCompras.nIdCompra = Grid2.CurrentRow.Cells[1].Value.ToString();
                    FrmAddCompra frmAddCompra = new FrmAddCompra();
                    frmAddCompra.WindowState = FormWindowState.Normal;
                    frmAddCompra.ShowDialog(this);
                }
                else
                {
                    FrmVentas.almacenSelec = "001";
                    FrmVentas.nIdVenta = Grid2.CurrentRow.Cells[1].Value.ToString();
                    FrmAddVenta frmAddventa = new FrmAddVenta();
                    frmAddventa.WindowState = FormWindowState.Normal;
                    frmAddventa.ShowDialog(this);
                }
            }
        }

        private void Grid1_DockChanged(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                if (ObjTipoCambio.BuscarTipoCambio())
                {
                    double tCambio = ObjTipoCambio.Valor;
                    textBox18.Text = tCambio.ToString("#.###").Trim();

                }
                else
                {

                }
                CalculaGananciaTipoCambio(textBox18.Text);
                CalcularGanancia();

            }
            else
            {
                CalculaGanancia2();
                CalcularGanancia();
            }
        }

        private void CheckActivo()
        {
            if (checkBox3.Checked == true)
            {
                if (ObjTipoCambio.BuscarTipoCambio())
                {
                    double tCambio = ObjTipoCambio.Valor;
                    textBox18.Text = tCambio.ToString("###,##0.000").Trim();
                }
                else
                {

                }
                CalculaGananciaTipoCambio(textBox18.Text);
                CalcularGanancia();

            }
            else
            {
                CalculaGanancia2();
                CalcularGanancia();
            }
        }

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string nnombre = comboBox5.Text.ToString();
                datos = csql.dataset("Call SpArticuloubicacion_2('" + nnombre.ToString() + "', '" + codAlmacen.ToString() + "','" + rucEmpresa.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
            }
            else
            {
                CargarDatos();
            }
        }

        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string nnombre = comboBox5.Text.ToString();
                datos = csql.dataset("Call SpArticuloubicacion_2('" + nnombre.ToString() + "', '" +  codAlmacen.ToString() + "','" + rucEmpresa.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
            }
            else
            {
                CargarDatos();
            }
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        { 
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            nmPro = 'N';
            frmVistaCodBarra frmVistaCodBarra = new frmVistaCodBarra();
            frmVistaCodBarra.WindowState = FormWindowState.Normal;
            frmVistaCodBarra.MdiParent = this.MdiParent;
            frmVistaCodBarra.Show();
        }
    }
}