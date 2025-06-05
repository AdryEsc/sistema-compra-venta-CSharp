using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        private CD_Categoria objcd_categoria = new CD_Categoria();

        public List<Categoria> listarCategorias()
        {
            return objcd_categoria.listarCategorias();
        }

        public int registrarCategoria(Categoria categ, out string mensaje)
        {
                return objcd_categoria.registrarCategoria(categ, out mensaje);
        }

        public bool actualizarCategoria(Categoria categ, out string mensaje)
        {
                return objcd_categoria.actualizarCategoria(categ, out mensaje);
        }

        public bool eliminarCategoria(Categoria categ, out string mensaje)
        {
            return objcd_categoria.eliminarCategoria(categ, out mensaje);
        }

        
    }
}
