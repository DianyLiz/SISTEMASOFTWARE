using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using CapaEntidad;

namespace CapaDatos
{
    public class CD_Usuario
    {
        private readonly Conexion conexion = new Conexion();

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            using (SqlConnection oconexion = conexion.Conectar)
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT u.IdUsuario, u.Nombre, u.Contraseña, u.Email, u.Telefono, r.IdRol, r.NombreRol, u.Estado, u.FechaCreacion FROM Usuarios u");
                    query.AppendLine("INNER JOIN Roles r ON u.IdRol = r.IdRol");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario()
                            {
                                IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                                Nombre = dr["Nombre"].ToString(),
                                Contraseña = dr["Contraseña"].ToString(),
                                Email = dr["Email"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                oRol = new Rol()
                                {
                                    IdRol = Convert.ToInt32(dr["IdRol"]),
                                    NombreRol = dr["NombreRol"].ToString()
                                },
                                Estado = Convert.ToBoolean(dr["Estado"]),
                                FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al listar usuarios: " + ex.Message);
                    lista = new List<Usuario>();
                }
                finally
                {
                    conexion.cerrarConexion();
                }
            }

            return lista;
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            int idusuariogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (Nombre, Contraseña, Email, Telefono, IdRol, Estado, FechaCreacion) " +
                                                    "VALUES (@Nombre, @Contraseña, @Email, @Telefono, @IdRol, @Estado, @FechaCreacion); SELECT SCOPE_IDENTITY();", oconexion);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Contraseña", obj.Contraseña);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@FechaCreacion", obj.FechaCreacion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idusuariogenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Usuario registrado con éxito.";
                }
            }
            catch (Exception ex)
            {
                idusuariogenerado = 0;
                Mensaje = "Error al registrar usuario: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return idusuariogenerado;
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Usuarios SET Nombre = @Nombre, Contraseña = @Contraseña, Email = @Email, Telefono = @Telefono, IdRol = @IdRol, Estado = @Estado, FechaCreacion = @FechaCreacion WHERE IdUsuario = @IdUsuario", oconexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@Contraseña", obj.Contraseña);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@IdRol", obj.oRol.IdRol);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@FechaCreacion", obj.FechaCreacion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Usuario actualizado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al actualizar usuario: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Usuarios WHERE IdUsuario = @IdUsuario", oconexion);
                    cmd.Parameters.AddWithValue("@IdUsuario", obj.IdUsuario);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Usuario eliminado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar usuario: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }
    }
}
