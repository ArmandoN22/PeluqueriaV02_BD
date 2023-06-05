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
    public class D_Factura
    {
        public string CrearFactura(int idCi)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("CrearFactura", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("p_IdCita", OracleDbType.Int32).Value = idCi;
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

        public DataTable Mostrar()
        {
            OracleDataReader Resultado;
            DataTable tabla = new DataTable();
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                //cTexto = "%" + cTexto + "%";
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("select * from VistaFacturas", SqlCon);
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

        public List<E_Servicios> MostrarServicios(string idCita)
        {
            //OracleDataReader Resultado;
            //DataTable tabla = new DataTable();
            DataTable tabla = new DataTable();
            decimal valor;
            List<E_Servicios> servicios = new List<E_Servicios>();
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                //cTexto = "%" + cTexto + "%";
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("SELECT s.ID_SERVICIO, s.DESCRIPCION, s.PRECIO " +
                                          "FROM FACTURAS f " +
                                          "JOIN CITAS_SERVICIOS cs ON f.IDCITA = cs.IDCITA " +
                                          "JOIN SERVICIOS s ON cs.IDSERVICIO = s.ID_SERVICIO " +
                                          "WHERE f.IDCITA = '"+ idCita +"'", SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();
                var Resultado = Comando.ExecuteReader();
                tabla.Load(Resultado);

                //OracleDataAdapter adapter = new OracleDataAdapter(Comando);
                //DataTable dataTable = new DataTable();
                //adapter.Fill(dataTable);

                if (tabla.Rows.Count > 0)
                {
                    foreach (DataRow row in tabla.Rows)
                    {
                        E_Servicios servicio = new E_Servicios();
                        servicio.Id_Servicio = Convert.ToInt32(row["ID_SERVICIO"]);
                        servicio.Nombre = row["DESCRIPCION"].ToString();
                        valor = Convert.ToDecimal(row["PRECIO"]);
                        servicio.Precio = Convert.ToSingle(valor);
                        servicios.Add(servicio);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return servicios;
        }

        public string Eliminar(int idCi)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();


                OracleCommand Comando = new OracleCommand("USP_ELIMINAR_FA", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pId_factura", OracleDbType.Int32).Value = idCi;
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
