using CapaServicioWindows.Conexion;
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
            catch (Exception)
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
