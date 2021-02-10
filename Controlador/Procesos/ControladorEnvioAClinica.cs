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
    public class ControladorEnvioAClinica
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        List<EnvioDetalle> envioDetalle = new List<EnvioDetalle>();

        ArticuloBLL articuloBLL = new ArticuloBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        EnvioBLL envioBLL = new EnvioBLL();
        StockBLL stockBLL = new StockBLL();
        ParametroDelSistemaBLL parametroDelSistemaBLL = new ParametroDelSistemaBLL();
        #endregion

        #region Formulario
        public ControladorEnvioAClinica(Form pForm)
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

                    var articuloSeleccionado = (Articulo)((ComboBox)controles.Find(x => x.Name == "comboArticulo")).SelectedItem;
                    articuloBLL.ValidarCortePorBulto(articuloSeleccionado, int.Parse(((TextBox)controles.Find(x => x.Name == "txtCantidad")).Text));
                    
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
                        detalle.PrecioUnitario = articuloBLL.Obtener(idArticulo).PrecioUnitario;

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
                    if (articuloBLL.ValidarBultosCompuestos((DataGridView)controles.Find(x => x.Name == "gridItems")))
                    {
                        var envio = new Envio();

                        string idUbicacionOrigen = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen")).SelectedValue.ToString();
                        string idUbicacionDestino = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino")).SelectedValue.ToString();
                        envio.UbicacionOrigen = ubicacionBLL.Obtener(int.Parse(idUbicacionOrigen));
                        envio.UbicacionDestino = ubicacionBLL.Obtener(int.Parse(idUbicacionDestino));
                        envio.Usuario = SeguridadBLL.usuarioLogueado;
                        envio.FechaCreacion = DateTime.Now;
                        envio.Detalle = this.envioDetalle;

                        envioBLL.Alta(envio);

                        MessageBox.Show("El Envio fue creado exitosamente. Puede consultarlo en Reportes > Envios a Clinica", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MessageBox.Show($"La facturación para este envío es de ${envioBLL.ObtenerFacturacionTotal(envio)}. " +
                            $"{ (envio.UbicacionDestino.ClienteExterno ? string.Empty : $"Se aplicó un descuento de {parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.PorcentajeDescuentoDeEnvios).Valor}%.") }",
                            "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frm.Hide();
                    }
                    else 
                        MessageBox.Show("Se leyó una prenda con categoría compuesta, pero no se leyó el bulto compuesto completo. " +
                            "Puede revisar su configuración en Administración de Bultos Compuestos", "Bulto compuesto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #endregion

        #region Metodos Privados
        private void CargarCombos()
        {
            var comboArticulo = (ComboBox)controles.Find(x => x.Name == "comboArticulo");
            var comboUbicacionOrigen = (ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen");
            var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");

            comboArticulo.DataSource = null;
            comboArticulo.DataSource = articuloBLL.ObtenerTodos();
            comboArticulo.ValueMember = "Id";
            comboArticulo.DisplayMember = "Codigo";

            comboUbicacionOrigen.DataSource = null;
            comboUbicacionOrigen.DataSource = ubicacionBLL.ObtenerTodos(SeguridadBLL.usuarioLogueado.Ubicacion.Id).Where(x => x.TipoDeUbicacion == (int)TipoDeUbicacion.Lavanderia && !x.EsUbicacionInterna).ToList();
            comboUbicacionOrigen.ValueMember = "Id";
            comboUbicacionOrigen.DisplayMember = "Descripcion";

            comboUbicacionDestino.DataSource = null;
            comboUbicacionDestino.DataSource = ubicacionBLL.ObtenerTodos().Where(x => x.TipoDeUbicacion == (int)TipoDeUbicacion.Clinica && !x.EsUbicacionInterna).ToList();
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
