using CapaServicioWindows_x64.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicioWindows_x64.Modular
{
    public class Util
    {
        public string get_ruta_locationProcesa_dbf(string name)
        {
            string ruta = "";
            string sqlquery = "USP_GET_LOCATION_DBF";
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@location_dbf", name);
                            SqlDataReader dr = cmd.ExecuteReader();



                            if (dr.HasRows)
                            {

                                while (dr.Read())
                                {
                                    ruta = dr["RUTLOC_LOCATION"].ToString();
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
                throw;
            }
            return ruta;
        }
    }
}
