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
    public partial class FrmResumenStock : Form
    {
        public FrmResumenStock()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Nombre";
            Grid1.Columns[2].HeaderText = "Precio Venta";
            Grid1.Columns[3].HeaderText = "Precio Compra";
            Grid1.Columns[4].HeaderText = "Stock Articulo";
            Grid1.Columns[5].HeaderText = "Stock Cod. Barra";
            Grid1.Columns[6].HeaderText = "Stock Vendido";

            Grid1.Columns[0].Width = 40;
            Grid1.Columns[1].Width = 200;
            Grid1.Columns[2].Width = 80;
            Grid1.Columns[3].Width = 80;
            Grid1.Columns[4].Width = 80;
            Grid1.Columns[5].Width = 80;
            Grid1.Columns[6].Width = 80;

            Grid1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[2].DefaultCellStyle.Format = "###,##0.0000";
            Grid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[3].DefaultCellStyle.Format = "###,##0.0000";
            Grid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            Grid1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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

        private void FrmResumenStock_Load(object sender, EventArgs e)
        {
            Grilla();
        }
    }
}
