using Entidades.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class AuditoriaDetalle : IDetalle
    {
        public int Cantidad { get; set; }

        #region Entidades
        public Articulo Articulo { get; set; }
        public Auditoria Auditoria { get; set; }
        #endregion
    }
}
