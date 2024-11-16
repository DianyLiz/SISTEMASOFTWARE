using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Doctores
    {
        private readonly Conexion conexion = new Conexion();

        public List<Doctores> Listar()
        {
            List<Doctores> lista = new List<Doctores>();

            using (SqlConnection oconexion = conexion.Conectar)
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT d.Id, d.Nombre,e.IdEspecialidad, e.NombreEspecialidad, d.Telefono, d.Email, d.HorarioAtencion FROM Doctores d");
                    query.AppendLine("INNER JOIN Especialidades e ON d.IdEspecialidad = e.IdEspecialidad");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Doctores()
                            {
                                Id = Convert.ToInt32(dr["Id"]),
                                Nombre = dr["Nombre"].ToString(),
                                oEspecialidad = new Especialidades()
                                {
                                    IdEspecialidad = Convert.ToInt32(dr["IdEspecialidad"]),
                                    NombreEspecialidad = dr["NombreEspecialidad"].ToString()
                                },
                                Telefono = dr["Telefono"].ToString(),
                                Email = dr["Email"].ToString(),
                                HorarioAtencion = dr["HorarioAtencion"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al listar Doctores: " + ex.Message);
                    lista = new List<Doctores>();
                }
                finally
                {
                    conexion.cerrarConexion();
                }
            }

            return lista;
        }

        public int Registrar(Doctores obj, out string Mensaje)
        {
            int idDoctorgenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Doctores(Nombre, IdEspecialidad, Telefono, Email, HorarioAtencion) " +
                    "VALUES (@Nombre, @IdEspecialidad, @Telefono, @Email, @HorarioAtencion); SELECT SCOPE_IDENTITY();", oconexion);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@IdEspecialidad", obj.oEspecialidad.IdEspecialidad);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@HorarioAtencion", obj.HorarioAtencion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idDoctorgenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Doctor registrado con éxito.";
                }
            }
            catch (Exception ex)
            {
                idDoctorgenerado = 0;
                Mensaje = "Error al registrar Doctor: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return idDoctorgenerado;
        }

        public bool Editar(Doctores obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Doctores SET Nombre = @Nombre, IdEspecialidad = @IdEspecialidad, Telefono = @Telefono, Email = @Email, HorarioAtencion = @HorarioAtencion WHERE Id = @Id", oconexion);
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    // Corregido: se cambia "@IdEsoecialidad" por "@IdEspecialidad"
                    cmd.Parameters.AddWithValue("@IdEspecialidad", obj.oEspecialidad.IdEspecialidad);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@HorarioAtencion", obj.HorarioAtencion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Doctor actualizado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al actualizar Doctor: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }

        public bool Eliminar(Doctores obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Doctores WHERE Id = @Id", oconexion);
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Doctor eliminado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar Doctor: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }
    }
}

