using BLL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorAdministracionTipoDePrenda
    {
        #region Variables locales

        TipoDePrendaBLL tipoDePrendaBLL = new TipoDePrendaBLL();
        List<Control> controles = new List<Control>();
        Form formAlta;
        #endregion

        #region Formulario
        public ControladorAdministracionTipoDePrenda(Form frmAbm, Form pFrmAlta)
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

        private void CargarGrilla(List<TipoDePrenda> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridTiposDePrenda");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = source;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "Id", "Categoria");
            }
        }
        #endregion

        #region Eventos de controles
        private void FormLoad(object sender, EventArgs e)
        {
            CargarGrilla(tipoDePrendaBLL.ObtenerTodos());
        }

        private void ClickAlta(object sender, EventArgs e)
        {
            try
            {
                formAlta.Text = "Alta de Tipo de Prenda";

                formAlta.ShowDialog();
                CargarGrilla(tipoDePrendaBLL.ObtenerTodos());
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
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridTiposDePrenda").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                if (MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?",
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tipoDePrendaBLL.Baja(tipoDePrendaBLL.Obtener(int.Parse(id)));
                    CargarGrilla(tipoDePrendaBLL.ObtenerTodos());
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
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridTiposDePrenda").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                formAlta.Controls.Find("IdTipoDePrenda", false).FirstOrDefault().Text = id;

                formAlta.Text = "Modificación de Tipo de Prenda";
                formAlta.ShowDialog();

                CargarGrilla(tipoDePrendaBLL.ObtenerTodos());
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
                CargarGrilla(tipoDePrendaBLL.ObtenerTodos(descripcion));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

    }
}
