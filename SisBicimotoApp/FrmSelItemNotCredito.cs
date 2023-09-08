using SisBicimotoApp.Interface;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmSelItemNotCredito : Form
    {
        public ISelNotCredito Opener { get; set; }

        public FrmSelItemNotCredito()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSelItemNotCredito_Load(object sender, EventArgs e)
        {
            label7.Text = FrmAddNotCredito.nCodigo.ToString();
            label2.Text = FrmAddNotCredito.nProducto.ToString();
            label4.Text = FrmAddNotCredito.nUnidad.ToString();
            textBox10.Text = FrmAddNotCredito.nCantidad.ToString();
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                button1.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double nCanIni = Double.Parse(FrmAddNotCredito.nCantidad.ToString());
            double nCanFin = Double.Parse(textBox10.Text.ToString());
            if (textBox10.TextLength == 0)
            {
                MessageBox.Show("Ingrese cantidad", "SISTEMA");
                textBox10.Focus();
                return;
            }

            if (nCanFin > nCanIni)
            {
                MessageBox.Show("No se puede cambiar una cantidad mayor a la inicial", "SISTEMA");
                textBox10.Focus();
                return;
            }

            if (nCanFin == 0)
            {
                MessageBox.Show("Ingrese cantidad", "SISTEMA");
                textBox10.Focus();
                return;
            }

            this.Opener.SelectItemCantidad(nCanFin, label7.Text.ToString());
            this.Close();
        }
    }
}