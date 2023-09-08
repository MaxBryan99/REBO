using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisBicimotoApp.Lib;
using SisBicimotoApp.Clases;

namespace SisBicimotoApp
{
    public partial class FrmSerieDoc : Form
    {
        DataSet datos;
        string vDoc = "";
        string tMod = "";
        ClsDocumento ObjDocumento = new ClsDocumento();
        ClsSerie ObjSerie = new ClsSerie();
        ClsDetCatalogo ObjDetCatalogo = new ClsDetCatalogo();
        public FrmSerieDoc()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Serie";
            Grid1.Columns[1].HeaderText = "Prefijo";
            Grid1.Columns[2].HeaderText = "Núm.Corr.";
            Grid1.Columns[3].HeaderText = "Correla";
            Grid1.Columns[4].HeaderText = "Serie Imp.";

            Grid1.Columns[0].Width = 50;
            Grid1.Columns[1].Width = 50;
            Grid1.Columns[2].Width = 65;
            Grid1.Columns[3].Width = 55;
            Grid1.Columns[4].Width = 100;

            Grid1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Grid1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        void LlenarCampos(string InCod, string serie)
        {
            string nomFormato = "";
            if (ObjSerie.BuscarDocSerie(InCod, serie))
            {
                string vCodCat = "025";
                if (ObjDetCatalogo.BuscarDetCatalogoCod(vCodCat.ToString(), ObjSerie.Formato_Imp.ToString()))
                {
                    nomFormato = ObjDetCatalogo.Descripcion.ToString();
                }
                else
                {
                    nomFormato = "";
                }

                comboBox2.Text = nomFormato;
                comboBox3.Text = ObjSerie.Impresora.ToString();
            }
            else
            {
                MessageBox.Show("FALSE");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            textBox9.Enabled = true;
            textBox1.Enabled = true;
            textBox3.Enabled = true;
            textBox2.Enabled = false;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = true;
            button7.Enabled = true;
            checkBox1.Checked = false;
            Grid1.Enabled = false;
            textBox9.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox2.Text = "";
            comboBox3.Text="";
            tMod = "N";
            textBox9.Focus();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.Enabled = true;
                textBox2.Focus();
            } else
            {
                textBox2.Enabled = false;
            }
        }

        private void FrmSerieDoc_Load(object sender, EventArgs e)
        {
            vDoc = FrmRegisDoc.cod;
            datos = csql.dataset("Call SpSerieBusGen('" + vDoc.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
            if (ObjDocumento.BuscarDoc(vDoc))
            {
                label4.Text = ObjDocumento.Nombre.ToString();
            } else
            {
                label4.Text = "";
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

            this.Text ="Series - " + ObjDocumento.Nombre.ToString(); 
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tMod = "M";
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = true;
            Grid1.Enabled = false;
            checkBox1.Enabled = true;
            textBox9.Enabled = true;
            textBox1.Enabled = true;
            textBox3.Enabled = true;
            comboBox2.Enabled = true;
            comboBox3.Enabled = true;
            if (checkBox1.Checked ==true)
            {
                textBox2.Enabled = true;
            } else
            {
                textBox2.Enabled = false;
            }
            button7.Enabled = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            textBox9.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            button7.Enabled = false;
            checkBox1.Checked = false;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = false;
            Grid1.Enabled = true;
        }

        private void Grid1_SelectionChanged(object sender, EventArgs e)
        {
            textBox9.Text = Grid1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = Grid1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = Grid1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = Grid1.CurrentRow.Cells[4].Value.ToString();
            if (Grid1.CurrentRow.Cells[3].Value.ToString().Equals("S"))
            {
                checkBox1.Checked = true;
                if (toolStripButton1.Enabled == true)
                {
                    textBox2.Enabled = false;
                } else
                {
                    textBox2.Enabled = true;
                }
            } else
            {
                checkBox1.Checked = false;
            }
            string Cod = FrmRegisDoc.cod.ToString();
            LlenarCampos(Cod, textBox9.Text.Trim());

        }

        private void Grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox9.TextLength == 0)
            {
                MessageBox.Show("Ingrese Serie de Documento", "SISTEMA");
                textBox9.Focus();
                return;
            }

            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Prefijo de Documento", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (checkBox1.Checked == true)
            {
                if (textBox2.TextLength == 0)
                {
                    MessageBox.Show("Ingrese N° correlativo de serie", "SISTEMA");
                    textBox2.Focus();
                    return;
                }
            }

            /*if (tMod.Equals("M"))
            {
                if (checkBox1.Checked == true)
                {
                    if (ObjSerie.BuscarDocSerieVentas(vDoc, textBox9.Text.ToString()))
                    {
                        MessageBox.Show("Existen ventas para esta serie, no se puede modificar como correlativo", "SISTEMA");
                        textBox9.Focus();
                        return;
                    }
                }
            }*/

            string Usuario = FrmLogin.x_login_usuario;
            ObjSerie.Doc = vDoc;
            ObjSerie.Serie = textBox9.Text;
            ObjSerie.PrefijoSerie = textBox1.Text;
            ObjSerie.Correla = checkBox1.Checked == true ? "S" : "N";
            ObjSerie.Numero = int.Parse(textBox2.Text.ToString().Equals("") ? "0" : textBox2.Text.ToString().Trim());
            ObjSerie.NumSerieImp = textBox3.Text;
            ObjSerie.UserCreacion = Usuario.ToString();
            ObjSerie.UserModi = Usuario.ToString();
            //Formato Impresion
            ObjDetCatalogo.BuscarDetCatalogoDes("025", comboBox2.Text.Trim(), "1");
            ObjSerie.Formato_Imp = comboBox2.Text.ToString().Equals("") ? "" : ObjDetCatalogo.CodDetCat.ToString();
            ObjSerie.Impresora = comboBox3.Text.ToString();
            if (Grid1.RowCount > 0)
            {
                ObjSerie.SerieAnt = Grid1.CurrentRow.Cells[0].Value.Equals("") ? "0" : Grid1.CurrentRow.Cells[0].Value.ToString();
            }
            
            if (tMod.Equals("N"))
            {
                if (ObjSerie.Crear())
                {
                    MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            } else
            {
                if (ObjSerie.Modificar())
                {
                    MessageBox.Show("Datos Actualizados Correctamente", "SISTEMA");
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("No se registro correctamente", "SISTEMA");
                }
            }

            datos = csql.dataset("Call SpSerieBusGen('" + vDoc.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
            checkBox1.Enabled = false;
            textBox9.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            button7.Enabled = false;
            checkBox1.Checked = false;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = false;
            Grid1.Enabled = true;
        }
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
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
            if (e.KeyChar == 13)
            {
                textBox3.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

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
                button7.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (checkBox1.Checked == true)
                {
                    textBox2.Focus();
                }
                else
                {
                    button7.Focus();
                }
            }
        }

        private void textBox9_Validated(object sender, EventArgs e)
        {
            try
            {
                double Net = 0;
                Net = double.Parse(textBox9.Text.ToString().Equals("") ? "0" : textBox9.Text.ToString().Trim());
                if (Net.ToString().Trim().Equals("0"))
                    textBox9.Text = "";
                else
                    textBox9.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox9.Focus();
            }
        }
    }
}
