using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperTalle : IAdministrable<Talle>
    {
        AccesoArchivo dal = new AccesoArchivo();

        #region IAdministrable
        public void Alta(Talle obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Talle");
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

        public void Baja(Talle obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Talle");
                var talle = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(talle);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Talle obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Talle");
                var talle = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, talle);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Talle Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Talle");
                var talle = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(talle);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Talle> ObtenerTodos()
        {
            try
            {
                var lista = new List<Talle>();
                DataTable dt = dal.ObtenerTabla("Talle");
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

        private Talle ConvertirDataRowAEntidad(DataRow row)
        {
            var talle = new Talle()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString()
            };
            return talle;
        }

        private void ConvertirEntidadEnDataRow(Talle obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
        }
    }
}
