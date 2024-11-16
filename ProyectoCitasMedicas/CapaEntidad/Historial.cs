using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Historial
    {
        public int IdHistorial { get; set; }
        public Pacientes oPaciente { get; set; }
        public Citas oCita { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public DateTime Fecha { get; set; }
    }
}
