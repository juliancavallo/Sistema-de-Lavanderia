using System;
using System.Collections.Generic;
using System.Data;
using Entidades;
using System.Text;
using System.Linq;

namespace Servicios
{
    public class ServicioTablas
    {
        public void CrearDataSet(DataSet ds)
        {
            if (ds.Tables.Count == 0)
            {
                this.CrearTablas(ds);
            }
        }

        private void CrearTablas(DataSet ds)
        {
            //Parametro del Sistema
            DataTable dtParametros = new DataTable();
            dtParametros.TableName = "ParametroDelSistema";

            DataColumn colParamId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colParamNombre = new DataColumn("Nombre", Type.GetType("System.String"));
            DataColumn colParamValor = new DataColumn("Valor", Type.GetType("System.String"));

            dtParametros.Columns.AddRange(new DataColumn[]
            {
                  colParamId,
                  colParamNombre,
                  colParamValor
            });

            colParamId.AutoIncrement = true;
            colParamId.AutoIncrementSeed = 1;
            dtParametros.PrimaryKey = new DataColumn[] { colParamId };

            ds.Tables.Add(dtParametros);

            //Usuario
            DataTable dtUsuario = new DataTable();
            dtUsuario.TableName = "Usuario";

            DataColumn colUsuarioId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colUsuarioNombreDeUsuario = new DataColumn("NombreDeUsuario", Type.GetType("System.String"));
            DataColumn colUsuarioContraseña = new DataColumn("Contraseña", Type.GetType("System.String"));
            DataColumn colUsuarioNombre = new DataColumn("Nombre", Type.GetType("System.String"));
            DataColumn colUsuarioApellido = new DataColumn("Apellido", Type.GetType("System.String"));
            DataColumn colUsuarioDNI = new DataColumn("DNI", Type.GetType("System.Int32"));
            DataColumn colUsuarioCorreo = new DataColumn("Correo", Type.GetType("System.String"));
            DataColumn colUsuarioIdRol = new DataColumn("IdRol", Type.GetType("System.Int32"));
            DataColumn colUsuarioIdUbicacion = new DataColumn("IdUbicacion", Type.GetType("System.Int32"));

            dtUsuario.Columns.AddRange(new DataColumn[]
            {
                        colUsuarioId,
                        colUsuarioNombreDeUsuario,
                        colUsuarioContraseña,
                        colUsuarioNombre,
                        colUsuarioApellido,
                        colUsuarioDNI,
                        colUsuarioCorreo,
                        colUsuarioIdRol,
                        colUsuarioIdUbicacion
            });

            colUsuarioId.AutoIncrement = true;
            colUsuarioId.AutoIncrementSeed = 1;
            dtUsuario.PrimaryKey = new DataColumn[] { colUsuarioId };

            ds.Tables.Add(dtUsuario);

            //Rol
            DataTable dtRol = new DataTable();
            dtRol.TableName = "Rol";

            DataColumn colRolId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colRolDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));

            dtRol.Columns.AddRange(new DataColumn[]
            {
                            colRolId,
                            colRolDescripcion
            });

            colRolId.AutoIncrement = true;
            colRolId.AutoIncrementSeed = 1;
            dtRol.PrimaryKey = new DataColumn[] { colRolId };

            ds.Tables.Add(dtRol);

            DataRelation FKRol_Usuario = new DataRelation(
                "FKRol_Usuario",
                ds.Tables["Rol"].Columns["Id"],
                ds.Tables["Usuario"].Columns["IdRol"]);

            ds.Relations.Add(FKRol_Usuario);

