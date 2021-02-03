using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class Categoria
    {
        [System.ComponentModel.DisplayName("Id")]
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Descripcion")]
        public string Descripcion { get; set; }

        [System.ComponentModel.DisplayName("Categoría compuesta")]
        public bool EsCompuesta { get; set; }
    }
}
