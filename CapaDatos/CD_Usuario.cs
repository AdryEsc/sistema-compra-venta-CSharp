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
                    consulta.AppendLine("order by u.NombreCompleto");

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

                    oConexion.Close();


                }
                catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }

        //Metodo para agregar un usuario
        public int registrarUsuario(Usuario user,out string mensaje) {
            int idUsuarioGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_RegistrarUsuario", oConexion);
                    comando.Parameters.AddWithValue("Documento", user.Documento);
                    comando.Parameters.AddWithValue("NombreCompleto", user.NombreCompleto);
                    comando.Parameters.AddWithValue("Correo", user.Correo);
                    comando.Parameters.AddWithValue("Clave", user.Clave);
                    comando.Parameters.AddWithValue("IdRol", user.oRol.Id);
                    comando.Parameters.AddWithValue("Estado", user.Estado);
                    comando.Parameters.Add("IdUsuarioResultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    comando.ExecuteNonQuery();

                    idUsuarioGenerado = Convert.ToInt32(comando.Parameters["IdUsuarioResultado"].Value);
                    mensaje = comando.Parameters["Mensaje"].Value.ToString();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                idUsuarioGenerado = 0;
                mensaje = ex.Message;
            }


            return idUsuarioGenerado;
        }

        //Metodo para actualizarar un usuario
        public bool actualizarUsuario(Usuario user, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_EditarUsuario", oConexion);
                    comando.Parameters.AddWithValue("Id", user.Id);
                    comando.Parameters.AddWithValue("Documento", user.Documento);
                    comando.Parameters.AddWithValue("NombreCompleto", user.NombreCompleto);
                    comando.Parameters.AddWithValue("Correo", user.Correo);
                    comando.Parameters.AddWithValue("Clave", user.Clave);
                    comando.Parameters.AddWithValue("IdRol", user.oRol.Id);
                    comando.Parameters.AddWithValue("Estado", user.Estado);
                    comando.Parameters.Add("Respuesta", SqlDbType.Int).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    comando.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(comando.Parameters["Respuesta"].Value);
                    mensaje = comando.Parameters["Mensaje"].Value.ToString();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }


            return respuesta;
        }

        //Metodo para eliminar (dejar inactivo) un usuario
        public bool eliminarUsuario(Usuario user, out string mensaje)
        {
            bool respuesta = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_EliminarUsuario", oConexion);
                    comando.Parameters.AddWithValue("Id", user.Id);
                    comando.Parameters.Add("Mensaje", SqlDbType.VarChar,500).Direction = ParameterDirection.Output;

                    comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    comando.ExecuteNonQuery();

                    respuesta = true;
                    mensaje = comando.Parameters["Mensaje"].Value.ToString();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                mensaje = ex.Message;
            }


            return respuesta;
        }

        //Busca usuario por nombre
        public List<Usuario> buscarUsuarioPorNombre(string caracter)
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select u.Id,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.Id,r.Descripcion from Usuario u");
                    consulta.AppendLine("inner join rol r on r.Id = u.IdRol");
                    consulta.AppendLine("where u.NombreCompleto like '%" + caracter + "%' order by u.NombreCompleto");

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

                    oConexion.Close();


                }
                catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }

        //Busca usuario por dni
        public List<Usuario> buscarUsuarioPorDni(string caracter)
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select u.Id,u.Documento,u.NombreCompleto,u.Correo,u.Clave,u.Estado,r.Id,r.Descripcion from Usuario u");
                    consulta.AppendLine("inner join rol r on r.Id = u.IdRol");
                    consulta.AppendLine("where u.Documento like '%" + caracter + "%' order by u.NombreCompleto");

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

                    oConexion.Close();


                }
                catch (Exception)
                {
                    lista = new List<Usuario>();
                }
            }
            return lista;
        }

    }//Fin clase
}
