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
    public class ControladorReporteMovimientos
    {
        #region Variables locales

        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        MovimientoBLL movimientoBLL = new MovimientoBLL();
        List<Control> controles = new List<Control>();
        Form form;
        #endregion

        #region Formulario
        public ControladorReporteMovimientos(Form frm)
        {
            form = frm;

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
        }

        private void VisibleChanged(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
        }

        private void CargarGrilla(List<MovimientoVista> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridMovimientos");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = source;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
            }
        }

        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();

            var controlFechaDesde = (DateTimePicker)controles.Find(x => x.Name == "dateTimeDesde");
            var controlFechaHasta = (DateTimePicker)controles.Find(x => x.Name == "dateTimeHasta");

            controlFechaDesde.Value = DateTime.Today.AddDays(-7);
            controlFechaHasta.Value = DateTime.Today;

            CargarGrilla(movimientoBLL.ObtenerMovimientosParaVista(
                fechaDesde: controlFechaDesde.Value, 
                fechaHasta: controlFechaHasta.Value.AddDays(1)));
        }
        #endregion

        #region Eventos de controles
        private void ClickBuscar(object sender, EventArgs e)
        {
            try
            {
                var idUbicacionOrigen = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen")).SelectedValue.ToString();
                var idUbicacionDestino = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino")).SelectedValue.ToString();
                var fechaDesde = ((DateTimePicker)controles.Find(x => x.Name == "dateTimeDesde")).Value;
                var fechaHasta = ((DateTimePicker)controles.Find(x => x.Name == "dateTimeHasta")).Value;
                fechaHasta = fechaHasta.AddDays(1); //se agrega 1 dia para que se busquen resultados incluidos en el mismo dia

                CargarGrilla(movimientoBLL.ObtenerMovimientosParaVista(
                    fechaDesde, 
                    fechaHasta,
                    int.Parse(idUbicacionOrigen),
                    int.Parse(idUbicacionDestino)));
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

            var ubicacionesOrigen = ubicacionBLL.ObtenerTodos().ToList();
            ubicacionesOrigen.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
            ubicacionesOrigen = ubicacionesOrigen.OrderBy(x => x.Id).ToList();

            comboUbicacionOrigen.DataSource = null;
            comboUbicacionOrigen.DataSource = ubicacionesOrigen;
            comboUbicacionOrigen.ValueMember = "Id";
            comboUbicacionOrigen.DisplayMember = "Descripcion";
            comboUbicacionOrigen.SelectedValue = 0;

            var ubicacionesDestino = ubicacionBLL.ObtenerTodos().ToList();
            ubicacionesDestino.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
            ubicacionesDestino = ubicacionesDestino.OrderBy(x => x.Id).ToList();

            comboUbicacionDestino.DataSource = null;
            comboUbicacionDestino.DataSource = ubicacionesDestino;
            comboUbicacionDestino.ValueMember = "Id";
            comboUbicacionDestino.DisplayMember = "Descripcion";
            comboUbicacionDestino.SelectedValue = 0;
        }

        #endregion

    }
}
