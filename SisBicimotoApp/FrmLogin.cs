using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmLogin : Form
    {
        public static string x_codigo_usuario = "";
        public static string x_nombre_usuario = "";
        public static string x_login_usuario = "";
        private string x_clave = "";
        public static string x_NomEmpresa = "";
        public static string x_RucEmpresa = "";
        public static string x_NomAlmacen = "";
        public static string x_CodAlmacen = "";
        public static string XServidor = "";
        public static string XDB = "";
        public static string XUser = "";
        public static string XPassword = "";
        public static string x_serie = "";
        public static string x_SslMode = "";
        public static string x_TipoCambioSUNATVenta = "";
        public static string x_TipoCambioSUNATCompra = "";
        public static string x_TipoCambioSUNATFecha = "";
        public static string XServicio = "";
        private ClsTipoCambio ObjTipoCambio = new ClsTipoCambio();
        private ClsEmpresa ObjEmpresa = new ClsEmpresa();
        private CookieContainer cokkie = new CookieContainer();


        public FrmLogin()
        {
            InitializeComponent();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.cerrar();
        }

        public void TipoCambioSUNAT()
        {

            try
            {
                string myurl = @"http://www.sunat.gob.pe/a/txt/tipoCambio.txt";
                ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myurl);
                myWebRequest.CookieContainer = cokkie;

                HttpWebResponse myhttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myStream = myhttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myStream);
                string xDat = ""; int pos = 0;
                string razon = "";
                string direccion = "";

                while (!myStreamReader.EndOfStream)
                {
                    xDat = myStreamReader.ReadLine();
                    pos++;

                    switch (pos)
                    {
                        case 1:
                            razon = getDatafromXML(xDat, 16);
                            break;

                        
                    }
                    
                }

                x_TipoCambioSUNATVenta = razon;
               
            }
            catch 
            {
            }
        }
        private string getDatafromXML(string lineRead, int len = 0)
        {
            try
            {
                lineRead = lineRead.Trim();
                lineRead = lineRead.Remove(0, len);
                lineRead = lineRead.Replace("</td>", "");
                lineRead = lineRead.Replace("\\u00d1", "Ñ");
                lineRead = lineRead.Replace("\\u00ba", "º");
                lineRead = lineRead.Replace("\"", "");
                lineRead = lineRead.Replace(",", "");
                lineRead = lineRead.Replace("|", "");

                while (lineRead.Contains("  "))
                {
                    lineRead = lineRead.Replace("  ", " ");
                }
                return lineRead;
            }
            catch (Exception ex)
            {
                return "NO SE ENCONTRO RESULTADO";
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            TipoCambioSUNAT();
            //empieza coneccion
            string archivo = System.Environment.CurrentDirectory + @"\base.ini";

            //MessageBox.Show("Llega");
            try
            {
                //Inicializar servidor
                cini ciniarchivo = new cini(archivo);
                XServidor = ciniarchivo.ReadValue("BaseDatos", "Servidor", "localhost");
                XDB = ciniarchivo.ReadValue("BaseDatos", "Base", "bdbicimoto");
                XUser = ciniarchivo.ReadValue("BaseDatos", "Usuario", "root");
                XPassword = ciniarchivo.ReadValue("BaseDatos", "Password", "123server");
                x_SslMode = ciniarchivo.ReadValue("BaseDatos", "SslMode", "none");
                XServicio = ciniarchivo.ReadValue("Configura", "Service", "DESARROLLO");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Hubo problemas al leer el archivo INI. " + ex.Message, "SISTEMA");
            }


            csql.cadena_coneccion = "datasource=" + XServidor + ";username=" + XUser + ";password=" + XPassword + ";database=" + XDB + ";SslMode=" + x_SslMode;
            if (csql.conectar() == 0)
            {
                //Carga Modulos
                DataSet datos1 = csql.dataset("Call SpModuloGen()");
                comboBox1.DisplayMember = "nombreMod";
                comboBox1.ValueMember = "IdMod";
                comboBox1.DataSource = datos1.Tables[0];

                //Obtener Empresa Predeterminada
                if (ObjEmpresa.BuscarPredeterminado())
                {
                    x_RucEmpresa = ObjEmpresa.Ruc.ToString().Trim();
                    x_NomEmpresa = ObjEmpresa.Razon.ToString().Trim();
                }
                else
                {
                }
                //Carga Almacen
                DataSet datos2 = csql.dataset("Call SpAlmacenGen('" + x_RucEmpresa.ToString() + "')");
                comboBox2.DisplayMember = "nombre";
                comboBox2.ValueMember = "CodAlmacen";
                comboBox2.DataSource = datos2.Tables[0];

                textBox3.Text = x_TipoCambioSUNATVenta;

            }
            else
            {
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {        
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
                object[] parametro = new object[2];
                parametro[0] = textBox1.Text;
                parametro[1] = textBox2.Text;
                DataSet datos = csql.dataset_cadena("Call SpUsuarioValUser('" + parametro[0] + "','" + parametro[1] + "')");
                if (datos.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {

                        x_codigo_usuario = fila[0].ToString();
                        x_login_usuario = fila[1].ToString();
                        x_nombre_usuario = fila[2].ToString() + " " + fila[3].ToString();
                        x_clave = fila[4].ToString();
                        if (textBox4.Enabled == false)
                        {
                            textBox4.Text = fila[5].ToString();
                            if (textBox4.Text.Equals(""))
                            {
                                MessageBox.Show("Usuario no tiene Serie registrada. Ingrese Serie", "SISTEMA");
                                textBox4.Enabled = true;
                                textBox4.Focus();
                                return;
                            }
                        }
                    }
                }   //comboBox2.SelectionStart = 0;
                comboBox2.Focus();
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                //textBox4.Focus();
                comboBox2.Focus();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1.Focus();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Usuario", "SISTEMA");
                textBox1.Focus();
                return;
            }

            if (textBox2.TextLength == 0)
            {
                MessageBox.Show("Ingrese Contraseña", "SISTEMA");
                textBox2.Focus();
                return;
            }

            if (textBox4.TextLength == 0 && textBox4.Enabled == true)
            {
                MessageBox.Show("Ingrese Serie", "SISTEMA");
                textBox4.Focus();
                return;
            }
            object[] parametro = new object[2];
            parametro[0] = textBox1.Text;
            parametro[1] = textBox2.Text;
            DataSet datos = csql.dataset_cadena("Call SpUsuarioValUser('" + parametro[0] + "','" + parametro[1] + "')");
            if (datos.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    x_codigo_usuario = fila[0].ToString();
                    x_login_usuario = fila[1].ToString();
                    x_nombre_usuario = fila[2].ToString() + " " + fila[3].ToString();
                    x_clave = fila[4].ToString();
                    if (textBox4.Enabled == false)
                    {
                        textBox4.Text = fila[5].ToString();
                        if (textBox4.Text.Equals(""))
                        {
                            MessageBox.Show("Usuario no tiene Serie registrada. Ingrese Serie", "SISTEMA");
                            textBox4.Enabled = true;
                            textBox4.Focus();
                            return;
                        }
                    }

                    //Verificamos acceso al modulo
                    object[] parametro1 = new object[2];
                    parametro1[0] = x_codigo_usuario;
                    parametro1[1] = int.Parse(comboBox1.SelectedValue.ToString());
                    DataSet datosMod = csql.dataset_cadena("Call SpValModUser('" + parametro1[0] + "','" + parametro1[1] + "')");

                    if (datosMod.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("El usuario " + textBox1.Text + " no esta registrado para el modulo " + comboBox1.Text, "SISTEMA");
                        return;
                    }

                    //Verificamos acceso al almacen
                    object[] parametro2 = new object[2];
                    parametro2[0] = x_codigo_usuario;
                    parametro2[1] = int.Parse(comboBox2.SelectedValue.ToString());
                    DataSet datosAlm = csql.dataset_cadena("Call SpValAlmUser('" + parametro2[0] + "','" + parametro2[1] + "')");

                    if (datosAlm.Tables[0].Rows.Count == 0)
                    {
                        MessageBox.Show("El usuario " + textBox1.Text + " no esta registrado para el almacén " + comboBox2.Text, "SISTEMA");
                        return;
                    }
                    x_NomAlmacen = comboBox2.Text;
                    x_CodAlmacen = comboBox2.SelectedValue.ToString();
                    x_serie = textBox4.Text;
                    ObjTipoCambio.Valor = (textBox3.Text.Equals("")) ? 0 : double.Parse(textBox3.Text);
                    ObjTipoCambio.Actualiza();
                    MainForm Principal = new MainForm();
                    //NewMainForm Principal = new NewMainForm();
                    Principal.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Se le denego el acceso por error en identificaciòn", "SISTEMA");
                textBox1.SelectionStart = 0;
                textBox1.SelectionLength = textBox1.TextLength;
                textBox1.Focus();
            }
        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Valida numeros
            if (//char.IsLetter(e.KeyChar) || //Letras
            char.IsSymbol(e.KeyChar) || //Símbolos
            char.IsWhiteSpace(e.KeyChar)) //|| //Espaçio
                //char.IsPunctuation(e.KeyChar)) //Pontuacion
                e.Handled = true;

            if (e.KeyChar == 13)
            {
                comboBox2.Focus();
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
                else
                    textBox4.Text = Net.ToString("000").Trim();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Caracter no valido, " + ex.Message, "SISTEMA");
                textBox4.Focus();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
        }

        private void lineShape3_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }
    }
}