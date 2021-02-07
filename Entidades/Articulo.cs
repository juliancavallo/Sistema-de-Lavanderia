using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public decimal PrecioUnitario { get; set; }
        
        #region Entidades
        public TipoDePrenda TipoDePrenda { get; set; }
        public Color Color { get; set; }
        public Talle Talle { get; set; }
        #endregion
    }
}
