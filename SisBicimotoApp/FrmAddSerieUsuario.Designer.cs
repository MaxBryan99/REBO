namespace SisBicimotoApp
{
    partial class FrmAddSerieUsuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAddSerieUsuario));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Grid1 = new System.Windows.Forms.DataGridView();
            this.Código = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UND = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PVENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IGV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IMPORTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SELECCIONAR = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CODMARCA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODPROCED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROCED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Grid1)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Navy;
            this.label5.Location = new System.Drawing.Point(12, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 22);
            this.label5.TabIndex = 118;
            this.label5.Text = "Usuario:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Info;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Navy;
            this.label4.Location = new System.Drawing.Point(118, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(201, 27);
            this.label4.TabIndex = 119;
            this.label4.Text = "Usuario: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(13, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 22);
            this.label1.TabIndex = 120;
            this.label1.Text = "Nro. de Serie";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Info;
            this.textBox1.Font = new System.Drawing.Font("Arial", 10F);
            this.textBox1.Location = new System.Drawing.Point(119, 98);
            this.textBox1.MaxLength = 3;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(91, 23);
            this.textBox1.TabIndex = 121;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(216, 95);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(60, 30);
            this.button2.TabIndex = 122;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.Navy;
            this.button3.Image = global::SisBicimotoApp.Properties.Resources.exit;
            this.button3.Location = new System.Drawing.Point(325, 229);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 43);
            this.button3.TabIndex = 124;
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Navy;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 22);
            this.label2.TabIndex = 125;
            this.label2.Text = "Perfil:";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Info;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Navy;
            this.label3.Location = new System.Drawing.Point(118, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 27);
            this.label3.TabIndex = 126;
            this.label3.Text = "Usuario: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Grid1
            // 
            this.Grid1.AllowUserToAddRows = false;
            this.Grid1.AllowUserToDeleteRows = false;
            this.Grid1.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Grid1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Grid1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Grid1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Grid1.ColumnHeadersHeight = 26;
            this.Grid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Código,
            this.Producto,
            this.UND,
            this.CANTIDAD,
            this.PVENT,
            this.IGV,
            this.IMPORTE,
            this.SELECCIONAR,
            this.CODMARCA,
            this.CODPROCED,
            this.PROCED});
            this.Grid1.EnableHeadersVisualStyles = false;
            this.Grid1.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.Grid1.Location = new System.Drawing.Point(16, 131);
            this.Grid1.Name = "Grid1";
            this.Grid1.ReadOnly = true;
            this.Grid1.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            this.Grid1.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.Grid1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.Grid1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Grid1.Size = new System.Drawing.Size(303, 141);
            this.Grid1.TabIndex = 127;
            // 
            // Código
            // 
            this.Código.HeaderText = "CÓDIGO";
            this.Código.Name = "Código";
            this.Código.ReadOnly = true;
            this.Código.Width = 77;
            // 
            // Producto
            // 
            this.Producto.HeaderText = "PRODUCTO";
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            this.Producto.Width = 390;
            // 
            // UND
            // 
            this.UND.HeaderText = "UND.";
            this.UND.Name = "UND";
            this.UND.ReadOnly = true;
            this.UND.Width = 50;
            // 
            // CANTIDAD
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CANTIDAD.DefaultCellStyle = dataGridViewCellStyle2;
            this.CANTIDAD.HeaderText = "CANT.";
            this.CANTIDAD.Name = "CANTIDAD";
            this.CANTIDAD.ReadOnly = true;
            this.CANTIDAD.Width = 60;
            // 
            // PVENT
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PVENT.DefaultCellStyle = dataGridViewCellStyle3;
            this.PVENT.HeaderText = "P.VENT.";
            this.PVENT.Name = "PVENT";
            this.PVENT.ReadOnly = true;
            this.PVENT.Width = 70;
            // 
            // IGV
            // 
            this.IGV.HeaderText = "IGV";
            this.IGV.Name = "IGV";
            this.IGV.ReadOnly = true;
            this.IGV.Width = 70;
            // 
            // IMPORTE
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.IMPORTE.DefaultCellStyle = dataGridViewCellStyle4;
            this.IMPORTE.HeaderText = "IMPORTE";
            this.IMPORTE.Name = "IMPORTE";
            this.IMPORTE.ReadOnly = true;
            this.IMPORTE.Width = 75;
            // 
            // SELECCIONAR
            // 
            this.SELECCIONAR.FillWeight = 50F;
            this.SELECCIONAR.HeaderText = "SEL.";
            this.SELECCIONAR.Name = "SELECCIONAR";
            this.SELECCIONAR.ReadOnly = true;
            this.SELECCIONAR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SELECCIONAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SELECCIONAR.Width = 35;
            // 
            // CODMARCA
            // 
            this.CODMARCA.HeaderText = "CODMARCA";
            this.CODMARCA.Name = "CODMARCA";
            this.CODMARCA.ReadOnly = true;
            this.CODMARCA.Visible = false;
            // 
            // CODPROCED
            // 
            this.CODPROCED.HeaderText = "CODPROCED";
            this.CODPROCED.Name = "CODPROCED";
            this.CODPROCED.ReadOnly = true;
            this.CODPROCED.Visible = false;
            // 
            // PROCED
            // 
            this.PROCED.HeaderText = "PROCED";
            this.PROCED.Name = "PROCED";
            this.PROCED.ReadOnly = true;
            this.PROCED.Visible = false;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(325, 168);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(79, 37);
            this.button4.TabIndex = 128;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // FrmAddSerieUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(415, 282);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.Grid1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmAddSerieUsuario";
            this.Text = "Agregar Serie de ventas a Usuarios";
            this.Load += new System.EventHandler(this.FrmAddSerieUsuario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Grid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView Grid1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Código;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn UND;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANTIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn PVENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn IGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn IMPORTE;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SELECCIONAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODMARCA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODPROCED;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROCED;
        private System.Windows.Forms.Button button4;
    }
}