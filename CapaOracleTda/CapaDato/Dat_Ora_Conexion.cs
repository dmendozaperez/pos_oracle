
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaOracleTda
{
    public class Dat_Ora_Conexion
    {
        public Ent_Ora_Conexion get_conexion_ora(string tienda)
        {
            Ent_Ora_Conexion ora_conexion = null;
            string sqlquery = "USP_XSTORE_GET_ORA_TDA";
            try
            {
                using (SqlConnection cn = new SqlConnection(CapaEntidad.Util.Ent_Conexion.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@COD_TDA",tienda);

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                ora_conexion = new Ent_Ora_Conexion();
                                while(dr.Read())
                                {
                                    ora_conexion.tienda = dr["tienda"].ToString();
                                    ora_conexion.descripcion = dr["descripcion"].ToString();
                                    ora_conexion.server_ora = dr["server_ora"].ToString();
                                    ora_conexion.user_ora = dr["user_ora"].ToString();
                                    ora_conexion.pas_ora = dr["pas_ora"].ToString();
                                    ora_conexion.port_ora =Convert.ToInt32(dr["port_ora"]);
                                    ora_conexion.sid_ora = dr["sid_ora"].ToString();
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
            catch (Exception)
            {
                ora_conexion = null;                
            }
            return ora_conexion;
        }
    }
}
