using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Catalogos
{
    public class CatGeneroData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;

        //Muestra la lista de genero
        public IEnumerable<CatGenero> ListaGeneros()
        {
            List<CatGenero> listGenero = new List<CatGenero>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Genero";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();

            while (leer.Read())
            {
                CatGenero generos = new CatGenero()
                {
                    IdGender = Convert.ToInt32(leer["ID_GENDER"]),
                    Genero = leer["GENERO"].ToString()
                };

                listGenero.Add(generos);
            }
            leer.Close();
            conn.CerrarConexion();

            return listGenero;
        }

        //Permite crear nuevo genero
        public void AgregarGenero(CatGenero generos)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "AgregarGeneros";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Genero", generos.Genero);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();

        }

        //Permite consultar los detalles de un genero
        public CatGenero ConsultarListaGenero(int? id)
        {
            CatGenero generos = new CatGenero();

            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Genero_Por_Id";
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@Id", id);

            leer = comando.ExecuteReader();
            while (leer.Read())
            {

                generos.IdGender = Convert.ToInt32(leer["ID_GENDER"]);
                generos.Genero = leer["GENERO"].ToString();

            }
            return generos;

        }

        //Permite actualiza la informacion de un genero
        public void ActualizarGenero(CatGenero generos)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Actualizar_Genero";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", generos.IdGender);
            comando.Parameters.AddWithValue("@Genero", generos.Genero);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();
        }

        //Permite eliminar la informacion de un genero
        public void EliminarGenero(int? id)
        {
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "EliminarGenero";
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.AddWithValue("@Id", id);

            comando.ExecuteNonQuery();
            conn.CerrarConexion();
        }


    }
}

