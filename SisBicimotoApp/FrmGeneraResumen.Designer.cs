namespace SisBicimotoApp
{
    partial class FrmGeneraResumen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.DTP1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.Grid1 = new System.Windows.Forms.DataGridView();
            this.FECHA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLIENTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Est = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SELECCIONAR = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IDCOMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TDOC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SERIE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUMERO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCompRel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.Grid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(12, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 15);
            this.label5.TabIndex = 119;
            this.label5.Text = "Total documentos seleccionados: ";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Navy;
            this.button1.Location = new System.Drawing.Point(513, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(159, 29);
            this.button1.TabIndex = 117;
            this.button1.Text = "Enviar Resumen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // DTP1
            // 
            this.DTP1.CalendarFont = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTP1.Font = new System.Drawing.Font("Arial", 9F);
            this.DTP1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DTP1.Location = new System.Drawing.Point(67, 52);
            this.DTP1.Name = "DTP1";
            this.DTP1.Size = new System.Drawing.Size(90, 21);
            this.DTP1.TabIndex = 116;
            this.DTP1.ValueChanged += new System.EventHandler(this.DTP1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(14, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 115;
            this.label1.Text = "Fecha";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button8
            // 
            this.button8.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button8.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.ForeColor = System.Drawing.Color.Navy;
            this.button8.Image = global::SisBicimotoApp.Properties.Resources.exit;
            this.button8.Location = new System.Drawing.Point(733, 42);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(95, 45);
            this.button8.TabIndex = 120;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(358, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 15);
            this.label2.TabIndex = 121;
            this.label2.Text = "Total documentos: ";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::SisBicimotoApp.Properties.Resources.edit_find;
            this.button3.Location = new System.Drawing.Point(176, 50);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(62, 28);
            this.button3.TabIndex = 122;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.Green;
            this.checkBox1.Location = new System.Drawing.Point(578, 83);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(165, 20);
            this.checkBox1.TabIndex = 128;
            this.checkBox1.Text = "Todos Los Artículos";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Info;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.DarkRed;
            this.textBox2.Location = new System.Drawing.Point(134, 11);
            this.textBox2.MaxLength = 11;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(381, 19);
            this.textBox2.TabIndex = 130;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Navy;
            this.label20.Location = new System.Drawing.Point(14, 12);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(114, 19);
            this.label20.TabIndex = 129;
            this.label20.Text = "Url del Servicio";
            // 
            // Grid1
            // 
            this.Grid1.AllowUserToAddRows = false;
            this.Grid1.AllowUserToDeleteRows = false;
            this.Grid1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.Grid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Grid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FECHA,
            this.COMP,
            this.DOC,
            this.CLIENTE,
            this.Est,
            this.TOTAL,
            this.SELECCIONAR,
            this.IDCOMP,
            this.TDOC,
            this.SERIE,
            this.NUMERO,
            this.IdCompRel,
            this.Almacen,
            this.Tipo});
            this.Grid1.EnableHeadersVisualStyles = false;
            this.Grid1.Location = new System.Drawing.Point(12, 138);
            this.Grid1.Name = "Grid1";
            this.Grid1.RowHeadersVisible = false;
            this.Grid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid1.Size = new System.Drawing.Size(908, 411);
            this.Grid1.TabIndex = 131;
            this.Grid1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid1_CellContentClick_1);
            this.Grid1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Grid1_CellFormatting);
            this.Grid1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.Grid1_DataError);
            this.Grid1.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.Grid1_DefaultValuesNeeded);
            this.Grid1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Grid1_EditingControlShowing);
            // 
            // FECHA
            // 
            this.FECHA.HeaderText = "Fecha";
            this.FECHA.Name = "FECHA";
            this.FECHA.ReadOnly = true;
            this.FECHA.Width = 85;
            // 
            // COMP
            // 
            this.COMP.HeaderText = "Comprobante";
            this.COMP.Name = "COMP";
            this.COMP.ReadOnly = true;
            this.COMP.Width = 115;
            // 
            // DOC
            // 
            this.DOC.HeaderText = "Doc. Cliente";
            this.DOC.Name = "DOC";
            this.DOC.ReadOnly = true;
            this.DOC.Width = 80;
            // 
            // CLIENTE
            // 
            this.CLIENTE.HeaderText = "Cliente";
            this.CLIENTE.Name = "CLIENTE";
            this.CLIENTE.ReadOnly = true;
            this.CLIENTE.Width = 350;
            // 
            // Est
            // 
            this.Est.HeaderText = "Estado";
            this.Est.Name = "Est";
            this.Est.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Est.Width = 80;
            // 
            // TOTAL
            // 
            this.TOTAL.HeaderText = "Total";
            this.TOTAL.Name = "TOTAL";
            this.TOTAL.ReadOnly = true;
            this.TOTAL.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TOTAL.Width = 70;
            // 
            // SELECCIONAR
            // 
            this.SELECCIONAR.HeaderText = "";
            this.SELECCIONAR.Name = "SELECCIONAR";
            this.SELECCIONAR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SELECCIONAR.Width = 25;
            // 
            // IDCOMP
            // 
            this.IDCOMP.HeaderText = "IdComp";
            this.IDCOMP.Name = "IDCOMP";
            this.IDCOMP.Visible = false;
            // 
            // TDOC
            // 
            this.TDOC.HeaderText = "TDOC";
            this.TDOC.Name = "TDOC";
            this.TDOC.Visible = false;
            // 
            // SERIE
            // 
            this.SERIE.HeaderText = "SERIE";
            this.SERIE.Name = "SERIE";
            this.SERIE.Visible = false;
            // 
            // NUMERO
            // 
            this.NUMERO.HeaderText = "NUMERO";
            this.NUMERO.Name = "NUMERO";
            this.NUMERO.Visible = false;
            // 
            // IdCompRel
            // 
            this.IdCompRel.HeaderText = "IdCompRel";
            this.IdCompRel.Name = "IdCompRel";
            this.IdCompRel.Visible = false;
            // 
            // Almacen
            // 
            this.Almacen.HeaderText = "Almacen";
            this.Almacen.Name = "Almacen";
            this.Almacen.Visible = false;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Location = new System.Drawing.Point(6, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(834, 30);
            this.panel1.TabIndex = 132;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(834, 27);
            this.toolStrip1.TabIndex = 130;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::SisBicimotoApp.Properties.Resources.new_document;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(150, 24);
            this.toolStripButton1.Text = "&Agregar Comprobante";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::SisBicimotoApp.Properties.Resources.delete__1_;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(74, 24);
            this.toolStripButton3.Text = "&Eliminar";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // FrmGeneraResumen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(934, 561);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Grid1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DTP1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "FrmGeneraResumen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Generar Resumen de Boletas de Venta y Notas Vinculadas";
            this.Load += new System.EventHandler(this.FrmGeneraResumen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker DTP1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DataGridView Grid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHA;
        private System.Windows.Forms.DataGridViewTextBoxColumn COMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLIENTE;
        private System.Windows.Forms.DataGridViewComboBoxColumn Est;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SELECCIONAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCOMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn TDOC;
        private System.Windows.Forms.DataGridViewTextBoxColumn SERIE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUMERO;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCompRel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Almacen;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
    }
}