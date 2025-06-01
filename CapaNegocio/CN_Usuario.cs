using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        private CD_Usuario objcd_usuario = new CD_Usuario();

        public List<Usuario> listarUsuarios()
        {
            return objcd_usuario.listarUsuarios();
        }

        public int registrarUsuario(Usuario user, out string mensaje)
        {
            mensaje = string.Empty;

            if (user.Documento == "")
            {
                mensaje += "Es necesario el documento del usuario\n";
            }

            if (user.NombreCompleto == "") {
                mensaje += "Es necesario el nombre del usuario\n";
            }


            if (user.Correo == "")
            {
                mensaje += "Es necesario el correo del usuario\n";
            }

            if (user.Clave == "")
            {
                mensaje += "Es necesario la contraseña del usuario\n";
            }

            if (mensaje != string.Empty)
            {
                return 0;
            }
            else {
                return objcd_usuario.registrarUsuario(user, out mensaje);
            }

               
        }

        public bool actualizarUsuario(Usuario user, out string mensaje)
        {
            mensaje = string.Empty;

            if (user.Documento == "")
            {
                mensaje += "Es necesario el documento del usuario\n";
            }

            if (user.NombreCompleto == "")
            {
                mensaje += "Es necesario el nombre del usuario\n";
            }


            if (user.Correo == "")
            {
                mensaje += "Es necesario el correo del usuario\n";
            }

            if (user.Clave == "")
            {
                mensaje += "Es necesario la contraseña del usuario\n";
            }

            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_usuario.actualizarUsuario(user, out mensaje);
            }

           
        }

        public bool eliminarUsuario(Usuario user, out string mensaje)
        {
            return objcd_usuario.eliminarUsuario(user, out mensaje);
        }

        public List<Usuario> buscarUsuarioPorNombre(string caracter)
        {
            return objcd_usuario.buscarUsuarioPorNombre(caracter);
        }
    }
}
