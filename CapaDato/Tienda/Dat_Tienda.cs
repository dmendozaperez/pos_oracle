using System;
using CapaEntidad.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Tienda
{
    public class Dat_Tienda
    {
        public DataSet dsgettienda(ref string verror)
        {
            SqlConnection cn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            string sql = "USP_LEER_TIENDA";
            try
            {
                cn = new SqlConnection(Ent_Conexion.conexion_posperu);
                cmd = new SqlCommand(sql, cn);
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                verror = ex.Message;
                ds = null;
            }
            if (cn.State == ConnectionState.Open) cn.Close();
            return ds;
        }
    }
}
