using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Vistas
{
    public class ArticuloVista
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        [System.ComponentModel.DisplayName("Tipo de Prenda")]
        public string TipoDePrenda { get; set; }
        public string Color { get; set; }
        public string Talle { get; set; }
        [System.ComponentModel.DisplayName("Precio Unitario")]
        public string PrecioUnitario { get; set; }

        [System.ComponentModel.DisplayName("Peso Unitario")]
        public string PesoUnitario { get; set; }
    }
}
