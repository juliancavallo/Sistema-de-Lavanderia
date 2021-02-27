using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EnvioDetalle : Detalle
    {
        public decimal PrecioUnitario { get; set; }

        #region Entidades
        public Envio Envio { get; set; }
        #endregion
    }
}
