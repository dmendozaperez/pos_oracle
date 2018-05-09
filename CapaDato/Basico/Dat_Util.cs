using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Basico
{
    public class Dat_Util
    {
        /// <summary>
        /// Captura la ruta de los dbf 
        /// </summary>
        /// <returns></returns>
        public List<Ent_PathDBF> get_location_dbf()
        {
            string sqlquery = "USP_GET_LOCATION_DBF";
            List<Ent_PathDBF> list = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd=new SqlCommand(sqlquery,cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                list = new List<Ent_PathDBF>();
                                while(dr.Read())
                                {
                                    Ent_PathDBF dbf = new Ent_PathDBF();
                                    dbf.rutloc_namedbf = dr["RUTLOC_NAMEDBF"].ToString();
                                    dbf.rutloc_location = dr["RUTLOC_LOCATION"].ToString();
                                    list.Add(dbf);
                                }
                            }
                            

                        }   
                    }
                    catch (Exception)
                    {                       
                        list = null;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch (Exception)
            {
                list = null;                
            }
            return list;
        }
    }
}
