using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddLinea : Form, IFamilia
    {
        public IFamilia Opener { get; set; }

        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private ClsLinea ObjLinea = new ClsLinea();
        private string Cod = "";
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmAddLinea()
        {
            InitializeComponent();

            Cod = FrmLinea.cod.ToString();

            if (FrmLinea.nmLinea == 'M')
            {
                LlenarCampos(Cod);
            }
        }

        #region IFamilia Members

        public void SelectItem(string codFam)
        {
            textBox1.Text = codFam;
        }

        #endregion IFamilia Members

        private void LlenarCampos(string InCod)
        {
            try
            {
                if (ObjLinea.BuscarLinea(InCod, rucEmpresa.ToString()))
                {
                    textBox1.Text = ObjLinea.CodFamilia.ToString().Trim();
                    textBox2.Text = ObjLinea.Descripcion.ToString().Trim();
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

        private void BusFamilia(string vcodcat, string vcoddetcat)
        {
            if (ObjDetCatalogo.BuscarDetCatalogoCod(vcodcat, vcoddetcat))
            {
                label2.Text = ObjDetCatalogo.Descripcion.ToString().Trim();
            }
            else
            {
                label2.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddLinea_Load(object sender, EventArgs e)
        {
            if (FrmLinea.nmLinea == 'N')
            {
                this.Text = "Registrar Lineas de Artículos";
            }
            else
            {
                this.Text = "Modificar Línea de Artículo";
                textBox1.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Familia de Artículo", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Descripción de Línea", "SISTEMA");
                textBox2.Focus();
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            ObjLinea.Codigo = Cod.ToString();
            ObjLinea.CodFamilia = textBox1.Text.Trim();
            ObjLinea.Descripcion = textBox2.Text.Trim();
            ObjLinea.UserCreacion = Usuario.ToString().Trim();
            ObjLinea.UserModi = Usuario.ToString().Trim();
            ObjLinea.RucEmpresa = rucEmpresa.ToString();
            if (FrmLinea.nmLinea == 'N')
            {
                if (ObjLinea.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjLinea.Modificar())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string CodDetCat = textBox1.Text.ToString().Trim();
            string CodCat = "008";
            BusFamilia(CodCat, CodDetCat);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBusFamilia frmBusFamilia = new FrmBusFamilia();
            frmBusFamilia.WindowState = FormWindowState.Normal;
            frmBusFamilia.Opener = this;
            frmBusFamilia.MdiParent = this.MdiParent;
            frmBusFamilia.Show();
        }
    }
}