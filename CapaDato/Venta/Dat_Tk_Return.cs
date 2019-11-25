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
                        tk.estado_error = exc.Message;   
                    }
                    if (cn!= null)
                        if (cn.State==ConnectionState.Open) cn.Close();
                    
                }
            }
            catch (Exception exc)
            {
                tk.estado_error = exc.Message;
                
            }
            return tk;
        }
        public Ent_Tk_Get_Valores bata_get_tk_return(Ent_Tk_Get_Parametro param)
        {
            Ent_Tk_Get_Valores tk = null;
            string sqlquery = "USP_BATA_GET_TKRETURN";
            try
            {
                tk = new Ent_Tk_Get_Valores();
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
                            cmd.Parameters.AddWithValue("@CODIGO_CUPON", param.COD_CUP);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            { 
                                tk.CUP_RTN_BARRA            = dt.Rows[0]["CUP_RTN_BARRA"].ToString();
                                tk.CUP_RTN_TDA_GEN          = dt.Rows[0]["CUP_RTN_TDA_GEN"].ToString();
                                tk.CUP_RTN_FC_SUNA_GEN      = dt.Rows[0]["CUP_RTN_FC_SUNA_GEN"].ToString();
                                tk.CUP_RTN_SERIE_GEN        = dt.Rows[0]["CUP_RTN_SERIE_GEN"].ToString();
                                tk.CUP_RTN_NUMERO_GEN       = dt.Rows[0]["CUP_RTN_NUMERO_GEN"].ToString();
                                tk.CUP_RTN_FECHA_GEN        = dt.Rows[0]["CUP_RTN_FECHA_GEN"].ToString();
                                tk.CUP_RTN_MONTO_GEN        = Convert.ToDecimal(dt.Rows[0]["CUP_RTN_MONTO_GEN"]);
                                tk.CUP_RTN_TDA_USO          = dt.Rows[0]["CUP_RTN_TDA_USO"].ToString();
                                tk.CUP_RTN_FC_SUNA_USO      = dt.Rows[0]["CUP_RTN_FC_SUNA_USO"].ToString();
                                tk.CUP_RTN_SERIE_USO        = dt.Rows[0]["CUP_RTN_SERIE_USO"].ToString();
                                tk.CUP_RTN_NUMERO_USO       = dt.Rows[0]["CUP_RTN_NUMERO_USO"].ToString();
                                tk.CUP_RTN_FECHA_USO        = dt.Rows[0]["CUP_RTN_FECHA_USO"].ToString();
                                tk.CUP_RTN_TOTAL_USO        = Convert.ToDecimal(dt.Rows[0]["CUP_RTN_TOTAL_USO"]);
                                tk.CUP_RTN_FEC_INI_USO      = dt.Rows[0]["CUP_RTN_FEC_INI_USO"].ToString();
                                tk.CUP_RTN_FEC_FIN_USO      = dt.Rows[0]["CUP_RTN_FEC_FIN_USO"].ToString();
                                tk.CUP_RTN_MONTO_USO        = Convert.ToDecimal(dt.Rows[0]["CUP_RTN_MONTO_USO"]);
                                tk.CUP_RTN_ESTADO           = dt.Rows[0]["CUP_RTN_ESTADO"].ToString();
                                tk.CUP_RTN_FEC_ING          = dt.Rows[0]["CUP_RTN_FEC_ING"].ToString();
                                tk.CUP_RTN_FEC_ACT          = dt.Rows[0]["CUP_RTN_FEC_ACT"].ToString();
                                tk.CUP_RTN_LOG_ING          = dt.Rows[0]["CUP_RTN_LOG_ING"].ToString();
                                tk.CUP_RTN_LOG_UPD          = dt.Rows[0]["CUP_RTN_LOG_UPD"].ToString();
                                tk.CUP_RTN_IMP              = Convert.ToBoolean(dt.Rows[0]["CUP_RTN_IMP"]);
                                tk.CUP_RTN_IMP_LOG          = dt.Rows[0]["CUP_RTN_IMP_LOG"].ToString();
                            }
                            else
                            {
                                tk.estado_error = "No se encontró el cupon " + param.COD_CUP;
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        tk = new Ent_Tk_Get_Valores();
                        tk.estado_error = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();

                }
            }
            catch (Exception exc)
            {
                tk = new Ent_Tk_Get_Valores();
                tk.estado_error = exc.Message;

            }
            return tk;
        }
    }
}
