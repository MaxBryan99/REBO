namespace SisBicimotoApp
{
    partial class NewMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMainForm));
            this.PanelPrincipal = new System.Windows.Forms.Panel();
            this.PanelContenido = new System.Windows.Forms.Panel();
            this.PanelUsuario = new System.Windows.Forms.Panel();
            this.lblFecha = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.PanelMenu = new System.Windows.Forms.Panel();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMenu = new System.Windows.Forms.PictureBox();
            this.PanelTitulo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnNormal = new System.Windows.Forms.PictureBox();
            this.mini = new System.Windows.Forms.PictureBox();
            this.btnMaxi = new System.Windows.Forms.PictureBox();
            this.btnCerrar = new System.Windows.Forms.PictureBox();
            this.tmHorayFecha = new System.Windows.Forms.Timer(this.components);
            this.tmExpanderMenú = new System.Windows.Forms.Timer(this.components);
            this.tmContraerMenú = new System.Windows.Forms.Timer(this.components);
            this.PanelPrincipal.SuspendLayout();
            this.PanelUsuario.SuspendLayout();
            this.PanelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).BeginInit();
            this.PanelTitulo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNormal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mini)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaxi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelPrincipal
            // 
            this.PanelPrincipal.BackColor = System.Drawing.Color.Silver;
            this.PanelPrincipal.Controls.Add(this.PanelContenido);
            this.PanelPrincipal.Controls.Add(this.PanelUsuario);
            this.PanelPrincipal.Controls.Add(this.PanelMenu);
            this.PanelPrincipal.Controls.Add(this.PanelTitulo);
            this.PanelPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelPrincipal.Location = new System.Drawing.Point(0, 0);
            this.PanelPrincipal.Name = "PanelPrincipal";
            this.PanelPrincipal.Size = new System.Drawing.Size(1044, 600);
            this.PanelPrincipal.TabIndex = 0;
            this.PanelPrincipal.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelPrincipal_Paint);
            // 
            // PanelContenido
            // 
            this.PanelContenido.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(69)))), ((int)(((byte)(76)))));
            this.PanelContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContenido.Location = new System.Drawing.Point(230, 43);
            this.PanelContenido.Name = "PanelContenido";
            this.PanelContenido.Size = new System.Drawing.Size(814, 485);
            this.PanelContenido.TabIndex = 3;
            // 
            // PanelUsuario
            // 
            this.PanelUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(61)))), ((int)(((byte)(69)))));
            this.PanelUsuario.Controls.Add(this.lblFecha);
            this.PanelUsuario.Controls.Add(this.lblHora);
            this.PanelUsuario.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelUsuario.Location = new System.Drawing.Point(230, 528);
            this.PanelUsuario.Name = "PanelUsuario";
            this.PanelUsuario.Size = new System.Drawing.Size(814, 72);
            this.PanelUsuario.TabIndex = 2;
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblFecha.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblFecha.Location = new System.Drawing.Point(665, 30);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(97, 18);
            this.lblFecha.TabIndex = 1;
            this.lblFecha.Text = "Hora y Fecha";
            this.lblFecha.Click += new System.EventHandler(this.lblFecha_Click);
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblHora.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblHora.Location = new System.Drawing.Point(761, 47);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(41, 18);
            this.lblHora.TabIndex = 0;
            this.lblHora.Text = "Hora";
            // 
            // PanelMenu
            // 
            this.PanelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(34)))), ((int)(((byte)(39)))));
            this.PanelMenu.Controls.Add(this.button9);
            this.PanelMenu.Controls.Add(this.button8);
            this.PanelMenu.Controls.Add(this.button7);
            this.PanelMenu.Controls.Add(this.button6);
            this.PanelMenu.Controls.Add(this.button5);
            this.PanelMenu.Controls.Add(this.button4);
            this.PanelMenu.Controls.Add(this.button3);
            this.PanelMenu.Controls.Add(this.button2);
            this.PanelMenu.Controls.Add(this.button1);
            this.PanelMenu.Controls.Add(this.btnMenu);
            this.PanelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelMenu.Location = new System.Drawing.Point(0, 43);
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(230, 557);
            this.PanelMenu.TabIndex = 1;
            // 
            // button9
            // 
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button9.Location = new System.Drawing.Point(2, 439);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(228, 40);
            this.button9.TabIndex = 9;
            this.button9.Text = "REPORTES";
            this.button9.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.FlatAppearance.BorderSize = 0;
            this.button8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button8.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button8.Location = new System.Drawing.Point(1, 393);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(228, 40);
            this.button8.TabIndex = 8;
            this.button8.Text = "CLIENTES";
            this.button8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button7.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button7.Location = new System.Drawing.Point(2, 347);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(228, 40);
            this.button7.TabIndex = 7;
            this.button7.Text = "PROVEEDORES";
            this.button7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button6.Location = new System.Drawing.Point(1, 301);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(228, 40);
            this.button6.TabIndex = 6;
            this.button6.Text = "SALIDAS";
            this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button5.Location = new System.Drawing.Point(1, 255);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(228, 40);
            this.button5.TabIndex = 5;
            this.button5.Text = "INGRESOS";
            this.button5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button4.Location = new System.Drawing.Point(1, 209);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(228, 40);
            this.button4.TabIndex = 4;
            this.button4.Text = "COMPRAS";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button3.Location = new System.Drawing.Point(1, 163);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(228, 40);
            this.button3.TabIndex = 3;
            this.button3.Text = "VENTAS";
            this.button3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button2.Location = new System.Drawing.Point(2, 117);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(228, 40);
            this.button2.TabIndex = 2;
            this.button2.Text = "VENTAS EN LINEA";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(45)))), ((int)(((byte)(53)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Image = global::SisBicimotoApp.Properties.Resources.producto;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1, 71);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(228, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "PRODUCTOS";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnMenu
            // 
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.Image = ((System.Drawing.Image)(resources.GetObject("btnMenu.Image")));
            this.btnMenu.Location = new System.Drawing.Point(184, 6);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(40, 40);
            this.btnMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnMenu.TabIndex = 0;
            this.btnMenu.TabStop = false;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // PanelTitulo
            // 
            this.PanelTitulo.BackColor = System.Drawing.Color.Coral;
            this.PanelTitulo.Controls.Add(this.label1);
            this.PanelTitulo.Controls.Add(this.btnNormal);
            this.PanelTitulo.Controls.Add(this.mini);
            this.PanelTitulo.Controls.Add(this.btnMaxi);
            this.PanelTitulo.Controls.Add(this.btnCerrar);
            this.PanelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitulo.Location = new System.Drawing.Point(0, 0);
            this.PanelTitulo.Name = "PanelTitulo";
            this.PanelTitulo.Size = new System.Drawing.Size(1044, 43);
            this.PanelTitulo.TabIndex = 0;
            this.PanelTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTitulo_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "SISTEMA BICIMOTO";
            // 
            // btnNormal
            // 
            this.btnNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNormal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNormal.Image = global::SisBicimotoApp.Properties.Resources.Normal;
            this.btnNormal.Location = new System.Drawing.Point(956, 5);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(35, 35);
            this.btnNormal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnNormal.TabIndex = 2;
            this.btnNormal.TabStop = false;
            this.btnNormal.Visible = false;
            this.btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
            // 
            // mini
            // 
            this.mini.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mini.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mini.Image = global::SisBicimotoApp.Properties.Resources.Minimize;
            this.mini.Location = new System.Drawing.Point(915, 5);
            this.mini.Name = "mini";
            this.mini.Size = new System.Drawing.Size(35, 35);
            this.mini.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.mini.TabIndex = 1;
            this.mini.TabStop = false;
            this.mini.Click += new System.EventHandler(this.mini_Click);
            // 
            // btnMaxi
            // 
            this.btnMaxi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaxi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMaxi.Image = global::SisBicimotoApp.Properties.Resources.maximize2;
            this.btnMaxi.Location = new System.Drawing.Point(956, 5);
            this.btnMaxi.Name = "btnMaxi";
            this.btnMaxi.Size = new System.Drawing.Size(35, 35);
            this.btnMaxi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnMaxi.TabIndex = 1;
            this.btnMaxi.TabStop = false;
            this.btnMaxi.Click += new System.EventHandler(this.btnMaxi_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.Image = global::SisBicimotoApp.Properties.Resources.Close;
            this.btnCerrar.Location = new System.Drawing.Point(997, 5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(35, 35);
            this.btnCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.TabStop = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // tmHorayFecha
            // 
            this.tmHorayFecha.Enabled = true;
            this.tmHorayFecha.Tick += new System.EventHandler(this.tmHorayFecha_Tick);
            // 
            // tmExpanderMenú
            // 
            this.tmExpanderMenú.Interval = 15;
            this.tmExpanderMenú.Tick += new System.EventHandler(this.tmExpanderMenú_Tick);
            // 
            // tmContraerMenú
            // 
            this.tmContraerMenú.Interval = 15;
            this.tmContraerMenú.Tick += new System.EventHandler(this.tmContraerMenú_Tick);
            // 
            // NewMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1044, 600);
            this.Controls.Add(this.PanelPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(680, 500);
            this.Name = "NewMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NewMainForm";
            this.Load += new System.EventHandler(this.NewMainForm_Load);
            this.PanelPrincipal.ResumeLayout(false);
            this.PanelUsuario.ResumeLayout(false);
            this.PanelUsuario.PerformLayout();
            this.PanelMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).EndInit();
            this.PanelTitulo.ResumeLayout(false);
            this.PanelTitulo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnNormal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mini)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMaxi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCerrar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PanelPrincipal;
        private System.Windows.Forms.Panel PanelContenido;
        private System.Windows.Forms.Panel PanelUsuario;
        private System.Windows.Forms.Panel PanelMenu;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox btnMenu;
        private System.Windows.Forms.Panel PanelTitulo;
        private System.Windows.Forms.PictureBox btnCerrar;
        private System.Windows.Forms.PictureBox mini;
        private System.Windows.Forms.PictureBox btnMaxi;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Timer tmHorayFecha;
        private System.Windows.Forms.Timer tmExpanderMenú;
        private System.Windows.Forms.Timer tmContraerMenú;
        private System.Windows.Forms.PictureBox btnNormal;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label1;
    }
}