using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.DescripcionItem
{
    public class DescripcionItemData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;

        public void AgregarItems(DescripcionItem items)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "AgregarItem";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Item", items.EstiloItem);
            comando.Parameters.AddWithValue("@Color", items.IdColor);
            comando.Parameters.AddWithValue("@Qty", items.Cantidad);
            comando.Parameters.AddWithValue("@Price", items.Precio);
            comando.Parameters.AddWithValue("@IdPedidos", items.Pedidos);
            
            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }
    }
}