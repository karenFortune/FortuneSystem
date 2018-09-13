using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Item
{
    public class ItemTallaData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;
            
       public void RegistroTallas(ItemTalla tallas)
        {
            comando.Connection = conn.AbrirConexion();
            try
            {
                comando.CommandText = "INSERT INTO  ITEM_SIZE (TALLA_ITEM,CANTIDAD,EXTRAS,EJEMPLOS,ID_SUMMARY) " +
                    " VALUES((SELECT ID FROM CAT_ITEM_SIZE WHERE TALLA ='"+ tallas.Talla + "'),'" + tallas.Cantidad + "','"+ tallas.Extras + "','" + tallas.Ejemplos + "','" + tallas.IdSummary + "')";
                comando.ExecuteNonQuery();
            }
            finally { conn.CerrarConexion(); }
        }



    }
}