            //Menu
            DataTable dtMenu = new DataTable();
            dtMenu.TableName = "Menu";
            DataColumn colMenuId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colMenuDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));
            DataColumn colMenuNombre = new DataColumn("Nombre", Type.GetType("System.String"));
            DataColumn colMenuCodigo = new DataColumn("Codigo", Type.GetType("System.String"));
            DataColumn colMenuIdMenuPadre = new DataColumn("IdMenuPadre", Type.GetType("System.Int32")) { AllowDBNull = true };

            dtMenu.Columns.AddRange(new DataColumn[]
            {
                    colMenuId,
                    colMenuNombre,
                    colMenuDescripcion,
                    colMenuCodigo,
                    colMenuIdMenuPadre
            });

            colMenuId.AutoIncrement = true;
            colMenuId.AutoIncrementSeed = 1;
            dtMenu.PrimaryKey = new DataColumn[] { colMenuId };

            ds.Tables.Add(dtMenu);

            //MenuPorRoles
            DataTable dtMenuPorRoles = new DataTable();
            dtMenuPorRoles.TableName = "MenuPorRoles";

            DataColumn colMenuPorRolesIdRol = new DataColumn("IdRol", Type.GetType("System.Int32"));
            DataColumn colMenuPorRolesIdMenu = new DataColumn("IdMenu", Type.GetType("System.Int32"));
            DataColumn colMenuPorRolesSimbolo = new DataColumn("Simbolo", Type.GetType("System.Boolean"));

            dtMenuPorRoles.Columns.AddRange(new DataColumn[]
            {
                colMenuPorRolesIdRol,
                colMenuPorRolesIdMenu,
                colMenuPorRolesSimbolo
            });
            dtMenuPorRoles.PrimaryKey = new DataColumn[] { colMenuPorRolesIdRol, colMenuPorRolesIdMenu };
            ds.Tables.Add(dtMenuPorRoles);

            DataRelation FKRol_MenuPorRoles = new DataRelation(
                "FKRol_MenuPorRoles",
                ds.Tables["Rol"].Columns["Id"],
                ds.Tables["MenuPorRoles"].Columns["IdRol"]);

            DataRelation FKMenu_MenuPorRoles = new DataRelation(
                "FKMenu_MenuPorRoles",
                ds.Tables["Menu"].Columns["Id"],
                ds.Tables["MenuPorRoles"].Columns["IdMenu"]);

            ds.Relations.Add(FKRol_MenuPorRoles);
            ds.Relations.Add(FKMenu_MenuPorRoles);


            //Categoria
            DataTable dtCategoria = new DataTable();
            dtCategoria.TableName = "Categoria";

            DataColumn colCategoriaId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colCategoriaDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));
            DataColumn colCategoriaEsCompuesta = new DataColumn("EsCompuesta", Type.GetType("System.Boolean"));

            dtCategoria.Columns.AddRange(new DataColumn[]
            {
                 colCategoriaId,
                 colCategoriaDescripcion,
                 colCategoriaEsCompuesta
            });

            colCategoriaId.AutoIncrement = true;
            colCategoriaId.AutoIncrementSeed = 1;
            dtCategoria.PrimaryKey = new DataColumn[] { colCategoriaId };
            ds.Tables.Add(dtCategoria);


            //Tipo de Prenda
            DataTable dtTipoDePrenda = new DataTable();
            dtTipoDePrenda.TableName = "TipoDePrenda";

            DataColumn colTipoDePrendaId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colTipoDePrendaDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));
            DataColumn colTipoDePrendaCortePorBulto = new DataColumn("CortePorBulto", Type.GetType("System.Int32"));
            DataColumn colTipoDePrendaUsaCortePorBulto = new DataColumn("UsaCortePorBulto", Type.GetType("System.Boolean"));
            DataColumn colTipoDePrendaIdCategoria = new DataColumn("IdCategoria", Type.GetType("System.Int32"));

            dtTipoDePrenda.Columns.AddRange(new DataColumn[]
            {
                        colTipoDePrendaId,
                        colTipoDePrendaDescripcion,
                        colTipoDePrendaCortePorBulto,
                        colTipoDePrendaUsaCortePorBulto,
                        colTipoDePrendaIdCategoria
            });

            colTipoDePrendaId.AutoIncrement = true;
            colTipoDePrendaId.AutoIncrementSeed = 1;
            dtTipoDePrenda.PrimaryKey = new DataColumn[] { colTipoDePrendaId };
            
            ds.Tables.Add(dtTipoDePrenda);

            DataRelation FKCategoria_TipoDePrenda = new DataRelation(
                "FKCategoria_TipoDePrenda",
                ds.Tables["Categoria"].Columns["Id"],
                ds.Tables["TipoDePrenda"].Columns["IdCategoria"]);

            ds.Relations.Add(FKCategoria_TipoDePrenda);

            //Color
            DataTable dtColor = new DataTable();
            dtColor.TableName = "Color";

            DataColumn colColorId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colColorDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));

            dtColor.Columns.AddRange(new DataColumn[]
            {
                 colColorId,
                 colColorDescripcion
            });

            colColorId.AutoIncrement = true;
            colColorId.AutoIncrementSeed = 1;
            dtColor.PrimaryKey = new DataColumn[] { colColorId };
            ds.Tables.Add(dtColor);


            //Talle
            DataTable dtTalle = new DataTable();
            dtTalle.TableName = "Talle";

            DataColumn colTalleId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colTalleDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));

            dtTalle.Columns.AddRange(new DataColumn[]
            {
                 colTalleId,
                 colTalleDescripcion
            });

            colTalleId.AutoIncrement = true;
            colTalleId.AutoIncrementSeed = 1;
            dtTalle.PrimaryKey = new DataColumn[] { colTalleId };
            ds.Tables.Add(dtTalle);


            //TipoDeUbicacion
            DataTable dtTipoDeUbicacion = new DataTable();
            dtTipoDeUbicacion.TableName = "TipoDeUbicacion";

            DataColumn colTipoDeUbicacionId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colTipoDeUbicacionDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));

            dtTipoDeUbicacion.Columns.AddRange(new DataColumn[]
            {
                 colTipoDeUbicacionId,
                 colTipoDeUbicacionDescripcion
            });

            dtTipoDeUbicacion.PrimaryKey = new DataColumn[] { colTipoDeUbicacionId };
            ds.Tables.Add(dtTipoDeUbicacion);


            //Ubicacion
            DataTable dtUbicacion = new DataTable();
            dtUbicacion.TableName = "Ubicacion";

            DataColumn colUbicacionId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colUbicacionDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));
            DataColumn colUbicacionDireccion = new DataColumn("Direccion", Type.GetType("System.String"));
            DataColumn colUbicacionIdTipoDeUbicacion = new DataColumn("IdTipoDeUbicacion", Type.GetType("System.Int32"));
            DataColumn colUbicacionIdUbicacionPadre = new DataColumn("IdUbicacionPadre", Type.GetType("System.Int32"));
            DataColumn colUbicacionClienteExterno = new DataColumn("ClienteExterno", Type.GetType("System.Boolean"));
            DataColumn colUbicacionCapacidadTotal = new DataColumn("CapacidadTotal", Type.GetType("System.Decimal"));

            dtUbicacion.Columns.AddRange(new DataColumn[]
            {
                 colUbicacionId,
                 colUbicacionDescripcion,
                 colUbicacionDireccion,
                 colUbicacionIdTipoDeUbicacion,
                 colUbicacionIdUbicacionPadre,
                 colUbicacionClienteExterno,
                 colUbicacionCapacidadTotal
            });

            colUbicacionId.AutoIncrement = true;
            colUbicacionId.AutoIncrementSeed = 1;
            dtUbicacion.PrimaryKey = new DataColumn[] { colUbicacionId };
            ds.Tables.Add(dtUbicacion);

            DataRelation FKUbicacion_Usuario = new DataRelation(
                "FKUbicacion_Usuario",
                ds.Tables["Ubicacion"].Columns["Id"],
                ds.Tables["Usuario"].Columns["IdUbicacion"]);

            DataRelation FKTipoDeUbicacion_Ubicacion = new DataRelation(
                "FKTipoDeUbicacion_Ubicacion",
                ds.Tables["TipoDeUbicacion"].Columns["Id"],
                ds.Tables["Ubicacion"].Columns["IdTipoDeUbicacion"]);

            ds.Relations.Add(FKUbicacion_Usuario);
            ds.Relations.Add(FKTipoDeUbicacion_Ubicacion);

            //Articulo
            DataTable dtArticulo = new DataTable();
            dtArticulo.TableName = "Articulo";

            DataColumn colArticuloId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colArticuloCodigo = new DataColumn("Codigo", Type.GetType("System.String"));
            DataColumn colArticuloIdTipoDePrenda = new DataColumn("IdTipoDePrenda", Type.GetType("System.Int32"));
            DataColumn colArticuloIdColor = new DataColumn("IdColor", Type.GetType("System.Int32"));
            DataColumn colArticuloIdTalle = new DataColumn("IdTalle", Type.GetType("System.Int32"));
            DataColumn colArticuloPrecioUnitario = new DataColumn("PrecioUnitario", Type.GetType("System.Decimal"));
            DataColumn colArticuloPesoUnitario = new DataColumn("PesoUnitario", Type.GetType("System.Decimal"));


            dtArticulo.Columns.AddRange(new DataColumn[]
            {
                 colArticuloId,
                 colArticuloCodigo,
                 colArticuloIdTipoDePrenda,
                 colArticuloIdColor,
                 colArticuloIdTalle,
                 colArticuloPrecioUnitario,
                 colArticuloPesoUnitario
            });

            colArticuloId.AutoIncrement = true;
            colArticuloId.AutoIncrementSeed = 1;
            dtArticulo.PrimaryKey = new DataColumn[] { colArticuloId };
            ds.Tables.Add(dtArticulo);

            DataRelation FKTipoDePrenda_Articulo = new DataRelation(
                "FKTipoDePrenda_Articulo",
                ds.Tables["TipoDePrenda"].Columns["Id"],
                ds.Tables["Articulo"].Columns["IdTipoDePrenda"]);

            DataRelation FKColor_Articulo = new DataRelation(
                "FKColor_Articulo",
                ds.Tables["Color"].Columns["Id"],
                ds.Tables["Articulo"].Columns["IdColor"]);

            DataRelation FKTalle_Articulo = new DataRelation(
                "FKTalle_Articulo",
                ds.Tables["Talle"].Columns["Id"],
                ds.Tables["Articulo"].Columns["IdTalle"]);

            ds.Relations.Add(FKTipoDePrenda_Articulo);
            ds.Relations.Add(FKColor_Articulo);
            ds.Relations.Add(FKTalle_Articulo);


            //Stock
            DataTable dtStock = new DataTable();
            dtStock.TableName = "Stock";

            DataColumn colStockId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colStockCantidad = new DataColumn("Cantidad", Type.GetType("System.Int32"));
            DataColumn colStockIdUbicacion = new DataColumn("IdUbicacion", Type.GetType("System.Int32"));
            DataColumn colStockIdArticulo = new DataColumn("IdArticulo", Type.GetType("System.Int32"));

            dtStock.Columns.AddRange(new DataColumn[]
            {
                 colStockId,
                 colStockCantidad,
                 colStockIdUbicacion,
                 colStockIdArticulo
            });

            colStockId.AutoIncrement = true;
            colStockId.AutoIncrementSeed = 1;
            dtStock.PrimaryKey = new DataColumn[] { colStockId };
            ds.Tables.Add(dtStock);

            DataRelation FKUbicacion_Stock = new DataRelation(
                "FKUbicacion_Stock",
                ds.Tables["Ubicacion"].Columns["Id"],
                ds.Tables["Stock"].Columns["IdUbicacion"]);

            DataRelation FKArticulo_Stock = new DataRelation(
                "FKArticulo_Stock",
                ds.Tables["Articulo"].Columns["Id"],
                ds.Tables["Stock"].Columns["IdArticulo"]);

            ds.Relations.Add(FKUbicacion_Stock);
            ds.Relations.Add(FKArticulo_Stock);


            //Auditoria
            DataTable dtAuditoria = new DataTable();
            dtAuditoria.TableName = "Auditoria";

            DataColumn colAuditoriaId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colAuditoriaIdUbicacion = new DataColumn("IdUbicacion", Type.GetType("System.Int32"));
            DataColumn colAuditoriaIdUsuario = new DataColumn("IdUsuario", Type.GetType("System.Int32"));
            DataColumn colAuditoriaFechaCreacion = new DataColumn("FechaCreacion", Type.GetType("System.DateTime"));

            dtAuditoria.Columns.AddRange(new DataColumn[]
            {
                 colAuditoriaId,
                 colAuditoriaIdUbicacion,
                 colAuditoriaIdUsuario,
                 colAuditoriaFechaCreacion
            });

            colAuditoriaId.AutoIncrement = true;
            colAuditoriaId.AutoIncrementSeed = 1;
            dtAuditoria.PrimaryKey = new DataColumn[] { colAuditoriaId };

            ds.Tables.Add(dtAuditoria);

            DataRelation FKUbicacion_Auditoria = new DataRelation(
                "FKUbicacion_Auditoria",
                ds.Tables["Ubicacion"].Columns["Id"],
                ds.Tables["Auditoria"].Columns["IdUbicacion"]);

            DataRelation FKUsuario_Auditoria = new DataRelation(
                "FKUsuario_Auditoria",
                ds.Tables["Usuario"].Columns["Id"],
                ds.Tables["Auditoria"].Columns["IdUsuario"]);

            ds.Relations.Add(FKUbicacion_Auditoria);
            ds.Relations.Add(FKUsuario_Auditoria);


            //AuditoriaDetalle
            DataTable dtAuditoriaDetalle = new DataTable();
            dtAuditoriaDetalle.TableName = "AuditoriaDetalle";

            DataColumn colAuditoriaDetalleCantidad = new DataColumn("Cantidad", Type.GetType("System.Int32"));
            DataColumn colAuditoriaDetalleIdAuditoria = new DataColumn("IdAuditoria", Type.GetType("System.Int32"));
            DataColumn colAuditoriaDetalleIdArticulo = new DataColumn("IdArticulo", Type.GetType("System.Int32"));

            dtAuditoriaDetalle.Columns.AddRange(new DataColumn[]
            {
                 colAuditoriaDetalleCantidad,
                 colAuditoriaDetalleIdAuditoria,
                 colAuditoriaDetalleIdArticulo
            });
            dtAuditoriaDetalle.PrimaryKey = new DataColumn[] { colAuditoriaDetalleIdAuditoria, colAuditoriaDetalleIdArticulo };
            ds.Tables.Add(dtAuditoriaDetalle);

            DataRelation FKAuditoria_AuditoriaDetalle = new DataRelation(
                "FKAuditoria_AuditoriaDetalle",
                ds.Tables["Auditoria"].Columns["Id"],
                ds.Tables["AuditoriaDetalle"].Columns["IdAuditoria"]);

            DataRelation FKArticulo_AuditoriaDetalle = new DataRelation(
                "FKArticulo_AuditoriaDetalle",
                ds.Tables["Articulo"].Columns["Id"],
                ds.Tables["AuditoriaDetalle"].Columns["IdArticulo"]);

            ds.Relations.Add(FKAuditoria_AuditoriaDetalle);
            ds.Relations.Add(FKArticulo_AuditoriaDetalle);


            //EstadoHojaDeRuta
            DataTable dtEstadoHojaDeRuta = new DataTable();
            dtEstadoHojaDeRuta.TableName = "EstadoHojaDeRuta";

            DataColumn colEstadoHojaDeRutaId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colEstadoHojaDeRutaDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));

            dtEstadoHojaDeRuta.Columns.AddRange(new DataColumn[]
            {
                  colEstadoHojaDeRutaId,
                  colEstadoHojaDeRutaDescripcion
            });

            colEstadoHojaDeRutaId.AutoIncrement = true;
            colEstadoHojaDeRutaId.AutoIncrementSeed = 1;
            dtEstadoHojaDeRuta.PrimaryKey = new DataColumn[] { colEstadoHojaDeRutaId };

            ds.Tables.Add(dtEstadoHojaDeRuta);


            //HojaDeRuta
            DataTable dtHojaDeRuta = new DataTable();
            dtHojaDeRuta.TableName = "HojaDeRuta";

            DataColumn colHojaDeRutaId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colHojaDeRutaIdEstado = new DataColumn("IdEstado", Type.GetType("System.Int32"));
            DataColumn colHojaDeRutaIdUsuario = new DataColumn("IdUsuario", Type.GetType("System.Int32"));
            DataColumn colHojaDeRutaFechaCreacion = new DataColumn("FechaCreacion", Type.GetType("System.DateTime"));

            dtHojaDeRuta.Columns.AddRange(new DataColumn[]
            {
                 colHojaDeRutaId,
                 colHojaDeRutaIdEstado,
                 colHojaDeRutaIdUsuario,
                 colHojaDeRutaFechaCreacion
            });

            colHojaDeRutaId.AutoIncrement = true;
            colHojaDeRutaId.AutoIncrementSeed = 1;
            dtHojaDeRuta.PrimaryKey = new DataColumn[] { colHojaDeRutaId };

            ds.Tables.Add(dtHojaDeRuta);

            DataRelation FKUsuario_HojaDeRuta = new DataRelation(
                "FKUsuario_HojaDeRuta",
                ds.Tables["Usuario"].Columns["Id"],
                ds.Tables["HojaDeRuta"].Columns["IdUsuario"]);

            DataRelation FKEstado_HojaDeRuta = new DataRelation(
                "FKEstado_HojaDeRuta",
                ds.Tables["EstadoHojaDeRuta"].Columns["Id"],
                ds.Tables["HojaDeRuta"].Columns["IdEstado"]);

            ds.Relations.Add(FKUsuario_HojaDeRuta);
            ds.Relations.Add(FKEstado_HojaDeRuta);
            

            //EstadoEnvio
            DataTable dtEstadoEnvio = new DataTable();
            dtEstadoEnvio.TableName = "EstadoEnvio";

            DataColumn colEstadoEnvioId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colEstadoEnvioDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));

            dtEstadoEnvio.Columns.AddRange(new DataColumn[]
            {
                  colEstadoEnvioId,
                  colEstadoEnvioDescripcion
            });

            colEstadoEnvioId.AutoIncrement = true;
            colEstadoEnvioId.AutoIncrementSeed = 1;
            dtEstadoEnvio.PrimaryKey = new DataColumn[] { colEstadoEnvioId };

            ds.Tables.Add(dtEstadoEnvio);


            //Envio
            DataTable dtEnvio = new DataTable();
            dtEnvio.TableName = "Envio";

            DataColumn colEnvioId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colEnvioIdUbicacionOrigen = new DataColumn("IdUbicacionOrigen", Type.GetType("System.Int32"));
            DataColumn colEnvioIdUbicacionDestino = new DataColumn("IdUbicacionDestino", Type.GetType("System.Int32"));
            DataColumn colEnvioIdEstado = new DataColumn("IdEstado", Type.GetType("System.Int32"));
            DataColumn colEnvioIdUsuario = new DataColumn("IdUsuario", Type.GetType("System.Int32"));
            DataColumn colEnvioIdHojaDeRuta = new DataColumn("IdHojaDeRuta", Type.GetType("System.Int32"));
            DataColumn colEnvioFechaCreacion = new DataColumn("FechaCreacion", Type.GetType("System.DateTime"));
            DataColumn colEnvioFechaEnvio = new DataColumn("FechaEnvio", Type.GetType("System.DateTime"));
            DataColumn colEnvioFechaRecepcion = new DataColumn("FechaRecepcion", Type.GetType("System.DateTime"));

            dtEnvio.Columns.AddRange(new DataColumn[]
            {
                 colEnvioId,
                 colEnvioIdUbicacionOrigen,
                 colEnvioIdUbicacionDestino,
                 colEnvioIdEstado,
                 colEnvioIdUsuario,
                 colEnvioIdHojaDeRuta,
                 colEnvioFechaCreacion,
                 colEnvioFechaEnvio,
                 colEnvioFechaRecepcion
            });

            colEnvioId.AutoIncrement = true;
            colEnvioId.AutoIncrementSeed = 1;
            dtEnvio.PrimaryKey = new DataColumn[] { colEnvioId };

            ds.Tables.Add(dtEnvio);

            DataRelation FKUbicacionOrigen_Envio = new DataRelation(
                "FKUbicacionOrigen_Envio",
                ds.Tables["Ubicacion"].Columns["Id"],
                ds.Tables["Envio"].Columns["IdUbicacionOrigen"]);

            DataRelation FKUbicacionDestino_Envio = new DataRelation(
                "FKUbicacionDestino_Envio",
                ds.Tables["Ubicacion"].Columns["Id"],
                ds.Tables["Envio"].Columns["IdUbicacionDestino"]);

            DataRelation FKUsuario_Envio = new DataRelation(
                "FKUsuario_Envio",
                ds.Tables["Usuario"].Columns["Id"],
                ds.Tables["Envio"].Columns["IdUsuario"]);

            DataRelation FKEstado_Envio = new DataRelation(
                "FKEstado_Envio",
                ds.Tables["EstadoEnvio"].Columns["Id"],
                ds.Tables["Envio"].Columns["IdEstado"]);

            DataRelation FKHojaDeRuta_Envio = new DataRelation(
                "FKHojaDeRuta_Envio",
                ds.Tables["HojaDeRuta"].Columns["Id"],
                ds.Tables["Envio"].Columns["IdHojaDeRuta"]);

            ds.Relations.Add(FKUbicacionOrigen_Envio);
            ds.Relations.Add(FKUbicacionDestino_Envio);
            ds.Relations.Add(FKUsuario_Envio);
            ds.Relations.Add(FKEstado_Envio);
            ds.Relations.Add(FKHojaDeRuta_Envio);


            //EnvioDetalle
            DataTable dtEnvioDetalle = new DataTable();
            dtEnvioDetalle.TableName = "EnvioDetalle";

            DataColumn colEnvioDetalleCantidad = new DataColumn("Cantidad", Type.GetType("System.Int32"));
            DataColumn colEnvioDetalleIdEnvio = new DataColumn("IdEnvio", Type.GetType("System.Int32"));
            DataColumn colEnvioDetalleIdArticulo = new DataColumn("IdArticulo", Type.GetType("System.Int32"));
            DataColumn colEnvioDetallePrecioUnitario = new DataColumn("PrecioUnitario", Type.GetType("System.Decimal"));

            dtEnvioDetalle.Columns.AddRange(new DataColumn[]
            {
                 colEnvioDetalleCantidad,
                 colEnvioDetalleIdEnvio,
                 colEnvioDetalleIdArticulo,
                 colEnvioDetallePrecioUnitario
            });
            dtEnvioDetalle.PrimaryKey = new DataColumn[] { colEnvioDetalleIdEnvio, colEnvioDetalleIdArticulo };
            ds.Tables.Add(dtEnvioDetalle);

            DataRelation FKEnvio_EnvioDetalle = new DataRelation(
                "FKEnvio_EnvioDetalle",
                ds.Tables["Envio"].Columns["Id"],
                ds.Tables["EnvioDetalle"].Columns["IdEnvio"]);

            DataRelation FKArticulo_EnvioDetalle = new DataRelation(
                "FKArticulo_EnvioDetalle",
                ds.Tables["Articulo"].Columns["Id"],
                ds.Tables["EnvioDetalle"].Columns["IdArticulo"]);

            ds.Relations.Add(FKEnvio_EnvioDetalle);
            ds.Relations.Add(FKArticulo_EnvioDetalle);


            //Recepcion
            DataTable dtRecepcion = new DataTable();
            dtRecepcion.TableName = "Recepcion";

            DataColumn colRecepcionId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colRecepcionIdUbicacionOrigen = new DataColumn("IdUbicacionOrigen", Type.GetType("System.Int32"));
            DataColumn colRecepcionIdUbicacionDestino = new DataColumn("IdUbicacionDestino", Type.GetType("System.Int32"));
            DataColumn colRecepcionIdUsuario = new DataColumn("IdUsuario", Type.GetType("System.Int32"));
            DataColumn colRecepcionIdHojaDeRuta = new DataColumn("IdHojaDeRuta", Type.GetType("System.Int32"));
            DataColumn colRecepcionFechaCreacion = new DataColumn("FechaCreacion", Type.GetType("System.DateTime"));

            dtRecepcion.Columns.AddRange(new DataColumn[]
            {
                 colRecepcionId,
                 colRecepcionIdUbicacionOrigen,
                 colRecepcionIdUbicacionDestino,
                 colRecepcionIdUsuario,
                 colRecepcionIdHojaDeRuta,
                 colRecepcionFechaCreacion
            });

            colRecepcionId.AutoIncrement = true;
            colRecepcionId.AutoIncrementSeed = 1;
            dtRecepcion.PrimaryKey = new DataColumn[] { colRecepcionId };

            ds.Tables.Add(dtRecepcion);

            DataRelation FKUbicacionOrigen_Recepcion = new DataRelation(
                "FKUbicacionOrigen_Recepcion",
                ds.Tables["Ubicacion"].Columns["Id"],
                ds.Tables["Recepcion"].Columns["IdUbicacionOrigen"]);

            DataRelation FKUbicacionDestino_Recepcion = new DataRelation(
                "FKUbicacionDestino_Recepcion",
                ds.Tables["Ubicacion"].Columns["Id"],
                ds.Tables["Recepcion"].Columns["IdUbicacionDestino"]);

            DataRelation FKUsuario_Recepcion = new DataRelation(
                "FKUsuario_Recepcion",
                ds.Tables["Usuario"].Columns["Id"],
                ds.Tables["Recepcion"].Columns["IdUsuario"]);

            DataRelation FKHojaDeRuta_Recepcion = new DataRelation(
                "FKHojaDeRuta_Recepcion",
                ds.Tables["HojaDeRuta"].Columns["Id"],
                ds.Tables["Recepcion"].Columns["IdHojaDeRuta"]);

            ds.Relations.Add(FKUbicacionOrigen_Recepcion);
            ds.Relations.Add(FKUbicacionDestino_Recepcion);
            ds.Relations.Add(FKUsuario_Recepcion);
            ds.Relations.Add(FKHojaDeRuta_Recepcion);


            //RecepcionDetalle
            DataTable dtRecepcionDetalle = new DataTable();
            dtRecepcionDetalle.TableName = "RecepcionDetalle";

            DataColumn colRecepcionDetalleCantidadARecibir = new DataColumn("CantidadARecibir", Type.GetType("System.Int32"));
            DataColumn colRecepcionDetalleCantidad = new DataColumn("Cantidad", Type.GetType("System.Int32"));
            DataColumn colRecepcionDetalleIdRecepcion = new DataColumn("IdRecepcion", Type.GetType("System.Int32"));
            DataColumn colRecepcionDetalleIdArticulo = new DataColumn("IdArticulo", Type.GetType("System.Int32"));
            DataColumn colRecepcionDetallePrecioUnitario = new DataColumn("PrecioUnitario", Type.GetType("System.Decimal"));


            dtRecepcionDetalle.Columns.AddRange(new DataColumn[]
            {
                 colRecepcionDetalleCantidadARecibir,
                 colRecepcionDetalleCantidad,
                 colRecepcionDetalleIdRecepcion,
                 colRecepcionDetalleIdArticulo,
                 colRecepcionDetallePrecioUnitario
            });
            dtRecepcionDetalle.PrimaryKey = new DataColumn[] { colRecepcionDetalleIdRecepcion, colRecepcionDetalleIdArticulo };
            ds.Tables.Add(dtRecepcionDetalle);

            DataRelation FKRecepcion_RecepcionDetalle = new DataRelation(
                "FKRecepcion_RecepcionDetalle",
                ds.Tables["Recepcion"].Columns["Id"],
                ds.Tables["RecepcionDetalle"].Columns["IdRecepcion"]);

            DataRelation FKArticulo_RecepcionDetalle = new DataRelation(
                "FKArticulo_RecepcionDetalle",
                ds.Tables["Articulo"].Columns["Id"],
                ds.Tables["RecepcionDetalle"].Columns["IdArticulo"]);

            ds.Relations.Add(FKRecepcion_RecepcionDetalle);
            ds.Relations.Add(FKArticulo_RecepcionDetalle);


            //AjusteStock
            DataTable dtAjusteStock = new DataTable();
            dtAjusteStock.TableName = "AjusteStock";

            DataColumn colAjusteStockId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colAjusteStockNuevaCantidad = new DataColumn("NuevaCantidad", Type.GetType("System.Int32"));
            DataColumn colAjusteStockCantidadPrevia = new DataColumn("CantidadPrevia", Type.GetType("System.Int32"));
            DataColumn colAjusteStockIdUbicacion = new DataColumn("IdUbicacion", Type.GetType("System.Int32"));
            DataColumn colAjusteStockIdArticulo = new DataColumn("IdArticulo", Type.GetType("System.Int32"));
            DataColumn colAjusteStockFechaCreacion = new DataColumn("FechaCreacion", Type.GetType("System.DateTime"));
            DataColumn colAjusteStockIdUsuario = new DataColumn("IdUsuario", Type.GetType("System.Int32"));
            DataColumn colAjusteStockObservaciones = new DataColumn("Observaciones", Type.GetType("System.String"));

            dtAjusteStock.Columns.AddRange(new DataColumn[]
            {
                 colAjusteStockId,
                 colAjusteStockNuevaCantidad,
                 colAjusteStockCantidadPrevia,
                 colAjusteStockIdUbicacion,
                 colAjusteStockIdArticulo,
                 colAjusteStockFechaCreacion,
                 colAjusteStockIdUsuario,
                 colAjusteStockObservaciones
            });

            colAjusteStockId.AutoIncrement = true;
            colAjusteStockId.AutoIncrementSeed = 1;
            dtAjusteStock.PrimaryKey = new DataColumn[] { colAjusteStockId };
            ds.Tables.Add(dtAjusteStock);

            DataRelation FKUsuario_AjusteStock = new DataRelation(
                "FKUsuario_AjusteStock",
                ds.Tables["Usuario"].Columns["Id"],
                ds.Tables["AjusteStock"].Columns["IdUsuario"]);

            DataRelation FKUbicacion_AjusteStock = new DataRelation(
                "FKUbicacion_AjusteStock",
                ds.Tables["Ubicacion"].Columns["Id"],
                ds.Tables["AjusteStock"].Columns["IdUbicacion"]);

            DataRelation FKArticulo_AjusteStock = new DataRelation(
                "FKArticulo_AjusteStock",
                ds.Tables["Articulo"].Columns["Id"],
                ds.Tables["AjusteStock"].Columns["IdArticulo"]);

            ds.Relations.Add(FKUsuario_AjusteStock);
            ds.Relations.Add(FKUbicacion_AjusteStock);
            ds.Relations.Add(FKArticulo_AjusteStock);


            //BultoCompuesto
            DataTable dtBultoCompuesto = new DataTable();
            dtBultoCompuesto.TableName = "BultoCompuesto";

            DataColumn colBultoCompuestoId = new DataColumn("Id", Type.GetType("System.Int32"));
            DataColumn colBultoCompuestoDescripcion = new DataColumn("Descripcion", Type.GetType("System.String"));

            dtBultoCompuesto.Columns.AddRange(new DataColumn[]
            {
                 colBultoCompuestoId,
                 colBultoCompuestoDescripcion
            });

            colBultoCompuestoId.AutoIncrement = true;
            colBultoCompuestoId.AutoIncrementSeed = 1;
            dtBultoCompuesto.PrimaryKey = new DataColumn[] { colBultoCompuestoId };
            ds.Tables.Add(dtBultoCompuesto);


            //BultoCompuestoDetalle
            DataTable dtBultoCompuestoDetalle = new DataTable();
            dtBultoCompuestoDetalle.TableName = "BultoCompuestoDetalle";

            DataColumn colBultoCompuestoDetalleIdBultoCompuesto = new DataColumn("IdBultoCompuesto", Type.GetType("System.Int32"));
            DataColumn colBultoCompuestoDetalleIdTipoDePrenda = new DataColumn("IdTipoDePrenda", Type.GetType("System.Int32"));

            dtBultoCompuestoDetalle.Columns.AddRange(new DataColumn[]
            {
                 colBultoCompuestoDetalleIdBultoCompuesto,
                 colBultoCompuestoDetalleIdTipoDePrenda
            });

            dtBultoCompuestoDetalle.PrimaryKey = new DataColumn[] { colBultoCompuestoDetalleIdBultoCompuesto, colBultoCompuestoDetalleIdTipoDePrenda };
            ds.Tables.Add(dtBultoCompuestoDetalle);

            DataRelation FKBultoCompuesto_BultoCompuestoDetalle = new DataRelation(
                "FKBultoCompuesto_BultoCompuestoDetalle",
                ds.Tables["BultoCompuesto"].Columns["Id"],
                ds.Tables["BultoCompuestoDetalle"].Columns["IdBultoCompuesto"]);

            DataRelation FKTipoDePrenda_BultoCompuestoDetalle = new DataRelation(
                "FKTipoDePrenda_BultoCompuestoDetalle",
                ds.Tables["TipoDePrenda"].Columns["Id"],
                ds.Tables["BultoCompuestoDetalle"].Columns["IdTipoDePrenda"]);

            ds.Relations.Add(FKBultoCompuesto_BultoCompuestoDetalle);
            ds.Relations.Add(FKTipoDePrenda_BultoCompuestoDetalle);

        }
    }
}
