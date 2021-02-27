using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class RecepcionDetalle : IDetalle
    {
        public int Cantidad { get; set; }
        public int CantidadARecibir { get; set; }
        public decimal PrecioUnitario { get; set; }

        #region Entidades
        public Recepcion Recepcion { get; set; }
        public Articulo Articulo { get; set; }
        #endregion
    }
}
