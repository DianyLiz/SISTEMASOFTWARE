using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CapaDatos
{
    public class CD_Pacientes
    {
        private readonly Conexion conexion = new Conexion();

        public List<Pacientes> Listar()
        {
            List<Pacientes> lista = new List<Pacientes>();

            using (SqlConnection oconexion = conexion.Conectar)
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT p.IdPaciente, p.Nombre, p.FechaNacimiento, p.Genero, p.Direccion, p.Telefono, p.Email, p.FechaRegistro FROM Pacientes p");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pacientes()
                            {
                                IdPaciente = Convert.ToInt32(dr["IdPaciente"]),
                                Nombre = dr["Nombre"].ToString(),
                                FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"].ToString()),
                                Genero = dr["Genero"].ToString(),
                                Direccion = dr["Direccion"].ToString(),
                                Telefono = dr["Telefono"].ToString(),
                                Email = dr["Email"].ToString(),
                                FechaRegistro = Convert.ToDateTime(dr["FechaRegistro"].ToString())
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al listar Paciente: " + ex.Message);
                    lista = new List<Pacientes>();
                }
                finally
                {
                    conexion.cerrarConexion();
                }
            }

            return lista;
        }

        public int Registrar(Pacientes obj, out string Mensaje)
        {
            int idpacientegenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Pacientes (Nombre, FechaNacimiento, Genero, Direccion, Telefono, Email, FechaRegistro) " +
                                                    "VALUES (@Nombre, @FechaNacimiento, @Genero, @Direccion, @Telefono, @Email, @FechaRegistro); SELECT SCOPE_IDENTITY();", oconexion);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Genero", obj.Genero);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@FechaRegistro", obj.FechaRegistro);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idpacientegenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Paciente registrado con éxito.";
                }
            }
            catch (Exception ex)
            {
                idpacientegenerado = 0;
                Mensaje = "Error al registrar paciente: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return idpacientegenerado;
        }

        public bool Editar(Pacientes obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Pacientes SET Nombre = @Nombre, FechaNacimiento = @FechaNacimiento, Genero = @Genero, Direccion = @Direccion, Telefono = @Telefono, Email = @Email, FechaRegistro = @FechaRegistro WHERE IdPaciente = @IdPaciente", oconexion);
                    cmd.Parameters.AddWithValue("@IdPaciente", obj.IdPaciente);
                    cmd.Parameters.AddWithValue("@Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", obj.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Genero", obj.Genero);
                    cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("@Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("@Email", obj.Email);
                    cmd.Parameters.AddWithValue("@FechaRegistro", obj.FechaRegistro);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Paciente actualizado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al actualizar Paciente: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }

        public bool Eliminar(Pacientes obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Pacientes WHERE IdPaciente = @IdPaciente", oconexion);
                    cmd.Parameters.AddWithValue("@IdPaciente", obj.IdPaciente);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Paciente eliminado con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar paciente: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }
    }
}

