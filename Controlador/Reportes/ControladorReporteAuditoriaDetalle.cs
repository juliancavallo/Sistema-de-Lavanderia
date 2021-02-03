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
    public class ControladorReporteAuditoriaDetalle
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        int? idAuditoria = null;

        AuditoriaBLL auditoriaBLL = new AuditoriaBLL();

        #endregion

        #region Formulario
        public ControladorReporteAuditoriaDetalle(Form pForm)
        {
            pForm.Controls.Add(new TextBox() { Name = "IdAuditoria", Visible = false });

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
            Control ctrlOculto = controles.Find(x => x.Name == "IdAuditoria");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idAuditoria = result;

            if (idAuditoria.HasValue)
            {
                var auditoria = auditoriaBLL.Obtener(idAuditoria.Value);
                this.CargarGrilla(auditoriaBLL.ConvertirDetalleAVista(auditoria.Detalle));
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            idAuditoria = null;
        }

        private void CargarGrilla(List<AuditoriaDetalleVista> source)
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
