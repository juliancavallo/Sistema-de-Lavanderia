using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperRol : IAdministrable<Rol>
    {
        AccesoArchivo dal = new AccesoArchivo();
        MapperMenu mppMenu = new MapperMenu();

        #region IAdministrable
        public void Alta(Rol obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Rol");
                DataRow dr = dt.NewRow();

                this.ConvertirEntidadEnDataRow(obj, dr);

                dt.Rows.Add(dr);
                dal.ActualizarDataSet();

                obj.Id = (int)dr["Id"];

                this.AltaMenuPorRoles(obj);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Baja(Rol obj)
        {
            try
            {
                this.BajaMenuPorRoles(obj);

                DataTable dt = dal.ObtenerTabla("Rol");
                var rol = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(rol);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Rol obj)
        {
            try
            {
                this.BajaMenuPorRoles(obj);
                this.AltaMenuPorRoles(obj);

                DataTable dt = dal.ObtenerTabla("Rol");
                var rol = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, rol);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Rol Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Rol");
                var parametroDelSistema = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(parametroDelSistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Rol> ObtenerTodos()
        {
            try
            {
                var lista = new List<Rol>();
                DataTable dt = dal.ObtenerTabla("Rol");
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

        #region MenuPorRoles
        public void AltaMenuPorRoles(Rol obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("MenuPorRoles");
                foreach (var menu in obj.Menus)
                {
                    DataRow dr = dt.NewRow();
                    dr["IdRol"] = obj.Id;
                    dr["IdMenu"] = menu.Id;
                    dr["Simbolo"] = menu.Simbolo;
                    dt.Rows.Add(dr);
                }
                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void BajaMenuPorRoles(Rol obj)
        {
            DataTable dt = dal.ObtenerTabla("MenuPorRoles");
            var menuPorRoles = dt.Select("IdRol = " + obj.Id);
            foreach (var menu in menuPorRoles)
            {
                dt.Rows.Remove(menu);
            }

            dal.ActualizarDataSet();
        }
        #endregion

        #region RolesPorUsuario
        public List<Rol> ObtenerPorUsuario(int idUsuario)
        {
            try
            {
                var lista = new List<Rol>();
                DataTable dt = dal.ObtenerTabla("RolesPorUsuario");
                foreach (DataRow row in dt.Select("IdUsuario = " + idUsuario))
                {
                    lista.Add(this.Obtener(int.Parse(row["IdRol"].ToString())));
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        private Rol ConvertirDataRowAEntidad(DataRow row)
        {
            var rol = new Rol()
            {
                Id = int.Parse(row["Id"].ToString()),
                Descripcion = row["Descripcion"].ToString(),
                Menus = mppMenu.ObtenerPorRol(int.Parse(row["Id"].ToString()))
            };
            return rol;
        }

        private void ConvertirEntidadEnDataRow(Rol obj, DataRow dr)
        {
            dr["Descripcion"] = obj.Descripcion;
        }
    }
}
