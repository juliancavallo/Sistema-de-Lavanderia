using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperArticulo : IAdministrable<Articulo>
    {
        AccesoArchivo dal = new AccesoArchivo();

        MapperTalle mppTalle = new MapperTalle();
        MapperTipoDePrenda mppTipoDePrenda = new MapperTipoDePrenda();
        MapperColor mppColor = new MapperColor();

        #region IAdministrable
        public void Alta(Articulo obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Articulo");
                DataRow dr = dt.NewRow();

                this.ConvertirEntidadADataRow(obj, dr);

                dt.Rows.Add(dr);
                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Baja(Articulo obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Articulo");
                var articulo = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(articulo);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Articulo obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Articulo");
                var articulo = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadADataRow(obj, articulo);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Articulo Obtener(int id)
        {
            try
            {
                var lista = new List<Articulo>();
                DataTable dt = dal.ObtenerTabla("Articulo");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> ObtenerTodos()
        {
            try
            {
                var lista = new List<Articulo>();
                DataTable dt = dal.ObtenerTabla("Articulo");
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

        private Articulo ConvertirDataRowAEntidad(DataRow row)
        {
            int idTipoDePrenda = int.Parse(row["IdTipoDePrenda"].ToString());
            int idTalle = int.Parse(row["IdTalle"].ToString());
            int idColor = int.Parse(row["IdColor"].ToString());
            var articulo = new Articulo()
            {
                Id = int.Parse(row["Id"].ToString()),
                Codigo = row["Codigo"].ToString(),
                TipoDePrenda = mppTipoDePrenda.Obtener(idTipoDePrenda),
                Color = mppColor.Obtener(idColor),
                Talle = mppTalle.Obtener(idTalle),
                PrecioUnitario = decimal.Parse(row["PrecioUnitario"].ToString()),
                PesoUnitario = decimal.Parse(row["PesoUnitario"].ToString())
            };
            return articulo;
        }
        private void ConvertirEntidadADataRow(Articulo obj, DataRow dr)
        {
           dr["Codigo"] = obj.Codigo;
           dr["IdTipoDePrenda"] = obj.TipoDePrenda.Id;
           dr["IdColor"] = obj.Color.Id;
           dr["IdTalle"] = obj.Talle.Id;
           dr["PrecioUnitario"] = obj.PrecioUnitario;
           dr["PesoUnitario"] = obj.PesoUnitario;
        }
    }
}
