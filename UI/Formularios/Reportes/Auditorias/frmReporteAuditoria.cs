using Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Formularios.Reportes.Auditorias
{
    public partial class frmReporteAuditoria : Form
    {
        public frmReporteAuditoria()
        {
            InitializeComponent();
            ControladorReporteAuditoria ctrl = new ControladorReporteAuditoria(this, new frmReporteAuditoriaDetalle()); 
        }
    }
}
