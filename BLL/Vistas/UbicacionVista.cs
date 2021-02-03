using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Vistas
{
    public class UbicacionVista
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        [System.ComponentModel.DisplayName("Ubicacion Padre")]
        public string UbicacionPadre { get; set; }
        [System.ComponentModel.DisplayName("Tipo de Ubicacion")]
        public string TipoDeUbicacion { get; set; }
    }
}
