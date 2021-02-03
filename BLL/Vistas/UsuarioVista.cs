using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Vistas
{
    public class UsuarioVista
    {
        public int Id { get; set; }

        [System.ComponentModel.DisplayName("Nombre de Usuario")]
        public string NombreDeUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string DNI { get; set; }
        public string Correo { get; set; }
        public string Ubicacion { get; set; }
    }
}
