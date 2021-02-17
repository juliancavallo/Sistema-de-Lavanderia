using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class TipoDeUbicacionBLL
    {
        MapperTipoDeUbicacion mpp = new MapperTipoDeUbicacion();

        public void Alta(TipoDeUbicacion obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe un tipo de ubicacion con el mismo nombre");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoDeUbicacion Obtener(string nombre)
        {
            try
            {
                return mpp.Obtener(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoDeUbicacion Obtener(int id)
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

        public List<TipoDeUbicacion> ObtenerTodos()
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
    }
}
