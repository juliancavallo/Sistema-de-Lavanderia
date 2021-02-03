using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperColor : IAdministrable<Color>
    {
        AccesoArchivo dal = new AccesoArchivo();

        #region IAdministrable
        public void Alta(Color obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Color");
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

        public void Baja(Color obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Color");
                var color = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(color);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Color obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Color");
                var color = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, color);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Color Obtener(int id)
        {
            try
            {
                var lista = new List<Color>();
                DataTable dt = dal.ObtenerTabla("Color");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Color> ObtenerTodos()
        {
            try
            {
                var lista = new List<Color>();
                DataTable dt = dal.ObtenerTabla("Color");
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

        private Color ConvertirDataRowAEntidad(DataRow row)
        {
            var color = new Color()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString()
            };
            return color;
        }

        private void ConvertirEntidadEnDataRow(Color obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
        }
    }
}
