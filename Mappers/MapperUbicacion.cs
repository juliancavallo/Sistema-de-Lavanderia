using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperUbicacion : IAdministrable<Ubicacion>
    {
        AccesoArchivo dal = new AccesoArchivo();

        #region IAdministrable
        public void Alta(Ubicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Ubicacion");
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

        public void Baja(Ubicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Ubicacion");
                var ubicacion = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(ubicacion);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Ubicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Ubicacion");
                var ubicacion = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, ubicacion);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ubicacion Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Ubicacion");
                var ubicacion = dt.Select("Id = " + id).FirstOrDefault();
                if (ubicacion != null)
                    return this.ConvertirDataRowAEntidad(ubicacion);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ubicacion> ObtenerTodos()
        {
            try
            {
                var lista = new List<Ubicacion>();
                DataTable dt = dal.ObtenerTabla("Ubicacion");
                foreach(DataRow row in dt.Rows)
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
        #endregion

        private Ubicacion ConvertirDataRowAEntidad(DataRow row)
        {
            string idUbicacionPadreString = row["IdUbicacionPadre"].ToString();
            int? idUbicacionPadre = int.TryParse(idUbicacionPadreString, out var temp) ? temp : (int?)null;

            var ubicacion = new Ubicacion()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString(),
                Direccion = row["Direccion"].ToString(),
                TipoDeUbicacion = int.Parse(row["TipoDeUbicacion"].ToString()),
                UbicacionPadre = idUbicacionPadre.HasValue ? this.Obtener(idUbicacionPadre.Value) : null
            };
            return ubicacion;
        }

        public void ConvertirEntidadEnDataRow(Ubicacion obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
            dr["Direccion"] = obj.Direccion;
            dr["TipoDeUbicacion"] = obj.TipoDeUbicacion;
            dr["IdUbicacionPadre"] = (object)obj.UbicacionPadre?.Id ?? DBNull.Value;
        }
    }
}
