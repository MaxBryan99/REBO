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
    public partial class FrmCorteCaja : Form
    {
        public static double nTotalCaja = 0;

        public FrmCorteCaja()
        {
            InitializeComponent();
        }

        private void CalcularTotal()
        {
            Double nTot200 = 0;
            Double nTot100 = 0;
            Double nTot50 = 0;
            Double nTot20 = 0;
            Double nTot10 = 0;
            Double nTot5 = 0;
            Double nTot2 = 0;
            Double nTot1 = 0;
            Double nTot050 = 0;
            Double nTot020 = 0;
            Double nTot010 = 0;
            Double nTotal = 0;
            nTot200 = Convert.ToDouble(label14.Text.Equals("") ? "0" : label14.Text);
            nTot100 = Convert.ToDouble(label13.Text.Equals("") ? "0" : label13.Text);
            nTot50 = Convert.ToDouble(label12.Text.Equals("") ? "0" : label12.Text);
            nTot20 = Convert.ToDouble(label11.Text.Equals("") ? "0" : label11.Text);
            nTot10 = Convert.ToDouble(label9.Text.Equals("") ? "0" : label9.Text);
            nTot5 = Convert.ToDouble(label19.Text.Equals("") ? "0" : label19.Text);
            nTot2 = Convert.ToDouble(label18.Text.Equals("") ? "0" : label18.Text);
            nTot1 = Convert.ToDouble(label17.Text.Equals("") ? "0" : label17.Text);
            nTot050 = Convert.ToDouble(label16.Text.Equals("") ? "0" : label16.Text);
            nTot020 = Convert.ToDouble(label15.Text.Equals("") ? "0" : label15.Text);
            nTot010 = Convert.ToDouble(label25.Text.Equals("") ? "0" : label25.Text);
            nTotal = nTot200 + nTot100 + nTot50 + nTot20 + nTot10 + nTot5 + nTot2 + nTot1 + nTot050 + nTot020 + nTot010;
            label28.Text = nTotal.ToString("###,##0.00").Trim();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox9_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox9.Text.ToString().Equals("") ? "0" : textBox9.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox9.Text = "";
                //else
                //textBox9.Text = Net.ToString("0").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox9.Focus();
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox1.Text.ToString().Equals("") ? "0" : textBox1.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox1.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox1.Focus();
            }
        }

        private void textBox2_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox2.Text.ToString().Equals("") ? "0" : textBox2.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox2.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox2.Focus();
            }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox3.Text.ToString().Equals("") ? "0" : textBox3.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox3.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox3.Focus();
            }
        }

        private void textBox4_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox4.Text.ToString().Equals("") ? "0" : textBox4.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox4.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox4.Focus();
            }
        }

        private void textBox10_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox10.Text.ToString().Equals("") ? "0" : textBox10.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox10.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox10.Focus();
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
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox8.Focus();
            }
        }

        private void textBox7_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox7.Text.ToString().Equals("") ? "0" : textBox7.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox7.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox7.Focus();
            }
        }

        private void textBox6_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox6.Text.ToString().Equals("") ? "0" : textBox6.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox6.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox6.Focus();
            }
        }

        private void textBox5_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox5.Text.ToString().Equals("") ? "0" : textBox5.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox5.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox5.Focus();
            }
        }

        private void textBox11_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox11.Text.ToString().Equals("") ? "0" : textBox11.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox11.Text = "";
                //else
                //textBox1.Text = Net.ToString("0.00").Trim();

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox11.Focus();
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox9.Text.Equals("") ? "0" : textBox9.Text);
            nTotal = nCant * 200;
            label14.Text = nTotal.ToString("###,##0.00").Trim();
            label14.Visible = true;
            CalcularTotal();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox1.Text.Equals("") ? "0" : textBox1.Text);
            nTotal = nCant * 100;
            label13.Text = nTotal.ToString("###,##0.00").Trim();
            label13.Visible = true;
            CalcularTotal();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox2.Text.Equals("") ? "0" : textBox2.Text);
            nTotal = nCant * 50;
            label12.Text = nTotal.ToString("###,##0.00").Trim();
            label12.Visible = true;
            CalcularTotal();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox3.Text.Equals("") ? "0" : textBox3.Text);
            nTotal = nCant * 20;
            label11.Text = nTotal.ToString("###,##0.00").Trim();
            label11.Visible = true;
            CalcularTotal();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox4.Text.Equals("") ? "0" : textBox4.Text);
            nTotal = nCant * 10;
            label9.Text = nTotal.ToString("###,##0.00").Trim();
            label9.Visible = true;
            CalcularTotal();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox10.Text.Equals("") ? "0" : textBox10.Text);
            nTotal = nCant * 5;
            label19.Text = nTotal.ToString("###,##0.00").Trim();
            label19.Visible = true;
            CalcularTotal();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox8.Text.Equals("") ? "0" : textBox8.Text);
            nTotal = nCant * 2;
            label18.Text = nTotal.ToString("###,##0.00").Trim();
            label18.Visible = true;
            CalcularTotal();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox7.Text.Equals("") ? "0" : textBox7.Text);
            nTotal = nCant * 1;
            label17.Text = nTotal.ToString("###,##0.00").Trim();
            label17.Visible = true;
            CalcularTotal();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox6.Text.Equals("") ? "0" : textBox6.Text);
            nTotal = nCant * 0.50;
            label16.Text = nTotal.ToString("###,##0.00").Trim();
            label16.Visible = true;
            CalcularTotal();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox5.Text.Equals("") ? "0" : textBox5.Text);
            nTotal = nCant * 0.20;
            label15.Text = nTotal.ToString("###,##0.00").Trim();
            label15.Visible = true;
            CalcularTotal();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            int nCant = 0;
            Double nTotal = 0;
            nCant = Convert.ToInt32(textBox11.Text.Equals("") ? "0" : textBox11.Text);
            nTotal = nCant * 0.10;
            label25.Text = nTotal.ToString("###,##0.00").Trim();
            label25.Visible = true;
            CalcularTotal();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nTotalCaja = Convert.ToDouble(label28.Text.Equals("") ? "0" : label28.Text);

            if (MessageBox.Show("Datos Correctos, se procederá a procesar la información", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            FrmCorteCalculado frmCorteCalculado = new FrmCorteCalculado();
            frmCorteCalculado.WindowState = FormWindowState.Normal;
            //frmCorteCalculado.MdiParent = this.MdiParent;
            //frmCorteCalculado.Show();
            frmCorteCalculado.ShowDialog();
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
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
                textBox2.Focus();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox4.Focus();
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox10.Focus();
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox8.Focus();
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox7.Focus();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox6.Focus();
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
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
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
            char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                textBox11.Focus();
            }
        }
    }
}
