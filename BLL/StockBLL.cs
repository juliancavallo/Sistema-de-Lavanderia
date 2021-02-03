using BLL.Vistas;
using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class StockBLL : IAdministrable<Stock>
    {
        MapperStock mpp = new MapperStock();
        MapperUbicacion mppUbicacion = new MapperUbicacion();
        MapperArticulo mppArticulo = new MapperArticulo();

        public void Alta(Stock obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => 
                    x.Articulo.Id == obj.Articulo.Id &&
                    x.Ubicacion.Id == obj.Ubicacion.Id))
                    throw new Exception("Ya existe un stock con la misma configuracion");

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Baja(Stock obj)
        {
            try
            {
                mpp.Baja(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(Stock obj)
        {
            try
            {
                if (ObtenerTodos().Any(x =>
                    x.Id != obj.Id &&
                    x.Articulo.Id == obj.Articulo.Id &&
                    x.Ubicacion.Id == obj.Ubicacion.Id))
                    throw new Exception("Ya existe un stock con la misma configuracion");

                mpp.Modificacion(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidarStock(int idArticulo, int idUbicacion, int cantidad)
        {
            var stock = this.ObtenerTodos(idUbicacion, idArticulo);

            return stock.Count > 0 && stock.FirstOrDefault().Cantidad >= cantidad;
        }

        public void Enviar(List<Envio> envios)
        {
            //Se restan las prendas enviadas de stock de cada ubicacion
            //Ver de que manera se pueden optimizar las siguientes lineas de codigo
            var stockParaActualizar = new List<Stock>();
            foreach (var envio in envios)
            {
                int idUbicacion = envio.UbicacionOrigen.Id;
                foreach (var item in envio.Detalle)
                {
                    int idArticulo = item.Articulo.Id;
                    int cantidad = item.Cantidad;

                    Stock stock = stockParaActualizar.Where(x => x.Articulo.Id == idArticulo && x.Ubicacion.Id == idUbicacion).FirstOrDefault();

                    if (stock == null)
                    {
                        stock = mpp.Obtener(idUbicacion, idArticulo);
                    }

                    if ((stock.Cantidad - cantidad) < 0)
                    {
                        throw new Exception(
                           string.Format("La hoja de ruta no puede ser creada ya que el" +
                           " stock disponible del articulo {0} es menor que el indicado en el envio {1}",
                           item.Articulo.Codigo, envio.Id.ToString()));
                    }
                    else
                    {
                        stock.Cantidad -= cantidad;
                        stockParaActualizar.Add(stock);
                    }
                }
            }

            stockParaActualizar.ForEach(x => mpp.Modificacion(x));
        }

        public void Recibir(Recepcion recepcion)
        {
            foreach (var detalle in recepcion.Detalle)
            {
                int idArticulo = detalle.Articulo.Id;
                int cantidad = detalle.CantidadRecibida;

                var stock = this.Obtener(recepcion.UbicacionDestino.Id, idArticulo);
                if (stock == null)
                {
                    stock = new Stock()
                    {
                        Ubicacion = recepcion.UbicacionDestino,
                        Articulo = detalle.Articulo,
                        Cantidad = 0
                    };
                    this.Alta(stock);
                }
                stock.Cantidad += cantidad;
                this.Modificacion(stock);
            }
        }

        public void Recibir(Envio envio)
        {
            foreach (var detalle in envio.Detalle)
            {
                int idArticulo = detalle.Articulo.Id;
                int cantidad = detalle.Cantidad;

                var stock = this.Obtener(envio.UbicacionDestino.Id, idArticulo);
                if (stock == null)
                {
                    stock = new Stock()
                    {
                        Ubicacion = envio.UbicacionDestino,
                        Articulo = detalle.Articulo,
                        Cantidad = 0
                    };
                    this.Alta(stock);
                }
                stock.Cantidad += cantidad;
                this.Modificacion(stock);
            }
        }


        #region Obtencion
        public Stock Obtener(int id)
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

        public Stock Obtener(int idUbicacion, int idArticulo)
        {
            try
            {
                return mpp.Obtener(idUbicacion, idArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Stock> ObtenerTodos()
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

        public List<Stock> ObtenerTodos(int idUbicacion, int idArticulo, List<int> ubicacionesPorDefecto = null )
        {
            try
            {
                var lista = this.ObtenerTodos().AsEnumerable();

                if (idUbicacion > 0)
                    lista = lista.Where(x => x.Ubicacion.Id == idUbicacion);
                else
                    lista = lista.Where(x => ubicacionesPorDefecto.Contains(x.Ubicacion.Id));

                if (idArticulo > 0)
                    lista = lista.Where(x => x.Articulo.Id == idArticulo);

                return lista.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StockVista> ObtenerTodosParaVista(List<int> ubicacionesPorDefecto, int idUbicacion = 0, int idArticulo = 0)
        {
            return this.ObtenerTodos(idUbicacion, idArticulo, ubicacionesPorDefecto).Select(x =>
            new StockVista()
            {
                Id = x.Id,
                Ubicacion = x.Ubicacion.Descripcion,
                Articulo = x.Articulo.Codigo,
                Cantidad = x.Cantidad.ToString()
            }).ToList();
        }

        #endregion

        #region Ajuste de Stock
        public List<AjusteStockVista> ConvertirAVistaAjuste(List<Stock> lista)
        {
            return lista.Select(x => new AjusteStockVista()
            {
                IdArticulo = x.Articulo.Id,
                Articulo = x.Articulo.Codigo,
                Ubicacion = x.Ubicacion.Descripcion,
                CantidadPrevia = x.Cantidad,
                NuevaCantidad = 0,
                IdUbicacion = x.Ubicacion.Id
            }).ToList();
        }

        public List<AjusteStockVista> ConvertirAVistaAjuste(List<AjusteStock> lista)
        {
            return lista.Select(x => new AjusteStockVista()
            {
                IdArticulo = x.Articulo.Id,
                Articulo = x.Articulo.Codigo,
                Ubicacion = x.Ubicacion.Descripcion,
                CantidadPrevia = x.CantidadPrevia,
                NuevaCantidad = x.NuevaCantidad,
                Observaciones = x.Observaciones,
                IdUbicacion = x.Ubicacion.Id,
                FechaCreacion = x.FechaCreacion.ToString(),
                Usuario = x.Usuario?.NombreDeUsuario
            }).ToList();
        }

        public List<AjusteStock> ConvertirAAjusteStock(List<AjusteStockVista> lista)
        {
            return lista.Select(x => new AjusteStock()
            {
                Articulo = mppArticulo.Obtener(x.IdArticulo),
                CantidadPrevia = x.CantidadPrevia,
                NuevaCantidad = x.NuevaCantidad,
                Ubicacion = mppUbicacion.Obtener(x.IdUbicacion),
                Observaciones = x.Observaciones
            }).ToList();
        }

        public List<AjusteStockVista> AjustarVistaPorAuditoria(List<AjusteStockVista> lista, Auditoria auditoria)
        {
            var listaAjuste = this.ConvertirAAjusteStock(lista);
            auditoria.Detalle.ForEach(x => 
            {
                var item = listaAjuste.Find(l => l.Articulo.Id == x.Articulo.Id);
                if(item == null)
                {
                    var nuevoAjuste = new AjusteStock()
                    {
                        Articulo = x.Articulo,
                        CantidadPrevia = 0,
                        NuevaCantidad = x.Cantidad,
                        Observaciones = "Ajuste por auditoria",
                        Ubicacion = auditoria.Ubicacion
                    };

                    listaAjuste.Add(nuevoAjuste);
                }
                else
                {
                    item.NuevaCantidad = x.Cantidad;
                    item.Observaciones = "Ajuste por auditoria";
                }
            });

            return this.ConvertirAVistaAjuste(listaAjuste);
        }

        public void AjustarPorAuditoria(List<AjusteStockVista> lista)
        {
            var listaAjuste = this.ConvertirAAjusteStock(lista);
            listaAjuste.ForEach(x =>
            {
                x.FechaCreacion = DateTime.Now;
                x.Usuario = SeguridadBLL.usuarioLogueado;
            });

            mpp.Alta(listaAjuste);

            listaAjuste.ForEach(x => 
            {
                var item = this.Obtener(x.Ubicacion.Id, x.Articulo.Id);
                if (item != null)
                {
                    item.Cantidad = x.NuevaCantidad;
                    this.Modificacion(item);
                }
                else
                {
                    this.Alta(new Stock()
                    {
                        Articulo = x.Articulo,
                        Ubicacion = x.Ubicacion,
                        Cantidad = x.NuevaCantidad
                    });
                }
            });
        }

        public List<AjusteStock> ObtenerAjustesStock()
        {
            try
            {
                return mpp.ObtenerAjustesStock();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AjusteStock> ObtenerAjustesStock(int idUbicacion, int idArticulo,
            DateTime? fechaDesde, DateTime? fechaHasta, List<int> ubicacionesPorDefecto) 
        {
            try
            {
                var lista = ObtenerAjustesStock().AsEnumerable();

                if (idArticulo > 0)
                    lista = lista.Where(x => x.Articulo.Id == idArticulo);

                if (idUbicacion > 0)
                    lista = lista.Where(x => x.Ubicacion.Id == idUbicacion);
                else
                    lista = lista.Where(x => ubicacionesPorDefecto.Contains(x.Ubicacion.Id));

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

        public List<AjusteStockVista> ObtenerAjustesParaVista(List<int> ubicacionesPorDefecto, 
            int idUbicacion = 0, int idArticulo = 0, DateTime? fechaDesde = null, 
            DateTime? fechaHasta = null)
        {
            return ObtenerAjustesStock(idUbicacion, idArticulo, fechaDesde, fechaHasta ,ubicacionesPorDefecto)
                .Select(x => new AjusteStockVista() 
                {
                    Articulo = x.Articulo.Codigo,
                    Ubicacion = x.Ubicacion.Descripcion,
                    CantidadPrevia = x.CantidadPrevia,
                    NuevaCantidad = x.NuevaCantidad,
                    IdArticulo = x.Articulo.Id,
                    IdUbicacion = x.Ubicacion.Id,
                    Observaciones = x.Observaciones,
                    FechaCreacion = x.FechaCreacion.ToString(),
                    Usuario = x.Usuario.NombreDeUsuario
                }).ToList();
        }

        #endregion
    }
}
