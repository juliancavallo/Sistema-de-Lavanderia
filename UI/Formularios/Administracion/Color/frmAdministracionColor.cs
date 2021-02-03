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

namespace UI.Formularios.Administracion.Color
{
    public partial class frmAdministracionColor : Form
    {
        public frmAdministracionColor()
        {
            InitializeComponent();
            ControladorAdministracionColor ctrl = new ControladorAdministracionColor(this, new frmAltaColor());
        }
    }
}
