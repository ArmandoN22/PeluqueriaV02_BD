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

        public static DataTable listar_sr()
        {
            D_Citas Datos = new D_Citas();
            return Datos.listado_sr();
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

        public static List<float> ObtenerPrecios()
        {
            DataTable tablaServicios = listar_sr();
            List<float> preciosServicios = new List<float>();

            foreach (DataRow row in tablaServicios.Rows)
            {
                float precio = Convert.ToSingle(row["Precio"]);
                preciosServicios.Add(precio);
            }

            return preciosServicios;
        } //esta funcion le retornaba a FrmCrud_Citas una lista con los precios, pero no la utilizo ya que encontre otra solucion.

        public static string Guardar(E_Citas oCi, List<int> serviciosSeleccionados)
        {
            D_Citas Datos = new D_Citas();
            return Datos.Guardar(oCi, serviciosSeleccionados);
        }

        public static string Actualizar(E_Citas oCi, List<int> serviciosSeleccionados)
        {
            D_Citas Datos = new D_Citas();
            return Datos.Actualizar(oCi, serviciosSeleccionados);
        }

        public static string Eliminar(int IdCi)
        {
            D_Citas Datos = new D_Citas();
            return Datos.Eliminar(IdCi);
        }

        public  List<string> ObtenerIdsServiciosAsociados(string idCita)
        {

            D_Citas Datos = new D_Citas();
            return Datos.ObtenerIdsServiciosAsociados(idCita);
        }
        //private int ObtenerIdServicioSeleccionado(CheckedListBox clbServicios)
        //{
        //    if (clbServicios.SelectedItem != null)
        //    {
        //        // Obtener el valor seleccionado en función del ValueMember configurado
        //        object selectedValue = clbServicios.SelectedValue;
        //        int idServicio;
        //        if (selectedValue != null && int.TryParse(selectedValue.ToString(), out idServicio))
        //        {
        //            return idServicio;
        //        }
        //    }

        //    // No se ha seleccionado ningún servicio
        //    return -1; // O cualquier otro valor que indique que no hay selección

        //}
    }
}
