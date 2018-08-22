using CapaEntidad.Util;
using CapaEntidad.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDato.Interfaces
{
   public class Dat_Scactco
    {
        public Ent_MsgTransac insertar_Scactco(Ent_List_Scactco lista_scactco)
        {
            string sqlquery = "USP_INSERTAR_SCACTCO";
            Ent_MsgTransac msg = null;
            DataTable dt_scactco = null;

            try
            {
                msg = new Ent_MsgTransac();
                dt_scactco = ConvertListToDataTable(lista_scactco);
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TMP_SCACTCO", dt_scactco);
                            cmd.ExecuteNonQuery();
                            msg.codigo = "0";
                            msg.descripcion = "Se inserto correctamente";
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
            catch (Exception exc)
            {

                msg.codigo = "1";
                msg.descripcion = exc.Message;
            }
            return msg;
        }



        private DataTable ConvertListToDataTable(Ent_List_Scactco list)
        {
            // New table.
            DataTable table = null;
            try
            {
                table = new DataTable();
                table.Columns.Add("ctco_talpr", typeof(string));
                table.Columns.Add("ctco_plaoc", typeof(string));
                table.Columns.Add("ctco_artic", typeof(string));
                table.Columns.Add("ctco_calid", typeof(string));
                table.Columns.Add("ctco_plpr", typeof(string));
                table.Columns.Add("ctco_impr", typeof(string));
                table.Columns.Add("ctco_med00", typeof(string));
                table.Columns.Add("ctco_med01", typeof(string));
                table.Columns.Add("ctco_med02", typeof(string));
                table.Columns.Add("ctco_med03", typeof(string));
                table.Columns.Add("ctco_med04", typeof(string));
                table.Columns.Add("ctco_med05", typeof(string));
                table.Columns.Add("ctco_med06", typeof(string));
                table.Columns.Add("ctco_med07", typeof(string));
                table.Columns.Add("ctco_med08", typeof(string));
                table.Columns.Add("ctco_med09", typeof(string));
                table.Columns.Add("ctco_med10", typeof(string));
                table.Columns.Add("ctco_med11", typeof(string));
                table.Columns.Add("ctco_orige", typeof(string));
                table.Columns.Add("ctco_fecha", typeof(DateTime));
                table.Columns.Add("ctco_usern", typeof(string));
                table.Columns.Add("ctco_empre", typeof(string));
                table.Columns.Add("ctco_ftx", typeof(string));
                table.Columns.Add("ctco_txpos", typeof(string));
               
                                                          

                foreach (var item in list.lista_scactco)
                {
                    
                    table.Rows.Add(
                            item.ctco_talpr,
                            item.ctco_plaoc,
                            item.ctco_artic,
                            item.ctco_calid,
                            item.ctco_plpr,
                            item.ctco_impr,
                            item.ctco_med00,
                            item.ctco_med01,
                            item.ctco_med02,
                            item.ctco_med03,
                            item.ctco_med04,
                            item.ctco_med05,
                            item.ctco_med06,
                            item.ctco_med07,
                            item.ctco_med08,
                            item.ctco_med09,
                            item.ctco_med10,
                            item.ctco_med11,
                            item.ctco_orige,
                            item.ctco_fecha,
                            item.ctco_usern,
                            item.ctco_empre,
                            item.ctco_ftx,
                            item.ctco_txpos
                    );
                }
            }
            catch (Exception)
            {

                throw;
            }
            return table;
        }
    }
}
