using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.POSummary
{
    public class DescripcionItemData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;

        //Muestra la lista de PO Summary
      /*  public IEnumerable<POSummary> ListaPOSummary()
        {
            List<POSummary> listSummary = new List<POSummary>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Usuarios";
            comando.CommandType = CommandType.StoredProcedure;
            leerFilas = comando.ExecuteReader();

            while (leerFilas.Read())
            {
                POSummary ItemSummary = new POSummary();
                CatRoles roles = new CatRoles();
                usuarios.Id = Convert.ToInt32(leerFilas["Id"]);
                usuarios.NoEmpleado = Convert.ToInt32(leerFilas["NoEmpleado"]);
                usuarios.Nombres = leerFilas["Nombres"].ToString();
                usuarios.Apellidos = leerFilas["Apellidos"].ToString();
                usuarios.Cargo = Convert.ToInt32(leerFilas["Cargo"]);
                usuarios.Email = leerFilas["Email"].ToString();
                usuarios.Contrasena = leerFilas["Contrasena"].ToString();
                roles.Rol = leerFilas["rol"].ToString();
                usuarios.CatRoles = roles;
                listUsuarios.Add(usuarios);

            }
            leerFilas.Close();
            conn.CerrarConexion();

            return listUsuarios;
        }*/

        public void AgregarItems(POSummary items)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "AgregarItem";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Item", items.EstiloItem);
            comando.Parameters.AddWithValue("@Color", items.IdColor);
            comando.Parameters.AddWithValue("@Qty", items.Cantidad);
            comando.Parameters.AddWithValue("@Price", items.Precio);
            comando.Parameters.AddWithValue("@IdPedidos", items.PedidosId);
            
            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }
    }
}