using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Rol
    {
        public List<Rol> listarRoles()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection oConexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder consulta = new StringBuilder();
                    consulta.AppendLine("select Id,Descripcion from Rol");
              

                    SqlCommand comando = new SqlCommand(consulta.ToString(), oConexion);
                    comando.CommandType = CommandType.Text;

                    oConexion.Open();

                    using (SqlDataReader dr = comando.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception)
                {
                    lista = new List<Rol>();
                }
            }
            return lista;
        }
    }
}
