using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UI.Formularios.Administracion;
using UI.Formularios.Administracion.Articulo;
using UI.Formularios.Administracion.BultoCompuesto;
using UI.Formularios.Administracion.Categoria;
using UI.Formularios.Administracion.Color;
using UI.Formularios.Administracion.Stock;
using UI.Formularios.Administracion.Talle;
using UI.Formularios.Administracion.Ubicacion;
using UI.Formularios.Procesos.Ajuste_de_Stock;
using UI.Formularios.Procesos.Auditoria;
using UI.Formularios.Procesos.EnvioAClinica;
using UI.Formularios.Procesos.HojaDeRuta;
using UI.Formularios.Reportes.Auditorias;
using UI.Formularios.Reportes.EnviosALavadero;
using UI.Formularios.Reportes.EnviosInternos;
using UI.Formularios.Reportes.HojasDeRuta;
using UI.Formularios.Reportes.RecepcionEnLavadero;
using UI.Formularios.Seguridad;
using UI.Formularios.Seguridad.Roles;
using UI.Formularios.Seguridad.Usuarios;

namespace UI.Formularios
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            
            List<Form> menus = new List<Form>();
            menus.Add(new frmAdministracionTipoDePrenda());
            menus.Add(new frmAdministracionColor());
            menus.Add(new frmAdministracionTalle());
            menus.Add(new frmAdministracionUbicacion());
            menus.Add(new frmAdministracionArticulo());
            menus.Add(new frmAdministracionStock());
            menus.Add(new frmAdministracionCategoria());
            menus.Add(new frmAdministracionBultoCompuesto());

            menus.Add(new frmAuditoria());
            menus.Add(new frmEnvioAClinica());
            menus.Add(new frmEnvioALavadero());
            menus.Add(new frmEnvioInterno());
            menus.Add(new frmHojaDeRuta());
            menus.Add(new frmRecepcionEnLavadero());
            menus.Add(new frmRecepcionEnClinica());
            menus.Add(new frmAjusteStock());

            menus.Add(new frmReporteAuditoria());
            menus.Add(new frmReporteEnviosAClinica());
            menus.Add(new frmReporteEnviosALavadero());
            menus.Add(new frmReporteEnviosInternos());
            menus.Add(new frmReporteRecepcionesEnLavadero());
            menus.Add(new frmReporteRecepcionesEnClinica());
            menus.Add(new frmReporteHojasDeRuta());
            menus.Add(new frmReporteMovimientos());
            menus.Add(new frmReporteAjusteStock());

            menus.Add(new frmAdministracionRoles());
            menus.Add(new frmAdministracionUsuarios());

            menus.Add(new frmParametrosDelSistema());

            this.BackgroundImage = Properties.Resources.fondo;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Controlador.ControladorPrincipal ctrl = new Controlador.ControladorPrincipal(this, menus);
        }
    }
}
