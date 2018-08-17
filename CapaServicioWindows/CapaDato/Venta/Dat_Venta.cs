using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.CapaDato.Venta
{
    public class Dat_Venta
    {
        /// <summary>
        /// procesar venta al sql por procedure
        /// </summary>
        /// <param name="error"></param>
        public void procesar_ventas_SQL(ref string error)
        {
            string sqlquery = "[USP_PROCESAR_VENTAS_TDA]";           
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
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exc)
                    {
                        error = exc.Message;

                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
        
        }

        public string inserta_venta(string cod_tda,DataSet dsventa)
        {
            string sqlquery = "[USP_INSERTAR_VENTAS_TDA]";
            string error = "";      
            try
            {                

                if (dsventa.Tables.Count > 0)
                {
                    DataTable dt_ffc = dsventa.Tables[0];
                    DataTable dt_ffd = dsventa.Tables[1];
                    DataTable dt_not = dsventa.Tables[2];



                    using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                    {
                        try
                        {
                            if (cn.State == 0) cn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                            {
                                cmd.CommandTimeout = 120;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                                cmd.Parameters.AddWithValue("@TMP_FFC", dt_ffc);
                                cmd.Parameters.AddWithValue("@TMP_FFD", dt_ffd);
                                cmd.Parameters.AddWithValue("@TMP_NOT", dt_not);
                                cmd.ExecuteNonQuery();
                              
                            }
                        }
                        catch (Exception exc)
                        {
                            error = exc.Message;                            
                        }
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }               
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }

        public string inserta_venta_dbf(string cod_tda)
        {
            string sqlquery = "[USP_INSERTAR_VENTAS_DBF]";
            string error = "";
            try
            {
               
                    using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                    {
                        try
                        {
                            if (cn.State == 0) cn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                            {
                                cmd.CommandTimeout = 120;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);                              
                                cmd.ExecuteNonQuery();

                            }
                        }
                        catch (Exception exc)
                        {
                            error = exc.Message;
                        }
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
              
            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }

    }
}
