using Entidades;
using Mappers;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MenuBLL
    {
        MapperMenu mpp = new MapperMenu();

        public void Alta(Menu obj)
        {
            try
            {
                mpp.Alta(obj);
            }
            catch (Exception)
            {

                throw; 
            }
        }
        public void Alta(List<Menu> obj)
        {
            try
            {
                mpp.Alta(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Menu Obtener(int id)
        {
            return mpp.Obtener(id);
        }

        public Menu Obtener(string codigo)
        {
            return this.ObtenerTodos().Where(x => x.Codigo == codigo).FirstOrDefault();
        }

        public List<Menu> ObtenerTodos()
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

        public List<Menu> ObtenerHijos(int idMenuPadre)
        {
            try
            {
                return mpp.ObtenerTodos()
                    .Where(x => x is MenuSimple && ((MenuSimple)x).MenuPadre.Id == idMenuPadre).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> ObtenerCompuestos()
        {
            try
            {
                return mpp.ObtenerTodos().Where(x => x is MenuCompuesto).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> ObtenerSimples()
        {
            try
            {
                return mpp.ObtenerTodos().Where(x => x is MenuSimple).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Menu> ObtenerNeto(List<Menu> menus)
        {
            try
            {
                var lista = new List<Menu>();

                foreach(var menu in menus.Where(x => x.Simbolo))
                {
                    lista.Add(menu);
                    if (menu is MenuCompuesto)
                    {
                        lista.AddRange(this.ObtenerHijos(menu.Id));
                    }
                }

                var menusParaEliminar = menus.Where(x => !x.Simbolo).Select(x => x.Id).ToList();
                
                return lista.Where(x => !menusParaEliminar.Contains(x.Id)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
