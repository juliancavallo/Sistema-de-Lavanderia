using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperTipoDePrenda : IAdministrable<TipoDePrenda>
    {
        AccesoArchivo dal = new AccesoArchivo();
        MapperCategoria mapperCategoria = new MapperCategoria();

        #region IAdministrable
        public void Alta(TipoDePrenda obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDePrenda");
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

        public void Baja(TipoDePrenda obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDePrenda");
                var tipoDePrenda = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(tipoDePrenda);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(TipoDePrenda obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDePrenda");
                var tipoDePrenda = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, tipoDePrenda);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TipoDePrenda Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("TipoDePrenda");
                var tipoDePrenda = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(tipoDePrenda);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TipoDePrenda> ObtenerTodos()
        {
            try
            {
                var lista = new List<TipoDePrenda>();
                DataTable dt = dal.ObtenerTabla("TipoDePrenda");
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

        private TipoDePrenda ConvertirDataRowAEntidad(DataRow row)
        {
            var tipoDePRenda = new TipoDePrenda()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString(),
                CortePorBulto = int.Parse(row["CortePorBulto"].ToString()),
                UsaCortePorBulto = bool.Parse(row["UsaCortePorBulto"].ToString()),
                Categoria = mapperCategoria.Obtener(int.Parse(row["IdCategoria"].ToString()))
            };
            return tipoDePRenda;
        }

        private void ConvertirEntidadEnDataRow(TipoDePrenda obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
            dr["CortePorBulto"] = obj.CortePorBulto;
            dr["UsaCortePorBulto"] = obj.UsaCortePorBulto;
            dr["IdCategoria"] = obj.Categoria.Id;
        }
    }
}
