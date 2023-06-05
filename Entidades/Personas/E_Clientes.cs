using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class E_Clientes : E_Persona
    {
        public E_Clientes()
        {
        }

        public E_Clientes(E_Clientes cliente)
        {
            cliente.Id = Id;
            cliente.Nombre = Nombre;
            cliente.Apellido = Apellido;
            cliente.Telefono = Telefono;
            cliente.Correo = Correo;
        }

        public E_Clientes(int id, string nombre, string apellido, string telefono, string correo) : base(id, nombre, apellido, telefono)
        {
            Correo = correo;
        }

        public string Correo { get; set; }
    }
}
