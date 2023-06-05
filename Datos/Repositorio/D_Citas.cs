using Entidades;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Citas
    {
        int IdCi;

        public DataTable Mostrar(string cTexto)
        {
            OracleDataReader Resultado;
            DataTable tabla = new DataTable();
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                cTexto = "%" + cTexto + "%";
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("select * from VISTA_CITAS where ID_CLIENTE like '" + cTexto + "' ", SqlCon);
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

        public DataTable listado_em()
        {
            
            OracleDataReader Resultado;
            DataTable tabla = new DataTable();
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("SELECT Nombre || ' ' || Apellido AS NombreCompleto, Cedula FROM Empleados", SqlCon);
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

        public DataTable listado_sr()
        {

            OracleDataReader Resultado;
            DataTable tabla = new DataTable();
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("SELECT DESCRIPCION || ' ' || PRECIO AS NOMBREPRECIO,ID_SERVICIO, PRECIO FROM VISTA_SERVICIOS", SqlCon);
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

        public DataTable listar_cl(string cTexto)
        {
            OracleDataReader Resultado;
            DataTable tabla = new DataTable();
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                cTexto = "%" + cTexto + "%";
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("SELECT Nombre || ' ' || Apellido AS NombreCompleto, Cedula FROM Clientes where Cedula like '" + cTexto + "' ", SqlCon);
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

        public string Guardar(E_Citas oCi, List<int> serviciosSeleccionados)
        {
            string Rpta = "";
            string recibe;
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_GUARDAR_CI", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                //Comando.Parameters.Add("pId_Cita", OracleDbType.Int32).Value = oCi.Id_Cita;
                Comando.Parameters.Add("pId_Cita", OracleDbType.Int32).Direction = ParameterDirection.Output; // Parámetro de salida para el ID de la cita
                Comando.Parameters.Add("pFecha_cita", OracleDbType.Date).Value = oCi.FechaCita;
                Comando.Parameters.Add("pHora_cita", OracleDbType.Varchar2).Value = oCi.HoraCita;
                Comando.Parameters.Add("pId_Empleado", OracleDbType.Int32).Value = oCi.Id_Empleado;
                Comando.Parameters.Add("pId_Cliente", OracleDbType.Int32).Value = oCi.Id_Cliente;


                SqlCon.Open();
                Comando.ExecuteNonQuery();

                //Obtener el valor del ID de la cita generado
                recibe = Convert.ToString(Comando.Parameters["pId_Cita"].Value);
                IdCi = Convert.ToInt32(recibe);

                foreach (int servicio in serviciosSeleccionados)
                {
                    OracleCommand Comando2 = new OracleCommand("USP_GUARDAR_CS", SqlCon);
                    Comando2.CommandType = CommandType.StoredProcedure;
                    Comando2.Parameters.Add("pId_Cita_cs", OracleDbType.Int32).Value = IdCi;
                    int idSer = Convert.ToInt32(servicio);
                    Comando2.Parameters.Add("pId_Servicio_cs", OracleDbType.Int32).Value = idSer;
                    Comando2.ExecuteNonQuery();
                }
                

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

        public string Actualizar(E_Citas oCi, List<int> nuevosServicios)
        {
            string Rpta = "";
            string IdCi;
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("USP_ACTUALIZAR_CI", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pId_Cita", OracleDbType.Int32).Value = oCi.Id_Cita;
                Comando.Parameters.Add("pFecha_cita", OracleDbType.Date).Value = oCi.FechaCita;
                Comando.Parameters.Add("pHora_cita", OracleDbType.Varchar2).Value = oCi.HoraCita;
                Comando.Parameters.Add("pId_Empleado", OracleDbType.Int32).Value = oCi.Id_Empleado;
                Comando.Parameters.Add("pId_Cliente", OracleDbType.Int32).Value = oCi.Id_Cliente;


                SqlCon.Open();
                Comando.ExecuteNonQuery();

                IdCi = Convert.ToString(oCi.Id_Cita);
                List<string> serviciosAsociadosString = ObtenerIdsServiciosAsociados(IdCi);
                List<int> serviciosActuales = serviciosAsociadosString.ConvertAll(int.Parse);

                List<int> serviciosAgregar = nuevosServicios.Except(serviciosActuales).ToList();
                List<int> serviciosEliminar = serviciosActuales.Except(nuevosServicios).ToList();

                // Agregar los nuevos servicios

                if (serviciosAgregar.Count > 0)
                {
                    foreach (int idServicioAgregar in serviciosAgregar)
                    {
                        OracleCommand Comando2 = new OracleCommand("USP_GUARDAR_CS", SqlCon);
                        Comando2.CommandType = CommandType.StoredProcedure;
                        Comando2.Parameters.Add("pId_Cita_cs", OracleDbType.Int32).Value = IdCi;
                        //int idSer = Convert.ToInt32(servicio);
                        Comando2.Parameters.Add("pId_Servicio_cs", OracleDbType.Int32).Value = idServicioAgregar;
                        Comando2.ExecuteNonQuery();
                    }
                }
                else if (serviciosEliminar.Count > 0)
                {
                    // Eliminar los servicios que ya no se requieren
                    try
                    {
                        foreach (int idServicioEliminar in serviciosEliminar)
                        {
                            OracleCommand Comando3 = new OracleCommand("USP_ELIMINAR_CS2", SqlCon);
                            Comando3.CommandType = CommandType.StoredProcedure;
                            Comando3.Parameters.Add("pId_Cita_cs", IdCi);
                            Comando3.Parameters.Add("pId_Servicios_cs", idServicioEliminar);
                            Comando3.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }


                //Obtener el valor del ID de la cita generado
                //recibe = Convert.ToString(Comando.Parameters["pId_Cita"].Value);
                //IdCi = Convert.ToInt32(recibe);

                //foreach (int servicio in serviciosSeleccionados)
                //{
                //    OracleCommand Comando2 = new OracleCommand("USP_ACTUALIZAR_CS", SqlCon);
                //    Comando2.CommandType = CommandType.StoredProcedure;
                //    Comando2.Parameters.Add("pId_Cita_cs", OracleDbType.Int32).Value = oCi.Id_Cita;
                //    int idSer = Convert.ToInt32(servicio);
                //    Comando2.Parameters.Add("pId_Servicio_cs", OracleDbType.Int32).Value = idSer;
                //    Comando2.ExecuteNonQuery();
                //}

                //foreach (int servicio in serviciosSeleccionados)
                //{
                //    if (!serviciosActuales.Contains(servicio))
                //    {
                //        // Agregar el nuevo servicio a la tabla Citas_Servicios
                //        OracleCommand Comando2 = new OracleCommand("USP_GUARDAR_CS", SqlCon);
                //        Comando2.CommandType = CommandType.StoredProcedure;
                //        Comando2.Parameters.Add("pId_Cita_cs", OracleDbType.Int32).Value = oCi.Id_Cita;
                //        Comando2.Parameters.Add("pId_Servicio_cs", OracleDbType.Int32).Value = servicio;
                //        Comando2.ExecuteNonQuery();
                //    }
                //}

                // Agregar nuevos servicios y actualizar servicios existentes
                //foreach (int servicio in nuevosServicios)
                //{
                //    if (serviciosActuales.Contains(servicio))
                //    {
                //        // Es un servicio existente, ejecutar comando de actualización en la tabla Citas_Servicios
                //        //OracleCommand Comando2 = new OracleCommand("USP_ACTUALIZAR_CS", SqlCon);
                //        //Comando2.CommandType = CommandType.StoredProcedure;
                //        //Comando2.Parameters.Add("pId_Cita_cs", OracleDbType.Int32).Value = oCi.Id_Cita;
                //        //Comando2.Parameters.Add("pId_Servicio_cs", OracleDbType.Int32).Value = servicio;
                //        //Comando2.ExecuteNonQuery();
                //        OracleCommand Comando2 = new OracleCommand("USP_ACTUALIZAR_CS", SqlCon);
                //        Comando2.CommandType = CommandType.StoredProcedure;
                //        Comando2.Parameters.Add("pId_Cita_cs", OracleDbType.Int32).Value = oCi.Id_Cita;
                //        int idSer = Convert.ToInt32(servicio);
                //        Comando2.Parameters.Add("pId_Servicio_cs", OracleDbType.Int32).Value = idSer;
                //        Comando2.ExecuteNonQuery();
                //    }
                //    else
                //    {
                //        // Es un nuevo servicio, ejecutar comando de inserción en la tabla Citas_Servicios
                //        OracleCommand Comando3 = new OracleCommand("USP_GUARDAR_CS", SqlCon);
                //        Comando3.CommandType = CommandType.StoredProcedure;
                //        Comando3.Parameters.Add("pId_Cita_cs", OracleDbType.Int32).Value = oCi.Id_Cita;
                //        Comando3.Parameters.Add("pId_Servicio_cs", OracleDbType.Int32).Value = servicio;
                //        Comando3.ExecuteNonQuery();
                //    }
                //}




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

        public string Eliminar(int idCi)
        {
            string Rpta = "";
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                
                /*para poder borrar una instancia de la tabla citas primero debia borrar todas las
                citas_servicios que se relacionan con el id de la cita que queria borrar*/

                OracleCommand Comando2 = new OracleCommand("USP_ELIMINAR_CS", SqlCon);
                Comando2.CommandType = CommandType.StoredProcedure;
                Comando2.Parameters.Add("pId_Cita_cs", OracleDbType.Int32).Value = idCi;
                SqlCon.Open();
                Comando2.ExecuteNonQuery();

                OracleCommand Comando = new OracleCommand("USP_ELIMINAR_CI", SqlCon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("pId_Cita", OracleDbType.Int32).Value = idCi;
                //SqlCon.Open();
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

        public List<string> ObtenerIdsServiciosAsociados(string idCita)
        {
            List<string> serviciosAsociados = new List<string>();
            OracleConnection SqlCon = new OracleConnection();
            try
            {
                SqlCon = ConexionBD.getInstancia().CrearConexion();
                OracleCommand Comando = new OracleCommand("SELECT IDSERVICIO FROM Citas_Servicios WHERE IDCITA = '"+ idCita + "'", SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();

                //Comando.Parameters.Add("pIdCita", OracleDbType.Int32).Value = idCita;
                using (var reader = Comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string idServicio = reader.GetString(0);
                        serviciosAsociados.Add(idServicio);
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
            return serviciosAsociados;
        }



    }
}
