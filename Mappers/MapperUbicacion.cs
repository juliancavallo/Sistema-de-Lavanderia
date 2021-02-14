using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperUbicacion : IAdministrable<Ubicacion>
    {
        AccesoArchivo dal = new AccesoArchivo();
        MapperArticulo mppArticulo = new MapperArticulo();

        #region IAdministrable
        public void Alta(Ubicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Ubicacion");
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

        public void Baja(Ubicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Ubicacion");
                var ubicacion = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(ubicacion);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Ubicacion obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Ubicacion");
                var ubicacion = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, ubicacion);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Ubicacion Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Ubicacion");
                var ubicacion = dt.Select("Id = " + id).FirstOrDefault();
                if (ubicacion != null)
                    return this.ConvertirDataRowAEntidad(ubicacion);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ubicacion> ObtenerTodos()
        {
            try
            {
                var lista = new List<Ubicacion>();
                DataTable dt = dal.ObtenerTabla("Ubicacion");
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

        private Ubicacion ConvertirDataRowAEntidad(DataRow row)
        {
            string idUbicacionPadreString = row["IdUbicacionPadre"].ToString();
            int? idUbicacionPadre = int.TryParse(idUbicacionPadreString, out var temp) ? temp : (int?)null;

            var ubicacion = new Ubicacion()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString(),
                Direccion = row["Direccion"].ToString(),
                TipoDeUbicacion = int.Parse(row["TipoDeUbicacion"].ToString()),
                UbicacionPadre = idUbicacionPadre.HasValue ? this.Obtener(idUbicacionPadre.Value) : null,
                ClienteExterno = bool.Parse(row["ClienteExterno"].ToString()),
                CapacidadTotal = decimal.Parse(row["CapacidadTotal"].ToString()),
                Stock = this.ObtenerStock(int.Parse(row["Id"].ToString()))
            };
            return ubicacion;
        }

        public void ConvertirEntidadEnDataRow(Ubicacion obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
            dr["Direccion"] = obj.Direccion;
            dr["TipoDeUbicacion"] = obj.TipoDeUbicacion;
            dr["IdUbicacionPadre"] = (object)obj.UbicacionPadre?.Id ?? DBNull.Value;
            dr["ClienteExterno"] = obj.ClienteExterno;
            dr["CapacidadTotal"] = obj.CapacidadTotal;
        }

        private List<Stock> ObtenerStock(int idUbicacion)
        {
            var lista = new List<Stock>();
            DataTable dt = dal.ObtenerTabla("Stock");
            var stock = dt.Select("IdUbicacion = " + idUbicacion);
            foreach (DataRow row in stock)
            {
                lista.Add(new Stock() 
                {
                    Id = int.Parse(row["Id"].ToString()),
                    Cantidad = int.Parse(row["Cantidad"].ToString()),
                    Articulo = mppArticulo.Obtener(int.Parse(row["IdArticulo"].ToString()))
                });
            }
            return lista;
        }
    }
}
