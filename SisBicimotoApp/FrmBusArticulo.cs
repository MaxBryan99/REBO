using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmBusArticulo : Form
    {
        public IArticulo Opener { get; set; }

        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private ClsProducto ObjProducto = new ClsProducto();
        public static string codArt = "";
        public static string nomArt = "";
        public static string StockArt = "";

        public FrmBusArticulo()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Nombre Producto";
            Grid1.Columns[2].HeaderText = "CodInternac";
            Grid1.Columns[3].HeaderText = "TProducto";
            Grid1.Columns[4].HeaderText = "Marca";
            Grid1.Columns[5].HeaderText = "Modelo";
            Grid1.Columns[6].HeaderText = "Familia";
            Grid1.Columns[7].HeaderText = "Linea";
            Grid1.Columns[8].HeaderText = "Proc.";
            Grid1.Columns[9].HeaderText = "U.M.";
            Grid1.Columns[10].HeaderText = "Moneda";
            Grid1.Columns[11].HeaderText = "GenStock";
            Grid1.Columns[12].HeaderText = "P.Costo";
            Grid1.Columns[13].HeaderText = "P.Venta";
            Grid1.Columns[14].HeaderText = "P.May.";
            Grid1.Columns[15].HeaderText = "P.Vol.";
            Grid1.Columns[16].HeaderText = "STMin";
            Grid1.Columns[17].HeaderText = "Ubicacion";
            Grid1.Columns[18].HeaderText = "Proveedor";
            Grid1.Columns[19].HeaderText = "TSerie";
            Grid1.Columns[20].HeaderText = "IsVehiculo";
            Grid1.Columns[21].HeaderText = "Descripcion";
            //Grid1.Columns[22].HeaderText = "Image";
            Grid1.Columns[22].HeaderText = "Estado";
            Grid1.Columns[23].HeaderText = "Stock";
            Grid1.Columns[24].HeaderText = "Stock Real";
            Grid1.Columns[0].Width = 55;
            Grid1.Columns[1].Width = 340;
            Grid1.Columns[2].Width = 300;
            Grid1.Columns[3].Width = 300;
            Grid1.Columns[4].Width = 70;
            Grid1.Columns[5].Width = 85;
            Grid1.Columns[6].Width = 200;
            Grid1.Columns[7].Width = 200;
            Grid1.Columns[8].Width = 45;
            Grid1.Columns[9].Width = 55;
            Grid1.Columns[12].Width = 55;
            Grid1.Columns[13].Width = 55;
            Grid1.Columns[14].Width = 55;
            Grid1.Columns[15].Width = 55;
            Grid1.Columns[24].Width = 75;
            Grid1.Columns[2].Visible = false;
            Grid1.Columns[3].Visible = false;
            Grid1.Columns[4].Visible = false;
            Grid1.Columns[5].Visible = false;
            Grid1.Columns[6].Visible = false;
            Grid1.Columns[7].Visible = false;
            Grid1.Columns[8].Visible = false;
            Grid1.Columns[10].Visible = false;
            Grid1.Columns[11].Visible = false;
            Grid1.Columns[12].Visible = false;
            Grid1.Columns[16].Visible = false;
            Grid1.Columns[17].Visible = false;
            Grid1.Columns[18].Visible = false;
            Grid1.Columns[19].Visible = false;
            Grid1.Columns[20].Visible = false;
            Grid1.Columns[21].Visible = false;
            Grid1.Columns[22].Visible = false;
            Grid1.Columns[23].Visible = false;
            //Grid1.Columns[24].Visible = false;
            Grid1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            /*Grid1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;*/
            Grid1.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[24].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[12].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[13].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[14].DefaultCellStyle.Format = "###,##0.00";
            Grid1.Columns[15].DefaultCellStyle.Format = "###,##0.00";
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
            label28.Text = "Total Productos: " + Grid1.RowCount;
        }

        private void ColorStock()
        {
            
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpProductoBusConsulta('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");

            Grid1.DataSource = datos.Tables[0];
            ColorStock();
            Grilla();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmBusArticulo_Load(object sender, EventArgs e)
        {
            CargarDatos();
            ColorStock();
            label2.Text = nomAlmacen.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                string nnombre = textBox2.Text.Trim();
                datos = csql.dataset("Call SpProductoBusConsultaNomSt('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "','" + nnombre.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                ColorStock();
            }
            else
            {
                string nnombre = textBox2.Text.Trim();
                datos = csql.dataset("Call SpProductoBusConsultaNom('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "','" + nnombre.ToString() + "')");
                Grid1.DataSource = datos.Tables[0];
                Grilla();
                ColorStock();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Grid1.Focus();
            }
        }

        private void Grid1_DoubleClick(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                codArt = Grid1.CurrentRow.Cells[0].Value.ToString();
                this.Opener.SelectItem(codArt);
                this.Close();
            }
        }

        private void Grid1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                codArt = Grid1.CurrentRow.Cells[0].Value.ToString();
                this.Opener.SelectItem(codArt);
                this.Close();
            }

            if (e.KeyCode == Keys.F2)
            {
                codArt = Grid1.CurrentRow.Cells[0].Value.ToString();
                nomArt = Grid1.CurrentRow.Cells[1].Value.ToString();
                FrmAlmacenStock frmAlmacenStock = new FrmAlmacenStock();
                frmAlmacenStock.WindowState = FormWindowState.Normal;
                frmAlmacenStock.ShowDialog(this);
            }

            if (e.KeyCode == Keys.F1)
            {
                //checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
                if (checkBox1.Checked == true)
                {
                    checkBox1.Checked = false;
                    datos = csql.dataset("Call SpProductoBusConsulta('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    ColorStock();
                }
                else
                {
                    checkBox1.Checked = true;
                    datos = csql.dataset("Call SpProductoBusConsultaSt('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    ColorStock();
                }
            }

            if (e.KeyCode == Keys.F3)
            {
                textBox2.Focus();
            }
        }

        private void Grid1_SelectionChanged(object sender, EventArgs e)
        {
            var rowsCount = Grid1.SelectedRows.Count;
            if (rowsCount == 0 || rowsCount > 1) return;

            codArt = Grid1.CurrentRow.Cells[0].Value.ToString();
            StockArt = Grid1.CurrentRow.Cells[24].Value.ToString();
            int stock = Convert.ToInt32(StockArt);
            if (stock <= 0 )
            {
                label6.ForeColor = Color.Red;
                label5.ForeColor = Color.Red;
                label4.ForeColor = Color.Red;
                label3.ForeColor = Color.Red;
                label2.ForeColor = Color.Red;
            }
            else
            {
                label6.ForeColor = Color.Green;
                label5.ForeColor = Color.Green;
                label4.ForeColor = Color.Green;
                label3.ForeColor = Color.Green;
                label2.ForeColor = Color.Green;
            }
            label4.Text = Grid1.CurrentRow.Cells[23].Value.ToString();
            label2.Text = Grid1.CurrentRow.Cells[1].Value.ToString();
            label6.Text = nomAlmacen;
            LlenarCampos(codArt);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                codArt = Grid1.CurrentRow.Cells[0].Value.ToString();
                nomArt = Grid1.CurrentRow.Cells[1].Value.ToString();
                FrmAlmacenStock frmAlmacenStock = new FrmAlmacenStock();
                frmAlmacenStock.WindowState = FormWindowState.Normal;
                frmAlmacenStock.ShowDialog(this);
            }

            if (e.KeyCode == Keys.F1)
            {
                if (checkBox1.Checked == true)
                {
                    checkBox1.Checked = false;
                    datos = csql.dataset("Call SpProductoBusConsulta('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    ColorStock();
                }
                else
                {
                    checkBox1.Checked = true;
                    datos = csql.dataset("Call SpProductoBusConsultaSt('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                    Grid1.DataSource = datos.Tables[0];
                    Grilla();
                    ColorStock();
                }
            }

            if (e.KeyCode == Keys.F3)
            {
                Grid1.Focus();
            }

            if (e.KeyCode == Keys.Down)
            {
                Grid1.Focus();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
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
                //MessageBox.Show("No se puede cargar datos: " + ex.Message);
            }
        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}