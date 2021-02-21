using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperEnvio : IProcesable<Envio>
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperEnvioDetalle mppEnvioDetalle = new MapperEnvioDetalle();
        MapperUbicacion mppUbicacion = new MapperUbicacion();
        MapperUsuario mppUsuario = new MapperUsuario();
        MapperEstadoEnvio mppEstadoEnvio = new MapperEstadoEnvio();
        MapperHojaDeRuta mppHojaDeRuta = new MapperHojaDeRuta();

        public void Alta(Envio obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Envio");
                DataRow dr = dt.NewRow();

                this.ConvertirEntidadEnDataRow(obj, dr);

                dt.Rows.Add(dr);
                dal.ActualizarDataSet();

                obj.Id = int.Parse(dr["Id"].ToString());
                
                obj.Detalle.ForEach(x => x.Envio = obj);
                mppEnvioDetalle.Alta(obj.Detalle);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Modificacion(Envio obj)
        {
            DataTable dt = dal.ObtenerTabla("Envio");
            var envio = dt.Select("Id = " + obj.Id).FirstOrDefault();

            this.ConvertirEntidadEnDataRow(obj, envio);

            obj.Detalle.ForEach(x => 
            {
                if (x.Cantidad > 0)
                    mppEnvioDetalle.Modificacion(x);
                else
                    mppEnvioDetalle.Baja(x);

            });
            

            dal.ActualizarDataSet();
        }

        public Envio Obtener(int id)
        {
            try
            {
                var lista = new List<Color>();
                DataTable dt = dal.ObtenerTabla("Envio");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
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
                var lista = new List<Envio>();
                DataTable dt = dal.ObtenerTabla("Envio");
                foreach (DataRow row in dt.Rows)
                {
                    lista.Add(this.ConvertirDataRowAEntidad(row));
                }
                return lista;
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
                var lista = new List<Envio>();
                DataTable dt = dal.ObtenerTabla("Envio");
                foreach (DataRow row in dt.Select("IdHojaDeRuta = " + idHojaDeRuta))
                {
                    lista.Add(this.ConvertirDataRowAEntidad(row));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Envio ConvertirDataRowAEntidad(DataRow row)
        {
            int? idHojaDeRuta = int.TryParse(row["IdHojaDeRuta"].ToString(), out var temp) ? temp : (int?)null;
            DateTime? fechaEnvio = string.IsNullOrEmpty(row["FechaEnvio"].ToString()) ? (DateTime?)null : Convert.ToDateTime(row["FechaEnvio"].ToString());
            DateTime? fechaRecepcion = string.IsNullOrEmpty(row["FechaRecepcion"].ToString()) ? (DateTime?)null : Convert.ToDateTime(row["FechaRecepcion"].ToString());

            var envio = new Envio()
            {
                Id = int.Parse(row["Id"].ToString()),
                FechaCreacion = Convert.ToDateTime(row["FechaCreacion"].ToString()),
                FechaEnvio = fechaEnvio,
                FechaRecepcion = fechaRecepcion,

                HojaDeRuta = idHojaDeRuta.HasValue ? mppHojaDeRuta.Obtener(idHojaDeRuta.Value) : null,
                Detalle = mppEnvioDetalle.ObtenerTodos(int.Parse(row["Id"].ToString())),
                Estado = mppEstadoEnvio.Obtener(int.Parse(row["IdEstado"].ToString())),
                Usuario = mppUsuario.Obtener(int.Parse(row["IdUsuario"].ToString())),
                UbicacionOrigen = mppUbicacion.Obtener(int.Parse(row["IdUbicacionOrigen"].ToString())),
                UbicacionDestino = mppUbicacion.Obtener(int.Parse(row["IdUbicacionDestino"].ToString()))
            };

            envio.Detalle.ForEach(x => x.Envio = envio);
            return envio;
        }

        private void ConvertirEntidadEnDataRow(Envio obj, DataRow dr)
        {
            dr["IdUbicacionOrigen"] = obj.UbicacionOrigen.Id;
            dr["IdUbicacionDestino"] = obj.UbicacionDestino.Id;
            dr["IdUsuario"] = obj.Usuario.Id;
            dr["IdEstado"] = obj.Estado.Id;
            dr["IdHojaDeRuta"] = (object)obj.HojaDeRuta?.Id ?? DBNull.Value;
            dr["FechaCreacion"] = obj.FechaCreacion;
            dr["FechaEnvio"] = (object)obj.FechaEnvio ?? DBNull.Value;
            dr["FechaRecepcion"] = (object)obj.FechaRecepcion ?? DBNull.Value;
        }
    }
}
