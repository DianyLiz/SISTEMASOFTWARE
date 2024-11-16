using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocios
{
    public class CN_Usuario
    {
        private CD_Usuario objcd_usuario = new CD_Usuario();

        public List<Usuario> Listar()
        {
            return objcd_usuario.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                Mensaje += "Es necesario el Nombre del usuario\n";
            }

            if (string.IsNullOrEmpty(obj.Contraseña))
            {
                Mensaje += "Es necesario la contraseña del usuario\n";
            }

            if (string.IsNullOrEmpty(obj.Email))
            {
                Mensaje += "Es necesario el email del usuario\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }
            else
            {
                return objcd_usuario.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }

            if (string.IsNullOrEmpty(obj.Contraseña))
            {
                Mensaje += "Es necesario la contraseña del usuario\n";
            }

            if (string.IsNullOrEmpty(obj.Email))
            {
                Mensaje += "Es necesario el email del usuario\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcd_usuario.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Usuario obj, out string Mensaje)
        {
            return objcd_usuario.Eliminar(obj, out Mensaje);
        }
    }
}
