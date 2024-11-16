using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisCitasMedicas.Utilidades
{
    public class OpcionCombo
    {
        public int Valor { get; set; }
        public string Texto { get; set; }
        public override string ToString()
        {
            return Texto;
        }
    }
}
