using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BLL
{
    public class TalleBLL : IAdministrable<Talle>
    {
        MapperTalle mpp = new MapperTalle();
        MapperArticulo mppArticulo = new MapperArticulo();

        public void Alta(Talle obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe un talle con la misma descripcion");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Talle obj)
        {
            try
            {
                if (mppArticulo.ObtenerTodos().Where(x => x.Talle.Id == obj.Id).Count() > 0)
                    throw new Exception("El talle no se puede elimianr porque hay al menos un artículo asociado al mismo");

                mpp.Baja(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(Talle obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion && x.Id != obj.Id))
                    throw new Exception("Ya existe un talle con la misma descripcion");

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Talle Obtener(int id)
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

        public Talle Obtener(string descripcion)
        {
            try
            {
                return mpp.ObtenerTodos().FirstOrDefault(x => x.Descripcion.ToLower() == descripcion.ToLower());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Talle> ObtenerTodos()
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

        public List<Talle> ObtenerTodos(string descripcion)
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
