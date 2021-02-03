using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Vistas
{
    public class AuditoriaDetalleVista
    {
        public int IdArticulo { get; set; }
        public string Articulo { get; set; }
        [System.ComponentModel.DisplayName("Tipo de Prenda")]
        public string TipoDePrenda { get; set; }
        public string Color { get; set; }
        public string Talle { get; set; }
        public string Cantidad { get; set; }
    }
}
