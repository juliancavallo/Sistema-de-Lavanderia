using DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mappers
{
    public class MapperSeguridad
    {
        AccesoArchivo dal = new AccesoArchivo();
        public bool ArchivoExiste()
        {
            return dal.ArchivoExiste();
        }

        public void GenerarDataSet()
        {
            dal.GenerarDataSet();
        }

        public void CargarDataSet()
        {
            dal.CargarDataSet();
        }

        public void HacerBackup()
        {
            dal.HacerBackup();
        }
    }
}
