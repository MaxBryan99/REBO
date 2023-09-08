using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddDesCatalogo : Form
    {
        private DataSet datos;
        private string codCat = "";
        private string codItem = "";
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();

        public FrmAddDesCatalogo()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Código";
            Grid1.Columns[1].HeaderText = "Itém Catálogo";
            Grid1.Columns[2].HeaderText = "Descripción Corta";
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 350;
            Grid1.Columns[2].Visible = false;
        }

        public void CargarDatos()
        {
            string cod = FrmMantCatalogo.codCat.ToString().Trim();
            datos = csql.dataset("Call SpDetCatalogoBusGen('" + cod.ToString().Trim() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddDesCatalogo_Load(object sender, EventArgs e)
        {
            tabPage2.Enabled = false;
            label1.Text = FrmMantCatalogo.NomCat.ToString().Trim();
            codCat = FrmMantCatalogo.codCat.ToString().Trim();
            CargarDatos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "Registrar Item";
            tabControl1.SelectedIndex = 1;
            tabPage2.Enabled = true;
            codItem = "";
            textBox2.Text = "";
            textBox1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            label2.Text = "Registrar Item";
            textBox1.Text = "";
            codItem = "";
            tabPage2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                label2.Text = "Modificar Item";
                textBox1.Text = Grid1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = Grid1.CurrentRow.Cells[2].Value.ToString();
                codItem = Grid1.CurrentRow.Cells[0].Value.ToString();
                tabPage2.Enabled = true;
                tabControl1.SelectedIndex = 1;
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("No existen Items registrados", "SISTEMA");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                label2.Text = "Registrar Item";
                textBox1.Text = "";
                codItem = "";
                tabPage2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Descripción", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Descripción corta", "SISTEMA");
                textBox2.Focus();
                return;
            }
            string Usuario = FrmLogin.x_login_usuario;
            ObjDetCatalogo.CodCatalogo = codCat.ToString().Trim();
            ObjDetCatalogo.CodDetCat = codItem.ToString().Trim();
            ObjDetCatalogo.Descripcion = textBox1.Text.Trim();
            ObjDetCatalogo.DescCorta = textBox2.Text.Trim();
            ObjDetCatalogo.UserCreacion = Usuario.ToString().Trim();
            ObjDetCatalogo.UserModi = Usuario.ToString().Trim();

            if (label2.Text.Equals("Registrar Item"))
            {
                if (ObjDetCatalogo.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    //this.Close();
                    CargarDatos();
                    tabControl1.SelectedIndex = 0;
                    label2.Text = "Registrar Item";
                    textBox1.Text = "";
                    codItem = "";
                    tabPage2.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjDetCatalogo.Modificar())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    //this.Close();
                    CargarDatos();
                    tabControl1.SelectedIndex = 0;
                    label2.Text = "Registrar Item";
                    textBox1.Text = "";
                    codItem = "";
                    tabPage2.Enabled = false;
                }
                else
                {
                    MessageBox.Show("No se actualizó correctamente", "SISTEMA");
                }
            }

            tabControl1.SelectedIndex = 0;
            label2.Text = "Registrar Item";
            textBox1.Text = "";
            codItem = "";
            tabPage2.Enabled = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button3.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Grid1.RowCount > 0)
            {
                codItem = Grid1.CurrentRow.Cells[0].Value.ToString();
                string nDescripcion = Grid1.CurrentRow.Cells[1].Value.ToString();
                if (MessageBox.Show("¿Está seguro de querer eliminar la descripción: " + nDescripcion + "?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    string Usuario = FrmLogin.x_login_usuario;
                    ObjDetCatalogo.CodCatalogo = codCat.ToString().Trim();
                    ObjDetCatalogo.CodDetCat = codItem.ToString();
                    ObjDetCatalogo.UserModi = Usuario.ToString().Trim();
                    if (ObjDetCatalogo.Eliminar())
                    {
                        MessageBox.Show("Item eliminado", "SISTEMA");
                        CargarDatos();
                        label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar", "SISTEMA");
                    }
                }
            }
        }
    }
}