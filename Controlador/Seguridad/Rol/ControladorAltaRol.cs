using BLL;
using Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Servicios;
using System.Linq;
using System.Xml;

namespace Controlador
{
    public class ControladorAltaRol
    {
        #region Variables globales
        Form frm;
        List<Control> controles = new List<Control>();
        RolBLL rolBLL = new RolBLL();
        MenuBLL menuBLL = new MenuBLL();
        int? idRol = null;
        List<Entidades.Menu> menus = new List<Entidades.Menu>();
        #endregion

        #region Formulario
        public ControladorAltaRol(Form pForm)
        {
            ServicioConfiguracionDeControles.ConfigurarFormDialogo(pForm);
            pForm.Controls.Add(new TextBox() { Name = "IdRol", Visible = false });

            frm = pForm;

            foreach (Control control in frm.Controls)
            {
                controles.Add(control);
            }

            pForm.Load += FormLoad;
            pForm.FormClosed += FormClosed;
            ((Button)controles.Find(x => x.Name == "btnAceptar")).Click += ClickAceptar;
            ((Button)controles.Find(x => x.Name == "btnCancelar")).Click += ClickCancelar;
            ((TreeView)controles.Find(x => x.Name == "treeViewMenusCompuestos")).AfterCheck += AfterCheck;
        }


        private void FormLoad(object sender, EventArgs e)
        {
            var treeViewMenusCompuestos = (TreeView)controles.Find(x => x.Name == "treeViewMenusCompuestos");
            this.ConfigurarTreeView(treeViewMenusCompuestos);

            Control ctrlOculto = controles.Find(x => x.Name == "IdRol");

            if (int.TryParse(ctrlOculto.Text, out var result))
                idRol = result;

            if (idRol.HasValue)
            {
                var rol = rolBLL.Obtener(idRol.Value);

                if (rol != null)
                {
                    ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text = rol.Descripcion;

                    var menusDelRol = menuBLL.ObtenerNeto(rol.Menus).Select(r => r.Codigo).ToList();
                    foreach (TreeNode nodo in treeViewMenusCompuestos.Nodes)
                    {
                        nodo.Checked = menusDelRol.Contains(nodo.Tag.ToString());

                        foreach (TreeNode hijo in nodo.Nodes)
                        {
                            hijo.Checked = menusDelRol.Contains(hijo.Tag.ToString());
                        }
                    }
                }
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            ServicioConfiguracionDeControles.LimpiarControles(this.controles);
            idRol = null;
        }
        #endregion

        #region Eventos de controles

        private void ClickAceptar(object sender, EventArgs e)
        {
            try
            {
                if (DatosValidos())
                {
                    var rol = new Rol();
                    rol.Id = idRol ?? 0;
                    rol.Descripcion = ((TextBox)controles.Find(x => x.Name == "txtDescripcion")).Text;
                    rol.Menus = new List<Entidades.Menu>();

                    var treeViewMenusCompuestos = (TreeView)controles.Find(x => x.Name == "treeViewMenusCompuestos");
                    foreach(TreeNode nodoPadre in treeViewMenusCompuestos.Nodes)
                    {
                        if(nodoPadre.Checked)
                        {
                            var menu = menuBLL.Obtener(nodoPadre.Tag.ToString());
                            menu.Simbolo = true;
                            rol.Menus.Add(menu);
                            foreach(TreeNode nodoHijo in nodoPadre.Nodes)
                            {
                                if(!nodoHijo.Checked)
                                {
                                    menu = menuBLL.Obtener(nodoHijo.Tag.ToString());
                                    menu.Simbolo = false;
                                    rol.Menus.Add(menu);
                                }
                            }
                        }
                    }


                    if (idRol.HasValue)
                        rolBLL.Modificacion(rol);
                    else
                        rolBLL.Alta(rol);

                    if(SeguridadBLL.usuarioLogueado.Rol.Id == rol.Id)
                        MessageBox.Show("Para ver los cambios reflejados, debe cerrar sesión y volver a iniciar", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    frm.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ClickCancelar(object sender, EventArgs e)
        {
            frm.Close();
        }

        private void AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    //Nodo padre
                    foreach (TreeNode hijo in e.Node.Nodes)
                    {
                        hijo.Checked = e.Node.Checked;
                    }
                }
                else
                {
                    //Nodos hermanos
                    var nodos = e.Node.Parent.Nodes;
                    bool tildarPadre = false;
                    foreach (TreeNode nodo in nodos)
                    {
                        tildarPadre |= nodo.Checked;
                    }

                    e.Node.Parent.Checked = tildarPadre;
                }
            }
        }

        #endregion


        #region Metodos Privados
        private bool DatosValidos()
        {
            ServicioValidaciones.TextoCompleto((TextBox)controles.Find(x => x.Name == "txtDescripcion"), "Descripcion");
            ServicioValidaciones.NodoHijoSelecciondo((TreeView)controles.Find(x => x.Name == "treeViewMenusCompuestos"), "Menus Compuestos");

            return true;
        }

        private void ConfigurarTreeView(TreeView treeViewMenusCompuestos)
        {
            treeViewMenusCompuestos.CheckBoxes = true;

            foreach (var menuCompuesto in menuBLL.ObtenerCompuestos())
            {
                var nodoPadre = new TreeNode(menuCompuesto.Descripcion);
                nodoPadre.Tag = menuCompuesto.Codigo;

                foreach (var menuHijo in menuBLL.ObtenerHijos(menuCompuesto.Id))
                {
                    var nodoHijo = new TreeNode(menuHijo.Descripcion);
                    nodoHijo.Tag = menuHijo.Codigo;
                    nodoPadre.Nodes.Add(nodoHijo);
                }
                treeViewMenusCompuestos.Nodes.Add(nodoPadre);
            }
            treeViewMenusCompuestos.ExpandAll();
            treeViewMenusCompuestos.Nodes[0].EnsureVisible();
        }
        #endregion
    }
}
