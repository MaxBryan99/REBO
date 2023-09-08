namespace SisBicimotoApp
{
    partial class FrmItemProve
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
            this.button5 = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = global::SisBicimotoApp.Properties.Resources.edit_find;
            this.button5.Location = new System.Drawing.Point(44, 55);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(66, 30);
            this.button5.TabIndex = 64;
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.SystemColors.Info;
            this.label20.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Navy;
            this.label20.Location = new System.Drawing.Point(119, 60);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(471, 21);
            this.label20.TabIndex = 63;
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.Info;
            this.textBox9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.Location = new System.Drawing.Point(122, 26);
            this.textBox9.MaxLength = 11;
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(142, 21);
            this.textBox9.TabIndex = 62;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Navy;
            this.label19.Location = new System.Drawing.Point(44, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(66, 15);
            this.label19.TabIndex = 61;
            this.label19.Text = "Proveedor";
            // 
            // FrmItemProve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(690, 409);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.label19);
            this.Name = "FrmItemProve";
            this.Text = "Consulta  Articulos de Proveedor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label19;
    }
}