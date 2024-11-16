using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Especialidades
    {
        private CD_Especialidades objcd_Especialidad = new CD_Especialidades();

        public List<Especialidades> Listar()
        {
            return objcd_Especialidad.Listar();
        }
        public int Registrar(Especialidades obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.NombreEspecialidad))
            {
                Mensaje += "Es necesario el Nombre de la Especialidad\n";
            }

            if (string.IsNullOrEmpty(obj.Descripcion))
            {
                Mensaje += "Es necesario la descripcion de la Especialidad\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }
            else
            {
                return objcd_Especialidad.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Especialidades obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.NombreEspecialidad))
            {
                Mensaje += "Es necesario el nombre de la Especialidad\n";
            }

            if (string.IsNullOrEmpty(obj.Descripcion))
            {
                Mensaje += "Es necesario la descripcion de la Especialidad\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcd_Especialidad.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Especialidades obj, out string Mensaje)
        {
            return objcd_Especialidad.Eliminar(obj, out Mensaje);
        }
    }
}

