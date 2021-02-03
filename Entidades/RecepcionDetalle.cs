using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class RecepcionDetalle
    {
        public int CantidadRecibida { get; set; }
        public int CantidadARecibir { get; set; }

        #region Entidades
        public Recepcion Recepcion { get; set; }
        public Articulo Articulo { get; set; }
        #endregion
    }
}
