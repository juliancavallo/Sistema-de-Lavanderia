using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Recepcion
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }

        #region Entidades
        public List<RecepcionDetalle> Detalle { get; set; }
        public Usuario Usuario { get; set; }
        public HojaDeRuta HojaDeRuta { get; set; }
        public Ubicacion UbicacionOrigen { get; set; }
        public Ubicacion UbicacionDestino { get; set; }
        #endregion
    }
}
