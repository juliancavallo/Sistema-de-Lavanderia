using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Servicios
{
    public class ServicioMenus
    {
        public List<Entidades.Menu> GenerarMenus()
        {
            
            var menuAdministracion = new MenuCompuesto() 
            { 
                Id = 1,
                Nombre = Entidades.Enums.NombreMenu.Administracion,
                Descripcion = "Administración",
                Codigo = Entidades.Enums.CodigoMenu.Administracion 
            };
            var menuTiposDePrenda = new MenuSimple() 
            { 
                Id = 2, 
                Nombre = Entidades.Enums.NombreMenu.TiposDePrenda, 
                Descripcion = "Tipos de Prenda",
                Codigo = Entidades.Enums.CodigoMenu.TiposDePrenda, 
                MenuPadre = menuAdministracion
            };
            var menuArticulos = new MenuSimple() 
            {
                Id = 3, 
                Nombre = Entidades.Enums.NombreMenu.Articulos,
                Descripcion = "Artículos",
                Codigo = Entidades.Enums.CodigoMenu.Articulos, 
                MenuPadre = menuAdministracion
            };
            var menuColores = new MenuSimple() 
            { 
                Id = 4, 
                Nombre = Entidades.Enums.NombreMenu.Colores, 
                Descripcion ="Colores",
                Codigo = Entidades.Enums.CodigoMenu.Colores, 
                MenuPadre = menuAdministracion
            };
            var menuTalles = new MenuSimple() 
            { 
                Id = 5, 
                Nombre = Entidades.Enums.NombreMenu.Talles, 
                Descripcion ="Talles",
                Codigo = Entidades.Enums.CodigoMenu.Talles,
                MenuPadre = menuAdministracion
            };
            var menuProcesos = new MenuCompuesto()
            {
                Id = 6,
                Nombre = Entidades.Enums.NombreMenu.Procesos,
                Descripcion = "Procesos",
                Codigo = Entidades.Enums.CodigoMenu.Procesos
            };
            var menuEnviosAClinica = new MenuSimple()
            {
                Id = 7,
                Nombre = Entidades.Enums.NombreMenu.EnviosAClinica,
                Descripcion = "Envíos a Clínica",
                Codigo = Entidades.Enums.CodigoMenu.EnviosAClinica,
                MenuPadre = menuProcesos
            };
            var menuEnviosInternos = new MenuSimple()
            {
                Id = 8,
                Nombre = Entidades.Enums.NombreMenu.EnviosInternos,
                Descripcion = "Envíos Internos",
                Codigo = Entidades.Enums.CodigoMenu.EnviosInternos,
                MenuPadre = menuProcesos
            };
            var menuEnviosALavadero = new MenuSimple()
            {
                Id = 9,
                Nombre = Entidades.Enums.NombreMenu.EnviosALavadero,
                Descripcion = "Envíos a Lavadero",
                Codigo = Entidades.Enums.CodigoMenu.EnviosALavadero,
                MenuPadre = menuProcesos
            };
            var menuHojasDeRuta = new MenuSimple()
            {
                Id = 10,
                Nombre = Entidades.Enums.NombreMenu.HojasDeRuta,
                Descripcion = "Hojas de Ruta",
                Codigo = Entidades.Enums.CodigoMenu.HojasDeRuta,
                MenuPadre = menuProcesos
            };
            var menuRecepcionesEnLavadero = new MenuSimple()
            {
                Id = 11,
                Nombre = Entidades.Enums.NombreMenu.RecepcionesEnLavadero,
                Descripcion = "Recepciones en Lavadero",
                Codigo = Entidades.Enums.CodigoMenu.RecepcionesEnLavadero,
                MenuPadre = menuProcesos
            };
            var menuRecepcionesEnClinica = new MenuSimple()
            {
                Id = 12,
                Nombre = Entidades.Enums.NombreMenu.RecepcionesEnClinica,
                Descripcion = "Recepciones en Clínica",
                Codigo = Entidades.Enums.CodigoMenu.RecepcionesEnClinica,
                MenuPadre = menuProcesos
            };
            var menuReportes = new MenuCompuesto()
            {
                Id = 13,
                Nombre = Entidades.Enums.NombreMenu.Reportes,
                Descripcion = "Reportes",
                Codigo = Entidades.Enums.CodigoMenu.Reportes
            };
            var menuReporteMovimientos = new MenuSimple()
            {
                Id = 14,
                Nombre = Entidades.Enums.NombreMenu.ReporteMovimientos,
                Descripcion = "Reporte de Movimientos",
                Codigo = Entidades.Enums.CodigoMenu.ReporteMovimientos,
                MenuPadre = menuReportes
            };
            var menuSeguridad = new MenuCompuesto()
            {
                Id = 15,
                Nombre = Entidades.Enums.NombreMenu.Seguridad,
                Descripcion = "Seguridad",
                Codigo = Entidades.Enums.CodigoMenu.Seguridad
            };
            var menuRoles = new MenuSimple()
            {
                Id = 16,
                Nombre = Entidades.Enums.NombreMenu.Roles,
                Descripcion = "Roles",
                Codigo = Entidades.Enums.CodigoMenu.Roles,
                MenuPadre = menuSeguridad
            };
            var menuUsuarios = new MenuSimple()
            {
                Id = 17,
                Nombre = Entidades.Enums.NombreMenu.Usuarios,
                Descripcion = "Usuarios",
                Codigo = Entidades.Enums.CodigoMenu.Usuarios,
                MenuPadre = menuSeguridad
            };
            var menuAuditorias = new MenuSimple()
            {
                Id = 18,
                Nombre = Entidades.Enums.NombreMenu.Auditorias,
                Descripcion = "Auditorias",
                Codigo = Entidades.Enums.CodigoMenu.Auditorias,
                MenuPadre = menuProcesos
            };
            var menuUbicaciones = new MenuSimple()
            {
                Id = 19,
                Nombre = Entidades.Enums.NombreMenu.Ubicaciones,
                Descripcion = "Ubicaciones",
                Codigo = Entidades.Enums.CodigoMenu.Ubicaciones,
                MenuPadre = menuAdministracion
            };
            var menuStock = new MenuSimple()
            {
                Id = 20,
                Nombre = Entidades.Enums.NombreMenu.Stock,
                Descripcion = "Stock",
                Codigo = Entidades.Enums.CodigoMenu.Stock,
                MenuPadre = menuAdministracion
            };
            var menuReporteAuditoria = new MenuSimple()
            {
                Id = 21,
                Nombre = Entidades.Enums.NombreMenu.ReporteAuditoria,
                Descripcion = "Reporte de Auditorias",
                Codigo = Entidades.Enums.CodigoMenu.ReporteAuditorias,
                MenuPadre = menuReportes
            };
            var menuReporteEnvioAClinica = new MenuSimple()
            {
                Id = 22,
                Nombre = Entidades.Enums.NombreMenu.ReporteEnviosAClinica,
                Descripcion = "Reporte de Envios a Clinica",
                Codigo = Entidades.Enums.CodigoMenu.ReporteEnviosAClinica,
                MenuPadre = menuReportes
            };
            var menuReporteEnvioALavadero = new MenuSimple()
            {
                Id = 23,
                Nombre = Entidades.Enums.NombreMenu.ReporteEnviosALavadero,
                Descripcion = "Reporte de Envios a Lavadero",
                Codigo = Entidades.Enums.CodigoMenu.ReporteEnviosALavadero,
                MenuPadre = menuReportes
            };
            var menuReporteEnviosInterno = new MenuSimple()
            {
                Id = 24,
                Nombre = Entidades.Enums.NombreMenu.ReporteEnviosInternos,
                Descripcion = "Reporte de Envios internos",
                Codigo = Entidades.Enums.CodigoMenu.ReporteEnviosInternos,
                MenuPadre = menuReportes
            }; 
            var menuReporteRecepcionesEnClinica = new MenuSimple()
            {
                Id = 25,
                Nombre = Entidades.Enums.NombreMenu.ReporteRecepcionesEnClinica,
                Descripcion = "Reporte de Recepciones en Clinica",
                Codigo = Entidades.Enums.CodigoMenu.ReporteRecepcionesEnClinica,
                MenuPadre = menuReportes
            };
            var menuReporteRecepcionesEnLavadero = new MenuSimple()
            {
                Id = 26,
                Nombre = Entidades.Enums.NombreMenu.ReporteRecepcionesEnLavadero,
                Descripcion = "Reporte de Recepciones en Lavadero",
                Codigo = Entidades.Enums.CodigoMenu.ReporteRecepcionesEnLavadero,
                MenuPadre = menuReportes
            };
            var menuReporteHojasDeRuta = new MenuSimple()
            {
                Id = 27,
                Nombre = Entidades.Enums.NombreMenu.ReporteHojasDeRuta,
                Descripcion = "Reporte de Hojas de Ruta",
                Codigo = Entidades.Enums.CodigoMenu.ReporteHojasDeRuta,
                MenuPadre = menuReportes
            };
            var menuAjusteStock = new MenuSimple()
            {
                Id = 28,
                Nombre = Entidades.Enums.NombreMenu.AjusteStock,
                Descripcion = "Ajuste de Stock",
                Codigo = Entidades.Enums.CodigoMenu.AjusteStock,
                MenuPadre = menuProcesos
            };
            var menuReporteAjusteStock = new MenuSimple()
            {
                Id = 29,
                Nombre = Entidades.Enums.NombreMenu.ReporteAjusteStock,
                Descripcion = "Reporte de Ajuste de Stock",
                Codigo = Entidades.Enums.CodigoMenu.ReporteAjusteStock,
                MenuPadre = menuReportes
            };
            var menuCategoria = new MenuSimple()
            {
                Id = 30,
                Nombre = Entidades.Enums.NombreMenu.Categoria,
                Descripcion = "Categorías",
                Codigo = Entidades.Enums.CodigoMenu.Categoria,
                MenuPadre = menuAdministracion
            };
            var menuBultoCompuesto = new MenuSimple()
            {
                Id = 31,
                Nombre = Entidades.Enums.NombreMenu.BultoCompuesto,
                Descripcion = "Bultos Compuestos",
                Codigo = Entidades.Enums.CodigoMenu.BultoCompuesto,
                MenuPadre = menuAdministracion
            };
            var menuParametros = new MenuSimple()
            {
                Id = 32,
                Nombre = Entidades.Enums.NombreMenu.Parametros,
                Descripcion = "Parámetros del Sistema",
                Codigo = Entidades.Enums.CodigoMenu.Parametros,
                MenuPadre = menuSeguridad
            };


            return new List<Entidades.Menu>() 
            {
                menuAdministracion,
                menuTiposDePrenda,
                menuArticulos,
                menuColores,
                menuTalles,
                menuProcesos,
                menuEnviosAClinica,
                menuEnviosInternos,
                menuEnviosALavadero,
                menuHojasDeRuta,
                menuRecepcionesEnLavadero,
                menuRecepcionesEnClinica,
                menuReportes,
                menuReporteMovimientos,
                menuSeguridad,
                menuRoles,
                menuUsuarios,
                menuAuditorias,
                menuUbicaciones,
                menuStock,
                menuReporteAuditoria,
                menuReporteEnvioAClinica,
                menuReporteEnvioALavadero,
                menuReporteEnviosInterno,
                menuReporteRecepcionesEnClinica,
                menuReporteRecepcionesEnLavadero,
                menuReporteHojasDeRuta,
                menuAjusteStock,
                menuReporteAjusteStock,
                menuCategoria,
                menuBultoCompuesto,
                menuParametros
            };
        }
    }
}
