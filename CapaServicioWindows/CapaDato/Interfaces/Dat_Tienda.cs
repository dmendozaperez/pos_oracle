using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.CapaDato.Interfaces
{
    public class Dat_Tienda
    {
        public DataTable get_tienda_xstore(string pais)
        {
            DataTable dt = null;
            string sqlquery = "[USP_GET_XSTORE_TIENDA]";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PAIS", pais);
                        cmd.Parameters.AddWithValue("@xstore", "1");

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable();                                                      
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception)
            {
                dt = null;
            }
            return dt;
        }
    }
}
