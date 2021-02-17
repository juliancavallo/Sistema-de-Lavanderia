using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Entidades
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public bool EsUbicacionInterna { get { return this.UbicacionPadre != null; } }
        public bool ClienteExterno { get; set; }
        public decimal CapacidadTotal { get; set; }
        public decimal CapacidadDisponible 
        { 
            get 
            {
                return this.CapacidadTotal - this.Stock.Sum(x => x.Cantidad * x.Articulo.PesoUnitario); 
            } 
        }

        #region Entidades
        public Ubicacion UbicacionPadre { get; set; }
        public List<Stock> Stock { get; set; }
        public TipoDeUbicacion TipoDeUbicacion { get; set; }
        #endregion
    }
}
