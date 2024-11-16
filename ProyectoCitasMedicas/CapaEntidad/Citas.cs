using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Citas
    {
        public int IdCita { get; set; }
        public Pacientes oPaciente { get; set; }
        public Doctores oDoctor { get; set; }
        public DateTime FechaCita { get; set; }
        public string Motivo { get; set; }
        public bool Estado { get; set; }
        public Consultorios oConsultorio { get; set; }
    }
}
