using BLL;
using BLL.Vistas;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Controlador.Reportes
{
    public class ControladorReporteEnvioDetalle
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        int? idEnvio = null;

        EnvioBLL envioBLL = new EnvioBLL();

        #endregion

        #region Formulario
        public ControladorReporteEnvioDetalle(Form pForm)
        {
            pForm.Controls.Add(new TextBox() { Name = "IdEnvio", Visible = false });

            frm = pForm;

            foreach (Control c in frm.Controls)
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

            pForm.Shown += FormLoad;
            pForm.VisibleChanged += FormClosed;
            ((Button)controles.Find(x => x.Name == "btnVolver")).Click += ClickVolver;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            Control ctrlOculto = controles.Find(x => x.Name == "IdEnvio");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idEnvio = result;

            if (idEnvio.HasValue)
            {
                var envio = envioBLL.Obtener(idEnvio.Value);
                this.CargarGrilla(envioBLL.ConvertirDetalleAVista(envio.Detalle));
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            idEnvio = null;
        }

        private void CargarGrilla(List<EnvioDetalleVista> source)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == "gridDetalle");
            if (dgv != null)
            {
                dgv.DataSource = null;
                dgv.DataSource = source;
                ServicioConfiguracionDeControles.ConfigurarGrilla(dgv);
                ServicioConfiguracionDeControles.OcultarColumnasEnGrilla(dgv, "IdArticulo");
            }
        }
        #endregion

        #region Eventos de controles

        private void ClickVolver(object sender, EventArgs e)
        {
            frm.Hide();
        }

        #endregion
    }
}
