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
                                    dbf.rutloc_location_ecu= dr["RUTLOC_LOCATION_ECU"].ToString();


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
        /// <summary>
        /// tiempo de ejecucion de transmision
        /// </summary>
        /// <param name="codigo de transmision"></param>
        /// <returns></returns>
        public Ent_Config_Service get_config_service(string cser_cod)
        {
            Ent_Config_Service config = null;
            string sqlquery = "USP_CONFIG_SERTIVIO_TIENDA";
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
                            cmd.Parameters.AddWithValue("@CSER_COD", cser_cod);

                            cmd.Parameters.Add("@CSER_MIN", SqlDbType.Int);
                            cmd.Parameters["@CSER_MIN"].Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();

                            config = new Ent_Config_Service();
                            config.cser_min =Convert.ToInt32(cmd.Parameters["@CSER_MIN"].Value);
                        }
                    }
                    catch (Exception)
                    {
                        config = null;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            catch 
            {
                config = null;
            }
            return config;
        }
        /// <summary>
        /// ruta de paquete de venta 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Ent_PathDBF> get_ruta_locationProcesa_dbf(string tipo)
        {
            //string ruta = "";
            string sqlquery = "USP_GET_LOCATION_DBF";
            List<Ent_PathDBF> list = null;
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
                            cmd.Parameters.AddWithValue("@tipo_location", tipo);
                            SqlDataReader dr = cmd.ExecuteReader();



                            if (dr.HasRows)
                            {
                                list = new List<Ent_PathDBF>();
                                while (dr.Read())
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

                //ruta = list[0].rutloc_location;
            }
            catch (Exception)
            {
                list = null;
            }
            return list;//ruta;
        }
    }
}
