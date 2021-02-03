using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorAltaUsuarios
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        int? idUsuario = null;
        
        RolBLL rolBLL = new RolBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        ServicioSeguridad servicioSeguridad = new ServicioSeguridad();

        #endregion

        #region Formulario
        public ControladorAltaUsuarios(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdUsuario", Visible = false });

            frm = pForm;

            foreach (Control control in frm.Controls)
            {
                controles.Add(control);
            }

            pForm.Load += FormLoad;
            pForm.FormClosed += FormClosed;
            ((Button)controles.Find(x => x.Name == "btnAceptar")).Click += ClickAceptar;
            ((Button)controles.Find(x => x.Name == "btnCancelar")).Click += ClickCancelar;
        }

        private bool DatosValidos()
        {
            if (controles.Find(x => x.Name == "txtContraseña").Text != controles.Find(x => x.Name == "txtConfirmarContraseña").Text)
                throw new Exception("Las contraseñas deben coincidir");

            if (controles.Find(x => x.Name == "txtContraseña").Text.Length < 8)
                throw new Exception("La contraseña debe tener 8 caracteres como minimo");

            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtNombre"), "Nombre");
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtApellido"), "Apellido");
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtNombreDeUsuario"), "Nombre de Usuario");
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtDNI"), "DNI");
            ServicioValidaciones.CorreoValido((TextBox)controles.Find(x => x.Name == "txtCorreo"), "Correo");
            ServicioValidaciones.FormatoNumericoValido((TextBox)controles.Find(x => x.Name == "txtDNI"), "DNI");
            ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboRoles"), "Roles");
            ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacion"), "Ubicacion");

            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();
            Control ctrlOculto = controles.Find(x => x.Name == "IdUsuario");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idUsuario = result;

            if (idUsuario.HasValue)
            {
                var usuario = usuarioBLL.Obtener(idUsuario.Value);

                if (usuario != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtNombre")).Text = usuario.Nombre;
                    ((TextBox)controles.Find(x => x.Name == "txtApellido")).Text = usuario.Apellido;
                    ((TextBox)controles.Find(x => x.Name == "txtNombreDeUsuario")).Text = usuario.NombreDeUsuario;
                    ((TextBox)controles.Find(x => x.Name == "txtDNI")).Text = usuario.DNI.ToString();
                    ((TextBox)controles.Find(x => x.Name == "txtContraseña")).Text = servicioSeguridad.Desencriptar(usuario.Contraseña);
                    ((TextBox)controles.Find(x => x.Name == "txtConfirmarContraseña")).Text = servicioSeguridad.Desencriptar(usuario.Contraseña);
                    ((TextBox)controles.Find(x => x.Name == "txtCorreo")).Text = usuario.Correo;
                    ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedValue = usuario.Ubicacion.Id;
                    ((ComboBox)controles.Find(x => x.Name == "comboRoles")).SelectedValue = usuario.Rol.Id;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idUsuario = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var usuario = new Entidades.Usuario();
                    usuario.Id = idUsuario ?? 0;
                    usuario.Nombre = ((TextBox)controles.Find(x => x.Name == "txtNombre")).Text;
                    usuario.Apellido = ((TextBox)controles.Find(x => x.Name == "txtApellido")).Text;
                    usuario.DNI = int.Parse(((TextBox)controles.Find(x => x.Name == "txtDNI")).Text);
                    usuario.NombreDeUsuario = ((TextBox)controles.Find(x => x.Name == "txtNombreDeUsuario")).Text;
                    usuario.Contraseña = servicioSeguridad.Encriptar(((TextBox)controles.Find(x => x.Name == "txtContraseña")).Text);
                    usuario.Correo = ((TextBox)controles.Find(x => x.Name == "txtCorreo")).Text;

                    string idRolSeleccionado = ((ComboBox)controles.Find(x => x.Name == "comboRoles")).SelectedValue.ToString();
                    usuario.Rol = rolBLL.Obtener(int.Parse(idRolSeleccionado));

                    string idUbicacionSeleccionado = ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedValue.ToString();
                    usuario.Ubicacion = ubicacionBLL.Obtener(int.Parse(idUbicacionSeleccionado));

                    if (idUsuario.HasValue)
                    {
                        usuarioBLL.Modificacion(usuario);
                        MessageBox.Show("Recuerde que si modificó al usuario con el que está logueado, debe cerrar sesión y volver a iniciar para ver reflejados los cambios", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        usuarioBLL.Alta(usuario);

                    frm.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ClickCancelar(object sender, EventArgs e)
        {
            frm.Close();
        }

        #endregion

        #region Metodos Privados
        private void CargarCombos()
        {
            ComboBox comboRoles = (ComboBox)controles.Find(x => x.Name == "comboRoles");
            comboRoles.DataSource = null;
            comboRoles.DataSource = rolBLL.ObtenerTodos();
            comboRoles.ValueMember = "Id";
            comboRoles.DisplayMember = "Descripcion";

            var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");
            var ubicaciones = ubicacionBLL.ObtenerTodos();
            ubicaciones = ubicaciones.OrderBy(x => x.Id).ToList();

            comboUbicacion.DataSource = null;
            comboUbicacion.DataSource = ubicaciones;
            comboUbicacion.ValueMember = "Id";
            comboUbicacion.DisplayMember = "Descripcion";
        }
        #endregion
    }
}
