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

namespace UI.Formularios.Seguridad.Usuarios
{
    public partial class frmAdministracionUsuarios : Form
    {
        public frmAdministracionUsuarios()
        {
            InitializeComponent();
            ControladorAdministracionUsuarios ctrl = new ControladorAdministracionUsuarios(this, new frmAltaUsuario());
        }
    }
}
