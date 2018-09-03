using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Items
{
    public class ItemDescripcionData
    {

        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leerFilas = null;

        //Permite crear un nuevo Item descripcion
        public void AgregarItemDescripcion(ItemDescripcion itemDesc)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Agregar_Item_Desc";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Style", itemDesc.ItemEstilo);
            comando.Parameters.AddWithValue("@Descripcion", itemDesc.Descripcion);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }


        public string Verificar_Item_CD(string cadena)
        {
            string texto = null;
            cadena = cadena.TrimEnd();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "VerificarItem";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Cadena", cadena);
            texto = (string)comando.ExecuteScalar();
            comando.Parameters.Clear();
            conn.CerrarConexion();
            if (texto != null) texto = texto.TrimEnd();
            return texto;
        }
    }
}