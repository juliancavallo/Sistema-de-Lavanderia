using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class CategoriaBLL : IAdministrable<Categoria>
    {
        MapperCategoria mpp = new MapperCategoria();
        MapperTipoDePrenda mppTipoDePrenda = new MapperTipoDePrenda();

        public void Alta(Categoria obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe una Categoria con la misma descripcion");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Categoria obj)
        {
            try
            {
                if (mppTipoDePrenda.ObtenerTodos().Where(x => x.Categoria.Id == obj.Id).Count() > 0)
                    throw new Exception("La Categoria no se puede eliminar porque hay al menos un tipo de prenda asociado a la misma");

                mpp.Baja(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(Categoria obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion && x.Id != obj.Id))
                    throw new Exception("Ya existe una Categoria con la misma descripcion");

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Categoria Obtener(int id)
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

        public List<Categoria> ObtenerTodos()
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

        public List<Categoria> ObtenerTodos(string descripcion)
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
