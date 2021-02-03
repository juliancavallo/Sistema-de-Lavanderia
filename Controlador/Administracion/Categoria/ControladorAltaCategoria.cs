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
    public class ControladorAltaCategoria
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        CategoriaBLL CategoriaBLL = new CategoriaBLL();
        int? idCategoria = null;
        #endregion

        #region Formulario
        public ControladorAltaCategoria(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdCategoria", Visible = false });

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
            Control ctrlOculto = controles.Find(x => x.Name == "IdCategoria");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idCategoria = result;

            if (idCategoria.HasValue)
            {
                var categoria = CategoriaBLL.Obtener(idCategoria.Value);

                if (categoria != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text = categoria.Descripcion;
                    ((CheckBox)controles.Find(x => x.Name == "chkEsCompuesta")).Checked = categoria.EsCompuesta;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idCategoria = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var categoria = new Categoria();
                    categoria.Id = idCategoria ?? 0;
                    categoria.Descripcion = ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text;
                    categoria.EsCompuesta = ((CheckBox)controles.Find(x => x.Name == "chkEsCompuesta")).Checked;

                    if (idCategoria.HasValue)
                        CategoriaBLL.Modificacion(categoria);
                    else
                        CategoriaBLL.Alta(categoria);

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
