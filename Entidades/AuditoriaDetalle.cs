using Entidades.Clases_Padre;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class AuditoriaDetalle : Detalle
    {
        #region Entidades
        public Auditoria Auditoria { get; set; }
        #endregion
    }
}
