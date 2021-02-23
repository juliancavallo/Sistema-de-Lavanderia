using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class TipoDePrendaBLL : IAdministrable<TipoDePrenda>
    {
        MapperTipoDePrenda mpp = new MapperTipoDePrenda();
        MapperArticulo mppArticulo = new MapperArticulo();

        public void Alta(TipoDePrenda obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe un tipo de prenda con la misma descripcion");

                if (!obj.UsaCortePorBulto)
                    obj.CortePorBulto = 1;

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(TipoDePrenda obj)
        {
            try
            {
                if (mppArticulo.ObtenerTodos().Where(x => x.TipoDePrenda.Id == obj.Id).Count() > 0)
                    throw new Exception("El tipo de prenda no se puede elimianr porque hay al menos un artículo asociado al mismo");

                mpp.Baja(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(TipoDePrenda obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion && x.Id != obj.Id))
                    throw new Exception("Ya existe un tipo de prenda con la misma descripcion");

                if (!obj.UsaCortePorBulto)
                    obj.CortePorBulto = 1;

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoDePrenda Obtener(int id)
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

        public TipoDePrenda Obtener(string descripcion)
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

        public List<TipoDePrenda> ObtenerTodos()
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


        public List<TipoDePrenda> ObtenerTodos(string descripcion)
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
