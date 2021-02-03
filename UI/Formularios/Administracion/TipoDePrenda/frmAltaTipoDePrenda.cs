using System.Windows.Forms;
using Controlador;

namespace UI.Formularios.Administracion.TipoDePrenda
{
    public partial class frmAltaTipoDePrenda : Form
    {
        public frmAltaTipoDePrenda()
        {
            InitializeComponent();
            ControladorAltaTipoDePrenda ctrl = new ControladorAltaTipoDePrenda(this);
        }
    }
}
