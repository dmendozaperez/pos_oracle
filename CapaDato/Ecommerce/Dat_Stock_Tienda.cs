using CapaEntidad.Ecommerce;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Ecommerce
{
    public class Dat_Stock_Tienda
    {
        /// <summary>
        /// return stock de tienda
        /// </summary>
        /// <param name="cod_tda"></param>
        /// <param name="talla"></param>
        /// <param name="ubigeo"></param>
        /// <returns></returns>
        public Ent_Stock_Lista get_stock_tienda(string cod_art,string talla,string ubigeo="-1")
        {
            string sqlquery = "[USP_GET_STK_ARTICULO]";
            Ent_Stock_Lista result = null;
            List<Ent_Stock_Tienda> lista = null;
            Ent_Stock_Tienda_Acceso valida_msg = null;
            try
            {
                valida_msg = new Ent_Stock_Tienda_Acceso();
                result = new Ent_Stock_Lista();
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@COD_ART", cod_art);
                            cmd.Parameters.AddWithValue("@TALLA", talla);
                            cmd.Parameters.AddWithValue("@COD_UBI", ubigeo);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                lista = new List<Ent_Stock_Tienda>();
                                lista = (from DataRow dr in dt.Rows
                                         select new Ent_Stock_Tienda()
                                         {
                                             cod_tda = dr["COD_TDA"].ToString(),
                                             des_tda= dr["DES_TDA"].ToString(),
                                             ubigeo_tda= dr["UBIGEO_TDA"].ToString(),
                                             direccion_tda= dr["DIRECCION_TDA"].ToString(),
                                             cod_art= dr["COD_ART"].ToString(),
                                             talla= dr["TALLA"].ToString(),
                                             cantidad=Convert.ToInt32(dr["CANTIDAD"]),
                                         }
                                       ).ToList();

                                result.lista_stk_tda = lista.ToArray();
                                valida_msg.estado = "0";
                                valida_msg.descripcion = "consulta satisfactoria";
                                result.valida = valida_msg;
                            }
                        }

                    }
                    catch (Exception exc)
                    {
                        valida_msg.estado = "-1";
                        valida_msg.descripcion = exc.Message;
                        result.valida = valida_msg;
                        
                    }
                    if (cn!= null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception exc)
            {
                valida_msg.estado = "-1";
                valida_msg.descripcion = exc.Message;
                result.valida = valida_msg;
            }
            return result;
        }
    }
}
