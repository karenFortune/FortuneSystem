﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Catalogos
{
    public class CatClienteFinalData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;

        //Muestra la lista de clientes
        public IEnumerable<CatClienteFinal> ListaClientesFinal()
        {
            List<CatClienteFinal> listClientesFinal = new List<CatClienteFinal>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_ClientesFinal";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();

            while (leer.Read())
            {
                CatClienteFinal clientesFinal = new CatClienteFinal()
                {
                    CustomerFinal = Convert.ToInt32(leer["CUSTOMER_FINAL"]),
                    Nombre = leer["NAME"].ToString()
                };

                listClientesFinal.Add(clientesFinal);
            }
            leer.Close();
            conn.CerrarConexion();

            return listClientesFinal;
        }

        //Permite crear un nuevo cliente
        public void AgregarClientesFinal(CatClienteFinal clientesFinal)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "AgregarClienteFinal";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Nombre", clientesFinal.Nombre);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }

        //Permite consultar los detalles de un cliente
        public CatClienteFinal ConsultarListaClientesFinal(int? id)
        {
            CatClienteFinal clientesFinal = new CatClienteFinal();

            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_ClienteFinal_Por_Id";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", id);

            leer = comando.ExecuteReader();
            while (leer.Read())
            {

                clientesFinal.CustomerFinal = Convert.ToInt32(leer["CUSTOMER_FINAL"]);
                clientesFinal.Nombre = leer["NAME"].ToString();

            }
            return clientesFinal;

        }

        //Permite actualiza la informacion de un cliente
        public void ActualizarClienteFinal(CatClienteFinal clientesFinal)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Actualizar_ClienteFinal";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", clientesFinal.CustomerFinal);
            comando.Parameters.AddWithValue("@Nombre", clientesFinal.Nombre);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();
        }

        //Permite eliminar la informacion de un cliente
        public void EliminarClienteFinal(int? id)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "EliminarClientesFinal";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", id);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();
        }


    }
}


