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
    public class ControladorReporteAuditoria
    {
        #region Variables locales

        AuditoriaBLL auditoriaBLL = new AuditoriaBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        List<Control> controles = new List<Control>();
        Form form;
        Form formDetalle;
        #endregion

        #region Formulario
        public ControladorReporteAuditoria(Form frm, Form frmDetalle)
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

        private void CargarGrilla(List<AuditoriaVista> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridAuditorias");
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

            CargarGrilla(auditoriaBLL.ObtenerTodosParaVista(
                fechaDesde: controlFechaDesde.Value, 
                fechaHasta: controlFechaHasta.Value.AddDays(1)));
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

                    var idUbicacion = ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedValue.ToString();
                    var fechaDesde = ((DateTimePicker)controles.Find(x => x.Name == "dateTimeDesde")).Value;
                    var fechaHasta = ((DateTimePicker)controles.Find(x => x.Name == "dateTimeHasta")).Value;
                    fechaHasta = fechaHasta.AddDays(1); //se agrega 1 dia para que se busquen resultados incluidos en el mismo dia

                    CargarGrilla(auditoriaBLL.ObtenerTodosParaVista(
                        result, int.Parse(idUbicacion), fechaDesde, fechaHasta));
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
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridAuditorias").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");


                formDetalle.Controls.Find("IdAuditoria", false).FirstOrDefault().Text = id;

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

        private bool DatosValidos()
        {
            var txtNro = (TextBox)controles.Find(x => x.Name == "txtNro");
            if (!string.IsNullOrWhiteSpace(txtNro.Text))
            ServicioValidaciones.FormatoNumericoValido(txtNro, "Nro. de Auditoria");
         
            return true;
        }
        #endregion

    }
}
