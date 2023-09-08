using SisBicimotoApp.Interface;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmValidarVenta : Form
    {
        public IVenta Opener { get; set; }

        public static string userVenta = "";

        public FrmValidarVenta()
        {
            InitializeComponent();
            if (!userVenta.Equals(""))
            {
                textBox1.Text = userVenta.ToString();
            }
        }

        private void FrmValidarVenta_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string val = "V";
            object[] parametro = new object[2];
            int valor = 0;
            parametro[0] = textBox1.Text;
            parametro[1] = textBox2.Text;
            DataSet datos = csql.dataset_cadena("Call SpVendedorValUser('" + parametro[0] + "','" + parametro[1] + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    valor = Int32.Parse(fila[0].ToString());
                }

                if (valor == 0)
                {
                    MessageBox.Show("Usuario o contraseña incorrectos", "SISTEMA");
                    textBox1.SelectionStart = 0;
                    textBox1.SelectionLength = textBox1.TextLength;
                    textBox1.Focus();
                }
                else
                {
                    userVenta = textBox1.Text;
                    this.Close();
                    Opener.validarVenta(val);
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
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.Focus();
            }
        }
    }
}