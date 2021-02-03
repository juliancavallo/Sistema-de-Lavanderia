using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Servicios;

namespace DAL
{
    public class AccesoArchivo
    {
        static DataSet ds = new DataSet();
        ServicioTablas servicioTablas = new ServicioTablas();

        string path = Environment.CurrentDirectory + @"\Archivo.xml";

        public bool ArchivoExiste()
        {
            return File.Exists(path);
        }

        public void GenerarDataSet()
        {
            servicioTablas.CrearDataSet(ds);
            ds.WriteXml(path, XmlWriteMode.WriteSchema);
        }

        public void CargarDataSet()
        {
            if (ds.Tables.Count == 0)
                ds.ReadXml(path);
        }

        public DataTable ObtenerTabla(string nombre)
        {
            return ds.Tables[nombre];
        }

        public void ActualizarDataSet()
        {
            ds.WriteXml(path, XmlWriteMode.WriteSchema);
        }

        public void HacerBackup()
        {
            string carpeta = Environment.CurrentDirectory + @"\Backups";
            string nombreBackup = carpeta + @"\Archivo" + DateTime.Now.ToString("ddMMyyyyHHmmssffff") + ".xml";

            //crear carpeta Backups en Environment.CurrentDirectory si no existe
            Directory.CreateDirectory(carpeta);

            //obtener los archivos ordenados por fecha de creacion
            var archivos = new DirectoryInfo(carpeta).GetFiles().OrderBy(x => x.CreationTime).ToList();

            //si hay 15 archivos o mas, eliminar desde el mas viejo hasta que queden 15
            if (archivos.Count() >= 15)
            {
                var archivosAux = archivos.ToList();
                for (int i = 0; i < archivosAux.Count() - 14; i++)
                {
                    File.Delete(archivos.FirstOrDefault().FullName);
                    archivos.Remove(archivos.FirstOrDefault());
                }
            }

            //copiar el xml de ubicado en path y pegarlo en esta nueva carpeta, agregandole el sufijo ddMMyyyyHHmmSS
            File.Copy(path, nombreBackup);

        }
    }
}
