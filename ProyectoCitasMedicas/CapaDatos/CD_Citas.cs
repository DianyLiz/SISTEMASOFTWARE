using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace CapaDatos
{
    public class CD_Citas
    {
        private readonly Conexion conexion = new Conexion();

        public List<Citas> Listar()
        {
            List<Citas> lista = new List<Citas>();

            using (SqlConnection oconexion = conexion.Conectar)
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT c.IdCita, p.IdPaciente, p.Nombre AS NombrePaciente, d.Id AS IdDoctor, d.Nombre AS NombreDoctor,");
                    query.AppendLine("c.FechaCita, c.Motivo, c.Estado, l.IdConsultorio, l.Consultorio");
                    query.AppendLine("FROM Citas c");
                    query.AppendLine("INNER JOIN Pacientes p ON c.IdPaciente = p.IdPaciente");
                    query.AppendLine("INNER JOIN Doctores d ON c.IdMedico = d.Id");
                    query.AppendLine("INNER JOIN Consultorios l ON c.IdConsultorio = l.IdConsultorio");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Citas()
                            {
                                IdCita = dr["IdCita"] != DBNull.Value ? Convert.ToInt32(dr["IdCita"]) : 0,
                                oPaciente = new Pacientes()
                                {
                                    IdPaciente = dr["IdPaciente"] != DBNull.Value ? Convert.ToInt32(dr["IdPaciente"]) : 0,
                                    Nombre = dr["NombrePaciente"] != DBNull.Value ? dr["NombrePaciente"].ToString() : string.Empty
                                },
                                oDoctor = new Doctores()
                                {
                                    Id = dr["IdDoctor"] != DBNull.Value ? Convert.ToInt32(dr["IdDoctor"]) : 0,
                                    Nombre = dr["NombreDoctor"] != DBNull.Value ? dr["NombreDoctor"].ToString() : string.Empty
                                },
                                FechaCita = dr["FechaCita"] != DBNull.Value ? Convert.ToDateTime(dr["FechaCita"]) : DateTime.MinValue,
                                Motivo = dr["Motivo"] != DBNull.Value ? dr["Motivo"].ToString() : string.Empty,
                                Estado = dr["Estado"] != DBNull.Value && Convert.ToBoolean(dr["Estado"]),
                                oConsultorio = new Consultorios()
                                {
                                    IdConsultorio = dr["IdConsultorio"] != DBNull.Value ? Convert.ToInt32(dr["IdConsultorio"]) : 0,
                                    Consultorio = dr["Consultorio"] != DBNull.Value ? dr["Consultorio"].ToString() : string.Empty
                                }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al listar Cita: " + ex.Message);
                    lista = new List<Citas>();
                }
                finally
                {
                    conexion.cerrarConexion();
                }
            }

            return lista;
        }

        public int Registrar(Citas obj, out string Mensaje)
        {
            int idcitagenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Citas(IdPaciente, IdMedico, FechaCita, Motivo, Estado, IdConsultorio) " +
                                                    "VALUES (@IdPaciente, @IdMedico, @FechaCita, @Motivo, @Estado, @IdConsultorio); SELECT SCOPE_IDENTITY();", oconexion);
                    cmd.Parameters.AddWithValue("@IdPaciente", obj.oPaciente.IdPaciente);
                    cmd.Parameters.AddWithValue("@IdMedico", obj.oDoctor.Id);
                    cmd.Parameters.AddWithValue("@FechaCita", obj.FechaCita);
                    cmd.Parameters.AddWithValue("@Motivo", obj.Motivo);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@IdConsultorio", obj.oConsultorio.IdConsultorio);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idcitagenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Cita registrada con éxito.";
                }
            }
            catch (Exception ex)
            {
                idcitagenerado = 0;
                Mensaje = "Error al registrar cita: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return idcitagenerado;
        }

        public bool Editar(Citas obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Citas SET IdPaciente = @IdPaciente, IdMedico = @IdMedico, FechaCita = @FechaCita, Estado = @Estado, IdConsultorio = @IdConsultorio WHERE IdCita = @IdCita", oconexion);
                    cmd.Parameters.AddWithValue("@IdCita", obj.IdCita);
                    cmd.Parameters.AddWithValue("@IdPaciente", obj.oPaciente.IdPaciente);
                    cmd.Parameters.AddWithValue("@IdMedico", obj.oDoctor.Id);
                    cmd.Parameters.AddWithValue("@FechaCita", obj.FechaCita);
                    cmd.Parameters.AddWithValue("@Motivo", obj.Motivo);
                    cmd.Parameters.AddWithValue("@Estado", obj.Estado);
                    cmd.Parameters.AddWithValue("@IdConsultorio", obj.oConsultorio.IdConsultorio);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Cita actualizada con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al actualizar cita: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }

        public bool Eliminar(Citas obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Citas WHERE IdCita = @IdCita", oconexion);
                    cmd.Parameters.AddWithValue("@IdCita", obj.IdCita);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Cita eliminada con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar cita: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }
    }
}
