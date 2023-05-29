using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class L_Citas
    {
        public static DataTable listar_em()
        {
            D_Citas Datos = new D_Citas();
            return Datos.listado_em();
        }

        public static DataTable listar_cl(string cTexto)
        {
            D_Citas Datos = new D_Citas();
            return Datos.listar_cl(cTexto);
        }

        public static DataTable Mostrar(string cTexto)
        {
            D_Citas Datos = new D_Citas();
            return Datos.Mostrar(cTexto);
        }
    }
}
