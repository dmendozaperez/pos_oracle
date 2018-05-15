using CapaEntidad.Logistica;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Logistica
{
    public class Dat_GuiasDespacho
    {
        /// <summary>
        /// get detalle de guias
        /// </summary>
        /// <param name="codigo de almacen"></param>
        /// <param name="numero de guia"></param>
        /// <returns></returns>
        private List<Ent_GuiasDespacho_Det> get_guias_tda_det(string cod_alm,string nro_guia)
        {
            List<Ent_GuiasDespacho_Det> listar_det = null;
            string sqlquery = "[USP_GET_GUIAS_TIENDA_DET]";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DESD_ALMAC", cod_alm);
                            cmd.Parameters.AddWithValue("@DESD_GUDIS", nro_guia);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                listar_det = new List<Ent_GuiasDespacho_Det>();
                                listar_det = (from DataRow dr in dt.Rows
                                              select new Ent_GuiasDespacho_Det
                                              {
                                                  art_cod = dr["ARTICULO"].ToString(),
                                                  art_tall = dr["TALLA"].ToString(),
                                                  art_par =Convert.ToInt32(dr["PARES"]),
                                              }).ToList();
                            }
                        }

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listar_det;
        }
        /// <summary>
        /// consulta de guias de despacho
        /// </summary>
        /// <param name="fecha incio"></param>
        /// <param name="fecha final"></param>
        /// <param name="codigo de tienda"></param>
        /// <returns></returns>
        public List<Ent_GuiasDespacho_Cab> get_guias_tda_cab(DateTime fecha_ini,DateTime fecha_fin,string cod_tda)
        {
            string sqlquery = "USP_GET_GUIAS_TIENDA_CAB";
            List<Ent_GuiasDespacho_Cab> listar = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fec_ini", fecha_ini);
                        cmd.Parameters.AddWithValue("@fec_fin", fecha_fin);
                        cmd.Parameters.AddWithValue("@cod_tda", cod_tda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<Ent_GuiasDespacho_Cab>();
                            listar = (from DataRow dr in dt.Rows
                                      select new Ent_GuiasDespacho_Cab()
                                      {
                                          cod_alm = dr["COD_ALM"].ToString(),
                                          nro_guia = dr["NROGUIA"].ToString(),
                                          nro_despacho = dr["NDESPACHO"].ToString(),
                                          cod_tda = dr["COD_TDA"].ToString(),
                                          des_tda = dr["DES_TDA"].ToString(),
                                          con_des = dr["CONCEPTO"].ToString(),
                                          fec_des = Convert.ToDateTime(dr["FECHA_DESP"])
                                      }).ToList();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listar;
        }


        /// <summary>
        /// get de articulo por talla y pares
        /// </summary>
        /// <returns></returns>
        public List<Ent_Guias_Articulo> get_guia_articulo_pares(string cod_alm, string nro_guia)
        {
            List<Ent_GuiasDespacho_Det> list_guia_det = null;
            List<Ent_Guias_Articulo> list_art = null;
            List<Ent_Guias_Tallas> list_art_talla = null;
            try
            {
                list_art = new List<Ent_Guias_Articulo>();
                list_guia_det = get_guias_tda_det(cod_alm, nro_guia);

                if (list_guia_det!=null)
                {
                    list_art = new List<Ent_Guias_Articulo>();                   
                    foreach(var item_art in list_guia_det.GroupBy(p=>p.art_cod))
                    {
                        Ent_Guias_Articulo art_cab = new Ent_Guias_Articulo();
                        art_cab.articulo = item_art.Key.ToString();
                        list_art_talla = new List<Ent_Guias_Tallas>();
                        foreach (var item_art_tall in list_guia_det.Where(t=>t.art_cod==item_art.Key.ToString()))
                        {
                            Ent_Guias_Tallas art_talla = new Ent_Guias_Tallas();
                            art_talla.talla = item_art_tall.art_tall.ToString();
                            art_talla.pares = item_art_tall.art_par;
                            list_art_talla.Add(art_talla);
                        }
                        art_cab.total_pares = list_guia_det.Where(b => b.art_cod == item_art.Key.ToString()).Sum(efe => efe.art_par);

                        art_cab.list_tallas = list_art_talla;

                        list_art.Add(art_cab);
                    }

                }

            }
            catch (Exception)
            {
                throw;
                
            }
            return list_art;
        }

    }
}
