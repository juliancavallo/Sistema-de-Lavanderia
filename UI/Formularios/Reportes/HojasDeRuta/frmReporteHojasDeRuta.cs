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

namespace UI.Formularios.Reportes.HojasDeRuta
{
    public partial class frmReporteHojasDeRuta : Form
    {
        public frmReporteHojasDeRuta()
        {
            InitializeComponent();
            ControladorReporteHojasDeRuta ctrl = new ControladorReporteHojasDeRuta(this); 
        }
    }
}
