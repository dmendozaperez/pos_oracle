using CapaEntidad.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDato.Venta
{
    public class Dat_Venta
    {
        /// <summary>
        /// insertar transaccion de ventas de tiendas
        /// </summary>
        /// <param name="tablas de transacciones de tda dataset"></param>
        /// <returns></returns>
        public Ent_MsgTransac inserta_venta(string cod_tda,DataSet ds_venta)
        {
            string sqlquery = "[USP_INSERTAR_VENTAS_TDA]";
            Ent_MsgTransac msg = null;
            try
            {
                msg = new Ent_MsgTransac();
                if (ds_venta.Tables.Count > 0)
                {
                    DataTable dt_ffc = new DataTable("FFACTC");
                    DataTable dt_ffd = new DataTable("FFACTD");
                    DataTable dt_not = new DataTable("FNOTAA");

                    dt_ffc = ds_venta.Tables[0];
                    dt_ffd = ds_venta.Tables[1];
                    dt_not = ds_venta.Tables[2];

                    using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                    {
                        try
                        {
                            if (cn.State == 0) cn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlquery,cn))
                            {
                                cmd.CommandTimeout = 0;
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@COD_TDA", cod_tda);
                                cmd.Parameters.AddWithValue("@TMP_FFC", dt_ffc);
                                cmd.Parameters.AddWithValue("@TMP_FFD", dt_ffd);
                                cmd.Parameters.AddWithValue("@TMP_NOT", dt_not);
                                cmd.ExecuteNonQuery();
                                msg.codigo = "0";
                                msg.descripcion = "Se actualizo correctamente";
                            }
                        }
                        catch (Exception exc)
                        {
                            msg.codigo = "1";
                            msg.descripcion = exc.Message;
                        }
                        if (cn != null)
                            if (cn.State == ConnectionState.Open) cn.Close();
                    }
                }
                else
                {
                    msg.codigo = "0";
                    msg.descripcion = "No hay trasacciones para actualizar";
                }
            }
            catch (Exception exc)
            {
                msg.codigo = "1";
                msg.descripcion = exc.Message;                
            }
            return msg;
        }
       
    }
}
