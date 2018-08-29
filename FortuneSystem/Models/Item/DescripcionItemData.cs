using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.DescripcionItem
{
    public class DescripcionItemData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;

        public void AgregarPO(Pedido ordenCompra)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "AgregarPedido";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@idPO", ordenCompra.PO);
            comando.Parameters.AddWithValue("@idPOF", ordenCompra.VPO);
            comando.Parameters.AddWithValue("@Customer", ordenCompra.Cliente);
            comando.Parameters.AddWithValue("@CustomerF", ordenCompra.ClienteFinal);
            comando.Parameters.AddWithValue("@datecancel", ordenCompra.FechaCancel);
            comando.Parameters.AddWithValue("@datePO", ordenCompra.FechaOrden);
            comando.Parameters.AddWithValue("@totUnid", ordenCompra.TotalUnidades);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }
    }
}