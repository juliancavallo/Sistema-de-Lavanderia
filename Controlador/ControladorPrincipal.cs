using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Servicios;
using BLL;
using Entidades;
using Entidades.Enums;

namespace Controlador
{
    public class ControladorPrincipal
    {
        #region Variables globales
        Form frmPrincipal;
        List<Form> menus;
        List<ToolStripMenuItem> controles = new List<ToolStripMenuItem>();
        bool cerrarSesion;

        UsuarioBLL usuarioBLL = new UsuarioBLL();
        MenuBLL menuBLL = new MenuBLL();
        SeguridadBLL seguridadBLL = new SeguridadBLL();
        #endregion

        #region Formulario
        public ControladorPrincipal(Form pForm, List<Form> pMenus)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);

            frmPrincipal = pForm;
            menus = pMenus;

            var menuPrincipal = (MenuStrip)pForm.Controls.Find("menuPrincipal", false).First();

            //Se recorren los 4 menus principales (Adm, Procesos, Reportes y Seguridad)
            foreach (ToolStripMenuItem control in menuPrincipal.Items)
            {
                controles.Add(control);
                //Se recorren todos los items de cada menu
                foreach (ToolStripMenuItem menu in control.DropDownItems)
                {
                    controles.Add(menu);
                }
            }

            frmPrincipal.Load += FormLoad;
            frmPrincipal.FormClosed += FormClosed;

            //Administracion
            controles.Find(x => x.Name == NombreMenu.TiposDePrenda).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionTipoDePrenda", CodigoMenu.TiposDePrenda); };
            controles.Find(x => x.Name == NombreMenu.Colores).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionColor", CodigoMenu.Colores); };
            controles.Find(x => x.Name == NombreMenu.Talles).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionTalle", CodigoMenu.Talles); };
            controles.Find(x => x.Name == NombreMenu.Ubicaciones).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionUbicacion", CodigoMenu.Ubicaciones); };
            controles.Find(x => x.Name == NombreMenu.Articulos).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionArticulo", CodigoMenu.Articulos); };
            controles.Find(x => x.Name == NombreMenu.Stock).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionStock", CodigoMenu.Stock); };
            controles.Find(x => x.Name == NombreMenu.Categoria).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionCategoria", CodigoMenu.Categoria); };
            controles.Find(x => x.Name == NombreMenu.BultoCompuesto).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionBultoCompuesto", CodigoMenu.BultoCompuesto); };

            //Procesos
            controles.Find(x => x.Name == NombreMenu.Auditorias).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAuditoria", CodigoMenu.Auditorias); };
            controles.Find(x => x.Name == NombreMenu.EnviosAClinica).Click += delegate (object sender, EventArgs e) { ClickMenu("frmEnvioAClinica", CodigoMenu.EnviosAClinica); };
            controles.Find(x => x.Name == NombreMenu.EnviosALavadero).Click += delegate (object sender, EventArgs e) { ClickMenu("frmEnvioALavadero", CodigoMenu.EnviosALavadero); };
            controles.Find(x => x.Name == NombreMenu.EnviosInternos).Click += delegate (object sender, EventArgs e) { ClickMenu("frmEnvioInterno", CodigoMenu.EnviosInternos); };
            controles.Find(x => x.Name == NombreMenu.HojasDeRuta).Click += delegate (object sender, EventArgs e) { ClickMenu("frmHojaDeRuta", CodigoMenu.HojasDeRuta); };
            controles.Find(x => x.Name == NombreMenu.RecepcionesEnLavadero).Click += delegate (object sender, EventArgs e) { ClickMenu("frmRecepcionEnLavadero", CodigoMenu.RecepcionesEnLavadero); };
            controles.Find(x => x.Name == NombreMenu.RecepcionesEnClinica).Click += delegate (object sender, EventArgs e) { ClickMenu("frmRecepcionEnClinica", CodigoMenu.RecepcionesEnClinica); };
            controles.Find(x => x.Name == NombreMenu.AjusteStock).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAjusteStock", CodigoMenu.AjusteStock); };

