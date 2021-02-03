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

namespace UI.Formularios.Administracion.Categoria
{
    public partial class frmAdministracionCategoria : Form
    {
        public frmAdministracionCategoria()
        {
            InitializeComponent();
            ControladorAdministracionCategoria ctrl = new ControladorAdministracionCategoria(this, new frmAltaCategoria());
        }
    }
}
