using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Citas
    {
        public int Id_Cita { get; set; }
        public DateTime FechaCita { get; set; }
        public string  HoraCita { get; set; }
        public int Id_Empleado { get; set; }
        public int Id_Cliente { get; set; }

    }
}
