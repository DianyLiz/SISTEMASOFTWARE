using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Doctores
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public Especialidades oEspecialidad { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string HorarioAtencion { get; set; }
    }
}
