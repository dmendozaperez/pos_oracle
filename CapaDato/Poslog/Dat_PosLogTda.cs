using CapaEntidad.Poslog;
using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Poslog
{
    public class Dat_PosLogTda
    {
        public string inserta_poslog_tda(Ent_PosLog_Tda pos)
        {
            string sqlquery = "USP_XSTORE_INSERTAR_POSLOG_TDA";
            string error = "";
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
                            cmd.Parameters.AddWithValue("@RTL_LOC_ID", pos.rtl_loc_id);
                            cmd.Parameters.AddWithValue("@WKSTN_ID", pos.wkstn_id);
                            cmd.Parameters.AddWithValue("@TRANS_SEQ", pos.trans_seq);
                            cmd.Parameters.AddWithValue("@BUSINESS_DATE", pos.business_date);
                            cmd.Parameters.AddWithValue("@NUMDOC", pos.numdoc);
                            cmd.Parameters.AddWithValue("@TOTAL", pos.total);
                            cmd.Parameters.AddWithValue("@DOCUMENT_TYPCODE", pos.document_typcode);
                            cmd.Parameters.AddWithValue("@POS_LOG_DATA", pos.pos_log_data);
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
