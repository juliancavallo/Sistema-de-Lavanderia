using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Servicios;

namespace Controlador
{
    public class ControladorAltaTipoDePrenda
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        TipoDePrendaBLL tipoDePrendaBLL = new TipoDePrendaBLL();
        CategoriaBLL categoriaBLL = new CategoriaBLL();
        int? idTipoDePrenda = null;
        #endregion

        #region Formulario
        public ControladorAltaTipoDePrenda(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdTipoDePrenda", Visible = false });

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

            if (ServicioValidaciones.EstadoCheckbox((CheckBox)controles.Find(x => x.Name == "checkCortePorBulto")))
            {
                ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtCortePorBulto"), "Corte por Bulto");
                ServicioValidaciones.FormatoNumericoValido((TextBox)controles.Find(x => x.Name == "txtCortePorBulto"), "Corte por Bulto");
                ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboCategoria"), "Categoria");
            }

            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Control ctrlOculto = controles.Find(x => x.Name == "IdTipoDePrenda");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idTipoDePrenda = result;

            var comboCategoria = (ComboBox)controles.Find(x => x.Name == "comboCategoria");

            this.CargarCombos(comboCategoria);

            if (idTipoDePrenda.HasValue)
            {
                var tipoDePrenda = tipoDePrendaBLL.Obtener(idTipoDePrenda.Value);

                if (tipoDePrenda != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text = tipoDePrenda.Descripcion;
                    ((TextBox)controles.Find(x => x.Name == "txtCortePorBulto")).Text = tipoDePrenda.CortePorBulto.ToString();
                    ((CheckBox)controles.Find(x => x.Name == "checkCortePorBulto")).Checked = tipoDePrenda.UsaCortePorBulto;
                    comboCategoria.SelectedValue = tipoDePrenda.Categoria.Id;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idTipoDePrenda = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var tipoDePrenda = new TipoDePrenda();
                    tipoDePrenda.Id = idTipoDePrenda ?? 0;
                    tipoDePrenda.Descripcion = ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text;
                    tipoDePrenda.CortePorBulto = ServicioConfiguracionDeControles.ConvertirAEntero((TextBox)controles.Find(x => x.Name == "txtCortePorBulto"));
                    tipoDePrenda.UsaCortePorBulto = ((CheckBox)controles.Find(x => x.Name == "checkCortePorBulto")).Checked;

                    string categoria = ((ComboBox)controles.Find(x => x.Name == "comboCategoria")).SelectedValue.ToString();
                    tipoDePrenda.Categoria = categoriaBLL.Obtener(int.Parse(categoria));

                    if (idTipoDePrenda.HasValue)
                        tipoDePrendaBLL.Modificacion(tipoDePrenda);
                    else
                        tipoDePrendaBLL.Alta(tipoDePrenda);

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

        #region Metodos Privados
        private void CargarCombos(ComboBox comboCategoria)
        {
            comboCategoria.DataSource = null;
            comboCategoria.DataSource = categoriaBLL.ObtenerTodos();
            comboCategoria.ValueMember = "Id";
            comboCategoria.DisplayMember = "Descripcion";
        }
        #endregion
    }
}
