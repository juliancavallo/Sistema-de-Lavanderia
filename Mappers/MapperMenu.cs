using DAL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Mappers
{
    public class MapperMenu
    {
        AccesoArchivo dal = new AccesoArchivo();

        public void Alta(Menu obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Menu");
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

        public void Alta(List<Menu> obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Menu");
                foreach (var menu in obj)
                {
                    DataRow dr = dt.NewRow();

                    this.ConvertirEntidadEnDataRow(menu, dr);

                    dt.Rows.Add(dr);

                }
                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> ObtenerTodos()
        {
            try
            {
                var lista = new List<Menu>();
                DataTable dt = dal.ObtenerTabla("Menu");
                foreach (DataRow row in dt.Rows)
                {
                    if (string.IsNullOrWhiteSpace(row["IdMenuPadre"].ToString()))
                    {
                        lista.Add(ConvertirDataRowAEntidadCompuesta(row));
                    }
                    else
                    {
                        lista.Add(ConvertirDataRowAEntidadSimple(row));
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Menu Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Menu");
                var row = dt.Select("Id = " + id).FirstOrDefault();
                if (string.IsNullOrWhiteSpace(row["IdMenuPadre"].ToString()))
                    return ConvertirDataRowAEntidadCompuesta(row);
                else
                    return ConvertirDataRowAEntidadSimple(row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> ObtenerPorRol(int idRol)
        {
            try
            {
                var lista = new List<Menu>();
                DataTable dt = dal.ObtenerTabla("MenuPorRoles");
                foreach (DataRow row in dt.Select("IdRol = " + idRol))
                {
                    var idMenu = int.Parse(row["IdMenu"].ToString());
                    var menu = this.Obtener(idMenu);
                    menu.Simbolo = bool.Parse(row["Simbolo"].ToString());
                    
                    lista.Add(menu);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Menu ConvertirDataRowAEntidadCompuesta(DataRow row)
        {
            var menu = new MenuCompuesto()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString(),
                Codigo = row["Codigo"].ToString(),
                Nombre = row["Nombre"].ToString()
            };
            return menu;
        }

        private Menu ConvertirDataRowAEntidadSimple(DataRow row)
        {
            var menu = new MenuSimple()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString(),
                Codigo = row["Codigo"].ToString(),
                Nombre = row["Nombre"].ToString(),
                MenuPadre = this.Obtener(int.Parse(row["IdMenuPadre"].ToString())) 
            };
            return menu;
        }

        private void ConvertirEntidadEnDataRow(Menu obj, DataRow dr)
        {
            dr["Nombre"] = obj.Nombre;
            dr["Descripcion"] = obj.Descripcion;
            dr["Codigo"] = obj.Codigo;
            dr["IdMenuPadre"] = obj is MenuSimple ? (obj as MenuSimple).MenuPadre.Id as object : DBNull.Value;
        }
    }
}
