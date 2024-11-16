using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace CapaDatos
{
    public class CD_Historial
    {
        private readonly Conexion conexion = new Conexion();

        public List<Historial> Listar()
        {
            List<Historial> lista = new List<Historial>();

            using (SqlConnection oconexion = conexion.Conectar)
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT h.IdHistorial, p.IdPaciente, p.Nombre AS NombrePaciente, c.IdCita, c.Motivo, h.Diagnostico, h.Tratamiento, h.Fecha");
                    query.AppendLine("FROM Historial h");
                    query.AppendLine("INNER JOIN Citas c ON h.IdCita = c.IdCita");
                    query.AppendLine("INNER JOIN Pacientes p ON c.IdPaciente = p.IdPaciente");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Historial()
                            {
                                IdHistorial = dr["IdHistorial"] != DBNull.Value ? Convert.ToInt32(dr["IdHistorial"]) : 0,
                                oPaciente = new Pacientes()
                                {
                                    IdPaciente = dr["IdPaciente"] != DBNull.Value ? Convert.ToInt32(dr["IdPaciente"]) : 0,
                                    Nombre = dr["NombrePaciente"] != DBNull.Value ? dr["NombrePaciente"].ToString() : string.Empty
                                },
                                oCita = new Citas()
                                {
                                    IdCita = dr["IdCita"] != DBNull.Value ? Convert.ToInt32(dr["IdCita"]) : 0,
                                    Motivo = dr["Motivo"] != DBNull.Value ? dr["Motivo"].ToString() : string.Empty
                                },
                                Diagnostico = dr["Diagnostico"] != DBNull.Value ? dr["Diagnostico"].ToString() : string.Empty,
                                Tratamiento = dr["Tratamiento"] != DBNull.Value ? dr["Tratamiento"].ToString() : string.Empty,
                                Fecha = dr["Fecha"] != DBNull.Value ? Convert.ToDateTime(dr["Fecha"]) : DateTime.MinValue,
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al listar Historial: {ex.Message}\n{ex.StackTrace}");
                    lista = new List<Historial>();
                }
                finally
                {
                    conexion.cerrarConexion();
                }
            }

            return lista;
        }

        public int Registrar(Historial obj, out string Mensaje)
        {
            int idhistorialgenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Historial(IdPaciente, IdCita, Diagnostico, Tratamiento, Fecha) " +
                                                    "VALUES (@IdPaciente, @IdCita, @Diagnostico, @Tratamiento, @Fecha); SELECT SCOPE_IDENTITY();", oconexion);
                    cmd.Parameters.AddWithValue("@IdPaciente", obj.oPaciente.IdPaciente);
                    cmd.Parameters.AddWithValue("@IdCita", obj.oCita.IdCita);
                    cmd.Parameters.AddWithValue("@Diagnostico", obj.Diagnostico);
                    cmd.Parameters.AddWithValue("@Tratamiento", obj.Tratamiento);
                    cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idhistorialgenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Historial registrado con éxito.";
                }
            }
            catch (Exception ex)
            {
                idhistorialgenerado = 0;
                Mensaje = "Error al registrar Historial: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return idhistorialgenerado;
        }

        public bool Editar(Historial obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Historial SET IdPaciente = @IdPaciente, IdCita = @IdCita, Diagnostico = @Diagnostico, Tratamiento = @Tratamiento, Fecha = @Fecha WHERE IdHistorial = @IdHistorial", oconexion);
                    cmd.Parameters.AddWithValue("@IdHistorial", obj.IdHistorial);
                    cmd.Parameters.AddWithValue("@IdPaciente", obj.oPaciente.IdPaciente);
                    cmd.Parameters.AddWithValue("@IdCita", obj.oCita.IdCita);
                    cmd.Parameters.AddWithValue("@Diagnostico", obj.Diagnostico);
                    cmd.Parameters.AddWithValue("@Tratamiento", obj.Tratamiento);
                    cmd.Parameters.AddWithValue("@Fecha", obj.Fecha);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Historial actualizado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al actualizar Historial: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }

        public bool Eliminar(Historial obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Historial WHERE IdHistorial = @IdHistorial", oconexion);
                    cmd.Parameters.AddWithValue("@IdHistorial", obj.IdHistorial);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Historial eliminado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar Historial: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }
    }
}
