namespace UI.Formularios.Seguridad
{
    partial class frmParametrosDelSistema
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCapacidadMaxima = new System.Windows.Forms.TextBox();
            this.txtCorreoSoporte = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDescuentoEnvios = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRolAdmin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(566, 0);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(46, 17);
            this.lblTitulo.TabIndex = 14;
            this.lblTitulo.Text = "label2";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 52);
            this.label1.TabIndex = 15;
            this.label1.Text = "Capacidad máxima en Hojas de Ruta";
            // 
            // txtCapacidadMaxima
            // 
            this.txtCapacidadMaxima.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCapacidadMaxima.Location = new System.Drawing.Point(250, 51);
            this.txtCapacidadMaxima.Name = "txtCapacidadMaxima";
            this.txtCapacidadMaxima.ReadOnly = true;
            this.txtCapacidadMaxima.Size = new System.Drawing.Size(272, 23);
            this.txtCapacidadMaxima.TabIndex = 16;
            // 
            // txtCorreoSoporte
            // 
            this.txtCorreoSoporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoSoporte.Location = new System.Drawing.Point(250, 123);
            this.txtCorreoSoporte.Name = "txtCorreoSoporte";
            this.txtCorreoSoporte.ReadOnly = true;
            this.txtCorreoSoporte.Size = new System.Drawing.Size(272, 23);
            this.txtCorreoSoporte.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 40);
            this.label2.TabIndex = 17;
            this.label2.Text = "Correo electrónico de soporte";
            // 
            // txtDescuentoEnvios
            // 
            this.txtDescuentoEnvios.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuentoEnvios.Location = new System.Drawing.Point(250, 192);
            this.txtDescuentoEnvios.Name = "txtDescuentoEnvios";
            this.txtDescuentoEnvios.ReadOnly = true;
            this.txtDescuentoEnvios.Size = new System.Drawing.Size(272, 23);
            this.txtDescuentoEnvios.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(35, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 83);
            this.label3.TabIndex = 19;
            this.label3.Text = "Porcentaje de descuento en envíos de misma organización";
            // 
            // txtRolAdmin
            // 
            this.txtRolAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRolAdmin.Location = new System.Drawing.Point(250, 272);
            this.txtRolAdmin.Multiline = true;
            this.txtRolAdmin.Name = "txtRolAdmin";
            this.txtRolAdmin.ReadOnly = true;
            this.txtRolAdmin.Size = new System.Drawing.Size(272, 58);
            this.txtRolAdmin.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(35, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(177, 83);
            this.label4.TabIndex = 21;
            this.label4.Text = "Rol administrador de usuarios";
            // 
            // frmParametrosDelSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 535);
            this.Controls.Add(this.txtRolAdmin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDescuentoEnvios);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCorreoSoporte);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCapacidadMaxima);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmParametrosDelSistema";
            this.Text = "Parametros del Sistema";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCapacidadMaxima;
        private System.Windows.Forms.TextBox txtCorreoSoporte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDescuentoEnvios;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRolAdmin;
        private System.Windows.Forms.Label label4;
    }
}