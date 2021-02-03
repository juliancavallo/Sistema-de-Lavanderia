using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class BultoCompuesto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public List<BultoCompuestoDetalle> Detalle { get; set; }
    }
}
