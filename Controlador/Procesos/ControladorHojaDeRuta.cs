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
    public class ControladorHojaDeRuta
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        List<Envio> envios = new List<Envio>();

        EnvioBLL envioBLL = new EnvioBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        HojaDeRutaBLL hojaDeRutaBLL = new HojaDeRutaBLL();
        #endregion

        #region Formulario
        public ControladorHojaDeRuta(Form pForm)
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
            ((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen")).SelectionChangeCommitted += ComboUbicacion_SelectionChangeCommitted;
            ((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino")).SelectionChangeCommitted += ComboUbicacion_SelectionChangeCommitted;

        }


        private void VisibleChanged(object sender, EventArgs e)
        {
            this.BloquearCombos(false);
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);

            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            dgv.DataSource = null;
        }

        private bool DatosValidos(int accion)
        {
            switch(accion)
            {
                case 0: //Agregar
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen"), "Ubicacion Origen");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino"), "Ubicacion Destino");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboEnvio"), "Nro. de Envio");
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
                dgv.DataSource = envioBLL.ConvertirAVista(this.envios);
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "Id");
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
                    var comboEnvio = (ComboBox)controles.Find(x => x.Name == "comboEnvio");

                    if (!this.envios.Any(x => x.Id == int.Parse(comboEnvio.SelectedValue.ToString())))
                    { 
                        this.envios.Add((Envio)comboEnvio.SelectedItem); 
                    }

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
                    int idEnvioSeleccionado = int.Parse(grilla.CurrentRow.Cells["Id"].Value.ToString());
                    
                    envios.RemoveAt(envios.FindIndex(x => x.Id == idEnvioSeleccionado));

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
                    var hojaDeRuta = new HojaDeRuta();
                    
                    hojaDeRuta.Usuario = SeguridadBLL.usuarioLogueado;
                    hojaDeRuta.FechaCreacion = DateTime.Now;
                    hojaDeRuta.Envios = this.envios;

                    hojaDeRutaBLL.Alta(hojaDeRuta);


                    MessageBox.Show("La Hoja de Ruta fue creada exitosamente. Puede consultarla en Reportes > Hojas de Ruta", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ComboUbicacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                var comboUbicacionOrigen = (ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen");
                var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");

                var ubicacionOrigen = (Ubicacion)comboUbicacionOrigen.SelectedItem;
                var ubicacionDestino = (Ubicacion)comboUbicacionDestino.SelectedItem;

                if (ubicacionOrigen.Id > 0 && ubicacionDestino.Id > 0)
                {
                    var envios = envioBLL.ObtenerTodos().Where(x =>
                        x.UbicacionOrigen.Id == ubicacionOrigen.Id 
                        && x.UbicacionDestino.Id == ubicacionDestino.Id
                        && x.Estado.Descripcion == Entidades.Enums.EstadoEnvio.Generado).ToList();

                    this.CargarEnvios(envios);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Metodos Privados
        private void CargarCombos()
        {
            var comboUbicacionOrigen = (ComboBox)controles.Find(x => x.Name == "comboUbicacionOrigen");
            var comboUbicacionDestino = (ComboBox)controles.Find(x => x.Name == "comboUbicacionDestino");
            var comboEnvio = (ComboBox)controles.Find(x => x.Name == "comboEnvio");


            var ubicaciones = ubicacionBLL.ObtenerTodos().Where(x => !x.EsUbicacionInterna).ToList();
            ubicaciones.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
            ubicaciones = ubicaciones.OrderBy(x => x.Id).ToList();

            comboUbicacionOrigen.DataSource = null;
            comboUbicacionOrigen.DataSource = ubicaciones.Where(x => x.Id == SeguridadBLL.usuarioLogueado.Ubicacion.Id).ToList();
            comboUbicacionOrigen.ValueMember = "Id";
            comboUbicacionOrigen.DisplayMember = "Descripcion";
            comboUbicacionOrigen.SelectedValue = 0;

            comboUbicacionDestino.DataSource = null;
            comboUbicacionDestino.DataSource = ubicaciones.ToList();
            comboUbicacionDestino.ValueMember = "Id";
            comboUbicacionDestino.DisplayMember = "Descripcion";
            comboUbicacionDestino.SelectedValue = 0;

            comboEnvio.DataSource = null;

        }

        private void CargarEnvios(List<Envio> envios)
        {
            envios = envios.OrderByDescending(x => x.Id).ToList();
            var comboEnvio = (ComboBox)controles.Find(x => x.Name == "comboEnvio");

            comboEnvio.DataSource = null;
            comboEnvio.DataSource = envios;
            comboEnvio.ValueMember = "Id";
            comboEnvio.DisplayMember = "Id";
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
