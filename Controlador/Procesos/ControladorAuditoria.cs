using BLL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controlador.Procesos
{
    public class ControladorAuditoria
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();

        ArticuloBLL articuloBLL = new ArticuloBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        AuditoriaBLL auditoriaBLL = new AuditoriaBLL();
        List<AuditoriaDetalle> auditoriaDetalle = new List<AuditoriaDetalle>();
        #endregion

        #region Formulario
        public ControladorAuditoria(Form pForm)
        {
            foreach (Control c in pForm.Controls)
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

            this.frm = pForm;

            pForm.Activated += FormLoad;
            pForm.VisibleChanged += VisibleChanged;

            ((Button)controles.Find(x => x.Name == "btnAgregar")).Click += ClickAgregar;
            ((Button)controles.Find(x => x.Name == "btnCancelar")).Click += ClickCancelar;
            ((Button)controles.Find(x => x.Name == "btnGuardar")).Click += ClickGuardar;
            ((Button)controles.Find(x => x.Name == "btnEliminar")).Click += ClickEliminar;
        }

        private void VisibleChanged(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);

            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            dgv.DataSource = null;

            auditoriaDetalle.Clear();
        }

        private bool DatosValidos(int accion)
        {
            switch(accion)
            {
                case 0: //Agregar
                    ServicioValidaciones.FormatoNumericoValido((TextBox)controles.Find(x => x.Name == "txtCantidad"), "Cantidad");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboArticulo"), "Articulo");
                    break;

                case 1: //Guardar
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacion"), "Ubicacion");
                    ServicioValidaciones.GrillaConDatos((DataGridView)controles.Find(x => x.Name == "gridItems"));
                    break;
            }
            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            var comboArticulo = (ComboBox)controles.Find(x => x.Name == "comboArticulo");
            var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");
            this.CargarCombos(comboArticulo, comboUbicacion);

        }

        private void CargarGrilla()
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = auditoriaBLL.ConvertirDetalleAVista(auditoriaDetalle);
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "IdArticulo");
            }
        }
        #endregion


        #region Eventos de los controles
        private void ClickAgregar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos(0))
                {
                    var detalle = new AuditoriaDetalle();
                    detalle.Cantidad = int.Parse(((TextBox)controles.Find(x => x.Name == "txtCantidad")).Text);

                    string idArticulo = ((ComboBox)controles.Find(x => x.Name == "comboArticulo")).SelectedValue.ToString();
                    detalle.Articulo = articuloBLL.Obtener(int.Parse(idArticulo));

                    var articuloAgregado = auditoriaDetalle.Find(x => x.Articulo.Id == detalle.Articulo.Id);
                    if (articuloAgregado == null)
                        auditoriaDetalle.Add(detalle);
                    else
                        articuloAgregado.Cantidad += detalle.Cantidad;

                    this.CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickEliminar(object sender, EventArgs e)
        {
            try
            {
                var grilla = (DataGridView)controles.Find(x => x.Name == "gridItems");
                if(grilla.CurrentRow != null)
                {
                    int articuloSeleccioando = int.Parse(grilla.CurrentRow.Cells["IdArticulo"].Value.ToString());
                    
                    auditoriaDetalle.RemoveAt(auditoriaDetalle.FindIndex(x => x.Articulo.Id == articuloSeleccioando));

                    this.CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void ClickGuardar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos(1))
                {
                    var auditoria = new Auditoria();
                    
                    string idUbicacion = ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedValue.ToString();
                    auditoria.Ubicacion = ubicacionBLL.Obtener(int.Parse(idUbicacion));
                    auditoria.Usuario = SeguridadBLL.usuarioLogueado;
                    auditoria.FechaDeCreacion = DateTime.Now;
                    auditoria.Detalle = this.auditoriaDetalle;

                    auditoriaBLL.Alta(auditoria);

                    MessageBox.Show("La Auditoria fue creada exitosamente. Puede consultarla en Reportes > Auditorias", "Auditoría", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    /*if(DialogResult.Yes == MessageBox.Show(
                        "¿Desea actualizar el stock de la ubicación con los datos de la auditoría?",
                        "Actualización de Stock", 
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question))
                    {
                        auditoriaBLL.ActualizarStock(auditoria);
                        MessageBox.Show("El stock se actualizó correctamente", "Actualización de Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }*/

                    frm.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickCancelar(object sender, EventArgs e)
        {
            frm.Hide();
        }

        #endregion

        #region Metodos Privados
        private void CargarCombos(ComboBox comboArticulo, ComboBox comboUbicacion)
        {
            comboArticulo.DataSource = null;
            comboArticulo.DataSource = articuloBLL.ObtenerTodos();
            comboArticulo.ValueMember = "Id";
            comboArticulo.DisplayMember = "Codigo";

            comboUbicacion.DataSource = null;
            comboUbicacion.DataSource = ubicacionBLL.ObtenerTodos();
            comboUbicacion.ValueMember = "Id";
            comboUbicacion.DisplayMember = "Descripcion";
        }
        #endregion
    }
}
