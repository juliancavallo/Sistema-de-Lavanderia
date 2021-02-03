using System;
using System.Collections.Generic;

namespace Servicios
{
    public interface IProcesable<T>
    {
        void Alta(T obj);
        T Obtener(int id);
        List<T> ObtenerTodos();
    }
}
