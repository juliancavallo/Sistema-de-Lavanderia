using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public string Correo { get; set; }

        #region Entidades
        public Rol Rol { get; set; }
        public Ubicacion Ubicacion { get; set; }
        public List<Menu> Menus 
        { 
            get
            {
                return this.Rol.Menus.ToList();
            } 
        }
        #endregion
    }
}
