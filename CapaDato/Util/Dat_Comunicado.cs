using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Util
{
    public class Dat_Comunicado
    {
        public string insert_comunicado(Ent_Comunicado obj)
        {
            string error = "";
            string sqlquery = "USP_COMUNICADO_INS_FILE";
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
                            cmd.Parameters.AddWithValue("@FILE_COD_TDA", obj.file_cod_tda);
                            cmd.Parameters.AddWithValue("@FILE_NOMBRE", obj.file_nombre);
                            cmd.Parameters.AddWithValue("@FILE_DESCRIPCION", obj.file_descripcion);
                            cmd.Parameters.AddWithValue("@FILE_FECHA_HORA_CRE", obj.file_fecha_hora_cre);
                            cmd.Parameters.AddWithValue("@FILE_FECHA_HORA_MOD", obj.file_fecha_hora_mod);
                            cmd.Parameters.AddWithValue("@FILE_USER_NOV", obj.file_user_nov);
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
