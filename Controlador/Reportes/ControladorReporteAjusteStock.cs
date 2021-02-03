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
    public class ControladorReporteAjusteStock
    {
        #region Variables locales

        ArticuloBLL articuloBLL = new ArticuloBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        StockBLL stockBLL = new StockBLL();
        List<Control> controles = new List<Control>();
        Form form;
        #endregion

        #region Formulario
        public ControladorReporteAjusteStock(Form frm)
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

        private void CargarGrilla(List<AjusteStockVista> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = source;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "IdArticulo","IdUbicacion");
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();

            var controlFechaDesde = (DateTimePicker)controles.Find(x => x.Name == "dateTimeDesde");
            var controlFechaHasta = (DateTimePicker)controles.Find(x => x.Name == "dateTimeHasta");

            controlFechaDesde.Value = DateTime.Today.AddDays(-7);
            controlFechaHasta.Value = DateTime.Today;

            var ubicacionesPorDefecto = ubicacionBLL.ObtenerPadreOHermanos(SeguridadBLL.usuarioLogueado.Ubicacion);
            ubicacionesPorDefecto.Add(SeguridadBLL.usuarioLogueado.Ubicacion);

            CargarGrilla(stockBLL.ObtenerAjustesParaVista(ubicacionesPorDefecto.Select(x => x.Id).ToList(),
                fechaDesde: controlFechaDesde.Value, 
                fechaHasta: controlFechaHasta.Value.AddDays(1)));
        }
        #endregion

        #region Eventos de controles
        private void ClickBuscar(object sender, EventArgs e)
        {
            try
            {
                var idUbicacion = ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedValue.ToString();
                var idArticulo = ((ComboBox)controles.Find(x => x.Name == "comboArticulo")).SelectedValue.ToString();
                var fechaDesde = ((DateTimePicker)controles.Find(x => x.Name == "dateTimeDesde")).Value;
                var fechaHasta = ((DateTimePicker)controles.Find(x => x.Name == "dateTimeHasta")).Value;
                fechaHasta = fechaHasta.AddDays(1); //se agrega 1 dia para que se busquen resultados incluidos en el mismo dia

                var ubicacionesPorDefecto = ubicacionBLL.ObtenerPadreOHermanos(SeguridadBLL.usuarioLogueado.Ubicacion);
                ubicacionesPorDefecto.Add(SeguridadBLL.usuarioLogueado.Ubicacion);

                CargarGrilla(stockBLL.ObtenerAjustesParaVista(ubicacionesPorDefecto.Select(x => x.Id).ToList(),
                    int.Parse(idUbicacion), int.Parse(idArticulo), fechaDesde, fechaHasta));
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
            var comboArticulo = (ComboBox)controles.Find(x => x.Name == "comboArticulo");

            var ubicaciones = ubicacionBLL.ObtenerPadreOHermanos(SeguridadBLL.usuarioLogueado.Ubicacion);
            ubicaciones.Add(SeguridadBLL.usuarioLogueado.Ubicacion);
            ubicaciones.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
            ubicaciones = ubicaciones.OrderBy(x => x.Id).ToList();

            var articulos = articuloBLL.ObtenerTodos();
            articulos.Add(new Articulo() { Id = 0, Codigo = "Seleccionar..." });
            articulos = articulos.OrderBy(x => x.Id).ToList();

            comboUbicacion.DataSource = null;
            comboUbicacion.DataSource = ubicaciones;
            comboUbicacion.ValueMember = "Id";
            comboUbicacion.DisplayMember = "Descripcion";
            comboUbicacion.SelectedValue = 0;

            comboArticulo.DataSource = null;
            comboArticulo.DataSource = articulos;
            comboArticulo.ValueMember = "Id";
            comboArticulo.DisplayMember = "Codigo";
            comboArticulo.SelectedValue = 0;
        }

        #endregion

    }
}
