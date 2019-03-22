using CapaServicioWindows.Conexion;
using CapaServicioWindows.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CapaServicioWindows.CapaDato.Novell
{
    public class Dat_Prescripcion
    {
        public string insertar_prescripcion(SCCCGUD list)
        {
            string sqlquery = "USP_INSERTAR_PRESCRIPCION";
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
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@cgud_gudis",list.cgud_gudis);
                            cmd.Parameters.AddWithValue("@cgud_tndcl", list.cgud_tndcl);
                            cmd.Parameters.AddWithValue("@cgud_calid", list.cgud_calid);
                            cmd.Parameters.AddWithValue("@cgud_empre", list.cgud_empre);
                            cmd.Parameters.AddWithValue("@cgud_canal", list.cgud_canal);
                            cmd.Parameters.AddWithValue("@cgud_caden", list.cgud_caden);
                            cmd.Parameters.AddWithValue("@cgud_almac", list.cgud_almac);
                            cmd.Parameters.AddWithValue("@cgud_secci", list.cgud_secci);
                            cmd.Parameters.AddWithValue("@cgud_estad", list.cgud_estad);
                            cmd.Parameters.AddWithValue("@cgud_ftnda", list.cgud_ftnda);
                            cmd.Parameters.AddWithValue("@cgud_nomtc", list.cgud_nomtc);
                            cmd.Parameters.AddWithValue("@cgud_ructc", list.cgud_ructc);
                            cmd.Parameters.AddWithValue("@cgud_vorca", list.cgud_vorca);
                            cmd.Parameters.AddWithValue("@cgud_vornc", list.cgud_vornc);
                            cmd.Parameters.AddWithValue("@cgud_unoca", list.cgud_unoca);
                            cmd.Parameters.AddWithValue("@cgud_unonc", list.cgud_unonc);
                            cmd.Parameters.AddWithValue("@cgud_uneca", list.cgud_uneca);
                            cmd.Parameters.AddWithValue("@cgud_unenc", list.cgud_unenc);
                            cmd.Parameters.AddWithValue("@cgud_ftx", list.cgud_ftx);
                            cmd.Parameters.AddWithValue("@cgud_dspch", list.cgud_dspch);
                            cmd.Parameters.AddWithValue("@cgud_ssd", list.cgud_ssd);
                            cmd.Parameters.AddWithValue("@cgud_semre", list.cgud_semre);
                            cmd.Parameters.AddWithValue("@cgud_anore", list.cgud_anore);
                            cmd.Parameters.AddWithValue("@cgud_frect", list.cgud_frect);
                            cmd.Parameters.AddWithValue("@cgud_fecre", list.cgud_fecre);
                            cmd.Parameters.AddWithValue("@cgud_scal", list.cgud_scal);
                            cmd.Parameters.AddWithValue("@cgud_scalm", list.cgud_scalm);
                            cmd.Parameters.AddWithValue("@cgud_sacc", list.cgud_sacc);
                            cmd.Parameters.AddWithValue("@cgud_saccm", list.cgud_saccm);
                            cmd.Parameters.AddWithValue("@cgud_ccal", list.cgud_ccal);
                            cmd.Parameters.AddWithValue("@cgud_ccalm", list.cgud_ccalm);
                            cmd.Parameters.AddWithValue("@cgud_cacc", list.cgud_cacc);
                            cmd.Parameters.AddWithValue("@cgud_caccm", list.cgud_caccm);
                            cmd.Parameters.AddWithValue("@cgud_caj", list.cgud_caj);
                            cmd.Parameters.AddWithValue("@cgud_cajm", list.cgud_cajm);
                            cmd.Parameters.AddWithValue("@cgud_ftda", list.cgud_ftda);
                            cmd.Parameters.AddWithValue("@cgud_subgr", list.cgud_subgr);
                            cmd.Parameters.AddWithValue("@cgud_ftxtd", list.cgud_ftxtd);
                            cmd.Parameters.AddWithValue("@cgud_ftxan", list.cgud_ftxan);
                            cmd.Parameters.AddWithValue("@cgud_ano", list.cgud_ano);
                            cmd.Parameters.AddWithValue("@cgud_seman", list.cgud_seman);
                            cmd.Parameters.AddWithValue("@cgud_user", list.cgud_user);
                            cmd.Parameters.AddWithValue("@cgud_femis", list.cgud_femis);
                            cmd.Parameters.AddWithValue("@cgud_hemis", list.cgud_hemis);
                            cmd.Parameters.AddWithValue("@cgud_conce", list.cgud_conce);
                            cmd.Parameters.AddWithValue("@cgud_flsf", list.cgud_flsf);
                            cmd.Parameters.AddWithValue("@cgud_aorig", list.cgud_aorig);
                            cmd.Parameters.AddWithValue("@cgud_pedid", list.cgud_pedid);
                            cmd.Parameters.AddWithValue("@cgud_deliv", list.cgud_deliv);
                            cmd.Parameters.AddWithValue("@log_ultmod", list.log_ultmod);
                            cmd.Parameters.AddWithValue("@TMP_DET", list.dt_SCDDGUD);
                            cmd.ExecuteNonQuery();                            
                        }
                    }
                    catch (Exception exc)
                    {
                        error=exc.Message;                        
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
