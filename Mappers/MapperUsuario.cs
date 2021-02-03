using DAL;
using Entidades;
using Servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Mappers
{
    public class MapperUsuario : IAdministrable<Usuario>
    {
        AccesoArchivo dal = new AccesoArchivo();
        MapperRol mppRol = new MapperRol();
        MapperUbicacion mppUbicacion = new MapperUbicacion();

        #region IAdministrable
        public void Alta(Usuario obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Usuario");
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

        public void Baja(Usuario obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Usuario");
                var Usuario = dt.Select("Id = " + obj.Id).FirstOrDefault();
                dt.Rows.Remove(Usuario);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public void Modificacion(Usuario obj)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Usuario");
                var usuario = dt.Select("Id = " + obj.Id).FirstOrDefault();

                this.ConvertirEntidadEnDataRow(obj, usuario);

                dal.ActualizarDataSet();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Obtener(int id)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Usuario");
                var usuario = dt.Select("Id = " + id).FirstOrDefault();
                return this.ConvertirDataRowAEntidad(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Usuario Obtener(string nombreDeUsuario)
        {
            try
            {
                DataTable dt = dal.ObtenerTabla("Usuario");
                var usuario = dt.Select("NombreDeUsuario = '" + nombreDeUsuario + "'").FirstOrDefault();
                return this.ConvertirDataRowAEntidad(usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Usuario> ObtenerTodos()
        {
            try
            {
                var lista = new List<Usuario>();
                DataTable dt = dal.ObtenerTabla("Usuario");
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
        
        private Usuario ConvertirDataRowAEntidad(DataRow row)
        {
            var usuario = new Usuario()
            {
                Id = int.Parse(row["Id"].ToString()),
                NombreDeUsuario = row["NombreDeUsuario"].ToString(),
                Contraseña = row["Contraseña"].ToString(),
                Nombre = row["Nombre"].ToString(),
                Apellido = row["Apellido"].ToString(),
                DNI = int.Parse(row["DNI"].ToString()),
                Correo = row["Correo"].ToString(),
                Rol = mppRol.Obtener(int.Parse(row["IdRol"].ToString())),
                Ubicacion = mppUbicacion.Obtener(int.Parse(row["IdUbicacion"].ToString()))
            };
            return usuario;
        }

        private void ConvertirEntidadEnDataRow(Usuario obj, DataRow dr)
        {
            dr["NombreDeUsuario"] = obj.NombreDeUsuario;
            dr["Contraseña"] = obj.Contraseña;
            dr["Nombre"] = obj.Nombre;
            dr["Apellido"] = obj.Apellido;
            dr["DNI"] = obj.DNI;
            dr["Correo"] = obj.Correo;
            dr["IdRol"] = obj.Rol.Id;
            dr["IdUbicacion"] = obj.Ubicacion.Id;
        }
    }
}
