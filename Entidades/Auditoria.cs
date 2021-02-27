using Entidades.Clases_Padre;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Auditoria : Proceso
    {
        public int Id { get; set; }

        #region Entidades
        public List<AuditoriaDetalle> Detalle { get; set; }
        public Ubicacion Ubicacion { get; set; }
        #endregion
    }
}
