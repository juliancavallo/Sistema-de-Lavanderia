using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Rol
    {
        [System.ComponentModel.DisplayName("Id")]
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Descripcion")]
        public string Descripcion { get; set; }
        
        #region Entidades
        public List<Menu> Menus { get; set; }
        #endregion
    }
}
