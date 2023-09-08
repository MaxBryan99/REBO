using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAlmacenStock : Form
    {
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string codArti = "";
        private DataSet datos;

        public FrmAlmacenStock()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Almacén";
            Grid1.Columns[1].HeaderText = "Stock";
            Grid1.Columns[0].Width = 350;
            Grid1.Columns[1].Width = 75;
            Grid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[1].DefaultCellStyle.Format = "###,##0.00";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAlmacenStock_Load(object sender, EventArgs e)
        {
            codArti = FrmBusArticulo.codArt;
            label2.Text = FrmBusArticulo.nomArt;
            datos = csql.dataset("Call SpProductoStAlmGen('" + codArti.ToString() + "','" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }
    }
}