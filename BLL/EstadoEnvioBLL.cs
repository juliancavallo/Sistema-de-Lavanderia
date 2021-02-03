using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class EstadoEnvioBLL
    {
        MapperEstadoEnvio mpp = new MapperEstadoEnvio();

        public void Alta(EstadoEnvio obj)
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

        public EstadoEnvio Obtener(string nombre)
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

        public List<EstadoEnvio> ObtenerTodos()
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
