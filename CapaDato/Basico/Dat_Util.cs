using CapaEntidad.Util;
using System;
using CapaDato.Venta;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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

        public string get_Ruta_locationProcesa_dbf(string name)
        {
            string ruta = "";
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
                            cmd.Parameters.AddWithValue("@location_dbf", name);
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

                ruta = list[0].rutloc_location;
            }
            catch (Exception)
            {
                list = null;
            }
            return ruta;
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
        
        

        public string LeerDataDBF_TemporalVenta(string codTienda,string carpeta)
        {
            OleDbConnection cn = null;
            OleDbCommand cmd = null;
            OleDbDataAdapter da = null;
            DataTable dt_cab = null;
            DataTable dt_det = null;
            DataTable dt_pago = null;
            DataSet ds_transac_tda = new DataSet();
            string _venta_cab = "FFACTC";
            string _venta_det = "FFACTD";
            string _venta_pago = "FNOTAA";
            string strRespuesta = "S";
            string sqlquery = "";

            try
            {

                string cadena = Ent_Conexion.conexion_DBF_POS;
                cadena = cadena.Replace("XXXX", carpeta);
                cn = new OleDbConnection(cadena);

                sqlquery = "select * from " + _venta_cab;

                cmd = new OleDbCommand(sqlquery, cn);
                cmd.CommandTimeout = 0;
                da = new OleDbDataAdapter(cmd);
                dt_cab = new DataTable();
                da.Fill(dt_cab);

                if (dt_cab.Rows.Count > 0)
                {

                    sqlquery = "select * from " + _venta_det;
                    cmd = new OleDbCommand(sqlquery, cn);
                    cmd.CommandTimeout = 0;
                    da = new OleDbDataAdapter(cmd);
                    dt_det = new DataTable();
                    da.Fill(dt_det);

                    sqlquery = "select * from " + _venta_pago;
                    cmd = new OleDbCommand(sqlquery, cn);
                    cmd.CommandTimeout = 0;
                    da = new OleDbDataAdapter(cmd);
                    dt_pago = new DataTable();
                    da.Fill(dt_pago);


                    ds_transac_tda.Tables.Add(dt_cab);
                    ds_transac_tda.Tables.Add(dt_det);
                    ds_transac_tda.Tables.Add(dt_pago);

                    Dat_Venta update_venta = new Dat_Venta();
                    codTienda = "50" + codTienda;
                    Ent_MsgTransac msg_transac = null;

                    msg_transac = update_venta.inserta_venta(codTienda, ds_transac_tda);

                    if (msg_transac.codigo == "01") {
                        strRespuesta = "error del archivo dbf ==>" + carpeta + "<==>" + "50" + codTienda + "<==>.Detalle" + msg_transac.descripcion;

                    }
                }
               
            }
            catch (Exception ex) {

                strRespuesta = "N";
                strRespuesta = "error del archivo dbf ==>" + carpeta + "<==>" + "50" + codTienda + "<==>.Detalle" + ex.Message;
               
            }

            return strRespuesta;

        }

        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

    }
}
