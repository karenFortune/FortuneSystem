using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Pedidos
{
    public class PedidosData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;

        //Muestra la lista de PO
      /*  public IEnumerable<Pedido> ListaOrdenCompra()
        {
            List<Pedido> listPedidos = new List<Pedido>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "ListarPedidos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();

            while (leer.Read())
            {
                Pedido pedidos = new Pedido()
                {
                   IdPedido = Convert.ToInt32(leer["ID_PEDIDO"]),
                    PO = leer["PO"].ToString(),
                    VPO = Convert.ToInt32(leer["VPO"]),
                    Cliente = Convert.ToInt32(leer["CUSTOMER"]),
                    ClienteFinal = Convert.ToInt32(leer["CUSTOMER_FINAL"]),
                    FechaCancel = Convert.ToDateTime(leer["DATE_CANCEL"]),
                    FechaOrden = Convert.ToDateTime(leer["DATE_ORDER"]),
                    TotalUnidades = Convert.ToInt32(leer["TOTAL_UNITS"])
                };

                listPedidos.Add(pedidos);
            }
            leer.Close();
            conn.CerrarConexion();

            return listPedidos;
        }

        public DataTable ListarPedidos()
        {
            DataTable Tabla = new DataTable();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "ListarPedidos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();
            Tabla.Load(leer);
            leer.Close();
            conn.CerrarConexion();
            return Tabla;

        }*/

        //Permite crear un nuevo PO
        public void AgregarPO(OrdenesCompra ordenCompra)
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
            comando.Parameters.AddWithValue("@idStatus", ordenCompra.IdStatus);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }
    }
}