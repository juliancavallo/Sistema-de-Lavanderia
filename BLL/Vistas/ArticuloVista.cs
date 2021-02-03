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
    }
}
