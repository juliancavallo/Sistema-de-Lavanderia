using System;
using System.Collections.Generic;

namespace Servicios
{
    public interface IAdministrable<T>
    {
        void Alta(T obj);
        void Baja(T obj);
        void Modificacion(T obj);
        T Obtener(int id);
        List<T> ObtenerTodos();
    }
}
