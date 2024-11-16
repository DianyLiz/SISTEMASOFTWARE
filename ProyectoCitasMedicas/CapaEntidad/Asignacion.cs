using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Asignacion
    {
        public int IdAsignacion { get; set; }
        public Doctores oDoctor { get; set; }
        public Consultorios oConsultorio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
