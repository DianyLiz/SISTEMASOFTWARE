using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_Citas
    {
        private CD_Citas objcd_citas = new CD_Citas();

        public List<Citas> Listar()
        {
            return objcd_citas.Listar();
        }

        public int Registrar(Citas obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Motivo))
            {
                Mensaje += "Es necesario el motivo de la cita\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }
            else
            {
                return objcd_citas.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Citas obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Motivo))
            {
                Mensaje += "Es necesario el motivo de la  cita\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcd_citas.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Citas obj, out string Mensaje)
        {
            return objcd_citas.Eliminar(obj, out Mensaje);
        }
    }
}

