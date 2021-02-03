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
    public class ControladorAltaColor
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        ColorBLL colorBLL = new ColorBLL();
        int? idColor = null;
        #endregion

        #region Formulario
        public ControladorAltaColor(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdColor", Visible = false });

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
            Control ctrlOculto = controles.Find(x => x.Name == "IdColor");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idColor = result;

            if (idColor.HasValue)
            {
                var color = colorBLL.Obtener(idColor.Value);

                if (color != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text = color.Descripcion;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idColor = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var color = new Color();
                    color.Id = idColor ?? 0;
                    color.Descripcion = ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text;
                   
                    if (idColor.HasValue)
                        colorBLL.Modificacion(color);
                    else
                        colorBLL.Alta(color);

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
