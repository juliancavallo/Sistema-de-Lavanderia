using BLL.Vistas;
using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class UbicacionBLL : IAdministrable<Ubicacion>
    {
        MapperUbicacion mpp = new MapperUbicacion();
        MapperStock mppStock = new MapperStock();
        MapperAuditoria mppAuditoria = new MapperAuditoria();

        public void Alta(Ubicacion obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion))
                    throw new Exception("Ya existe una ubicacion con la misma descripcion");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Ubicacion obj)
        {
            try
            {
                if (mppStock.ObtenerTodos().Where(x => x.Ubicacion.Id == obj.Id).Count() > 0)
                    throw new Exception("La ubicación no se puede eliminar porque hay al menos una configuración de stock asociada al mismo");

                if (mppAuditoria.ObtenerTodos().Where(x => x.Ubicacion.Id == obj.Id).Count() > 0)
                    throw new Exception("La ubicación no se puede eliminar porque hay al menos una auditoria asociada al mismo");

                mpp.Baja(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(Ubicacion obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Descripcion == obj.Descripcion && x.Id != obj.Id))
                    throw new Exception("Ya existe una ubicacion con la misma descripcion");

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ubicacion Obtener(int id)
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

        public List<Ubicacion> ObtenerTodos()
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
        public List<Ubicacion> ObtenerTodos(int idUbicacionUsuario = 0)
        {
            try
            {
                var lista = mpp.ObtenerTodos().AsEnumerable();

                if (idUbicacionUsuario > 0)
                    lista = lista.Where(x => x.Id == idUbicacionUsuario);

                return lista.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ubicacion> ObtenerTodos(string descripcion, string direccion, int tipoDeUbicacion, int idUbicacionPadre)
        {
            try
            {
                var lista = this.ObtenerTodos().AsEnumerable();

                if (!string.IsNullOrWhiteSpace(descripcion))
                    lista = lista.Where(x => x.Descripcion.ToLower().Contains(descripcion.ToLower()));

                if (!string.IsNullOrWhiteSpace(direccion))
                    lista = lista.Where(x => x.Direccion.ToLower().Contains(direccion.ToLower()));

                if (tipoDeUbicacion > 0)
                    lista = lista.Where(x => x.TipoDeUbicacion == tipoDeUbicacion);

                if (idUbicacionPadre > -1)
                {
                    if (idUbicacionPadre == 0)
                        lista = lista.Where(x => x.UbicacionPadre == null);
                    else
                        lista = lista.Where(x => x.UbicacionPadre.Id == idUbicacionPadre);
                }

                return lista.ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UbicacionVista> ObtenerTodosParaVista(string descripcion = "", string direccion = "", int tipoDeUbicacion = 0, int idUbicacionPadre = -1)
        {
            return this.ObtenerTodos(descripcion, direccion, tipoDeUbicacion, idUbicacionPadre).Select(x =>
                   new UbicacionVista()
                   {
                       Id = x.Id,
                       Descripcion = x.Descripcion,
                       Direccion = x.Direccion,
                       UbicacionPadre = x.UbicacionPadre?.Descripcion,
                       TipoDeUbicacion = ((Entidades.Enums.TipoDeUbicacion)x.TipoDeUbicacion).ToString(),
                       CapacidadDisponible = x.CapacidadDisponible + " Kg"
                   }).ToList();
        }

        public List<Ubicacion> ObtenerSeleccionablesParaUbicacionPadre(int? idUbicacion)
        {
            try
            {
                var lista = mpp.ObtenerTodos().AsEnumerable();

                lista = lista.Where(x => !x.EsUbicacionInterna);

                if (idUbicacion.HasValue)
                    lista = lista.Where(x => x.Id != idUbicacion.Value);

                return lista.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ubicacion> ObtenerPadreOHermanos(Ubicacion ubicacion)
        {
            try
            {
                var lista = mpp.ObtenerTodos().AsEnumerable();

                if (ubicacion.EsUbicacionInterna)
                {
                    lista = lista.Where(x => x.Id != ubicacion.Id);
                    lista = lista.Where(x => x.Id == ubicacion.UbicacionPadre.Id
                        || (x.EsUbicacionInterna && ubicacion.UbicacionPadre.Id == x.UbicacionPadre.Id));
                }
                else
                {
                    lista = lista.Where(x => x.UbicacionPadre != null && x.UbicacionPadre.Id == ubicacion.Id);
                }

                return lista.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
