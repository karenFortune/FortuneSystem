﻿using FortuneSystem.Models.Roles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Usuarios
{
    public class CatUsuarioData
    {

        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leerFilas = null;
        int numero = 0;

        //Muestra la lista de Usuarios 
        public IEnumerable<CatUsuario> ListaUsuarios()
        {
            List<CatUsuario> listUsuarios = new List<CatUsuario>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Usuarios";
            comando.CommandType = CommandType.StoredProcedure;
            leerFilas = comando.ExecuteReader();

            while (leerFilas.Read())
            {
                CatUsuario usuarios = new CatUsuario();
                CatRoles roles = new CatRoles();
                usuarios.Id = Convert.ToInt32(leerFilas["Id"]);
                usuarios.NoEmpleado = Convert.ToInt32(leerFilas["NoEmpleado"]);
                usuarios.Nombres = leerFilas["Nombres"].ToString();
                usuarios.Apellidos = leerFilas["Apellidos"].ToString();
                usuarios.Cargo = Convert.ToInt32(leerFilas["Cargo"]);
                usuarios.Email = leerFilas["Email"].ToString();
                usuarios.Contrasena = leerFilas["Contrasena"].ToString();
                roles.Rol= leerFilas["rol"].ToString();
                usuarios.CatRoles = roles;
                listUsuarios.Add(usuarios);
           
            }
            leerFilas.Close();
            conn.CerrarConexion();

            return listUsuarios;
        }
        
        //Permite crear un nuevo usuario
        public void AgregarUsuarios(CatUsuario usuarios)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "AgregarUsuarios";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@NoEmpleado", usuarios.NoEmpleado);
            comando.Parameters.AddWithValue("@Nombres", usuarios.Nombres);
            comando.Parameters.AddWithValue("@Apellidos", usuarios.Apellidos);
            comando.Parameters.AddWithValue("@Cargo", usuarios.Cargo);
            comando.Parameters.AddWithValue("@Email", usuarios.Email);
            comando.Parameters.AddWithValue("@Contrasena", usuarios.Contrasena);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }

        //Permite consultar los detalles de un Usuario
        public CatUsuario ConsultarListaUsuarios (int? id)
        {
            CatUsuario usuarios = new CatUsuario();

            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Lista_Usuario_Por_Id";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", id);

            leerFilas = comando.ExecuteReader();
            while (leerFilas.Read())
            {
                
                usuarios.Id = Convert.ToInt32(leerFilas["Id"]);
                usuarios.NoEmpleado = Convert.ToInt32(leerFilas["NoEmpleado"]);
                usuarios.Nombres = leerFilas["Nombres"].ToString();
                usuarios.Apellidos = leerFilas["Apellidos"].ToString();
                usuarios.Cargo = Convert.ToInt32(leerFilas["Cargo"]);
                usuarios.Email = leerFilas["Email"].ToString();
                usuarios.Contrasena = leerFilas["Contrasena"].ToString();

            }
            return usuarios;

        }

        //Permite actualiza la informacion de un usuario
        public void ActualizarUsuarios(CatUsuario usuarios)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Actualizar_Usuarios";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", usuarios.Id);
            comando.Parameters.AddWithValue("@Nombres", usuarios.Nombres);
            comando.Parameters.AddWithValue("@NoEmpleado", usuarios.NoEmpleado);
            comando.Parameters.AddWithValue("@Apellidos", usuarios.Apellidos);
            comando.Parameters.AddWithValue("@Cargo", usuarios.Cargo);
            comando.Parameters.AddWithValue("@Email", usuarios.Email);
            comando.Parameters.AddWithValue("@Contrasena", usuarios.Contrasena);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();
        }

        //Permite eliminar la informacion de un usuario
        public void EliminarUsuario(int? id)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "EliminarUsuarios";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", id);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();
        }


    }
}