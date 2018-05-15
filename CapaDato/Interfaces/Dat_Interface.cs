﻿using CapaEntidad;
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
        public DataTable get_tienda(Boolean _select_todos=false)
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
        public DataSet get_retail_location(string _cod_tda)
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
        public DataTable get_item_dimension_type()
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
        public DataTable get_item_dimension_value()
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
        public DataTable get_item()
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
        public DataTable get_price_update_2()
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
        public DataTable get_item_images()
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
        public DataTable get_item_xref()
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
        /// get stock de articulos
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="codtda"></param>
        /// <returns></returns>
        public DataTable get_stock_ledger(string fecha,string codtda)
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
        public DataTable get_inv_valid_destinations(string codtda)
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
        #endregion


    }
}