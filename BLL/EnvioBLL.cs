using BLL.Vistas;
using Entidades;
using Entidades.Enums;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class EnvioBLL : IProcesable<Envio>
    {
        MapperEnvio mpp = new MapperEnvio();
        EstadoEnvioBLL estadoEnvioBLL = new EstadoEnvioBLL();
        ParametroDelSistemaBLL parametroDelSistemaBLL = new ParametroDelSistemaBLL();

        public void Alta(Envio obj)
        {
            try
            {
                obj.Estado = estadoEnvioBLL.Obtener(Entidades.Enums.EstadoEnvio.Generado);
                mpp.Alta(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Enviar(Envio obj, HojaDeRuta hojaDeRuta)
        {
            try
            {
                var estado = estadoEnvioBLL.Obtener(Entidades.Enums.EstadoEnvio.Enviado);
                obj.Estado = estado;
                obj.FechaEnvio = DateTime.Now;
                
                if(hojaDeRuta != null)
                    obj.HojaDeRuta = hojaDeRuta;
                
                mpp.Modificacion(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Recibir(Envio obj)
        {
            try
            {
                var estado = estadoEnvioBLL.Obtener(Entidades.Enums.EstadoEnvio.Recibido);
                obj.Estado = estado;
                obj.FechaRecepcion = DateTime.Now;

                mpp.Modificacion(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Envio Obtener(int id)
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

        public List<Envio> ObtenerPorHojaDeRuta(int idHojaDeRuta)
        {
            try
            {
                return mpp.ObtenerPorHojaDeRuta(idHojaDeRuta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Envio> ObtenerTodos()
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
        public List<Envio> ObtenerTodos(List<int> ubicacionesPorDefecto, 
            int idUbicacionOrigen, int idUbicacionDestino, 
            DateTime? fechaDesde, DateTime? fechaHasta, int numero, int idEstado, int tipo)
        {
            try
            {
                var lista = ObtenerTodos().AsEnumerable();

                switch(tipo)
                {
                    case (int)TipoDeEnvio.ALavadero:
                        lista = lista.Where(x => !x.UbicacionDestino.EsUbicacionInterna 
                        && !x.UbicacionOrigen.EsUbicacionInterna
                        && x.UbicacionDestino.TipoDeUbicacion == (int)TipoDeUbicacion.Lavanderia);
                        break;

                    case (int)TipoDeEnvio.AClinica:
                        lista = lista.Where(x => !x.UbicacionDestino.EsUbicacionInterna
                        && !x.UbicacionOrigen.EsUbicacionInterna
                        && x.UbicacionDestino.TipoDeUbicacion == (int)TipoDeUbicacion.Clinica);
                        break;

                    case (int)TipoDeEnvio.Interno:
                        lista = lista.Where(x => x.UbicacionDestino.EsUbicacionInterna 
                        || x.UbicacionOrigen.EsUbicacionInterna);
                        break;
                }

                if (numero > 0)
                    lista = lista.Where(x => x.Id == numero);

                if (idUbicacionOrigen > 0)
                    lista = lista.Where(x => x.UbicacionOrigen.Id == idUbicacionOrigen);
                else
                    lista = lista.Where(x => ubicacionesPorDefecto.Contains(x.UbicacionOrigen.Id));

                if (idUbicacionDestino > 0)
                    lista = lista.Where(x => x.UbicacionDestino.Id == idUbicacionDestino);

                if (fechaDesde.HasValue)
                    lista = lista.Where(x => x.FechaCreacion> fechaDesde);

                if (fechaHasta.HasValue)
                    lista = lista.Where(x => x.FechaCreacion < fechaHasta);

                if (idEstado > 0)
                    lista = lista.Where(x => x.Estado.Id == idEstado);

                return lista.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EnvioVista> ObtenerTodosParaVista(List<int> ubicacionesPorDefecto,
            int idUbicacionOrigen = 0, int idUbicacionDestino = 0,
            DateTime? fechaDesde = null, DateTime? fechaHasta = null, int numero = 0, int idEstado = 0, int tipo = 0)
        {
            var envios = this.ObtenerTodos(ubicacionesPorDefecto,
                idUbicacionOrigen, idUbicacionDestino, fechaDesde, fechaHasta, numero, idEstado, tipo);
            return this.ConvertirAVista(envios);
        }

        public List<EnvioDetalleVista> ConvertirDetalleAVista(List<EnvioDetalle> detalle)
        {
            return detalle.Select(x =>
            new EnvioDetalleVista()
            {
                IdArticulo = x.Articulo.Id,
                Articulo = x.Articulo.Codigo,
                Cantidad = x.Cantidad.ToString(),
                TipoDePrenda = x.Articulo.TipoDePrenda.Descripcion,
                Color = x.Articulo.Color.Descripcion,
                Talle = x.Articulo.Talle.Descripcion
            }).ToList();
        }
        public List<EnvioVista> ConvertirAVista(List<Envio> envios)
        {
            return envios.Select(x => ConvertirAVista(x)).ToList();
        }
        public EnvioVista ConvertirAVista(Envio envio)
        {
            return new EnvioVista()
            {
                Id = envio.Id,
                Numero = envio.Id.ToString(),
                FechaDeCreacion = envio.FechaCreacion.ToString(),
                Usuario = envio.Usuario.NombreDeUsuario,
                UbicacionOrigen = envio.UbicacionOrigen.Descripcion,
                UbicacionDestino = envio.UbicacionDestino.Descripcion,
                Estado = envio.Estado.Descripcion,
                FacturacionTotal = 
                    (envio.UbicacionOrigen.TipoDeUbicacion == (int)TipoDeUbicacion.Lavanderia &&
                    envio.UbicacionDestino.TipoDeUbicacion == (int)TipoDeUbicacion.Clinica) 
                    ? $"${this.ObtenerFacturacionTotal(envio).ToString()}" : "No aplica"
            };
        }

        public decimal ObtenerFacturacionTotal(Envio envio)
        {
            decimal precio = 0;
            
            if (envio.UbicacionOrigen.TipoDeUbicacion == (int)TipoDeUbicacion.Lavanderia &&
                envio.UbicacionDestino.TipoDeUbicacion == (int)TipoDeUbicacion.Clinica)
            {
                envio.Detalle.ForEach(x => 
                {
                    precio += (x.PrecioUnitario * x.Cantidad);
                });

                if (!envio.UbicacionDestino.ClienteExterno)
                {
                    var descuento = decimal.Parse(parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.PorcentajeDescuentoDeEnvios).Valor);
                    precio -= precio * descuento / 100;
                }
            }

            return precio;
        }

    }
}
