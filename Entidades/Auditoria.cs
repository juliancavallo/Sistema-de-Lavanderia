using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Auditoria
    {
        public int Id { get; set; }
        public DateTime FechaDeCreacion { get; set; }

        #region Entidades
        public Ubicacion Ubicacion { get; set; }
        public Usuario Usuario { get; set; }
        public List<AuditoriaDetalle> Detalle { get; set; }
        #endregion
    }
}
