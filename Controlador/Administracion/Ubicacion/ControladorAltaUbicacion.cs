using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Servicios;
using System.Xml;
using System.Linq;

namespace Controlador
{
    public class ControladorAltaUbicacion
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        int? idUbicacion = null;
        #endregion

        #region Formulario
        public ControladorAltaUbicacion(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdUbicacion", Visible = false });

            frm = pForm;

            foreach (Control control in frm.Controls)
            {
                controles.Add(control);
            }

            pForm.Load += FormLoad;
            pForm.FormClosed += FormClosed;
            ((Button)controles.Find(x => x.Name == "btnAceptar")).Click += ClickAceptar;
            ((Button)controles.Find(x => x.Name == "btnCancelar")).Click += ClickCancelar;
            ((ComboBox)controles.Find(x => x.Name == "comboTipo")).SelectionChangeCommitted += comboTipo_SelectionChangeCommited;
        }


        private bool DatosValidos()
        {
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtCapacidadTotal"), "Capacidad Total");
            ServicioValidaciones.FormatoDecimalValido((TextBox)controles.Find(x => x.Name == "txtCapacidadTotal"), "Capacidad Total");
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtDescripcion"), "Descripcion");
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtDireccion"), "Direccion");
            
            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Control ctrlOculto = controles.Find(x => x.Name == "IdUbicacion");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idUbicacion = result;

            ComboBox comboTipo = (ComboBox)controles.Find(x => x.Name == "comboTipo");
            this.ConfigurarComboTipo(comboTipo);

            int tipo = (int)comboTipo.SelectedValue;
            ComboBox comboUbicacionPadre = (ComboBox)controles.Find(x => x.Name == "comboUbicacionPadre");
            this.ConfigurarComboUbicacionPadre(comboUbicacionPadre, ubicacionBLL.ObtenerSeleccionablesParaUbicacionPadre(idUbicacion).Where(x=> x.TipoDeUbicacion == tipo).ToList());


            if (idUbicacion.HasValue)
            {
                var ubicacion = ubicacionBLL.Obtener(idUbicacion.Value);

                if (ubicacion != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text = ubicacion.Descripcion;
                    ((TextBox)controles.Find(x => x.Name == "txtDireccion")).Text = ubicacion.Direccion;
                    ((CheckBox)controles.Find(x => x.Name == "chkClienteExterno")).Checked = ubicacion.ClienteExterno;
                    ((TextBox)controles.Find(x => x.Name == "txtCapacidadTotal")).Text = ubicacion.CapacidadTotal.ToString();

                    comboTipo.SelectedValue = ubicacion.TipoDeUbicacion;
                    
                    this.ConfigurarComboUbicacionPadre(comboUbicacionPadre, ubicacionBLL.ObtenerSeleccionablesParaUbicacionPadre(idUbicacion).Where(x => x.TipoDeUbicacion == ubicacion.TipoDeUbicacion).ToList());
                    comboUbicacionPadre.SelectedValue = ubicacion.UbicacionPadre != null ? ubicacion.UbicacionPadre.Id : 0;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idUbicacion = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var ubicacion = new Ubicacion();
                    ubicacion.Id = idUbicacion ?? 0;
                    ubicacion.Descripcion = ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text;
                    ubicacion.Direccion = ((TextBox)controles.Find(x => x.Name == "txtDireccion")).Text;
                    ubicacion.ClienteExterno = ((CheckBox)controles.Find(x => x.Name == "chkClienteExterno")).Checked;
                    ubicacion.CapacidadTotal = decimal.Parse(((TextBox)controles.Find(x => x.Name == "txtCapacidadTotal")).Text);

                    string tipoDeUbicacion = ((ComboBox)controles.Find(x => x.Name == "comboTipo")).SelectedValue.ToString();
                    ubicacion.TipoDeUbicacion = int.Parse(tipoDeUbicacion);

                    string idUbicacionPadre = ((ComboBox)controles.Find(x => x.Name == "comboUbicacionPadre")).SelectedValue.ToString();
                    ubicacion.UbicacionPadre = ubicacionBLL.Obtener(int.Parse(idUbicacionPadre));

                    if (idUbicacion.HasValue)
                        ubicacionBLL.Modificacion(ubicacion);
                    else
                        ubicacionBLL.Alta(ubicacion);

                    frm.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickCancelar(object sender, EventArgs e)
        {
            frm.Close();
        }

        private void comboTipo_SelectionChangeCommited(object sender, EventArgs e)
        {
            var tipo = int.Parse(((ComboBox)controles.Find(x => x.Name == "comboTipo")).SelectedValue.ToString());
            ComboBox comboUbicacionPadre = (ComboBox)controles.Find(x => x.Name == "comboUbicacionPadre");

            this.ConfigurarComboUbicacionPadre(comboUbicacionPadre,
                ubicacionBLL.ObtenerSeleccionablesParaUbicacionPadre(idUbicacion)
                .Where(x => x.TipoDeUbicacion == tipo).ToList());
        }

        #endregion

        #region Metodos Privados
        public void ConfigurarComboUbicacionPadre(ComboBox combo, List<Ubicacion> source)
        {
            combo.DataSource = null;

            source.Add(new Ubicacion() { Id = 0, Descripcion = "Ninguna" });

            source = source.OrderBy(x => x.Id).ToList();

            combo.DataSource = source;
            combo.ValueMember = "Id";
            combo.DisplayMember = "Descripcion";
            combo.SelectedIndex = 0;
        }
        public void ConfigurarComboTipo(ComboBox combo)
        {
            combo.DataSource = null;

            var source = new List<object>();
            source.Add(new { Id = (int)Entidades.Enums.TipoDeUbicacion.Lavanderia, Descripcion = "Lavanderia" });
            source.Add(new { Id = (int)Entidades.Enums.TipoDeUbicacion.Clinica, Descripcion = "Clinica" });

            combo.DataSource = source;
            combo.ValueMember = "Id";
            combo.DisplayMember = "Descripcion";
            combo.SelectedValue = 1;
        }
        #endregion
    }
}
