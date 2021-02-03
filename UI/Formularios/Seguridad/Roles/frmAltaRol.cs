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

namespace UI.Formularios.Seguridad.Roles
{
    public partial class frmAltaRol : Form
    {
        public frmAltaRol()
        {
            InitializeComponent();

            ControladorAltaRol ctrl = new ControladorAltaRol(this);
        }
    }
}
