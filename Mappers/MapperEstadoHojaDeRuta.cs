using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperEstadoHojaDeRuta
    {
        AccesoArchivo dal = new AccesoArchivo();

        public void Alta(EstadoHojaDeRuta obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("EstadoHojaDeRuta");
                DataRow dr = dt.NewRow();

                this.ConvertirEntidadEnDataRow(obj, dr);

                dt.Rows.Add(dr);
                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public EstadoHojaDeRuta Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("EstadoHojaDeRuta");
                var estadoHojaDeRuta = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(estadoHojaDeRuta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoHojaDeRuta Obtener(string descripcion)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("EstadoHojaDeRuta");
                var estadoHojaDeRuta = dt.Select("Descripcion = '" + descripcion + "'").FirstOrDefault();
                return this.ConvertirDataRowAEntidad(estadoHojaDeRuta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoHojaDeRuta> ObtenerTodos()
        {
            try
            {
                var lista = new List<EstadoHojaDeRuta>();
                DataTable dt = dal.ObtenerTabla("EstadoHojaDeRuta");
                foreach(DataRow row in dt.Rows)
                {
                    lista.Add(ConvertirDataRowAEntidad(row));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private EstadoHojaDeRuta ConvertirDataRowAEntidad(DataRow row)
        {
            var obj = new EstadoHojaDeRuta()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString()
            };
            return obj;
        }

        private void ConvertirEntidadEnDataRow(EstadoHojaDeRuta obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
        }
    }
}
