using BLL;
using BLL.Vistas;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorAdministracionUsuarios
    {
        #region Variables locales

        UsuarioBLL usuarioBLL = new UsuarioBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();

        List<Control> controles = new List<Control>();
        Form formAlta;
        #endregion

        #region Formulario
        public ControladorAdministracionUsuarios(Form frmAbm, Form pFrmAlta)
        {
            formAlta = pFrmAlta;

            foreach (Control c in frmAbm.Controls)
            {
                controles.Add(c);
                if (c.Controls.Count > 0)
                {
                    foreach (Control c2 in c.Controls)
                    {
                        controles.Add(c2);
                    }
                }
            }

            frmAbm.Activated += FormLoad;
            frmAbm.VisibleChanged += VisibleChanged;
            ((Button)controles.Find(x => x.Name == "btnAlta")).Click += ClickAlta;
            ((Button)controles.Find(x => x.Name == "btnModificacion")).Click += ClickModificacion;
            ((Button)controles.Find(x => x.Name == "btnBaja")).Click += ClickBaja;
            ((Button)controles.Find(x => x.Name == "btnBuscar")).Click += ClickBuscar;
        }
        private void VisibleChanged(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
        }
        private void CargarGrilla(List<UsuarioVista> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridUsuarios");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = source;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "Id");
            }
        }

        private bool DatosValidos()
        {
            ServicioValidaciones.FormatoNumericoValido((TextBox)controles.Find(x => x.Name == "txtDNI"), "DNI");

            return true;
        }
        #endregion

        #region Eventos de controles
        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();
            CargarGrilla(usuarioBLL.ObtenerTodosParaVista());
        }

        private void ClickAlta(object sender, EventArgs e)
        {
            try
            {
                formAlta.Text = "Alta de Usuario";

                formAlta.ShowDialog();
                CargarGrilla(usuarioBLL.ObtenerTodosParaVista());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickBaja(object sender, EventArgs e)
        {
            try
            {
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridUsuarios").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                if (MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?",
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    usuarioBLL.Baja(usuarioBLL.Obtener(int.Parse(id)));
                    CargarGrilla(usuarioBLL.ObtenerTodosParaVista());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClickModificacion(object sender, EventArgs e)
        {
            try
            {
                var idUsuario = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridUsuarios").ToString();

                if (string.IsNullOrWhiteSpace(idUsuario))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                formAlta.Controls.Find("IdUsuario", false).FirstOrDefault().Text = idUsuario;

                formAlta.Text = "Modificación de Usuario";
                formAlta.ShowDialog();

                CargarGrilla(usuarioBLL.ObtenerTodosParaVista());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickBuscar(object sender, EventArgs e)
        {
            try
            {
                var dni = ((TextBox)controles.Find(x => x.Name == "txtDNI")).Text;
                if ((!string.IsNullOrWhiteSpace(dni) && DatosValidos()) || string.IsNullOrWhiteSpace(dni))
                {
                    var nombre = ((TextBox)controles.Find(x => x.Name == "txtNombre")).Text;
                    var nombreDeUsuario = ((TextBox)controles.Find(x => x.Name == "txtNombreDeUsuario")).Text;
                    var apellido = ((TextBox)controles.Find(x => x.Name == "txtApellido")).Text;
                    var correo = ((TextBox)controles.Find(x => x.Name == "txtCorreo")).Text;
                    var idUbicacion = ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedValue.ToString();

                    CargarGrilla(usuarioBLL.ObtenerTodosParaVista(nombre, apellido, dni, nombreDeUsuario, correo, int.Parse(idUbicacion)));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Metodos Privados
        private void CargarCombos()
        {
            var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");

            var ubicaciones = ubicacionBLL.ObtenerTodos();
            ubicaciones.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
            ubicaciones = ubicaciones.OrderBy(x => x.Id).ToList();

            comboUbicacion.DataSource = null;
            comboUbicacion.DataSource = ubicaciones;
            comboUbicacion.ValueMember = "Id";
            comboUbicacion.DisplayMember = "Descripcion";
            comboUbicacion.SelectedValue = 0;
        }
        #endregion
    }
}
