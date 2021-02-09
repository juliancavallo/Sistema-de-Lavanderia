using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperEnvioDetalle : IProcesable<EnvioDetalle>
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperArticulo mppArticulo = new MapperArticulo();
        
        public void Alta(EnvioDetalle obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("EnvioDetalle");
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

        public void Alta(List<EnvioDetalle> obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("EnvioDetalle");
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

        public EnvioDetalle Obtener(int id)
        {
            try
            {
                var lista = new List<Color>();
                DataTable dt = dal.ObtenerTabla("EnvioDetalle");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EnvioDetalle> ObtenerTodos()
        {
            try
            {
                var lista = new List<EnvioDetalle>();
                DataTable dt = dal.ObtenerTabla("EnvioDetalle");
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

        public List<EnvioDetalle> ObtenerTodos(int idEnvio)
        {
            try
            {
                var lista = new List<EnvioDetalle>();
                DataTable dt = dal.ObtenerTabla("EnvioDetalle");
                foreach (DataRow row in dt.Select("IdEnvio = " + idEnvio))
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

        private EnvioDetalle ConvertirDataRowAEntidad(DataRow row)
        {
            var detalle = new EnvioDetalle()
            {
                Cantidad = int.Parse(row["Cantidad"].ToString()),
                Articulo = mppArticulo.Obtener(int.Parse(row["IdArticulo"].ToString())),
                PrecioUnitario = decimal.Parse(row["PrecioUnitario"].ToString())
            };
            return detalle;
        }

        private void ConvertirEntidadEnDataRow(EnvioDetalle obj, DataRow dr)
        {
            dr["IdEnvio"] = obj.Envio.Id;
            dr["IdArticulo"] = obj.Articulo.Id;
            dr["Cantidad"] = obj.Cantidad;
            dr["PrecioUnitario"] = obj.Articulo.PrecioUnitario;
        }
    }
}
