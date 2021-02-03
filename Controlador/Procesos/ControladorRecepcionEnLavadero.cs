using BLL;
using BLL.Vistas;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controlador.Procesos
{
    public class ControladorRecepcionEnLavadero
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        List<RecepcionDetalleVista> recepcionDetalle = new List<RecepcionDetalleVista>();

        ArticuloBLL articuloBLL = new ArticuloBLL();
        RecepcionBLL recepcionBLL = new RecepcionBLL();
        HojaDeRutaBLL hojaDeRutaBLL = new HojaDeRutaBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        EnvioBLL envioBLL = new EnvioBLL();
        #endregion

        #region Formulario
        public ControladorRecepcionEnLavadero(Form pForm)
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
            ((Button)controles.Find(x => x.Name == "btnAceptar")).Click += ClickAceptar;
            ((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino")).SelectionChangeCommitted += ComboUbicacionDestino_SelectionChangeCommitted; ;
        }

        private void VisibleChanged(object sender, EventArgs e)
        {
            this.BloquearCombos(false);
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);

            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            dgv.DataSource = null;

            recepcionDetalle.Clear();
        }

        private bool DatosValidos(int accion)
        {
            switch (accion)
            {
                case 0: //Agregar
                    ServicioValidaciones.FormatoNumericoValido((TextBox)controles.Find(x => x.Name == "txtCantidad"), "Cantidad");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboArticulo"), "Articulo");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboHojaDeRuta"), "Nro. de hoja de Ruta");
                    break;

                case 1: //Guardar
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboHojaDeRuta"), "Nro. de hoja de Ruta");
                    ServicioValidaciones.GrillaConDatos((DataGridView)controles.Find(x => x.Name == "gridItems"));
                    break;

                case 2: //Aceptar
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino"), "Ubicacion Destino");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboHojaDeRuta"), "Nro. de hoja de Ruta");
                    break;
            }
            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();
        }

        private void CargarGrilla()
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = recepcionDetalle;
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
                    int cantidad = int.Parse(((TextBox)controles.Find(x => x.Name == "txtCantidad")).Text); ;
                    int idArticulo = int.Parse(((ComboBox)controles.Find(x => x.Name == "comboArticulo")).SelectedValue.ToString());
                    
                    var articuloAgregado = recepcionDetalle.Find(x => x.IdArticulo == idArticulo);
                    cantidad += articuloAgregado != null ? articuloAgregado.CantidadRecibida : 0;

                    if (articuloAgregado == null)
                    {
                        var nuevoDetalle = new RecepcionDetalle();
                        nuevoDetalle.Articulo = articuloBLL.Obtener(idArticulo);
                        nuevoDetalle.CantidadARecibir = 0;
                        nuevoDetalle.CantidadRecibida = cantidad;

                        var nuevoDetalleVista = recepcionBLL.ConvertirDetalleAVista(new List<RecepcionDetalle>() { nuevoDetalle }).FirstOrDefault();

                        recepcionDetalle.Add(nuevoDetalleVista);
                    }
                    else
                        articuloAgregado.CantidadRecibida = cantidad;

                    ServicioConfiguracionDeControles.LimpiarControles(this.controles);
                    this.CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClickEliminar(object sender, EventArgs e)
        {
            try
            {
                var grilla = (DataGridView)controles.Find(x => x.Name == "gridItems");
                if (grilla.CurrentRow != null)
                {
                    int articuloSeleccionado = int.Parse(grilla.CurrentRow.Cells["IdArticulo"].Value.ToString());

                    recepcionDetalle.Find(x => x.IdArticulo == articuloSeleccionado).CantidadRecibida = 0;

                    this.CargarGrilla();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClickGuardar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos(1))
                {
                    var comboHojaDeRuta = (ComboBox)controles.Find(x => x.Name == "comboHojaDeRuta");

                    var recepcion = new Recepcion();

                    recepcion.Usuario = SeguridadBLL.usuarioLogueado;
                    recepcion.FechaCreacion = DateTime.Now;
                    recepcion.Detalle = recepcionBLL.ConvertirVistaADetalle(this.recepcionDetalle);
                    recepcion.HojaDeRuta = (HojaDeRuta)comboHojaDeRuta.SelectedItem;

                    recepcionBLL.Alta(recepcion);

                    MessageBox.Show("La Recepcion fue creada exitosamente. Puede consultarla en Reportes > Recepciones en Lavadero", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frm.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClickCancelar(object sender, EventArgs e)
        {
            frm.Hide();
        }

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if(this.DatosValidos(2))
                    this.BloquearCombos(true);

                var comboHojaDeRuta = (ComboBox)controles.Find(x => x.Name == "comboHojaDeRuta");
                var idHojaDeRuta = int.Parse(comboHojaDeRuta.SelectedValue.ToString());
                var detalle = recepcionBLL.ConvertirDetalleAVista(recepcionBLL.ObtenerDetallePorHojaDeRuta(idHojaDeRuta));

                this.recepcionDetalle = detalle;

                this.CargarGrilla();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComboUbicacionDestino_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");
                var ubicacionSeleccionada = (Ubicacion)comboUbicacionDestino.SelectedItem;

                var comboHojaDeRuta = (ComboBox)controles.Find(x => x.Name == "comboHojaDeRuta");

                comboHojaDeRuta.DataSource = null;
                comboHojaDeRuta.DataSource = hojaDeRutaBLL
                    .ObtenerTodos(ubicacionSeleccionada.Id)
                    .Where(x => x.Estado.Descripcion == Entidades.Enums.EstadoHojaDeRuta.Generada).ToList();
                comboHojaDeRuta.ValueMember = "Id";
                comboHojaDeRuta.DisplayMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Metodos Privados
        private void CargarCombos()
        {
            try
            {
                var comboArticulo = (ComboBox)controles.Find(x => x.Name == "comboArticulo");

                comboArticulo.DataSource = null;
                comboArticulo.DataSource = articuloBLL.ObtenerTodos();
                comboArticulo.ValueMember = "Id";
                comboArticulo.DisplayMember = "Codigo";


                var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");

                var ubicaciones = ubicacionBLL.ObtenerTodos(SeguridadBLL.usuarioLogueado.Ubicacion.Id).Where(x => !x.EsUbicacionInterna && x.TipoDeUbicacion == (int)Entidades.Enums.TipoDeUbicacion.Lavanderia).ToList();
                ubicaciones.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
                ubicaciones = ubicaciones.OrderBy(x => x.Id).ToList();

                comboUbicacionDestino.DataSource = null;
                comboUbicacionDestino.DataSource = ubicaciones;
                comboUbicacionDestino.ValueMember = "Id";
                comboUbicacionDestino.DisplayMember = "Descripcion";
                comboUbicacionDestino.SelectedValue = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BloquearCombos(bool bloquear)
        {
            var comboHojaDeRuta = (ComboBox)controles.Find(x => x.Name == "comboHojaDeRuta");
            var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");

            comboHojaDeRuta.Enabled = !bloquear;
            comboUbicacionDestino.Enabled = !bloquear;
        }
        #endregion
    }
}
