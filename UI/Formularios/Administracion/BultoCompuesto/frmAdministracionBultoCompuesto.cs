﻿using Controlador;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Formularios.Administracion.BultoCompuesto
{
    public partial class frmAdministracionBultoCompuesto : Form
    {
        public frmAdministracionBultoCompuesto()
        {
            InitializeComponent();
            ControladorAdministracionBultoCompuesto ctrl = new ControladorAdministracionBultoCompuesto(this, new frmAltaBultoCompuesto());
        }
    }
}
