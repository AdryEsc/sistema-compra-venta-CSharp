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
    public class CD_Usuario
    {
        /*
        public List<Usuario> listarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    string consulta = "select Id,Documento,NombreCompleto,Correo,Clave,Estado from Usuario";
                    SqlCommand comando = new SqlCommand(consulta, oConexion);
                    comando.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToInt32(dr["Estado"])
                            });
                        }
                    }


                } catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }
        */

        public List<Usuario> listarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select u.Id,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.Id,r.Descripcion from Usuario u");
                    consulta.AppendLine("inner join rol r on r.Id = u.IdRol");
                    
                    SqlCommand comando = new SqlCommand(consulta.ToString(), oConexion);
                    comando.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Documento = dr["Documento"].ToString(),
                                NombreCompleto = dr["NombreCompleto"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Clave = dr["Clave"].ToString(),
                                Estado = Convert.ToInt32(dr["Estado"]),
                                oRol = new Rol() { Id = Convert.ToInt32(dr["Id"]), Descripcion = dr["Descripcion"].ToString() }
                            });
                        }
                    }


                }
                catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }
    }
}
