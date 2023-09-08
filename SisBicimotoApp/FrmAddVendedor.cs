using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddVendedor : Form, IPersonal
    {
        public IForm Opener { get; set; }

        private ClsPersonal ObjPersonal = new ClsPersonal();
        private ClsVendedor ObjVendedor = new ClsVendedor();
        private ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        private string rucEmpresa = FrmLogin.x_RucEmpresa;

        public FrmAddVendedor()
        {
            InitializeComponent();

            string Cod = FrmVendedor.cod.ToString();
            if (FrmVendedor.nmVend == 'M')
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
                if (ObjVendedor.BuscarVendedor(InCod, rucEmpresa.ToString()))
                {
                    textBox1.Text = ObjVendedor.CodVend.ToString().Trim();
                    textBox5.Text = ObjVendedor.NomUser.ToString().Trim();
                    textBox6.Text = ObjVendedor.Pass.ToString().Trim();
                    comboBox1.Text = ObjVendedor.Zona.ToString().Trim();
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

        private void BusVendedor(string vcod)
        {
            if (ObjPersonal.BuscarPersonal(vcod, rucEmpresa.ToString()))
            {
                textBox2.Text = ObjPersonal.Nombre.ToString().Trim();
                textBox3.Text = ObjPersonal.Cargo.ToString().Trim();
                textBox4.Text = ObjPersonal.Area.ToString().Trim();
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
            //Carga Zona
            string codCatTipDoc = "007";
            string tipPresDoc = "1";
            DataSet datos = csql.dataset_cadena("Call SpCargarDetCat('" + codCatTipDoc + "','" + tipPresDoc + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                comboBox1.Items.Add("");
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    comboBox1.Items.Add(fila[1].ToString());
                }
            }

            if (FrmVendedor.nmVend == 'N')
            {
                this.Text = "Registrar Vendedor";
                button5.Visible = true;
            }
            else
            {
                this.Text = "Modificar Vendedor";
                comboBox1.Text = ObjVendedor.Zona.ToString().Trim();
                textBox1.Enabled = false;
                button5.Visible = false;
                textBox5.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string Cod = textBox1.Text.ToString().Trim();
            BusVendedor(Cod);
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
                textBox5.Focus();
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox6.Focus();
            }
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
            if (textBox5.TextLength == 0)
            {
                MessageBox.Show("Ingrese usuario de venta para el vendedor " + textBox2.Text.ToString(), "SISTEMA");
                textBox5.SelectionStart = 0;
                textBox5.SelectionLength = textBox2.TextLength;
                textBox5.Focus();
                return;
            }

            if (textBox6.TextLength == 0)
            {
                MessageBox.Show("Ingrese contraseña de venta para el vendedor " + textBox2.Text.ToString(), "SISTEMA");
                textBox6.SelectionStart = 0;
                textBox6.SelectionLength = textBox2.TextLength;
                textBox6.Focus();
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;
            ObjVendedor.CodVend = textBox1.Text.Trim();
            ObjDetCatalogo.BuscarDetCatalogoDes("007", comboBox1.Text.Trim(), "1");
            ObjVendedor.Zona = comboBox1.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            ObjVendedor.NomUser = textBox5.Text.Trim();
            ObjVendedor.Pass = textBox6.Text.Trim();
            ObjVendedor.UserCreacion = Usuario.ToString().Trim();
            ObjVendedor.UserModi = Usuario.ToString().Trim();
            ObjVendedor.RucEmpresa = rucEmpresa.ToString();
            if (FrmVendedor.nmVend == 'N')
            {
                if (ObjVendedor.ValidarVendedor(textBox1.Text.Trim(), rucEmpresa.ToString()))
                {
                    MessageBox.Show("Vendedor ya se encuentra registrado, por favor verifique", "SISTEMA");
                    textBox1.SelectionStart = 0;
                    textBox1.SelectionLength = textBox2.TextLength;
                    textBox1.Focus();
                    return;
                }

                if (ObjVendedor.Crear())
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
                if (ObjVendedor.Modificar())
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