using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperAuditoria : IProcesable<Auditoria>
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperAuditoriaDetalle mppAuditoriaDetalle = new MapperAuditoriaDetalle();
        MapperUbicacion mppUbicacion = new MapperUbicacion();
        MapperUsuario mppUsuario = new MapperUsuario();

        public void Alta(Auditoria obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Auditoria");
                DataRow dr = dt.NewRow();

                this.ConvertirEntidadEnDataRow(obj, dr);

                dt.Rows.Add(dr);
                dal.ActualizarDataSet();

                obj.Id = int.Parse(dr["Id"].ToString());

                obj.Detalle.ForEach(x => x.Auditoria = obj);
                mppAuditoriaDetalle.Alta(obj.Detalle);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Auditoria Obtener(int id)
        {
            try
            {
                var lista = new List<Color>();
                DataTable dt = dal.ObtenerTabla("Auditoria");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Auditoria> ObtenerTodos()
        {
            try
            {
                var lista = new List<Auditoria>();
                DataTable dt = dal.ObtenerTabla("Auditoria");
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

        private Auditoria ConvertirDataRowAEntidad(DataRow row)
        {
            var color = new Auditoria()
            {
                Id = int.Parse(row["Id"].ToString()),
                FechaDeCreacion = Convert.ToDateTime(row["FechaDeCreacion"].ToString()),
                Usuario = mppUsuario.Obtener(int.Parse(row["IdUsuario"].ToString())),
                Ubicacion = mppUbicacion.Obtener(int.Parse(row["IdUbicacion"].ToString())),
                Detalle = mppAuditoriaDetalle.ObtenerTodos(int.Parse(row["Id"].ToString()))
            };
            return color;
        }

        private void ConvertirEntidadEnDataRow(Auditoria obj, DataRow dr)
        {
            dr["IdUbicacion"] = obj.Ubicacion.Id;
            dr["IdUsuario"] = obj.Usuario.Id;
            dr["FechaDeCreacion"] = obj.FechaDeCreacion;
        }
    }
}
