using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace Datos
{
    public class ConexionBD
    {
        private static ConexionBD Con = null;

        public ConexionBD()
        {

        }
        //  CREAR CONEXION A LA BASE DE DATOSS
        public OracleConnection CrearConexion()
        {
            OracleConnection Cadena = new OracleConnection();
            try
            {
                Cadena.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=LAPTOP-O1445JB9)" +
                                          "(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=xepdb1)))" +
                                          ";User Id=PELUQUERIA;Password=1234;";
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }

        public static ConexionBD getInstancia()
        {
            if (Con == null)
            {
                Con = new ConexionBD();
            }
            return Con;
        }
    }
}
