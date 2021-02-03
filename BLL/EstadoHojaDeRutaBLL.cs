using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class EstadoHojaDeRutaBLL
    {
        MapperEstadoHojaDeRuta mpp = new MapperEstadoHojaDeRuta();

        public void Alta(EstadoHojaDeRuta obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe un estado con la misma descripcion");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoHojaDeRuta Obtener(string nombre)
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

        public List<EstadoHojaDeRuta> ObtenerTodos()
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
