using BLL;
using BLL.Vistas;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorAdministracionArticulo
    {
        #region Variables locales
        List<Control> controles = new List<Control>();
        Form formAlta;

        ArticuloBLL articuloBLL = new ArticuloBLL();
        TipoDePrendaBLL tipoDePrendaBLL = new TipoDePrendaBLL();
        ColorBLL colorBLL = new ColorBLL();
        TalleBLL talleBLL = new TalleBLL();
        #endregion

        #region Formulario
        public ControladorAdministracionArticulo(Form frmAbm, Form pFrmAlta)
        {
            formAlta = pFrmAlta;

            foreach (Control c in frmAbm.Controls)
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

            frmAbm.Activated += FormLoad;
            frmAbm.VisibleChanged += VisibleChanged;
            ((Button)controles.Find(x => x.Name == "btnAlta")).Click += ClickAlta;
            ((Button)controles.Find(x => x.Name == "btnModificacion")).Click += ClickModificacion;
            ((Button)controles.Find(x => x.Name == "btnBaja")).Click += ClickBaja;
            ((Button)controles.Find(x => x.Name == "btnBuscar")).Click += ClickBuscar;
        }

        private void VisibleChanged(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
        }

        private void CargarGrilla(List<ArticuloVista> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridArticulos");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = source;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "Id");
            }
        }
        #endregion

        #region Eventos de controles
        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();

            CargarGrilla(articuloBLL.ObtenerTodosParaVista());
        }

        private void ClickAlta(object sender, EventArgs e)
        {
            try
            {
                formAlta.Text = "Alta de Articulo";

                formAlta.ShowDialog();
                CargarGrilla(articuloBLL.ObtenerTodosParaVista());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickBaja(object sender, EventArgs e)
        {
            try
            {
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridArticulos").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                if (MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?",
                    "",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    articuloBLL.Baja(articuloBLL.Obtener(int.Parse(id)));
                    CargarGrilla(articuloBLL.ObtenerTodosParaVista());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickModificacion(object sender, EventArgs e)
        {
            try
            {
                var id = ServicioConfiguracionDeControles.ObtenerCampoSeleccionado(controles, "Id", "gridArticulos").ToString();

                if (string.IsNullOrWhiteSpace(id))
                    throw new Exception("Debe seleccionar un registro de la grilla");

                formAlta.Controls.Find("IdArticulo", false).FirstOrDefault().Text = id;

                formAlta.Text = "Modificación de Articulo";
                formAlta.ShowDialog();

                CargarGrilla(articuloBLL.ObtenerTodosParaVista());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickBuscar(object sender, EventArgs e)
        {
            try
            {
                var codigo = ((TextBox)controles.Find(x => x.Name == "txtCodigo")).Text;
                var idTipoDePrenda = ((ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda")).SelectedValue.ToString();
                var idColor = ((ComboBox)controles.Find(x => x.Name == "comboColor")).SelectedValue.ToString();
                var idTalle = ((ComboBox)controles.Find(x => x.Name == "comboTalle")).SelectedValue.ToString();

                CargarGrilla(articuloBLL.ObtenerTodosParaVista(codigo, int.Parse(idTipoDePrenda), int.Parse(idColor), int.Parse(idTalle)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Metodos Privados
        private void CargarCombos()
        {
            var comboTipoDePrenda = (ComboBox)controles.Find(x => x.Name == "comboTipoDePrenda");
            var comboColor = (ComboBox)controles.Find(x => x.Name == "comboColor");
            var comboTalle = (ComboBox)controles.Find(x => x.Name == "comboTalle");

            var tiposDePrenda = tipoDePrendaBLL.ObtenerTodos();
            tiposDePrenda.Add(new TipoDePrenda() { Id = 0, Descripcion = "Seleccionar..." });
            tiposDePrenda = tiposDePrenda.OrderBy(x => x.Id).ToList();

            var colores = colorBLL.ObtenerTodos();
            colores.Add(new Color() { Id = 0, Descripcion = "Seleccionar..." });
            colores = colores.OrderBy(x => x.Id).ToList();

            var talles = talleBLL.ObtenerTodos();
            talles.Add(new Talle() { Id = 0, Descripcion = "Seleccionar..." });
            talles = talles.OrderBy(x => x.Id).ToList();

            comboTipoDePrenda.DataSource = null;
            comboTipoDePrenda.DataSource = tiposDePrenda;
            comboTipoDePrenda.ValueMember = "Id";
            comboTipoDePrenda.DisplayMember = "Descripcion";
            comboTipoDePrenda.SelectedValue = 0;

            comboColor.DataSource = null;
            comboColor.DataSource = colores;
            comboColor.ValueMember = "Id";
            comboColor.DisplayMember = "Descripcion";
            comboColor.SelectedValue = 0;

            comboTalle.DataSource = null;
            comboTalle.DataSource = talles;
            comboTalle.ValueMember = "Id";
            comboTalle.DisplayMember = "Descripcion";
            comboTalle.SelectedValue = 0;
        }
        #endregion
    }
}
