﻿using Controlador.Procesos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Formularios.Procesos.EnvioAClinica
{
    public partial class frmRecepcionEnLavadero : Form
    {
        public frmRecepcionEnLavadero()
        {
            InitializeComponent();
            ControladorRecepcionEnLavadero ctrl = new ControladorRecepcionEnLavadero(this);
        }
    }
}
