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
    public partial class frmVistaCodBarra : Form
    {
        public static char nmPro = 'N';
        private DataSet datos;
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;

        public frmVistaCodBarra()
        {
            InitializeComponent();
        }
        public void CargarDatos()
        {
            datos = csql.dataset("Call SpProductosCodBarras('" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }
        public void Grilla()
        {
            Grid1.Columns[1].HeaderText = "Código";
            Grid1.Columns[2].HeaderText = "Cod. Barras";
            Grid1.Columns[3].HeaderText = "Nombre Producto";
            Grid1.Columns[4].HeaderText = "Cant. Cod.";
            
            Grid1.Columns[1].Width = 80;
            Grid1.Columns[2].Width = 150;
            Grid1.Columns[3].Width = 230;
            Grid1.Columns[4].Width = 80;

            Grid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            Grid1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmVistaCodBarra_Load(object sender, EventArgs e)
        {
            //llenar combobox1
            comboBox1.Items.Add("");
            comboBox1.Items.Add("Cod.Art");
            comboBox1.Items.Add("Cod.Barra");
            comboBox1.Items.Add("Nombre");

            CargarDatos();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAutoexplicativo2_TextChanged(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            if (comboBox1.SelectedItem == null)
            {
                CargarDatos();
            }
            else
            {
                if (selectedIndex.Equals(1))
                {
                    string codigo = txtAutoexplicativo2.Text.Trim();
                    if (codigo.Trim().Length < 7)
                    {
                        datos = csql.dataset("Call SpBuscarCodArt('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                    }
                }
                if (selectedIndex.Equals(3))
                {
                    string nombre = txtAutoexplicativo2.Text.Trim();
                    if (nombre.Trim().Length < 50)
                    {
                        datos = csql.dataset("Call SpBuscarNombre('" + nombre.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                    }
                }
                if (selectedIndex.Equals(2))
                {
                    string codbarra = txtAutoexplicativo2.Text.Trim();
                    if (codbarra.Trim().Length < 14)
                    {
                        datos = csql.dataset("Call SpBuscarCodBarra('" + codbarra.ToString() + "','" + rucEmpresa.ToString() + "','" + codAlmacen.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                    }
                }
            }
        }

        private void txtAutoexplicativo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            if (selectedIndex.Equals(1))
            {
                //Valida numeros
                if (char.IsLetter(e.KeyChar) || //Letras
                char.IsSymbol(e.KeyChar) || //Símbolos
                char.IsWhiteSpace(e.KeyChar) || //Espaçio
                    char.IsPunctuation(e.KeyChar)) //Pontuacion
                    e.Handled = true;
            }
            if (selectedIndex.Equals(2))
            {
                //Valida numeros
                if (char.IsLetter(e.KeyChar) || //Letras
                char.IsSymbol(e.KeyChar) || //Símbolos
                char.IsWhiteSpace(e.KeyChar) || //Espaçio
                    char.IsPunctuation(e.KeyChar)) //Pontuacion
                    e.Handled = true;
            }
        }
    }
}
