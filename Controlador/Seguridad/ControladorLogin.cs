using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorLogin
    {
        #region Variables globales
        List<Control> controles = new List<Control>();
        Form frm;
        Form frmPrincipal;

        SeguridadBLL seguridadBLL = new SeguridadBLL();
        UsuarioBLL usuarioBLL = new UsuarioBLL();

        ServicioSeguridad servicioSeguridad = new ServicioSeguridad();

        Dictionary<string, int> intentosPorUsuario = new Dictionary<string, int>();
        #endregion

        #region Formulario
        public ControladorLogin(Form frm, Form frmPrincipal)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(frm);

            this.frm = frm;
            this.frmPrincipal = frmPrincipal;

            foreach (Control control in frm.Controls)
            {
                controles.Add(control);
            }

            ((Button)controles.Find(x => x.Name == "btnIngresar")).Click += ClickIngresar;
            ((Button)controles.Find(x => x.Name == "btnSalir")).Click += ClickSalir;
            this.frm.Load += FormLoad;

            ServicioConfiguracionDeControles.ConfigurarTextBoxContraseña((TextBox)controles.Find(x => x.Name == "txtContraseña"));
        }

        private void FormLoad(object sender, EventArgs e)
        {
            seguridadBLL.GenerarDataSet();
        }
        #endregion

        #region Eventos de controles
        private void ClickSalir(object sender, EventArgs e)
        {
            frm.Close();
        }

        private void ClickIngresar(object sender, EventArgs e)
        {
            try
            {
                var nombreDeUsuario = ((TextBox)controles.Find(x => x.Name == "txtUsuario")).Text;
                var contraseña = servicioSeguridad.Encriptar(((TextBox)controles.Find(x => x.Name == "txtContraseña")).Text);

                if (string.IsNullOrWhiteSpace(nombreDeUsuario) || string.IsNullOrWhiteSpace(contraseña))
                {
                    ((TextBox)controles.Find(x => x.Name == "txtUsuario")).Focus();
                    throw new Exception("Debe completar todos los campos");
                }
                if (!seguridadBLL.NombreDeUsuarioValido(nombreDeUsuario))
                {
                    ((TextBox)controles.Find(x => x.Name == "txtUsuario")).Focus();
                    ServicioConfiguracionDeControles.LimpiarControles(this.controles);
                    throw new Exception("El usuario no existe");
                }

                if (seguridadBLL.ContraseñaValida(nombreDeUsuario, contraseña))
                {
                    SeguridadBLL.usuarioLogueado = usuarioBLL.Obtener(nombreDeUsuario);
                    ServicioConfiguracionDeControles.LimpiarControles(this.controles);

                    frm.Hide();
                    frmPrincipal.ShowDialog();
                    frm.Show();
                }
                else
                {
                    if (!intentosPorUsuario.ContainsKey(nombreDeUsuario))
                        intentosPorUsuario.Add(nombreDeUsuario, 0);

                    ServicioConfiguracionDeControles.LimpiarControles(this.controles);
                    intentosPorUsuario[nombreDeUsuario] += 1;

                    if (intentosPorUsuario[nombreDeUsuario] == 5)
                    { 
                        this.ReestablecerContraseña(nombreDeUsuario);
                        intentosPorUsuario[nombreDeUsuario] = 0;
                    }
                    else
                    {
                        ((TextBox)controles.Find(x => x.Name == "txtUsuario")).Focus();
                        throw new Exception("La contraseña es incorrecta");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReestablecerContraseña(string nombreDeUsuario)
        {
            MessageBox.Show(@"Ha superado el limite máximo de intentos de " +
                       "inicio de sesión para el usuario " + nombreDeUsuario + ". " +
                       "Le enviaremos a su correo su nueva contraseña," +
                       "la cual recomendamos cambiar la proxima vez que ingrese.",
                           "Advertencia",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Warning);

            seguridadBLL.ReestablecerContraseña(nombreDeUsuario);
            seguridadBLL.EnviarCorreoRecuperoDeContraseña(usuarioBLL.Obtener(nombreDeUsuario));
            ((TextBox)controles.Find(x => x.Name == "txtUsuario")).Focus();

        }
        #endregion
    }
}
