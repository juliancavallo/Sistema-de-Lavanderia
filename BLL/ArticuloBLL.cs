using BLL.Vistas;
using Entidades;
using Entidades.Clases_Padre;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BLL
{
    public class ArticuloBLL : IAdministrable<Articulo>
    {
        MapperArticulo mpp = new MapperArticulo();
        MapperStock mppStock = new MapperStock();
        MapperAuditoria mppAuditoria = new MapperAuditoria();
        ParametroDelSistemaBLL parametroDelSistemaBLL = new ParametroDelSistemaBLL();
        BultoCompuestoBLL bultoCompuestoBLL = new BultoCompuestoBLL();

        public void Alta(Articulo obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Codigo == obj.Codigo))
                    throw new Exception("Ya existe un articulo con el mismo codigo");

                decimal capacidadMaxima = decimal.Parse(parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.CapacidadMaximaHojaDeRuta).Valor);
                if(obj.PesoUnitario > capacidadMaxima)
                    throw new Exception($"El peso del articulo supera la capacidad máxima que se puede enviar ({capacidadMaxima} kg)");

                mpp.Alta(obj);
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
                if (mppStock.ObtenerTodos().Where(x => x.Articulo.Id == obj.Id).Count() > 0)
                    throw new Exception("El artículo no se puede eliminar porque hay al menos una configuración de stock asociada al mismo");

                if (mppAuditoria.ObtenerTodos().Any(d => d.Detalle.Select(x => x.Articulo.Id).Contains(obj.Id)))
                    throw new Exception("El artículo no se puede eliminar porque hay al menos una auditoria asociada al mismo");

                mpp.Baja(obj);
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
                if (ObtenerTodos().Any(x => x.Codigo == obj.Codigo && x.Id != obj.Id))
                    throw new Exception("Ya existe un articulo con el mismo codigo");
                
                decimal capacidadMaxima = decimal.Parse(parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.CapacidadMaximaHojaDeRuta).Valor);
                if (obj.PesoUnitario > capacidadMaxima)
                    throw new Exception($"El peso del articulo supera la capacidad máxima que se puede enviar ({capacidadMaxima} kg)");

                mpp.Modificacion(obj);
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
                return mpp.Obtener(id);
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
                return mpp.ObtenerTodos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> ObtenerTodos(string codigo, int idTipoDePrenda, int idColor, int idTalle)
        {
            try
            {
                var lista = mpp.ObtenerTodos().AsEnumerable();

                if (!string.IsNullOrWhiteSpace(codigo))
                    lista = lista.Where(x => x.Codigo.ToLower().Contains(codigo.ToLower()));

                if (idTipoDePrenda > 0)
                    lista = lista.Where(x => x.TipoDePrenda.Id == idTipoDePrenda);

                if (idColor > 0)
                    lista = lista.Where(x => x.Color.Id == idColor);

                if (idTalle > 0)
                    lista = lista.Where(x => x.Talle.Id == idTalle);

                return lista.ToList(); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArticuloVista> ObtenerTodosParaVista(string codigo = "", int idTipoDePrenda = 0, int idColor = 0, int idTalle = 0)
        {
            try
            {
                return this.ObtenerTodos(codigo,idTipoDePrenda,idColor,idTalle).Select(x => 
                new ArticuloVista() 
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Color = x.Color.Descripcion,
                    Talle = x.Talle.Descripcion,
                    TipoDePrenda = x.TipoDePrenda.Descripcion,
                    PrecioUnitario = "$" + x.PrecioUnitario.ToString(),
                    PesoUnitario = x.PesoUnitario.ToString() + " Kg",

                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Validaciones
        public bool ValidarCortePorBulto(Articulo articulo, int cantidadIngresada)
        {
            if (articulo.TipoDePrenda.UsaCortePorBulto)
            {
                if (cantidadIngresada % articulo.TipoDePrenda.CortePorBulto != 0)
                    throw new Exception("No se respeta el corte por bulto de la prenda ingresada. El corte por bulto es " + articulo.TipoDePrenda.CortePorBulto);
            }
            return true;
        }

        public bool ValidarBultosCompuestos(List<Detalle> detalles)
        {
            var result = true;
            var articulos = detalles.Select(x => x.Articulo);


            var tiposDePrendaCompuestos = articulos.Select(x => x.TipoDePrenda).Where(x => x.Categoria.EsCompuesta).ToList();
            var bultosUsados = new List<BultoCompuesto>();
            tiposDePrendaCompuestos.ForEach(x => 
            {
                var bultoAAgregar = bultoCompuestoBLL.ObtenerPorTipoDePrenda(x.Id);
                if (!bultosUsados.Any(b => b.Id == bultoAAgregar.Id))
                    bultosUsados.Add(bultoAAgregar);
            });

            foreach(var bulto in bultosUsados) 
            {
                var cantidadDeBultos = new List<int>();
                bulto.Detalle.ForEach(detalle =>
                {
                    int cantidadTotal = detalles.Where(x => x.Articulo.TipoDePrenda.Id == detalle.TipoDePrenda.Id).Sum(x => x.Cantidad);
                    cantidadDeBultos.Add(cantidadTotal / detalle.TipoDePrenda.CortePorBulto);
                });

                if (cantidadDeBultos.Distinct().Count() > 1)
                    return false;
            };


            return result;
        }

        #endregion
        public List<Tuple<int, Articulo, int>> SepararDetallePorBulto(List<EnvioDetalle> envioDetalle)
        {
            //Id, Articulo, Corte por Bulto
            var detallePorBulto = this.ConvertirEntidadEnTupla(envioDetalle.Where(x => !x.Articulo.TipoDePrenda.Categoria.EsCompuesta).ToList());

            this.ReordenarBultosCompuestos(envioDetalle, detallePorBulto.LastOrDefault()?.Item1 ?? 0, detallePorBulto);

            return detallePorBulto;
        }

        public List<Tuple<int, Articulo, int>> ConvertirEntidadEnTupla(List<EnvioDetalle> detalle)
        {
            var detallePorBulto = new List<Tuple<int, Articulo, int>>(); //Id, Articulo, Corte por Bulto
            int id = 0;

            foreach (var item in detalle)
            {
                for (int i = 0; i < item.Cantidad; i += item.Articulo.TipoDePrenda.CortePorBulto)
                {
                    id++;
                    detallePorBulto.Add(new Tuple<int, Articulo, int>(id, item.Articulo, item.Articulo.TipoDePrenda.CortePorBulto));
                }
            }

            return detallePorBulto;
        }

        private void ReordenarBultosCompuestos(List<EnvioDetalle> envioDetalle, int id, List<Tuple<int, Articulo, int>> detallePorBulto)
        {

            //Ordenar las prendas compuestas de manera intercalada: CHAQUETA, PANTALON, CHAQUETA, PANTALON, en lugar de CHAQUETA, CHAQUETA, PANTALON, PANTALON
            var bultosCompuestosOriginal = envioDetalle.Where(x => x.Articulo.TipoDePrenda.Categoria.EsCompuesta);
            var bultosUsados = new List<BultoCompuesto>(); //Ambo

            bultosCompuestosOriginal
                .Select(x => x.Articulo.TipoDePrenda.Id)
                .ToList()
                .ForEach(x =>
                {
                    if (!bultosUsados.Contains(bultoCompuestoBLL.ObtenerPorTipoDePrenda(x)))
                        bultosUsados.Add(bultoCompuestoBLL.ObtenerPorTipoDePrenda(x));
                });

            //Se obtiene el primer id de tipo de prenda de cada bulto compuesto
            var idsParaComparar = bultosUsados.Select(x => x.Detalle.First().TipoDePrenda.Id).ToList();

            //En el detalle de envio original se dejan solo los primeros tipos de prenda de cada bulto compuesto
            var bultosCompuestosFiltrados = bultosCompuestosOriginal.Where(x => idsParaComparar.Contains(x.Articulo.TipoDePrenda.Id));
            var bultosCompuestosComplementarios = bultosCompuestosOriginal.Where(x => !idsParaComparar.Contains(x.Articulo.TipoDePrenda.Id)).ToList();
            var bultosCompuestosComplementariosPorBulto = this.ConvertirEntidadEnTupla(bultosCompuestosComplementarios);

            foreach (var detalle in bultosCompuestosFiltrados)
            {
                //Se hace i += CortePorbulto, porque 
                for (int i = 0; i < detalle.Cantidad; i += detalle.Articulo.TipoDePrenda.CortePorBulto)
                {
                    var bultoCompuesto = bultoCompuestoBLL.ObtenerPorTipoDePrenda(detalle.Articulo.TipoDePrenda.Id);

                    foreach (var itemCompuesto in bultoCompuesto.Detalle)
                    {
                        id++;

                        if (itemCompuesto.TipoDePrenda.Id == detalle.Articulo.TipoDePrenda.Id)
                        {
                            //Es la prenda principal
                            detallePorBulto.Add(new Tuple<int, Articulo, int>(id, detalle.Articulo, detalle.Articulo.TipoDePrenda.CortePorBulto));
                        }
                        else
                        {
                            //Es prenda complementaria
                            var elemento = bultosCompuestosComplementariosPorBulto
                                .FirstOrDefault(x => x.Item2.TipoDePrenda.Id == itemCompuesto.TipoDePrenda.Id);

                            //Se elimina el elemento porque se necesita tener el primer articulo disponible
                            //A medida que se van eliminando, se va obteniendo con First() el siguiente articulo de ese tipo de prenda
                            bultosCompuestosComplementariosPorBulto.Remove(elemento);

                            detallePorBulto.Add(new Tuple<int, Articulo, int>(id, elemento.Item2, elemento.Item2.TipoDePrenda.CortePorBulto));

                        }
                    }
                }
            }
        }

    }
}
