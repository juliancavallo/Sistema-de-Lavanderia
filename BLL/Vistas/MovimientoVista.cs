using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Vistas
{
    public class MovimientoVista
    {
        public string Descripcion { get; set; }
        public string Numero { get; set; }
        [System.ComponentModel.DisplayName("Ubicacion Origen")]
        public string UbicacionOrigen { get; set; }
        [System.ComponentModel.DisplayName("Ubicacion Destino")]
        public string UbicacionDestino { get; set; }
        public string Fecha { get; set; }
        public string Usuario { get; set; }
    }
}
