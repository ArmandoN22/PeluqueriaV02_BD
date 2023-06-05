using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class L_Clientes
    {
        public static DataTable Mostrar(string cTexto)
        {
            D_Clientes Datos = new D_Clientes();
            return Datos.Mostrar(cTexto);
        }

        public static string Guardar(E_Clientes oCl)
        {
            D_Clientes Datos = new D_Clientes();
            if (Exist(oCl))
            {
                return "ID Repetido";
            }
            else
            {
                return Datos.Guardar(oCl);
            }

        }


        public static bool Exist(E_Clientes oCl)
        {
            try
            {
                DataTable dataTable = Mostrar("%");

                foreach (DataRow row in dataTable.Rows)
                {
                    int id = Convert.ToInt32(row[0]);

                    if (id == oCl.Id)
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

        public static string Actualizar(E_Clientes cliente_new, string id_cliente)
        {
            int Cliente_old;
            DataTable dataTable = Mostrar("%");
            DataRow[] rows = dataTable.Select($"CEDULA = '{id_cliente}'");

            if (dataTable.Rows.Count == 0)
            {
                return "Lista vacía";
            }
            else if (rows.Length == 0)
            {
                return "No se encontró el ID";
            }
            else if (Exist(cliente_new) && cliente_new.Id != int.Parse(id_cliente))
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

                Cliente_old = int.Parse(id_cliente);
                D_Clientes Datos = new D_Clientes();
                return Datos.Actualizar(cliente_new, Cliente_old);
            }

                

        }

        public static string Eliminar(int cedula)
        {
            D_Clientes Datos = new D_Clientes();
            return Datos.Eliminar(cedula);
        }


    }
}
