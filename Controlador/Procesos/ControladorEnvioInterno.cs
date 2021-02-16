using BLL;
using Entidades;
using Entidades.Enums;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Controlador.Procesos
{
    public class ControladorEnvioInterno
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        List<EnvioDetalle> envioDetalle = new List<EnvioDetalle>();

        ArticuloBLL articuloBLL = new ArticuloBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        EnvioBLL envioBLL = new EnvioBLL();
        StockBLL stockBLL = new StockBLL();
        #endregion

        #region Formulario
        public ControladorEnvioInterno(Form pForm)
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
            ((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen")).SelectionChangeCommitted += comboUbicacionOrigen_SelectionChangeCommitted;
        }

        private void VisibleChanged(object sender, EventArgs e)
        {
            this.BloquearCombos(false);
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);

            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            dgv.DataSource = null;

            envioDetalle.Clear();
        }

        private bool DatosValidos(int accion)
        {
            switch(accion)
            {
                case 0: //Agregar
                    ServicioValidaciones.FormatoNumericoValido((TextBox)controles.Find(x => x.Name == "txtCantidad"), "Cantidad");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboArticulo"), "Articulo");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen"), "Ubicacion Origen");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino"), "Ubicacion Destino");
                    break;

                case 1: //Guardar
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen"), "Ubicacion Origen");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino"), "Ubicacion Destino");
                    ServicioValidaciones.GrillaConDatos((DataGridView)controles.Find(x => x.Name == "gridItems"));
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
                dgv.DataSource = envioBLL.ConvertirDetalleAVista(envioDetalle);
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
                    this.BloquearCombos(true);

                    int cantidad = int.Parse(((TextBox)controles.Find(x => x.Name == "txtCantidad")).Text); ;
                    int idArticulo = int.Parse(((ComboBox)controles.Find(x => x.Name == "comboArticulo")).SelectedValue.ToString());
                    int idUbicacionOrigen = int.Parse(((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen")).SelectedValue.ToString());

                    var articuloAgregado = envioDetalle.Find(x => x.Articulo.Id == idArticulo);
                    cantidad += articuloAgregado != null ? articuloAgregado.Cantidad : 0;

                    if (stockBLL.ValidarStock(idArticulo, idUbicacionOrigen, cantidad))
                    {
                        var detalle = new EnvioDetalle();
                        detalle.Cantidad = cantidad;
                        detalle.Articulo = articuloBLL.Obtener(idArticulo);

                        if (articuloAgregado == null)
                            envioDetalle.Add(detalle);
                        else
                            articuloAgregado.Cantidad  = cantidad;

                        ServicioConfiguracionDeControles.LimpiarControles(this.controles);
                        this.CargarGrilla();
                    }
                    else
                        MessageBox.Show("La prenda no se puede agregar porque la ubicacion origen "
                            + "no cuenta con el stock suficiente. Revise el stock desde Administracion > Stock",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if(grilla.CurrentRow != null)
                {
                    int articuloSeleccioando = int.Parse(grilla.CurrentRow.Cells["IdArticulo"].Value.ToString());
                    
                    envioDetalle.RemoveAt(envioDetalle.FindIndex(x => x.Articulo.Id == articuloSeleccioando));

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
                    var envio = new Envio();
                    
                    string idUbicacionOrigen = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen")).SelectedValue.ToString();
                    string idUbicacionDestino = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino")).SelectedValue.ToString();
                    envio.UbicacionOrigen = ubicacionBLL.Obtener(int.Parse(idUbicacionOrigen));
                    envio.UbicacionDestino = ubicacionBLL.Obtener(int.Parse(idUbicacionDestino));
                    envio.Usuario = SeguridadBLL.usuarioLogueado;
                    envio.FechaCreacion = DateTime.Now;
                    envio.Detalle = this.envioDetalle;

                    if (envioBLL.ValidarCapacidadDestino(envio.UbicacionDestino, envio.Detalle))
                    {
                        envioBLL.Alta(envio);
                        envioBLL.Enviar(envio, null);
                        envioBLL.Recibir(envio);

                        stockBLL.Enviar(new List<Envio>() { envio });
                        stockBLL.Recibir(envio);

                        MessageBox.Show("El Envio fue creado exitosamente. Puede consultarlo en Reportes > Envios a Lavadero", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frm.Hide();
                    }
                    else
                    {
                        MessageBox.Show("La Recepción no se puede crear ya que se está superando la capacidad disponible " +
                            "en la ubicación destino.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void comboUbicacionOrigen_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.ActualizarDestinos();
        }

        #endregion

        #region Metodos Privados
        private void CargarCombos()
        {
            var comboArticulo = (ComboBox)controles.Find(x => x.Name == "comboArticulo");
            var comboUbicacionOrigen = (ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen");

            comboArticulo.DataSource = null;
            comboArticulo.DataSource = articuloBLL.ObtenerTodos();
            comboArticulo.ValueMember = "Id";
            comboArticulo.DisplayMember = "Codigo";

            var ubicaciones = ubicacionBLL.ObtenerPadreOHermanos(SeguridadBLL.usuarioLogueado.Ubicacion).ToList();
            ubicaciones.Add(SeguridadBLL.usuarioLogueado.Ubicacion);

            comboUbicacionOrigen.DataSource = null;
            comboUbicacionOrigen.DataSource = ubicaciones;
            comboUbicacionOrigen.ValueMember = "Id";
            comboUbicacionOrigen.DisplayMember = "Descripcion";

            ActualizarDestinos();
        }

        private void ActualizarDestinos()
        {
            var comboUbicacionOrigen = (ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen");
            var ubicacionOrigen = (Ubicacion)comboUbicacionOrigen.SelectedItem;
            var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");

            comboUbicacionDestino.DataSource = null;
            comboUbicacionDestino.DataSource = ubicacionBLL.ObtenerPadreOHermanos(ubicacionOrigen);
            comboUbicacionDestino.ValueMember = "Id";
            comboUbicacionDestino.DisplayMember = "Descripcion";
        }


        private void BloquearCombos(bool bloquear)
        {
            var comboUbicacionOrigen = (ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen");
            var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");

            comboUbicacionOrigen.Enabled = !bloquear;
            comboUbicacionDestino.Enabled = !bloquear;
        }
        #endregion
    }
}
