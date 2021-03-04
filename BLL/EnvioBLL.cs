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
        ArticuloBLL articuloBLL = new ArticuloBLL();
        ParametroDelSistemaBLL parametroDelSistemaBLL = new ParametroDelSistemaBLL();
        BultoCompuestoBLL bultoCompuestoBLL = new BultoCompuestoBLL();

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
        public void Modificar(Envio envio)
        {
            try
            {
                mpp.Modificacion(envio);
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

        #region Obtencion
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
                        && x.UbicacionDestino.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Lavanderia);
                        break;

                    case (int)TipoDeEnvio.AClinica:
                        lista = lista.Where(x => !x.UbicacionDestino.EsUbicacionInterna
                        && !x.UbicacionOrigen.EsUbicacionInterna
                        && x.UbicacionDestino.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Clinica);
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
                    (envio.UbicacionOrigen.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Lavanderia &&
                    envio.UbicacionDestino.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Clinica) 
                    ? $"${this.ObtenerFacturacionTotal(envio).ToString()}" : "No aplica",
                PesoTotal = envio.PesoTotal.ToString() + " Kg"
            };
        }
        #endregion

        public decimal ObtenerFacturacionTotal(Envio envio)
        {
            decimal precio = 0;
            
            if (envio.UbicacionOrigen.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Lavanderia &&
                envio.UbicacionDestino.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Clinica)
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

        public bool ValidarCapacidadDestino(Ubicacion ubicacionDestino, decimal pesoTotal)
        {
            return (ubicacionDestino.CapacidadDisponible - pesoTotal) > 0;
        }

        #region Division
        public List<Envio> DividirPorCapacidadMaxima(Envio envio)
        {
            //Se divide el envio en dos: el primero no supera la capacidad maxima, y el otro si
            decimal capacidadMax = decimal.Parse(parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.CapacidadMaximaHojaDeRuta).Valor);
            int contadorEnvios = 0;

            var listaEnvios = new List<Envio>() { envio };
            var detallesParaNuevoEnvio = new List<Tuple<int, Articulo, int>>();  //Id, Articulo, Corte por Bulto

            var detallePorBulto = articuloBLL.SepararDetallePorBulto(envio.Detalle);

            foreach (var item in detallePorBulto)
            {
                decimal pesoEnvioAcumulado = detallesParaNuevoEnvio.Sum(d => d.Item3 * d.Item2.PesoUnitario);
                decimal pesoBultoActual = item.Item3 * item.Item2.PesoUnitario;

                if (envio.UbicacionOrigen.TipoDeUbicacion.Id == (int)Entidades.Enums.TipoDeUbicacion.Lavanderia 
                    && item.Item2.TipoDePrenda.Categoria.EsCompuesta)
                    this.ValidarEspacioParaBultoCompuesto(item, detallePorBulto, ref pesoBultoActual);
                

                if ((pesoEnvioAcumulado + pesoBultoActual) > capacidadMax)
                {
                    if (contadorEnvios == 0)
                    {
                        //Se elimina el sobrante del envío original
                        var detalleSobranteTupla = detallePorBulto.Where(x => detallesParaNuevoEnvio.All(d => d.Item1 != x.Item1)).ToList();
                        var detalleSobranteEntidad = this.ConvertirTuplaAEntidad(detalleSobranteTupla);
                        this.EliminarDetalleSobrante(envio, detalleSobranteEntidad);
                    }
                    else
                        listaEnvios.Add(this.CrearEnvioConDetalle(envio, this.ConvertirTuplaAEntidad(detallesParaNuevoEnvio)));
                    
                    detallesParaNuevoEnvio.Clear();
                    contadorEnvios++;
                }
                
                detallesParaNuevoEnvio.Add(item);
            }

            //Si sobraron detalles, se agregan en un nuevo envio
            if (detallesParaNuevoEnvio.Count != 0)
                listaEnvios.Add(this.CrearEnvioConDetalle(envio, this.ConvertirTuplaAEntidad(detallesParaNuevoEnvio)));

            return listaEnvios;
        }

        
        private Envio CrearEnvioConDetalle(Envio envio, List<EnvioDetalle> detalleSobrante)
        {
            var nuevoEnvio = new Envio()
            {
                FechaCreacion = DateTime.Now,
                UbicacionOrigen = envio.UbicacionOrigen,
                UbicacionDestino = envio.UbicacionDestino,
                Usuario = SeguridadBLL.usuarioLogueado
            };
            nuevoEnvio.Detalle = detalleSobrante;

            return nuevoEnvio;
        }

        private void EliminarDetalleSobrante(Envio envio, List<EnvioDetalle> detalleSobrante)
        {
            foreach(var detalle in detalleSobrante)
            {
                //Se resta la cantidad sobrante al detalle del envio original
                envio.Detalle.Find(x => x.Articulo.Id == detalle.Articulo.Id).Cantidad -= detalle.Cantidad;
                
            }
          
        }

        private List<EnvioDetalle> ConvertirTuplaAEntidad(List<Tuple<int, Articulo, int>> detalleTupla)
        {
            return detalleTupla
                        .GroupBy(x => x.Item2)
                        .Select(x => new EnvioDetalle()
                        {
                            Articulo = x.Key,
                            Cantidad = detalleTupla.Where(d => d.Item2 == x.Key).Sum(d => d.Item3),
                            PrecioUnitario = x.Key.PrecioUnitario
                        }).ToList();
        }

        private void ValidarEspacioParaBultoCompuesto(Tuple<int, Articulo, int> itemCompuesto, List<Tuple<int, Articulo, int>> detallePorBulto, ref decimal pesoBultoActual)
        {
            var bultoCompuesto = bultoCompuestoBLL.ObtenerPorTipoDePrenda(itemCompuesto.Item2.TipoDePrenda.Id);
            //Si es el primer id del detalle, quiere decir que se está agregando un bulto nuevo.
            //En ese caso, validar si hay espacio para un bulto compuesto completo
            //Si no es la primer id del detalle, agregar la prenda directo, porque se supone que ya se valido el espacio
            if (itemCompuesto.Item2.TipoDePrenda.Id == bultoCompuesto.Detalle.First().TipoDePrenda.Id)
            {
                var detallesProximos = detallePorBulto.Where(x => x.Item1 >= itemCompuesto.Item1).Take(bultoCompuesto.Detalle.Count).ToList();

                pesoBultoActual = bultoCompuesto.Detalle.Sum(c => c.TipoDePrenda.CortePorBulto * detallesProximos.First(x => x.Item2.TipoDePrenda.Id == c.TipoDePrenda.Id).Item2.PesoUnitario);
            }
        }

        #endregion
    }
}
