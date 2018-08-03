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
    public class Dat_PosLog
    {
        public List<Ent_PosLog> get_poslog()
        {
            string sqlquery = "USP_GET_XSTORE_POSLOG";
            List<Ent_PosLog> lista = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                lista = new List<Ent_PosLog>();
                                lista = (from DataRow dr in dt.Rows
                                         select new Ent_PosLog
                                         {
                                             pos_log = dr["POS_LOG_DATA"].ToString(),
                                         }).ToList();
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
            catch (Exception)
            {
                lista = null;
            }
            return lista;
        }
    }
}
