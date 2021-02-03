using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Servicios
{
    public static class ServicioConfiguracionDeControles
    {
        public static void ConfigurarHijoMDI(Form frmHijo, Form frmPadre)
        {
            frmHijo.MdiParent = frmPadre;
            frmHijo.Dock = DockStyle.Fill;
            frmHijo.WindowState = FormWindowState.Maximized;
            frmHijo.FormBorderStyle = FormBorderStyle.None;
            frmHijo.ControlBox = false;
            frmHijo.ShowIcon = false;
            frmHijo.ShowInTaskbar = false;
            frmHijo.Controls.Find("lblTitulo", true)[0].Text = frmHijo.Text;

        }
        public static void ConfigurarFormDialogo(Form frm)
        {
            frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            frm.MaximizeBox = false;
            frm.StartPosition = FormStartPosition.CenterScreen;
        }
        public static void ConfigurarFormDetalle(Form frm)
        {
            frm.Controls.Find("lblTitulo", true)[0].Text = frm.Text;
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm.StartPosition = FormStartPosition.CenterScreen;
        }
        public static void ConfigurarGrilla(DataGridView dgv)
        {
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ReadOnly = true;
            
            foreach(DataGridViewColumn column in dgv.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        public static void OcultarColumnasEnGrilla(DataGridView dgv, params string[] columnas)
        {
            foreach(var columna in columnas)
            {
                dgv.Columns[columna].Visible = false;
            }
        }
        public static void ConfigurarTextBoxContraseña(TextBox textBox)
        {
            textBox.UseSystemPasswordChar = true;
        }
        public static void LimpiarControles(List<Control> controles)
        {
            foreach(Control ctrl in controles)
            {
                if (ctrl is TextBox)
                    (ctrl as TextBox).Clear();

                if (ctrl is CheckBox)
                    (ctrl as CheckBox).Checked = false;

                if (ctrl is CheckedListBox)
                {
                    var chckListBox = ctrl as CheckedListBox;
                    for (int i = 0; i < chckListBox.Items.Count; i++)
                    {
                        chckListBox.SetItemChecked(i, false);
                    }
                }

                if(ctrl is TreeView)
                {
                    var treeView = ctrl as TreeView;
                    treeView.Nodes.Clear();
                }

                if (ctrl is ComboBox && ((ComboBox)ctrl).Enabled )
                {
                    var combo = ctrl as ComboBox;
                    if (combo.Items.Count > 0)
                        combo.SelectedIndex = 0;

                }
            }
        }
        public static int ConvertirAEntero(TextBox txt)
        {
            //Devuelve 0 si no se puede convertir
            int result;
            int.TryParse(txt.Text, out result);

            return result;
        }
        public static string ObtenerCampoSeleccionado(List<Control> controles, string nombreDeCampo, string nombreDeGrilla)
        {
            DataGridView dgv = (DataGridView)controles.Find(x => x.Name == nombreDeGrilla);
            if (dgv != null && dgv.CurrentRow != null)
            {
                if (dgv.CurrentRow.Cells[nombreDeCampo] == null)
                    throw new Exception("La columna " + nombreDeCampo + " no existe en la grila");

                return dgv.CurrentRow.Cells[nombreDeCampo].Value.ToString();
            }
            else
                throw new Exception("Debe seleccionar un registro de la grilla");
        }
        public static void AgregarItemDefault(ComboBox combo)
        {
            combo.Items.Add(new { Id = -1, Descripcion = "Seleccionar..." });
        }
    }
}
