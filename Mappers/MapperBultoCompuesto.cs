using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperBultoCompuesto : IAdministrable<BultoCompuesto>
    {
        AccesoArchivo dal = new AccesoArchivo();
        MapperBultoCompuestoDetalle mapperBultoCompuestoDetalle = new MapperBultoCompuestoDetalle();

        #region IAdministrable
        public void Alta(BultoCompuesto obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("BultoCompuesto");
                DataRow dr = dt.NewRow();

                this.ConvertirEntidadEnDataRow(obj, dr);

                dt.Rows.Add(dr);
                dal.ActualizarDataSet();

                obj.Id = int.Parse(dr["Id"].ToString());

                obj.Detalle.ForEach(x => x.BultoCompuesto = obj);
                mapperBultoCompuestoDetalle.Alta(obj.Detalle);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Baja(BultoCompuesto obj)
        {
            try
            {
                obj.Detalle.ForEach(x => mapperBultoCompuestoDetalle.Baja(x));

                DataTable dt = dal.ObtenerTabla("BultoCompuesto");
                var bultoCompuesto = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(bultoCompuesto);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificacion(BultoCompuesto obj)
        {
            this.Baja(obj);
            this.Alta(obj);
        }

        public BultoCompuesto Obtener(int id)
        {
            try
            {
                var lista = new List<BultoCompuesto>();
                DataTable dt = dal.ObtenerTabla("BultoCompuesto");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BultoCompuesto> ObtenerTodos()
        {
            try
            {
                var lista = new List<BultoCompuesto>();
                DataTable dt = dal.ObtenerTabla("BultoCompuesto");
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
        #endregion

        private BultoCompuesto ConvertirDataRowAEntidad(DataRow row)
        {
            var bultoCompuesto = new BultoCompuesto()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString(),
                Detalle = mapperBultoCompuestoDetalle.ObtenerTodos(int.Parse(row["Id"].ToString()))
            };
            return bultoCompuesto;
        }

        private void ConvertirEntidadEnDataRow(BultoCompuesto obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
        }
    }
}