            //Reportes
            controles.Find(x => x.Name == NombreMenu.ReporteAuditoria).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteAuditoria", CodigoMenu.ReporteAuditorias); };
            controles.Find(x => x.Name == NombreMenu.ReporteEnviosAClinica).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteEnviosAClinica", CodigoMenu.ReporteEnviosAClinica); };
            controles.Find(x => x.Name == NombreMenu.ReporteEnviosALavadero).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteEnviosALavadero", CodigoMenu.ReporteEnviosALavadero); };
            controles.Find(x => x.Name == NombreMenu.ReporteEnviosInternos).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteEnviosInternos", CodigoMenu.ReporteEnviosInternos); };
            controles.Find(x => x.Name == NombreMenu.ReporteRecepcionesEnLavadero).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteRecepcionesEnLavadero", CodigoMenu.ReporteEnviosALavadero); };
            controles.Find(x => x.Name == NombreMenu.ReporteRecepcionesEnClinica).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteRecepcionesEnClinica", CodigoMenu.ReporteRecepcionesEnClinica); };
            controles.Find(x => x.Name == NombreMenu.ReporteHojasDeRuta).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteHojasDeRuta", CodigoMenu.ReporteHojasDeRuta); };
            controles.Find(x => x.Name == NombreMenu.ReporteMovimientos).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteMovimientos", CodigoMenu.ReporteMovimientos); };
            controles.Find(x => x.Name == NombreMenu.ReporteAjusteStock).Click += delegate (object sender, EventArgs e) { ClickMenu("frmReporteAjusteStock", CodigoMenu.ReporteAjusteStock); };

            //Seguridad
            controles.Find(x => x.Name == NombreMenu.Roles).Click += delegate(object sender, EventArgs e) { ClickMenu("frmAdministracionRoles", CodigoMenu.Roles); };
            controles.Find(x => x.Name == NombreMenu.Usuarios).Click += delegate (object sender, EventArgs e) { ClickMenu("frmAdministracionUsuarios", CodigoMenu.Usuarios); }; ;

            //General
            controles.Find(x => x.Name == NombreMenu.Inicio).Click += ClickMenuPrincipal;
            controles.Find(x => x.Name == NombreMenu.CerrarSesion).Click += ClickCerrarSesion;
        }

        private void FormClosed(object sender, FormClosedEventArgs e)
        {
            seguridadBLL.HacerBackup();
            if (!cerrarSesion)
                Application.Exit();
        }

        private void FormLoad(object sender, EventArgs e)
        {
            cerrarSesion = false;
            this.ConfigurarMenusPorPermiso(controles, SeguridadBLL.usuarioLogueado);
        }

        #endregion

        #region Eventos de controles
        
        private void ClickMenu(string nombreMenu, string codigoMenu)
        {
            try
            {
                //Se vuelve a obtener el usuario logueado, ya que si se modifica algun rol no se verá reflejado en la propiedad usuarioLogueado de SeguridadBLL
                var usuarioLogueado = usuarioBLL.Obtener(SeguridadBLL.usuarioLogueado.Id);
                var menusUsuarioLogueado = menuBLL.ObtenerNeto(usuarioLogueado.Menus);

                if (!menusUsuarioLogueado.Select(x => x.Codigo).Contains(codigoMenu))
                    throw new Exception("El usuario no tiene el permiso necesario para acceder al modulo");

                var frm = menus.Where(x => x.Name == nombreMenu).FirstOrDefault();

                if (frm != null)
                {
                    menus.ForEach(x => x.Hide());
                    ServicioConfiguracionDeControles.ConfigurarHijoMDI(frm, frmPrincipal);
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClickMenuPrincipal(object sender, EventArgs e)
        {
            menus.ForEach(x => x.Hide());
        }

        private void ClickCerrarSesion(object sender, EventArgs e)
        {
            menus.ForEach(x => x.Hide());
            cerrarSesion = true;
            frmPrincipal.Close();
        }
        #endregion

        #region Metodos Privados
        public void ConfigurarMenusPorPermiso(List<ToolStripMenuItem> controles, Usuario usuario)
        {
            if (usuario != null)
            {
                controles = controles.Where(x => x.Tag != null).ToList();
                controles.ForEach(x => x.Visible = false);

                var menus = menuBLL.ObtenerNeto(usuario.Rol.Menus);

                foreach (var control in controles)
                {
                    control.Visible = menus.Select(x => x.Codigo).Contains(control.Tag.ToString());
                }
            }
        }
        #endregion
    }
}
