using BLL.Vistas;
using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class AuditoriaBLL : IProcesable<Auditoria>
    {
        MapperAuditoria mpp = new MapperAuditoria();
        StockBLL stockBLL = new StockBLL();

        public void Alta(Auditoria obj)
        {
            try
            {
                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Auditoria Obtener(int id)
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

        public List<Auditoria> ObtenerTodos()
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

        public List<Auditoria> ObtenerTodos(int numero, int idUbicacion, 
            DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try
            {
                var lista = ObtenerTodos().AsEnumerable();

                if (numero > 0)
                    lista = lista.Where(x => x.Id == numero);

                if (idUbicacion > 0)
                    lista = lista.Where(x => x.Ubicacion.Id == idUbicacion);

                if (fechaDesde.HasValue)
                    lista = lista.Where(x => x.FechaCreacion > fechaDesde);

                if (fechaHasta.HasValue)
                    lista = lista.Where(x => x.FechaCreacion < fechaHasta);


                return lista.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AuditoriaVista> ObtenerTodosParaVista(
            int numero = -1, int idUbicacion = 0, DateTime? fechaDesde = null, 
            DateTime? fechaHasta = null)
        {
            return this.ObtenerTodos(numero, idUbicacion, fechaDesde, fechaHasta).Select(x =>
            new AuditoriaVista() 
            { 
                Id = x.Id,
                Fecha = x.FechaCreacion.ToString(),
                Numero = x.Id.ToString(),
                Ubicacion = x.Ubicacion.Descripcion,
                Usuario = x.Usuario.NombreDeUsuario
            }).ToList();
        }

        public List<AuditoriaDetalleVista> ConvertirDetalleAVista(List<AuditoriaDetalle> detalle)
        {
            return detalle.Select(x =>
            new AuditoriaDetalleVista()
            {
                IdArticulo = x.Articulo.Id,
                Articulo = x.Articulo.Codigo,
                Cantidad = x.Cantidad.ToString(),
                TipoDePrenda = x.Articulo.TipoDePrenda.Descripcion,
                Color = x.Articulo.Color.Descripcion,
                Talle = x.Articulo.Talle.Descripcion
            }).ToList();
        }

        public void ActualizarStock(Auditoria auditoria)
        {
            try
            {
                foreach(var detalle in auditoria.Detalle)
                {
                    var stock = stockBLL.Obtener(auditoria.Ubicacion.Id, detalle.Articulo.Id);
                    if(stock == null)
                    {
                        stockBLL.Alta(new Stock() 
                        {
                            Articulo = detalle.Articulo,
                            Ubicacion = auditoria.Ubicacion,
                            Cantidad = detalle.Cantidad
                        });
                    }
                    else
                    {
                        stock.Cantidad = detalle.Cantidad;
                        stockBLL.Modificacion(stock);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
