using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddResponsable : Form, IPersonal
    {
        public IForm Opener { get; set; }

        private ClsPersonal ObjPersonal = new ClsPersonal();
        private ClsResponsable ObjResponsable = new ClsResponsable();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string Almacen = FrmLogin.x_CodAlmacen;

        public FrmAddResponsable()
        {
            InitializeComponent();

            string Cod = FrmResponsable.cod.ToString();
            if (FrmResponsable.nmResp == 'M')
            {
                LlenarCampos(Cod);
            }
        }

        #region IPersonal Members

        public void SelectItem(string codVend)
        {
            textBox1.Text = codVend;
        }

        #endregion IPersonal Members

        #region IVendedor Members

        public void SelectItemVend(string codVend)
        {
            string valor = codVend;
        }

        #endregion IVendedor Members

        private void LlenarCampos(string InCod)
        {
            try
            {
                if (ObjResponsable.BuscarResponsable(InCod, Almacen, rucEmpresa.ToString()))
                {
                    textBox1.Text = ObjResponsable.Codigo.ToString().Trim();
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

        private void BusResponsable(string vcod)
        {
            if (ObjPersonal.BuscarPersonal(vcod, rucEmpresa.ToString()))
            {
                textBox2.Text = ObjPersonal.Nombre.ToString().Trim();
                textBox3.Text = ObjPersonal.Cargo.ToString().Trim();
                textBox4.Text = ObjPersonal.Direccion.ToString().Trim();
            }
            else
            {
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddVendedor_Load(object sender, EventArgs e)
        {
            if (FrmResponsable.nmResp == 'N')
            {
                this.Text = "Registrar Responsable";
                button5.Visible = true;
            }
            else
            {
                this.Text = "Modificar Responsable";
                textBox1.Enabled = false;
                button5.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string Cod = textBox1.Text.ToString().Trim();
            BusResponsable(Cod);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button2.Focus();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBusPersonal frmBusPersonal = new FrmBusPersonal();
            frmBusPersonal.WindowState = FormWindowState.Normal;
            frmBusPersonal.Opener = this;
            frmBusPersonal.MdiParent = this.MdiParent;
            frmBusPersonal.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Usuario = FrmLogin.x_login_usuario;
            ObjResponsable.Codigo = textBox1.Text.Trim();
            ObjResponsable.UserCreacion = Usuario.ToString().Trim();
            ObjResponsable.UserModi = Usuario.ToString().Trim();
            ObjResponsable.Almacen = Almacen.ToString();
            ObjResponsable.RucEmpresa = rucEmpresa.ToString();
            if (FrmResponsable.nmResp == 'N')
            {
                if (ObjResponsable.ValidarResponsable(textBox1.Text.Trim(), Almacen.ToString(), rucEmpresa.ToString()))
                {
                    MessageBox.Show("Responsable ya se encuentra registrado, por favor verifique", "SISTEMA");
                    textBox1.SelectionStart = 0;
                    textBox1.SelectionLength = textBox2.TextLength;
                    textBox1.Focus();
                    return;
                }

                if (ObjResponsable.Crear())
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
                if (ObjResponsable.Modificar())
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
        }
    }
}