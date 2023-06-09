﻿using Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Clientes
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
                OracleCommand Comando = new OracleCommand("select * from VISTA_CLIENTES where Cedula like '" + cTexto + "' ", SqlCon);
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

        public string Guardar(E_Clientes oSer)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_GUARDAR_CL", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pCedula", OracleDbType.Int32).Value = oSer.Id;
                Comando.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = oSer.Nombre;
                Comando.Parameters.Add("pApellido", OracleDbType.Varchar2).Value = oSer.Apellido;
                Comando.Parameters.Add("pTelefono", OracleDbType.Varchar2).Value = oSer.Telefono;
                Comando.Parameters.Add("pCorreo", OracleDbType.Varchar2).Value = oSer.Correo;
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

        public string Actualizar(E_Clientes oSer, int Cliente_old)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_ACTUALIZAR_CL", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pCliente_old", OracleDbType.Int32).Value = Cliente_old;
                Comando.Parameters.Add("pCedula", OracleDbType.Int32).Value = oSer.Id;
                Comando.Parameters.Add("pNombre", OracleDbType.Varchar2).Value = oSer.Nombre;
                Comando.Parameters.Add("pApellido", OracleDbType.Varchar2).Value = oSer.Apellido;
                Comando.Parameters.Add("pTelefono", OracleDbType.Varchar2).Value = oSer.Telefono;
                Comando.Parameters.Add("pCorreo", OracleDbType.Varchar2).Value = oSer.Correo;
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
                OracleCommand Comando = new OracleCommand("USP_ELIMINAR_CL", SqlCon);
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
