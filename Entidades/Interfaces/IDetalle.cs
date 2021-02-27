using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IDetalle
    {
        Articulo Articulo { get; set; }
        int Cantidad { get; set; }
    }
}
