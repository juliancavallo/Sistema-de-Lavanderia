using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ParametroDelSistemaBLL
    {
        MapperParametroDelSistema mpp = new MapperParametroDelSistema();

        public void Alta(ParametroDelSistema obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Nombre == obj.Nombre))
                    throw new Exception("Ya existe un parametro con el mismo nombre");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ParametroDelSistema Obtener(string nombre)
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

        public List<ParametroDelSistema> ObtenerTodos()
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
