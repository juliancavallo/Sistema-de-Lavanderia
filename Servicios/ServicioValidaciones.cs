using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Servicios
{
    public static class ServicioValidaciones
    {
        public static bool TextoCompleto(TextBox txt, string nombreControl)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
                throw new Exception(string.Format("El campo {0} es obligatorio", nombreControl));

            return true;
        }

        public static bool EstadoCheckbox(CheckBox chk)
        {
            return chk.Checked;
        }

        public static bool FormatoNumericoValido(TextBox txt, string nombreControl)
        {
            int result;
            int.TryParse(txt.Text, out result);

            if (result < 1)
                throw new Exception(string.Format("El campo {0} debe ser numerico y mayor a 0", nombreControl));

            return true;
        }

        public static bool FormatoDecimalValido(TextBox txt, string nombreControl)
        {
            decimal result;
            decimal.TryParse(txt.Text.Replace('.',','), out result);

            if (result < 0)
                throw new Exception(string.Format("El campo {0} debe ser un número decimal mayor a 0", nombreControl));

            return true;
        }

        public static bool ItemSeleccionado(ComboBox combo, string nombreControl)
        {
            if(combo.SelectedItem == null)
                throw new Exception(string.Format("Debe seleccionar un item de la lista {0}", nombreControl));

            return true;
        }

        public static bool ItemObligatorioSeleccionado(ComboBox combo, string nombreControl)
        {
            if (combo.SelectedItem == null || combo.SelectedIndex == 0)
                throw new Exception(string.Format("Debe seleccionar un item de la lista {0}", nombreControl));

            return true;
        }

        public static bool NodoHijoSelecciondo(TreeView treeView, string nombreControl)
        {
            foreach (TreeNode nodoPadre in treeView.Nodes)
            {
                foreach(TreeNode nodoHijo in nodoPadre.Nodes)
                {
                    if (nodoHijo.Checked)
                        return true;
                }
            }

            throw new Exception(string.Format("Debe seleccionar al menos un nodo hijo de {0}", nombreControl));
        }

        public static bool CorreoValido(TextBox txt, string nombreControl)
        {
            string mensajeError = string.Format("El campo {0} debe tener un formato de correo electronico valido", nombreControl);
            try
            {
                var mail = new System.Net.Mail.MailAddress(txt.Text);
                if (mail.Address == txt.Text)
                    return true;
                else
                    throw new Exception(mensajeError);
            }
            catch
            {
                throw new Exception(mensajeError);
            }
        }

        public static bool GrillaConDatos(DataGridView grid)
        {
            if (grid.Rows.Count == 0)
                throw new Exception("Debe existir al menos un registro en la grilla");

            return true;
        }

    }
}
