using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class L_Factura
    {
        public static DataTable Mostrar()
        {
            D_Factura Datos = new D_Factura();
            return Datos.Mostrar();
        }

        public static string CrearFactura(int IdCi)
        {
            D_Factura Datos = new D_Factura();
            return Datos.CrearFactura(IdCi);
        }
    }
}
