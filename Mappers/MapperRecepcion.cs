using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperRecepcion : IProcesable<Recepcion>
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperRecepcionDetalle mppRecepcionDetalle = new MapperRecepcionDetalle();
        MapperUbicacion mppUbicacion = new MapperUbicacion();
        MapperUsuario mppUsuario = new MapperUsuario();
        MapperHojaDeRuta mppHojaDeRuta = new MapperHojaDeRuta();

        public void Alta(Recepcion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Recepcion");
                DataRow dr = dt.NewRow();

                this.ConvertirEntidadEnDataRow(obj, dr);

                dt.Rows.Add(dr);
                dal.ActualizarDataSet();

                obj.Id = int.Parse(dr["Id"].ToString());

                obj.Detalle.ForEach(x => x.Recepcion = obj);
                mppRecepcionDetalle.Alta(obj.Detalle);
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
                var lista = new List<Color>();
                DataTable dt = dal.ObtenerTabla("Recepcion");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
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
                var lista = new List<Recepcion>();
                DataTable dt = dal.ObtenerTabla("Recepcion");
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

        public List<Recepcion> ObtenerPorHojaDeRuta(int idHojaDeRuta)
        {
            try
            {
                var lista = new List<Recepcion>();
                DataTable dt = dal.ObtenerTabla("Recepcion");
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

        private Recepcion ConvertirDataRowAEntidad(DataRow row)
        {
            var envio = new Recepcion()
            {
                Id = int.Parse(row["Id"].ToString()),
                FechaCreacion = Convert.ToDateTime(row["FechaCreacion"].ToString()),
                HojaDeRuta = mppHojaDeRuta.Obtener(int.Parse(row["IdHojaDeRuta"].ToString())),
                Detalle = mppRecepcionDetalle.ObtenerTodos(int.Parse(row["Id"].ToString())),
                Usuario = mppUsuario.Obtener(int.Parse(row["IdUsuario"].ToString())),
                UbicacionOrigen = mppUbicacion.Obtener(int.Parse(row["IdUbicacionOrigen"].ToString())),
                UbicacionDestino = mppUbicacion.Obtener(int.Parse(row["IdUbicacionDestino"].ToString()))
            };
            return envio;
        }

        private void ConvertirEntidadEnDataRow(Recepcion obj, DataRow dr)
        {
            dr["IdUbicacionOrigen"] = obj.UbicacionOrigen.Id;
            dr["IdUbicacionDestino"] = obj.UbicacionDestino.Id;
            dr["IdUsuario"] = obj.Usuario.Id;
            dr["IdHojaDeRuta"] = obj.HojaDeRuta.Id;
            dr["FechaCreacion"] = obj.FechaCreacion;
        }
    }
}
