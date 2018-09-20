using FortuneSystem.Models.Catalogos;
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
        int IdPedido;

        //Muestra la lista de PO
        public IEnumerable<OrdenesCompra> ListaOrdenCompra()
        {
            List<OrdenesCompra> listPedidos = new List<OrdenesCompra>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Pedidos";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();

            while (leer.Read())
            {
             
                OrdenesCompra pedidos = new OrdenesCompra()
                {
                    IdPedido = Convert.ToInt32(leer["ID_PEDIDO"]),
                    PO = leer["PO"].ToString(),
                    VPO = Convert.ToInt32(leer["VPO"]),
                    Cliente = Convert.ToInt32(leer["CUSTOMER"]),
                    ClienteFinal = Convert.ToInt32(leer["CUSTOMER_FINAL"]),
                    FechaCancel = Convert.ToDateTime(leer["DATE_CANCEL"]),
                    FechaOrden = Convert.ToDateTime(leer["DATE_ORDER"]),
                    TotalUnidades = Convert.ToInt32(leer["TOTAL_UNITS"]),
                    IdStatus = Convert.ToInt32(leer["ID_STATUS"])

                };
                CatStatus status = new CatStatus()
                {
                    Estado = leer["ESTADO"].ToString()
                };
                CatClienteFinal clienteFinal = new CatClienteFinal()
                {
                    NombreCliente = leer["NAME"].ToString()
                };

                pedidos.CatStatus = status;
                pedidos.CatClienteFinal = clienteFinal;
   

                listPedidos.Add(pedidos);
            }
            leer.Close();
            conn.CerrarConexion();

            return listPedidos;
        }

        //Permite consultar los detalles de un PO
        public OrdenesCompra ConsultarListaPO(int? id)
        {
            OrdenesCompra pedidos = new OrdenesCompra();

            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Lista_Pedido_Por_Id";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", id);

            leer = comando.ExecuteReader();
            while (leer.Read())
            {

                pedidos.PO = leer["PO"].ToString();
                pedidos.VPO = Convert.ToInt32(leer["VPO"]);

                pedidos.Cliente = Convert.ToInt32(leer["CUSTOMER"]);
                pedidos.ClienteFinal = Convert.ToInt32(leer["CUSTOMER_FINAL"]);
                pedidos.FechaCancel = Convert.ToDateTime(leer["DATE_CANCEL"]);
                pedidos.FechaOrden = Convert.ToDateTime(leer["DATE_ORDER"]);
                pedidos.TotalUnidades = Convert.ToInt32(leer["TOTAL_UNITS"]);
                pedidos.IdStatus = Convert.ToInt32(leer["ID_STATUS"]);

            }
            return pedidos;

        }


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

        //Permite actualiza la informacion de un PO
        public void ActualizarPedidos(OrdenesCompra ordenCompra)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Actualizar_Pedido";
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

        public int Obtener_Utlimo_po() {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            Conexion conex = new Conexion();
            try
            {
                cmd.Connection = conex.AbrirConexion();
                cmd.CommandText = "SELECT ID_PEDIDO FROM PEDIDO WHERE ID_PEDIDO = (SELECT MAX(ID_PEDIDO) FROM PEDIDO) ";
                cmd.CommandType = CommandType.Text;
                reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    return Convert.ToInt32(reader["ID_PEDIDO"]);
                }
                conex.CerrarConexion();
            }
            finally { conex.CerrarConexion(); }
            return 0;
        }      

       
    }
}