using BLL.Vistas;
using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class UsuarioBLL : IAdministrable<Usuario>
    {
        MapperUsuario mpp = new MapperUsuario();
        ParametroDelSistemaBLL parametroDelSistemaBLL = new ParametroDelSistemaBLL();

        public void Alta(Usuario obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.NombreDeUsuario == obj.NombreDeUsuario))
                    throw new Exception("Ya existe un usuario con el mismo nombre");

                if (ObtenerTodos().Any(x => x.DNI == obj.DNI))
                    throw new Exception("Ya existe un usuario con el mismo DNI");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Usuario obj)
        {
            try
            {
                mpp.Baja(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(Usuario obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.NombreDeUsuario == obj.NombreDeUsuario && x.Id != obj.Id))
                    throw new Exception("Ya existe un usuario con el mismo nombre");

                if (ObtenerTodos().Any(x => x.DNI == obj.DNI && x.Id != obj.Id))
                    throw new Exception("Ya existe un usuario con el mismo DNI");

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Obtener(int id)
        {
            try
            {
                return mpp.Obtener(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Obtener(string nombreDeUsuario)
        {
            try
            {
                return mpp.Obtener(nombreDeUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> ObtenerTodos()
        {
            try
            {
                return mpp.ObtenerTodos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> ObtenerPorRol(int idRol)
        {
            try
            {
                return mpp.ObtenerTodos().Where(x => x.Rol.Id == idRol).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> ObtenerTodos(string nombre, string apellido, string dni, string nombreDeUsuario, string correo, int idUbicacion)
        {
            try
            {
                var lista = this.ObtenerTodos().AsEnumerable();

                if (!string.IsNullOrWhiteSpace(nombre))
                    lista = lista.Where(x => x.Nombre.ToLower().Contains(nombre.ToLower()));

                if (!string.IsNullOrWhiteSpace(apellido))
                    lista = lista.Where(x => x.Apellido.ToLower().Contains(apellido.ToLower()));

                if (!string.IsNullOrWhiteSpace(nombreDeUsuario))
                    lista = lista.Where(x => x.NombreDeUsuario.ToLower().Contains(nombreDeUsuario.ToLower()));

                if (!string.IsNullOrWhiteSpace(dni))
                    lista = lista.Where(x => x.DNI.ToString().ToLower().Contains(dni.ToLower()));
                
                if (!string.IsNullOrWhiteSpace(correo))
                    lista = lista.Where(x => x.Correo.ToLower().Contains(correo.ToLower()));

                if (idUbicacion > 0)
                    lista = lista.Where(x => x.Ubicacion.Id == idUbicacion);

                return lista.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 

        public List<UsuarioVista> ObtenerTodosParaVista(string nombre = "", string apellido = "",
            string dni = "", string nombreDeUsuario = "", string correo = "", int idUbicacion = 0)
        {
            var lista = this.ObtenerTodos(nombre, apellido, dni, nombreDeUsuario, correo, idUbicacion).AsEnumerable();
            var rolesValidos = parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.RolesAdministradoresDeUsuarios).Valor.Split(',');

            if(!rolesValidos.Contains(SeguridadBLL.usuarioLogueado.Rol.Descripcion))
                lista = lista.Where(x => x.Id == SeguridadBLL.usuarioLogueado.Id);
            
            return lista.Select(x =>
                new UsuarioVista()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Apellido = x.Apellido,
                    DNI = x.DNI.ToString(),
                    NombreDeUsuario = x.NombreDeUsuario,
                    Correo = x.Correo,
                    Ubicacion = x.Ubicacion.Descripcion
                }).ToList();
        }
    }
}
