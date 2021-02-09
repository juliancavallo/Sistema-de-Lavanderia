﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Vistas
{
    public class EnvioVista
    {
        public int Id { get; set; }
        public string Numero { get; set; }

        [System.ComponentModel.DisplayName("Ubicacion Origen")]
        public string UbicacionOrigen { get; set; }
        [System.ComponentModel.DisplayName("Ubicacion Destino")]
        public string UbicacionDestino { get; set; }
        [System.ComponentModel.DisplayName("Fecha de Creacion")]
        public string FechaDeCreacion { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        [System.ComponentModel.DisplayName("Facturación Total")]
        public string FacturacionTotal { get; set; }
    }
}
