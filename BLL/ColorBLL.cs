using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ColorBLL : IAdministrable<Color>
    {
        MapperColor mpp = new MapperColor();
        MapperArticulo mppArticulo = new MapperArticulo();

        public void Alta(Color obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe un color con la misma descripcion");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Color obj)
        {
            try
            {
                if (mppArticulo.ObtenerTodos().Where(x => x.Color.Id == obj.Id).Count() > 0)
                    throw new Exception("El color no se puede elimianr porque hay al menos un artículo asociado al mismo");

                mpp.Baja(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(Color obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion && x.Id != obj.Id))
                    throw new Exception("Ya existe un color con la misma descripcion");

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Color Obtener(int id)
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

        public Color Obtener(string descripcion)
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

        public List<Color> ObtenerTodos()
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

        public List<Color> ObtenerTodos(string descripcion)
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
