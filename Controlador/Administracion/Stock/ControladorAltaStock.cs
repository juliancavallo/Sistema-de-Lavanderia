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
    public class ControladorAltaStock
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();

        StockBLL stockBLL = new StockBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        ArticuloBLL articuloBLL = new ArticuloBLL();

        int? idStock = null;
        #endregion

        #region Formulario
        public ControladorAltaStock(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdStock", Visible = false });

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
            ServicioValidaciones.FormatoNumericoValido((TextBox)controles.Find(x => x.Name == "txtCantidad"), "Cantidad");
            ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacion"), "Ubicacion");
            ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboArticulo"), "Articulo");

            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Control ctrlOculto = controles.Find(x => x.Name == "IdStock");

            this.CargarCombos();

            if (int.TryParse(ctrlOculto.Text, out var result))
                idStock = result;

            if (idStock.HasValue)
            {
                var stock = stockBLL.Obtener(idStock.Value);

                if (stock != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtCantidad")).Text = stock.Cantidad.ToString();
                    ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedValue = stock.Ubicacion.Id;
                    ((ComboBox)controles.Find(x => x.Name == "comboArticulo")).SelectedValue = stock.Articulo.Id;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idStock = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var stock = new Stock();
                    stock.Id = idStock ?? 0;
                    stock.Cantidad = int.Parse(((TextBox)controles.Find(x => x.Name == "txtCantidad")).Text);

                    var ubicacion = ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectedItem as Ubicacion;
                    var articulo = ((ComboBox)controles.Find(x => x.Name == "comboArticulo")).SelectedItem as Articulo;

                    stock.Ubicacion = ubicacion;
                    stock.Articulo = articulo;

                    if (idStock.HasValue)
                        stockBLL.Modificacion(stock);
                    else
                        stockBLL.Alta(stock);

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
        private void CargarCombos()
        {
            var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");
            var comboArticulo = (ComboBox)controles.Find(x => x.Name == "comboArticulo");

            var ubicaciones = ubicacionBLL.ObtenerPadreOHermanos(SeguridadBLL.usuarioLogueado.Ubicacion);
            ubicaciones.Add(SeguridadBLL.usuarioLogueado.Ubicacion);
            ubicaciones = ubicaciones.OrderBy(x => x.Id).ToList();

            var articulos = articuloBLL.ObtenerTodos();
            articulos = articulos.OrderBy(x => x.Id).ToList();


            comboUbicacion.DataSource = null;
            comboUbicacion.DataSource = ubicaciones;
            comboUbicacion.ValueMember = "Id";
            comboUbicacion.DisplayMember = "Descripcion";

            comboArticulo.DataSource = null;
            comboArticulo.DataSource = articulos;
            comboArticulo.ValueMember = "Id";
            comboArticulo.DisplayMember = "Codigo";

        }
        #endregion
    }
}
