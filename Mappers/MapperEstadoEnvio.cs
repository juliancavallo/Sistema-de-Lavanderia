using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperEstadoEnvio
    {
        AccesoArchivo dal = new AccesoArchivo();

        #region IAdministrable
        public void Alta(EstadoEnvio obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("EstadoEnvio");
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


        public EstadoEnvio Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("EstadoEnvio");
                var estadoEnvio = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(estadoEnvio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EstadoEnvio Obtener(string descripcion)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("EstadoEnvio");
                var estadoEnvio = dt.Select("Descripcion = '" + descripcion + "'").FirstOrDefault();
                return this.ConvertirDataRowAEntidad(estadoEnvio);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EstadoEnvio> ObtenerTodos()
        {
            try
            {
                var lista = new List<EstadoEnvio>();
                DataTable dt = dal.ObtenerTabla("EstadoEnvio");
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
        #endregion

        private EstadoEnvio ConvertirDataRowAEntidad(DataRow row)
        {
            var obj = new EstadoEnvio()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString()
            };
            return obj;
        }

        private void ConvertirEntidadEnDataRow(EstadoEnvio obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
        }
    }
}
