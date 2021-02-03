using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Vistas
{
    public class AjusteStockVista
    {
        //IdArticulo e IdUbicacion se utilizan para obtener las entidades 
        //por Id en la conversion a AjusteStock
        public int IdArticulo { get; set; }
        public int IdUbicacion { get; set; }
        public string Articulo { get; set; }
        public string Ubicacion { get; set; }
        [System.ComponentModel.DisplayName("Cantidad previa")]
        public int CantidadPrevia { get; set; }
        [System.ComponentModel.DisplayName("Nueva cantidad")]
        public int NuevaCantidad { get; set; }
        public string Observaciones { get; set; }
        [System.ComponentModel.DisplayName("Fecha de creacion")]
        public string FechaCreacion { get; set; }
        public string Usuario { get; set; }
    }
}
