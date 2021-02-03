using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperParametroDelSistema : IAdministrable<ParametroDelSistema>
    {
        AccesoArchivo dal = new AccesoArchivo();

        #region IAdministrable
        public void Alta(ParametroDelSistema obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("ParametroDelSistema");
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

        public void Baja(ParametroDelSistema obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("ParametroDelSistema");
                var parametroDelSistema = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(parametroDelSistema);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(ParametroDelSistema obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("ParametroDelSistema");
                var parametroDelSistema = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, parametroDelSistema);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ParametroDelSistema Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("ParametroDelSistema");
                var parametroDelSistema = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(parametroDelSistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ParametroDelSistema Obtener(string nombre)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("ParametroDelSistema");
                var parametroDelSistema = dt.Select("Nombre = '" + nombre + "'").FirstOrDefault();
                return this.ConvertirDataRowAEntidad(parametroDelSistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ParametroDelSistema> ObtenerTodos()
        {
            try
            {
                var lista = new List<ParametroDelSistema>();
                DataTable dt = dal.ObtenerTabla("ParametroDelSistema");
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

        private ParametroDelSistema ConvertirDataRowAEntidad(DataRow row)
        {
            var param = new ParametroDelSistema()
            {
                Id = int.Parse(row["Id"].ToString()),
                Nombre = row["Nombre"].ToString(),
                Valor = row["Valor"].ToString()
            };
            return param;
        }

        private void ConvertirEntidadEnDataRow(ParametroDelSistema obj, DataRow dr)
        {
            dr["Valor"] = obj.Valor;
            dr["Nombre"] = obj.Nombre;
        }
    }
}
