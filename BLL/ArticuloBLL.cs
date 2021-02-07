using BLL.Vistas;
using Entidades;
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
        BultoCompuestoBLL bultoCompuestoBLL = new BultoCompuestoBLL();

        public void Alta(Articulo obj)
        {
            try
            {
                if (ObtenerTodos().Any(x => x.Codigo == obj.Codigo))
                    throw new Exception("Ya existe un articulo con el mismo codigo");

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
                    PrecioUnitario = x.PrecioUnitario
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

        public bool ValidarBultosCompuestos(DataGridView dgv)
        {
            var result = true;
            var articulos = new List<Articulo>();
            foreach (DataGridViewRow item in dgv.Rows)
            {
                int idArticulo = int.Parse(item.Cells["IdArticulo"].Value.ToString());
                articulos.Add(this.Obtener(idArticulo));
            }

            var articulosCompuestos = articulos.Where(x => x.TipoDePrenda.Categoria.EsCompuesta).ToList();
            foreach (var articuloCompuesto in articulosCompuestos)
            {
                var bultos = bultoCompuestoBLL.ObtenerTodos(articuloCompuesto.TipoDePrenda.Id);

                if (bultos.Count > 0)
                {
                    foreach (var bulto in bultos)
                    {
                        var detalleComplementario = bulto.Detalle.Select(x => x.TipoDePrenda)
                            .Where(x => x.Id != articuloCompuesto.TipoDePrenda.Id).ToList();

                        foreach (var tipoDePrenda in detalleComplementario)
                        {
                            if (!articulosCompuestos.Select(x => x.TipoDePrenda.Id).Contains(tipoDePrenda.Id))
                                return false;
                        }
                    }
                }
                else
                    return false;
            }

            return result;
        }

        #endregion
    }
}
