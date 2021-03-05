using BLL;
using BLL.Vistas;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorAdministracionStock
    {
        #region Variables locales

        StockBLL stockBLL = new StockBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        ArticuloBLL articuloBLL = new ArticuloBLL();

        List<Control> controles = new List<Control>();
        Form formAlta;
        #endregion

        #region Formulario
        public ControladorAdministracionStock(Form frmAbm, Form pFrmAlta)
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

            var btnAlta = ((Button)controles.Find(x => x.Name == "btnAlta"));
            var btnModificacion = ((Button)controles.Find(x => x.Name == "btnModificacion"));
            var btnBaja = ((Button)controles.Find(x => x.Name == "btnBaja"));

            btnAlta.Click += ClickAlta;
            btnModificacion.Click += ClickModificacion;
            btnBaja.Click += ClickBaja;
            ((Button)controles.Find(x => x.Name == "btnBuscar")).Click += ClickBuscar;

            btnAlta.Visible = false;
            btnModificacion.Visible = false;
            btnBaja.Visible = false;
        }

        private void VisibleChanged(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
        }

        private void CargarGrilla(int idUbicacion = 0, int idArticulo = 0)
        {
            var ubicaciones = ubicacionBLL.ObtenerPadreOHermanos(SeguridadBLL.usuarioLogueado.Ubicacion);
            ubicaciones.Add(SeguridadBLL.usuarioLogueado.Ubicacion);

            List<StockVista> source = stockBLL.ObtenerTodosParaVista(ubicaciones.Select(x => x.Id).ToList(),idUbicacion, idArticulo);
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridStock");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = source;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "Id");
            }
        }
        #endregion

        #region Eventos de controles
        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();
            CargarGrilla();
        }

        private void ClickAlta(object sender, EventArgs e)
        {
            try
            {
                formAlta.Text = "Alta de Stock";

                formAlta.ShowDialog();
                CargarGrilla();
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
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridStock").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                if (MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?",
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    stockBLL.Baja(stockBLL.Obtener(int.Parse(id)));
                    CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) ;
            }
        }

        private void ClickModificacion(object sender, EventArgs e)
        {
            try
            {
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridStock").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                formAlta.Controls.Find("IdStock", false).FirstOrDefault().Text = id;

                formAlta.Text = "Modificación de Stock";
                formAlta.ShowDialog();

                CargarGrilla();
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
                var idUbicacion = ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedValue.ToString();
                var idArticulo = ((ComboBox)controles.Find(x => x.Name == "comboArticulo")).SelectedValue.ToString();
                CargarGrilla(int.Parse(idUbicacion), int.Parse(idArticulo));
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
