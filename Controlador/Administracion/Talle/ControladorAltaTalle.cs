using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Servicios;
using System.Xml;

namespace Controlador
{
    public class ControladorAltaTalle
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        TalleBLL talleBLL = new TalleBLL();
        int? idTalle = null;
        #endregion

        #region Formulario
        public ControladorAltaTalle(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdTalle", Visible = false });

            frm = pForm;

            foreach (Control control in frm.Controls)
            {
                controles.Add(control);
            }

            pForm.Load += FormLoad;
            pForm.FormClosed += FormClosed;
            ((Button)controles.Find(x => x.Name == "btnAceptar")).Click += ClickAceptar;
            ((Button)controles.Find(x => x.Name == "btnCancelar")).Click += ClickCancelar;
        }

        private bool DatosValidos()
        {
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtDescripcion"), "Descripcion");

            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Control ctrlOculto = controles.Find(x => x.Name == "IdTalle");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idTalle = result;

            if (idTalle.HasValue)
            {
                var talle = talleBLL.Obtener(idTalle.Value);

                if (talle != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text = talle.Descripcion;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idTalle = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var talle = new Talle();
                    talle.Id = idTalle ?? 0;
                    talle.Descripcion = ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text;
                   
                    if (idTalle.HasValue)
                        talleBLL.Modificacion(talle);
                    else
                        talleBLL.Alta(talle);

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

        #endregion
    }
}
