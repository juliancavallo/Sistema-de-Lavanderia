using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class HojaDeRuta
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Ubicacion UbicacionOrigen { get {return this.Envios.First()?.UbicacionOrigen; } }
        public Ubicacion UbicacionDestino { get { return this.Envios.First()?.UbicacionDestino; } }
        public List<Envio> Envios { get; set; }

        #region Entidades
        public EstadoHojaDeRuta Estado { get; set; }
        public Usuario Usuario { get; set; }
        #endregion
    }
}
