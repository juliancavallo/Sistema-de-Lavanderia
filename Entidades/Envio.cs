using Entidades.Clases_Padre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Envio : Proceso
    {
        public int Id { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public decimal PesoTotal 
        { 
            get
            {
                return this.Detalle.Sum(x => x.Cantidad * x.Articulo.PesoUnitario);
            } 
        }

        #region Entidades
        public List<EnvioDetalle> Detalle { get; set; }
        public EstadoEnvio Estado { get; set; }
        public HojaDeRuta HojaDeRuta { get; set; }
        public Ubicacion UbicacionOrigen { get; set; }
        public Ubicacion UbicacionDestino { get; set; }
        #endregion
    }
}
