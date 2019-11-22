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

                            cmd.Parameters["@GENERA_CUPON"].Direction = ParameterDirection.Output;

                            cmd.Parameters["@CUPON_IMPRIMIR"].Direction = ParameterDirection.Output;

                            cmd.ExecuteNonQuery();


                            tk.genera_cupon =Convert.ToDecimal(cmd.Parameters["@GENERA_CUPON"].Value);
                            tk.cupon_imprimir = cmd.Parameters["@CUPON_IMPRIMIR"].Value.ToString();

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
    }
}
