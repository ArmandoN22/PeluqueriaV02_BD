using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos
{
    public class D_Servicios
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
                OracleCommand Comando = new OracleCommand("select * from VISTA_SERVICIOS where Descripcion like '" + cTexto + "' ", SqlCon);
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

        public string Guardar(E_Servicios oSer)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_GUARDAR_SR", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pId_servicio", OracleDbType.Int32).Value = oSer.Id_Servicio;
                Comando.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = oSer.Nombre;
                Comando.Parameters.Add("pPrecio", OracleDbType.Decimal).Value = oSer.Precio;
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

        public string Actualizar(E_Servicios oSer)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_ACTUALIZAR_SR", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pId_servicio", OracleDbType.Int32).Value = oSer.Id_Servicio;
                Comando.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = oSer.Nombre;
                Comando.Parameters.Add("pPrecio", OracleDbType.Decimal).Value = oSer.Precio;
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

        public string Eliminar(int idSer)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_ELIMINAR_SR", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pId_servicio", OracleDbType.Int32).Value = idSer;
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
