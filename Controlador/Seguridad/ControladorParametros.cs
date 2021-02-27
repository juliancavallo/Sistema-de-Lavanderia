using BLL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Controlador
{
    public class ControladorParametros
    {
        #region Variables globales
        List<Control> controles = new List<Control>();

        ParametroDelSistemaBLL parametroDelSistemaBLL = new ParametroDelSistemaBLL();

        #endregion

        #region Formulario
        public ControladorParametros(Form frm)
        {
            foreach (Control c in frm.Controls)
            {
                controles.Add(c);
                if (c.Controls.Count > 0)
                {
                    foreach (Control c2 in c.Controls)
                    {
                        controles.Add(c2);
                    }
                }
            }

            frm.Activated += FormLoad;
        }

        private void FormLoad(object sender, EventArgs e)
        {
            try
            {
                string capacidadHojaDeRuta = parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.CapacidadMaximaHojaDeRuta).Valor;
                string correoSoporte = parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.CorreoSoporte).Valor;
                string descuentoEnvios = parametroDelSistemaBLL.Obtener(Entidades.Enums.ParametroDelSistema.PorcentajeDescuentoDeEnvios).Valor;

                ((TextBox)controles.Find(x => x.Name == "txtCapacidadMaxima")).Text = capacidadHojaDeRuta + " Kg";
                ((TextBox)controles.Find(x => x.Name == "txtCorreoSoporte")).Text = correoSoporte;
                ((TextBox)controles.Find(x => x.Name == "txtDescuentoEnvios")).Text = descuentoEnvios + " %";
            }
            catch(Exception)
            {

            }
        }
        #endregion

        
    }
}
