using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Historial
    {
        private CD_Historial objcd_historial = new CD_Historial();

        public List<Historial> Listar()
        {
            return objcd_historial.Listar();
        }

        public int Registrar(Historial obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Diagnostico))
            {
                Mensaje += "Es necesario el diagnostico del Paciente\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }
            else
            {
                return objcd_historial.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Historial obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Diagnostico))
            {
                Mensaje += "Es necesario el diagnostico del paciente\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcd_historial.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Historial obj, out string Mensaje)
        {
            return objcd_historial.Eliminar(obj, out Mensaje);
        }
    }
}

