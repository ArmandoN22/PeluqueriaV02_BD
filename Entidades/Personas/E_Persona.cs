using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Persona
    {
        public E_Persona()
        {
        }

        public E_Persona(int id, string nombre, string apellido, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Telefono { get; set; }
    }
}
