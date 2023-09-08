using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
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
    public partial class FrmAddDocum : Form
    {
        ClsDocumento ObjDocumento = new ClsDocumento();
        ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        string Usuario = FrmLogin.x_login_usuario;
        string Cod = FrmRegisDoc.cod.ToString();
        public FrmAddDocum()
        {
            InitializeComponent();
        }

        void LlenarCampos(string InCod)
        {
            if(ObjDocumento.BuscarDoc(InCod))
            {
                textBox1.Text = ObjDocumento.NCorto.ToString();
                textBox9.Text = ObjDocumento.Nombre.ToString();
                switch(ObjDocumento.Modulo.ToString())
                    {
                    case "VEN":
                        comboBox1.Text = "VENTAS";
                        break;

                    case "COM":
                        comboBox1.Text = "COMPRAS";
                            break;
                    case "SAL":
                        comboBox1.Text = "SALIDAS";
                        break;
                    case " ING":
                        comboBox1.Text = "INGRESOS";
                        break;
                    case "ALM":
                        comboBox1.Text = "ALMACEN";
                        break;
                }

                if (ObjDocumento.EnvSunat.Equals("S"))
                {
                    checkBox1.Checked = true;
                    if (ObjDocumento.TipDocElectronico.Equals("F"))
                    {
                        radioButton1.Checked = true;
                    }
                    if (ObjDocumento.TipDocElectronico.Equals("B"))
                    {
                        radioButton2.Checked = true;
                    }
                }
                textBox2.Text = ObjDocumento.NFila.ToString();

                string vCodCat = "025";
                if (ObjDetCatalogo.BuscarDetCatalogoCod(vCodCat.ToString(), ObjDocumento.Formato_Imp.ToString()))
                {
                    comboBox2.Text = ObjDetCatalogo.Descripcion.ToString();
                } else
                {
                    comboBox2.Text = "";
                }
                comboBox3.Text = ObjDocumento.Impresora.ToString();

                if (ObjDocumento.Imp.Equals("S"))
                {
                    comboBox4.Text = "Si";
                    comboBox4.SelectedIndex = 0;
                    comboBox2.Enabled = true;
                    comboBox3.Enabled = true;
                } else
                {
                    comboBox4.Text = "No";
                    comboBox4.SelectedIndex = 1;
                    comboBox2.Enabled = false;
                    comboBox3.Enabled = false;
                }

            }
            else
            {
                MessageBox.Show("FALSE");
            }
        }

        

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAddDocum_Load(object sender, EventArgs e)
        {
            //Carga MODULO
            //string modulo = " ";
            DataSet datos = csql.dataset_cadena("Call SpModuloBusGen()");

            if (datos.Tables[0].Rows.Count > 0)
            {
                comboBox1.Items.Add("");
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    comboBox1.Items.Add(fila[0].ToString());
                }
            }
            if (FrmRegisDoc.nmDoc == 'N')
            {
                this.Text = "Registrar Documento";
            }
            else
            {
                this.Text = "Modificar documento";
                //comboBox1.Text = ObjDocumento.Modulo.ToString();
                LlenarCampos(Cod);
                textBox1.Enabled = false;
                textBox9.Focus();

            }

            //Carga Formato de Impresion
            string codFormImp = "025";
            string tipPresDoc = "1";
            DataSet datosFormato = csql.dataset_cadena("Call SpCargarDetCat('" + codFormImp + "','" + tipPresDoc + "')");

            if (datosFormato.Tables[0].Rows.Count > 0)
            {
                comboBox2.Items.Add("");
                foreach (DataRow fila in datosFormato.Tables[0].Rows)
                {
                    comboBox2.Items.Add(fila[1].ToString());
                }
            }

            comboBox3.Items.Add("");

            foreach (String strPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)

            {
                comboBox3.Items.Add(strPrinter);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox9.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre de Documento", "SISTEMA");
                textBox9.Focus();
                return;
            }

            if (comboBox4.Text == "Si")
            {
                if (comboBox2.Text.Equals(""))
                {
                    MessageBox.Show("Ingrese Formato de Impresión", "SISTEMA");
                    comboBox2.Focus();
                    return;
                }
                
            }

            if (checkBox1.Checked==true)
            {
                if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    MessageBox.Show("Si es Documento Electrónico, debe seleccionar un tipo de Documento", "SISTEMA");
                    return;
                }
            }
            string Usuario = FrmLogin.x_login_usuario;
            ObjDocumento.Codigo = Cod;
            ObjDocumento.Nombre = textBox9.Text.ToString();
            ObjDocumento.NCorto = textBox1.Text.ToString();
            ObjDocumento.Modulo = comboBox1.Text.Substring(0,3);
            ObjDocumento.NFila = int.Parse(textBox2.Text.ToString().Equals("") ? "0" : textBox2.Text.ToString().Trim());
            //Formato Impresion
            ObjDetCatalogo.BuscarDetCatalogoDes("025", comboBox2.Text.Trim(), "1");
            ObjDocumento.EnvSunat = checkBox1.Checked == true ? "S" : "";
            ObjDocumento.UserCreacion = Usuario.ToString().Trim();
            ObjDocumento.UserModi = Usuario.ToString().Trim();
            if (comboBox4.Text.Equals("Si"))
            {
                ObjDocumento.Imp = "S";
                ObjDocumento.Formato_Imp = comboBox2.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
                ObjDocumento.Impresora = comboBox3.Text.ToString();
            } else
            {
                ObjDocumento.Imp = "N";
                ObjDocumento.Formato_Imp = "";
                ObjDocumento.Impresora = "";
            }

            if (checkBox1.Checked == true)
            {
                if (radioButton1.Checked == true)
                {
                    ObjDocumento.TipDocElectronico = "F";
                }
                if (radioButton2.Checked == true)
                {
                    ObjDocumento.TipDocElectronico = "B";
                }

            } else
            {
                ObjDocumento.TipDocElectronico = "";
            }

            if (FrmRegisDoc.nmDoc == 'N')
            {
                if (ObjDocumento.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            } else { 

                if (ObjDocumento.Modificar())
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar) || //Espaçio
                char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox4.Focus();
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button6.Focus();
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text.ToString().Equals("Si"))
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text.ToString().Equals("Si"))
            {
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Enabled = true;
            } else
            {
                groupBox1.Enabled = false;
            }
        }
    }
}

