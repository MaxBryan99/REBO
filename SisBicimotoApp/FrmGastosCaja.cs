using SisBicimotoApp.Clases;
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
    public partial class FrmGastosCaja : Form
    {
        string Cod = "";
        ClsMovCaja ObjMovCaja = new ClsMovCaja();

        public FrmGastosCaja()
        {
            InitializeComponent();
        }

        void LlenarCampos(string InCod)
        {
            double Net = 0;

            if (ObjMovCaja.BuscarMovimiento(InCod))
            {
                DTP1.Text = ObjMovCaja.Fecha.ToString().Trim();
                comboBox1.Text = ObjMovCaja.Tipo.ToString().Trim();
                textBox2.Text = ObjMovCaja.Descripcion.ToString().Trim();
                Net = ObjMovCaja.Monto;
                textBox8.Text = Net.ToString("###,##0.00").Trim();
            }
            else
            {
                MessageBox.Show("FALSE");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals(""))
            {
                MessageBox.Show("Ingrese tipo de movimiento", "Sistema");
                comboBox1.Focus();
                return;
            }

            if (textBox2.Text.Equals(""))
            {
                MessageBox.Show("Ingrese descripción", "Sistema");
                textBox2.Focus();
                return;
            }

            if (textBox8.Text.Equals(""))
            {
                MessageBox.Show("Ingrese descripción", "Sistema");
                textBox8.Focus();
                return;
            }
            string Usuario = FrmLogin.x_login_usuario;

            DateTime fechaHoy = DateTime.Now;
            string fecha = fechaHoy.ToString("d");
            string fechaAnio = fecha.Substring(6, 4);
            string fechaMes = fecha.Substring(3, 2);
            string fechaDia = fecha.Substring(0, 2);
            string fecActual = fechaAnio.ToString() + fechaMes.ToString() + fechaDia.ToString();
            string hora = DateTime.Now.Hour.ToString("D2") + DateTime.Now.Minute.ToString("D2") + DateTime.Now.Second.ToString("D2");
            string nId = "";
            if (FrmMovCaja.nmMov == 'N')
            {
                nId = fecActual + hora;
            }
            else
            {
                nId = Cod;
            }

            ObjMovCaja.Id = nId;
            ObjMovCaja.Fecha = DTP1.Value.Year.ToString() + "/" + DTP1.Value.Month.ToString("00") + "/" + DTP1.Value.Day.ToString("00");
            ObjMovCaja.Descripcion = textBox2.Text.ToString();
            ObjMovCaja.Monto = Double.Parse(textBox8.Text.ToString().Equals("") ? "0" : textBox8.Text.ToString().Trim());
            ObjMovCaja.Tipo = comboBox1.Text.Substring(0, 1);
            ObjMovCaja.UserCreacion = Usuario;
            ObjMovCaja.UserModif = Usuario;

            if (FrmMovCaja.nmMov == 'N')
            {
                if (ObjMovCaja.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    textBox2.Clear();
                    textBox8.Clear();
                    DTP1.Focus();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
            else
            {
                if (ObjMovCaja.Modificar())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DTP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox1.Focus();
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox2.Focus();
            }
        }

        private void textBox8_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox8.Text.ToString().Equals("") ? "0" : textBox8.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox8.Text = "";
                else
                    textBox8.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "Sistema");
                textBox8.Focus();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) // || //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button6.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox8.Focus();
            }
        }

        private void FrmGastosCaja_Load(object sender, EventArgs e)
        {
            Cod = FrmMovCaja.nIdMov.ToString();
            if (FrmMovCaja.nmMov == 'M')
            {
                LlenarCampos(Cod);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
