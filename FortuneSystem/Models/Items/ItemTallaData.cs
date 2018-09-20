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

        //Muestra la lista de tallas por estilo
        public IEnumerable<ItemTalla> ListaTallasPorEstilo(int? id)
        {
            List<ItemTalla> listTallas = new List<ItemTalla>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Lista_Tallas_Por_Estilo";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", id);
            leer = comando.ExecuteReader();

            while (leer.Read())
            {
                ItemTalla tallas = new ItemTalla()
                {
                    Talla = leer["TALLA"].ToString(),
                    Cantidad = Convert.ToInt32(leer["CANTIDAD"]),
                    Extras = Convert.ToInt32(leer["EXTRAS"]),
                    Ejemplos= Convert.ToInt32(leer["EJEMPLOS"]),
                    Estilo = leer["ITEM_STYLE"].ToString()

                };

                listTallas.Add(tallas);
            }
            leer.Close();
            conn.CerrarConexion();

            return listTallas;
        }



    }
}