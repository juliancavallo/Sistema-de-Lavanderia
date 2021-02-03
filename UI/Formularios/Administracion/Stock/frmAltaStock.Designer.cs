namespace UI.Formularios.Administracion.Stock
{
    partial class frmAltaStock
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
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboUbicacion = new System.Windows.Forms.ComboBox();
            this.comboArticulo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(166, 212);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(132, 46);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(12, 212);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(132, 46);
            this.btnAceptar.TabIndex = 18;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Ubicación";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(33, 150);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(224, 20);
            this.txtCantidad.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Cantidad";
            // 
            // comboUbicacion
            // 
            this.comboUbicacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUbicacion.FormattingEnabled = true;
            this.comboUbicacion.Location = new System.Drawing.Point(33, 44);
            this.comboUbicacion.Name = "comboUbicacion";
            this.comboUbicacion.Size = new System.Drawing.Size(224, 21);
            this.comboUbicacion.TabIndex = 22;
            // 
            // comboArticulo
            // 
            this.comboArticulo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboArticulo.FormattingEnabled = true;
            this.comboArticulo.Location = new System.Drawing.Point(33, 98);
            this.comboArticulo.Name = "comboArticulo";
            this.comboArticulo.Size = new System.Drawing.Size(224, 21);
            this.comboArticulo.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Artículo";
            // 
            // frmAltaStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(309, 270);
            this.Controls.Add(this.comboArticulo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboUbicacion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCantidad);
            this.Name = "frmAltaStock";
            this.Text = "frmAltaStock";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboUbicacion;
        private System.Windows.Forms.ComboBox comboArticulo;
        private System.Windows.Forms.Label label3;
    }
}