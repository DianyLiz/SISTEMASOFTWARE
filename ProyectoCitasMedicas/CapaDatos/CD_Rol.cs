using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Rol
    {
        private readonly Conexion conexion = new Conexion();

        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();

            using (SqlConnection oconexion = conexion.Conectar)
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT r.IdRol, r.NombreRol, r.Descripcion from Roles r ");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol()
                            {
                                IdRol = Convert.ToInt32(dr["IdRol"]),
                                NombreRol = dr["NombreRol"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al listar Rol: " + ex.Message);
                    lista = new List<Rol>();
                }
                finally
                {
                    conexion.cerrarConexion();
                }
            }

            return lista;
        }

        public int Registrar(Rol obj, out string Mensaje)
        {
            int idrolgenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Roles(NombreRol, Descripcion) " +
                                                    "VALUES (@NombreRol, @Descripcion); SELECT SCOPE_IDENTITY();", oconexion);
                    cmd.Parameters.AddWithValue("@NombreRol", obj.NombreRol);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idrolgenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Rol registrado con éxito.";
                }
            }
            catch (Exception ex)
            {
                idrolgenerado = 0;
                Mensaje = "Error al registrar rol: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return idrolgenerado;
        }

        public bool Editar(Rol obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Roles SET NombreRol = @NombreRol,Descripcion = @Descripcion WHERE IdRol = @IdRol", oconexion);
                    cmd.Parameters.AddWithValue("@IdRol", obj.IdRol);
                    cmd.Parameters.AddWithValue("@NombreRol", obj.NombreRol);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Rol actualizado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al actualizar Rol: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }

        public bool Eliminar(Rol obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Roles WHERE IdRol = @IdRol", oconexion);
                    cmd.Parameters.AddWithValue("@IdRol", obj.IdRol);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Rol eliminado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar Rol: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }
    }
}
