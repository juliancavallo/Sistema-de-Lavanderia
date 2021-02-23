using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperAuditoriaDetalle : IProcesable<AuditoriaDetalle>
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperArticulo mppArticulo = new MapperArticulo();
        
        public void Alta(AuditoriaDetalle obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("AuditoriaDetalle");
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

        public void Alta(List<AuditoriaDetalle> obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("AuditoriaDetalle");
                foreach (var item in obj)
                {
                    DataRow dr = dt.NewRow();

                    this.ConvertirEntidadEnDataRow(item, dr);

                    dt.Rows.Add(dr);
                }
                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AuditoriaDetalle Obtener(int id)
        {
            try
            {
                var lista = new List<Color>();
                DataTable dt = dal.ObtenerTabla("AuditoriaDetalle");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AuditoriaDetalle> ObtenerTodos()
        {
            try
            {
                var lista = new List<AuditoriaDetalle>();
                DataTable dt = dal.ObtenerTabla("AuditoriaDetalle");
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

        public List<AuditoriaDetalle> ObtenerTodos(int idAuditoria)
        {
            try
            {
                var lista = new List<AuditoriaDetalle>();
                DataTable dt = dal.ObtenerTabla("AuditoriaDetalle");
                foreach (DataRow row in dt.Select("IdAuditoria = " + idAuditoria))
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

        private AuditoriaDetalle ConvertirDataRowAEntidad(DataRow row)
        {
            var color = new AuditoriaDetalle()
            {
                Cantidad = int.Parse(row["Cantidad"].ToString()),
                Articulo = mppArticulo.Obtener(int.Parse(row["IdArticulo"].ToString()))
            };
            return color;
        }

        private void ConvertirEntidadEnDataRow(AuditoriaDetalle obj, DataRow dr)
        {
            dr["IdArticulo"] = obj.Articulo.Id;
            dr["Cantidad"] = obj.Cantidad;
            dr["IdAuditoria"] = obj.Auditoria.Id;
        }
    }
}
