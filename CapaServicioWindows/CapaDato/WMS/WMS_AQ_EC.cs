using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.CapaDato.WMS
{
    public class WMS_AQ_EC
    {
        public string WMS_Proc_AQ_EC(string canal)
        {
            /*canal aq es conexion AQUARELLA y EC ES ECCOMERCE*/
            string slquery = "USP_EXTREAR_PEDIDO_WMS";
            string error = "";
            try
            {
                using (SqlConnection cn = new SqlConnection((canal == "AQ" ? ConexionSQL.conexion_aq : ConexionSQL.conexion_ec)))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(slquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception exc)
                    {
                        error = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == System.Data.ConnectionState.Open) cn.Close();
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
