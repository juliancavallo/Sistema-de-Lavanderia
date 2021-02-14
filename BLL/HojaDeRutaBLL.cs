using BLL.Vistas;
using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class HojaDeRutaBLL : IProcesable<HojaDeRuta>
    {
        MapperHojaDeRuta mpp = new MapperHojaDeRuta();
        StockBLL stockBLL = new StockBLL();
        
        EstadoHojaDeRutaBLL estadoHojaDeRutaBLL = new EstadoHojaDeRutaBLL();
        EnvioBLL envioBLL = new EnvioBLL();

        public void Alta(HojaDeRuta obj)
        {
            try
            {
                stockBLL.Enviar(obj.Envios);

                obj.Estado = estadoHojaDeRutaBLL.Obtener(Entidades.Enums.EstadoHojaDeRuta.Generada);
                mpp.Alta(obj);

                foreach (var envio in obj.Envios)
                {
                    envioBLL.Enviar(envio, obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Recibir(HojaDeRuta obj)
        {
            try
            {
                obj.Estado = estadoHojaDeRutaBLL.Obtener(Entidades.Enums.EstadoHojaDeRuta.Recibida);
                mpp.Modificacion(obj);

                obj.Envios.ForEach(x => envioBLL.Recibir(x));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public HojaDeRuta Obtener(int id)
        {
            try
            {
                var hoja = mpp.Obtener(id);
                hoja.Envios = envioBLL.ObtenerPorHojaDeRuta(hoja.Id);
                return hoja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HojaDeRuta> ObtenerTodos()
        {
            try
            {
                var hojas = mpp.ObtenerTodos();
                foreach (var hoja in hojas)
                {
                    hoja.Envios = envioBLL.ObtenerPorHojaDeRuta(hoja.Id);
                }

                return hojas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HojaDeRuta> ObtenerTodos(List<int> ubicacionesPorDefecto, 
            int idUbicacionOrigen, int idUbicacionDestino,
            DateTime? fechaDesde, DateTime? fechaHasta, int numero)
        {
            try
            {
                var lista = ObtenerTodos().AsEnumerable();

                if (numero > 0)
                    lista = lista.Where(x => x.Id == numero);

                if (idUbicacionOrigen > 0)
                    lista = lista.Where(x => x.UbicacionOrigen.Id == idUbicacionOrigen);
                else
                    lista = lista.Where(x => ubicacionesPorDefecto.Contains(x.UbicacionOrigen.Id));

                if (idUbicacionDestino > 0)
                    lista = lista.Where(x => x.UbicacionDestino.Id == idUbicacionDestino);

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

        public List<HojaDeRutaVista> ObtenerTodosParaVista(List<int> ubicacionesPorDefecto,
            int idUbicacionOrigen = 0, int idUbicacionDestino = 0,
            DateTime? fechaDesde = null, DateTime? fechaHasta = null, int numero = 0)
        {
            var envios = this.ObtenerTodos(ubicacionesPorDefecto, idUbicacionOrigen, idUbicacionDestino, fechaDesde, fechaHasta, numero);
            return this.ConvertirAVista(envios);
        }

        public List<HojaDeRuta> ObtenerTodos(int idUbicacionDestino)
        {
            try
            {
                //Se asume que todas las hojas de ruta tienen envios con el mismo origen y destino
                var hojasDeRuta = mpp.ObtenerTodos();

                foreach(var hoja in hojasDeRuta)
                {
                    hoja.Envios = envioBLL.ObtenerPorHojaDeRuta(hoja.Id);
                }

                return hojasDeRuta.Where(x => x.Envios.First().UbicacionDestino.Id == idUbicacionDestino).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HojaDeRuta> ObtenerTodos(List<int> idsUbicacionDestino)
        {
            try
            {
                //Se asume que todas las hojas de ruta tienen envios con el mismo origen y destino
                var hojasDeRuta = mpp.ObtenerTodos();

                foreach (var hoja in hojasDeRuta)
                {
                    hoja.Envios = envioBLL.ObtenerPorHojaDeRuta(hoja.Id);
                }

                return hojasDeRuta.Where(x => idsUbicacionDestino.Contains(x.Envios.First().UbicacionDestino.Id)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HojaDeRutaVista> ConvertirAVista(List<HojaDeRuta> lista)
        {
            return lista.Select(x => ConvertirAVista(x)).ToList();
        }

        public HojaDeRutaVista ConvertirAVista(HojaDeRuta obj)
        {
            return new HojaDeRutaVista()
            {
                Id = obj.Id,
                Numero = obj.Id.ToString(),
                Envios = string.Join(", ", obj.Envios.Select(x => x.Id)),
                FechaDeCreacion = obj.FechaCreacion.ToString(),
                Usuario = obj.Usuario.NombreDeUsuario,
                Estado = obj.Estado.Descripcion,
                UbicacionOrigen = obj.UbicacionOrigen.Descripcion,
                UbicacionDestino = obj.UbicacionDestino.Descripcion
            };
        }

        public bool ValidarCapacidadDestino(Ubicacion ubicacionDestino, List<Envio> envios)
        { 
            decimal pesoTotalEnEnvios = envios.SelectMany(x => x.Detalle).Sum(x => x.Articulo.PesoUnitario * x.Cantidad);

            return (ubicacionDestino.CapacidadDisponible - pesoTotalEnEnvios) > 0;
        }
    }
}
