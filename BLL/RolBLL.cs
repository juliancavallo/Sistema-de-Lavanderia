using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class RolBLL : IAdministrable<Rol>
    {
        MapperRol mpp = new MapperRol();
        UsuarioBLL usuarioBll = new UsuarioBLL();

        public void Alta(Rol obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe un rol con la misma descripcion");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Rol obj)
        {
            try
            {
                if (usuarioBll.ObtenerPorRol(obj.Id).Count == 0)
                {
                    if (this.ObtenerTodos().Where(x => x.Id != obj.Id 
                        && x.Menus.Any(m => m.Codigo == Entidades.Enums.CodigoMenu.Seguridad)).Count() > 0)
                        mpp.Baja(obj);
                    else
                        throw new Exception("El rol no se puede eliminar ya que es el unico que puede acceder a la seguridad del sistema");
                }
                else
                    throw new Exception("El rol no se puede eliminar porque hay usuarios asociados al mismo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(Rol obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion && x.Id != obj.Id))
                    throw new Exception("Ya existe un rol con la misma descripcion");

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Rol Obtener(int id)
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

        public List<Rol> ObtenerTodos()
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

        public List<Rol> ObtenerTodos(string descripcion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(descripcion))
                    return mpp.ObtenerTodos();
                else
                    return mpp.ObtenerTodos().Where(x => x.Descripcion.ToLower().Contains(descripcion.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
