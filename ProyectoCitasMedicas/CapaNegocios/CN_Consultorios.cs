using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Consultorios
    {
        private CD_Consultorios objcd_consultorio = new CD_Consultorios();

        public List<Consultorios> Listar()
        {
            return objcd_consultorio.Listar();
        }

        public int Registrar(Consultorios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Consultorio))
            {
                Mensaje += "Es necesario el Nombre del Consultorio\n";
            }

            if (string.IsNullOrEmpty(obj.Ubicacion))
            {
                Mensaje += "Es necesario la ubicacion del rol\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }
            else
            {
                return objcd_consultorio.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Consultorios obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Consultorio))
            {
                Mensaje += "Es necesario el nombre del consultorio\n";
            }

            if (string.IsNullOrEmpty(obj.Ubicacion))
            {
                Mensaje += "Es necesario la ubicacion del rol\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcd_consultorio.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Consultorios obj, out string Mensaje)
        {
            return objcd_consultorio.Eliminar(obj, out Mensaje);
        }
    }
}
