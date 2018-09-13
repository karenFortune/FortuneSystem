using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Catalogos
{
    public class CatClienteData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leerCliente = null;

        //Muestra la lista de clientes
        public IEnumerable<CatCliente> ListaClientes()
        {
            List<CatCliente> listClientes = new List<CatCliente>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Clientes";
            comando.CommandType = CommandType.StoredProcedure;
            leerCliente = comando.ExecuteReader();

            while (leerCliente.Read())
            {
                CatCliente clientes = new CatCliente()
                {
                    Customer = Convert.ToInt32(leerCliente["CUSTOMER"]),
                    Nombre = leerCliente["NAME"].ToString()
                };  

                listClientes.Add(clientes);
            }
            leerCliente.Close();
            conn.CerrarConexion();

            return listClientes;
        }

        public IEnumerable<CatCliente> ListaClientes2()
        {
            SqlDataReader leerClienteP = null;
            List<CatCliente> listClientes = new List<CatCliente>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Clientes";
            comando.CommandType = CommandType.StoredProcedure;
            leerClienteP = comando.ExecuteReader();

            while (leerCliente.Read())
            {
                CatCliente clientes = new CatCliente()
                {
                    Customer = Convert.ToInt32(leerClienteP["CUSTOMER"]),
                    Nombre = leerClienteP["NAME"].ToString()
                };

                listClientes.Add(clientes);
            }
            leerClienteP.Close();
            conn.CerrarConexion();

            return listClientes;
        }

        //Permite crear un nuevo cliente
        public void AgregarClientes(CatCliente clientes)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "AgregarCliente";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Nombre", clientes.Nombre);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }

        //Permite consultar los detalles de un cliente
        public CatCliente ConsultarListaClientes(int? id)
        {
            CatCliente clientes = new CatCliente();

            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Cliente_Por_Id";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", id);

            leerCliente = comando.ExecuteReader();
            while (leerCliente.Read())
            {

                clientes.Customer= Convert.ToInt32(leerCliente["CUSTOMER"]);
                clientes.Nombre = leerCliente["NAME"].ToString();
       
            }
            return clientes;

        }

        //Permite actualiza la informacion de un cliente
        public void ActualizarCliente(CatCliente clientes)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Actualizar_Cliente";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", clientes.Customer);
            comando.Parameters.AddWithValue("@Nombre", clientes.Nombre);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();
        }

        //Permite eliminar la informacion de un cliente
        public void EliminarCliente(int? id)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "EliminarClientes";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", id);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();
        }


    }
}


