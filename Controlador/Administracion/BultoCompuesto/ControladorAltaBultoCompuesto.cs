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
    public class ControladorAltaBultoCompuesto
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        BultoCompuestoBLL bultoCompuestoBLL = new BultoCompuestoBLL();
        TipoDePrendaBLL tipoDePrendaBLL = new TipoDePrendaBLL();
        int? idBultoCompuesto = null;
        #endregion

        #region Formulario
        public ControladorAltaBultoCompuesto(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdBultoCompuesto", Visible = false });

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

            var comboTipoDePrenda1 = (ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda1");
            var comboTipoDePrenda2 = (ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda2");

            ServicioValidaciones.ItemSeleccionado(comboTipoDePrenda1, "Tipo de Prenda 1");
            ServicioValidaciones.ItemSeleccionado(comboTipoDePrenda2, "Tipo de Prenda 2");

            if (comboTipoDePrenda1.SelectedValue == comboTipoDePrenda2.SelectedValue)
                throw new Exception("Los tipos de prenda deben ser distintos");

            var tipoDePrenda1 = (TipoDePrenda)comboTipoDePrenda1.SelectedItem;
            var tipoDePrenda2 = (TipoDePrenda)comboTipoDePrenda2.SelectedItem;

            if (!tipoDePrenda1.Categoria.EsCompuesta || !tipoDePrenda2.Categoria.EsCompuesta)
                throw new Exception("Recuerde que la categoría de los tipos de prenda debe ser compuesta");

            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Control ctrlOculto = controles.Find(x => x.Name == "IdBultoCompuesto");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idBultoCompuesto = result;

            var comboTipoDePrenda1 = (ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda1");
            var comboTipoDePrenda2 = (ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda2");

            this.CargarCombos(comboTipoDePrenda1, comboTipoDePrenda2);

            if (idBultoCompuesto.HasValue)
            {
                var bultoCompuesto = bultoCompuestoBLL.Obtener(idBultoCompuesto.Value);

                if (bultoCompuesto != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text = bultoCompuesto.Descripcion;
                    comboTipoDePrenda1.SelectedValue = bultoCompuesto.Detalle.First().TipoDePrenda.Id;
                    comboTipoDePrenda2.SelectedValue = bultoCompuesto.Detalle.Last().TipoDePrenda.Id;
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idBultoCompuesto = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var bultoCompuesto = new BultoCompuesto();
                    bultoCompuesto.Id = idBultoCompuesto ?? 0;
                    bultoCompuesto.Descripcion = ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text;

                    var detalle = new List<BultoCompuestoDetalle>();
                    var tipoDePrenda1 = (TipoDePrenda)((ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda1")).SelectedItem;
                    var tipoDePrenda2 = (TipoDePrenda)((ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda2")).SelectedItem;
                    detalle.Add(new BultoCompuestoDetalle() { TipoDePrenda = tipoDePrenda1, BultoCompuesto = bultoCompuesto });
                    detalle.Add(new BultoCompuestoDetalle() { TipoDePrenda = tipoDePrenda2, BultoCompuesto = bultoCompuesto });

                    bultoCompuesto.Detalle = detalle;

                    if (idBultoCompuesto.HasValue)
                        bultoCompuestoBLL.Modificacion(bultoCompuesto);
                    else
                        bultoCompuestoBLL.Alta(bultoCompuesto);

                    frm.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, null, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClickCancelar(object sender, EventArgs e)
        {
            frm.Close();
        }

        #endregion

        #region Metodos Privados
        private void CargarCombos(ComboBox comboTipoDePrenda1, ComboBox comboTipoDePrenda2)
        {
            comboTipoDePrenda1.DataSource = null;
            comboTipoDePrenda1.DataSource = tipoDePrendaBLL.ObtenerTodos();
            comboTipoDePrenda1.ValueMember = "Id";
            comboTipoDePrenda1.DisplayMember = "Descripcion";

            comboTipoDePrenda2.DataSource = null;
            comboTipoDePrenda2.DataSource = tipoDePrendaBLL.ObtenerTodos();
            comboTipoDePrenda2.ValueMember = "Id";
            comboTipoDePrenda2.DisplayMember = "Descripcion";
        }
        #endregion
    }
}
