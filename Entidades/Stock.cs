using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Stock
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }

        #region Entidades
        public Ubicacion Ubicacion { get; set; }
        public Articulo Articulo { get; set; }
        #endregion
    }
}
