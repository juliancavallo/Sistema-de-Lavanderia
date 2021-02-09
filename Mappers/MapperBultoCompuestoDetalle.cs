using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperBultoCompuestoDetalle
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperTipoDePrenda mapperTipoDePrenda = new MapperTipoDePrenda();
        public void Alta(BultoCompuestoDetalle obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("BultoCompuestoDetalle");
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

        public void Alta(List<BultoCompuestoDetalle> obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("BultoCompuestoDetalle");
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

        public void Baja(BultoCompuestoDetalle obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("BultoCompuestoDetalle");
                var bultoCompuestoDetalle = dt.Select($"IdBultoCompuesto = {obj.BultoCompuesto.Id}").FirstOrDefault();
                dt.Rows.Remove(bultoCompuestoDetalle);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BultoCompuestoDetalle> ObtenerTodos()
        {
            try
            {
                var lista = new List<BultoCompuestoDetalle>();
                DataTable dt = dal.ObtenerTabla("BultoCompuestoDetalle");
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

        public List<BultoCompuestoDetalle> ObtenerTodos(int idBultoCompuesto)
        {
            try
            {
                var lista = new List<BultoCompuestoDetalle>();
                DataTable dt = dal.ObtenerTabla("BultoCompuestoDetalle");
                foreach (DataRow row in dt.Select("IdBultoCompuesto = " + idBultoCompuesto))
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

        private BultoCompuestoDetalle ConvertirDataRowAEntidad(DataRow row)
        {
            var bultoCompuestoDetalle = new BultoCompuestoDetalle()
            {
                TipoDePrenda = mapperTipoDePrenda.Obtener(int.Parse(row["IdTipoDePrenda"].ToString()))
            };
            return bultoCompuestoDetalle;
        }

        private void ConvertirEntidadEnDataRow(BultoCompuestoDetalle obj, DataRow dr)
        {
            dr["IdBultoCompuesto"] = obj.BultoCompuesto.Id;
            dr["IdTipoDePrenda"] = obj.TipoDePrenda.Id;
        }
    }
}
