using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class BultoCompuestoBLL : IAdministrable<BultoCompuesto>
    {
        MapperBultoCompuesto mpp = new MapperBultoCompuesto();
        MapperEnvioDetalle mapperEnvioDetalle = new MapperEnvioDetalle();

        public void Alta(BultoCompuesto obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe un bulto con la misma descripcion");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(BultoCompuesto obj)
        {
            try
            {
                if (mapperEnvioDetalle.ObtenerTodos().Where(x => obj.Detalle.Select(o => o.TipoDePrenda.Id).Contains(x.Articulo.TipoDePrenda.Id)).Count() > 0)
                    throw new Exception("El bulto no se puede eliminar porque hay al menos un envío asociado al mismo");

                mpp.Baja(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(BultoCompuesto obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion && x.Id != obj.Id))
                    throw new Exception("Ya existe un bulto con la misma descripcion");

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BultoCompuesto Obtener(int id)
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
        public List<BultoCompuesto> ObtenerTodos(int idTipoDePrenda)
        {
            try
            {
                return mpp.ObtenerTodos().Where(x => x.Detalle.Any(d => d.TipoDePrenda.Id == idTipoDePrenda)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BultoCompuesto> ObtenerTodos()
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

        public List<BultoCompuesto> ObtenerTodos(string descripcion)
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
