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

namespace UI.Formularios.Reportes.RecepcionEnLavadero
{
    public partial class frmReporteRecepcionesEnLavadero : Form
    {
        public frmReporteRecepcionesEnLavadero()
        {
            InitializeComponent();
            ControladorReporteRecepcionesEnLavadero ctrl = new ControladorReporteRecepcionesEnLavadero(this, new frmReporteRecepcionDetalle()); 
        }
    }
}
