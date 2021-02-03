using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class AjusteStock
    {
        public int Id { get; set; }
        public string Observaciones { get; set; }
        public int CantidadPrevia { get; set; }
        public int NuevaCantidad { get; set; }
        public Articulo Articulo { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Usuario Usuario { get; set; }
    }
}
