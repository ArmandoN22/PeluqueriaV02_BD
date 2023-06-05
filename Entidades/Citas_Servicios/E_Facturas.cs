using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Facturas
    {
        public int Id_Factura { get; set; }
        public DateTime Fecha_Fa { get; set; }
        public int Id_Cita { get; set; }
        public int Id_Cliente { get; set; }
        public float Total_Factura { get; set; }
    }
}
