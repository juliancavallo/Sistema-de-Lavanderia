using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Vistas
{
    public class RecepcionDetalleVista
    {
        public int IdArticulo { get; set; }
        [System.ComponentModel.DisplayName("Tipo de Prenda")]
        public string TipoDePrenda { get; set; }
        public string Color { get; set; }
        public string Talle { get; set; }
        public string Articulo { get; set; }

        [System.ComponentModel.DisplayName("Cantidad a recibir")]
        public int CantidadARecibir { get; set; }
        [System.ComponentModel.DisplayName("Cantidad recibida")]
        public int Cantidad { get; set; }
    }
}
