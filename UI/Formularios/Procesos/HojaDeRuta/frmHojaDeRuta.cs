using Controlador.Procesos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Formularios.Procesos.HojaDeRuta
{
    public partial class frmHojaDeRuta : Form
    {
        public frmHojaDeRuta()
        {
            InitializeComponent();
            ControladorHojaDeRuta ctrl = new ControladorHojaDeRuta(this);
        }
    }
}
