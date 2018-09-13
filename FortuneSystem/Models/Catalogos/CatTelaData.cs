using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Catalogos
{
    public class CatTelaData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;


        public IEnumerable<CatTela> ListaTela()
        {
            List<CatTela> listTela = new List<CatTela>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Tela";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();

            while (leer.Read())
            {
                CatTela tela = new CatTela()
                {
                    Id_Tela = Convert.ToInt32(leer["ID"]),
                    Tela = leer["FABRIC"].ToString(),
                    EstiloTela = leer["CODE"].ToString()
                };

                listTela.Add(tela);
            }
            leer.Close();
            conn.CerrarConexion();

            return listTela;
        }
    }
}