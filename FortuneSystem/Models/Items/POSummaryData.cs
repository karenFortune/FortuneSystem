﻿using System;
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
            comando.Parameters.AddWithValue("@IdGenero", items.IdGenero);
            comando.Parameters.AddWithValue("@IdTela", items.IdTela);
            comando.Parameters.AddWithValue("@TipoCamiseta", items.TipoCamiseta);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }

        public int Obtener_Utlimo_Item()
        {
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;
            try
            {
                cmd.Connection = conn.AbrirConexion();
                cmd.CommandText = "SELECT ID_PO_SUMMARY FROM PO_SUMMARY WHERE ID_PO_SUMMARY = (SELECT MAX(ID_PO_SUMMARY) FROM PO_SUMMARY) ";
                cmd.CommandType = CommandType.Text;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return Convert.ToInt32(reader["ID_PO_SUMMARY"]);
                }
                conn.CerrarConexion();
            }
            finally { conn.CerrarConexion(); }
            return 0;
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

        public string Verificar_Color_CD(string cadena)
        {
            string texto = null;
            cadena = cadena.TrimEnd();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "VerificarColor";
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