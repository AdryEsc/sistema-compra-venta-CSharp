using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Producto
    {
        public List<Producto> listarProductos()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select u.Id,u.Codigo,u.Descripcion,u.Stock,u.PrecioCompra,u.PrecioVenta,u.Estado,c.Id as IdCategoria,c.Descripcion as DescripcionCategoria from Producto u");
                    consulta.AppendLine("inner join Categoria c on u.IdCategoria = c.Id");
                    consulta.AppendLine("order by u.Descripcion");

                    SqlCommand comando = new SqlCommand(consulta.ToString(), oConexion);
                    comando.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Codigo = dr["Codigo"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                PrecioCompra = Convert.ToDecimal(dr["PrecioCompra"]),
                                PrecioVenta = Convert.ToDecimal(dr["PrecioVenta"]),
                                oCategoria = new Categoria() { Id = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DescripcionCategoria"].ToString() },
                                Estado = Convert.ToInt32(dr["Estado"])
                            });
                        }
                    }

                    oConexion.Close();


                }
                catch (Exception)
                {
                    lista = new List<Producto>();
                }
            }
            return lista;
        }

        //Meotodo para registrar un producto
        public int registrarProducto(Producto prod, out string mensaje)
        {
            int idProductoGenerado = 0;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_RegistrarProducto", oConexion);
                    comando.Parameters.AddWithValue("Codigo", prod.Codigo);
                    comando.Parameters.AddWithValue("Descripcion", prod.Descripcion);
                    comando.Parameters.AddWithValue("Stock", prod.Stock);
                    comando.Parameters.AddWithValue("PrecioCompra", prod.PrecioCompra);
                    comando.Parameters.AddWithValue("PrecioVenta", prod.PrecioVenta);
                    comando.Parameters.AddWithValue("IdCategoria", prod.oCategoria.Id);
                    comando.Parameters.AddWithValue("Estado", prod.Estado);
                    comando.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    comando.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;

                    comando.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    comando.ExecuteNonQuery();

                    idProductoGenerado = Convert.ToInt32(comando.Parameters["Resultado"].Value);
                    mensaje = comando.Parameters["Mensaje"].Value.ToString();

                    oConexion.Close();
                }
            }
            catch (Exception ex)
            {
                idProductoGenerado = 0;
                mensaje = ex.Message;
            }


            return idProductoGenerado;
        }

        //Metodo para editar una categoria
        public bool actualizarProducto(Producto prod, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_EditarProducto", oConexion);
                    comando.Parameters.AddWithValue("Id", prod.Id);
                    comando.Parameters.AddWithValue("Codigo", prod.Codigo);
                    comando.Parameters.AddWithValue("Descripcion", prod.Descripcion);
                    comando.Parameters.AddWithValue("Stock", prod.Stock);
                    comando.Parameters.AddWithValue("PrecioCompra", prod.PrecioCompra);
                    comando.Parameters.AddWithValue("PrecioVenta", prod.PrecioVenta);
                    comando.Parameters.AddWithValue("Estado", prod.Estado);
                    comando.Parameters.AddWithValue("IdCategoria", prod.oCategoria.Id);
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

        //Metodo para eliminar (dejar inactivo) un producto
        public bool eliminarProducto(Producto prod, out string mensaje)
        {
            bool resultado = false;
            mensaje = string.Empty;

            try
            {
                using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
                {

                    SqlCommand comando = new SqlCommand("SP_EliminarProducto", oConexion);
                    comando.Parameters.AddWithValue("Id", prod.Id);
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
