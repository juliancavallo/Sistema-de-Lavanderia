using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class TipoDePrenda
    {
        [System.ComponentModel.DisplayName("Id")]
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Descripcion")]
        public string Descripcion { get; set; }

        [System.ComponentModel.DisplayName("Usa corte por bulto")]
        public bool UsaCortePorBulto { get; set; }

        [System.ComponentModel.DisplayName("Corte por bulto")]
        public int CortePorBulto { get; set; }
        public Categoria Categoria { get; set; }
    }
}
