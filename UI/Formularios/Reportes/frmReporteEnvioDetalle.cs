using Controlador;
using Controlador.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Formularios.Reportes
{
    public partial class frmReporteEnvioDetalle : Form
    {
        public frmReporteEnvioDetalle()
        {
            InitializeComponent();
            ControladorReporteEnvioDetalle ctrl = new ControladorReporteEnvioDetalle(this);
        }
    }
}
