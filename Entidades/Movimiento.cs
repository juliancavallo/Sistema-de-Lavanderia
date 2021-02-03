using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Entidades
{
    public class Movimiento
    {
        public string Numero { get; set; }
        public string Descripcion { get; set; }
        public Usuario Usuario { get; set; }
        public Ubicacion UbicacionOrigen { get; set; }
        public Ubicacion UbicacionDestino { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
