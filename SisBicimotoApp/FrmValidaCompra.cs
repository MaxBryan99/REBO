using SisBicimotoApp.Clases;
using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmValidaCompra : Form
    {
        public ICompra Opener { get; set; }
        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string nomAlmacen = FrmLogin.x_NomAlmacen;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        private string usuario = FrmLogin.x_login_usuario;
        private string valIdCompra = "";
        private ClsCompra ObjCompra = new ClsCompra();

        public FrmValidaCompra()
        {
            InitializeComponent();
            valIdCompra = FrmCompras.nIdCompra.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmValidaCompra_Load(object sender, EventArgs e)
        {
            label6.Text = FrmCompras.Doc.ToString();
            textBox1.Text = usuario.ToString();
            textBox1.SelectionStart = 0;
            textBox1.SelectionLength = textBox1.TextLength;
            textBox1.Focus();
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
                    if (MessageBox.Show("¿Está seguro de querer ANULAR el registro de compra?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    if (ObjCompra.VerificaDetalle(valIdCompra, codAlmacen, rucEmpresa))
                    {
                        MessageBox.Show("No se puede Anular la compra seleccionada, intente modificar, verificar", "SISTEMA");
                        return;
                    }
                    else
                    {
                        //ICompra parent = this.Owner as ICompra;

                        //parent.CargarConsulta(val);

                        if (ObjCompra.Anular(valIdCompra, codAlmacen, rucEmpresa, textBox1.Text.ToString().Trim()))
                        {
                            MessageBox.Show("Datos Anulados Correctamente", "SISTEMA");
                            //ICompra parent = this.Owner as ICompra;
                            //string val = "V";
                            //parent.CargarConsulta(val);
                            //Opener.CargarConsulta(val);

                            /*ICompra miInterfaz = this.Owner as ICompra;
                            if (miInterfaz != null)
                                miInterfaz.CargarConsulta(val);*/
                            Opener.CargarConsulta(val);

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("El proceso, no se realizó correctamente, verificar", "SISTEMA");
                        }
                    }
                }

                if (radioButton2.Checked == true)
                {
                    if (MessageBox.Show("¿Está seguro de querer ELIMINAR el registro de compra?", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                    if (ObjCompra.VerificaDetalle(valIdCompra, codAlmacen, rucEmpresa))
                    {
                        MessageBox.Show("No se puede Eliminar la compra seleccionada, intente modificar, verificar", "SISTEMA");
                    }
                    else
                    {
                        if (ObjCompra.Eliminar(valIdCompra, codAlmacen, rucEmpresa, textBox1.Text.ToString().Trim()))
                        {
                            MessageBox.Show("Datos Eliminados Correctamente", "SISTEMA");
                            Opener.CargarConsulta(val);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("El proceso, no se realizó correctamente, verificar", "SISTEMA");
                        }
                    }
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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}