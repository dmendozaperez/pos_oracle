using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.CapaDato.Novell
{
    public class Dat_Proc_Novell
    {
        /// <summary>
        /// get de envio para enviar a novell el paquete
        /// </summary>
        /// <returns></returns>
        public DataTable dt_get_envio_novell()
        {
            string sqlquery = "USP_EXTRAER_LISTA_ENVIO_XSTORE";
            //string sqlquery = "USP_EXTRAER_LISTA_ENVIO_XSTORE_CAJA_PRUEBA";
            //string sqlquery = "select tienda=Mc_Tda,fecha=Mc_FechaDoc from MOVIMIENTO_CAB group by Mc_Tda,Mc_FechaDoc order by Mc_FechaDoc asc";
            DataTable dt = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionSQL.conexion))
                {
                    try
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
                    catch (Exception exc)
                    {
                        dt = null;
                    }
                }
            }
            catch 
            {
                dt = null;                
            }
            return dt;
        }
        /// <summary>
        /// modificando la tabla para enviar el estado de enviado al novell
        /// </summary>
        /// <param name="cod_tda"></param>
        /// <param name="fec_cierre"></param>
        /// <returns></returns>
        public string update_system_envio(string cod_tda,DateTime fec_cierre)
        {
            string sqlquery = "USP_UPDATE_SYSTEM_STORE_ENVIO";
            string error = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@cod_tda", cod_tda);
                            cmd.Parameters.AddWithValue("@fec_cie", fec_cierre);
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
        public DataSet GET_OBTENER_VENTA_XSTORE(string cod_tda, DateTime fecha)
        {
            //string sqlquery = "USP_EXTRAER_VENTAS_TDA_PRUEBA_01";
            string sqlquery = "USP_EXTRAER_VENTAS_TDA";
            //string sqlquery = "[USP_EXTRAER_VENTAS_TDA_STOCK_SEMANA]";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion.ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                        cmd.Parameters.AddWithValue("@FEC_INI", fecha);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                            ds.Tables[0].TableName = "FFACTC";
                            ds.Tables[1].TableName = "FFACTD";
                            ds.Tables[2].TableName = "FNOTAA";
                            ds.Tables[3].TableName = "FSTKG";
                            ds.Tables[4].TableName = "FCIERR";
                            ds.Tables[5].TableName = "FFLASH";
                            ds.Tables[6].TableName = "FMC";
                            ds.Tables[7].TableName = "FMD";
                            ds.Tables[8].TableName = "FCACB";
                            ds.Tables[9].TableName = "FDECB";
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
    }
}
