using CapaServicioWindows.Conexion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.CapaDato.Venta
{
    public class Dat_Venta
    {
        /// <summary>
        /// procesar venta al sql por procedure
        /// </summary>
        /// <param name="error"></param>
        public void procesar_ventas_SQL(ref string error)
        {
            string sqlquery = "[USP_PROCESAR_VENTAS_TDA]";           
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
        
        }



    }
}
