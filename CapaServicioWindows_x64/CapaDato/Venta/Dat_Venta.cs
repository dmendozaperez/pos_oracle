using CapaServicioWindows_x64.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaServicioWindows_x64.CapaDato.Venta
{
    public class Dat_Venta
    {
        public DataSet procesar_listaGuia_ToXstore()
        {
            string sqlquery = "USP_LISTAR_GUIA_TOXSTORE";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ds = null;
            }

            return ds;
        }


        public DataSet get_inv_doc(string cod_alm, string nro_guia, string pais)
        {
            string sqlquery = (pais == "PE") ? "[USP_XSTORE_GET_INV_DOC]" : "[USP_XSTORE_GET_INV_DOC_ECU]";
            DataSet ds = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DESC_ALMAC", cod_alm);
                        cmd.Parameters.AddWithValue("@DESC_GUDIS", nro_guia);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                /*guias de traspasos*/
                                ds.Tables[0].TableName = "INV_DOC";
                                ds.Tables[1].TableName = "INV_DOC_LINE_ITEM";
                                ds.Tables[2].TableName = "CARTON";
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ds = null;
            }
            return ds;
        }

        public string Actualizar_Guia_ToXstore(string almac, string gudis, string tdes)
        {
            string sqlquery = "USP_ACTUALIZAR_GUIA_TOXSTORE";
            string error = "";
            try
            {

                using (SqlConnection cn = new SqlConnection(ConexionSQL.conexion))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 120;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ALMAC", almac);
                            cmd.Parameters.AddWithValue("@GUDIS", gudis);
                            cmd.Parameters.AddWithValue("@TDES", tdes);
                            cmd.ExecuteNonQuery();

                        }
                    }
                    catch (Exception exc)
                    {
                        error = exc.Message;
                    }
                    if (cn != null)
                        if (cn.State == ConnectionState.Open) cn.Close();
                }

            }
            catch (Exception exc)
            {
                error = exc.Message;
            }
            return error;
        }
    }
}
