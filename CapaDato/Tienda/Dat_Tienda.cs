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

        public Boolean tienda_traspaso(string cod_tda)
        {
            string sqlquery = "USP_GET_ACCESO_TRASPADO_TDA";
            Boolean acceso = false;
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
                            cmd.Parameters.AddWithValue("@cod_tda", cod_tda);
                            cmd.Parameters.Add("@acceso_tras", SqlDbType.Bit);
                            cmd.Parameters["@acceso_tras"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            acceso =Convert.ToBoolean(cmd.Parameters["@acceso_tras"].Value);
                        }
                    }
                    catch
                    {

                        acceso = false;
                    }
                }
            }
            catch 
            {
                acceso = false;                
            }
            return acceso;
        }
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
