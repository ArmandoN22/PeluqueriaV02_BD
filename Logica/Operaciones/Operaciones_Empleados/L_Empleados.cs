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
    public class L_Empleados
    {
        public static DataTable Mostrar(string cTexto)
        {
            D_Empleados Datos = new D_Empleados();
            return Datos.Mostrar(cTexto);
        }

        public static string Guardar(E_Empleados oEm)
        {
            D_Empleados Datos = new D_Empleados();
            if (Exist(oEm))
            {
                return "ID Repetido";
            }
            else
            {
                return Datos.Guardar(oEm);
            }

        }

        public static bool Exist(E_Empleados oEm)
        {
            try
            {
                DataTable dataTable = Mostrar("%");

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row[0]);

                    if (id == oEm.Id)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string Actualizar(E_Empleados empleado_new, string id_empleado)
        {
            int Empleado_old;
            DataTable dataTable = Mostrar("%");
            DataRow[] rows = dataTable.Select($"CEDULA = '{id_empleado}'");

            if (dataTable.Rows.Count == 0)
            {
                return "Lista vacía";
            }
            else if (rows.Length == 0)
            {
                return "No se encontró el ID";
            }
            else if (Exist(empleado_new) && empleado_new.Id != int.Parse(id_empleado))
            {
                return "El cliente ingresado ya existe";
            }
            else
            {
                //E_Clientes cliente_old = new E_Clientes;
                //DataRow row = rows[0];
                //row["Id"] = cliente_new.Id;
                //row["Nombre"] = cliente_new.Nombre;
                //row["Apellido"] = cliente_new.Apellido;
                //row["Telefono"] = cliente_new.Telefono;
                //row["Correo"] = cliente_new.Correo;

                Empleado_old = int.Parse(id_empleado);
                D_Empleados Datos = new D_Empleados();
                return Datos.Actualizar(empleado_new, Empleado_old);
            }
        }

        public static string Eliminar(int cedula)
        {
            D_Empleados Datos = new D_Empleados();
            return Datos.Eliminar(cedula);
        }
    }
}
