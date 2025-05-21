using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Permiso
    {
        public List<Permiso> listarPermisos(int idusuario)
        {
            List<Permiso> lista = new List<Permiso>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select p.IdRol,p.NombreMenu from Permiso p");
                    consulta.AppendLine("inner join Rol r on r.Id = p.IdRol");
                    consulta.AppendLine("inner join Usuario u on u.IdRol = r.Id");
                    consulta.AppendLine("where u.Id = @idUsuario");

                    SqlCommand comando = new SqlCommand(consulta.ToString(), oConexion);
                    comando.Parameters.AddWithValue("@idUsuario", idusuario);
                    comando.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Permiso()
                            {
                                oRol = new Rol() {Id = Convert.ToInt32(dr["IdRol"]) },
                                NombreMenu = dr["NombreMenu"].ToString()
                            });
                        }
                    }


                }
                catch (Exception)
                {
                    lista = new List<Permiso>();
                }
            }
            return lista;
        }
    }
}
