using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperTipoDeUbicacion : IAdministrable<TipoDeUbicacion>
    {
        AccesoArchivo dal = new AccesoArchivo();

        #region IAdministrable
        public void Alta(TipoDeUbicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDeUbicacion");
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

        public void Baja(TipoDeUbicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDeUbicacion");
                var tipoDeUbicacion = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(tipoDeUbicacion);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(TipoDeUbicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDeUbicacion");
                var tipoDeUbicacion = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, tipoDeUbicacion);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoDeUbicacion Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDeUbicacion");
                var tipoDeUbicacion = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(tipoDeUbicacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoDeUbicacion Obtener(string descripcion)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDeUbicacion");
                var tipoDeUbicacion = dt.Select("Descripcion = '" + descripcion + "'").FirstOrDefault();
                return this.ConvertirDataRowAEntidad(tipoDeUbicacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoDeUbicacion> ObtenerTodos()
        {
            try
            {
                var lista = new List<TipoDeUbicacion>();
                DataTable dt = dal.ObtenerTabla("TipoDeUbicacion");
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

        private TipoDeUbicacion ConvertirDataRowAEntidad(DataRow row)
        {
            var param = new TipoDeUbicacion()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString()
            };
            return param;
        }

        private void ConvertirEntidadEnDataRow(TipoDeUbicacion obj, DataRow dr)
        {
            dr["Id"] = obj.Id;
            dr["Descripcion"] = obj.Descripcion;
        }
    }
}
