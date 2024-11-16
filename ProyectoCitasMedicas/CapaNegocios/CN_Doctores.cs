using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CapaNegocios
{
    public class CN_Doctores
    {
        private CD_Doctores objcd_doctores = new CD_Doctores();

        public List<Doctores> Listar()
        {
            return objcd_doctores.Listar();
        }

        public int Registrar(Doctores obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                Mensaje += "Es necesario el Nombre del Doctor\n";
            }


            if (string.IsNullOrEmpty(obj.Email))
            {
                Mensaje += "Es necesario el email del Doctor\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }
            else
            {
                return objcd_doctores.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Doctores obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                Mensaje += "Es necesario el nombre del Doctor\n";
            }

            if (string.IsNullOrEmpty(obj.Email))
            {
                Mensaje += "Es necesario el email del Doctor\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcd_doctores.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Doctores obj, out string Mensaje)
        {
            return objcd_doctores.Eliminar(obj, out Mensaje);
        }
    }
}

