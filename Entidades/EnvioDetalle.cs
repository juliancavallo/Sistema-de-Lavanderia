using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EnvioDetalle
    {
        public int Cantidad { get; set; }

        #region Entidades
        public Articulo Articulo { get; set; }
        public Envio Envio { get; set; }
        #endregion
    }
}
