using SisBicimotoApp.Clases;
using System;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class MainForm : Form
    {
        private int childFormNumber = 0;

        private ClsRolMenu ObjRolMenu = new ClsRolMenu();

        public MainForm()
        {
            InitializeComponent();
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Xml.UseInsecureHashAlgorithms", true);
            AppContext.SetSwitch("Switch.System.Security.Cryptography.Pkcs.UseInsecureHashAlgorithms", true);
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DialogResult dialogo = MessageBox.Show("Desea cerrar la aplicación?", "aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (dialogo == DialogResult.Yes)
                {
                    base.OnClosed(e);
                }
                else
                    e.Cancel = true;
            }
            catch { }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.cerrar();
        }

        private void regClienteMenuItem_Click(object sender, EventArgs e)
        {
            FrmCliente childForm = new FrmCliente();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regProveedoresMenuItem_Click(object sender, EventArgs e)
        {
            FrmProveedor childForm = new FrmProveedor();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regPersonalMenuItem_Click(object sender, EventArgs e)
        {
            FrmPersonal childForm = new FrmPersonal();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regVendedorMenuItem_Click(object sender, EventArgs e)
        {
            FrmVendedor childForm = new FrmVendedor();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regTransportistaMenuItem_Click(object sender, EventArgs e)
        {
            FrmTransportista childForm = new FrmTransportista();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regFamiliaMenuItem_Click(object sender, EventArgs e)
        {
            FrmLinea childForm = new FrmLinea();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regCatalogoMenuItem_Click(object sender, EventArgs e)
        {
            FrmMantCatalogo childForm = new FrmMantCatalogo();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regLineaMenuItem_Click(object sender, EventArgs e)
        {
            FrmLinea childForm = new FrmLinea();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regProductosMenuItem_Click(object sender, EventArgs e)
        {
            FrmArticulos childForm = new FrmArticulos();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = FrmLogin.x_NomEmpresa.ToString() + " - " + FrmLogin.x_NomAlmacen.ToString();
            string vIdUser = FrmLogin.x_codigo_usuario.ToString();
            //regConfiguraEmpresa.Enabled = false;
            //Obtener accesos de menus por usuario logueado
            toolStripStatusLabel.Text = "BASE DE DATOS: " + FrmLogin.XDB;
            toolStripStatusLabel1.Text = "USUARIO: " + FrmLogin.x_login_usuario;
            toolStripStatusLabel3.Text = "SERVIDOR: " + FrmLogin.XServidor;

            foreach (ToolStripMenuItem mnuitOpcion in menuStrip.Items)
            {
                // en las opciones del submenú

                if (mnuitOpcion.DropDownItems.Count > 0)
                {
                    //Buscar y verificar si esta asignado al usuario
                    //recorrer el submenú
                    foreach (ToolStripItem itmOpcion in mnuitOpcion.DropDownItems)

                    {
                        //MessageBox.Show("Nombre de Sub Menu " + itmOpcion.Name, "SISTEMA");
                        // si esta opción a su vez despliega un nuevo submenú
                        // llamar recursivamente a este método para cambiar sus opciones
                        if (ObjRolMenu.BuscarMenuUser(vIdUser.ToString(), itmOpcion.Name))
                        {
                            itmOpcion.Enabled = true;
                        }
                        else
                        {
                            itmOpcion.Enabled = false;
                        }
                    }
                }

                if (ObjRolMenu.BuscarMenuUser(vIdUser.ToString(), mnuitOpcion.Name))
                {
                    mnuitOpcion.Enabled = true;
                }
                else
                {
                    mnuitOpcion.Enabled = false;
                }
            }

            string vNomTst = "";
            int tamanio = 0;
            foreach (ToolStripItem mnuistOpcion in toolStrip.Items)
            {
                tamanio = mnuistOpcion.Name.Length - 3;
                vNomTst = mnuistOpcion.Name.Substring(3, tamanio);

                vNomTst = "reg" + vNomTst.ToString();

                if (ObjRolMenu.BuscarMenuUser(vIdUser.ToString(), vNomTst.ToString()))
                {
                    mnuistOpcion.Enabled = true;
                }
                else
                {
                    mnuistOpcion.Enabled = false;
                }
            }
        }

        private void regCompraMenuItem_Click(object sender, EventArgs e)
        {
            FrmCompras childForm = new FrmCompras();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regVentaMenuItem_Click(object sender, EventArgs e)
        {
            FrmVentas childForm = new FrmVentas();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regVentaLinMenuItem_Click(object sender, EventArgs e)
        {
            FrmVentasEnLinea childForm = new FrmVentasEnLinea();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regIngresosMenuItem_Click(object sender, EventArgs e)
        {
            FrmIngresosAlm childForm = new FrmIngresosAlm();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regSalidasMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalidasAlm childForm = new FrmSalidasAlm();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regResponsableMenuItem_Click(object sender, EventArgs e)
        {
            FrmResponsable childForm = new FrmResponsable();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void pruebaXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*FrmPruebaXML childForm = new FrmPruebaXML();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();*/
        }

        private void regNotCreItem_Click(object sender, EventArgs e)
        {
            FrmNotaCredito childForm = new FrmNotaCredito();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regNotDebItem_Click(object sender, EventArgs e)
        {
            FrmNotaDebito childForm = new FrmNotaDebito();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regUsuarioMenuItem_Click(object sender, EventArgs e)
        {
            FrmUsuario childForm = new FrmUsuario();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regConfiguraEmpresa_Click(object sender, EventArgs e)
        {
            FrmEmpresa childForm = new FrmEmpresa();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regRolesUsuarioMenuItem_Click(object sender, EventArgs e)
        {
            FrmRol childForm = new FrmRol();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FrmCompras childForm = new FrmCompras();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FrmVentas childForm = new FrmVentas();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            FrmSalidasAlm childForm = new FrmSalidasAlm();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            FrmIngresosAlm childForm = new FrmIngresosAlm();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            FrmUsuario childForm = new FrmUsuario();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void toolStripButton7_Click_1(object sender, EventArgs e)
        {
            FrmVentas.nmVen = 'N';
            FrmVentasEnLinea childForm = new FrmVentasEnLinea();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();

        }

        private void tstConfiguraEmpresa_Click(object sender, EventArgs e)
        {
            FrmEmpresa childForm = new FrmEmpresa();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regParametros_Click(object sender, EventArgs e)
        {
            FrmParametro childForm = new FrmParametro();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regConGenArtMenuItem_Click(object sender, EventArgs e)
        {
            FrmConsulGenArt childForm = new FrmConsulGenArt();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regReimprimirMenuItem_Click(object sender, EventArgs e)
        {
            FrmReimprimirComp childForm = new FrmReimprimirComp();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = DateTime.Now.ToString("G");
            statusStrip.Refresh();
        }

        private void regKardexMenuItem_Click(object sender, EventArgs e)
        {
            FrmKardex childForm = new FrmKardex();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regComunicaBajaMenuItem_Click(object sender, EventArgs e)
        {
            FrmComunicacionBaja childForm = new FrmComunicacionBaja();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regResumenBolMenuItem_Click(object sender, EventArgs e)
        {
            FrmResumenBolCPE childForm = new FrmResumenBolCPE();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regNotCreItemAlm_Click(object sender, EventArgs e)
        {
            FrmNotaCreditoSalAlm childForm = new FrmNotaCreditoSalAlm();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regEnvioMenuItem_Click(object sender, EventArgs e)
        {
            FrmEnviarSunat childForm = new FrmEnviarSunat();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FrmArticulos childForm = new FrmArticulos();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regDeDocMenuItem_Click(object sender, EventArgs e)
        {
            FrmRegisDoc childForm = new FrmRegisDoc();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
        }

        private void listaDePedidosMenuItem_Click(object sender, EventArgs e)
        {
            FrmListaDePedidos childForm = new FrmListaDePedidos();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regUnidadMenuItem_Click(object sender, EventArgs e)
        {
            FrmUnidad childForm = new FrmUnidad();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void corteCajaMenuItem_Click(object sender, EventArgs e)
        {
            FrmCorteCaja childForm = new FrmCorteCaja();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regIngresosEgresos_Click(object sender, EventArgs e)
        {
            FrmMovCaja childForm = new FrmMovCaja();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void regConsultaCajaMenuItem_Click(object sender, EventArgs e)
        {
            FrmConsultaCorte childForm = new FrmConsultaCorte();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void consultaDeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmResumenStock childForm = new FrmResumenStock();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void reporteDeArticulosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRepArticulo childForm = new FrmRepArticulo();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }

        private void registroDeTurnosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTurnos childForm = new FrmTurnos();
            childForm.WindowState = FormWindowState.Normal;
            childForm.MdiParent = this;
            childForm.Show();
        }
    }
}