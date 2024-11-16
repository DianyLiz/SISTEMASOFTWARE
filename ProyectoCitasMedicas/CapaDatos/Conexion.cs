using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        string cadena = "Data Source=Diany\\SQLEXPRESS; " +
                        "Initial Catalog=CitasMedicas; " +
                        "Integrated Security=True";
        public SqlConnection Conectar = new SqlConnection();

        public Conexion()
        {
            Conectar.ConnectionString = cadena;
        }

        public void abrirConexion()
        {
            try
            {
                Conectar.Open();
                Console.WriteLine("Conexion exitosa.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexion: " + ex.Message);
            }
        }

        public void cerrarConexion()
        {
            Conectar.Close();
        }
    }
}