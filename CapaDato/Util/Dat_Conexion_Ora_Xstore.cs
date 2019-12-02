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
    public class Dat_Conexion_Ora_Xstore
    {
        public Ent_Conexion_Ora_Xstore get_conexion_ora()
        {
            Ent_Conexion_Ora_Xstore con = null;
            string sqlquery = "USP_XSTORE_ACCESO_ORACLE_TDA";
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
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                con = new Ent_Conexion_Ora_Xstore();
                                while (dr.Read())
                                {
                                    con.server = dr["server"].ToString();
                                    con.usuario = dr["usuario"].ToString();
                                    con.password = dr["password"].ToString();
                                    con.port = Convert.ToInt32(dr["port"]);
                                    con.sid = dr["sid"].ToString();                                    
                                }
                            }
                        }
                    }
                    catch (Exception)
                    {
                        
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();

                }
            }
            catch 
            {
                con = null;                
            }
            return con;
        }
    }
}
