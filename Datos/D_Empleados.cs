using Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Empleados
    {
        public DataTable Mostrar(string cTexto)
        {
            OracleDataReader Resultado;
            DataTable tabla = new DataTable();
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                cTexto = "%" + cTexto + "%";
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("select * from VISTA_EMPLEADOS where Cedula like '" + cTexto + "' ", SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();
                Resultado = Comando.ExecuteReader();
                tabla.Load(Resultado);
                return tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public string Guardar(E_Empleados oEm)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_GUARDAR_EM", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pCedula", OracleDbType.Int32).Value = oEm.Id;
                Comando.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = oEm.Nombre;
                Comando.Parameters.Add("pApellido", OracleDbType.Varchar2).Value = oEm.Apellido;
                Comando.Parameters.Add("pTelefono", OracleDbType.Varchar2).Value = oEm.Telefono;
                Comando.Parameters.Add("pFecha_co", OracleDbType.Date).Value = oEm.FechaContratacion;
                Comando.Parameters.Add("pSalario", OracleDbType.Decimal).Value = oEm.Salario;
                SqlCon.Open();
                Comando.ExecuteNonQuery();
                Rpta = "OK";


            }
            catch (Exception ex)
            {

                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string Actualizar(E_Empleados oEm, int Empleado_old)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_ACTUALIZAR_EM", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pEmpleado_old", OracleDbType.Int32).Value = Empleado_old;
                Comando.Parameters.Add("pCedula", OracleDbType.Int32).Value = oEm.Id;
                Comando.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = oEm.Nombre;
                Comando.Parameters.Add("pApellido", OracleDbType.Varchar2).Value = oEm.Apellido;
                Comando.Parameters.Add("pTelefono", OracleDbType.Varchar2).Value = oEm.Telefono;
                Comando.Parameters.Add("pFecha_co", OracleDbType.Date).Value = oEm.FechaContratacion;
                Comando.Parameters.Add("pSalario", OracleDbType.Decimal).Value = oEm.Salario;
                SqlCon.Open();
                Comando.ExecuteNonQuery();
                Rpta = "OK";


            }
            catch (Exception ex)
            {

                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }

        public string Eliminar(int cedula)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_ELIMINAR_EM", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pCedula", OracleDbType.Int32).Value = cedula;
                SqlCon.Open();
                Comando.ExecuteNonQuery();
                Rpta = "OK";
            }
            catch (Exception ex)
            {

                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return Rpta;
        }
    }
}
