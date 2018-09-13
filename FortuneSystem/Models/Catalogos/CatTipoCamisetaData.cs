using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FortuneSystem.Models.Catalogos
{
    public class CatTipoCamisetaData
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leer = null;


        public IEnumerable<CatTipoCamiseta> ListaTipoCamiseta()
        {
            List<CatTipoCamiseta> listTipoCamiseta = new List<CatTipoCamiseta>();
            comando.Connection = conn.AbrirConexion();
            comando.CommandText = "Listar_Tipo_Camiseta";
            comando.CommandType = CommandType.StoredProcedure;
            leer = comando.ExecuteReader();

            while (leer.Read())
            {
                CatTipoCamiseta tipoC = new CatTipoCamiseta()
                {
                    IdTipo = Convert.ToInt32(leer["ID_TYPE_CODE"]),
                    TipoProducto = leer["PRODUCT_TYPE_CODE"].ToString(),
                    DescripcionTipo = leer["DESCRIPTION"].ToString(),
                    TipoGrupo = leer["GROUP_TYPE"].ToString()
                };

                listTipoCamiseta.Add(tipoC);
            }
            leer.Close();
            conn.CerrarConexion();

            return listTipoCamiseta;
        }
    }
}