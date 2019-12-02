using CapaEntidad.Util;
using CapaEntidad.Venta;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Venta
{
    public class Dat_Tk_Return
    {

        public void bata_tk_return_imp_update(string cod_tda,string barra)
        {
            string sqlquery = "USP_USP_BATA_GET_TKRETURN_REIMPR_UPD";
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                            cmd.Parameters.AddWithValue("@BARRA", barra);
                            cmd.ExecuteNonQuery();

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

                
            }
        }

        /// <summary>
        /// reimprimir tickets retorno
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// 
        public List<Ent_Tk_Return> bata_tk_return_reimpimir(string tda)
        {
            List<Ent_Tk_Return> listar = null;
            string sqlquery = "USP_BATA_GET_TKRETURN_REIMPR";
            
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@COD_TDA", tda);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            listar = new List<Ent_Tk_Return>();
                            listar = (from DataRow dr in dt.Rows
                                      select new Ent_Tk_Return
                                      {
                                          cupon_imprimir = dr["CUP_RTN_BARRA"].ToString(),
                                          text1_cup = dr["TEX1_CUP"].ToString(),
                                          text2_cup = dr["TEX2_CUP"].ToString(),
                                          text3_cup = dr["TEX3_CUP"].ToString(),
                                          text4_cup = dr["TEX4_CUP"].ToString(),
                                      }).ToList();

                        }
                    }
                }
            }
            catch 
            {

                
            }
            return listar;

        }
        public Ent_Tk_Return bata_genera_tk_return(Ent_Tk_Set_Parametro param)
        {
            Ent_Tk_Return tk = null;
            string sqlquery = "USP_BATA_GET_GENERA_TKRETURN";
            try
            {
                tk = new Ent_Tk_Return();
                tk.estado_error = "";
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@COD_TDA", param.COD_TDA);
                            cmd.Parameters.AddWithValue("@FC_SUNA", param.FC_SUNA);
                            cmd.Parameters.AddWithValue("@SERIE", param.SERIE);
                            cmd.Parameters.AddWithValue("@NUMERO", param.NUMERO);
                            cmd.Parameters.AddWithValue("@MONTO", param.MONTO);
                            cmd.Parameters.AddWithValue("@FECHA", param.FECHA);

                            cmd.Parameters.Add("@GENERA_CUPON", SqlDbType.Decimal);
                            cmd.Parameters.Add("@CUPON_IMPRIMIR", SqlDbType.NVarChar, -1);
                            cmd.Parameters.Add("@TEX1_CUP", SqlDbType.NVarChar, -1);
                            cmd.Parameters.Add("@TEX2_CUP", SqlDbType.NVarChar, -1);
                            cmd.Parameters.Add("@TEX3_CUP", SqlDbType.NVarChar, -1);
                            cmd.Parameters.Add("@TEX4_CUP", SqlDbType.NVarChar, -1);

                            cmd.Parameters["@GENERA_CUPON"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@CUPON_IMPRIMIR"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@TEX1_CUP"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@TEX2_CUP"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@TEX3_CUP"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@TEX4_CUP"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();


                            tk.genera_cupon =Convert.ToDecimal(cmd.Parameters["@GENERA_CUPON"].Value);
                            tk.cupon_imprimir = cmd.Parameters["@CUPON_IMPRIMIR"].Value.ToString();
                            tk.text1_cup = cmd.Parameters["@TEX1_CUP"].Value.ToString();
                            tk.text2_cup = cmd.Parameters["@TEX2_CUP"].Value.ToString();
                            tk.text3_cup = cmd.Parameters["@TEX3_CUP"].Value.ToString();
                            tk.text4_cup = cmd.Parameters["@TEX4_CUP"].Value.ToString();

                        }
                    }
                    catch (Exception exc)
                    {
                        tk.estado_error = exc.Message + " ERROR DE WS ==> bata_genera_tk_return";   
                    }
                    if (cn!= null)
                        if (cn.State==ConnectionState.Open) cn.Close();
                    
                }
            }
            catch (Exception exc)
            {
                tk.estado_error = exc.Message + " ERROR DE WS ==> bata_genera_tk_return";
                
            }
            return tk;
        }
        public Ent_Tk_Valores bata_valida_tk_return(Ent_Tk_Get_Parametro param)
        {
            Ent_Tk_Valores tk = null;
            string sqlquery = "USP_BATA_VALIDA_CUPON_RETORNO";
            try
            {
                tk = new Ent_Tk_Valores();
                tk.estado_error = "";
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        DataTable dt = new DataTable();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CUPON", param.COD_CUP);
                            cmd.Parameters.AddWithValue("@COD_TDA", param.COD_TDA);
                            int valida_cupon = 0;
                            string mensaje_cupon = "";
                            cmd.Parameters.Add("@VALIDA_CUPON",SqlDbType.Int);
                            cmd.Parameters.Add("@MENSAJE_CUPON",SqlDbType.VarChar,-1);
                            cmd.Parameters["@VALIDA_CUPON"].Direction = ParameterDirection.Output;
                            cmd.Parameters["@MENSAJE_CUPON"].Direction = ParameterDirection.Output;                           

                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);

                            valida_cupon = Convert.ToInt32(cmd.Parameters["@VALIDA_CUPON"].Value);
                            mensaje_cupon = cmd.Parameters["@MENSAJE_CUPON"].Value.ToString();

                            tk.valida_cupon = valida_cupon.ToString();
                            tk.estado_error = mensaje_cupon;

                            if (valida_cupon == 1)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    tk.CUP_RTN_BARRA = dt.Rows[0]["CUP_RTN_BARRA"].ToString();
                                    tk.CUP_RTN_FEC_INI_USO = dt.Rows[0]["CUP_RTN_FEC_INI_USO"].ToString();
                                    tk.CUP_RTN_FEC_FIN_USO = dt.Rows[0]["CUP_RTN_FEC_FIN_USO"].ToString();
                                    tk.MTO_USO_MIN = Convert.ToDecimal(dt.Rows[0]["MTO_USO_MIN"]);
                                    tk.MTO_DCTO = Convert.ToDecimal(dt.Rows[0]["MTO_DCTO"]);
                                    tk.CUP_RTN_ESTADO = dt.Rows[0]["CUP_RTN_ESTADO"].ToString();
                                }
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        tk = new Ent_Tk_Valores();
                        tk.valida_cupon = "0";
                        tk.estado_error = "Error catch0 ws => " + exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();

                }
            }
            catch (Exception exc)
            {
                tk = new Ent_Tk_Valores();
                tk.valida_cupon = "0";
                tk.estado_error = "Error catch1 ws => " + exc.Message;

            }
            return tk;
        }

        public Ent_Tk_Valores bata_consumo_tk_return(Ent_Tk_Get_Parametro param)
        {
            Ent_Tk_Valores tk = null;
            string sqlquery = "USP_BATA_UPDATE_CUPON_RETORNO";
            try
            {
                tk = new Ent_Tk_Valores();
                tk.estado_error = "";
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CUPON", param.COD_CUP);
                            cmd.Parameters.AddWithValue("@COD_TDA", param.COD_TDA);
                            cmd.Parameters.AddWithValue("@FC_SUNA", param.FC_SUNA);
                            cmd.Parameters.AddWithValue("@SERIE", param.SERIE);
                            cmd.Parameters.AddWithValue("@NUMERO", param.NUMERO);
                            cmd.Parameters.AddWithValue("@MONTO", param.MONTO);
                            cmd.Parameters.AddWithValue("@FECHA", param.FECHA);

                            int filas =  cmd.ExecuteNonQuery();

                            if (filas > 0)
                            {
                                tk.valida_cupon = "1";
                                tk.estado_error = "Cupon actualizado.";
                            }else
                            {
                                tk.valida_cupon = "0";
                                tk.estado_error = "Error al actualziar";
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        tk.valida_cupon = "0";
                        tk.estado_error = "Error catch0 ws => " + exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();

                }
            }
            catch (Exception exc)
            {
                tk.valida_cupon = "0";
                tk.estado_error = "Error catch0 ws => " + exc.Message;
            }
            return tk;
        }
    }
}
