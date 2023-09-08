using SisBicimotoApp.Clases;
using SisBicimotoApp.Lib;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class FrmUsuario : Form
    {
        private DataSet datos;

        private ClsRolUser ObjRolUser = new ClsRolUser();
        private ClsRol ObjRol = new ClsRol();
        private ClsRolMenu ObjRolMenu = new ClsRolMenu();

        private string rucEmpresa = FrmLogin.x_RucEmpresa;
        private string codAlmacen = FrmLogin.x_CodAlmacen;
        public static string idUser = "";

        public static char nmUsu = 'N';

        public FrmUsuario()
        {
            InitializeComponent();
        }

        public void Grilla()
        {
            Grid1.Columns[0].HeaderText = "CÓDIGO";
            Grid1.Columns[1].HeaderText = "DNI";
            Grid1.Columns[2].HeaderText = "NOMBRE";
            Grid1.Columns[3].HeaderText = "APELLIDO";
            Grid1.Columns[4].HeaderText = "USUARIO";
            Grid1.Columns[5].HeaderText = "EST.";
            Grid1.Columns[0].Width = 55;
            Grid1.Columns[1].Width = 70;
            Grid1.Columns[2].Width = 182;
            Grid1.Columns[3].Width = 182;
            Grid1.Columns[4].Width = 100;
            Grid1.Columns[5].Width = 35;
        }

        private void LoadTreeView()
        {
            DataSet datosMenu = csql.dataset_cadena("Call SpMenuGen()");

            foreach (DataRow fila in datosMenu.Tables[0].Rows)
            {
                TreeNode nodeMenu = new TreeNode((fila[0].ToString() + " - " + fila[1].ToString()));

                DataSet datosMenuI = csql.dataset_cadena("Call SpMenuItemGen('" + fila[0].ToString().Substring(0, 2) + "')");

                foreach (DataRow filaItem in datosMenuI.Tables[0].Rows)
                {
                    TreeNode nodeMenuItem = new TreeNode((filaItem[0].ToString() + " - " + filaItem[1].ToString()));

                    DataSet datosMenuSItem = csql.dataset_cadena("Call SpMenuSubItemGen('" + fila[0].ToString().Substring(0, 2) + "','" + filaItem[0].ToString().Substring(2, 2) + "')");

                    foreach (DataRow filaSubItem in datosMenuSItem.Tables[0].Rows)
                    {
                        TreeNode nodeSubMenuItem = new TreeNode((filaSubItem[0].ToString() + " - " + filaSubItem[1].ToString()));

                        nodeMenuItem.Nodes.Add(nodeSubMenuItem);
                    }

                    nodeMenu.Nodes.Add(nodeMenuItem);
                }

                tvMenu.Nodes.Add(nodeMenu);
            }
        }

        private void LimpiarTreeView()
        {
            foreach (TreeNode node in tvMenu.Nodes)
            {
                node.Checked = false;
                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode nodeItem in node.Nodes)
                    {
                        nodeItem.Checked = false;
                    }
                }
            }
        }

        public void CargarDatos()
        {
            datos = csql.dataset("Call SpUsuarioGen('" + rucEmpresa.ToString() + "')");
            Grid1.DataSource = datos.Tables[0];
            Grilla();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            //Cargar Roles
            DataSet datos = csql.dataset_cadena("Call SpRolGen()");

            if (datos.Tables[0].Rows.Count > 0)
            {
                comboBox1.Items.Add("");
                foreach (DataRow fila in datos.Tables[0].Rows)
                {
                    comboBox1.Items.Add(fila[1].ToString());
                }
            }

            //Cargar Roles
            DataSet datosRol = csql.dataset_cadena("Call SpRolGen()");

            if (datosRol.Tables[0].Rows.Count > 0)
            {
                comboBox2.Items.Add("");
                foreach (DataRow fila in datosRol.Tables[0].Rows)
                {
                    comboBox2.Items.Add(fila[1].ToString());
                }
            }

            //Generar ToolTip
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 300;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.button2, "Modificar datos de usuario");
            toolTip1.SetToolTip(this.button3, "Eliminar usuario");
            toolTip1.SetToolTip(this.button4, "Agregar nuevo usuario");
            toolTip1.SetToolTip(this.button9, "Registrar serie de usuarios");

            LoadTreeView();

            tvMenu.AfterCheck += tvMenu_AfterCheck;

            CargarDatos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedIndex = cbBusqueda.SelectedIndex;
            if (cbBusqueda.SelectedItem == null)
            {
                CargarDatos();
            }
            else
            {
                if (selectedIndex.Equals(0))
                {
                    if (textBox1.TextLength > 0)
                    {
                        string codigo = textBox1.Text.Trim();
                        datos = csql.dataset("Call SpUsuarioBusCodG('" + codigo.ToString() + "','" + rucEmpresa.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                        //label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                    }
                    else
                    {
                        CargarDatos();
                    }
                }
                if (selectedIndex.Equals(1))
                {
                    if (textBox1.TextLength > 0)
                    {
                        string nDni = textBox1.Text.Trim();
                        datos = csql.dataset("Call SpUsuarioBusDni('" + nDni.ToString() + "','" + rucEmpresa.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                        //label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                    }
                    else
                    {
                        CargarDatos();
                    }
                }
                if (selectedIndex.Equals(2))
                {
                    if (textBox1.TextLength > 0)
                    {
                        string nNombre = textBox1.Text.Trim();
                        datos = csql.dataset("Call SpUsuarioBusNom('" + nNombre.ToString() + "','" + rucEmpresa.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                        //label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                    }
                    else
                    {
                        CargarDatos();
                    }
                }
                if (selectedIndex.Equals(3))
                {
                    if (textBox1.TextLength > 0)
                    {
                        string nApe = textBox1.Text.Trim();
                        datos = csql.dataset("Call SpUsuarioBusApe('" + nApe.ToString() + "','" + rucEmpresa.ToString() + "')");
                        Grid1.DataSource = datos.Tables[0];
                        Grilla();
                        //label1.Text = "Registros Encontrados: " + Grid1.RowCount.ToString();
                    }
                    else
                    {
                        CargarDatos();
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            nmUsu = 'N';
            FrmAddUsuario frmAddUsuario = new FrmAddUsuario();
            frmAddUsuario.WindowState = FormWindowState.Normal;
            frmAddUsuario.MdiParent = this.MdiParent;
            frmAddUsuario.Show();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button5.Focus();
            }
        }

        private void cbBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nmUsu = 'M';
            if (Grid1.RowCount > 0)
            {
                idUser = Grid1.CurrentRow.Cells[0].Value.ToString();

                FrmAddUsuario frmAddUsuario = new FrmAddUsuario();
                frmAddUsuario.WindowState = FormWindowState.Normal;
                frmAddUsuario.MdiParent = this.MdiParent;
                frmAddUsuario.Show();
            }
            else
            {
                MessageBox.Show("No existen Usuarios registrados", "SISTEMA");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2)
            {
                //sE SELECCIONAN los datos del ruc y el nombre de la empresa seleccionada en la Grilla
                label4.Text = Grid1.CurrentRow.Cells[4].Value.ToString();

                //Cargar Roles
                DataSet datos = csql.dataset_cadena("Call SpRolGen()");

                comboBox1.Items.Clear();

                if (datos.Tables[0].Rows.Count > 0)
                {
                    comboBox1.Items.Add("");
                    foreach (DataRow fila in datos.Tables[0].Rows)
                    {
                        comboBox1.Items.Add(fila[1].ToString());
                    }
                }

                //Buscar Rol de Usuario
                string vIdUser = Grid1.CurrentRow.Cells[0].Value.ToString();
                if (ObjRolUser.BuscarRolUser(vIdUser.ToString()))
                {
                    if (ObjRol.BuscarRol(ObjRolUser.IdRol))
                    {
                        comboBox1.Text = ObjRol.Nombre.ToString();
                    }
                    else
                    {
                        comboBox1.Text = "";
                    }
                }
                else
                {
                    comboBox1.Text = "";
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmRol.nmRol = 'N';
            FrmAddRol frmAddRol = new FrmAddRol();
            frmAddRol.WindowState = FormWindowState.Normal;
            frmAddRol.ShowDialog(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Seleccione un ROL para el usuario " + label4.Text, "SISTEMA");
                comboBox1.Focus();
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar los datos", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            //Buscar Nombre Rol

            if (ObjRol.BuscarRolNom(comboBox1.Text))
            {
                ObjRolUser.IdRol = ObjRol.Codigo;
            }
            else
            {
                MessageBox.Show("ROL incorrecto", "SISTEMA");
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;

            ObjRolUser.IdUsuario = Grid1.CurrentRow.Cells[0].Value.ToString();
            ObjRolUser.UserCreacion = Usuario.ToString();

            if (ObjRolUser.Crear())
            {
                MessageBox.Show("Datos Grabados Correctamente", "SISTEMA");
            }
            else
            {
                MessageBox.Show("No se registro correctamente", "SISTEMA");
            }
        }

        private void tvMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //
            // Se remueve el evento para evitar que se ejecute nuevamente por accion de cambio de estado
            // en esta operacion
            //
            tvMenu.AfterCheck -= tvMenu_AfterCheck;

            //
            // Se valida si el nodo marcado tiene presedente
            // en caso de tenerlo se debe evaluar los nodos al mismo nivel para determinar si todos estan marcados,
            // si lo estan se marca tambien el nodo padre
            //
            if (e.Node.Parent != null)
            {
                bool result = true;
                foreach (TreeNode node in e.Node.Parent.Nodes)
                {
                    if (node.Checked)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }

                e.Node.Parent.Checked = result;
            }

            //
            // Se valida si el nodo tiene hijos
            // si los tiene se recorren y asignan el estado del nodo que se esta evaluando
            //
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }

            tvMenu.AfterCheck += tvMenu_AfterCheck;
        }

        private void tvMenu_AfterCheck_UsingLinq(object sender, TreeViewEventArgs e)
        {
            //
            // Se remueve el evento para evitar que se ejecute nuevamente por accion de cambio de estado
            // en esta operacion
            //
            tvMenu.AfterCheck -= tvMenu_AfterCheck_UsingLinq;

            //
            // Se valida si el nodo marcado tiene presedente
            // en caso de tenerlo se debe evaluar los nodos al mismo nivel para determinar si todos estan marcados,
            // si lo estan se marca tambien el nodo padre
            //
            if (e.Node.Parent != null)
            {
                e.Node.Parent.Checked = !e.Node.Parent.Nodes.Cast<TreeNode>().Any(x => !x.Checked);
            }

            //
            // Se valida si el nodo tiene hijos
            // si los tiene se recorren y asignan el estado del nodo que se esta evaluando
            //
            if (e.Node.Nodes.Count > 0)
            {
                e.Node.Nodes.Cast<TreeNode>().ToList().ForEach(x => x.Checked = e.Node.Checked);
            }

            tvMenu.AfterCheck += tvMenu_AfterCheck_UsingLinq;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Seleccione un ROL", "SISTEMA");
                comboBox2.Focus();
                return;
            }

            if (MessageBox.Show("Datos Correctos, se procedera a registrar los datos", "SISTEMA", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            string Usuario = FrmLogin.x_login_usuario;

            //Buscar Nombre Rol
            string vRol = "";
            string vIdMenu = "";
            if (ObjRol.BuscarRolNom(comboBox2.Text))
            {
                vRol = ObjRol.Codigo;
            }
            else
            {
                MessageBox.Show("ROL incorrecto", "SISTEMA");
                return;
            }

            if (ObjRolMenu.Eliminar(vRol.ToString()))
            {
            }
            else
            {
                MessageBox.Show("No se actualizó informacion del ROL, verifique", "SISTEMA");
                return;
            }

            foreach (TreeNode node in tvMenu.Nodes)
            {
                if (node.Checked == true)
                {
                    vIdMenu = node.Text.Substring(0, 6);
                    ObjRolMenu.IdRol = vRol.ToString();
                    ObjRolMenu.IdMenu = vIdMenu.ToString();
                    ObjRolMenu.UserCreacion = Usuario.ToString();
                    if (ObjRolMenu.Crear())
                    {
                    }
                    else
                    {
                        MessageBox.Show("No se registro correctamente Menú: " + vIdMenu.ToString(), "SISTEMA");
                        return;
                    }
                    if (node.Nodes.Count > 0)
                    {
                        foreach (TreeNode nodeItem in node.Nodes)
                        {
                            if (nodeItem.Checked == true)
                            {
                                vIdMenu = nodeItem.Text.Substring(0, 6);
                                ObjRolMenu.IdRol = vRol.ToString();
                                ObjRolMenu.IdMenu = vIdMenu.ToString();
                                ObjRolMenu.UserCreacion = Usuario.ToString();
                                if (ObjRolMenu.Crear())
                                {
                                }
                                else
                                {
                                    MessageBox.Show("No se registro correctamente Menú: " + nodeItem.ToString(), "SISTEMA");
                                    return;
                                }

                                if (nodeItem.Nodes.Count > 0)
                                {
                                    foreach (TreeNode nodeSubItem in nodeItem.Nodes)
                                    {
                                        vIdMenu = nodeSubItem.Text.Substring(0, 6);
                                        ObjRolMenu.IdRol = vRol.ToString();
                                        ObjRolMenu.IdMenu = vIdMenu.ToString();
                                        ObjRolMenu.UserCreacion = Usuario.ToString();
                                        if (ObjRolMenu.Crear())
                                        {
                                        }
                                        else
                                        {
                                            MessageBox.Show("No se registro correctamente Menú: " + nodeItem.ToString(), "SISTEMA");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //node.Checked = e.Node.Checked;
            }

            MessageBox.Show("Datos grabados correctamente", "SISTEMA");
        }

        private void PrintRecursive(TreeNode treeNode)
        {
            // Print the node.
            System.Diagnostics.Debug.WriteLine(treeNode.Text);
            MessageBox.Show(treeNode.Text);
            // Print each node recursively.
            foreach (TreeNode tn in treeNode.Nodes)
            {
                PrintRecursive(tn);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarTreeView();

            string vRol = "";
            if (ObjRol.BuscarRolNom(comboBox2.Text))
            {
                vRol = ObjRol.Codigo;
            }
            else
            {
            }

            string vIdMenu = "";

            foreach (TreeNode node in tvMenu.Nodes)
            {
                vIdMenu = node.Text.Substring(0, 6);
                if (ObjRolMenu.BuscarMenuValor(vRol.ToString(), vIdMenu.ToString()))
                {
                    node.Checked = true;
                }
                else
                {
                    node.Checked = false;
                }

                if (node.Nodes.Count > 0)
                {
                    foreach (TreeNode nodeItem in node.Nodes)
                    {
                        vIdMenu = nodeItem.Text.Substring(0, 6);
                        if (ObjRolMenu.BuscarMenuValor(vRol.ToString(), vIdMenu.ToString()))
                        {
                            nodeItem.Checked = true;
                        }
                        else
                        {
                            nodeItem.Checked = false;
                        }
                    }
                }
            }
        }
    }
}