﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public  class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public Rol oRol { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
