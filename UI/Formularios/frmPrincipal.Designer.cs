namespace UI.Formularios
{
    partial class frmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.menuInicio = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAdministracion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTiposDePrenda = new System.Windows.Forms.ToolStripMenuItem();
            this.menuColores = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTalles = new System.Windows.Forms.ToolStripMenuItem();
            this.menuArticulos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUbicaciones = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStock = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCategoria = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProcesos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAuditorias = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEnviosAClinica = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEnviosInternos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEnviosALavadero = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHojasDeRuta = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecepcionesEnLavadero = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRecepcionesEnClinica = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAjusteStock = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteMovimientos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteAuditoria = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteEnviosAClinica = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteEnviosALavadero = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteEnviosInternos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteHojasDeRuta = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteRecepcionesEnClinica = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteRecepcionesEnLavadero = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReporteAjusteStock = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSeguridad = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRoles = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBultosCompuestos = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInicio,
            this.menuAdministracion,
            this.menuProcesos,
            this.menuReportes,
            this.menuSeguridad,
            this.menuCerrarSesion});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(784, 24);
            this.menuPrincipal.TabIndex = 0;
            this.menuPrincipal.Text = "menuStrip1";
            // 
            // menuInicio
            // 
            this.menuInicio.Name = "menuInicio";
            this.menuInicio.Size = new System.Drawing.Size(48, 20);
            this.menuInicio.Text = "Inicio";
            // 
            // menuAdministracion
            // 
            this.menuAdministracion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTiposDePrenda,
            this.menuColores,
            this.menuTalles,
            this.menuArticulos,
            this.menuUbicaciones,
            this.menuStock,
            this.menuCategoria,
            this.menuBultosCompuestos});
            this.menuAdministracion.Name = "menuAdministracion";
            this.menuAdministracion.Size = new System.Drawing.Size(100, 20);
            this.menuAdministracion.Tag = "C1";
            this.menuAdministracion.Text = "Administracion";
            // 
            // menuTiposDePrenda
            // 
            this.menuTiposDePrenda.Name = "menuTiposDePrenda";
            this.menuTiposDePrenda.Size = new System.Drawing.Size(180, 22);
            this.menuTiposDePrenda.Tag = "S1";
            this.menuTiposDePrenda.Text = "Tipos de Prenda";
            // 
            // menuColores
            // 
            this.menuColores.Name = "menuColores";
            this.menuColores.Size = new System.Drawing.Size(180, 22);
            this.menuColores.Tag = "S3";
            this.menuColores.Text = "Colores";
            // 
            // menuTalles
            // 
            this.menuTalles.Name = "menuTalles";
            this.menuTalles.Size = new System.Drawing.Size(180, 22);
            this.menuTalles.Tag = "S4";
            this.menuTalles.Text = "Talles";
            // 
            // menuArticulos
            // 
            this.menuArticulos.Name = "menuArticulos";
            this.menuArticulos.Size = new System.Drawing.Size(180, 22);
            this.menuArticulos.Tag = "S2";
            this.menuArticulos.Text = "Articulos";
            // 
            // menuUbicaciones
            // 
            this.menuUbicaciones.Name = "menuUbicaciones";
            this.menuUbicaciones.Size = new System.Drawing.Size(180, 22);
            this.menuUbicaciones.Tag = "S16";
            this.menuUbicaciones.Text = "Ubicaciones";
            // 
            // menuStock
            // 
            this.menuStock.Name = "menuStock";
            this.menuStock.Size = new System.Drawing.Size(180, 22);
            this.menuStock.Tag = "S12";
            this.menuStock.Text = "Stock";
            // 
            // menuCategoria
            // 
            this.menuCategoria.Name = "menuCategoria";
            this.menuCategoria.Size = new System.Drawing.Size(180, 22);
            this.menuCategoria.Tag = "S26";
            this.menuCategoria.Text = "Categoría";
            // 
            // menuProcesos
            // 
            this.menuProcesos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAuditorias,
            this.menuEnviosAClinica,
            this.menuEnviosInternos,
            this.menuEnviosALavadero,
            this.menuHojasDeRuta,
            this.menuRecepcionesEnLavadero,
            this.menuRecepcionesEnClinica,
            this.menuAjusteStock});
            this.menuProcesos.Name = "menuProcesos";
            this.menuProcesos.Size = new System.Drawing.Size(66, 20);
            this.menuProcesos.Tag = "C2";
            this.menuProcesos.Text = "Procesos";
            // 
            // menuAuditorias
            // 
            this.menuAuditorias.Name = "menuAuditorias";
            this.menuAuditorias.Size = new System.Drawing.Size(207, 22);
            this.menuAuditorias.Tag = "S15";
            this.menuAuditorias.Text = "Auditorias";
            // 
            // menuEnviosAClinica
            // 
            this.menuEnviosAClinica.Name = "menuEnviosAClinica";
            this.menuEnviosAClinica.Size = new System.Drawing.Size(207, 22);
            this.menuEnviosAClinica.Tag = "S5";
            this.menuEnviosAClinica.Text = "Envios a Clinica";
            // 
            // menuEnviosInternos
            // 
            this.menuEnviosInternos.Name = "menuEnviosInternos";
            this.menuEnviosInternos.Size = new System.Drawing.Size(207, 22);
            this.menuEnviosInternos.Tag = "S6";
            this.menuEnviosInternos.Text = "Envios Internos";
            // 
            // menuEnviosALavadero
            // 
            this.menuEnviosALavadero.Name = "menuEnviosALavadero";
            this.menuEnviosALavadero.Size = new System.Drawing.Size(207, 22);
            this.menuEnviosALavadero.Tag = "S7";
            this.menuEnviosALavadero.Text = "Envios a Lavadero";
            // 
            // menuHojasDeRuta
            // 
            this.menuHojasDeRuta.Name = "menuHojasDeRuta";
            this.menuHojasDeRuta.Size = new System.Drawing.Size(207, 22);
            this.menuHojasDeRuta.Tag = "S8";
            this.menuHojasDeRuta.Text = "Hojas de Ruta";
            // 
            // menuRecepcionesEnLavadero
            // 
            this.menuRecepcionesEnLavadero.Name = "menuRecepcionesEnLavadero";
            this.menuRecepcionesEnLavadero.Size = new System.Drawing.Size(207, 22);
            this.menuRecepcionesEnLavadero.Tag = "S9";
            this.menuRecepcionesEnLavadero.Text = "Recepciones en Lavadero";
            // 
            // menuRecepcionesEnClinica
            // 
            this.menuRecepcionesEnClinica.Name = "menuRecepcionesEnClinica";
            this.menuRecepcionesEnClinica.Size = new System.Drawing.Size(207, 22);
            this.menuRecepcionesEnClinica.Tag = "S10";
            this.menuRecepcionesEnClinica.Text = "Recepciones en Clinica";
            // 
            // menuAjusteStock
            // 
            this.menuAjusteStock.Name = "menuAjusteStock";
            this.menuAjusteStock.Size = new System.Drawing.Size(207, 22);
            this.menuAjusteStock.Tag = "S24";
            this.menuAjusteStock.Text = "Ajuste de Stock";
            // 
            // menuReportes
            // 
            this.menuReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuReporteMovimientos,
            this.menuReporteAuditoria,
            this.menuReporteEnviosAClinica,
            this.menuReporteEnviosALavadero,
            this.menuReporteEnviosInternos,
            this.menuReporteHojasDeRuta,
            this.menuReporteRecepcionesEnClinica,
            this.menuReporteRecepcionesEnLavadero,
            this.menuReporteAjusteStock});
            this.menuReportes.Name = "menuReportes";
            this.menuReportes.Size = new System.Drawing.Size(65, 20);
            this.menuReportes.Tag = "C3";
            this.menuReportes.Text = "Reportes";
            // 
            // menuReporteMovimientos
            // 
            this.menuReporteMovimientos.Name = "menuReporteMovimientos";
            this.menuReporteMovimientos.Size = new System.Drawing.Size(207, 22);
            this.menuReporteMovimientos.Tag = "S11";
            this.menuReporteMovimientos.Text = "Movimientos";
            // 
            // menuReporteAuditoria
            // 
            this.menuReporteAuditoria.Name = "menuReporteAuditoria";
            this.menuReporteAuditoria.Size = new System.Drawing.Size(207, 22);
            this.menuReporteAuditoria.Tag = "S23";
            this.menuReporteAuditoria.Text = "Auditorias";
            // 
            // menuReporteEnviosAClinica
            // 
            this.menuReporteEnviosAClinica.Name = "menuReporteEnviosAClinica";
            this.menuReporteEnviosAClinica.Size = new System.Drawing.Size(207, 22);
            this.menuReporteEnviosAClinica.Tag = "S17";
            this.menuReporteEnviosAClinica.Text = "Envios a Clinica";
            // 
            // menuReporteEnviosALavadero
            // 
            this.menuReporteEnviosALavadero.Name = "menuReporteEnviosALavadero";
            this.menuReporteEnviosALavadero.Size = new System.Drawing.Size(207, 22);
            this.menuReporteEnviosALavadero.Tag = "S18";
            this.menuReporteEnviosALavadero.Text = "Envios a Lavadero";
            // 
            // menuReporteEnviosInternos
            // 
            this.menuReporteEnviosInternos.Name = "menuReporteEnviosInternos";
            this.menuReporteEnviosInternos.Size = new System.Drawing.Size(207, 22);
            this.menuReporteEnviosInternos.Tag = "S19";
            this.menuReporteEnviosInternos.Text = "Envios internos";
            // 
            // menuReporteHojasDeRuta
            // 
            this.menuReporteHojasDeRuta.Name = "menuReporteHojasDeRuta";
            this.menuReporteHojasDeRuta.Size = new System.Drawing.Size(207, 22);
            this.menuReporteHojasDeRuta.Tag = "S22";
            this.menuReporteHojasDeRuta.Text = "Hojas de Ruta";
            // 
            // menuReporteRecepcionesEnClinica
            // 
            this.menuReporteRecepcionesEnClinica.Name = "menuReporteRecepcionesEnClinica";
            this.menuReporteRecepcionesEnClinica.Size = new System.Drawing.Size(207, 22);
            this.menuReporteRecepcionesEnClinica.Tag = "S20";
            this.menuReporteRecepcionesEnClinica.Text = "Recepciones en Clinica";
            // 
            // menuReporteRecepcionesEnLavadero
            // 
            this.menuReporteRecepcionesEnLavadero.Name = "menuReporteRecepcionesEnLavadero";
            this.menuReporteRecepcionesEnLavadero.Size = new System.Drawing.Size(207, 22);
            this.menuReporteRecepcionesEnLavadero.Tag = "S21";
            this.menuReporteRecepcionesEnLavadero.Text = "Recepciones en Lavadero";
            // 
            // menuReporteAjusteStock
            // 
            this.menuReporteAjusteStock.Name = "menuReporteAjusteStock";
            this.menuReporteAjusteStock.Size = new System.Drawing.Size(207, 22);
            this.menuReporteAjusteStock.Tag = "S25";
            this.menuReporteAjusteStock.Text = "Ajuste de Stock";
            // 
            // menuSeguridad
            // 
            this.menuSeguridad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRoles,
            this.menuUsuarios});
            this.menuSeguridad.Name = "menuSeguridad";
            this.menuSeguridad.Size = new System.Drawing.Size(72, 20);
            this.menuSeguridad.Tag = "C4";
            this.menuSeguridad.Text = "Seguridad";
            // 
            // menuRoles
            // 
            this.menuRoles.Name = "menuRoles";
            this.menuRoles.Size = new System.Drawing.Size(119, 22);
            this.menuRoles.Tag = "S13";
            this.menuRoles.Text = "Roles";
            // 
            // menuUsuarios
            // 
            this.menuUsuarios.Name = "menuUsuarios";
            this.menuUsuarios.Size = new System.Drawing.Size(119, 22);
            this.menuUsuarios.Tag = "S14";
            this.menuUsuarios.Text = "Usuarios";
            // 
            // menuCerrarSesion
            // 
            this.menuCerrarSesion.Name = "menuCerrarSesion";
            this.menuCerrarSesion.Size = new System.Drawing.Size(88, 20);
            this.menuCerrarSesion.Text = "Cerrar Sesion";
            // 
            // menuBultosCompuestos
            // 
            this.menuBultosCompuestos.Name = "menuBultosCompuestos";
            this.menuBultosCompuestos.Size = new System.Drawing.Size(180, 22);
            this.menuBultosCompuestos.Tag = "S27";
            this.menuBultosCompuestos.Text = "Bultos Compuestos";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(784, 510);
            this.Controls.Add(this.menuPrincipal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuPrincipal;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem menuAdministracion;
        private System.Windows.Forms.ToolStripMenuItem menuTiposDePrenda;
        private System.Windows.Forms.ToolStripMenuItem menuArticulos;
        private System.Windows.Forms.ToolStripMenuItem menuColores;
        private System.Windows.Forms.ToolStripMenuItem menuTalles;
        private System.Windows.Forms.ToolStripMenuItem menuProcesos;
        private System.Windows.Forms.ToolStripMenuItem menuEnviosAClinica;
        private System.Windows.Forms.ToolStripMenuItem menuHojasDeRuta;
        private System.Windows.Forms.ToolStripMenuItem menuRecepcionesEnLavadero;
        private System.Windows.Forms.ToolStripMenuItem menuReportes;
        private System.Windows.Forms.ToolStripMenuItem menuReporteMovimientos;
        private System.Windows.Forms.ToolStripMenuItem menuSeguridad;
        private System.Windows.Forms.ToolStripMenuItem menuRoles;
        private System.Windows.Forms.ToolStripMenuItem menuUsuarios;
        private System.Windows.Forms.ToolStripMenuItem menuEnviosInternos;
        private System.Windows.Forms.ToolStripMenuItem menuEnviosALavadero;
        private System.Windows.Forms.ToolStripMenuItem menuRecepcionesEnClinica;
        private System.Windows.Forms.ToolStripMenuItem menuInicio;
        private System.Windows.Forms.ToolStripMenuItem menuCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem menuAuditorias;
        private System.Windows.Forms.ToolStripMenuItem menuUbicaciones;
        private System.Windows.Forms.ToolStripMenuItem menuStock;
        private System.Windows.Forms.ToolStripMenuItem menuReporteAuditoria;
        private System.Windows.Forms.ToolStripMenuItem menuReporteEnviosAClinica;
        private System.Windows.Forms.ToolStripMenuItem menuReporteEnviosALavadero;
        private System.Windows.Forms.ToolStripMenuItem menuReporteEnviosInternos;
        private System.Windows.Forms.ToolStripMenuItem menuReporteHojasDeRuta;
        private System.Windows.Forms.ToolStripMenuItem menuReporteRecepcionesEnClinica;
        private System.Windows.Forms.ToolStripMenuItem menuReporteRecepcionesEnLavadero;
        private System.Windows.Forms.ToolStripMenuItem menuAjusteStock;
        private System.Windows.Forms.ToolStripMenuItem menuReporteAjusteStock;
        private System.Windows.Forms.ToolStripMenuItem menuCategoria;
        private System.Windows.Forms.ToolStripMenuItem menuBultosCompuestos;
    }
}

