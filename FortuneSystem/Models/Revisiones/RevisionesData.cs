using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Revisiones
{
    public class RevisionesData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;
        //Permite crear un nuevo PO
        public void AgregarRevisionesPO(Revisiones revision)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "AgregarPedido";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idPedido", revision.IdPedido);
            comando.Parameters.AddWithValue("@idPedidoRevision", revision.IdRevisionPO);
            comando.Parameters.AddWithValue("@dateRevision", revision.FechaRevision);
            comando.Parameters.AddWithValue("@idStatu", revision.IdStatus);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }
    }
}