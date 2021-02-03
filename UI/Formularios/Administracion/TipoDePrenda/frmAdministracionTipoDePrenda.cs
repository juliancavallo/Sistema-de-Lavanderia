using System.Windows.Forms;
using Controlador;
using UI.Formularios.Administracion.TipoDePrenda;

namespace UI.Formularios.Administracion
{
    public partial class frmAdministracionTipoDePrenda : Form
    {
        public frmAdministracionTipoDePrenda()
        {
            InitializeComponent();
            ControladorAdministracionTipoDePrenda ctrl = new ControladorAdministracionTipoDePrenda(this, new frmAltaTipoDePrenda());
        }
    }
}
