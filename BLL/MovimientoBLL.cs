using BLL.Vistas;
using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL
{
    public class MovimientoBLL
    {
        HojaDeRutaBLL hojaDeRutaBLL = new HojaDeRutaBLL();
        EnvioBLL envioBLL = new EnvioBLL();
        RecepcionBLL recepcionBLL = new RecepcionBLL();
        AuditoriaBLL auditoriaBLL = new AuditoriaBLL();

        public List<Movimiento> ObtenerMovimientos()
        {
            var hojasDeRuta = hojaDeRutaBLL.ObtenerTodos();
            var envios = envioBLL.ObtenerTodos();
            var recepciones = recepcionBLL.ObtenerTodos();
            var auditorias = auditoriaBLL.ObtenerTodos();
            
            
            var hojasDeRutaMovimientos = hojasDeRuta.Select(x =>
            new Movimiento()
            {
                Descripcion = "Hoja de Ruta",
                FechaCreacion = x.FechaCreacion,
                Numero = x.Id.ToString(),
                Usuario = x.Usuario,
                UbicacionDestino = x.UbicacionDestino,
                UbicacionOrigen = x.UbicacionOrigen
            });

            var enviosMovimientos = envios.Select(x =>
            new Movimiento()
            {
                Descripcion = "Envio",
                FechaCreacion = x.FechaCreacion,
                Numero = x.Id.ToString(),
                Usuario = x.Usuario,
                UbicacionDestino = x.UbicacionDestino,
                UbicacionOrigen = x.UbicacionOrigen
            });

            var recepcionesMovimientos = recepciones.Select(x =>
            new Movimiento()
            {
                Descripcion = "Recepcion",
                FechaCreacion = x.FechaCreacion,
                Numero = x.Id.ToString(),
                Usuario = x.Usuario,
                UbicacionDestino = x.UbicacionDestino,
                UbicacionOrigen = x.UbicacionOrigen
            });

            var auditoriasMovimientos = auditorias.Select(x =>
            new Movimiento()
            {
                Descripcion = "Auditoria",
                FechaCreacion = x.FechaCreacion,
                Numero = x.Id.ToString(),
                Usuario = x.Usuario,
                UbicacionDestino = x.Ubicacion,
                UbicacionOrigen = x.Ubicacion
            });

            var movimientos = new List<Movimiento>();
            movimientos.AddRange(enviosMovimientos);
            movimientos.AddRange(recepcionesMovimientos);
            movimientos.AddRange(hojasDeRutaMovimientos);
            movimientos.AddRange(auditoriasMovimientos);

            return movimientos.OrderBy(x => x.FechaCreacion).ToList();
        }

        public List<Movimiento> ObtenerMovimientos(DateTime? fechaDesde, DateTime? fechaHasta, int idUbicacionOrigen,
            int idUbicacionDestino)
        {
            try
            {
                var lista = ObtenerMovimientos().AsEnumerable();

                if (idUbicacionOrigen > 0)
                    lista = lista.Where(x => x.UbicacionOrigen.Id == idUbicacionOrigen);

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


        public List<MovimientoVista> ObtenerMovimientosParaVista(DateTime? fechaDesde = null, DateTime? fechaHasta = null, int idUbicacionOrigen = 0,
            int idUbicacionDestino = 0)
        {
            return this.ObtenerMovimientos(fechaDesde, fechaHasta, idUbicacionOrigen, idUbicacionDestino)
                .Select(x => this.ConvertirEntidadAVista(x)).ToList();
        }

        private MovimientoVista ConvertirEntidadAVista(Movimiento obj)
        {
            return new MovimientoVista()
            {
                Descripcion = obj.Descripcion,
                Fecha = obj.FechaCreacion.ToString(),
                Numero = obj.Numero,
                Usuario = obj.Usuario.NombreDeUsuario,
                UbicacionDestino = obj.UbicacionDestino.Descripcion,
                UbicacionOrigen = obj.UbicacionOrigen.Descripcion
            };
        }
    }
}
