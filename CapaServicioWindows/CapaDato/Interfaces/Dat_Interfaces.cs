﻿using CapaServicioWindows.Conexion;
using CapaServicioWindows.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.CapaDato.Interfaces
{
    public class Dat_Interfaces
    {

        public List<Ent_InterAuto_PL> lista_inter_pl(string pais)
        {
            List<Ent_InterAuto_PL> lista = null;
            string sqlquery = "USP_XSTORE_GET_PROC_AUTO";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PAIS", pais);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lista = new List<Ent_InterAuto_PL>();
                                while(dr.Read())
                                {
                                    Ent_InterAuto_PL inter = new Ent_InterAuto_PL();
                                    inter.inter_nom = dr["inter_nom"].ToString();
                                    inter.entorno = dr["entorno"].ToString();
                                    inter.usp_pl = dr["usp_pl"].ToString();
                                    lista.Add(inter);
                                }
                            }
                        }
                    }
                    catch 
                    {                        
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch 
            {
                lista = null;
            }
            return lista;
        }

        public List<Ent_InterGenera_PL> lista_inter_pl_genera(string pais)
        {
            List<Ent_InterGenera_PL> lista = null;
            string sqlquery = "USP_XSTORE_GET_GENERA_INTERFACE_WEB";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PAIS", pais);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                lista = new List<Ent_InterGenera_PL>();
                                while (dr.Read())
                                {
                                    Ent_InterGenera_PL inter = new Ent_InterGenera_PL();
                                    inter.int_cab_cod= dr["int_cab_cod"].ToString();
                                    inter.int_id= dr["int_id"].ToString();
                                    inter.int_nom = dr["int_nom"].ToString();

                                    inter.cod_tda = dr["cod_tda"].ToString();
                                    inter.pais = dr["pais"].ToString();
                                    inter.rut_gen = dr["rut_gen"].ToString();
                                    inter.pl_pe = dr["pl_pe"].ToString();
                                    inter.pl_ec = dr["pl_ec"].ToString();
                                    inter.entorno= dr["entorno"].ToString();

                                    lista.Add(inter);
                                }
                            }
                        }
                    }
                    catch
                    {
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch
            {
                lista = null;
            }
            return lista;
        }


        #region<GENERACION DE INTERFACES MNT>
        #region<INTERFACES PERU>
        #region<XOFICCE>
        public DataTable get_item_PE(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_item_dimension_type_PE(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_DIMENSION_TYPE";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_tender_repository_PE(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_TENDER_REPOSITORY";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataSet SET_XSTORE_VENTA_EXPORTAR_PE(string cod_tda, DateTime fechaIni, DateTime fechaFin)
        {
            string sqlquery = "USP_SET_XSTORE_VENTA_EXPORTAR";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                        cmd.Parameters.AddWithValue("@FEC_INI", fechaIni);
                        cmd.Parameters.AddWithValue("@FEC_FIN", fechaFin);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                            ds.Tables[0].TableName = "TRANS_LINE_TENDER";
                            ds.Tables[1].TableName = "TRANS_TAX";
                            ds.Tables[2].TableName = "TRANS_LINE_ITEM_TAX";
                            ds.Tables[3].TableName = "TRANS_LINE_ITEM";
                            ds.Tables[4].TableName = "TRANS_HEADER";

                        }
                    }
                }
            }
            catch (Exception exc)
            {
                ds = null;
            }
            return ds;
        }
        /// </summary>
        /// <param name="codtda"></param>
        /// <returns></returns>
        public DataTable get_inv_valid_destinations_PE(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_INV_VALID_DESTINATIONS";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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

        public DataTable get_inv_valid_destinations_property_PE(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_INV_VALID_DESTINATIONS_PROPERTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_tender_repository_property_PE(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_TENDER_REPOSITORY_PROPERTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_manual_correlatives_PE(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_MANUAL_CORRELATIVES";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_electronic_correlatives_PE(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_ELECTRONIC_CORRELATIVES";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_state_county_PE(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_STATE_COUNTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_county_city_PE(string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_BCL_COUNTY_CITY";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        /// <param name="fecha"></param>
        /// <param name="codtda"></param>
        /// <returns></returns>
        public DataTable get_stock_ledger_PE(string fecha, string codtda, string pais)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_STOCK_LEDGER";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        /// </summary>
        /// <returns></returns>
        public DataTable get_Location_Property_PE(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_INV_LOCATION_PROPERTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        /// get PARTY
        /// </summary>
        /// <returns></returns>
        public DataTable get_Party_PE(string pais, string codtda, string strCodSupl, string strCodEmpl)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_PARTY";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        /// get de la talla - bata
        /// </summary>
        /// <returns></returns>
        public DataTable get_item_dimension_value_PE(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_DIMENSION_VALUE";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataSet get_retail_location_PE(string _cod_tda, string pais)
        {
            string sqlquery = "USP_XSTORE_GET_RETAIL_LOCATION";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
            catch (Exception exc)
            {
                ds = null;
            }
            return ds;
        }
        public DataTable get_price_update_2_PE(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_PRICE_UPDATE_2";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_merch_hier_PE(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_MERCH_HIER";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_item_images_PE(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_IMAGES";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        #endregion
        #region<ORCE>
        public DataTable ItemMaintenance_PE()
        {
            DataTable dt = null;
            string sqlquery = "USP_ORCE_ItemMaintenance";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable MerchandiseHierarch_PE()
        {
            DataTable dt = null;
            string sqlquery = "USP_ORCE_MerchandiseHierarchyMaintenance";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable OrcRetailLocations_PE()
        {
            DataTable dt = null;
            string sqlquery = "USP_ORCE_RetailLocations";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        #endregion
        #region<INTERFACES ECUADOR>
        #region<XOFICCE
        public DataTable get_item_EC(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_EC";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_price_update_2_EC(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_PRICE_UPDATE_2_ECU";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_merch_hier_EC(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_MERCH_HIER_ECU";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        public DataTable get_item_images_EC(string pais, string codtda)
        {
            DataTable dt = null;
            string sqlquery = "USP_XSTORE_GET_ITEM_EC";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
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
        #endregion
        #region<ORCE>

        #endregion
        #endregion
        #endregion
    }
}
