using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objcd_producto = new CD_Producto();

        public List<Producto> listarProductos()
        {
            return objcd_producto.listarProductos();
        }

        public int registrarProducto(Producto prod, out string mensaje)
        {
            return objcd_producto.registrarProducto(prod, out mensaje);
        }

        public bool actualizarProducto(Producto prod, out string mensaje)
        {
            return objcd_producto.actualizarProducto(prod, out mensaje);
        }

        public bool eliminarProducto(Producto prod, out string mensaje)
        {
            return objcd_producto.eliminarProducto(prod, out mensaje);
        }
    }//Fin clase
}
