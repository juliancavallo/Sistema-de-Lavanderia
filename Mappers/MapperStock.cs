using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperStock : IAdministrable<Stock>
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperArticulo mppArticulo = new MapperArticulo();
        MapperUbicacion mppUbicacion = new MapperUbicacion();
        MapperUsuario mppUsuario = new MapperUsuario();

        #region IAdministrable
        public void Alta(Stock obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Stock");
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

        public void Baja(Stock obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Stock");
                var stock = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(stock);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Stock obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Stock");
                var stock = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, stock);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Stock Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Stock");
                var stock = dt.Select("Id = " + id).FirstOrDefault();
                if (stock != null)
                    return this.ConvertirDataRowAEntidad(stock);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Stock Obtener(int idUbicacion, int idArticulo)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Stock");
                var stock = dt.Select("IdUbicacion = " + idUbicacion + " AND IdArticulo = " + idArticulo).FirstOrDefault();
                if (stock != null)
                    return this.ConvertirDataRowAEntidad(stock);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Stock> ObtenerTodos()
        {
            try
            {
                var lista = new List<Stock>();
                DataTable dt = dal.ObtenerTabla("Stock");
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

        private Stock ConvertirDataRowAEntidad(DataRow row)
        {
            var stock = new Stock()
            {
                Id = int.Parse(row["Id"].ToString()),
                Cantidad = int.Parse(row["Cantidad"].ToString()),
                Ubicacion = mppUbicacion.Obtener(int.Parse(row["IdUbicacion"].ToString())),
                Articulo = mppArticulo.Obtener(int.Parse(row["IdArticulo"].ToString()))
            };
            return stock;
        }

        private void ConvertirEntidadEnDataRow(Stock obj, DataRow dr)
        {
            dr["Cantidad"] = obj.Cantidad;
            dr["IdUbicacion"] = obj.Ubicacion.Id;
            dr["IdArticulo"] = obj.Articulo.Id;
        }

        #region Ajuste de Stock
        public void Alta(List<AjusteStock> lista)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("AjusteStock");

                foreach (var item in lista)
                { 
                    DataRow dr = dt.NewRow();
                    this.ConvertirAjusteStockEnDataRow(item, dr);
                    dt.Rows.Add(dr);
                }
                dal.ActualizarDataSet();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AjusteStock> ObtenerAjustesStock()
        {
            try
            {
                var lista = new List<AjusteStock>();
                DataTable dt = dal.ObtenerTabla("AjusteStock");
                foreach (DataRow row in dt.Rows)
                {
                    lista.Add(this.ConvertirDataRowEnAjusteStock(row));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ConvertirAjusteStockEnDataRow(AjusteStock obj, DataRow dr)
        {
            dr["NuevaCantidad"] = obj.NuevaCantidad;
            dr["CantidadPrevia"] = obj.CantidadPrevia;
            dr["IdUbicacion"] = obj.Ubicacion.Id;
            dr["IdArticulo"] = obj.Articulo.Id;
            dr["Observaciones"] = obj.Observaciones;
            dr["FechaCreacion"] = obj.FechaCreacion;
            dr["IdUsuario"] = obj.Usuario.Id;
        }

        private AjusteStock ConvertirDataRowEnAjusteStock(DataRow row)
        {
            var ajuste = new AjusteStock()
            {
                Id = int.Parse(row["Id"].ToString()),
                CantidadPrevia = int.Parse(row["CantidadPrevia"].ToString()),
                NuevaCantidad = int.Parse(row["NuevaCantidad"].ToString()),
                Ubicacion = mppUbicacion.Obtener(int.Parse(row["IdUbicacion"].ToString())),
                Articulo = mppArticulo.Obtener(int.Parse(row["IdArticulo"].ToString())),
                Observaciones = row["Observaciones"].ToString(),
                FechaCreacion = DateTime.Parse(row["FechaCreacion"].ToString()),
                Usuario = mppUsuario.Obtener(int.Parse(row["IdUsuario"].ToString()))
            };

            return ajuste;
        }
        #endregion
    }
}
