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
        #region<REGION ENVIO XSTORE>
        public string procesar_poslog()
        {
            string sqlquery = "[USP_PROCESAR_TEM_POS_LOG]";
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
            return error;
        }

        public string procesar_ventas_xstore()
        {
            string sqlquery = "USP_PROCESAR_VENTAS_XSTORE";
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
            return error;
        }

        public void procesar_ventas_movimiento()
        {
            string sqlquery = "[USP_PROCESAR_MOV_STOCK_VENTA_XSTORE]";
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
           // return error;
        }
        public void procesar_guias_movimiento()
        {
            string sqlquery = "[USP_PROCESAR_GUIAS_XSTORE]";
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
            //return error;
        }

        public DataSet procesar_listaEnvioXstore()
        {
            string sqlquery = "USP_EXTRAER_LISTA_ENVIO_XSTORE";
            DataSet ds = null;
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
                            ds = new DataSet();
                            da.Fill(ds);

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


        public DataSet GET_OBTENER_VENTA_XSTORE(string cod_tda, DateTime fecha)
        {
            string sqlquery = "USP_EXTRAER_VENTAS_TDA";
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
