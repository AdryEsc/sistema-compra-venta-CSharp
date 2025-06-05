using CapaEntidad;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Categoria
    {
        public List<Categoria> listarCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select Id,Descripcion,Estado from Categoria order by Descripcion");

                    SqlCommand comando = new SqlCommand(consulta.ToString(), oConexion);
                    comando.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Categoria()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Descripcion = dr["Descripcion"].ToString(),
                                Estado = Convert.ToInt32(dr["Estado"])
                            });
                        }
                    }

                    oConexion.Close();


                }
                catch (Exception)
                {
                    lista = new List<Categoria>();
                }
            }
            return lista;
        }

        //Meotodo para registrar una categoria
        public int registrarCategoria(Categoria categ, out string mensaje)
        {
            int idCategoriaGenerada = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_RegistrarCategoria", oConexion);
                    comando.Parameters.AddWithValue("Descripcion", categ.Descripcion);
                    comando.Parameters.AddWithValue("Estado", categ.Estado);
                    comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    comando.ExecuteNonQuery();

                    idCategoriaGenerada = Convert.ToInt32(comando.Parameters["Resultado"].Value);
                    mensaje = comando.Parameters["Mensaje"].Value.ToString();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                idCategoriaGenerada = 0;
                mensaje = ex.Message;
            }


            return idCategoriaGenerada;
        }

        //Metodo para editar una categoria
        public bool actualizarCategoria(Categoria categ, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_EditarCategoria", oConexion);
                    comando.Parameters.AddWithValue("Id", categ.Id);
                    comando.Parameters.AddWithValue("Descripcion", categ.Descripcion);
                    comando.Parameters.AddWithValue("Estado", categ.Estado);
                    comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    comando.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(comando.Parameters["Resultado"].Value);
                    mensaje = comando.Parameters["Mensaje"].Value.ToString();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }


            return resultado;
        }

        //Metodo para eliminar (dejar inactivo) una categoria
        public bool eliminarCategoria(Categoria categ, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_EliminarCategoria", oConexion);
                    comando.Parameters.AddWithValue("Id", categ.Id);
                    comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    comando.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(comando.Parameters["Resultado"].Value);
                    mensaje = comando.Parameters["Mensaje"].Value.ToString();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }


            return resultado;
        }

    }//Fin clase
}
