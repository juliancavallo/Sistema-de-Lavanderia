using System;
using System.Collections.Generic;
using System.Text;
using Entidades;

namespace Servicios
{
    public class ServicioSeguridad
    {
        Random rand = new Random();

        public string Encriptar(string texto)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(texto);
            return Convert.ToBase64String(bytes);
        }

        public string Desencriptar(string texto)
        {
            byte[] bytes = Convert.FromBase64String(texto);
            return Encoding.Unicode.GetString(bytes);
        }

        public string GenerarContraseñaAleatoria(int tamaño)
        {
            string abecedario =
            "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            
            char[] chars = new char[tamaño];
            for (int i = 0; i < tamaño; i++)
            {
                chars[i] = abecedario[rand.Next(abecedario.Length)];
            }
            return new string(chars);
        }

    }
}
