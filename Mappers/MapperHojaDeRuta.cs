using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperHojaDeRuta : IProcesable<HojaDeRuta>
    {
        AccesoArchivo dal = new AccesoArchivo();

        
        MapperUsuario mppUsuario = new MapperUsuario();
        MapperEstadoHojaDeRuta mppEstadoHojaDeRuta = new MapperEstadoHojaDeRuta();

        public void Alta(HojaDeRuta obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("HojaDeRuta");
                DataRow dr = dt.NewRow();

                this.ConvertirEntidadEnDataRow(obj, dr);

                dt.Rows.Add(dr);
                dal.ActualizarDataSet();

                obj.Id = int.Parse(dr["Id"].ToString());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Modificacion(HojaDeRuta obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("HojaDeRuta");
                var hojaDeRuta = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, hojaDeRuta);

                dal.ActualizarDataSet();
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
                var lista = new List<HojaDeRuta>();
                DataTable dt = dal.ObtenerTabla("HojaDeRuta");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
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
                var lista = new List<HojaDeRuta>();
                DataTable dt = dal.ObtenerTabla("HojaDeRuta");
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

        private HojaDeRuta ConvertirDataRowAEntidad(DataRow row)
        {
            var hojaDeRuta = new HojaDeRuta()
            {
                Id = int.Parse(row["Id"].ToString()),
                FechaCreacion = Convert.ToDateTime(row["FechaCreacion"].ToString()),
                Estado = mppEstadoHojaDeRuta.Obtener(int.Parse(row["IdEstado"].ToString())),
                Usuario = mppUsuario.Obtener(int.Parse(row["IdUsuario"].ToString())),
                
            };
            return hojaDeRuta;
        }

        private void ConvertirEntidadEnDataRow(HojaDeRuta obj, DataRow dr)
        {
            dr["IdUsuario"] = obj.Usuario.Id;
            dr["IdEstado"] = obj.Estado.Id;
            dr["FechaCreacion"] = obj.FechaCreacion;
        }
    }
}
