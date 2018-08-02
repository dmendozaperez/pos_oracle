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
   public class Dat_Scdremb
    {
        public Ent_MsgTransac insertar_Scdrem(Ent_List_Scdrem lista_scdrem)
        {
            string sqlquery = "[USP_INSERTAR_SCDREM]";
            Ent_MsgTransac msg = null;
            DataTable dt_scdrem = null;

            try
            {
                msg = new Ent_MsgTransac();
                dt_scdrem = ConvertListToDataTable(lista_scdrem);
                using (SqlConnection cn = new SqlConnection(Ent_Conexion.conexion_posperu))
                {
                    try
                    {
                        if (cn.State == 0) cn.Open();
                        using (SqlCommand cmd = new SqlCommand(sqlquery, cn))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@TMP_SCDREMB", dt_scdrem);
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



        private DataTable ConvertListToDataTable(Ent_List_Scdrem list)
        {
            // New table.
            DataTable table = null;
            try
            {
                table = new DataTable();
                table.Columns.Add("remb_guiac", typeof(string));
                table.Columns.Add("remb_artic", typeof(string));
                table.Columns.Add("remb_calid", typeof(string));
                table.Columns.Add("remb_medid", typeof(string));
                table.Columns.Add("remb_corra", typeof(string));
                table.Columns.Add("remb_canti", typeof(Decimal));
                table.Columns.Add("remb_almac", typeof(string));
                table.Columns.Add("remb_cpack", typeof(string));
                table.Columns.Add("remb_condm", typeof(string));
                table.Columns.Add("remb_rmed", typeof(string));
                table.Columns.Add("remb_u_med", typeof(string));
                table.Columns.Add("remb_categ", typeof(string));
                table.Columns.Add("remb_subca", typeof(string));
                table.Columns.Add("remb_clase", typeof(string));
                table.Columns.Add("remb_prvta", typeof(Decimal));
                table.Columns.Add("remb_costo", typeof(Decimal));
                table.Columns.Add("remb_talpr", typeof(string));
                table.Columns.Add("remb_plaoc", typeof(string));
                table.Columns.Add("remb_femba", typeof(string));//fecha date
                table.Columns.Add("remb_hemba", typeof(string));
                table.Columns.Add("remb_empre", typeof(string));
                table.Columns.Add("remb_secci", typeof(string));
                table.Columns.Add("remb_user", typeof(string));
                table.Columns.Add("remb_aassd", typeof(string));
                table.Columns.Add("remb_flag", typeof(string));
                table.Columns.Add("remb_secue", typeof(Decimal));
                table.Columns.Add("remb_estad", typeof(string));
                table.Columns.Add("remb_log", typeof(string));
                table.Columns.Add("remb_ftx", typeof(string));
                                                          

                foreach (var item in list.lista_scdremb)
                {
                    
                    table.Rows.Add(
                            item.remb_guiac,
                            item.remb_artic,
                            item.remb_calid,
                            item.remb_medid,
                            item.remb_corra,
                            item.remb_canti,
                            item.remb_almac,
                            item.remb_cpack,
                            item.remb_condm,
                            item.remb_rmed,
                            item.remb_u_med,
                            item.remb_categ,
                            item.remb_subca,
                            item.remb_clase,
                            item.remb_prvta,
                            item.remb_costo,
                            item.remb_talpr,
                            item.remb_plaoc,
                            item.remb_femba,
                            item.remb_hemba,
                            item.remb_empre,
                            item.remb_secci,
                            item.remb_user,
                            item.remb_aassd,
                            item.remb_flag,
                            item.remb_secue,
                            item.remb_estad,
                            item.remb_log,
                            item.remb_ftx
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
