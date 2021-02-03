using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Envio
    {
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaRecepcion { get; set; }

        #region Entidades
        public List<EnvioDetalle> Detalle { get; set; }
        public EstadoEnvio Estado { get; set; }
        public HojaDeRuta HojaDeRuta { get; set; }
        public Usuario Usuario { get; set; }
        public Ubicacion UbicacionOrigen { get; set; }
        public Ubicacion UbicacionDestino { get; set; }
        #endregion
    }
}
