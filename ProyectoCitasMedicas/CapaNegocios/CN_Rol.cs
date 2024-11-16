using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocios
{
    public  class CN_Rol
    {
        private CD_Rol objcd_rol = new CD_Rol();

        public List<Rol> Listar()
        {
            return objcd_rol.Listar();
        }

        public int Registrar(Rol obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.NombreRol))
            {
                Mensaje += "Es necesario el Nombre del Rol\n";
            }

            if (string.IsNullOrEmpty(obj.Descripcion))
            {
                Mensaje += "Es necesario la descripcion del rol\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return 0;
            }
            else
            {
                return objcd_rol.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(Rol obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.NombreRol))
            {
                Mensaje += "Es necesario el nombre del rol\n";
            }

            if (string.IsNullOrEmpty(obj.Descripcion))
            {
                Mensaje += "Es necesario la descripcion del rol\n";
            }

            if (!string.IsNullOrEmpty(Mensaje))
            {
                return false;
            }
            else
            {
                return objcd_rol.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(Rol obj, out string Mensaje)
        {
            return objcd_rol.Eliminar(obj, out Mensaje);
        }
    }
}

