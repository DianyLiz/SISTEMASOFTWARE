using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public  class CN_Pacientes
    {
        private CD_Pacientes objcd_paciente = new CD_Pacientes();

        public List<Pacientes> Listar()
        {
            return objcd_paciente.Listar();
        }

        public int Registrar(Pacientes obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                Mensaje += "Es necesario el Nombre del Paciente\n";
            }

            if (string.IsNullOrEmpty(obj.Genero))
            {
                Mensaje += "Es necesario el genero del paciente\n";
            }

            if (string.IsNullOrEmpty(obj.Direccion))
            {
                Mensaje += "Es necesario la direccion del paciente\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }
            else
            {
                return objcd_paciente.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Pacientes obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                Mensaje += "Es necesario el Nombre del Paciente\n";
            }

            if (string.IsNullOrEmpty(obj.Genero))
            {
                Mensaje += "Es necesario el genero del paciente\n";
            }

            if (string.IsNullOrEmpty(obj.Direccion))
            {
                Mensaje += "Es necesario la direccion del paciente\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcd_paciente.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Pacientes obj, out string Mensaje)
        {
            return objcd_paciente.Eliminar(obj, out Mensaje);
        }
    }
}
