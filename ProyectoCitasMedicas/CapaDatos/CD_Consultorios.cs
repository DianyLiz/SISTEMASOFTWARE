using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace CapaDatos
{
    public class CD_Consultorios
    {
        private readonly Conexion conexion = new Conexion();

        public List<Consultorios> Listar()
        {
            List<Consultorios> lista = new List<Consultorios>();

            using (SqlConnection oconexion = conexion.Conectar)
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT c.IdConsultorio, c.Consultorio, c.Ubicacion, c.Capacidad FROM Consultorios c");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Consultorios()
                            {
                                IdConsultorio = Convert.ToInt32(dr["IdConsultorio"]),
                                Consultorio = dr["Consultorio"].ToString(),
                                Ubicacion = dr["Ubicacion"].ToString(),
                                Capacidad = dr["Capacidad"] != DBNull.Value ? Convert.ToInt32(dr["Capacidad"]) : 0 // Verificación de nulos
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al listar Consultorio: " + ex.Message);
                    lista = new List<Consultorios>();
                }
            }

            return lista;
        }

        public int Registrar(Consultorios obj, out string Mensaje)
        {
            int idconsultoriogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Consultorios(Consultorio, Ubicacion, Capacidad) " +
                                                    "VALUES (@Consultorio, @Ubicacion, @Capacidad); SELECT SCOPE_IDENTITY();", oconexion);
                    cmd.Parameters.AddWithValue("@Consultorio", obj.Consultorio);
                    cmd.Parameters.AddWithValue("@Ubicacion", obj.Ubicacion);
                    cmd.Parameters.AddWithValue("@Capacidad", obj.Capacidad);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idconsultoriogenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Consultorio registrado con éxito.";
                }
            }
            catch (Exception ex)
            {
                idconsultoriogenerado = 0;
                Mensaje = "Error al registrar consultorio: " + ex.Message;
            }

            return idconsultoriogenerado;
        }

        public bool Editar(Consultorios obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Consultorios SET Consultorio = @Consultorio, Ubicacion = @Ubicacion, Capacidad = @Capacidad WHERE IdConsultorio = @IdConsultorio", oconexion);
                    cmd.Parameters.AddWithValue("@IdConsultorio", obj.IdConsultorio);
                    cmd.Parameters.AddWithValue("@Consultorio", obj.Consultorio);
                    cmd.Parameters.AddWithValue("@Ubicacion", obj.Ubicacion);
                    cmd.Parameters.AddWithValue("@Capacidad", obj.Capacidad);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Consultorio actualizado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al actualizar Consultorio: " + ex.Message;
            }

            return respuesta;
        }

        public bool Eliminar(Consultorios obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Consultorios WHERE IdConsultorio = @IdConsultorio", oconexion);
                    cmd.Parameters.AddWithValue("@IdConsultorio", obj.IdConsultorio);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Consultorio eliminado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar Consultorio: " + ex.Message;
            }

            return respuesta;
        }
    }
}
