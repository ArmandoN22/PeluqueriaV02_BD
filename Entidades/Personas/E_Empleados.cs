using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Empleados : E_Persona
    {
        public E_Empleados()
        {
        }


        public E_Empleados(int id, string nombre, string apellido, string telefono, DateTime fechaContratacion, double salario) : base(id, nombre, apellido, telefono)
        {
            FechaContratacion = fechaContratacion;
            Salario = salario;
        }

        public DateTime FechaContratacion { get; set; }
        public double Salario { get; set; }
    }
}
