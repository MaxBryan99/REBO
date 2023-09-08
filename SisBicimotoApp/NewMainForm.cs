using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SisBicimotoApp
{
    public partial class NewMainForm : Form
    {
        public NewMainForm()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.DoubleBuffered = true;
        }

        //METODO PARA REDIMENCIONAR/CAMBIAR TAMAÑO A FORMULARIO  TIEMPO DE EJECUCION ----------------------------------------------------------
        private int tolerance = 15;

        private const int WM_NCHITTEST = 132;
        private const int HTBOTTOMRIGHT = 17;
        private Rectangle sizeGripRectangle;

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    var hitPoint = this.PointToClient(new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16));
                    if (sizeGripRectangle.Contains(hitPoint))
                        m.Result = new IntPtr(HTBOTTOMRIGHT);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        //----------------DIBUJAR RECTANGULO / EXCLUIR ESQUINA PANEL
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var region = new Region(new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            sizeGripRectangle = new Rectangle(this.ClientRectangle.Width - tolerance, this.ClientRectangle.Height - tolerance, tolerance, tolerance);
            region.Exclude(sizeGripRectangle);
            this.PanelPrincipal.Region = region;
            this.Invalidate();
        }

        //----------------COLOR Y GRIP DE RECTANGULO INFERIOR
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush blueBrush = new SolidBrush(Color.FromArgb(55, 61, 69));
            e.Graphics.FillRectangle(blueBrush, sizeGripRectangle);
            base.OnPaint(e);
            ControlPaint.DrawSizeGrip(e.Graphics, Color.Transparent, sizeGripRectangle);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (PanelMenu.Width == 230)
            {
                this.tmContraerMenú.Start();
            }
            else if (PanelMenu.Width == 55)
            {
                this.tmExpanderMenú.Start();
            }
        }

        private void tmContraerMenú_Tick(object sender, EventArgs e)
        {
            if (PanelMenu.Width <= 55)
                this.tmContraerMenú.Stop();
            else
                PanelMenu.Width = PanelMenu.Width - 5;
        }

        private void tmExpanderMenú_Tick(object sender, EventArgs e)
        {
            if (PanelMenu.Width >= 230)
                this.tmExpanderMenú.Stop();
            else
                PanelMenu.Width = PanelMenu.Width + 5;
        }

        private int lx, ly;
        private int sw, sh;

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            this.Size = new Size(sw, sh);
            this.Location = new Point(lx, ly);
            btnNormal.Visible = false;
            btnMaxi.Visible = true;
        }

        private void btnMaxi_Click(object sender, EventArgs e)
        {
            lx = this.Location.X;
            ly = this.Location.Y;
            sw = this.Size.Width;
            sh = this.Size.Height;
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            btnMaxi.Visible = false;
            btnNormal.Visible = true;
        }

        private void mini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PanelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void tmHorayFecha_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToLongDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void AbrirFormEnPanel(object formHijo)
        {
            if (this.PanelContenido.Controls.Count > 0)
                this.PanelContenido.Controls.RemoveAt(0);
            Form fh = formHijo as Form;
            fh.TopLevel = false;
            fh.FormBorderStyle = FormBorderStyle.None;
            fh.Dock = DockStyle.Fill;
            this.PanelContenido.Controls.Add(fh);
            this.PanelContenido.Tag = fh;
            fh.Show();
        }

        private void PanelPrincipal_Paint(object sender, PaintEventArgs e)
        {
        }

        private void NewMainForm_Load(object sender, EventArgs e)
        {
            MostrarFormLogo();
        }

        private void lblFecha_Click(object sender, EventArgs e)
        {
        }

        private void MostrarFormLogo()
        {
            AbrirFormEnPanel(new FormLogo());
        }

        private void MostrarFormLogoAlCerrarForms(object sender, FormClosedEventArgs e)
        {
            MostrarFormLogo();
        }
    }
}