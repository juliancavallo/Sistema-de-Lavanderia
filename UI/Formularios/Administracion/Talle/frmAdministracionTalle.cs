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

namespace UI.Formularios.Administracion.Talle
{
    public partial class frmAdministracionTalle : Form
    {
        public frmAdministracionTalle()
        {
            InitializeComponent();
            ControladorAdministracionTalle ctrl = new ControladorAdministracionTalle(this, new frmAltaTalle());
        }
    }
}
