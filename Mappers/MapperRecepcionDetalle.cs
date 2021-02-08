using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperRecepcionDetalle : IProcesable<RecepcionDetalle>
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperArticulo mppArticulo = new MapperArticulo();
        
        public void Alta(RecepcionDetalle obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("RecepcionDetalle");
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

        public void Alta(List<RecepcionDetalle> obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("RecepcionDetalle");
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

        public RecepcionDetalle Obtener(int id)
        {
            try
            {
                var lista = new List<Color>();
                DataTable dt = dal.ObtenerTabla("RecepcionDetalle");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RecepcionDetalle> ObtenerTodos()
        {
            try
            {
                var lista = new List<RecepcionDetalle>();
                DataTable dt = dal.ObtenerTabla("RecepcionDetalle");
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

        public List<RecepcionDetalle> ObtenerTodos(int idRecepcion)
        {
            try
            {
                var lista = new List<RecepcionDetalle>();
                DataTable dt = dal.ObtenerTabla("RecepcionDetalle");
                foreach (DataRow row in dt.Select("IdRecepcion = " + idRecepcion))
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

        private RecepcionDetalle ConvertirDataRowAEntidad(DataRow row)
        {
            var color = new RecepcionDetalle()
            {
                CantidadARecibir = int.Parse(row["CantidadARecibir"].ToString()),
                CantidadRecibida = int.Parse(row["CantidadRecibida"].ToString()),
                PrecioUnitario = int.Parse(row["PrecioUnitario"].ToString()),
                Articulo = mppArticulo.Obtener(int.Parse(row["IdArticulo"].ToString()))
            };
            return color;
        }

        private void ConvertirEntidadEnDataRow(RecepcionDetalle obj, DataRow dr)
        {
            dr["IdRecepcion"] = obj.Recepcion.Id;
            dr["IdArticulo"] = obj.Articulo.Id;
            dr["CantidadARecibir"] = obj.CantidadARecibir;
            dr["CantidadRecibida"] = obj.CantidadRecibida;
            dr["PrecioUnitario"] = obj.Articulo.PrecioUnitario;
        }
    }
}
