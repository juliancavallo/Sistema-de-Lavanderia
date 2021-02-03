namespace UI.Formularios.Administracion.Articulo
{
    partial class frmAltaArticulo
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboTipoDePrenda = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboColor = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboTalle = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(163, 314);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(132, 46);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(12, 314);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(132, 46);
            this.btnAceptar.TabIndex = 14;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Codigo";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(45, 53);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(217, 20);
            this.txtCodigo.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Tipo de Prenda";
            // 
            // comboTipoDePrenda
            // 
            this.comboTipoDePrenda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTipoDePrenda.FormattingEnabled = true;
            this.comboTipoDePrenda.Location = new System.Drawing.Point(45, 111);
            this.comboTipoDePrenda.Name = "comboTipoDePrenda";
            this.comboTipoDePrenda.Size = new System.Drawing.Size(217, 21);
            this.comboTipoDePrenda.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Color";
            // 
            // comboColor
            // 
            this.comboColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboColor.FormattingEnabled = true;
            this.comboColor.Location = new System.Drawing.Point(45, 170);
            this.comboColor.Name = "comboColor";
            this.comboColor.Size = new System.Drawing.Size(217, 21);
            this.comboColor.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Talle";
            // 
            // comboTalle
            // 
            this.comboTalle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboTalle.FormattingEnabled = true;
            this.comboTalle.Location = new System.Drawing.Point(45, 229);
            this.comboTalle.Name = "comboTalle";
            this.comboTalle.Size = new System.Drawing.Size(217, 21);
            this.comboTalle.TabIndex = 21;
            // 
            // frmAltaArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 372);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboTalle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboTipoDePrenda);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigo);
            this.Name = "frmAltaArticulo";
            this.Text = "frmAltaArticulo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboTipoDePrenda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboTalle;
    }
}