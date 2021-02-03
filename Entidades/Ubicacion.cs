using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Entidades
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public int TipoDeUbicacion { get; set; }
        public bool EsUbicacionInterna { get { return this.UbicacionPadre != null; } }

        #region Entidades
        public Ubicacion UbicacionPadre { get; set; }
        #endregion
    }
}
