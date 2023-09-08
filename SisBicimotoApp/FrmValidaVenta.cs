using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmValidaVenta : Form
    {
        public IVenta Opener { get; set; }
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string usuario = FrmLogin.x_login_usuario;
        private string valIdVenta = "";
        private ClsVenta ObjVenta = new ClsVenta();

        public FrmValidaVenta()
        {
            InitializeComponent();
            valIdVenta = FrmVentas.nIdVenta.ToString();
        }

        private void FrmValidaVenta_Load(object sender, EventArgs e)
        {
            label6.Text = FrmVentas.Doc.ToString();
            if (FrmVentas.EstDoc.Equals("N"))
            {
                radioButton1.Visible = false;
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Visible = true;
                radioButton1.Checked = true;
            }
            textBox1.Text = usuario.ToString();
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.TextLength;
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.SelectionStart = 0;
                textBox2.SelectionLength = textBox2.TextLength;
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.SelectionStart = 0;
                textBox2.SelectionLength = textBox2.TextLength;
                button1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string val = "V";
            object[] parametro = new object[2];
            parametro[0] = textBox1.Text;
            parametro[1] = textBox2.Text;
            DataSet datos = csql.dataset_cadena("Call SpUsuarioValUser('" + parametro[0] + "','" + parametro[1] + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                if (radioButton1.Checked == true)
                {
                    if (MessageBox.Show("¿Está seguro de querer ANULAR el registro de venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    /*if (ObjCompra.VerificaDetalle(valIdCompra, codAlmacen, rucEmpresa))
                    {
                        MessageBox.Show("No se puede Anular la compra seleccionada, intente modificar, verificar", "SISTEMA");
                        return;
                    }
                    else
                    {*/

                    if (ObjVenta.Anular(valIdVenta, codAlmacen, rucEmpresa, textBox1.Text.ToString().Trim()))
                    {
                        MessageBox.Show("Datos Anulados Correctamente", "SISTEMA");
                        Opener.CargarConsulta(val);

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El proceso, no se realizó correctamente, verificar", "SISTEMA");
                    }
                    //}
                }

                if (radioButton2.Checked == true)
                {
                    if (MessageBox.Show("¿Está seguro de querer ELIMINAR el registro de venta?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    /*if (ObjCompra.VerificaDetalle(valIdCompra, codAlmacen, rucEmpresa))
                    {
                        MessageBox.Show("No se puede Eliminar la compra seleccionada, intente modificar, verificar", "SISTEMA");
                    }
                    else
                    {*/
                    if (ObjVenta.Eliminar(valIdVenta, codAlmacen, rucEmpresa, textBox1.Text.ToString().Trim()))
                    {
                        MessageBox.Show("Datos Eliminados Correctamente", "SISTEMA");
                        Opener.CargarConsulta(val);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El proceso, no se realizó correctamente, verificar", "SISTEMA");
                    }
                    //}
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "SISTEMA");
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = textBox1.TextLength;
                textBox1.Focus();
            }
        }
    }
}