using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models
{
    public class Conexion
    {
        private SqlConnection conn = new SqlConnection("Server=W_KAREN;Database=FortuneTest;Integrated Security =true");

        public SqlConnection AbrirConexion()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
               
            }
            return conn;
        }

        public SqlConnection CerrarConexion()
        {
            if(conn.State == ConnectionState.Closed)
            {
                conn.Close();
            }
            return conn;
        }
    }
}