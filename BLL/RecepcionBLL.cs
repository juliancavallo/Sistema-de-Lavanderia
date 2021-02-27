using BLL.Vistas;
using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BLL
{
    public class RecepcionBLL : IProcesable<Recepcion>
    {
        MapperRecepcion mpp = new MapperRecepcion();
        EnvioBLL envioBLL = new EnvioBLL();
        ArticuloBLL articuloBLL = new ArticuloBLL();
        StockBLL stockBLL = new StockBLL();
        HojaDeRutaBLL hojaDeRutaBLL = new HojaDeRutaBLL();

        public void Alta(Recepcion obj)
        {
            try
            {
                obj.UbicacionOrigen = obj.HojaDeRuta.Envios.First().UbicacionOrigen;
                obj.UbicacionDestino = obj.HojaDeRuta.Envios.First().UbicacionDestino;

                if (!this.ValidarCapacidadDestino(obj.UbicacionDestino, obj.PesoTotal))
                    throw new Exception("La Recepción no se puede crear ya que se está superando la capacidad disponible " +
                    "en la ubicación destino.");

                stockBLL.Recibir(obj);
                hojaDeRutaBLL.Recibir(obj.HojaDeRuta);

                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Recepcion Obtener(int id)
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
        public List<Recepcion> ObtenerTodos()
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
        public List<Recepcion> ObtenerTodos(List<int> ubicacionesPorDefecto,
            int nro, int idHojaDeRuta, 
            DateTime? fechaDesde, DateTime? fechaHasta, int idUbicacionOrigen, 
            int idUbicacionDestino, int tipo)
        {
            try
            {
                var lista = ObtenerTodos().AsEnumerable();

                switch(tipo)
                {
                    case 1:
                        lista = lista.Where(x => x.UbicacionDestino.TipoDeUbicacion.Id == tipo);
                        break;

                    case 2:
                        lista = lista.Where(x => x.UbicacionDestino.TipoDeUbicacion.Id == tipo);
                        break;
                }

                if (nro > 0)
                    lista = lista.Where(x => x.Id == nro);

                if (idHojaDeRuta > 0)
                    lista = lista.Where(x => x.HojaDeRuta.Id == idHojaDeRuta);

                if (idUbicacionOrigen > 0)
                    lista = lista.Where(x => x.UbicacionOrigen.Id == idUbicacionOrigen);

                if (idUbicacionDestino > 0)
                    lista = lista.Where(x => x.UbicacionDestino.Id == idUbicacionDestino);
                else
                    lista = lista.Where(x => ubicacionesPorDefecto.Contains(x.UbicacionDestino.Id));

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
        public List<RecepcionDetalle> ObtenerDetallePorHojaDeRuta(int idHojaDeRuta)
        {
            var envios = envioBLL.ObtenerPorHojaDeRuta(idHojaDeRuta);
            var detalles = envios.SelectMany(x => x.Detalle);
            var detalleAgrupado = detalles
                .GroupBy(x => x.Articulo.Id)
                .Select(g =>
                new
                {
                    IdArticulo = g.Key,
                    Cantidad = detalles.Where(d => d.Articulo.Id == g.Key).Sum(x => x.Cantidad)
                }
            );

            return detalleAgrupado.Select(x => 
                new RecepcionDetalle() 
                {
                    Articulo = articuloBLL.Obtener(x.IdArticulo),
                    CantidadARecibir = x.Cantidad
                }).ToList();
        }

        public List<RecepcionVista> ObtenerTodosParaVista(List<int> ubicacionesPorDefecto,
            int nro = 0, int idHojaDeRuta = 0,
            DateTime? fechaDesde = null, DateTime? fechaHasta = null, int idUbicacionOrigen = 0, 
            int idUbicacionDestino = 0, int tipo = 0)
        {
            var lista = this.ObtenerTodos(ubicacionesPorDefecto, nro, idHojaDeRuta, fechaDesde, fechaHasta, 
                idUbicacionOrigen, idUbicacionDestino, tipo);
            return lista.Select(x => ConvertirAVista(x)).ToList();
        }


        public RecepcionVista ConvertirAVista(Recepcion obj)
        {
            return new RecepcionVista()
            {
                Id = obj.Id,
                Numero = obj.Id.ToString(),
                FechaDeCreacion = obj.FechaCreacion.ToString(),
                HojaDeRuta = obj.HojaDeRuta.Id.ToString(),
                Usuario = obj.Usuario.NombreDeUsuario,
                UbicacionOrigen = obj.UbicacionOrigen.Descripcion,
                UbicacionDestino = obj.UbicacionDestino.Descripcion
            };
        }
        public List<RecepcionDetalleVista> ConvertirDetalleAVista(List<RecepcionDetalle> detalle)
        {
            var lista = new List<RecepcionDetalleVista>();

            foreach(var x in detalle)
            {
                var vista = new RecepcionDetalleVista()
                {
                    IdArticulo = x.Articulo.Id,
                    Articulo = x.Articulo.Codigo,
                    CantidadARecibir = x.CantidadARecibir,
                    TipoDePrenda = x.Articulo.TipoDePrenda.Descripcion,
                    Talle = x.Articulo.Talle.Descripcion,
                    Color = x.Articulo.Color.Descripcion,
                    Cantidad = x.Cantidad
                };
                lista.Add(vista);
            }

            return lista;
        }
        public List<RecepcionDetalle> ConvertirVistaADetalle(List<RecepcionDetalleVista> vista)
        {
            return vista.Select(x => 
                new RecepcionDetalle() 
                {
                    Cantidad = x.Cantidad,
                    Articulo = articuloBLL.Obtener(x.IdArticulo),
                    CantidadARecibir = x.CantidadARecibir
                }).ToList();
        }
        private bool ValidarCapacidadDestino(Ubicacion ubicacionDestino, decimal pesoTotal)
        {
            return (ubicacionDestino.CapacidadDisponible - pesoTotal) > 0;
        }
    }
}
