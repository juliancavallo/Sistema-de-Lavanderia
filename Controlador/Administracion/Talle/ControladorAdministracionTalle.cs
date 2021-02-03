using BLL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorAdministracionTalle
    {
        #region Variables locales

        TalleBLL talleBLL = new TalleBLL();
        List<Control> controles = new List<Control>();
        Form formAlta;
        #endregion

        #region Formulario
        public ControladorAdministracionTalle(Form frmAbm, Form pFrmAlta)
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
        private void CargarGrilla(List<Talle> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridTalles");
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
            CargarGrilla(talleBLL.ObtenerTodos());
        }

        private void ClickAlta(object sender, EventArgs e)
        {
            try
            {
                formAlta.Text = "Alta de Talle";

                formAlta.ShowDialog();
                CargarGrilla(talleBLL.ObtenerTodos());
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
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridTalles").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                if (MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?",
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    talleBLL.Baja(talleBLL.Obtener(int.Parse(id)));
                    CargarGrilla(talleBLL.ObtenerTodos());
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
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridTalles").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                formAlta.Controls.Find("IdTalle", false).FirstOrDefault().Text = id;

                formAlta.Text = "Modificación de Talle";
                formAlta.ShowDialog();

                CargarGrilla(talleBLL.ObtenerTodos());
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
                CargarGrilla(talleBLL.ObtenerTodos(descripcion));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

    }
}
