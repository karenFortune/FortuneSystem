using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FortuneSystem.Models.Recibos
{
    public class DatosRecibos
    {
        private Conexion conn = new Conexion();
        private SqlCommand comando = new SqlCommand();
        private SqlDataReader leerFilas = null;
        public int total_unidades,total_pendientes,total_recibidas;
        public string fecha, usuario, id_po, lista_po_autocompletado;

        public string Llenar_lista_autocompletado() {
            comando.Connection = conn.AbrirConexion();
            try{
                comando.CommandText = "SELECT PO FROM PEDIDO  ";
                leerFilas = comando.ExecuteReader();
                while (leerFilas.Read()){
                    if (string.IsNullOrEmpty(lista_po_autocompletado)){
                        lista_po_autocompletado += "\"" + leerFilas["PO"] + "\"";
                    }else {
                        lista_po_autocompletado += ", \"" + leerFilas["PO"] + "\"";
                    }
                }
            }finally { conn.CerrarConexion(); }
            return lista_po_autocompletado;
        }

        public void Registrar_alta(string po,int tr){
            id_po = po.ToUpper();
            total_recibidas = tr;
            fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            total_unidades = obtener_total_unidades_po(po);
            total_pendientes = total_unidades - total_recibidas;
            Alta();
        }

        public int obtener_total_unidades_po(string po) {
            comando.Connection = conn.AbrirConexion();
            try{
                comando.CommandText = "SELECT TOTAL_UNITS FROM PEDIDO WHERE PO='"+po+"' ";
                leerFilas = comando.ExecuteReader();
                while (leerFilas.Read()) {
                    return Convert.ToInt32(leerFilas["TOTAL_UNITS"]);
                }
            }finally { conn.CerrarConexion(); }
            return 0;
        }

        public void Alta(){
            comando.Connection = conn.AbrirConexion();
            try{
                comando.CommandText = "INSERT INTO  recibos (id_po,fecha,usuario,total_recibidas,total_pendientes) " +
                    " VALUES('" + id_po + "','" + fecha + "','2','" + total_recibidas + "','"+total_pendientes+"')";
                comando.ExecuteNonQuery();
            }finally { conn.CerrarConexion(); }
        }
        public string lista;

        public string llenar_lista_po_summary(string po) {
            comando.Connection = conn.AbrirConexion();
            try{
                comando.CommandText = "SELECT PS.ID_PO_SUMMARY FROM PO_SUMMARY PS, PO P WHERE P.ID_PEDIDO=PS.ID_PEDIDO AND P.PO='" + po + "' ";
                leerFilas = comando.ExecuteReader();
                while (leerFilas.Read()){
                     lista=+Convert.ToString(leerFilas["ID_PO_SUMMARY"])+"*";
                }
            }finally { conn.CerrarConexion(); }
            return lista;
        }

    }
}