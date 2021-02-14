using Entidades;
using Enums = Entidades.Enums;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class SeguridadBLL
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        RolBLL rolBLL = new RolBLL();
        MenuBLL menuBLL = new MenuBLL();
        ParametroDelSistemaBLL parametroBLL = new ParametroDelSistemaBLL();
        EstadoEnvioBLL estadoEnvioBLL = new EstadoEnvioBLL();
        EstadoHojaDeRutaBLL estadoHojaDeRutaBLL = new EstadoHojaDeRutaBLL();
        UbicacionBLL ubicacionBLL = new UbicacionBLL();
        ColorBLL colorBLL = new ColorBLL();
        TalleBLL talleBLL = new TalleBLL();
        TipoDePrendaBLL tipoDePrendaBLL = new TipoDePrendaBLL();
        ArticuloBLL articuloBLL = new ArticuloBLL();
        CategoriaBLL categoriaBLL = new CategoriaBLL();

        ServicioSeguridad servicioSeguridad = new ServicioSeguridad();
        ServicioMenus servicioMenus = new ServicioMenus();

        MapperSeguridad mppSeguridad = new MapperSeguridad();

        public static Usuario usuarioLogueado;

        public bool NombreDeUsuarioValido(string nombreUsuario)
        {
            try
            {
                var usuario = usuarioBLL.ObtenerTodos().Where(x => x.NombreDeUsuario == nombreUsuario).FirstOrDefault();
                return usuario != null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ContraseñaValida(string nombreUsuario, string contraseña)
        {
            try
            {
                var usuario = usuarioBLL.Obtener(nombreUsuario);
                return usuario.Contraseña == contraseña;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ReestablecerContraseña(string nombreUsuario)
        {
            try
            {
                var usuario = usuarioBLL.Obtener(nombreUsuario);
                usuario.Contraseña = servicioSeguridad.Encriptar(servicioSeguridad.GenerarContraseñaAleatoria(8));

                usuarioBLL.Modificacion(usuario);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EnviarCorreoRecuperoDeContraseña(Usuario destino)
        {
            try
            {
                string mensaje = string.Format(
                    @"<p>Su nueva contraseña es <h1>{0}</h1> </p> " +
                    "<p>Recuerde cambiarla cuando inicie sesión nuevamente</p>" +
                    "<p>Muchas gracias</p>",
                    servicioSeguridad.Desencriptar(destino.Contraseña));

                ServicioMail servicioMail = new ServicioMail();
                servicioMail.EnviarMail(
                    destino.Correo,
                    "Reestablecimiento de contraseña",
                    mensaje,
                    parametroBLL.Obtener(Entidades.Enums.ParametroDelSistema.CorreoSoporte).Valor,
                    servicioSeguridad.Desencriptar(parametroBLL.Obtener(Entidades.Enums.ParametroDelSistema.ContraseñaCorreoSoporte).Valor));
            }
            catch(Exception)
            { 
                throw; 
            }
        }

        public void GenerarDataSet()
        {
            if (!mppSeguridad.ArchivoExiste())
            {
                //Si no existe el archivo, se insertan los datos para poder iniciar sesion
                mppSeguridad.GenerarDataSet();

                this.GenerarDatosDeInicio();
            }
            else
                mppSeguridad.CargarDataSet();
        }

        private void GenerarDatosDeInicio()
        {
            var menus = servicioMenus.GenerarMenus();
            menuBLL.Alta(menus);

            var rolAdministrador = new Rol()
            {
                Descripcion = "Administrador",
                Menus = new List<Menu>()
                    {
                        menus.Where(x => x.Codigo == Enums.CodigoMenu.Administracion).FirstOrDefault(),
                        menus.Where(x => x.Codigo == Enums.CodigoMenu.Procesos).FirstOrDefault(),
                        menus.Where(x => x.Codigo == Enums.CodigoMenu.Reportes).FirstOrDefault(),
                        menus.Where(x => x.Codigo == Enums.CodigoMenu.Seguridad).FirstOrDefault()
                    }
            };
            rolAdministrador.Menus.ForEach(x => x.Simbolo = true);

            rolBLL.Alta(rolAdministrador);

            var ubicacion = new Ubicacion()
            {
                Descripcion = "Lavanderia",
                Direccion = "Av. Belgrano 1200",
                TipoDeUbicacion = (int)Enums.TipoDeUbicacion.Lavanderia,
                UbicacionPadre = null,
                ClienteExterno = false
            };
            ubicacionBLL.Alta(ubicacion);

            ubicacion = new Ubicacion()
            {
                Descripcion = "Clinica Olivos",
                Direccion = "Av. Maipú 1660",
                TipoDeUbicacion = (int)Enums.TipoDeUbicacion.Clinica,
                UbicacionPadre = null,
                ClienteExterno = true
            };
            ubicacionBLL.Alta(ubicacion);

            ubicacion = new Ubicacion()
            {
                Descripcion = "Olivos Piso 1",
                Direccion = "Av. Maipú 1660 - Piso 1",
                TipoDeUbicacion = (int)Enums.TipoDeUbicacion.Clinica,
                UbicacionPadre = ubicacionBLL.ObtenerTodos().First(x => x.Descripcion == "Clinica Olivos"),
                ClienteExterno = true
            };
            ubicacionBLL.Alta(ubicacion);

            ubicacion = new Ubicacion()
            {
                Descripcion = "Clinica Estrada",
                Direccion = "Uriarte 1200",
                TipoDeUbicacion = (int)Enums.TipoDeUbicacion.Clinica,
                UbicacionPadre = null,
                ClienteExterno = false
            };
            ubicacionBLL.Alta(ubicacion);


            var usuario = new Usuario()
            {
                NombreDeUsuario = "admin",
                Contraseña = "MQAyADMANAA1ADYANwA4AA==",
                Nombre = "Usuario",
                Apellido = "Administrador",
                DNI = 12345678,
                Rol = rolAdministrador,
                Correo = "jcavallo11@gmail.com",
                Ubicacion = ubicacionBLL.ObtenerTodos().First()
            };
            usuarioBLL.Alta(usuario);

            var parametro = new ParametroDelSistema()
            {
                Nombre = "CorreoSoporte",
                Valor = "sistemalavanderia.soporte@gmail.com"
            };
            parametroBLL.Alta(parametro);

            parametro = new ParametroDelSistema()
            {
                Nombre = "ContraseñaCorreoSoporte",
                Valor = "cwBvAHAAbwByAHQAZQAyADQAMwA1AA=="
            };
            parametroBLL.Alta(parametro);

            parametro = new ParametroDelSistema()
            {
                Nombre = "RolesAdministradoresDeUsuarios",
                Valor = "Administrador,Coordinador de Lavadero,Coordinador de Clinica"
            };
            parametroBLL.Alta(parametro);

            parametro = new ParametroDelSistema()
            {
                Nombre = "PorcentajeDescuentoDeEnvios",
                Valor = "10,2"
            };
            parametroBLL.Alta(parametro);

            var estadoEnvio = new EstadoEnvio()
            {
                Descripcion = Enums.EstadoEnvio.Generado
            };
            estadoEnvioBLL.Alta(estadoEnvio);

            estadoEnvio = new EstadoEnvio()
            {
                Descripcion = Enums.EstadoEnvio.Enviado
            };
            estadoEnvioBLL.Alta(estadoEnvio);

            estadoEnvio = new EstadoEnvio()
            {
                Descripcion = Enums.EstadoEnvio.Recibido
            };
            estadoEnvioBLL.Alta(estadoEnvio);

            var estadoHojaDeRuta = new EstadoHojaDeRuta()
            {
                Descripcion = Enums.EstadoHojaDeRuta.Generada
            };
            estadoHojaDeRutaBLL.Alta(estadoHojaDeRuta);

            estadoHojaDeRuta = new EstadoHojaDeRuta()
            {
                Descripcion = Enums.EstadoHojaDeRuta.Recibida
            };
            estadoHojaDeRutaBLL.Alta(estadoHojaDeRuta);

            //Accesorios
            var categoria = new Categoria()
            {
                Descripcion = "Accesorios",
                EsCompuesta = false
            };
            categoriaBLL.Alta(categoria);

            var tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Cofia",
                CortePorBulto = 1,
                UsaCortePorBulto = true,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Barbijo",
                CortePorBulto = 0,
                UsaCortePorBulto = false,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            //Ropa de cama
            categoria = new Categoria()
            {
                Descripcion = "Ropa de Cama",
                EsCompuesta = false
            };
            categoriaBLL.Alta(categoria);

            tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Sabana",
                CortePorBulto = 10,
                UsaCortePorBulto = true,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Cubrecama",
                CortePorBulto = 5,
                UsaCortePorBulto = true,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Colcha",
                CortePorBulto = 5,
                UsaCortePorBulto = true,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            //Baño
            categoria = new Categoria()
            {
                Descripcion = "Baño",
                EsCompuesta = false
            };
            categoriaBLL.Alta(categoria);

            tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Toalla de mano",
                CortePorBulto = 5,
                UsaCortePorBulto = true,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Toallón",
                CortePorBulto = 5,
                UsaCortePorBulto = true,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            //Uniformes
            categoria = new Categoria()
            {
                Descripcion = "Ambo",
                EsCompuesta = true
            };
            categoriaBLL.Alta(categoria);

            tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Ambo chaqueta",
                CortePorBulto = 10,
                UsaCortePorBulto = true,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            tipoDePrenda = new TipoDePrenda()
            {
                Descripcion = "Ambo pantalón",
                CortePorBulto = 10,
                UsaCortePorBulto = true,
                Categoria = categoria
            };
            tipoDePrendaBLL.Alta(tipoDePrenda);

            var color = new Color() { Descripcion = "Blanco" };
            colorBLL.Alta(color);

            color = new Color() { Descripcion = "Azul" };
            colorBLL.Alta(color);

            color = new Color() { Descripcion = "Beige" };
            colorBLL.Alta(color);

            color = new Color() { Descripcion = "Marron claro" };
            colorBLL.Alta(color);

            var talle = new Talle() { Descripcion = "Mediano" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "Grande" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "Chico" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "Generico" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "XL" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "L" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "M" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "XXL" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "38" };
            talleBLL.Alta(talle);

            talle = new Talle() { Descripcion = "36" };
            talleBLL.Alta(talle);

            var articulo = new Articulo()
            {
                Codigo = "TOA-001",
                Color = colorBLL.Obtener("Blanco"),
                Talle = talleBLL.Obtener("Mediano"),
                TipoDePrenda = tipoDePrendaBLL.Obtener("Toalla de mano"),
                PrecioUnitario = 70m,
                PesoUnitario = 0.4m
            };
            articuloBLL.Alta(articulo);

            articulo = new Articulo()
            {
                Codigo = "TOA-002",
                Color = colorBLL.Obtener("Blanco"),
                Talle = talleBLL.Obtener("Chico"),
                TipoDePrenda = tipoDePrendaBLL.Obtener("Toalla de mano"),
                PrecioUnitario = 70m,
                PesoUnitario = 0.4m
            };
            articuloBLL.Alta(articulo);


            articulo = new Articulo()
            {
                Codigo = "SAB-001",
                Color = colorBLL.Obtener("Blanco"),
                Talle = talleBLL.Obtener("Grande"),
                TipoDePrenda = tipoDePrendaBLL.Obtener("Sabana"),
                PrecioUnitario = 150.5m,
                PesoUnitario = 2m
            };
            articuloBLL.Alta(articulo);

            articulo = new Articulo()
            {
                Codigo = "CHA-001",
                Color = colorBLL.Obtener("Azul"),
                Talle = talleBLL.Obtener("XL"),
                TipoDePrenda = tipoDePrendaBLL.Obtener("Ambo chaqueta"),
                PrecioUnitario = 115.6m,
                PesoUnitario = 0.5m
            };
            articuloBLL.Alta(articulo);

            articulo = new Articulo()
            {
                Codigo = "PAN-001",
                Color = colorBLL.Obtener("Azul"),
                Talle = talleBLL.Obtener("38"),
                TipoDePrenda = tipoDePrendaBLL.Obtener("Ambo pantalón"),
                PrecioUnitario = 90m,
                PesoUnitario = 0.4m
            };
            articuloBLL.Alta(articulo);

            articulo = new Articulo()
            {
                Codigo = "CHA-002",
                Color = colorBLL.Obtener("Azul"),
                Talle = talleBLL.Obtener("L"),
                TipoDePrenda = tipoDePrendaBLL.Obtener("Ambo chaqueta"),
                PrecioUnitario = 115.6m,
                PesoUnitario = 0.5m
            };
            articuloBLL.Alta(articulo);

            articulo = new Articulo()
            {
                Codigo = "PAN-002",
                Color = colorBLL.Obtener("Azul"),
                Talle = talleBLL.Obtener("36"),
                TipoDePrenda = tipoDePrendaBLL.Obtener("Ambo pantalón"),
                PrecioUnitario = 90m,
                PesoUnitario = 0.4m
            };
            articuloBLL.Alta(articulo);

            
        }

        public void HacerBackup()
        {
            mppSeguridad.HacerBackup();
        }
    }
}
