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
    public class Dat_ProcXstore
    {
        public List<Ent_CarpetaUpload_Xstore> get_xs_carpeta_upload()
        {
            string sqlquery = "USP_XSTORE_GET_CARPETA_UPLOAD";
            List<Ent_CarpetaUpload_Xstore> list = null;
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
                                list = new List<Ent_CarpetaUpload_Xstore>();
                                while(dr.Read())
                                {
                                    Ent_CarpetaUpload_Xstore upl = new Ent_CarpetaUpload_Xstore();
                                    upl.pais = dr["PAIS"].ToString();
                                    upl.entorno= dr["ENTORNO"].ToString();
                                    upl.opcion= dr["OPCION"].ToString();
                                    upl.rut_upload= dr["RUT_UPLOAD"].ToString();
                                    upl.ftp_server= dr["FTP_SERVER"].ToString();
                                    upl.ftp_user = dr["FTP_USER"].ToString();
                                    upl.ftp_pass= dr["FTP_PASS"].ToString();
                                    upl.ftp_port=Convert.ToInt32(dr["FTP_PORT"]);
                                    upl.ftp_folder= dr["FTP_FOLDER"].ToString();
                                    upl.ftp_send= dr["FTP_SEND"].ToString();
                                    list.Add(upl);
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
            catch
            {
                list = null;
            }
            return list;
        }
    }
}
