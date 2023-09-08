using SisBicimotoApp.Clases;

//Se inicializan las carpetas de las clases y las librerias de conexion
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmEmpresa : Form
    {
        //Aca se declaran las variables globales y locales fijate en los ejemplos
        public static char nmEmp = 'N';

        public static string codRuc = ""; //Variable donde se carga el ruc seleccionado
        private DataSet datos; //variable de conexion
        private string rucEmpresa = FrmLogin.x_RucEmpresa; //variable de ruc
        private ClsAlmacen ObjAlmacen = new ClsAlmacen();

        public FrmEmpresa()
        {
            InitializeComponent();
            if (FrmEmpresa.nmEmp == 'N') //Si es modificacion busca la informacion del cliente seleccionado
            {
                CargarDatos();
            }
        }

        //aca voy a crear el metodo cargarDatos DONDE se carga todos los registros de las Empresas TblEmpresa
        public void CargarDatos()
        {
            //datos es una variable local de conexion
            //Se invoca al procedimiento creado en la BD
            //datos = csql.dataset("Call SpEmpresaBusGen('" + rucEmpresa.ToString() + "')"); //rucEmpresa es una variable global que obtengo cuano el usuario accede al sistema
            datos = csql.dataset("Call SpEmpresaBusGen()");
            Grid1.DataSource = datos.Tables[0];
            Grilla(); //Grilla es un metodo para dar formato a la grilla
        }

        public void CargarDatosAlmacen(string vRuc)
        {
            //datos es una variable local de conexion
            //Se invoca al procedimiento creado en la BD
            //datos = csql.dataset("Call SpEmpresaBusGen('" + rucEmpresa.ToString() + "')"); //rucEmpresa es una variable global que obtengo cuano el usuario accede al sistema
            datos = csql.dataset("Call SpAlmacenBusGen('" + vRuc.ToString() + "')");
            Grid2.DataSource = datos.Tables[0];
            Grilla2(); //Grilla es un metodo para dar formato a la grilla
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "Ruc";
            Grid1.Columns[1].HeaderText = "Empresa";
            Grid1.Columns[2].HeaderText = "Estado";
            Grid1.Columns[3].HeaderText = "Pred.";
            Grid1.Columns[0].Width = 90;
            Grid1.Columns[1].Width = 300;
            Grid1.Columns[2].Width = 65;
            Grid1.Columns[3].Width = 50;
        }

        public void Grilla2()
        {
            Grid2.Columns[0].HeaderText = "Código";
            Grid2.Columns[1].HeaderText = "Descripción";
            Grid2.Columns[2].HeaderText = "Estado";
            Grid2.Columns[3].HeaderText = "Pred.";
            Grid2.Columns[0].Width = 70;
            Grid2.Columns[1].Width = 250;
            Grid2.Columns[2].Width = 65;
            Grid2.Columns[3].Width = 50;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CargarDatos(); //Se invoca Se crea un metodo para cargar datos esi lo hago
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void label1_Click_2(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nmEmp = 'N'; //Esto es una variable global para identificar que el form va a registrar los datos
            FrmAddEmpresa frmAddEmpresa = new FrmAddEmpresa();
            frmAddEmpresa.WindowState = FormWindowState.Normal;
            //frmAddEmpresa.MdiParent = this.MdiParent;
            frmAddEmpresa.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmEmp = 'M'; //Esto es una variable global para identificar que el form va a editar los datos
            if (Grid1.RowCount > 0)
            {
                codRuc = Grid1.CurrentRow.Cells[0].Value.ToString();
                FrmAddEmpresa frmAddEmpresa = new FrmAddEmpresa();
                frmAddEmpresa.WindowState = FormWindowState.Normal;
                //frmAddEmpresa.MdiParent = this.MdiParent;
                frmAddEmpresa.ShowDialog(this);
            }
            else
            {
                MessageBox.Show("No existen registros", "SISTEMA");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                //sE SELECCIONAN los datos del ruc y el nombre de la empresa seleccionada en la Grilla
                label2.Text = Grid1.CurrentRow.Cells[0].Value.ToString() + " - " + Grid1.CurrentRow.Cells[1].Value.ToString();

                // Cargue almacenes
                CargarDatosAlmacen(Grid1.CurrentRow.Cells[0].Value.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string nnombre = textBox2.Text.Trim();
            datos = csql.dataset("Call SpEmpresaBusNom('" + nnombre.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
            //label1.Text = "Empresas Encontradas : " + Grid1.RowCount.ToString();
            //esto por el momento no va
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                MessageBox.Show("Ingrese Nombre del Almacen", "SISTEMA");
                textBox1.Focus();
                return;
            }
            string Usuario = FrmLogin.x_login_usuario;
            ObjAlmacen.Nombre = textBox1.Text.Trim();
            ObjAlmacen.RucEmpresa = Grid1.CurrentRow.Cells[0].Value.ToString();
            ObjAlmacen.UserCreacion = Usuario;
            if (ObjAlmacen.Crear())
            {
                MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
                // Cargue almacenes
                CargarDatosAlmacen(Grid1.CurrentRow.Cells[0].Value.ToString());
                textBox1.Focus();
            }
            else
            {
                MessageBox.Show("No se registro correctamente", "SISTEMA");
                textBox1.Focus();
            }
        }
    }
}