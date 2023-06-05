using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Logica
{
    public class L_Servicios
    {

        public static DataTable Mostrar(string cTexto)
        {
            D_Servicios Datos = new D_Servicios();
            return Datos.Mostrar(cTexto);
        }

        public static string Guardar( E_Servicios oSe)
        {
            D_Servicios Datos = new D_Servicios();
            return Datos.Guardar(oSe);
        }

        public static string Actualizar(E_Servicios oSe)
        {
            D_Servicios Datos = new D_Servicios();
            return Datos.Actualizar(oSe);

        }

        public static string Eliminar(int IdSer)
        {
            D_Servicios Datos = new D_Servicios();
            return Datos.Eliminar(IdSer);
        }
    }
}
