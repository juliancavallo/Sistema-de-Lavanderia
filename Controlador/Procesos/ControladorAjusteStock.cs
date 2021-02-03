using BLL;
using BLL.Vistas;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controlador.Procesos
{
    public class ControladorAjusteStock
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();

        AuditoriaBLL auditoriaBLL = new AuditoriaBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        StockBLL stockBLL = new StockBLL();
        #endregion

        #region Formulario
        public ControladorAjusteStock(Form pForm)
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

            ((Button)controles.Find(x => x.Name == "btnActualizar")).Click += ClickActualizar;
            ((Button)controles.Find(x => x.Name == "btnCargar")).Click += ClickCargar;
            ((Button)controles.Find(x => x.Name == "btnCancelar")).Click += ClickCancelar;
            ((Button)controles.Find(x => x.Name == "btnGuardar")).Click += ClickGuardar;
            ((ComboBox)controles.Find(x => x.Name == "comboUbicacion")).SelectionChangeCommitted += comboUbicacion_SelectionChangeCommitted;
        }


        private void VisibleChanged(object sender, EventArgs e)
        {
            var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");
            comboUbicacion.Enabled = true;

            ServicioConfiguracionDeControles.LimpiarControles(this.controles);

            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            dgv.DataSource = null;
        }

        private bool DatosValidos(int accion)
        {
            switch (accion)
            {
                case 0: //Cargar
                    ServicioValidaciones.ItemObligatorioSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacion"), "Ubicacion");
                    break;

                case 1: //Actualizar
                    ServicioValidaciones.ItemObligatorioSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacion"), "Ubicacion");
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboAuditoria"), "Última auditoria");
                    var gridItems = (DataGridView)controles.Find(x => x.Name == "gridItems");
                    var lista = gridItems.DataSource as List<AjusteStockVista>;

                    if (lista == null)
                        throw new Exception("Debe hacer clic en Cargar Datos antes de agregar la auditoría");
                    break;

                case 2: //Guardar
                    ServicioValidaciones.ItemSeleccionado((ComboBox)controles.Find(x => x.Name == "comboUbicacion"), "Ubicacion");
                    ServicioValidaciones.GrillaConDatos((DataGridView)controles.Find(x => x.Name == "gridItems"));
                    
                    DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
                    foreach(DataGridViewRow row in dgv.Rows)
                    {
                        var cellValue = row.Cells["Observaciones"].Value;
                        if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                            throw new Exception("El campo Observaciones es obligatorio para todos los registros");
                    }
                    break;
            }
            return true;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            this.CargarCombos();

        }

        private void CargarGrilla(List<AjusteStockVista> ajusteStockLista)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = ajusteStockLista;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "IdArticulo", "IdUbicacion", "Ubicacion", "FechaCreacion", "Usuario");
                dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                dgv.ReadOnly = false;
                foreach (DataGridViewColumn column in dgv.Columns)
                {
                    column.ReadOnly = column.Name != "NuevaCantidad" && column.Name != "Observaciones";
                }
            }
        }
        #endregion


        #region Eventos de los controles
        private void ClickCargar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos(0))
                {
                    var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");
                    var stock = stockBLL.ObtenerTodos(((Ubicacion)comboUbicacion.SelectedItem).Id, 0);
                    var lista = stockBLL.ConvertirAVistaAjuste(stock);
                    this.CargarGrilla(lista);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClickActualizar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos(1))
                {
                    var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");
                    comboUbicacion.Enabled = false;

                    var dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
                    var lista = dgv.DataSource as List<AjusteStockVista>;

                    var comboAuditoria = (ComboBox)controles.Find(x => x.Name == "comboAuditoria");
                    var auditoria = comboAuditoria.SelectedItem as Auditoria;

                    lista = stockBLL.AjustarVistaPorAuditoria(lista, auditoria);
                    this.CargarGrilla(lista);
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
                if (DatosValidos(2))
                {
                    var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");
                    var ubicacion = (Ubicacion)comboUbicacion.SelectedItem;

                    var dgv = (DataGridView)controles.Find(x => x.Name == "gridItems");
                    var lista = dgv.DataSource as List<AjusteStockVista>;

                    stockBLL.AjustarPorAuditoria(lista);

                    MessageBox.Show("El ajuste de stock fue creado exitosamente. Puede consultarlo en Reportes > Ajuste de Stock", "Ajuste de Stock", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void comboUbicacion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.ActualizarAuditorias();
        }
        #endregion

        #region Metodos Privados
        private void CargarCombos()
        {
            var comboAuditoria = (ComboBox)controles.Find(x => x.Name == "comboAuditoria");
            var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");

            var ubicaciones = ubicacionBLL.ObtenerPadreOHermanos(SeguridadBLL.usuarioLogueado.Ubicacion);
            ubicaciones.Add(SeguridadBLL.usuarioLogueado.Ubicacion);
            ubicaciones.Add(new Ubicacion() { Id = 0, Descripcion = "Seleccionar..." });
            ubicaciones = ubicaciones.OrderBy(x => x.Id).ToList();

            comboUbicacion.DataSource = null;
            comboUbicacion.DataSource = ubicaciones;
            comboUbicacion.ValueMember = "Id";
            comboUbicacion.DisplayMember = "Descripcion";
            comboUbicacion.SelectedValue = 0;

            comboAuditoria.DataSource = null;
        }

        private void ActualizarAuditorias()
        {
            var comboAuditoria = (ComboBox)controles.Find(x => x.Name == "comboAuditoria");
            var comboUbicacion = (ComboBox)controles.Find(x => x.Name == "comboUbicacion");
            var ubicacion = (Ubicacion)comboUbicacion.SelectedItem;
            
            var ultimaAuditoria = auditoriaBLL.ObtenerTodos().LastOrDefault(x => x.Ubicacion.Id == ubicacion.Id);

            comboAuditoria.DataSource = null;
            if (ultimaAuditoria != null)
            {
                comboAuditoria.DataSource = new List<Auditoria>() { ultimaAuditoria };
                comboAuditoria.ValueMember = "Id";
                comboAuditoria.DisplayMember = "Id";
            }
        }
        #endregion
    }
}
