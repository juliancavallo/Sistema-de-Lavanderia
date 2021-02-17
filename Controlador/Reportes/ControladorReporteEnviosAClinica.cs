using BLL;
using BLL.Vistas;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorReporteEnviosAClinica
    {
        #region Variables locales

        EnvioBLL envioBLL = new EnvioBLL();
        EstadoEnvioBLL estadoEnvioBLL = new EstadoEnvioBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        List<Control> controles = new List<Control>();
        Form form;
        Form formDetalle;
        #endregion

        #region Formulario
        public ControladorReporteEnviosAClinica(Form frm, Form frmDetalle)
        {
            form = frm;
            formDetalle = frmDetalle;

            foreach (Control c in form.Controls)
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

            form.Activated += FormLoad;
            form.VisibleChanged += VisibleChanged;
            ((Button)controles.Find(x => x.Name == "btnBuscar")).Click += ClickBuscar;
            ((Button)controles.Find(x => x.Name == "btnVerDetalle")).Click += ClickDetalle;
        }

        private void VisibleChanged(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
        }

        private void CargarGrilla(List<EnvioVista> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridEnvios");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = source;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "Id");
            }
        }

        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();

            var controlFechaDesde = (DateTimePicker)controles.Find(x => x.Name == "dateTimeDesde");
            var controlFechaHasta = (DateTimePicker)controles.Find(x => x.Name == "dateTimeHasta");

            controlFechaDesde.Value = DateTime.Today.AddDays(-7);
            controlFechaHasta.Value = DateTime.Today;

            var ubicacionesPorDefecto = ubicacionBLL.ObtenerTodos(SeguridadBLL.usuarioLogueado.Ubicacion.Id).Where(x => !x.EsUbicacionInterna && x.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Lavanderia).ToList();

            CargarGrilla(envioBLL.ObtenerTodosParaVista(ubicacionesPorDefecto.Select(x => x.Id).ToList(),
                fechaDesde: controlFechaDesde.Value,
                fechaHasta: controlFechaHasta.Value.AddDays(1),
                tipo: (int)Entidades.Enums.TipoDeEnvio.AClinica));
        }
        #endregion

        #region Eventos de controles
        private void ClickBuscar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var nro = ((TextBox)controles.Find(x => x.Name == "txtNro")).Text;
                    int.TryParse(nro, out int result);

                    var idUbicacionOrigen = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen")).SelectedValue.ToString();
                    var idUbicacionDestino = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino")).SelectedValue.ToString();
                    var idEstado = ((ComboBox)controles.Find(x => x.Name == "comboEstado")).SelectedValue.ToString();
                    var fechaDesde = ((DateTimePicker)controles.Find(x => x.Name == "dateTimeDesde")).Value;
                    var fechaHasta = ((DateTimePicker)controles.Find(x => x.Name == "dateTimeHasta")).Value;
                    fechaHasta = fechaHasta.AddDays(1); //se agrega 1 dia para que se busquen resultados incluidos en el mismo dia

                    var ubicacionesPorDefecto = ubicacionBLL.ObtenerTodos(SeguridadBLL.usuarioLogueado.Ubicacion.Id).Where(x => !x.EsUbicacionInterna && x.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Lavanderia).ToList();

                    CargarGrilla(envioBLL.ObtenerTodosParaVista(ubicacionesPorDefecto.Select(x => x.Id).ToList(),
                        int.Parse(idUbicacionOrigen), int.Parse(idUbicacionDestino),
                        fechaDesde, fechaHasta, result, int.Parse(idEstado), (int)Entidades.Enums.TipoDeEnvio.AClinica));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ClickDetalle(object sender, EventArgs e)
        {
            try
            {
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridEnvios").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");


                formDetalle.Controls.Find("IdEnvio", false).FirstOrDefault().Text = id;

                ServicioConfiguracionDeControles.ConfigurarFormDetalle(formDetalle);
                formDetalle.ShowDialog();
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
            var comboUbicacionOrigen = (ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen");
            var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");

            var ubicacionesOrigen = ubicacionBLL.ObtenerTodos(SeguridadBLL.usuarioLogueado.Ubicacion.Id).Where(x => !x.EsUbicacionInterna && x.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Lavanderia).ToList();
            ubicacionesOrigen.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
            ubicacionesOrigen = ubicacionesOrigen.OrderBy(x => x.Id).ToList();

            comboUbicacionOrigen.DataSource = null;
            comboUbicacionOrigen.DataSource = ubicacionesOrigen;
            comboUbicacionOrigen.ValueMember = "Id";
            comboUbicacionOrigen.DisplayMember = "Descripcion";
            comboUbicacionOrigen.SelectedValue = 0;

            var ubicacionesDestino = ubicacionBLL.ObtenerTodos().Where(x => !x.EsUbicacionInterna && x.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Clinica).ToList();
            ubicacionesDestino.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
            ubicacionesDestino = ubicacionesDestino.OrderBy(x => x.Id).ToList();

            comboUbicacionDestino.DataSource = null;
            comboUbicacionDestino.DataSource = ubicacionesDestino;
            comboUbicacionDestino.ValueMember = "Id";
            comboUbicacionDestino.DisplayMember = "Descripcion";
            comboUbicacionDestino.SelectedValue = 0;


            var comboEstado = (ComboBox)controles.Find(x => x.Name == "comboEstado");

            var estados = estadoEnvioBLL.ObtenerTodos().ToList();
            estados.Add(new EstadoEnvio() { Id = 0, Descripcion = "Seleccionar..." });
            estados = estados.OrderBy(x => x.Id).ToList();

            comboEstado.DataSource = null;
            comboEstado.DataSource = estados;
            comboEstado.ValueMember = "Id";
            comboEstado.DisplayMember = "Descripcion";
            comboEstado.SelectedValue = 0;
        }

        private bool DatosValidos()
        {
            var txtNro = (TextBox)controles.Find(x => x.Name == "txtNro");
            if (!string.IsNullOrWhiteSpace(txtNro.Text))
            ServicioValidaciones.FormatoNumericoValido(txtNro, "Nro. de Envio");

            return true;
        }
        #endregion

    }
}
