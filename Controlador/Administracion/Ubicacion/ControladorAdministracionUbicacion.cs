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
    public class ControladorAdministracionUbicacion
    {
        #region Variables locales

        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        TipoDeUbicacionBLL tipoDeUbicacionBLL = new TipoDeUbicacionBLL();
        List<Control> controles = new List<Control>();
        Form formAlta;
        #endregion

        #region Formulario
        public ControladorAdministracionUbicacion(Form frmAbm, Form pFrmAlta)
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
        private void CargarGrilla(List<UbicacionVista> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridUbicaciones");
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
            ComboBox comboTipo = (ComboBox)controles.Find(x => x.Name == "comboTipo");
            this.ConfigurarComboTipo(comboTipo);

            ComboBox comboUbicacionPadre = (ComboBox)controles.Find(x => x.Name == "comboUbicacionPadre");
            this.ConfigurarComboUbicacionPadre(comboUbicacionPadre);

            CargarGrilla(ubicacionBLL.ObtenerTodosParaVista());
        }

        private void ClickAlta(object sender, EventArgs e)
        {
            try
            {
                formAlta.Text = "Alta de Ubicacion";

                formAlta.ShowDialog();
                CargarGrilla(ubicacionBLL.ObtenerTodosParaVista());
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
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridUbicaciones").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                if (MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?",
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ubicacionBLL.Baja(ubicacionBLL.Obtener(int.Parse(id)));
                    CargarGrilla(ubicacionBLL.ObtenerTodosParaVista());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickModificacion(object sender, EventArgs e)
        {
            try
            {
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridUbicaciones").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                formAlta.Controls.Find("IdUbicacion", false).FirstOrDefault().Text = id;

                formAlta.Text = "Modificación de Ubicacion";
                formAlta.ShowDialog();

                CargarGrilla(ubicacionBLL.ObtenerTodosParaVista());
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
                var descripcion = ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text;
                var direccion = ((TextBox)controles.Find(x => x.Name == "txtDireccion")).Text;
                var tipoDeUbicacion = ((ComboBox)controles.Find(x => x.Name == "comboTipo")).SelectedValue.ToString();
                var idUbicacionPadre = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionPadre")).SelectedValue.ToString();

                CargarGrilla(ubicacionBLL.ObtenerTodosParaVista(descripcion, direccion, int.Parse(tipoDeUbicacion), int.Parse(idUbicacionPadre)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Metodos Privados
        public void ConfigurarComboUbicacionPadre(ComboBox combo)
        {
            combo.DataSource = null;

            var source = ubicacionBLL.ObtenerSeleccionablesParaUbicacionPadre(null);
            source.Add(new Ubicacion() { Id = -1, Descripcion = "Seleccionar..." });
            source.Add(new Ubicacion() { Id = 0, Descripcion = "Ninguna" });

            source = source.OrderBy(x => x.Id).ToList();

            combo.DataSource = source;
            combo.ValueMember = "Id";
            combo.DisplayMember = "Descripcion";
            combo.SelectedValue = -1;
        }
        public void ConfigurarComboTipo(ComboBox combo)
        {
            combo.DataSource = null;

            var tipos = new List<object>();
            tipos.Add(new { Id = 0, Descripcion = "Seleccionar..." });
            tipoDeUbicacionBLL.ObtenerTodos().ForEach(x => 
            { 
                tipos.Add(new { x.Id, x.Descripcion });       
            });

            combo.DataSource = tipos;
            combo.ValueMember = "Id";
            combo.DisplayMember = "Descripcion";
            combo.SelectedValue = 0;
        }
        #endregion

    }
}
