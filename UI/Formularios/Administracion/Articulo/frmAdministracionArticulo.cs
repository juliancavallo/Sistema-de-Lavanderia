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

namespace UI.Formularios.Administracion.Articulo
{
    public partial class frmAdministracionArticulo : Form
    {
        public frmAdministracionArticulo()
        {
            InitializeComponent();
            ControladorAdministracionArticulo ctrl = new ControladorAdministracionArticulo(this, new frmAltaArticulo());
        }
    }
}
