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
    public class CD_Especialidades
    {
        private readonly Conexion conexion = new Conexion();

        public List<Especialidades> Listar()
        {
            List<Especialidades> lista = new List<Especialidades>();

            using (SqlConnection oconexion = conexion.Conectar)
            {
                try
                {

                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT IdEspecialidad, NombreEspecialidad, Descripcion FROM Especialidades");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;


                    conexion.abrirConexion();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(new Especialidades()
                            {
                                IdEspecialidad = Convert.ToInt32(dr["IdEspecialidad"]),
                                NombreEspecialidad = dr["NombreEspecialidad"].ToString(),
                                Descripcion = dr["Descripcion"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al listar Especialidades: " + ex.Message);
                    lista = new List<Especialidades>();
                }
                finally
                {
                    conexion.cerrarConexion();
                }
            }

            return lista;
        }
        public int Registrar(Especialidades obj, out string Mensaje)
        {
            int idespecialidadgenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Especialidades(NombreEspecialidad, Descripcion)  " +
                                                    "VALUES (@NombreEspecialidad, @Descripcion); SELECT SCOPE_IDENTITY();", oconexion);
                    cmd.Parameters.AddWithValue("@NombreEspecialidad", obj.NombreEspecialidad);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    idespecialidadgenerado = Convert.ToInt32(cmd.ExecuteScalar());
                    Mensaje = "Especialidad registrada con éxito.";
                }
            }
            catch (Exception ex)
            {
                idespecialidadgenerado = 0;
                Mensaje = "Error al registrar especialidad: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return idespecialidadgenerado;
        }

        public bool Editar(Especialidades obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Especialidades SET NombreEspecialidad = @NombreEspecialidad,Descripcion = @Descripcion WHERE IdEspecialidad = @IdEspecialidad", oconexion);
                    cmd.Parameters.AddWithValue("@IdEspecialidad", obj.IdEspecialidad);
                    cmd.Parameters.AddWithValue("@NombreEspecialidad", obj.NombreEspecialidad);
                    cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Especialidad actualizada con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al actualizar Especialidad: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }

        public bool Eliminar(Especialidades obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = conexion.Conectar)
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Especialidades WHERE IdEspecialidad = @IdEspecialidad", oconexion);
                    cmd.Parameters.AddWithValue("@IdEspecialidad", obj.IdEspecialidad);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    respuesta = true;
                    Mensaje = "Especialidad eliminada con éxito.";
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
                Mensaje = "Error al eliminar especialidad: " + ex.Message;
            }
            finally
            {
                conexion.cerrarConexion();
            }

            return respuesta;
        }
    }
}

