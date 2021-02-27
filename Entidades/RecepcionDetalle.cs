using Entidades.Clases_Padre;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class RecepcionDetalle : Detalle
    {
        public int CantidadARecibir { get; set; }
        public decimal PrecioUnitario { get; set; }

        #region Entidades
        public Recepcion Recepcion { get; set; }
        #endregion
    }
}
