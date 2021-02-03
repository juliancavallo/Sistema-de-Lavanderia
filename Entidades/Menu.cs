﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Entidades
{
    public abstract class Menu
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Simbolo { get; set; }
    }
}
