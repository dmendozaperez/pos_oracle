using CapaEntidad;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Interfaces
{
    public class Dat_Interface
    {
        #region<LISTA DE TIENDA>
        public DataTable get_tienda(string pais,Boolean _select_todos=false)
        {
            DataTable dt = null;
            string sqlquery = "[USP_GET_XSTORE_Tienda]";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            if (_select_todos)
                            { 
                                dt.Columns.Add("cod_entid", typeof(string));
                                dt.Columns.Add("des_entid", typeof(string));
                                dt.Rows.Add("-1", "---SELECCIONAR TODOS---");
                            }
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception)
            {
                dt=null;
            }
            return dt;
        }
        #endregion

        #region<INTERFACES XSTORE>
        /// <summary>
        /// get de maestros tienda
        /// </summary>
        /// <param name="_cod_tda"></param>
        /// <returns></returns>
        public DataSet get_retail_location(string _cod_tda, string pais)
        {
            string sqlquery = "USP_XSTORE_GET_RETAIL_LOCATION";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", _cod_tda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                            ds.Tables[0].TableName = "RETAIL_LOCATION";
                            ds.Tables[1].TableName = "RETAIL_LOCATION_PROPERTY";

                        }
                    }
                }
            }
            catch (Exception)
            {
                ds = null;                
            }
            return ds;
        }
        /// <summary>
        /// get de la regla de medida - talla
        /// </summary>
        /// <returns></returns>
        public DataTable get_item_dimension_type(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_DIMENSION_TYPE";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                       
                    }
                }
            }
            catch (Exception)
            {
                dt = null;                
            }
            return dt;
        }
        /// <summary>
        /// get de la talla - bata
        /// </summary>
        /// <returns></returns>
        public DataTable get_item_dimension_value(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_DIMENSION_VALUE";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@CODTIENDA", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get maestros de articulos
        /// </summary>
        /// <returns></returns>
        public DataTable get_item(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@CODTIENDA", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get precios de articulos
        /// </summary>
        /// <returns></returns>
        public DataTable get_price_update_2(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_PRICE_UPDATE_2";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@CODTIENDA", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get imagenes de articulos
        /// </summary>
        /// <returns></returns>
        public DataTable get_item_images(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_IMAGES";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@CODTIENDA", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get codigo de barra
        /// </summary>
        /// <returns></returns>
        public DataTable get_item_xref(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_XREF";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@CODTIENDA", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception exc)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get ORG_HIER
        /// </summary>
        /// <returns></returns>
        public DataTable get_org_hier(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ORG_HIER";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@CODTIENDA", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get ORG_HIER
        /// </summary>
        /// <returns></returns>
        public DataTable get_merch_hier(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_MERCH_HIER";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@CODTIENDA", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get PARTY
        /// </summary>
        /// <returns></returns>
        public DataTable get_Party(string pais, string codtda, string strCodSupl, string strCodEmpl)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_PARTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@CODTIENDA", codtda);
                        cmd.Parameters.AddWithValue("@CODSPL", strCodSupl);
                        cmd.Parameters.AddWithValue("@CODEMPL", strCodEmpl);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get PARTY
        /// </summary>
        /// <returns></returns>
        public DataTable get_Location_Property(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_INV_LOCATION_PROPERTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get stock de articulos
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="codtda"></param>
        /// <returns></returns>
        public DataTable get_stock_ledger(string fecha,string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_STOCK_LEDGER";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fecha_stk", fecha);
                        cmd.Parameters.AddWithValue("@cod_tda", codtda); 
                        cmd.Parameters.AddWithValue("@PAIS", pais);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
        /// <summary>
        /// get transferencia de tienda a tienda
        /// </summary>
        /// <param name="codtda"></param>
        /// <returns></returns>
        public DataTable get_inv_valid_destinations(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_INV_VALID_DESTINATIONS";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable get_inv_valid_destinations_property(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_INV_VALID_DESTINATIONS_PROPERTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// Genera la interface de traspasos del cd hacia tda
        /// </summary>
        /// <param name="codigo de almacen"></param>
        /// <param name="numero de guia"></param>
        /// <returns></returns>
        public DataSet get_inv_doc(string cod_alm,string nro_guia)
        {
            string sqlquery = "[USP_XSTORE_GET_INV_DOC]";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DESC_ALMAC", cod_alm);
                        cmd.Parameters.AddWithValue("@DESC_GUDIS", nro_guia);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count>0)
                            {
                                /*guias de traspasos*/
                                ds.Tables[0].TableName = "INV_DOC";
                                ds.Tables[1].TableName = "INV_DOC_LINE_ITEM";
                                ds.Tables[2].TableName = "CARTON";
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                ds = null;                
            }
            return ds;
        }


        public DataTable get_county_city(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_COUNTY_CITY";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable get_electronic_correlatives(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_ELECTRONIC_CORRELATIVES";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable get_manual_correlatives(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_MANUAL_CORRELATIVES";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable get_state_county(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_STATE_COUNTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable get_variacion_precio(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_VARIACION_PRECIOS";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable get_tender_repository(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_TENDER_REPOSITORY";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable get_tender_repository_property(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_TENDER_REPOSITORY_PROPERTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cod_tda", codtda);
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }



        #endregion

        #region<DATOS_ORCE>
        public DataTable ItemMaintenance()
        {
            DataTable dt = null;
            string sqlquery = "USP_ORCE_ItemMaintenance";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                       using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable MerchandiseHierarch()
        {
            DataTable dt = null;
            string sqlquery = "USP_ORCE_MerchandiseHierarchyMaintenance";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        public DataTable OrcRetailLocations()
        {
            DataTable dt = null;
            string sqlquery = "USP_ORCE_RetailLocations";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();
                            da.Fill(dt);
                        }

                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }

        #endregion


    }
}
