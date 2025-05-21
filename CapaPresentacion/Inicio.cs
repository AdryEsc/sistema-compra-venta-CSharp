using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaEntidad;
using CapaNegocio;
using FontAwesome.Sharp;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private static IconMenuItem menuActivo = null;
        private static Form formularioActivo = null;

        private static Usuario usuarioActual;
        public Inicio(Usuario oUsuario)
        {
            usuarioActual = oUsuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            //Mostramos el menu que le corresponde dependiendo el rol del usuario
            List<Permiso> listaPermisos = new CN_Permiso().listarPermisos(usuarioActual.Id);

            foreach (IconMenuItem iconmenu in menu.Items) {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (encontrado == false) {
                    iconmenu.Visible = false;
                }
            }

            //Mostramos nombre del usuario al inicio
            lblUsuario.Text = usuarioActual.NombreCompleto;
        }

        //abrir y dar formato al form seleccionado
        private void abrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (menuActivo != null) {
                menuActivo.BackColor = Color.White;
            }

            menu.BackColor = Color.Silver;
            menuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.SteelBlue;

            contenedor.Controls.Add(formulario);
            formulario.Show();
        }

        //Selecciona el form segun el menu elegido
        private void menuUsuario_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        private void submenuCategoriaProd_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuProducto, new frmCategoriaProductos());
        }

        private void submenuProductos_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuProducto, new frmProductos());
        }

        private void submenuRegistrarVenta_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuVenta, new frmVentas());
        }

        private void submenuDetalleVenta_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuVenta, new frmDetalleVentas());
        }

        private void submenuRegistrarCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuCompra, new frmCompras());
        }

        private void submenuDetalleCompra_Click(object sender, EventArgs e)
        {
            abrirFormulario(menuCompra, new frmDetalleCompras());
        }

        private void menuProveedor_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmProveedores());
        }

        private void menuCliente_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void menuReporte_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmReportes());
        }

        private void menuAjuste_Click(object sender, EventArgs e)
        {
            abrirFormulario((IconMenuItem)sender, new frmAjustes());
        }
    }
}
