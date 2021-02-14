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
    public class ControladorAltaArticulo
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        int? idArticulo = null;

        ArticuloBLL articuloBLL = new ArticuloBLL();
        TipoDePrendaBLL tipoDePrendaBLL = new TipoDePrendaBLL();
        ColorBLL colorBLL = new ColorBLL();
        TalleBLL talleBLL = new TalleBLL();
        #endregion

        #region Formulario
        public ControladorAltaArticulo(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdArticulo", Visible = false });

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
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtCodigo"), "Codigo");
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtPrecioUnitario"), "Precio Unitario");
            ServicioValidaciones.FormatoDecimalValido((TextBox)controles.Find(x => x.Name == "txtPrecioUnitario"), "Precio Unitario");
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtPesoUnitario"), "Peso Unitario");
            ServicioValidaciones.FormatoDecimalValido((TextBox)controles.Find(x => x.Name == "txtPesoUnitario"), "Peso Unitario");
            ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda"), "Tipo de Prenda");
            ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboColor"), "Color");
            ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboTalle"), "Talle");
            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Control ctrlOculto = controles.Find(x => x.Name == "IdArticulo");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idArticulo = result;

            var comboTipoDePrenda = (ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda");
            var comboColor = (ComboBox)controles.Find(x => x.Name == "comboColor");
            var comboTalle = (ComboBox)controles.Find(x => x.Name == "comboTalle");

            this.CargarCombos(comboTipoDePrenda, comboColor, comboTalle);

            if (idArticulo.HasValue)
            {
                var articulo = articuloBLL.Obtener(idArticulo.Value);

                if (articulo != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtCodigo")).Text = articulo.Codigo;
                    ((TextBox)controles.Find(x => x.Name == "txtPrecioUnitario")).Text = articulo.PrecioUnitario.ToString();
                    ((TextBox)controles.Find(x => x.Name == "txtPesoUnitario")).Text = articulo.PesoUnitario.ToString();
                    comboTipoDePrenda.SelectedValue = articulo.TipoDePrenda.Id;
                    comboColor.SelectedValue = articulo.Color.Id;
                    comboTalle.SelectedValue = articulo.Talle.Id;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idArticulo = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var articulo = new Articulo();
                    articulo.Id = idArticulo ?? 0;
                    articulo.Codigo = ((TextBox)controles.Find(x => x.Name == "txtCodigo")).Text;
                    articulo.PrecioUnitario = decimal.Parse(((TextBox)controles.Find(x => x.Name == "txtPrecioUnitario")).Text.Replace('.',','));
                    articulo.PesoUnitario = decimal.Parse(((TextBox)controles.Find(x => x.Name == "txtPesoUnitario")).Text.Replace('.', ','));

                    string tipoDePrenda = ((ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda")).SelectedValue.ToString();
                    string color = ((ComboBox)controles.Find(x => x.Name == "comboColor")).SelectedValue.ToString();
                    string talle = ((ComboBox)controles.Find(x => x.Name == "comboTalle")).SelectedValue.ToString();
                    articulo.TipoDePrenda = tipoDePrendaBLL.Obtener(int.Parse(tipoDePrenda));
                    articulo.Color = colorBLL.Obtener(int.Parse(color));
                    articulo.Talle = talleBLL.Obtener(int.Parse(talle));


                    if (idArticulo.HasValue)
                        articuloBLL.Modificacion(articulo);
                    else
                        articuloBLL.Alta(articulo);

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
        private void CargarCombos(ComboBox comboTipoDePrenda, ComboBox comboColor, ComboBox comboTalle)
        {
            comboTipoDePrenda.DataSource = null;
            comboTipoDePrenda.DataSource = tipoDePrendaBLL.ObtenerTodos();
            comboTipoDePrenda.ValueMember = "Id";
            comboTipoDePrenda.DisplayMember = "Descripcion";

            comboColor.DataSource = null;
            comboColor.DataSource = colorBLL.ObtenerTodos();
            comboColor.ValueMember = "Id";
            comboColor.DisplayMember = "Descripcion";

            comboTalle.DataSource = null;
            comboTalle.DataSource = talleBLL.ObtenerTodos();
            comboTalle.ValueMember = "Id";
            comboTalle.DisplayMember = "Descripcion";
        }
        #endregion
    }
}
