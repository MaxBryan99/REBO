using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmAddDocSerie : Form
    {
        private ClsDocumento ObjDocumento = new ClsDocumento();
        private ClsSerie ObjSerie = new ClsSerie();
        private string Usuario = FrmLogin.x_login_usuario;

        public FrmAddDocSerie()
        {
            InitializeComponent();
            string Cod = FrmRegisDoc.cod.ToString();
            if (FrmRegisDoc.nmDoc == 'M')
            {
                LlenarCampos(Cod);
            }
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Serie";
            Grid1.Columns[1].HeaderText = "N° Correlativo";
            Grid1.Columns[2].HeaderText = "Correlativo";
            Grid1.Columns[0].Width = 70;
            Grid1.Columns[1].Width = 70;
            Grid1.Columns[2].Width = 70;
        }

        private void LlenarCampos(string InCod)
        {
            if (ObjDocumento.BuscarDoc(InCod))
            {
                textBox9.Text = ObjDocumento.Nombre.ToString();
                switch (ObjDocumento.Modulo.ToString())
                {
                    case "VEN":
                        textBox3.Text = "VENTAS";
                        break;

                    case "COM":
                        textBox3.Text = "COMPRAS";
                        break;

                    case "SAL":
                        textBox3.Text = "SALIDAS";
                        break;

                    case " ING":
                        textBox3.Text = "INGRESOS";
                        break;

                    case "ALM":
                        textBox3.Text = "ALMACEN";
                        break;
                }
            }
            else
            {
                MessageBox.Show("FALSE");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmAddDocSerie_Load(object sender, EventArgs e)
        {
            string modulo = " ";
            DataSet datos = csql.dataset_cadena("Call SpDocBusModNom('" + modulo + "')");
        }
    }
}