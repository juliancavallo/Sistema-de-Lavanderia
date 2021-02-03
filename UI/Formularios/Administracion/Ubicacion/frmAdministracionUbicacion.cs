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

namespace UI.Formularios.Administracion.Ubicacion
{
    public partial class frmAdministracionUbicacion : Form
    {
        public frmAdministracionUbicacion()
        {
            InitializeComponent();
            ControladorAdministracionUbicacion ctrl = new ControladorAdministracionUbicacion(this, new frmAltaUbicacion());
        }
    }
}
