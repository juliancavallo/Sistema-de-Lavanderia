using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperCategoria : IAdministrable<Categoria>
    {
        AccesoArchivo dal = new AccesoArchivo();

        #region IAdministrable
        public void Alta(Categoria obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Categoria");
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

        public void Baja(Categoria obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Categoria");
                var color = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(color);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Categoria obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Categoria");
                var color = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, color);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Categoria Obtener(int id)
        {
            try
            {
                var lista = new List<Categoria>();
                DataTable dt = dal.ObtenerTabla("Categoria");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Categoria> ObtenerTodos()
        {
            try
            {
                var lista = new List<Categoria>();
                DataTable dt = dal.ObtenerTabla("Categoria");
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

        private Categoria ConvertirDataRowAEntidad(DataRow row)
        {
            var color = new Categoria()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString(),
                EsCompuesta = bool.Parse(row["EsCompuesta"].ToString())
            };
            return color;
        }

        private void ConvertirEntidadEnDataRow(Categoria obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
            dr["EsCompuesta"] = obj.EsCompuesta;
        }
    }
}
